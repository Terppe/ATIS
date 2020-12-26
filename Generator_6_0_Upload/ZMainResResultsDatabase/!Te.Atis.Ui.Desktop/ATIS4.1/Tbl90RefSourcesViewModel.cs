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

    
         //    Tbl90RefSourcesViewModel Skriptdatum:   24.07.2018  10:32    

namespace Te.Atis.Ui.Desktop.Views.Database
{     
    
    public class Tbl90RefSourcesViewModel : ViewModelBase                     
    {     
         
        #region "Private Data Members"

        private static IBusinessLayer _businessLayer;
        private static DbEntityException _entityException;   
         
        #endregion "Private Data Members"               
      
        #region "Constructor"

        public Tbl90RefSourcesViewModel()
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

             
        #region "Public Commands Basic Tbl90RefSource"
        //-------------------------------------------------------------------------
        private RelayCommand _clearRefSourceCommand;

        public ICommand ClearRefSourceCommand => _clearRefSourceCommand ??
                                                  (_clearRefSourceCommand = new RelayCommand(delegate { ClearRefSource(null); }));         
             
        private RelayCommand _getRefSourcesByNameOrIdCommand;  

        public  ICommand GetRefSourcesByNameOrIdCommand => _getRefSourcesByNameOrIdCommand ??
                                                           (_getRefSourcesByNameOrIdCommand = new RelayCommand(delegate { GetRefSourcesByNameOrId(null); }));        
             
        private RelayCommand _addRefSourceCommand;

        public ICommand AddRefSourceCommand => _addRefSourceCommand ??
                                                (_addRefSourceCommand = new RelayCommand(delegate { AddRefSource(null); }));

        private RelayCommand _copyRefSourceCommand;

        public ICommand CopyRefSourceCommand => _copyRefSourceCommand ??
                                                 (_copyRefSourceCommand = new RelayCommand(delegate { CopyRefSource(null); }));      
             
        private RelayCommand _deleteRefSourceCommand;

        public ICommand DeleteRefSourceCommand => _deleteRefSourceCommand ??
                                                   (_deleteRefSourceCommand = new RelayCommand(delegate { DeleteRefSource(null); }));    
             
        private RelayCommand _saveRefSourceCommand;

        public ICommand SaveRefSourceCommand => _saveRefSourceCommand ??
                                                 (_saveRefSourceCommand = new RelayCommand(delegate { SaveRefSource(null); }));
        //-------------------------------------------------------------------------          
        
        private void ClearRefSource(object o)
        {
            SearchRefSourceName = string.Empty;

            Tbl90RefSourcesList?.Clear();
        }
        //----------------------------------------------------------------------                  
        
        private void GetRefSourcesByNameOrId(object o)
        {
            Tbl90RefSourcesList = int.TryParse(SearchRefSourceName, out var id) ?
                new ObservableCollection<Tbl90RefSource>(_businessLayer.ListTbl90RefSourcesByRefSourceId(id)) :
                new ObservableCollection<Tbl90RefSource>(_businessLayer.ListTbl90RefSourcesByRefSourceName(SearchRefSourceName));

            RefSourcesView = CollectionViewSource.GetDefaultView(Tbl90RefSourcesList);
            RefSourcesView.Refresh();
        }
        //------------------------------------------------------------------------------------                          
        
        private void AddRefSource(object o)
        {
            Tbl90RefSourcesList = new ObservableCollection<Tbl90RefSource> {new Tbl90RefSource
                    {   RefSourceName = CultRes.StringsRes.DatasetNew      }  };

            RefSourcesView = CollectionViewSource.GetDefaultView(Tbl90RefSourcesList);
            RefSourcesView.MoveCurrentToFirst();
        }
        //------------------------------------------------------------------------------------                               
        
        private void CopyRefSource(object o)
        {
            Tbl90RefSourcesList = new ObservableCollection<Tbl90RefSource>();

            var refSource = _businessLayer.SingleListTbl90RefSourcesByRefSourceId(CurrentTbl90RefSource.RefSourceID);

            Tbl90RefSourcesList.Add(new Tbl90RefSource
            {
                RefSourceName = CultRes.StringsRes.DatasetNew,
                Valid = refSource.Valid,
                ValidYear = refSource.ValidYear,
                SourceYear = refSource.SourceYear,
                Info = refSource.Info,
                Notes = refSource.Notes,
                Memo = refSource.Memo
            });

            RefSourcesView = CollectionViewSource.GetDefaultView(Tbl90RefSourcesList);
            RefSourcesView.MoveCurrentToFirst();
        }
        //---------------------------------------------------------------------------------------                            
     
