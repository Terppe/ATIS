using System.Collections.ObjectModel;
using ATIS.WinUi.Contracts.Services;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using ATIS.WinUi.Helpers;
using ATIS.WinUi.Models;
using CommunityToolkit.Mvvm.ComponentModel;

//    Tbl90RefExpertsViewModel Skriptdatum:  25.04.2023  10:32    

namespace ATIS.WinUi.ViewModels.Database;

public class Tbl90RefExpertsViewModel : ObservableObject
{

    #region [Private Data Members]
    private readonly IDataService _dataService;
    public ObservableCollection<Tbl90RefExpert> RefExpertItems { get; } = new();

    private readonly AllDialogs _allDialogs = new();
    #endregion [Private Data Members]      

    #region [Constructor]
    public Tbl90RefExpertsViewModel(IDataService dataService)
    {
        _dataService = dataService;
    }

    #endregion [Constructor]  


    //    Part 1    



    #region [Commands RefExpert]

    public ICommand GetRefExpertsByNameOrIdCommand => new RelayCommand(execute: delegate { var task = GetRefExpertsByNameOrId(searchName: SearchRefExpertName); });

    public ICommand AddRefExpertCommand => new RelayCommand<string>(AddRefExpert_Executed);
    public ICommand CopyRefExpertCommand => new RelayCommand<string>(CopyRefExpert_Executed);
    public ICommand DeleteRefExpertCommand => new RelayCommand<string>(DeleteRefExpert_Executed);

  //  public ICommand DeleteRefExpertCommand => new RelayCommand(execute: delegate { var task = DeleteRefExpert(SearchRefExpertName); });

    public ICommand SaveRefExpertCommand => new RelayCommand(execute: delegate { var task = SaveRefExpert_Executed(SearchRefExpertName); });
    public ICommand RefreshRefExpertServerCommand => new RelayCommand(execute: delegate { var task = RefreshRefExpertServer_Executed(SearchRefExpertName); });

    #endregion [Commands RefExpert]       

    #region [Methods RefExpert]

    private async Task GetRefExpertsByNameOrId(string searchName)
    {
        RefExpertStartModify();
        Tbl90RefExpertsList?.Clear();
        RefExpertItems.Clear();

        Tbl90RefExpertsList ??= new ObservableCollection<Tbl90RefExpert>();
        Tbl90RefExpertsList = await _dataService.GetTbl90RefExpertsCollectionOrderByRefExpertNameFromSearchNameOrId(searchName);

        if (Tbl90RefExpertsList.Count == 0)
        {
            await _allDialogs.NoDatasetFoundInfoMessageDialogAsync();
            return;
        }
        RefExpertDataSetCount = Tbl90RefExpertsList.Count;

        RefreshRefExpertItems();
    }

    private void AddRefExpert_Executed(string? parm)
    {
        RefExpertStartEdit();
        RefExpertStartNew();
        Tbl90RefExpertsList ??= new ObservableCollection<Tbl90RefExpert>();
        Tbl90RefExpertsList.Insert(0, new Tbl90RefExpert { RefExpertName = "New" });

        RefreshRefExpertItems();
    }

    private async void CopyRefExpert_Executed(string? parm)
    {
        if (RefExpertSelected == null)
        {
            await _allDialogs.NoDatasetSelectedWarnMessageDialogAsync();
            return;
        }
        RefExpertStartEdit();
        RefExpertStartNew();
        Tbl90RefExpertsList ??= new ObservableCollection<Tbl90RefExpert>();
        Tbl90RefExpertsList = await _dataService.CopyRefExpert(RefExpertSelected);
        // evtl verbundene tabellen-Datensätze auch kopieren Expert, Source, Author und Comment

        RefreshRefExpertItems();
    }

    private async void DeleteRefExpert_Executed(string? searchName)
    {
        if (RefExpertSelected == null)
        {
            await _allDialogs.NoDatasetSelectedWarnMessageDialogAsync();
            return;
        }

        if (await _allDialogs.DeleteDatasetQuestionConfirmationDialogAsync(RefExpertSelected.RefExpertName!))
        {
            //necessary to delete before

            var ret = _dataService.DeleteRefExpert(RefExpertSelected);
            if (!await ret)
            {
                return;
            }

            Tbl90RefExpertsList = await _dataService.GetTbl90RefExpertsCollectionOrderByRefExpertNameFromSearchNameOrId(searchName!);

            RefExpertDataSetCount = Tbl90RefExpertsList.Count;
            RefreshRefExpertItems();
        }
    }

