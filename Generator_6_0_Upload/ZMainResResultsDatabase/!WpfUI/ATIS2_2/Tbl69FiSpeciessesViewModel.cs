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

    
using System.Collections.Generic;
using System.IO;
using System.Windows.Media.Imaging;
using Microsoft.Win32;   
    
         //    Tbl69FiSpeciessesViewModel Skriptdatum:  23.12.2017  10:32    

namespace WPFUI.Views.Database
{     
    
    public partial class Tbl69FiSpeciessesViewModel : Tbl66GenussesViewModel
    {     
         
        #region "Private Data Members"

        private readonly AllListVm _allListVm = new AllListVm();
        private readonly Repository<Tbl69FiSpecies, int> _tbl69FiSpeciessesRepository = new Repository<Tbl69FiSpecies, int>();   
         
        private readonly Repository<Tbl66Genus, int> _tbl66GenussesRepository = new Repository<Tbl66Genus, int>();   
           
        private readonly Repository<Tbl68Speciesgroup, int> _tbl68SpeciesgroupsRepository = new Repository<Tbl68Speciesgroup, int>();   
           
        private readonly Repository<Tbl78Name, int> _tbl78NamesRepository = new Repository<Tbl78Name, int>();   
           
        private readonly Repository<Tbl81Image, int> _tbl81ImagesRepository = new Repository<Tbl81Image, int>();   
           
        private readonly Repository<Tbl84Synonym, int> _tbl84SynonymsRepository = new Repository<Tbl84Synonym, int>();   
           
        private readonly Repository<Tbl87Geographic, int> _tbl87GeographicsRepository = new Repository<Tbl87Geographic, int>();   
           
        private readonly Repository<Tbl90Reference, int> _tbl90ReferencesRepository = new Repository<Tbl90Reference, int>();
        private readonly Repository<Tbl93Comment, int> _tbl93CommentsRepository = new Repository<Tbl93Comment, int>();    

        #endregion "Private Data Members"               
      
        #region "Constructor"

        public Tbl69FiSpeciessesViewModel()
        {
            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.
            }
            else
            {    
    
                GetValueLanguage();
                GetValueContinent();
                GetValueMimeType();
                RegisterCommands();  
     
            }
        }     
        private  new bool IsInDesignMode { get; set; }

        #endregion "Constructor"             
 

 //    Part 1    

             
        #region "Public Commands Basic Tbl69FiSpecies"

        private RelayCommand _getFiSpeciesByNameOrIdCommand;    
                
        public new ICommand GetFiSpeciesByNameOrIdCommand        
    
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
Tbl66GenussesAllList?.Clear();
            Tbl66GenussesAllList = _allListVm.GetValueTbl66GenussesAllList();  
                