        private void DeleteRefSource(object o)
        {
            try
            {
                var refSource = _businessLayer.SingleListTbl90RefSourcesByRefSourceId(CurrentTbl90RefSource.RefSourceID);
                if (refSource != null)
                {
                    if (WpfMessageBox.Show(CultRes.StringsRes.DeleteQuestion1, CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl90RefSource.RefSourceName,
                            MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes)
                        return;
                    refSource.EntityState = EntityState.Deleted;
                    _businessLayer.RemoveRefSource(refSource);

                    WpfMessageBox.Show(CultRes.StringsRes.DeleteSuccess, CurrentTbl90RefSource.RefSourceName,
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                }
                else
                {
                    WpfMessageBox.Show(CultRes.StringsRes.Information, CultRes.StringsRes.DeleteCan + " " + CurrentTbl90RefSource.RefSourceName + " " + CultRes.StringsRes.DeleteCan1,
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                }
            }
            catch (DbEntityValidationException ex)
            {
                _entityException.EntityException(ex);
            }

            Tbl90RefSourcesList = new ObservableCollection<Tbl90RefSource>(_businessLayer.ListTbl90RefSourcesByRefSourceName(SearchRefSourceName));

            RefSourcesView = CollectionViewSource.GetDefaultView(Tbl90RefSourcesList);
            RefSourcesView.Refresh();
        }
        //-------------------------------------------------------------------------------------------------                    
       
        private void SaveRefSource(object o)
        {
            try
            {
                var refSource = _businessLayer.SingleListTbl90RefSourcesByRefSourceId(CurrentTbl90RefSource.RefSourceID);
                if (CurrentTbl90RefSource.RefSourceID != 0)
                {
                    if (refSource != null) //update
                    {
                            refSource.RefSourceName = CurrentTbl90RefSource.RefSourceName;
                            refSource.Valid = CurrentTbl90RefSource.Valid;
                            refSource.ValidYear = CurrentTbl90RefSource.ValidYear;
                            refSource.SourceYear = CurrentTbl90RefSource.SourceYear;
                            refSource.Info = CurrentTbl90RefSource.Info;
                            refSource.Notes = CurrentTbl90RefSource.Notes;
                            refSource.Updater = Environment.UserName;
                            refSource.UpdaterDate = DateTime.Now;
                            refSource.Memo = CurrentTbl90RefSource.Memo;
                        refSource.EntityState = EntityState.Modified;
                    }
                }
                else
                {
                            refSource = new Tbl90RefSource   //add new
                            {
                            RefSourceName = CurrentTbl90RefSource.RefSourceName,
                            CountID = RandomHelper.Randomnumber(),
                            Valid = CurrentTbl90RefSource.Valid,
                            ValidYear = CurrentTbl90RefSource.ValidYear,
                            SourceYear = CurrentTbl90RefSource.SourceYear,
                            Info = CurrentTbl90RefSource.Info,
                            Notes = CurrentTbl90RefSource.Notes,
                            Writer = Environment.UserName,
                            WriterDate = DateTime.Now,
                            Updater = Environment.UserName,
                            UpdaterDate = DateTime.Now,
                            Memo = CurrentTbl90RefSource.Memo,
                            EntityState = EntityState.Added
                    };
                }
                {
                    //check if dataset with Name and SourceYear already exist       
                    var dataset = _businessLayer.ListTbl90RefSourcesByRefSourceNameAndSourceYear(CurrentTbl90RefSource.RefSourceName, CurrentTbl90RefSource.SourceYear);

                    if (dataset.Count != 0 && CurrentTbl90RefSource.RefSourceID == 0)  //dataset exist
                    {
                        WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTbl90RefSource.RefSourceName,
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                    }
                    if (dataset.Count == 0 && CurrentTbl90RefSource.RefSourceID == 0 ||
                        dataset.Count != 0 && CurrentTbl90RefSource.RefSourceID != 0 ||
                        dataset.Count == 0 && CurrentTbl90RefSource.RefSourceID != 0) //new dataset and update
                    {
                        if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTbl90RefSource.RefSourceName,
                                MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes)
                            return;
                        {
                            _businessLayer.UpdateRefSource(refSource);

                            WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess, CurrentTbl90RefSource.RefSourceName,
                                MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        }
                    }
                }
            }
            catch (DbEntityValidationException ex)
            {
                _entityException.EntityException(ex);
            }

            if (CurrentTbl90RefSource.RefSourceID == 0)  //new Dataset                        
                Tbl90RefSourcesList = new ObservableCollection<Tbl90RefSource>(_businessLayer.ListTbl90RefSourcesByRefSourceName(CurrentTbl90RefSource.RefSourceName));
            if (CurrentTbl90RefSource.RefSourceID != 0)   //update 
                Tbl90RefSourcesList = new ObservableCollection<Tbl90RefSource>(_businessLayer.ListTbl90RefSourcesByRefSourceId(CurrentTbl90RefSource.RefSourceID));

            RefSourcesView = CollectionViewSource.GetDefaultView(Tbl90RefSourcesList);
            RefSourcesView.Refresh();
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

     
        #region "Public Properties Tbl90RefSource"

        private string _searchRefSourceName = string.Empty;
        public string SearchRefSourceName
        {
            get => _searchRefSourceName; 
            set { _searchRefSourceName = value; RaisePropertyChanged();  }
        }

        public  ICollectionView RefSourcesView;
        private   Tbl90RefSource CurrentTbl90RefSource => RefSourcesView?.CurrentItem as Tbl90RefSource;

        private ObservableCollection<Tbl90RefSource> _tbl90RefSourcesList;
        public  ObservableCollection<Tbl90RefSource> Tbl90RefSourcesList
        {
            get => _tbl90RefSourcesList; 
            set {  _tbl90RefSourcesList = value; RaisePropertyChanged();   }
        }

        private ObservableCollection<Tbl90RefSource> _tbl90RefSourcesAllList;
        public  ObservableCollection<Tbl90RefSource> Tbl90RefSourcesAllList
        {
            get => _tbl90RefSourcesAllList; 
            set {  _tbl90RefSourcesAllList = value; RaisePropertyChanged();   }
        }

        #endregion "Public Properties"   
 

 



   }
}   
