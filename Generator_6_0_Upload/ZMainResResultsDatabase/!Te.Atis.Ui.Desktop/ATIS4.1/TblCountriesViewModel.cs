using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity.Validation;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using Te.Atis.BusinessLayer;
using Te.Atis.DomainModel;
using Te.Atis.Ui.Desktop.Domain;
using Te.Atis.Ui.Desktop.Domain.Helper;
using Te.Atis.Ui.Desktop.MessageBox;    

    
         //    TblCountriesViewModel Skriptdatum:   31.07.2018 12:32      

namespace Te.Atis.Ui.Desktop.Views.Database
{     
    
    public class TblCountriesViewModel : ViewModelBase                     
    {     
         
        #region "Private Data Members"

        private static IBusinessLayer _businessLayer;
        private static DbEntityException _entityException;   
         
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
                _businessLayer = new BusinessLayer.BusinessLayer();
                _entityException = new DbEntityException();
            }
        }     
        #endregion "Constructor"         
 

 //    Part 1    

             
        #region "Public Commands Basic TblCountry"
        //-------------------------------------------------------------------------
        private RelayCommand _clearCountryCommand;

        public ICommand ClearCountryCommand => _clearCountryCommand ??
                                                  (_clearCountryCommand = new RelayCommand(delegate { ClearCountry(null); }));         
             
        private RelayCommand _getCountriesByNameOrIdCommand;  

        public  ICommand GetCountriesByNameOrIdCommand => _getCountriesByNameOrIdCommand ??
                                                           (_getCountriesByNameOrIdCommand = new RelayCommand(delegate { GetCountriesByNameOrId(null); }));        
             
        private RelayCommand _addCountryCommand;

        public ICommand AddCountryCommand => _addCountryCommand ??
                                                (_addCountryCommand = new RelayCommand(delegate { AddCountry(null); }));

        private RelayCommand _copyCountryCommand;

        public ICommand CopyCountryCommand => _copyCountryCommand ??
                                                 (_copyCountryCommand = new RelayCommand(delegate { CopyCountry(null); }));      
             
        private RelayCommand _deleteCountryCommand;

        public ICommand DeleteCountryCommand => _deleteCountryCommand ??
                                                   (_deleteCountryCommand = new RelayCommand(delegate { DeleteCountry(null); }));    
             
        private RelayCommand _saveCountryCommand;

        public ICommand SaveCountryCommand => _saveCountryCommand ??
                                                 (_saveCountryCommand = new RelayCommand(delegate { SaveCountry(null); }));
        //-------------------------------------------------------------------------          
        
        private void ClearCountry(object o)
        {
            SearchCountryName = string.Empty;

            TblCountriesList?.Clear();
        }
        //----------------------------------------------------------------------                  
        
        private void GetCountriesByNameOrId(object o)
        {
            TblCountriesList = int.TryParse(SearchCountryName, out var id) ?
                new ObservableCollection<TblCountry>(_businessLayer.ListTblCountriesByCountryId(id)) :
                new ObservableCollection<TblCountry>(_businessLayer.ListTblCountriesByCountryName(SearchCountryName));

            CountriesView = CollectionViewSource.GetDefaultView(TblCountriesList);
            CountriesView.Refresh();
        }
        //------------------------------------------------------------------------------------                          
        
        private void AddCountry(object o)
        {
            TblCountriesList = new ObservableCollection<TblCountry> {new TblCountry
                    {   Name = CultRes.StringsRes.DatasetNew      }  };

            CountriesView = CollectionViewSource.GetDefaultView(TblCountriesList);
            CountriesView.MoveCurrentToFirst();
        }
        //------------------------------------------------------------------------------------                               
        
        private void CopyCountry(object o)
        {
            TblCountriesList = new ObservableCollection<TblCountry>();

            var country = _businessLayer.SingleListTblCountriesByCountryId(CurrentTblCountry.CountryID);

            TblCountriesList.Add(new TblCountry
            {
                Name = country.Name,
                Regex = country.Regex
            });

            CountriesView = CollectionViewSource.GetDefaultView(TblCountriesList);
            CountriesView.MoveCurrentToFirst();
        }
        //---------------------------------------------------------------------------------------                            
        
        private void DeleteCountry(object o)
        {
            try
            {
                var country = _businessLayer.SingleListTblCountriesByCountryId(CurrentTblCountry.CountryID);
                if (country != null)
                {
                    if (WpfMessageBox.Show(CultRes.StringsRes.DeleteQuestion1, CultRes.StringsRes.DeleteQuestion + " " + CurrentTblCountry.Name,
                            MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes)
                        return;
                    country.EntityState = EntityState.Deleted;
                    _businessLayer.RemoveCountry(country);

                    WpfMessageBox.Show(CultRes.StringsRes.DeleteSuccess, CurrentTblCountry.Name,
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                }
                else
                {
                    WpfMessageBox.Show(CultRes.StringsRes.Information, CultRes.StringsRes.DeleteCan + " " + CurrentTblCountry.Name + " " + CultRes.StringsRes.DeleteCan1,
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                }
            }
            catch (DbEntityValidationException ex)
            {
                _entityException.EntityException(ex);
            }

            TblCountriesList = new ObservableCollection<TblCountry>(_businessLayer.ListTblCountriesByCountryName(SearchCountryName));

            CountriesView = CollectionViewSource.GetDefaultView(TblCountriesList);
            CountriesView.Refresh();
        }
        //-------------------------------------------------------------------------------------------------                    
       
        private void SaveCountry(object o)
        {
            try
            {
                var country = _businessLayer.SingleListTblCountriesByCountryId(CurrentTblCountry.CountryID);
                if (CurrentTblCountry.CountryID != 0)
                {
                    if (country != null) //update
                    {
                            country.Name = CurrentTblCountry.Name;
                            country.Regex = CurrentTblCountry.Regex;
                            country.EntityState = EntityState.Modified;
                    }
                }
                else
                {
                            country = new TblCountry   //add new
                            {
                            Name = CurrentTblCountry.Name,
                            Regex = CurrentTblCountry.Regex,
                            EntityState = EntityState.Added
                    };
                }
                {
                        //check if dataset with Name already exist       
                        var dataset = _businessLayer.ListTblCountriesByCountryName(CurrentTblCountry.Name);

                    if (dataset.Count != 0 && CurrentTblCountry.CountryID == 0)  //dataset exist
                    {
                        WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTblCountry.Name,
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                    }
                    if (dataset.Count == 0 && CurrentTblCountry.CountryID == 0 ||
                        dataset.Count != 0 && CurrentTblCountry.CountryID != 0 ||
                        dataset.Count == 0 && CurrentTblCountry.CountryID != 0) //new dataset and update
                    {
                        if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTblCountry.Name,
                                MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes)
                            return;
                        {
                            _businessLayer.UpdateCountry(country);

                            WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess, CurrentTblCountry.Name,
                                MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        }
                    }
                }
            }
            catch (DbEntityValidationException ex)
            {
                _entityException.EntityException(ex);
            }

            if (CurrentTblCountry.CountryID == 0)  //new Dataset                        
                TblCountriesList = new ObservableCollection<TblCountry>(_businessLayer.ListTblCountriesByCountryName(CurrentTblCountry.Name));
            if (CurrentTblCountry.CountryID != 0)   //update 
                TblCountriesList = new ObservableCollection<TblCountry>(_businessLayer.ListTblCountriesByCountryId(CurrentTblCountry.CountryID));

            CountriesView = CollectionViewSource.GetDefaultView(TblCountriesList);
            CountriesView.Refresh();
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

        private string _searchCountryName = string.Empty;
        public string SearchCountryName
        {
            get => _searchCountryName; 
            set { _searchCountryName = value; RaisePropertyChanged();  }
        }

        public  ICollectionView CountriesView;
        private   TblCountry CurrentTblCountry => CountriesView?.CurrentItem as TblCountry;

        private ObservableCollection<TblCountry> _tblCountriesList;
        public  ObservableCollection<TblCountry> TblCountriesList
        {
            get => _tblCountriesList; 
            set {  _tblCountriesList = value; RaisePropertyChanged();   }
        }

        private ObservableCollection<TblCountry> _tblCountriesAllList;
        public  ObservableCollection<TblCountry> TblCountriesAllList
        {
            get => _tblCountriesAllList; 
            set {  _tblCountriesAllList = value; RaisePropertyChanged();   }
        }

        #endregion "Public Properties"   
 

 



   }
}   
