
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
using Tbl18Superclass = ATIS.WinUi.Models.Tbl18Superclass;

//    Tbl21ClassesViewModel Skriptdatum:  30.03.2023  18:32    

namespace ATIS.WinUi.ViewModels.Database;

public class Tbl21ClassesViewModel : ObservableObject
{

    #region [Private Data Members]

    private readonly IDataService _dataService;

    public ObservableCollection<Tbl21Class?> ClassItems
    {
        get;
    } = new();

    public ObservableCollection<Tbl24Subclass> SubclassItems
    {
        get;
    } = new();

    public ObservableCollection<Tbl18Superclass> SuperclassItems
    {
        get;
    } = new();

    public ObservableCollection<Tbl90Reference> ReferenceExpertItems
    {
        get;
    } = new();

    public ObservableCollection<Tbl90Reference> ReferenceSourceItems
    {
        get;
    } = new();

    public ObservableCollection<Tbl90Reference> ReferenceAuthorItems
    {
        get;
    } = new();

    public ObservableCollection<Tbl93Comment> CommentItems
    {
        get;
    } = new();

    private readonly AllDialogs _allDialogs = new();

    #endregion [Private Data Members]

    #region [Constructor]

    public Tbl21ClassesViewModel(IDataService dataService)
    {
        _dataService = dataService;
        SelectedMainDetailTabIndex = 1; //Tab Datagrid
        GetAllCollections();
    }

