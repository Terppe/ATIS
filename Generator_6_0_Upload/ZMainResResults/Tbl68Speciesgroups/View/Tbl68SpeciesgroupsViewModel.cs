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

    
         //    Tbl68SpeciesgroupsViewModel Skriptdatum:  09.11.2018  10:32    

namespace WPFUI.Views.Database
{     
    
    public class Tbl68SpeciesgroupsViewModel : Tbl03RegnumsViewModel
    {     
        
        #region "Private Data Members"
 
        private readonly AllListVm _allListVm = new AllListVm();
        private readonly Repository<Tbl66Genus, int> _tbl66GenussesRepository = new Repository<Tbl66Genus, int>();
        private readonly Repository<Tbl68Speciesgroup, int> _tbl68SpeciesgroupsRepository = new Repository<Tbl68Speciesgroup, int>();  
           
        private readonly Repository<Tbl69FiSpecies, int> _tbl69FiSpeciessesRepository = new Repository<Tbl69FiSpecies, int>();   
           
        private readonly Repository<Tbl72PlSpecies, int> _tbl72PlSpeciessesRepository = new Repository<Tbl72PlSpecies, int>();   
      

        #endregion "Private Data Members"            
    
        #region "Constructor"

        public Tbl68SpeciesgroupsViewModel()
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

           
        #region "Public Commands Basic Tbl68Speciesgroup"

        private RelayCommand _getSpeciesgroupByNameOrIdCommand;     
    
        public ICommand GetSpeciesgroupByNameOrIdCommand    
    
        {
            get { return _getSpeciesgroupByNameOrIdCommand ?? (_getSpeciesgroupByNameOrIdCommand = new RelayCommand(delegate { GetSpeciesgroupByNameOrId(null); })); }   
        }

        private void GetSpeciesgroupByNameOrId(object o)       
        {   
    
            int id;
            if (int.TryParse(SearchSpeciesgroupName, out id))
                Tbl68SpeciesgroupsList = new ObservableCollection<Tbl68Speciesgroup> { _tbl68SpeciesgroupsRepository.Get(id) };
            else           
                Tbl68SpeciesgroupsList = _allListVm.GetValueTbl68SpeciesgroupsList(SearchSpeciesgroupName);      
Tbl66GenussesAllList = _allListVm.GetValueTbl66GenussesAllList();      
       
            Tbl66GenussesAllList = _allListVm.GetValueTbl66GenussesAllList();      
SpeciesgroupsView = CollectionViewSource.GetDefaultView(Tbl68SpeciesgroupsList);
            SpeciesgroupsView.Refresh();
        }
        //------------------------------------------------------------------------------                
           
        private RelayCommand _addSpeciesgroupCommand;           
    
        public ICommand AddSpeciesgroupCommand       
    
        {
            get { return _addSpeciesgroupCommand ?? (_addSpeciesgroupCommand = new RelayCommand(delegate { AddSpeciesgroup(null); })); }
        }

        private void AddSpeciesgroup(object o)
        {
            Tbl68SpeciesgroupsList = new ObservableCollection<Tbl68Speciesgroup>();   
Tbl68SpeciesgroupsList.Insert(0, new Tbl68Speciesgroup{ SpeciesgroupName= CultRes.StringsRes.DatasetNew });  
          
            Tbl66GenussesAllList = _allListVm.GetValueTbl66GenussesAllList();      
SpeciesgroupsView = CollectionViewSource.GetDefaultView(Tbl68SpeciesgroupsList);
            SpeciesgroupsView.Refresh();
        }
        //---------------------------------------------------------------------------------------  
           
        private RelayCommand _copySpeciesgroupCommand;              
    
        public ICommand CopySpeciesgroupCommand             
         
        {
            get { return _copySpeciesgroupCommand ?? (_copySpeciesgroupCommand = new RelayCommand(delegate { CopySpeciesgroup(null); })); }
        }

        private void CopySpeciesgroup(object o)
        {
            Tbl68SpeciesgroupsList = new ObservableCollection<Tbl68Speciesgroup>();

            var speciesgroup = _tbl68SpeciesgroupsRepository.Get(CurrentTbl68Speciesgroup.SpeciesgroupID);

            Tbl68SpeciesgroupsList.Insert(0, new Tbl68Speciesgroup
            {                 
SpeciesgroupName = CultRes.StringsRes.DatasetNew,              
                            Subspeciesgroup = CultRes.StringsRes.DatasetNew,              
                            Valid = speciesgroup.Valid,
                            ValidYear = speciesgroup.ValidYear,
                            Synonym = speciesgroup.Synonym,
                            Author = speciesgroup.Author,
                            AuthorYear = speciesgroup.AuthorYear,
                            Info = speciesgroup.Info,
                            EngName = speciesgroup.EngName,
                            GerName = speciesgroup.GerName,
                            FraName = speciesgroup.FraName,
                            PorName = speciesgroup.PorName,
                            Memo = speciesgroup.Memo                    
        
            });

            SpeciesgroupsView = CollectionViewSource.GetDefaultView(Tbl68SpeciesgroupsList);
            SpeciesgroupsView.Refresh();
        }
        //---------------------------------------------------------------------------------------                  
           
        private RelayCommand _deleteSpeciesgroupCommand;              
    
        public ICommand DeleteSpeciesgroupCommand             
         
        {
            get { return _deleteSpeciesgroupCommand ?? (_deleteSpeciesgroupCommand = new RelayCommand(delegate { DeleteSpeciesgroup(null); })); }
        }

