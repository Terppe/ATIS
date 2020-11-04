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

    
         //    Tbl09DivisionsViewModel Skriptdatum:  04.11.2020  12:32    

namespace WPFUI.Views.Database
{     
    
    public class Tbl09DivisionsViewModel : Tbl03RegnumsViewModel
    {     
    
       #region "Private Data Members"  

        private readonly AllListVm _allListVm = new AllListVm();
           
        private readonly Repository<Tbl03Regnum, int> _tbl03RegnumsRepository = new Repository<Tbl03Regnum, int>();   
   
        private readonly Repository<Tbl09Division, int> _tbl09DivisionsRepository = new Repository<Tbl09Division, int>();   
           
        private readonly Repository<Tbl15Subdivision, int> _tbl15SubdivisionsRepository = new Repository<Tbl15Subdivision, int>();   
           
        private readonly Repository<Tbl18Superclass, int> _tbl18SuperclassesRepository = new Repository<Tbl18Superclass, int>();   
          
        private readonly Repository<Tbl90Reference, int> _tbl90ReferencesRepository = new Repository<Tbl90Reference, int>();
        private readonly Repository<Tbl93Comment, int> _tbl93CommentsRepository = new Repository<Tbl93Comment, int>();    

        #endregion "Private Data Members"               
    
        #region "Constructor"

        public Tbl09DivisionsViewModel()
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

           
        #region "Public Commands Basic Tbl09Division"

        private RelayCommand _getDivisionByNameOrIdCommand;     
                
        public new ICommand GetDivisionByNameOrIdCommand      
    
        {
            get { return _getDivisionByNameOrIdCommand ?? (_getDivisionByNameOrIdCommand = new RelayCommand(delegate { GetDivisionByNameOrId(null); })); }   
        }

        private void GetDivisionByNameOrId(object o)       
        {   
    
            int id;
            if (int.TryParse(SearchDivisionName, out id))
                Tbl09DivisionsList = new ObservableCollection<Tbl09Division> { _tbl09DivisionsRepository.Get(id) };
            else           
                Tbl09DivisionsList = _allListVm.GetValueTbl09DivisionsList(SearchDivisionName);      
Tbl03RegnumsAllList = _allListVm.GetValueTbl03RegnumsAllList();      
  DivisionsView = CollectionViewSource.GetDefaultView(Tbl09DivisionsList);
            DivisionsView.Refresh();
        }
        //------------------------------------------------------------------------------                
           
        private RelayCommand _addDivisionCommand;           
                
        public new ICommand AddDivisionCommand         
    
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
                
        public new ICommand CopyDivisionCommand             
         
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
                
        public new ICommand DeleteDivisionCommand             
         
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
                    }    
DivisionsView = CollectionViewSource.GetDefaultView(Tbl09DivisionsList);
                         DivisionsView.Refresh();
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
                
        public new ICommand SaveDivisionCommand             
         
        {
            get { return _saveDivisionCommand ?? (_saveDivisionCommand = new RelayCommand(delegate { SaveDivision(null); })); }
        }

