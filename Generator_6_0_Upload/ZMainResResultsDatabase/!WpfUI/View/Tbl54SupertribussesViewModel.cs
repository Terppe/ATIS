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

    
         //    Tbl54SupertribussesViewModel Skriptdatum:  23.12.2017  10:32    

namespace WPFUI.Views.Database
{     
    
    public class Tbl54SupertribussesViewModel : Tbl03RegnumsViewModel
    {     
    
       #region "Private Data Members"  

        private readonly AllListVm _allListVm = new AllListVm();
           
        private readonly Repository<Tbl51Infrafamily, int> _tbl51InfrafamiliesRepository = new Repository<Tbl51Infrafamily, int>();   
   
        private readonly Repository<Tbl54Supertribus, int> _tbl54SupertribussesRepository = new Repository<Tbl54Supertribus, int>();   
           
        private readonly Repository<Tbl57Tribus, int> _tbl57TribussesRepository = new Repository<Tbl57Tribus, int>();   
          
        private readonly Repository<Tbl90Reference, int> _tbl90ReferencesRepository = new Repository<Tbl90Reference, int>();
        private readonly Repository<Tbl93Comment, int> _tbl93CommentsRepository = new Repository<Tbl93Comment, int>();    

        #endregion "Private Data Members"               
    
        #region "Constructor"

        public Tbl54SupertribussesViewModel()
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

           
        #region "Public Commands Basic Tbl54Supertribus"

        private RelayCommand _getSupertribusByNameOrIdCommand;     
    
        public ICommand GetSupertribusByNameOrIdCommand    
    
        {
            get { return _getSupertribusByNameOrIdCommand ?? (_getSupertribusByNameOrIdCommand = new RelayCommand(delegate { GetSupertribusByNameOrId(null); })); }   
        }

        private void GetSupertribusByNameOrId(object o)       
        {   
    
            int id;
            if (int.TryParse(SearchSupertribusName, out id))
                Tbl54SupertribussesList = new ObservableCollection<Tbl54Supertribus> { _tbl54SupertribussesRepository.Get(id) };
            else           
                Tbl54SupertribussesList = _allListVm.GetValueTbl54SupertribussesList(SearchSupertribusName);      
Tbl51InfrafamiliesAllList = _allListVm.GetValueTbl51InfrafamiliesAllList();      
  SupertribussesView = CollectionViewSource.GetDefaultView(Tbl54SupertribussesList);
            SupertribussesView.Refresh();
        }
        //------------------------------------------------------------------------------                
           
        private RelayCommand _addSupertribusCommand;           
    
        public ICommand AddSupertribusCommand       
    
        {
            get { return _addSupertribusCommand ?? (_addSupertribusCommand = new RelayCommand(delegate { AddSupertribus(null); })); }
        }

        private void AddSupertribus(object o)
        {
            Tbl54SupertribussesList = new ObservableCollection<Tbl54Supertribus>();   
Tbl54SupertribussesList.Insert(0, new Tbl54Supertribus{ SupertribusName= CultRes.StringsRes.DatasetNew });  

            Tbl51InfrafamiliesAllList = _allListVm.GetValueTbl51InfrafamiliesAllList();      
SupertribussesView = CollectionViewSource.GetDefaultView(Tbl54SupertribussesList);
            SupertribussesView.Refresh();
        }
        //---------------------------------------------------------------------------------------  
           
        private RelayCommand _copySupertribusCommand;              
    
        public ICommand CopySupertribusCommand             
         
        {
            get { return _copySupertribusCommand ?? (_copySupertribusCommand = new RelayCommand(delegate { CopySupertribus(null); })); }
        }

        private void CopySupertribus(object o)
        {
            Tbl54SupertribussesList = new ObservableCollection<Tbl54Supertribus>();

            var supertribus = _tbl54SupertribussesRepository.Get(CurrentTbl54Supertribus.SupertribusID);

            Tbl54SupertribussesList.Insert(0, new Tbl54Supertribus
            {                 
InfrafamilyID = supertribus.InfrafamilyID,              
                            SupertribusName = CultRes.StringsRes.DatasetNew,              
                            Valid = supertribus.Valid,
                            ValidYear = supertribus.ValidYear,
                            Synonym = supertribus.Synonym,
                            Author = supertribus.Author,
                            AuthorYear = supertribus.AuthorYear,
                            Info = supertribus.Info,
                            EngName = supertribus.EngName,
                            GerName = supertribus.GerName,
                            FraName = supertribus.FraName,
                            PorName = supertribus.PorName,
                            Memo = supertribus.Memo                    
        
            });

            SupertribussesView = CollectionViewSource.GetDefaultView(Tbl54SupertribussesList);
            SupertribussesView.Refresh();
        }
        //---------------------------------------------------------------------------------------                  
           
        private RelayCommand _deleteSupertribusCommand;              
    
        public ICommand DeleteSupertribusCommand             
         
        {
            get { return _deleteSupertribusCommand ?? (_deleteSupertribusCommand = new RelayCommand(delegate { DeleteSupertribus(null); })); }
        }

