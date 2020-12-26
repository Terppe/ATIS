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
    
         //    Tbl90RefExpertsViewModel Skriptdatum:  29.11.2018  10:32    

namespace WPFUI.Views.Database
{     
    
    public class Tbl90RefExpertsViewModel : ViewModelBase                     
    {     
        
        #region "Private Data Members"

        private readonly AllListVm _allListVm = new AllListVm();
        private readonly Repository<Tbl90RefExpert, int> _tbl90RefExpertsRepository = new Repository<Tbl90RefExpert, int>();  
    

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
            }
        }
        private new bool IsInDesignMode { get; set; }

        #endregion "Constructor"           
 

 //    Part 1    

           
        #region "Public Commands Basic Tbl90RefExpert"

        private RelayCommand _getRefExpertByNameOrIdCommand;     
    
        public ICommand GetRefExpertByNameOrIdCommand    
    
        {
            get { return _getRefExpertByNameOrIdCommand ?? (_getRefExpertByNameOrIdCommand = new RelayCommand(delegate { GetRefExpertByNameOrId(null); })); }   
        }

        private void GetRefExpertByNameOrId(object o)       
        {   
       
            int id;
            if (int.TryParse(SearchRefExpertName, out id))
                Tbl90RefExpertsList = new ObservableCollection<Tbl90RefExpert> { _tbl90RefExpertsRepository.Get(id) };
             else
                Tbl90RefExpertsList = _allListVm.GetValueTbl90ExpertsList(SearchRefExpertName);     
RefExpertsView = CollectionViewSource.GetDefaultView(Tbl90RefExpertsList);
            RefExpertsView.Refresh();
        }
        //------------------------------------------------------------------------------                
           
        private RelayCommand _addRefExpertCommand;           
    
        public ICommand AddRefExpertCommand       
    
        {
            get { return _addRefExpertCommand ?? (_addRefExpertCommand = new RelayCommand(delegate { AddRefExpert(null); })); }
        }

        private void AddRefExpert(object o)
        {
            Tbl90RefExpertsList = new ObservableCollection<Tbl90RefExpert>();   
Tbl90RefExpertsList.Insert(0, new Tbl90RefExpert{ RefExpertName= CultRes.StringsRes.DatasetNew });       
               
            ExpertsView = CollectionViewSource.GetDefaultView(Tbl90RefExpertsList);
            ExpertsView.Refresh();
        }
        //------------------------------------------------------------------------------                
           
        private RelayCommand _copyRefExpertCommand;              
    
        public ICommand CopyRefExpertCommand             
         
        {
            get { return _copyRefExpertCommand ?? (_copyRefExpertCommand = new RelayCommand(delegate { CopyRefExpert(null); })); }
        }

        private void CopyRefExpert(object o)
        {
            Tbl90RefExpertsList = new ObservableCollection<Tbl90RefExpert>();

            var refExpert = _tbl90RefExpertsRepository.Get(CurrentTbl90RefExpert.RefExpertID);

            Tbl90RefExpertsList.Insert(0, new Tbl90RefExpert
            {                 
RefExpertName = CultRes.StringsRes.DatasetNew,              
                            Valid = refExpert.Valid,
                            ValidYear = refExpert.ValidYear,              
                            Info = refExpert.Info,
                            Notes = refExpert.Notes,
                            Memo = refExpert.Memo      
               
            });
            ExpertsView = CollectionViewSource.GetDefaultView(Tbl90RefExpertsList);
            ExpertsView.Refresh();
        }
        //------------------------------------------------------------------------------                
           
        private RelayCommand _deleteRefExpertCommand;              
    
        public ICommand DeleteRefExpertCommand             
                
        {
            get { return _deleteRefExpertCommand ?? (_deleteRefExpertCommand = new RelayCommand(delegate { DeleteRefExpert(null); })); }
        }

        private void DeleteRefExpert(object o)
        {
            try
            {
                var refExpert = _tbl90RefExpertsRepository.Get(CurrentTbl90RefExpert.RefExpertID);
                if (refExpert!= null)
                {
                    if (WpfMessageBox.Show(CultRes.StringsRes.DeleteQuestion1, CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl90RefExpert.RefExpertName,
                         MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes) 
                    return;

                    _tbl90RefExpertsRepository.Delete(refExpert);
                    _tbl90RefExpertsRepository.Save();

                    WpfMessageBox.Show(CultRes.StringsRes.DeleteSuccess, CurrentTbl90RefExpert.RefExpertName, 
                        MessageBoxButton.OK, MessageBoxImage.Information);     

                    GetExpertByNameOrId(o);  //search       
RefExpertsView = CollectionViewSource.GetDefaultView(Tbl90RefExpertsList);
                        RefExpertsView.Refresh();
                }
                else
                {
                    WpfMessageBox.Show(CultRes.StringsRes.Information, CultRes.StringsRes.DeleteCan + " " + CurrentTbl90RefExpert.RefExpertName+ " " + CultRes.StringsRes.DeleteCan1,
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
           
        private RelayCommand _saveRefExpertCommand;              
     
        public ICommand SaveRefExpertCommand             
         
        {
            get { return _saveRefExpertCommand ?? (_saveRefExpertCommand = new RelayCommand(delegate { SaveRefExpert(null); })); }
        }

        private void SaveRefExpert(object o)
        {
            try
            {
                var refExpert = _tbl90RefExpertsRepository.Get(CurrentTbl90RefExpert.RefExpertID);
                if (CurrentTbl90RefExpert == null)              
                    WpfMessageBox.Show(CultRes.StringsRes.Warning, CultRes.StringsRes.DatasetNotExist, 
                        MessageBoxButton.OK, MessageBoxImage.Warning);               
                else
                {
                    if (CurrentTbl90RefExpert.RefExpertID!= 0)
                    {
                        if (refExpert!= null) //update
                        {   
refExpert.RefExpertName = CurrentTbl90RefExpert.RefExpertName;  
                
                            refExpert.Valid = CurrentTbl90RefExpert.Valid;
                            refExpert.ValidYear = CurrentTbl90RefExpert.ValidYear;
                            refExpert.Info = CurrentTbl90RefExpert.Info;
                            refExpert.Notes = CurrentTbl90RefExpert.Notes;
                            refExpert.Updater = Environment.UserName;
                            refExpert.UpdaterDate = DateTime.Now;
                            refExpert.Memo = CurrentTbl90RefExpert.Memo;                      
         
                        }
                    }
                    else
                    {
                        _tbl90RefExpertsRepository.Add(new Tbl90RefExpert     //add new
                        {   
RefExpertName= CurrentTbl90RefExpert.RefExpertName,              
                            CountID = RandomHelper.Randomnumber(),
                            Valid = CurrentTbl90RefExpert.Valid,
                            ValidYear = CurrentTbl90RefExpert.ValidYear,
                            Info = CurrentTbl90RefExpert.Info,
                            Notes = CurrentTbl90RefExpert.Notes,
                            Writer = Environment.UserName,
                            WriterDate = DateTime.Now,
                            Updater = Environment.UserName,
                            UpdaterDate = DateTime.Now,
                            Memo = CurrentTbl90RefExpert.Memo  
                
                        });
                    }
                    {
                        //check about double Name
                        var dataset = new ObservableCollection<Tbl90RefExpert>
                        (from a in _tbl90RefExpertsRepository.GetAll()
                         where
                         a.RefExpertName == CurrentTbl90RefExpert.RefExpertName  
                         select a);

                        if (dataset.Count != 0 && CurrentTbl90RefExpert.RefExpertID== 0)  //dataset exist
                        {
                              WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTbl90RefExpert.RefExpertName,
                              MessageBoxButton.OK, MessageBoxImage.Information);
                        }

                        if (dataset.Count == 0 && CurrentTbl90RefExpert.RefExpertID == 0 ||
                            dataset.Count != 0 && CurrentTbl90RefExpert.RefExpertID != 0  ||
                            dataset.Count == 0 && CurrentTbl90RefExpert.RefExpertID != 0) //new dataset and update
                        {
                            if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTbl90RefExpert.RefExpertName,
                                    MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)                            
                                return;                            
                            {
                                _tbl90RefExpertsRepository.Save();

                                WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess, CurrentTbl90RefExpert.RefExpertName,
                                    MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                        }  
               
                        if (CurrentTbl90RefExpert.RefExpertID== 0)  //new Dataset
                                GetRefExpertByNameOrId(o); //search                             
                        {
                            Tbl90RefExpertsList = new ObservableCollection<Tbl90RefExpert>
                                                  (from x in _tbl90RefExpertsRepository.GetAll()
                                                   where x.RefExpertID == CurrentTbl90RefExpert.RefExpertID
                                                   select x);

            ExpertsView = CollectionViewSource.GetDefaultView(Tbl90RefExpertsList);
            ExpertsView.Refresh();
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

  
     
        #region "Public Properties Tbl90RefExpert"

        public ICollectionView ExpertsView;
        public Tbl90RefExpert CurrentTbl90RefExpert => ExpertsView?.CurrentItem as Tbl90RefExpert;
        
        private string _searchRefExpertName;
        public string SearchRefExpertName
        {
            get => _searchRefExpertName; 
            set { _searchRefExpertName = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl90RefExpert> _tbl90RefExpertsList;
        public  ObservableCollection<Tbl90RefExpert> Tbl90RefExpertsList
        {
            get => _tbl90RefExpertsList; 
            set { _tbl90RefExpertsList = value; RaisePropertyChanged();  }
        }

        private ObservableCollection<Tbl90RefExpert> _tbl90RefExpertsAllList;
        public ObservableCollection<Tbl90RefExpert> Tbl90RefExpertsAllList
        {
            get => _tbl90RefExpertsAllList; 
            set { _tbl90RefExpertsAllList = value; RaisePropertyChanged(); }
        }

        #endregion "Public Properties"
   
 

 //    Part 12    

 

   }
}   
