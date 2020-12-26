using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity.Validation;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using DAL;
using DAL.Helper;
using DAL.Models;
using WPFUI.ViewModel;
using GalaSoft.MvvmLight.Command;
using MessageBoxImage = System.Windows.MessageBoxImage;

    
using System.Collections.Generic;
using GalaSoft.MvvmLight; 
using WPFUI.Helper; 
    
         //    TblUserProfilesViewModel Skriptdatum:   15.11.2017  10:32    

namespace WPFUI.Views.Database
{     
    
    public class TblUserProfilesViewModel : ViewModelBase                     
    {     
        
        #region "Private Data Members"

        private readonly AllListVm _allListVm = new AllListVm();
        private readonly Repository<TblUserProfile, int> _tblUserProfilesRepository = new Repository<TblUserProfile, int>();  
        private readonly Repository<TblCountry, int> _tblCountriesRepository = new Repository<TblCountry, int>();  
    

        #endregion "Private Data Members"            
    
        #region "Constructor"

        public TblUserProfilesViewModel()
        {
            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.
            }
            else
            {
                // Code runs "for real"
                TblCountriesList = new ObservableCollection<TblCountry>
                    (from y in _tblCountriesRepository.GetAll()
                       orderby y.Name
                   select y);

                GetValueRole();
                GetValueGender();
                GetValueTitle();
            }
        }

        private new bool IsInDesignMode { get; set; }

        #endregion "Constructor"           
 

 //    Part 1    

           
        #region "Public Commands Basic TblUserProfile"

        private RelayCommand _getUserProfileByNameOrIdCommand;     
    
        public ICommand GetUserProfileByNameOrIdCommand    
    
        {
            get { return _getUserProfileByNameOrIdCommand ?? (_getUserProfileByNameOrIdCommand = new RelayCommand(delegate { GetUserProfileByNameOrId(null); })); }   
        }

        private void GetUserProfileByNameOrId(object o)       
        {   
       
            int id;
            if (int.TryParse(SearchEmail, out id))
                TblUserProfilesList = new ObservableCollection<TblUserProfile> { _tblUserProfilesRepository.Get(id) };
            else  
                TblUserProfilesList = _allListVm.GetValueTblUserProfilesList(SearchEmail);     
UserProfilesView = CollectionViewSource.GetDefaultView(TblUserProfilesList);
            UserProfilesView.Refresh();
        }
        //------------------------------------------------------------------------------                
           
        private RelayCommand _addUserProfileCommand;           
    
        public ICommand AddUserProfileCommand       
    
        {
            get { return _addUserProfileCommand ?? (_addUserProfileCommand = new RelayCommand(delegate { AddUserProfile(null); })); }
        }

        private void AddUserProfile(object o)
        {
            TblUserProfilesList = new ObservableCollection<TblUserProfile>();   
TblUserProfilesList.Insert(0, new TblUserProfile{ LastName= CultRes.StringsRes.DatasetNew });       
               
            UserProfilesView = CollectionViewSource.GetDefaultView(TblUserProfilesList);
            UserProfilesView.Refresh();
        }
        //------------------------------------------------------------------------------                
           
        private RelayCommand _copyUserProfileCommand;              
    
        public ICommand CopyUserProfileCommand             
         
        {
            get { return _copyUserProfileCommand ?? (_copyUserProfileCommand = new RelayCommand(delegate { CopyUserProfile(null); })); }
        }

        private void CopyUserProfile(object o)
        {
            TblUserProfilesList = new ObservableCollection<TblUserProfile>();

            var userprofile = _tblUserProfilesRepository.Get(CurrentTblUserProfile.UserProfileID);

            TblUserProfilesList.Insert(0, new TblUserProfile
            {                 
       
                Email = CultRes.StringsRes.DatasetNew,
                Role = userprofile.Role,
                Flag = userprofile.Flag,
                Colour = userprofile.Colour,
                Title = userprofile.Title,
                FirstName = userprofile.FirstName,
                LastName = userprofile.LastName,
                BirthDate = userprofile.BirthDate,
                Gender = userprofile.Gender,
                Country = userprofile.Country,
                Postcode = userprofile.Postcode,
                City = userprofile.City,
                Street1 = userprofile.Street1,
                Street2 = userprofile.Street2,
                Tel = userprofile.Tel,
                Mobil = userprofile.Mobil,
                Fax = userprofile.Fax,
                HomePageURL = userprofile.HomePageURL,
                Business = userprofile.Business,
                Company = userprofile.Company,
                Filestream = userprofile.Filestream,
                ImageMimeType = userprofile.ImageMimeType,
                FilestreamID = Guid.NewGuid(),
                Signature = userprofile.Signature,
                MailNewsletter = userprofile.MailNewsletter,
                MaulHTML = userprofile.MaulHTML,
                Known = userprofile.Known,
                StartDate = userprofile.StartDate,
                EndDate = userprofile.EndDate,
                Memo = userprofile.Memo                   
               
            });
            UserProfilesView = CollectionViewSource.GetDefaultView(TblUserProfilesList);
            UserProfilesView.Refresh();
        }
        //------------------------------------------------------------------------------                
           
