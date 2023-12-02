using System.Collections.ObjectModel;
using ATIS.WinUi.Contracts.Services;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using ATIS.WinUi.Helpers;
using ATIS.WinUi.Models;
using CommunityToolkit.Mvvm.ComponentModel;

//    Tbl90RefAuthorsViewModel Skriptdatum:  24.04.2023  10:32    

namespace ATIS.WinUi.ViewModels.Database;

public class Tbl90RefAuthorsViewModel : ObservableObject
{

    #region [Private Data Members]
    private readonly IDataService _dataService = null!;
    public ObservableCollection<Tbl90RefAuthor> RefAuthorItems { get; } = new();

    private readonly AllDialogs _allDialogs = new();
    #endregion [Private Data Members]           

    #region [Constructor]
    public Tbl90RefAuthorsViewModel(IDataService dataService)
    {
        _dataService = dataService;
    }

    #endregion [Constructor]  


    //    Part 1    



    #region [Commands RefAuthor]

    public ICommand GetRefAuthorsByNameOrIdCommand => new RelayCommand(execute: delegate
    { var task = GetRefAuthorsByNameOrId_Executed(SearchRefAuthorName); });

    public ICommand AddRefAuthorCommand => new RelayCommand<string>(AddRefAuthor_Executed);
    public ICommand CopyRefAuthorCommand => new RelayCommand<string>(CopyRefAuthor_Executed);

    public ICommand DeleteRefAuthorCommand => new RelayCommand(execute: delegate { DeleteRefAuthor_Executed(SearchRefAuthorName); });

    public ICommand SaveRefAuthorCommand => new RelayCommand(execute: delegate { var task = SaveRefAuthor_Executed(SearchRefAuthorName); });
    public ICommand RefreshRefAuthorServerCommand => new RelayCommand(execute: delegate { RefreshRefAuthorServer_Executed(SearchRefAuthorName); });

    #endregion [Commands RefAuthor]       

    #region [Methods RefAuthor]

    private async Task GetRefAuthorsByNameOrId_Executed(string searchName)
    {
        RefAuthorStartModify();
        Tbl90RefAuthorsList?.Clear();

        RefAuthorItems.Clear();

        Tbl90RefAuthorsList ??= new ObservableCollection<Tbl90RefAuthor>();
        Tbl90RefAuthorsList = await _dataService.GetTbl90RefAuthorsCollectionOrderByRefAuthorNameFromSearchNameOrId(searchName);

        if (Tbl90RefAuthorsList.Count == 0)
        {
            await _allDialogs.NoDatasetFoundInfoMessageDialogAsync();
            return;
        }
        RefAuthorDataSetCount = Tbl90RefAuthorsList.Count;
        RefreshRefAuthorItems();
    }

    private void AddRefAuthor_Executed(string? parm)
    {
        RefAuthorStartEdit();
        RefAuthorStartNew();

        Tbl90RefAuthorsList ??= new ObservableCollection<Tbl90RefAuthor>();
        Tbl90RefAuthorsList.Insert(0, new Tbl90RefAuthor { RefAuthorName = "New" });

        RefreshRefAuthorItems();
    }

    private async void CopyRefAuthor_Executed(string? parm)
    {
        RefAuthorStartEdit();
        RefAuthorStartNew();

        Tbl90RefAuthorsList = await _dataService.CopyRefAuthor(RefAuthorSelected);
        // evtl verbundene tabellen-Datensätze auch kopieren Expert, Source, Author und Comment

        RefreshRefAuthorItems();
    }

    private async void DeleteRefAuthor_Executed(string? parm)
    {

        if (await _allDialogs.DeleteDatasetQuestionConfirmationDialogAsync(RefAuthorSelected!.RefAuthorName!))
        {
            //necessary to delete before
            var ret = _dataService.DeleteRefAuthor(RefAuthorSelected);
            if (!await ret)
            {
                return;
            }

            Tbl90RefAuthorsList = await _dataService.GetTbl90RefAuthorsCollectionOrderByRefAuthorNameFromSearchNameOrId(parm!);

            RefAuthorDataSetCount = Tbl90RefAuthorsList.Count;
            RefreshRefAuthorItems();
        }
    }

