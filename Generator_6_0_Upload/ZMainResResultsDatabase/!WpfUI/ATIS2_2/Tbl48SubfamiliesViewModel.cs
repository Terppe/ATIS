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

    
         //    Tbl48SubfamiliesViewModel Skriptdatum:  23.12.2017  10:32    

namespace WPFUI.Views.Database
{     
    
    public partial class Tbl48SubfamiliesViewModel : Tbl03RegnumsViewModel
    {     
         
        #region "Private Data Members"

        private readonly AllListVm _allListVm = new AllListVm();
        private readonly Repository<Tbl48Subfamily, int> _tbl48SubfamiliesRepository = new Repository<Tbl48Subfamily, int>();   
         
        private readonly Repository<Tbl45Family, int> _tbl45FamiliesRepository = new Repository<Tbl45Family, int>();   
           
        private readonly Repository<Tbl51Infrafamily, int> _tbl51InfrafamiliesRepository = new Repository<Tbl51Infrafamily, int>();   
           
        private readonly Repository<Tbl90Reference, int> _tbl90ReferencesRepository = new Repository<Tbl90Reference, int>();
        private readonly Repository<Tbl93Comment, int> _tbl93CommentsRepository = new Repository<Tbl93Comment, int>();    

        #endregion "Private Data Members"               
      
        #region "Constructor"

        public Tbl48SubfamiliesViewModel()
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

             
        #region "Public Commands Basic Tbl48Subfamily"

        private RelayCommand _getSubfamilyByNameOrIdCommand;    
    
        public ICommand GetSubfamilyByNameOrIdCommand    
    
        {
            get { return _getSubfamilyByNameOrIdCommand ?? (_getSubfamilyByNameOrIdCommand = new RelayCommand(delegate { GetSubfamilyByNameOrId(null); })); }   
        }

        private void GetSubfamilyByNameOrId(object o)       
        {   
    
            int id;
            if (int.TryParse(SearchSubfamilyName, out id))
                Tbl48SubfamiliesList = new ObservableCollection<Tbl48Subfamily> { _tbl48SubfamiliesRepository.Get(id) };
            else           
                Tbl48SubfamiliesList = _allListVm.GetValueTbl48SubfamiliesList(SearchSubfamilyName);      
Tbl45FamiliesAllList?.Clear();
            Tbl45FamiliesAllList = _allListVm.GetValueTbl45FamiliesAllList();  
SubfamiliesView = CollectionViewSource.GetDefaultView(Tbl48SubfamiliesList);
            SubfamiliesView.Refresh();
        }
        //------------------------------------------------------------------------------------                
           
        private RelayCommand _addSubfamilyCommand;           
    
        public ICommand AddSubfamilyCommand       
    
        {
            get { return _addSubfamilyCommand ?? (_addSubfamilyCommand = new RelayCommand(delegate { AddSubfamily(null); })); }
        }

        private void AddSubfamily(object o)
        {
            Tbl48SubfamiliesList = new ObservableCollection<Tbl48Subfamily>();   
Tbl48SubfamiliesList.Insert(0, new Tbl48Subfamily{ SubfamilyName= CultRes.StringsRes.DatasetNew });  

            Tbl45FamiliesAllList?.Clear();
            Tbl45FamiliesAllList = _allListVm.GetValueTbl45FamiliesAllList();             
SubfamiliesView = CollectionViewSource.GetDefaultView(Tbl48SubfamiliesList);
            SubfamiliesView.Refresh();
        }
        //------------------------------------------------------------------------------------                
           
        private RelayCommand _copySubfamilyCommand;              
    
        public ICommand CopySubfamilyCommand             
           
        {
            get { return _copySubfamilyCommand ?? (_copySubfamilyCommand = new RelayCommand(delegate { CopySubfamily(null); })); }
        }

        private void CopySubfamily(object o)
        {
            Tbl48SubfamiliesList = new ObservableCollection<Tbl48Subfamily>();

            var subfamily = _tbl48SubfamiliesRepository.Get(CurrentTbl48Subfamily.SubfamilyID);

            Tbl48SubfamiliesList.Insert(0, new Tbl48Subfamily
            {    
FamilyID = subfamily.FamilyID,   
SubfamilyName = CultRes.StringsRes.DatasetNew,
                Valid = subfamily.Valid,
                ValidYear = subfamily.ValidYear,
                Synonym = subfamily.Synonym,
                Author = subfamily.Author,
                AuthorYear = subfamily.AuthorYear,
                Info = subfamily.Info,
                EngName = subfamily.EngName,
                GerName = subfamily.GerName,
                FraName = subfamily.FraName,
                PorName = subfamily.PorName,
                Memo = subfamily.Memo
              
          
            });

            SubfamiliesView = CollectionViewSource.GetDefaultView(Tbl48SubfamiliesList);
            SubfamiliesView.Refresh();
        }
        //---------------------------------------------------------------------------------------                  
           
        private RelayCommand _deleteSubfamilyCommand;              
    
        public ICommand DeleteSubfamilyCommand             
             
        {
            get { return _deleteSubfamilyCommand ?? (_deleteSubfamilyCommand = new RelayCommand(delegate { DeleteSubfamily(null); })); }
        }