        private void SaveDivision(object o)
        {
            try
            {
                var division = _tbl09DivisionsRepository.Get(CurrentTbl09Division.DivisionID);
                if (CurrentTbl09Division == null)              
                    WpfMessageBox.Show(CultRes.StringsRes.Warning, CultRes.StringsRes.DatasetNotExist, 
                        MessageBoxButton.OK, MessageBoxImage.Warning);               
                else
                {
                    if (CurrentTbl09Division.DivisionID!= 0)
                    {
                        if (division!= null) //update
                        {   
division.DivisionName = CurrentTbl09Division.DivisionName;  
division.RegnumID = CurrentTbl09Division.RegnumID;
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
RegnumID= CurrentTbl09Division.RegnumID,              
                            DivisionName= CurrentTbl09Division.DivisionName,              
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

                        if (dataset.Count != 0 && CurrentTbl09Division.DivisionID== 0)  //dataset exist
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
      

 //    Part 2    

           
        #region "Public Commands Connect <== Tbl03Regnum"                 

        private RelayCommand _getRegnumByNameOrIdCommand;     
                
        public new ICommand GetRegnumByNameOrIdCommand      
    
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
                
        public new ICommand AddRegnumCommand    
    
        {
            get { return _addRegnumCommand ?? (_addRegnumCommand = new RelayCommand(delegate { AddRegnum(null); })); }
        }

        private void AddRegnum(object o)
        {
            Tbl03RegnumsList = new ObservableCollection<Tbl03Regnum>();   
Tbl03RegnumsList.Insert(0, new Tbl03Regnum{ RegnumName = CultRes.StringsRes.DatasetNew });      
RegnumsView = CollectionViewSource.GetDefaultView(Tbl03RegnumsList);
            RegnumsView.Refresh();
        }
        //---------------------------------------------------------------------------------------  
           
        private RelayCommand _copyRegnumCommand;            
                
        public new ICommand CopyRegnumCommand          
         
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
                
        public new ICommand DeleteRegnumCommand             
         
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

                    WpfMessageBox.Show(CultRes.StringsRes.DeleteSuccess, CurrentTbl03Regnum.RegnumName+ " " + CurrentTbl03Regnum.Subregnum,
                       MessageBoxButton.OK, MessageBoxImage.Information);  
         
                        if (SearchRegnumName == null)                       
                            GetConnectedTablesById(o); //refresh doubleClick                                          
                        else
                        {
                        Tbl03RegnumsList = _allListVm.GetValueTbl03RegnumsList(SearchRegnumName);  
RegnumsView = CollectionViewSource.GetDefaultView(Tbl03RegnumsList);
                            RegnumsView.Refresh();
                    }
                }
                else
                {   
                
                    WpfMessageBox.Show(CultRes.StringsRes.Information, CultRes.StringsRes.DeleteCan + " " + CurrentTbl03Regnum.RegnumName+ " " + CurrentTbl03Regnum.Subregnum + " " + CultRes.StringsRes.DeleteCan1,
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
                
        public new ICommand SaveRegnumCommand             
         
        {
            get { return _saveRegnumCommand ?? (_saveRegnumCommand = new RelayCommand(delegate { SaveRegnum(null); })); }
        }

        private void SaveRegnum(object o)
        {
            try
            {
                var regnum = _tbl03RegnumsRepository.Get(CurrentTbl03Regnum.RegnumID);
                if (CurrentTbl03Regnum == null)              
                    WpfMessageBox.Show(CultRes.StringsRes.Warning, CultRes.StringsRes.DatasetNotExist, MessageBoxButton.OK, MessageBoxImage.Warning);               
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
RegnumName= CurrentTbl03Regnum.RegnumName,     
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

                        if (dataset.Count != 0 && CurrentTbl03Regnum.RegnumID == 0)  //dataset exist
                        {
                              WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, 
                              CurrentTbl03Regnum.RegnumName + " " + CurrentTbl03Regnum.Subregnum,
                              MessageBoxButton.OK, MessageBoxImage.Information);
                        }

                        if (dataset.Count == 0 && CurrentTbl03Regnum.RegnumID == 0 ||
                            dataset.Count != 0 && CurrentTbl03Regnum.RegnumID != 0  ||
                            dataset.Count == 0 && CurrentTbl03Regnum.RegnumID != 0) //new dataset and update
                        {
                            if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, 
                                    CurrentTbl03Regnum.RegnumName + " " + CurrentTbl03Regnum.Subregnum,
                                    MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)                            
                                return;                            
                            {
                                _tbl03RegnumsRepository.Save();

                                WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess, 
                                    CurrentTbl03Regnum.RegnumName + " " + CurrentTbl03Regnum.Subregnum,
                                    MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                        }  
        
                        if (SearchRegnumName == null && CurrentTbl03Regnum.RegnumID == 0)  //new Dataset                        
                            GetConnectedTablesById(o); //refresh doubleClick                                          
                        if (SearchRegnumName == null && CurrentTbl03Regnum.RegnumID != 0)  //update                     
                            Tbl03RegnumsList = _allListVm.GetValueTbl03RegnumsList(CurrentTbl03Regnum.RegnumID);
                        if (SearchRegnumName != null && CurrentTbl03Regnum.RegnumID == 0)  //new Dataset                        
                            GetConnectedTablesById(o); //refresh doubleClick                                          
                        if (SearchRegnumName != null && CurrentTbl03Regnum.RegnumID != 0)  //update                     
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
      

 //    Part 3    

      

 //    Part 4    

             
        #region "Public Commands Connect ==> Tbl15Subdivision"                 

        private RelayCommand _getSubdivisionByNameOrIdCommand;     
               
        public ICommand GetSubdivisionByNameOrIdCommand   
               
        {
            get { return _getSubdivisionByNameOrIdCommand ?? (_getSubdivisionByNameOrIdCommand = new RelayCommand(delegate { GetSubdivisionByNameOrId(null); })); }   
        }

        private void GetSubdivisionByNameOrId(object o)       
        {   
                
            int id;
            if (int.TryParse(SearchSubdivisionName, out id))
                Tbl15SubdivisionsList = new ObservableCollection<Tbl15Subdivision> { _tbl15SubdivisionsRepository.Get(id) };
            else 
                Tbl15SubdivisionsList = _allListVm.GetValueTbl15SubdivisionsList(SearchSubdivisionName);       
  SubdivisionsView = CollectionViewSource.GetDefaultView(Tbl15SubdivisionsList);
            SubdivisionsView.Refresh();
        }
        //------------------------------------------------------------------------------     
                      
        private RelayCommand _addSubdivisionCommand;      
                       
        public ICommand AddSubdivisionCommand    
                        
        {
            get { return _addSubdivisionCommand ?? (_addSubdivisionCommand = new RelayCommand(delegate { AddSubdivision(null); })); }
        }

        private void AddSubdivision(object o)
        {
            Tbl15SubdivisionsList = new ObservableCollection<Tbl15Subdivision>();   
  Tbl15SubdivisionsList.Insert(0, new Tbl15Subdivision{ SubdivisionName= CultRes.StringsRes.DatasetNew });  

            if (Tbl09DivisionsAllList == null)
            Tbl09DivisionsAllList = _allListVm.GetValueTbl09DivisionsAllList();     
  SubdivisionsView = CollectionViewSource.GetDefaultView(Tbl15SubdivisionsList);
            SubdivisionsView.Refresh();
        }
        //---------------------------------------------------------------------------------------  
                              
        private RelayCommand _copySubdivisionCommand;            
                              
        public ICommand CopySubdivisionCommand          
                                 
        {
            get { return _copySubdivisionCommand ?? (_copySubdivisionCommand = new RelayCommand(delegate { CopySubdivision(null); })); }
        }

        private void CopySubdivision(object o)
        {
            Tbl15SubdivisionsList = new ObservableCollection<Tbl15Subdivision>();

            var subdivision = _tbl15SubdivisionsRepository.Get(CurrentTbl15Subdivision.SubdivisionID);

            Tbl15SubdivisionsList.Insert(0, new Tbl15Subdivision
            {                 
  DivisionID = subdivision.DivisionID,
                SubdivisionName = CultRes.StringsRes.DatasetNew,     
                Valid = subdivision.Valid,
                ValidYear = subdivision.ValidYear,
                Synonym = subdivision.Synonym,
                Author = subdivision.Author,
                AuthorYear = subdivision.AuthorYear,
                Info = subdivision.Info,
                EngName = subdivision.EngName,
                GerName = subdivision.GerName,
                FraName = subdivision.FraName,
                PorName = subdivision.PorName,
                Memo = subdivision.Memo         
                                     
            });

            SubdivisionsView = CollectionViewSource.GetDefaultView(Tbl15SubdivisionsList);
            SubdivisionsView.Refresh();
        }
        //---------------------------------------------------------------------------------------    
                                           
        private RelayCommand _deleteSubdivisionCommand;              
                                           
        public ICommand DeleteSubdivisionCommand             
                                                
        {
            get { return _deleteSubdivisionCommand ?? (_deleteSubdivisionCommand = new RelayCommand(delegate { DeleteSubdivision(null); })); }
        }

        private void DeleteSubdivision(object o)
        {
            try
            {
                var subdivision = _tbl15SubdivisionsRepository.Get(CurrentTbl15Subdivision.SubdivisionID);
                if (subdivision!= null)
                {  
                                                    
                    if (WpfMessageBox.Show(CultRes.StringsRes.DeleteQuestion1, CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl15Subdivision.SubdivisionName,
                         MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes) 
                    return;

                    _tbl15SubdivisionsRepository.Delete(subdivision);
                    _tbl15SubdivisionsRepository.Save();

                    WpfMessageBox.Show(CultRes.StringsRes.DeleteSuccess, CurrentTbl15Subdivision.SubdivisionName,
                       MessageBoxButton.OK, MessageBoxImage.Information);  
                                                        
                        if (SearchSubdivisionName == null)                       
                            GetConnectedTablesById(o); //refresh doubleClick                                          
                        else
                        {
                        Tbl15SubdivisionsList = _allListVm.GetValueTbl15SubdivisionsList(SearchSubdivisionName);  
  SubdivisionsView = CollectionViewSource.GetDefaultView(Tbl15SubdivisionsList);
                            SubdivisionsView.Refresh();
                    }
                }
                else
                {   
                                                          
                    WpfMessageBox.Show(CultRes.StringsRes.Information, CultRes.StringsRes.DeleteCan + " " + CurrentTbl15Subdivision.SubdivisionName+ " " + CultRes.StringsRes.DeleteCan1,
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
                                                               
        private RelayCommand _saveSubdivisionCommand;              
                                                                 
        public ICommand SaveSubdivisionCommand             
                                                                     
        {
            get { return _saveSubdivisionCommand ?? (_saveSubdivisionCommand = new RelayCommand(delegate { SaveSubdivision(null); })); }
        }

        private void SaveSubdivision(object o)
        {
            try
            {
                var subdivision = _tbl15SubdivisionsRepository.Get(CurrentTbl15Subdivision.SubdivisionID);
                if (CurrentTbl15Subdivision == null)               
                    WpfMessageBox.Show(CultRes.StringsRes.Warning, CultRes.StringsRes.DatasetNotExist, 
                        MessageBoxButton.OK, MessageBoxImage.Warning);              
                else
                {
                    if (CurrentTbl15Subdivision.SubdivisionID!= 0)
                    {
                        if (subdivision!= null) //update
                        {   
  subdivision.DivisionID= CurrentTbl15Subdivision.DivisionID;            
                            subdivision.SubdivisionName= CurrentTbl15Subdivision.SubdivisionName;
                            subdivision.Valid = CurrentTbl15Subdivision.Valid;
                            subdivision.ValidYear = CurrentTbl15Subdivision.ValidYear;
                            subdivision.Synonym = CurrentTbl15Subdivision.Synonym;
                            subdivision.Author = CurrentTbl15Subdivision.Author;
                            subdivision.AuthorYear = CurrentTbl15Subdivision.AuthorYear;
                            subdivision.Info = CurrentTbl15Subdivision.Info;
                            subdivision.EngName = CurrentTbl15Subdivision.EngName;
                            subdivision.GerName = CurrentTbl15Subdivision.GerName;
                            subdivision.FraName = CurrentTbl15Subdivision.FraName;
                            subdivision.PorName = CurrentTbl15Subdivision.PorName;
                            subdivision.Updater = Environment.UserName;
                            subdivision.UpdaterDate = DateTime.Now;
                            subdivision.Memo = CurrentTbl15Subdivision.Memo;                                                              
                                                                            
                        }
                    }
                    else
                    {
                        _tbl15SubdivisionsRepository.Add(new Tbl15Subdivision    // add new
                        {   
  DivisionID= CurrentTbl15Subdivision.DivisionID,              
                            SubdivisionName= CurrentTbl15Subdivision.SubdivisionName,              
                            CountID = RandomHelper.Randomnumber(),
                            Valid = CurrentTbl15Subdivision.Valid,
                            ValidYear = CurrentTbl15Subdivision.ValidYear,
                            Synonym = CurrentTbl15Subdivision.Synonym,
                            Author = CurrentTbl15Subdivision.Author,
                            AuthorYear = CurrentTbl15Subdivision.AuthorYear,
                            Info = CurrentTbl15Subdivision.Info,
                            EngName = CurrentTbl15Subdivision.EngName,
                            GerName = CurrentTbl15Subdivision.GerName,
                            FraName = CurrentTbl15Subdivision.FraName,
                            PorName = CurrentTbl15Subdivision.PorName,
                            Writer = Environment.UserName,
                            WriterDate = DateTime.Now,
                            Updater = Environment.UserName,
                            UpdaterDate = DateTime.Now,
                            Memo = CurrentTbl15Subdivision.Memo   
                                                                                    
                        });
                    }
                    {
                        //DivisionID may be not 0
                        if (CurrentTbl15Subdivision.DivisionID == 0)
                        {
                            WpfMessageBox.Show(CultRes.StringsRes.RequiredGenealogyConnect, CultRes.StringsRes.RequiredInput,
                                MessageBoxButton.OK, MessageBoxImage.Information);
                            return;
                        }

                        //check if dataset with Name and vb-name already exist
                        var dataset = new ObservableCollection<Tbl15Subdivision>
                        (from a in _tbl15SubdivisionsRepository.GetAll()
                         where
                         a.SubdivisionName.Trim() == CurrentTbl15Subdivision.SubdivisionName.Trim() &&                
                         a.DivisionID == CurrentTbl15Subdivision.DivisionID
                         select a);

                        if (dataset.Count != 0 && CurrentTbl15Subdivision.SubdivisionID== 0)  //dataset exist
                        {
                              WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTbl15Subdivision.SubdivisionName,
                              MessageBoxButton.OK, MessageBoxImage.Information);
                        }

                        if (dataset.Count == 0 && CurrentTbl15Subdivision.SubdivisionID == 0 ||
                            dataset.Count != 0 && CurrentTbl15Subdivision.SubdivisionID != 0  ||
                            dataset.Count == 0 && CurrentTbl15Subdivision.SubdivisionID != 0) //new dataset and update
                        {
                            if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTbl15Subdivision.SubdivisionName,
                                    MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)                            
                                return;                            
                            {
                                _tbl15SubdivisionsRepository.Save();

                                WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess, CurrentTbl15Subdivision.SubdivisionName,
                                    MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                        }  
                
                        if (SearchSubdivisionName == null && CurrentTbl15Subdivision.SubdivisionID == 0)  //new Dataset                        
                            Tbl15SubdivisionsList = _allListVm.GetValueTbl15SubdivisionsList();  //last Dataset
                        if (SearchSubdivisionName == null && CurrentTbl15Subdivision.SubdivisionID != 0)   //update
                            Tbl15SubdivisionsList = _allListVm.GetValueTbl15SubdivisionsList(CurrentTbl15Subdivision.SubdivisionID);
                        if (SearchSubdivisionName != null && CurrentTbl15Subdivision.SubdivisionID == 0)  //new Dataset
                            Tbl15SubdivisionsList = _allListVm.GetValueTbl15SubdivisionsList();  //last Dataset
                        if (SearchSubdivisionName != null && CurrentTbl15Subdivision.SubdivisionID != 0)   //update
                            Tbl15SubdivisionsList = _allListVm.GetValueTbl15SubdivisionsList(CurrentTbl15Subdivision.SubdivisionID);
                                                                       

                        SubdivisionsView = CollectionViewSource.GetDefaultView(Tbl15SubdivisionsList);
                        SubdivisionsView.Refresh();             
                                                                                          
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


             
        #region "Public Commands Connect ==> Tbl18Superclass"                 

        private RelayCommand _getSuperclassByNameOrIdCommand;     
               
        public ICommand GetSuperclassByNameOrIdCommand   
               
        {
            get { return _getSuperclassByNameOrIdCommand ?? (_getSuperclassByNameOrIdCommand = new RelayCommand(delegate { GetSuperclassByNameOrId(null); })); }   
        }

        private void GetSuperclassByNameOrId(object o)       
        {   
                
            int id;
            if (int.TryParse(SearchSuperclassName, out id))
                Tbl18SuperclassesList = new ObservableCollection<Tbl18Superclass> { _tbl18SuperclassesRepository.Get(id) };
            else
                Tbl18SuperclassesList = _allListVm.GetValueTbl18SuperclassesList (SearchSuperclassName);      
  SuperclassesView = CollectionViewSource.GetDefaultView(Tbl18SuperclassesList);
            SuperclassesView.Refresh();
        }
        //------------------------------------------------------------------------------     
                      
        private RelayCommand _addSuperclassCommand;      
                       
        public ICommand AddSuperclassCommand    
                        
        {
            get { return _addSuperclassCommand ?? (_addSuperclassCommand = new RelayCommand(delegate { AddSuperclass(null); })); }
        }

        private void AddSuperclass(object o)
        {
            Tbl18SuperclassesList = new ObservableCollection<Tbl18Superclass>();   
  Tbl18SuperclassesList.Insert(0, new Tbl18Superclass{ SuperclassName= CultRes.StringsRes.DatasetNew });   
            Tbl09DivisionsAllList = _allListVm.GetValueTbl09DivisionsAllList();    
  SuperclassesView = CollectionViewSource.GetDefaultView(Tbl18SuperclassesList);
            SuperclassesView.Refresh();
        }
        //---------------------------------------------------------------------------------------  
                              
        private RelayCommand _copySuperclassCommand;            
                              
        public ICommand CopySuperclassCommand          
                                 
        {
            get { return _copySuperclassCommand ?? (_copySuperclassCommand = new RelayCommand(delegate { CopySuperclass(null); })); }
        }

        private void CopySuperclass(object o)
        {
            Tbl18SuperclassesList = new ObservableCollection<Tbl18Superclass>();

            var superclass = _tbl18SuperclassesRepository.Get(CurrentTbl18Superclass.SuperclassID);

            Tbl18SuperclassesList.Insert(0, new Tbl18Superclass
            {                 
  DivisionID = superclass.DivisionID,     
                SuperclassName = CultRes.StringsRes.DatasetNew,     
                Valid = superclass.Valid,
                ValidYear = superclass.ValidYear,
                Synonym = superclass.Synonym,
                Author = superclass.Author,
                AuthorYear = superclass.AuthorYear,
                Info = superclass.Info,
                EngName = superclass.EngName,
                GerName = superclass.GerName,
                FraName = superclass.FraName,
                PorName = superclass.PorName,
                Memo = superclass.Memo           
                                     
            });

            SuperclassesView = CollectionViewSource.GetDefaultView(Tbl18SuperclassesList);
            SuperclassesView.Refresh();
        }
        //---------------------------------------------------------------------------------------    
                                           
        private RelayCommand _deleteSuperclassCommand;              
                                           
        public ICommand DeleteSuperclassCommand             
                                                
        {
            get { return _deleteSuperclassCommand ?? (_deleteSuperclassCommand = new RelayCommand(delegate { DeleteSuperclass(null); })); }
        }

        private void DeleteSuperclass(object o)
        {
            try
            {
                var superclass = _tbl18SuperclassesRepository.Get(CurrentTbl18Superclass.SuperclassID);
                if (superclass!= null)
                {  
                                                    
                    if (WpfMessageBox.Show(CultRes.StringsRes.DeleteQuestion1, CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl18Superclass.SuperclassName,
                         MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes) 
                    return;

                    _tbl18SuperclassesRepository.Delete(superclass);
                    _tbl18SuperclassesRepository.Save();

                    WpfMessageBox.Show(CultRes.StringsRes.DeleteSuccess, CurrentTbl18Superclass.SuperclassName,
                       MessageBoxButton.OK, MessageBoxImage.Information);  
                                                        
                        if (SearchSuperclassName == null)                       
                            GetConnectedTablesById(o); //refresh doubleClick                                          
                        else
                        {
                            Tbl18SuperclassesList = _allListVm.GetValueTbl18SuperclassesList(SearchSuperclassName);  
  SuperclassesView = CollectionViewSource.GetDefaultView(Tbl18SuperclassesList);
                            SuperclassesView.Refresh();
                    }
                }
                else
                {   
                                                          
                    WpfMessageBox.Show(CultRes.StringsRes.Information, CultRes.StringsRes.DeleteCan + " " + CurrentTbl18Superclass.SuperclassName+ " " + CultRes.StringsRes.DeleteCan1,
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
                                                               
        private RelayCommand _saveSuperclassCommand;              
                                                                 
        public ICommand SaveSuperclassCommand             
                                                                     
        {
            get { return _saveSuperclassCommand ?? (_saveSuperclassCommand = new RelayCommand(delegate { SaveSuperclass(null); })); }
        }

        private void SaveSuperclass(object o)
        {
            try
            {
                var superclass = _tbl18SuperclassesRepository.Get(CurrentTbl18Superclass.SuperclassID);
                if (CurrentTbl18Superclass == null)                
                    WpfMessageBox.Show(CultRes.StringsRes.Warning, CultRes.StringsRes.DatasetNotExist, MessageBoxButton.OK, MessageBoxImage.Warning);              
                else
                {
                    if (CurrentTbl18Superclass.SuperclassID!= 0)
                    {
                        if (superclass!= null) //update
                        {   
  superclass.DivisionID= CurrentTbl18Superclass.DivisionID;            
                            superclass.SuperclassName= CurrentTbl18Superclass.SuperclassName;
                            superclass.Valid = CurrentTbl18Superclass.Valid;
                            superclass.ValidYear = CurrentTbl18Superclass.ValidYear;
                            superclass.Synonym = CurrentTbl18Superclass.Synonym;
                            superclass.Author = CurrentTbl18Superclass.Author;
                            superclass.AuthorYear = CurrentTbl18Superclass.AuthorYear;
                            superclass.Info = CurrentTbl18Superclass.Info;
                            superclass.EngName = CurrentTbl18Superclass.EngName;
                            superclass.GerName = CurrentTbl18Superclass.GerName;
                            superclass.FraName = CurrentTbl18Superclass.FraName;
                            superclass.PorName = CurrentTbl18Superclass.PorName;
                            superclass.Updater = Environment.UserName;
                            superclass.UpdaterDate = DateTime.Now;
                            superclass.Memo = CurrentTbl18Superclass.Memo;                                                              
                                                                            
                        }
                    }
                    else
                    {
                        _tbl18SuperclassesRepository.Add(new Tbl18Superclass     //add new
                        {   
  DivisionID = CurrentTbl18Superclass.DivisionID,              
                            SuperclassName = CurrentTbl18Superclass.SuperclassName,              
                            CountID = RandomHelper.Randomnumber(),
                            Valid = CurrentTbl18Superclass.Valid,
                            ValidYear = CurrentTbl18Superclass.ValidYear,
                            Synonym = CurrentTbl18Superclass.Synonym,
                            Author = CurrentTbl18Superclass.Author,
                            AuthorYear = CurrentTbl18Superclass.AuthorYear,
                            Info = CurrentTbl18Superclass.Info,
                            EngName = CurrentTbl18Superclass.EngName,
                            GerName = CurrentTbl18Superclass.GerName,
                            FraName = CurrentTbl18Superclass.FraName,
                            PorName = CurrentTbl18Superclass.PorName,
                            Writer = Environment.UserName,
                            WriterDate = DateTime.Now,
                            Updater = Environment.UserName,
                            UpdaterDate = DateTime.Now,
                            Memo = CurrentTbl18Superclass.Memo   
                                                                                    
                        });
                    }
                    {
                        //DivisionID may be not 0
                        if (CurrentTbl18Superclass.DivisionID == 0)
                        {
                            WpfMessageBox.Show(CultRes.StringsRes.RequiredGenealogyConnect, CultRes.StringsRes.RequiredInput,
                                MessageBoxButton.OK, MessageBoxImage.Information);
                            return;
                        }

                        //check if dataset with Name and vb-name already exist
                        var dataset = new ObservableCollection<Tbl18Superclass>
                        (from a in _tbl18SuperclassesRepository.GetAll()
                         where
                         a.SuperclassName.Trim() == CurrentTbl18Superclass.SuperclassName.Trim() &&                
                         a.DivisionID == CurrentTbl18Superclass.DivisionID
                         select a);

                        if (dataset.Count != 0 && CurrentTbl18Superclass.SuperclassID == 0)  //dataset exist
                        {
                              WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTbl18Superclass.SuperclassName,
                              MessageBoxButton.OK, MessageBoxImage.Information);
                        }

                        if (dataset.Count == 0 && CurrentTbl18Superclass.SuperclassID== 0 ||
                            dataset.Count != 0 && CurrentTbl18Superclass.SuperclassID != 0  ||
                            dataset.Count == 0 && CurrentTbl18Superclass.SuperclassID != 0) //new dataset and update
                        {
                            if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTbl18Superclass.SuperclassName,
                                    MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)                            
                                return;                            
                            {
                                _tbl18SuperclassesRepository.Save();

                                WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess, CurrentTbl18Superclass.SuperclassName,
                                    MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                        }  
                 

                        if (SearchSuperclassName == null && CurrentTbl18Superclass.SuperclassID == 0)  //new Dataset                        
                            Tbl18SuperclassesList = _allListVm.GetValueTbl18SuperclassesList();  //last Dataset
                        if (SearchSuperclassName == null && CurrentTbl18Superclass.SuperclassID != 0)   //update
                            Tbl18SuperclassesList = _allListVm.GetValueTbl18SuperclassesList(CurrentTbl18Superclass.SuperclassID);
                        if (SearchSuperclassName != null && CurrentTbl18Superclass.SuperclassID == 0)  //new Dataset
                            Tbl18SuperclassesList = _allListVm.GetValueTbl18SuperclassesList();  //last Dataset
                        if (SearchSuperclassName != null && CurrentTbl18Superclass.SuperclassID != 0)   //update
                            Tbl18SuperclassesList = _allListVm.GetValueTbl18SuperclassesList(CurrentTbl18Superclass.SuperclassID);     
                                                                 
                        SuperclassesView = CollectionViewSource.GetDefaultView(Tbl18SuperclassesList);
                        SuperclassesView.Refresh();           
                                                                                          
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
    
        public new ICommand GetRefAuthorByNameOrIdCommand  
    
        {
            get { return _getRefAuthorByNameOrIdCommand ?? (_getRefAuthorByNameOrIdCommand = new RelayCommand(delegate { GetRefAuthorByNameOrId(null); })); }
        }

        public new void GetRefAuthorByNameOrId(object o)
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
    
        public new ICommand AddRefAuthorCommand      
    
        {
            get { return _addRefAuthorCommand ?? (_addRefAuthorCommand = new RelayCommand(delegate { AddRefAuthor(null); })); }
        }

        public new void AddRefAuthor(object o)
        {
            Tbl90RefAuthorsList = new ObservableCollection<Tbl90Reference>();  
    
            Tbl90RefAuthorsList.Insert(0, new Tbl90Reference { Info = CultRes.StringsRes.DatasetNew }); 

           Tbl09DivisionsAllList = _allListVm.GetValueTbl09DivisionsAllList();
           Tbl90AuthorsAllList = _allListVm.GetValueTbl90AuthorsAllList();    
    

            RefAuthorsView = CollectionViewSource.GetDefaultView(Tbl90RefAuthorsList);
            RefAuthorsView.Refresh();
        }
        //---------------------------------------------------------------------------------------  
           
        private RelayCommand _copyRefAuthorCommand;            
    
        public new ICommand CopyRefAuthorCommand       
         
        {
            get { return _copyRefAuthorCommand ?? (_copyRefAuthorCommand = new RelayCommand(delegate { CopyRefAuthor(null); })); }
        }

        public new void CopyRefAuthor(object o)
        {
            Tbl90RefAuthorsList = new ObservableCollection<Tbl90Reference>();

            var refAuthor = _tbl90ReferencesRepository.Get(CurrentTbl90RefAuthor.ReferenceID);

            Tbl90RefAuthorsList.Insert(0, new Tbl90Reference
            {                 
DivisionID = refAuthor.DivisionID,              
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
    
        public new ICommand DeleteRefAuthorCommand          
         
        {
            get { return _deleteRefAuthorCommand ?? (_deleteRefAuthorCommand = new RelayCommand(delegate { DeleteRefAuthor(null); })); }
        }

        public new void DeleteRefAuthor(object o)
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
    
        public new ICommand SaveRefAuthorCommand           
         
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
         
                            refAuthor.DivisionID = CurrentTbl90RefAuthor.DivisionID;  
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
DivisionID = CurrentTbl90RefAuthor.DivisionID,              
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
                        //DivisionID may be not 0
                        if (CurrentTbl90RefAuthor.DivisionID == 0 || CurrentTbl90RefAuthor.RefAuthorID == 0)
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
                         a.DivisionID == CurrentTbl90RefAuthor.DivisionID &&
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
    
        public new ICommand GetRefSourceByNameOrIdCommand  
    
        {
            get { return _getRefSourceByNameOrIdCommand ?? (_getRefSourceByNameOrIdCommand = new RelayCommand(delegate { GetRefSourceByNameOrId(null); })); }
        }

        public new void GetRefSourceByNameOrId(object o)
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
    
        public new ICommand AddRefSourceCommand      
    
        {
            get { return _addRefSourceCommand ?? (_addRefSourceCommand = new RelayCommand(delegate { AddRefSource(null); })); }
        }

        public new void AddRefSource(object o)
        {
            Tbl90RefSourcesList = new ObservableCollection<Tbl90Reference>();  
    
            Tbl90RefSourcesList.Insert(0, new Tbl90Reference { Info = CultRes.StringsRes.DatasetNew });    

            Tbl09DivisionsAllList = _allListVm.GetValueTbl09DivisionsAllList();
            Tbl90SourcesAllList = _allListVm.GetValueTbl90SourcesAllList();     
    

            RefSourcesView = CollectionViewSource.GetDefaultView(Tbl90RefSourcesList);
            RefSourcesView.Refresh();
        }
        //---------------------------------------------------------------------------------------  
           
        private RelayCommand _copyRefSourceCommand;            
    
        public new ICommand CopyRefSourceCommand       
         
        {
            get { return _copyRefSourceCommand ?? (_copyRefSourceCommand = new RelayCommand(delegate { CopyRefSource(null); })); }
        }

        public new void CopyRefSource(object o)
        {
            Tbl90RefSourcesList = new ObservableCollection<Tbl90Reference>();

            var refSource = _tbl90ReferencesRepository.Get(CurrentTbl90RefSource.ReferenceID);

            Tbl90RefSourcesList.Insert(0, new Tbl90Reference
            {                 
DivisionID = refSource.DivisionID,              
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
    
        public new ICommand DeleteRefSourceCommand          
         
        {
            get { return _deleteRefSourceCommand ?? (_deleteRefSourceCommand = new RelayCommand(delegate { DeleteRefSource(null); })); }
        }

        public new void DeleteRefSource(object o)
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
    
        public new ICommand SaveRefSourceCommand           
         
        {
            get { return _saveRefSourceCommand ?? (_saveRefSourceCommand = new RelayCommand(delegate { SaveRefSource(null); })); }
        }

        public new void SaveRefSource(object o)
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
         
                            refSource.DivisionID = CurrentTbl90RefSource.DivisionID;            
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
DivisionID = CurrentTbl90RefSource.DivisionID,              
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
                        //DivisionID may be not 0
                        if (CurrentTbl90RefSource.DivisionID == 0 || CurrentTbl90RefSource.RefSourceID == 0)
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
                         a.DivisionID == CurrentTbl90RefSource.DivisionID &&
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
    
        public new ICommand GetRefExpertByNameOrIdCommand  
    
        {
            get { return _getRefExpertByNameOrIdCommand ?? (_getRefExpertByNameOrIdCommand = new RelayCommand(delegate { GetRefExpertByNameOrId(null); })); }
        }

        public new void GetRefExpertByNameOrId(object o)
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
    
        public new ICommand AddRefExpertCommand      
    
        {
            get { return _addRefExpertCommand ?? (_addRefExpertCommand = new RelayCommand(delegate { AddRefExpert(null); })); }
        }

        public new void AddRefExpert(object o)
        {
            Tbl90RefExpertsList = new ObservableCollection<Tbl90Reference>();      
    
            Tbl90RefExpertsList.Insert(0, new Tbl90Reference { Info = CultRes.StringsRes.DatasetNew });   

            Tbl09DivisionsAllList = _allListVm.GetValueTbl09DivisionsAllList();
            Tbl90ExpertsAllList = _allListVm.GetValueTbl90ExpertsAllList();        
    

            RefExpertsView = CollectionViewSource.GetDefaultView(Tbl90RefExpertsList);
            RefExpertsView.Refresh();
        }
        //---------------------------------------------------------------------------------------  
           
        private RelayCommand _copyRefExpertCommand;            
    
        public new ICommand CopyRefExpertCommand       
         
        {
            get { return _copyRefExpertCommand ?? (_copyRefExpertCommand = new RelayCommand(delegate { CopyRefExpert(null); })); }
        }

        public new void CopyRefExpert(object o)
        {
            Tbl90RefExpertsList = new ObservableCollection<Tbl90Reference>();

            var refExpert = _tbl90ReferencesRepository.Get(CurrentTbl90RefExpert.ReferenceID);

            Tbl90RefExpertsList.Insert(0, new Tbl90Reference
            {                 
DivisionID = refExpert.DivisionID,              
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
    
        public new ICommand DeleteRefExpertCommand          
         
        {
            get { return _deleteRefExpertCommand ?? (_deleteRefExpertCommand = new RelayCommand(delegate { DeleteRefExpert(null); })); }
        }

        public new void DeleteRefExpert(object o)
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
    
        public new ICommand SaveRefExpertCommand           
         
        {
            get { return _saveRefExpertCommand ?? (_saveRefExpertCommand = new RelayCommand(delegate { SaveRefExpert(null); })); }
        }

        public new void SaveRefExpert(object o)
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
      
                            refExpert.DivisionID = CurrentTbl90RefExpert.DivisionID;           
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
DivisionID = CurrentTbl90RefExpert.DivisionID,              
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
                        //DivisionID may be not 0
                        if (CurrentTbl90RefExpert.DivisionID == 0 || CurrentTbl90RefExpert.RefExpertID == 0)   
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
                         a.DivisionID == CurrentTbl90RefExpert.DivisionID &&
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
    
        public new ICommand GetCommentByNameOrIdCommand  
    
        {
            get { return _getCommentByNameOrIdCommand ?? (_getCommentByNameOrIdCommand = new RelayCommand(delegate { GetCommentByNameOrId(null); })); }
        }

        public new void GetCommentByNameOrId(object o)
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
    
        public new ICommand AddCommentCommand      
    
        {
            get { return _addCommentCommand ?? (_addCommentCommand = new RelayCommand(delegate { AddComment(null); })); }
        }

        public new void AddComment(object o)
        {
                Tbl93CommentsList = new ObservableCollection<Tbl93Comment>();  
    
            Tbl93CommentsList.Insert(0, new Tbl93Comment { Info = CultRes.StringsRes.DatasetNew });    

            Tbl09DivisionsAllList = _allListVm.GetValueTbl09DivisionsAllList();      
    

            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            CommentsView.Refresh();
        }
        //---------------------------------------------------------------------------------------  
           
        private RelayCommand _copyCommentCommand;            
    
        public new ICommand CopyCommentCommand       
         
        {
            get { return _copyCommentCommand ?? (_copyCommentCommand = new RelayCommand(delegate { CopyComment(null); })); }
        }

        public new void CopyComment(object o)
        {
            Tbl93CommentsList = new ObservableCollection<Tbl93Comment>();

            var comment = _tbl93CommentsRepository.Get(CurrentTbl93Comment.CommentID);

            Tbl93CommentsList.Insert(0, new Tbl93Comment
            {                 
DivisionID = comment.DivisionID,              
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
    
        public new ICommand DeleteCommentCommand          
         
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
    
        public new ICommand SaveCommentCommand           
         
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
      
                            comment.DivisionID = CurrentTbl93Comment.DivisionID;            
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
DivisionID = CurrentTbl93Comment.DivisionID,              
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
                        //DivisionID may be not 0
                        if (CurrentTbl93Comment.DivisionID == 0)
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
                         a.DivisionID == CurrentTbl93Comment.DivisionID
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
        public new ICommand GetConnectedTablesCommand
        {
            get { return _getConnectedTablesCommand ?? (_getConnectedTablesCommand = new RelayCommand(delegate { GetConnectedTablesById(null); })); }
        }

        public void GetConnectedTablesById(object o)
        {
            SelectedDetailThreeRefTabIndex = 1;  //change to Connect tab

            Tbl03RegnumsList =
                new ObservableCollection<Tbl03Regnum>
                                         (from x in _tbl03RegnumsRepository.GetAll()
                                           where x.RegnumID == CurrentTbl09Division.RegnumID
                                           orderby x.RegnumName, x.Subregnum
                                           select x);


            RegnumsView = CollectionViewSource.GetDefaultView(Tbl03RegnumsList);
            RegnumsView.Refresh();
            //-----------------------------------------------------------------------------------
             Tbl15SubdivisionsList =
                 new ObservableCollection<Tbl15Subdivision>
                                          (from y in _tbl15SubdivisionsRepository.GetAll()
                                          where y.DivisionID == CurrentTbl09Division.DivisionID
                                          orderby y.SubdivisionName
                                          select y);


             SubdivisionsView = CollectionViewSource.GetDefaultView(Tbl15SubdivisionsList);
             SubdivisionsView.Refresh();
            //-----------------------------------------------------------------------------------
            Tbl90RefAuthorsList =
                new ObservableCollection<Tbl90Reference>((from refAu in _tbl90ReferencesRepository.GetAll()
                                                           where refAu.DivisionID == CurrentTbl09Division.DivisionID
                                                          && refAu.RefExpertID == null
                                                          && refAu.RefSourceID == null
                                                          orderby refAu.Tbl90RefAuthors.RefAuthorName, refAu.Tbl90RefAuthors.BookName, refAu.Tbl90RefAuthors.Page1
                                                          select refAu));

            RefAuthorsView = CollectionViewSource.GetDefaultView(Tbl90RefAuthorsList);
            RefAuthorsView.Refresh();
            //--------------------------------------------------------------------------------------
            Tbl90RefSourcesList =
                new ObservableCollection<Tbl90Reference>((from refSo in _tbl90ReferencesRepository.GetAll()
                                                           where refSo.DivisionID == CurrentTbl09Division.DivisionID
                                                          && refSo.RefExpertID == null
                                                          && refSo.RefAuthorID == null
                                                          orderby refSo.Tbl90RefSources.RefSourceName
                                                          select refSo));

            RefSourcesView = CollectionViewSource.GetDefaultView(Tbl90RefSourcesList);
            RefSourcesView.Refresh();
            //--------------------------------------------------------------------------------------
            Tbl90RefExpertsList =
                new ObservableCollection<Tbl90Reference>((from refEx in _tbl90ReferencesRepository.GetAll()
                                                           where refEx.DivisionID == CurrentTbl09Division.DivisionID
                                                          && refEx.RefAuthorID == null
                                                          && refEx.RefSourceID == null
                                                          orderby refEx.Tbl90RefExperts.RefExpertName
                                                          select refEx));

            RefExpertsView = CollectionViewSource.GetDefaultView(Tbl90RefExpertsList);
            RefExpertsView.Refresh();
            //-----------------------------------------------------------------------------------
            Tbl93CommentsList =
                new ObservableCollection<Tbl93Comment>((from comm in _tbl93CommentsRepository.GetAll()
                                                           where comm.DivisionID == CurrentTbl09Division.DivisionID
                                                           orderby comm.Info
                                                           select comm));

            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            CommentsView.Refresh();
            //------------------------------------------------------------------------------------

            Tbl09DivisionsAllList = _allListVm.GetValueTbl09DivisionsAllList();

            Tbl90AuthorsAllList = _allListVm.GetValueTbl90AuthorsAllList();

            Tbl90SourcesAllList = _allListVm.GetValueTbl90SourcesAllList();

            Tbl90ExpertsAllList = _allListVm.GetValueTbl90ExpertsAllList();
        }

        #endregion "Public Commands Connected Tables by DoubleClick"
   
 

 //    Part 10    

 

 //    Part 11    

     
        #region "Public Properties Tbl09Division"

        private string _searchDivisionName;
        public new string SearchDivisionName
        {
            get => _searchDivisionName; 
            set { _searchDivisionName = value; RaisePropertyChanged();  }
        }

        public new ICollectionView DivisionsView;
        public new Tbl09Division CurrentTbl09Division => DivisionsView?.CurrentItem as Tbl09Division;

        private ObservableCollection<Tbl09Division> _tbl09DivisionsList;
        public new ObservableCollection<Tbl09Division> Tbl09DivisionsList
        {
            get => _tbl09DivisionsList; 
            set {  _tbl09DivisionsList = value; RaisePropertyChanged();   }
        }

        private ObservableCollection<Tbl09Division> _tbl09DivisionsAllList;
        public new ObservableCollection<Tbl09Division> Tbl09DivisionsAllList
        {
            get => _tbl09DivisionsAllList; 
            set { _tbl09DivisionsAllList = value; RaisePropertyChanged(); }
        }

        #endregion "Public Properties"   
       
        #region "Public Properties Tbl03Regnum"

        private string _searchRegnumName;
        public new string SearchRegnumName
        {
            get => _searchRegnumName; 
            set { _searchRegnumName = value; RaisePropertyChanged(); }
        }

        public new  ICollectionView RegnumsView;
        public new  Tbl03Regnum CurrentTbl03Regnum => RegnumsView?.CurrentItem as Tbl03Regnum;           

        private ObservableCollection<Tbl03Regnum> _tbl03RegnumsList;
        public new  ObservableCollection<Tbl03Regnum> Tbl03RegnumsList
        {
            get => _tbl03RegnumsList; 
            set { _tbl03RegnumsList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl03Regnum> _tbl03RegnumsAllList;
        public new  ObservableCollection<Tbl03Regnum> Tbl03RegnumsAllList
        {
            get => _tbl03RegnumsAllList; 
            set { _tbl03RegnumsAllList = value; RaisePropertyChanged(); }       
        }

        #endregion "Public Properties"
   
  
       
        #region "Public Properties Tbl15Subdivision"

        private string _searchSubdivisionName;
        public string SearchSubdivisionName
        {
            get => _searchSubdivisionName; 
            set { _searchSubdivisionName = value; RaisePropertyChanged(); }
        }

        public ICollectionView SubdivisionsView;
        public Tbl15Subdivision CurrentTbl15Subdivision => SubdivisionsView?.CurrentItem as Tbl15Subdivision;           

        private ObservableCollection<Tbl15Subdivision> _tbl15SubdivisionsList;
        public ObservableCollection<Tbl15Subdivision> Tbl15SubdivisionsList
        {
            get => _tbl15SubdivisionsList; 
            set { _tbl15SubdivisionsList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl15Subdivision> _tbl15SubdivisionsAllList;
        public ObservableCollection<Tbl15Subdivision> Tbl15SubdivisionsAllList
        {
            get => _tbl15SubdivisionsAllList; 
            set { _tbl15SubdivisionsAllList = value; RaisePropertyChanged(); }       
        }

        #endregion "Public Properties"
   
  
       
        #region "Public Properties Tbl18Superclass"

        private string _searchSuperclassName;
        public string SearchSuperclassName
        {
            get => _searchSuperclassName; 
            set { _searchSuperclassName = value; RaisePropertyChanged(); }
        }

        public ICollectionView SuperclassesView;
        public Tbl18Superclass CurrentTbl18Superclass => SuperclassesView?.CurrentItem as Tbl18Superclass;           

        private ObservableCollection<Tbl18Superclass> _tbl18SuperclassesList;
        public ObservableCollection<Tbl18Superclass> Tbl18SuperclassesList
        {
            get => _tbl18SuperclassesList; 
            set { _tbl18SuperclassesList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl18Superclass> _tbl18SuperclassesAllList;
        public ObservableCollection<Tbl18Superclass> Tbl18SuperclassesAllList
        {
            get => _tbl18SuperclassesAllList; 
            set { _tbl18SuperclassesAllList = value; RaisePropertyChanged(); }       
        }

        #endregion "Public Properties"
   
   

 //    Part 12    

 

   }
}   
