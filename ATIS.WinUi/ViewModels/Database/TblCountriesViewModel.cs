
using System.Collections.ObjectModel;
using ATIS.WinUi.Contracts.Services;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using ATIS.WinUi.Helpers;
using ATIS.WinUi.Models;
using CommunityToolkit.Mvvm.ComponentModel;

//    TblCountriesViewModel Skriptdatum:   26.04.2023 12:32      

namespace ATIS.WinUi.ViewModels.Database;

public class TblCountriesViewModel : ObservableObject
{

    #region [Private Data Members]
    private readonly IDataService _dataService = null!;
    public ObservableCollection<TblCountry> CountryItems { get; } = new();

    private readonly AllDialogs _allDialogs = new();
    #endregion [Private Data Members]           

    #region [Constructor]
    public TblCountriesViewModel(IDataService dataService)
    {
        _dataService = dataService;
    }

    #endregion [Constructor]  


    //    Part 1    



    #region [Commands Country]

    public ICommand GetCountriesByNameOrIdCommand => new RelayCommand(execute: delegate
    {
        var task = GetCountriesByNameOrId_Executed(SearchCountryNameOrId);
    });

    public ICommand AddCountryCommand => new RelayCommand<string>(AddCountry_Executed);
    public ICommand CopyCountryCommand => new RelayCommand<string>(CopyCountry_Executed);
    public ICommand DeleteCountryCommand => new RelayCommand<string>(DeleteCountry_Executed);
  //  public ICommand DeleteCountryCommand => new RelayCommand(execute: delegate { DeleteCountry_Executed(SearchCountryNameOrId); });

    public ICommand SaveCountryCommand => new RelayCommand(execute: delegate { var task = SaveCountry_Executed(SearchCountryNameOrId); });
    public ICommand RefreshCountryServerCommand => new RelayCommand(execute: delegate { RefreshCountryServer_Executed(SearchCountryNameOrId); });

    #endregion [Commands Country]       

    #region [Methods Country]

