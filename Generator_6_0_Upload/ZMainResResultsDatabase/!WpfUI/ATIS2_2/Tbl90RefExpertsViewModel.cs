using System;
using System.Collections.ObjectModel;
using System.Data.Entity.Validation;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using DAL;
using DAL.Helper;
using DAL.Models;
using GalaSoft.MvvmLight;
using WPFUI.MessageBox;
using WPFUI.Utility;
using WPFUI.Views.Main;
using MessageBoxImage = System.Windows.MessageBoxImage;      

    
         //    Tbl90RefExpertsViewModel Skriptdatum:  14.11.2017  10:32    

namespace WPFUI.Views.Database
{     
    
    public partial class Tbl90RefExpertsViewModel : ViewModelBase                     
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
        private  new bool IsInDesignMode { get; set; }

        #endregion "Constructor"             
 

 //    Part 1    

             
        #region "Public Commands Basic Tbl90RefExpert"

        private RelayCommand _getExpertByNameOrIdCommand;    
    
        public ICommand GetExpertByNameOrIdCommand    
    
        {
            get { return _getExpertByNameOrIdCommand ?? (_getExpertByNameOrIdCommand = new RelayCommand(delegate { GetExpertByNameOrId(null); })); }   
        }

        private void GetExpertByNameOrId(object o)       
        {   
       
            int id;
            if (int.TryParse(SearchRefExpertName, out id))
                Tbl90RefExpertsList = new ObservableCollection<Tbl90RefExpert> { _tbl90RefExpertsRepository.Get(id) };
             else
                Tbl90RefExpertsList = _allListVm.GetValueTbl90ExpertsList(SearchRefExpertName);     
ExpertsView = CollectionViewSource.GetDefaultView(Tbl90RefExpertsList);
            ExpertsView.Refresh();
        }
        //------------------------------------------------------------------------------------                
           
        private RelayCommand _addExpertCommand;           
    
        public ICommand AddExpertCommand       
    
        {
            get { return _addExpertCommand ?? (_addExpertCommand = new RelayCommand(delegate { AddExpert(null); })); }
        }

        private void AddExpert(object o)
        {
            Tbl90RefExpertsList = new ObservableCollection<Tbl90RefExpert>();   
Tbl90RefExpertsList.Insert(0, new Tbl90RefExpert{ RefExpertName= CultRes.StringsRes.DatasetNew });       
ExpertsView = CollectionViewSource.GetDefaultView(Tbl90RefExpertsList);
            ExpertsView.Refresh();
        }
        //------------------------------------------------------------------------------------                
           
        private RelayCommand _copyExpertCommand;              
    
        public ICommand CopyExpertCommand             
           
        {
            get { return _copyExpertCommand ?? (_copyExpertCommand = new RelayCommand(delegate { CopyExpert(null); })); }
        }

        private void CopyExpert(object o)
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
        //---------------------------------------------------------------------------------------                  
           
        private RelayCommand _deleteExpertCommand;              
    
        public ICommand DeleteExpertCommand             
             
        {
            get { return _deleteExpertCommand ?? (_deleteExpertCommand = new RelayCommand(delegate { DeleteExpert(null); })); }
        }

        private void DeleteExpert(object o)
        {
            try
            {
                var refExpert = _tbl90RefExpertsRepository.Get(CurrentTbl90RefExpert.RefExpertID);
                if (refExpert != null)
                {   
          
                    if (WpfMessageBox.Show(CultRes.StringsRes.DeleteQuestion1, CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl90RefExpert.RefExpertName,
                         MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
                        return;    
_tbl90RefExpertsRepository.Delete(refExpert);
                    _tbl90RefExpertsRepository.Save();     
          
                    WpfMessageBox.Show(CultRes.StringsRes.DeleteSuccess, CurrentTbl90RefExpert.RefExpertName,
                        MessageBoxButton.OK, MessageBoxImage.Information);   
               
                                        GetExpertByNameOrId(o);  //search                                                                  
ExpertsView = CollectionViewSource.GetDefaultView(Tbl90RefExpertsList);
                    ExpertsView.Refresh();
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
           
        private RelayCommand _saveExpertCommand;              
     
        public ICommand SaveExpertCommand             
           
        {
            get { return _saveExpertCommand ?? (_saveExpertCommand = new RelayCommand(delegate { SaveExpert(null); })); }
        }

        private void SaveExpert(object o)
        {
            try
            {
                var refExpert = _tbl90RefExpertsRepository.Get(CurrentTbl90RefExpert.RefExpertID);
                if (CurrentTbl90RefExpert == null)
                    WpfMessageBox.Show(CultRes.StringsRes.Warning, CultRes.StringsRes.DatasetNotExist,
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                else
                {
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
         
                        }
                    }
                    else
                    {
                        _tbl90RefExpertsRepository.Add(new Tbl90RefExpert     //add new
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
                            Memo = CurrentTbl90RefExpert.Memo 
        
                        });
                    }
                    {    
             
                        //check about double Name
                        var dataset = new ObservableCollection<Tbl90RefExpert>
                        (from a in _tbl90RefExpertsRepository.GetAll()
                         where
                         a.RefExpertName.Trim() == CurrentTbl90RefExpert.RefExpertName.Trim()     
         
                         select a);

                        if (dataset.Count != 0 && CurrentTbl90RefExpert.RefExpertID == 0)  //dataset exist
                        {       
             
                            WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTbl90RefExpert.RefExpertName,
                            MessageBoxButton.OK, MessageBoxImage.Information);  
             
                        }
                        if (dataset.Count == 0 && CurrentTbl90RefExpert.RefExpertID == 0 ||
                            dataset.Count != 0 && CurrentTbl90RefExpert.RefExpertID != 0 ||
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

                        if (CurrentTbl90RefExpert.RefExpertID == 0)  //new Dataset
                            GetExpertByNameOrId(o); //search                             
                        
                        Tbl90RefExpertsList = new ObservableCollection<Tbl90RefExpert>
                                                  (from x in _tbl90RefExpertsRepository.GetAll()
                                                   where x.RefExpertID == CurrentTbl90RefExpert.RefExpertID
                                                   select x);  
ExpertsView = CollectionViewSource.GetDefaultView(Tbl90RefExpertsList);
                        ExpertsView.Refresh();

                    }
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

 




   }
}   
