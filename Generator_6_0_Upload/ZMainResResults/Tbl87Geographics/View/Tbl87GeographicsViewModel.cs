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

    
using System.Collections.Generic; 
using System.Globalization; 
using System.Windows.Threading;
using WPFUI.Helper.ValidationRules; 
    
         //    Tbl87GeographicsViewModel Skriptdatum:  22.01.2019  10:32    

namespace WPFUI.Views.Database
{     
    
    public class Tbl87GeographicsViewModel : Tbl03RegnumsViewModel
    {     
        
        #region "Private Data Members"

        private readonly AllListVm _allListVm = new AllListVm();
        private DispatcherTimer _timer;
        private readonly Repository<Tbl87Geographic, int> _tbl87GeographicsRepository = new Repository<Tbl87Geographic, int>();  
           
        private readonly Repository<Tbl69FiSpecies, int> _tbl69FiSpeciessesRepository = new Repository<Tbl69FiSpecies, int>();   
           
        private readonly Repository<Tbl72PlSpecies, int> _tbl72PlSpeciessesRepository = new Repository<Tbl72PlSpecies, int>();   
           
        private readonly Repository<Tbl66Genus, int> _tbl66GenussesRepository = new Repository<Tbl66Genus, int>();   
       
        private readonly Repository<Tbl68Speciesgroup, int> _tbl68SpeciesgroupsRepository = new Repository<Tbl68Speciesgroup, int>();
        private readonly Repository<TblCountry, int> _tblCountriesRepository = new Repository<TblCountry, int>();  
    

        #endregion "Private Data Members"            
    
        #region "Constructor"

        public Tbl87GeographicsViewModel()
        {
            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.
            }
            else
            {
                GetValueContinent();
                TblCountriesList = new ObservableCollection<TblCountry>
                    (from y in _tblCountriesRepository.GetAll()
                     orderby y.Name
                     select y);
            }
        }
        private new bool IsInDesignMode { get; set; }

        #endregion "Constructor"           
 

 //    Part 1    

    
        #region "Public Commands Basic Tbl87Geographic"

        private RelayCommand _getGeographicByIdCommand;      
    
        public  ICommand GetGeographicByIdCommand      
    
        {
            get { return _getGeographicByIdCommand ?? (_getGeographicByIdCommand = new RelayCommand(delegate { GetGeographicById(null); })); }   
        }

        private void GetGeographicById(object o)       
        {   
       
            int id;
            if (int.TryParse(SearchGeographicId.ToString(CultureInfo.InvariantCulture), out id))
                Tbl87GeographicsList = new ObservableCollection<Tbl87Geographic> { _tbl87GeographicsRepository.Get(id) };        
Tbl69FiSpeciessesAllList = _allListVm.GetValueTbl69FiSpeciessesAllList();      
  Tbl72PlSpeciessesAllList = _allListVm.GetValueTbl72PlSpeciessesAllList();      
  GeographicsView = CollectionViewSource.GetDefaultView(Tbl87GeographicsList);
            GeographicsView.Refresh();
        }
        //------------------------------------------------------------------------------                
           
        private RelayCommand _addGeographicCommand;           
    
        public ICommand AddGeographicCommand       
    
        {
            get { return _addGeographicCommand ?? (_addGeographicCommand = new RelayCommand(delegate { AddGeographic(null); })); }
        }

        private void AddGeographic(object o)
        {
            Tbl87GeographicsList = new ObservableCollection<Tbl87Geographic>();   
Tbl87GeographicsList.Insert(0, new Tbl87Geographic{ GeographicID = 0 });

            Tbl69FiSpeciessesAllList = _allListVm.GetValueTbl69FiSpeciessesAllList();   
            Tbl72PlSpeciessesAllList = _allListVm.GetValueTbl72PlSpeciessesAllList();      
GeographicsView = CollectionViewSource.GetDefaultView(Tbl87GeographicsList);
            GeographicsView.Refresh();
        }
        //---------------------------------------------------------------------------------------  
           
        private RelayCommand _copyGeographicCommand;              
    
        public ICommand CopyGeographicCommand             
         
        {
            get { return _copyGeographicCommand ?? (_copyGeographicCommand = new RelayCommand(delegate { CopyGeographic(null); })); }
        }

