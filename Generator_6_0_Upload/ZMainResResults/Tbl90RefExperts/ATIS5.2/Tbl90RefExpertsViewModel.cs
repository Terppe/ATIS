using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using log4net;
using Te.Atis.DomainModel;
using Te.Atis.Ui.Desktop.BusinessLayer;
using Te.Atis.Ui.Desktop.Domain;
using Te.Atis.Ui.Desktop.Domain.Helper;
using Te.Atis.Ui.Desktop.MessageBox;    

    
         //    Tbl90RefExpertsViewModel Skriptdatum:  29.11.2018  10:32    

namespace Te.Atis.Ui.Desktop.Views.Database
{     
    
    public class Tbl90RefExpertsViewModel : ViewModelBase                     
    {     
         
        #region "Private Data Members"
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static IBusinessLayer _businessLayer;
        private static DbEntityException _entityException;
        private int _position;   
         
        #endregion "Private Data Members"               
      
        #region "Constructor"

        public Tbl90RefExpertsViewModel()
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

             
        #region "Public Commands Basic Tbl90RefExpert"
        //-------------------------------------------------------------------------
        private RelayCommand _clearRefExpertCommand;

        public ICommand ClearRefExpertCommand => _clearRefExpertCommand ??
                                                  (_clearRefExpertCommand = new RelayCommand(delegate { ClearRefExpert(null); }));         
             
        private RelayCommand _getRefExpertsByNameOrIdCommand;  

        public  ICommand GetRefExpertsByNameOrIdCommand => _getRefExpertsByNameOrIdCommand ??
                                                           (_getRefExpertsByNameOrIdCommand = new RelayCommand(delegate { GetRefExpertsByNameOrId(null); }));        
             
        private RelayCommand _addRefExpertCommand;

        public ICommand AddRefExpertCommand => _addRefExpertCommand ??
                                                (_addRefExpertCommand = new RelayCommand(delegate { AddRefExpert(null); }));

        private RelayCommand _copyRefExpertCommand;

        public ICommand CopyRefExpertCommand => _copyRefExpertCommand ??
                                                 (_copyRefExpertCommand = new RelayCommand(delegate { CopyRefExpert(null); }));      
             
        private RelayCommand _deleteRefExpertCommand;

        public ICommand DeleteRefExpertCommand => _deleteRefExpertCommand ??
                                                   (_deleteRefExpertCommand = new RelayCommand(delegate { DeleteRefExpert(null); }));    
             
        private RelayCommand _saveRefExpertCommand;

        public ICommand SaveRefExpertCommand => _saveRefExpertCommand ??
                                                 (_saveRefExpertCommand = new RelayCommand(delegate { SaveRefExpert(null); }));
        //-------------------------------------------------------------------------          
        
        private void ClearRefExpert(object o)
        {
            SearchRefExpertName = "";

            Tbl90RefExpertsList?.Clear();
        }
        //----------------------------------------------------------------------                  
        