    private async Task SaveRefAuthor_Executed(string? parm)
    {
        if (string.IsNullOrEmpty(RefAuthorSelected?.RefAuthorName))
        {
            await _allDialogs.NameRequiredWarnMessageDialogAsync();
            return;
        }
        if (Tbl90RefAuthorsList != null)
        {

            var iNdx = Tbl90RefAuthorsList.IndexOf(Tbl90RefAuthorsList.First(t =>
                 t.RefAuthorName == RefAuthorSelected.RefAuthorName));

            var ret = _dataService.SaveRefAuthor(RefAuthorSelected);
            if (!await ret)
            {
                return;
            }

            if (string.IsNullOrEmpty(parm))
            {
                Tbl90RefAuthorsList = await _dataService.GetLastDatasetInTbl90RefAuthors();
                RefreshRefAuthorItems();
            }
            else
            {
                if (RefAuthorSelected.RefAuthorId == 0) //new
                {
                    Tbl90RefAuthorsList = await _dataService.GetLastDatasetInTbl90RefAuthors();
                    RefreshRefAuthorItems();
                }
                else
                {
                    Tbl90RefAuthorsList = await _dataService.GetTbl90RefAuthorsCollectionOrderByRefAuthorNameFromSearchNameOrId(parm);
                    //   Index Position ?
                    if (iNdx < Tbl90RefAuthorsList!.Count)
                    {
                        RefAuthorItems.Clear();
                        foreach (var item in Tbl90RefAuthorsList)
                        {
                            RefAuthorItems.Add(item);
                        }

                        RefAuthorSelected = Tbl90RefAuthorsList[iNdx];
                    }
                }
            }
        }
        RefAuthorDataSetCount = Tbl90RefAuthorsList!.Count;
        RefAuthorCancelEditsAsync();
    }

    private async void RefreshRefAuthorServer_Executed(string? parm)
    {
        Tbl90RefAuthorsList = await _dataService.GetTbl90RefAuthorsCollectionOrderByRefAuthorNameFromSearchNameOrId(parm!);

        RefAuthorDataSetCount = Tbl90RefAuthorsList.Count;
        RefreshRefAuthorItems();
    }

    public void RefAuthorStartEdit() => IsInEdit = true;
    public void RefAuthorStartModify() => IsModified = true;
    public void RefAuthorStartNew() => IsNewRefAuthor = true;
    public event EventHandler AddNewRefAuthorCanceled = null!;
    public void RefAuthorCancelEditsAsync()
    {
        if (IsNewRefAuthor)
        {
            IsInEdit = false;
            AddNewRefAuthorCanceled?.Invoke(this, EventArgs.Empty);
            IsNewRefAuthor = false;
        }
        else
        {
            IsInEdit = false;
            IsModified = false;
        }
    }

    #endregion [Methods Tbl90RefAuthor]    




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


    //    Part 11 

    #region All Properties


    #region Selected Properties


    private Tbl90RefAuthor _refAuthorSelected = null!;

    public Tbl90RefAuthor RefAuthorSelected
    {
        get => _refAuthorSelected;
        set => SetProperty(ref _refAuthorSelected, value);
    }


    #endregion

    #region Refresh Properties


    private void RefreshRefAuthorItems()
    {
        RefAuthorItems.Clear();
        foreach (var item in Tbl90RefAuthorsList)
        {
            RefAuthorItems.Add(item);
        }
        if (Tbl90RefAuthorsList.Count == 0)
        {
            return;
        }

        if (RefAuthorSelected == null && Tbl90RefAuthorsList.Count != 0)
        {
            RefAuthorSelected = RefAuthorItems.First();
        }
    }
    #endregion Refresh Properties

    #region Public Properties  

    private int _refAuthorDataSetCount;
    public int RefAuthorDataSetCount
    {
        get => _refAuthorDataSetCount;
        set
        {
            _refAuthorDataSetCount = value; OnPropertyChanged(nameof(RefAuthorDataSetCount));
        }
    }

    private string _searchRefAuthorName = "";

    public string SearchRefAuthorName
    {
        get => _searchRefAuthorName;
        set
        {
            _searchRefAuthorName = value; OnPropertyChanged(nameof(SearchRefAuthorName));
        }
    }

    private ObservableCollection<Tbl90RefAuthor> _tbl90RefAuthorsList = null!;

    public ObservableCollection<Tbl90RefAuthor> Tbl90RefAuthorsList
    {
        get => _tbl90RefAuthorsList;
        set
        {
            _tbl90RefAuthorsList = value; OnPropertyChanged(nameof(Tbl90RefAuthorsList));
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

    private bool _isNewRefAuthor;
    public bool IsNewRefAuthor
    {
        get => _isNewRefAuthor;
        set => SetProperty(ref _isNewRefAuthor, value);
    }



    #endregion Public Properties

    #endregion All Properties






}

