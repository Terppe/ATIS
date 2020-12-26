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

    
using GalaSoft.MvvmLight; 
    
         //    TblCountriesViewModel Skriptdatum:   29.11.2018 12:32      

namespace WPFUI.Views.Database
{     
    
    public class TblCountriesViewModel : ViewModelBase                     
    {     
        
        #region "Private Data Members"

        private readonly AllListVm _allListVm = new AllListVm();
        private readonly Repository<TblCountry, int> _tblCountriesRepository = new Repository<TblCountry, int>();  
    

        #endregion "Private Data Members"            
    
        #region "Constructor"

        public TblCountriesViewModel()
        {
            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.
            }
            else
            {
                // Code runs "for real"
            }
        }
        private new bool IsInDesignMode { get; set; }

        #endregion "Constructor"           
 

 //    Part 1    

           
        #region "Public Commands Basic TblCountry"

        private RelayCommand _getCountryByNameOrIdCommand;     
    
        public ICommand GetCountryByNameOrIdCommand    
    
        {
            get { return _getCountryByNameOrIdCommand ?? (_getCountryByNameOrIdCommand = new RelayCommand(delegate { GetCountryByNameOrId(null); })); }   
        }

        private void GetCountryByNameOrId(object o)       
        {   
       
            int id;
            if (int.TryParse(SearchCountryName, out id))
                TblCountriesList = new ObservableCollection<TblCountry> { _tblCountriesRepository.Get(id) };
            else
                TblCountriesList = _allListVm.GetValueTblCountriesList(SearchCountryName);      
CountriesView = CollectionViewSource.GetDefaultView(TblCountriesList);
            CountriesView.Refresh();
        }
        //------------------------------------------------------------------------------                
           
        private RelayCommand _addCountryCommand;           
    
        public ICommand AddCountryCommand       
    
        {
            get { return _addCountryCommand ?? (_addCountryCommand = new RelayCommand(delegate { AddCountry(null); })); }
        }

        private void AddCountry(object o)
        {
            TblCountriesList = new ObservableCollection<TblCountry>();   
       
            TblCountriesList.Insert(0, new TblCountry { Name = CultRes.StringsRes.DatasetNew });  
               
            CountriesView = CollectionViewSource.GetDefaultView(TblCountriesList);
            CountriesView.Refresh();
        }
        //------------------------------------------------------------------------------                
           
        private RelayCommand _copyCountryCommand;              
    
        public ICommand CopyCountryCommand             
         
        {
            get { return _copyCountryCommand ?? (_copyCountryCommand = new RelayCommand(delegate { CopyCountry(null); })); }
        }

        private void CopyCountry(object o)
        {
            TblCountriesList = new ObservableCollection<TblCountry>();

            var country = _tblCountriesRepository.Get(CurrentTblCountry.CountryID);

            TblCountriesList.Insert(0, new TblCountry
            {                 
       
                            Name = country.Name,
                            Regex = country.Regex                      
               
            });
            CountriesView = CollectionViewSource.GetDefaultView(TblCountriesList);
            CountriesView.Refresh();
        }
        //------------------------------------------------------------------------------                
           
        private RelayCommand _deleteCountryCommand;              
    
        public ICommand DeleteCountryCommand             
                
        {
            get { return _deleteCountryCommand ?? (_deleteCountryCommand = new RelayCommand(delegate { DeleteCountry(null); })); }
        }

