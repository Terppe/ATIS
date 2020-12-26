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
    
         //    Tbl03RegnumsViewModel Skriptdatum:  27.11.2020  12:32      

namespace WPFUI.Views.Database
{     
    
    public class Tbl03RegnumsViewModel : ViewModelBase                     
    {     
        
        #region "Private Data Members"

        private readonly AllListVm _allListVm = new AllListVm();
        private readonly Repository<Tbl03Regnum, int> _tbl03RegnumsRepository = new Repository<Tbl03Regnum, int>();   
           
        private readonly Repository<Tbl06Phylum, int> _tbl06PhylumsRepository = new Repository<Tbl06Phylum, int>();   
           
        private readonly Repository<Tbl09Division, int> _tbl09DivisionsRepository = new Repository<Tbl09Division, int>();   
          
        private readonly Repository<Tbl90Reference, int> _tbl90ReferencesRepository = new Repository<Tbl90Reference, int>();
        private readonly Repository<Tbl93Comment, int> _tbl93CommentsRepository = new Repository<Tbl93Comment, int>();    

        #endregion "Private Data Members"               
    
        #region "Constructor"

        public Tbl03RegnumsViewModel()
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

           
        #region "Public Commands Basic Tbl03Regnum"

        private RelayCommand _getRegnumByNameOrIdCommand;     
    
        public ICommand GetRegnumByNameOrIdCommand    
    
        {
            get { return _getRegnumByNameOrIdCommand ?? (_getRegnumByNameOrIdCommand = new RelayCommand(delegate { GetRegnumByNameOrId(null); })); }   
        }

        private void GetRegnumByNameOrId(object o)       
        {   
       
            int id;
            if (int.TryParse(SearchRegnumName, out id))
                Tbl03RegnumsList = new ObservableCollection<Tbl03Regnum> { _tbl03RegnumsRepository.Get(id) };
            else
                Tbl03RegnumsList = _allListVm.GetValueTbl03RegnumsList(SearchRegnumName);      
RegnumsView = CollectionViewSource.GetDefaultView(Tbl03RegnumsList);
            RegnumsView.Refresh();
        }
        //------------------------------------------------------------------------------                
           
        private RelayCommand _addRegnumCommand;           
    
        public ICommand AddRegnumCommand       
    
        {
            get { return _addRegnumCommand ?? (_addRegnumCommand = new RelayCommand(delegate { AddRegnum(null); })); }
        }

        private void AddRegnum(object o)
        {
            Tbl03RegnumsList = new ObservableCollection<Tbl03Regnum>();   
Tbl03RegnumsList.Insert(0, new Tbl03Regnum{ RegnumName= CultRes.StringsRes.DatasetNew });      
RegnumsView = CollectionViewSource.GetDefaultView(Tbl03RegnumsList);
            RegnumsView.Refresh();
        }
        //---------------------------------------------------------------------------------------  
           
        private RelayCommand _copyRegnumCommand;              
                
        public ICommand CopyRegnumCommand              
         
        {
            get { return _copyRegnumCommand ?? (_copyRegnumCommand = new RelayCommand(delegate { CopyRegnum(null); })); }
        }

        private void CopyRegnum(object o)
        {
            Tbl03RegnumsList = new ObservableCollection<Tbl03Regnum>();

            var regnum = _tbl03RegnumsRepository.Get(CurrentTbl03Regnum.RegnumID);

            Tbl03RegnumsList.Insert(0, new Tbl03Regnum
            {                 
RegnumName = CultRes.StringsRes.DatasetNew,              
                            Subregnum = regnum.Subregnum,
                            Valid = regnum.Valid,
                            ValidYear = regnum.ValidYear,
                            Synonym = regnum.Synonym,
                            Author = regnum.Author,
                            AuthorYear = regnum.AuthorYear,
                            Info = regnum.Info,
                            EngName = regnum.EngName,
                            GerName = regnum.GerName,
                            FraName = regnum.FraName,
                            PorName = regnum.PorName,
                            Memo = regnum.Memo                            
        
            });

            RegnumsView = CollectionViewSource.GetDefaultView(Tbl03RegnumsList);
            RegnumsView.Refresh();
        }
        //---------------------------------------------------------------------------------------                  
           
        private RelayCommand _deleteRegnumCommand;              
                
        public ICommand DeleteRegnumCommand           
                
        {
            get { return _deleteRegnumCommand ?? (_deleteRegnumCommand = new RelayCommand(delegate { DeleteRegnum(null); })); }
        }