        private void DeleteSpeciesgroup(object o)
        {
            try
            {
                var speciesgroup = _tbl68SpeciesgroupsRepository.Get(CurrentTbl68Speciesgroup.SpeciesgroupID);
                if (speciesgroup!= null)
                {
                    if (WpfMessageBox.Show(CultRes.StringsRes.DeleteQuestion1, CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl68Speciesgroup.SpeciesgroupName,
                         MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes) 
                    return;
                    _tbl68SpeciesgroupsRepository.Delete(speciesgroup);
                    _tbl68SpeciesgroupsRepository.Save();

                    WpfMessageBox.Show(CultRes.StringsRes.DeleteSuccess, CurrentTbl68Speciesgroup.SpeciesgroupName, 
                        MessageBoxButton.OK, MessageBoxImage.Information);

                    if (SearchSpeciesgroupName == null)                   
                        GetConnectedTablesById(o); //refresh doubleClick                                       
                    else
                    {
                        Tbl68SpeciesgroupsList = _allListVm.GetValueTbl68SpeciesgroupsList(SearchSpeciesgroupName); 
                    }    
SpeciesgroupsView = CollectionViewSource.GetDefaultView(Tbl68SpeciesgroupsList);
                         SpeciesgroupsView.Refresh();
                }
                else
                {
                    WpfMessageBox.Show(CultRes.StringsRes.Information, CultRes.StringsRes.DeleteCan + " " + CurrentTbl68Speciesgroup.SpeciesgroupName+ " " + CultRes.StringsRes.DeleteCan1,
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
           
        private RelayCommand _saveSpeciesgroupCommand;              
     
        public ICommand SaveSpeciesgroupCommand             
         
        {
            get { return _saveSpeciesgroupCommand ?? (_saveSpeciesgroupCommand = new RelayCommand(delegate { SaveSpeciesgroup(null); })); }
        }

        private void SaveSpeciesgroup(object o)
        {
            try
            {
                var speciesgroup = _tbl68SpeciesgroupsRepository.Get(CurrentTbl68Speciesgroup.SpeciesgroupID);
                if (CurrentTbl68Speciesgroup == null)              
                    WpfMessageBox.Show(CultRes.StringsRes.Warning, CultRes.StringsRes.DatasetNotExist, 
                        MessageBoxButton.OK, MessageBoxImage.Warning);               
                else
                {
                    if (CurrentTbl68Speciesgroup.SpeciesgroupID!= 0)
                    {
                        if (speciesgroup!= null) //update
                        {   
speciesgroup.SpeciesgroupName = CurrentTbl68Speciesgroup.SpeciesgroupName;
                            speciesgroup.Subspeciesgroup = CurrentTbl68Speciesgroup.Subspeciesgroup ;                        
speciesgroup.Subspeciesgroup = CurrentTbl68Speciesgroup.Subspeciesgroup ;
                            speciesgroup.Valid = CurrentTbl68Speciesgroup.Valid;
                            speciesgroup.ValidYear = CurrentTbl68Speciesgroup.ValidYear;
                            speciesgroup.Synonym = CurrentTbl68Speciesgroup.Synonym;
                            speciesgroup.Author = CurrentTbl68Speciesgroup.Author;
                            speciesgroup.AuthorYear = CurrentTbl68Speciesgroup.AuthorYear;
                            speciesgroup.Info = CurrentTbl68Speciesgroup.Info;
                            speciesgroup.EngName = CurrentTbl68Speciesgroup.EngName;
                            speciesgroup.GerName = CurrentTbl68Speciesgroup.GerName;
                            speciesgroup.FraName = CurrentTbl68Speciesgroup.FraName;
                            speciesgroup.PorName = CurrentTbl68Speciesgroup.PorName;
                            speciesgroup.Updater = Environment.UserName;
                            speciesgroup.UpdaterDate = DateTime.Now; 
                            speciesgroup.Memo = CurrentTbl68Speciesgroup.Memo;  
         
                        }
                    }
                    else
                    {
                        _tbl68SpeciesgroupsRepository.Add(new Tbl68Speciesgroup     //add new
                        {   
SpeciesgroupName = CurrentTbl68Speciesgroup.SpeciesgroupName,              
                            Subspeciesgroup = CurrentTbl68Speciesgroup.Subspeciesgroup ,              
                            CountID = RandomHelper.Randomnumber(),
                            Valid = CurrentTbl68Speciesgroup.Valid,
                            ValidYear = CurrentTbl68Speciesgroup.ValidYear,
                            Synonym = CurrentTbl68Speciesgroup.Synonym,
                            Author = CurrentTbl68Speciesgroup.Author,
                            AuthorYear = CurrentTbl68Speciesgroup.AuthorYear,
                            Info = CurrentTbl68Speciesgroup.Info,
                            EngName = CurrentTbl68Speciesgroup.EngName,
                            GerName = CurrentTbl68Speciesgroup.GerName,
                            FraName = CurrentTbl68Speciesgroup.FraName,
                            PorName = CurrentTbl68Speciesgroup.PorName,
                            Writer = Environment.UserName,
                            WriterDate = DateTime.Now,
                            Updater = Environment.UserName,
                            UpdaterDate = DateTime.Now,
                            Memo = CurrentTbl68Speciesgroup.Memo  
                
                        });
                    }
                    {
                        //check about double Name
                        var dataset = new ObservableCollection<Tbl68Speciesgroup>
                        (from a in _tbl68SpeciesgroupsRepository.GetAll()
                         where
                         a.SpeciesgroupName.Trim() == CurrentTbl68Speciesgroup.SpeciesgroupName.Trim() &&                
                         a.Subspeciesgroup.Trim() == CurrentTbl68Speciesgroup.Subspeciesgroup.Trim()                 
                         select a);

                        if (dataset.Count != 0 && CurrentTbl68Speciesgroup.SpeciesgroupID== 0)  //dataset exist
                        {
                              WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTbl68Speciesgroup.SpeciesgroupName,
                              MessageBoxButton.OK, MessageBoxImage.Information);
                        }

                        if (dataset.Count == 0 && CurrentTbl68Speciesgroup.SpeciesgroupID == 0 ||
                            dataset.Count != 0 && CurrentTbl68Speciesgroup.SpeciesgroupID != 0  ||
                            dataset.Count == 0 && CurrentTbl68Speciesgroup.SpeciesgroupID != 0) //new dataset and update
                        {
                            if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTbl68Speciesgroup.SpeciesgroupName + " " + CurrentTbl68Speciesgroup.Subspeciesgroup,
                                    MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)                            
                                return;                            
                            {
                                _tbl68SpeciesgroupsRepository.Save();

                                WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess, CurrentTbl68Speciesgroup.SpeciesgroupName + " " + CurrentTbl68Speciesgroup.Subspeciesgroup,
                                    MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                        }  
         
                        if (SearchSpeciesgroupName == null && CurrentTbl68Speciesgroup.SpeciesgroupID == 0)  //new Dataset                        
                            Tbl68SpeciesgroupsList = _allListVm.GetValueTbl68SpeciesgroupsList();  //last Dataset
                        if (SearchSpeciesgroupName == null && CurrentTbl68Speciesgroup.SpeciesgroupID != 0)   //update 
                            Tbl68SpeciesgroupsList = _allListVm.GetValueTbl68SpeciesgroupsList(CurrentTbl68Speciesgroup.SpeciesgroupID);
                        if (SearchSpeciesgroupName != null && CurrentTbl68Speciesgroup.SpeciesgroupID == 0)  //new Dataset                        
                            Tbl68SpeciesgroupsList = _allListVm.GetValueTbl68SpeciesgroupsList();  //last Dataset
                        if (SearchSpeciesgroupName != null && CurrentTbl68Speciesgroup.SpeciesgroupID != 0)   //update 
                            Tbl68SpeciesgroupsList = _allListVm.GetValueTbl68SpeciesgroupsList(CurrentTbl68Speciesgroup.SpeciesgroupID);

                            SpeciesgroupsView = CollectionViewSource.GetDefaultView(Tbl68SpeciesgroupsList);
                            SpeciesgroupsView.Refresh();                          
         
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

             
        #region "Public Commands Connect ==> Tbl69FiSpecies"                 

        private RelayCommand _getFiSpeciesByNameOrIdCommand;     
               
        public ICommand GetFiSpeciesByNameOrIdCommand   
               
        {
            get { return _getFiSpeciesByNameOrIdCommand ?? (_getFiSpeciesByNameOrIdCommand = new RelayCommand(delegate { GetFiSpeciesByNameOrId(null); })); }   
        }

        private void GetFiSpeciesByNameOrId(object o)       
        {   
                
            int id;
            if (int.TryParse(SearchFiSpeciesName, out id))
                Tbl69FiSpeciessesList = new ObservableCollection<Tbl69FiSpecies> { _tbl69FiSpeciessesRepository.Get(id) };
            else
                Tbl69FiSpeciessesList = _allListVm.GetValueTbl69FiSpeciessesList(SearchFiSpeciesName);  

            Tbl68SpeciesgroupsAllList = _allListVm.GetValueTbl68SpeciesgroupsAllList();       
  FiSpeciessesView = CollectionViewSource.GetDefaultView(Tbl69FiSpeciessesList);
            FiSpeciessesView.Refresh();
        }
        //------------------------------------------------------------------------------     
                      
        private RelayCommand _addFiSpeciesCommand;      
                       
        public ICommand AddFiSpeciesCommand    
                        
        {
            get { return _addFiSpeciesCommand ?? (_addFiSpeciesCommand = new RelayCommand(delegate { AddFiSpecies(null); })); }
        }

        private void AddFiSpecies(object o)
        {
            Tbl69FiSpeciessesList = new ObservableCollection<Tbl69FiSpecies>();   
  Tbl69FiSpeciessesList.Insert(0, new Tbl69FiSpecies{ FiSpeciesName= CultRes.StringsRes.DatasetNew });  

            if (Tbl68SpeciesgroupsAllList == null)
            Tbl68SpeciesgroupsAllList = _allListVm.GetValueTbl68SpeciesgroupsAllList();     
  FiSpeciessesView = CollectionViewSource.GetDefaultView(Tbl69FiSpeciessesList);
            FiSpeciessesView.Refresh();
        }
        //---------------------------------------------------------------------------------------  
                              
        private RelayCommand _copyFiSpeciesCommand;            
                              
        public ICommand CopyFiSpeciesCommand          
                                 
        {
            get { return _copyFiSpeciesCommand ?? (_copyFiSpeciesCommand = new RelayCommand(delegate { CopyFiSpecies(null); })); }
        }

        private void CopyFiSpecies(object o)
        {
            Tbl69FiSpeciessesList = new ObservableCollection<Tbl69FiSpecies>();

            var fispecies = _tbl69FiSpeciessesRepository.Get(CurrentTbl69FiSpecies.FiSpeciesID);

            Tbl69FiSpeciessesList.Insert(0, new Tbl69FiSpecies
            {                 
                  
                            GenusID = fispecies.GenusID,
                            SpeciesgroupID = fispecies.SpeciesgroupID,              
                            FiSpeciesName = CultRes.StringsRes.DatasetNew,              
                            Subspecies = fispecies.Subspecies,
                            Divers = fispecies.Divers,
                            Valid = fispecies.Valid,
                            ValidYear = fispecies.ValidYear,
                            MemoSpecies = fispecies.MemoSpecies,
                            TradeName = fispecies.TradeName,
                            Author = fispecies.Author,
                            AuthorYear = fispecies.AuthorYear,
                            Importer = fispecies.Importer,
                            ImportingYear = fispecies.ImportingYear,
                            TypeSpecies = fispecies.TypeSpecies,
                            LNumber = fispecies.LNumber,
                            LOrigin = fispecies.LOrigin,
                            LDAOrigin = fispecies.LDAOrigin,
                            LDANumber = fispecies.LDANumber,
                            BasinLength = fispecies.BasinLength,
                            FishLength = fispecies.FishLength,
                            Karnivore = fispecies.Karnivore,
                            Herbivore = fispecies.Herbivore,
                            Limnivore = fispecies.Limnivore,
                            Omnivore = fispecies.Omnivore,
                            MemoFoods = fispecies.MemoFoods,
                            Difficult1 = fispecies.Difficult1,
                            Difficult2 = fispecies.Difficult2,
                            Difficult3 = fispecies.Difficult3,
                            Difficult4 = fispecies.Difficult4,
                            RegionTop = fispecies.RegionTop,
                            RegionMiddle = fispecies.RegionMiddle,
                            RegionBottom = fispecies.RegionBottom,
                            MemoRegion = fispecies.MemoRegion,
                            MemoTech = fispecies.MemoTech,
                            Ph1 = fispecies.Ph1,
                            Ph2 = fispecies.Ph2,
                            Temp1 = fispecies.Temp1,
                            Temp2 = fispecies.Temp2,
                            Hardness1 = fispecies.Hardness1,
                            Hardness2 = fispecies.Hardness2,
                            CarboHardness1 = fispecies.CarboHardness1,
                            CarboHardness2 = fispecies.CarboHardness2,
                            MemoHusbandry = fispecies.MemoHusbandry,
                            MemoBuilt = fispecies.MemoBuilt,
                            MemoColor = fispecies.MemoColor,
                            MemoSozial = fispecies.MemoSozial,
                            MemoDomorphism = fispecies.MemoDomorphism,
                            MemoSpecial = fispecies.MemoSpecial,
                            MemoBreeding = fispecies.MemoBreeding       
                                   
            });

            FiSpeciessesView = CollectionViewSource.GetDefaultView(Tbl69FiSpeciessesList);
            FiSpeciessesView.Refresh();
        }
        //---------------------------------------------------------------------------------------    
                                           
        private RelayCommand _deleteFiSpeciesCommand;              
                                           
        public ICommand DeleteFiSpeciesCommand             
                                                
        {
            get { return _deleteFiSpeciesCommand ?? (_deleteFiSpeciesCommand = new RelayCommand(delegate { DeleteFiSpecies(null); })); }
        }

        private void DeleteFiSpecies(object o)
        {
            try
            {
                var fispecies = _tbl69FiSpeciessesRepository.Get(CurrentTbl69FiSpecies.FiSpeciesID);
                if (fispecies!= null)
                {  
                                                    
                    if (WpfMessageBox.Show(CultRes.StringsRes.DeleteQuestion1, CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl69FiSpecies.FiSpeciesName + " " +
                        CurrentTbl69FiSpecies.Subspecies + " " + CurrentTbl69FiSpecies.Divers,
                         MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes) 
                    return;

                    _tbl69FiSpeciessesRepository.Delete(fispecies);
                    _tbl69FiSpeciessesRepository.Save();

                    WpfMessageBox.Show(CultRes.StringsRes.DeleteSuccess, CurrentTbl69FiSpecies.FiSpeciesName+ " " +
                         CurrentTbl69FiSpecies.Subspecies + " " + CurrentTbl69FiSpecies.Divers,             
                       MessageBoxButton.OK, MessageBoxImage.Information);  
                                                        
                        if (SearchFiSpeciesName == null)                       
                            GetConnectedTablesById(o); //refresh doubleClick                                          
                        else
                        {
                        Tbl69FiSpeciessesList = _allListVm.GetValueTbl69FiSpeciessesList(SearchFiSpeciesName);  
  FiSpeciessesView = CollectionViewSource.GetDefaultView(Tbl69FiSpeciessesList);
                            FiSpeciessesView.Refresh();
                    }
                }
                else
                {   
                                                          
                    WpfMessageBox.Show(CultRes.StringsRes.Information, CultRes.StringsRes.DeleteCan + " " + CurrentTbl69FiSpecies.FiSpeciesName + " " + 
                        CurrentTbl69FiSpecies.Subspecies + " " + CurrentTbl69FiSpecies.Divers + " " + CultRes.StringsRes.DeleteCan1,
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
                                                               
        private RelayCommand _saveFiSpeciesCommand;              
                                                                 
        public ICommand SaveFiSpeciesCommand             
                                                                     
        {
            get { return _saveFiSpeciesCommand ?? (_saveFiSpeciesCommand = new RelayCommand(delegate { SaveFiSpecies(null); })); }
        }

        private void SaveFiSpecies(object o)
        {
            try
            {
                var fispecies = _tbl69FiSpeciessesRepository.Get(CurrentTbl69FiSpecies.FiSpeciesID);
                if (CurrentTbl69FiSpecies == null)               
                    WpfMessageBox.Show(CultRes.StringsRes.Warning, CultRes.StringsRes.DatasetNotExist, 
                        MessageBoxButton.OK, MessageBoxImage.Warning);              
                else
                {
                    if (CurrentTbl69FiSpecies.FiSpeciesID!= 0)
                    {
                        if (fispecies!= null) //update
                        {   
  fispecies.GenusID = CurrentTbl69FiSpecies.GenusID;            
                            fispecies.SpeciesgroupID = CurrentTbl69FiSpecies.SpeciesgroupID;            
                            fispecies.FiSpeciesName = CurrentTbl69FiSpecies.FiSpeciesName;
                            fispecies.Subspecies = CurrentTbl69FiSpecies.Subspecies;
                            fispecies.Divers = CurrentTbl69FiSpecies.Divers;
                            fispecies.Valid = CurrentTbl69FiSpecies.Valid;
                            fispecies.ValidYear = CurrentTbl69FiSpecies.ValidYear;              
                            fispecies.TradeName = CurrentTbl69FiSpecies.TradeName;
                            fispecies.Author = CurrentTbl69FiSpecies.Author;
                            fispecies.AuthorYear = CurrentTbl69FiSpecies.AuthorYear;
                            fispecies.Importer = CurrentTbl69FiSpecies.Importer;
                            fispecies.ImportingYear = CurrentTbl69FiSpecies.ImportingYear;
                            fispecies.TypeSpecies = CurrentTbl69FiSpecies.TypeSpecies;
                            fispecies.LNumber = CurrentTbl69FiSpecies.LNumber;
                            fispecies.LOrigin = CurrentTbl69FiSpecies.LOrigin;
                            fispecies.LDAOrigin = CurrentTbl69FiSpecies.LDAOrigin;
                            fispecies.LDANumber = CurrentTbl69FiSpecies.LDANumber;
                            fispecies.BasinLength = CurrentTbl69FiSpecies.BasinLength;
                            fispecies.FishLength = CurrentTbl69FiSpecies.FishLength;
                            fispecies.Karnivore = CurrentTbl69FiSpecies.Karnivore;
                            fispecies.Herbivore = CurrentTbl69FiSpecies.Herbivore;
                            fispecies.Limnivore = CurrentTbl69FiSpecies.Limnivore;
                            fispecies.Omnivore = CurrentTbl69FiSpecies.Omnivore;
                            fispecies.MemoFoods = CurrentTbl69FiSpecies.MemoFoods;
                            fispecies.Difficult1 = CurrentTbl69FiSpecies.Difficult1;
                            fispecies.Difficult2 = CurrentTbl69FiSpecies.Difficult2;
                            fispecies.Difficult3 = CurrentTbl69FiSpecies.Difficult3;
                            fispecies.Difficult4 = CurrentTbl69FiSpecies.Difficult4;
                            fispecies.RegionTop = CurrentTbl69FiSpecies.RegionTop;
                            fispecies.RegionMiddle = CurrentTbl69FiSpecies.RegionMiddle;
                            fispecies.RegionBottom = CurrentTbl69FiSpecies.RegionBottom;
                            fispecies.MemoRegion = CurrentTbl69FiSpecies.MemoRegion;
                            fispecies.MemoTech = CurrentTbl69FiSpecies.MemoTech;
                            fispecies.Ph1 = CurrentTbl69FiSpecies.Ph1;
                            fispecies.Ph2 = CurrentTbl69FiSpecies.Ph2;
                            fispecies.Temp1 = CurrentTbl69FiSpecies.Temp1;
                            fispecies.Temp2 = CurrentTbl69FiSpecies.Temp2;
                            fispecies.Hardness1 = CurrentTbl69FiSpecies.Hardness1;
                            fispecies.Hardness2 = CurrentTbl69FiSpecies.Hardness2;
                            fispecies.CarboHardness1 = CurrentTbl69FiSpecies.CarboHardness1;
                            fispecies.CarboHardness2 = CurrentTbl69FiSpecies.CarboHardness2;
                            fispecies.MemoHusbandry = CurrentTbl69FiSpecies.MemoHusbandry;
                            fispecies.MemoBuilt = CurrentTbl69FiSpecies.MemoBuilt;
                            fispecies.MemoColor = CurrentTbl69FiSpecies.MemoColor;
                            fispecies.MemoSozial = CurrentTbl69FiSpecies.MemoSozial;
                            fispecies.MemoDomorphism = CurrentTbl69FiSpecies.MemoDomorphism;
                            fispecies.MemoSpecial = CurrentTbl69FiSpecies.MemoSpecial;
                            fispecies.Updater = Environment.UserName;
                            fispecies.UpdaterDate = DateTime.Now;
                            fispecies.MemoBreeding = CurrentTbl69FiSpecies.MemoBreeding;                                                            
                                                                            
                        }
                    }
                    else
                    {
                        _tbl69FiSpeciessesRepository.Add(new Tbl69FiSpecies    // add new
                        {   
                  
                            GenusID = CurrentTbl69FiSpecies.GenusID,
                            SpeciesgroupID = CurrentTbl69FiSpecies.SpeciesgroupID,
                            FiSpeciesName= CurrentTbl69FiSpecies.FiSpeciesName,              
                            Subspecies = CurrentTbl69FiSpecies.Subspecies,
                            Divers = CurrentTbl69FiSpecies.Divers,
                            CountID = RandomHelper.Randomnumber(),
                            Valid = CurrentTbl69FiSpecies.Valid,
                            ValidYear = CurrentTbl69FiSpecies.ValidYear,
                            MemoSpecies = CurrentTbl69FiSpecies.MemoSpecies,
                            TradeName = CurrentTbl69FiSpecies.TradeName,
                            Author = CurrentTbl69FiSpecies.Author,
                            AuthorYear = CurrentTbl69FiSpecies.AuthorYear,
                            Importer = CurrentTbl69FiSpecies.Importer,
                            ImportingYear = CurrentTbl69FiSpecies.ImportingYear,
                            TypeSpecies = CurrentTbl69FiSpecies.TypeSpecies,
                            LNumber = CurrentTbl69FiSpecies.LNumber,
                            LOrigin = CurrentTbl69FiSpecies.LOrigin,
                            LDAOrigin = CurrentTbl69FiSpecies.LDAOrigin,
                            LDANumber = CurrentTbl69FiSpecies.LDANumber,
                            BasinLength = CurrentTbl69FiSpecies.BasinLength,
                            FishLength = CurrentTbl69FiSpecies.FishLength,
                            Karnivore = CurrentTbl69FiSpecies.Karnivore,
                            Herbivore = CurrentTbl69FiSpecies.Herbivore,
                            Limnivore = CurrentTbl69FiSpecies.Limnivore,
                            Omnivore = CurrentTbl69FiSpecies.Omnivore,
                            MemoFoods = CurrentTbl69FiSpecies.MemoFoods,
                            Difficult1 = CurrentTbl69FiSpecies.Difficult1,
                            Difficult2 = CurrentTbl69FiSpecies.Difficult2,
                            Difficult3 = CurrentTbl69FiSpecies.Difficult3,
                            Difficult4 = CurrentTbl69FiSpecies.Difficult4,
                            RegionTop = CurrentTbl69FiSpecies.RegionTop,
                            RegionMiddle = CurrentTbl69FiSpecies.RegionMiddle,
                            RegionBottom = CurrentTbl69FiSpecies.RegionBottom,
                            MemoRegion = CurrentTbl69FiSpecies.MemoRegion,
                            MemoTech = CurrentTbl69FiSpecies.MemoTech,
                            Ph1 = CurrentTbl69FiSpecies.Ph1,
                            Ph2 = CurrentTbl69FiSpecies.Ph2,
                            Temp1 = CurrentTbl69FiSpecies.Temp1,
                            Temp2 = CurrentTbl69FiSpecies.Temp2,
                            Hardness1 = CurrentTbl69FiSpecies.Hardness1,
                            Hardness2 = CurrentTbl69FiSpecies.Hardness2,
                            CarboHardness1 = CurrentTbl69FiSpecies.CarboHardness1,
                            CarboHardness2 = CurrentTbl69FiSpecies.CarboHardness2,
                            MemoHusbandry = CurrentTbl69FiSpecies.MemoHusbandry,
                            MemoBuilt = CurrentTbl69FiSpecies.MemoBuilt,
                            MemoColor = CurrentTbl69FiSpecies.MemoColor,
                            MemoSozial = CurrentTbl69FiSpecies.MemoSozial,
                            MemoDomorphism = CurrentTbl69FiSpecies.MemoDomorphism,
                            MemoSpecial = CurrentTbl69FiSpecies.MemoSpecial,
                            Writer = Environment.UserName,
                            WriterDate = DateTime.Now,
                            Updater = Environment.UserName,
                            UpdaterDate = DateTime.Now,
                            MemoBreeding = CurrentTbl69FiSpecies.MemoBreeding   
                                                                                  
                        });
                    }
                    {
                        //SpeciesgroupID may be not 0
                        if (CurrentTbl69FiSpecies.SpeciesgroupID == 0)
                        {
                            WpfMessageBox.Show(CultRes.StringsRes.RequiredGenealogyConnect, CultRes.StringsRes.RequiredInput,
                                MessageBoxButton.OK, MessageBoxImage.Information);
                            return;
                        }

                        //check if dataset with Name and vb-name already exist
                        var dataset = new ObservableCollection<Tbl69FiSpecies>
                        (from a in _tbl69FiSpeciessesRepository.GetAll()
                         where
                         a.FiSpeciesName.Trim() == CurrentTbl69FiSpecies.FiSpeciesName.Trim() &&                
                         a.Subspecies .Trim() == CurrentTbl69FiSpecies.Subspecies .Trim()   &&              
                         a.Divers.Trim() == CurrentTbl69FiSpecies.Divers.Trim()    &&             
                         a.GenusID == CurrentTbl69FiSpecies.GenusID &&
                         a.SpeciesgroupID== CurrentTbl69FiSpecies.SpeciesgroupID
                        select a);

                        if (dataset.Count != 0 && CurrentTbl69FiSpecies.FiSpeciesID == 0)  //dataset exist
                        {
                              WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, 
                              CurrentTbl69FiSpecies.Tbl66Genusses.GenusName + " " + CurrentTbl69FiSpecies.FiSpeciesName + " " + CurrentTbl69FiSpecies.Subspecies + " " + CurrentTbl69FiSpecies.Divers,
                              MessageBoxButton.OK, MessageBoxImage.Information);
                        } 

                        if (dataset.Count == 0 && CurrentTbl69FiSpecies.FiSpeciesID == 0 ||
                            dataset.Count != 0 && CurrentTbl69FiSpecies.FiSpeciesID != 0  ||
                            dataset.Count == 0 && CurrentTbl69FiSpecies.FiSpeciesID != 0) //new dataset and update
                        {
                            if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, 
                                    CurrentTbl69FiSpecies.Tbl66Genusses.GenusName + " " + CurrentTbl69FiSpecies.FiSpeciesName + " " + CurrentTbl69FiSpecies.Subspecies + " " + CurrentTbl69FiSpecies.Divers,
                                    MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)                            
                                return;                            
                            {
                                _tbl69FiSpeciessesRepository.Save();

                                WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess, 
                                    CurrentTbl69FiSpecies.Tbl66Genusses.GenusName + " " + CurrentTbl69FiSpecies.FiSpeciesName + " " + CurrentTbl69FiSpecies.Subspecies + " " + CurrentTbl69FiSpecies.Divers,
                                    MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                        } 
                
                        if (SearchFiSpeciesName == null && CurrentTbl69FiSpecies.FiSpeciesID == 0)  //new Dataset                        
                            Tbl69FiSpeciessesList = _allListVm.GetValueTbl69FiSpeciessesList();  //last Dataset
                        if (SearchFiSpeciesName == null && CurrentTbl69FiSpecies.FiSpeciesID != 0)   //update
                            Tbl69FiSpeciessesList = _allListVm.GetValueTbl69FiSpeciessesList(CurrentTbl69FiSpecies.FiSpeciesID);
                        if (SearchFiSpeciesName != null && CurrentTbl69FiSpecies.FiSpeciesID == 0)  //new Dataset
                            Tbl69FiSpeciessesList = _allListVm.GetValueTbl69FiSpeciessesList();  //last Dataset
                        if (SearchFiSpeciesName != null && CurrentTbl69FiSpecies.FiSpeciesID != 0)   //update
                            Tbl69FiSpeciessesList = _allListVm.GetValueTbl69FiSpeciessesList(CurrentTbl69FiSpecies.FiSpeciesID);
                                                                       

                        FiSpeciessesView = CollectionViewSource.GetDefaultView(Tbl69FiSpeciessesList);
                        FiSpeciessesView.Refresh();             
                                                                                          
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


             
        #region "Public Commands Connect ==> Tbl72PlSpecies"                 

        private RelayCommand _getPlSpeciesByNameOrIdCommand;     
               
        public ICommand GetPlSpeciesByNameOrIdCommand   
               
        {
            get { return _getPlSpeciesByNameOrIdCommand ?? (_getPlSpeciesByNameOrIdCommand = new RelayCommand(delegate { GetPlSpeciesByNameOrId(null); })); }   
        }

        private void GetPlSpeciesByNameOrId(object o)       
        {   
                
            int id;
            if (int.TryParse(SearchPlSpeciesName, out id))
                Tbl72PlSpeciessesList = new ObservableCollection<Tbl72PlSpecies> { _tbl72PlSpeciessesRepository.Get(id) };
            else
                Tbl72PlSpeciessesList = _allListVm.GetValueTbl72PlSpeciessesList (SearchPlSpeciesName);   

            Tbl68SpeciesgroupsAllList = _allListVm.GetValueTbl68SpeciesgroupsAllList();      
  PlSpeciessesView = CollectionViewSource.GetDefaultView(Tbl72PlSpeciessesList);
            PlSpeciessesView.Refresh();
        }
        //------------------------------------------------------------------------------     
                      
        private RelayCommand _addPlSpeciesCommand;      
                       
        public ICommand AddPlSpeciesCommand    
                        
        {
            get { return _addPlSpeciesCommand ?? (_addPlSpeciesCommand = new RelayCommand(delegate { AddPlSpecies(null); })); }
        }

        private void AddPlSpecies(object o)
        {
            Tbl72PlSpeciessesList = new ObservableCollection<Tbl72PlSpecies>();   
  Tbl72PlSpeciessesList.Insert(0, new Tbl72PlSpecies{ PlSpeciesName= CultRes.StringsRes.DatasetNew });   
            Tbl68SpeciesgroupsAllList = _allListVm.GetValueTbl68SpeciesgroupsAllList();    
  PlSpeciessesView = CollectionViewSource.GetDefaultView(Tbl72PlSpeciessesList);
            PlSpeciessesView.Refresh();
        }
        //---------------------------------------------------------------------------------------  
                              
        private RelayCommand _copyPlSpeciesCommand;            
                              
        public ICommand CopyPlSpeciesCommand          
                                 
        {
            get { return _copyPlSpeciesCommand ?? (_copyPlSpeciesCommand = new RelayCommand(delegate { CopyPlSpecies(null); })); }
        }

        private void CopyPlSpecies(object o)
        {
            Tbl72PlSpeciessesList = new ObservableCollection<Tbl72PlSpecies>();

            var plspecies = _tbl72PlSpeciessesRepository.Get(CurrentTbl72PlSpecies.PlSpeciesID);

            Tbl72PlSpeciessesList.Insert(0, new Tbl72PlSpecies
            {                 
                  
                            GenusID = plspecies.GenusID,
                            SpeciesgroupID = plspecies.SpeciesgroupID,
                            PlSpeciesName = CultRes.StringsRes.DatasetNew,              
                            Subspecies = plspecies.Subspecies,
                            Divers = plspecies.Divers,
                            Valid = plspecies.Valid,
                            ValidYear = plspecies.ValidYear,
                            MemoSpecies = plspecies.MemoSpecies,
                            TradeName = plspecies.TradeName,
                            Author = plspecies.Author,
                            AuthorYear = plspecies.AuthorYear,
                            Importer = plspecies.Importer,
                            ImportingYear = plspecies.ImportingYear,
                            BasinHeight = plspecies.BasinHeight,
                            PlantLength = plspecies.PlantLength,
                            Difficult1 = plspecies.Difficult1,
                            Difficult2 = plspecies.Difficult2,
                            Difficult3 = plspecies.Difficult3,
                            Difficult4 = plspecies.Difficult4,
                            MemoTech = plspecies.MemoTech,
                            Ph1 = plspecies.Ph1,
                            Ph2 = plspecies.Ph2,
                            Temp1 = plspecies.Temp1,
                            Temp2 = plspecies.Temp2,
                            Hardness1 = plspecies.Hardness1,
                            Hardness2 = plspecies.Hardness2,
                            CarboHardness1 = plspecies.CarboHardness1,
                            CarboHardness2 = plspecies.CarboHardness2,
                            MemoBuilt = plspecies.MemoBuilt,
                            MemoColor = plspecies.MemoColor,
                            MemoReproduction = plspecies.MemoReproduction,
                            MemoCulture = plspecies.MemoCulture,
                            MemoGlobal = plspecies.MemoGlobal         
                                   
            });

            PlSpeciessesView = CollectionViewSource.GetDefaultView(Tbl72PlSpeciessesList);
            PlSpeciessesView.Refresh();
        }
        //---------------------------------------------------------------------------------------    
                                           
        private RelayCommand _deletePlSpeciesCommand;              
                                           
        public ICommand DeletePlSpeciesCommand             
                                                
        {
            get { return _deletePlSpeciesCommand ?? (_deletePlSpeciesCommand = new RelayCommand(delegate { DeletePlSpecies(null); })); }
        }

        private void DeletePlSpecies(object o)
        {
            try
            {
                var plspecies = _tbl72PlSpeciessesRepository.Get(CurrentTbl72PlSpecies.PlSpeciesID);
                if (plspecies!= null)
                {  
                                                    
                    if (WpfMessageBox.Show(CultRes.StringsRes.DeleteQuestion1, CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl72PlSpecies.PlSpeciesName + " " +
                        CurrentTbl72PlSpecies.Subspecies + " " + CurrentTbl72PlSpecies.Divers,
                         MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes) 
                     return;

                    _tbl72PlSpeciessesRepository.Delete(plspecies);
                    _tbl72PlSpeciessesRepository.Save();

                    WpfMessageBox.Show(CultRes.StringsRes.DeleteSuccess, CurrentTbl72PlSpecies.PlSpeciesName+ " " +
                         CurrentTbl72PlSpecies.Subspecies + " " + CurrentTbl72PlSpecies.Divers,             
                       MessageBoxButton.OK, MessageBoxImage.Information);  
                                                        
                        if (SearchPlSpeciesName == null)                       
                            GetConnectedTablesById(o); //refresh doubleClick                                          
                        else
                        {
                            Tbl72PlSpeciessesList = _allListVm.GetValueTbl72PlSpeciessesList(SearchPlSpeciesName);  
  PlSpeciessesView = CollectionViewSource.GetDefaultView(Tbl72PlSpeciessesList);
                            PlSpeciessesView.Refresh();
                    }
                }
                else
                {   
                                                          
                    WpfMessageBox.Show(CultRes.StringsRes.Information, CultRes.StringsRes.DeleteCan + " " + CurrentTbl72PlSpecies.PlSpeciesName + " " + 
                        CurrentTbl72PlSpecies.Subspecies + " " + CurrentTbl72PlSpecies.Divers + " " + CultRes.StringsRes.DeleteCan1,
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
                                                               
        private RelayCommand _savePlSpeciesCommand;              
                                                                 
        public ICommand SavePlSpeciesCommand             
                                                                     
        {
            get { return _savePlSpeciesCommand ?? (_savePlSpeciesCommand = new RelayCommand(delegate { SavePlSpecies(null); })); }
        }

        private void SavePlSpecies(object o)
        {
            try
            {
                var plspecies = _tbl72PlSpeciessesRepository.Get(CurrentTbl72PlSpecies.PlSpeciesID);
                if (CurrentTbl72PlSpecies == null)                
                    WpfMessageBox.Show(CultRes.StringsRes.Warning, CultRes.StringsRes.DatasetNotExist, MessageBoxButton.OK, MessageBoxImage.Warning);              
                else
                {
                    if (CurrentTbl72PlSpecies.PlSpeciesID!= 0)
                    {
                        if (plspecies!= null) //update
                        {   
  plspecies.GenusID = CurrentTbl72PlSpecies.GenusID;            
                            plspecies.SpeciesgroupID = CurrentTbl72PlSpecies.SpeciesgroupID;            
                            plspecies.PlSpeciesName = CurrentTbl72PlSpecies.PlSpeciesName;
                            plspecies.Subspecies = CurrentTbl72PlSpecies.Subspecies;
                            plspecies.Divers = CurrentTbl72PlSpecies.Divers;
                            plspecies.Valid = CurrentTbl72PlSpecies.Valid;
                            plspecies.ValidYear = CurrentTbl72PlSpecies.ValidYear;
                            plspecies.MemoSpecies = CurrentTbl72PlSpecies.MemoSpecies;          
                            plspecies.TradeName = CurrentTbl72PlSpecies.TradeName;
                            plspecies.Author = CurrentTbl72PlSpecies.Author;
                            plspecies.AuthorYear = CurrentTbl72PlSpecies.AuthorYear;
                            plspecies.Importer = CurrentTbl72PlSpecies.Importer;
                            plspecies.ImportingYear = CurrentTbl72PlSpecies.ImportingYear;
                            plspecies.BasinHeight = CurrentTbl72PlSpecies.BasinHeight;
                            plspecies.PlantLength = CurrentTbl72PlSpecies.PlantLength;
                            plspecies.Difficult1 = CurrentTbl72PlSpecies.Difficult1;
                            plspecies.Difficult2 = CurrentTbl72PlSpecies.Difficult2;
                            plspecies.Difficult3 = CurrentTbl72PlSpecies.Difficult3;
                            plspecies.Difficult4 = CurrentTbl72PlSpecies.Difficult4;
                            plspecies.MemoTech = CurrentTbl72PlSpecies.MemoTech;
                            plspecies.Ph1 = CurrentTbl72PlSpecies.Ph1;
                            plspecies.Ph2 = CurrentTbl72PlSpecies.Ph2;
                            plspecies.Temp1 = CurrentTbl72PlSpecies.Temp1;
                            plspecies.Temp2 = CurrentTbl72PlSpecies.Temp2;
                            plspecies.Hardness1 = CurrentTbl72PlSpecies.Hardness1;
                            plspecies.Hardness2 = CurrentTbl72PlSpecies.Hardness2;
                            plspecies.CarboHardness1 = CurrentTbl72PlSpecies.CarboHardness1;
                            plspecies.CarboHardness2 = CurrentTbl72PlSpecies.CarboHardness2;
                            plspecies.MemoBuilt = CurrentTbl72PlSpecies.MemoBuilt;
                            plspecies.MemoColor = CurrentTbl72PlSpecies.MemoColor;
                            plspecies.MemoReproduction = CurrentTbl72PlSpecies.MemoReproduction;
                            plspecies.MemoCulture = CurrentTbl72PlSpecies.MemoCulture;
                            plspecies.MemoGlobal = CurrentTbl72PlSpecies.MemoGlobal;
                            plspecies.Updater = Environment.UserName;
                            plspecies.UpdaterDate = DateTime.Now;                                                            
                                                                          
                        }
                    }
                    else
                    {
                        _tbl72PlSpeciessesRepository.Add(new Tbl72PlSpecies     //add new
                        {   
                  
                            GenusID = CurrentTbl72PlSpecies.GenusID,
                            SpeciesgroupID = CurrentTbl72PlSpecies.SpeciesgroupID,
                            PlSpeciesName= CurrentTbl72PlSpecies.PlSpeciesName,              
                            Subspecies = CurrentTbl72PlSpecies.Subspecies,
                            Divers = CurrentTbl72PlSpecies.Divers,
                            CountID = RandomHelper.Randomnumber(),
                            Valid = CurrentTbl72PlSpecies.Valid,
                            ValidYear = CurrentTbl72PlSpecies.ValidYear,
                            MemoSpecies = CurrentTbl72PlSpecies.MemoSpecies,
                            TradeName = CurrentTbl72PlSpecies.TradeName,
                            Author = CurrentTbl72PlSpecies.Author,
                            AuthorYear = CurrentTbl72PlSpecies.AuthorYear,
                            Importer = CurrentTbl72PlSpecies.Importer,
                            ImportingYear = CurrentTbl72PlSpecies.ImportingYear,
                            BasinHeight = CurrentTbl72PlSpecies.BasinHeight ,
                            PlantLength = CurrentTbl72PlSpecies.PlantLength ,
                            Difficult1 = CurrentTbl72PlSpecies.Difficult1,
                            Difficult2 = CurrentTbl72PlSpecies.Difficult2,
                            Difficult3 = CurrentTbl72PlSpecies.Difficult3,
                            Difficult4 = CurrentTbl72PlSpecies.Difficult4,
                            MemoTech = CurrentTbl72PlSpecies.MemoTech,
                            Ph1 = CurrentTbl72PlSpecies.Ph1,
                            Ph2 = CurrentTbl72PlSpecies.Ph2,
                            Temp1 = CurrentTbl72PlSpecies.Temp1,
                            Temp2 = CurrentTbl72PlSpecies.Temp2,
                            Hardness1 = CurrentTbl72PlSpecies.Hardness1,
                            Hardness2 = CurrentTbl72PlSpecies.Hardness2,
                            CarboHardness1 = CurrentTbl72PlSpecies.CarboHardness1,
                            CarboHardness2 = CurrentTbl72PlSpecies.CarboHardness2,
                            MemoBuilt = CurrentTbl72PlSpecies.MemoBuilt,
                            MemoColor = CurrentTbl72PlSpecies.MemoColor,
                            MemoReproduction = CurrentTbl72PlSpecies.MemoReproduction ,
                            MemoCulture = CurrentTbl72PlSpecies.MemoCulture,
                            MemoGlobal = CurrentTbl72PlSpecies.MemoGlobal,
                            Writer = Environment.UserName,
                            WriterDate = DateTime.Now,
                            Updater = Environment.UserName,
                            UpdaterDate = DateTime.Now  
                                                                                  
                        });
                    }
                    {
                        //check about double Name   
                        var dataset = new ObservableCollection<Tbl72PlSpecies>
                        (from a in _tbl72PlSpeciessesRepository.GetAll()
                         where
                         a.PlSpeciesName.Trim() == CurrentTbl72PlSpecies.PlSpeciesName.Trim() &&                
                         a.Subspecies .Trim() == CurrentTbl72PlSpecies.Subspecies .Trim()   &&              
                         a.Divers.Trim() == CurrentTbl72PlSpecies.Divers.Trim()    &&             
                         a.GenusID == CurrentTbl72PlSpecies.GenusID &&
                         a.SpeciesgroupID== CurrentTbl72PlSpecies.SpeciesgroupID
                        select a);

                        if (dataset.Count != 0 && CurrentTbl72PlSpecies.PlSpeciesID == 0)  //dataset exist
                        {
                              WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, 
                                    CurrentTbl72PlSpecies.Tbl66Genusses.GenusName + " " + CurrentTbl72PlSpecies.PlSpeciesName + " " + CurrentTbl72PlSpecies.Subspecies + " " + CurrentTbl72PlSpecies.Divers,
                              MessageBoxButton.OK, MessageBoxImage.Information);
                        }

                        if (dataset.Count == 0 && CurrentTbl72PlSpecies.PlSpeciesID == 0 ||
                            dataset.Count != 0 && CurrentTbl72PlSpecies.PlSpeciesID != 0  ||
                            dataset.Count == 0 && CurrentTbl72PlSpecies.PlSpeciesID != 0) //new dataset and update
                        {
                            if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, 
                                    CurrentTbl72PlSpecies.Tbl66Genusses.GenusName + " " + CurrentTbl72PlSpecies.PlSpeciesName + " " + CurrentTbl72PlSpecies.Subspecies + " " + CurrentTbl72PlSpecies.Divers,
                                    MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)                            
                                return;                            
                            {
                                _tbl72PlSpeciessesRepository.Save();

                                WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess, 
                                    CurrentTbl72PlSpecies.Tbl66Genusses.GenusName + " " + CurrentTbl72PlSpecies.PlSpeciesName + " " + CurrentTbl72PlSpecies.Subspecies + " " + CurrentTbl72PlSpecies.Divers,
                                    MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                        }  
                 

                        if (SearchPlSpeciesName == null && CurrentTbl72PlSpecies.PlSpeciesID == 0)  //new Dataset                        
                            Tbl72PlSpeciessesList = _allListVm.GetValueTbl72PlSpeciessesList();  //last Dataset
                        if (SearchPlSpeciesName == null && CurrentTbl72PlSpecies.PlSpeciesID != 0)   //update
                            Tbl72PlSpeciessesList = _allListVm.GetValueTbl72PlSpeciessesList(CurrentTbl72PlSpecies.PlSpeciesID);
                        if (SearchPlSpeciesName != null && CurrentTbl72PlSpecies.PlSpeciesID == 0)  //new Dataset
                            Tbl72PlSpeciessesList = _allListVm.GetValueTbl72PlSpeciessesList();  //last Dataset
                        if (SearchPlSpeciesName != null && CurrentTbl72PlSpecies.PlSpeciesID != 0)   //update
                            Tbl72PlSpeciessesList = _allListVm.GetValueTbl72PlSpeciessesList(CurrentTbl72PlSpecies.PlSpeciesID);     
                                                                 
                        PlSpeciessesView = CollectionViewSource.GetDefaultView(Tbl72PlSpeciessesList);
                        PlSpeciessesView.Refresh();           
                                                                                          
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

            Tbl69FiSpeciessesList =  new ObservableCollection<Tbl69FiSpecies>
                                                      (from x in _tbl69FiSpeciessesRepository.GetAll()
                                                       where x.SpeciesgroupID == CurrentTbl68Speciesgroup.SpeciesgroupID 
                                                       orderby  x.FiSpeciesName, x.Subspecies, x.Divers
                                                       select x);

            FiSpeciessesView = CollectionViewSource.GetDefaultView(Tbl69FiSpeciessesList);
            FiSpeciessesView.Refresh();
            //-----------------------------------------------------------------------------------
            Tbl72PlSpeciessesList =   new ObservableCollection<Tbl72PlSpecies>
                                                      (from y in _tbl72PlSpeciessesRepository.GetAll()
                                                       where y.SpeciesgroupID == CurrentTbl68Speciesgroup.SpeciesgroupID 
                                                       orderby  y.PlSpeciesName, y.Subspecies, y.Divers
                                                       select y);

            PlSpeciessesView = CollectionViewSource.GetDefaultView(Tbl72PlSpeciessesList);
            PlSpeciessesView.Refresh();
        }

        #endregion "Public Commands Connected Tables by DoubleClick"
    
 

 //    Part 10    

 

 //    Part 11    

    
     
        #region "Public Properties Tbl68Speciesgroup"

        private string _searchSpeciesgroupName;
        public string SearchSpeciesgroupName
        {
            get => _searchSpeciesgroupName; 
            set { _searchSpeciesgroupName = value; RaisePropertyChanged(); }
        }

        public ICollectionView SpeciesgroupsView;
        public Tbl68Speciesgroup CurrentTbl68Speciesgroup => SpeciesgroupsView?.CurrentItem as Tbl68Speciesgroup;           

        private ObservableCollection<Tbl68Speciesgroup> _tbl68SpeciesgroupsList;
        public ObservableCollection<Tbl68Speciesgroup> Tbl68SpeciesgroupsList
        {
            get => _tbl68SpeciesgroupsList; 
            set { _tbl68SpeciesgroupsList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl68Speciesgroup> _tbl68SpeciesgroupsAllList;
        public ObservableCollection<Tbl68Speciesgroup> Tbl68SpeciesgroupsAllList
        {
            get => _tbl68SpeciesgroupsAllList; 
            set { _tbl68SpeciesgroupsAllList = value; RaisePropertyChanged(); }       
        }

        private ObservableCollection<Tbl66Genus> _tbl66GenussesAllList;
        public ObservableCollection<Tbl66Genus> Tbl66GenussesAllList
        {
            get => _tbl66GenussesAllList; 
            set { _tbl66GenussesAllList = value; RaisePropertyChanged(); }
        }
        #endregion "Public Properties"   

        #region "Public Properties Tbl69FiSpecies"

        private string _searchFiSpeciesName;
        public string SearchFiSpeciesName
        {
            get => _searchFiSpeciesName; 
            set { _searchFiSpeciesName = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl69FiSpecies> _tbl69FiSpeciesList;
        public ObservableCollection<Tbl69FiSpecies> Tbl69FiSpeciessesList
        {
            get => _tbl69FiSpeciesList; 
            set { _tbl69FiSpeciesList = value; RaisePropertyChanged(); }
        }

        public ICollectionView FiSpeciessesView;
        public Tbl69FiSpecies CurrentTbl69FiSpecies => FiSpeciessesView?.CurrentItem as Tbl69FiSpecies;

        #endregion "Public Properties"

        #region "Public Properties Tbl72PlSpecies"

        private string _searchPlSpeciesName;
        public string SearchPlSpeciesName
        {
            get => _searchPlSpeciesName; 
            set { _searchPlSpeciesName = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl72PlSpecies> _tbl72PlSpeciesList;
        public ObservableCollection<Tbl72PlSpecies> Tbl72PlSpeciessesList
        {
            get => _tbl72PlSpeciesList; 
            set { _tbl72PlSpeciesList = value; RaisePropertyChanged(); }
        }

        public ICollectionView PlSpeciessesView;
        public Tbl72PlSpecies CurrentTbl72PlSpecies => PlSpeciessesView?.CurrentItem as Tbl72PlSpecies;

        #endregion "Public Properties"   
 

 //    Part 12    

 

   }
}   