        private void GetRefExpertsByNameOrId(object o)
        {
            if (SearchRefExpertName != "")
            {
                Tbl90RefExpertsList?.Clear();
                if (SearchRefExpertName == "*") // show whole table
                {
                    SearchRefExpertName = "";
                    _businessLayer = new BusinessLayer.BusinessLayer();
                    Tbl90RefExpertsList = new ObservableCollection<Tbl90RefExpert>(_businessLayer.ListTbl90RefExpertsByRefExpertName(SearchRefExpertName));
                    SearchRefExpertName = "*";
                }
                else
                {
                    _businessLayer = new BusinessLayer.BusinessLayer();
                    Tbl90RefExpertsList = int.TryParse(SearchRefExpertName, out var id) ?
                        new ObservableCollection<Tbl90RefExpert>(_businessLayer.ListTbl90RefExpertsByRefExpertId(id)) :
                        new ObservableCollection<Tbl90RefExpert>(_businessLayer.ListTbl90RefExpertsByRefExpertName(SearchRefExpertName));
                }

                if (Tbl90RefExpertsList.Count == 0)
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
            RefExpertsView = CollectionViewSource.GetDefaultView(Tbl90RefExpertsList);
            RefExpertsView.Refresh();
        }
        //------------------------------------------------------------------------------------                          
        
        private void AddRefExpert(object o)
        {
            if (Tbl90RefExpertsList == null)
                Tbl90RefExpertsList =  new ObservableCollection<Tbl90RefExpert>( );

            Tbl90RefExpertsList.Insert(0, new Tbl90RefExpert {   RefExpertName = CultRes.StringsRes.DatasetNew}  );

            RefExpertsView = CollectionViewSource.GetDefaultView(Tbl90RefExpertsList);
            RefExpertsView.MoveCurrentToFirst();
        }
        //------------------------------------------------------------------------------------                               
        
        private void CopyRefExpert(object o)
        {
            if (CurrentTbl90RefExpert == null)
            {
                WpfMessageBox.Show(CultRes.StringsRes.DatasetNew,
                    CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                return;
            }
            _businessLayer = new BusinessLayer.BusinessLayer();

            var refExpert = _businessLayer.SingleListTbl90RefExpertsByRefExpertId(CurrentTbl90RefExpert.RefExpertID);

            Tbl90RefExpertsList.Insert(0, new Tbl90RefExpert
            {
                RefExpertName = CultRes.StringsRes.DatasetNew,
                Valid = refExpert.Valid,
                ValidYear = refExpert.ValidYear,
                Info = refExpert.Info,
                Notes = refExpert.Notes,
                Memo = refExpert.Memo
            });

            RefExpertsView = CollectionViewSource.GetDefaultView(Tbl90RefExpertsList);
            RefExpertsView.MoveCurrentToFirst();
        }
        //---------------------------------------------------------------------------------------                            
        
        private void DeleteRefExpert(object o)
        {
            if (CurrentTbl90RefExpert == null)
            {
                WpfMessageBox.Show(CultRes.StringsRes.DatasetNew,
                    CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                return;
            }
            _businessLayer = new BusinessLayer.BusinessLayer();

            try
            {
                var refExpert = _businessLayer.SingleListTbl90RefExpertsByRefExpertId(CurrentTbl90RefExpert.RefExpertID);
                if (refExpert != null)
                {
                    if (WpfMessageBox.Show(CultRes.StringsRes.DeleteQuestion1, CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl90RefExpert.RefExpertName,
                            MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes)
                        return;
                    refExpert.EntityState = EntityState.Deleted;
                    _businessLayer.RemoveRefExpert(refExpert);

                    WpfMessageBox.Show(CultRes.StringsRes.DeleteSuccess, CurrentTbl90RefExpert.RefExpertName,
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                }
                else
                {
                    WpfMessageBox.Show(CultRes.StringsRes.Information, CultRes.StringsRes.DeleteCan + " " + CurrentTbl90RefExpert.RefExpertName + " " + CultRes.StringsRes.DeleteCan1,
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                }
            }
            catch (DbEntityValidationException ex)
            {
                _entityException.EntityException(ex);
                    Log.Error(ex);
            }

            if (SearchRefExpertName != "")
            {
                if (SearchRefExpertName == "*")  //show all datasets
                {
                    SearchRefExpertName = "";
                    Tbl90RefExpertsList.Clear();
                    
                Tbl90RefExpertsList = new ObservableCollection<Tbl90RefExpert>(_businessLayer.ListTbl90RefExpertsByRefExpertName(SearchRefExpertName));            
                    SearchRefExpertName = "*";
                }
                else
                {               
                    Tbl90RefExpertsList =  new ObservableCollection<Tbl90RefExpert>(_businessLayer.ListTbl90RefExpertsByRefExpertName(SearchRefExpertName));

                }
                RefExpertsView = CollectionViewSource.GetDefaultView(Tbl90RefExpertsList);
                RefExpertsView.Refresh();
            }
            else  //SearchName = empty
            {
                Tbl90RefExpertsList = new ObservableCollection<Tbl90RefExpert>(_businessLayer.ListTbl90RefExpertsByRefExpertName(SearchRefExpertName));

                RefExpertsView = CollectionViewSource.GetDefaultView(Tbl90RefExpertsList);
                RefExpertsView.MoveCurrentToFirst();
             }
        }
        //-------------------------------------------------------------------------------------------------                    
       
        private void SaveRefExpert(object o)
        {
            if (CurrentTbl90RefExpert == null)
            {
                WpfMessageBox.Show(CultRes.StringsRes.DatasetNew,
                    CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                return;
            }
            _businessLayer = new BusinessLayer.BusinessLayer();

            try
            {
                var refExpert = _businessLayer.SingleListTbl90RefExpertsByRefExpertId(CurrentTbl90RefExpert.RefExpertID);
                if (CurrentTbl90RefExpert.RefExpertID != 0)
                {
                    if (refExpert != null) //update
                    {
                            refExpert.RefExpertName = CurrentTbl90RefExpert.RefExpertName;
                            refExpert.Valid = CurrentTbl90RefExpert.Valid;
                            refExpert.ValidYear = CurrentTbl90RefExpert.ValidYear;
                            refExpert.Info = CurrentTbl90RefExpert.Info;
                            refExpert.Notes = CurrentTbl90RefExpert.Notes;
                            refExpert.Updater = Environment.UserName;
                            refExpert.UpdaterDate = DateTime.Now;
                            refExpert.Memo = CurrentTbl90RefExpert.Memo;
                        refExpert.EntityState = EntityState.Modified;
                    }
                }
                else
                {
                            refExpert = new Tbl90RefExpert   //add new
                            {
                            RefExpertName = CurrentTbl90RefExpert.RefExpertName,
                            CountID = RandomHelper.Randomnumber(),
                            Valid = CurrentTbl90RefExpert.Valid,
                            ValidYear = CurrentTbl90RefExpert.ValidYear,
                            Info = CurrentTbl90RefExpert.Info,
                            Notes = CurrentTbl90RefExpert.Notes,
                            Writer = Environment.UserName,
                            WriterDate = DateTime.Now,
                            Updater = Environment.UserName,
                            UpdaterDate = DateTime.Now,
                            Memo = CurrentTbl90RefExpert.Memo,
                            EntityState = EntityState.Added
                    };
                }
                {
                    //check if dataset with Name already exist       
                    var dataset = _businessLayer.ListTbl90RefExpertsByRefExpertName(CurrentTbl90RefExpert.RefExpertName);

                    if (dataset.Count != 0 && CurrentTbl90RefExpert.RefExpertID == 0)  //dataset exist
                    {
                        WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTbl90RefExpert.RefExpertName,
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        return;
                    }
                    if (dataset.Count == 0 && CurrentTbl90RefExpert.RefExpertID == 0 ||
                        dataset.Count != 0 && CurrentTbl90RefExpert.RefExpertID != 0 ||
                        dataset.Count == 0 && CurrentTbl90RefExpert.RefExpertID != 0) //new dataset and update
                    {
                        if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTbl90RefExpert.RefExpertName,
                                MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes)
                            return;
                        {
                            try
                            {
                                _businessLayer.UpdateRefExpert(refExpert);
                                _position = RefExpertsView.CurrentPosition;
                            }
                            catch (DbUpdateException e)
                            {
                                if (e.InnerException != null)
                                    System.Windows.MessageBox.Show(e.InnerException.ToString(), CultRes.StringsRes.FailedToSave,
                                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);

                                Log.Error(e);
                                return;
                            }
                            catch (Exception e)
                            {
                                System.Windows.MessageBox.Show(e.Message, CultRes.StringsRes.Error,
                                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);
                                Log.Error(e);
                                return;
                            }
                                    WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess,
                                        CurrentTbl90RefExpert.RefExpertID == 0
                                            ? CultRes.StringsRes.DatasetNew
                                            : CurrentTbl90RefExpert.RefExpertName,
                                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        }
                    }
                }
            }
            catch (DbEntityValidationException ex)
            {
                _entityException.EntityException(ex);
                    Log.Error(ex);
                  return;
            }

            if (SearchRefExpertName != "")
            {
                if (SearchRefExpertName == "*")  //show all datasets
                {
                    SearchRefExpertName = "";
                    Tbl90RefExpertsList.Clear();
                    
                Tbl90RefExpertsList = new ObservableCollection<Tbl90RefExpert>(_businessLayer.ListTbl90RefExpertsByRefExpertName(SearchRefExpertName));            
                    SearchRefExpertName = "*";
                }
                else
                {               
                    Tbl90RefExpertsList = int.TryParse(SearchRefExpertName, out var id)
                        ? new ObservableCollection<Tbl90RefExpert>(_businessLayer.ListTbl90RefExpertsByRefExpertId(id))
                        : new ObservableCollection<Tbl90RefExpert>(_businessLayer.ListTbl90RefExpertsByRefExpertName(SearchRefExpertName));

                }
                RefExpertsView = CollectionViewSource.GetDefaultView(Tbl90RefExpertsList);
                RefExpertsView.MoveCurrentToPosition(_position);
            }
            else  
            {
                Tbl90RefExpertsList = new ObservableCollection<Tbl90RefExpert>(_businessLayer.ListTbl90RefExpertsByRefExpertName(CurrentTbl90RefExpert.RefExpertName));

                RefExpertsView= CollectionViewSource.GetDefaultView(Tbl90RefExpertsList);
                RefExpertsView.Refresh();
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

     
        #region "Public Properties Tbl90RefExpert"

        private string _searchRefExpertName = "";
        public string SearchRefExpertName
        {
            get => _searchRefExpertName; 
            set { _searchRefExpertName = value; RaisePropertyChanged();  }
        }

        public  ICollectionView RefExpertsView;
        private   Tbl90RefExpert CurrentTbl90RefExpert => RefExpertsView?.CurrentItem as Tbl90RefExpert;

        private ObservableCollection<Tbl90RefExpert> _tbl90RefExpertsList;
        public  ObservableCollection<Tbl90RefExpert> Tbl90RefExpertsList
        {
            get => _tbl90RefExpertsList; 
            set {  _tbl90RefExpertsList = value; RaisePropertyChanged();   }
        }

        private ObservableCollection<Tbl90RefExpert> _tbl90RefExpertsAllList;
        public  ObservableCollection<Tbl90RefExpert> Tbl90RefExpertsAllList
        {
            get => _tbl90RefExpertsAllList; 
            set {  _tbl90RefExpertsAllList = value; RaisePropertyChanged();   }
        }

        #endregion "Public Properties"   
 

 



   }
}   