        private RelayCommand _deleteUserProfileCommand;              
    
        public ICommand DeleteUserProfileCommand             
                
        {
            get { return _deleteUserProfileCommand ?? (_deleteUserProfileCommand = new RelayCommand(delegate { DeleteUserProfile(null); })); }
        }

        private void DeleteUserProfile(object o)
        {
            try
            {
                var userprofile = _tblUserProfilesRepository.Get(CurrentTblUserProfile.UserProfileID);
                if (userprofile!= null)
                {
                    if (WpfMessageBox.Show(CultRes.StringsRes.DeleteQuestion1, CultRes.StringsRes.DeleteQuestion + " " + CurrentTblUserProfile.Email,
                         MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes) 
                    return;

                    _tblUserProfilesRepository.Delete(userprofile);
                    _tblUserProfilesRepository.Save();

                    WpfMessageBox.Show(CultRes.StringsRes.DeleteSuccess, CurrentTblUserProfile.Email, MessageBoxButton.OK, 
                        MessageBoxImage.Information);     

                    GetUserProfileByNameOrId(o);  //search       
UserProfilesView = CollectionViewSource.GetDefaultView(TblUserProfilesList);
                        UserProfilesView.Refresh();
                }
                else
                {
                    WpfMessageBox.Show(CultRes.StringsRes.Information, CultRes.StringsRes.DeleteCan + " " + CurrentTblUserProfile.Email + " " + CultRes.StringsRes.DeleteCan1,
                         MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (DbEntityValidationException ex)
            {
                //Retrieve the Error messages as a list of strings
                var errorMessages = ex.EntityValidationErrors
                    .SelectMany(x => x.ValidationErrors)
                    .Select(x => x.ErrorMessage);
                //Join the list to a single string
                var fullErrorMessage = string.Join("; ", errorMessages);
                //Combine the original exeption message with the new one.
                var exeptionMessage = string.Concat(ex.Message, CultRes.StringsRes.ValidationErrors, fullErrorMessage);
                //throw a new DbEntityValidationException
                throw new DbEntityValidationException(exeptionMessage, ex.EntityValidationErrors);
            }
        }
        //-------------------------------------------------------------------------------------------------    
           
        private RelayCommand _saveUserProfileCommand;              
     
        public ICommand SaveUserProfileCommand             
         
        {
            get { return _saveUserProfileCommand ?? (_saveUserProfileCommand = new RelayCommand(delegate { SaveUserProfile(null); })); }
        }

        private void SaveUserProfile(object o)
        {
            try
            {
                var userprofile = _tblUserProfilesRepository.Get(CurrentTblUserProfile.UserProfileID);
                if (CurrentTblUserProfile == null)              
                    WpfMessageBox.Show(CultRes.StringsRes.Warning, CultRes.StringsRes.DatasetNotExist, 
                        MessageBoxButton.OK, MessageBoxImage.Warning);               
                else
                {
                    if (CurrentTblUserProfile.UserProfileID!= 0)
                    {
                        if (userprofile!= null) //update
                        {   
                     
                            userprofile.Email= CurrentTblUserProfile.Email;
                            userprofile.Password = Crypt.CalculateHash(CurrentTblUserProfile.Password, CurrentTblUserProfile.Email);
                            userprofile.Role = CurrentTblUserProfile.Role;
                            userprofile.Flag = CurrentTblUserProfile.Flag;
                            userprofile.Colour = CurrentTblUserProfile.Colour;
                            userprofile.Title = CurrentTblUserProfile.Title;
                            userprofile.FirstName = CurrentTblUserProfile.FirstName;
                            userprofile.LastName = CurrentTblUserProfile.LastName;
                            userprofile.BirthDate = CurrentTblUserProfile.BirthDate;
                            userprofile.Gender = CurrentTblUserProfile.Gender;
                            userprofile.Country = CurrentTblUserProfile.Country;
                            userprofile.Postcode = CurrentTblUserProfile.Postcode;
                            userprofile.City = CurrentTblUserProfile.City;
                            userprofile.Street1 = CurrentTblUserProfile.Street1;
                            userprofile.Street2 = CurrentTblUserProfile.Street2;
                            userprofile.Tel = CurrentTblUserProfile.Tel;
                            userprofile.Mobil = CurrentTblUserProfile.Mobil;
                            userprofile.Fax = CurrentTblUserProfile.Fax;
                            userprofile.HomePageURL = CurrentTblUserProfile.HomePageURL;
                            userprofile.Business = CurrentTblUserProfile.Business;
                            userprofile.Company = CurrentTblUserProfile.Company;
                            userprofile.Filestream = CurrentTblUserProfile.Filestream;
                            userprofile.ImageMimeType = CurrentTblUserProfile.ImageMimeType;
                            userprofile.FilestreamID = CurrentTblUserProfile.FilestreamID;
                            userprofile.Signature = CurrentTblUserProfile.Signature;
                            userprofile.MailNewsletter = CurrentTblUserProfile.MailNewsletter;
                            userprofile.MaulHTML = CurrentTblUserProfile.MaulHTML;
                            userprofile.Known = CurrentTblUserProfile.Known;
                            userprofile.StartDate = DateTime.Now;
                            userprofile.EndDate = DateTime.Now;
                            userprofile.Updater = Environment.UserName;
                            userprofile.UpdaterDate = DateTime.Now;
                            userprofile.Memo = CurrentTblUserProfile.Memo;                         
         
                        }
                    }
                    else
                    {
                        _tblUserProfilesRepository.Add(new TblUserProfile     //add new
                        {   
                
                            Email= CurrentTblUserProfile.Email,
                            CountID = RandomHelper.Randomnumber(),
                            Password = Crypt.CalculateHash(CurrentTblUserProfile.Password, CurrentTblUserProfile.Email),
                            Role = CurrentTblUserProfile.Role,
                            Flag = CurrentTblUserProfile.Flag,
                            Colour = CurrentTblUserProfile.Colour,
                            Title = CurrentTblUserProfile.Title,
                            FirstName = CurrentTblUserProfile.FirstName,
                            LastName = CurrentTblUserProfile.LastName,
                            BirthDate = CurrentTblUserProfile.BirthDate,
                            Gender = CurrentTblUserProfile.Gender,
                            Country = CurrentTblUserProfile.Country,
                            Postcode = CurrentTblUserProfile.Postcode,
                            City = CurrentTblUserProfile.City,
                            Street1 = CurrentTblUserProfile.Street1,
                            Street2 = CurrentTblUserProfile.Street2,
                            Tel = CurrentTblUserProfile.Tel,
                            Mobil = CurrentTblUserProfile.Mobil,
                            Fax = CurrentTblUserProfile.Fax,
                            HomePageURL = CurrentTblUserProfile.HomePageURL,
                            Business = CurrentTblUserProfile.Business,
                            Company = CurrentTblUserProfile.Company,
                            Filestream = CurrentTblUserProfile.Filestream,
                            ImageMimeType = CurrentTblUserProfile.ImageMimeType,
                            FilestreamID = Guid.NewGuid(),
                            Signature = CurrentTblUserProfile.Signature,
                            MailNewsletter = CurrentTblUserProfile.MailNewsletter,
                            MaulHTML = CurrentTblUserProfile.MaulHTML,
                            Known = CurrentTblUserProfile.Known,
                            StartDate = DateTime.Now,
                            EndDate = DateTime.Now,
                            Writer = Environment.UserName,
                            WriterDate = DateTime.Now,
                            Updater = Environment.UserName,
                            UpdaterDate = DateTime.Now,
                            Memo = CurrentTblUserProfile.Memo  
                
                        });
                    }
                    {
                        //check about double email
                        var dataset = new ObservableCollection<TblUserProfile>
                        (from a in _tblUserProfilesRepository.GetAll()
                         where
                         a.Email.Trim() == CurrentTblUserProfile.Email.Trim()                
                         select a);

                        if (dataset.Count != 0 && CurrentTblUserProfile.UserProfileID== 0)  //dataset exist
                        {
                              WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTblUserProfile.Email,
                              MessageBoxButton.OK, MessageBoxImage.Information);
                        }

                        if (dataset.Count == 0 && CurrentTblUserProfile.UserProfileID == 0 ||
                            dataset.Count != 0 && CurrentTblUserProfile.UserProfileID != 0  ||
                            dataset.Count == 0 && CurrentTblUserProfile.UserProfileID != 0) //new dataset and update
                        {
                            if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTblUserProfile.Email,
                                    MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)                            
                                return;                            
                            {
                                _tblUserProfilesRepository.Save();

                                WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess, CurrentTblUserProfile.Email,
                                    MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                        }  
          
                        if (SearchEmail == null && CurrentTblUserProfile.UserProfileID == 0)  //new Dataset                        
                            TblUserProfilesList = _allListVm.GetValueTblUserProfilesList();  //last Dataset
                        if (SearchEmail == null && CurrentTblUserProfile.UserProfileID != 0)   //update 
                            TblUserProfilesList = _allListVm.GetValueTblUserProfilesList(CurrentTblUserProfile.UserProfileID);
                        if (SearchEmail != null && CurrentTblUserProfile.UserProfileID == 0)  //new Dataset                        
                            TblUserProfilesList = _allListVm.GetValueTblUserProfilesList();  //last Dataset
                        if (SearchEmail != null && CurrentTblUserProfile.UserProfileID != 0)   //update 
                            TblUserProfilesList = _allListVm.GetValueTblUserProfilesList(CurrentTblUserProfile.UserProfileID);

                        UserProfilesView = CollectionViewSource.GetDefaultView(TblUserProfilesList);
                        UserProfilesView.Refresh();
                          
         
                    }
                }
            }
            catch (DbEntityValidationException ex)
            {
                //  WpfMessageBox.Show(CultRes.StringsRes.Error, ex.Message, MessageBoxButton.OK, MessageBoxImage.Error);
                //Retrieve the Error messages as a list of strings
                var errorMessages = ex.EntityValidationErrors
                    .SelectMany(x => x.ValidationErrors)
                    .Select(x => x.ErrorMessage);
                //Join the list to a single string
                var fullErrorMessage = string.Join("; ", errorMessages);
                //Combine the original exeption message with the new one.
                var exeptionMessage = string.Concat(ex.Message, CultRes.StringsRes.ValidationErrors, fullErrorMessage);
                //throw a new DbEntityValidationException
                throw new DbEntityValidationException(exeptionMessage, ex.EntityValidationErrors);
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

 

 //    Part 11    

  
     
        #region "Public Properties TblUserProfile"

        public ICollectionView UserProfilesView;
        public TblUserProfile CurrentTblUserProfile => UserProfilesView?.CurrentItem as TblUserProfile;
        
        private string _searchEmail;
        public string SearchEmail
        {
            get => _searchEmail; 
            set { _searchEmail = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<TblUserProfile> _tblUserProfilesList;
        public  ObservableCollection<TblUserProfile> TblUserProfilesList
        {
            get => _tblUserProfilesList; 
            set { _tblUserProfilesList = value; RaisePropertyChanged();  }
        }

        private ObservableCollection<TblUserProfile> _tblUserProfilesAllList;
        public ObservableCollection<TblUserProfile> TblUserProfilesAllList
        {
            get => _tblUserProfilesAllList; 
            set { _tblUserProfilesAllList = value; RaisePropertyChanged(); }
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

        private ObservableCollection<TblCountry> _tblCountriesList;
        public ObservableCollection<TblCountry> TblCountriesList
        {
            get => _tblCountriesList; 
            set { _tblCountriesList = value; RaisePropertyChanged(); }
        }

        //-----------------------------------------------------------
        private List<Role> _roles;
        public List<Role> Roles
        {
            get => _roles; 
            set { _roles = value; RaisePropertyChanged(); }
        }

        private Role _selectedRole;
        public Role SelectedRole
        {
            get => _selectedRole; 
            set { _selectedRole = value; RaisePropertyChanged(); }
        }

        private List<Gender> _genders;
        public List<Gender> Genders
        {
            get => _genders; 
            set { _genders = value; RaisePropertyChanged(); }
        }

        private Gender _selectedGender;
        public Gender SelectedGender
        {
            get => _selectedGender; 
            set { _selectedGender = value; RaisePropertyChanged(); }
        }

        private List<Title> _titles;
        public List<Title> Titles
        {
            get => _titles; 
            set { _titles = value; RaisePropertyChanged(); }
        }

        private Title _selectedTitle;
        public Title SelectedTitle
        {
            get => _selectedTitle; 
            set { _selectedTitle = value; RaisePropertyChanged(); }
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