        private void DeleteRegnum(object o)
        {
            try
            {
                var regnum = _tbl03RegnumsRepository.Get(CurrentTbl03Regnum.RegnumID);
                if (regnum!= null)
                {
                    if (WpfMessageBox.Show(CultRes.StringsRes.DeleteQuestion1, CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl03Regnum.RegnumName + " " + CurrentTbl03Regnum.Subregnum,
                         MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes) 
                     return;

                    _tbl03RegnumsRepository.Delete(regnum);
                    _tbl03RegnumsRepository.Save();

                    WpfMessageBox.Show(CultRes.StringsRes.DeleteSuccess, CurrentTbl03Regnum.RegnumName + " " + CurrentTbl03Regnum.Subregnum, 
                        MessageBoxButton.OK, MessageBoxImage.Information);

                    if (SearchRegnumName == null)                   
                        GetConnectedTablesById(o); //refresh doubleClick                                       
                    else
                    {
                        Tbl03RegnumsList = _allListVm.GetValueTbl03RegnumsList(SearchRegnumName);   
                    }        
RegnumsView = CollectionViewSource.GetDefaultView(Tbl03RegnumsList);
                        RegnumsView.Refresh();
                }
                else
                {
                    WpfMessageBox.Show(CultRes.StringsRes.Information, CultRes.StringsRes.DeleteCan + " " + CurrentTbl03Regnum.RegnumName + " " + CurrentTbl03Regnum.Subregnum + " " + CultRes.StringsRes.DeleteCan1,
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
           
        private RelayCommand _saveRegnumCommand;              
                
        public ICommand SaveRegnumCommand             
         
        {
            get { return _saveRegnumCommand ?? (_saveRegnumCommand = new RelayCommand(delegate { SaveRegnum(null); })); }
        }

        private void SaveRegnum(object o)
        {
            try
            {
                var regnum = _tbl03RegnumsRepository.Get(CurrentTbl03Regnum.RegnumID);
                if (CurrentTbl03Regnum == null)              
                    WpfMessageBox.Show(CultRes.StringsRes.Warning, CultRes.StringsRes.DatasetNotExist, 
                        MessageBoxButton.OK, MessageBoxImage.Warning);               
                else
                {
                    if (CurrentTbl03Regnum.RegnumID!= 0)
                    {
                        if (regnum!= null) //update
                        {   
regnum.RegnumName = CurrentTbl03Regnum.RegnumName;
                            regnum.Subregnum = CurrentTbl03Regnum.Subregnum;                     
                
                            regnum.Valid = CurrentTbl03Regnum.Valid;
                            regnum.ValidYear = CurrentTbl03Regnum.ValidYear;
                            regnum.Synonym = CurrentTbl03Regnum.Synonym;
                            regnum.Author = CurrentTbl03Regnum.Author;
                            regnum.AuthorYear = CurrentTbl03Regnum.AuthorYear;
                            regnum.Info = CurrentTbl03Regnum.Info;
                            regnum.EngName = CurrentTbl03Regnum.EngName;
                            regnum.GerName = CurrentTbl03Regnum.GerName;
                            regnum.FraName = CurrentTbl03Regnum.FraName;
                            regnum.PorName = CurrentTbl03Regnum.PorName;
                            regnum.Updater = Environment.UserName;
                            regnum.UpdaterDate = DateTime.Now;
                            regnum.Memo = CurrentTbl03Regnum.Memo; 
         
                        }
                    }
                    else
                    {
                        _tbl03RegnumsRepository.Add(new Tbl03Regnum     //add new
                        {   
RegnumName = CurrentTbl03Regnum.RegnumName,     
                            Subregnum = CurrentTbl03Regnum.Subregnum,
                            CountID = RandomHelper.Randomnumber(),
                            Valid = CurrentTbl03Regnum.Valid,
                            ValidYear = CurrentTbl03Regnum.ValidYear,
                            Synonym = CurrentTbl03Regnum.Synonym,
                            Author = CurrentTbl03Regnum.Author,
                            AuthorYear = CurrentTbl03Regnum.AuthorYear,
                            Info = CurrentTbl03Regnum.Info,
                            EngName = CurrentTbl03Regnum.EngName,
                            GerName = CurrentTbl03Regnum.GerName,
                            FraName = CurrentTbl03Regnum.FraName,
                            PorName = CurrentTbl03Regnum.PorName,
                            Writer = Environment.UserName,
                            WriterDate = DateTime.Now,
                            Updater = Environment.UserName,
                            UpdaterDate = DateTime.Now,
                            Memo = CurrentTbl03Regnum.Memo   
                
                        });
                    }
                    {
                        //check about double Name
                        var dataset = new ObservableCollection<Tbl03Regnum>
                        (from a in _tbl03RegnumsRepository.GetAll()
                         where
                         a.RegnumName.Trim() == CurrentTbl03Regnum.RegnumName.Trim() &&                
                         a.Subregnum.Trim() == CurrentTbl03Regnum.Subregnum.Trim()                 
                         select a);

                        if (dataset.Count != 0 && CurrentTbl03Regnum.RegnumID== 0)  //dataset exist
                        {
                              WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTbl03Regnum.RegnumName + " " + CurrentTbl03Regnum.Subregnum,
                              MessageBoxButton.OK, MessageBoxImage.Information);
                        }

                        if (dataset.Count == 0 && CurrentTbl03Regnum.RegnumID == 0 ||
                            dataset.Count != 0 && CurrentTbl03Regnum.RegnumID != 0  ||
                            dataset.Count == 0 && CurrentTbl03Regnum.RegnumID != 0) //new dataset and update
                        {
                            if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTbl03Regnum.RegnumName + " " + CurrentTbl03Regnum.Subregnum,
                                    MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)                            
                                return;                            
                            {
                                _tbl03RegnumsRepository.Save();

                                WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess, CurrentTbl03Regnum.RegnumName + " " + CurrentTbl03Regnum.Subregnum,
                                    MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                        }  
         
                        if (SearchRegnumName == null && CurrentTbl03Regnum.RegnumID == 0)  //new Dataset                        
                            Tbl03RegnumsList = _allListVm.GetValueTbl03RegnumsList();  //last Dataset
                        if (SearchRegnumName == null && CurrentTbl03Regnum.RegnumID != 0)   //update 
                            Tbl03RegnumsList = _allListVm.GetValueTbl03RegnumsList(CurrentTbl03Regnum.RegnumID);
                        if (SearchRegnumName != null && CurrentTbl03Regnum.RegnumID == 0)  //new Dataset                        
                            Tbl03RegnumsList = _allListVm.GetValueTbl03RegnumsList();  //last Dataset
                        if (SearchRegnumName != null && CurrentTbl03Regnum.RegnumID != 0)   //update 
                            Tbl03RegnumsList = _allListVm.GetValueTbl03RegnumsList(CurrentTbl03Regnum.RegnumID);

                            RegnumsView = CollectionViewSource.GetDefaultView(Tbl03RegnumsList);
                            RegnumsView.Refresh();                          
         
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

             
        #region "Public Commands Connect ==> Tbl06Phylum"                 

        private RelayCommand _getPhylumByNameOrIdCommand;     
               
        public ICommand GetPhylumByNameOrIdCommand   
               
        {
            get { return _getPhylumByNameOrIdCommand ?? (_getPhylumByNameOrIdCommand = new RelayCommand(delegate { GetPhylumByNameOrId(null); })); }   
        }

        private void GetPhylumByNameOrId(object o)       
        {   
                
            int id;
            if (int.TryParse(SearchPhylumName, out id))
                Tbl06PhylumsList = new ObservableCollection<Tbl06Phylum> { _tbl06PhylumsRepository.Get(id) };
            else 
                Tbl06PhylumsList = _allListVm.GetValueTbl06PhylumsList(SearchPhylumName);       
  PhylumsView = CollectionViewSource.GetDefaultView(Tbl06PhylumsList);
            PhylumsView.Refresh();
        }
        //------------------------------------------------------------------------------     
                      
        private RelayCommand _addPhylumCommand;      
                       
        public ICommand AddPhylumCommand    
                        
        {
            get { return _addPhylumCommand ?? (_addPhylumCommand = new RelayCommand(delegate { AddPhylum(null); })); }
        }

        private void AddPhylum(object o)
        {
            Tbl06PhylumsList = new ObservableCollection<Tbl06Phylum>();   
  Tbl06PhylumsList.Insert(0, new Tbl06Phylum{ PhylumName= CultRes.StringsRes.DatasetNew });  

            if (Tbl03RegnumsAllList == null)
            Tbl03RegnumsAllList = _allListVm.GetValueTbl03RegnumsAllList();     
  PhylumsView = CollectionViewSource.GetDefaultView(Tbl06PhylumsList);
            PhylumsView.Refresh();
        }
        //---------------------------------------------------------------------------------------  
                              
        private RelayCommand _copyPhylumCommand;            
                              
        public ICommand CopyPhylumCommand          
                                 
        {
            get { return _copyPhylumCommand ?? (_copyPhylumCommand = new RelayCommand(delegate { CopyPhylum(null); })); }
        }

        private void CopyPhylum(object o)
        {
            Tbl06PhylumsList = new ObservableCollection<Tbl06Phylum>();

            var phylum = _tbl06PhylumsRepository.Get(CurrentTbl06Phylum.PhylumID);

            Tbl06PhylumsList.Insert(0, new Tbl06Phylum
            {                 
  RegnumID = phylum.RegnumID,
                PhylumName = CultRes.StringsRes.DatasetNew,     
                Valid = phylum.Valid,
                ValidYear = phylum.ValidYear,
                Synonym = phylum.Synonym,
                Author = phylum.Author,
                AuthorYear = phylum.AuthorYear,
                Info = phylum.Info,
                EngName = phylum.EngName,
                GerName = phylum.GerName,
                FraName = phylum.FraName,
                PorName = phylum.PorName,
                Memo = phylum.Memo         
                                     
            });

            PhylumsView = CollectionViewSource.GetDefaultView(Tbl06PhylumsList);
            PhylumsView.Refresh();
        }
        //---------------------------------------------------------------------------------------    
                                           
        private RelayCommand _deletePhylumCommand;              
                                           
        public ICommand DeletePhylumCommand             
                                                
        {
            get { return _deletePhylumCommand ?? (_deletePhylumCommand = new RelayCommand(delegate { DeletePhylum(null); })); }
        }

        private void DeletePhylum(object o)
        {
            try
            {
                var phylum = _tbl06PhylumsRepository.Get(CurrentTbl06Phylum.PhylumID);
                if (phylum!= null)
                {  
                                                    
                    if (WpfMessageBox.Show(CultRes.StringsRes.DeleteQuestion1, CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl06Phylum.PhylumName,
                         MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes) 
                    return;

                    _tbl06PhylumsRepository.Delete(phylum);
                    _tbl06PhylumsRepository.Save();

                    WpfMessageBox.Show(CultRes.StringsRes.DeleteSuccess, CurrentTbl06Phylum.PhylumName,
                       MessageBoxButton.OK, MessageBoxImage.Information);  
                                                        
                        if (SearchPhylumName == null)                       
                            GetConnectedTablesById(o); //refresh doubleClick                                          
                        else
                        {
                        Tbl06PhylumsList = _allListVm.GetValueTbl06PhylumsList(SearchPhylumName);  
  PhylumsView = CollectionViewSource.GetDefaultView(Tbl06PhylumsList);
                            PhylumsView.Refresh();
                    }
                }
                else
                {   
                                                          
                    WpfMessageBox.Show(CultRes.StringsRes.Information, CultRes.StringsRes.DeleteCan + " " + CurrentTbl06Phylum.PhylumName+ " " + CultRes.StringsRes.DeleteCan1,
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
                                                               
        private RelayCommand _savePhylumCommand;              
                                                                 
        public ICommand SavePhylumCommand             
                                                                     
        {
            get { return _savePhylumCommand ?? (_savePhylumCommand = new RelayCommand(delegate { SavePhylum(null); })); }
        }

        private void SavePhylum(object o)
        {
            try
            {
                var phylum = _tbl06PhylumsRepository.Get(CurrentTbl06Phylum.PhylumID);
                if (CurrentTbl06Phylum == null)               
                    WpfMessageBox.Show(CultRes.StringsRes.Warning, CultRes.StringsRes.DatasetNotExist, 
                        MessageBoxButton.OK, MessageBoxImage.Warning);              
                else
                {
                    if (CurrentTbl06Phylum.PhylumID!= 0)
                    {
                        if (phylum!= null) //update
                        {   
  phylum.RegnumID= CurrentTbl06Phylum.RegnumID;            
                            phylum.PhylumName= CurrentTbl06Phylum.PhylumName;
                            phylum.Valid = CurrentTbl06Phylum.Valid;
                            phylum.ValidYear = CurrentTbl06Phylum.ValidYear;
                            phylum.Synonym = CurrentTbl06Phylum.Synonym;
                            phylum.Author = CurrentTbl06Phylum.Author;
                            phylum.AuthorYear = CurrentTbl06Phylum.AuthorYear;
                            phylum.Info = CurrentTbl06Phylum.Info;
                            phylum.EngName = CurrentTbl06Phylum.EngName;
                            phylum.GerName = CurrentTbl06Phylum.GerName;
                            phylum.FraName = CurrentTbl06Phylum.FraName;
                            phylum.PorName = CurrentTbl06Phylum.PorName;
                            phylum.Updater = Environment.UserName;
                            phylum.UpdaterDate = DateTime.Now;
                            phylum.Memo = CurrentTbl06Phylum.Memo;                                                              
                                                                            
                        }
                    }
                    else
                    {
                        _tbl06PhylumsRepository.Add(new Tbl06Phylum    // add new
                        {   
  RegnumID= CurrentTbl06Phylum.RegnumID,              
                            PhylumName= CurrentTbl06Phylum.PhylumName,              
                            CountID = RandomHelper.Randomnumber(),
                            Valid = CurrentTbl06Phylum.Valid,
                            ValidYear = CurrentTbl06Phylum.ValidYear,
                            Synonym = CurrentTbl06Phylum.Synonym,
                            Author = CurrentTbl06Phylum.Author,
                            AuthorYear = CurrentTbl06Phylum.AuthorYear,
                            Info = CurrentTbl06Phylum.Info,
                            EngName = CurrentTbl06Phylum.EngName,
                            GerName = CurrentTbl06Phylum.GerName,
                            FraName = CurrentTbl06Phylum.FraName,
                            PorName = CurrentTbl06Phylum.PorName,
                            Writer = Environment.UserName,
                            WriterDate = DateTime.Now,
                            Updater = Environment.UserName,
                            UpdaterDate = DateTime.Now,
                            Memo = CurrentTbl06Phylum.Memo   
                                                                                    
                        });
                    }
                    {
                        //RegnumID may be not 0
                        if (CurrentTbl06Phylum.RegnumID == 0)
                        {
                            WpfMessageBox.Show(CultRes.StringsRes.RequiredGenealogyConnect, CultRes.StringsRes.RequiredInput,
                                MessageBoxButton.OK, MessageBoxImage.Information);
                            return;
                        }

                        //check if dataset with Name and vb-name already exist
                        var dataset = new ObservableCollection<Tbl06Phylum>
                        (from a in _tbl06PhylumsRepository.GetAll()
                         where
                         a.PhylumName.Trim() == CurrentTbl06Phylum.PhylumName.Trim() &&                
                         a.RegnumID == CurrentTbl06Phylum.RegnumID
                         select a);

                        if (dataset.Count != 0 && CurrentTbl06Phylum.PhylumID== 0)  //dataset exist
                        {
                              WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTbl06Phylum.PhylumName,
                              MessageBoxButton.OK, MessageBoxImage.Information);
                        }

                        if (dataset.Count == 0 && CurrentTbl06Phylum.PhylumID == 0 ||
                            dataset.Count != 0 && CurrentTbl06Phylum.PhylumID != 0  ||
                            dataset.Count == 0 && CurrentTbl06Phylum.PhylumID != 0) //new dataset and update
                        {
                            if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTbl06Phylum.PhylumName,
                                    MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)                            
                                return;                            
                            {
                                _tbl06PhylumsRepository.Save();

                                WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess, CurrentTbl06Phylum.PhylumName,
                                    MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                        }  
                
                        if (SearchPhylumName == null && CurrentTbl06Phylum.PhylumID == 0)  //new Dataset                        
                            Tbl06PhylumsList = _allListVm.GetValueTbl06PhylumsList();  //last Dataset
                        if (SearchPhylumName == null && CurrentTbl06Phylum.PhylumID != 0)   //update
                            Tbl06PhylumsList = _allListVm.GetValueTbl06PhylumsList(CurrentTbl06Phylum.PhylumID);
                        if (SearchPhylumName != null && CurrentTbl06Phylum.PhylumID == 0)  //new Dataset
                            Tbl06PhylumsList = _allListVm.GetValueTbl06PhylumsList();  //last Dataset
                        if (SearchPhylumName != null && CurrentTbl06Phylum.PhylumID != 0)   //update
                            Tbl06PhylumsList = _allListVm.GetValueTbl06PhylumsList(CurrentTbl06Phylum.PhylumID);
                                                                       

                        PhylumsView = CollectionViewSource.GetDefaultView(Tbl06PhylumsList);
                        PhylumsView.Refresh();             
                                                                                          
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
        

 //    Part 5    


             
        #region "Public Commands Connect ==> Tbl09Division"                 

        private RelayCommand _getDivisionByNameOrIdCommand;     
               
        public ICommand GetDivisionByNameOrIdCommand   
               
        {
            get { return _getDivisionByNameOrIdCommand ?? (_getDivisionByNameOrIdCommand = new RelayCommand(delegate { GetDivisionByNameOrId(null); })); }   
        }

        private void GetDivisionByNameOrId(object o)       
        {   
                
            int id;
            if (int.TryParse(SearchDivisionName, out id))
                Tbl09DivisionsList = new ObservableCollection<Tbl09Division> { _tbl09DivisionsRepository.Get(id) };
            else
                Tbl09DivisionsList = _allListVm.GetValueTbl09DivisionsList (SearchDivisionName);      
  DivisionsView = CollectionViewSource.GetDefaultView(Tbl09DivisionsList);
            DivisionsView.Refresh();
        }
        //------------------------------------------------------------------------------     
                      
        private RelayCommand _addDivisionCommand;      
                       
        public ICommand AddDivisionCommand    
                        
        {
            get { return _addDivisionCommand ?? (_addDivisionCommand = new RelayCommand(delegate { AddDivision(null); })); }
        }

        private void AddDivision(object o)
        {
            Tbl09DivisionsList = new ObservableCollection<Tbl09Division>();   
  Tbl09DivisionsList.Insert(0, new Tbl09Division{ DivisionName= CultRes.StringsRes.DatasetNew });   
            Tbl03RegnumsAllList = _allListVm.GetValueTbl03RegnumsAllList();    
  DivisionsView = CollectionViewSource.GetDefaultView(Tbl09DivisionsList);
            DivisionsView.Refresh();
        }
        //---------------------------------------------------------------------------------------  
                              
        private RelayCommand _copyDivisionCommand;            
                              
        public ICommand CopyDivisionCommand          
                                 
        {
            get { return _copyDivisionCommand ?? (_copyDivisionCommand = new RelayCommand(delegate { CopyDivision(null); })); }
        }

        private void CopyDivision(object o)
        {
            Tbl09DivisionsList = new ObservableCollection<Tbl09Division>();

            var division = _tbl09DivisionsRepository.Get(CurrentTbl09Division.DivisionID);

            Tbl09DivisionsList.Insert(0, new Tbl09Division
            {                 
  RegnumID = division.RegnumID,     
                DivisionName = CultRes.StringsRes.DatasetNew,     
                Valid = division.Valid,
                ValidYear = division.ValidYear,
                Synonym = division.Synonym,
                Author = division.Author,
                AuthorYear = division.AuthorYear,
                Info = division.Info,
                EngName = division.EngName,
                GerName = division.GerName,
                FraName = division.FraName,
                PorName = division.PorName,
                Memo = division.Memo           
                                     
            });

            DivisionsView = CollectionViewSource.GetDefaultView(Tbl09DivisionsList);
            DivisionsView.Refresh();
        }
        //---------------------------------------------------------------------------------------    
                                           
        private RelayCommand _deleteDivisionCommand;              
                                           
        public ICommand DeleteDivisionCommand             
                                                
        {
            get { return _deleteDivisionCommand ?? (_deleteDivisionCommand = new RelayCommand(delegate { DeleteDivision(null); })); }
        }

        private void DeleteDivision(object o)
        {
            try
            {
                var division = _tbl09DivisionsRepository.Get(CurrentTbl09Division.DivisionID);
                if (division!= null)
                {  
                                                    
                    if (WpfMessageBox.Show(CultRes.StringsRes.DeleteQuestion1, CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl09Division.DivisionName,
                         MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes) 
                    return;

                    _tbl09DivisionsRepository.Delete(division);
                    _tbl09DivisionsRepository.Save();

                    WpfMessageBox.Show(CultRes.StringsRes.DeleteSuccess, CurrentTbl09Division.DivisionName,
                       MessageBoxButton.OK, MessageBoxImage.Information);  
                                                        
                        if (SearchDivisionName == null)                       
                            GetConnectedTablesById(o); //refresh doubleClick                                          
                        else
                        {
                            Tbl09DivisionsList = _allListVm.GetValueTbl09DivisionsList(SearchDivisionName);  
  DivisionsView = CollectionViewSource.GetDefaultView(Tbl09DivisionsList);
                            DivisionsView.Refresh();
                    }
                }
                else
                {   
                                                          
                    WpfMessageBox.Show(CultRes.StringsRes.Information, CultRes.StringsRes.DeleteCan + " " + CurrentTbl09Division.DivisionName+ " " + CultRes.StringsRes.DeleteCan1,
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
                                                               
        private RelayCommand _saveDivisionCommand;              
                                                                 
        public ICommand SaveDivisionCommand             
                                                                     
        {
            get { return _saveDivisionCommand ?? (_saveDivisionCommand = new RelayCommand(delegate { SaveDivision(null); })); }
        }

        private void SaveDivision(object o)
        {
            try
            {
                var division = _tbl09DivisionsRepository.Get(CurrentTbl09Division.DivisionID);
                if (CurrentTbl09Division == null)                
                    WpfMessageBox.Show(CultRes.StringsRes.Warning, CultRes.StringsRes.DatasetNotExist, MessageBoxButton.OK, MessageBoxImage.Warning);              
                else
                {
                    if (CurrentTbl09Division.DivisionID!= 0)
                    {
                        if (division!= null) //update
                        {   
  division.RegnumID= CurrentTbl09Division.RegnumID;            
                            division.DivisionName= CurrentTbl09Division.DivisionName;
                            division.Valid = CurrentTbl09Division.Valid;
                            division.ValidYear = CurrentTbl09Division.ValidYear;
                            division.Synonym = CurrentTbl09Division.Synonym;
                            division.Author = CurrentTbl09Division.Author;
                            division.AuthorYear = CurrentTbl09Division.AuthorYear;
                            division.Info = CurrentTbl09Division.Info;
                            division.EngName = CurrentTbl09Division.EngName;
                            division.GerName = CurrentTbl09Division.GerName;
                            division.FraName = CurrentTbl09Division.FraName;
                            division.PorName = CurrentTbl09Division.PorName;
                            division.Updater = Environment.UserName;
                            division.UpdaterDate = DateTime.Now;
                            division.Memo = CurrentTbl09Division.Memo;                                                              
                                                                            
                        }
                    }
                    else
                    {
                        _tbl09DivisionsRepository.Add(new Tbl09Division     //add new
                        {   
  RegnumID = CurrentTbl09Division.RegnumID,              
                            DivisionName = CurrentTbl09Division.DivisionName,              
                            CountID = RandomHelper.Randomnumber(),
                            Valid = CurrentTbl09Division.Valid,
                            ValidYear = CurrentTbl09Division.ValidYear,
                            Synonym = CurrentTbl09Division.Synonym,
                            Author = CurrentTbl09Division.Author,
                            AuthorYear = CurrentTbl09Division.AuthorYear,
                            Info = CurrentTbl09Division.Info,
                            EngName = CurrentTbl09Division.EngName,
                            GerName = CurrentTbl09Division.GerName,
                            FraName = CurrentTbl09Division.FraName,
                            PorName = CurrentTbl09Division.PorName,
                            Writer = Environment.UserName,
                            WriterDate = DateTime.Now,
                            Updater = Environment.UserName,
                            UpdaterDate = DateTime.Now,
                            Memo = CurrentTbl09Division.Memo   
                                                                                    
                        });
                    }
                    {
                        //RegnumID may be not 0
                        if (CurrentTbl09Division.RegnumID == 0)
                        {
                            WpfMessageBox.Show(CultRes.StringsRes.RequiredGenealogyConnect, CultRes.StringsRes.RequiredInput,
                                MessageBoxButton.OK, MessageBoxImage.Information);
                            return;
                        }

                        //check if dataset with Name and vb-name already exist
                        var dataset = new ObservableCollection<Tbl09Division>
                        (from a in _tbl09DivisionsRepository.GetAll()
                         where
                         a.DivisionName.Trim() == CurrentTbl09Division.DivisionName.Trim() &&                
                         a.RegnumID == CurrentTbl09Division.RegnumID
                         select a);

                        if (dataset.Count != 0 && CurrentTbl09Division.DivisionID == 0)  //dataset exist
                        {
                              WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTbl09Division.DivisionName,
                              MessageBoxButton.OK, MessageBoxImage.Information);
                        }

                        if (dataset.Count == 0 && CurrentTbl09Division.DivisionID== 0 ||
                            dataset.Count != 0 && CurrentTbl09Division.DivisionID != 0  ||
                            dataset.Count == 0 && CurrentTbl09Division.DivisionID != 0) //new dataset and update
                        {
                            if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTbl09Division.DivisionName,
                                    MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)                            
                                return;                            
                            {
                                _tbl09DivisionsRepository.Save();

                                WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess, CurrentTbl09Division.DivisionName,
                                    MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                        }  
                 

                        if (SearchDivisionName == null && CurrentTbl09Division.DivisionID == 0)  //new Dataset                        
                            Tbl09DivisionsList = _allListVm.GetValueTbl09DivisionsList();  //last Dataset
                        if (SearchDivisionName == null && CurrentTbl09Division.DivisionID != 0)   //update
                            Tbl09DivisionsList = _allListVm.GetValueTbl09DivisionsList(CurrentTbl09Division.DivisionID);
                        if (SearchDivisionName != null && CurrentTbl09Division.DivisionID == 0)  //new Dataset
                            Tbl09DivisionsList = _allListVm.GetValueTbl09DivisionsList();  //last Dataset
                        if (SearchDivisionName != null && CurrentTbl09Division.DivisionID != 0)   //update
                            Tbl09DivisionsList = _allListVm.GetValueTbl09DivisionsList(CurrentTbl09Division.DivisionID);     
                                                                 
                        DivisionsView = CollectionViewSource.GetDefaultView(Tbl09DivisionsList);
                        DivisionsView.Refresh();           
                                                                                          
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
        

 //    Part 6    

      

 //    Part 7    

      

 //    Part 8    

           
        #region "Public Commands Connect ==> Tbl90RefAuthor"
        private RelayCommand _getRefAuthorByNameOrIdCommand;    
                   
        public ICommand GetRefAuthorByNameOrIdCommand  
                   
        {
            get { return _getRefAuthorByNameOrIdCommand ?? (_getRefAuthorByNameOrIdCommand = new RelayCommand(delegate { GetRefAuthorByNameOrId(null); })); }
        }

        public void GetRefAuthorByNameOrId(object o)
        {   
    
            int id;
            if (int.TryParse(SearchRefAuthorName, out id))
                Tbl90RefAuthorsList = new ObservableCollection<Tbl90Reference> { _tbl90ReferencesRepository.Get(id) };
            else
                Tbl90RefAuthorsList = _allListVm.GetValueTbl90RefAuthorsList(SearchRefAuthorName);     
     
            RefAuthorsView = CollectionViewSource.GetDefaultView(Tbl90RefAuthorsList);
            RefAuthorsView.Refresh();
        }
        //------------------------------------------------------------------------------                
           
        private RelayCommand _addRefAuthorCommand;         
                   
        public ICommand AddRefAuthorCommand      
                   
        {
            get { return _addRefAuthorCommand ?? (_addRefAuthorCommand = new RelayCommand(delegate { AddRefAuthor(null); })); }
        }

        public void AddRefAuthor(object o)
        {
            Tbl90RefAuthorsList = new ObservableCollection<Tbl90Reference>();  
    
            Tbl90RefAuthorsList.Insert(0, new Tbl90Reference { Info = CultRes.StringsRes.DatasetNew }); 

           Tbl03RegnumsAllList = _allListVm.GetValueTbl03RegnumsAllList();
           Tbl90AuthorsAllList = _allListVm.GetValueTbl90AuthorsAllList();    
    

            RefAuthorsView = CollectionViewSource.GetDefaultView(Tbl90RefAuthorsList);
            RefAuthorsView.Refresh();
        }
        //---------------------------------------------------------------------------------------  
           
        private RelayCommand _copyRefAuthorCommand;            
                
        public  ICommand CopyRefAuthorCommand          
                   
        {
            get { return _copyRefAuthorCommand ?? (_copyRefAuthorCommand = new RelayCommand(delegate { CopyRefAuthor(null); })); }
        }

        public void CopyRefAuthor(object o)
        {
            Tbl90RefAuthorsList = new ObservableCollection<Tbl90Reference>();

            var refAuthor = _tbl90ReferencesRepository.Get(CurrentTbl90RefAuthor.ReferenceID);

            Tbl90RefAuthorsList.Insert(0, new Tbl90Reference
            {                 
RegnumID = refAuthor.RegnumID,              
                Info = CultRes.StringsRes.DatasetNew,
                Valid = refAuthor.Valid,
                ValidYear = refAuthor.ValidYear,
                Memo = refAuthor.Memo          
        
            });

            RefAuthorsView = CollectionViewSource.GetDefaultView(Tbl90RefAuthorsList);
            RefAuthorsView.Refresh();
        }
        //---------------------------------------------------------------------------------------                  
           
        private RelayCommand _deleteRefAuthorCommand;             
                
        public ICommand DeleteRefAuthorCommand          
                
        {
            get { return _deleteRefAuthorCommand ?? (_deleteRefAuthorCommand = new RelayCommand(delegate { DeleteRefAuthor(null); })); }
        }

        public void DeleteRefAuthor(object o)
        {
            try
            {
                var refAuthor = _tbl90ReferencesRepository.Get(CurrentTbl90RefAuthor.ReferenceID);
                if (refAuthor != null)
                {
                    if (WpfMessageBox.Show(CultRes.StringsRes.DeleteQuestion1, CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl90RefAuthor.Info,
                         MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes) 
                    return;

                    _tbl90ReferencesRepository.Delete(refAuthor);
                    _tbl90ReferencesRepository.Save();

                    WpfMessageBox.Show(CultRes.StringsRes.DeleteSuccess, CurrentTbl90RefAuthor.Info, 
                        MessageBoxButton.OK, MessageBoxImage.Information);

                    if (SearchRefAuthorName == null)                    
                        GetConnectedTablesById(o); //refresh doubleClick                                       
                    else
                    {
                        Tbl90RefAuthorsList = _allListVm.GetValueTbl90RefAuthorsList(SearchRefAuthorName);  
    
                        RefAuthorsView = CollectionViewSource.GetDefaultView(Tbl90RefAuthorsList);
                        RefAuthorsView.Refresh();
                    }
                }
                else
                {
                    WpfMessageBox.Show(CultRes.StringsRes.Information, CultRes.StringsRes.DeleteCan + " " + CurrentTbl90RefAuthor.Info + " " + CultRes.StringsRes.DeleteCan1,
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
           
        private RelayCommand _saveRefAuthorCommand;            
                
        public ICommand SaveRefAuthorCommand           
         
        {
            get { return _saveRefAuthorCommand ?? (_saveRefAuthorCommand = new RelayCommand(delegate { SaveRefAuthor(null); })); }
        }

        public new void SaveRefAuthor(object o)
        {
            try
            {
                var refAuthor = _tbl90ReferencesRepository.Get(CurrentTbl90RefAuthor.ReferenceID);
                if (CurrentTbl90RefAuthor == null)               
                    WpfMessageBox.Show(CultRes.StringsRes.Warning, CultRes.StringsRes.DatasetNotExist, 
                         MessageBoxButton.OK, MessageBoxImage.Warning);               
                else
                {
                    if (CurrentTbl90RefAuthor.ReferenceID != 0 && CurrentTbl90RefAuthor.RefAuthorID != 0)
                    {
                        if (refAuthor != null)  //update
                        {   
         
                            refAuthor.RegnumID = CurrentTbl90RefAuthor.RegnumID;  
                            refAuthor.RefAuthorID = CurrentTbl90RefAuthor.RefAuthorID;
                            refAuthor.Valid = CurrentTbl90RefAuthor.Valid;
                            refAuthor.ValidYear = CurrentTbl90RefAuthor.ValidYear;
                            refAuthor.Info = CurrentTbl90RefAuthor.Info;
                            refAuthor.Updater = Environment.UserName;
                            refAuthor.UpdaterDate = DateTime.Now;
                            refAuthor.Memo = CurrentTbl90RefAuthor.Memo;  
         
                        }
                    }
                    else
                    {
                        _tbl90ReferencesRepository.Add(new Tbl90Reference   //add new
                        {   
RegnumID = CurrentTbl90RefAuthor.RegnumID,              
                            RefAuthorID = CurrentTbl90RefAuthor.RefAuthorID,
                            CountID = RandomHelper.Randomnumber(),
                            Valid = CurrentTbl90RefAuthor.Valid,
                            ValidYear = CurrentTbl90RefAuthor.ValidYear,
                            Info = CurrentTbl90RefAuthor.Info,
                            Writer = Environment.UserName,
                            WriterDate = DateTime.Now,
                            Updater = Environment.UserName,
                            UpdaterDate = DateTime.Now,
                            Memo = CurrentTbl90RefAuthor.Memo   
         
                        });
                    }
                    {
                        //RegnumID may be not 0
                        if (CurrentTbl90RefAuthor.RegnumID == 0 || CurrentTbl90RefAuthor.RefAuthorID == 0)
                        {
                            WpfMessageBox.Show(CultRes.StringsRes.RequiredGenealogyConnect, CultRes.StringsRes.RequiredInput,
                                MessageBoxButton.OK, MessageBoxImage.Information);
                            return;
                        }

                        //check if dataset with Name and vb-name already exist
                        var dataset = new ObservableCollection<Tbl90Reference>
                        (from a in _tbl90ReferencesRepository.GetAll()
                         where
                         a.Info.Trim() == CurrentTbl90RefAuthor.Info.Trim() &&                
                         a.RegnumID == CurrentTbl90RefAuthor.RegnumID &&
                         a.RefAuthorID == CurrentTbl90RefAuthor.RefAuthorID
                         select a);

                        if (dataset.Count != 0 && CurrentTbl90RefAuthor.ReferenceID == 0)  //dataset exist
                        {
                              WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTbl90RefAuthor.Info,
                              MessageBoxButton.OK, MessageBoxImage.Information);
                        }

                        if (dataset.Count == 0 && CurrentTbl90RefAuthor.ReferenceID == 0 ||
                            dataset.Count != 0 && CurrentTbl90RefAuthor.ReferenceID != 0  ||
                            dataset.Count == 0 && CurrentTbl90RefAuthor.ReferenceID != 0) //new dataset and update
                        {
                            if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTbl90RefAuthor.Info,
                                    MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)                            
                                return;                            
                            {
                              _tbl90ReferencesRepository.Save();
                         
                                WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess, CurrentTbl90RefAuthor.Info,
                                    MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                       }  
      
                        if (SearchRefAuthorName == null && CurrentTbl90RefAuthor.ReferenceID == 0)  //new Dataset                        
                            GetConnectedTablesById(o); //refresh doubleClick                                          
                        if (SearchRefAuthorName == null && CurrentTbl90RefAuthor.ReferenceID != 0)   //update
                            Tbl90RefAuthorsList = _allListVm.GetValueTbl90RefAuthorsList(CurrentTbl90RefAuthor.ReferenceID);
                        if (SearchRefAuthorName != null && CurrentTbl90RefAuthor.ReferenceID == 0)  //new Dataset
                            GetConnectedTablesById(o); //refresh doubleClick                                          
                        if (SearchRefAuthorName != null && CurrentTbl90RefAuthor.ReferenceID != 0)   //update
                            Tbl90RefAuthorsList = _allListVm.GetValueTbl90RefAuthorsList(CurrentTbl90RefAuthor.ReferenceID);

                        RefAuthorsView = CollectionViewSource.GetDefaultView(Tbl90RefAuthorsList);
                        RefAuthorsView.Refresh();     
        
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
           
        #region "Public Commands Connect ==> Tbl90RefSource"

        private RelayCommand _getRefSourceByNameOrIdCommand;   
                
        public ICommand GetRefSourceByNameOrIdCommand  
                
        {
            get { return _getRefSourceByNameOrIdCommand ?? (_getRefSourceByNameOrIdCommand = new RelayCommand(delegate { GetRefSourceByNameOrId(null); })); }
        }

        public void GetRefSourceByNameOrId(object o)
        {   
    
            int id;
            if (int.TryParse(SearchRefSourceName, out id))
                Tbl90RefSourcesList = new ObservableCollection<Tbl90Reference> { _tbl90ReferencesRepository.Get(id) };
            else
                Tbl90RefSourcesList = _allListVm.GetValueTbl90RefSourcesList(SearchRefSourceName);     
     
            RefSourcesView = CollectionViewSource.GetDefaultView(Tbl90RefSourcesList);
            RefSourcesView.Refresh();
        }
        //------------------------------------------------------------------------------                
           
        private RelayCommand _addRefSourceCommand;         
                
        public ICommand AddRefSourceCommand      
                
        {
            get { return _addRefSourceCommand ?? (_addRefSourceCommand = new RelayCommand(delegate { AddRefSource(null); })); }
        }

        public void AddRefSource(object o)
        {
            Tbl90RefSourcesList = new ObservableCollection<Tbl90Reference>();  
    
            Tbl90RefSourcesList.Insert(0, new Tbl90Reference { Info = CultRes.StringsRes.DatasetNew });    

            Tbl03RegnumsAllList = _allListVm.GetValueTbl03RegnumsAllList();
            Tbl90SourcesAllList = _allListVm.GetValueTbl90SourcesAllList();     
    

            RefSourcesView = CollectionViewSource.GetDefaultView(Tbl90RefSourcesList);
            RefSourcesView.Refresh();
        }
        //---------------------------------------------------------------------------------------  
           
        private RelayCommand _copyRefSourceCommand;            
                
        public  ICommand CopyRefSourceCommand          
                
        {
            get { return _copyRefSourceCommand ?? (_copyRefSourceCommand = new RelayCommand(delegate { CopyRefSource(null); })); }
        }

        public void CopyRefSource(object o)
        {
            Tbl90RefSourcesList = new ObservableCollection<Tbl90Reference>();

            var refSource = _tbl90ReferencesRepository.Get(CurrentTbl90RefSource.ReferenceID);

            Tbl90RefSourcesList.Insert(0, new Tbl90Reference
            {                 
RegnumID = refSource.RegnumID,              
                Info = CultRes.StringsRes.DatasetNew,
                Valid = refSource.Valid,
                ValidYear = refSource.ValidYear,
                Memo = refSource.Memo          
        
            });

            RefSourcesView = CollectionViewSource.GetDefaultView(Tbl90RefSourcesList);
            RefSourcesView.Refresh();
        }
        //---------------------------------------------------------------------------------------                  
           
        private RelayCommand _deleteRefSourceCommand;             
                
        public ICommand DeleteRefSourceCommand          
                
        {
            get { return _deleteRefSourceCommand ?? (_deleteRefSourceCommand = new RelayCommand(delegate { DeleteRefSource(null); })); }
        }

        public  void DeleteRefSource(object o)
        {
            try
            {
                var refSource = _tbl90ReferencesRepository.Get(CurrentTbl90RefSource.ReferenceID);
                if (refSource != null)
                {
                    if (WpfMessageBox.Show(CultRes.StringsRes.DeleteQuestion1, CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl90RefSource.Info,
                         MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes) 
                    return;

                    _tbl90ReferencesRepository.Delete(refSource);
                    _tbl90ReferencesRepository.Save();

                    WpfMessageBox.Show(CultRes.StringsRes.DeleteSuccess, CurrentTbl90RefSource.Info, 
                        MessageBoxButton.OK, MessageBoxImage.Information);

                    if (SearchRefSourceName == null)                   
                        GetConnectedTablesById(o); //refresh doubleClick                                       
                    else
                    {
                        Tbl90RefSourcesList = _allListVm.GetValueTbl90RefSourcesList(SearchRefSourceName);   
    
                        RefSourcesView = CollectionViewSource.GetDefaultView(Tbl90RefSourcesList);
                        RefSourcesView.Refresh();
                    }
                }
                else
                {
                    WpfMessageBox.Show(CultRes.StringsRes.Information, CultRes.StringsRes.DeleteCan + " " + CurrentTbl90RefSource.Info + " " + CultRes.StringsRes.DeleteCan1,
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
        
           
        private RelayCommand _saveRefSourceCommand;            
                
        public ICommand SaveRefSourceCommand           
                
        {
            get { return _saveRefSourceCommand ?? (_saveRefSourceCommand = new RelayCommand(delegate { SaveRefSource(null); })); }
        }

        public void SaveRefSource(object o)
        {
            try
            {
                var refSource = _tbl90ReferencesRepository.Get(CurrentTbl90RefSource.ReferenceID);
                if (CurrentTbl90RefSource == null)                
                    WpfMessageBox.Show(CultRes.StringsRes.Warning, CultRes.StringsRes.DatasetNotExist, MessageBoxButton.OK, MessageBoxImage.Warning);                
                else
                {
                    if (CurrentTbl90RefSource.ReferenceID != 0)
                    {
                        if (refSource != null)  //update
                        {   
         
                            refSource.RegnumID = CurrentTbl90RefSource.RegnumID;            
                            refSource.RefSourceID = CurrentTbl90RefSource.RefSourceID;
                            refSource.Valid = CurrentTbl90RefSource.Valid;
                            refSource.ValidYear = CurrentTbl90RefSource.ValidYear;
                            refSource.Info = CurrentTbl90RefSource.Info;
                            refSource.Updater = Environment.UserName;
                            refSource.UpdaterDate = DateTime.Now;
                            refSource.Memo = CurrentTbl90RefSource.Memo;  
         
                        }
                    }
                    else
                    {
                        _tbl90ReferencesRepository.Add(new Tbl90Reference    //add new
                        {   
RegnumID = CurrentTbl90RefSource.RegnumID,              
                            RefSourceID = CurrentTbl90RefSource.RefSourceID,
                            CountID = RandomHelper.Randomnumber(),
                            Valid = CurrentTbl90RefSource.Valid,
                            ValidYear = CurrentTbl90RefSource.ValidYear,
                            Info = CurrentTbl90RefSource.Info,
                            Writer = Environment.UserName,
                            WriterDate = DateTime.Now,
                            Updater = Environment.UserName,
                            UpdaterDate = DateTime.Now,
                            Memo = CurrentTbl90RefSource.Memo   
         
                        });
                    }
                    {
                        //RegnumID may be not 0
                        if (CurrentTbl90RefSource.RegnumID == 0 || CurrentTbl90RefSource.RefSourceID == 0)
                        {
                            WpfMessageBox.Show(CultRes.StringsRes.RequiredGenealogyConnect, CultRes.StringsRes.RequiredInput,            
                                MessageBoxButton.OK, MessageBoxImage.Information);
                            return;
                        }

                        //check if dataset with Name and vb-name already exist
                        var dataset = new ObservableCollection<Tbl90Reference>
                        (from a in _tbl90ReferencesRepository.GetAll()
                         where
                         a.Info.Trim() == CurrentTbl90RefSource.Info.Trim() &&                
                         a.RegnumID == CurrentTbl90RefSource.RegnumID &&
                         a.RefSourceID == CurrentTbl90RefSource.RefSourceID
                         select a);

                        if (dataset.Count != 0 && CurrentTbl90RefSource.ReferenceID == 0)  //dataset exist
                        {
                              WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTbl90RefSource.Info,
                              MessageBoxButton.OK, MessageBoxImage.Information);
                        }

                        if (dataset.Count == 0 && CurrentTbl90RefSource.ReferenceID == 0 ||
                            dataset.Count != 0 && CurrentTbl90RefSource.ReferenceID != 0  ||
                            dataset.Count == 0 && CurrentTbl90RefSource.ReferenceID != 0) //new dataset and update
                        {
                            if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTbl90RefSource.Info,
                                    MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)                            
                                return;                            
                            {
                                 _tbl90ReferencesRepository.Save();

                                WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess, CurrentTbl90RefSource.Info,
                                    MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                        }      
         
                        if (SearchRefSourceName == null && CurrentTbl90RefSource.ReferenceID == 0)  //new Dataset                        
                            Tbl90RefSourcesList = _allListVm.GetValueTbl90RefSourcesList();  //last Dataset
                        if (SearchRefSourceName == null && CurrentTbl90RefSource.ReferenceID != 0)   //update
                            Tbl90RefSourcesList = _allListVm.GetValueTbl90RefSourcesList(CurrentTbl90RefSource.ReferenceID);
                        if (SearchRefSourceName != null && CurrentTbl90RefSource.ReferenceID == 0)  //new Dataset
                            Tbl90RefSourcesList = _allListVm.GetValueTbl90RefSourcesList();  //last Dataset
                        if (SearchRefSourceName != null && CurrentTbl90RefSource.ReferenceID != 0)   //update
                            Tbl90RefSourcesList = _allListVm.GetValueTbl90RefSourcesList(CurrentTbl90RefSource.ReferenceID);

                        RefSourcesView = CollectionViewSource.GetDefaultView(Tbl90RefSourcesList);
                        RefSourcesView.Refresh();     
         
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
           
        #region "Public Commands Connect ==> Tbl90RefExpert"

        private RelayCommand _getRefExpertByNameOrIdCommand;   
                
        public ICommand GetRefExpertByNameOrIdCommand  
                
        {
            get { return _getRefExpertByNameOrIdCommand ?? (_getRefExpertByNameOrIdCommand = new RelayCommand(delegate { GetRefExpertByNameOrId(null); })); }
        }

        public void GetRefExpertByNameOrId(object o)
        {   
    
            int id;
            if (int.TryParse(SearchRefExpertName, out id))
                Tbl90RefExpertsList = new ObservableCollection<Tbl90Reference> { _tbl90ReferencesRepository.Get(id) };
            else
                Tbl90RefExpertsList = _allListVm.GetValueTbl90RefExpertsList(SearchRefExpertName);      
     
            RefExpertsView = CollectionViewSource.GetDefaultView(Tbl90RefExpertsList);
            RefExpertsView.Refresh();
        }
        //------------------------------------------------------------------------------                
           
        private RelayCommand _addRefExpertCommand;         
                
        public ICommand AddRefExpertCommand      
                
        {
            get { return _addRefExpertCommand ?? (_addRefExpertCommand = new RelayCommand(delegate { AddRefExpert(null); })); }
        }

        public void AddRefExpert(object o)
        {
            Tbl90RefExpertsList = new ObservableCollection<Tbl90Reference>();      
    
            Tbl90RefExpertsList.Insert(0, new Tbl90Reference { Info = CultRes.StringsRes.DatasetNew });   

            Tbl03RegnumsAllList = _allListVm.GetValueTbl03RegnumsAllList();
            Tbl90ExpertsAllList = _allListVm.GetValueTbl90ExpertsAllList();        
    

            RefExpertsView = CollectionViewSource.GetDefaultView(Tbl90RefExpertsList);
            RefExpertsView.Refresh();
        }
        //---------------------------------------------------------------------------------------  
           
        private RelayCommand _copyRefExpertCommand;            
                
        public ICommand CopyRefExpertCommand          
                
        {
            get { return _copyRefExpertCommand ?? (_copyRefExpertCommand = new RelayCommand(delegate { CopyRefExpert(null); })); }
        }

        public void CopyRefExpert(object o)
        {
            Tbl90RefExpertsList = new ObservableCollection<Tbl90Reference>();

            var refExpert = _tbl90ReferencesRepository.Get(CurrentTbl90RefExpert.ReferenceID);

            Tbl90RefExpertsList.Insert(0, new Tbl90Reference
            {                 
RegnumID = refExpert.RegnumID,              
                Info = CultRes.StringsRes.DatasetNew,
                Valid = refExpert.Valid,
                ValidYear = refExpert.ValidYear,
                Memo = refExpert.Memo          
        
            });

            RefExpertsView = CollectionViewSource.GetDefaultView(Tbl90RefExpertsList);
            RefExpertsView.Refresh();
        }
        //---------------------------------------------------------------------------------------                  
           
        private RelayCommand _deleteRefExpertCommand;             
                
        public ICommand DeleteRefExpertCommand          
                
        {
            get { return _deleteRefExpertCommand ?? (_deleteRefExpertCommand = new RelayCommand(delegate { DeleteRefExpert(null); })); }
        }

        public void DeleteRefExpert(object o)
        {
            try
            {
                var refExpert = _tbl90ReferencesRepository.Get(CurrentTbl90RefExpert.ReferenceID);
                if (refExpert != null)
                {
                    if (WpfMessageBox.Show(CultRes.StringsRes.DeleteQuestion1, CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl90RefExpert.Info,
                         MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes) 
                    return;

                    _tbl90ReferencesRepository.Delete(refExpert);
                    _tbl90ReferencesRepository.Save();

                    WpfMessageBox.Show(CultRes.StringsRes.DeleteSuccess, CurrentTbl90RefExpert.Info, 
                        MessageBoxButton.OK, MessageBoxImage.Information);

                    if (SearchRefExpertName == null)                   
                        GetConnectedTablesById(o); //refresh doubleClick                                       
                    else
                    {
                        Tbl90RefExpertsList = _allListVm.GetValueTbl90RefExpertsList(SearchRefExpertName);  
    
                        RefExpertsView = CollectionViewSource.GetDefaultView(Tbl90RefExpertsList);
                        RefExpertsView.Refresh();
                    }
                }
                else
                {
                    WpfMessageBox.Show(CultRes.StringsRes.Information, CultRes.StringsRes.DeleteCan + " " + CurrentTbl90RefExpert.Info + " " + CultRes.StringsRes.DeleteCan1,
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
          
           
        private RelayCommand _saveRefExpertCommand;            
                
        public ICommand SaveRefExpertCommand           
                
        {
            get { return _saveRefExpertCommand ?? (_saveRefExpertCommand = new RelayCommand(delegate { SaveRefExpert(null); })); }
        }

        public void SaveRefExpert(object o)
        {
            try
            {
                var refExpert = _tbl90ReferencesRepository.Get(CurrentTbl90RefExpert.ReferenceID);
                if (CurrentTbl90RefExpert == null)               
                    WpfMessageBox.Show(CultRes.StringsRes.Warning, CultRes.StringsRes.DatasetNotExist, MessageBoxButton.OK, MessageBoxImage.Warning);               
                else
                {
                    if (CurrentTbl90RefExpert.ReferenceID != 0 && CurrentTbl90RefExpert.RefExpertID != 0)
                    {
                        if (refExpert != null)	//update
                        {   
      
                            refExpert.RegnumID = CurrentTbl90RefExpert.RegnumID;           
                            refExpert.RefExpertID = CurrentTbl90RefExpert.RefExpertID;
                            refExpert.Valid = CurrentTbl90RefExpert.Valid;
                            refExpert.ValidYear = CurrentTbl90RefExpert.ValidYear;
                            refExpert.Info = CurrentTbl90RefExpert.Info;
                            refExpert.Updater = Environment.UserName;
                            refExpert.UpdaterDate = DateTime.Now;
                            refExpert.Memo = CurrentTbl90RefExpert.Memo;     
         
                        }
                    }
                    else
                    {
                        _tbl90ReferencesRepository.Add(new Tbl90Reference  //add new
                        {   
RegnumID = CurrentTbl90RefExpert.RegnumID,              
                            RefExpertID= CurrentTbl90RefExpert.RefExpertID,
                            CountID = RandomHelper.Randomnumber(),
                            Valid = CurrentTbl90RefExpert.Valid,
                            ValidYear = CurrentTbl90RefExpert.ValidYear,
                            Info = CurrentTbl90RefExpert.Info,
                            Writer = Environment.UserName,
                            WriterDate = DateTime.Now,
                            Updater = Environment.UserName,
                            UpdaterDate = DateTime.Now,
                            Memo = CurrentTbl90RefExpert.Memo   
         
                        });
                    }
                    {
                        //RegnumID may be not 0
                        if (CurrentTbl90RefExpert.RegnumID == 0 || CurrentTbl90RefExpert.RefExpertID == 0)   
                        {
                            WpfMessageBox.Show(CultRes.StringsRes.RequiredGenealogyConnect, CultRes.StringsRes.RequiredInput,          
                                MessageBoxButton.OK, MessageBoxImage.Information);
                            return;
                        }
                        //check if dataset with Name and vb-name already exist
                        var dataset = new ObservableCollection<Tbl90Reference>
                        (from a in _tbl90ReferencesRepository.GetAll()
                         where
                         a.Info.Trim() == CurrentTbl90RefExpert.Info.Trim() &&                
                         a.RegnumID == CurrentTbl90RefExpert.RegnumID &&
                         a.RefExpertID == CurrentTbl90RefExpert.RefExpertID
                         select a);

                        if (dataset.Count != 0 && CurrentTbl90RefExpert.ReferenceID == 0)  //dataset exist
                        {
                              WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTbl90RefExpert.Info,
                              MessageBoxButton.OK, MessageBoxImage.Information);
                        }

                        if (dataset.Count == 0 && CurrentTbl90RefExpert.ReferenceID == 0 ||
                            dataset.Count != 0 && CurrentTbl90RefExpert.ReferenceID != 0  ||
                            dataset.Count == 0 && CurrentTbl90RefExpert.ReferenceID != 0) //new dataset and update
                        {
                            if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTbl90RefExpert.Info,
                                    MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)                            
                                return;                            
                            {
                                    _tbl90ReferencesRepository.Save();

                                WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess, CurrentTbl90RefExpert.Info,
                                    MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                        }      
         
                        if (SearchRefExpertName == null && CurrentTbl90RefExpert.ReferenceID == 0)  //new Dataset                        
                            Tbl90RefExpertsList = _allListVm.GetValueTbl90RefExpertsList();  //last Dataset
                        if (SearchRefExpertName == null && CurrentTbl90RefExpert.ReferenceID != 0)   //update
                            Tbl90RefExpertsList = _allListVm.GetValueTbl90RefExpertsList(CurrentTbl90RefExpert.ReferenceID);
                        if (SearchRefExpertName != null && CurrentTbl90RefExpert.ReferenceID == 0)  //new Dataset
                            Tbl90RefExpertsList = _allListVm.GetValueTbl90RefExpertsList();  //last Dataset
                        if (SearchRefExpertName != null && CurrentTbl90RefExpert.ReferenceID != 0)   //update
                            Tbl90RefExpertsList = _allListVm.GetValueTbl90RefExpertsList(CurrentTbl90RefExpert.ReferenceID);

                        RefExpertsView = CollectionViewSource.GetDefaultView(Tbl90RefExpertsList);
                        RefExpertsView.Refresh();      
        
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
           
        #region "Public Commands Connect ==> Tbl93Comment"

        private RelayCommand _getCommentByNameOrIdCommand;   
                
        public ICommand GetCommentByNameOrIdCommand  
                
        {
            get { return _getCommentByNameOrIdCommand ?? (_getCommentByNameOrIdCommand = new RelayCommand(delegate { GetCommentByNameOrId(null); })); }
        }

        public void GetCommentByNameOrId(object o)
        {   
    
            int id;
            if (int.TryParse(SearchCommentInfo, out id))
                Tbl93CommentsList = new ObservableCollection<Tbl93Comment> { _tbl93CommentsRepository.Get(id) };
            else
                Tbl93CommentsList = _allListVm.GetValueTbl93CommentsList(SearchCommentInfo);    
     
            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            CommentsView.Refresh();
        }
        //------------------------------------------------------------------------------                
           
        private RelayCommand _addCommentCommand;         
                
        public ICommand AddCommentCommand      
                
        {
            get { return _addCommentCommand ?? (_addCommentCommand = new RelayCommand(delegate { AddComment(null); })); }
        }

        public void AddComment(object o)
        {
                Tbl93CommentsList = new ObservableCollection<Tbl93Comment>();  
    
            Tbl93CommentsList.Insert(0, new Tbl93Comment { Info = CultRes.StringsRes.DatasetNew });    

            Tbl03RegnumsAllList = _allListVm.GetValueTbl03RegnumsAllList();      
    

            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            CommentsView.Refresh();
        }
        //---------------------------------------------------------------------------------------  
           
        private RelayCommand _copyCommentCommand;            
                
        public ICommand CopyCommentCommand          
                
        {
            get { return _copyCommentCommand ?? (_copyCommentCommand = new RelayCommand(delegate { CopyComment(null); })); }
        }

        public void CopyComment(object o)
        {
            Tbl93CommentsList = new ObservableCollection<Tbl93Comment>();

            var comment = _tbl93CommentsRepository.Get(CurrentTbl93Comment.CommentID);

            Tbl93CommentsList.Insert(0, new Tbl93Comment
            {                 
RegnumID = comment.RegnumID,              
                Info = CultRes.StringsRes.DatasetNew,
                Valid = comment.Valid,
                ValidYear = comment.ValidYear,
                Memo = comment.Memo          
        
            });

            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            CommentsView.Refresh();
        }
        //---------------------------------------------------------------------------------------                  
           
        private RelayCommand _deleteCommentCommand;             
                
        public ICommand DeleteCommentCommand          
         
        {
            get { return _deleteCommentCommand ?? (_deleteCommentCommand = new RelayCommand(delegate { DeleteComment(null); })); }
        }

        private void DeleteComment(object o)
        {
            try
            {
                var comment = _tbl93CommentsRepository.Get(CurrentTbl93Comment.CommentID);
                if (comment != null)
                {
                    if (WpfMessageBox.Show(CultRes.StringsRes.DeleteQuestion1, CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl93Comment.CommentID,
                         MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes) 
                     return;

                    _tbl93CommentsRepository.Delete(comment);
                    _tbl93CommentsRepository.Save();

                    WpfMessageBox.Show(CultRes.StringsRes.DeleteSuccess, CurrentTbl93Comment.CommentID.ToString(), 
                        MessageBoxButton.OK, MessageBoxImage.Information);

                    if (SearchCommentInfo == null)                    
                        GetConnectedTablesById(o); //refresh doubleClick                                       
                    else
                    {
                        Tbl93CommentsList = _allListVm.GetValueTbl93CommentsList(SearchCommentInfo);    
    

                        CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
                        CommentsView.Refresh();
                    }
                }
                else
                {
                    WpfMessageBox.Show(CultRes.StringsRes.Information, CultRes.StringsRes.DeleteCan + " " + CurrentTbl93Comment.CommentID + " " + CultRes.StringsRes.DeleteCan1,
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
           
        private RelayCommand _saveCommentCommand;            
                
        public ICommand SaveCommentCommand           
         
        {
            get { return _saveCommentCommand ?? (_saveCommentCommand = new RelayCommand(delegate { SaveComment(null); })); }
        }

        private void SaveComment(object o)
        {
            try
            {
                var comment = _tbl93CommentsRepository.Get(CurrentTbl93Comment.CommentID);
                if (CurrentTbl93Comment == null)                
                    WpfMessageBox.Show(CultRes.StringsRes.Warning, CultRes.StringsRes.DatasetNotExist, MessageBoxButton.OK, MessageBoxImage.Warning);               
                else
                {
                    if (CurrentTbl93Comment.CommentID != 0)
                    {
                        if (comment != null)  //update
                        {   
      
                            comment.RegnumID = CurrentTbl93Comment.RegnumID;            
                            comment.Valid = CurrentTbl93Comment.Valid;
                            comment.ValidYear = CurrentTbl93Comment.ValidYear;
                            comment.Info = CurrentTbl93Comment.Info;
                            comment.Updater = Environment.UserName;
                            comment.UpdaterDate = DateTime.Now;
                            comment.Memo = CurrentTbl93Comment.Memo;     
         
                        }
                    }
                    else
                    {
                        _tbl93CommentsRepository.Add(new Tbl93Comment  //add new
                        {   
RegnumID = CurrentTbl93Comment.RegnumID,              
                            CountID = RandomHelper.Randomnumber(),
                            Valid = CurrentTbl93Comment.Valid,
                            ValidYear = CurrentTbl93Comment.ValidYear,
                            Info = CurrentTbl93Comment.Info,
                            Writer = Environment.UserName,
                            WriterDate = DateTime.Now,
                            Updater = Environment.UserName,
                            UpdaterDate = DateTime.Now,
                            Memo = CurrentTbl93Comment.Memo   
         
                        });
                    }
                    {
                        //RegnumID may be not 0
                        if (CurrentTbl93Comment.RegnumID == 0)
                        {
                            WpfMessageBox.Show(CultRes.StringsRes.RequiredGenealogyConnect, CultRes.StringsRes.RequiredInput,
                                MessageBoxButton.OK, MessageBoxImage.Information);
                            return;
                        }

                        //check if dataset with Name and vb-name already exist
                        var dataset = new ObservableCollection<Tbl93Comment>
                        (from a in _tbl93CommentsRepository.GetAll()
                         where
                         a.Info.Trim() == CurrentTbl93Comment.Info.Trim() &&                
                         a.RegnumID == CurrentTbl93Comment.RegnumID
                         select a);

                        if (dataset.Count != 0 && CurrentTbl93Comment.CommentID == 0)  //dataset exist
                        {
                              WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTbl93Comment.Info,
                              MessageBoxButton.OK, MessageBoxImage.Information);
                        }

                        if (dataset.Count == 0 && CurrentTbl93Comment.CommentID == 0 ||
                            dataset.Count != 0 && CurrentTbl93Comment.CommentID != 0  ||
                            dataset.Count == 0 && CurrentTbl93Comment.CommentID != 0) //new dataset and update
                        {
                            if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTbl93Comment.Info,
                                    MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)                            
                                return;                            
                            {
                        _tbl93CommentsRepository.Save();

                                WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess, CurrentTbl93Comment.Info,
                                    MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                        }   
         
                        if (SearchCommentInfo == null && CurrentTbl93Comment.CommentID == 0)  //new Dataset                        
                            Tbl93CommentsList = _allListVm.GetValueTbl93CommentsList();  //last Dataset
                        if (SearchCommentInfo == null && CurrentTbl93Comment.CommentID != 0)   //update
                            Tbl93CommentsList = _allListVm.GetValueTbl93CommentsList(CurrentTbl93Comment.CommentID);
                        if (SearchCommentInfo != null && CurrentTbl93Comment.CommentID == 0)  //new Dataset
                            Tbl93CommentsList = _allListVm.GetValueTbl93CommentsList();  //last Dataset
                        if (SearchCommentInfo != null && CurrentTbl93Comment.CommentID != 0)   //update
                            Tbl93CommentsList = _allListVm.GetValueTbl93CommentsList(CurrentTbl93Comment.CommentID);

                        CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
                        CommentsView.Refresh();      
       
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
      

 //    Part 9    


     
        #region "Public Commands Connected Tables by DoubleClick"

        private RelayCommand _getConnectedTablesCommand;
        public ICommand GetConnectedTablesCommand
        {
            get { return _getConnectedTablesCommand ?? (_getConnectedTablesCommand = new RelayCommand(delegate { GetConnectedTablesById(null); })); }
        }

        private void GetConnectedTablesById(object o)
        {
            SelectedDetailThreeRefTabIndex = 1;  //change to Connect tab

            Tbl06PhylumsList = new ObservableCollection<Tbl06Phylum>
                (from x in _tbl06PhylumsRepository.GetAll()
                where x.RegnumID == CurrentTbl03Regnum.RegnumID
                orderby x.PhylumName
                select x);

            PhylumsView = CollectionViewSource.GetDefaultView(Tbl06PhylumsList);
            PhylumsView.Refresh();
            //-----------------------------------------------------------------------------------
            Tbl09DivisionsList = new ObservableCollection<Tbl09Division>
                (from y in _tbl09DivisionsRepository.GetAll()
                 where y.RegnumID == CurrentTbl03Regnum.RegnumID
                 orderby y.DivisionName
                 select y);

            DivisionsView = CollectionViewSource.GetDefaultView(Tbl09DivisionsList);
            DivisionsView.Refresh();
            //-----------------------------------------------------------------------------------
            Tbl90RefAuthorsList = new ObservableCollection<Tbl90Reference>
                (from refAu in _tbl90ReferencesRepository.GetAll()
                 where refAu.RegnumID == CurrentTbl03Regnum.RegnumID
                && refAu.RefExpertID == null
                && refAu.RefSourceID == null
                orderby refAu.Tbl90RefAuthors.RefAuthorName, refAu.Tbl90RefAuthors.BookName, refAu.Tbl90RefAuthors.Page1
                select refAu);

            RefAuthorsView = CollectionViewSource.GetDefaultView(Tbl90RefAuthorsList);
            RefAuthorsView.Refresh();
            //--------------------------------------------------------------------------------------
            Tbl90RefSourcesList = new ObservableCollection<Tbl90Reference>
                (from refSo in _tbl90ReferencesRepository.GetAll()
                where refSo.RegnumID == CurrentTbl03Regnum.RegnumID
                && refSo.RefExpertID == null
                && refSo.RefAuthorID == null
                orderby refSo.Tbl90RefSources.RefSourceName
                select refSo);

            RefSourcesView = CollectionViewSource.GetDefaultView(Tbl90RefSourcesList);
            RefSourcesView.Refresh();
            //--------------------------------------------------------------------------------------
            Tbl90RefExpertsList = new ObservableCollection<Tbl90Reference>
                (from refEx in _tbl90ReferencesRepository.GetAll()
                where refEx.RegnumID == CurrentTbl03Regnum.RegnumID
                && refEx.RefAuthorID == null
                && refEx.RefSourceID == null
                orderby refEx.Tbl90RefExperts.RefExpertName
                select refEx);

            RefExpertsView = CollectionViewSource.GetDefaultView(Tbl90RefExpertsList);
            RefExpertsView.Refresh();
            //-----------------------------------------------------------------------------------
            Tbl93CommentsList = new ObservableCollection<Tbl93Comment>
                (from comm in _tbl93CommentsRepository.GetAll()
                where comm.RegnumID == CurrentTbl03Regnum.RegnumID
                orderby comm.Info
                select comm);

            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            CommentsView.Refresh();
            //------------------------------------------------------------------------------------

            Tbl03RegnumsAllList = _allListVm.GetValueTbl03RegnumsAllList();

            Tbl90AuthorsAllList = _allListVm.GetValueTbl90AuthorsAllList();

            Tbl90SourcesAllList = _allListVm.GetValueTbl90SourcesAllList();

            Tbl90ExpertsAllList = _allListVm.GetValueTbl90ExpertsAllList();
        }

        #endregion "Public Commands Connected Tables by DoubleClick"
   
 

 //    Part 10    

     
        #region "Public Commands to open Detail TabItems"

        private int _selectedMainTabIndex;
        public int SelectedMainTabIndex
        {
            get => _selectedMainTabIndex; 
            set
            {
                if (value == _selectedMainTabIndex) return;
                _selectedMainTabIndex = value; RaisePropertyChanged();
                if (_selectedMainTabIndex == 0)
                    SelectedDetailTabIndex = 0;
                if (_selectedMainTabIndex == 1)
                    SelectedDetailTabIndex = 1;
                if (_selectedMainTabIndex == 2)
                {
                    SelectedMainSubRefTabIndex = 0;
                    SelectedDetailThreeRefTabIndex = 1;
                    SelectedDetailTabIndex = 2;
                    SelectedDetailSubRefTabIndex = 0;
                }
                if (_selectedMainTabIndex == 3)
                    SelectedDetailTabIndex = 3;
                if (_selectedMainTabIndex == 4)
                    SelectedDetailTabIndex = 4;
                if (_selectedMainTabIndex == 5)
                    SelectedDetailTabIndex = 5;
                if (_selectedMainTabIndex == 6)
                    SelectedDetailTabIndex = 6;
                if (_selectedMainTabIndex == 7)
                    SelectedDetailTabIndex = 7;
            }
        }

        private int _selectedMainSubTabIndex;
        public int SelectedMainSubTabIndex
        {
            get => _selectedMainSubTabIndex; 
            set
            {
                if (value == _selectedMainSubTabIndex) return;
                _selectedMainSubTabIndex = value; RaisePropertyChanged();
                if (_selectedMainSubTabIndex == 0)
                    SelectedDetailSubTabIndex = 0;
                if (_selectedMainSubTabIndex == 1)
                    SelectedDetailSubTabIndex = 1;
                if (_selectedMainSubTabIndex == 2)
                    SelectedDetailSubTabIndex = 2;
            }
        }

        private int _selectedMainSubRefTabIndex;
        public int SelectedMainSubRefTabIndex
        {
            get => _selectedMainSubRefTabIndex; 
            set
            {
                if (value == _selectedMainSubRefTabIndex) return;
                _selectedMainSubRefTabIndex = value; RaisePropertyChanged();
                if (_selectedMainSubRefTabIndex == 0)
                    SelectedDetailSubRefTabIndex = 0;
                if (_selectedMainSubRefTabIndex == 1)
                    SelectedDetailSubRefTabIndex = 1;
                if (_selectedMainSubRefTabIndex == 2)
                    SelectedDetailSubRefTabIndex = 2;
            }
        }

        private int _selectedDetailTabIndex;
        public int SelectedDetailTabIndex
        {
            get => _selectedDetailTabIndex; 
            set
            {
                if (value == _selectedDetailTabIndex) return;
                _selectedDetailTabIndex = value; RaisePropertyChanged();
                if (_selectedDetailTabIndex == 0)
                    SelectedMainTabIndex = 0;
                if (_selectedDetailTabIndex == 1)
                    SelectedMainTabIndex = 1;
                if (_selectedDetailTabIndex == 2)
                    SelectedMainTabIndex = 2;
                if (_selectedDetailTabIndex == 3)
                    SelectedMainTabIndex = 3;
                if (_selectedDetailTabIndex == 4)
                    SelectedMainTabIndex = 4;
                if (_selectedDetailTabIndex == 5)
                    SelectedMainTabIndex = 5;
                if (_selectedDetailTabIndex == 6)
                    SelectedMainTabIndex = 6;
                if (_selectedDetailTabIndex == 7)
                    SelectedMainTabIndex = 7;
            }
        }

        private int _selectedDetailSubTabIndex;
        public int SelectedDetailSubTabIndex
        {
            get => _selectedDetailSubTabIndex; 
            set
            {
                if (value == _selectedDetailSubTabIndex) return;
                _selectedDetailSubTabIndex = value; RaisePropertyChanged();
                if (_selectedDetailSubTabIndex == 0)
                    SelectedMainSubTabIndex = 0;
                if (_selectedDetailSubTabIndex == 1)
                    SelectedMainSubTabIndex = 1;
                if (_selectedDetailSubTabIndex == 2)
                    SelectedMainSubTabIndex = 2;
                if (_selectedDetailSubTabIndex == 3)
                    SelectedMainSubTabIndex = 3;

            }
        }

        private int _selectedDetailSubRefTabIndex;
        public int SelectedDetailSubRefTabIndex
        {
            get => _selectedDetailSubRefTabIndex; 
            set
            {
                if (value == _selectedDetailSubRefTabIndex) return;
                _selectedDetailSubRefTabIndex = value; RaisePropertyChanged();
                if (_selectedDetailSubRefTabIndex == 0)
                    SelectedMainSubRefTabIndex = 0;
                if (_selectedDetailSubRefTabIndex == 1)
                    SelectedMainSubRefTabIndex = 1;
                if (_selectedDetailSubRefTabIndex == 2)
                    SelectedMainSubRefTabIndex = 2;
            }
        }

        private int _selectedDetailThreeRefTabIndex;
        public int SelectedDetailThreeRefTabIndex
        {
            get => _selectedDetailThreeRefTabIndex; 
            set
            {
                if (value == _selectedDetailThreeRefTabIndex) return;
                _selectedDetailThreeRefTabIndex = value; RaisePropertyChanged();
                if (_selectedDetailThreeRefTabIndex == 0)
                    SelectedMainSubRefTabIndex = 0;
                if (_selectedDetailThreeRefTabIndex == 1)
                    SelectedMainSubRefTabIndex = 1;
                if (_selectedDetailThreeRefTabIndex == 2)
                    SelectedMainSubRefTabIndex = 2;
            }
        }

        #endregion "Public Commands to open Detail TabItems"
 

 //    Part 11    


     
        #region "Public Properties Tbl03Regnum"

        private string _searchRegnumName;
        public string SearchRegnumName
        {
            get => _searchRegnumName; 
            set { _searchRegnumName = value; RaisePropertyChanged(); }
        }

        public ICollectionView RegnumsView;
        public Tbl03Regnum CurrentTbl03Regnum => RegnumsView?.CurrentItem as Tbl03Regnum;           

        private ObservableCollection<Tbl03Regnum> _tbl03RegnumsList;
        public ObservableCollection<Tbl03Regnum> Tbl03RegnumsList
        {
            get => _tbl03RegnumsList; 
            set { _tbl03RegnumsList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl03Regnum> _tbl03RegnumsAllList;
        public ObservableCollection<Tbl03Regnum> Tbl03RegnumsAllList
        {
            get => _tbl03RegnumsAllList; 
            set { _tbl03RegnumsAllList = value; RaisePropertyChanged(); }       
        }

        #endregion "Public Properties"
   

       
        #region "Public Properties Tbl06Phylum"

        private string _searchPhylumName;
        public string SearchPhylumName
        {
            get => _searchPhylumName; 
            set { _searchPhylumName = value; RaisePropertyChanged(); }
        }

        public ICollectionView PhylumsView;
        public Tbl06Phylum CurrentTbl06Phylum => PhylumsView?.CurrentItem as Tbl06Phylum;           

        private ObservableCollection<Tbl06Phylum> _tbl06PhylumsList;
        public ObservableCollection<Tbl06Phylum> Tbl06PhylumsList
        {
            get => _tbl06PhylumsList; 
            set { _tbl06PhylumsList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl06Phylum> _tbl06PhylumsAllList;
        public ObservableCollection<Tbl06Phylum> Tbl06PhylumsAllList
        {
            get => _tbl06PhylumsAllList; 
            set { _tbl06PhylumsAllList = value; RaisePropertyChanged(); }       
        }

        #endregion "Public Properties"
   
  
       
        #region "Public Properties Tbl09Division"

        private string _searchDivisionName;
        public string SearchDivisionName
        {
            get => _searchDivisionName; 
            set { _searchDivisionName = value; RaisePropertyChanged(); }
        }

        public ICollectionView DivisionsView;
        public Tbl09Division CurrentTbl09Division => DivisionsView?.CurrentItem as Tbl09Division;           

        private ObservableCollection<Tbl09Division> _tbl09DivisionsList;
        public ObservableCollection<Tbl09Division> Tbl09DivisionsList
        {
            get => _tbl09DivisionsList; 
            set { _tbl09DivisionsList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl09Division> _tbl09DivisionsAllList;
        public ObservableCollection<Tbl09Division> Tbl09DivisionsAllList
        {
            get => _tbl09DivisionsAllList; 
            set { _tbl09DivisionsAllList = value; RaisePropertyChanged(); }       
        }

        #endregion "Public Properties"
   
  
     
        #region "Public Properties Tbl90Author"

        private ObservableCollection<Tbl90RefAuthor> _tbl90AuthorsAllList;
        public ObservableCollection<Tbl90RefAuthor> Tbl90AuthorsAllList
        {
            get => _tbl90AuthorsAllList; 
            set { _tbl90AuthorsAllList = value; RaisePropertyChanged(); }
        }

        #endregion "Public Properties "

        #region "Public Properties Tbl90Source"

        private ObservableCollection<Tbl90RefSource> _tbl90SourcesAllList;
        public ObservableCollection<Tbl90RefSource> Tbl90SourcesAllList
        {
            get => _tbl90SourcesAllList; 
            set { _tbl90SourcesAllList = value; RaisePropertyChanged(); }
        }

        #endregion "Public Properties "

        #region "Public Properties Tbl90Expert"

        private ObservableCollection<Tbl90RefExpert> _tbl90ExpertsAllList;
        public ObservableCollection<Tbl90RefExpert> Tbl90ExpertsAllList
        {
            get => _tbl90ExpertsAllList; 
            set { _tbl90ExpertsAllList = value; RaisePropertyChanged(); }
        }

        #endregion "Public Properties "

        #region "Public Properties Tbl90RefAuthor"

        private string _searchRefAuthorName;
        public string SearchRefAuthorName
        {
            get => _searchRefAuthorName; 
            set { _searchRefAuthorName = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl90Reference> _tbl90RefAuthorsList;
        public ObservableCollection<Tbl90Reference> Tbl90RefAuthorsList
        {
            get => _tbl90RefAuthorsList; 
            set { _tbl90RefAuthorsList = value; RaisePropertyChanged(); }
        }

        public ICollectionView RefAuthorsView;
        public Tbl90Reference CurrentTbl90RefAuthor => RefAuthorsView?.CurrentItem as Tbl90Reference;

        #endregion "Public Properties"

        #region "Public Properties Tbl90RefSource"

        private string _searchRefSourceName;
        public string SearchRefSourceName
        {
            get => _searchRefSourceName; 
            set { _searchRefSourceName = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl90Reference> _tbl90RefSourcesList;
        public ObservableCollection<Tbl90Reference> Tbl90RefSourcesList
        {
            get => _tbl90RefSourcesList; 
            set { _tbl90RefSourcesList = value; RaisePropertyChanged(); }
        }

        public ICollectionView RefSourcesView;
        public Tbl90Reference CurrentTbl90RefSource => RefSourcesView?.CurrentItem as Tbl90Reference;

        #endregion "Public Properties"

        #region "Public Properties Tbl90RefExpert"

        private string _searchRefExpertName;
        public string SearchRefExpertName
        {
            get => _searchRefExpertName; 
            set { _searchRefExpertName = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl90Reference> _tbl90RefExpertsList;
        public ObservableCollection<Tbl90Reference> Tbl90RefExpertsList
        {
            get => _tbl90RefExpertsList; 
            set { _tbl90RefExpertsList = value; RaisePropertyChanged(); }
        }

        public ICollectionView RefExpertsView;
        public Tbl90Reference CurrentTbl90RefExpert => RefExpertsView?.CurrentItem as Tbl90Reference;

        #endregion "Public Properties"
   

     
        #region "Public Properties Tbl93Comment"

        private string _searchCommentInfo;
        public string SearchCommentInfo
        {
            get => _searchCommentInfo; 
            set { _searchCommentInfo = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl93Comment> _tbl93CommentsList;
        public ObservableCollection<Tbl93Comment> Tbl93CommentsList
        {
            get => _tbl93CommentsList; 
            set { _tbl93CommentsList = value; RaisePropertyChanged(); }
        }

        public ICollectionView CommentsView;
        public Tbl93Comment CurrentTbl93Comment => CommentsView?.CurrentItem as Tbl93Comment;

        #endregion "Public Properties"
   
 

 //    Part 12    

 

   }
}   
