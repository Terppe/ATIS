using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity.Validation;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using Te.Atis.BusinessLayer;
using Te.Atis.DomainModel;
using Te.Atis.Ui.Desktop.Domain;
using Te.Atis.Ui.Desktop.Domain.Helper;
using Te.Atis.Ui.Desktop.MessageBox;    

    
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Media.Imaging;
using Microsoft.Win32;   
    
         //    Tbl69FiSpeciessesViewModel Skriptdatum:  23.06.2018  10:32    

namespace Te.Atis.Ui.Desktop.Views.Database
{     
    
    public class Tbl69FiSpeciessesViewModel : Tbl66GenussesViewModel
    {     
         
        #region "Private Data Members"

        private static IBusinessLayer _businessLayer;
        private static DbEntityException _entityException;   
         
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
                _businessLayer = new BusinessLayer.BusinessLayer();
                _entityException = new DbEntityException();
            }
        }
        #endregion "Constructor"          
 

 //    Part 1    

    
        #region "Public Commands Basic Tbl69FiSpecies"
        //-------------------------------------------------------------------------
        private RelayCommand _clearFiSpeciesCommand;

        public new  ICommand ClearFiSpeciesCommand => _clearFiSpeciesCommand ??
                                                  (_clearFiSpeciesCommand = new RelayCommand(delegate { ClearFiSpecies(null); }));         
    
        private RelayCommand _getFiSpeciessesByNameOrIdCommand;  

        public new ICommand GetFiSpeciessesByNameOrIdCommand => _getFiSpeciessesByNameOrIdCommand ??
                                                           (_getFiSpeciessesByNameOrIdCommand = new RelayCommand(delegate { GetFiSpeciessesByNameOrId(null); }));        
    
        private RelayCommand _addFiSpeciesCommand;

        public new ICommand AddFiSpeciesCommand => _addFiSpeciesCommand ??
                                                (_addFiSpeciesCommand = new RelayCommand(delegate { AddFiSpecies(null); }));

        private RelayCommand _copyFiSpeciesCommand;

        public new ICommand CopyFiSpeciesCommand => _copyFiSpeciesCommand ??
                                                 (_copyFiSpeciesCommand = new RelayCommand(delegate { CopyFiSpecies(null); }));       
             
        private RelayCommand _deleteFiSpeciesCommand;

        public ICommand DeleteFiSpeciesCommand => _deleteFiSpeciesCommand ??
                                                   (_deleteFiSpeciesCommand = new RelayCommand(delegate { DeleteFiSpecies(null); }));    
    
        private RelayCommand _saveFiSpeciesCommand;

        public new ICommand SaveFiSpeciesCommand => _saveFiSpeciesCommand ??
                                                 (_saveFiSpeciesCommand = new RelayCommand(delegate { SaveFiSpecies(null); }));

        //-------------------------------------------------------------------------          
     
        private void ClearFiSpecies(object o)
        {
            SearchFiSpeciesName = string.Empty;

            Tbl66GenussesList?.Clear();
            Tbl69FiSpeciessesList?.Clear();
            Tbl78NamesList?.Clear();
            Tbl90ReferenceExpertsList?.Clear();
            Tbl90ReferenceSourcesList?.Clear();
            Tbl90ReferenceAuthorsList?.Clear();
            Tbl93CommentsList?.Clear();
        }
        //----------------------------------------------------------------------                  
     
        private void GetFiSpeciessesByNameOrId(object o)
        {
            Tbl66GenussesList?.Clear();
            Tbl78NamesList?.Clear();
            Tbl90ReferenceExpertsList?.Clear();
            Tbl90ReferenceSourcesList?.Clear();
            Tbl90ReferenceAuthorsList?.Clear();
            Tbl93CommentsList?.Clear();

            Tbl69FiSpeciessesList = int.TryParse(SearchFiSpeciesName, out var id) ?
                new ObservableCollection<Tbl69FiSpecies>(_businessLayer.ListTbl69FiSpeciessesByFiSpeciesId(id)) :
                new ObservableCollection<Tbl69FiSpecies>(_businessLayer.ListTbl69FiSpeciessesByFiSpeciesName(SearchFiSpeciesName));

            Tbl66GenussesAllList = new ObservableCollection<Tbl66Genus>(_businessLayer.ListTbl66Genusses());

            FiSpeciessesView = CollectionViewSource.GetDefaultView(Tbl69FiSpeciessesList);
            FiSpeciessesView.Refresh();
        }
        //------------------------------------------------------------------------------------                          
     
        private void AddFiSpecies(object o)
        {
            Tbl69FiSpeciessesList = new ObservableCollection<Tbl69FiSpecies> {new Tbl69FiSpecies
            {
                FiSpeciesName = CultRes.StringsRes.DatasetNew,
                GenusID = CurrentTbl69FiSpecies.GenusID
            }  };

            Tbl66GenussesAllList?.Clear();
            Tbl66GenussesAllList = new ObservableCollection<Tbl66Genus>(_businessLayer.ListTbl66Genusses());

            FiSpeciessesView = CollectionViewSource.GetDefaultView(Tbl69FiSpeciessesList);
            FiSpeciessesView.MoveCurrentToFirst();
        }
        //------------------------------------------------------------------------------------                               
        
        private void CopyFiSpecies(object o)
        {
            Tbl69FiSpeciessesList = new ObservableCollection<Tbl69FiSpecies>();

            var fispecies = _businessLayer.SingleListTbl69FiSpeciessesByFiSpeciesId(CurrentTbl69FiSpecies.FiSpeciesID);

            Tbl69FiSpeciessesList.Add(new Tbl69FiSpecies
            {
                            GenusID = fispecies.GenusID,              
                            SpeciesgroupID = fispecies.SpeciesgroupID,              
                            FiSpeciesName = CultRes.StringsRes.DatasetNew,              
                            Subspecies = CultRes.StringsRes.DatasetNew, 
                            Divers = CultRes.StringsRes.DatasetNew, 
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
            FiSpeciessesView.MoveCurrentToFirst();
        }
        //---------------------------------------------------------------------------------------                            
     
        private void DeleteFiSpecies(object o)
        {
            try
            {
                var fispecies = _businessLayer.SingleListTbl69FiSpeciessesByFiSpeciesId(CurrentTbl69FiSpecies.FiSpeciesID);
                if (fispecies != null)
                {
                    if (WpfMessageBox.Show(CultRes.StringsRes.DeleteQuestion1, CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl69FiSpecies.FiSpeciesName,
                            MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes)
                        return;
                    fispecies.EntityState = EntityState.Deleted;
                    _businessLayer.RemoveFiSpecies(fispecies);

                    WpfMessageBox.Show(CultRes.StringsRes.DeleteSuccess, CurrentTbl69FiSpecies.FiSpeciesName,
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                }
                else
                {
                    WpfMessageBox.Show(CultRes.StringsRes.Information, CultRes.StringsRes.DeleteCan + " " + CurrentTbl69FiSpecies.FiSpeciesName + " " + CultRes.StringsRes.DeleteCan1,
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                }
            }
            catch (DbEntityValidationException ex)
            {
                _entityException.EntityException(ex);
            }

            Tbl69FiSpeciessesList = new ObservableCollection<Tbl69FiSpecies>(_businessLayer.ListTbl69FiSpeciessesByFiSpeciesName(SearchFiSpeciesName));

            FiSpeciessesView = CollectionViewSource.GetDefaultView(Tbl69FiSpeciessesList);
            FiSpeciessesView.Refresh();
        }
        //-------------------------------------------------------------------------------------------------                    
        
        private void SaveFiSpecies(object o)
        {
            try
            {
                var fispecies = _businessLayer.SingleListTbl69FiSpeciessesByFiSpeciesId(CurrentTbl69FiSpecies.FiSpeciesID);
                if (CurrentTbl69FiSpecies.FiSpeciesID != 0)
                {
                    if (fispecies != null) //update
                    {
                            fispecies.FiSpeciesName = CurrentTbl69FiSpecies.FiSpeciesName;
                            fispecies.GenusID = CurrentTbl69FiSpecies.GenusID;
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
                            fispecies.EntityState = EntityState.Modified;
                    }
                }
                else
                {
                    fispecies = new Tbl69FiSpecies   //add new
                    {
                            FiSpeciesName = CurrentTbl69FiSpecies.FiSpeciesName,
                            GenusID = CurrentTbl69FiSpecies.GenusID,
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
                            MemoBreeding = CurrentTbl69FiSpecies.MemoBreeding,
                            EntityState = EntityState.Added
                    };
                }
                {
                    //GenusID and SpeciesgroupID may be not 0
                    if (CurrentTbl69FiSpecies.GenusID == 0 || CurrentTbl69FiSpecies.SpeciesgroupID == 0)
                    {
                        WpfMessageBox.Show(CultRes.StringsRes.RequiredGenealogyConnect, CultRes.StringsRes.RequiredInput,
                            MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        return;
                    }

                    //check if dataset with Name and SpeciesgroupId already exist       
                    var dataset = _businessLayer.ListTbl69FiSpeciessesByFiSpeciesNameAndSubspeciesAndDiversAndGenusIdAndSpeciesgroupId(CurrentTbl69FiSpecies.FiSpeciesName, CurrentTbl69FiSpecies.Subspecies, CurrentTbl69FiSpecies.Divers, CurrentTbl69FiSpecies.GenusID, CurrentTbl69FiSpecies.SpeciesgroupID);

                    if (dataset.Count != 0 && CurrentTbl69FiSpecies.FiSpeciesID == 0)  //dataset exist
                    {
                        WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTbl69FiSpecies.FiSpeciesName + " " + CurrentTbl69FiSpecies.Subspecies + " " + CurrentTbl69FiSpecies.Divers,
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                    }
                    if (dataset.Count == 0 && CurrentTbl69FiSpecies.FiSpeciesID == 0 ||
                        dataset.Count != 0 && CurrentTbl69FiSpecies.FiSpeciesID != 0 ||
                        dataset.Count == 0 && CurrentTbl69FiSpecies.FiSpeciesID != 0) //new dataset and update
                    {
                        if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTbl69FiSpecies.FiSpeciesName + " " + CurrentTbl69FiSpecies.Subspecies + " " + CurrentTbl69FiSpecies.Divers,
                                MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes)
                            return;
                        {
                            _businessLayer.UpdateFiSpecies(fispecies);

                            WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess, CurrentTbl69FiSpecies.FiSpeciesName + " " + CurrentTbl69FiSpecies.Subspecies + " " + CurrentTbl69FiSpecies.Divers,
                                MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        }
                    }
                }
            }
            catch (DbEntityValidationException ex)
            {
                _entityException.EntityException(ex);
            }

            if (CurrentTbl69FiSpecies.FiSpeciesID == 0)  //new Dataset                        
                Tbl69FiSpeciessesList = new ObservableCollection<Tbl69FiSpecies>(_businessLayer.ListTbl69FiSpeciessesByFiSpeciesNameAndSubspeciesAndDivers(CurrentTbl69FiSpecies.FiSpeciesName, CurrentTbl69FiSpecies.Subspecies, CurrentTbl69FiSpecies.Divers));
            if (CurrentTbl69FiSpecies.FiSpeciesID != 0)   //update 
                Tbl69FiSpeciessesList = new ObservableCollection<Tbl69FiSpecies>(_businessLayer.ListTbl69FiSpeciessesByFiSpeciesId(CurrentTbl69FiSpecies.FiSpeciesID));

            FiSpeciessesView = CollectionViewSource.GetDefaultView(Tbl69FiSpeciessesList);
            FiSpeciessesView.Refresh();
        }
        #endregion "Public Commands"                  
 
 

 //    Part 2    

           
        #region "Public Commands Connect <== Tbl66Genus"                 
        //-------------------------------------------------------------------------
        private RelayCommand _clearGenusCommand;

        public new ICommand ClearGenusCommand => _clearGenusCommand ??
                                                  (_clearGenusCommand = new RelayCommand(delegate { ClearGenus(null); }));

        private RelayCommand _getGenussesByNameOrIdCommand;  

        public new ICommand GetGenussesByNameOrIdCommand => _getGenussesByNameOrIdCommand ??
                                                           (_getGenussesByNameOrIdCommand = new RelayCommand(delegate { GetGenussesByNameOrId(null); }));

        private RelayCommand _addGenusCommand;

        public new ICommand AddGenusCommand => _addGenusCommand ??
                                                (_addGenusCommand = new RelayCommand(delegate { AddGenus(null); }));

        private RelayCommand _copyGenusCommand;

        public new ICommand CopyGenusCommand => _copyGenusCommand ??
                                                 (_copyGenusCommand = new RelayCommand(delegate { CopyGenus(null); }));

        private RelayCommand _saveGenusCommand;

        public new ICommand SaveGenusCommand => _saveGenusCommand ??
                                                 (_saveGenusCommand = new RelayCommand(delegate { SaveGenus(null); }));

        //-------------------------------------------------------------------------          
     
        private void ClearGenus(object o)
        {
            SearchGenusName = string.Empty;
            Tbl66GenussesList?.Clear();
        }
        //----------------------------------------------------------------------            
     
        private void GetGenussesByNameOrId(object o)
        {
            Tbl66GenussesList = int.TryParse(SearchGenusName, out var id) ?
                new ObservableCollection<Tbl66Genus>(_businessLayer.ListTbl66GenussesByGenusId(id)) :
                new ObservableCollection<Tbl66Genus>(_businessLayer.ListTbl66GenussesByGenusName(SearchGenusName));

            GenussesView = CollectionViewSource.GetDefaultView(Tbl66GenussesList);
            GenussesView.Refresh();
        }
        //----------------------------------------------------------------------            
     
        private void AddGenus(object o)      
        {
            Tbl66GenussesList = new ObservableCollection<Tbl66Genus> {new Tbl66Genus
            {   GenusName = CultRes.StringsRes.DatasetNew    }  };

            GenussesView = CollectionViewSource.GetDefaultView(Tbl66GenussesList);
            GenussesView.MoveCurrentToFirst();
        }
        //----------------------------------------------------------------------            
     
        private void CopyGenus(object o)
        {
            Tbl66GenussesList = new ObservableCollection<Tbl66Genus>();

            var genus = _businessLayer.SingleListTbl66GenussesByGenusId(CurrentTbl66Genus.GenusID);

            Tbl66GenussesList.Add(new Tbl66Genus
            {
                InfratribusID = genus.InfratribusID,
                GenusName = CultRes.StringsRes.DatasetNew,
                Valid =  genus.Valid,
                ValidYear =  genus.ValidYear,
                Synonym =  genus.Synonym,
                Author =  genus.Author,
                AuthorYear =  genus.AuthorYear,
                Info =  genus.Info,
                EngName =  genus.EngName,
                GerName =  genus.GerName,
                FraName =  genus.FraName,
                PorName =  genus.PorName,
                Memo =  genus.Memo
            });

            GenussesView = CollectionViewSource.GetDefaultView(Tbl66GenussesList);
            GenussesView.MoveCurrentToFirst();
        }
        //----------------------------------------------------------------------            
     
        private void SaveGenus(object o)
        {
            try
            {
                var genus = _businessLayer.SingleListTbl66GenussesByGenusId(CurrentTbl66Genus.GenusID);
                if (CurrentTbl66Genus.GenusID != 0)
                {
                    if (genus != null) //update
                    {
                        genus.GenusName = CurrentTbl66Genus.GenusName;
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
                        genus.EntityState = EntityState.Modified;
                    }
                }
                else
                {
                    genus = new Tbl66Genus   //add new
                    {
                        GenusName = CurrentTbl66Genus.GenusName,              
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
                        Memo = CurrentTbl66Genus.Memo,
                        EntityState = EntityState.Added
                    };
                }
                {
                    //InfratribusID may be not 0
                    if (CurrentTbl66Genus.InfratribusID == 0)          

                    {
                        WpfMessageBox.Show(CultRes.StringsRes.RequiredGenealogyConnect, CultRes.StringsRes.RequiredInput,
                            MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        return;
                    }

                    //check if dataset with Name and GenusId already exist       
                    var dataset = _businessLayer.ListTbl66GenussesByGenusNameAndInfratribusId(CurrentTbl66Genus.GenusName, CurrentTbl66Genus.InfratribusID);

                    if (dataset.Count != 0 && CurrentTbl66Genus.GenusID == 0)  //dataset exist
                    {
                        WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTbl66Genus.GenusName,
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                    }
                    if (dataset.Count == 0 && CurrentTbl66Genus.GenusID == 0 ||
                        dataset.Count != 0 && CurrentTbl66Genus.GenusID != 0 ||
                        dataset.Count == 0 && CurrentTbl66Genus.GenusID != 0) //new dataset and update
                    {
                        if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTbl66Genus.GenusName,
                                MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes)
                            return;
                        {
                            _businessLayer.UpdateGenus(genus);

                            WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess, CurrentTbl66Genus.GenusName,
                                MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        }
                    }
                }
            }
            catch (DbEntityValidationException ex)
            {
                _entityException.EntityException(ex);
            }

            if (CurrentTbl66Genus.GenusID == 0)  //new Dataset                        
                Tbl66GenussesList = new ObservableCollection<Tbl66Genus>(_businessLayer.ListTbl66GenussesByGenusName(CurrentTbl66Genus.GenusName));
            if (CurrentTbl66Genus.GenusID != 0)   //update 
                Tbl66GenussesList = new ObservableCollection<Tbl66Genus>(_businessLayer.ListTbl66GenussesByGenusId(CurrentTbl66Genus.GenusID));

            SelectedMainTabIndex = 0;
            GenussesView = CollectionViewSource.GetDefaultView(Tbl66GenussesList);
            GenussesView.Refresh();
        }
        #endregion "Public Commands"                  
 
            

 //    Part 3    

           
        #region "Public Commands Connect <== Tbl68Speciesgroup"                 
        //-------------------------------------------------------------------------
        private RelayCommand _clearSpeciesgroupCommand;

        public  ICommand ClearSpeciesgroupCommand => _clearSpeciesgroupCommand ??
                                                  (_clearSpeciesgroupCommand = new RelayCommand(delegate { ClearSpeciesgroup(null); }));

        private RelayCommand _getSpeciesgroupsByNameOrIdCommand;  

        public  ICommand GetSpeciesgroupsByNameOrIdCommand => _getSpeciesgroupsByNameOrIdCommand ??
                                                           (_getSpeciesgroupsByNameOrIdCommand = new RelayCommand(delegate { GetSpeciesgroupsByNameOrId(null); }));

        private RelayCommand _addSpeciesgroupCommand;

        public ICommand AddSpeciesgroupCommand => _addSpeciesgroupCommand ??
                                                (_addSpeciesgroupCommand = new RelayCommand(delegate { AddSpeciesgroup(null); }));

        private RelayCommand _copySpeciesgroupCommand;

        public ICommand CopySpeciesgroupCommand => _copySpeciesgroupCommand ??
                                                 (_copySpeciesgroupCommand = new RelayCommand(delegate { CopySpeciesgroup(null); }));

        private RelayCommand _saveSpeciesgroupCommand;

        public ICommand SaveSpeciesgroupCommand => _saveSpeciesgroupCommand ??
                                                 (_saveSpeciesgroupCommand = new RelayCommand(delegate { SaveSpeciesgroup(null); }));

        //-------------------------------------------------------------------------          
     
        private void ClearSpeciesgroup(object o)
        {
            SearchSpeciesgroupName = string.Empty;
            Tbl68SpeciesgroupsList?.Clear();
        }
        //----------------------------------------------------------------------            
     
        private void GetSpeciesgroupsByNameOrId(object o)
        {
            Tbl68SpeciesgroupsList = int.TryParse(SearchSpeciesgroupName, out var id) ?
                new ObservableCollection<Tbl68Speciesgroup>(_businessLayer.ListTbl68SpeciesgroupsBySpeciesgroupId(id)) :
                new ObservableCollection<Tbl68Speciesgroup>(_businessLayer.ListTbl68SpeciesgroupsBySpeciesgroupName(SearchSpeciesgroupName));

            SpeciesgroupsView = CollectionViewSource.GetDefaultView(Tbl68SpeciesgroupsList);
            SpeciesgroupsView.Refresh();
        }
        //----------------------------------------------------------------------            
       
        private void AddSpeciesgroup(object o)      
        {
            Tbl68SpeciesgroupsList = new ObservableCollection<Tbl68Speciesgroup> {new Tbl68Speciesgroup
            {     SpeciesgroupName = CultRes.StringsRes.DatasetNew    }  };

            SpeciesgroupsView = CollectionViewSource.GetDefaultView(Tbl68SpeciesgroupsList);
            SpeciesgroupsView.MoveCurrentToFirst();
        }
        //----------------------------------------------------------------------            
       
        private void CopySpeciesgroup(object o)
        {
            Tbl68SpeciesgroupsList = new ObservableCollection<Tbl68Speciesgroup>();

            var speciesgroup = _businessLayer.SingleListTbl68SpeciesgroupsBySpeciesgroupId(CurrentTbl68Speciesgroup.SpeciesgroupID);

            Tbl68SpeciesgroupsList.Add(new Tbl68Speciesgroup
            {
                SpeciesgroupName = CultRes.StringsRes.DatasetNew,
                Subspeciesgroup = CultRes.StringsRes.DatasetNew,
                Valid =  speciesgroup.Valid,
                ValidYear =  speciesgroup.ValidYear,
                Synonym =  speciesgroup.Synonym,
                Author =  speciesgroup.Author,
                AuthorYear =  speciesgroup.AuthorYear,
                Info =  speciesgroup.Info,
                EngName =  speciesgroup.EngName,
                GerName =  speciesgroup.GerName,
                FraName =  speciesgroup.FraName,
                PorName =  speciesgroup.PorName,
                Memo =  speciesgroup.Memo
            });

            SpeciesgroupsView = CollectionViewSource.GetDefaultView(Tbl68SpeciesgroupsList);
            SpeciesgroupsView.MoveCurrentToFirst();
        }
        //----------------------------------------------------------------------            
        
        private void SaveSpeciesgroup(object o)
        {
            try
            {
                var speciesgroup = _businessLayer.SingleListTbl68SpeciesgroupsBySpeciesgroupId(CurrentTbl68Speciesgroup.SpeciesgroupID);
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
                        speciesgroup.EntityState = EntityState.Modified;
                    }
                }
                else
                {
                    speciesgroup = new Tbl68Speciesgroup   //add new
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
                        Memo = CurrentTbl68Speciesgroup.Memo,
                        EntityState = EntityState.Added
                    };
                }
                {
                    //check if dataset with Name and Subspeciesgroup already exist       
                    var dataset = _businessLayer.ListTbl68SpeciesgroupsBySpeciesgroupNameAndSubspeciesgroup(CurrentTbl68Speciesgroup.SpeciesgroupName, CurrentTbl68Speciesgroup.Subspeciesgroup);

                    if (dataset.Count != 0 && CurrentTbl68Speciesgroup.SpeciesgroupID == 0)  //dataset exist
                    {
                        WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTbl68Speciesgroup.SpeciesgroupName + " " + CurrentTbl68Speciesgroup.Subspeciesgroup,
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                    }
                    if (dataset.Count == 0 && CurrentTbl68Speciesgroup.SpeciesgroupID == 0 ||
                        dataset.Count != 0 && CurrentTbl68Speciesgroup.SpeciesgroupID != 0 ||
                        dataset.Count == 0 && CurrentTbl68Speciesgroup.SpeciesgroupID != 0) //new dataset and update
                    {
                        if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTbl68Speciesgroup.SpeciesgroupName + " " + CurrentTbl68Speciesgroup.Subspeciesgroup,
                                MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes)
                            return;
                        {
                            _businessLayer.UpdateSpeciesgroup(speciesgroup);

                            WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess, CurrentTbl68Speciesgroup.SpeciesgroupName + " " + CurrentTbl68Speciesgroup.Subspeciesgroup,
                                MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        }
                    }
                }
            }
            catch (DbEntityValidationException ex)
            {
                _entityException.EntityException(ex);
            }

            if (CurrentTbl68Speciesgroup.SpeciesgroupID == 0)  //new Dataset                        
                Tbl68SpeciesgroupsList = new ObservableCollection<Tbl68Speciesgroup>(_businessLayer.ListTbl68SpeciesgroupsBySpeciesgroupNameAndSubspeciesgroup(CurrentTbl68Speciesgroup.SpeciesgroupName, CurrentTbl68Speciesgroup.Subspeciesgroup));
            if (CurrentTbl68Speciesgroup.SpeciesgroupID != 0)   //update 
                Tbl68SpeciesgroupsList = new ObservableCollection<Tbl68Speciesgroup>(_businessLayer.ListTbl68SpeciesgroupsBySpeciesgroupId(CurrentTbl68Speciesgroup.SpeciesgroupID));

            SelectedMainTabIndex = 1;
            SpeciesgroupsView = CollectionViewSource.GetDefaultView(Tbl68SpeciesgroupsList);
            SpeciesgroupsView.Refresh();
        }
        #endregion "Public Commands"                  
 


 //    Part 4    

           
        #region "Public Commands Connect <== Tbl78Name"                 
        //-------------------------------------------------------------------------
        private RelayCommand _clearNameCommand;

        public ICommand ClearNameCommand => _clearNameCommand ??
                                                  (_clearNameCommand = new RelayCommand(delegate { ClearName(null); }));

        private RelayCommand _getNamesByNameOrIdCommand;  

        public  ICommand GetNamesByNameOrIdCommand => _getNamesByNameOrIdCommand ??
                                                           (_getNamesByNameOrIdCommand = new RelayCommand(delegate { GetNamesByNameOrId(null); }));

        private RelayCommand _addNameCommand;

        public ICommand AddNameCommand => _addNameCommand ??
                                                (_addNameCommand = new RelayCommand(delegate { AddName(null); }));

        private RelayCommand _copyNameCommand;

        public ICommand CopyNameCommand => _copyNameCommand ??
                                                 (_copyNameCommand = new RelayCommand(delegate { CopyName(null); }));

        private RelayCommand _saveNameCommand;

        public ICommand SaveNameCommand => _saveNameCommand ??
                                                 (_saveNameCommand = new RelayCommand(delegate { SaveName(null); }));

        //-------------------------------------------------------------------------          
     
        private void ClearName(object o)
        {
            SearchNameName = string.Empty;
            Tbl78NamesList?.Clear();
        }
        //----------------------------------------------------------------------            
     
        private void GetNamesByNameOrId(object o)
        {
            Tbl78NamesList = int.TryParse(SearchNameName, out var id) ?
                new ObservableCollection<Tbl78Name>(_businessLayer.ListTbl78NamesByNameId(id)) :
                new ObservableCollection<Tbl78Name>(_businessLayer.ListTbl78NamesByNameName(SearchNameName));

            NamesView = CollectionViewSource.GetDefaultView(Tbl78NamesList);
            NamesView.Refresh();
        }
        //----------------------------------------------------------------------            
     
        private void AddName(object o)      
        {
            Tbl78NamesList = new ObservableCollection<Tbl78Name> {new Tbl78Name
            {  
                NameName = CultRes.StringsRes.DatasetNew,
                FiSpeciesID = CurrentTbl69FiSpecies.FiSpeciesID
            }    };

            NamesView = CollectionViewSource.GetDefaultView(Tbl78NamesList);
            NamesView.MoveCurrentToFirst();
        }
        //----------------------------------------------------------------------            
             
        private void CopyName(object o)
        {
            Tbl78NamesList = new ObservableCollection<Tbl78Name>();

            var name = _businessLayer.SingleListTbl78NamesByNameId(CurrentTbl78Name.NameID);

            Tbl78NamesList.Add(new Tbl78Name
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
            NamesView.MoveCurrentToFirst();
        }
        //----------------------------------------------------------------------            
             
        private void SaveName(object o)
        {
            try
            {
                var name = _businessLayer.SingleListTbl78NamesByNameId(CurrentTbl78Name.NameID);
                if (CurrentTbl78Name.NameID != 0)
                {
                    if (name != null) //update
                    {
                            name.NameName = CurrentTbl78Name.NameName;
                            name.FiSpeciesID = CurrentTbl78Name.FiSpeciesID;
                            name.PlSpeciesID = 2;
                            name.Valid = CurrentTbl78Name.Valid;
                            name.ValidYear = CurrentTbl78Name.ValidYear;
                            name.Language = CurrentTbl78Name.Language;
                            name.Info = CurrentTbl78Name.Info;
                            name.Updater = Environment.UserName;
                            name.UpdaterDate = DateTime.Now;
                            name.Memo = CurrentTbl78Name.Memo;
                             name.EntityState = EntityState.Modified;
                       }
                    }
                    else
                    {
                        name = new Tbl78Name     //add new
                        {
                            NameName = CurrentTbl78Name.NameName,
                            FiSpeciesID = CurrentTbl78Name.FiSpeciesID,
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
                            Memo = CurrentTbl78Name.Memo,        
                            EntityState = EntityState.Added
                    };
                }
                {
                    //FiSpeciesID may be not 0
                    if (CurrentTbl78Name.FiSpeciesID == 0)          

                    {
                        WpfMessageBox.Show(CultRes.StringsRes.RequiredGenealogyConnect, CultRes.StringsRes.RequiredInput,
                            MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        return;
                    }

                    //check if dataset with Name and FiSpeciesId already exist       
                    var dataset = _businessLayer.ListTbl78NamesByNameNameAndFiSpeciesId(CurrentTbl78Name.NameName, CurrentTbl78Name.FiSpeciesID);

                    if (dataset.Count != 0 && CurrentTbl78Name.NameID == 0)  //dataset exist
                    {
                        WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTbl78Name.NameName,
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                    }
                    if (dataset.Count == 0 && CurrentTbl78Name.NameID == 0 ||
                        dataset.Count != 0 && CurrentTbl78Name.NameID != 0 ||
                        dataset.Count == 0 && CurrentTbl78Name.NameID != 0) //new dataset and update
                    {
                        if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTbl78Name.NameName,
                                MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes)
                            return;
                        {
                            _businessLayer.UpdateName(name);

                            WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess, CurrentTbl78Name.NameName,
                                MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        }
                    }
                }
            }
            catch (DbEntityValidationException ex)
            {
                _entityException.EntityException(ex);
            }

            if (CurrentTbl78Name.NameID == 0)  //new Dataset                        
                Tbl78NamesList = new ObservableCollection<Tbl78Name>(_businessLayer.ListTbl78NamesByNameName(CurrentTbl78Name.NameName));
            if (CurrentTbl78Name.NameID != 0)   //update 
                Tbl78NamesList = new ObservableCollection<Tbl78Name>(_businessLayer.ListTbl78NamesByNameId(CurrentTbl78Name.NameID));

            SelectedMainTabIndex = 1;
            NamesView = CollectionViewSource.GetDefaultView(Tbl78NamesList);
            NamesView.Refresh();
        }
        #endregion "Public Commands"                  
 
             

 //    Part 5    

                       
        #region "Public Commands Connect ==> Tbl81Image"                 
        //-------------------------------------------------------------------------
        private RelayCommand _clearImageCommand;

        public ICommand ClearImageCommand => _clearImageCommand ??
                                                  (_clearImageCommand = new RelayCommand(delegate { ClearImage(null); }));

        private RelayCommand _getImagesByNameOrIdCommand;  

        public  ICommand GetImagesByNameOrIdCommand => _getImagesByNameOrIdCommand ??
                                                           (_getImagesByNameOrIdCommand = new RelayCommand(delegate { GetImagesByNameOrId(null); }));

        private RelayCommand _addImageCommand;

        public ICommand AddImageCommand => _addImageCommand ??
                                                (_addImageCommand = new RelayCommand(delegate { AddImage(null); }));

        private RelayCommand _copyImageCommand;

        public ICommand CopyImageCommand => _copyImageCommand ??
                                                 (_copyImageCommand = new RelayCommand(delegate { CopyImage(null); }));

        private RelayCommand _saveImageCommand;

        public ICommand SaveImageCommand => _saveImageCommand ??
                                                 (_saveImageCommand = new RelayCommand(delegate { SaveImage(null); }));

        //-------------------------------------------------------------------------          
                         
        private void ClearImage(object o)
        {
            SearchImageName = string.Empty;
            Tbl81ImagesList?.Clear();
        }
        //----------------------------------------------------------------------            
                         
        private void GetImagesByNameOrId(object o)
        {
            Tbl81ImagesList =  new ObservableCollection<Tbl81Image>(_businessLayer.ListTbl81ImagesByImageId(Convert.ToInt32(SearchImageName)));

            ImagesView = CollectionViewSource.GetDefaultView(Tbl81ImagesList);
        }
        //----------------------------------------------------------------------            
                         
        private void AddImage(object o)      
        {
            Tbl81ImagesList = new ObservableCollection<Tbl81Image> {new Tbl81Image
            {      FiSpeciesID = CurrentTbl69FiSpecies.FiSpeciesID       }    };

            ImagesView = CollectionViewSource.GetDefaultView(Tbl81ImagesList);
            ImagesView.MoveCurrentToFirst();
        }
        //----------------------------------------------------------------------            
                         
        private void CopyImage(object o)
        {
            Tbl81ImagesList = new ObservableCollection<Tbl81Image>();

            var image = _businessLayer.SingleListTbl81ImagesByImageId(CurrentTbl81Image.ImageID);

            Tbl81ImagesList.Add(new Tbl81Image
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
            ImagesView.MoveCurrentToFirst();
        }
        //----------------------------------------------------------------------            
                         
        private void SaveImage(object o)
        {
            try
            {
                var image = _businessLayer.SingleListTbl81ImagesByImageId(CurrentTbl81Image.ImageID);
                if (CurrentTbl81Image.ImageID != 0)
                {
                    if (image != null) //update
                    {
                            image.FiSpeciesID = CurrentTbl81Image.FiSpeciesID;         
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
                            image.EntityState = EntityState.Modified;
                    }
                }
                else
                {
                    image = new Tbl81Image   //add new
                    {
                            FiSpeciesID = CurrentTbl81Image.FiSpeciesID,
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
                            UpdaterDate = DateTime.Now,
                            EntityState = EntityState.Added
                    };
                }
                {
                    // FiSpeciesID may be not 0
                    if (CurrentTbl81Image.FiSpeciesID == 0 )          

                    {
                        WpfMessageBox.Show(CultRes.StringsRes.RequiredGenealogyConnect, CultRes.StringsRes.RequiredInput,
                            MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        return;
                    }

                    //check if dataset with Name and FiSpeciesId already exist       
                    var dataset = _businessLayer.ListTbl81ImagesByImageIdAndFiSpeciesId(CurrentTbl81Image.ImageID,  CurrentTbl81Image.FiSpeciesID);

                    if (dataset.Count != 0 && CurrentTbl81Image.ImageID == 0)  //dataset exist
                    {
                        WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTbl81Image.ImageID.ToString(),
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                    }
                    if (dataset.Count == 0 && CurrentTbl81Image.ImageID == 0 ||
                        dataset.Count != 0 && CurrentTbl81Image.ImageID != 0 ||
                        dataset.Count == 0 && CurrentTbl81Image.ImageID != 0) //new dataset and update
                    {
                        if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTbl81Image.ImageID.ToString(),
                                MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes)
                            return;
                        {
                            _businessLayer.UpdateImage(image);

                            WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess, CurrentTbl81Image.ImageID.ToString(),
                                MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        }
                    }
                }
            }
            catch (DbEntityValidationException ex)
            {
                _entityException.EntityException(ex);
            }

                Tbl81ImagesList = new ObservableCollection<Tbl81Image>(_businessLayer.ListTbl81ImagesByImageId(CurrentTbl81Image.ImageID));

            SelectedMainTabIndex = 2;
            ImagesView = CollectionViewSource.GetDefaultView(Tbl81ImagesList);
            ImagesView.Refresh();
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
        //-------------------------------------------------------------------------
        private RelayCommand _clearSynonymCommand;

        public ICommand ClearSynonymCommand => _clearSynonymCommand ??
                                                  (_clearSynonymCommand = new RelayCommand(delegate { ClearSynonym(null); }));

        private RelayCommand _getSynonymsByNameOrIdCommand;  

        public  ICommand GetSynonymsByNameOrIdCommand => _getSynonymsByNameOrIdCommand ??
                                                           (_getSynonymsByNameOrIdCommand = new RelayCommand(delegate { GetSynonymsByNameOrId(null); }));

        private RelayCommand _addSynonymCommand;

        public ICommand AddSynonymCommand => _addSynonymCommand ??
                                                (_addSynonymCommand = new RelayCommand(delegate { AddSynonym(null); }));

        private RelayCommand _copySynonymCommand;

        public ICommand CopySynonymCommand => _copySynonymCommand ??
                                                 (_copySynonymCommand = new RelayCommand(delegate { CopySynonym(null); }));

        private RelayCommand _saveSynonymCommand;

        public ICommand SaveSynonymCommand => _saveSynonymCommand ??
                                                 (_saveSynonymCommand = new RelayCommand(delegate { SaveSynonym(null); }));

        //-------------------------------------------------------------------------          
               
        private void ClearSynonym(object o)
        {
            SearchSynonymName = string.Empty;
            Tbl84SynonymsList?.Clear();
        }
        //----------------------------------------------------------------------            
               
        private void GetSynonymsByNameOrId(object o)
        {
            Tbl84SynonymsList = int.TryParse(SearchSynonymName, out var id) ?
                new ObservableCollection<Tbl84Synonym>(_businessLayer.ListTbl84SynonymsBySynonymId(id)) :
                new ObservableCollection<Tbl84Synonym>(_businessLayer.ListTbl84SynonymsBySynonymName(SearchSynonymName));

            SynonymsView = CollectionViewSource.GetDefaultView(Tbl84SynonymsList);
        }
        //----------------------------------------------------------------------            
               
        private void AddSynonym(object o)      
        {
            Tbl84SynonymsList = new ObservableCollection<Tbl84Synonym> {new Tbl84Synonym
            {  
                SynonymName = CultRes.StringsRes.DatasetNew,
                FiSpeciesID = CurrentTbl69FiSpecies.FiSpeciesID
            }    };

            SynonymsView = CollectionViewSource.GetDefaultView(Tbl84SynonymsList);
            SynonymsView.MoveCurrentToFirst();
        }
        //----------------------------------------------------------------------            
               
        private void CopySynonym(object o)
        {
            Tbl84SynonymsList = new ObservableCollection<Tbl84Synonym>();

            var synonym = _businessLayer.SingleListTbl84SynonymsBySynonymId(CurrentTbl84Synonym.SynonymID);

            Tbl84SynonymsList.Add(new Tbl84Synonym
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
            SynonymsView.MoveCurrentToFirst();
        }
        //----------------------------------------------------------------------            
               
        private void SaveSynonym(object o)
        {
            try
            {
                var synonym = _businessLayer.SingleListTbl84SynonymsBySynonymId(CurrentTbl84Synonym.SynonymID);
                if (CurrentTbl84Synonym.SynonymID != 0)
                {
                    if (synonym != null) //update
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
                             synonym.EntityState = EntityState.Modified;
                       }
                    }
                    else
                    {
                        synonym = new Tbl84Synonym     //add new
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
                    };
                }
                {
                    //FiSpeciesID may be not 0
                    if (CurrentTbl84Synonym.FiSpeciesID == 0)          

                    {
                        WpfMessageBox.Show(CultRes.StringsRes.RequiredGenealogyConnect, CultRes.StringsRes.RequiredInput,
                            MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        return;
                    }

                    //check if dataset with Name and FiSpeciesId already exist       
                    var dataset = _businessLayer.ListTbl84SynonymsBySynonymNameAndFiSpeciesId(CurrentTbl84Synonym.SynonymName, CurrentTbl84Synonym.FiSpeciesID);

                    if (dataset.Count != 0 && CurrentTbl84Synonym.SynonymID == 0)  //dataset exist
                    {
                        WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTbl84Synonym.SynonymName,
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                    }
                    if (dataset.Count == 0 && CurrentTbl84Synonym.SynonymID == 0 ||
                        dataset.Count != 0 && CurrentTbl84Synonym.SynonymID != 0 ||
                        dataset.Count == 0 && CurrentTbl84Synonym.SynonymID != 0) //new dataset and update
                    {
                        if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTbl84Synonym.SynonymName,
                                MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes)
                            return;
                        {
                            _businessLayer.UpdateSynonym(synonym);

                            WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess, CurrentTbl84Synonym.SynonymName,
                                MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        }
                    }
                }
            }
            catch (DbEntityValidationException ex)
            {
                _entityException.EntityException(ex);
            }

            if (CurrentTbl84Synonym.SynonymID == 0)  //new Dataset                        
                Tbl84SynonymsList = new ObservableCollection<Tbl84Synonym>(_businessLayer.ListTbl84SynonymsBySynonymName(CurrentTbl84Synonym.SynonymName));
            if (CurrentTbl84Synonym.SynonymID != 0)   //update 
                Tbl84SynonymsList = new ObservableCollection<Tbl84Synonym>(_businessLayer.ListTbl84SynonymsBySynonymId(CurrentTbl84Synonym.SynonymID));

            SelectedMainTabIndex = 3;
            SynonymsView = CollectionViewSource.GetDefaultView(Tbl84SynonymsList);
            SynonymsView.Refresh();
        }
        #endregion "Public Commands"                  
   
            

 //    Part 7    

             
       #region "Public Commands Connect ==> Tbl87Geographic"                 
        //-------------------------------------------------------------------------
        private RelayCommand _clearGeographicCommand;

        public ICommand ClearGeographicCommand => _clearGeographicCommand ??
                                                  (_clearGeographicCommand = new RelayCommand(delegate { ClearGeographic(null); }));

        private RelayCommand _getGeographicsByNameOrIdCommand;  

        public  ICommand GetGeographicsByNameOrIdCommand => _getGeographicsByNameOrIdCommand ??
                                                           (_getGeographicsByNameOrIdCommand = new RelayCommand(delegate { GetGeographicsByNameOrId(null); }));

        private RelayCommand _addGeographicCommand;

        public ICommand AddGeographicCommand => _addGeographicCommand ??
                                                (_addGeographicCommand = new RelayCommand(delegate { AddGeographic(null); }));

        private RelayCommand _copyGeographicCommand;

        public ICommand CopyGeographicCommand => _copyGeographicCommand ??
                                                 (_copyGeographicCommand = new RelayCommand(delegate { CopyGeographic(null); }));

        private RelayCommand _saveGeographicCommand;

        public ICommand SaveGeographicCommand => _saveGeographicCommand ??
                                                 (_saveGeographicCommand = new RelayCommand(delegate { SaveGeographic(null); }));

        //-------------------------------------------------------------------------          
               
        private void ClearGeographic(object o)
        {
            SearchGeographicName = string.Empty;
            Tbl87GeographicsList?.Clear();
        }
        //----------------------------------------------------------------------            
                         
        private void GetGeographicsByNameOrId(object o)
        {
            Tbl87GeographicsList =  new ObservableCollection<Tbl87Geographic>(_businessLayer.ListTbl87GeographicsByGeographicId(Convert.ToInt32(SearchGeographicName)));

            GeographicsView = CollectionViewSource.GetDefaultView(Tbl87GeographicsList);
        }
        //----------------------------------------------------------------------            
               
        private void AddGeographic(object o)      
        {
            Tbl87GeographicsList = new ObservableCollection<Tbl87Geographic> {new Tbl87Geographic
            {        FiSpeciesID = CurrentTbl69FiSpecies.FiSpeciesID      }    };

            GeographicsView = CollectionViewSource.GetDefaultView(Tbl87GeographicsList);
            GeographicsView.MoveCurrentToFirst();
        }
        //----------------------------------------------------------------------            
               
        private void CopyGeographic(object o)
        {
            Tbl87GeographicsList = new ObservableCollection<Tbl87Geographic>();

            var geographic = _businessLayer.SingleListTbl87GeographicsByGeographicId(CurrentTbl87Geographic.GeographicID);

            Tbl87GeographicsList.Add(new Tbl87Geographic
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
            GeographicsView.MoveCurrentToFirst();
        }
        //----------------------------------------------------------------------            
               
        private void SaveGeographic(object o)
        {
            try
            {
                var geographic = _businessLayer.SingleListTbl87GeographicsByGeographicId(CurrentTbl87Geographic.GeographicID);
                if (CurrentTbl87Geographic.GeographicID != 0)
                {
                    if (geographic != null) //update
                    {
                            geographic.PlSpeciesID = CurrentTbl87Geographic.PlSpeciesID;
                            geographic.FiSpeciesID = 3;
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
                             geographic.EntityState = EntityState.Modified;
                       }
                    }
                    else
                    {
                        geographic = new Tbl87Geographic     //add new
                        {
                            PlSpeciesID = CurrentTbl87Geographic.PlSpeciesID,
                            FiSpeciesID = 3,
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
                            Memo = CurrentTbl87Geographic.Memo,
                                EntityState = EntityState.Added
                                              };
                }
                {
                    //FiSpeciesID may be not 0
                    if (CurrentTbl87Geographic.FiSpeciesID == 0)          

                    {
                        WpfMessageBox.Show(CultRes.StringsRes.RequiredGenealogyConnect, CultRes.StringsRes.RequiredInput,
                            MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        return;
                    }

                    //check if dataset with Name and FiSpeciesId already exist       
                    var dataset = _businessLayer.ListTbl87GeographicsByGeographicIdAndFiSpeciesId(CurrentTbl87Geographic.GeographicID, CurrentTbl87Geographic.FiSpeciesID);

                    if (dataset.Count != 0 && CurrentTbl87Geographic.GeographicID == 0)  //dataset exist
                    {
                        WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTbl87Geographic.GeographicID.ToString(),
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                    }
                    if (dataset.Count == 0 && CurrentTbl87Geographic.GeographicID == 0 ||
                        dataset.Count != 0 && CurrentTbl87Geographic.GeographicID != 0 ||
                        dataset.Count == 0 && CurrentTbl87Geographic.GeographicID != 0) //new dataset and update
                    {
                        if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTbl87Geographic.GeographicID.ToString(),
                                MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes)
                            return;
                        {
                            _businessLayer.UpdateGeographic(geographic);

                            WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess, CurrentTbl87Geographic.GeographicID.ToString(),
                                MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        }
                    }
                }
            }
            catch (DbEntityValidationException ex)
            {
                _entityException.EntityException(ex);
            }

            Tbl87GeographicsList = new ObservableCollection<Tbl87Geographic>(_businessLayer.ListTbl87GeographicsByGeographicId(CurrentTbl87Geographic.GeographicID));

            SelectedMainTabIndex = 4;
            GeographicsView = CollectionViewSource.GetDefaultView(Tbl87GeographicsList);
            GeographicsView.Refresh();
        }
        #endregion "Public Commands"                  
   

 //    Part 8    

           
        #region "Public Commands Connect ==> Tbl90RefAuthor"
        //-------------------------------------------------------------------------
        private RelayCommand _clearReferenceAuthorCommand;
        public new ICommand ClearReferenceAuthorCommand => _clearReferenceAuthorCommand ??
                                                 (_clearReferenceAuthorCommand = new RelayCommand(delegate { ClearReferenceAuthor(null); }));

        private RelayCommand _getReferenceAuthorsByNameOrIdCommand;

        public new ICommand GetReferenceAuthorsByNameOrIdCommand => _getReferenceAuthorsByNameOrIdCommand ??
                                                            (_getReferenceAuthorsByNameOrIdCommand = new RelayCommand(delegate { GetReferenceAuthorsByNameOrId(null); }));

        private RelayCommand _addReferenceAuthorCommand;

        public new ICommand AddReferenceAuthorCommand => _addReferenceAuthorCommand ??
                                                    (_addReferenceAuthorCommand = new RelayCommand(delegate { AddReferenceAuthor(null); }));

        private RelayCommand _copyReferenceAuthorCommand;

        public new ICommand CopyReferenceAuthorCommand => _copyReferenceAuthorCommand ??
                        (_copyReferenceAuthorCommand = new RelayCommand(delegate { CopyReferenceAuthor(null); }));


        private RelayCommand _saveReferenceAuthorCommand;

        public new ICommand SaveReferenceAuthorCommand => _saveReferenceAuthorCommand ??
                     (_saveReferenceAuthorCommand = new RelayCommand(delegate { SaveReferenceAuthor(null); }));
        //-------------------------------------------------------------------------          
     
        private void ClearReferenceAuthor(object o)
        {
            SearchReferenceAuthorName = string.Empty;
            Tbl90ReferenceAuthorsList?.Clear();
        }
        //----------------------------------------------------------------------            
     
        public new void GetReferenceAuthorsByNameOrId(object o)
        {

            Tbl90ReferenceAuthorsList = int.TryParse(SearchReferenceAuthorName, out int id) ? 
                new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ReferencesByReferenceId(id)) : 
                new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ReferencesByReferenceName(SearchReferenceAuthorName));

            ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
        }
        //----------------------------------------------------------------------            
     
        public new void AddReferenceAuthor(object o)
        {
            Tbl90ReferenceAuthorsList = new ObservableCollection<Tbl90Reference> { new Tbl90Reference
            {
                Info = CultRes.StringsRes.DatasetNew,
                FiSpeciesID = CurrentTbl69FiSpecies.FiSpeciesID
            } };

            ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
            ReferenceAuthorsView.MoveCurrentToFirst();
         }
        //----------------------------------------------------------------------            
     
        public new void CopyReferenceAuthor(object o)
        {
            Tbl90ReferenceAuthorsList = new ObservableCollection<Tbl90Reference>();

            var reference = _businessLayer.SingleListTbl90ReferencesByReferenceId(CurrentTbl90ReferenceAuthor.ReferenceID);

            Tbl90ReferenceAuthorsList.Add(new Tbl90Reference
            {
                RefAuthorID = reference.RefAuthorID,
                FiSpeciesID = reference.FiSpeciesID,
                Valid = reference.Valid,
                ValidYear = reference.ValidYear,
                Info = CultRes.StringsRes.DatasetNew,
                Memo = reference.Memo
            });

            ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
            ReferenceAuthorsView.MoveCurrentToFirst();
        }
        //----------------------------------------------------------------------            
     
        public new void SaveReferenceAuthor(object o)
        {
            try
            {
                var reference = _businessLayer.SingleListTbl90ReferencesByReferenceId(CurrentTbl90ReferenceAuthor.ReferenceID);
                if (CurrentTbl90ReferenceAuthor.ReferenceID != 0)
                {
                    if (reference != null) //update
                    {
                        reference.RefExpertID = CurrentTbl90ReferenceAuthor.RefExpertID;
                        reference.RefAuthorID = CurrentTbl90ReferenceAuthor.RefAuthorID;
                        reference.RefSourceID = CurrentTbl90ReferenceAuthor.RefSourceID;
                        reference.RegnumID = CurrentTbl90ReferenceAuthor.RegnumID;
                        reference.PhylumID = CurrentTbl90ReferenceAuthor.PhylumID;
                        reference.DivisionID = CurrentTbl90ReferenceAuthor.DivisionID;
                        reference.SubphylumID = CurrentTbl90ReferenceAuthor.SubphylumID;
                        reference.SubdivisionID = CurrentTbl90ReferenceAuthor.SubdivisionID;
                        reference.SuperclassID = CurrentTbl90ReferenceAuthor.SuperclassID;
                        reference.ClassID = CurrentTbl90ReferenceAuthor.ClassID;
                        reference.SubclassID = CurrentTbl90ReferenceAuthor.SubclassID;
                        reference.InfraclassID = CurrentTbl90ReferenceAuthor.InfraclassID;
                        reference.LegioID = CurrentTbl90ReferenceAuthor.LegioID;
                        reference.OrdoID = CurrentTbl90ReferenceAuthor.OrdoID;
                        reference.SubordoID = CurrentTbl90ReferenceAuthor.SubordoID;
                        reference.InfraordoID = CurrentTbl90ReferenceAuthor.InfraordoID;
                        reference.SuperfamilyID = CurrentTbl90ReferenceAuthor.SuperfamilyID;
                        reference.FamilyID = CurrentTbl90ReferenceAuthor.FamilyID;
                        reference.SubfamilyID = CurrentTbl90ReferenceAuthor.SubfamilyID;
                        reference.InfrafamilyID = CurrentTbl90ReferenceAuthor.InfrafamilyID;
                        reference.SupertribusID = CurrentTbl90ReferenceAuthor.SupertribusID;
                        reference.TribusID = CurrentTbl90ReferenceAuthor.TribusID;
                        reference.SubtribusID = CurrentTbl90ReferenceAuthor.SubtribusID;
                        reference.InfratribusID = CurrentTbl90ReferenceAuthor.InfratribusID;
                        reference.GenusID = CurrentTbl90ReferenceAuthor.GenusID;
                        reference.PlSpeciesID = CurrentTbl90ReferenceAuthor.PlSpeciesID;
                        reference.FiSpeciesID = CurrentTbl90ReferenceAuthor.FiSpeciesID;
                        reference.Valid = CurrentTbl90ReferenceAuthor.Valid;
                        reference.ValidYear = CurrentTbl90ReferenceAuthor.ValidYear;
                        reference.Info = CurrentTbl90ReferenceAuthor.Info;
                        reference.Updater = Environment.UserName;
                        reference.UpdaterDate = DateTime.Now;
                        reference.Memo = CurrentTbl90ReferenceAuthor.Memo;

                        reference.EntityState = EntityState.Modified;
                    }
                }
                else
                {
                    reference = new Tbl90Reference     //add new
                    {
                        RefAuthorID = CurrentTbl90ReferenceAuthor.RefAuthorID,
                        RefSourceID = CurrentTbl90ReferenceAuthor.RefSourceID,
                        RefExpertID = CurrentTbl90ReferenceAuthor.RefExpertID,
                        RegnumID = CurrentTbl90ReferenceAuthor.RegnumID,
                        PhylumID = CurrentTbl90ReferenceAuthor.PhylumID,
                        DivisionID = CurrentTbl90ReferenceAuthor.DivisionID,
                        SubphylumID = CurrentTbl90ReferenceAuthor.SubphylumID,
                        SubdivisionID = CurrentTbl90ReferenceAuthor.SubdivisionID,
                        SuperclassID = CurrentTbl90ReferenceAuthor.SuperclassID,
                        ClassID = CurrentTbl90ReferenceAuthor.ClassID,
                        SubclassID = CurrentTbl90ReferenceAuthor.SubclassID,
                        InfraclassID = CurrentTbl90ReferenceAuthor.InfraclassID,
                        LegioID = CurrentTbl90ReferenceAuthor.LegioID,
                        OrdoID = CurrentTbl90ReferenceAuthor.OrdoID,
                        SubordoID = CurrentTbl90ReferenceAuthor.SubordoID,
                        InfraordoID = CurrentTbl90ReferenceAuthor.InfraordoID,
                        SuperfamilyID = CurrentTbl90ReferenceAuthor.SuperfamilyID,
                        FamilyID = CurrentTbl90ReferenceAuthor.FamilyID,
                        SubfamilyID = CurrentTbl90ReferenceAuthor.SubfamilyID,
                        InfrafamilyID = CurrentTbl90ReferenceAuthor.InfrafamilyID,
                        SupertribusID = CurrentTbl90ReferenceAuthor.SupertribusID,
                        TribusID = CurrentTbl90ReferenceAuthor.TribusID,
                        SubtribusID = CurrentTbl90ReferenceAuthor.SubtribusID,
                        InfratribusID = CurrentTbl90ReferenceAuthor.InfratribusID,
                        GenusID = CurrentTbl90ReferenceAuthor.GenusID,
                        PlSpeciesID = CurrentTbl90ReferenceAuthor.PlSpeciesID,
                        FiSpeciesID = CurrentTbl90ReferenceAuthor.FiSpeciesID,
                        CountID = RandomHelper.Randomnumber(),
                        Valid = CurrentTbl90ReferenceAuthor.Valid,
                        ValidYear = CurrentTbl90ReferenceAuthor.ValidYear,
                        Info = CurrentTbl90ReferenceAuthor.Info,
                        Memo = CurrentTbl90ReferenceAuthor.Memo,
                        Writer = Environment.UserName,
                        WriterDate = DateTime.Now,
                        Updater = Environment.UserName,
                        UpdaterDate = DateTime.Now,
                        EntityState = EntityState.Added
                    };
                }
                {
                    //RefExpertID or RefSourceID or RefAuthorID may be not 0
                    if (CurrentTbl90ReferenceAuthor.RefExpertID == null && CurrentTbl90ReferenceAuthor.RefSourceID == null && CurrentTbl90ReferenceAuthor.RefAuthorID == null)
                    {
                        WpfMessageBox.Show(CultRes.StringsRes.RequiredGenealogyConnect, CultRes.StringsRes.RequiredInput,
                            MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        return;
                    }

                    //check if dataset with vb-name already exist   
                    var dataset = _businessLayer.ListTbl90ReferencesByRefExpertIdAndRefSourceIdAndRefAuthorIdAndInfo(CurrentTbl90ReferenceAuthor);

                    if (dataset.Count != 0 && CurrentTbl90ReferenceAuthor.ReferenceID == 0)  //dataset exist
                    {
                        WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTbl90ReferenceAuthor.ReferenceID.ToString(),
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                    }
                    if (dataset.Count == 0 && CurrentTbl90ReferenceAuthor.ReferenceID == 0 ||
                        dataset.Count != 0 && CurrentTbl90ReferenceAuthor.ReferenceID != 0 ||
                        dataset.Count == 0 && CurrentTbl90ReferenceAuthor.ReferenceID != 0) //new dataset and update
                    {
                        if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTbl90ReferenceAuthor.ReferenceID.ToString(),
                                MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes)
                            return;
                        {
                            _businessLayer.UpdateReference(reference);

                            WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess, CurrentTbl90ReferenceAuthor.ReferenceID.ToString(),
                                MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        }
                    }
                }
            }
            catch (DbEntityValidationException ex)
            {
                _entityException.EntityException(ex);
            }

            if (CurrentTbl90ReferenceAuthor.ReferenceID == 0)  //new Dataset                        
                Tbl90ReferenceAuthorsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ReferencesByInfo(CurrentTbl90ReferenceAuthor.Info));
            if (CurrentTbl90ReferenceAuthor.ReferenceID != 0)   //update 
                Tbl90ReferenceAuthorsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ReferencesByReferenceId(CurrentTbl90ReferenceAuthor.ReferenceID));

            SelectedMainSubRefTabIndex = 2;

            ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
            ReferenceAuthorsView.Refresh();
        }
        #endregion "Public Commands"                  
           
        #region "Public Commands Connect ==> Tbl90RefSource" 
        //-------------------------------------------------------------------------
        private RelayCommand _clearReferenceSourceCommand;
        public new ICommand ClearReferenceSourceCommand => _clearReferenceSourceCommand ??
                                                       (_clearReferenceSourceCommand = new RelayCommand(delegate { ClearReferenceSource(null); }));

        private RelayCommand _getReferenceSourcesByNameOrIdCommand;

        public new ICommand GetReferenceSourcesByNameOrIdCommand => _getReferenceSourcesByNameOrIdCommand ??
                                                            (_getReferenceSourcesByNameOrIdCommand = new RelayCommand(delegate { GetReferenceSourcesByNameOrId(null); }));

        private RelayCommand _addReferenceSourceCommand;

        public new ICommand AddReferenceSourceCommand => _addReferenceSourceCommand ??
                                                    (_addReferenceSourceCommand = new RelayCommand(delegate { AddReferenceSource(null); }));

        private RelayCommand _copyReferenceSourceCommand;

        public new ICommand CopyReferenceSourceCommand => _copyReferenceSourceCommand ??
                        (_copyReferenceSourceCommand = new RelayCommand(delegate { CopyReferenceSource(null); }));


        private RelayCommand _saveReferenceSourceCommand;

        public new ICommand SaveReferenceSourceCommand => _saveReferenceSourceCommand ??
                     (_saveReferenceSourceCommand = new RelayCommand(delegate { SaveReferenceSource(null); }));

        //-------------------------------------------------------------------------          
     
        private void ClearReferenceSource(object o)
        {
            SearchReferenceSourceName = string.Empty;
            Tbl90ReferenceSourcesList?.Clear();
        }
        //----------------------------------------------------------------------            
     
        public new void GetReferenceSourcesByNameOrId(object o)
        {
            Tbl90ReferenceSourcesList = int.TryParse(SearchReferenceSourceName, out var id) ?
                new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ReferencesByReferenceId(id)) :
                new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ReferencesByReferenceName(SearchReferenceSourceName));


            ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
            ReferenceSourcesView.Refresh();
        }
        //----------------------------------------------------------------------            
     
        public new void AddReferenceSource(object o)
        {
            Tbl90ReferenceSourcesList = new ObservableCollection<Tbl90Reference> { new Tbl90Reference
            {
                Info = CultRes.StringsRes.DatasetNew,
                FiSpeciesID = CurrentTbl69FiSpecies.FiSpeciesID
            } };

            ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
            ReferenceSourcesView.MoveCurrentToFirst();
         }
        //----------------------------------------------------------------------            
     
        public new void CopyReferenceSource(object o)
        {

            Tbl90ReferenceSourcesList = new ObservableCollection<Tbl90Reference>();

            var reference = _businessLayer.SingleListTbl90ReferencesByReferenceId(CurrentTbl90ReferenceSource.ReferenceID);

            Tbl90ReferenceSourcesList.Add(new Tbl90Reference
            {
                RefSourceID = reference.RefSourceID,
                FiSpeciesID = reference.FiSpeciesID,
                Valid = reference.Valid,
                ValidYear = reference.ValidYear,
                Info = CultRes.StringsRes.DatasetNew,
                Memo = reference.Memo
            });

            ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
            ReferenceSourcesView.MoveCurrentToFirst();
        }
        //----------------------------------------------------------------------            
     
        public new void SaveReferenceSource(object o)
        {
            try
            {
                var reference = _businessLayer.SingleListTbl90ReferencesByReferenceId(CurrentTbl90ReferenceSource.ReferenceID);
                if (CurrentTbl90ReferenceSource.ReferenceID != 0)
                {
                    if (reference != null) //update
                    {
                        reference.RefExpertID = CurrentTbl90ReferenceSource.RefExpertID;
                        reference.RefAuthorID = CurrentTbl90ReferenceSource.RefAuthorID;
                        reference.RefSourceID = CurrentTbl90ReferenceSource.RefSourceID;
                        reference.RegnumID = CurrentTbl90ReferenceSource.RegnumID;
                        reference.PhylumID = CurrentTbl90ReferenceSource.PhylumID;
                        reference.DivisionID = CurrentTbl90ReferenceSource.DivisionID;
                        reference.SubphylumID = CurrentTbl90ReferenceSource.SubphylumID;
                        reference.SubdivisionID = CurrentTbl90ReferenceSource.SubdivisionID;
                        reference.SuperclassID = CurrentTbl90ReferenceSource.SuperclassID;
                        reference.ClassID = CurrentTbl90ReferenceSource.ClassID;
                        reference.SubclassID = CurrentTbl90ReferenceSource.SubclassID;
                        reference.InfraclassID = CurrentTbl90ReferenceSource.InfraclassID;
                        reference.LegioID = CurrentTbl90ReferenceSource.LegioID;
                        reference.OrdoID = CurrentTbl90ReferenceSource.OrdoID;
                        reference.SubordoID = CurrentTbl90ReferenceSource.SubordoID;
                        reference.InfraordoID = CurrentTbl90ReferenceSource.InfraordoID;
                        reference.SuperfamilyID = CurrentTbl90ReferenceSource.SuperfamilyID;
                        reference.FamilyID = CurrentTbl90ReferenceSource.FamilyID;
                        reference.SubfamilyID = CurrentTbl90ReferenceSource.SubfamilyID;
                        reference.InfrafamilyID = CurrentTbl90ReferenceSource.InfrafamilyID;
                        reference.SupertribusID = CurrentTbl90ReferenceSource.SupertribusID;
                        reference.TribusID = CurrentTbl90ReferenceSource.TribusID;
                        reference.SubtribusID = CurrentTbl90ReferenceSource.SubtribusID;
                        reference.InfratribusID = CurrentTbl90ReferenceSource.InfratribusID;
                        reference.GenusID = CurrentTbl90ReferenceSource.GenusID;
                        reference.PlSpeciesID = CurrentTbl90ReferenceSource.PlSpeciesID;
                        reference.FiSpeciesID = CurrentTbl90ReferenceSource.FiSpeciesID;
                        reference.Valid = CurrentTbl90ReferenceSource.Valid;
                        reference.ValidYear = CurrentTbl90ReferenceSource.ValidYear;
                        reference.Info = CurrentTbl90ReferenceSource.Info;
                        reference.Updater = Environment.UserName;
                        reference.UpdaterDate = DateTime.Now;
                        reference.Memo = CurrentTbl90ReferenceSource.Memo;

                        reference.EntityState = EntityState.Modified;
                    }
                }
                else
                {
                    reference = new Tbl90Reference     //add new
                    {
                        RefAuthorID = CurrentTbl90ReferenceSource.RefAuthorID,
                        RefSourceID = CurrentTbl90ReferenceSource.RefSourceID,
                        RefExpertID = CurrentTbl90ReferenceSource.RefExpertID,
                        RegnumID = CurrentTbl90ReferenceSource.RegnumID,
                        PhylumID = CurrentTbl90ReferenceSource.PhylumID,
                        DivisionID = CurrentTbl90ReferenceSource.DivisionID,
                        SubphylumID = CurrentTbl90ReferenceSource.SubphylumID,
                        SubdivisionID = CurrentTbl90ReferenceSource.SubdivisionID,
                        SuperclassID = CurrentTbl90ReferenceSource.SuperclassID,
                        ClassID = CurrentTbl90ReferenceSource.ClassID,
                        SubclassID = CurrentTbl90ReferenceSource.SubclassID,
                        InfraclassID = CurrentTbl90ReferenceSource.InfraclassID,
                        LegioID = CurrentTbl90ReferenceSource.LegioID,
                        OrdoID = CurrentTbl90ReferenceSource.OrdoID,
                        SubordoID = CurrentTbl90ReferenceSource.SubordoID,
                        InfraordoID = CurrentTbl90ReferenceSource.InfraordoID,
                        SuperfamilyID = CurrentTbl90ReferenceSource.SuperfamilyID,
                        FamilyID = CurrentTbl90ReferenceSource.FamilyID,
                        SubfamilyID = CurrentTbl90ReferenceSource.SubfamilyID,
                        InfrafamilyID = CurrentTbl90ReferenceSource.InfrafamilyID,
                        SupertribusID = CurrentTbl90ReferenceSource.SupertribusID,
                        TribusID = CurrentTbl90ReferenceSource.TribusID,
                        SubtribusID = CurrentTbl90ReferenceSource.SubtribusID,
                        InfratribusID = CurrentTbl90ReferenceSource.InfratribusID,
                        GenusID = CurrentTbl90ReferenceSource.GenusID,
                        PlSpeciesID = CurrentTbl90ReferenceSource.PlSpeciesID,
                        FiSpeciesID = CurrentTbl90ReferenceSource.FiSpeciesID,
                        CountID = RandomHelper.Randomnumber(),
                        Valid = CurrentTbl90ReferenceSource.Valid,
                        ValidYear = CurrentTbl90ReferenceSource.ValidYear,
                        Info = CurrentTbl90ReferenceSource.Info,
                        Memo = CurrentTbl90ReferenceSource.Memo,
                        Writer = Environment.UserName,
                        WriterDate = DateTime.Now,
                        Updater = Environment.UserName,
                        UpdaterDate = DateTime.Now,
                        EntityState = EntityState.Added
                    };
                }
                {
                    //RefExpertID or RefSourceID or RefAuthorID may be not 0
                    if (CurrentTbl90ReferenceSource.RefExpertID == null && CurrentTbl90ReferenceSource.RefSourceID == null && CurrentTbl90ReferenceSource.RefAuthorID == null)
                    {
                        WpfMessageBox.Show(CultRes.StringsRes.RequiredGenealogyConnect, CultRes.StringsRes.RequiredInput,
                            MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        return;
                    }

                    //check if dataset with vb-name already exist   
                    var dataset = _businessLayer.ListTbl90ReferencesByRefExpertIdAndRefSourceIdAndRefAuthorIdAndInfo(CurrentTbl90ReferenceSource);

                    if (dataset.Count != 0 && CurrentTbl90ReferenceSource.ReferenceID == 0)  //dataset exist
                    {
                        WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTbl90ReferenceSource.ReferenceID.ToString(),
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                    }
                    if (dataset.Count == 0 && CurrentTbl90ReferenceSource.ReferenceID == 0 ||
                        dataset.Count != 0 && CurrentTbl90ReferenceSource.ReferenceID != 0 ||
                        dataset.Count == 0 && CurrentTbl90ReferenceSource.ReferenceID != 0) //new dataset and update
                    {
                        if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTbl90ReferenceSource.ReferenceID.ToString(),
                                MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes)
                            return;
                        {
                            _businessLayer.UpdateReference(reference);

                            WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess, CurrentTbl90ReferenceSource.ReferenceID.ToString(),
                                MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        }
                    }
                }
            }
            catch (DbEntityValidationException ex)
            {
                _entityException.EntityException(ex);
            }

            if (CurrentTbl90ReferenceSource.ReferenceID == 0)  //new Dataset                        
                Tbl90ReferenceSourcesList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ReferencesByInfo(CurrentTbl90ReferenceSource.Info));
            if (CurrentTbl90ReferenceSource.ReferenceID != 0)   //update 
                Tbl90ReferenceSourcesList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ReferencesByReferenceId(CurrentTbl90ReferenceSource.ReferenceID));

            SelectedMainSubRefTabIndex = 1;

            ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
            ReferenceSourcesView.Refresh();
        }
        #endregion "Public Commands"                  
           
        #region "Public Commands Connect ==> Tbl90RefExpert"

        //-------------------------------------------------------------------------
        private RelayCommand _clearReferenceExpertCommand;
        public new ICommand ClearReferenceExpertCommand => _clearReferenceExpertCommand ??
                                                       (_clearReferenceExpertCommand = new RelayCommand(delegate { ClearReferenceExpert(null); }));

        private RelayCommand _getReferenceExpertsByNameOrIdCommand;

        public new ICommand GetReferenceExpertsByNameOrIdCommand => _getReferenceExpertsByNameOrIdCommand ??
                                                            (_getReferenceExpertsByNameOrIdCommand = new RelayCommand(delegate { GetReferenceExpertsByNameOrId(null); }));

        private RelayCommand _addReferenceExpertCommand;

        public new ICommand AddReferenceExpertCommand => _addReferenceExpertCommand ??
                                                    (_addReferenceExpertCommand = new RelayCommand(delegate { AddReferenceExpert(null); }));

        private RelayCommand _copyReferenceExpertCommand;

        public new ICommand CopyReferenceExpertCommand => _copyReferenceExpertCommand ??
                        (_copyReferenceExpertCommand = new RelayCommand(delegate { CopyReferenceExpert(null); }));


        private RelayCommand _saveReferenceExpertCommand;

        public new ICommand SaveReferenceExpertCommand => _saveReferenceExpertCommand ??
                     (_saveReferenceExpertCommand = new RelayCommand(delegate { SaveReferenceExpert(null); }));
        //-------------------------------------------------------------------------          
     
        private void ClearReferenceExpert(object o)
        {
            SearchReferenceExpertName = string.Empty;
            Tbl90ReferenceExpertsList?.Clear();
        }
        //----------------------------------------------------------------------            
     
        public new void GetReferenceExpertsByNameOrId(object o)
        {
            Tbl90ReferenceExpertsList = int.TryParse(SearchReferenceExpertName, out var id) ?
                new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ReferencesByReferenceId(id)) :
                new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ReferencesByReferenceName(SearchReferenceExpertName));

            ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
            ReferenceExpertsView.Refresh();
         }
        //----------------------------------------------------------------------            
     
        public new void AddReferenceExpert(object o)
        {
            Tbl90ReferenceExpertsList = new ObservableCollection<Tbl90Reference> { new Tbl90Reference
            {
                Info = CultRes.StringsRes.DatasetNew,
                FiSpeciesID = CurrentTbl69FiSpecies.FiSpeciesID
            } };

            ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
            ReferenceExpertsView.MoveCurrentToFirst();
         }
        //----------------------------------------------------------------------            
     
        public new void CopyReferenceExpert(object o)
        {
            Tbl90ReferenceExpertsList = new ObservableCollection<Tbl90Reference>();

            var reference = _businessLayer.SingleListTbl90ReferencesByReferenceId(CurrentTbl90ReferenceExpert.ReferenceID);

            Tbl90ReferenceExpertsList.Add(new Tbl90Reference
            {
                RefExpertID = reference.RefExpertID,
                FiSpeciesID = reference.FiSpeciesID,
                Valid = reference.Valid,
                ValidYear = reference.ValidYear,
                Info = CultRes.StringsRes.DatasetNew,
                Memo = reference.Memo
            });

            ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
            ReferenceExpertsView.MoveCurrentToFirst();
        }
        //----------------------------------------------------------------------            
     
        public new void SaveReferenceExpert(object o)
        {
            try
            {
                var reference = _businessLayer.SingleListTbl90ReferencesByReferenceId(CurrentTbl90ReferenceExpert.ReferenceID);
                if (CurrentTbl90ReferenceExpert.ReferenceID != 0)
                {
                    if (reference != null) //update
                    {
                        reference.RefExpertID = CurrentTbl90ReferenceExpert.RefExpertID;
                        reference.RefAuthorID = CurrentTbl90ReferenceExpert.RefAuthorID;
                        reference.RefSourceID = CurrentTbl90ReferenceExpert.RefSourceID;
                        reference.RegnumID = CurrentTbl90ReferenceExpert.RegnumID;
                        reference.PhylumID = CurrentTbl90ReferenceExpert.PhylumID;
                        reference.DivisionID = CurrentTbl90ReferenceExpert.DivisionID;
                        reference.SubphylumID = CurrentTbl90ReferenceExpert.SubphylumID;
                        reference.SubdivisionID = CurrentTbl90ReferenceExpert.SubdivisionID;
                        reference.SuperclassID = CurrentTbl90ReferenceExpert.SuperclassID;
                        reference.ClassID = CurrentTbl90ReferenceExpert.ClassID;
                        reference.SubclassID = CurrentTbl90ReferenceExpert.SubclassID;
                        reference.InfraclassID = CurrentTbl90ReferenceExpert.InfraclassID;
                        reference.LegioID = CurrentTbl90ReferenceExpert.LegioID;
                        reference.OrdoID = CurrentTbl90ReferenceExpert.OrdoID;
                        reference.SubordoID = CurrentTbl90ReferenceExpert.SubordoID;
                        reference.InfraordoID = CurrentTbl90ReferenceExpert.InfraordoID;
                        reference.SuperfamilyID = CurrentTbl90ReferenceExpert.SuperfamilyID;
                        reference.FamilyID = CurrentTbl90ReferenceExpert.FamilyID;
                        reference.SubfamilyID = CurrentTbl90ReferenceExpert.SubfamilyID;
                        reference.InfrafamilyID = CurrentTbl90ReferenceExpert.InfrafamilyID;
                        reference.SupertribusID = CurrentTbl90ReferenceExpert.SupertribusID;
                        reference.TribusID = CurrentTbl90ReferenceExpert.TribusID;
                        reference.SubtribusID = CurrentTbl90ReferenceExpert.SubtribusID;
                        reference.InfratribusID = CurrentTbl90ReferenceExpert.InfratribusID;
                        reference.GenusID = CurrentTbl90ReferenceExpert.GenusID;
                        reference.PlSpeciesID = CurrentTbl90ReferenceExpert.PlSpeciesID;
                        reference.FiSpeciesID = CurrentTbl90ReferenceExpert.FiSpeciesID;
                        reference.Valid = CurrentTbl90ReferenceExpert.Valid;
                        reference.ValidYear = CurrentTbl90ReferenceExpert.ValidYear;
                        reference.Info = CurrentTbl90ReferenceExpert.Info;
                        reference.Updater = Environment.UserName;
                        reference.UpdaterDate = DateTime.Now;
                        reference.Memo = CurrentTbl90ReferenceExpert.Memo;

                        reference.EntityState = EntityState.Modified;
                    }
                }
                else
                {
                    reference = new Tbl90Reference     //add new
                    {
                        RefAuthorID = CurrentTbl90ReferenceExpert.RefAuthorID,
                        RefSourceID = CurrentTbl90ReferenceExpert.RefSourceID,
                        RefExpertID = CurrentTbl90ReferenceExpert.RefExpertID,
                        RegnumID = CurrentTbl90ReferenceExpert.RegnumID,
                        PhylumID = CurrentTbl90ReferenceExpert.PhylumID,
                        DivisionID = CurrentTbl90ReferenceExpert.DivisionID,
                        SubphylumID = CurrentTbl90ReferenceExpert.SubphylumID,
                        SubdivisionID = CurrentTbl90ReferenceExpert.SubdivisionID,
                        SuperclassID = CurrentTbl90ReferenceExpert.SuperclassID,
                        ClassID = CurrentTbl90ReferenceExpert.ClassID,
                        SubclassID = CurrentTbl90ReferenceExpert.SubclassID,
                        InfraclassID = CurrentTbl90ReferenceExpert.InfraclassID,
                        LegioID = CurrentTbl90ReferenceExpert.LegioID,
                        OrdoID = CurrentTbl90ReferenceExpert.OrdoID,
                        SubordoID = CurrentTbl90ReferenceExpert.SubordoID,
                        InfraordoID = CurrentTbl90ReferenceExpert.InfraordoID,
                        SuperfamilyID = CurrentTbl90ReferenceExpert.SuperfamilyID,
                        FamilyID = CurrentTbl90ReferenceExpert.FamilyID,
                        SubfamilyID = CurrentTbl90ReferenceExpert.SubfamilyID,
                        InfrafamilyID = CurrentTbl90ReferenceExpert.InfrafamilyID,
                        SupertribusID = CurrentTbl90ReferenceExpert.SupertribusID,
                        TribusID = CurrentTbl90ReferenceExpert.TribusID,
                        SubtribusID = CurrentTbl90ReferenceExpert.SubtribusID,
                        InfratribusID = CurrentTbl90ReferenceExpert.InfratribusID,
                        GenusID = CurrentTbl90ReferenceExpert.GenusID,
                        PlSpeciesID = CurrentTbl90ReferenceExpert.PlSpeciesID,
                        FiSpeciesID = CurrentTbl90ReferenceExpert.FiSpeciesID,
                        CountID = RandomHelper.Randomnumber(),
                        Valid = CurrentTbl90ReferenceExpert.Valid,
                        ValidYear = CurrentTbl90ReferenceExpert.ValidYear,
                        Info = CurrentTbl90ReferenceExpert.Info,
                        Memo = CurrentTbl90ReferenceExpert.Memo,
                        Writer = Environment.UserName,
                        WriterDate = DateTime.Now,
                        Updater = Environment.UserName,
                        UpdaterDate = DateTime.Now,
                        EntityState = EntityState.Added
                    };
                }
                {
                    //RefExpertID or RefSourceID or RefAuthorID may be not 0
                    if (CurrentTbl90ReferenceExpert.RefExpertID == null && CurrentTbl90ReferenceExpert.RefSourceID == null && CurrentTbl90ReferenceExpert.RefAuthorID == null)
                    {
                        WpfMessageBox.Show(CultRes.StringsRes.RequiredGenealogyConnect, CultRes.StringsRes.RequiredInput,
                            MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        return;
                    }

                    //check if dataset with vb-name already exist   
                    var dataset = _businessLayer.ListTbl90ReferencesByRefExpertIdAndRefSourceIdAndRefAuthorIdAndInfo(CurrentTbl90ReferenceExpert);

                    if (dataset.Count != 0 && CurrentTbl90ReferenceExpert.ReferenceID == 0)  //dataset exist
                    {
                        WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTbl90ReferenceExpert.ReferenceID.ToString(),
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                    }
                    if (dataset.Count == 0 && CurrentTbl90ReferenceExpert.ReferenceID == 0 ||
                        dataset.Count != 0 && CurrentTbl90ReferenceExpert.ReferenceID != 0 ||
                        dataset.Count == 0 && CurrentTbl90ReferenceExpert.ReferenceID != 0) //new dataset and update
                    {
                        if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTbl90ReferenceExpert.ReferenceID.ToString(),
                                MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes)
                            return;
                        {
                            _businessLayer.UpdateReference(reference);

                            WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess, CurrentTbl90ReferenceExpert.ReferenceID.ToString(),
                                MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        }
                    }
                }
            }
            catch (DbEntityValidationException ex)
            {
                _entityException.EntityException(ex);
            }

            if (CurrentTbl90ReferenceExpert.ReferenceID == 0)  //new Dataset                        
                Tbl90ReferenceExpertsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ReferencesByInfo(CurrentTbl90ReferenceExpert.Info));
            if (CurrentTbl90ReferenceExpert.ReferenceID != 0)   //update 
                Tbl90ReferenceExpertsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ReferencesByReferenceId(CurrentTbl90ReferenceExpert.ReferenceID));

            SelectedMainSubRefTabIndex = 0;

            ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
            ReferenceExpertsView.Refresh();
        }
        #endregion "Public Commands"                  
           
        #region "Public Commands Connect ==> Tbl93Comment"

        //-------------------------------------------------------------------------
        private RelayCommand _clearCommentCommand;
        public new ICommand ClearCommentCommand => _clearCommentCommand ??
                                               (_clearCommentCommand = new RelayCommand(delegate { ClearComment(null); }));

        private RelayCommand _getCommentsByNameOrIdCommand;

        public new ICommand GetCommentsByNameOrIdCommand => _getCommentsByNameOrIdCommand ??
                                                            (_getCommentsByNameOrIdCommand = new RelayCommand(delegate { GetCommentsByNameOrId(null); }));

        private RelayCommand _addCommentCommand;

        public new ICommand AddCommentCommand => _addCommentCommand ??
                                                 (_addCommentCommand = new RelayCommand(delegate { AddComment(null); }));

        private RelayCommand _copyCommentCommand;

        public new ICommand CopyCommentCommand => _copyCommentCommand ??
                                                  (_copyCommentCommand = new RelayCommand(delegate { CopyComment(null); }));

        private RelayCommand _saveCommentCommand;

        public new ICommand SaveCommentCommand => _saveCommentCommand ??
                                                  (_saveCommentCommand = new RelayCommand(delegate { SaveComment(null); }));
        //-------------------------------------------------------------------------          
     
        private void ClearComment(object o)
        {
            SearchCommentInfo = string.Empty;
            Tbl93CommentsList?.Clear();
        }
        //----------------------------------------------------------------------            
     
        public new void GetCommentsByNameOrId(object o)
        {
            Tbl93CommentsList = int.TryParse(SearchCommentInfo, out int id) ? 
                new ObservableCollection<Tbl93Comment>(_businessLayer.ListTbl93CommentsByCommentId(id)) : 
                new ObservableCollection<Tbl93Comment>(_businessLayer.ListTbl93CommentsByInfo(SearchCommentInfo));

            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
        }            
        //----------------------------------------------------------------------            
     
        public  new void AddComment(object o)
        {
            Tbl93CommentsList = new ObservableCollection<Tbl93Comment> { new Tbl93Comment
            {
                Info = CultRes.StringsRes.DatasetNew,
                FiSpeciesID = CurrentTbl69FiSpecies.FiSpeciesID
            } };

            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            CommentsView.MoveCurrentToFirst();
         }
        //----------------------------------------------------------------------            
     
        public new void CopyComment(object o)
        {
            Tbl93CommentsList = new ObservableCollection<Tbl93Comment>();

            var comment = _businessLayer.SingleListTbl93CommentsByCommentId(CurrentTbl93Comment.CommentID);

            Tbl93CommentsList.Add(new Tbl93Comment
            {
                FiSpeciesID = comment.FiSpeciesID,
                Valid = comment.Valid,
                ValidYear = comment.ValidYear,
                Info = CultRes.StringsRes.DatasetNew,
                Memo = comment.Memo
            });

            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            CommentsView.MoveCurrentToFirst();
        }
        //----------------------------------------------------------------------            
     
        private void SaveComment(object o)
        {
            try
            {
                var comment = _businessLayer.SingleListTbl93CommentsByCommentId(CurrentTbl93Comment.CommentID);
                if (CurrentTbl93Comment.CommentID != 0)
                {
                    if (comment != null) //update
                    {
                        comment.RegnumID = CurrentTbl93Comment.RegnumID;
                        comment.PhylumID = CurrentTbl93Comment.PhylumID;
                        comment.DivisionID = CurrentTbl93Comment.DivisionID;
                        comment.SubphylumID = CurrentTbl93Comment.SubphylumID;
                        comment.SubdivisionID = CurrentTbl93Comment.SubdivisionID;
                        comment.SuperclassID = CurrentTbl93Comment.SuperclassID;
                        comment.ClassID = CurrentTbl93Comment.ClassID;
                        comment.SubclassID = CurrentTbl93Comment.SubclassID;
                        comment.InfraclassID = CurrentTbl93Comment.InfraclassID;
                        comment.LegioID = CurrentTbl93Comment.LegioID;
                        comment.OrdoID = CurrentTbl93Comment.OrdoID;
                        comment.SubordoID = CurrentTbl93Comment.SubordoID;
                        comment.InfraordoID = CurrentTbl93Comment.InfraordoID;
                        comment.SuperfamilyID = CurrentTbl93Comment.SuperfamilyID;
                        comment.FamilyID = CurrentTbl93Comment.FamilyID;
                        comment.SubfamilyID = CurrentTbl93Comment.SubfamilyID;
                        comment.InfrafamilyID = CurrentTbl93Comment.InfrafamilyID;
                        comment.SupertribusID = CurrentTbl93Comment.SupertribusID;
                        comment.TribusID = CurrentTbl93Comment.TribusID;
                        comment.SubtribusID = CurrentTbl93Comment.SubtribusID;
                        comment.InfratribusID = CurrentTbl93Comment.InfratribusID;
                        comment.GenusID = CurrentTbl93Comment.GenusID;
                        comment.PlSpeciesID = CurrentTbl93Comment.PlSpeciesID;
                        comment.FiSpeciesID = CurrentTbl93Comment.FiSpeciesID;
                        comment.Valid = CurrentTbl93Comment.Valid;
                        comment.ValidYear = CurrentTbl93Comment.ValidYear;
                        comment.Info = CurrentTbl93Comment.Info;
                        comment.Memo = CurrentTbl93Comment.Memo;
                        comment.Updater = Environment.UserName;
                        comment.UpdaterDate = DateTime.Now;
                        comment.EntityState = EntityState.Modified;
                    }
                }
                else
                {
                    comment = new Tbl93Comment     //add new
                    {
                        RegnumID = CurrentTbl93Comment.RegnumID,
                        PhylumID = CurrentTbl93Comment.PhylumID,
                        DivisionID = CurrentTbl93Comment.DivisionID,
                        SubphylumID = CurrentTbl93Comment.SubphylumID,
                        SubdivisionID = CurrentTbl93Comment.SubdivisionID,
                        SuperclassID = CurrentTbl93Comment.SuperclassID,
                        ClassID = CurrentTbl93Comment.ClassID,
                        SubclassID = CurrentTbl93Comment.SubclassID,
                        InfraclassID = CurrentTbl93Comment.InfraclassID,
                        LegioID = CurrentTbl93Comment.LegioID,
                        OrdoID = CurrentTbl93Comment.OrdoID,
                        SubordoID = CurrentTbl93Comment.SubordoID,
                        InfraordoID = CurrentTbl93Comment.InfraordoID,
                        SuperfamilyID = CurrentTbl93Comment.SuperfamilyID,
                        FamilyID = CurrentTbl93Comment.FamilyID,
                        SubfamilyID = CurrentTbl93Comment.SubfamilyID,
                        InfrafamilyID = CurrentTbl93Comment.InfrafamilyID,
                        SupertribusID = CurrentTbl93Comment.SupertribusID,
                        TribusID = CurrentTbl93Comment.TribusID,
                        SubtribusID = CurrentTbl93Comment.SubtribusID,
                        InfratribusID = CurrentTbl93Comment.InfratribusID,
                        GenusID = CurrentTbl93Comment.GenusID,
                        PlSpeciesID = CurrentTbl93Comment.PlSpeciesID,
                        FiSpeciesID = CurrentTbl93Comment.FiSpeciesID,
                        CountID = RandomHelper.Randomnumber(),
                        Valid = CurrentTbl93Comment.Valid,
                        ValidYear = CurrentTbl93Comment.ValidYear,
                        Info = CurrentTbl93Comment.Info,
                        Memo = CurrentTbl93Comment.Memo,
                        Writer = Environment.UserName,
                        WriterDate = DateTime.Now,
                        Updater = Environment.UserName,
                        UpdaterDate = DateTime.Now,
                        EntityState = EntityState.Added
                    };
                }
                {
                    //check if dataset with Name and VbIds already exist       
                    var dataset = _businessLayer.ListTbl93CommentsByCurrentItem(CurrentTbl93Comment);

                    if (dataset.Count != 0 && CurrentTbl93Comment.CommentID == 0)  //dataset exist
                    {
                        WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTbl93Comment.Info,
                            MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                    }

                    if (dataset.Count == 0 && CurrentTbl93Comment.CommentID == 0 ||
                        dataset.Count != 0 && CurrentTbl93Comment.CommentID != 0 ||
                        dataset.Count == 0 && CurrentTbl93Comment.CommentID != 0) //new dataset and update
                    {
                        if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTbl93Comment.Info,
                                MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes)
                            return;
                        {
                            _businessLayer.UpdateComment(comment);

                            WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess, CurrentTbl93Comment.Info,
                                MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        }
                    }
                }
            }
            catch (DbEntityValidationException ex)
            {
                _entityException.EntityException(ex);
            }

            if (CurrentTbl93Comment.CommentID == 0)  //new Dataset                        
                Tbl93CommentsList = new ObservableCollection<Tbl93Comment>(_businessLayer.ListTbl93CommentsByInfo(CurrentTbl93Comment.Info));
            if (CurrentTbl93Comment.CommentID != 0)   //update 
                Tbl93CommentsList = new ObservableCollection<Tbl93Comment>(_businessLayer.ListTbl93CommentsByCommentId(CurrentTbl93Comment.CommentID));

            SelectedMainTabIndex = 3;
            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            CommentsView.Refresh();
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
            Tbl66GenussesList = new ObservableCollection<Tbl66Genus>(
                _businessLayer.ListTbl66GenussesByGenusId(CurrentTbl69FiSpecies.GenusID));

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
                    Tbl68SpeciesgroupsList = new ObservableCollection<Tbl68Speciesgroup>(
                        _businessLayer.ListTbl68SpeciesgroupsBySpeciesgroupId(CurrentTbl69FiSpecies.SpeciesgroupID);

                    SpeciesgroupsView = CollectionViewSource.GetDefaultView(Tbl68SpeciesgroupsList);
                    SpeciesgroupsView.Refresh();

                    SelectedMainTabIndex = 1;
                    SelectedDetailTabIndex = 7;
                }
                if (_selectedDetailSubTabIndex == 2)
                {
                    Tbl78NamesList = new ObservableCollection<Tbl78Name>(
                        _businessLayer.ListTbl78NamesByFiSpeciesId(CurrentTbl69FiSpecies.FiSpeciesID));

                    NamesView = CollectionViewSource.GetDefaultView(Tbl78NamesList);
                    NamesView.Refresh();

                    SelectedMainTabIndex = 2;
                    SelectedDetailTabIndex = 7;
                }
                if (_selectedDetailSubTabIndex == 3)
                {
                    Tbl81ImagesList = new ObservableCollection<Tbl81Image>(
                        _businessLayer.ListTbl81ImagesByFiSpeciesId(CurrentTbl69FiSpecies.FiSpeciesID));

                    ImagesView = CollectionViewSource.GetDefaultView(Tbl81ImagesList);
                    ImagesView.Refresh();

                    SelectedMainTabIndex = 3;
                    SelectedDetailTabIndex = 7;
                }
                if (_selectedDetailSubTabIndex == 4)
                {
                    Tbl84SynonymsList = new ObservableCollection<Tbl84Synonym>(
                        _businessLayer.ListTbl84SynonymsByFiSpeciesId(CurrentTbl69FiSpecies.FiSpeciesID));

                    SynonymsView = CollectionViewSource.GetDefaultView(Tbl84SynonymsList);
                    SynonymsView.Refresh();

                    SelectedMainTabIndex = 4;
                    SelectedDetailTabIndex = 7;
                }
                if (_selectedDetailSubTabIndex == 5)
                {
                    TblCountriesAllList = new ObservableCollection<TblCountry>(
                        _businessLayer.ListTblCountries());
 
                    Tbl87GeographicsList = new ObservableCollection<Tbl87Geographic>(
                        _businessLayer.ListTbl87GeographicsByFiSpeciesId(CurrentTbl69FiSpecies.FiSpeciesID));

                    GeographicsView = CollectionViewSource.GetDefaultView(Tbl87GeographicsList);
                    GeographicsView.Refresh();

                    SelectedMainTabIndex = 5;
                    SelectedDetailTabIndex = 7;
                }
                if (_selectedDetailSubTabIndex == 6)
                {
                    Tbl90ExpertsAllList = new ObservableCollection<Tbl90RefExpert>(
                        _businessLayer.ListTbl90RefExperts());
                    Tbl90ReferenceExpertsList = new ObservableCollection<Tbl90Reference>(
                        _businessLayer.ListTbl90ReferenceListRefExpertsByFiSpeciesId(CurrentTbl69FiSpecies.FiSpeciesID));

                    ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
                    ReferenceExpertsView.Refresh();

                    SelectedMainTabIndex = 6;
                    SelectedDetailTabIndex = 7;
                }
                if (_selectedDetailSubTabIndex == 7)
                {
                    Tbl93CommentsList = new ObservableCollection<Tbl93Comment>(
                        _businessLayer.ListTbl93CommentsByFiSpeciesId(CurrentTbl69FiSpecies.FiSpeciesID));

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
                     Tbl90ExpertsAllList = new ObservableCollection<Tbl90RefExpert>(
                        _businessLayer.ListTbl90RefExperts());
                    Tbl90ReferenceExpertsList = new ObservableCollection<Tbl90Reference>(
                        _businessLayer.ListTbl90ReferenceListRefExpertsByFiSpeciesId(CurrentTbl69FiSpecies.FiSpeciesID));

                    ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
                    ReferenceExpertsView.Refresh();

                   SelectedMainSubRefTabIndex = 0;
                }
                if (_selectedDetailSubRefTabIndex == 1)
                {
                    Tbl90SourcesAllList = new ObservableCollection<Tbl90RefSource>(
                        _businessLayer.ListTbl90RefSources());

                    Tbl90ReferenceSourcesList = new ObservableCollection<Tbl90Reference>(
                        _businessLayer.ListTbl90ReferenceListRefSourcesByGenusId(CurrentTbl69FiSpecies.FiSpeciesID));

                    ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
                    ReferenceSourcesView.Refresh();

                    SelectedMainSubRefTabIndex = 1;
                }
                if (_selectedDetailSubRefTabIndex == 2)
                {
                    Tbl90AuthorsAllList = new ObservableCollection<Tbl90RefAuthor>(
                        _businessLayer.ListTbl90RefAuthors());

                    Tbl90ReferenceAuthorsList = new ObservableCollection<Tbl90Reference>(
                        _businessLayer.ListTbl90ReferenceListRefAuthorsByGenusId(CurrentTbl69FiSpecies.FiSpeciesID));

                    ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
                    ReferenceAuthorsView.Refresh();

                    SelectedMainSubRefTabIndex = 2;
                }
            }
        }

        public int SelectedDetailSubImageTabIndex
        {
            get => _selectedDetailSubImageTabIndex;
            set { _selectedDetailSubImageTabIndex = value; RaisePropertyChanged(); }
        }

        #endregion "Public Commands to open Detail TabItems"
 

 //    Part 11    

     
        #region "Public Properties Tbl69FiSpecies"

        private string _searchFiSpeciesName = string.Empty;
        public string SearchFiSpeciesName
        {
            get => _searchFiSpeciesName; 
            set { _searchFiSpeciesName = value; RaisePropertyChanged();  }
        }

        public  ICollectionView FiSpeciessesView;
        private   Tbl69FiSpecies CurrentTbl69FiSpecies => FiSpeciessesView?.CurrentItem as Tbl69FiSpecies;

        private ObservableCollection<Tbl69FiSpecies> _tbl69FiSpeciessesList;
        public  ObservableCollection<Tbl69FiSpecies> Tbl69FiSpeciessesList
        {
            get => _tbl69FiSpeciessesList; 
            set {  _tbl69FiSpeciessesList = value; RaisePropertyChanged();   }
        }

        private ObservableCollection<Tbl69FiSpecies> _tbl69FiSpeciessesAllList;
        public  ObservableCollection<Tbl69FiSpecies> Tbl69FiSpeciessesAllList
        {
            get => _tbl69FiSpeciessesAllList; 
            set {  _tbl69FiSpeciessesAllList = value; RaisePropertyChanged();   }
        }

        #endregion "Public Properties"   
       
        #region "Public Properties Tbl66Genus"

        private string _searchGenusName = string.Empty;
        public new string SearchGenusName
        {
            get  => _searchGenusName; 
            set { _searchGenusName = value; RaisePropertyChanged(); }
        }

        public new ICollectionView GenussesView;
        private Tbl66Genus CurrentTbl66Genus => GenussesView?.CurrentItem as Tbl66Genus;           

        private ObservableCollection<Tbl66Genus> _tbl66GenussesList;
        public new ObservableCollection<Tbl66Genus> Tbl66GenussesList
        {
            get => _tbl66GenussesList; 
            set { _tbl66GenussesList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl66Genus> _tbl66GenussesAllList;
        public  ObservableCollection<Tbl66Genus> Tbl66GenussesAllList
        {
            get => _tbl66GenussesAllList; 
            set { _tbl66GenussesAllList = value; RaisePropertyChanged(); }       
        }

        #endregion "Public Properties"   
  
       
        #region "Public Properties Tbl68Speciesgroup"

        private string _searchSpeciesgroupName = string.Empty;
        public new  string SearchSpeciesgroupName
        {
            get => _searchSpeciesgroupName; 
            set { _searchSpeciesgroupName = value; RaisePropertyChanged(); }
        }

        public new ICollectionView SpeciesgroupsView;
        private  Tbl68Speciesgroup CurrentTbl68Speciesgroup => SpeciesgroupsView?.CurrentItem as Tbl68Speciesgroup;           

        private ObservableCollection<Tbl68Speciesgroup> _tbl68SpeciesgroupsList;
        public  new ObservableCollection<Tbl68Speciesgroup> Tbl68SpeciesgroupsList
        {
            get => _tbl68SpeciesgroupsList; 
            set { _tbl68SpeciesgroupsList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl68Speciesgroup> _tbl68SpeciesgroupsAllList;
        public new ObservableCollection<Tbl68Speciesgroup> Tbl68SpeciesgroupsAllList
        {
            get => _tbl68SpeciesgroupsAllList; 
            set { _tbl68SpeciesgroupsAllList = value; RaisePropertyChanged(); }       
        }

        #endregion "Public Properties"   
        
        #region "Public Properties Tbl78Name"

        private string _searchNameName = string.Empty;
        public string SearchNameName
        {
            get => _searchNameName; 
            set { _searchNameName = value; RaisePropertyChanged(); }
        }

        public ICollectionView NamesView;
        private Tbl78Name CurrentTbl78Name => NamesView?.CurrentItem as Tbl78Name;           

        private ObservableCollection<Tbl78Name> _tbl78NamesList;
        public ObservableCollection<Tbl78Name> Tbl78NamesList
        {
            get => _tbl78NamesList; 
            set { _tbl78NamesList = value; RaisePropertyChanged(); }
        }
        #endregion "Public Properties"     
        
        #region "Public Properties Tbl81Image"

        private string _searchImageName = string.Empty;
        public string SearchImageName
        {
            get => _searchImageName; 
            set { _searchImageName = value; RaisePropertyChanged(); }
        }

        public ICollectionView ImagesView;
        private Tbl81Image CurrentTbl81Image => ImagesView?.CurrentItem as Tbl81Image;           

        private ObservableCollection<Tbl81Image> _tbl81ImagesList;
        public ObservableCollection<Tbl81Image> Tbl81ImagesList
        {
            get => _tbl81ImagesList; 
            set { _tbl81ImagesList = value; RaisePropertyChanged(); }
        }
        #endregion "Public Properties"     
        
        #region "Public Properties Tbl84Synonym"

        private string _searchSynonymName = string.Empty;
        public string SearchSynonymName
        {
            get => _searchSynonymName; 
            set { _searchSynonymName = value; RaisePropertyChanged(); }
        }

        public ICollectionView SynonymsView;
        private Tbl84Synonym CurrentTbl84Synonym => SynonymsView?.CurrentItem as Tbl84Synonym;           

        private ObservableCollection<Tbl84Synonym> _tbl84SynonymsList;
        public ObservableCollection<Tbl84Synonym> Tbl84SynonymsList
        {
            get => _tbl84SynonymsList; 
            set { _tbl84SynonymsList = value; RaisePropertyChanged(); }
        }
        private ObservableCollection<Tbl84Synonym> _tbl84SynonymsAllList;
        public  ObservableCollection<Tbl84Synonym> Tbl84SynonymsAllList
        {
            get => _tbl84SynonymsAllList; 
            set { _tbl84SynonymsAllList = value; RaisePropertyChanged(); }       
        }

        #endregion "Public Properties"     
        
        #region "Public Properties Tbl87Geographic"

        private string _searchGeographicName = string.Empty;
        public string SearchGeographicName
        {
            get => _searchGeographicName; 
            set { _searchGeographicName = value; RaisePropertyChanged(); }
        }

        public ICollectionView GeographicsView;
        private Tbl87Geographic CurrentTbl87Geographic => GeographicsView?.CurrentItem as Tbl87Geographic;           

        private ObservableCollection<Tbl87Geographic> _tbl87GeographicsList;
        public ObservableCollection<Tbl87Geographic> Tbl87GeographicsList
        {
            get => _tbl87GeographicsList; 
            set { _tbl87GeographicsList = value; RaisePropertyChanged(); }
        }
        private ObservableCollection<Tbl87Geographic> _tbl87GeographicsAllList;
        public  ObservableCollection<Tbl87Geographic> Tbl87GeographicsAllList
        {
            get => _tbl87GeographicsAllList; 
            set { _tbl87GeographicsAllList = value; RaisePropertyChanged(); }       
        }

        #endregion "Public Properties"     
         
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
            set {  _continents = value;  RaisePropertyChanged(); }
        }

        private Continent _selectedContinent;
        public Continent SelectedContinent
        {
            get => _selectedContinent;
            set  {  _selectedContinent = value;  RaisePropertyChanged();  }
        }

        public class Continent
        {
            public string Name { get; set; }
        }

        private ObservableCollection<TblCountry> _tblCountriesList;
        public ObservableCollection<TblCountry> TblCountriesList
        {
            get => _tblCountriesList;
            set  {  _tblCountriesList = value;  RaisePropertyChanged();  }
        }

        #endregion "Private Methods"       
         
        #region "Public Properties Tbl90Author"

        private ObservableCollection<Tbl90RefAuthor> _tbl90AuthorsAllList;
        public new ObservableCollection<Tbl90RefAuthor> Tbl90AuthorsAllList
        {
            get => _tbl90AuthorsAllList; 
            set { _tbl90AuthorsAllList = value; RaisePropertyChanged(); }
        }

        #endregion "Public Properties "

        #region "Public Properties Tbl90Source"

        private ObservableCollection<Tbl90RefSource> _tbl90SourcesAllList;
        public new ObservableCollection<Tbl90RefSource> Tbl90SourcesAllList
        {
            get => _tbl90SourcesAllList; 
            set { _tbl90SourcesAllList = value; RaisePropertyChanged(); }
        }

        #endregion "Public Properties "

        #region "Public Properties Tbl90Expert"

        private ObservableCollection<Tbl90RefExpert> _tbl90ExpertsAllList;
        public new ObservableCollection<Tbl90RefExpert> Tbl90ExpertsAllList
        {
            get => _tbl90ExpertsAllList; 
            set { _tbl90ExpertsAllList = value; RaisePropertyChanged(); }
        }

        #endregion "Public Properties "

        #region "Public Properties Tbl90ReferenceAuthor"

        private string _searchReferenceAuthorName  = string.Empty;
        public new string SearchReferenceAuthorName
        {
            get => _searchReferenceAuthorName; 
            set { _searchReferenceAuthorName = value; RaisePropertyChanged(); }
        }
        public new ICollectionView ReferenceAuthorsView;
        private Tbl90Reference CurrentTbl90ReferenceAuthor => ReferenceAuthorsView?.CurrentItem as Tbl90Reference;

        private ObservableCollection<Tbl90Reference> _tbl90ReferenceAuthorsList;
        public new ObservableCollection<Tbl90Reference> Tbl90ReferenceAuthorsList
        {
            get => _tbl90ReferenceAuthorsList; 
            set { _tbl90ReferenceAuthorsList = value; RaisePropertyChanged(); }
        }

        #endregion "Public Properties"

        #region "Public Properties Tbl90ReferenceSource"

        private string _searchReferenceSourceName  = string.Empty;
        public new string SearchReferenceSourceName
        {
            get => _searchReferenceSourceName; 
            set { _searchReferenceSourceName = value; RaisePropertyChanged(); }
        }
        public new ICollectionView ReferenceSourcesView;
        private Tbl90Reference CurrentTbl90ReferenceSource => ReferenceSourcesView?.CurrentItem as Tbl90Reference;

        private ObservableCollection<Tbl90Reference> _tbl90ReferenceSourcesList;
        public new ObservableCollection<Tbl90Reference> Tbl90ReferenceSourcesList
        {
            get => _tbl90ReferenceSourcesList; 
            set { _tbl90ReferenceSourcesList = value; RaisePropertyChanged(); }
        }

        #endregion "Public Properties"

        #region "Public Properties Tbl90ReferenceExpert"

        private string _searchReferenceExpertName  = string.Empty;
        public new string SearchReferenceExpertName
        {
            get => _searchReferenceExpertName; 
            set { _searchReferenceExpertName = value; RaisePropertyChanged(); }
        }
        public new ICollectionView ReferenceExpertsView;
        private Tbl90Reference CurrentTbl90ReferenceExpert => ReferenceExpertsView?.CurrentItem as Tbl90Reference;

        private ObservableCollection<Tbl90Reference> _tbl90ReferenceExpertsList;
        public new ObservableCollection<Tbl90Reference> Tbl90ReferenceExpertsList
        {
            get => _tbl90ReferenceExpertsList; 
            set { _tbl90ReferenceExpertsList = value; RaisePropertyChanged(); }
        }

        #endregion "Public Properties"   
         
        #region "Public Properties Tbl93Comment"

        private string _searchCommentInfo = string.Empty;
        public new string SearchCommentInfo
        {
            get => _searchCommentInfo; 
            set { _searchCommentInfo = value; RaisePropertyChanged(); }
        }
        public new ICollectionView CommentsView;
        private Tbl93Comment CurrentTbl93Comment => CommentsView?.CurrentItem as Tbl93Comment;

        private ObservableCollection<Tbl93Comment> _tbl93CommentsList;
        public new ObservableCollection<Tbl93Comment> Tbl93CommentsList
        {
            get => _tbl93CommentsList; 
            set { _tbl93CommentsList = value; RaisePropertyChanged(); }
        }

        #endregion "Public Properties"     
      
        #region "Public Property  TblCountry"

        private ObservableCollection<TblCountry> _tblCountriesAllList;
        public ObservableCollection<TblCountry> TblCountriesAllList
        {
            get { return _tblCountriesAllList; }
            set { _tblCountriesAllList = value; RaisePropertyChanged(); }
        }

        #endregion "Public Properties"  

        private void GetValueLanguage()
             {
                 _languages = new List<Language>()
                 {
                     new Language {Name = "GER"},
                     new Language {Name = "ENG"},
                     new Language {Name = "FRE"},
                     new Language {Name = "POR"}
                 };

                 _selectedLanguage = new Language();
             }

             private List<Language> _languages;

             public List<Language> Languages
             {
                 get => _languages;
                 set { _languages = value; RaisePropertyChanged(); }
             }

             private Language _selectedLanguage;

             public Language SelectedLanguage
             {
                 get => _selectedLanguage;
                 set { _selectedLanguage = value; RaisePropertyChanged(); }
             }

             public class Language
             {
                 public string Name
                 {
                     get;
                     set;
                 }
             }

 
             #region Mimetype

             private void GetValueMimeType()
             {
                 _mimeTypes = new List<MimeType>()
                 {
                     new MimeType {Name = "jpg"},
                     new MimeType {Name = "png"},
                     new MimeType {Name = "bmp"},
                     new MimeType {Name = "tiff"},
                     new MimeType {Name = "gif"},
                     new MimeType {Name = "icon"},
                     new MimeType {Name = "jpeg"},
                     new MimeType {Name = "wmf"},
                     new MimeType {Name = "wmv"},
                     new MimeType {Name = "mpg"},
                     new MimeType {Name = "mp4"},
                     new MimeType {Name = "avi"},
                     new MimeType {Name = "mov"},
                     new MimeType {Name = "swf"},
                     new MimeType {Name = "flv"}
                 };

                 _selectedMimeType = new MimeType();
             }

             private List<MimeType> _mimeTypes;

             public List<MimeType> MimeTypes
             {
                 get => _mimeTypes;
                 set {  _mimeTypes = value;  RaisePropertyChanged();  }
             }

             private MimeType _selectedMimeType;
             public MimeType SelectedMimeType
             {
                 get => _selectedMimeType;
                 set  {  _selectedMimeType = value;  RaisePropertyChanged(); }
             }

             public class MimeType
             {
                 public string Name { get; set; }
             }
        #endregion

        #region OpenfileDialog

        public static RelayCommand OpenCommand { get; set; }
        private string _selectedPath;

        public string SelectedPath
        {
            get => _selectedPath;
            set { _selectedPath = value; RaisePropertyChanged(); }
        }

        private BitmapImage _imageSource;

        public BitmapImage ImageSource
        {
            get => _imageSource;
            set { _imageSource = value; RaisePropertyChanged(); }
        }

        public readonly string DefaultPath;

        private void RegisterCommands()
        {
            OpenCommand = new RelayCommand(ExecuteOpenFileDialog);
        }

        private void ExecuteOpenFileDialog(object o)
        {
            var dialog = new OpenFileDialog
            {
                Title = "Select A File",
                InitialDirectory = DefaultPath,
                Filter = "All images|*.jpg;*.jpeg;*.jpe;*.bmp;*.gif;*.ico;*.png;*.tif;*.tiff;*.hpd;*.jxr;*.wdp|" +
                         "JPEG image|*.jpg;*.jpeg;*.jpe|Windows BMP image|*.bmp|GIF image|*.gif|Microsoft Windows icon|*.ico|" +
                         "PNG image|*.png|TIFF image|*.tif;*.tiff|JPEG XR|*.hpd;*.jxr;*.wdp",

                FilterIndex = 1
            };
            dialog.ShowDialog();

            SelectedPath = dialog.FileName;
            ImageSource = new BitmapImage(new Uri(dialog.FileName));
        }

        #endregion    
 

 



   }
}   