        private void DeleteSupertribus(object o)
        {
            try
            {
                var supertribus = _tbl54SupertribussesRepository.Get(CurrentTbl54Supertribus.SupertribusID);
                if (supertribus!= null)
                {
                    if (WpfMessageBox.Show(CultRes.StringsRes.DeleteQuestion1, CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl54Supertribus.SupertribusName,
                         MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes) 
                    return;
                    _tbl54SupertribussesRepository.Delete(supertribus);
                    _tbl54SupertribussesRepository.Save();

                    WpfMessageBox.Show(CultRes.StringsRes.DeleteSuccess, CurrentTbl54Supertribus.SupertribusName, 
                        MessageBoxButton.OK, MessageBoxImage.Information);

                    if (SearchSupertribusName == null)                   
                        GetConnectedTablesById(o); //refresh doubleClick                                       
                    else
                    {
                        Tbl54SupertribussesList = _allListVm.GetValueTbl54SupertribussesList(SearchSupertribusName); 
                    }    
SupertribussesView = CollectionViewSource.GetDefaultView(Tbl54SupertribussesList);
                         SupertribussesView.Refresh();
                }
                else
                {
                    WpfMessageBox.Show(CultRes.StringsRes.Information, CultRes.StringsRes.DeleteCan + " " + CurrentTbl54Supertribus.SupertribusName+ " " + CultRes.StringsRes.DeleteCan1,
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
           
        private RelayCommand _saveSupertribusCommand;              
     
        public ICommand SaveSupertribusCommand             
         
        {
            get { return _saveSupertribusCommand ?? (_saveSupertribusCommand = new RelayCommand(delegate { SaveSupertribus(null); })); }
        }

        private void SaveSupertribus(object o)
        {
            try
            {
                var supertribus = _tbl54SupertribussesRepository.Get(CurrentTbl54Supertribus.SupertribusID);
                if (CurrentTbl54Supertribus == null)              
                    WpfMessageBox.Show(CultRes.StringsRes.Warning, CultRes.StringsRes.DatasetNotExist, 
                        MessageBoxButton.OK, MessageBoxImage.Warning);               
                else
                {
                    if (CurrentTbl54Supertribus.SupertribusID!= 0)
                    {
                        if (supertribus!= null) //update
                        {   
supertribus.SupertribusName = CurrentTbl54Supertribus.SupertribusName;  
supertribus.InfrafamilyID = CurrentTbl54Supertribus.InfrafamilyID;
                            supertribus.Valid = CurrentTbl54Supertribus.Valid;
                            supertribus.ValidYear = CurrentTbl54Supertribus.ValidYear;
                            supertribus.Synonym = CurrentTbl54Supertribus.Synonym;
                            supertribus.Author = CurrentTbl54Supertribus.Author;
                            supertribus.AuthorYear = CurrentTbl54Supertribus.AuthorYear;
                            supertribus.Info = CurrentTbl54Supertribus.Info;
                            supertribus.EngName = CurrentTbl54Supertribus.EngName;
                            supertribus.GerName = CurrentTbl54Supertribus.GerName;
                            supertribus.FraName = CurrentTbl54Supertribus.FraName;
                            supertribus.PorName = CurrentTbl54Supertribus.PorName;
                            supertribus.Updater = Environment.UserName;
                            supertribus.UpdaterDate = DateTime.Now; 
                            supertribus.Memo = CurrentTbl54Supertribus.Memo;  
         
                        }
                    }
                    else
                    {
                        _tbl54SupertribussesRepository.Add(new Tbl54Supertribus     //add new
                        {   
InfrafamilyID= CurrentTbl54Supertribus.InfrafamilyID,              
                            SupertribusName= CurrentTbl54Supertribus.SupertribusName,              
                            CountID = RandomHelper.Randomnumber(),
                            Valid = CurrentTbl54Supertribus.Valid,
                            ValidYear = CurrentTbl54Supertribus.ValidYear,
                            Synonym = CurrentTbl54Supertribus.Synonym,
                            Author = CurrentTbl54Supertribus.Author,
                            AuthorYear = CurrentTbl54Supertribus.AuthorYear,
                            Info = CurrentTbl54Supertribus.Info,
                            EngName = CurrentTbl54Supertribus.EngName,
                            GerName = CurrentTbl54Supertribus.GerName,
                            FraName = CurrentTbl54Supertribus.FraName,
                            PorName = CurrentTbl54Supertribus.PorName,
                            Writer = Environment.UserName,
                            WriterDate = DateTime.Now,
                            Updater = Environment.UserName,
                            UpdaterDate = DateTime.Now,
                            Memo = CurrentTbl54Supertribus.Memo   
         
                        });
                    }
                    {
                        //InfrafamilyID may be not 0
                        if (CurrentTbl54Supertribus.InfrafamilyID == 0)
                        {
                            WpfMessageBox.Show(CultRes.StringsRes.RequiredGenealogyConnect, CultRes.StringsRes.RequiredInput,
                                MessageBoxButton.OK, MessageBoxImage.Information);
                            return;
                        }

                        //check if dataset with Name and vb-name already exist
                        var dataset = new ObservableCollection<Tbl54Supertribus>
                        (from a in _tbl54SupertribussesRepository.GetAll()
                         where
                         a.SupertribusName.Trim() == CurrentTbl54Supertribus.SupertribusName.Trim() &&                
                         a.InfrafamilyID == CurrentTbl54Supertribus.InfrafamilyID
                         select a);

                        if (dataset.Count != 0 && CurrentTbl54Supertribus.SupertribusID== 0)  //dataset exist
                        {
                              WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTbl54Supertribus.SupertribusName,
                              MessageBoxButton.OK, MessageBoxImage.Information);
                        } 

                        if (dataset.Count == 0 && CurrentTbl54Supertribus.SupertribusID== 0 ||
                            dataset.Count != 0 && CurrentTbl54Supertribus.SupertribusID != 0  ||
                            dataset.Count == 0 && CurrentTbl54Supertribus.SupertribusID != 0) //new dataset and update
                        {
                            if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTbl54Supertribus.SupertribusName,
                                    MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)                            
                                return;                            
                            {
                                _tbl54SupertribussesRepository.Save();

                                WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess, CurrentTbl54Supertribus.SupertribusName,
                                    MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                        } 
         
                        if (SearchSupertribusName == null && CurrentTbl54Supertribus.SupertribusID == 0)  //new Dataset                        
                            Tbl54SupertribussesList = _allListVm.GetValueTbl54SupertribussesList();  //last Dataset
                        if (SearchSupertribusName == null && CurrentTbl54Supertribus.SupertribusID != 0)   //update 
                            Tbl54SupertribussesList = _allListVm.GetValueTbl54SupertribussesList(CurrentTbl54Supertribus.SupertribusID);
                        if (SearchSupertribusName != null && CurrentTbl54Supertribus.SupertribusID == 0)  //new Dataset                        
                            Tbl54SupertribussesList = _allListVm.GetValueTbl54SupertribussesList();  //last Dataset
                        if (SearchSupertribusName != null && CurrentTbl54Supertribus.SupertribusID != 0)   //update 
                            Tbl54SupertribussesList = _allListVm.GetValueTbl54SupertribussesList(CurrentTbl54Supertribus.SupertribusID);

                            SupertribussesView = CollectionViewSource.GetDefaultView(Tbl54SupertribussesList);
                            SupertribussesView.Refresh();                          
         
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

           
        #region "Public Commands Connect <== Tbl51Infrafamily"                 

        private RelayCommand _getInfrafamilyByNameOrIdCommand;     
    
        public ICommand GetInfrafamilyByNameOrIdCommand    
    
        {
            get { return _getInfrafamilyByNameOrIdCommand ?? (_getInfrafamilyByNameOrIdCommand = new RelayCommand(delegate { GetInfrafamilyByNameOrId(null); })); }   
        }

        private void GetInfrafamilyByNameOrId(object o)       
        {   
    
            int id;
            if (int.TryParse(SearchInfrafamilyName, out id))
                Tbl51InfrafamiliesList = new ObservableCollection<Tbl51Infrafamily> { _tbl51InfrafamiliesRepository.Get(id) };
            else
                Tbl51InfrafamiliesList = _allListVm.GetValueTbl51InfrafamiliesList(SearchInfrafamilyName);     
InfrafamiliesView = CollectionViewSource.GetDefaultView(Tbl51InfrafamiliesList);
            InfrafamiliesView.Refresh();
        }
        //------------------------------------------------------------------------------                
           
        private RelayCommand _addInfrafamilyCommand;      
    
        public ICommand AddInfrafamilyCommand    
    
        {
            get { return _addInfrafamilyCommand ?? (_addInfrafamilyCommand = new RelayCommand(delegate { AddInfrafamily(null); })); }
        }

        private void AddInfrafamily(object o)
        {
            Tbl51InfrafamiliesList = new ObservableCollection<Tbl51Infrafamily>();   
Tbl51InfrafamiliesList.Insert(0, new Tbl51Infrafamily{ InfrafamilyName = CultRes.StringsRes.DatasetNew });   

            if (Tbl48SubfamiliesAllList == null)
            Tbl48SubfamiliesAllList = _allListVm.GetValueTbl48SubfamiliesAllList();    
InfrafamiliesView = CollectionViewSource.GetDefaultView(Tbl51InfrafamiliesList);
            InfrafamiliesView.Refresh();
        }
        //---------------------------------------------------------------------------------------  
           
        private RelayCommand _copyInfrafamilyCommand;            
    
        public ICommand CopyInfrafamilyCommand          
         
        {
            get { return _copyInfrafamilyCommand ?? (_copyInfrafamilyCommand = new RelayCommand(delegate { CopyInfrafamily(null); })); }
        }

        private void CopyInfrafamily(object o)
        {
            Tbl51InfrafamiliesList = new ObservableCollection<Tbl51Infrafamily>();

            var infrafamily = _tbl51InfrafamiliesRepository.Get(CurrentTbl51Infrafamily.InfrafamilyID);

            Tbl51InfrafamiliesList.Insert(0, new Tbl51Infrafamily
            {                 
SubfamilyID = infrafamily.SubfamilyID,     
                InfrafamilyName = CultRes.StringsRes.DatasetNew,     
                Valid = infrafamily.Valid,
                ValidYear = infrafamily.ValidYear,
                Synonym = infrafamily.Synonym,
                Author = infrafamily.Author,
                AuthorYear = infrafamily.AuthorYear,
                Info = infrafamily.Info,
                EngName = infrafamily.EngName,
                GerName = infrafamily.GerName,
                FraName = infrafamily.FraName,
                PorName = infrafamily.PorName,
                Memo = infrafamily.Memo           
        
            });

            InfrafamiliesView = CollectionViewSource.GetDefaultView(Tbl51InfrafamiliesList);
            InfrafamiliesView.Refresh();
        }
        //---------------------------------------------------------------------------------------                  
           
        private RelayCommand _deleteInfrafamilyCommand;              
    
        public ICommand DeleteInfrafamilyCommand             
         
        {
            get { return _deleteInfrafamilyCommand ?? (_deleteInfrafamilyCommand = new RelayCommand(delegate { DeleteInfrafamily(null); })); }
        }

        private void DeleteInfrafamily(object o)
        {
            try
            {
                var infrafamily = _tbl51InfrafamiliesRepository.Get(CurrentTbl51Infrafamily.InfrafamilyID);
                if (infrafamily!= null)
                {  
         
                    if (WpfMessageBox.Show(CultRes.StringsRes.DeleteQuestion1, CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl51Infrafamily.InfrafamilyName,
                         MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes) 
                    return;

                    _tbl51InfrafamiliesRepository.Delete(infrafamily);
                    _tbl51InfrafamiliesRepository.Save();

                    WpfMessageBox.Show(CultRes.StringsRes.DeleteSuccess, CurrentTbl51Infrafamily.InfrafamilyName, 
                        MessageBoxButton.OK, MessageBoxImage.Information);  
         
                        if (SearchInfrafamilyName == null)                       
                            GetConnectedTablesById(o); //refresh doubleClick                                          
                        else
                        {
                        Tbl51InfrafamiliesList = _allListVm.GetValueTbl51InfrafamiliesList(SearchInfrafamilyName);  
InfrafamiliesView = CollectionViewSource.GetDefaultView(Tbl51InfrafamiliesList);
                            InfrafamiliesView.Refresh();
                    }
                }
                else
                {   
    
                    WpfMessageBox.Show(CultRes.StringsRes.Information, CultRes.StringsRes.DeleteCan + " " + CurrentTbl51Infrafamily.InfrafamilyName+ " " + CultRes.StringsRes.DeleteCan1,
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
           
        private RelayCommand _saveInfrafamilyCommand;              
    
        public ICommand SaveInfrafamilyCommand             
         
        {
            get { return _saveInfrafamilyCommand ?? (_saveInfrafamilyCommand = new RelayCommand(delegate { SaveInfrafamily(null); })); }
        }

        private void SaveInfrafamily(object o)
        {
            try
            {
                var infrafamily = _tbl51InfrafamiliesRepository.Get(CurrentTbl51Infrafamily.InfrafamilyID);
                if (CurrentTbl51Infrafamily == null)              
                    WpfMessageBox.Show(CultRes.StringsRes.Warning, CultRes.StringsRes.DatasetNotExist, MessageBoxButton.OK, MessageBoxImage.Warning);               
                else
                {
                    if (CurrentTbl51Infrafamily.InfrafamilyID!= 0)
                    {
                        if (infrafamily!= null) //update
                        {   
infrafamily.SubfamilyID = CurrentTbl51Infrafamily.SubfamilyID;
                            infrafamily.InfrafamilyName= CurrentTbl51Infrafamily.InfrafamilyName;             
infrafamily.Valid = CurrentTbl51Infrafamily.Valid;
                            infrafamily.ValidYear = CurrentTbl51Infrafamily.ValidYear;
                            infrafamily.Synonym = CurrentTbl51Infrafamily.Synonym;
                            infrafamily.Author = CurrentTbl51Infrafamily.Author;
                            infrafamily.AuthorYear = CurrentTbl51Infrafamily.AuthorYear;
                            infrafamily.Info = CurrentTbl51Infrafamily.Info;
                            infrafamily.EngName = CurrentTbl51Infrafamily.EngName;
                            infrafamily.GerName = CurrentTbl51Infrafamily.GerName;
                            infrafamily.FraName = CurrentTbl51Infrafamily.FraName;
                            infrafamily.PorName = CurrentTbl51Infrafamily.PorName;
                            infrafamily.Updater = Environment.UserName;
                            infrafamily.UpdaterDate = DateTime.Now; 
                            infrafamily.Memo = CurrentTbl51Infrafamily.Memo;   
         
                        }
                    }
                    else
                    {
                        _tbl51InfrafamiliesRepository.Add(new Tbl51Infrafamily     //add new
                        {   
SubfamilyID = CurrentTbl51Infrafamily.SubfamilyID,     
                            InfrafamilyName= CurrentTbl51Infrafamily.InfrafamilyName,              
                            CountID = RandomHelper.Randomnumber(),
                            Valid = CurrentTbl51Infrafamily.Valid,
                            ValidYear = CurrentTbl51Infrafamily.ValidYear,
                            Synonym = CurrentTbl51Infrafamily.Synonym,
                            Author = CurrentTbl51Infrafamily.Author,
                            AuthorYear = CurrentTbl51Infrafamily.AuthorYear,
                            Info = CurrentTbl51Infrafamily.Info,
                            EngName = CurrentTbl51Infrafamily.EngName,
                            GerName = CurrentTbl51Infrafamily.GerName,
                            FraName = CurrentTbl51Infrafamily.FraName,
                            PorName = CurrentTbl51Infrafamily.PorName,
                            Writer = Environment.UserName,
                            WriterDate = DateTime.Now,
                            Updater = Environment.UserName,
                            UpdaterDate = DateTime.Now,
                            Memo = CurrentTbl51Infrafamily.Memo   
         
                        });
                    }
                    {
                        //SubfamilyID may be not 0
                        if (CurrentTbl51Infrafamily.SubfamilyID == 0)
                        {
                            WpfMessageBox.Show(CultRes.StringsRes.RequiredGenealogyConnect, CultRes.StringsRes.RequiredInput,
                                MessageBoxButton.OK, MessageBoxImage.Information);
                            return;
                        }

                        //check if dataset with Name and vb-name already exist
                        var dataset = new ObservableCollection<Tbl51Infrafamily>
                        (from a in _tbl51InfrafamiliesRepository.GetAll()
                         where
                         a.InfrafamilyName.Trim() == CurrentTbl51Infrafamily.InfrafamilyName.Trim() &&                
                         a.SubfamilyID == CurrentTbl51Infrafamily.SubfamilyID
                         select a);

                        if (dataset.Count != 0 && CurrentTbl51Infrafamily.InfrafamilyID== 0)  //dataset exist
                        {
                              WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTbl51Infrafamily.InfrafamilyName,
                              MessageBoxButton.OK, MessageBoxImage.Information);
                        }

                        if (dataset.Count == 0 && CurrentTbl51Infrafamily.InfrafamilyID == 0 ||
                            dataset.Count != 0 && CurrentTbl51Infrafamily.InfrafamilyID != 0  ||
                            dataset.Count == 0 && CurrentTbl51Infrafamily.InfrafamilyID != 0) //new dataset and update
                        {
                            if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTbl51Infrafamily.InfrafamilyName,
                                    MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)                            
                                return;                            
                            {
                                _tbl51InfrafamiliesRepository.Save();

                                WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess, CurrentTbl51Infrafamily.InfrafamilyName,
                                    MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                        }  
        
                        if (SearchInfrafamilyName == null && CurrentTbl51Infrafamily.InfrafamilyID == 0)  //new Dataset                        
                            GetConnectedTablesById(o); //refresh doubleClick                                          
                        if (SearchInfrafamilyName == null && CurrentTbl51Infrafamily.InfrafamilyID != 0)  //update                     
                            Tbl51InfrafamiliesList = _allListVm.GetValueTbl51InfrafamiliesList(CurrentTbl51Infrafamily.InfrafamilyID);
                        if (SearchInfrafamilyName != null && CurrentTbl51Infrafamily.InfrafamilyID == 0)  //new Dataset                        
                            GetConnectedTablesById(o); //refresh doubleClick                                          
                        if (SearchInfrafamilyName != null && CurrentTbl51Infrafamily.InfrafamilyID != 0)  //update                     
                            Tbl51InfrafamiliesList = _allListVm.GetValueTbl51InfrafamiliesList(CurrentTbl51Infrafamily.InfrafamilyID); 

                        InfrafamiliesView = CollectionViewSource.GetDefaultView(Tbl51InfrafamiliesList);
                        InfrafamiliesView.Refresh();         
      
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

             
        #region "Public Commands Connect ==> Tbl57Tribus"                 

        private RelayCommand _getTribusByNameOrIdCommand;     
               
        public ICommand GetTribusByNameOrIdCommand   
               
        {
            get { return _getTribusByNameOrIdCommand ?? (_getTribusByNameOrIdCommand = new RelayCommand(delegate { GetTribusByNameOrId(null); })); }   
        }

        private void GetTribusByNameOrId(object o)       
        {   
                
            int id;
            if (int.TryParse(SearchTribusName, out id))
                Tbl57TribussesList = new ObservableCollection<Tbl57Tribus> { _tbl57TribussesRepository.Get(id) };
            else 
                Tbl57TribussesList = _allListVm.GetValueTbl57TribussesList(SearchTribusName);       
  TribussesView = CollectionViewSource.GetDefaultView(Tbl57TribussesList);
            TribussesView.Refresh();
        }
        //------------------------------------------------------------------------------     
                      
        private RelayCommand _addTribusCommand;      
                       
        public ICommand AddTribusCommand    
                        
        {
            get { return _addTribusCommand ?? (_addTribusCommand = new RelayCommand(delegate { AddTribus(null); })); }
        }

        private void AddTribus(object o)
        {
            Tbl57TribussesList = new ObservableCollection<Tbl57Tribus>();   
  Tbl57TribussesList.Insert(0, new Tbl57Tribus{ TribusName= CultRes.StringsRes.DatasetNew });  

            if (Tbl54SupertribussesAllList == null)
            Tbl54SupertribussesAllList = _allListVm.GetValueTbl54SupertribussesAllList();     
  TribussesView = CollectionViewSource.GetDefaultView(Tbl57TribussesList);
            TribussesView.Refresh();
        }
        //---------------------------------------------------------------------------------------  
                              
        private RelayCommand _copyTribusCommand;            
                              
        public ICommand CopyTribusCommand          
                                 
        {
            get { return _copyTribusCommand ?? (_copyTribusCommand = new RelayCommand(delegate { CopyTribus(null); })); }
        }

        private void CopyTribus(object o)
        {
            Tbl57TribussesList = new ObservableCollection<Tbl57Tribus>();

            var tribus = _tbl57TribussesRepository.Get(CurrentTbl57Tribus.TribusID);

            Tbl57TribussesList.Insert(0, new Tbl57Tribus
            {                 
  SupertribusID = tribus.SupertribusID,
                TribusName = CultRes.StringsRes.DatasetNew,     
                Valid = tribus.Valid,
                ValidYear = tribus.ValidYear,
                Synonym = tribus.Synonym,
                Author = tribus.Author,
                AuthorYear = tribus.AuthorYear,
                Info = tribus.Info,
                EngName = tribus.EngName,
                GerName = tribus.GerName,
                FraName = tribus.FraName,
                PorName = tribus.PorName,
                Memo = tribus.Memo         
                                     
            });

            TribussesView = CollectionViewSource.GetDefaultView(Tbl57TribussesList);
            TribussesView.Refresh();
        }
        //---------------------------------------------------------------------------------------    
                                           
        private RelayCommand _deleteTribusCommand;              
                                           
        public ICommand DeleteTribusCommand             
                                                
        {
            get { return _deleteTribusCommand ?? (_deleteTribusCommand = new RelayCommand(delegate { DeleteTribus(null); })); }
        }

        private void DeleteTribus(object o)
        {
            try
            {
                var tribus = _tbl57TribussesRepository.Get(CurrentTbl57Tribus.TribusID);
                if (tribus!= null)
                {  
                                                    
                    if (WpfMessageBox.Show(CultRes.StringsRes.DeleteQuestion1, CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl57Tribus.TribusName,
                         MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes) 
                    return;

                    _tbl57TribussesRepository.Delete(tribus);
                    _tbl57TribussesRepository.Save();

                    WpfMessageBox.Show(CultRes.StringsRes.DeleteSuccess, CurrentTbl57Tribus.TribusName,
                       MessageBoxButton.OK, MessageBoxImage.Information);  
                                                        
                        if (SearchTribusName == null)                       
                            GetConnectedTablesById(o); //refresh doubleClick                                          
                        else
                        {
                        Tbl57TribussesList = _allListVm.GetValueTbl57TribussesList(SearchTribusName);  
  TribussesView = CollectionViewSource.GetDefaultView(Tbl57TribussesList);
                            TribussesView.Refresh();
                    }
                }
                else
                {   
                                                          
                    WpfMessageBox.Show(CultRes.StringsRes.Information, CultRes.StringsRes.DeleteCan + " " + CurrentTbl57Tribus.TribusName+ " " + CultRes.StringsRes.DeleteCan1,
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
                                                               
        private RelayCommand _saveTribusCommand;              
                                                                 
        public ICommand SaveTribusCommand             
                                                                     
        {
            get { return _saveTribusCommand ?? (_saveTribusCommand = new RelayCommand(delegate { SaveTribus(null); })); }
        }

        private void SaveTribus(object o)
        {
            try
            {
                var tribus = _tbl57TribussesRepository.Get(CurrentTbl57Tribus.TribusID);
                if (CurrentTbl57Tribus == null)               
                    WpfMessageBox.Show(CultRes.StringsRes.Warning, CultRes.StringsRes.DatasetNotExist, 
                        MessageBoxButton.OK, MessageBoxImage.Warning);              
                else
                {
                    if (CurrentTbl57Tribus.TribusID!= 0)
                    {
                        if (tribus!= null) //update
                        {   
  tribus.SupertribusID= CurrentTbl57Tribus.SupertribusID;            
                            tribus.TribusName= CurrentTbl57Tribus.TribusName;
                            tribus.Valid = CurrentTbl57Tribus.Valid;
                            tribus.ValidYear = CurrentTbl57Tribus.ValidYear;
                            tribus.Synonym = CurrentTbl57Tribus.Synonym;
                            tribus.Author = CurrentTbl57Tribus.Author;
                            tribus.AuthorYear = CurrentTbl57Tribus.AuthorYear;
                            tribus.Info = CurrentTbl57Tribus.Info;
                            tribus.EngName = CurrentTbl57Tribus.EngName;
                            tribus.GerName = CurrentTbl57Tribus.GerName;
                            tribus.FraName = CurrentTbl57Tribus.FraName;
                            tribus.PorName = CurrentTbl57Tribus.PorName;
                            tribus.Updater = Environment.UserName;
                            tribus.UpdaterDate = DateTime.Now;
                            tribus.Memo = CurrentTbl57Tribus.Memo;                                                              
                                                                            
                        }
                    }
                    else
                    {
                        _tbl57TribussesRepository.Add(new Tbl57Tribus    // add new
                        {   
  SupertribusID= CurrentTbl57Tribus.SupertribusID,              
                            TribusName= CurrentTbl57Tribus.TribusName,              
                            CountID = RandomHelper.Randomnumber(),
                            Valid = CurrentTbl57Tribus.Valid,
                            ValidYear = CurrentTbl57Tribus.ValidYear,
                            Synonym = CurrentTbl57Tribus.Synonym,
                            Author = CurrentTbl57Tribus.Author,
                            AuthorYear = CurrentTbl57Tribus.AuthorYear,
                            Info = CurrentTbl57Tribus.Info,
                            EngName = CurrentTbl57Tribus.EngName,
                            GerName = CurrentTbl57Tribus.GerName,
                            FraName = CurrentTbl57Tribus.FraName,
                            PorName = CurrentTbl57Tribus.PorName,
                            Writer = Environment.UserName,
                            WriterDate = DateTime.Now,
                            Updater = Environment.UserName,
                            UpdaterDate = DateTime.Now,
                            Memo = CurrentTbl57Tribus.Memo   
                                                                                    
                        });
                    }
                    {
                        //SupertribusID may be not 0
                        if (CurrentTbl57Tribus.SupertribusID == 0)
                        {
                            WpfMessageBox.Show(CultRes.StringsRes.RequiredGenealogyConnect, CultRes.StringsRes.RequiredInput,
                                MessageBoxButton.OK, MessageBoxImage.Information);
                            return;
                        }

                        //check if dataset with Name and vb-name already exist
                        var dataset = new ObservableCollection<Tbl57Tribus>
                        (from a in _tbl57TribussesRepository.GetAll()
                         where
                         a.TribusName.Trim() == CurrentTbl57Tribus.TribusName.Trim() &&                
                         a.SupertribusID == CurrentTbl57Tribus.SupertribusID
                         select a);

                        if (dataset.Count != 0 && CurrentTbl57Tribus.TribusID== 0)  //dataset exist
                        {
                              WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTbl57Tribus.TribusName,
                              MessageBoxButton.OK, MessageBoxImage.Information);
                        }

                        if (dataset.Count == 0 && CurrentTbl57Tribus.TribusID == 0 ||
                            dataset.Count != 0 && CurrentTbl57Tribus.TribusID != 0  ||
                            dataset.Count == 0 && CurrentTbl57Tribus.TribusID != 0) //new dataset and update
                        {
                            if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTbl57Tribus.TribusName,
                                    MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)                            
                                return;                            
                            {
                                _tbl57TribussesRepository.Save();

                                WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess, CurrentTbl57Tribus.TribusName,
                                    MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                        }  
                
                        if (SearchTribusName == null && CurrentTbl57Tribus.TribusID == 0)  //new Dataset                        
                            Tbl57TribussesList = _allListVm.GetValueTbl57TribussesList();  //last Dataset
                        if (SearchTribusName == null && CurrentTbl57Tribus.TribusID != 0)   //update
                            Tbl57TribussesList = _allListVm.GetValueTbl57TribussesList(CurrentTbl57Tribus.TribusID);
                        if (SearchTribusName != null && CurrentTbl57Tribus.TribusID == 0)  //new Dataset
                            Tbl57TribussesList = _allListVm.GetValueTbl57TribussesList();  //last Dataset
                        if (SearchTribusName != null && CurrentTbl57Tribus.TribusID != 0)   //update
                            Tbl57TribussesList = _allListVm.GetValueTbl57TribussesList(CurrentTbl57Tribus.TribusID);
                                                                       

                        TribussesView = CollectionViewSource.GetDefaultView(Tbl57TribussesList);
                        TribussesView.Refresh();             
                                                                                          
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

           Tbl54SupertribussesAllList = _allListVm.GetValueTbl54SupertribussesAllList();
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
SupertribusID = refAuthor.SupertribusID,              
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
         
                            refAuthor.SupertribusID = CurrentTbl90RefAuthor.SupertribusID;  
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
SupertribusID = CurrentTbl90RefAuthor.SupertribusID,              
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
                        //SupertribusID may be not 0
                        if (CurrentTbl90RefAuthor.SupertribusID == 0 || CurrentTbl90RefAuthor.RefAuthorID == 0)
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
                         a.SupertribusID == CurrentTbl90RefAuthor.SupertribusID &&
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

            Tbl54SupertribussesAllList = _allListVm.GetValueTbl54SupertribussesAllList();
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
SupertribusID = refSource.SupertribusID,              
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
         
                            refSource.SupertribusID = CurrentTbl90RefSource.SupertribusID;            
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
SupertribusID = CurrentTbl90RefSource.SupertribusID,              
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
                        //SupertribusID may be not 0
                        if (CurrentTbl90RefSource.SupertribusID == 0 || CurrentTbl90RefSource.RefSourceID == 0)
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
                         a.SupertribusID == CurrentTbl90RefSource.SupertribusID &&
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

            Tbl54SupertribussesAllList = _allListVm.GetValueTbl54SupertribussesAllList();
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
SupertribusID = refExpert.SupertribusID,              
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
      
                            refExpert.SupertribusID = CurrentTbl90RefExpert.SupertribusID;           
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
SupertribusID = CurrentTbl90RefExpert.SupertribusID,              
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
                        //SupertribusID may be not 0
                        if (CurrentTbl90RefExpert.SupertribusID == 0 || CurrentTbl90RefExpert.RefExpertID == 0)   
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
                         a.SupertribusID == CurrentTbl90RefExpert.SupertribusID &&
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

            Tbl54SupertribussesAllList = _allListVm.GetValueTbl54SupertribussesAllList();      
    

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
SupertribusID = comment.SupertribusID,              
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
      
                            comment.SupertribusID = CurrentTbl93Comment.SupertribusID;            
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
SupertribusID = CurrentTbl93Comment.SupertribusID,              
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
                        //SupertribusID may be not 0
                        if (CurrentTbl93Comment.SupertribusID == 0)
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
                         a.SupertribusID == CurrentTbl93Comment.SupertribusID
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

        public  void GetConnectedTablesById(object o)
        {
            SelectedDetailThreeRefTabIndex = 1;  //change to Connect tab

            Tbl51InfrafamiliesList =  new ObservableCollection<Tbl51Infrafamily>
                                                       (from x in _tbl51InfrafamiliesRepository.GetAll()
                                                       where x.InfrafamilyID == CurrentTbl54Supertribus.InfrafamilyID
                                                       orderby x.InfrafamilyName
                                                       select x);

            InfrafamiliesView = CollectionViewSource.GetDefaultView(Tbl51InfrafamiliesList);
            InfrafamiliesView.Refresh();
            //-----------------------------------------------------------------------------------
            Tbl57TribussesList =  new ObservableCollection<Tbl57Tribus>
                                                       (from y in _tbl57TribussesRepository.GetAll()
                                                       where y.SupertribusID == CurrentTbl54Supertribus.SupertribusID
                                                       orderby y.TribusName
                                                       select y);


            TribussesView = CollectionViewSource.GetDefaultView(Tbl57TribussesList);
            TribussesView.Refresh();
            //-----------------------------------------------------------------------------------
            Tbl90RefAuthorsList =  new ObservableCollection<Tbl90Reference>
                                                          (from refAu in _tbl90ReferencesRepository.GetAll()
                                                          where refAu.SupertribusID == CurrentTbl54Supertribus.SupertribusID
                                                          && refAu.RefExpertID == null
                                                          && refAu.RefSourceID == null
                                                          orderby refAu.Tbl90RefAuthors.RefAuthorName, refAu.Tbl90RefAuthors.BookName, refAu.Tbl90RefAuthors.Page1
                                                          select refAu);

            RefAuthorsView = CollectionViewSource.GetDefaultView(Tbl90RefAuthorsList);
            RefAuthorsView.Refresh();
            //--------------------------------------------------------------------------------------
            Tbl90RefSourcesList =  new ObservableCollection<Tbl90Reference>
                                                          (from refSo in _tbl90ReferencesRepository.GetAll()
                                                          where refSo.SupertribusID == CurrentTbl54Supertribus.SupertribusID
                                                          && refSo.RefExpertID == null
                                                          && refSo.RefAuthorID == null
                                                          orderby refSo.Tbl90RefSources.RefSourceName
                                                          select refSo);

            RefSourcesView = CollectionViewSource.GetDefaultView(Tbl90RefSourcesList);
            RefSourcesView.Refresh();
            //--------------------------------------------------------------------------------------
            Tbl90RefExpertsList =   new ObservableCollection<Tbl90Reference>
                                                          (from refEx in _tbl90ReferencesRepository.GetAll()
                                                          where refEx.SupertribusID == CurrentTbl54Supertribus.SupertribusID
                                                          && refEx.RefAuthorID == null
                                                          && refEx.RefSourceID == null
                                                          orderby refEx.Tbl90RefExperts.RefExpertName
                                                          select refEx);

            RefExpertsView = CollectionViewSource.GetDefaultView(Tbl90RefExpertsList);
            RefExpertsView.Refresh();
            //-----------------------------------------------------------------------------------
            Tbl93CommentsList =  new ObservableCollection<Tbl93Comment>
                                                        (from comm in _tbl93CommentsRepository.GetAll()
                                                        where comm.SupertribusID == CurrentTbl54Supertribus.SupertribusID
                                                        orderby comm.Info
                                                        select comm);

            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            CommentsView.Refresh();
            //------------------------------------------------------------------------------------    

  Tbl48SubfamiliesAllList = _allListVm.GetValueTbl48SubfamiliesAllList();

            Tbl54SupertribussesAllList = _allListVm.GetValueTbl54SupertribussesAllList();

            Tbl90AuthorsAllList = _allListVm.GetValueTbl90AuthorsAllList();

            Tbl90SourcesAllList = _allListVm.GetValueTbl90SourcesAllList();

            Tbl90ExpertsAllList = _allListVm.GetValueTbl90ExpertsAllList();

        }
        #endregion "Public Commands Connected Tables by DoubleClick"   
 

 //    Part 10    

 

 //    Part 11    

     
        #region "Public Properties Tbl54Supertribus"

        private string _searchSupertribusName;
        public  string SearchSupertribusName
        {
            get => _searchSupertribusName; 
            set { _searchSupertribusName = value; RaisePropertyChanged();  }
        }

        public  ICollectionView SupertribussesView;
        public  Tbl54Supertribus CurrentTbl54Supertribus => SupertribussesView?.CurrentItem as Tbl54Supertribus;

        private ObservableCollection<Tbl54Supertribus> _tbl54SupertribussesList;
        public  ObservableCollection<Tbl54Supertribus> Tbl54SupertribussesList
        {
            get => _tbl54SupertribussesList; 
            set {  _tbl54SupertribussesList = value; RaisePropertyChanged();   }
        }

        private ObservableCollection<Tbl54Supertribus> _tbl54SupertribussesAllList;
        public  ObservableCollection<Tbl54Supertribus> Tbl54SupertribussesAllList
        {
            get => _tbl54SupertribussesAllList; 
            set { _tbl54SupertribussesAllList = value; RaisePropertyChanged(); }
        }

        #endregion "Public Properties"   
       
        #region "Public Properties Tbl51Infrafamily"

        private string _searchInfrafamilyName;
        public string SearchInfrafamilyName
        {
            get  _searchInfrafamilyName; 
            set { _searchInfrafamilyName = value; RaisePropertyChanged(); }
        }

        public ICollectionView InfrafamiliesView;
        public Tbl51Infrafamily CurrentTbl51Infrafamily => InfrafamiliesView?.CurrentItem as Tbl51Infrafamily;           

        private ObservableCollection<Tbl51Infrafamily> _tbl51InfrafamiliesList;
        public ObservableCollection<Tbl51Infrafamily> Tbl51InfrafamiliesList
        {
            get => _tbl51InfrafamiliesList; 
            set { _tbl51InfrafamiliesList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl51Infrafamily> _tbl51InfrafamiliesAllList;
        public ObservableCollection<Tbl51Infrafamily> Tbl51InfrafamiliesAllList
        {
            get => _tbl51InfrafamiliesAllList; 
            set { _tbl51InfrafamiliesAllList = value; RaisePropertyChanged(); }       
        }

        #endregion "Public Properties"
   
  
       
        #region "Public Properties Tbl57Tribus"

        private string _searchTribusName;
        public string SearchTribusName
        {
            get => _searchTribusName; 
            set { _searchTribusName = value; RaisePropertyChanged(); }
        }

        public ICollectionView TribussesView;
        public Tbl57Tribus CurrentTbl57Tribus => TribussesView?.CurrentItem as Tbl57Tribus;           

        private ObservableCollection<Tbl57Tribus> _tbl57TribussesList;
        public ObservableCollection<Tbl57Tribus> Tbl57TribussesList
        {
            get => _tbl57TribussesList; 
            set { _tbl57TribussesList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl57Tribus> _tbl57TribussesAllList;
        public ObservableCollection<Tbl57Tribus> Tbl57TribussesAllList
        {
            get => _tbl57TribussesAllList; 
            set { _tbl57TribussesAllList = value; RaisePropertyChanged(); }       
        }

        #endregion "Public Properties"
   
  
       
        #region "Public Properties Tbl48Subfamily"

        private ObservableCollection<Tbl48Subfamily> _tbl48SubfamiliesAllList;
        public  ObservableCollection<Tbl48Subfamily> Tbl48SubfamiliesAllList
        {
            get => _tbl48SubfamiliesAllList; 
            set { _tbl48SubfamiliesAllList = value; RaisePropertyChanged(); }
        }

        #endregion "Public Properties"
   
   

 //    Part 12    

 

   }
}   
