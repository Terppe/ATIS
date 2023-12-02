
using System.Collections.ObjectModel;
using ATIS.WinUi.Contracts.Services;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using ATIS.WinUi.Helpers;
using ATIS.WinUi.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml.Data;

//    TblUserProfilesViewModel Skriptdatum:   26.04.2023  10:32    

namespace ATIS.WinUi.ViewModels.Database;

public class TblUserProfilesViewModel : ObservableObject
{

    #region [Private Data Members]
    private readonly IDataService _dataService = null!;
    public ObservableCollection<TblUserProfile> UserProfileItems { get; } = new();

    private readonly AllDialogs _allDialogs = new();
    #endregion [Private Data Members]           

    #region [Constructor]
    public TblUserProfilesViewModel(IDataService dataService)
    {
        _dataService = dataService;
        TblCountriesAllList ??= new ObservableCollection<TblCountry>();
        TblCountriesAllList = _dataService.GetTblCountriesCollectionOrderByName();
    }

    #endregion [Constructor]  


    //    Part 1    



    #region [Commands UserProfile]

    public ICommand GetUserProfilesByEmailOrIdCommand => new RelayCommand(execute: delegate { var task = GetUserProfilesByEmailOrId_Executed(SearchEmail); });

    public ICommand AddUserProfileCommand => new RelayCommand<string>(AddUserProfile_Executed);
    public ICommand CopyUserProfileCommand => new RelayCommand<string>(CopyUserProfile_Executed);

    public ICommand DeleteUserProfileCommand => new RelayCommand(execute: delegate { var task = DeleteUserProfile(SearchEmail); });

    public ICommand SaveUserProfileCommand => new RelayCommand(execute: delegate { var task = SaveUserProfile(SearchEmail); });
    public ICommand RefreshUserProfileServerCommand => new RelayCommand(execute: delegate { var task = RefreshUserProfileServer(SearchEmail); });

    #endregion [Commands UserProfile]       

    #region [Methods UserProfile]

    private async Task GetUserProfilesByEmailOrId_Executed(string searchEmail)
    {
        UserProfileStartModify();
        TblUserProfilesList?.Clear();
        UserProfileItems.Clear();

        TblUserProfilesList ??= new ObservableCollection<TblUserProfile>();
        TblUserProfilesList = await _dataService.GetTblUserProfilesCollectionOrderByEmailFromSearchEmailOrId(searchEmail);

        if (TblUserProfilesList.Count == 0)
        {
            await _allDialogs.NoDatasetFoundInfoMessageDialogAsync();
            return;
        }
        UserProfileDataSetCount = TblUserProfilesList.Count;

        RefreshUserProfileItems();
    }

    private void AddUserProfile_Executed(string? parm)
    {
        UserProfileStartEdit();
        UserProfileStartNew();

        TblUserProfilesList ??= new ObservableCollection<TblUserProfile>();
        TblUserProfilesList.Insert(0, new TblUserProfile { Email = "New" });
    }

    private async void CopyUserProfile_Executed(string? parm)
    {
        UserProfileStartEdit();
        UserProfileStartNew();

        TblUserProfilesList = await _dataService.CopyUserProfile(UserProfileSelected);
        // evtl verbundene tabellen-Datensätze auch kopieren Expert, Source, Author und Comment

        RefreshUserProfileItems();
    }

    private async Task DeleteUserProfile(string searchEmail)
    {
        if (UserProfileSelected == null)
        {
            await _allDialogs.NoDatasetSelectedWarnMessageDialogAsync();
            return;
        }

        if (await _allDialogs.DeleteDatasetQuestionConfirmationDialogAsync(UserProfileSelected!.Email!))
        {
            //necessary to delete before
            var ret = _dataService.DeleteUserProfile(UserProfileSelected);
            if (!await ret)
            {
                return;
            }

            TblUserProfilesList = await _dataService.GetTblUserProfilesCollectionOrderByEmailFromSearchEmailOrId(searchEmail);

            UserProfileDataSetCount = TblUserProfilesList.Count;
            RefreshUserProfileItems();
        }
    }

