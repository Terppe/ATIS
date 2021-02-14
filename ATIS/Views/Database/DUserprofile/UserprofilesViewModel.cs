using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using ATIS.Dal.Models;
using ATIS.Ui.Core;
using ATIS.Ui.Helper;

//    UserProfilesViewModel Skriptdatum:   13.02.2021  10:32    

namespace ATIS.Ui.Views.Database.DUserprofile
{

    public class UserprofilesViewModel : ViewModelBase
    {
        // Version with Generic Unit Of Work and AtisDbContext for general use   

        #region [Private Data Members]
        private readonly CrudFunctions _extCrud = new CrudFunctions();
        private readonly DeleteFunctions _extDelete = new DeleteFunctions();
        private readonly SaveFunctions _extSave = new SaveFunctions();
        private readonly AllMessageBoxes _allMessageBoxes = new AllMessageBoxes();
        private int _position;

        #endregion [Private Data Members]               

        #region [Constructor]

        public UserprofilesViewModel()
        {
            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.
            }
            else
            {

                GetValueRole();
                GetValueGender();
                GetValueTitle();
            }
        }
        public bool IsInDesignMode { get; set; }

        #endregion [Constructor]          


        //    Part 1    



        #region [Commands UserProfile]

        private RelayCommand _getUserProfilesByNameOrIdCommand;
        public ICommand GetUserProfilesByNameOrIdCommand => _getUserProfilesByNameOrIdCommand ??= new RelayCommand(delegate { ExecuteGetUserProfilesByNameOrId(SearchEmail); });

        private RelayCommand _addUserProfileCommand;
        public ICommand AddUserProfileCommand => _addUserProfileCommand ??= new RelayCommand(delegate { ExecuteAddUserProfile(null); });

        private RelayCommand _copyUserProfileCommand;
        public ICommand CopyUserProfileCommand => _copyUserProfileCommand ??= new RelayCommand(delegate { ExecuteCopyUserProfile(null); });

        private RelayCommand _deleteUserProfileCommand;
        public ICommand DeleteUserProfileCommand => _deleteUserProfileCommand ??= new RelayCommand(delegate { ExecuteDeleteUserProfile(SearchEmail); });

        private RelayCommand _saveUserProfileCommand;
        public ICommand SaveUserProfileCommand => _saveUserProfileCommand ??= new RelayCommand(delegate { ExecuteSaveUserProfile(SearchEmail); });

        #endregion [Commands UserProfile]       


        #region [Methods UserProfile]

        private void ExecuteGetUserProfilesByNameOrId(string searchName)
        {
            if (TblUserProfilesList == null)
                TblUserProfilesList ??= new ObservableCollection<TblUserProfile>();
            else
                TblUserProfilesList.Clear();


            if (TblCountriesAllList == null)
                TblCountriesAllList ??= new ObservableCollection<TblCountry>();
            else
                TblCountriesAllList.Clear();

            TblCountriesAllList = _extCrud.GetCollectionAllOrderBy<TblCountry>("Country");

            TblUserProfilesList = _extCrud.GetUserProfilesCollectionFromSearchNameOrIdOrderBy<TblUserProfile>(searchName);

            if (_allMessageBoxes.NoDatasetFoundInfoMessageBox(TblUserProfilesList.Count)) return;

            UserProfilesView = CollectionViewSource.GetDefaultView(TblUserProfilesList);
            UserProfilesView.Refresh();
        }

        private void ExecuteAddUserProfile(object o)
        {
            if (TblUserProfilesList == null)
                TblUserProfilesList ??= new ObservableCollection<TblUserProfile>();
            else
                TblUserProfilesList.Clear();

            if (TblCountriesAllList == null)
                TblCountriesAllList ??= new ObservableCollection<TblCountry>();
            else
                TblCountriesAllList.Clear();

            TblCountriesAllList = _extCrud.GetCollectionAllOrderBy<TblCountry>("Country");

            TblUserProfilesList.Insert(0, new TblUserProfile { LastName = CultRes.StringsRes.DatasetNew });

            UserProfilesView = CollectionViewSource.GetDefaultView(TblUserProfilesList);
            UserProfilesView.MoveCurrentToFirst();
        }

