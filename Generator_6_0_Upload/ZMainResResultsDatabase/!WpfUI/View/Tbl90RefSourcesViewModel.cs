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
    
         //    Tbl90RefSourcesViewModel Skriptdatum:   14.11.2017  10:32    

namespace WPFUI.Views.Database
{     
    
    public class Tbl90RefSourcesViewModel : ViewModelBase                     
    {     
        
        #region "Private Data Members"

        private readonly AllListVm _allListVm = new AllListVm();
        private readonly Repository<Tbl90RefSource, int> _tbl90RefSourcesRepository = new Repository<Tbl90RefSource, int>();  
    

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
            }
        }
        private new bool IsInDesignMode { get; set; }

        #endregion "Constructor"           
 

 //    Part 1    

           
        #region "Public Commands Basic Tbl90RefSource"

        private RelayCommand _getSourceByNameOrIdCommand;     
    
        public ICommand GetSourceByNameOrIdCommand    
    
        {
            get { return _getSourceByNameOrIdCommand ?? (_getSourceByNameOrIdCommand = new RelayCommand(delegate { GetSourceByNameOrId(null); })); }   
        }

        private void GetSourceByNameOrId(object o)       
        {   
       
            int id;
            if (int.TryParse(SearchRefSourceName, out id))
                Tbl90RefSourcesList = new ObservableCollection<Tbl90RefSource> { _tbl90RefSourcesRepository.Get(id) };
            else
                Tbl90RefSourcesList = _allListVm.GetValueTbl90SourcesList(SearchRefSourceName);     
SourcesView = CollectionViewSource.GetDefaultView(Tbl90RefSourcesList);
            SourcesView.Refresh();
        }
        //------------------------------------------------------------------------------                
           
        private RelayCommand _addSourceCommand;           
    
        public ICommand AddSourceCommand       
    
        {
            get { return _addSourceCommand ?? (_addSourceCommand = new RelayCommand(delegate { AddSource(null); })); }
        }

        private void AddSource(object o)
        {
            Tbl90RefSourcesList = new ObservableCollection<Tbl90RefSource>();   
Tbl90RefSourcesList.Insert(0, new Tbl90RefSource{ RefSourceName= CultRes.StringsRes.DatasetNew });       
               
            SourcesView = CollectionViewSource.GetDefaultView(Tbl90RefSourcesList);
            SourcesView.Refresh();
        }
        //------------------------------------------------------------------------------                
           
        private RelayCommand _copySourceCommand;              
    
        public ICommand CopySourceCommand             
         
        {
            get { return _copySourceCommand ?? (_copySourceCommand = new RelayCommand(delegate { CopySource(null); })); }
        }

        private void CopySource(object o)
        {
            Tbl90RefSourcesList = new ObservableCollection<Tbl90RefSource>();

            var refSource = _tbl90RefSourcesRepository.Get(CurrentTbl90RefSource.RefSourceID);

            Tbl90RefSourcesList.Insert(0, new Tbl90RefSource
            {                 
RefSourceName = CultRes.StringsRes.DatasetNew,              
                            Valid = refSource.Valid,
                            ValidYear = refSource.ValidYear,              
                            SourceYear = refSource.SourceYear,
                            Info = refSource.Info,
                            Notes = refSource.Notes,
                            Memo = refSource.Memo       
               
            });
            SourcesView = CollectionViewSource.GetDefaultView(Tbl90RefSourcesList);
            SourcesView.Refresh();
        }
        //------------------------------------------------------------------------------                
           
        private RelayCommand _deleteSourceCommand;              
    
        public ICommand DeleteSourceCommand             
                
        {
            get { return _deleteSourceCommand ?? (_deleteSourceCommand = new RelayCommand(delegate { DeleteSource(null); })); }
        }

        private void DeleteSource(object o)
        {
            try
            {
                var refSource = _tbl90RefSourcesRepository.Get(CurrentTbl90RefSource.RefSourceID);
                if (refSource!= null)
                {
                    if (WpfMessageBox.Show(CultRes.StringsRes.DeleteQuestion1, CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl90RefSource.RefSourceName,
                         MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes) 
                    return;

                    _tbl90RefSourcesRepository.Delete(refSource);
                    _tbl90RefSourcesRepository.Save();

                    WpfMessageBox.Show(CultRes.StringsRes.DeleteSuccess, CurrentTbl90RefSource.RefSourceName, 
                        MessageBoxButton.OK, MessageBoxImage.Information);     

                    GetSourceByNameOrId(o);  //search       
SourcesView = CollectionViewSource.GetDefaultView(Tbl90RefSourcesList);
                        SourcesView.Refresh();
                }
                else
                {
                    WpfMessageBox.Show(CultRes.StringsRes.Information, CultRes.StringsRes.DeleteCan + " " + CurrentTbl90RefSource.RefSourceName+ " " + CultRes.StringsRes.DeleteCan1,
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
           
        private RelayCommand _saveSourceCommand;              
     
        public ICommand SaveSourceCommand             
         
        {
            get { return _saveSourceCommand ?? (_saveSourceCommand = new RelayCommand(delegate { SaveSource(null); })); }
        }

        private void SaveSource(object o)
        {
            try
            {
                var refSource = _tbl90RefSourcesRepository.Get(CurrentTbl90RefSource.RefSourceID);
                if (CurrentTbl90RefSource == null)              
                    WpfMessageBox.Show(CultRes.StringsRes.Warning, CultRes.StringsRes.DatasetNotExist, 
                        MessageBoxButton.OK, MessageBoxImage.Warning);               
                else
                {
                    if (CurrentTbl90RefSource.RefSourceID!= 0)
                    {
                        if (refSource!= null) //update
                        {   
refSource.RefSourceName = CurrentTbl90RefSource.RefSourceName;  
                
                            refSource.Valid = CurrentTbl90RefSource.Valid;
                            refSource.ValidYear = CurrentTbl90RefSource.ValidYear;
                            refSource.Info = CurrentTbl90RefSource.Info;
                            refSource.Notes = CurrentTbl90RefSource.Notes;
                            refSource.Updater = Environment.UserName;
                            refSource.UpdaterDate = DateTime.Now;
                            refSource.Memo = CurrentTbl90RefSource.Memo;                      
         
                        }
                    }
                    else
                    {
                        _tbl90RefSourcesRepository.Add(new Tbl90RefSource     //add new
                        {   
RefSourceName= CurrentTbl90RefSource.RefSourceName,              
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
                            Memo = CurrentTbl90RefSource.Memo  
                
                        });
                    }
                    {
                        //check about double Name
                        var dataset = new ObservableCollection<Tbl90RefSource>
                        (from a in _tbl90RefSourcesRepository.GetAll()
                         where
                         a.RefSourceName == CurrentTbl90RefSource.RefSourceName &&
                         a.SourceYear == CurrentTbl90RefSource.SourceYear
                         select a);

                        if (dataset.Count != 0 && CurrentTbl90RefSource.RefSourceID == 0)  //dataset exist
                        {
                            WpfMessageBox.Show(CultRes.StringsRes.DatasetExist,
                                CurrentTbl90RefSource.RefSourceName + " " + CurrentTbl90RefSource.SourceYear,
                            MessageBoxButton.OK, MessageBoxImage.Information);                       
                        }

                        if (dataset.Count == 0 && CurrentTbl90RefSource.RefSourceID == 0 ||
                            dataset.Count != 0 && CurrentTbl90RefSource.RefSourceID != 0 ||
                            dataset.Count == 0 && CurrentTbl90RefSource.RefSourceID != 0) //new dataset and update
                        {
                            if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, 
                                CurrentTbl90RefSource.RefSourceName + " " + CurrentTbl90RefSource.SourceYear,
                                    MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
                                return;
                            {
                                _tbl90RefSourcesRepository.Save();

                                WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess,
                                CurrentTbl90RefSource.RefSourceName + " " + CurrentTbl90RefSource.SourceYear,
                                    MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                        }  
               
                        if (CurrentTbl90RefSource.RefSourceID== 0)  //new Dataset
                                GetSourceByNameOrId(o); //search                             
                        {
                            Tbl90RefSourcesList = new ObservableCollection<Tbl90RefSource>
                                                  (from x in _tbl90RefSourcesRepository.GetAll()
                                                   where x.RefSourceID == CurrentTbl90RefSource.RefSourceID
                                                   select x);

            SourcesView = CollectionViewSource.GetDefaultView(Tbl90RefSourcesList);
            SourcesView.Refresh();
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

   
     
        #region "Public Properties Tbl90RefSource"

        public ICollectionView SourcesView;
        public Tbl90RefSource CurrentTbl90RefSource => SourcesView?.CurrentItem as Tbl90RefSource;
        
        private string _searchRefSourceName;
        public string SearchRefSourceName
        {
            get => _searchRefSourceName; 
            set { _searchRefSourceName = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl90RefSource> _tbl90RefSourcesList;
        public  ObservableCollection<Tbl90RefSource> Tbl90RefSourcesList
        {
            get => _tbl90RefSourcesList; 
            set { _tbl90RefSourcesList = value; RaisePropertyChanged();  }
        }

        private ObservableCollection<Tbl90RefSource> _tbl90RefSourcesAllList;
        public ObservableCollection<Tbl90RefSource> Tbl90RefSourcesAllList
        {
            get => _tbl90RefSourcesAllList; 
            set { _tbl90RefSourcesAllList = value; RaisePropertyChanged(); }
        }

        #endregion "Public Properties"
   
 

 //    Part 12    

 

   }
}   
