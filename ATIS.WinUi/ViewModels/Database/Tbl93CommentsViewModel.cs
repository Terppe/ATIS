
using System.Collections.ObjectModel;
using ATIS.WinUi.Contracts.Services;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using ATIS.WinUi.Helpers;
using ATIS.WinUi.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml.Data;
using Tbl18Superclass = ATIS.WinUi.Models.Tbl18Superclass;

//    Tbl93CommentsViewModel Skriptdatum:  26.04.2023  10:32    

namespace ATIS.WinUi.ViewModels.Database;

public class Tbl93CommentsViewModel : ObservableObject
{

    #region [Private Data Members]
    private readonly IDataService _dataService = null!;
    public ObservableCollection<Tbl93Comment> CommentItems { get; } = new();

    private readonly AllDialogs _allDialogs = new();
    #endregion [Private Data Members]           

    #region [Constructor]
    public Tbl93CommentsViewModel(IDataService dataService)
    {
        _dataService = dataService;
        GetAllCollections();
    }

    private void GetAllCollections()
    {
        Tbl03RegnumsAllList ??= new ObservableCollection<Tbl03Regnum>();
        Tbl03RegnumsAllList = _dataService.GetTbl03RegnumsCollectionOrderByRegnumNameAndSubregnum();

        Tbl06PhylumsAllList ??= new ObservableCollection<Tbl06Phylum>();
        Tbl06PhylumsAllList = _dataService.GetTbl06PhylumsCollectionOrderByPhylumName();

        Tbl09DivisionsAllList ??= new ObservableCollection<Tbl09Division>();
        Tbl09DivisionsAllList = _dataService.GetTbl09DivisionsCollectionOrderByDivisionName();

        Tbl12SubphylumsAllList ??= new ObservableCollection<Tbl12Subphylum>();
        Tbl12SubphylumsAllList = _dataService.GetTbl12SubphylumsCollectionOrderBySubphylumName();

        Tbl15SubdivisionsAllList ??= new ObservableCollection<Tbl15Subdivision>();
        Tbl15SubdivisionsAllList = _dataService.GetTbl15SubdivisionsCollectionOrderBySubdivisionName();

        Tbl18SuperclassesAllList ??= new ObservableCollection<Tbl18Superclass>();
        Tbl18SuperclassesAllList = _dataService.GetTbl18SuperclassesCollectionOrderBySuperclassName();

        Tbl21ClassesAllList ??= new ObservableCollection<Tbl21Class>();
        Tbl21ClassesAllList = _dataService.GetTbl21ClassesCollectionOrderByClassName();

        Tbl24SubclassesAllList ??= new ObservableCollection<Tbl24Subclass>();
        Tbl24SubclassesAllList = _dataService.GetTbl24SubclassesCollectionOrderBySubclassName();

        Tbl27InfraclassesAllList ??= new ObservableCollection<Tbl27Infraclass>();
        Tbl27InfraclassesAllList = _dataService.GetTbl27InfraclassesCollectionOrderByInfraclassName();

        Tbl30LegiosAllList ??= new ObservableCollection<Tbl30Legio>();
        Tbl30LegiosAllList = _dataService.GetTbl30LegiosCollectionOrderByLegioName();

        Tbl33OrdosAllList ??= new ObservableCollection<Tbl33Ordo>();
        Tbl33OrdosAllList = _dataService.GetTbl33OrdosCollectionOrderByOrdoName();

        Tbl36SubordosAllList ??= new ObservableCollection<Tbl36Subordo>();
        Tbl36SubordosAllList = _dataService.GetTbl36SubordosCollectionOrderBySubordoName();

        Tbl39InfraordosAllList ??= new ObservableCollection<Tbl39Infraordo>();
        Tbl39InfraordosAllList = _dataService.GetTbl39InfraordosCollectionOrderByInfraordoName();

        Tbl42SuperfamiliesAllList ??= new ObservableCollection<Tbl42Superfamily>();
        Tbl42SuperfamiliesAllList = _dataService.GetTbl42SuperfamiliesCollectionOrderBySuperfamilyName();

        Tbl45FamiliesAllList ??= new ObservableCollection<Tbl45Family>();
        Tbl45FamiliesAllList = _dataService.GetTbl45FamiliesCollectionOrderByFamilyName();

        Tbl48SubfamiliesAllList ??= new ObservableCollection<Tbl48Subfamily>();
        Tbl48SubfamiliesAllList = _dataService.GetTbl48SubfamiliesCollectionOrderBySubfamilyName();

        Tbl51InfrafamiliesAllList ??= new ObservableCollection<Tbl51Infrafamily>();
        Tbl51InfrafamiliesAllList = _dataService.GetTbl51InfrafamiliesCollectionOrderByInfrafamilyName();

        Tbl54SupertribussesAllList ??= new ObservableCollection<Tbl54Supertribus>();
        Tbl54SupertribussesAllList = _dataService.GetTbl54SupertribussesCollectionOrderBySupertribusName();

        Tbl57TribussesAllList ??= new ObservableCollection<Tbl57Tribus>();
        Tbl57TribussesAllList = _dataService.GetTbl57TribussesCollectionOrderByTribusName();

        Tbl60SubtribussesAllList ??= new ObservableCollection<Tbl60Subtribus>();
        Tbl60SubtribussesAllList = _dataService.GetTbl60SubtribussesCollectionOrderBySubtribusName();

        Tbl63InfratribussesAllList ??= new ObservableCollection<Tbl63Infratribus>();
        Tbl63InfratribussesAllList = _dataService.GetTbl63InfratribussesCollectionOrderByInfratribusName();

        Tbl66GenussesAllList ??= new ObservableCollection<Tbl66Genus>();
        Tbl66GenussesAllList = _dataService.GetTbl66GenussesCollectionOrderByGenusName();

        Tbl69FiSpeciessesAllList ??= new ObservableCollection<Tbl69FiSpecies>();
        Tbl69FiSpeciessesAllList =
            _dataService.GetTbl69FiSpeciessesCollectionOrderByGenusNameAndFiSpeciesNameAndSubspeciesAndDivers();

        Tbl72PlSpeciessesAllList ??= new ObservableCollection<Tbl72PlSpecies>();
        Tbl72PlSpeciessesAllList =
            _dataService.GetTbl72PlSpeciessesCollectionOrderByGenusNameAndPlSpeciesNameAndSubspeciesAndDivers();
    }