    private async Task GetCountriesByNameOrId_Executed(string searchName)
    {
        CountryStartModify();
        TblCountriesList?.Clear();

        CountryItems.Clear();

        TblCountriesList ??= new ObservableCollection<TblCountry>();
        TblCountriesList = await _dataService.GetTblCountriesCollectionOrderByNameFromSearchNameOrId(searchName);

        if (TblCountriesList.Count == 0)
        {
            await _allDialogs.NoDatasetFoundInfoMessageDialogAsync();
            return;
        }
        CountryDataSetCount = TblCountriesList.Count;
        RefreshCountryItems();
    }
    private void AddCountry_Executed(string? parm)
    { 
        CountryStartEdit();
        CountryStartNew();

        TblCountriesList ??= new ObservableCollection<TblCountry>();
        TblCountriesList.Insert(0, new TblCountry { Name = "New" });

        RefreshCountryItems();
    }
    private async void CopyCountry_Executed(string? parm)
    {
        CountryStartEdit();
        CountryStartNew();

        TblCountriesList = await _dataService.CopyCountry(CountrySelected);
        // evtl verbundene tabellen-Datensätze auch kopieren Expert, Source, Author und Comment

        RefreshCountryItems();
    }
    private async void DeleteCountry_Executed(string? parm)
    {

        if (await _allDialogs.DeleteDatasetQuestionConfirmationDialogAsync(CountrySelected!.Name!))
        {
            //necessary to delete before
            var ret = _dataService.DeleteCountry(CountrySelected);
            if (!await ret)
            {
                return;
            }

            TblCountriesList = await _dataService.GetTblCountriesCollectionOrderByNameFromSearchNameOrId(parm!);

            CountryDataSetCount = TblCountriesList.Count;
            RefreshCountryItems();
        }
    }
    private async Task SaveCountry_Executed(string? parm)
    {
        if (string.IsNullOrEmpty(CountrySelected?.Name))
        {
            await _allDialogs.NameRequiredWarnMessageDialogAsync();
            return;
        }
        if (TblCountriesList != null)
        {

            var iNdx = TblCountriesList.IndexOf(TblCountriesList.First(t =>
                 t.Name == CountrySelected.Name));

            var ret = _dataService.SaveCountry(CountrySelected);
            if (!await ret)
            {
                return;
            }

            if (string.IsNullOrEmpty(parm))
            {
                TblCountriesList = await _dataService.GetLastDatasetInTblCountries();
                RefreshCountryItems();
            }
            else
            {
                if (CountrySelected.CountryId == 0) //new
                {
                    TblCountriesList = await _dataService.GetLastDatasetInTblCountries();
                    RefreshCountryItems();
                }
                else
                {
                    TblCountriesList = await _dataService.GetTblCountriesCollectionOrderByNameFromSearchNameOrId(parm);
                    //   Index Position ?
                    if (iNdx < TblCountriesList!.Count)
                    {
                        CountryItems.Clear();
                        foreach (var item in TblCountriesList)
                        {
                            CountryItems.Add(item);
                        }

                        CountrySelected = TblCountriesList[iNdx];
                    }
                }
            }
        }
        CountryDataSetCount = TblCountriesList!.Count;
        CountryCancelEditsAsync();
    }
    private async void RefreshCountryServer_Executed(string? parm)
    {
        TblCountriesList = await _dataService.GetTblCountriesCollectionOrderByNameFromSearchNameOrId(parm!);

        CountryDataSetCount = TblCountriesList.Count;
        RefreshCountryItems();
    }
    public void CountryStartEdit() => IsInEdit = true;
    public void CountryStartModify() => IsModified = true;
    public void CountryStartNew() => IsNewCountry = true;
    public event EventHandler AddNewCountryCanceled = null!;
    public void CountryCancelEditsAsync()
    {
        if (IsNewCountry)
        {
            IsInEdit = false;
            AddNewCountryCanceled?.Invoke(this, EventArgs.Empty);
            IsNewCountry = false;
        }
        else
        {
            IsInEdit = false;
            IsModified = false;
        }
    }

    #endregion [Methods TblCountry]    




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


    private TblCountry _countrySelected = null!;

    public TblCountry CountrySelected
    {
        get => _countrySelected;
        set => SetProperty(ref _countrySelected, value);
    }


    #endregion

    #region Refresh Properties


    private void RefreshCountryItems()
    {
        CountryItems.Clear();
        foreach (var item in TblCountriesList)
        {
            CountryItems.Add(item);
        }

        if (TblCountriesList.Count == 0)
        {
            return;
        }

        if (CountrySelected == null && TblCountriesList.Count != 0)
        {
            CountrySelected = CountryItems.First();
        }
    }
    #endregion Refresh Properties

    #region Public Properties  

    private int _countryDataSetCount;
    public int CountryDataSetCount
    {
        get => _countryDataSetCount;
        set
        {
            _countryDataSetCount = value; OnPropertyChanged(nameof(CountryDataSetCount));
        }
    }

    private string _searchCountryNameOrId = "";

    public string SearchCountryNameOrId
    {
        get => _searchCountryNameOrId;
        set
        {
            _searchCountryNameOrId = value; OnPropertyChanged(nameof(SearchCountryNameOrId));
        }
    }


    private ObservableCollection<TblCountry> _tblCountriesList = null!;

    public ObservableCollection<TblCountry> TblCountriesList
    {
        get => _tblCountriesList;
        set
        {
            _tblCountriesList = value; OnPropertyChanged(nameof(TblCountriesList));
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

    private bool _isNewCountry;
    public bool IsNewCountry
    {
        get => _isNewCountry;
        set => SetProperty(ref _isNewCountry, value);
    }

    #endregion Public Properties

    #endregion All Properties


}
  