            Tbl68SpeciesgroupsAllList?.Clear();
            Tbl68SpeciesgroupsAllList = _allListVm.GetValueTbl68SpeciesgroupsAllList();
            TblCountriesAllList = _allListVm.GetValueTblCountriesAllList();   
FiSpeciessesView = CollectionViewSource.GetDefaultView(Tbl69FiSpeciessesList);
            FiSpeciessesView.Refresh();
        }
        //------------------------------------------------------------------------------------                
           
        private RelayCommand _addFiSpeciesCommand;           
                
        public new ICommand AddFiSpeciesCommand         
    
        {
            get { return _addFiSpeciesCommand ?? (_addFiSpeciesCommand = new RelayCommand(delegate { AddFiSpecies(null); })); }
        }

        private void AddFiSpecies(object o)
        {
            Tbl69FiSpeciessesList = new ObservableCollection<Tbl69FiSpecies>();   
Tbl69FiSpeciessesList.Insert(0, new Tbl69FiSpecies{ FiSpeciesName= CultRes.StringsRes.DatasetNew });  

            Tbl66GenussesAllList?.Clear();
            Tbl66GenussesAllList = _allListVm.GetValueTbl66GenussesAllList();
            Tbl68SpeciesgroupsAllList?.Clear();
            Tbl68SpeciesgroupsAllList = _allListVm.GetValueTbl68SpeciesgroupsAllList();
            TblCountriesAllList?.Clear();
            TblCountriesAllList = _allListVm.GetValueTblCountriesAllList();   
FiSpeciessesView = CollectionViewSource.GetDefaultView(Tbl69FiSpeciessesList);
            FiSpeciessesView.Refresh();
        }
        //------------------------------------------------------------------------------------                
           
        private RelayCommand _copyFiSpeciesCommand;              
                
        public new ICommand CopyFiSpeciesCommand             
           
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
                
        public new ICommand DeleteFiSpeciesCommand             
             
        {
            get { return _deleteFiSpeciesCommand ?? (_deleteFiSpeciesCommand = new RelayCommand(delegate { DeleteFiSpecies(null); })); }
        }

        private void DeleteFiSpecies(object o)
        {
            try
            {
                var fispecies = _tbl69FiSpeciessesRepository.Get(CurrentTbl69FiSpecies.FiSpeciesID);
                if (fispecies != null)
                {   
                
                    if (WpfMessageBox.Show(CultRes.StringsRes.DeleteQuestion1, CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl69FiSpecies.Tbl66Genusses.GenusName + " " +
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
             
                    }
                    FiSpeciessesView = CollectionViewSource.GetDefaultView(Tbl69FiSpeciessesList);
                    FiSpeciessesView.Refresh();
                }
                else
                {    
                
                    WpfMessageBox.Show(CultRes.StringsRes.Information, CultRes.StringsRes.DeleteCan + " " + CurrentTbl69FiSpecies.Tbl66Genusses.GenusName + " " + CurrentTbl69FiSpecies.FiSpeciesName + " " +
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
                
        public new ICommand SaveFiSpeciesCommand             
           
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
                    if (CurrentTbl69FiSpecies.FiSpeciesID != 0)   
                    {
                        if (fispecies != null) //update
                        {  
fispecies.FiSpeciesName = CurrentTbl69FiSpecies.FiSpeciesName;
                            fispecies.GenusID = CurrentTbl69FiSpecies.GenusID;           
                
                            fispecies.Subspecies = CurrentTbl69FiSpecies.Subspecies;
                            fispecies.Divers = CurrentTbl69FiSpecies.Divers;  
                            fispecies.SpeciesgroupID = CurrentTbl69FiSpecies.SpeciesgroupID;    
                
                            fispecies.Valid = CurrentTbl69FiSpecies.Valid;
                            fispecies.ValidYear = CurrentTbl69FiSpecies.ValidYear;
                            fispecies.MemoSpecies = CurrentTbl69FiSpecies.MemoSpecies;
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
                            fispecies.MemoBreeding = CurrentTbl69FiSpecies.MemoBreeding;
                            fispecies.Updater = Environment.UserName;
                            fispecies.UpdaterDate = DateTime.Now;      
         
                        }
                    }
                    else
                    {
                        _tbl69FiSpeciessesRepository.Add(new Tbl69FiSpecies     //add new
                        {   
FiSpeciesName = CurrentTbl69FiSpecies.FiSpeciesName,
                            GenusID = CurrentTbl69FiSpecies.GenusID,    
                
                            Subspecies = CurrentTbl69FiSpecies.Subspecies,
                            Divers = CurrentTbl69FiSpecies.Divers, 
                            SpeciesgroupID = CurrentTbl69FiSpecies.SpeciesgroupID,     
                
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
                
                         a.Subspecies.Trim() == CurrentTbl69FiSpecies.Subspecies.Trim() &&
                         a.Divers.Trim() == CurrentTbl69FiSpecies.Divers.Trim() &&
                         a.GenusID == CurrentTbl69FiSpecies.GenusID &&
                         a.SpeciesgroupID == CurrentTbl69FiSpecies.SpeciesgroupID   
         
                         select a);

                        if (dataset.Count != 0 && CurrentTbl69FiSpecies.FiSpeciesID == 0)  //dataset exist
                        {       
                
                            WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTbl69FiSpecies.Tbl66Genusses.GenusName + " " + CurrentTbl69FiSpecies.FiSpeciesName + " " + CurrentTbl69FiSpecies.Subspecies + " " + CurrentTbl69FiSpecies.Divers,
                            MessageBoxButton.OK, MessageBoxImage.Information);   
             
                        }
                        if (dataset.Count == 0 && CurrentTbl69FiSpecies.FiSpeciesID == 0 ||
                            dataset.Count != 0 && CurrentTbl69FiSpecies.FiSpeciesID != 0 ||
                            dataset.Count == 0 && CurrentTbl69FiSpecies.FiSpeciesID != 0) //new dataset and update
                        {    
                
                            if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTbl69FiSpecies.Tbl66Genusses.GenusName + " " + CurrentTbl69FiSpecies.FiSpeciesName + " " + CurrentTbl69FiSpecies.Subspecies + " " + CurrentTbl69FiSpecies.Divers,
                                    MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
                                return;  
               
                            {
                                _tbl69FiSpeciessesRepository.Save();     
                
                                WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess, CurrentTbl69FiSpecies.Tbl66Genusses.GenusName + " " + CurrentTbl69FiSpecies.FiSpeciesName + " " + CurrentTbl69FiSpecies.Subspecies + " " + CurrentTbl69FiSpecies.Divers,
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

           
        #region "Public Commands Connect <== Tbl66Genus"                 

        private RelayCommand _getGenusByNameOrIdCommand;     
                
        public new ICommand GetGenusByNameOrIdCommand        
           
        {
            get { return _getGenusByNameOrIdCommand ?? (_getGenusByNameOrIdCommand = new RelayCommand(delegate { GetGenusByNameOrId(null); })); }
        }

        private void GetGenusByNameOrId(object o)    
        {

            int id;
            if (int.TryParse(SearchGenusName, out id))
                Tbl66GenussesList = new ObservableCollection<Tbl66Genus> { _tbl66GenussesRepository.Get(id) };
            else
                Tbl66GenussesList = _allListVm.GetValueTbl66GenussesList(SearchGenusName);

            GenussesView = CollectionViewSource.GetDefaultView(Tbl66GenussesList);
            GenussesView.Refresh();
        }

        //------------------------------------------------------------------------------------                
           
        private RelayCommand _addGenusCommand;      
                
        public new ICommand AddGenusCommand    
    
        {
            get { return _addGenusCommand ?? (_addGenusCommand = new RelayCommand(delegate { AddGenus(null); })); }
        }

        private void AddGenus(object o)
        {
            Tbl66GenussesList = new ObservableCollection<Tbl66Genus>();   
Tbl66GenussesList.Insert(0, new Tbl66Genus{ GenusName = CultRes.StringsRes.DatasetNew });   

            Tbl63InfratribussesAllList?.Clear();
            Tbl63InfratribussesAllList = _allListVm.GetValueTbl63InfratribussesAllList();    
GenussesView = CollectionViewSource.GetDefaultView(Tbl66GenussesList);
            GenussesView.Refresh();
        }
        //------------------------------------------------------------------------------------                
           
        private RelayCommand _copyGenusCommand;            
                
        public new ICommand CopyGenusCommand          
         
        {
            get { return _copyGenusCommand ?? (_copyGenusCommand = new RelayCommand(delegate { CopyGenus(null); })); }
        }

        private void CopyGenus(object o)
        {
            Tbl66GenussesList = new ObservableCollection<Tbl66Genus>();

            var genus = _tbl66GenussesRepository.Get(CurrentTbl66Genus.GenusID);

            Tbl66GenussesList.Insert(0, new Tbl66Genus
            {                 
InfratribusID = genus.InfratribusID,
                GenusName = CultRes.StringsRes.DatasetNew,
                Valid = genus.Valid,
                ValidYear = genus.ValidYear,
                Synonym = genus.Synonym,
                Author = genus.Author,
                AuthorYear = genus.AuthorYear,
                Info = genus.Info,
                EngName = genus.EngName,
                GerName = genus.GerName,
                FraName = genus.FraName,
                PorName = genus.PorName,
                Memo = genus.Memo         
        
            });

            GenussesView = CollectionViewSource.GetDefaultView(Tbl66GenussesList);
            GenussesView.Refresh();
        }
        //---------------------------------------------------------------------------------------                  
           
        private RelayCommand _deleteGenusCommand;              
                
        public new ICommand DeleteGenusCommand             
         
        {
            get { return _deleteGenusCommand ?? (_deleteGenusCommand = new RelayCommand(delegate { DeleteGenus(null); })); }
        }

        private void DeleteGenus(object o)
        {
            try
            {
                var genus = _tbl66GenussesRepository.Get(CurrentTbl66Genus.GenusID);
                if (genus!= null)
                {  
         
                    if (WpfMessageBox.Show(CultRes.StringsRes.DeleteQuestion1, CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl66Genus.GenusName,
                         MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes) 
                    return;

                    _tbl66GenussesRepository.Delete(genus);
                    _tbl66GenussesRepository.Save();

                    WpfMessageBox.Show(CultRes.StringsRes.DeleteSuccess, CurrentTbl66Genus.GenusName, 
                        MessageBoxButton.OK, MessageBoxImage.Information);  
         
                        if (SearchGenusName == null)                       
                            GetConnectedTablesById(o); //refresh doubleClick                                          
                        else
                        {
                        Tbl66GenussesList = _allListVm.GetValueTbl66GenussesList(SearchGenusName);  
    
                    }
                            GenussesView = CollectionViewSource.GetDefaultView(Tbl66GenussesList);
                            GenussesView.Refresh();
                }
                else
                {   
    
                    WpfMessageBox.Show(CultRes.StringsRes.Information, CultRes.StringsRes.DeleteCan + " " + CurrentTbl66Genus.GenusName+ " " + CultRes.StringsRes.DeleteCan1,
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
           
        private RelayCommand _saveGenusCommand;              
                
        public new ICommand SaveGenusCommand             
         
        {
            get { return _saveGenusCommand ?? (_saveGenusCommand = new RelayCommand(delegate { SaveGenus(null); })); }
        }

        private void SaveGenus(object o)
        {
            try
            {
                var genus = _tbl66GenussesRepository.Get(CurrentTbl66Genus.GenusID);
                if (CurrentTbl66Genus == null)              
                    WpfMessageBox.Show(CultRes.StringsRes.Warning, CultRes.StringsRes.DatasetNotExist, 
                       MessageBoxButton.OK, MessageBoxImage.Warning);               
                else
                {
                    if (CurrentTbl66Genus.GenusID!= 0)
                    {
                        if (genus!= null) //update
                        {   
genus.GenusName= CurrentTbl66Genus.GenusName; 
                            genus.InfratribusID = CurrentTbl66Genus.InfratribusID;              
genus.Valid = CurrentTbl66Genus.Valid;
                            genus.ValidYear = CurrentTbl66Genus.ValidYear;
                            genus.Synonym = CurrentTbl66Genus.Synonym;
                            genus.Author = CurrentTbl66Genus.Author;
                            genus.AuthorYear = CurrentTbl66Genus.AuthorYear;
                            genus.Info = CurrentTbl66Genus.Info;
                            genus.EngName = CurrentTbl66Genus.EngName;
                            genus.GerName = CurrentTbl66Genus.GerName;
                            genus.FraName = CurrentTbl66Genus.FraName;
                            genus.PorName = CurrentTbl66Genus.PorName;
                            genus.Updater = Environment.UserName;
                            genus.UpdaterDate = DateTime.Now; 
                            genus.Memo = CurrentTbl66Genus.Memo;   
         
                        }
                    }
                    else
                    {
                        _tbl66GenussesRepository.Add(new Tbl66Genus     //add new
                        {   
GenusName= CurrentTbl66Genus.GenusName,              
                            InfratribusID = CurrentTbl66Genus.InfratribusID,     
                            CountID = RandomHelper.Randomnumber(),
                            Valid = CurrentTbl66Genus.Valid,
                            ValidYear = CurrentTbl66Genus.ValidYear,
                            Synonym = CurrentTbl66Genus.Synonym,
                            Author = CurrentTbl66Genus.Author,
                            AuthorYear = CurrentTbl66Genus.AuthorYear,
                            Info = CurrentTbl66Genus.Info,
                            EngName = CurrentTbl66Genus.EngName,
                            GerName = CurrentTbl66Genus.GerName,
                            FraName = CurrentTbl66Genus.FraName,
                            PorName = CurrentTbl66Genus.PorName,
                            Writer = Environment.UserName,
                            WriterDate = DateTime.Now,
                            Updater = Environment.UserName,
                            UpdaterDate = DateTime.Now,
                            Memo = CurrentTbl66Genus.Memo   
          
                        });
                    }
                    {
                        //InfratribusID may be not 0
                        if (CurrentTbl66Genus.InfratribusID == 0)
                        {
                            WpfMessageBox.Show(CultRes.StringsRes.RequiredGenealogyConnect, CultRes.StringsRes.RequiredInput,
                                MessageBoxButton.OK, MessageBoxImage.Information);
                            return;
                        }     
           
                        //check if dataset with Name and vb-name already exist       
                        var dataset = new ObservableCollection<Tbl66Genus>
                        (from a in _tbl66GenussesRepository.GetAll()
                         where
                         a.GenusName.Trim() == CurrentTbl66Genus.GenusName.Trim() &&
                         a.InfratribusID == CurrentTbl66Genus.InfratribusID
                         select a);     
           
                        if (dataset.Count != 0 && CurrentTbl66Genus.GenusID == 0)  //dataset exist
                        {
                            WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTbl66Genus.GenusName,
                            MessageBoxButton.OK, MessageBoxImage.Information);
                        }   
           
                        if (dataset.Count == 0 && CurrentTbl66Genus.GenusID == 0 ||
                            dataset.Count != 0 && CurrentTbl66Genus.GenusID != 0 ||
                            dataset.Count == 0 && CurrentTbl66Genus.GenusID != 0) //new dataset and update
                        {  
           
                            if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTbl66Genus.GenusName,
                                    MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
                                return;
                            {
                                _tbl66GenussesRepository.Save();

                                WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess, CurrentTbl66Genus.GenusName,
                                    MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                        }   
           
                        if (SearchGenusName == null && CurrentTbl66Genus.GenusID == 0)  //new Dataset                        
                            GetConnectedTablesById(o); //refresh doubleClick                                          
                        if (SearchGenusName == null && CurrentTbl66Genus.GenusID != 0)   //update 
                            Tbl66GenussesList = _allListVm.GetValueTbl66GenussesList(CurrentTbl66Genus.GenusID);
                        if (SearchGenusName != null && CurrentTbl66Genus.GenusID == 0)  //new Dataset                        
                            GetConnectedTablesById(o); //refresh doubleClick                                          
                        if (SearchGenusName != null && CurrentTbl66Genus.GenusID != 0)   //update 
                            Tbl66GenussesList = _allListVm.GetValueTbl66GenussesList(CurrentTbl66Genus.GenusID);   
GenussesView = CollectionViewSource.GetDefaultView(Tbl66GenussesList);
                        GenussesView.Refresh();

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

             

        #region "Public Commands Connect <== Tbl68Speciesgroup"               

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

            SpeciesgroupsView = CollectionViewSource.GetDefaultView(Tbl68SpeciesgroupsList);
            SpeciesgroupsView.Refresh();
        }

        //------------------------------------------------------------------------------------                
               
        private RelayCommand _addSpeciesgroupCommand;

        public ICommand AddSpeciesgroupCommand

        {
            get { return _addSpeciesgroupCommand ?? (_addSpeciesgroupCommand = new RelayCommand(delegate { AddSpeciesgroup(null); })); }
        }

        private void AddSpeciesgroup(object o)
        {
            Tbl68SpeciesgroupsList = new ObservableCollection<Tbl68Speciesgroup>();
            Tbl68SpeciesgroupsList.Insert(0, new Tbl68Speciesgroup{ SpeciesgroupName = CultRes.StringsRes.DatasetNew });

            SpeciesgroupsView = CollectionViewSource.GetDefaultView(Tbl68SpeciesgroupsList);
            SpeciesgroupsView.Refresh();
        }
        //------------------------------------------------------------------------------------                            
               
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
                if (speciesgroup != null)
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
                    if (CurrentTbl68Speciesgroup.SpeciesgroupID != 0)   
                    {
                        if (speciesgroup != null) //update
                        {   
             
                            speciesgroup.SpeciesgroupName = CurrentTbl68Speciesgroup.SpeciesgroupName;
                            speciesgroup.Subspeciesgroup = CurrentTbl68Speciesgroup.Subspeciesgroup;
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
                            Subspeciesgroup = CurrentTbl68Speciesgroup.Subspeciesgroup,
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

                        if (dataset.Count != 0 && CurrentTbl68Speciesgroup.SpeciesgroupID == 0)  //dataset exist
                        {
                            WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTbl68Speciesgroup.SpeciesgroupName,
                            MessageBoxButton.OK, MessageBoxImage.Information);
                        }

                        if (dataset.Count == 0 && CurrentTbl68Speciesgroup.SpeciesgroupID == 0 ||
                            dataset.Count != 0 && CurrentTbl68Speciesgroup.SpeciesgroupID != 0 ||
                            dataset.Count == 0 && CurrentTbl68Speciesgroup.SpeciesgroupID != 0) //new dataset and update
                        {
                            if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTbl68Speciesgroup.SpeciesgroupName,
                                    MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
                                return;
                            {
                                _tbl68SpeciesgroupsRepository.Save();

                                WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess, CurrentTbl68Speciesgroup.SpeciesgroupName,
                                    MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                        }

                        if (SearchSpeciesgroupName == null && CurrentTbl68Speciesgroup.SpeciesgroupID == 0)  //new Dataset                        
                            GetConnectedTablesById(o); //refresh doubleClick                                          
                        if (SearchSpeciesgroupName == null && CurrentTbl68Speciesgroup.SpeciesgroupID != 0)   //update 
                            Tbl68SpeciesgroupsList = _allListVm.GetValueTbl68SpeciesgroupsList(CurrentTbl68Speciesgroup.SpeciesgroupID);
                        if (SearchSpeciesgroupName != null && CurrentTbl68Speciesgroup.SpeciesgroupID == 0)  //new Dataset                        
                            GetConnectedTablesById(o); //refresh doubleClick                                          
                        if (SearchSpeciesgroupName != null && CurrentTbl68Speciesgroup.SpeciesgroupID != 0)   //update 
                            Tbl68SpeciesgroupsList = _allListVm.GetValueTbl68SpeciesgroupsList(CurrentTbl68Speciesgroup.SpeciesgroupID);

                        SpeciesgroupsView = CollectionViewSource.GetDefaultView(Tbl68SpeciesgroupsList);
                        SpeciesgroupsView.Refresh();             
             
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
 

 //    Part 4    

           

        #region "Public Commands Connect ==> Tbl78Name"               

        private RelayCommand _getNameByNameOrIdCommand;

        public ICommand GetNameByNameOrIdCommand

        {
            get { return _getNameByNameOrIdCommand ?? (_getNameByNameOrIdCommand = new RelayCommand(delegate { GetNameByNameOrId(null); })); }
        }

        private void GetNameByNameOrId(object o)    
        {

            int id;
            if (int.TryParse(SearchNameName, out id))
                Tbl78NamesList = new ObservableCollection<Tbl78Name> { _tbl78NamesRepository.Get(id) };
            else
                Tbl78NamesList = _allListVm.GetValueTbl78NamesList(SearchNameName);

            NamesView = CollectionViewSource.GetDefaultView(Tbl78NamesList);
            NamesView.Refresh();
        }

        //------------------------------------------------------------------------------------                
                   
        private RelayCommand _addNameCommand;

        public ICommand AddNameCommand

        {
            get { return _addNameCommand ?? (_addNameCommand = new RelayCommand(delegate { AddName(null); })); }
        }

        private void AddName(object o)
        {
            Tbl78NamesList = new ObservableCollection<Tbl78Name>();
            Tbl78NamesList.Insert(0, new Tbl78Name{ NameName = CultRes.StringsRes.DatasetNew });

            Tbl66GenussesAllList?.Clear();
            Tbl66GenussesAllList = _allListVm.GetValueTbl66GenussesAllList();
            Tbl68SpeciesgroupsAllList?.Clear();
            Tbl68SpeciesgroupsAllList = _allListVm.GetValueTbl68SpeciesgroupsAllList();

            NamesView = CollectionViewSource.GetDefaultView(Tbl78NamesList);
            NamesView.Refresh();
        }
        //------------------------------------------------------------------------------------                     
              
        private RelayCommand _copyNameCommand;

        public ICommand CopyNameCommand

        {
            get { return _copyNameCommand ?? (_copyNameCommand = new RelayCommand(delegate { CopyName(null); })); }
        }

        private void CopyName(object o)
        {
            Tbl78NamesList = new ObservableCollection<Tbl78Name>();

            var name = _tbl78NamesRepository.Get(CurrentTbl78Name.NameID);

            Tbl78NamesList.Insert(0, new Tbl78Name
            {    
NameName = CultRes.StringsRes.DatasetNew,
                FiSpeciesID = name.FiSpeciesID,
                PlSpeciesID = 2,
                Valid = name.Valid,
                ValidYear = name.ValidYear,
                Language = name.Language,
                Info = name.Info,
                Memo = name.Memo               
                                   
            });

            NamesView = CollectionViewSource.GetDefaultView(Tbl78NamesList);
            NamesView.Refresh();
        }
        //---------------------------------------------------------------------------------------    
                                           
        private RelayCommand _deleteNameCommand;              
                                           
        public ICommand DeleteNameCommand             
                                                
        {
            get { return _deleteNameCommand ?? (_deleteNameCommand = new RelayCommand(delegate { DeleteName(null); })); }
        }

        private void DeleteName(object o)
        {
            try
            {
                var name = _tbl78NamesRepository.Get(CurrentTbl78Name.NameID);
                if (name!= null)
                {  
                                                    
                    if (WpfMessageBox.Show(CultRes.StringsRes.DeleteQuestion1, CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl78Name.NameName,
                         MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes) 
                    return;

                    _tbl78NamesRepository.Delete(name);
                    _tbl78NamesRepository.Save();

                    WpfMessageBox.Show(CultRes.StringsRes.DeleteSuccess, CurrentTbl78Name.NameName,
                       MessageBoxButton.OK, MessageBoxImage.Information);  
                                                        
                        if (SearchNameName == null)                       
                            GetConnectedTablesById(o); //refresh doubleClick                                          
                        else
                        {
                        Tbl78NamesList = _allListVm.GetValueTbl78NamesList(SearchNameName);  
                                                         
                    }
                            NamesView = CollectionViewSource.GetDefaultView(Tbl78NamesList);
                            NamesView.Refresh();
                }
                else
                {   
                                                          
                    WpfMessageBox.Show(CultRes.StringsRes.Information, CultRes.StringsRes.DeleteCan + " " + CurrentTbl78Name.NameName+ " " + CultRes.StringsRes.DeleteCan1,
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
               
        private RelayCommand _saveNameCommand;

        public ICommand SaveNameCommand

        {
            get { return _saveNameCommand ?? (_saveNameCommand = new RelayCommand(delegate { SaveName(null); })); }
        }

        private void SaveName(object o)
        {
            try
            {
                var name = _tbl78NamesRepository.Get(CurrentTbl78Name.NameID);
                if (CurrentTbl78Name == null)
                    WpfMessageBox.Show(CultRes.StringsRes.Warning, CultRes.StringsRes.DatasetNotExist,
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                else
                {
                    if (CurrentTbl78Name.NameID != 0)   
                    {
                        if (name != null) //update
                        {   
name.NameName = CurrentTbl78Name.NameName;
                            name.FiSpeciesID = CurrentTbl69FiSpecies.FiSpeciesID;
                            name.PlSpeciesID = 2;
                            name.Valid = CurrentTbl78Name.Valid;
                            name.ValidYear = CurrentTbl78Name.ValidYear;
                            name.Language = CurrentTbl78Name.Language;
                            name.Info = CurrentTbl78Name.Info;
                            name.Updater = Environment.UserName;
                            name.UpdaterDate = DateTime.Now;
                            name.Memo = CurrentTbl78Name.Memo;
                        }
                    }
                    else
                    {
                        _tbl78NamesRepository.Add(new Tbl78Name     //add new
                        {
                            NameName = CurrentTbl78Name.NameName,
                            FiSpeciesID = CurrentTbl69FiSpecies.FiSpeciesID,
                            PlSpeciesID = 2,
                            CountID = RandomHelper.Randomnumber(),
                            Valid = CurrentTbl78Name.Valid,
                            ValidYear = CurrentTbl78Name.ValidYear,
                            Language = CurrentTbl78Name.Language,
                            Info = CurrentTbl78Name.Info,
                            Writer = Environment.UserName,
                            WriterDate = DateTime.Now,
                            Updater = Environment.UserName,
                            UpdaterDate = DateTime.Now,
                            Memo = CurrentTbl78Name.Memo           
             
                        });
                    }
                    {
                        //check if dataset with Name and vb-name already exist       
                        var dataset = new ObservableCollection<Tbl78Name>
                        (from a in _tbl78NamesRepository.GetAll()
                         where
                         a.NameName.Trim() == CurrentTbl78Name.NameName.Trim() &&
                         a.FiSpeciesID == CurrentTbl69FiSpecies.FiSpeciesID &&
                         a.PlSpeciesID == 2
                         select a);             
        
                        if (dataset.Count != 0 && CurrentTbl78Name.NameID == 0)  //dataset exist
                        {
                            WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTbl78Name.NameName,
                            MessageBoxButton.OK, MessageBoxImage.Information);
                        }      
        
                        if (dataset.Count == 0 && CurrentTbl78Name.NameID == 0 ||
                            dataset.Count != 0 && CurrentTbl78Name.NameID != 0 ||
                            dataset.Count == 0 && CurrentTbl78Name.NameID != 0) //new dataset and update
                        { 
        
                            if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTbl78Name.NameName,
                                    MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
                                return;
                            {
                                _tbl78NamesRepository.Save();

                                WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess, CurrentTbl78Name.NameName,
                                    MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                        } 
        
                        Tbl78NamesList = _allListVm.GetValueTbl78NamesList(CurrentTbl69FiSpecies.FiSpeciesID);           
NamesView = CollectionViewSource.GetDefaultView(Tbl78NamesList);
                        NamesView.Refresh();
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

             
        #region "Public Commands Connect ==> Tbl81Image"                 

        private RelayCommand _getImageByIdCommand;     
      
        public  ICommand GetImageByIdCommand       
    
        {
            get { return _getImageByIdCommand ?? (_getImageByIdCommand = new RelayCommand(delegate { GetImageById(null); })); }   
        }

        private void GetImageById(object o)       
        {   
Tbl81ImagesList = new ObservableCollection<Tbl81Image> { _tbl81ImagesRepository.Get(SearchImageId) };      
  ImagesView = CollectionViewSource.GetDefaultView(Tbl81ImagesList);
            ImagesView.Refresh();
        }
        //------------------------------------------------------------------------------     
                      
        private RelayCommand _addImageCommand;      
                       
        public ICommand AddImageCommand    
                        
        {
            get { return _addImageCommand ?? (_addImageCommand = new RelayCommand(delegate { AddImage(null); })); }
        }

        private void AddImage(object o)
        {
            Tbl81ImagesList = new ObservableCollection<Tbl81Image>();   
  Tbl81ImagesList.Insert(0, new Tbl81Image{ Info = CultRes.StringsRes.DatasetNew });    
  
            Tbl66GenussesAllList?.Clear();
            Tbl66GenussesAllList = _allListVm.GetValueTbl66GenussesAllList();
            Tbl68SpeciesgroupsAllList?.Clear();
            Tbl68SpeciesgroupsAllList = _allListVm.GetValueTbl68SpeciesgroupsAllList();  
ImagesView = CollectionViewSource.GetDefaultView(Tbl81ImagesList);
            ImagesView.Refresh();
        }
        //---------------------------------------------------------------------------------------  
                              
        private RelayCommand _copyImageCommand;            
                              
        public ICommand CopyImageCommand          
                                 
        {
            get { return _copyImageCommand ?? (_copyImageCommand = new RelayCommand(delegate { CopyImage(null); })); }
        }

        private void CopyImage(object o)
        {
            Tbl81ImagesList = new ObservableCollection<Tbl81Image>();

            var image = _tbl81ImagesRepository.Get(CurrentTbl81Image.ImageID);

            Tbl81ImagesList.Insert(0, new Tbl81Image
            {                 
                  
                FiSpeciesID = image.FiSpeciesID,
                PlSpeciesID = 2,
                Valid = image.Valid,
                ValidYear = image.ValidYear,
                Info = image.Info,
                ShotDate = image.ShotDate,
                ImageData = image.ImageData,
                ImageMimeType = image.ImageMimeType,
                Filestream = image.Filestream,
                FilestreamID = image.FilestreamID,
                Memo = image.Memo      
                                   
            });

            ImagesView = CollectionViewSource.GetDefaultView(Tbl81ImagesList);
            ImagesView.Refresh();
        }
        //---------------------------------------------------------------------------------------    
                                           
        private RelayCommand _deleteImageCommand;              
                                           
        public ICommand DeleteImageCommand             
                                                
        {
            get { return _deleteImageCommand ?? (_deleteImageCommand = new RelayCommand(delegate { DeleteImage(null); })); }
        }

        private void DeleteImage(object o)
        {
            try
            {
                var image = _tbl81ImagesRepository.Get(CurrentTbl81Image.ImageID);
                if (image!= null)
                {  
                                                    
                    if (WpfMessageBox.Show(CultRes.StringsRes.DeleteQuestion1, CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl81Image.ImageID,
                         MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes) 
                    return;

                    _tbl81ImagesRepository.Delete(image);
                    _tbl81ImagesRepository.Save();

                    WpfMessageBox.Show(CultRes.StringsRes.DeleteSuccess, CurrentTbl81Image.ImageID.ToString(),
                       MessageBoxButton.OK, MessageBoxImage.Information);  
                                                        
                        if (SearchImageId == 0)                       
                            GetConnectedTablesById(o); //refresh doubleClick                                          
                        else
                        {
                            Tbl81ImagesList = _allListVm.GetValueTbl81ImagesList(SearchImageId);  
  ImagesView = CollectionViewSource.GetDefaultView(Tbl81ImagesList);
                            ImagesView.Refresh();
                    }
                }
                else
                {   
                                                          
                    WpfMessageBox.Show(CultRes.StringsRes.Information, CultRes.StringsRes.DeleteCan + " " + CurrentTbl81Image.ImageID + " " + CultRes.StringsRes.DeleteCan1,
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
                                                               
        private RelayCommand _saveImageCommand;              
                                                                 
        public ICommand SaveImageCommand             
                                                                     
        {
            get { return _saveImageCommand ?? (_saveImageCommand = new RelayCommand(delegate { SaveImage(null); })); }
        }

        private void SaveImage(object o)
        {
            try
            {
                var image = _tbl81ImagesRepository.Get(CurrentTbl81Image.ImageID);
                if (CurrentTbl81Image == null)                
                    WpfMessageBox.Show(CultRes.StringsRes.Warning, CultRes.StringsRes.DatasetNotExist, MessageBoxButton.OK, MessageBoxImage.Warning);              
                else
                {
                    if (CurrentTbl81Image.ImageID!= 0)
                    {
                        if (image!= null) //update
                        {   
  image.FiSpeciesID = CurrentTbl69FiSpecies.FiSpeciesID;         
                            image.PlSpeciesID = 2;
                            image.Valid = CurrentTbl81Image.Valid;
                            image.ValidYear = CurrentTbl81Image.ValidYear;
                            image.ShotDate = CurrentTbl81Image.ShotDate;
                            image.Info = CurrentTbl81Image.Info;
                            image.Memo = CurrentTbl81Image.Memo;
                            image.ImageData = CurrentTbl81Image.ImageData;
                            image.ImageMimeType = CurrentTbl81Image.ImageMimeType;
                           if (SelectedPath != null)  image.Filestream = LoadImageData(SelectedPath);
                            image.Updater = Environment.UserName;
                            image.UpdaterDate = DateTime.Now;    
                                                                          
                        }
                    }
                    else
                    {
                        _tbl81ImagesRepository.Add(new Tbl81Image     //add new
                        {   
                  
                            FiSpeciesID = CurrentTbl69FiSpecies.FiSpeciesID,
                            PlSpeciesID = 2,
                            CountID = RandomHelper.Randomnumber(),
                            Valid = CurrentTbl81Image.Valid,
                            ValidYear = CurrentTbl81Image.ValidYear,
                            ShotDate = CurrentTbl81Image.ShotDate,
                            Info = CurrentTbl81Image.Info,
                            Memo = CurrentTbl81Image.Memo,
                            ImageData = CurrentTbl81Image.ImageData, //empty
                            ImageMimeType = CurrentTbl81Image.ImageMimeType,
                            Filestream = LoadImageData(SelectedPath),
                            FilestreamID = Guid.NewGuid(), 
                            Writer = Environment.UserName,
                            WriterDate = DateTime.Now,
                            Updater = Environment.UserName,
                            UpdaterDate = DateTime.Now  
                                                                                  
                        });
                    }
                    {
                        
                            if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTbl81Image.ImageID.ToString(),
                                    MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)                            
                                return;                            
                            {
                                _tbl81ImagesRepository.Save();

                                WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess, CurrentTbl81Image.ImageID.ToString(),
                                    MessageBoxButton.OK, MessageBoxImage.Information);
                            }       
                                                                                       
                        Tbl81ImagesList = _allListVm.GetValueTbl81ImagesList(CurrentTbl69FiSpecies.FiSpeciesID);

                        ImagesView = CollectionViewSource.GetDefaultView(Tbl81ImagesList);
                        ImagesView.Refresh();  
                                                                                                             
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

        private static byte[] LoadImageData(string filePath)
        {
            var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            var br = new BinaryReader(fs);
            var imageBytes = br.ReadBytes((int)fs.Length);
            br.Close();
            fs.Close();
            return imageBytes;
        }
        #endregion "Public Commands"  
        

 //    Part 6    

             
        #region "Public Commands Connect ==> Tbl84Synonym"                 

        private RelayCommand _getSynonymByNameOrIdCommand;     
               
        public ICommand GetSynonymByNameOrIdCommand   
               
        {
            get { return _getSynonymByNameOrIdCommand ?? (_getSynonymByNameOrIdCommand = new RelayCommand(delegate { GetSynonymByNameOrId(null); })); }   
        }

        private void GetSynonymByNameOrId(object o)       
        {   
                
            int id;
            if (int.TryParse(SearchSynonymName, out id))
                Tbl84SynonymsList = new ObservableCollection<Tbl84Synonym> { _tbl84SynonymsRepository.Get(id) };
            else
                Tbl84SynonymsList = _allListVm.GetValueTbl84SynonymsList(SearchSynonymName);      
  SynonymsView = CollectionViewSource.GetDefaultView(Tbl84SynonymsList);
            SynonymsView.Refresh();
        }
        //------------------------------------------------------------------------------     
                      
        private RelayCommand _addSynonymCommand;      
                       
        public ICommand AddSynonymCommand    
                        
        {
            get { return _addSynonymCommand ?? (_addSynonymCommand = new RelayCommand(delegate { AddSynonym(null); })); }
        }

        private void AddSynonym(object o)
        {
            Tbl84SynonymsList = new ObservableCollection<Tbl84Synonym>();   
                       
            Tbl84SynonymsList.Insert(0, new Tbl84Synonym { SynonymName = CultRes.StringsRes.DatasetNew });

            Tbl66GenussesAllList?.Clear();
            Tbl66GenussesAllList = _allListVm.GetValueTbl66GenussesAllList();
            Tbl68SpeciesgroupsAllList?.Clear();
            Tbl68SpeciesgroupsAllList = _allListVm.GetValueTbl68SpeciesgroupsAllList();                 
SynonymsView = CollectionViewSource.GetDefaultView(Tbl84SynonymsList);
            SynonymsView.Refresh();
        }
        //---------------------------------------------------------------------------------------  
                              
        private RelayCommand _copySynonymCommand;            
                              
        public ICommand CopySynonymCommand          
                                 
        {
            get { return _copySynonymCommand ?? (_copySynonymCommand = new RelayCommand(delegate { CopySynonym(null); })); }
        }

        private void CopySynonym(object o)
        {
            Tbl84SynonymsList = new ObservableCollection<Tbl84Synonym>();

            var synonym = _tbl84SynonymsRepository.Get(CurrentTbl84Synonym.SynonymID);

            Tbl84SynonymsList.Insert(0, new Tbl84Synonym
            {                 
                  
                SynonymName = CultRes.StringsRes.DatasetNew,
                FiSpeciesID = synonym.FiSpeciesID,
                PlSpeciesID = 2,
                Valid = synonym.Valid,
                ValidYear = synonym.ValidYear,
                Author = synonym.Author,
                AuthorYear = synonym.AuthorYear,
                Info = synonym.Info,
                Memo = synonym.Memo      
                                   
            });

            SynonymsView = CollectionViewSource.GetDefaultView(Tbl84SynonymsList);
            SynonymsView.Refresh();
        }
        //---------------------------------------------------------------------------------------    
                                           
        private RelayCommand _deleteSynonymCommand;              
                                           
        public ICommand DeleteSynonymCommand             
                                                
        {
            get { return _deleteSynonymCommand ?? (_deleteSynonymCommand = new RelayCommand(delegate { DeleteSynonym(null); })); }
        }

        private void DeleteSynonym(object o)
        {
            try
            {
                var synonym = _tbl84SynonymsRepository.Get(CurrentTbl84Synonym.SynonymID);
                if (synonym!= null)
                {  
                                                    
                    if (WpfMessageBox.Show(CultRes.StringsRes.DeleteQuestion1, CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl84Synonym.SynonymName,
                         MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes) 
                     return;

                    _tbl84SynonymsRepository.Delete(synonym);
                    _tbl84SynonymsRepository.Save();

                    WpfMessageBox.Show(CultRes.StringsRes.DeleteSuccess, CurrentTbl84Synonym.SynonymName,
                       MessageBoxButton.OK, MessageBoxImage.Information);  
                                                        
                        if (SearchSynonymName == null)                       
                            GetConnectedTablesById(o); //refresh doubleClick                                          
                        else
                        {
                            Tbl84SynonymsList = _allListVm.GetValueTbl84SynonymsList(SearchSynonymName);     
  SynonymsView = CollectionViewSource.GetDefaultView(Tbl84SynonymsList);
                            SynonymsView.Refresh();
                    }
                }
                else
                {   
                                                          
                    WpfMessageBox.Show(CultRes.StringsRes.Information, CultRes.StringsRes.DeleteCan + " " + CurrentTbl84Synonym.SynonymName+ " " + CultRes.StringsRes.DeleteCan1,
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
                                                               
        private RelayCommand _saveSynonymCommand;              
                                                                 
        public ICommand SaveSynonymCommand             
                                                                     
        {
            get { return _saveSynonymCommand ?? (_saveSynonymCommand = new RelayCommand(delegate { SaveSynonym(null); })); }   
        }

        private void SaveSynonym(object o)
        {
            try
            {
                var synonym = _tbl84SynonymsRepository.Get(CurrentTbl84Synonym.SynonymID);
                if (CurrentTbl84Synonym == null)               
                    WpfMessageBox.Show(CultRes.StringsRes.Warning, CultRes.StringsRes.DatasetNotExist, MessageBoxButton.OK, MessageBoxImage.Warning);               
                else
                {
                    if (CurrentTbl84Synonym.SynonymID!= 0)
                    {
                        if (synonym!= null) //update
                        {   
  synonym.FiSpeciesID = CurrentTbl69FiSpecies.FiSpeciesID;
                            synonym.PlSpeciesID = 2;
                            synonym.SynonymName = CurrentTbl84Synonym.SynonymName;
                            synonym.Valid = CurrentTbl84Synonym.Valid;
                            synonym.ValidYear = CurrentTbl84Synonym.ValidYear;
                            synonym.Author = CurrentTbl84Synonym.Author;
                            synonym.AuthorYear = CurrentTbl84Synonym.AuthorYear;
                            synonym.Info = CurrentTbl84Synonym.Info;
                            synonym.Memo = CurrentTbl84Synonym.Memo;                                                       
                            synonym.Updater = Environment.UserName;
                            synonym.UpdaterDate = DateTime.Now;    
                                                                            
                        }
                    }
                    else
                    {
                        _tbl84SynonymsRepository.Add(new Tbl84Synonym   //add new
                        {   
                  
                            FiSpeciesID = CurrentTbl69FiSpecies.FiSpeciesID,
                            PlSpeciesID = 2,
                            SynonymName = CurrentTbl84Synonym.SynonymName,
                            CountID = RandomHelper.Randomnumber(),
                            Valid = CurrentTbl84Synonym.Valid,
                            ValidYear = CurrentTbl84Synonym.ValidYear,
                            Author = CurrentTbl84Synonym.Author,
                            AuthorYear = CurrentTbl84Synonym.AuthorYear,
                            Info = CurrentTbl84Synonym.Info,
                            Writer = Environment.UserName,
                            WriterDate = DateTime.Now,
                            Updater = Environment.UserName,
                            UpdaterDate = DateTime.Now,
                            Memo = CurrentTbl84Synonym.Memo  
                                                                                  
                        });
                    }
                    {
                         //check if dataset with Name and vb-name already exist
                        var dataset = new ObservableCollection<Tbl84Synonym>
                        (from a in _tbl84SynonymsRepository.GetAll()
                         where
                         a.SynonymName.Trim() == CurrentTbl84Synonym.SynonymName.Trim() &&                
                         a.FiSpeciesID == CurrentTbl69FiSpecies.FiSpeciesID &&
                         a.PlSpeciesID == 2
                         select a);

                        if (dataset.Count != 0 && CurrentTbl84Synonym.SynonymID == 0)  //dataset exist
                        {
                              WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTbl84Synonym.SynonymName,
                              MessageBoxButton.OK, MessageBoxImage.Information);
                        }

                        if (dataset.Count == 0 && CurrentTbl84Synonym.SynonymID== 0 ||
                            dataset.Count != 0 && CurrentTbl84Synonym.SynonymID != 0  ||
                            dataset.Count == 0 && CurrentTbl84Synonym.SynonymID != 0) //new dataset and update
                        {
                            if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTbl84Synonym.SynonymName,
                                    MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)                            
                                return;                            
                            {
                                _tbl84SynonymsRepository.Save();

                                WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess, CurrentTbl84Synonym.SynonymName,
                                    MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                        }  

                        Tbl84SynonymsList = _allListVm.GetValueTbl84SynonymsList(CurrentTbl69FiSpecies.FiSpeciesID);

                        SynonymsView = CollectionViewSource.GetDefaultView(Tbl84SynonymsList);
                        SynonymsView.Refresh();  
                                                                                          
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
        

 //    Part 7    

             
        #region "Public Commands Connect ==> Tbl87Geographic"                 

        private RelayCommand _getGeographicByIdCommand;     
               
        public ICommand GetGeographicByIdCommand   
               
        {
            get { return _getGeographicByIdCommand ?? (_getGeographicByIdCommand = new RelayCommand(delegate { GetGeographicById(null); })); }   
        }

        private void GetGeographicById(object o)       
        {   
  Tbl87GeographicsList = new ObservableCollection<Tbl87Geographic> { _tbl87GeographicsRepository.Get(SearchGeographicId) };       
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
  Tbl87GeographicsList.Insert(0, new Tbl87Geographic{ Info = CultRes.StringsRes.DatasetNew });   

            Tbl69FiSpeciessesAllList?.Clear();
            Tbl66GenussesAllList?.Clear();
            Tbl66GenussesAllList = _allListVm.GetValueTbl66GenussesAllList();
            Tbl68SpeciesgroupsAllList?.Clear();
            Tbl68SpeciesgroupsAllList = _allListVm.GetValueTbl68SpeciesgroupsAllList();    
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
                PlSpeciesID = 2,
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

                    WpfMessageBox.Show(CultRes.StringsRes.DeleteSuccess, CurrentTbl87Geographic.GeographicID.ToString(), MessageBoxButton.OK, MessageBoxImage.Information);  
                                                        
                        if (SearchGeographicId == 0)                       
                            GetConnectedTablesById(o); //refresh doubleClick                                          
                        else
                        {
                            Tbl87GeographicsList = _allListVm.GetValueTbl87GeographicsList(SearchGeographicId);      
  GeographicsView = CollectionViewSource.GetDefaultView(Tbl87GeographicsList);
                            GeographicsView.Refresh();
                    }
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
                    WpfMessageBox.Show(CultRes.StringsRes.Warning, CultRes.StringsRes.DatasetNotExist, MessageBoxButton.OK, MessageBoxImage.Warning);                
                else
                {
                    if (CurrentTbl87Geographic.GeographicID!= 0)
                    {
                        if (geographic!= null) //update
                        {   
               
                            geographic.FiSpeciesID = CurrentTbl69FiSpecies.FiSpeciesID;
                            geographic.PlSpeciesID = 2;
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
                            geographic.Info = CurrentTbl87Geographic.Info;
                            geographic.Memo = CurrentTbl87Geographic.Memo;                                                    
                            geographic.Updater = Environment.UserName;
                            geographic.UpdaterDate = DateTime.Now;    
                                                                            
                        }
                    }
                    else
                    {
                        _tbl87GeographicsRepository.Add(new Tbl87Geographic    //add new
                        {   
                  
                            FiSpeciesID = CurrentTbl69FiSpecies.FiSpeciesID,
                            PlSpeciesID = 2,
                            CountID = RandomHelper.Randomnumber(),
                            Address = CurrentTbl87Geographic.Address,
                            Continent = CurrentTbl87Geographic.Continent,
                            Country = CurrentTbl87Geographic.Country,
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
                            Info = CurrentTbl87Geographic.Info,
                            Writer = Environment.UserName,
                            WriterDate = DateTime.Now,
                            Updater = Environment.UserName,
                            UpdaterDate = DateTime.Now,
                            Memo = CurrentTbl87Geographic.Memo   
                                                                                  
                        });
                    }
                    {
                            if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTbl87Geographic.GeographicID.ToString(),
                                    MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)                            
                                return;                            
                            {
                                _tbl87GeographicsRepository.Save();

                                WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess, CurrentTbl87Geographic.GeographicID.ToString(),
                                    MessageBoxButton.OK, MessageBoxImage.Information);
                            }  
                                                                                       
                        Tbl87GeographicsList = _allListVm.GetValueTbl87GeographicsList(CurrentTbl69FiSpecies.FiSpeciesID);

                        GeographicsView = CollectionViewSource.GetDefaultView(Tbl87GeographicsList);
                        GeographicsView.Refresh();  
                                                                                          
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

           Tbl69FiSpeciessesAllList?.Clear();
           Tbl69FiSpeciessesAllList = _allListVm.GetValueTbl69FiSpeciessesAllList();
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
FiSpeciesID = refAuthor.FiSpeciesID,              
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
         
                            refAuthor.FiSpeciesID = CurrentTbl69FiSpecies.FiSpeciesID;  
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
FiSpeciesID = CurrentTbl69FiSpecies.FiSpeciesID,              
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
                        //FiSpeciesID may be not 0
                        if (CurrentTbl69FiSpecies.FiSpeciesID == 0 || CurrentTbl90RefAuthor.RefAuthorID == 0)
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
                         a.FiSpeciesID == CurrentTbl69FiSpecies.FiSpeciesID &&
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
                   
                        Tbl90RefAuthorsList = new ObservableCollection<Tbl90Reference>
                        (from refAu in _tbl90ReferencesRepository.GetAll()
                            where refAu.FiSpeciesID == CurrentTbl69FiSpecies.FiSpeciesID
                                  && refAu.RefExpertID == null
                                  && refAu.RefSourceID == null
                            orderby refAu.Tbl90RefAuthors.RefAuthorName, refAu.Tbl90RefAuthors.BookName, refAu.Tbl90RefAuthors.Page1
                            select refAu);

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

            Tbl69FiSpeciessesAllList?.Clear();  
            Tbl69FiSpeciessesAllList = _allListVm.GetValueTbl69FiSpeciessesAllList();
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
FiSpeciesID = refSource.FiSpeciesID,              
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
         
                            refSource.FiSpeciesID = CurrentTbl69FiSpecies.FiSpeciesID;            
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
FiSpeciesID = CurrentTbl69FiSpecies.FiSpeciesID,              
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
                        //FiSpeciesID may be not 0
                        if (CurrentTbl69FiSpecies.FiSpeciesID == 0 || CurrentTbl90RefSource.RefSourceID == 0)
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
                         a.FiSpeciesID == CurrentTbl69FiSpecies.FiSpeciesID &&
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
             
                        Tbl90RefSourcesList = new ObservableCollection<Tbl90Reference>
                        (from refSo in _tbl90ReferencesRepository.GetAll()
                            where refSo.FiSpeciesID == CurrentTbl69FiSpecies.FiSpeciesID
                                  && refSo.RefExpertID == null
                                  && refSo.RefAuthorID == null
                            orderby refSo.Tbl90RefSources.RefSourceName
                            select refSo);
 
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

            Tbl69FiSpeciessesAllList?.Clear();  
            Tbl69FiSpeciessesAllList = _allListVm.GetValueTbl69FiSpeciessesAllList();
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
FiSpeciesID = refExpert.FiSpeciesID,              
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
      
                            refExpert.FiSpeciesID = CurrentTbl69FiSpecies.FiSpeciesID;           
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
FiSpeciesID = CurrentTbl69FiSpecies.FiSpeciesID,              
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
                        //FiSpeciesID may be not 0
                        if (CurrentTbl69FiSpecies.FiSpeciesID == 0 || CurrentTbl90RefExpert.RefExpertID == 0)   
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
                         a.FiSpeciesID == CurrentTbl69FiSpecies.FiSpeciesID &&
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
           
                        Tbl90RefExpertsList = new ObservableCollection<Tbl90Reference>
                        (from refEx in _tbl90ReferencesRepository.GetAll()
                            where refEx.FiSpeciesID == CurrentTbl69FiSpecies.FiSpeciesID
                                  && refEx.RefAuthorID == null
                                  && refEx.RefSourceID == null
                            orderby refEx.Tbl90RefExperts.RefExpertName
                            select refEx);   

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

            Tbl69FiSpeciessesAllList?.Clear();  
            Tbl69FiSpeciessesAllList = _allListVm.GetValueTbl69FiSpeciessesAllList();      
    

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
FiSpeciesID = comment.FiSpeciesID,              
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
      
                            comment.FiSpeciesID = CurrentTbl69FiSpecies.FiSpeciesID;            
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
FiSpeciesID = CurrentTbl69FiSpecies.FiSpeciesID,              
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
                        //FiSpeciesID may be not 0
                        if (CurrentTbl69FiSpecies.FiSpeciesID == 0)
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
                         a.FiSpeciesID == CurrentTbl69FiSpecies.FiSpeciesID
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
                 
                        Tbl93CommentsList = new ObservableCollection<Tbl93Comment>
                        (from comm in _tbl93CommentsRepository.GetAll()
                            where comm.FiSpeciesID == CurrentTbl69FiSpecies.FiSpeciesID
                            orderby comm.Info
                            select comm);

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

        public new void GetConnectedTablesById(object o)
        {
            Tbl68SpeciesgroupsAllList?.Clear();
            Tbl68SpeciesgroupsAllList = _allListVm.GetValueTbl68SpeciesgroupsAllList();

            Tbl66GenussesList?.Clear();
            Tbl66GenussesList = new ObservableCollection<Tbl66Genus>
            (from genus in _tbl66GenussesRepository.GetAll()
                where genus.GenusID == CurrentTbl69FiSpecies.GenusID
                orderby genus.GenusName
                select genus);

            GenussesView = CollectionViewSource.GetDefaultView(Tbl66GenussesList);
            GenussesView.Refresh();

            SelectedMainTabIndex = 0;  //change to Connect tab
            SelectedDetailTabIndex = 7;
            SelectedDetailSubTabIndex = 0;
        }
        #endregion "Public Commands Connected Tables by DoubleClick"
    
 

 //    Part 10    

     
        #region "Public Commands to open Detail TabItems"

        private int _selectedMainTabIndex;
        private int _selectedMainSubRefTabIndex;
        private int _selectedDetailTabIndex;
        private int _selectedDetailSubTabIndex;
        private int _selectedDetailSubImageTabIndex;
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
                    SelectedDetailTabIndex = 7;
                    SelectedDetailSubTabIndex = 1;
                }
                if (_selectedMainTabIndex == 2)
                {
                    SelectedDetailTabIndex = 7;
                    SelectedDetailSubTabIndex = 2;
                }
                if (_selectedMainTabIndex == 3)
                {
                    SelectedDetailTabIndex = 7;
                    SelectedDetailSubTabIndex = 3;
                }
                if (_selectedMainTabIndex == 4)
                {
                    SelectedDetailTabIndex = 7;
                    SelectedDetailSubTabIndex = 4;
                }
                if (_selectedMainTabIndex == 5)
                {
                    SelectedDetailTabIndex = 7;
                    SelectedDetailSubTabIndex = 5;
                }
                if (_selectedMainTabIndex == 6)
                {
                    SelectedDetailTabIndex = 7;
                    SelectedDetailSubTabIndex = 6;
                }
                if (_selectedMainTabIndex == 7)
                {
                    SelectedDetailTabIndex = 7;
                    SelectedDetailSubTabIndex = 7;
                }
            }
        }

        public new int SelectedMainSubRefTabIndex
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


        public new int SelectedDetailTabIndex
        {
            get => _selectedDetailTabIndex;
            set
            {
                if (value == _selectedDetailTabIndex) return;
                _selectedDetailTabIndex = value; RaisePropertyChanged();
                SelectedMainTabIndex = 0;
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
                    Tbl68SpeciesgroupsList?.Clear();
                    Tbl68SpeciesgroupsList = new ObservableCollection<Tbl68Speciesgroup>
                    (from groups in _tbl68SpeciesgroupsRepository.GetAll()
                        where groups.SpeciesgroupID == CurrentTbl69FiSpecies.SpeciesgroupID
                        orderby groups.Subspeciesgroup, groups.Subspeciesgroup
                        select groups);

                    SpeciesgroupsView = CollectionViewSource.GetDefaultView(Tbl68SpeciesgroupsList);
                    SpeciesgroupsView.Refresh();

                    SelectedMainTabIndex = 1;
                    SelectedDetailTabIndex = 7;
                }
                if (_selectedDetailSubTabIndex == 2)
                {
                    Tbl78NamesList?.Clear();
                    Tbl78NamesList = new ObservableCollection<Tbl78Name>
                    (from x in _tbl78NamesRepository.GetAll()
                        where x.FiSpeciesID == CurrentTbl69FiSpecies.FiSpeciesID
                        orderby x.NameName
                        select x);

                    NamesView = CollectionViewSource.GetDefaultView(Tbl78NamesList);
                    NamesView.Refresh();

                    SelectedMainTabIndex = 2;
                    SelectedDetailTabIndex = 7;
                }
                if (_selectedDetailSubTabIndex == 3)
                {
                    Tbl81ImagesList?.Clear();
                    Tbl81ImagesList = new ObservableCollection<Tbl81Image>
                    (from x in _tbl81ImagesRepository.GetAll()
                        where x.FiSpeciesID == CurrentTbl69FiSpecies.FiSpeciesID
                        select x);

                    ImagesView = CollectionViewSource.GetDefaultView(Tbl81ImagesList);
                    ImagesView.Refresh();

                    SelectedMainTabIndex = 3;
                    SelectedDetailTabIndex = 7;
                }
                if (_selectedDetailSubTabIndex == 4)
                {
                    Tbl84SynonymsList?.Clear();
                    Tbl84SynonymsList = new ObservableCollection<Tbl84Synonym>
                    (from x in _tbl84SynonymsRepository.GetAll()
                        where x.FiSpeciesID == CurrentTbl69FiSpecies.FiSpeciesID
                        orderby x.SynonymName
                        select x);

                    SynonymsView = CollectionViewSource.GetDefaultView(Tbl84SynonymsList);
                    SynonymsView.Refresh();

                    SelectedMainTabIndex = 4;
                    SelectedDetailTabIndex = 7;
                }
                if (_selectedDetailSubTabIndex == 5)
                {
                    TblCountriesAllList?.Clear();
                    TblCountriesAllList = _allListVm.GetValueTblCountriesAllList();

                    Tbl87GeographicsList?.Clear();
                    Tbl87GeographicsList = new ObservableCollection<Tbl87Geographic>
                    (from x in _tbl87GeographicsRepository.GetAll()
                        where x.FiSpeciesID == CurrentTbl69FiSpecies.FiSpeciesID
                        select x);

                    GeographicsView = CollectionViewSource.GetDefaultView(Tbl87GeographicsList);
                    GeographicsView.Refresh();

                    SelectedMainTabIndex = 5;
                    SelectedDetailTabIndex = 7;
                }
                if (_selectedDetailSubTabIndex == 6)
                {
                    Tbl90ExpertsAllList?.Clear();
                    Tbl90ExpertsAllList = _allListVm.GetValueTbl90ExpertsAllList();

                    Tbl90RefExpertsList = new ObservableCollection<Tbl90Reference>
                    (from refEx in _tbl90ReferencesRepository.GetAll()
                        where refEx.FiSpeciesID == CurrentTbl69FiSpecies.FiSpeciesID
                              && refEx.RefAuthorID == null
                              && refEx.RefSourceID == null
                        orderby refEx.Tbl90RefExperts.RefExpertName
                        select refEx);

                    RefExpertsView = CollectionViewSource.GetDefaultView(Tbl90RefExpertsList);
                    RefExpertsView.Refresh();

                    SelectedMainTabIndex = 6;
                    SelectedDetailTabIndex = 7;
                }
                if (_selectedDetailSubTabIndex == 7)
                {
                    Tbl93CommentsList?.Clear();
                    Tbl93CommentsList = new ObservableCollection<Tbl93Comment>
                    (from comm in _tbl93CommentsRepository.GetAll()
                        where comm.FiSpeciesID == CurrentTbl69FiSpecies.FiSpeciesID
                        orderby comm.Info
                        select comm);

                    CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
                    CommentsView.Refresh();

                   SelectedDetailTabIndex = 7;
                   SelectedMainTabIndex = 7;
                }
            }
        }

        public new int SelectedDetailSubRefTabIndex
        {
            get => _selectedDetailSubRefTabIndex;
            set
            {
                if (value == _selectedDetailSubRefTabIndex) return;
                _selectedDetailSubRefTabIndex = value; RaisePropertyChanged();
                if (_selectedDetailSubRefTabIndex == 0)
                {
                    SelectedMainSubRefTabIndex = 0;

                    Tbl90ExpertsAllList = _allListVm.GetValueTbl90ExpertsAllList();

                    Tbl90RefExpertsList = new ObservableCollection<Tbl90Reference>
                    (from refEx in _tbl90ReferencesRepository.GetAll()
                     where refEx.FiSpeciesID == CurrentTbl69FiSpecies.FiSpeciesID
                           && refEx.RefAuthorID == null
                           && refEx.RefSourceID == null
                     orderby refEx.Tbl90RefExperts.RefExpertName
                     select refEx);
                }
                if (_selectedDetailSubRefTabIndex == 1)
                {
                    SelectedMainSubRefTabIndex = 1;

                    Tbl90SourcesAllList = _allListVm.GetValueTbl90SourcesAllList();

                    Tbl90RefSourcesList = new ObservableCollection<Tbl90Reference>
                    (from refSo in _tbl90ReferencesRepository.GetAll()
                     where refSo.FiSpeciesID == CurrentTbl69FiSpecies.FiSpeciesID
                           && refSo.RefExpertID == null
                           && refSo.RefAuthorID == null
                     orderby refSo.Tbl90RefSources.RefSourceName
                     select refSo);
                }
                if (_selectedDetailSubRefTabIndex == 2)
                {
                    SelectedMainSubRefTabIndex = 2;
                    Tbl90AuthorsAllList = _allListVm.GetValueTbl90AuthorsAllList();

                    Tbl90RefAuthorsList = new ObservableCollection<Tbl90Reference>
                    (from refAu in _tbl90ReferencesRepository.GetAll()
                     where refAu.FiSpeciesID == CurrentTbl69FiSpecies.FiSpeciesID
                           && refAu.RefExpertID == null
                           && refAu.RefSourceID == null
                     orderby refAu.Tbl90RefAuthors.RefAuthorName, refAu.Tbl90RefAuthors.BookName, refAu.Tbl90RefAuthors.Page1
                     select refAu);
                }
            }
        }

        public int SelectedDetailSubImageTabIndex
        {
            get => _selectedDetailSubImageTabIndex;
            set { _selectedDetailSubImageTabIndex = value; RaisePropertyChanged(); }
        }

        #endregion "Public Commands to open Detail TabItems"
 




   }
}   