        private void ExecuteCopyUserProfile(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTblUserProfile)) return;

            TblUserProfilesList = _extCrud.CopyUserProfile(CurrentTblUserProfile);

            UserProfilesView = CollectionViewSource.GetDefaultView(TblUserProfilesList);
            UserProfilesView.MoveCurrentToFirst();
        }

        private void ExecuteDeleteUserProfile(string searchName)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTblUserProfile)) return;

            _extDelete.DeleteUserProfile(CurrentTblUserProfile);

            TblUserProfilesList = _extCrud.GetCollectionFromSearchNameOrIdOrderBy<TblUserProfile>(searchName, "UserProfile");
            UserProfilesView = CollectionViewSource.GetDefaultView(TblUserProfilesList);
            UserProfilesView.MoveCurrentToLast();
        }

        private void ExecuteSaveUserProfile(string searchName)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTblUserProfile)) return;

            _position = UserProfilesView.CurrentPosition;

            var ret = _extSave.SaveUserProfile(CurrentTblUserProfile);

            if (ret != true)
            {
                UserProfilesView = CollectionViewSource.GetDefaultView(TblUserProfilesList);
                UserProfilesView.Refresh();
                return;
            }

            if (CurrentTblUserProfile.UserProfileId == 0) //new
            {
                TblUserProfilesList = _extCrud.GetLastUserProfilesDatasetOrderById();
                UserProfilesView = CollectionViewSource.GetDefaultView(TblUserProfilesList);
                UserProfilesView.MoveCurrentToFirst();
            }
            else
            {
                TblUserProfilesList = _extCrud.GetUserProfilesCollectionFromSearchNameOrIdOrderBy<TblUserProfile>(searchName);
                UserProfilesView = CollectionViewSource.GetDefaultView(TblUserProfilesList);
                UserProfilesView.MoveCurrentToPosition(_position);
            }
        }
        #endregion "Public Commands"                   



        //    Part 2    




        //    Part 3    





        //    Part 4    




        //    Part 5    



        //    Part 6    




        //    Part 7    



        //    Part 8    



        //    Part 9    







        //    Part 10    



 //    Part 10    



                //    Part 11    


                #region "Public Properties TblUserProfile"

        private string _searchEmail = "";
        public string SearchEmail
        {
            get => _searchEmail;
            set { _searchEmail = value; RaisePropertyChanged(""); }
        }

        public ICollectionView UserProfilesView;
        private TblUserProfile CurrentTblUserProfile => UserProfilesView?.CurrentItem as TblUserProfile;

        private ObservableCollection<TblUserProfile> _tblUserProfilesList;
        public ObservableCollection<TblUserProfile> TblUserProfilesList
        {
            get => _tblUserProfilesList;
            set { _tblUserProfilesList = value; RaisePropertyChanged(""); }
        }

        private ObservableCollection<TblUserProfile> _tblUserProfilesAllList;
        public ObservableCollection<TblUserProfile> TblUserProfilesAllList
        {
            get => _tblUserProfilesAllList;
            set { _tblUserProfilesAllList = value; RaisePropertyChanged(""); }
        }

        private string _passwordBox;
        public string PasswordBox
        {
            get => _passwordBox;

            set { _passwordBox = value; RaisePropertyChanged("PasswordBox"); }
        }

        private void GetValueRole()
        {
            _roles = new List<Role>()
            {
                new Role {Name = "Administrator"},
                new Role {Name = "Developer"},
                new Role {Name = "Biologist"},
                new Role {Name = "Zoologist"},
                new Role {Name = "User"},};

            _selectedRole = new Role();
        }

        private void GetValueTitle()
        {
            _titles = new List<Title>()
            {
                new Title {Name = ""},
                new Title {Name = "Dr."},
                new Title {Name = "Prof."},
                new Title {Name = "Dipl.-Ing."},
                new Title {Name = "Ing. grad"},
                new Title {Name = "Dr. Ing"}
            };

            _selectedTitle = new Title();
        }

        private void GetValueGender()
        {
            _genders = new List<Gender>()
            {
                new Gender {Name = "Female"},
                new Gender {Name = "Male"}
            };

            _selectedGender = new Gender();
        }

        private ObservableCollection<TblCountry> _tblCountriesAllList;
        public ObservableCollection<TblCountry> TblCountriesAllList
        {
            get => _tblCountriesAllList;
            set { _tblCountriesAllList = value; RaisePropertyChanged(""); }
        }

        //-----------------------------------------------------------
        private List<Role> _roles;
        public List<Role> Roles
        {
            get => _roles;
            set { _roles = value; RaisePropertyChanged(""); }
        }

        private Role _selectedRole;
        public Role SelectedRole
        {
            get => _selectedRole;
            set { _selectedRole = value; RaisePropertyChanged(""); }
        }

        private List<Gender> _genders;
        public List<Gender> Genders
        {
            get => _genders;
            set { _genders = value; RaisePropertyChanged(""); }
        }

        private Gender _selectedGender;
        public Gender SelectedGender
        {
            get => _selectedGender;
            set { _selectedGender = value; RaisePropertyChanged(""); }
        }

        private List<Title> _titles;
        public List<Title> Titles
        {
            get => _titles;
            set { _titles = value; RaisePropertyChanged(""); }
        }

        private Title _selectedTitle;
        public Title SelectedTitle
        {
            get => _selectedTitle;
            set { _selectedTitle = value; RaisePropertyChanged(""); }
        }

        #endregion "Public Properties"      


        //    Part 12    



        #region "Private Classes"

        public class Gender
        {
            public string Name
            {
                get;
                set;
            }
        }
        public class Title
        {
            public string Name
            {
                get;
                set;
            }
        }

        public class Role
        {
            public string Name
            {
                get;
                set;
            }
        }

        #endregion "Private Methods"  


    }
}