        private void CopyGeographic(object o)
        {
            Tbl87GeographicsList = new ObservableCollection<Tbl87Geographic>();

            var geographic = _tbl87GeographicsRepository.Get(CurrentTbl87Geographic.GeographicID);

            Tbl87GeographicsList.Insert(0, new Tbl87Geographic
            {                 
FiSpeciesID = geographic.FiSpeciesID,              
                            PlSpeciesID = geographic.PlSpeciesID,              
                            Address = geographic.Address,
                            Continent = geographic.Continent,
                            Country = geographic.Country,
                            Http = geographic.Http,
                            Latitude = geographic.Latitude,
                            Longitude = geographic.Longitude,
                            Latitude1 = geographic.Latitude1,
                            Longitude1 = geographic.Longitude1,
                            Latitude2 = geographic.Latitude2,
                            Longitude2 = geographic.Longitude2,
                            Latitude3 = geographic.Latitude3,
                            Longitude3 = geographic.Longitude3,
                            ZoomLevel = geographic.ZoomLevel,
                            Valid = geographic.Valid,
                            ValidYear = geographic.ValidYear,
                            Author = geographic.Author,
                            AuthorYear = geographic.AuthorYear,
                            Info = geographic.Info,
                            Memo = geographic.Memo            
        
            });

            GeographicsView = CollectionViewSource.GetDefaultView(Tbl87GeographicsList);
            GeographicsView.Refresh();
        }
        //---------------------------------------------------------------------------------------                  
           
        private RelayCommand _deleteGeographicCommand;              
    
        public ICommand DeleteGeographicCommand             
                
        {
            get { return _deleteGeographicCommand ?? (_deleteGeographicCommand = new RelayCommand(delegate { DeleteGeographic(null); })); }
        }