    private async Task SaveUserProfile(string searchName)
    {
        if (string.IsNullOrEmpty(UserProfileSelected.Email))
        {
            await _allDialogs.NameRequiredWarnMessageDialogAsync();
            return;
        }
        if (UserProfileSelected == null)
        {
            await _allDialogs.NoDatasetSelectedWarnMessageDialogAsync();
            return;
        }

        UserProfileSelected ??= TblUserProfilesList[0];
        var iNdx = TblUserProfilesList.IndexOf(TblUserProfilesList.First(t =>
            t.Email == UserProfileSelected.Email));

        var ret = _dataService.SaveUserProfile(UserProfileSelected);
        if (!await ret)
        {
            return;
        }

        if (string.IsNullOrEmpty(searchName))
        {
            TblUserProfilesList = await _dataService.GetLastDatasetInTblUserProfiles();
            RefreshUserProfileItems();
        }
        else
        {
            if (UserProfileSelected.UserProfileId == 0) //new
            {
                TblUserProfilesList = await _dataService.GetLastDatasetInTblUserProfiles();
                RefreshUserProfileItems();
            }
            else
            {
                TblUserProfilesList = await _dataService.GetTblUserProfilesCollectionOrderByEmailFromSearchEmailOrId(searchName);
                //   Index Position ?
                if (iNdx < TblUserProfilesList.Count)
                {
                    UserProfileItems.Clear();
                    foreach (var item in TblUserProfilesList)
                    {
                        UserProfileItems.Add(item);
                    }

                    UserProfileSelected = TblUserProfilesList[iNdx];
                }
            }
        }
        UserProfileDataSetCount = TblUserProfilesList.Count;
        UserProfileCancelEditsAsync();
    }

    private async Task RefreshUserProfileServer(string? parm)
    {
        TblUserProfilesList ??= new ObservableCollection<TblUserProfile>();
        TblUserProfilesList = await _dataService.GetTblUserProfilesCollectionOrderByEmailFromSearchEmailOrId(parm!);

        UserProfileDataSetCount = TblUserProfilesList.Count;
        RefreshUserProfileItems();
    }

    public void UserProfileStartEdit() => IsInEdit = true;
    public void UserProfileStartModify() => IsModified = true;
    public void UserProfileStartNew() => IsNewUserProfile = true;
    public event EventHandler AddNewUserProfileCanceled = null!;
    public void UserProfileCancelEditsAsync()
    {
        if (IsNewUserProfile)
        {
            AddNewUserProfileCanceled?.Invoke(this, EventArgs.Empty);
            IsInEdit = false;
            IsNewUserProfile = false;
        }
        else
        {
            IsInEdit = false;
            IsModified = false;
        }
    }

    #endregion [Methods UserProfile]    




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


    private TblUserProfile _userProfileSelected = null!;

    public TblUserProfile UserProfileSelected
    {
        get => _userProfileSelected;
        set => SetProperty(ref _userProfileSelected, value);
    }


    #endregion

    #region Refresh Properties


    private void RefreshUserProfileItems()
    {
        UserProfileItems.Clear();
        foreach (var item in TblUserProfilesList)
        {
            UserProfileItems.Add(item);
        }

        if (TblUserProfilesList.Count == 0)
        {
            return;
        }

        if (UserProfileSelected == null && TblUserProfilesList.Count != 0)
        {
            UserProfileSelected = UserProfileItems.First();
        }
    }
    #endregion Refresh Properties

    #region Public Properties  

    private int _userProfileDataSetCount;
    public int UserProfileDataSetCount
    {
        get => _userProfileDataSetCount;
        set
        {
            _userProfileDataSetCount = value; OnPropertyChanged(nameof(UserProfileDataSetCount));
        }
    }

    private string _searchEmail = "";

    public string SearchEmail
    {
        get => _searchEmail;
        set
        {
            _searchEmail = value; OnPropertyChanged(nameof(SearchEmail));
        }
    }


    private ObservableCollection<TblUserProfile> _tblUserProfilesList = null!;

    public ObservableCollection<TblUserProfile> TblUserProfilesList
    {
        get => _tblUserProfilesList;
        set
        {
            _tblUserProfilesList = value;
            OnPropertyChanged(nameof(TblUserProfilesList));
        }
    }

    private ObservableCollection<TblCountry> _tblCountriesAllList = null!;

    public ObservableCollection<TblCountry> TblCountriesAllList
    {
        get => _tblCountriesAllList;
        set
        {
            _tblCountriesAllList = value;
            OnPropertyChanged(nameof(TblCountriesAllList));
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

    private bool _isNewUserProfile;
    public bool IsNewUserProfile
    {
        get => _isNewUserProfile;
        set => SetProperty(ref _isNewUserProfile, value);
    }

    #endregion Public Properties

    #endregion All Properties


    //    Part 12    



    #region "Private Classes"

    public class Gender
    {
        public string Name
        {
            get;
            set;
        } = null!;
    }
    public class Title
    {
        public string Name
        {
            get;
            set;
        } = null!;
    }

    public class Role
    {
        public string Name
        {
            get;
            set;
        } = null!;
    }

    #endregion "Private Methods"  







}
   
