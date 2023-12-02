
using System.Collections.ObjectModel;
using ATIS.WinUi.Contracts.Services;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using ATIS.WinUi.Helpers;
using ATIS.WinUi.Models;
using ATIS.WinUi.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using Tbl18Superclass = ATIS.WinUi.Models.Tbl18Superclass;

//    Tbl18SuperclassesViewModel Skriptdatum:  29.03.2023  12:32    

namespace ATIS.WinUi.ViewModels.Database;

public class Tbl18SuperclassesViewModel : ObservableObject
{

    #region [Private Data Members]
    private readonly IDataService _dataService;
    public ObservableCollection<Tbl18Superclass?> SuperclassItems { get; } = new();
    public ObservableCollection<Tbl21Class> ClassItems { get; } = new();

    public ObservableCollection<Tbl12Subphylum> SubphylumItems { get; } = new();
    public ObservableCollection<Tbl15Subdivision> SubdivisionItems { get; } = new();

    public ObservableCollection<Tbl90Reference> ReferenceExpertItems { get; } = new();
    public ObservableCollection<Tbl90Reference> ReferenceSourceItems { get; } = new();
    public ObservableCollection<Tbl90Reference> ReferenceAuthorItems { get; } = new();
    public ObservableCollection<Tbl93Comment> CommentItems { get; } = new();
    private readonly AllDialogs _allDialogs = new();
    #endregion [Private Data Members]      

    #region [Constructor]
    public Tbl18SuperclassesViewModel(IDataService dataService)
    {
        _dataService = dataService;
        SelectedMainDetailTabIndex = 2; //Tab Datagrid
        GetAllCollections();
    }

    private void GetAllCollections()
    {
        Tbl06PhylumsAllList ??= new ObservableCollection<Tbl06Phylum>();
        Tbl06PhylumsAllList = _dataService.GetTbl06PhylumsCollectionOrderByPhylumName();
        Tbl09DivisionsAllList ??= new ObservableCollection<Tbl09Division>();
        Tbl09DivisionsAllList = _dataService.GetTbl09DivisionsCollectionOrderByDivisionName();

        Tbl12SubphylumsAllList ??= new ObservableCollection<Tbl12Subphylum>();
        Tbl12SubphylumsAllList = _dataService.GetTbl12SubphylumsCollectionOrderBySubphylumName();

        Tbl15SubdivisionsAllList ??= new ObservableCollection<Tbl15Subdivision>();
        Tbl15SubdivisionsAllList = _dataService.GetTbl15SubdivisionsCollectionOrderBySubdivisionName();

        Tbl90ExpertsAllList ??= new ObservableCollection<Tbl90RefExpert>();
        Tbl90ExpertsAllList = _dataService.GetTbl90RefExpertsCollectionOrderByRefExpertName();
        Tbl90SourcesAllList ??= new ObservableCollection<Tbl90RefSource>();
        Tbl90SourcesAllList = _dataService.GetTbl90RefSourcesCollectionOrderByRefSourceName();
        Tbl90AuthorsAllList ??= new ObservableCollection<Tbl90RefAuthor>();
        Tbl90AuthorsAllList = _dataService.GetTbl90RefAuthorsCollectionOrderByRefAuthorNameAndBookNameAndPage1();
    }

    #endregion [Constructor]  


    //    Part 1    



    #region [Commands Superclass]

    public ICommand GetSuperclassesByNameOrIdCommand => new RelayCommand(execute: delegate
    { var task = GetSuperclassesByNameOrId_Executed(SearchSuperclassName);});
    public ICommand AddSuperclassCommand => new RelayCommand<string>(AddSuperclass_Executed);
    public ICommand CopySuperclassCommand => new RelayCommand<string>(CopySuperclass_Executed);
    public ICommand DeleteSuperclassCommand => new RelayCommand(execute: delegate { DeleteSuperclass_Executed(SearchSuperclassName); });
    public ICommand SaveSuperclassCommand => new RelayCommand(execute: delegate { var task = SaveSuperclass_Executed(SearchSuperclassName); });
    public ICommand RefreshSuperclassServerCommand => new RelayCommand(execute: delegate { RefreshSuperclassServer_Executed(SearchSuperclassName); });

    #endregion [Commands Superclass]       

    #region [Methods Superclass]