    private async Task SaveRefExpert_Executed(string searchName)
    {
        if (string.IsNullOrEmpty(RefExpertSelected.RefExpertName))
        {
            await _allDialogs.NameRequiredWarnMessageDialogAsync();
            return;
        }
        if (RefExpertSelected == null)
        {
            await _allDialogs.NoDatasetSelectedWarnMessageDialogAsync();
            return;
        }

        RefExpertSelected ??= Tbl90RefExpertsList[0];
        var iNdx = Tbl90RefExpertsList.IndexOf(Tbl90RefExpertsList.First(t =>
            t.RefExpertName == RefExpertSelected.RefExpertName));

        var ret = _dataService.SaveRefExpert(RefExpertSelected);
        if (!await ret)
        {
            return;
        }

        if (string.IsNullOrEmpty(searchName))
        {
            Tbl90RefExpertsList = await _dataService.GetLastDatasetInTbl90RefExperts();
            RefreshRefExpertItems();
        }
        else
        {
            if (RefExpertSelected.RefExpertId == 0) //new
            {
                Tbl90RefExpertsList = await _dataService.GetLastDatasetInTbl90RefExperts();
                RefreshRefExpertItems();
            }
            else
            {
                Tbl90RefExpertsList = await _dataService.GetTbl90RefExpertsCollectionOrderByRefExpertNameFromSearchNameOrId(searchName);
                //   Index Position ?
                if (iNdx < Tbl90RefExpertsList.Count)
                {
                    RefExpertItems.Clear();
                    foreach (var item in Tbl90RefExpertsList)
                    {
                        RefExpertItems.Add(item);
                    }

                    RefExpertSelected = Tbl90RefExpertsList[iNdx];
                }
            }
        }
        RefExpertDataSetCount = Tbl90RefExpertsList.Count;
        RefExpertCancelEditsAsync();
    }
    private async Task RefreshRefExpertServer_Executed(string searchName)
    {
        Tbl90RefExpertsList ??= new ObservableCollection<Tbl90RefExpert>();
        Tbl90RefExpertsList = await _dataService.GetTbl90RefExpertsCollectionOrderByRefExpertNameFromSearchNameOrId(searchName);

        RefExpertDataSetCount = Tbl90RefExpertsList.Count;
        RefreshRefExpertItems();
    }

    public void RefExpertStartEdit() => IsInEdit = true;
    public void RefExpertStartModify() => IsModified = true;
    public void RefExpertStartNew() => IsNewRefExpert = true;
    public event EventHandler AddNewRefExpertCanceled = null!;
    public void RefExpertCancelEditsAsync()
    {
        if (IsNewRefExpert)
        {
            AddNewRefExpertCanceled?.Invoke(this, EventArgs.Empty);
            IsInEdit = false;
            IsNewRefExpert = false;
        }
        else
        {
            IsInEdit = false;
            IsModified = false;
        }
    }

    #endregion [Methods RefExpert]    




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

    #region Selected Properties


    private Tbl90RefExpert _refExpertSelected = null!;

    public Tbl90RefExpert RefExpertSelected
    {
        get => _refExpertSelected;
        set => SetProperty(ref _refExpertSelected, value);
    }


    #endregion

    #region Refresh Properties


    private void RefreshRefExpertItems()
    {
        RefExpertItems.Clear();
        foreach (var item in Tbl90RefExpertsList)
        {
            RefExpertItems.Add(item);
        }
        if (Tbl90RefExpertsList.Count == 0)
        {
            return;
        }

        if (RefExpertSelected == null && Tbl90RefExpertsList.Count != 0)
        {
            RefExpertSelected = RefExpertItems.First();
        }
    }
    #endregion Refresh Properties

    #region Public Properties  

    private int _refExpertDataSetCount;
    public int RefExpertDataSetCount
    {
        get => _refExpertDataSetCount;
        set
        {
            _refExpertDataSetCount = value; OnPropertyChanged();
        }
    }

    private string _searchRefExpertName = "";

    public string SearchRefExpertName
    {
        get => _searchRefExpertName;
        set
        {
            _searchRefExpertName = value; OnPropertyChanged();
        }
    }

    private ObservableCollection<Tbl90RefExpert> _tbl90RefExpertsList = null!;

    public ObservableCollection<Tbl90RefExpert> Tbl90RefExpertsList
    {
        get => _tbl90RefExpertsList;
        set
        {
            _tbl90RefExpertsList = value; OnPropertyChanged();
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

    private bool _isNewRefExpert;
    public bool IsNewRefExpert
    {
        get => _isNewRefExpert;
        set => SetProperty(ref _isNewRefExpert, value);
    }

    #endregion Public Properties

}
  