    private void GetAllCollections()
    {
        Tbl21ClassesAllList ??= new ObservableCollection<Tbl21Class>();
        Tbl21ClassesAllList = _dataService.GetTbl21ClassesCollectionOrderByClassName();
        Tbl18SuperclassesAllList ??= new ObservableCollection<Tbl18Superclass>();
        Tbl18SuperclassesAllList = _dataService.GetTbl18SuperclassesCollectionOrderBySuperclassName();

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



    #region [Commands Class]

    public ICommand GetClassesByNameOrIdCommand => new RelayCommand(execute: delegate
    {
        var task = GetClassesByNameOrId_Executed(SearchClassName);
    });

    public ICommand AddClassCommand => new RelayCommand<string>(AddClass_Executed);
    public ICommand CopyClassCommand => new RelayCommand<string>(CopyClass_Executed);

    public ICommand DeleteClassCommand => new RelayCommand(execute: delegate
    {
        DeleteClass_Executed(SearchClassName);
    });

    public ICommand SaveClassCommand => new RelayCommand(execute: delegate
    {
        var task = SaveClass_Executed(SearchClassName);
    });

    public ICommand RefreshClassServerCommand => new RelayCommand(execute: delegate
    {
        RefreshClassServer_Executed(SearchClassName);
    });

    #endregion [Commands Class]

    #region [Methods Tbl21Class]

    private async Task GetClassesByNameOrId_Executed(string? parm)
    {
        IsLoading = true;
        ClassStartModify();
        Tbl18SuperclassesList?.Clear();
        Tbl21ClassesList?.Clear();
        Tbl24SubclassesList?.Clear();
        Tbl90ReferenceExpertsList?.Clear();
        Tbl90ReferenceSourcesList?.Clear();
        Tbl90ReferenceAuthorsList?.Clear();
        Tbl93CommentsList?.Clear();

        SuperclassItems.Clear();
        ClassItems.Clear();
        SubclassItems.Clear();
        ReferenceAuthorItems.Clear();
        ReferenceSourceItems.Clear();
        ReferenceExpertItems.Clear();
        CommentItems.Clear();

        Tbl21ClassesList ??= new ObservableCollection<Tbl21Class>();
        Tbl21ClassesList = await _dataService.GetTbl21ClassesCollectionOrderByClassNameFromSearchNameOrId(parm!);

        if (Tbl21ClassesList is { Count: 0 })
        {
            await _allDialogs.NoDatasetFoundInfoMessageDialogAsync();
            return;
        }

        ClassDataSetCount = Tbl21ClassesList.Count;
        RefreshClassItems();

        SelectedMainDetailTabIndex = 1;
        IsLoading = false;
    }

    private async void AddClass_Executed(string? parm)
    {
        ClassStartEdit();
        ClassStartNew();

        //Id search for first Dataset of Tbl18SuperclassesList
        var single = await _dataService.GetSuperclassSingleFirstDataset();
        var id = single.SuperclassId;

        Tbl21ClassesList ??= new ObservableCollection<Tbl21Class>();
        Tbl21ClassesList.Insert(index: 0, item: new Tbl21Class { ClassName = "New", SuperclassId = id });

        RefreshClassItems();
    }

    private async void CopyClass_Executed(string? parm)
    {
        ClassStartEdit();
        ClassStartNew();

        Tbl21ClassesList = await _dataService.CopyClass(ClassSelected);
        // evtl verbundene tabellen-Datensätze auch kopieren Expert, Source, Author und Comment

        RefreshClassItems();
    }

    private async void DeleteClass_Executed(string? parm)
    {

        if (await _allDialogs.DeleteDatasetQuestionConfirmationDialogAsync(ClassSelected!.ClassName!))
        {
            //necessary to delete before
            await _dataService.DeleteConnectedSubclasses(ClassSelected);
            await _dataService.DeleteConnectedClassReferences(ClassSelected);
            await _dataService.DeleteConnectedClassComments(ClassSelected);

            var ret = _dataService.DeleteClass(ClassSelected);
            if (!await ret)
            {
                return;
            }

            Tbl21ClassesList = await _dataService.GetTbl21ClassesCollectionOrderByClassNameFromSearchNameOrId(parm!);

            ClassDataSetCount = Tbl21ClassesList.Count;
            RefreshClassItems();
        }
    }

    private async Task SaveClass_Executed(string? parm)
    {
        if (string.IsNullOrEmpty(ClassSelected?.ClassName))
        {
            await _allDialogs.NameRequiredWarnMessageDialogAsync();
            return;
        }

        if (Tbl21ClassesList != null)
        {

            var iNdx = Tbl21ClassesList.IndexOf(Tbl21ClassesList.First(t =>
                t.ClassName == ClassSelected.ClassName));

            var ret = _dataService.SaveClass(ClassSelected);
            if (!await ret)
            {
                return;
            }

            if (string.IsNullOrEmpty(parm))
            {
                Tbl21ClassesList = await _dataService.GetLastDatasetInTbl21Classes();
                RefreshClassItems();
            }
            else
            {
                if (ClassSelected.ClassId == 0) //new
                {
                    Tbl21ClassesList = await _dataService.GetLastDatasetInTbl21Classes();
                    RefreshClassItems();
                }
                else
                {
                    Tbl21ClassesList =
                        await _dataService.GetTbl21ClassesCollectionOrderByClassNameFromSearchNameOrId(parm);
                    //   Index Position ?
                    if (iNdx < Tbl21ClassesList!.Count)
                    {
                        ClassItems.Clear();
                        foreach (var item in Tbl21ClassesList)
                        {
                            ClassItems.Add(item);
                        }

                        ClassSelected = Tbl21ClassesList[iNdx];
                    }
                }
            }
        }

        ClassDataSetCount = Tbl21ClassesList!.Count;
        ClassCancelEditsAsync();
    }

    private async void RefreshClassServer_Executed(string? parm)
    {
        Tbl21ClassesList = await _dataService.GetTbl21ClassesCollectionOrderByClassNameFromSearchNameOrId(parm!);

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

    #endregion [Methods Tbl21Class]




    //    Part 2    


    #region "Public Commands Connect <== Tbl18Superclass"

    public ICommand SaveSuperclassCommand => new RelayCommand<string>(SaveSuperclass_Executed);
    public ICommand RefreshSuperclassServerCommand => new RelayCommand(execute: delegate
       { RefreshSuperclassServer_Executed(); });

    private async void SaveSuperclass_Executed(string? parm)
    {
        if (Tbl18SuperclassesList.Count > 0)
        {
            if (string.IsNullOrEmpty(SuperclassSelected.SuperclassName))
            {
                await _allDialogs.NameRequiredWarnMessageDialogAsync();
                return;
            }

            if (SuperclassSelected == null)
            {
                await _allDialogs.NoDatasetSelectedWarnMessageDialogAsync();
                return;
            }
        }

        var ret = _dataService.SaveSuperclass(SuperclassSelected);

        if (!await ret)
        {
            return;
        }

        Tbl18SuperclassesList = _dataService.GetTbl18SuperclassesCollectionOrderBySuperclassNameFromSuperclassId(SuperclassSelected.SuperclassId);
        RefreshSuperclassItems();
        SuperclassCancelEditsAsync();
    }

    private Task RefreshSuperclassServer_Executed()
    {
        Tbl12SubphylumsAllList ??= new ObservableCollection<Tbl12Subphylum>();
        Tbl12SubphylumsAllList = _dataService.GetTbl12SubphylumsCollectionOrderBySubphylumName();

        Tbl15SubdivisionsAllList ??= new ObservableCollection<Tbl15Subdivision>();
        Tbl15SubdivisionsAllList = _dataService.GetTbl15SubdivisionsCollectionOrderBySubdivisionName();

        Tbl18SuperclassesList ??= new ObservableCollection<Tbl18Superclass>();
        Tbl18SuperclassesList =
            _dataService
                .GetTbl18SuperclassesCollectionOrderBySuperclassNameFromSuperclassId(ClassSelected.SuperclassId);
        SuperclassDataSetCount = Tbl18SuperclassesList.Count;

        RefreshSuperclassItems();
        return Task.CompletedTask;
    }

    public void SuperclassStartEdit() => IsInEdit = true;
    public void SuperclassStartModify() => IsModified = true;

    public void SuperclassCancelEditsAsync()
    {
        IsInEdit = false;
        IsModified = false;
    }

    #endregion "Public Commands"



    //    Part 3    





    //    Part 4    


    #region [Public Commands Connect ==> Tbl24Subclass]

    public ICommand AddSubclassCommand => new RelayCommand<string>(AddSubclass_Executed);
    public ICommand CopySubclassCommand => new RelayCommand<string>(CopySubclass_Executed);
    public ICommand DeleteSubclassCommand => new RelayCommand<string>(DeleteSubclass_Executed);
    public ICommand SaveSubclassCommand => new RelayCommand<string>(SaveSubclass_Executed);

    public ICommand RefreshSubclassServerCommand => new RelayCommand(execute: delegate
    {
        RefreshSubclassServer_Executed(SubclassSelected!.ClassId);
    });

    #endregion [Public Commands Connect ==> Tbl24Subclass]

    #region [Public Methods Connect ==> Tbl24Subclass]

    private void AddSubclass_Executed(string? parm)
    {
        SubclassStartEdit();
        SubclassStartNew();
        Tbl24SubclassesList.Insert(0, new Tbl24Subclass { SubclassName = "New", ClassId = ClassSelected.ClassId });

        SubclassItems.Clear();
        foreach (var item in Tbl24SubclassesList)
        {
            SubclassItems.Add(item);
        }

        SubclassSelected = SubclassItems.First();
    }

    private async void CopySubclass_Executed(string? parm)
    {
        SubclassStartEdit();
        SubclassStartNew();
        SubclassSelected.ClassId = ClassSelected.ClassId; //combo vorbelegen

        Tbl24SubclassesList = await _dataService.CopySubclass(SubclassSelected);
        // evtl verbundene tabellen-Datensätze auch kopieren Expert, Source, Author und Comment

        RefreshSubclassItems();
    }

    private async void DeleteSubclass_Executed(string? parm)
    {
        if (SubclassSelected == null)
        {
            await _allDialogs.NoDatasetSelectedWarnMessageDialogAsync();
            return;
        }

        var ret = _dataService.DeleteSubclass(SubclassSelected);
        if (!await ret)
        {
            return;
        }

        Tbl24SubclassesList =
            _dataService.GetTbl24SubclassesCollectionOrderBySubclassNameFromClassId(SubclassSelected.ClassId);

        SubclassDataSetCount = Tbl24SubclassesList.Count;
        RefreshSubclassItems();
    }

    private async void SaveSubclass_Executed(string? parm)
    {
        if (SubclassSelected != null && string.IsNullOrEmpty(SubclassSelected.SubclassName))
        {
            await _allDialogs.NameRequiredWarnMessageDialogAsync();
            return;
        }

        if (Tbl24SubclassesList != null)
        {
            var indx = Tbl24SubclassesList.IndexOf(Tbl24SubclassesList.First(t =>
                SubclassSelected != null && t.SubclassName == SubclassSelected.SubclassName));

            if (SubclassSelected != null)
            {
                if (ClassSelected != null)
                {
                    SubclassSelected.ClassId = ClassSelected.ClassId;

                    var ret = _dataService.SaveSubclass(SubclassSelected);
                    if (!await ret)
                    {
                        return;
                    }

                    if (SubclassSelected.SubclassId == 0) //new
                    {
                        Tbl24SubclassesList = await _dataService.GetLastDatasetInTbl24Subclasses();
                        RefreshSubclassItems();
                    }
                    else
                    {
                        Tbl24SubclassesList =
                            _dataService.GetTbl24SubclassesCollectionOrderBySubclassNameFromClassId(ClassSelected
                                .ClassId);
                        //   Index Position ?
                        if (indx < Tbl24SubclassesList.Count)
                        {
                            SubclassItems.Clear();
                            foreach (var item in Tbl24SubclassesList)
                            {
                                SubclassItems.Add(item);
                            }

                            SubclassSelected = Tbl24SubclassesList[indx]; //Index
                        }
                    }
                }
            }
        }

        SubclassDataSetCount = Tbl24SubclassesList!.Count;
        SubclassCancelEditsAsync();
    }

    private void RefreshSubclassServer_Executed(int id)
    {
        Tbl24SubclassesList ??= new ObservableCollection<Tbl24Subclass>();
        Tbl24SubclassesList = _dataService.GetTbl24SubclassesCollectionOrderBySubclassNameFromClassId(id);

        SubclassDataSetCount = Tbl24SubclassesList.Count;

        RefreshSubclassItems();
    }

    public void SubclassStartEdit() => IsInEdit = true;
    public void SubclassStartModify() => IsModified = true;
    public void SubclassStartNew() => IsNewSubclass = true;
    public event EventHandler AddNewSubclassCanceled = null!;

    public void SubclassCancelEditsAsync()
    {
        if (IsNewSubclass)
        {
            IsInEdit = false;
            AddNewSubclassCanceled?.Invoke(this, EventArgs.Empty);
            IsNewSubclass = false;
        }
        else
        {
            IsInEdit = false;
            IsModified = false;
        }
    }

    #endregion [Public Methods  Connect ==> Tbl24Subclass]



    //    Part 5    




    //    Part 6    




    //    Part 7    



    //    Part 8    


    #region [Commands Class ==> Tbl90Reference Author]

    public ICommand AddReferenceAuthorCommand => new RelayCommand<string>(AddReferenceAuthor_Executed);
    public ICommand CopyReferenceAuthorCommand => new RelayCommand<string>(CopyReferenceAuthor_Executed);
    public ICommand DeleteReferenceAuthorCommand => new RelayCommand<string>(DeleteReferenceAuthor_Executed);
    public ICommand SaveReferenceAuthorCommand => new RelayCommand<string>(SaveReferenceAuthor_Executed);

    public ICommand RefreshReferenceAuthorServerCommand =>
        new RelayCommand<string>(RefreshReferenceAuthorServer_Executed);

    #endregion [Commands Class ==> Tbl90Reference Author]

    #region [Methods Class ==> Tbl90Reference Author]

    private void AddReferenceAuthor_Executed(string? parm)
    {
        ReferenceAuthorStartEdit();
        ReferenceAuthorStartNew();
        Tbl90AuthorsAllList = _dataService.GetTbl90RefAuthorsCollectionOrderByRefAuthorNameAndBookNameAndPage1();

        Tbl90ReferenceAuthorsList.Insert(index: 0,
            item: new Tbl90Reference { Info = "New", ClassId = ClassSelected!.ClassId });

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
        ReferenceAuthorSelected.ClassId = ClassSelected.ClassId; //combo vorbelegen

        Tbl90ReferenceAuthorsList = await _dataService.CopyReferenceClass(ReferenceAuthorSelected, "Author");

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

        Tbl90ReferenceAuthorsList =
            _dataService
                .GetTbl90ReferenceAuthorsCollectionOrderByInfoFromClassIdAndRefSourceIdIsNullAndRefExpertIdIsNull(
                    ReferenceAuthorSelected.ClassId);
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

            if (ClassSelected != null)
            {
                ReferenceAuthorSelected.ClassId = ClassSelected.ClassId;
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
                Tbl90ReferenceAuthorsList =
                    _dataService
                        .GetTbl90ReferenceAuthorsCollectionOrderByInfoFromClassIdAndRefSourceIdIsNullAndRefExpertIdIsNull(
                            ReferenceAuthorSelected.ClassId);
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

        Tbl90ReferenceAuthorsList =
            _dataService
                .GetTbl90ReferenceAuthorsCollectionOrderByInfoFromClassIdAndRefSourceIdIsNullAndRefExpertIdIsNull(
                    ClassSelected.ClassId);

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

    #endregion [Methods Class ==> Tbl90Reference Author]

    #region [Commands Class ==> Tbl90Reference Source]

    public ICommand AddReferenceSourceCommand => new RelayCommand<string>(AddReferenceSource_Executed);
    public ICommand CopyReferenceSourceCommand => new RelayCommand<string>(CopyReferenceSource_Executed);
    public ICommand DeleteReferenceSourceCommand => new RelayCommand<string>(DeleteReferenceSource_Executed);
    public ICommand SaveReferenceSourceCommand => new RelayCommand<string>(SaveReferenceSource_Executed);

    public ICommand RefreshReferenceSourceServerCommand =>
        new RelayCommand<string>(RefreshReferenceSourceServer_Executed);

    #endregion [Commands Class ==> Tbl90Reference Source]

    #region [Methods Class ==> Tbl90Reference Source]

    private void AddReferenceSource_Executed(string? parm)
    {
        ReferenceSourceStartEdit();
        ReferenceSourceStartNew();
        Tbl90SourcesAllList = _dataService.GetTbl90RefSourcesCollectionOrderByRefSourceName();

        Tbl90ReferenceSourcesList.Insert(index: 0,
            item: new Tbl90Reference { Info = "New", ClassId = ClassSelected!.ClassId });
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
        if (ClassSelected != null)
        {
            ReferenceSourceSelected.ClassId = ClassSelected.ClassId; //combo vorbelegen
        }

        Tbl90ReferenceSourcesList = await _dataService.CopyReferenceClass(ReferenceSourceSelected, "Source");
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

        Tbl90ReferenceSourcesList =
            _dataService
                .GetTbl90ReferenceSourcesCollectionOrderByInfoFromClassIdAndRefAuthorIdIsNullAndRefExpertIdIsNull(
                    ReferenceSourceSelected.ClassId);
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

            if (ClassSelected != null)
            {
                ReferenceSourceSelected.ClassId = ClassSelected.ClassId;
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
                Tbl90ReferenceSourcesList =
                    _dataService
                        .GetTbl90ReferenceSourcesCollectionOrderByInfoFromClassIdAndRefAuthorIdIsNullAndRefExpertIdIsNull(
                            ReferenceSourceSelected.ClassId);
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

        Tbl90ReferenceSourcesList =
            _dataService
                .GetTbl90ReferenceSourcesCollectionOrderByInfoFromClassIdAndRefAuthorIdIsNullAndRefExpertIdIsNull(
                    ClassSelected.ClassId);

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

    #endregion [Methods Class ==> Tbl90Reference Source]

    #region [Commands Class ==> Tbl90Reference Expert]

    public ICommand AddReferenceExpertCommand => new RelayCommand<string>(AddReferenceExpert_Executed);
    public ICommand CopyReferenceExpertCommand => new RelayCommand<string>(CopyReferenceExpert_Executed);
    public ICommand DeleteReferenceExpertCommand => new RelayCommand<string>(DeleteReferenceExpert_Executed);
    public ICommand SaveReferenceExpertCommand => new RelayCommand<string>(SaveReferenceExpert_Executed);

    public ICommand RefreshReferenceExpertServerCommand =>
        new RelayCommand<string>(RefreshReferenceExpertServer_Executed);

    #endregion [Commands Class ==> Tbl90Reference Expert]

    #region [Methods Class ==> Tbl90Reference Expert]

    private void AddReferenceExpert_Executed(string? parm)
    {
        ReferenceExpertStartEdit();
        ReferenceExpertStartNew();
        Tbl90ExpertsAllList = _dataService.GetTbl90RefExpertsCollectionOrderByRefExpertName();

        Tbl90ReferenceExpertsList.Insert(index: 0,
            item: new Tbl90Reference { Info = "New", ClassId = ClassSelected!.ClassId });
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
        if (ClassSelected != null)
        {
            ReferenceExpertSelected.ClassId = ClassSelected.ClassId; //combo vorbelegen
        }

        Tbl90ReferenceExpertsList = await _dataService.CopyReferenceClass(ReferenceExpertSelected, "Expert");
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

        Tbl90ReferenceExpertsList =
            _dataService
                .GetTbl90ReferenceExpertsCollectionOrderByInfoFromClassIdAndRefAuthorIdIsNullAndRefSourceIdIsNull(
                    ReferenceExpertSelected.ClassId);
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
            if (ClassSelected != null)
            {
                ReferenceExpertSelected.ClassId = ClassSelected.ClassId;
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
                    _dataService
                        .GetTbl90ReferenceExpertsCollectionOrderByInfoFromClassIdAndRefAuthorIdIsNullAndRefSourceIdIsNull(
                            ReferenceExpertSelected.ClassId);
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

        Tbl90ReferenceExpertsList =
            _dataService
                .GetTbl90ReferenceExpertsCollectionOrderByInfoFromClassIdAndRefAuthorIdIsNullAndRefSourceIdIsNull(
                    ClassSelected.ClassId);

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

    #endregion [Methods Class ==> Tbl90Reference Expert]

    #region [Commands Class ==> Tbl93Comments]

    public ICommand AddCommentCommand => new RelayCommand<string>(AddComment_Executed);
    public ICommand CopyCommentCommand => new RelayCommand<string>(CopyComment_Executed);
    public ICommand DeleteCommentCommand => new RelayCommand<string>(DeleteComment_Executed);
    public ICommand SaveCommentCommand => new RelayCommand<string>(SaveComment_Executed);
    public ICommand RefreshCommentServerCommand => new RelayCommand<string>(RefreshCommentServer_Executed);

    #endregion [Commands Class ==> Tbl93Comments]


    #region [Methods Class ==> Tbl93Comments]

    private void AddComment_Executed(string? parm)
    {
        CommentStartEdit();
        CommentStartNew();
        Tbl93CommentsList.Insert(0, new Tbl93Comment { Info = "New", ClassId = ClassSelected!.ClassId });

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
        if (ClassSelected != null)
        {
            CommentSelected.ClassId = ClassSelected.ClassId; //combo vorbelegen
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

        Tbl93CommentsList = _dataService.GetTbl93CommentsCollectionOrderByInfoFromClassId(CommentSelected.ClassId);
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
                if (ClassSelected != null)
                {
                    CommentSelected.ClassId = ClassSelected.ClassId;

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
                        Tbl93CommentsList =
                            _dataService.GetTbl93CommentsCollectionOrderByInfoFromClassId(ClassSelected.ClassId);
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
        Tbl93CommentsList = _dataService.GetTbl93CommentsCollectionOrderByInfoFromClassId(ClassSelected.ClassId);

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

    #endregion [Methods Class ==> Tbl93Comments]


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

            _selectedMainDetailTabIndex = value;
            OnPropertyChanged(nameof(SelectedMainDetailTabIndex));

            if (_selectedMainDetailTabIndex == 0)
            {
                if (ClassSelected != null)
                {
                    IsLoading = true;
                    SuperclassStartModify();
                    Tbl12SubphylumsAllList = _dataService.GetTbl12SubphylumsCollectionOrderBySubphylumName();

                    Tbl15SubdivisionsAllList = _dataService.GetTbl15SubdivisionsCollectionOrderBySubdivisionName();

                    Tbl18SuperclassesList =
                        _dataService.GetTbl18SuperclassesCollectionOrderBySuperclassNameFromSuperclassId(ClassSelected
                            .SuperclassId);

                    SuperclassDataSetCount = Tbl18SuperclassesList.Count;
                    RefreshSuperclassItems();
                    IsLoading = false;
                }
            }

            if (_selectedMainDetailTabIndex == 1)
            {
            }

            if (_selectedMainDetailTabIndex == 2)
            {
                if (ClassSelected != null)
                {
                    IsLoading = true;
                    SubclassStartModify();
                    Tbl24SubclassesList =
                        _dataService.GetTbl24SubclassesCollectionOrderBySubclassNameFromClassId(ClassSelected.ClassId);

                    SubclassDataSetCount = Tbl24SubclassesList.Count;
                    RefreshSubclassItems();
                    IsLoading = false;
                }
            }

            if (_selectedMainDetailTabIndex == 3)
            {
                if (ClassSelected != null)
                {
                    IsLoading = true;
                    ReferenceExpertStartModify();
                    Tbl90ExpertsAllList = _dataService.GetTbl90RefExpertsCollectionOrderByRefExpertName();

                    Tbl90ReferenceExpertsList =
                        _dataService
                            .GetTbl90ReferenceExpertsCollectionOrderByInfoFromClassIdAndRefAuthorIdIsNullAndRefSourceIdIsNull(
                                ClassSelected.ClassId);

                    ReferenceExpertDataSetCount = Tbl90ReferenceExpertsList.Count;
                    RefreshReferenceExpertItems();
                    IsLoading = false;
                }
            }

            if (_selectedMainDetailTabIndex == 4)
            {
                if (ClassSelected != null)
                {
                    IsLoading = true;
                    CommentStartModify();
                    Tbl93CommentsList =
                        _dataService.GetTbl93CommentsCollectionOrderByInfoFromClassId(ClassSelected.ClassId);

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

            _selectedMainDetailRefTabIndex = value;
            OnPropertyChanged(nameof(SelectedMainDetailRefTabIndex));

            if (_selectedMainDetailRefTabIndex == 0)
            {
                if (ClassSelected != null)
                {
                    IsLoading = true;
                    ReferenceExpertStartModify();
                    Tbl90ExpertsAllList = _dataService.GetTbl90RefExpertsCollectionOrderByRefExpertName();

                    Tbl90ReferenceExpertsList =
                        _dataService
                            .GetTbl90ReferenceExpertsCollectionOrderByInfoFromClassIdAndRefAuthorIdIsNullAndRefSourceIdIsNull(
                                ClassSelected.ClassId);
                    ReferenceExpertDataSetCount = Tbl90ReferenceExpertsList.Count;
                    RefreshReferenceExpertItems();
                    IsLoading = false;
                }
            }

            if (_selectedMainDetailRefTabIndex == 1)
            {
                if (ClassSelected != null)
                {
                    IsLoading = true;
                    ReferenceSourceStartModify();
                    Tbl90SourcesAllList = _dataService.GetTbl90RefSourcesCollectionOrderByRefSourceName();

                    Tbl90ReferenceSourcesList =
                        _dataService
                            .GetTbl90ReferenceSourcesCollectionOrderByInfoFromClassIdAndRefAuthorIdIsNullAndRefExpertIdIsNull(
                                ClassSelected.ClassId);

                    ReferenceSourceDataSetCount = Tbl90ReferenceSourcesList.Count;
                    RefreshReferenceSourceItems();
                    IsLoading = false;
                }
            }

            if (_selectedMainDetailRefTabIndex == 2)
            {
                if (ClassSelected != null)
                {
                    IsLoading = true;
                    ReferenceAuthorStartModify();
                    Tbl90AuthorsAllList =
                        _dataService.GetTbl90RefAuthorsCollectionOrderByRefAuthorNameAndBookNameAndPage1();

                    Tbl90ReferenceAuthorsList =
                        _dataService
                            .GetTbl90ReferenceAuthorsCollectionOrderByInfoFromClassIdAndRefSourceIdIsNullAndRefExpertIdIsNull(
                                ClassSelected.ClassId);

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

    private Tbl18Superclass _superclassSelected = null!;

    public Tbl18Superclass SuperclassSelected
    {
        get => _superclassSelected;
        set => SetProperty(ref _superclassSelected, value);
    }

    private Tbl21Class _classSelected = null!;

    public Tbl21Class ClassSelected
    {
        get => _classSelected;
        set => SetProperty(ref _classSelected, value);
    }

    private Tbl24Subclass _subclassSelected = null!;

    public Tbl24Subclass SubclassSelected
    {
        get => _subclassSelected;
        set => SetProperty(ref _subclassSelected, value);
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

    public string SearchClassName
    {
        get;
        set;
    } = null!;

    private ObservableCollection<Tbl15Subdivision> _tbl15SubdivisionsAllList = null!;
    public ObservableCollection<Tbl15Subdivision> Tbl15SubdivisionsAllList
    {
        get => _tbl15SubdivisionsAllList;
        set
        {
            _tbl15SubdivisionsAllList = value; OnPropertyChanged();
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

    private ObservableCollection<Tbl18Superclass> _tbl18SuperclassesList = null!;

    public ObservableCollection<Tbl18Superclass> Tbl18SuperclassesList
    {
        get => _tbl18SuperclassesList;
        set
        {
            _tbl18SuperclassesList = value;
            OnPropertyChanged();
        }
    }

    private ObservableCollection<Tbl18Superclass>? _tbl18SuperclassesAllList;

    public ObservableCollection<Tbl18Superclass>? Tbl18SuperclassesAllList
    {
        get => _tbl18SuperclassesAllList;
        set
        {
            _tbl18SuperclassesAllList = value;
            OnPropertyChanged();
        }
    }

    private ObservableCollection<Tbl21Class> _tbl21ClassesList = null!;

    public ObservableCollection<Tbl21Class> Tbl21ClassesList
    {
        get => _tbl21ClassesList;
        set
        {
            _tbl21ClassesList = value;
            OnPropertyChanged();
        }
    }

    private ObservableCollection<Tbl21Class> _tbl21ClassesAllList = null!;

    public ObservableCollection<Tbl21Class> Tbl21ClassesAllList
    {
        get => _tbl21ClassesAllList;
        set
        {
            _tbl21ClassesAllList = value;
            OnPropertyChanged();
        }
    }

    private ObservableCollection<Tbl24Subclass> _tbl24SubclassesList = null!;

    public ObservableCollection<Tbl24Subclass> Tbl24SubclassesList
    {
        get => _tbl24SubclassesList;
        set
        {
            _tbl24SubclassesList = value;
            OnPropertyChanged();
        }
    }

    private ObservableCollection<Tbl90RefAuthor> _tbl90AuthorsAllList = null!;

    public ObservableCollection<Tbl90RefAuthor> Tbl90AuthorsAllList
    {
        get => _tbl90AuthorsAllList;
        set
        {
            _tbl90AuthorsAllList = value;
            OnPropertyChanged();
        }
    }

    private ObservableCollection<Tbl90RefSource> _tbl90SourcesAllList = null!;

    public ObservableCollection<Tbl90RefSource> Tbl90SourcesAllList
    {
        get => _tbl90SourcesAllList;
        set
        {
            _tbl90SourcesAllList = value;
            OnPropertyChanged();
        }
    }

    private ObservableCollection<Tbl90RefExpert> _tbl90ExpertsAllList = null!;

    public ObservableCollection<Tbl90RefExpert> Tbl90ExpertsAllList
    {
        get => _tbl90ExpertsAllList;
        set
        {
            _tbl90ExpertsAllList = value;
            OnPropertyChanged();
        }
    }

    private ObservableCollection<Tbl90Reference> _tbl90ReferenceAuthorsList = null!;

    public ObservableCollection<Tbl90Reference> Tbl90ReferenceAuthorsList
    {
        get => _tbl90ReferenceAuthorsList;
        set
        {
            _tbl90ReferenceAuthorsList = value;
            OnPropertyChanged();
        }
    }

    private ObservableCollection<Tbl90Reference> _tbl90ReferenceSourcesList = null!;

    public ObservableCollection<Tbl90Reference> Tbl90ReferenceSourcesList
    {
        get => _tbl90ReferenceSourcesList;
        set
        {
            _tbl90ReferenceSourcesList = value;
            OnPropertyChanged();
        }
    }

    private ObservableCollection<Tbl90Reference> _tbl90ReferenceExpertsList = null!;

    public ObservableCollection<Tbl90Reference> Tbl90ReferenceExpertsList
    {
        get => _tbl90ReferenceExpertsList;
        set
        {
            _tbl90ReferenceExpertsList = value;
            OnPropertyChanged();
        }
    }

    private ObservableCollection<Tbl93Comment> _tbl93CommentsList = null!;

    public ObservableCollection<Tbl93Comment> Tbl93CommentsList
    {
        get => _tbl93CommentsList;
        set
        {
            _tbl93CommentsList = value;
            OnPropertyChanged();
        }
    }


    //---------------------------------------------------------------------
    private int _superclassDataSetCount;

    public int SuperclassDataSetCount
    {
        get => _superclassDataSetCount;
        set
        {
            _superclassDataSetCount = value;
            OnPropertyChanged();
        }
    }

    private int _classDataSetCount;

    public int ClassDataSetCount
    {
        get => _classDataSetCount;
        set
        {
            _classDataSetCount = value;
            OnPropertyChanged();
        }
    }

    private int _subclassDataSetCount;

    public int SubclassDataSetCount
    {
        get => _subclassDataSetCount;
        set
        {
            _subclassDataSetCount = value;
            OnPropertyChanged();
        }
    }

    private int _referenceExpertDataSetCount;

    public int ReferenceExpertDataSetCount
    {
        get => _referenceExpertDataSetCount;
        set
        {
            _referenceExpertDataSetCount = value;
            OnPropertyChanged();
        }
    }

    private int _referenceSourceDataSetCount;

    public int ReferenceSourceDataSetCount
    {
        get => _referenceSourceDataSetCount;
        set
        {
            _referenceSourceDataSetCount = value;
            OnPropertyChanged();
        }
    }

    private int _referenceAuthorDataSetCount;

    public int ReferenceAuthorDataSetCount
    {
        get => _referenceAuthorDataSetCount;
        set
        {
            _referenceAuthorDataSetCount = value;
            OnPropertyChanged();
        }
    }

    private int _commentDataSetCount;

    public int CommentDataSetCount
    {
        get => _commentDataSetCount;
        set
        {
            _commentDataSetCount = value;
            OnPropertyChanged();
        }
    }


    //---------------------------------------------------------------------
    public bool IsModified
    {
        get;
        set;
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

    private bool _isNewClass;

    public bool IsNewClass
    {
        get => _isNewClass;
        set => SetProperty(ref _isNewClass, value);
    }

    private bool _isNewSubclass;

    public bool IsNewSubclass
    {
        get => _isNewSubclass;
        set => SetProperty(ref _isNewSubclass, value);
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