    private async Task GetSuperclassesByNameOrId_Executed(string searchName)
    {
        SuperclassStartModify();
        Tbl12SubphylumsList?.Clear();
        Tbl15SubdivisionsList?.Clear();
        Tbl18SuperclassesList?.Clear();
        Tbl21ClassesList?.Clear();
        Tbl90ReferenceExpertsList?.Clear();
        Tbl90ReferenceSourcesList?.Clear();
        Tbl90ReferenceAuthorsList?.Clear();
        Tbl93CommentsList?.Clear();

        SubphylumItems.Clear();
        SubdivisionItems.Clear();
        SuperclassItems.Clear();
        ClassItems.Clear();
        ReferenceAuthorItems.Clear();
        ReferenceSourceItems.Clear();
        ReferenceExpertItems.Clear();
        CommentItems.Clear();

        Tbl18SuperclassesList ??= new ObservableCollection<Tbl18Superclass>();
        Tbl18SuperclassesList = await _dataService.GetTbl18SuperclassesCollectionOrderBySuperclassNameFromSearchNameOrId(searchName);

        if (Tbl18SuperclassesList.Count == 0)
        {
            await _allDialogs.NoDatasetFoundInfoMessageDialogAsync();
            return;
        }
        SuperclassDataSetCount = Tbl18SuperclassesList.Count;
        RefreshSuperclassItems();

        SelectedMainDetailTabIndex = 2;
    }

    private async void AddSuperclass_Executed(string? parm)
    {
        SuperclassStartEdit();
        SuperclassStartNew();
        //Id search for first Dataset of Tbl12SubphylumsList
        var id1 = 0;
        var single1 = await _dataService.GetSubphylumSingleFirstDataset();
        if (single1 != null)
        {
            id1 = single1.SubphylumId;
        }

        //Id search for first Dataset of Tbl15SubdivisionsList
        var id2 = 0;
        var single2 = await _dataService.GetSubdivisionSingleFirstDataset();
        if (single2 != null)
        {
            id2 = single2.SubdivisionId;
        }

        Tbl18SuperclassesList ??= new ObservableCollection<Tbl18Superclass>();
        Tbl18SuperclassesList.Insert(index: 0, item: new Tbl18Superclass { SuperclassName = "New", SubphylumId = id1, SubdivisionId = id2 });

        RefreshSuperclassItems();
    }

    private async void CopySuperclass_Executed(string? parm)
    {
        SuperclassStartEdit();
        SuperclassStartNew();

        Tbl18SuperclassesList = await _dataService.CopySuperclass(SuperclassSelected);
        // evtl verbundene tabellen-Datensätze auch kopieren Expert, Source, Author und Comment

        RefreshSuperclassItems();
    }

    private async void DeleteSuperclass_Executed(string? parm)
    {
        if (await _allDialogs.DeleteDatasetQuestionConfirmationDialogAsync(SuperclassSelected!.SuperclassName!))
        {
            //necessary to delete before
            await _dataService.DeleteConnectedClasses(SuperclassSelected);
            await _dataService.DeleteConnectedSuperclassReferences(SuperclassSelected);
            await _dataService.DeleteConnectedSuperclassComments(SuperclassSelected);

            var ret = _dataService.DeleteSuperclass(SuperclassSelected);
            if (!await ret)
            {
                return;
            }

            Tbl18SuperclassesList = await _dataService.GetTbl18SuperclassesCollectionOrderBySuperclassNameFromSearchNameOrId(parm!);

            SuperclassDataSetCount = Tbl18SuperclassesList.Count;
            RefreshSuperclassItems();
        }
    }

    private async Task SaveSuperclass_Executed(string? parm)
    {
        if (string.IsNullOrEmpty(SuperclassSelected?.SuperclassName))
        {
            await _allDialogs.NameRequiredWarnMessageDialogAsync();
            return;
        }
        if (Tbl18SuperclassesList != null)
        {

            var iNdx = Tbl18SuperclassesList.IndexOf(Tbl18SuperclassesList.First(t =>
                 t.SuperclassName == SuperclassSelected.SuperclassName));

            var ret = _dataService.SaveSuperclass(SuperclassSelected);
            if (!await ret)
            {
                return;
            }

            if (string.IsNullOrEmpty(parm))
            {
                Tbl18SuperclassesList = await _dataService.GetLastDatasetInTbl18Superclasses();
                RefreshSuperclassItems();
            }
            else
            {
                if (SuperclassSelected.SuperclassId == 0) //new
                {
                    Tbl18SuperclassesList = await _dataService.GetLastDatasetInTbl18Superclasses();
                    RefreshSuperclassItems();
                }
                else
                {
                    Tbl18SuperclassesList = await _dataService.GetTbl18SuperclassesCollectionOrderBySuperclassNameFromSearchNameOrId(parm);
                    //   Index Position ?
                    if (iNdx < Tbl18SuperclassesList!.Count)
                    {
                        SuperclassItems.Clear();
                        foreach (var item in Tbl18SuperclassesList)
                        {
                            SuperclassItems.Add(item);
                        }

                        SuperclassSelected = Tbl18SuperclassesList[iNdx];
                    }
                }
            }
        }
        SuperclassDataSetCount = Tbl18SuperclassesList!.Count;
        SuperclassCancelEditsAsync();
    }