    #endregion [Constructor]  


        //    Part 1    



    #region [Commands Comment]

        public ICommand GetCommentsByNameOrIdCommand => new RelayCommand(execute: delegate
        {
            var task = GetCommentsByNameOrId_Executed(searchName: SearchCommentInfoOrId);
        });

    public ICommand AddCommentCommand => new RelayCommand<string>(AddComment_Executed);
    public ICommand CopyCommentCommand => new RelayCommand<string>(CopyComment_Executed);
    public ICommand DeleteCommentCommand => new RelayCommand<string>(DeleteComment_Executed);

  //  public ICommand DeleteCommentCommand => new RelayCommand(delegate { var task = DeleteComment_Executed(null); });

    public ICommand SaveCommentCommand => new RelayCommand(delegate { var task = SaveComment_Executed(SearchCommentInfoOrId); });
    public ICommand RefreshCommentServerCommand => new RelayCommand(execute: delegate { var task = RefreshCommentServer_Executed(SearchCommentInfoOrId); });

    #endregion [Commands Comment]       

    #region [Methods Comment]

    private async Task GetCommentsByNameOrId_Executed(string searchName)
    {
        CommentStartModify();
        Tbl93CommentsList?.Clear();
        CommentItems.Clear();

        Tbl93CommentsList ??= new ObservableCollection<Tbl93Comment>();
        Tbl93CommentsList = await _dataService.GetTbl93CommentsCollectionOrderByInfoFromSearchInfoOrId(searchName);

        if (Tbl93CommentsList.Count == 0)
        {
            await _allDialogs.NoDatasetFoundInfoMessageDialogAsync();
            return;
        }
        CommentDataSetCount = Tbl93CommentsList.Count;
        RefreshCommentItems();
    }
    private  void AddComment_Executed(string? parm)
    {
        CommentStartEdit();
        CommentStartNew();

        Tbl93CommentsList ??= new ObservableCollection<Tbl93Comment>();
        Tbl93CommentsList.Insert(0, new Tbl93Comment { Info = "New" });

        RefreshCommentItems();
    }
    private async void CopyComment_Executed(string? parm)
    {
        CommentStartEdit();
        CommentStartNew();

        Tbl93CommentsList = await _dataService.CopyComment(CommentSelected);
        // evtl verbundene tabellen-Datensätze auch kopieren Expert, Source, Author und Comment

        RefreshCommentItems();
    }
    private async void DeleteComment_Executed(string? parm)
    {

        if (await _allDialogs.DeleteDatasetQuestionConfirmationDialogAsync(CommentSelected!.Info!))
        {
            //necessary to delete before
            var ret = _dataService.DeleteComment(CommentSelected);
            if (!await ret)
            {
                return;
            }

            Tbl93CommentsList = await _dataService.GetTbl93CommentsCollectionOrderByInfoFromSearchInfoOrId(parm!);

            CommentDataSetCount = Tbl93CommentsList.Count;
            RefreshCommentItems();
        }
    }
    private async Task SaveComment_Executed(string searchName)
    {
        if (string.IsNullOrEmpty(CommentSelected.Info))
        {
            await _allDialogs.NameRequiredWarnMessageDialogAsync();
            return;
        }
        if (CommentSelected == null)
        {
            await _allDialogs.NoDatasetSelectedWarnMessageDialogAsync();
            return;
        }
        CommentSelected ??= Tbl93CommentsList[0];
        var iNdx = Tbl93CommentsList.IndexOf(Tbl93CommentsList.First(t =>
             t.Info == CommentSelected.Info));

        var ret = _dataService.SaveComment(CommentSelected);
        if (!await ret)
        {
            return;
        }

        if (string.IsNullOrEmpty(searchName))
        {
            Tbl93CommentsList = await _dataService.GetLastDatasetInTbl93Comments();
            RefreshCommentItems();
        }
        else
        {
            if (CommentSelected.CommentId == 0) //new
            {
                Tbl93CommentsList = await _dataService.GetLastDatasetInTbl93Comments();
                RefreshCommentItems();
            }
            else
            {
                Tbl93CommentsList = await _dataService.GetTbl93CommentsCollectionOrderByInfoFromSearchInfoOrId(searchName);
                //   Index Position ?
                if (iNdx < Tbl93CommentsList.Count)
                {
                    CommentItems.Clear();
                    foreach (var item in Tbl93CommentsList)
                    {
                        CommentItems.Add(item);
                    }

                    CommentSelected = Tbl93CommentsList[iNdx];
                }
            }
        }
        CommentDataSetCount = Tbl93CommentsList.Count;
        CommentCancelEditsAsync();
    }
    private async Task RefreshCommentServer_Executed(string? parm)
    {
        Tbl93CommentsList ??= new ObservableCollection<Tbl93Comment>();
        Tbl93CommentsList = await _dataService.GetTbl93CommentsCollectionOrderByInfoFromSearchInfoOrId(parm!);

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
            AddNewCommentCanceled?.Invoke(this, EventArgs.Empty);
            IsInEdit = false;
            IsNewComment = false;
        }
        else
        {
            IsInEdit = false;
            IsModified = false;
        }
    }

    #endregion [Methods Comment]    




    //    Part 2    




    //    Part 3    





    //    Part 4    




    //    Part 5    




    //    Part 6    




    //    Part 7    



    //    Part 8    



    //    Part 9    



    //    Part 10    




    //    Part 11 


    #region All Properties


    #region Selected Properties


    private Tbl93Comment _commentSelected = null!;

    public Tbl93Comment CommentSelected
    {
        get => _commentSelected;
        set => SetProperty(ref _commentSelected, value);
    }


    #endregion

    #region Refresh Properties


    private void RefreshCommentItems()
    {
        CommentItems.Clear();
        foreach (var item in Tbl93CommentsList)
        {
            CommentItems.Add(item);
        }

        //     CommentSelected ??= CommentItems.First();
        if (CommentSelected == null && Tbl93CommentsList.Count != 0)
        {
            CommentSelected = CommentItems.First();
        }
    }
    #endregion Refresh Properties

    #region Public Properties  

    private int _commentDataSetCount;
    public int CommentDataSetCount
    {
        get => _commentDataSetCount;
        set
        {
            _commentDataSetCount = value; OnPropertyChanged();
        }
    }

    private string _searchCommentInfoOrId = "";

    public string SearchCommentInfoOrId
    {
        get => _searchCommentInfoOrId;
        set
        {
            _searchCommentInfoOrId = value; OnPropertyChanged(nameof(SearchCommentInfoOrId));
        }
    }

    private ObservableCollection<Tbl93Comment> _tbl93CommentsList = null!;

    public ObservableCollection<Tbl93Comment> Tbl93CommentsList
    {
        get => _tbl93CommentsList;
        set
        {
            _tbl93CommentsList = value; OnPropertyChanged(nameof(Tbl93CommentsList));
        }
    }

    private ObservableCollection<Tbl03Regnum>? _tbl03RegnumsAllList = null!;
    public ObservableCollection<Tbl03Regnum>? Tbl03RegnumsAllList
    {
        get => _tbl03RegnumsAllList;
        set
        {
            _tbl03RegnumsAllList = value; OnPropertyChanged(nameof(Tbl03RegnumsAllList));
        }
    }

    private ObservableCollection<Tbl06Phylum> _tbl06PhylumsAllList = null!;
    public ObservableCollection<Tbl06Phylum> Tbl06PhylumsAllList
    {
        get => _tbl06PhylumsAllList;
        set
        {
            _tbl06PhylumsAllList = value; OnPropertyChanged(nameof(Tbl06PhylumsAllList));
        }
    }

    private ObservableCollection<Tbl09Division> _tbl09DivisionsAllList = null!;
    public ObservableCollection<Tbl09Division> Tbl09DivisionsAllList
    {
        get => _tbl09DivisionsAllList;
        set
        {
            _tbl09DivisionsAllList = value; OnPropertyChanged(nameof(Tbl09DivisionsAllList));
        }
    }

    private ObservableCollection<Tbl12Subphylum> _tbl12SubphylumsAllList = null!;
    public ObservableCollection<Tbl12Subphylum> Tbl12SubphylumsAllList
    {
        get => _tbl12SubphylumsAllList;
        set
        {
            _tbl12SubphylumsAllList = value; OnPropertyChanged(nameof(Tbl12SubphylumsAllList));
        }
    }

    private ObservableCollection<Tbl15Subdivision> _tbl15SubdivisionsAllList = null!;
    public ObservableCollection<Tbl15Subdivision> Tbl15SubdivisionsAllList
    {
        get => _tbl15SubdivisionsAllList;
        set
        {
            _tbl15SubdivisionsAllList = value; OnPropertyChanged(nameof(Tbl15SubdivisionsAllList));
        }
    }

    private ObservableCollection<Tbl18Superclass> _tbl18SuperclassesAllList = null!;
    public ObservableCollection<Tbl18Superclass> Tbl18SuperclassesAllList
    {
        get => _tbl18SuperclassesAllList;
        set
        {
            _tbl18SuperclassesAllList = value; OnPropertyChanged(nameof(Tbl18SuperclassesAllList));
        }
    }

    private ObservableCollection<Tbl21Class> _tbl21ClassesAllList = null!;
    public ObservableCollection<Tbl21Class> Tbl21ClassesAllList
    {
        get => _tbl21ClassesAllList;
        set
        {
            _tbl21ClassesAllList = value; OnPropertyChanged(nameof(Tbl21ClassesAllList));
        }
    }

    private ObservableCollection<Tbl24Subclass> _tbl24SubclassesAllList = null!;
    public ObservableCollection<Tbl24Subclass> Tbl24SubclassesAllList
    {
        get => _tbl24SubclassesAllList;
        set
        {
            _tbl24SubclassesAllList = value; OnPropertyChanged(nameof(Tbl24SubclassesAllList));
        }
    }

    private ObservableCollection<Tbl27Infraclass> _tbl27InfraclassesAllList = null!;
    public ObservableCollection<Tbl27Infraclass> Tbl27InfraclassesAllList
    {
        get => _tbl27InfraclassesAllList;
        set
        {
            _tbl27InfraclassesAllList = value; OnPropertyChanged(nameof(Tbl27InfraclassesAllList));
        }
    }

    private ObservableCollection<Tbl30Legio> _tbl30LegiosAllList = null!;
    public ObservableCollection<Tbl30Legio> Tbl30LegiosAllList
    {
        get => _tbl30LegiosAllList;
        set
        {
            _tbl30LegiosAllList = value; OnPropertyChanged(nameof(Tbl30LegiosAllList));
        }
    }

    private ObservableCollection<Tbl33Ordo> _tbl33OrdosAllList = null!;
    public ObservableCollection<Tbl33Ordo> Tbl33OrdosAllList
    {
        get => _tbl33OrdosAllList;
        set
        {
            _tbl33OrdosAllList = value; OnPropertyChanged(nameof(Tbl33OrdosAllList));
        }
    }

    private ObservableCollection<Tbl36Subordo> _tbl36SubordosAllList = null!;
    public ObservableCollection<Tbl36Subordo> Tbl36SubordosAllList
    {
        get => _tbl36SubordosAllList;
        set
        {
            _tbl36SubordosAllList = value; OnPropertyChanged(nameof(Tbl36SubordosAllList));
        }
    }

    private ObservableCollection<Tbl39Infraordo> _tbl39InfraordosAllList = null!;
    public ObservableCollection<Tbl39Infraordo> Tbl39InfraordosAllList
    {
        get => _tbl39InfraordosAllList;
        set
        {
            _tbl39InfraordosAllList = value; OnPropertyChanged(nameof(Tbl39InfraordosAllList));
        }
    }

    private ObservableCollection<Tbl42Superfamily> _tbl42SuperfamiliesAllList = null!;
    public ObservableCollection<Tbl42Superfamily> Tbl42SuperfamiliesAllList
    {
        get => _tbl42SuperfamiliesAllList;
        set
        {
            _tbl42SuperfamiliesAllList = value; OnPropertyChanged(nameof(Tbl42SuperfamiliesAllList));
        }
    }

    private ObservableCollection<Tbl45Family> _tbl45FamiliesAllList = null!;
    public ObservableCollection<Tbl45Family> Tbl45FamiliesAllList
    {
        get => _tbl45FamiliesAllList;
        set
        {
            _tbl45FamiliesAllList = value; OnPropertyChanged(nameof(Tbl45FamiliesAllList));
        }
    }

    private ObservableCollection<Tbl48Subfamily> _tbl48SubfamiliesAllList = null!;
    public ObservableCollection<Tbl48Subfamily> Tbl48SubfamiliesAllList
    {
        get => _tbl48SubfamiliesAllList;
        set
        {
            _tbl48SubfamiliesAllList = value; OnPropertyChanged(nameof(Tbl48SubfamiliesAllList));
        }
    }

    private ObservableCollection<Tbl51Infrafamily> _tbl51InfrafamiliesAllList = null!;
    public ObservableCollection<Tbl51Infrafamily> Tbl51InfrafamiliesAllList
    {
        get => _tbl51InfrafamiliesAllList;
        set
        {
            _tbl51InfrafamiliesAllList = value; OnPropertyChanged(nameof(Tbl51InfrafamiliesAllList));
        }
    }

    private ObservableCollection<Tbl54Supertribus> _tbl54SupertribussesAllList = null!;
    public ObservableCollection<Tbl54Supertribus> Tbl54SupertribussesAllList
    {
        get => _tbl54SupertribussesAllList;
        set
        {
            _tbl54SupertribussesAllList = value; OnPropertyChanged(nameof(Tbl54SupertribussesAllList));
        }
    }

    private ObservableCollection<Tbl57Tribus> _tbl57TribussesAllList = null!;
    public ObservableCollection<Tbl57Tribus> Tbl57TribussesAllList
    {
        get => _tbl57TribussesAllList;
        set
        {
            _tbl57TribussesAllList = value; OnPropertyChanged(nameof(Tbl57TribussesAllList));
        }
    }

    private ObservableCollection<Tbl60Subtribus> _tbl60SubtribussesAllList = null!;
    public ObservableCollection<Tbl60Subtribus> Tbl60SubtribussesAllList
    {
        get => _tbl60SubtribussesAllList;
        set
        {
            _tbl60SubtribussesAllList = value; OnPropertyChanged(nameof(Tbl60SubtribussesAllList));
        }
    }

    private ObservableCollection<Tbl63Infratribus> _tbl63InfratribussesAllList = null!;
    public ObservableCollection<Tbl63Infratribus> Tbl63InfratribussesAllList
    {
        get => _tbl63InfratribussesAllList;
        set
        {
            _tbl63InfratribussesAllList = value; OnPropertyChanged(nameof(Tbl63InfratribussesAllList));
        }
    }

    private ObservableCollection<Tbl66Genus> _tbl66GenussesAllList = null!;
    public ObservableCollection<Tbl66Genus> Tbl66GenussesAllList
    {
        get => _tbl66GenussesAllList;
        set
        {
            _tbl66GenussesAllList = value; OnPropertyChanged(nameof(Tbl66GenussesAllList));
        }
    }

    private ObservableCollection<Tbl69FiSpecies> _tbl69FiSpeciessesAllList = null!;
    public ObservableCollection<Tbl69FiSpecies> Tbl69FiSpeciessesAllList
    {
        get => _tbl69FiSpeciessesAllList;
        set
        {
            _tbl69FiSpeciessesAllList = value; OnPropertyChanged(nameof(Tbl69FiSpeciessesAllList));
        }
    }

    private ObservableCollection<Tbl72PlSpecies> _tbl72PlSpeciessesAllList = null!;
    public ObservableCollection<Tbl72PlSpecies> Tbl72PlSpeciessesAllList
    {
        get => _tbl72PlSpeciessesAllList;
        set
        {
            _tbl72PlSpeciessesAllList = value; OnPropertyChanged(nameof(Tbl72PlSpeciessesAllList));
        }
    }
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

    private bool _isNewComment;
    public bool IsNewComment
    {
        get => _isNewComment;
        set => SetProperty(ref _isNewComment, value);
    }


    #endregion Public Properties

    #endregion All Properties








}