        private void DeleteCountry(object o)
        {
            try
            {
                var country = _tblCountriesRepository.Get(CurrentTblCountry.CountryID);
                if (country!= null)
                {
                    if (WpfMessageBox.Show(CultRes.StringsRes.DeleteQuestion1, CultRes.StringsRes.DeleteQuestion + " " + CurrentTblCountry.Name,
                         MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes) 
                    return;

                    _tblCountriesRepository.Delete(country);
                    _tblCountriesRepository.Save();

                    WpfMessageBox.Show(CultRes.StringsRes.DeleteSuccess, CurrentTblCountry.Name, 
                        MessageBoxButton.OK, MessageBoxImage.Information);   
  
                    GetCountryByNameOrId(o);  //search       
CountriesView = CollectionViewSource.GetDefaultView(TblCountriesList);
                        CountriesView.Refresh();
                }
                else
                {
                    WpfMessageBox.Show(CultRes.StringsRes.Information, CultRes.StringsRes.DeleteCan + " " + CurrentTblCountry.Name + " " + CultRes.StringsRes.DeleteCan1,
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
           
        private RelayCommand _saveCountryCommand;              
     
        public ICommand SaveCountryCommand             
         
        {
            get { return _saveCountryCommand ?? (_saveCountryCommand = new RelayCommand(delegate { SaveCountry(null); })); }
        }

        private void SaveCountry(object o)
        {
            try
            {
                var country = _tblCountriesRepository.Get(CurrentTblCountry.CountryID);
                if (CurrentTblCountry == null)              
                    WpfMessageBox.Show(CultRes.StringsRes.Warning, CultRes.StringsRes.DatasetNotExist, 
                        MessageBoxButton.OK, MessageBoxImage.Warning);               
                else
                {
                    if (CurrentTblCountry.CountryID!= 0)
                    {
                        if (country!= null) //update
                        {   
                     
                            country.Name = CurrentTblCountry.Name;
                            country.Regex = CurrentTblCountry.Regex;                       
         
                        }
                    }
                    else
                    {
                        _tblCountriesRepository.Add(new TblCountry     //add new
                        {   
                
                            Name= CurrentTblCountry.Name,
                            Regex = CurrentTblCountry.Regex  
                
                        });
                    }
                    {
                        //check about double Name
                        var dataset = new ObservableCollection<TblCountry>
                        (from a in _tblCountriesRepository.GetAll()
                         where
                         a.Name.Trim() == CurrentTblCountry.Name.Trim()                 
                         select a);

                        if (dataset.Count != 0 && CurrentTblCountry.CountryID== 0)  //dataset exist
                        {
                              WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTblCountry.Name,
                              MessageBoxButton.OK, MessageBoxImage.Information);
                        }

                        if (dataset.Count == 0 && CurrentTblCountry.CountryID== 0 ||
                            dataset.Count != 0 && CurrentTblCountry.CountryID != 0  ||
                            dataset.Count == 0 && CurrentTblCountry.CountryID != 0) //new dataset and update
                        {
                            if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTblCountry.Name,
                                    MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)                            
                                return;                            
                            {
                                _tblCountriesRepository.Save();

                                WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess, CurrentTblCountry.Name,
                                    MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                        }  
        
                        if (CurrentTblCountry.CountryID== 0)  //new Dataset
                                GetCountryByNameOrId(o); //search                             
                        {
                            TblCountriesList = new ObservableCollection<TblCountry>
                                                  (from x in _tblCountriesRepository.GetAll()
                                                   where x.CountryID == CurrentTblCountry.CountryID
                                                   select x);

            CountriesView = CollectionViewSource.GetDefaultView(TblCountriesList);
            CountriesView.Refresh();
                        }  
         
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

  
     
        #region "Public Properties TblCountry"

        private string _searchCountryName;
        public  string SearchCountryName
        {
            get => _searchCountryName; 
            set { _searchCountryName = value; RaisePropertyChanged();  }
        }

        public  ICollectionView CountriesView;
        public  TblCountry CurrentTblCountry => CountriesView?.CurrentItem as TblCountry;

        private ObservableCollection<TblCountry> _tblCountriesList;
        public  ObservableCollection<TblCountry> TblCountriesList
        {
            get => _tblCountriesList; 
            set {  _tblCountriesList = value; RaisePropertyChanged();     }      
        }

        private ObservableCollection<TblCountry> _tblCountriesAllList;
        public  ObservableCollection<TblCountry> TblCountriesAllList
        {
            get => _tblCountriesAllList; 
            set { _tblCountriesAllList = value; RaisePropertyChanged(); }
        }

        #endregion "Public Properties"      
 

 //    Part 12    

 

   }
}   