        private void DeleteGeographic(object o)
        {
            try
            {
                var geographic = _tbl87GeographicsRepository.Get(CurrentTbl87Geographic.GeographicID);
                if (geographic!= null)
                {
                    if (WpfMessageBox.Show(CultRes.StringsRes.DeleteQuestion1, CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl87Geographic.GeographicID,
                         MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes) 
                    return;

                    _tbl87GeographicsRepository.Delete(geographic);
                    _tbl87GeographicsRepository.Save();

                    WpfMessageBox.Show(CultRes.StringsRes.DeleteSuccess, CurrentTbl87Geographic.GeographicID.ToString(), 
                        MessageBoxButton.OK, MessageBoxImage.Information);

                    if (SearchGeographicId == 0)                   
                        GetConnectedTablesById(o); //refresh doubleClick                                       
                    else
                    {
                        Tbl87GeographicsList = new ObservableCollection<Tbl87Geographic> { _tbl87GeographicsRepository.Get(SearchGeographicId) };                        
                    }    
GeographicsView = CollectionViewSource.GetDefaultView(Tbl87GeographicsList);
                        GeographicsView.Refresh();
                }
                else
                {
                    WpfMessageBox.Show(CultRes.StringsRes.Information, CultRes.StringsRes.DeleteCan + " " + CurrentTbl87Geographic.GeographicID+ " " + CultRes.StringsRes.DeleteCan1,
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
           
        private RelayCommand _saveGeographicCommand;              
     
        public ICommand SaveGeographicCommand             
         
        {
            get { return _saveGeographicCommand ?? (_saveGeographicCommand = new RelayCommand(delegate { SaveGeographic(null); })); }
        }

        private void SaveGeographic(object o)
        {
            try
            {
                var geographic = _tbl87GeographicsRepository.Get(CurrentTbl87Geographic.GeographicID);
                if (CurrentTbl87Geographic == null)              
                    WpfMessageBox.Show(CultRes.StringsRes.Warning, CultRes.StringsRes.DatasetNotExist, 
                        MessageBoxButton.OK, MessageBoxImage.Warning);               
                else
                {
                    if (CurrentTbl87Geographic.GeographicID!= 0)
                    {
                        if (geographic!= null) //update
                        {   
                
                            geographic.FiSpeciesID = CurrentTbl87Geographic.FiSpeciesID;
                            geographic.PlSpeciesID = CurrentTbl87Geographic.PlSpeciesID;
                            geographic.Address = CurrentTbl87Geographic.Address;
                            geographic.Continent = CurrentTbl87Geographic.Continent;
                            geographic.Country = CurrentTbl87Geographic.Country;
                            geographic.Http = CurrentTbl87Geographic.Http;
                            geographic.Latitude = CurrentTbl87Geographic.Latitude;
                            geographic.Longitude = CurrentTbl87Geographic.Longitude;
                            geographic.Latitude1 = CurrentTbl87Geographic.Latitude1;
                            geographic.Longitude1 = CurrentTbl87Geographic.Longitude1;
                            geographic.Latitude2 = CurrentTbl87Geographic.Latitude2;
                            geographic.Longitude2 = CurrentTbl87Geographic.Longitude2;
                            geographic.Latitude3 = CurrentTbl87Geographic.Latitude3;
                            geographic.Longitude3 = CurrentTbl87Geographic.Longitude3;
                            geographic.ZoomLevel = CurrentTbl87Geographic.ZoomLevel;
                            geographic.Valid = CurrentTbl87Geographic.Valid;
                            geographic.ValidYear = CurrentTbl87Geographic.ValidYear;
                            geographic.Author = CurrentTbl87Geographic.Author;
                            geographic.AuthorYear = CurrentTbl87Geographic.AuthorYear;
                            geographic.Info = CurrentTbl87Geographic.Info;
                            geographic.Memo = CurrentTbl87Geographic.Memo;     
                            geographic.Updater = Environment.UserName;
                            geographic.UpdaterDate = DateTime.Now;                  
         
                        }
                    }
                    else
                    {
                        _tbl87GeographicsRepository.Add(new Tbl87Geographic     //add new
                        {   
FiSpeciesID= CurrentTbl87Geographic.FiSpeciesID,              
                            PlSpeciesID= CurrentTbl87Geographic.PlSpeciesID,              
                            CountID = RandomHelper.Randomnumber(),
                            Address = CurrentTbl87Geographic.Address,
                            Continent = CurrentTbl87Geographic.Continent ,
                            Country = CurrentTbl87Geographic.Country ,
                            Http = CurrentTbl87Geographic.Http,
                            Latitude = CurrentTbl87Geographic.Latitude,
                            Longitude = CurrentTbl87Geographic.Longitude,
                            Latitude1 = CurrentTbl87Geographic.Latitude1,
                            Longitude1 = CurrentTbl87Geographic.Longitude1,
                            Latitude2 = CurrentTbl87Geographic.Latitude2,
                            Longitude2 = CurrentTbl87Geographic.Longitude2,
                            Latitude3 = CurrentTbl87Geographic.Latitude3,
                            Longitude3 = CurrentTbl87Geographic.Longitude3,
                            ZoomLevel = CurrentTbl87Geographic.ZoomLevel,
                            Valid = CurrentTbl87Geographic.Valid,
                            ValidYear = CurrentTbl87Geographic.ValidYear,
                            Author = CurrentTbl87Geographic.Author,
                            AuthorYear = CurrentTbl87Geographic.AuthorYear,
                            Info = CurrentTbl87Geographic.Info,
                            Memo = CurrentTbl87Geographic.Memo,
                            Writer = Environment.UserName,
                            WriterDate = DateTime.Now,
                            Updater = Environment.UserName,
                            UpdaterDate = DateTime.Now  
                
                        });
                    }
                    {
                        //FiSpeciesID && PlSpeciesID may be not 0
                        if (CurrentTbl87Geographic.FiSpeciesID == 0 && CurrentTbl87Geographic.PlSpeciesID == 0)
                        {
                            WpfMessageBox.Show(CultRes.StringsRes.RequiredGenealogyConnect, CultRes.StringsRes.RequiredInput,
                                MessageBoxButton.OK, MessageBoxImage.Information);
                            return;
                        }

                        var dataset = new ObservableCollection<Tbl87Geographic>
                        (from a in _tbl87GeographicsRepository.GetAll()
                         where
                         a.GeographicID == CurrentTbl87Geographic.GeographicID &&                
                         a.FiSpeciesID == CurrentTbl87Geographic.FiSpeciesID  &&
                         a.PlSpeciesID == CurrentTbl87Geographic.PlSpeciesID
                         select a);

                        if (dataset.Count != 0 && CurrentTbl87Geographic.GeographicID== 0)  //dataset exist
                        {
                              WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTbl87Geographic.GeographicID.ToString(),
                              MessageBoxButton.OK, MessageBoxImage.Information);
                        }

                        if (dataset.Count == 0 && CurrentTbl87Geographic.GeographicID== 0 ||
                            dataset.Count != 0 && CurrentTbl87Geographic.GeographicID != 0  ||
                            dataset.Count == 0 && CurrentTbl87Geographic.GeographicID != 0) //new dataset and update
                        {
                            if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTbl87Geographic.GeographicID.ToString(),
                                    MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)                            
                                return;                            
                            {
                                _tbl87GeographicsRepository.Save();

                                WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess, CurrentTbl87Geographic.GeographicID.ToString(),
                                    MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                        }  
                
                        if (CurrentTbl87Geographic.GeographicID == 0)                        
                        {
                            Tbl87GeographicsList= new ObservableCollection<Tbl87Geographic>
                              { new ObservableCollection<Tbl87Geographic>
                                  (from x in _tbl87GeographicsRepository.Tables
                                   select x).LastOrDefault()
                              };
                            //last newest Dataset
                        }
                        else
                        {
                            Tbl87GeographicsList = new ObservableCollection<Tbl87Geographic>
                                                  (from x in _tbl87GeographicsRepository.GetAll()
                                                   where x.GeographicID == CurrentTbl87Geographic.GeographicID
                                                   select x);
                        }
                        GeographicsView = CollectionViewSource.GetDefaultView(Tbl87GeographicsList);
                        GeographicsView.Refresh();
               
         
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

    
        #region "Public Commands Connect <== Tbl69FiSpecies"                 

        private RelayCommand _getFiSpeciesByNameOrIdCommand;       
    
        public  ICommand GetFiSpeciesByNameOrIdCommand         
    
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
Tbl69FiSpeciessesList.Insert(0, new Tbl69FiSpecies{ FiSpeciesName = CultRes.StringsRes.DatasetNew });   

            if (Tbl66GenussesAllList == null)
            Tbl66GenussesAllList = _allListVm.GetValueTbl66GenussesAllList();    
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
                
                    if (WpfMessageBox.Show(CultRes.StringsRes.DeleteQuestion1, CultRes.StringsRes.DeleteQuestion  + " " + CurrentTbl69FiSpecies.Tbl66Genusses.GenusName + " " +
                        CurrentTbl69FiSpecies.FiSpeciesName + " " + CurrentTbl69FiSpecies.Subspecies + " " + CurrentTbl69FiSpecies.Divers,
                         MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes) 
                    return;

                    _tbl69FiSpeciessesRepository.Delete(fispecies);
                    _tbl69FiSpeciessesRepository.Save();

                    WpfMessageBox.Show(CultRes.StringsRes.DeleteSuccess, CurrentTbl69FiSpecies.Tbl66Genusses.GenusName + " " + CurrentTbl69FiSpecies.FiSpeciesName + " " + 
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
                
                    WpfMessageBox.Show(CultRes.StringsRes.Information, CultRes.StringsRes.DeleteCan + " " + CurrentTbl69FiSpecies.Tbl66Genusses.GenusName + " " + CurrentTbl69FiSpecies.FiSpeciesName+ " " + 
                       CurrentTbl69FiSpecies.Subspecies+ " " + CurrentTbl69FiSpecies.Divers + " " + CultRes.StringsRes.DeleteCan1,
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
                    WpfMessageBox.Show(CultRes.StringsRes.Warning, CultRes.StringsRes.DatasetNotExist, MessageBoxButton.OK, MessageBoxImage.Warning);               
                else
                {
                    if (CurrentTbl69FiSpecies.FiSpeciesID!= 0)
                    {
                        if (fispecies!= null) //update
                        {   
fispecies.GenusID = CurrentTbl69FiSpecies.GenusID;
                            fispecies.FiSpeciesName= CurrentTbl69FiSpecies.FiSpeciesName;             
         
                            fispecies.SpeciesgroupID = CurrentTbl69FiSpecies.SpeciesgroupID;
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
                        _tbl69FiSpeciessesRepository.Add(new Tbl69FiSpecies     //add new
                        {   
                
							GenusID = CurrentTbl69FiSpecies.GenusID,
							SpeciesgroupID = CurrentTbl69FiSpecies.SpeciesgroupID,
							FiSpeciesName = CurrentTbl69FiSpecies.FiSpeciesName,
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
                        //GenusID may be not 0
                        if (CurrentTbl69FiSpecies.GenusID == 0)
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
                         a.Divers.Trim() == CurrentTbl69FiSpecies.Divers.Trim()     &&          
                         a.GenusID == CurrentTbl69FiSpecies.GenusID     &&         
                         a.SpeciesgroupID == CurrentTbl69FiSpecies.SpeciesgroupID          
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
                            GetConnectedTablesById(o); //refresh doubleClick                                          
                        if (SearchFiSpeciesName == null && CurrentTbl69FiSpecies.FiSpeciesID != 0)  //update                     
                            Tbl69FiSpeciessesList = _allListVm.GetValueTbl69FiSpeciessesList(CurrentTbl69FiSpecies.FiSpeciesID);
                        if (SearchFiSpeciesName != null && CurrentTbl69FiSpecies.FiSpeciesID == 0)  //new Dataset                        
                            GetConnectedTablesById(o); //refresh doubleClick                                          
                        if (SearchFiSpeciesName != null && CurrentTbl69FiSpecies.FiSpeciesID != 0)  //update                     
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
      

 //    Part 3    

             
        #region "Public Commands Connect <== Tbl72PlSpecies"                 

        private RelayCommand _getPlSpeciesByNameOrIdCommand;     
      
        public  ICommand GetPlSpeciesByNameOrIdCommand         
             
        {
            get { return _getPlSpeciesByNameOrIdCommand ?? (_getPlSpeciesByNameOrIdCommand = new RelayCommand(delegate { GetPlSpeciesByNameOrId(null); })); }   
        }

        private void GetPlSpeciesByNameOrId(object o)       
        {   
                
            int id;
            if (int.TryParse(SearchPlSpeciesName, out id))
                Tbl72PlSpeciessesList = new ObservableCollection<Tbl72PlSpecies> { _tbl72PlSpeciessesRepository.Get(id) };
            else
                Tbl72PlSpeciessesList = _allListVm.GetValueTbl72PlSpeciessesList(SearchPlSpeciesName); 

            Tbl66GenussesAllList = _allListVm.GetValueTbl66GenussesAllList();      
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
    
            Tbl66GenussesAllList = _allListVm.GetValueTbl66GenussesAllList();   
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
                                                    
                    if (WpfMessageBox.Show(CultRes.StringsRes.DeleteQuestion1, CultRes.StringsRes.DeleteQuestion  + " " + CurrentTbl72PlSpecies.Tbl66Genusses.GenusName + " " +
                        CurrentTbl72PlSpecies.PlSpeciesName + " " + CurrentTbl72PlSpecies.Subspecies + " " + CurrentTbl72PlSpecies.Divers,
                         MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes) 
                    return;

                    _tbl72PlSpeciessesRepository.Delete(plspecies);
                    _tbl72PlSpeciessesRepository.Save();

                    WpfMessageBox.Show(CultRes.StringsRes.DeleteSuccess, CurrentTbl72PlSpecies.Tbl66Genusses.GenusName + " " + CurrentTbl72PlSpecies.PlSpeciesName + " " + 
                        CurrentTbl72PlSpecies.Subspecies + " " + CurrentTbl72PlSpecies.Divers,
                        MessageBoxButton.OK, MessageBoxImage.Information);  
                                                        
                        if (SearchPlSpeciesName == null)                       
                            GetConnectedTablesById(o); //refresh doubleClick                                          
                        else
                        {
                            Tbl72PlSpeciessesList = new ObservableCollection<Tbl72PlSpecies>
                                                  (from x in _tbl72PlSpeciessesRepository.GetAll()
                                                   where x.PlSpeciesName.StartsWith(SearchPlSpeciesName)   
                                                    
	orderby x.Tbl66Genusses.GenusName, x.PlSpeciesName, x.Subspecies, x.Divers   
                                                         
                                                   select x);

                            PlSpeciessesView = CollectionViewSource.GetDefaultView(Tbl72PlSpeciessesList);
                            PlSpeciessesView.Refresh();
                    }
                }
                else
                {   
                  
                    WpfMessageBox.Show(CultRes.StringsRes.Information, CultRes.StringsRes.DeleteCan + " " + CurrentTbl72PlSpecies.Tbl66Genusses.GenusName + " " + CurrentTbl72PlSpecies.PlSpeciesName+ " " + 
                       CurrentTbl72PlSpecies.Subspecies+ " " + CurrentTbl72PlSpecies.Divers + " " + CultRes.StringsRes.DeleteCan1,
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
                        _tbl72PlSpeciessesRepository.Add(new Tbl72PlSpecies   //add new
                        {   
                  
							GenusID = CurrentTbl72PlSpecies.GenusID,
							SpeciesgroupID = CurrentTbl72PlSpecies.SpeciesgroupID,
							PlSpeciesName = CurrentTbl72PlSpecies.PlSpeciesName,
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
							BasinHeight = CurrentTbl72PlSpecies.BasinHeight,
							PlantLength = CurrentTbl72PlSpecies.PlantLength,
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
							MemoReproduction = CurrentTbl72PlSpecies.MemoReproduction,
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
                         a.Subspecies .Trim() == CurrentTbl72PlSpecies.Subspecies .Trim()    &&             
                         a.Divers.Trim() == CurrentTbl72PlSpecies.Divers.Trim()     &&            
                         a.GenusID == CurrentTbl72PlSpecies.GenusID     &&         
                         a.SpeciesgroupID == CurrentTbl72PlSpecies.SpeciesgroupID          
                        select a);

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

                        if (dataset.Count != 0 && CurrentTbl72PlSpecies.PlSpeciesID == 0)  //dataset exist
                        {
                              WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, 
                              CurrentTbl72PlSpecies.Tbl66Genusses.GenusName + " " + CurrentTbl72PlSpecies.PlSpeciesName + " " + CurrentTbl72PlSpecies.Subspecies + " " + CurrentTbl72PlSpecies.Divers,
                              MessageBoxButton.OK, MessageBoxImage.Information);
                        }  
                                                                                     
                        if (SearchPlSpeciesName == null && CurrentTbl72PlSpecies.PlSpeciesID == 0)  //new Dataset                        
                            GetConnectedTablesById(o); //refresh doubleClick                                          
                        if (SearchPlSpeciesName == null && CurrentTbl72PlSpecies.PlSpeciesID != 0)  //update                     
                            Tbl72PlSpeciessesList = _allListVm.GetValueTbl72PlSpeciessesList(CurrentTbl72PlSpecies.PlSpeciesID);
                        if (SearchPlSpeciesName != null && CurrentTbl72PlSpecies.PlSpeciesID == 0)  //new Dataset                        
                            GetConnectedTablesById(o); //refresh doubleClick                                          
                        if (SearchPlSpeciesName != null && CurrentTbl72PlSpecies.PlSpeciesID != 0)  //update                     
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
        

 //    Part 4    

      

 //    Part 5    


      

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
            SelectedDetailThreeRefTabIndex = 2;  //change to Connect tab

            Tbl69FiSpeciessesList =   new ObservableCollection<Tbl69FiSpecies>
                                                       (from x in _tbl69FiSpeciessesRepository.GetAll()
                                                       where x.FiSpeciesID == CurrentTbl87Geographic.FiSpeciesID
                                                       orderby x.Tbl66Genusses.GenusName, x.FiSpeciesName, x.Subspecies, x.Divers
                                                       select x);

            FiSpeciessesView = CollectionViewSource.GetDefaultView(Tbl69FiSpeciessesList);
            FiSpeciessesView.Refresh();
            //-----------------------------------------------------------------------------------
            Tbl72PlSpeciessesList =   new ObservableCollection<Tbl72PlSpecies>
                                                       (from y in _tbl72PlSpeciessesRepository.GetAll()
                                                       where y.PlSpeciesID == CurrentTbl87Geographic.PlSpeciesID
                                                       orderby y.Tbl66Genusses.GenusName, y.PlSpeciesName, y.Subspecies, y.Divers
                                                       select y);

            PlSpeciessesView = CollectionViewSource.GetDefaultView(Tbl72PlSpeciessesList);
            PlSpeciessesView.Refresh();
            //-----------------------------------------------------------------------------------
            _timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(7) };
            _timer.Start();
            Tbl66GenussesTbl69FiSpeciessesAllList =   new ObservableCollection<Tbl66Genus>
                                                      (from z in _tbl66GenussesRepository.GetAll()
                                                      where z.GenusID == CurrentTbl87Geographic.Tbl69FiSpeciesses.GenusID
                                                      orderby z.GenusName
                                                      select z);
            _timer.Stop();
            _timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(7) };
            _timer.Start();

            //-----------------------------------------------------------------------------------
            Tbl66GenussesTbl72PlSpeciessesAllList =   new ObservableCollection<Tbl66Genus>
                                                      (from z in _tbl66GenussesRepository.GetAll()
                                                      where z.GenusID == CurrentTbl87Geographic.Tbl72PlSpeciesses.GenusID
                                                      orderby z.GenusName
                                                      select z);
            _timer.Stop();
            _timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(7) };
            _timer.Start();

            //-----------------------------------------------------------------------------------
            Tbl68SpeciesgroupsTbl69FiSpeciessesAllList =   new ObservableCollection<Tbl68Speciesgroup>
                                                             (from z in _tbl68SpeciesgroupsRepository.GetAll()
                                                              where z.SpeciesgroupID == CurrentTbl87Geographic.Tbl69FiSpeciesses.SpeciesgroupID
                                                              orderby z.SpeciesgroupName, z.Subspeciesgroup
                                                              select z);
            _timer.Stop();
            _timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(7) };
            _timer.Start();

            //-----------------------------------------------------------------------------------
            Tbl68SpeciesgroupsTbl72PlSpeciessesAllList =   new ObservableCollection<Tbl68Speciesgroup>
                                                              (from z in _tbl68SpeciesgroupsRepository.GetAll()
                                                              where z.SpeciesgroupID == CurrentTbl87Geographic.Tbl72PlSpeciesses.SpeciesgroupID
                                                              orderby z.SpeciesgroupName, z.Subspeciesgroup
                                                              select z);
            _timer.Stop();
        }

        #endregion "Public Commands Connected Tables by DoubleClick"
   
 

 //    Part 10    

 

 //    Part 11    


     
        #region "Public Properties Tbl87Geographic"

        public ICollectionView GeographicsView;
        public Tbl87Geographic CurrentTbl87Geographic => GeographicsView?.CurrentItem as Tbl87Geographic;
        
        private int _searchGeographicId;
        public int SearchGeographicId
        {
            get => _searchGeographicId; 
            set { _searchGeographicId = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl87Geographic> _tbl87GeographicsList;
        public  ObservableCollection<Tbl87Geographic> Tbl87GeographicsList
        {
            get => _tbl87GeographicsList; 
            set { _tbl87GeographicsList = value; RaisePropertyChanged();    }
        }

        private ObservableCollection<Tbl69FiSpecies> _tbl69FiSpeciessesAllList;
        public ObservableCollection<Tbl69FiSpecies> Tbl69FiSpeciessesAllList
        {
            get => _tbl69FiSpeciessesAllList; 
            set { _tbl69FiSpeciessesAllList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl72PlSpecies> _tbl72PlSpeciessesAllList;
        public ObservableCollection<Tbl72PlSpecies> Tbl72PlSpeciessesAllList
        {
            get => _tbl72PlSpeciessesAllList; 
            set { _tbl72PlSpeciessesAllList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl68Speciesgroup> _tbl68SpeciesgroupsTbl69FiSpeciessesAllList;
        public ObservableCollection<Tbl68Speciesgroup> Tbl68SpeciesgroupsTbl69FiSpeciessesAllList
        {
            get => _tbl68SpeciesgroupsTbl69FiSpeciessesAllList; 
            set { _tbl68SpeciesgroupsTbl69FiSpeciessesAllList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl68Speciesgroup> _tbl68SpeciesgroupsTbl72PlSpeciessesAllList;
        public ObservableCollection<Tbl68Speciesgroup> Tbl68SpeciesgroupsTbl72PlSpeciessesAllList
        {
            get => _tbl68SpeciesgroupsTbl72PlSpeciessesAllList; 
            set { _tbl68SpeciesgroupsTbl72PlSpeciessesAllList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl87Geographic> _tbl87GeographicsAllList;
        public ObservableCollection<Tbl87Geographic> Tbl87GeographicsAllList
        {
            get => _tbl87GeographicsAllList; 
            set { _tbl87GeographicsAllList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl66Genus> _tbl66GenussesTbl69FiSpeciessesAllList;
        public  ObservableCollection<Tbl66Genus> Tbl66GenussesTbl69FiSpeciessesAllList
        {
            get => _tbl66GenussesTbl69FiSpeciessesAllList; 
            set { _tbl66GenussesTbl69FiSpeciessesAllList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl66Genus> _tbl66GenussesTbl72PlSpeciessesAllList;
        public  ObservableCollection<Tbl66Genus> Tbl66GenussesTbl72PlSpeciessesAllList
        {
            get => _tbl66GenussesTbl72PlSpeciessesAllList; 
            set { _tbl66GenussesTbl72PlSpeciessesAllList = value; RaisePropertyChanged(); }
        }
        #endregion "Public Properties"
   
       
        #region "Public Properties Tbl69FiSpecies"

        private string _searchFiSpeciesName;
        public  string SearchFiSpeciesName
        {
            get => _searchFiSpeciesName; 
            set { _searchFiSpeciesName = value; RaisePropertyChanged();  }
        }

        public ICollectionView FiSpeciessesView;
        public Tbl69FiSpecies CurrentTbl69FiSpecies => FiSpeciessesView?.CurrentItem as Tbl69FiSpecies;

        private ObservableCollection<Tbl69FiSpecies> _tbl69FiSpeciessesList;
        public  ObservableCollection<Tbl69FiSpecies> Tbl69FiSpeciessesList
        {
            get => _tbl69FiSpeciessesList; 
            set {  _tbl69FiSpeciessesList = value; RaisePropertyChanged();  }
        }

        #endregion "Public Properties"
   
         
        #region "Public Properties Tbl72PlSpecies"

        private string _searchPlSpeciesName;
        public  string SearchPlSpeciesName
        {
            get => _searchPlSpeciesName; 
            set { _searchPlSpeciesName = value; RaisePropertyChanged();  }
        }

        public ICollectionView PlSpeciessesView;
        public Tbl72PlSpecies CurrentTbl72PlSpecies => PlSpeciessesView?.CurrentItem as Tbl72PlSpecies;

        private ObservableCollection<Tbl72PlSpecies> _tbl72PlSpeciessesList;
        public  ObservableCollection<Tbl72PlSpecies> Tbl72PlSpeciessesList
        {
            get => _tbl72PlSpeciessesList; 
            set {  _tbl72PlSpeciessesList = value; RaisePropertyChanged();  }
        }      
   
   
		private ObservableCollection<PhoneValidator> _phoneValidatorsList;
		public ObservableCollection<PhoneValidator> PhoneValidatorsList
		{
	                    get => _phoneValidatorsList; 
	                    set { _phoneValidatorsList = value; RaisePropertyChanged();     }
		}

        #endregion "Public Properties"  

       
        #region "Public Properties Tbl66Genus"

        private ObservableCollection<Tbl66Genus> _tbl66GenussesAllList;
        public  ObservableCollection<Tbl66Genus> Tbl66GenussesAllList
        {
            get => _tbl66GenussesAllList; 
            set { _tbl66GenussesAllList = value; RaisePropertyChanged(); }
        }

        #endregion "Public Properties"
   
   

 //    Part 12    

        

         #region "Private Methods"

       private void GetValueContinent()
        {
            _continents = new List<Continent>()
            {
                new Continent {Name = "Africa"},
                new Continent {Name = "Antarctica"},
                new Continent {Name = "Asia"},
                new Continent {Name = "Australia"},
                new Continent {Name = "Central/South America"},
                new Continent {Name = "Europe"},
                new Continent {Name = "North America/Caribbean"}
            };

            _selectedContinent = new Continent();
        }

        private List<Continent> _continents;
        public List<Continent> Continents
        {
            get => _continents; 
            set { _continents = value; RaisePropertyChanged(); }
        }

        private Continent _selectedContinent;
        public Continent SelectedContinent
        {
            get => _selectedContinent; 
            set { _selectedContinent = value; RaisePropertyChanged(); }
        }

        public class Continent
        {
            public string Name
            {
                get;
                set;
            }
        }

        private ObservableCollection<TblCountry> _tblCountriesList;
        public ObservableCollection<TblCountry> TblCountriesList
        {
            get => _tblCountriesList; 
            set { _tblCountriesList = value; RaisePropertyChanged(); }
        }

        #endregion "Private Methods"   
 

   }
}   