        private void DeleteSubfamily(object o)
        {
            try
            {
                var subfamily = _tbl48SubfamiliesRepository.Get(CurrentTbl48Subfamily.SubfamilyID);
                if (subfamily != null)
                {   
          
                    if (WpfMessageBox.Show(CultRes.StringsRes.DeleteQuestion1, CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl48Subfamily.SubfamilyName,
                         MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
                        return;    
_tbl48SubfamiliesRepository.Delete(subfamily);
                    _tbl48SubfamiliesRepository.Save();     
          
                    WpfMessageBox.Show(CultRes.StringsRes.DeleteSuccess, CurrentTbl48Subfamily.SubfamilyName,
                        MessageBoxButton.OK, MessageBoxImage.Information);   
             
                    if (SearchSubfamilyName == null)
                        GetConnectedTablesById(o); //refresh doubleClick                                       
                    else
                    {
                        Tbl48SubfamiliesList = _allListVm.GetValueTbl48SubfamiliesList(SearchSubfamilyName);   
             
                    }
                    SubfamiliesView = CollectionViewSource.GetDefaultView(Tbl48SubfamiliesList);
                    SubfamiliesView.Refresh();
                }
                else
                {    
             
                    WpfMessageBox.Show(CultRes.StringsRes.Information, CultRes.StringsRes.DeleteCan + " " + CurrentTbl48Subfamily.SubfamilyName+ " " + CultRes.StringsRes.DeleteCan1,
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
           
        private RelayCommand _saveSubfamilyCommand;              
     
        public ICommand SaveSubfamilyCommand             
           
        {
            get { return _saveSubfamilyCommand ?? (_saveSubfamilyCommand = new RelayCommand(delegate { SaveSubfamily(null); })); }
        }

        private void SaveSubfamily(object o)
        {
            try
            {
                var subfamily = _tbl48SubfamiliesRepository.Get(CurrentTbl48Subfamily.SubfamilyID);
                if (CurrentTbl48Subfamily == null)
                    WpfMessageBox.Show(CultRes.StringsRes.Warning, CultRes.StringsRes.DatasetNotExist,
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                else
                {
                    if (CurrentTbl48Subfamily.SubfamilyID != 0)   
                    {
                        if (subfamily != null) //update
                        {  
subfamily.SubfamilyName = CurrentTbl48Subfamily.SubfamilyName;
                            subfamily.FamilyID = CurrentTbl48Subfamily.FamilyID;           
subfamily.Valid = CurrentTbl48Subfamily.Valid;
                            subfamily.ValidYear = CurrentTbl48Subfamily.ValidYear;
                            subfamily.Synonym = CurrentTbl48Subfamily.Synonym;
                            subfamily.Author = CurrentTbl48Subfamily.Author;
                            subfamily.AuthorYear = CurrentTbl48Subfamily.AuthorYear;
                            subfamily.Info = CurrentTbl48Subfamily.Info;
                            subfamily.EngName = CurrentTbl48Subfamily.EngName;
                            subfamily.GerName = CurrentTbl48Subfamily.GerName;
                            subfamily.FraName = CurrentTbl48Subfamily.FraName;
                            subfamily.PorName = CurrentTbl48Subfamily.PorName;
                            subfamily.Updater = Environment.UserName;
                            subfamily.UpdaterDate = DateTime.Now;
                            subfamily.Memo = CurrentTbl48Subfamily.Memo;     
         
                        }
                    }
                    else
                    {
                        _tbl48SubfamiliesRepository.Add(new Tbl48Subfamily     //add new
                        {   
SubfamilyName = CurrentTbl48Subfamily.SubfamilyName,
                            FamilyID = CurrentTbl48Subfamily.FamilyID,    
          
                            CountID = RandomHelper.Randomnumber(),
                            Valid = CurrentTbl48Subfamily.Valid,
                            ValidYear = CurrentTbl48Subfamily.ValidYear,
                            Synonym = CurrentTbl48Subfamily.Synonym,
                            Author = CurrentTbl48Subfamily.Author,
                            AuthorYear = CurrentTbl48Subfamily.AuthorYear,
                            Info = CurrentTbl48Subfamily.Info,
                            EngName = CurrentTbl48Subfamily.EngName,
                            GerName = CurrentTbl48Subfamily.GerName,
                            FraName = CurrentTbl48Subfamily.FraName,
                            PorName = CurrentTbl48Subfamily.PorName,
                            Writer = Environment.UserName,
                            WriterDate = DateTime.Now,
                            Updater = Environment.UserName,
                            UpdaterDate = DateTime.Now,
                            Memo = CurrentTbl48Subfamily.Memo    
        
                        });
                    }
                    {    
         
                        //FamilyID may be not 0
                        if (CurrentTbl48Subfamily.FamilyID == 0)   
          
                        {
                            WpfMessageBox.Show(CultRes.StringsRes.RequiredGenealogyConnect, CultRes.StringsRes.RequiredInput,
                                MessageBoxButton.OK, MessageBoxImage.Information);
                            return;
                        }   
          
                        //check if dataset with Name and vb-name already exist       
                        var dataset = new ObservableCollection<Tbl48Subfamily>
                        (from a in _tbl48SubfamiliesRepository.GetAll()
                         where
                         a.SubfamilyName.Trim() == CurrentTbl48Subfamily.SubfamilyName.Trim() &&   
          
                         a.FamilyID == CurrentTbl48Subfamily.FamilyID         
                         select a);

                        if (dataset.Count != 0 && CurrentTbl48Subfamily.SubfamilyID == 0)  //dataset exist
                        {       
         
                            WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTbl48Subfamily.SubfamilyName,
                            MessageBoxButton.OK, MessageBoxImage.Information);     
             
                        }
                        if (dataset.Count == 0 && CurrentTbl48Subfamily.SubfamilyID == 0 ||
                            dataset.Count != 0 && CurrentTbl48Subfamily.SubfamilyID != 0 ||
                            dataset.Count == 0 && CurrentTbl48Subfamily.SubfamilyID != 0) //new dataset and update
                        {    
             
                            if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTbl48Subfamily.SubfamilyName,
                                    MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
                                return;  
               
                            {
                                _tbl48SubfamiliesRepository.Save();     
             
                                WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess, CurrentTbl48Subfamily.SubfamilyName,
                                    MessageBoxButton.OK, MessageBoxImage.Information);      
                   
                            }
                        }

                        if (SearchSubfamilyName == null && CurrentTbl48Subfamily.SubfamilyID == 0)  //new Dataset                        
                            Tbl48SubfamiliesList = _allListVm.GetValueTbl48SubfamiliesList();  //last Dataset
                        if (SearchSubfamilyName == null && CurrentTbl48Subfamily.SubfamilyID != 0)   //update 
                            Tbl48SubfamiliesList = _allListVm.GetValueTbl48SubfamiliesList(CurrentTbl48Subfamily.SubfamilyID);
                        if (SearchSubfamilyName != null && CurrentTbl48Subfamily.SubfamilyID == 0)  //new Dataset                        
                            Tbl48SubfamiliesList = _allListVm.GetValueTbl48SubfamiliesList();  //last Dataset
                        if (SearchSubfamilyName != null && CurrentTbl48Subfamily.SubfamilyID != 0)   //update 
                            Tbl48SubfamiliesList = _allListVm.GetValueTbl48SubfamiliesList(CurrentTbl48Subfamily.SubfamilyID);     
SubfamiliesView = CollectionViewSource.GetDefaultView(Tbl48SubfamiliesList);
                        SubfamiliesView.Refresh();

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

           
        #region "Public Commands Connect <== Tbl45Family"                 

        private RelayCommand _getFamilyByNameOrIdCommand;     
    
        public ICommand GetFamilyByNameOrIdCommand    
           
        {
            get { return _getFamilyByNameOrIdCommand ?? (_getFamilyByNameOrIdCommand = new RelayCommand(delegate { GetFamilyByNameOrId(null); })); }
        }

        private void GetFamilyByNameOrId(object o)    
        {

            int id;
            if (int.TryParse(SearchFamilyName, out id))
                Tbl45FamiliesList = new ObservableCollection<Tbl45Family> { _tbl45FamiliesRepository.Get(id) };
            else
                Tbl45FamiliesList = _allListVm.GetValueTbl45FamiliesList(SearchFamilyName);

            FamiliesView = CollectionViewSource.GetDefaultView(Tbl45FamiliesList);
            FamiliesView.Refresh();
        }

        //------------------------------------------------------------------------------------                
           
        private RelayCommand _addFamilyCommand;      
    
        public ICommand AddFamilyCommand    
    
        {
            get { return _addFamilyCommand ?? (_addFamilyCommand = new RelayCommand(delegate { AddFamily(null); })); }
        }

        private void AddFamily(object o)
        {
            Tbl45FamiliesList = new ObservableCollection<Tbl45Family>();   
Tbl45FamiliesList.Insert(0, new Tbl45Family{ FamilyName = CultRes.StringsRes.DatasetNew });   

            Tbl42SuperfamiliesAllList?.Clear();
            Tbl42SuperfamiliesAllList = _allListVm.GetValueTbl42SuperfamiliesAllList();    
FamiliesView = CollectionViewSource.GetDefaultView(Tbl45FamiliesList);
            FamiliesView.Refresh();
        }
        //------------------------------------------------------------------------------------                
           
        private RelayCommand _copyFamilyCommand;            
    
        public ICommand CopyFamilyCommand          
         
        {
            get { return _copyFamilyCommand ?? (_copyFamilyCommand = new RelayCommand(delegate { CopyFamily(null); })); }
        }

        private void CopyFamily(object o)
        {
            Tbl45FamiliesList = new ObservableCollection<Tbl45Family>();

            var family = _tbl45FamiliesRepository.Get(CurrentTbl45Family.FamilyID);

            Tbl45FamiliesList.Insert(0, new Tbl45Family
            {                 
SuperfamilyID = family.SuperfamilyID,
                FamilyName = CultRes.StringsRes.DatasetNew,
                Valid = family.Valid,
                ValidYear = family.ValidYear,
                Synonym = family.Synonym,
                Author = family.Author,
                AuthorYear = family.AuthorYear,
                Info = family.Info,
                EngName = family.EngName,
                GerName = family.GerName,
                FraName = family.FraName,
                PorName = family.PorName,
                Memo = family.Memo         
        
            });

            FamiliesView = CollectionViewSource.GetDefaultView(Tbl45FamiliesList);
            FamiliesView.Refresh();
        }
        //---------------------------------------------------------------------------------------                  
           
        private RelayCommand _deleteFamilyCommand;              
    
        public ICommand DeleteFamilyCommand             
         
        {
            get { return _deleteFamilyCommand ?? (_deleteFamilyCommand = new RelayCommand(delegate { DeleteFamily(null); })); }
        }

        private void DeleteFamily(object o)
        {
            try
            {
                var family = _tbl45FamiliesRepository.Get(CurrentTbl45Family.FamilyID);
                if (family!= null)
                {  
         
                    if (WpfMessageBox.Show(CultRes.StringsRes.DeleteQuestion1, CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl45Family.FamilyName,
                         MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes) 
                    return;

                    _tbl45FamiliesRepository.Delete(family);
                    _tbl45FamiliesRepository.Save();

                    WpfMessageBox.Show(CultRes.StringsRes.DeleteSuccess, CurrentTbl45Family.FamilyName, 
                        MessageBoxButton.OK, MessageBoxImage.Information);  
         
                        if (SearchFamilyName == null)                       
                            GetConnectedTablesById(o); //refresh doubleClick                                          
                        else
                        {
                        Tbl45FamiliesList = _allListVm.GetValueTbl45FamiliesList(SearchFamilyName);  
    
                    }
                            FamiliesView = CollectionViewSource.GetDefaultView(Tbl45FamiliesList);
                            FamiliesView.Refresh();
                }
                else
                {   
    
                    WpfMessageBox.Show(CultRes.StringsRes.Information, CultRes.StringsRes.DeleteCan + " " + CurrentTbl45Family.FamilyName+ " " + CultRes.StringsRes.DeleteCan1,
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
           
        private RelayCommand _saveFamilyCommand;              
    
        public ICommand SaveFamilyCommand             
         
        {
            get { return _saveFamilyCommand ?? (_saveFamilyCommand = new RelayCommand(delegate { SaveFamily(null); })); }
        }

        private void SaveFamily(object o)
        {
            try
            {
                var family = _tbl45FamiliesRepository.Get(CurrentTbl45Family.FamilyID);
                if (CurrentTbl45Family == null)              
                    WpfMessageBox.Show(CultRes.StringsRes.Warning, CultRes.StringsRes.DatasetNotExist, 
                       MessageBoxButton.OK, MessageBoxImage.Warning);               
                else
                {
                    if (CurrentTbl45Family.FamilyID!= 0)
                    {
                        if (family!= null) //update
                        {   
family.FamilyName= CurrentTbl45Family.FamilyName; 
                            family.SuperfamilyID = CurrentTbl45Family.SuperfamilyID;              
family.Valid = CurrentTbl45Family.Valid;
                            family.ValidYear = CurrentTbl45Family.ValidYear;
                            family.Synonym = CurrentTbl45Family.Synonym;
                            family.Author = CurrentTbl45Family.Author;
                            family.AuthorYear = CurrentTbl45Family.AuthorYear;
                            family.Info = CurrentTbl45Family.Info;
                            family.EngName = CurrentTbl45Family.EngName;
                            family.GerName = CurrentTbl45Family.GerName;
                            family.FraName = CurrentTbl45Family.FraName;
                            family.PorName = CurrentTbl45Family.PorName;
                            family.Updater = Environment.UserName;
                            family.UpdaterDate = DateTime.Now; 
                            family.Memo = CurrentTbl45Family.Memo;   
         
                        }
                    }
                    else
                    {
                        _tbl45FamiliesRepository.Add(new Tbl45Family     //add new
                        {   
FamilyName= CurrentTbl45Family.FamilyName,              
                            SuperfamilyID = CurrentTbl45Family.SuperfamilyID,     
                            CountID = RandomHelper.Randomnumber(),
                            Valid = CurrentTbl45Family.Valid,
                            ValidYear = CurrentTbl45Family.ValidYear,
                            Synonym = CurrentTbl45Family.Synonym,
                            Author = CurrentTbl45Family.Author,
                            AuthorYear = CurrentTbl45Family.AuthorYear,
                            Info = CurrentTbl45Family.Info,
                            EngName = CurrentTbl45Family.EngName,
                            GerName = CurrentTbl45Family.GerName,
                            FraName = CurrentTbl45Family.FraName,
                            PorName = CurrentTbl45Family.PorName,
                            Writer = Environment.UserName,
                            WriterDate = DateTime.Now,
                            Updater = Environment.UserName,
                            UpdaterDate = DateTime.Now,
                            Memo = CurrentTbl45Family.Memo   
          
                        });
                    }
                    {
                        //SuperfamilyID may be not 0
                        if (CurrentTbl45Family.SuperfamilyID == 0)
                        {
                            WpfMessageBox.Show(CultRes.StringsRes.RequiredGenealogyConnect, CultRes.StringsRes.RequiredInput,
                                MessageBoxButton.OK, MessageBoxImage.Information);
                            return;
                        }     
           
                        //check if dataset with Name and vb-name already exist       
                        var dataset = new ObservableCollection<Tbl45Family>
                        (from a in _tbl45FamiliesRepository.GetAll()
                         where
                         a.FamilyName.Trim() == CurrentTbl45Family.FamilyName.Trim() &&
                         a.SuperfamilyID == CurrentTbl45Family.SuperfamilyID
                         select a);     
           
                        if (dataset.Count != 0 && CurrentTbl45Family.FamilyID == 0)  //dataset exist
                        {
                            WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTbl45Family.FamilyName,
                            MessageBoxButton.OK, MessageBoxImage.Information);
                        }   
           
                        if (dataset.Count == 0 && CurrentTbl45Family.FamilyID == 0 ||
                            dataset.Count != 0 && CurrentTbl45Family.FamilyID != 0 ||
                            dataset.Count == 0 && CurrentTbl45Family.FamilyID != 0) //new dataset and update
                        {  
           
                            if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTbl45Family.FamilyName,
                                    MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
                                return;
                            {
                                _tbl45FamiliesRepository.Save();

                                WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess, CurrentTbl45Family.FamilyName,
                                    MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                        }   
           
                        if (SearchFamilyName == null && CurrentTbl45Family.FamilyID == 0)  //new Dataset                        
                            GetConnectedTablesById(o); //refresh doubleClick                                          
                        if (SearchFamilyName == null && CurrentTbl45Family.FamilyID != 0)   //update 
                            Tbl45FamiliesList = _allListVm.GetValueTbl45FamiliesList(CurrentTbl45Family.FamilyID);
                        if (SearchFamilyName != null && CurrentTbl45Family.FamilyID == 0)  //new Dataset                        
                            GetConnectedTablesById(o); //refresh doubleClick                                          
                        if (SearchFamilyName != null && CurrentTbl45Family.FamilyID != 0)   //update 
                            Tbl45FamiliesList = _allListVm.GetValueTbl45FamiliesList(CurrentTbl45Family.FamilyID);   
FamiliesView = CollectionViewSource.GetDefaultView(Tbl45FamiliesList);
                        FamiliesView.Refresh();

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
 

 //    Part 3    

 

 //    Part 4    

           

        #region "Public Commands Connect ==> Tbl51Infrafamily"               

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

        //------------------------------------------------------------------------------------                
             
        private RelayCommand _addInfrafamilyCommand;

        public ICommand AddInfrafamilyCommand

        {
            get { return _addInfrafamilyCommand ?? (_addInfrafamilyCommand = new RelayCommand(delegate { AddInfrafamily(null); })); }
        }

        private void AddInfrafamily(object o)
        {
            Tbl51InfrafamiliesList = new ObservableCollection<Tbl51Infrafamily>();
            Tbl51InfrafamiliesList.Insert(0, new Tbl51Infrafamily{ InfrafamilyName = CultRes.StringsRes.DatasetNew });

            Tbl48SubfamiliesAllList?.Clear();
            Tbl48SubfamiliesAllList = _allListVm.GetValueTbl48SubfamiliesAllList();

            InfrafamiliesView = CollectionViewSource.GetDefaultView(Tbl51InfrafamiliesList);
            InfrafamiliesView.Refresh();
        }
        //------------------------------------------------------------------------------------                
              
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
                                                         
                    }
                            InfrafamiliesView = CollectionViewSource.GetDefaultView(Tbl51InfrafamiliesList);
                            InfrafamiliesView.Refresh();
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
                    WpfMessageBox.Show(CultRes.StringsRes.Warning, CultRes.StringsRes.DatasetNotExist,
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                else
                {
                    if (CurrentTbl51Infrafamily.InfrafamilyID != 0)   
                    {
                        if (infrafamily != null) //update
                        {   
infrafamily.InfrafamilyName = CurrentTbl51Infrafamily.InfrafamilyName;
                            infrafamily.SubfamilyID = CurrentTbl48Subfamily.SubfamilyID;   
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
                            InfrafamilyName = CurrentTbl51Infrafamily.InfrafamilyName,
                            SubfamilyID = CurrentTbl48Subfamily.SubfamilyID,
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
                        if (CurrentTbl48Subfamily.SubfamilyID == 0)
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
                         a.SubfamilyID == CurrentTbl48Subfamily.SubfamilyID
                         select a); 
        
                        if (dataset.Count != 0 && CurrentTbl51Infrafamily.InfrafamilyID == 0)  //dataset exist
                        {
                            WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTbl51Infrafamily.InfrafamilyName,
                            MessageBoxButton.OK, MessageBoxImage.Information);
                        }      
        
                        if (dataset.Count == 0 && CurrentTbl51Infrafamily.InfrafamilyID == 0 ||
                            dataset.Count != 0 && CurrentTbl51Infrafamily.InfrafamilyID != 0 ||
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
                        if (SearchInfrafamilyName == null && CurrentTbl51Infrafamily.InfrafamilyID != 0)   //update 
                            Tbl51InfrafamiliesList = _allListVm.GetValueTbl51InfrafamiliesList(CurrentTbl51Infrafamily.InfrafamilyID);
                        if (SearchInfrafamilyName != null && CurrentTbl51Infrafamily.InfrafamilyID == 0)  //new Dataset                        
                            GetConnectedTablesById(o); //refresh doubleClick                                          
                        if (SearchInfrafamilyName != null && CurrentTbl51Infrafamily.InfrafamilyID != 0)   //update 
                            Tbl51InfrafamiliesList = _allListVm.GetValueTbl51InfrafamiliesList(CurrentTbl51Infrafamily.InfrafamilyID); 
InfrafamiliesView = CollectionViewSource.GetDefaultView(Tbl51InfrafamiliesList);
                        InfrafamiliesView.Refresh();
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

           Tbl48SubfamiliesAllList?.Clear();
           Tbl48SubfamiliesAllList = _allListVm.GetValueTbl48SubfamiliesAllList();
           Tbl90AuthorsAllList?.Clear();
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
SubfamilyID = refAuthor.SubfamilyID,              
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
         
                            refAuthor.SubfamilyID = CurrentTbl48Subfamily.SubfamilyID;  
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
SubfamilyID = CurrentTbl48Subfamily.SubfamilyID,              
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
                        //SubfamilyID may be not 0
                        if (CurrentTbl48Subfamily.SubfamilyID == 0 || CurrentTbl90RefAuthor.RefAuthorID == 0)
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
                         a.SubfamilyID == CurrentTbl48Subfamily.SubfamilyID &&
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

            Tbl48SubfamiliesAllList?.Clear();  
            Tbl48SubfamiliesAllList = _allListVm.GetValueTbl48SubfamiliesAllList();
            Tbl90SourcesAllList?.Clear();
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
SubfamilyID = refSource.SubfamilyID,              
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
         
                            refSource.SubfamilyID = CurrentTbl48Subfamily.SubfamilyID;            
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
SubfamilyID = CurrentTbl48Subfamily.SubfamilyID,              
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
                        //SubfamilyID may be not 0
                        if (CurrentTbl48Subfamily.SubfamilyID == 0 || CurrentTbl90RefSource.RefSourceID == 0)
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
                         a.SubfamilyID == CurrentTbl48Subfamily.SubfamilyID &&
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
                            GetConnectedTablesById(o); //refresh doubleClick                                          
                        if (SearchRefSourceName == null && CurrentTbl90RefSource.ReferenceID != 0)   //update
                            Tbl90RefSourcesList = _allListVm.GetValueTbl90RefSourcesList(CurrentTbl90RefSource.ReferenceID);
                        if (SearchRefSourceName != null && CurrentTbl90RefSource.ReferenceID == 0)  //new Dataset
                            GetConnectedTablesById(o); //refresh doubleClick                                          
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

            Tbl48SubfamiliesAllList?.Clear();  
            Tbl48SubfamiliesAllList = _allListVm.GetValueTbl48SubfamiliesAllList();
            Tbl90ExpertsAllList?.Clear();
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
SubfamilyID = refExpert.SubfamilyID,              
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
      
                            refExpert.SubfamilyID = CurrentTbl48Subfamily.SubfamilyID;           
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
SubfamilyID = CurrentTbl48Subfamily.SubfamilyID,              
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
                        //SubfamilyID may be not 0
                        if (CurrentTbl48Subfamily.SubfamilyID == 0 || CurrentTbl90RefExpert.RefExpertID == 0)   
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
                         a.SubfamilyID == CurrentTbl48Subfamily.SubfamilyID &&
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
                            GetConnectedTablesById(o); //refresh doubleClick                                          
                        if (SearchRefExpertName == null && CurrentTbl90RefExpert.ReferenceID != 0)   //update
                            Tbl90RefExpertsList = _allListVm.GetValueTbl90RefExpertsList(CurrentTbl90RefExpert.ReferenceID);
                        if (SearchRefExpertName != null && CurrentTbl90RefExpert.ReferenceID == 0)  //new Dataset
                            GetConnectedTablesById(o); //refresh doubleClick                                          
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

            Tbl48SubfamiliesAllList?.Clear();  
            Tbl48SubfamiliesAllList = _allListVm.GetValueTbl48SubfamiliesAllList();      
    

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
SubfamilyID = comment.SubfamilyID,              
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
      
                            comment.SubfamilyID = CurrentTbl48Subfamily.SubfamilyID;            
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
SubfamilyID = CurrentTbl48Subfamily.SubfamilyID,              
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
                        //SubfamilyID may be not 0
                        if (CurrentTbl48Subfamily.SubfamilyID == 0)
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
                         a.SubfamilyID == CurrentTbl48Subfamily.SubfamilyID
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
                            GetConnectedTablesById(o); //refresh doubleClick                                          
                        if (SearchCommentInfo == null && CurrentTbl93Comment.CommentID != 0)   //update
                            Tbl93CommentsList = _allListVm.GetValueTbl93CommentsList(CurrentTbl93Comment.CommentID);
                        if (SearchCommentInfo != null && CurrentTbl93Comment.CommentID == 0)  //new Dataset
                            GetConnectedTablesById(o); //refresh doubleClick                                          
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
            Tbl48SubfamiliesAllList?.Clear();
            Tbl48SubfamiliesAllList = _allListVm.GetValueTbl48SubfamiliesAllList();

            Tbl45FamiliesList =  new ObservableCollection<Tbl45Family>
                 (from x in _tbl45FamiliesRepository.GetAll()
                 where x.FamilyID == CurrentTbl48Subfamily.FamilyID
                  orderby x.FamilyName
                  select x);

            FamiliesView = CollectionViewSource.GetDefaultView(Tbl45FamiliesList);
            FamiliesView.Refresh();

            SelectedMainTabIndex = 0;  //change to Connect tab
            SelectedDetailTabIndex = 1;  
            SelectedDetailSubTabIndex = 0;  
        }

        #endregion "Public Commands Connected Tables by DoubleClick"   
 

 //    Part 10    

    
        #region "Public Commands to open Detail TabItems"

        private int _selectedMainTabIndex;
        private int _selectedMainSubRefTabIndex;
        private int _selectedDetailTabIndex;
        private int _selectedDetailSubTabIndex;
        private int _selectedDetailSubRefTabIndex;

        public new int SelectedMainTabIndex
        {
            get => _selectedMainTabIndex; 
            set
            {
                if (value == _selectedMainTabIndex) return;
                _selectedMainTabIndex = value; 
                RaisePropertyChanged();
                if (_selectedMainTabIndex == 0)             
                    SelectedDetailSubTabIndex = 0;              
                if (_selectedMainTabIndex == 1)
                {
                    SelectedDetailTabIndex = 1;
                    SelectedDetailSubTabIndex = 1;
                }
                if (_selectedMainTabIndex == 2)
                {
                    SelectedDetailTabIndex = 1;
                    SelectedDetailSubTabIndex = 2;
                }
                if (_selectedMainTabIndex == 3)
                {
                    SelectedDetailTabIndex = 1;
                    SelectedDetailSubTabIndex = 3;
                }
            }
        }

        public new int SelectedMainSubRefTabIndex
        {
            get => _selectedMainSubRefTabIndex; 
            set
            {
                if (value == _selectedMainSubRefTabIndex) return;
                _selectedMainSubRefTabIndex = value; 
                 RaisePropertyChanged();
                if (_selectedMainSubRefTabIndex == 0)
                    SelectedDetailSubRefTabIndex = 0;
                if (_selectedMainSubRefTabIndex == 1)
                    SelectedDetailSubRefTabIndex = 1;
                if (_selectedMainSubRefTabIndex == 2)
                    SelectedDetailSubRefTabIndex = 2;
            }
        }

        public new int SelectedDetailTabIndex
        {
            get => _selectedDetailTabIndex; 
            set
            {
                if (value == _selectedDetailTabIndex) return;
                _selectedDetailTabIndex = value; 
                RaisePropertyChanged();
                if (_selectedDetailTabIndex == 0)
                {
                    SelectedDetailSubTabIndex = 0;
                    SelectedMainTabIndex = 0;
                }
                if (_selectedDetailTabIndex == 1)                
                    SelectedDetailSubTabIndex = 1;                
                if (_selectedDetailTabIndex == 2)                
                    SelectedDetailSubTabIndex = 2;               
                if (_selectedDetailTabIndex == 3)
                    SelectedDetailSubTabIndex = 3;
            }
        }

        public new int SelectedDetailSubTabIndex
        {
            get => _selectedDetailSubTabIndex;
            set
            {
                if (value == _selectedDetailSubTabIndex) return;
                _selectedDetailSubTabIndex = value;
                RaisePropertyChanged();
                if (_selectedDetailSubTabIndex == 0)
                {
                    SelectedMainTabIndex = 0;
                }
                if (_selectedDetailSubTabIndex == 1)
                {
                    Tbl48SubfamiliesAllList?.Clear();
                    Tbl48SubfamiliesAllList = _allListVm.GetValueTbl48SubfamiliesAllList();

                    Tbl51InfrafamiliesList?.Clear();
                    Tbl51InfrafamiliesList =  new ObservableCollection<Tbl51Infrafamily>
                              (from x in _tbl51InfrafamiliesRepository.GetAll()
                              where x.SubfamilyID == CurrentTbl48Subfamily.SubfamilyID
                              orderby x.InfrafamilyName
                               select x);

                    InfrafamiliesView = CollectionViewSource.GetDefaultView(Tbl51InfrafamiliesList);
                    InfrafamiliesView.Refresh();

                    SelectedMainTabIndex = 1;
                }
                if (_selectedDetailSubTabIndex == 2)
                {
                    Tbl48SubfamiliesAllList?.Clear();
                    Tbl48SubfamiliesAllList = _allListVm.GetValueTbl48SubfamiliesAllList();
                    Tbl90ExpertsAllList?.Clear();
                    Tbl90ExpertsAllList = _allListVm.GetValueTbl90ExpertsAllList();

                    Tbl90RefExpertsList?.Clear();
                    Tbl90RefExpertsList = new ObservableCollection<Tbl90Reference>
                    (from refEx in _tbl90ReferencesRepository.GetAll()
                     where refEx.SubfamilyID == CurrentTbl48Subfamily.SubfamilyID
                           && refEx.RefAuthorID == null
                           && refEx.RefSourceID == null
                     orderby refEx.Tbl90RefExperts.RefExpertName
                     select refEx);

                    RefExpertsView = CollectionViewSource.GetDefaultView(Tbl90RefExpertsList);
                    RefExpertsView.Refresh();

                    SelectedMainTabIndex = 2;
                }
                if (_selectedDetailSubTabIndex == 3)
                {
                    Tbl48SubfamiliesAllList?.Clear();
                    Tbl48SubfamiliesAllList = _allListVm.GetValueTbl48SubfamiliesAllList();

                    Tbl93CommentsList?.Clear();
                    Tbl93CommentsList = new ObservableCollection<Tbl93Comment>
                    (from comm in _tbl93CommentsRepository.GetAll()
                     where comm.SubfamilyID == CurrentTbl48Subfamily.SubfamilyID
                     orderby comm.Info
                     select comm);

                    CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
                    CommentsView.Refresh();

                    SelectedMainTabIndex = 3;
                }
            }
        }

        public new int SelectedDetailSubRefTabIndex
        {
            get => _selectedDetailSubRefTabIndex;
            set
            {
                if (value == _selectedDetailSubRefTabIndex) return;
                _selectedDetailSubRefTabIndex = value;
                RaisePropertyChanged();
                if (_selectedDetailSubRefTabIndex == 0)
                {
                    Tbl90ExpertsAllList?.Clear();
                    Tbl90ExpertsAllList = _allListVm.GetValueTbl90ExpertsAllList();
                    Tbl48SubfamiliesAllList?.Clear();
                    Tbl48SubfamiliesAllList = _allListVm.GetValueTbl48SubfamiliesAllList();

                    Tbl90RefExpertsList?.Clear();
                    Tbl90RefExpertsList = new ObservableCollection<Tbl90Reference>
                    (from refEx in _tbl90ReferencesRepository.GetAll()
                     where refEx.SubfamilyID == CurrentTbl48Subfamily.SubfamilyID
                           && refEx.RefAuthorID == null
                           && refEx.RefSourceID == null
                     orderby refEx.Tbl90RefExperts.RefExpertName
                     select refEx);

                    RefExpertsView = CollectionViewSource.GetDefaultView(Tbl90RefExpertsList);
                    RefExpertsView.Refresh();

                    SelectedMainSubRefTabIndex = 0;
                }
                if (_selectedDetailSubRefTabIndex == 1)
                {
                    Tbl90SourcesAllList?.Clear();
                    Tbl90SourcesAllList = _allListVm.GetValueTbl90SourcesAllList();
                    Tbl48SubfamiliesAllList?.Clear();
                    Tbl48SubfamiliesAllList = _allListVm.GetValueTbl48SubfamiliesAllList();

                    Tbl90RefSourcesList?.Clear();
                    Tbl90RefSourcesList = new ObservableCollection<Tbl90Reference>
                    (from refSo in _tbl90ReferencesRepository.GetAll()
                     where refSo.SubfamilyID == CurrentTbl48Subfamily.SubfamilyID
                           && refSo.RefExpertID == null
                           && refSo.RefAuthorID == null
                     orderby refSo.Tbl90RefSources.RefSourceName
                     select refSo);

                    RefSourcesView = CollectionViewSource.GetDefaultView(Tbl90RefSourcesList);
                    RefSourcesView.Refresh();

                    SelectedMainSubRefTabIndex = 1;
                }
                if (_selectedDetailSubRefTabIndex == 2)
                {
                    Tbl90AuthorsAllList?.Clear();
                    Tbl90AuthorsAllList = _allListVm.GetValueTbl90AuthorsAllList();
                    Tbl48SubfamiliesAllList?.Clear();
                    Tbl48SubfamiliesAllList = _allListVm.GetValueTbl48SubfamiliesAllList();

                    Tbl90RefAuthorsList?.Clear();
                    Tbl90RefAuthorsList = new ObservableCollection<Tbl90Reference>
                    (from refAu in _tbl90ReferencesRepository.GetAll()
                     where refAu.SubfamilyID == CurrentTbl48Subfamily.SubfamilyID
                           && refAu.RefExpertID == null
                           && refAu.RefSourceID == null
                     orderby refAu.Tbl90RefAuthors.RefAuthorName, refAu.Tbl90RefAuthors.BookName, refAu
                         .Tbl90RefAuthors.Page1
                     select refAu);

                    RefAuthorsView = CollectionViewSource.GetDefaultView(Tbl90RefAuthorsList);
                    RefAuthorsView.Refresh();

                    SelectedMainSubRefTabIndex = 2;
                }
            }
        }

        #endregion "Public Commands to open Detail TabItems"
 




   }
}   
