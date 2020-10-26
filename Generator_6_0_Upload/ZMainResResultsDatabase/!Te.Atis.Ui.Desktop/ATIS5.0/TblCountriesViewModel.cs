using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity.Validation;
using System.Reflection;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using Te.Atis.DomainModel;
using Te.Atis.Ui.Desktop.BusinessLayer;
using Te.Atis.Ui.Desktop.Domain;
using Te.Atis.Ui.Desktop.Domain.Helper;
using Te.Atis.Ui.Desktop.MessageBox;    

    
         //    TblCountriesViewModel Skriptdatum:   29.11.2018 12:32      

namespace Te.Atis.Ui.Desktop.Views.Database
{     
    
    public class TblCountriesViewModel : ViewModelBase                     
    {     
         
        #region "Private Data Members"
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private static IBusinessLayer _businessLayer;
        private static DbEntityException _entityException;
        private int _position;   
         
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
            SearchCountryName = "";

            TblCountriesList?.Clear();
        }
        //----------------------------------------------------------------------                  
        
        private void GetCountriesByNameOrId(object o)
        {
            if (SearchCountryName != "")
            {
                TblCountriesList?.Clear();
                if (SearchCountryName == "*") // show whole table
                {
                    SearchCountryName = "";
                    _businessLayer = new BusinessLayer.BusinessLayer();
                    TblCountriesList = new ObservableCollection<TblCountry>(_businessLayer.ListTblCountriesByCountryName(SearchCountryName));
                    SearchCountryName = "*";
                }
                else
                {
                     _businessLayer = new BusinessLayer.BusinessLayer();
                       TblCountriesList = int.TryParse(SearchCountryName, out var id) ?
                        new ObservableCollection<TblCountry>(_businessLayer.ListTblCountriesByCountryId(id)) :
                        new ObservableCollection<TblCountry>(_businessLayer.ListTblCountriesByCountryName(SearchCountryName));
                }

                if (TblCountriesList.Count == 0)
                {
                    WpfMessageBox.Show(CultRes.StringsRes.Tables, CultRes.StringsRes.DatasetNot,
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                }
            }
            else
            {
                WpfMessageBox.Show(CultRes.StringsRes.SearchNameOrId, CultRes.StringsRes.InputRequested,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
            }
            CountriesView = CollectionViewSource.GetDefaultView(TblCountriesList);
            CountriesView.Refresh();
        }
        //------------------------------------------------------------------------------------                          
        
        private void AddCountry(object o)
        {
            if (TblCountriesList == null)
                TblCountriesList =  new ObservableCollection<TblCountry>( );

            TblCountriesList.Insert(0, new TblCountry {   Name = CultRes.StringsRes.DatasetNew}  );

            CountriesView = CollectionViewSource.GetDefaultView(TblCountriesList);
            CountriesView.MoveCurrentToFirst();
        }
        //------------------------------------------------------------------------------------                               
        
        private void CopyCountry(object o)
        {
            if (CurrentTblCountry == null)
            {
                WpfMessageBox.Show(CultRes.StringsRes.DatasetNew,
                    CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                return;
            }
            _businessLayer = new BusinessLayer.BusinessLayer();

            var country = _businessLayer.SingleListTblCountriesByCountryId(CurrentTblCountry.CountryID);

            TblCountriesList.Insert(0, new TblCountry
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
            if (CurrentTblCountry == null)
            {
                WpfMessageBox.Show(CultRes.StringsRes.DatasetNew,
                    CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                return;
            }
            _businessLayer = new BusinessLayer.BusinessLayer();

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

            if (SearchCountryName != "")
            {
                if (SearchCountryName == "*")  //show all datasets
                {
                    SearchCountryName = "";
                    TblCountriesList.Clear();
                    
                TblCountriesList = new ObservableCollection<TblCountry>(_businessLayer.ListTblCountriesByCountryName(SearchCountryName));            
                    SearchCountryName = "*";
                }
                else
                {               
                    TblCountriesList =  new ObservableCollection<TblCountry>(_businessLayer.ListTblCountriesByCountryName(SearchCountryName));

                }
                CountriesView = CollectionViewSource.GetDefaultView(TblCountriesList);
                CountriesView.Refresh();
            }
            else  //SearchName = empty
            {
                TblCountriesList = new ObservableCollection<TblCountry>(_businessLayer.ListTblCountriesByCountryName(SearchCountryName));

                CountriesView = CollectionViewSource.GetDefaultView(TblCountriesList);
                CountriesView.MoveCurrentToFirst();
             }
        }
        //-------------------------------------------------------------------------------------------------                    
       
        private void SaveCountry(object o)
        {
            if (CurrentTblCountry == null)
            {
                WpfMessageBox.Show(CultRes.StringsRes.DatasetNew,
                    CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                return;
            }
            _businessLayer = new BusinessLayer.BusinessLayer();

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
                        return;
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
                                _position = CountriesView.CurrentPosition;

                                    WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess,
                                        CurrentTblCountry.CountryID == 0
                                            ? CultRes.StringsRes.DatasetNew
                                            : CurrentTblCountry.Name,
                                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        }
                    }
                }
            }
            catch (DbEntityValidationException ex)
            {
                _entityException.EntityException(ex);
            }

            if (SearchCountryName != "")
            {
                if (SearchCountryName == "*")  //show all datasets
                {
                    SearchCountryName = "";
                    TblCountriesList.Clear();
                    
                TblCountriesList = new ObservableCollection<TblCountry>(_businessLayer.ListTblCountriesByCountryName(SearchCountryName));            
                    SearchCountryName = "*";
                }
                else
                {               
                    TblCountriesList = int.TryParse(SearchCountryName, out var id)
                        ? new ObservableCollection<TblCountry>(_businessLayer.ListTblCountriesByCountryId(id))
                        : new ObservableCollection<TblCountry>(_businessLayer.ListTblCountriesByCountryName(SearchCountryName));

                }
                CountriesView = CollectionViewSource.GetDefaultView(TblCountriesList);
                CountriesView.MoveCurrentToPosition(_position);
            }
            else  
            {
                TblCountriesList = new ObservableCollection<TblCountry>(_businessLayer.ListTblCountriesByCountryName(CurrentTblCountry.Name));

                CountriesView= CollectionViewSource.GetDefaultView(TblCountriesList);
                CountriesView.Refresh();
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

        private string _searchCountryName = "";
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