    private async void RefreshSuperclassServer_Executed(string? parm)
    {
        Tbl18SuperclassesList = await _dataService.GetTbl18SuperclassesCollectionOrderBySuperclassNameFromSearchNameOrId(parm!);

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

    #endregion [Methods Tbl18Superclass]    




    //    Part 2    


    #region "Public Commands Connect <== Tbl12Subphylum"                 

    public ICommand SaveSubphylumCommand => new RelayCommand<string>(SaveSubphylum_Executed);
    public ICommand RefreshSubphylumServerCommand => new RelayCommand(execute: delegate { RefreshSubphylumServer_Executed(SearchSuperclassName); });

    private async void SaveSubphylum_Executed(string? parm)
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
        Tbl12SubphylumsList = await _dataService.GetTbl12SubphylumsCollectionOrderBySubphylumNameFromSearchNameOrId(parm);

        SubphylumDataSetCount = Tbl12SubphylumsList.Count;
        RefreshSubphylumItems();
    }
    public void SubphylumStartEdit() => IsInEdit = true;
    public void SubphylumStartModify() => IsModified = true;
    public void SubphylumCancelEditsAsync()
    {
        IsInEdit = false;
        IsModified = false;
    }

    #endregion "Public Commands"                  



    //    Part 3    


    #region "Public Commands Connect <== Tbl15Subdivision"                 

    public ICommand SaveSubdivisionCommand => new RelayCommand<string>(SaveSubdivision_Executed);
    public ICommand RefreshSubdivisionServerCommand => new RelayCommand(execute: delegate { RefreshSubdivisionServer_Executed(); });
    private async void SaveSubdivision_Executed(string? parm)
    {
        if (string.IsNullOrEmpty(SubdivisionSelected.SubdivisionName))
        {
            await _allDialogs.NameRequiredWarnMessageDialogAsync();
            return;
        }

        if (SubdivisionSelected == null)
        {
            await _allDialogs.NoDatasetSelectedWarnMessageDialogAsync();
            return;
        }
        var ret = _dataService.SaveSubdivision(SubdivisionSelected);

        if (!await ret)
        {
            return;
        }

        Tbl15SubdivisionsList = _dataService.GetTbl15SubdivisionsCollectionOrderBySubdivisionNameFromSubdivisionId(SuperclassSelected.SubdivisionId);
        RefreshSubdivisionItems();
        SubdivisionCancelEditsAsync();
    }

    private Task RefreshSubdivisionServer_Executed()
    {
        Tbl09DivisionsAllList ??= new ObservableCollection<Tbl09Division>();
        Tbl09DivisionsAllList = _dataService.GetTbl09DivisionsCollectionOrderByDivisionName();

        Tbl15SubdivisionsList ??= new ObservableCollection<Tbl15Subdivision>();
        Tbl15SubdivisionsList = _dataService.GetTbl15SubdivisionsCollectionOrderBySubdivisionNameFromSubdivisionId(SuperclassSelected.SubdivisionId);

        SubdivisionDataSetCount = Tbl15SubdivisionsList.Count;

        RefreshSubdivisionItems();
        return Task.CompletedTask;
    }
    public void SubdivisionStartEdit() => IsInEdit = true;
    public void SubdivisionStartModify() => IsModified = true;
    public void SubdivisionCancelEditsAsync()
    {
        IsInEdit = false;
        IsModified = false;
    }
    #endregion "Public Commands"                  




    //    Part 4    


    #region [Public Commands Connect ==> Tbl21Class]       

    public ICommand AddClassCommand => new RelayCommand<string>(AddClass_Executed);
    public ICommand CopyClassCommand => new RelayCommand<string>(CopyClass_Executed);
    public ICommand DeleteClassCommand => new RelayCommand<string>(DeleteClass_Executed);
    public ICommand SaveClassCommand => new RelayCommand<string>(SaveClass_Executed);
    public ICommand RefreshClassServerCommand => new RelayCommand(execute: delegate { RefreshClassServer_Executed(ClassSelected!.SuperclassId); });

    #endregion [Public Commands Connect ==> Tbl21Class]    

    #region [Public Methods Connect ==> Tbl21Class]                   

    private void AddClass_Executed(string? parm)
    {
        ClassStartEdit();
        ClassStartNew();
        Tbl21ClassesList.Insert(0, new Tbl21Class { ClassName = "New", SuperclassId = SuperclassSelected.SuperclassId });

        ClassItems.Clear();
        foreach (var item in Tbl21ClassesList)
        {
            ClassItems.Add(item);
        }
        ClassSelected = ClassItems.First();
    }

    private async void CopyClass_Executed(string? parm)
    {
        ClassStartEdit();
        ClassStartNew();
        ClassSelected.SuperclassId = SuperclassSelected.SuperclassId;  //combo vorbelegen

        Tbl21ClassesList = await _dataService.CopyClass(ClassSelected);
        // evtl verbundene tabellen-Datensätze auch kopieren Expert, Source, Author und Comment

        RefreshClassItems();
    }

    private async void DeleteClass_Executed(string? parm)
    {
        if (ClassSelected == null)
        {
            await _allDialogs.NoDatasetSelectedWarnMessageDialogAsync();
            return;
        }
        var ret = _dataService.DeleteClass(ClassSelected);
        if (!await ret)
        {
            return;
        }

        Tbl21ClassesList = _dataService.GetTbl21ClassesCollectionOrderByClassNameFromSuperclassId(ClassSelected.SuperclassId);

        ClassDataSetCount = Tbl21ClassesList.Count;
        RefreshClassItems();
    }

    private async void SaveClass_Executed(string? parm)
    {
        if (ClassSelected != null && string.IsNullOrEmpty(ClassSelected.ClassName))
        {
            await _allDialogs.NameRequiredWarnMessageDialogAsync();
            return;
        }
        if (Tbl21ClassesList != null)
        {
            var indx = Tbl21ClassesList.IndexOf(Tbl21ClassesList.First(t =>
              ClassSelected != null && t.ClassName == ClassSelected.ClassName));

            if (ClassSelected != null)
            {
                if (SuperclassSelected != null)
                {
                    ClassSelected.SuperclassId = SuperclassSelected.SuperclassId;

                    var ret = _dataService.SaveClass(ClassSelected);
                    if (!await ret)
                    {
                        return;
                    }

                    if (ClassSelected.ClassId == 0) //new
                    {
                        Tbl21ClassesList = await _dataService.GetLastDatasetInTbl21Classes();
                        RefreshClassItems();
                    }
                    else
                    {
                        Tbl21ClassesList = _dataService.GetTbl21ClassesCollectionOrderByClassNameFromSuperclassId(SuperclassSelected.SuperclassId);
                        //   Index Position ?
                        if (indx < Tbl21ClassesList.Count)
                        {
                            ClassItems.Clear();
                            foreach (var item in Tbl21ClassesList)
                            {
                                ClassItems.Add(item);
                            }

                            ClassSelected = Tbl21ClassesList[indx];  //Index
                        }
                    }
                }
            }
        }
        ClassDataSetCount = Tbl21ClassesList!.Count;
        ClassCancelEditsAsync();
    }

    private void RefreshClassServer_Executed(int id)
    {
        Tbl21ClassesList ??= new ObservableCollection<Tbl21Class>();
        Tbl21ClassesList = _dataService.GetTbl21ClassesCollectionOrderByClassNameFromSuperclassId(id);

        ClassDataSetCount = Tbl21ClassesList.Count;

        RefreshClassItems();
    }
    public void ClassStartEdit() => IsInEdit = true;
    public void ClassStartModify() => IsModified = true;
    public void ClassStartNew() => IsNewClass = true;
    public event EventHandler AddNewClassCanceled = null!;
    public void ClassCancelEditsAsync()
    {
        if (IsNewClass)
        {
            IsInEdit = false;
            AddNewClassCanceled?.Invoke(this, EventArgs.Empty);
            IsNewClass = false;
        }
        else
        {
            IsInEdit = false;
            IsModified = false;
        }
    }
    #endregion [Public Methods  Connect ==> Tbl21Class]                                                                                                                                            



    //    Part 5    




    //    Part 6    




    //    Part 7    



    //    Part 8    


    #region [Commands Superclass ==> Tbl90Reference Author]
    public ICommand AddReferenceAuthorCommand => new RelayCommand<string>(AddReferenceAuthor_Executed);
    public ICommand CopyReferenceAuthorCommand => new RelayCommand<string>(CopyReferenceAuthor_Executed);
    public ICommand DeleteReferenceAuthorCommand => new RelayCommand<string>(DeleteReferenceAuthor_Executed);
    public ICommand SaveReferenceAuthorCommand => new RelayCommand<string>(SaveReferenceAuthor_Executed);
    public ICommand RefreshReferenceAuthorServerCommand => new RelayCommand<string>(RefreshReferenceAuthorServer_Executed);
    #endregion [Commands Superclass ==> Tbl90Reference Author]                

    #region [Methods Superclass ==> Tbl90Reference Author]

    private void AddReferenceAuthor_Executed(string? parm)
    {
        ReferenceAuthorStartEdit();
        ReferenceAuthorStartNew();
        Tbl90AuthorsAllList = _dataService.GetTbl90RefAuthorsCollectionOrderByRefAuthorNameAndBookNameAndPage1();

        Tbl90ReferenceAuthorsList.Insert(index: 0, item: new Tbl90Reference { Info = "New", SuperclassId = SuperclassSelected!.SuperclassId });

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
        ReferenceAuthorSelected.SuperclassId = SuperclassSelected.SuperclassId; //combo vorbelegen

        Tbl90ReferenceAuthorsList = await _dataService.CopyReferenceSuperclass(ReferenceAuthorSelected, "Author");

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

        Tbl90ReferenceAuthorsList = _dataService.GetTbl90ReferenceAuthorsCollectionOrderByInfoFromSuperclassIdAndRefSourceIdIsNullAndRefExpertIdIsNull(ReferenceAuthorSelected.SuperclassId);
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

            if (SuperclassSelected != null)
            {
                ReferenceAuthorSelected.SuperclassId = SuperclassSelected.SuperclassId;
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
                Tbl90ReferenceAuthorsList = _dataService.GetTbl90ReferenceAuthorsCollectionOrderByInfoFromSuperclassIdAndRefSourceIdIsNullAndRefExpertIdIsNull(ReferenceAuthorSelected.SuperclassId);
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

        Tbl90ReferenceAuthorsList = _dataService.GetTbl90ReferenceAuthorsCollectionOrderByInfoFromSuperclassIdAndRefSourceIdIsNullAndRefExpertIdIsNull(SuperclassSelected.SuperclassId);

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
    #endregion [Methods Superclass ==> Tbl90Reference Author]                   

    #region [Commands Superclass ==> Tbl90Reference Source]  
    public ICommand AddReferenceSourceCommand => new RelayCommand<string>(AddReferenceSource_Executed);
    public ICommand CopyReferenceSourceCommand => new RelayCommand<string>(CopyReferenceSource_Executed);
    public ICommand DeleteReferenceSourceCommand => new RelayCommand<string>(DeleteReferenceSource_Executed);
    public ICommand SaveReferenceSourceCommand => new RelayCommand<string>(SaveReferenceSource_Executed);
    public ICommand RefreshReferenceSourceServerCommand => new RelayCommand<string>(RefreshReferenceSourceServer_Executed);
    #endregion [Commands Superclass ==> Tbl90Reference Source]         

    #region [Methods Superclass ==> Tbl90Reference Source]      

    private void AddReferenceSource_Executed(string? parm)
    {
        ReferenceSourceStartEdit();
        ReferenceSourceStartNew();
        Tbl90SourcesAllList = _dataService.GetTbl90RefSourcesCollectionOrderByRefSourceName();

        Tbl90ReferenceSourcesList.Insert(index: 0, item: new Tbl90Reference { Info = "New", SuperclassId = SuperclassSelected!.SuperclassId });
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
        if (SuperclassSelected != null)
        {
            ReferenceSourceSelected.SuperclassId = SuperclassSelected.SuperclassId; //combo vorbelegen
        }
        Tbl90ReferenceSourcesList = await _dataService.CopyReferenceSuperclass(ReferenceSourceSelected, "Source");
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

        Tbl90ReferenceSourcesList = _dataService.GetTbl90ReferenceSourcesCollectionOrderByInfoFromSuperclassIdAndRefAuthorIdIsNullAndRefExpertIdIsNull(ReferenceSourceSelected.SuperclassId);
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

            if (SuperclassSelected != null)
            {
                ReferenceSourceSelected.SuperclassId = SuperclassSelected.SuperclassId;
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
                Tbl90ReferenceSourcesList = _dataService.GetTbl90ReferenceSourcesCollectionOrderByInfoFromSuperclassIdAndRefAuthorIdIsNullAndRefExpertIdIsNull(ReferenceSourceSelected.SuperclassId);
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

        Tbl90ReferenceSourcesList = _dataService.GetTbl90ReferenceSourcesCollectionOrderByInfoFromSuperclassIdAndRefAuthorIdIsNullAndRefExpertIdIsNull(SuperclassSelected.SuperclassId);

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
    #endregion [Methods Superclass ==> Tbl90Reference Source]           

    #region [Commands Superclass ==> Tbl90Reference Expert]       
    public ICommand AddReferenceExpertCommand => new RelayCommand<string>(AddReferenceExpert_Executed);
    public ICommand CopyReferenceExpertCommand => new RelayCommand<string>(CopyReferenceExpert_Executed);
    public ICommand DeleteReferenceExpertCommand => new RelayCommand<string>(DeleteReferenceExpert_Executed);
    public ICommand SaveReferenceExpertCommand => new RelayCommand<string>(SaveReferenceExpert_Executed);
    public ICommand RefreshReferenceExpertServerCommand => new RelayCommand<string>(RefreshReferenceExpertServer_Executed);
    #endregion [Commands Superclass ==> Tbl90Reference Expert]                    

    #region [Methods Superclass ==> Tbl90Reference Expert]                 

    private void AddReferenceExpert_Executed(string? parm)
    {
        ReferenceExpertStartEdit();
        ReferenceExpertStartNew();
        Tbl90ExpertsAllList = _dataService.GetTbl90RefExpertsCollectionOrderByRefExpertName();

        Tbl90ReferenceExpertsList.Insert(index: 0, item: new Tbl90Reference { Info = "New", SuperclassId = SuperclassSelected!.SuperclassId });
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
        if (SuperclassSelected != null)
        {
            ReferenceExpertSelected.SuperclassId = SuperclassSelected.SuperclassId; //combo vorbelegen
        }
        Tbl90ReferenceExpertsList = await _dataService.CopyReferenceSuperclass(ReferenceExpertSelected, "Expert");
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

        Tbl90ReferenceExpertsList = _dataService.GetTbl90ReferenceExpertsCollectionOrderByInfoFromSuperclassIdAndRefAuthorIdIsNullAndRefSourceIdIsNull(ReferenceExpertSelected.SuperclassId);
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
            if (SuperclassSelected != null)
            {
                ReferenceExpertSelected.SuperclassId = SuperclassSelected.SuperclassId;
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
                Tbl90ReferenceExpertsList = _dataService.GetTbl90ReferenceExpertsCollectionOrderByInfoFromSuperclassIdAndRefAuthorIdIsNullAndRefSourceIdIsNull(ReferenceExpertSelected.SuperclassId);
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

        Tbl90ReferenceExpertsList = _dataService.GetTbl90ReferenceExpertsCollectionOrderByInfoFromSuperclassIdAndRefAuthorIdIsNullAndRefSourceIdIsNull(SuperclassSelected.SuperclassId);

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
    #endregion [Methods Superclass ==> Tbl90Reference Expert]                                 

    #region [Commands Superclass ==> Tbl93Comments]         
    public ICommand AddCommentCommand => new RelayCommand<string>(AddComment_Executed);
    public ICommand CopyCommentCommand => new RelayCommand<string>(CopyComment_Executed);
    public ICommand DeleteCommentCommand => new RelayCommand<string>(DeleteComment_Executed);
    public ICommand SaveCommentCommand => new RelayCommand<string>(SaveComment_Executed);
    public ICommand RefreshCommentServerCommand => new RelayCommand<string>(RefreshCommentServer_Executed);
    #endregion [Commands Superclass ==> Tbl93Comments]           

    #region [Methods Superclass ==> Tbl93Comments]        

    private void AddComment_Executed(string? parm)
    {
        CommentStartEdit();
        CommentStartNew();
        Tbl93CommentsList.Insert(0, new Tbl93Comment { Info = "New", SuperclassId = SuperclassSelected!.SuperclassId });

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
        if (SuperclassSelected != null)
        {
            CommentSelected.SuperclassId = SuperclassSelected.SuperclassId;  //combo vorbelegen
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

        Tbl93CommentsList = _dataService.GetTbl93CommentsCollectionOrderByInfoFromSuperclassId(CommentSelected.SuperclassId);
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
                if (SuperclassSelected != null)
                {
                    CommentSelected.SuperclassId = SuperclassSelected.SuperclassId;

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
                        Tbl93CommentsList = _dataService.GetTbl93CommentsCollectionOrderByInfoFromSuperclassId(SuperclassSelected.SuperclassId);
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
        Tbl93CommentsList = _dataService.GetTbl93CommentsCollectionOrderByInfoFromSuperclassId(SuperclassSelected.SuperclassId);

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
    #endregion [Methods Superclass ==> Tbl93Comments]                             


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
                if (SuperclassSelected != null)
                {
                    IsLoading = true;
                    SubphylumStartModify();
                    Tbl06PhylumsAllList = _dataService.GetTbl06PhylumsCollectionOrderByPhylumName();

                    Tbl12SubphylumsList = _dataService.GetTbl12SubphylumsCollectionOrderBySubphylumNameFromSubphylumId(SuperclassSelected.SubphylumId);

                    SubphylumDataSetCount = Tbl12SubphylumsList.Count;
                    RefreshSubphylumItems();
                    IsLoading = false;
                }
            }

            if (_selectedMainDetailTabIndex == 1)
            {
                if (SuperclassSelected != null)
                {
                    IsLoading = true;
                    SubdivisionStartModify();
                    Tbl09DivisionsAllList = _dataService.GetTbl09DivisionsCollectionOrderByDivisionName();

                    Tbl15SubdivisionsList = _dataService.GetTbl15SubdivisionsCollectionOrderBySubdivisionNameFromSubdivisionId(SuperclassSelected.SubdivisionId);

                    SubdivisionDataSetCount = Tbl15SubdivisionsList.Count;
                    RefreshSubdivisionItems();
                    IsLoading = false;
                }
            }

            if (_selectedMainDetailTabIndex == 2)
            {

            }

            if (_selectedMainDetailTabIndex == 3)
            {
                if (SuperclassSelected != null)
                {
                    IsLoading = true;
                    ClassStartModify();
                    Tbl21ClassesList = _dataService.GetTbl21ClassesCollectionOrderByClassNameFromSuperclassId(SuperclassSelected.SuperclassId);
                    ClassDataSetCount = Tbl21ClassesList.Count;
                    RefreshClassItems();
                    IsLoading = false;
                }
            }

            if (_selectedMainDetailTabIndex == 4)
            {
                if (SuperclassSelected != null)
                {
                    IsLoading = true;
                    ReferenceExpertStartModify();
                    Tbl90ExpertsAllList = _dataService.GetTbl90RefExpertsCollectionOrderByRefExpertName();

                    Tbl90ReferenceExpertsList = _dataService.GetTbl90ReferenceExpertsCollectionOrderByInfoFromSuperclassIdAndRefAuthorIdIsNullAndRefSourceIdIsNull(SuperclassSelected.SuperclassId);
                    ReferenceExpertDataSetCount = Tbl90ReferenceExpertsList.Count;
                    RefreshReferenceExpertItems();
                    IsLoading = false;
                }
            }
            if (_selectedMainDetailTabIndex == 5)
            {
                if (SuperclassSelected != null)
                {
                    IsLoading = true;
                    CommentStartModify();
                    Tbl93CommentsList = _dataService.GetTbl93CommentsCollectionOrderByInfoFromSuperclassId(SuperclassSelected.SuperclassId);
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
                if (SuperclassSelected != null)
                {
                    IsLoading = true;
                    ReferenceExpertStartModify();
                    Tbl90ExpertsAllList = _dataService.GetTbl90RefExpertsCollectionOrderByRefExpertName();

                    Tbl90ReferenceExpertsList = _dataService.GetTbl90ReferenceExpertsCollectionOrderByInfoFromSuperclassIdAndRefAuthorIdIsNullAndRefSourceIdIsNull(SuperclassSelected.SuperclassId);
                    ReferenceExpertDataSetCount = Tbl90ReferenceExpertsList.Count;
                    RefreshReferenceExpertItems();
                    IsLoading = false;
                }
            }

            if (_selectedMainDetailRefTabIndex == 1)
            {
                if (SuperclassSelected != null)
                {
                    IsLoading = true;
                    ReferenceSourceStartModify();
                    Tbl90SourcesAllList = _dataService.GetTbl90RefSourcesCollectionOrderByRefSourceName();

                    Tbl90ReferenceSourcesList = _dataService.GetTbl90ReferenceSourcesCollectionOrderByInfoFromSuperclassIdAndRefAuthorIdIsNullAndRefExpertIdIsNull(SuperclassSelected.SuperclassId);

                    ReferenceSourceDataSetCount = Tbl90ReferenceSourcesList.Count;
                    RefreshReferenceSourceItems();
                    IsLoading = false;
                }
            }

            if (_selectedMainDetailRefTabIndex == 2)
            {
                if (SuperclassSelected != null)
                {
                    IsLoading = true;
                    ReferenceAuthorStartModify();
                    Tbl90AuthorsAllList = _dataService.GetTbl90RefAuthorsCollectionOrderByRefAuthorNameAndBookNameAndPage1();

                    Tbl90ReferenceAuthorsList = _dataService.GetTbl90ReferenceAuthorsCollectionOrderByInfoFromSuperclassIdAndRefSourceIdIsNullAndRefExpertIdIsNull(SuperclassSelected.SuperclassId);

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

    private Tbl12Subphylum _subphylumSelected = null!;
    public Tbl12Subphylum SubphylumSelected
    {
        get => _subphylumSelected;
        set => SetProperty(ref _subphylumSelected, value);
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

    private Tbl21Class _classeSelected = null!;
    public Tbl21Class ClassSelected
    {
        get => _classeSelected;
        set => SetProperty(ref _classeSelected, value);
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
    public string SearchSuperclassName { get; set; } = null!;

    private ObservableCollection<Tbl12Subphylum> _tbl12SubphylumsList = null!;
    public ObservableCollection<Tbl12Subphylum> Tbl12SubphylumsList
    {
        get => _tbl12SubphylumsList;
        set
        {
            _tbl12SubphylumsList = value; OnPropertyChanged();
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

    private ObservableCollection<Tbl12Subphylum>? _tbl12SubphylumsAllList;
    public ObservableCollection<Tbl12Subphylum>? Tbl12SubphylumsAllList
    {
        get => _tbl12SubphylumsAllList;
        set
        {
            _tbl12SubphylumsAllList = value; OnPropertyChanged();
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


    private ObservableCollection<Tbl09Division> _tbl09DivisionsAllList = null!;
    public ObservableCollection<Tbl09Division> Tbl09DivisionsAllList
    {
        get => _tbl09DivisionsAllList;
        set
        {
            _tbl09DivisionsAllList = value; OnPropertyChanged();
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
    private ObservableCollection<Tbl18Superclass> _tbl18SuperclassesAllList = null!;
    public ObservableCollection<Tbl18Superclass> Tbl18SuperclassesAllList
    {
        get => _tbl18SuperclassesAllList;
        set
        {
            _tbl18SuperclassesAllList = value; OnPropertyChanged();
        }
    }

    private ObservableCollection<Tbl21Class> _tbl21ClassesList = null!;
    public ObservableCollection<Tbl21Class> Tbl21ClassesList
    {
        get => _tbl21ClassesList;
        set
        {
            _tbl21ClassesList = value; OnPropertyChanged();
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
    private int _subphylumDataSetCount;
    public int SubphylumDataSetCount
    {
        get => _subphylumDataSetCount;
        set
        {
            _subphylumDataSetCount = value; OnPropertyChanged();
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

    private int _classeDataSetCount;
    public int ClassDataSetCount
    {
        get => _classeDataSetCount;
        set
        {
            _classeDataSetCount = value; OnPropertyChanged();
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

    private bool _isNewSuperclass;
    public bool IsNewSuperclass
    {
        get => _isNewSuperclass;
        set => SetProperty(ref _isNewSuperclass, value);
    }
    private bool _isNewClass;
    public bool IsNewClass
    {
        get => _isNewClass;
        set => SetProperty(ref _isNewClass, value);
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

    private void RefreshClassItems()
    {
        ClassItems.Clear();
        if (Tbl21ClassesList != null)
        {
            foreach (var item in Tbl21ClassesList)
            {
                ClassItems.Add(item);
            }
            if (Tbl21ClassesList.Count == 0)
            {
                return;
            }

            if (ClassSelected == null && Tbl21ClassesList.Count != 0)
            {
                ClassSelected = ClassItems.FirstOrDefault()!;
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
   
