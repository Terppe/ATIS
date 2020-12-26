using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using log4net;
using Te.Atis.DomainModel;
using Te.Atis.Ui.Desktop.BusinessLayer;
using Te.Atis.Ui.Desktop.Domain;
using Te.Atis.Ui.Desktop.Domain.Helper;
using Te.Atis.Ui.Desktop.MessageBox;    

    
using System.Collections.Generic;
using System.IO;
using System.Windows.Media.Imaging;
using Microsoft.Win32;   
    
         //    Tbl69FiSpeciessesViewModel Skriptdatum:  22.01.2019  10:32    

namespace Te.Atis.Ui.Desktop.Views.Database
{     
    
    public class Tbl69FiSpeciessesViewModel : ViewModelBase                     
    {     
         
        #region "Private Data Members"
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static IBusinessLayer _businessLayer;
        private static DbEntityException _entityException;
        private int _position;   
         
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
                _entityException = new DbEntityException();
            }
        }
        #endregion "Constructor"          
 

 //    Part 1    

             
        #region "Public Commands Basic Tbl69FiSpecies"
        //-------------------------------------------------------------------------
        private RelayCommand _clearFiSpeciesCommand;

        public ICommand ClearFiSpeciesCommand => _clearFiSpeciesCommand ??
                                                  (_clearFiSpeciesCommand = new RelayCommand(delegate { ClearFiSpecies(null); }));         
             
        private RelayCommand _getFiSpeciessesByNameOrIdCommand;  

        public  ICommand GetFiSpeciessesByNameOrIdCommand => _getFiSpeciessesByNameOrIdCommand ??
                                                           (_getFiSpeciessesByNameOrIdCommand = new RelayCommand(delegate { GetFiSpeciessesByNameOrId(null); }));        
             
        private RelayCommand _addFiSpeciesCommand;

        public ICommand AddFiSpeciesCommand => _addFiSpeciesCommand ??
                                                (_addFiSpeciesCommand = new RelayCommand(delegate { AddFiSpecies(null); }));

        private RelayCommand _copyFiSpeciesCommand;

        public ICommand CopyFiSpeciesCommand => _copyFiSpeciesCommand ??
                                                 (_copyFiSpeciesCommand = new RelayCommand(delegate { CopyFiSpecies(null); }));      
             
        private RelayCommand _deleteFiSpeciesCommand;

        public ICommand DeleteFiSpeciesCommand => _deleteFiSpeciesCommand ??
                                                   (_deleteFiSpeciesCommand = new RelayCommand(delegate { DeleteFiSpecies(null); }));    
             
        private RelayCommand _saveFiSpeciesCommand;

        public ICommand SaveFiSpeciesCommand => _saveFiSpeciesCommand ??
                                                 (_saveFiSpeciesCommand = new RelayCommand(delegate { SaveFiSpecies(null); }));
        //-------------------------------------------------------------------------          
     
        private void ClearFiSpecies(object o)
        {
            SearchFiSpeciesName = "";

            SelectedMainTabIndex = 0;  //change tab
            SelectedDetailTabIndex = 0;
            SelectedDetailSubTabIndex = 0;
            SelectedDetailSubRefTabIndex = 0;

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
            if (SearchFiSpeciesName != "")
            {
                Tbl69FiSpeciessesList?.Clear();
                if (SearchFiSpeciesName == "*") // show whole table
                {
                    SearchFiSpeciesName = "";
                    _businessLayer = new BusinessLayer.BusinessLayer();
                    Tbl66GenussesAllList = new ObservableCollection<Tbl66Genus>(_businessLayer.ListTbl66Genusses());
                    Tbl69FiSpeciessesList = new ObservableCollection<Tbl69FiSpecies>(_businessLayer.ListTbl69FiSpeciessesByFiSpeciesName(SearchFiSpeciesName));
                    SearchFiSpeciesName = "*";
                }
                else
                {
                    _businessLayer = new BusinessLayer.BusinessLayer();
                    Tbl66GenussesAllList = new ObservableCollection<Tbl66Genus>(_businessLayer.ListTbl66Genusses());
                    Tbl69FiSpeciessesList = int.TryParse(SearchFiSpeciesName, out var id) ?
                        new ObservableCollection<Tbl69FiSpecies>(_businessLayer.ListTbl69FiSpeciessesByFiSpeciesId(id)) :
                        new ObservableCollection<Tbl69FiSpecies>(_businessLayer.ListTbl69FiSpeciessesByFiSpeciesName(SearchFiSpeciesName));
                }

                if (Tbl69FiSpeciessesList.Count == 0)
                {
                    WpfMessageBox.Show(CultRes.StringsRes.Tables, CultRes.StringsRes.DatasetNot,
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                }
                else
                {
                    Tbl66GenussesList?.Clear();
                    Tbl78NamesList?.Clear();
                    Tbl90ReferenceExpertsList?.Clear();
                    Tbl90ReferenceSourcesList?.Clear();
                    Tbl90ReferenceAuthorsList?.Clear();
                    Tbl93CommentsList?.Clear();
                }
            }
            else
            {
                WpfMessageBox.Show(CultRes.StringsRes.SearchNameOrId, CultRes.StringsRes.InputRequested,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
            }
            FiSpeciessesView = CollectionViewSource.GetDefaultView(Tbl69FiSpeciessesList);
            FiSpeciessesView.Refresh();
        }
        //------------------------------------------------------------------------------------                          
     
        private void AddFiSpecies(object o)
        {
            if (Tbl69FiSpeciessesList == null)
                Tbl69FiSpeciessesList =  new ObservableCollection<Tbl69FiSpecies>( );

            Tbl69FiSpeciessesList.Insert(0, new Tbl69FiSpecies   {   FiSpeciesName = CultRes.StringsRes.DatasetNew  }  );

                    _businessLayer = new BusinessLayer.BusinessLayer();
                Tbl66GenussesAllList = new ObservableCollection<Tbl66Genus>(_businessLayer.ListTbl66Genusses());

            FiSpeciessesView = CollectionViewSource.GetDefaultView(Tbl69FiSpeciessesList);
            FiSpeciessesView.MoveCurrentToFirst();
        }
        //------------------------------------------------------------------------------------                               
        
        private void CopyFiSpecies(object o)
        {
            if (CurrentTbl69FiSpecies == null)
            {
                WpfMessageBox.Show(CultRes.StringsRes.DatasetNew,
                    CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                return;
            }
            _businessLayer = new BusinessLayer.BusinessLayer();

            var fispecies = _businessLayer.SingleListTbl69FiSpeciessesByFiSpeciesId(CurrentTbl69FiSpecies.FiSpeciesID);

            Tbl69FiSpeciessesList.Insert(0, new Tbl69FiSpecies
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
            if (CurrentTbl69FiSpecies == null)
            {
                WpfMessageBox.Show(CultRes.StringsRes.DatasetNew,
                    CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                return;
            }
            _businessLayer = new BusinessLayer.BusinessLayer();

            var ret = false;
            //check if in Tbl78Names or Tbl81Images or Tbl84Synonyms or Tbl87Geographics connected datasets, than return
            Tbl78NamesList = new ObservableCollection<Tbl78Name>(_businessLayer.ListTbl78NamesByFiSpeciesId(CurrentTbl69FiSpecies.FiSpeciesID));
            if (Tbl78NamesList.Count != 0)
            {
                WpfMessageBox.Show(CultRes.StringsRes.ConnectedTable, CultRes.StringsRes.Name + " " + CultRes.StringsRes.ConnectedDataset,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                 ret = true;              
            }           
            Tbl81ImagesList = new ObservableCollection<Tbl81Image>(_businessLayer.ListTbl81ImagesByFiSpeciesId(CurrentTbl69FiSpecies.FiSpeciesID));
            if (Tbl81ImagesList.Count != 0)
            {
                WpfMessageBox.Show(CultRes.StringsRes.ConnectedTable, CultRes.StringsRes.Image + " " + CultRes.StringsRes.ConnectedDataset,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                 ret = true;              
            }           
            Tbl84SynonymsList = new ObservableCollection<Tbl84Synonym>(_businessLayer.ListTbl84SynonymsByFiSpeciesId(CurrentTbl69FiSpecies.FiSpeciesID));
            if (Tbl84SynonymsList.Count != 0)
            {
                WpfMessageBox.Show(CultRes.StringsRes.ConnectedTable, CultRes.StringsRes.Synonym + " " + CultRes.StringsRes.ConnectedDataset,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                 ret = true;              
            }           
            Tbl87GeographicsList = new ObservableCollection<Tbl87Geographic>(_businessLayer.ListTbl87GeographicsByFiSpeciesId(CurrentTbl69FiSpecies.FiSpeciesID));
            if (Tbl87GeographicsList.Count != 0)
            {
                WpfMessageBox.Show(CultRes.StringsRes.ConnectedTable, CultRes.StringsRes.Geographic+ " " + CultRes.StringsRes.ConnectedDataset,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                 ret = true;              
            }           
            Tbl90ReferenceAuthorsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ReferenceListRefAuthorsByFiSpeciesId(CurrentTbl69FiSpecies.FiSpeciesID));
            if (Tbl90ReferenceAuthorsList.Count != 0)
            {
                WpfMessageBox.Show(CultRes.StringsRes.ConnectedTable, CultRes.StringsRes.ReferenceAuthor + " " + CultRes.StringsRes.ConnectedDataset,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                ret = true;
            }
            Tbl90ReferenceSourcesList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ReferenceListRefSourcesByFiSpeciesId(CurrentTbl69FiSpecies.FiSpeciesID));
            if (Tbl90ReferenceSourcesList.Count != 0)
            {
                WpfMessageBox.Show(CultRes.StringsRes.ConnectedTable, CultRes.StringsRes.ReferenceSource + " " + CultRes.StringsRes.ConnectedDataset,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                ret = true;
            }
            Tbl90ReferenceExpertsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ReferenceListRefExpertsByFiSpeciesId(CurrentTbl69FiSpecies.FiSpeciesID));
            if (Tbl90ReferenceExpertsList.Count != 0)
            {
                WpfMessageBox.Show(CultRes.StringsRes.ConnectedTable, CultRes.StringsRes.ReferenceExpert + " " + CultRes.StringsRes.ConnectedDataset,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                ret = true;
            }
            Tbl93CommentsList = new ObservableCollection<Tbl93Comment>(_businessLayer.ListTbl93CommentsByFiSpeciesId(CurrentTbl69FiSpecies.FiSpeciesID));
            if (Tbl93CommentsList.Count != 0)
            {
                WpfMessageBox.Show(CultRes.StringsRes.ConnectedTable, CultRes.StringsRes.Comment + " " + CultRes.StringsRes.ConnectedDataset,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                ret = true;
            }
            if (ret)  return;
            {
            try
            {
                var fispecies = _businessLayer.SingleListTbl69FiSpeciessesByFiSpeciesId(CurrentTbl69FiSpecies.FiSpeciesID);
                if (fispecies != null)
                {
                    if (WpfMessageBox.Show(CultRes.StringsRes.DeleteQuestion1, CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl69FiSpecies.Tbl66Genusses.GenusName + " " + CurrentTbl69FiSpecies.FiSpeciesName + " " + CurrentTbl69FiSpecies.Subspecies + " " + CurrentTbl69FiSpecies.Divers,
                            MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes)
                        return;
                    fispecies.EntityState = EntityState.Deleted;
                    _businessLayer.RemoveFiSpecies(fispecies);

                    WpfMessageBox.Show(CultRes.StringsRes.DeleteSuccess, CurrentTbl69FiSpecies.Tbl66Genusses.GenusName + " " + CurrentTbl69FiSpecies.FiSpeciesName + " " + CurrentTbl69FiSpecies.Subspecies + " " + CurrentTbl69FiSpecies.Divers,
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                }
                else
                {
                    WpfMessageBox.Show(CultRes.StringsRes.Information, CultRes.StringsRes.DeleteCan + " " + CurrentTbl69FiSpecies.Tbl66Genusses.GenusName + " " + CurrentTbl69FiSpecies.FiSpeciesName + " " + CurrentTbl69FiSpecies.Subspecies + " " + CurrentTbl69FiSpecies.Divers+ " " + CultRes.StringsRes.DeleteCan1,
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                }
            }
            catch (DbEntityValidationException ex)
            {
                _entityException.EntityException(ex);
                    Log.Error(ex);
            }
        }
            if (SearchFiSpeciesName != "")
            {
                if (SearchFiSpeciesName == "*")  //show all datasets
                {
                    SearchFiSpeciesName = "";
                    Tbl69FiSpeciessesList.Clear();
                    
                Tbl69FiSpeciessesList = new ObservableCollection<Tbl69FiSpecies>(_businessLayer.ListTbl69FiSpeciessesByFiSpeciesName(SearchFiSpeciesName));            
                    SearchFiSpeciesName = "*";
                }
                else
                {               
                    Tbl69FiSpeciessesList =  new ObservableCollection<Tbl69FiSpecies>(_businessLayer.ListTbl69FiSpeciessesByFiSpeciesName(SearchFiSpeciesName));

                }
                FiSpeciessesView = CollectionViewSource.GetDefaultView(Tbl69FiSpeciessesList);
                FiSpeciessesView.Refresh();
            }
            else  //SearchName = empty
            {
                Tbl69FiSpeciessesList = new ObservableCollection<Tbl69FiSpecies>(_businessLayer.ListTbl69FiSpeciessesByFiSpeciesName(SearchFiSpeciesName));

                FiSpeciessesView = CollectionViewSource.GetDefaultView(Tbl69FiSpeciessesList);
                FiSpeciessesView.MoveCurrentToFirst();
             }
        }
        //-------------------------------------------------------------------------------------------------                    
        
        private void SaveFiSpecies(object o)
        {
            if (CurrentTbl69FiSpecies == null)
            {
                WpfMessageBox.Show(CultRes.StringsRes.DatasetNew,
                    CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                return;
            }
            _businessLayer = new BusinessLayer.BusinessLayer();

            try
            {
                var fispecies = _businessLayer.SingleListTbl69FiSpeciessesByFiSpeciesId(CurrentTbl69FiSpecies.FiSpeciesID);
                if (CurrentTbl69FiSpecies.FiSpeciesID != 0)
                {
                    if (fispecies != null) //update
                    {
                            fispecies.FiSpeciesName = CurrentTbl69FiSpecies.FiSpeciesName;
                            fispecies.Subspecies = CurrentTbl69FiSpecies.Subspecies;
                            fispecies.Divers = CurrentTbl69FiSpecies.Divers;
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
                            Subspecies = CurrentTbl69FiSpecies.Subspecies,
                            Divers = CurrentTbl69FiSpecies.Divers,
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

                    //check if dataset with Name and GenusId and SpeciesgroupId already exist       
                    var dataset = _businessLayer.ListTbl69FiSpeciessesByFiSpeciesNameAndSubspeciesAndDiversAndGenusIdAndSpeciesgroupId(CurrentTbl69FiSpecies.FiSpeciesName, CurrentTbl69FiSpecies.Subspecies, CurrentTbl69FiSpecies.Divers, CurrentTbl69FiSpecies.GenusID, CurrentTbl69FiSpecies.SpeciesgroupID);

                    if (dataset.Count != 0 && CurrentTbl69FiSpecies.FiSpeciesID == 0)  //dataset exist
                    {
                        WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTbl69FiSpecies.FiSpeciesName + " " + CurrentTbl69FiSpecies.Subspecies + " " + CurrentTbl69FiSpecies.Divers,
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        return;
                    }
                    if (dataset.Count == 0 && CurrentTbl69FiSpecies.FiSpeciesID == 0 ||
                        dataset.Count != 0 && CurrentTbl69FiSpecies.FiSpeciesID != 0 ||
                        dataset.Count == 0 && CurrentTbl69FiSpecies.FiSpeciesID != 0) //new dataset and update
                    {
                        if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTbl69FiSpecies.FiSpeciesName + " " + CurrentTbl69FiSpecies.Subspecies + " " + CurrentTbl69FiSpecies.Divers,
                                MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes)
                            return;
                        {
                            try
                            {
                                _businessLayer.UpdateFiSpecies(fispecies);
                                _position = FiSpeciessesView.CurrentPosition;
                            }
                            catch (DbUpdateException e)
                            {
                                if (e.InnerException != null)
                                    System.Windows.MessageBox.Show(e.InnerException.ToString(), CultRes.StringsRes.FailedToSave,
                                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);

                                Log.Error(e);
                                return;
                            }
                            catch (Exception e)
                            {
                                System.Windows.MessageBox.Show(e.Message, CultRes.StringsRes.Error,
                                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);
                                Log.Error(e);
                                return;
                            }
                                    WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess,
                                        CurrentTbl69FiSpecies.FiSpeciesID == 0
                                            ? CultRes.StringsRes.DatasetNew
                                            :  CurrentTbl69FiSpecies.FiSpeciesName + " " + CurrentTbl69FiSpecies.Subspecies + " " + CurrentTbl69FiSpecies.Divers,
                                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        }
                    }
                }
            }
            catch (DbEntityValidationException ex)
            {
                _entityException.EntityException(ex);
                    Log.Error(ex);
                 return;
            }

            if (SearchFiSpeciesName != "")
            {
                if (SearchFiSpeciesName == "*")  //show all datasets
                {
                    SearchFiSpeciesName = "";
                    Tbl69FiSpeciessesList.Clear();
                    
                Tbl69FiSpeciessesList = new ObservableCollection<Tbl69FiSpecies>(_businessLayer.ListTbl69FiSpeciessesByFiSpeciesName(SearchFiSpeciesName));            
                    SearchFiSpeciesName = "*";
                }
                else
                {               
                    Tbl69FiSpeciessesList = int.TryParse(SearchFiSpeciesName, out var id)
                        ? new ObservableCollection<Tbl69FiSpecies>(_businessLayer.ListTbl69FiSpeciessesByFiSpeciesId(id))
                        : new ObservableCollection<Tbl69FiSpecies>(_businessLayer.ListTbl69FiSpeciessesByFiSpeciesName(SearchFiSpeciesName));

                }
                FiSpeciessesView = CollectionViewSource.GetDefaultView(Tbl69FiSpeciessesList);
                FiSpeciessesView.MoveCurrentToPosition(_position);
            }
            else  
            {
                Tbl69FiSpeciessesList = new ObservableCollection<Tbl69FiSpecies>(_businessLayer.ListTbl69FiSpeciessesByFiSpeciesName(CurrentTbl69FiSpecies.FiSpeciesName));

                FiSpeciessesView= CollectionViewSource.GetDefaultView(Tbl69FiSpeciessesList);
                FiSpeciessesView.Refresh();
            }
        }
        #endregion "Public Commands"                  
 
 

 //    Part 2    

           
        #region "Public Commands Connect <== Tbl66Genus"                 
        //-------------------------------------------------------------------------

        private RelayCommand _saveGenusCommand;

        public ICommand SaveGenusCommand => _saveGenusCommand ??
                                                 (_saveGenusCommand = new RelayCommand(delegate { SaveGenus(null); }));

        //-------------------------------------------------------------------------          
          
        private void SaveGenus(object o)
        {
            if (CurrentTbl66Genus == null)
            {
                WpfMessageBox.Show(CultRes.StringsRes.DatasetNew,
                    CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                return;
            }

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
                        return;
                    }
                    if (dataset.Count == 0 && CurrentTbl66Genus.GenusID == 0 ||
                        dataset.Count != 0 && CurrentTbl66Genus.GenusID != 0 ||
                        dataset.Count == 0 && CurrentTbl66Genus.GenusID != 0) //new dataset and update
                    {
                        if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTbl66Genus.GenusName,
                                MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes)
                            return;
                        {
                            try
                            {
                                _businessLayer.UpdateGenus(genus);
                            }
                            catch (DbUpdateException e)
                            {
                                if (e.InnerException != null)
                                    System.Windows.MessageBox.Show(e.InnerException.ToString(), CultRes.StringsRes.FailedToSave,
                                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);

                                Log.Error(e);
                                return;
                            }
                            catch (Exception e)
                            {
                                System.Windows.MessageBox.Show(e.Message, CultRes.StringsRes.Error,
                                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);
                                Log.Error(e);
                                return;
                            }
                                    WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess,
                                        CurrentTbl66Genus.GenusID == 0
                                            ? CultRes.StringsRes.DatasetNew
                                            : CurrentTbl66Genus.GenusName,
                                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        }
                    }
                }
            }
            catch (DbEntityValidationException ex)
            {
                _entityException.EntityException(ex);
                    Log.Error(ex);
                 return;
            }

                Tbl66GenussesList = new ObservableCollection<Tbl66Genus>(_businessLayer.ListTbl66GenussesByGenusId(CurrentTbl69FiSpecies.GenusID));            

            SelectedMainTabIndex = 0;
            SelectedDetailSubTabIndex = 0;
            GenussesView = CollectionViewSource.GetDefaultView(Tbl66GenussesList);
            GenussesView.Refresh();
        }
        #endregion "Public Commands"                  
                                                          

 //    Part 3    

           
        #region "Public Commands Connect <== Tbl68Speciesgroup"                 
        //-------------------------------------------------------------------------
        private RelayCommand _saveSpeciesgroupCommand;

        public ICommand SaveSpeciesgroupCommand => _saveSpeciesgroupCommand ??
                                                 (_saveSpeciesgroupCommand = new RelayCommand(delegate { SaveSpeciesgroup(null); }));

        //-------------------------------------------------------------------------          
        
        private void SaveSpeciesgroup(object o)
        {
            if (CurrentTbl68Speciesgroup == null)
            {
                WpfMessageBox.Show(CultRes.StringsRes.DatasetNew,
                    CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                return;
            }

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
                        return;
                    }
                    if (dataset.Count == 0 && CurrentTbl68Speciesgroup.SpeciesgroupID == 0 ||
                        dataset.Count != 0 && CurrentTbl68Speciesgroup.SpeciesgroupID != 0 ||
                        dataset.Count == 0 && CurrentTbl68Speciesgroup.SpeciesgroupID != 0) //new dataset and update
                    {
                        if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTbl68Speciesgroup.SpeciesgroupName + " " + CurrentTbl68Speciesgroup.Subspeciesgroup,
                                MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes)
                            return;
                        {
                            try
                            {
                                _businessLayer.UpdateSpeciesgroup(speciesgroup);
                            }
                            catch (DbUpdateException e)
                            {
                                if (e.InnerException != null)
                                    System.Windows.MessageBox.Show(e.InnerException.ToString(), CultRes.StringsRes.FailedToSave,
                                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);

                                Log.Error(e);
                                return;
                            }
                            catch (Exception e)
                            {
                                System.Windows.MessageBox.Show(e.Message, CultRes.StringsRes.Error,
                                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);
                                Log.Error(e);
                                return;
                            }
                                    WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess,
                                        CurrentTbl68Speciesgroup.SpeciesgroupID == 0
                                            ? CultRes.StringsRes.DatasetNew
                                            : CurrentTbl68Speciesgroup.SpeciesgroupName + " " + CurrentTbl68Speciesgroup.Subspeciesgroup,
                                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        }
                    }
                }
            }
            catch (DbEntityValidationException ex)
            {
                _entityException.EntityException(ex);
                    Log.Error(ex);
                  return;
            }

                Tbl68SpeciesgroupsList = new ObservableCollection<Tbl68Speciesgroup>(_businessLayer.ListTbl68SpeciesgroupsBySpeciesgroupId(CurrentTbl69FiSpecies.SpeciesgroupID));            

            SelectedMainTabIndex = 1;
            SelectedDetailSubTabIndex = 1;
            SpeciesgroupsView = CollectionViewSource.GetDefaultView(Tbl68SpeciesgroupsList);
            SpeciesgroupsView.Refresh();
        }
        #endregion "Public Commands"                  
                                                          



 //    Part 4    

           
        #region "Public Commands Connect ==> Tbl78Name"                 
        //-------------------------------------------------------------------------
        private RelayCommand _addNameCommand;

        public ICommand AddNameCommand => _addNameCommand ??
                                                (_addNameCommand = new RelayCommand(delegate { AddName(null); }));

        private RelayCommand _copyNameCommand;

        public ICommand CopyNameCommand => _copyNameCommand ??
                                                 (_copyNameCommand = new RelayCommand(delegate { CopyName(null); }));

        private RelayCommand _deleteNameCommand;

        public ICommand DeleteNameCommand => _deleteNameCommand ??
                                                 (_deleteNameCommand = new RelayCommand(delegate { DeleteName(null); }));

        private RelayCommand _saveNameCommand;

        public ICommand SaveNameCommand => _saveNameCommand ??
                                                 (_saveNameCommand = new RelayCommand(delegate { SaveName(null); }));

        //-------------------------------------------------------------------------          
            
        private void AddName(object o)      
        {
            if (Tbl78NamesList == null)
                Tbl78NamesList =  new ObservableCollection<Tbl78Name>( );

            Tbl78NamesList.Insert(0, new Tbl78Name  { NameName = CultRes.StringsRes.DatasetNew});

            NamesView = CollectionViewSource.GetDefaultView(Tbl78NamesList);
            NamesView.MoveCurrentToFirst();
        }
        //----------------------------------------------------------------------              
             
        private void CopyName(object o)
        {
            if (CurrentTbl78Name == null)
            {
                WpfMessageBox.Show(CultRes.StringsRes.DatasetNew,
                    CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                return;
            }

            var name = _businessLayer.SingleListTbl78NamesByNameId(CurrentTbl78Name.NameID);

            Tbl78NamesList.Insert(0, new Tbl78Name
            {
                NameName = CultRes.StringsRes.DatasetNew,
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
             
        private void DeleteName(object o)
        {
            if (CurrentTbl78Name == null)
            {
                WpfMessageBox.Show(CultRes.StringsRes.DatasetNew,
                    CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                return;
            }

            try
            {
                var name = _businessLayer.SingleListTbl78NamesByNameId(CurrentTbl78Name.NameID);
                if (name!= null)
                {
                    if (WpfMessageBox.Show(CultRes.StringsRes.DeleteQuestion1, CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl78Name.NameName,
                         MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes) 
                    return;
                    name.EntityState = EntityState.Deleted;
                    _businessLayer.RemoveName(name);

                    WpfMessageBox.Show(CultRes.StringsRes.DeleteSuccess, CurrentTbl78Name.NameName,
                       MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);  
                }
                else
                {
                    WpfMessageBox.Show(CultRes.StringsRes.Information, CultRes.StringsRes.DeleteCan + " " + CurrentTbl78Name.NameName + " " + CultRes.StringsRes.DeleteCan1,
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                }
            }
            catch (DbEntityValidationException ex)
            {
                _entityException.EntityException(ex);
                    Log.Error(ex);
            }
         
            Tbl78NamesList = new ObservableCollection<Tbl78Name>(_businessLayer.ListTbl78NamesByFiSpeciesId(CurrentTbl69FiSpecies.FiSpeciesID));

            NamesView = CollectionViewSource.GetDefaultView(Tbl78NamesList);
            NamesView.Refresh();
        }
        //-------------------------------------------------------------------------------------------------                    
      
    
        private void SaveName(object o)
        {
            if (CurrentTbl78Name == null)
            {
                WpfMessageBox.Show(CultRes.StringsRes.DatasetNew,
                    CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                return;
            }

            CurrentTbl78Name.FiSpeciesID = CurrentTbl69FiSpecies.FiSpeciesID;

            //Search for CurrentTbl78Name.PlSpeciesID with Plantae#Regnum# 
            var plantaeRegnum = _businessLayer.SingleListTbl72PlSpeciessesByPlSpeciesName("Plantae#Regnum#");
            CurrentTbl78Name.PlSpeciesID = plantaeRegnum.PlSpeciesID;

            try
            {
                var name = _businessLayer.SingleListTbl78NamesByNameId(CurrentTbl78Name.NameID);
                if (CurrentTbl78Name.NameID != 0)
                {
                    if (name != null) //update
                    {
                            name.NameName = CurrentTbl78Name.NameName;
                            name.FiSpeciesID = CurrentTbl78Name.FiSpeciesID;
                            name.PlSpeciesID = CurrentTbl78Name.PlSpeciesID;
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
                            PlSpeciesID = CurrentTbl78Name.PlSpeciesID,
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
                    //FiSpeciesID and PlSpeciesID may be not 0
                    if (CurrentTbl78Name.FiSpeciesID == 0 || CurrentTbl78Name.PlSpeciesID == 0)

                    {
                        WpfMessageBox.Show(CultRes.StringsRes.RequiredGenealogyConnect, CultRes.StringsRes.RequiredInput,
                            MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        return;
                    }

                    //check if dataset with Name and FiSpeciesId and PlSpeciesId already exist       
                    var dataset = _businessLayer.ListTbl78NamesByNameNameAndFiSpeciesIdAndPlSpeciesId(CurrentTbl78Name.NameName, CurrentTbl78Name.FiSpeciesID, CurrentTbl78Name.PlSpeciesID);

                    if (dataset.Count != 0 && CurrentTbl78Name.NameID == 0)  //dataset exist
                    {
                        WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTbl78Name.NameName,
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        return;
                    }
                    if (dataset.Count == 0 && CurrentTbl78Name.NameID == 0 ||
                        dataset.Count != 0 && CurrentTbl78Name.NameID != 0 ||
                        dataset.Count == 0 && CurrentTbl78Name.NameID != 0) //new dataset and update
                    {
                        if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTbl78Name.NameName,
                                MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes)
                            return;
                        {
                            try
                            {
                                _businessLayer.UpdateName(name);
                            }
                            catch (DbUpdateException e)
                            {
                                if (e.InnerException != null)
                                    System.Windows.MessageBox.Show(e.InnerException.ToString(), CultRes.StringsRes.FailedToSave,
                                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);

                                Log.Error(e);
                                return;
                            }
                            catch (Exception e)
                            {
                                System.Windows.MessageBox.Show(e.Message, CultRes.StringsRes.Error,
                                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);
                                Log.Error(e);
                                return;
                            }
                                    WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess,
                                        CurrentTbl78Name.NameID == 0
                                            ? CultRes.StringsRes.DatasetNew
                                            : CurrentTbl78Name.NameName,
                                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        }
                    }
                }
            }
            catch (DbEntityValidationException ex)
            {
                _entityException.EntityException(ex);
                    Log.Error(ex);
                  return;
            }

            Tbl78NamesList = new ObservableCollection<Tbl78Name>(_businessLayer.ListTbl78NamesByFiSpeciesId(CurrentTbl69FiSpecies.FiSpeciesID));            

            SelectedMainTabIndex = 2;
            SelectedDetailSubTabIndex = 2;
            NamesView = CollectionViewSource.GetDefaultView(Tbl78NamesList);
            NamesView.Refresh();
        }
        #endregion "Public Commands"                   
                                                          


 //    Part 5    

                       
        #region "Public Commands Connect ==> Tbl81Image"                 
        //-------------------------------------------------------------------------
        private RelayCommand _addImageCommand;

        public ICommand AddImageCommand => _addImageCommand ??
                                                (_addImageCommand = new RelayCommand(delegate { AddImage(null); }));

        private RelayCommand _copyImageCommand;

        public ICommand CopyImageCommand => _copyImageCommand ??
                                                 (_copyImageCommand = new RelayCommand(delegate { CopyImage(null); }));

        private RelayCommand _deleteImageCommand;

        public ICommand DeleteImageCommand => _deleteImageCommand ??
                                                 (_deleteImageCommand = new RelayCommand(delegate { DeleteImage(null); }));

        private RelayCommand _saveImageCommand;

        public ICommand SaveImageCommand => _saveImageCommand ??
                                                 (_saveImageCommand = new RelayCommand(delegate { SaveImage(null); }));

        //-------------------------------------------------------------------------          
              
        private void AddImage(object o)      
        {
            if (Tbl81ImagesList == null)
                Tbl81ImagesList =  new ObservableCollection<Tbl81Image>( );

            Tbl81ImagesList.Insert(0, new Tbl81Image   { Info = CultRes.StringsRes.DatasetNew});

            ImagesView = CollectionViewSource.GetDefaultView(Tbl81ImagesList);
            ImagesView.MoveCurrentToFirst();
        }
        //----------------------------------------------------------------------              
                       
        private void CopyImage(object o)
        {
            if (CurrentTbl81Image == null)
            {
                WpfMessageBox.Show(CultRes.StringsRes.DatasetNew,
                    CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                return;
            }

            var image = _businessLayer.SingleListTbl81ImagesByImageId(CurrentTbl81Image.ImageID);

            Tbl81ImagesList.Insert(0, new Tbl81Image
            {
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
                          
        private void DeleteImage(object o)
        {
            if (CurrentTbl81Image == null)
            {
                WpfMessageBox.Show(CultRes.StringsRes.DatasetNew,
                    CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                return;
            }

            try
            {
                var image = _businessLayer.SingleListTbl81ImagesByImageId(CurrentTbl81Image.ImageID);
                if (image!= null)
                {
                    if (WpfMessageBox.Show(CultRes.StringsRes.DeleteQuestion1, CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl81Image.ImageID,
                         MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes) 
                    return;
                    image.EntityState = EntityState.Deleted;
                    _businessLayer.RemoveImage(image);

                    WpfMessageBox.Show(CultRes.StringsRes.DeleteSuccess, Convert.ToString(CurrentTbl81Image.ImageID),
                       MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);  
                }
                else
                {
                    WpfMessageBox.Show(CultRes.StringsRes.Information, CultRes.StringsRes.DeleteCan + " " + CurrentTbl81Image.ImageID + " " + CultRes.StringsRes.DeleteCan1,
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                }
            }
            catch (DbEntityValidationException ex)
            {
                _entityException.EntityException(ex);
                    Log.Error(ex);
            }
         
            Tbl81ImagesList = new ObservableCollection<Tbl81Image>(_businessLayer.ListTbl81ImagesByFiSpeciesId(CurrentTbl69FiSpecies.FiSpeciesID));

            ImagesView = CollectionViewSource.GetDefaultView(Tbl81ImagesList);
            ImagesView.Refresh();
        }
        //-------------------------------------------------------------------------------------------------                    
                       
        private void SaveImage(object o)
        {
            if (CurrentTbl81Image == null)
            {
                WpfMessageBox.Show(CultRes.StringsRes.DatasetNew,
                    CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                return;
            }

            CurrentTbl81Image.FiSpeciesID = CurrentTbl69FiSpecies.FiSpeciesID;

            //Search for CurrentTbl81Image.PlSpeciesID with Plantae#Regnum# 
            var plantaeRegnum = _businessLayer.SingleListTbl72PlSpeciessesByPlSpeciesName("Plantae#Regnum#");
            CurrentTbl81Image.PlSpeciesID = plantaeRegnum.PlSpeciesID;

            try
            {
                var image = _businessLayer.SingleListTbl81ImagesByImageId(CurrentTbl81Image.ImageID);
                if (CurrentTbl81Image.ImageID != 0)
                {
                    if (image != null) //update
                    {
                            image.FiSpeciesID = CurrentTbl81Image.FiSpeciesID;         
                            image.PlSpeciesID = CurrentTbl81Image.PlSpeciesID;
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
                            PlSpeciesID = CurrentTbl81Image.PlSpeciesID,
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

                    //check if dataset with ImageId and FiSpeciesId already exist       
                    var dataset = _businessLayer.ListTbl81ImagesByImageIdAndFiSpeciesIdAndPlSpeciesId(CurrentTbl81Image.ImageID, CurrentTbl81Image.FiSpeciesID, CurrentTbl81Image.PlSpeciesID);

                    if (dataset.Count != 0 && CurrentTbl81Image.ImageID == 0)  //dataset exist
                    {
                        WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTbl81Image.ImageID.ToString(),
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        return;
                    }
                    if (dataset.Count == 0 && CurrentTbl81Image.ImageID == 0 ||
                        dataset.Count != 0 && CurrentTbl81Image.ImageID != 0 ||
                        dataset.Count == 0 && CurrentTbl81Image.ImageID != 0) //new dataset and update
                    {
                        if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTbl81Image.ImageID.ToString(),
                                MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes)
                            return;
                        {
                            try
                            {
                                _businessLayer.UpdateImage(image);
                            }
                            catch (DbUpdateException e)
                            {
                                if (e.InnerException != null)
                                    System.Windows.MessageBox.Show(e.InnerException.ToString(), CultRes.StringsRes.FailedToSave,
                                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);

                                Log.Error(e);
                                return;
                            }
                            catch (Exception e)
                            {
                                System.Windows.MessageBox.Show(e.Message, CultRes.StringsRes.Error,
                                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);
                                Log.Error(e);
                                return;
                            }
                                    WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess,
                                        CurrentTbl81Image.ImageID == 0
                                            ? CultRes.StringsRes.DatasetNew
                                            : CurrentTbl81Image.ImageID.ToString(),
                                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        }
                    }
                }
            }
            catch (DbEntityValidationException ex)
            {
                _entityException.EntityException(ex);
                    Log.Error(ex);
                  return;
            }

            Tbl81ImagesList = new ObservableCollection<Tbl81Image>(_businessLayer.ListTbl81ImagesByFiSpeciesId(CurrentTbl69FiSpecies.FiSpeciesID));            

            SelectedMainTabIndex = 3;
            SelectedDetailSubTabIndex = 3;
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
        private RelayCommand _addSynonymCommand;

        public ICommand AddSynonymCommand => _addSynonymCommand ??
                                                (_addSynonymCommand = new RelayCommand(delegate { AddSynonym(null); }));

        private RelayCommand _copySynonymCommand;

        public ICommand CopySynonymCommand => _copySynonymCommand ??
                                                 (_copySynonymCommand = new RelayCommand(delegate { CopySynonym(null); }));

        private RelayCommand _deleteSynonymCommand;

        public ICommand DeleteSynonymCommand => _deleteSynonymCommand ??
                                                 (_deleteSynonymCommand = new RelayCommand(delegate { DeleteSynonym(null); }));

        private RelayCommand _saveSynonymCommand;

        public ICommand SaveSynonymCommand => _saveSynonymCommand ??
                                                 (_saveSynonymCommand = new RelayCommand(delegate { SaveSynonym(null); }));

        //-------------------------------------------------------------------------          
              
        private void AddSynonym(object o)      
        {
            if (Tbl84SynonymsList == null)
                Tbl84SynonymsList =  new ObservableCollection<Tbl84Synonym>( );

             Tbl84SynonymsList.Insert(0, new Tbl84Synonym  { SynonymName = CultRes.StringsRes.DatasetNew});

            SynonymsView = CollectionViewSource.GetDefaultView(Tbl84SynonymsList);
            SynonymsView.MoveCurrentToFirst();
        }
        //----------------------------------------------------------------------              
             
        private void CopySynonym(object o)
        {
            if (CurrentTbl84Synonym == null)
            {
                WpfMessageBox.Show(CultRes.StringsRes.DatasetNew,
                    CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                return;
            }

            Tbl84SynonymsList = new ObservableCollection<Tbl84Synonym>();

            var synonym = _businessLayer.SingleListTbl84SynonymsBySynonymId(CurrentTbl84Synonym.SynonymID);

            Tbl84SynonymsList.Add(new Tbl84Synonym
            {
                SynonymName = CultRes.StringsRes.DatasetNew,
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
                          
        private void DeleteSynonym(object o)
        {
            if (CurrentTbl84Synonym == null)
            {
                WpfMessageBox.Show(CultRes.StringsRes.DatasetNew,
                    CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                return;
            }

            try
            {
                var synonym = _businessLayer.SingleListTbl84SynonymsBySynonymId(CurrentTbl84Synonym.SynonymID);
                if (synonym!= null)
                {
                    if (WpfMessageBox.Show(CultRes.StringsRes.DeleteQuestion1, CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl84Synonym.SynonymName,
                         MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes) 
                    return;
                    synonym.EntityState = EntityState.Deleted;
                    _businessLayer.RemoveSynonym(synonym);

                    WpfMessageBox.Show(CultRes.StringsRes.DeleteSuccess, CurrentTbl84Synonym.SynonymName,
                       MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);  
                }
                else
                {
                    WpfMessageBox.Show(CultRes.StringsRes.Information, CultRes.StringsRes.DeleteCan + " " + CurrentTbl84Synonym.SynonymName + " " + CultRes.StringsRes.DeleteCan1,
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                }
            }
            catch (DbEntityValidationException ex)
            {
                _entityException.EntityException(ex);
                                 Log.Error(ex);
           }
         
            Tbl84SynonymsList = new ObservableCollection<Tbl84Synonym>(_businessLayer.ListTbl84SynonymsByFiSpeciesId(CurrentTbl69FiSpecies.FiSpeciesID));

            SynonymsView = CollectionViewSource.GetDefaultView(Tbl84SynonymsList);
            SynonymsView.Refresh();
        }
        //-------------------------------------------------------------------------------------------------                    
             
        private void SaveSynonym(object o)
        {
            if (CurrentTbl84Synonym == null)
            {
                WpfMessageBox.Show(CultRes.StringsRes.DatasetNew,
                    CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                return;
            }

            CurrentTbl84Synonym.FiSpeciesID = CurrentTbl69FiSpecies.FiSpeciesID;

            //Search for CurrentTbl84Synonym.PlSpeciesID with Plantae#Regnum# 
            var plantaeRegnum = _businessLayer.SingleListTbl72PlSpeciessesByPlSpeciesName("Plantae#Regnum#");
            CurrentTbl84Synonym.PlSpeciesID = plantaeRegnum.PlSpeciesID;

            try
            {
                var synonym = _businessLayer.SingleListTbl84SynonymsBySynonymId(CurrentTbl84Synonym.SynonymID);
                if (CurrentTbl84Synonym.SynonymID != 0)
                {
                    if (synonym != null) //update
                    {
                            synonym.FiSpeciesID = CurrentTbl84Synonym.FiSpeciesID;
                            synonym.PlSpeciesID = CurrentTbl84Synonym.PlSpeciesID;
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
                            FiSpeciesID = CurrentTbl84Synonym.FiSpeciesID,
                            PlSpeciesID = CurrentTbl84Synonym.PlSpeciesID,
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
                            Memo = CurrentTbl84Synonym.Memo,
                            EntityState = EntityState.Added  
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
                        return;
                    }
                    if (dataset.Count == 0 && CurrentTbl84Synonym.SynonymID == 0 ||
                        dataset.Count != 0 && CurrentTbl84Synonym.SynonymID != 0 ||
                        dataset.Count == 0 && CurrentTbl84Synonym.SynonymID != 0) //new dataset and update
                    {
                        if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTbl84Synonym.SynonymName,
                                MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes)
                            return;
                        {
                            try
                            {
                                _businessLayer.UpdateSynonym(synonym);
                            }
                            catch (DbUpdateException e)
                            {
                                if (e.InnerException != null)
                                    System.Windows.MessageBox.Show(e.InnerException.ToString(), CultRes.StringsRes.FailedToSave,
                                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);

                                Log.Error(e);
                                return;
                            }
                            catch (Exception e)
                            {
                                System.Windows.MessageBox.Show(e.Message, CultRes.StringsRes.Error,
                                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);
                                Log.Error(e);
                                return;
                            }
                                    WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess,
                                        CurrentTbl84Synonym.SynonymID == 0
                                            ? CultRes.StringsRes.DatasetNew
                                            : CurrentTbl84Synonym.SynonymName,
                                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        }
                    }
                }
            }
            catch (DbEntityValidationException ex)
            {
                _entityException.EntityException(ex);
                     Log.Error(ex);
                   return;
           }

            Tbl84SynonymsList = new ObservableCollection<Tbl84Synonym>(_businessLayer.ListTbl84SynonymsByFiSpeciesId(CurrentTbl69FiSpecies.FiSpeciesID));            

            SelectedMainTabIndex = 4;
            SelectedDetailSubTabIndex = 4;
            SynonymsView = CollectionViewSource.GetDefaultView(Tbl84SynonymsList);
            SynonymsView.Refresh();
        }
        #endregion "Public Commands"                  
   
            

 //    Part 7    

             
       #region "Public Commands Connect ==> Tbl87Geographic"                 
        //-------------------------------------------------------------------------
        private RelayCommand _addGeographicCommand;

        public ICommand AddGeographicCommand => _addGeographicCommand ??
                                                (_addGeographicCommand = new RelayCommand(delegate { AddGeographic(null); }));

        private RelayCommand _copyGeographicCommand;

        public ICommand CopyGeographicCommand => _copyGeographicCommand ??
                                                 (_copyGeographicCommand = new RelayCommand(delegate { CopyGeographic(null); }));

        private RelayCommand _deleteGeographicCommand;

        public ICommand DeleteGeographicCommand => _deleteGeographicCommand ??
                                                 (_deleteGeographicCommand = new RelayCommand(delegate { DeleteGeographic(null); }));

        private RelayCommand _saveGeographicCommand;

        public ICommand SaveGeographicCommand => _saveGeographicCommand ??
                                                 (_saveGeographicCommand = new RelayCommand(delegate { SaveGeographic(null); }));

        //-------------------------------------------------------------------------          
              
        private void AddGeographic(object o)      
        {
            if (Tbl87GeographicsList == null)
                Tbl87GeographicsList =  new ObservableCollection<Tbl87Geographic>( );

            Tbl87GeographicsList.Insert(0, new Tbl87Geographic  { Info = CultRes.StringsRes.DatasetNew});

            GeographicsView = CollectionViewSource.GetDefaultView(Tbl87GeographicsList);
            GeographicsView.MoveCurrentToFirst();
        }
        //----------------------------------------------------------------------              
             
        private void CopyGeographic(object o)
        {
            if (CurrentTbl87Geographic == null)
            {
                WpfMessageBox.Show(CultRes.StringsRes.DatasetNew,
                    CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                return;
            }

            Tbl87GeographicsList = new ObservableCollection<Tbl87Geographic>();

            var geographic = _businessLayer.SingleListTbl87GeographicsByGeographicId(CurrentTbl87Geographic.GeographicID);

            Tbl87GeographicsList.Add(new Tbl87Geographic
            {
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
                          
        private void DeleteGeographic(object o)
        {
            if (CurrentTbl87Geographic == null)
            {
                WpfMessageBox.Show(CultRes.StringsRes.DatasetNew,
                    CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                return;
            }

            try
            {
                var geographic = _businessLayer.SingleListTbl87GeographicsByGeographicId(CurrentTbl87Geographic.GeographicID);
                if (geographic!= null)
                {
                    if (WpfMessageBox.Show(CultRes.StringsRes.DeleteQuestion1, CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl87Geographic.GeographicID,
                         MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes) 
                    return;
                    geographic.EntityState = EntityState.Deleted;
                    _businessLayer.RemoveGeographic(geographic);

                    WpfMessageBox.Show(CultRes.StringsRes.DeleteSuccess, CurrentTbl87Geographic.GeographicID.ToString(),
                       MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);  
                }
                else
                {
                    WpfMessageBox.Show(CultRes.StringsRes.Information, CultRes.StringsRes.DeleteCan + " " + CurrentTbl87Geographic.GeographicID + " " + CultRes.StringsRes.DeleteCan1,
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                }
            }
            catch (DbEntityValidationException ex)
            {
                _entityException.EntityException(ex);
                                Log.Error(ex);
            }
         
            Tbl87GeographicsList = new ObservableCollection<Tbl87Geographic>(_businessLayer.ListTbl87GeographicsByFiSpeciesId(CurrentTbl69FiSpecies.FiSpeciesID));

            GeographicsView = CollectionViewSource.GetDefaultView(Tbl87GeographicsList);
            GeographicsView.Refresh();
        }
        //-------------------------------------------------------------------------------------------------                    
             
        private void SaveGeographic(object o)
        {
            if (CurrentTbl87Geographic == null)
            {
                WpfMessageBox.Show(CultRes.StringsRes.DatasetNew,
                    CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                return;
            }

            CurrentTbl87Geographic.FiSpeciesID = CurrentTbl69FiSpecies.FiSpeciesID;

            //Search for CurrentTbl87Geographic.PlSpeciesID with Plantae#Regnum# 
            var plantaeRegnum = _businessLayer.SingleListTbl72PlSpeciessesByPlSpeciesName("Plantae#Regnum#");
            CurrentTbl87Geographic.PlSpeciesID = plantaeRegnum.PlSpeciesID;
            try
            {
                var geographic = _businessLayer.SingleListTbl87GeographicsByGeographicId(CurrentTbl87Geographic.GeographicID);
                if (CurrentTbl87Geographic.GeographicID != 0)
                {
                    if (geographic != null) //update
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
                            FiSpeciesID = CurrentTbl87Geographic.FiSpeciesID,
                            PlSpeciesID = CurrentTbl87Geographic.PlSpeciesID,
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
                        return;
                    }
                    if (dataset.Count == 0 && CurrentTbl87Geographic.GeographicID == 0 ||
                        dataset.Count != 0 && CurrentTbl87Geographic.GeographicID != 0 ||
                        dataset.Count == 0 && CurrentTbl87Geographic.GeographicID != 0) //new dataset and update
                    {
                        if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTbl87Geographic.GeographicID.ToString(),
                                MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes)
                            return;
                        {
                            try
                            {
                               _businessLayer.UpdateGeographic(geographic);
                            }
                            catch (DbUpdateException e)
                            {
                                if (e.InnerException != null)
                                    System.Windows.MessageBox.Show(e.InnerException.ToString(), CultRes.StringsRes.FailedToSave,
                                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);

                                Log.Error(e);
                                return;
                            }
                            catch (Exception e)
                            {
                                System.Windows.MessageBox.Show(e.Message, CultRes.StringsRes.Error,
                                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);
                                Log.Error(e);
                                return;
                            }
                                    WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess,
                                        CurrentTbl87Geographic.GeographicID == 0
                                            ? CultRes.StringsRes.DatasetNew
                                            : CurrentTbl87Geographic.GeographicID.ToString(),
                                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        }
                    }
                }
            }
            catch (DbEntityValidationException ex)
            {
                _entityException.EntityException(ex);
                                Log.Error(ex);
                  return;
           }

            Tbl87GeographicsList = new ObservableCollection<Tbl87Geographic>(_businessLayer.ListTbl87GeographicsByFiSpeciesId(CurrentTbl69FiSpecies.FiSpeciesID));            

            SelectedMainTabIndex = 5;
            SelectedDetailSubTabIndex = 5;
            GeographicsView = CollectionViewSource.GetDefaultView(Tbl87GeographicsList);
            GeographicsView.Refresh();
        }
        #endregion "Public Commands"                  
   

 //    Part 8    

           
        #region "Public Commands Connect ==> Tbl90ReferenceAuthor"
        //-------------------------------------------------------------------------
        private RelayCommand _addReferenceAuthorCommand;

        public ICommand AddReferenceAuthorCommand => _addReferenceAuthorCommand ??
                                                    (_addReferenceAuthorCommand = new RelayCommand(delegate { AddReferenceAuthor(null); }));

        private RelayCommand _copyReferenceAuthorCommand;

        public ICommand CopyReferenceAuthorCommand => _copyReferenceAuthorCommand ??
                        (_copyReferenceAuthorCommand = new RelayCommand(delegate { CopyReferenceAuthor(null); }));

        private RelayCommand _deleteReferenceAuthorCommand;

        public ICommand DeleteReferenceAuthorCommand => _deleteReferenceAuthorCommand ??
                                               (_deleteReferenceAuthorCommand = new RelayCommand(delegate { DeleteReferenceAuthor(null); }));

        private RelayCommand _saveReferenceAuthorCommand;

        public ICommand SaveReferenceAuthorCommand => _saveReferenceAuthorCommand ??
                     (_saveReferenceAuthorCommand = new RelayCommand(delegate { SaveReferenceAuthor(null); }));
        //-------------------------------------------------------------------------                    
     
        public void AddReferenceAuthor(object o)
        {
            if (Tbl90ReferenceAuthorsList == null)
                Tbl90ReferenceAuthorsList = new ObservableCollection<Tbl90Reference>();

            Tbl90ReferenceAuthorsList.Insert(0, new Tbl90Reference   { Info = CultRes.StringsRes.DatasetNew });

            ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
            ReferenceAuthorsView.MoveCurrentToFirst();
         }
        //----------------------------------------------------------------------            
     
        public void CopyReferenceAuthor(object o)
        {
            if (CurrentTbl90ReferenceAuthor == null)
            {
                WpfMessageBox.Show(CultRes.StringsRes.DatasetNew,
                    CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                return;
            }

            var reference = _businessLayer.SingleListTbl90ReferencesByReferenceId(CurrentTbl90ReferenceAuthor.ReferenceID);

            Tbl90ReferenceAuthorsList.Insert(0, new Tbl90Reference
            {
                RefAuthorID = reference.RefAuthorID,
                Valid = reference.Valid,
                ValidYear = reference.ValidYear,
                Info = CultRes.StringsRes.DatasetNew,
                Memo = reference.Memo
            });

            ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
            ReferenceAuthorsView.MoveCurrentToFirst();
        }
        //----------------------------------------------------------------------            
     
        private void DeleteReferenceAuthor(object o)
        {
            if (CurrentTbl90ReferenceAuthor == null)
            {
                WpfMessageBox.Show(CultRes.StringsRes.DatasetNew,
                    CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                return;
            }

                 try
                {
                    var reference = _businessLayer.SingleListTbl90ReferencesByReferenceId(CurrentTbl90ReferenceAuthor.ReferenceID);
                    if (reference != null)
                    {
                        if (WpfMessageBox.Show(CultRes.StringsRes.DeleteQuestion1, CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl90ReferenceAuthor.Info,
                                MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes)
                            return;
                        reference.EntityState = EntityState.Deleted;
                        _businessLayer.RemoveReference(reference);

                        WpfMessageBox.Show(CultRes.StringsRes.DeleteSuccess, CurrentTbl90ReferenceAuthor.Info,
                            MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                    }
                    else
                    {
                        WpfMessageBox.Show(CultRes.StringsRes.Information, CultRes.StringsRes.DeleteCan + " " + CurrentTbl90ReferenceAuthor.Info + " " + CultRes.StringsRes.DeleteCan1,
                            MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                    }
                }
                catch (DbEntityValidationException ex)
                {
                    _entityException.EntityException(ex);
                                Log.Error(ex);
                }

            Tbl90ReferenceAuthorsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ReferenceListRefAuthorsByFiSpeciesId(CurrentTbl69FiSpecies.FiSpeciesID));

            ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
            ReferenceAuthorsView.Refresh();
        }

        //----------------------------------------------------------------------            
     
        public void SaveReferenceAuthor(object o)
        {
            if (CurrentTbl90ReferenceAuthor == null)
            {
                WpfMessageBox.Show(CultRes.StringsRes.DatasetNew,
                    CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                return;
            }

            CurrentTbl90ReferenceAuthor.FiSpeciesID = CurrentTbl69FiSpecies.FiSpeciesID;

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
                        if (CurrentTbl90ReferenceAuthor.RefExpertID == null &&
                            CurrentTbl90ReferenceAuthor.RefSourceID == null &&
                            CurrentTbl90ReferenceAuthor.RefAuthorID == null)
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
                        return;
                    }
                    if (dataset.Count == 0 && CurrentTbl90ReferenceAuthor.ReferenceID == 0 ||
                        dataset.Count != 0 && CurrentTbl90ReferenceAuthor.ReferenceID != 0 ||
                        dataset.Count == 0 && CurrentTbl90ReferenceAuthor.ReferenceID != 0) //new dataset and update
                    {
                        if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTbl90ReferenceAuthor.Info,
                                MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes)
                            return;
                        {
                            try
                            {
                                _businessLayer.UpdateReference(reference);
                            }
                            catch (DbUpdateException e)
                            {
                                if (e.InnerException != null)
                                    System.Windows.MessageBox.Show(e.InnerException.ToString(), CultRes.StringsRes.FailedToSave,
                                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);

                                Log.Error(e);
                                return;
                            }
                            catch (Exception e)
                            {
                                System.Windows.MessageBox.Show(e.Message, CultRes.StringsRes.Error,
                                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);
                                Log.Error(e);
                                return;
                            }
                                    WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess,
                                        CurrentTbl90ReferenceAuthor.ReferenceID == 0
                                            ? CultRes.StringsRes.DatasetNew
                                            : CurrentTbl90ReferenceAuthor.ReferenceID.ToString(),
                                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        }
                    }
                }
            }
            catch (DbEntityValidationException ex)
            {
                _entityException.EntityException(ex);
                                Log.Error(ex);
                 return;
            }

            Tbl90ReferenceAuthorsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ReferenceListRefAuthorsByFiSpeciesId(CurrentTbl69FiSpecies.FiSpeciesID));           
      

            SelectedMainTabIndex = 6;
            SelectedDetailSubTabIndex = 6;
            SelectedMainSubRefTabIndex = 2;

            ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
            ReferenceAuthorsView.Refresh();
        }
        #endregion "Public Commands"                               
           
        #region "Public Commands Connect ==> Tbl90ReferenceSource" 
        //-------------------------------------------------------------------------
        private RelayCommand _addReferenceSourceCommand;

        public ICommand AddReferenceSourceCommand => _addReferenceSourceCommand ??
                                                    (_addReferenceSourceCommand = new RelayCommand(delegate { AddReferenceSource(null); }));

        private RelayCommand _copyReferenceSourceCommand;

        public ICommand CopyReferenceSourceCommand => _copyReferenceSourceCommand ??
                        (_copyReferenceSourceCommand = new RelayCommand(delegate { CopyReferenceSource(null); }));

        private RelayCommand _deleteReferenceSourceCommand;

        public ICommand DeleteReferenceSourceCommand => _deleteReferenceSourceCommand ??
                                                        (_deleteReferenceSourceCommand = new RelayCommand(delegate { DeleteReferenceSource(null); }));

        private RelayCommand _saveReferenceSourceCommand;

        public ICommand SaveReferenceSourceCommand => _saveReferenceSourceCommand ??
                     (_saveReferenceSourceCommand = new RelayCommand(delegate { SaveReferenceSource(null); }));

        //-------------------------------------------------------------------------          
     
        public void AddReferenceSource(object o)
        {
            if (Tbl90ReferenceSourcesList == null)
                Tbl90ReferenceSourcesList = new ObservableCollection<Tbl90Reference>();

            Tbl90ReferenceSourcesList .Insert(0, new Tbl90Reference  { Info = CultRes.StringsRes.DatasetNew });

            ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
            ReferenceSourcesView.MoveCurrentToFirst();
         }
        //----------------------------------------------------------------------            
     
        public void CopyReferenceSource(object o)
        {
            if (CurrentTbl90ReferenceSource == null)
            {
                WpfMessageBox.Show(CultRes.StringsRes.DatasetNew,
                    CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                return;
            }

            var reference = _businessLayer.SingleListTbl90ReferencesByReferenceId(CurrentTbl90ReferenceSource.ReferenceID);

            Tbl90ReferenceSourcesList.Insert(0, new Tbl90Reference
            {
                RefSourceID = reference.RefSourceID,
                Valid = reference.Valid,
                ValidYear = reference.ValidYear,
                Info = CultRes.StringsRes.DatasetNew,
                Memo = reference.Memo
            });

            ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
            ReferenceSourcesView.MoveCurrentToFirst();
        }
        //----------------------------------------------------------------------            
     
        private void DeleteReferenceSource(object o)
        {
            if (CurrentTbl90ReferenceSource == null)
            {
                WpfMessageBox.Show(CultRes.StringsRes.DatasetNew,
                    CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                return;
            }

            try
            {
                var reference = _businessLayer.SingleListTbl90ReferencesByReferenceId(CurrentTbl90ReferenceSource.ReferenceID);
                if (reference != null)
                {
                    if (WpfMessageBox.Show(CultRes.StringsRes.DeleteQuestion1, CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl90ReferenceSource.Info,
                            MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes)
                        return;
                    reference.EntityState = EntityState.Deleted;
                    _businessLayer.RemoveReference(reference);

                    WpfMessageBox.Show(CultRes.StringsRes.DeleteSuccess, CurrentTbl90ReferenceSource.Info,
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                }
                else
                {
                    WpfMessageBox.Show(CultRes.StringsRes.Information, CultRes.StringsRes.DeleteCan + " " + CurrentTbl90ReferenceSource.Info + " " + CultRes.StringsRes.DeleteCan1,
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                }
            }
            catch (DbEntityValidationException ex)
            {
                _entityException.EntityException(ex);
                                Log.Error(ex);
            }

            Tbl90ReferenceSourcesList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ReferenceListRefSourcesByFiSpeciesId(CurrentTbl69FiSpecies.FiSpeciesID));

            ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
            ReferenceSourcesView.Refresh();
        }        
        //----------------------------------------------------------------------            
     
        public void SaveReferenceSource(object o)
        {
            if (CurrentTbl90ReferenceSource == null)
            {
                WpfMessageBox.Show(CultRes.StringsRes.DatasetNew,
                    CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                return;
            }

            CurrentTbl90ReferenceSource.FiSpeciesID = CurrentTbl69FiSpecies.FiSpeciesID;

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
                        return;
                    }
                    if (dataset.Count == 0 && CurrentTbl90ReferenceSource.ReferenceID == 0 ||
                        dataset.Count != 0 && CurrentTbl90ReferenceSource.ReferenceID != 0 ||
                        dataset.Count == 0 && CurrentTbl90ReferenceSource.ReferenceID != 0) //new dataset and update
                    {
                        if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTbl90ReferenceSource.Info,
                                MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes)
                            return;
                        {
                            try
                            {
                                _businessLayer.UpdateReference(reference);
                            }
                            catch (DbUpdateException e)
                            {
                                if (e.InnerException != null)
                                    System.Windows.MessageBox.Show(e.InnerException.ToString(), CultRes.StringsRes.FailedToSave,
                                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);

                                Log.Error(e);
                                return;
                            }
                            catch (Exception e)
                            {
                                System.Windows.MessageBox.Show(e.Message, CultRes.StringsRes.Error,
                                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);
                                Log.Error(e);
                                return;
                            }
                                    WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess,
                                        CurrentTbl90ReferenceSource.ReferenceID == 0
                                            ? CultRes.StringsRes.DatasetNew
                                             : CurrentTbl90ReferenceSource.ReferenceID.ToString(),
                                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        }
                    }
                }
            }
            catch (DbEntityValidationException ex)
            {
                _entityException.EntityException(ex);
                                Log.Error(ex);
                 return;
            }

            Tbl90ReferenceSourcesList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ReferenceListRefSourcesByFiSpeciesId(CurrentTbl69FiSpecies.FiSpeciesID));           
      
            SelectedMainTabIndex = 6;
            SelectedDetailSubTabIndex = 6;
            SelectedMainSubRefTabIndex = 1;

            ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
            ReferenceSourcesView.Refresh();
        }
        #endregion "Public Commands"                              
           
        #region "Public Commands Connect ==> Tbl90ReferenceExpert"
        //-------------------------------------------------------------------------
 
        private RelayCommand _addReferenceExpertCommand;

        public ICommand AddReferenceExpertCommand => _addReferenceExpertCommand ??
                                                    (_addReferenceExpertCommand = new RelayCommand(delegate { AddReferenceExpert(null); }));

        private RelayCommand _copyReferenceExpertCommand;

        public ICommand CopyReferenceExpertCommand => _copyReferenceExpertCommand ??
                        (_copyReferenceExpertCommand = new RelayCommand(delegate { CopyReferenceExpert(null); }));

        private RelayCommand _deleteReferenceExpertCommand;

        public ICommand DeleteReferenceExpertCommand => _deleteReferenceExpertCommand ??
                                                        (_deleteReferenceExpertCommand = new RelayCommand(delegate { DeleteReferenceExpert(null); }));
        private RelayCommand _saveReferenceExpertCommand;

        public ICommand SaveReferenceExpertCommand => _saveReferenceExpertCommand ??
                     (_saveReferenceExpertCommand = new RelayCommand(delegate { SaveReferenceExpert(null); }));
        //-------------------------------------------------------------------------          
     
        public void AddReferenceExpert(object o)
        {
            if (Tbl90ReferenceExpertsList == null)
                Tbl90ReferenceExpertsList = new ObservableCollection<Tbl90Reference>();

            Tbl90ReferenceExpertsList .Insert(0, new Tbl90Reference   { Info = CultRes.StringsRes.DatasetNew });

            ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
            ReferenceExpertsView.MoveCurrentToFirst();
         }
        //----------------------------------------------------------------------            
     
        public void CopyReferenceExpert(object o)
        {
            if (CurrentTbl90ReferenceExpert == null)
            {
                WpfMessageBox.Show(CultRes.StringsRes.DatasetNew,
                    CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                return;
            }

            var reference = _businessLayer.SingleListTbl90ReferencesByReferenceId(CurrentTbl90ReferenceExpert.ReferenceID);

            Tbl90ReferenceExpertsList.Insert(0, new Tbl90Reference
            {
                RefExpertID = reference.RefExpertID,
                Valid = reference.Valid,
                ValidYear = reference.ValidYear,
                Info = CultRes.StringsRes.DatasetNew,
                Memo = reference.Memo
            });

            ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
            ReferenceExpertsView.MoveCurrentToFirst();
        }
        //----------------------------------------------------------------------            
     
        private void DeleteReferenceExpert(object o)
        {
            if (CurrentTbl90ReferenceExpert == null)
            {
                WpfMessageBox.Show(CultRes.StringsRes.DatasetNew,
                    CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                return;
            }

            try
            {
                var reference = _businessLayer.SingleListTbl90ReferencesByReferenceId(CurrentTbl90ReferenceExpert.ReferenceID);
                if (reference != null)
                {
                    if (WpfMessageBox.Show(CultRes.StringsRes.DeleteQuestion1, CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl90ReferenceExpert.Info,
                            MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes)
                        return;
                    reference.EntityState = EntityState.Deleted;
                    _businessLayer.RemoveReference(reference);

                    WpfMessageBox.Show(CultRes.StringsRes.DeleteSuccess, CurrentTbl90ReferenceExpert.Info,
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                }
                else
                {
                    WpfMessageBox.Show(CultRes.StringsRes.Information, CultRes.StringsRes.DeleteCan + " " + CurrentTbl90ReferenceExpert.Info + " " + CultRes.StringsRes.DeleteCan1,
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                }
            }
            catch (DbEntityValidationException ex)
            {
                _entityException.EntityException(ex);
                                Log.Error(ex);
            }

            Tbl90ReferenceExpertsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ReferenceListRefExpertsByFiSpeciesId(CurrentTbl69FiSpecies.FiSpeciesID));

            ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
            ReferenceExpertsView.Refresh();
        }
        //----------------------------------------------------------------------            
     
        public void SaveReferenceExpert(object o)
        {
            if (CurrentTbl90ReferenceExpert == null)
            {
                WpfMessageBox.Show(CultRes.StringsRes.DatasetNew,
                    CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                return;
            }

            CurrentTbl90ReferenceExpert.FiSpeciesID = CurrentTbl69FiSpecies.FiSpeciesID;

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
                        return;
                    }
                    if (dataset.Count == 0 && CurrentTbl90ReferenceExpert.ReferenceID == 0 ||
                        dataset.Count != 0 && CurrentTbl90ReferenceExpert.ReferenceID != 0 ||
                        dataset.Count == 0 && CurrentTbl90ReferenceExpert.ReferenceID != 0) //new dataset and update
                    {
                        if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTbl90ReferenceExpert.Info,
                                MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes)
                            return;
                        {
                            try
                            {
                                _businessLayer.UpdateReference(reference);
                            }
                            catch (DbUpdateException e)
                            {
                                if (e.InnerException != null)
                                    System.Windows.MessageBox.Show(e.InnerException.ToString(), CultRes.StringsRes.FailedToSave,
                                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);

                                Log.Error(e);
                                return;
                            }
                            catch (Exception e)
                            {
                                System.Windows.MessageBox.Show(e.Message, CultRes.StringsRes.Error,
                                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);
                                Log.Error(e);
                                return;
                            }
                                    WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess,
                                        CurrentTbl90ReferenceExpert.ReferenceID == 0
                                            ? CultRes.StringsRes.DatasetNew
                                             : CurrentTbl90ReferenceExpert.ReferenceID.ToString(),
                                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        }
                    }
                }
            }
            catch (DbEntityValidationException ex)
            {
                _entityException.EntityException(ex);
                                Log.Error(ex);
                  return;
            }

            Tbl90ReferenceExpertsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ReferenceListRefExpertsByFiSpeciesId(CurrentTbl69FiSpecies.FiSpeciesID));     
         
            SelectedMainTabIndex = 6;
            SelectedDetailSubTabIndex = 6;
            SelectedMainSubRefTabIndex = 0;

            ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
            ReferenceExpertsView.Refresh();
        }
        #endregion "Public Commands"                           
           
        #region "Public Commands Connect ==> Tbl93Comment"

        //-------------------------------------------------------------------------
        private RelayCommand _addCommentCommand;

        public ICommand AddCommentCommand => _addCommentCommand ??
                                                 (_addCommentCommand = new RelayCommand(delegate { AddComment(null); }));

        private RelayCommand _copyCommentCommand;

        public ICommand CopyCommentCommand => _copyCommentCommand ??
                                                  (_copyCommentCommand = new RelayCommand(delegate { CopyComment(null); }));

        private RelayCommand _deleteCommentCommand;

        public ICommand DeleteCommentCommand => _deleteCommentCommand ??
                                                        (_deleteCommentCommand = new RelayCommand(delegate { DeleteComment(null); }));

        private RelayCommand _saveCommentCommand;

        public ICommand SaveCommentCommand => _saveCommentCommand ??
                                                  (_saveCommentCommand = new RelayCommand(delegate { SaveComment(null); }));
        //-------------------------------------------------------------------------          
     
        public void AddComment(object o)
        {
            if (Tbl93CommentsList == null)
                Tbl93CommentsList = new ObservableCollection<Tbl93Comment>();

            Tbl93CommentsList .Insert(0, new Tbl93Comment  { Info = CultRes.StringsRes.DatasetNew });

            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            CommentsView.MoveCurrentToFirst();
         }
        //----------------------------------------------------------------------            
     
        public void CopyComment(object o)
        {
            if (CurrentTbl93Comment == null)
            {
                WpfMessageBox.Show(CultRes.StringsRes.DatasetNew,
                    CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                return;
            }

            var comment = _businessLayer.SingleListTbl93CommentsByCommentId(CurrentTbl93Comment.CommentID);

            Tbl93CommentsList.Insert(0, new Tbl93Comment
            {
                Valid = comment.Valid,
                ValidYear = comment.ValidYear,
                Info = CultRes.StringsRes.DatasetNew,
                Memo = comment.Memo
            });

            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            CommentsView.MoveCurrentToFirst();
        }
        //----------------------------------------------------------------------            
     
        private void DeleteComment(object o)
        {
            if (CurrentTbl93Comment == null)
            {
                WpfMessageBox.Show(CultRes.StringsRes.DatasetNew,
                    CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                return;
            }

            try
            {
                var comment = _businessLayer.SingleListTbl93CommentsByCommentId(CurrentTbl93Comment.CommentID);
                if (comment != null)
                {
                    if (WpfMessageBox.Show(CultRes.StringsRes.DeleteQuestion1, CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl93Comment.Info,
                            MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes)
                        return;
                    comment.EntityState = EntityState.Deleted;
                    _businessLayer.RemoveComment(comment);

                    WpfMessageBox.Show(CultRes.StringsRes.DeleteSuccess, CurrentTbl93Comment.Info,
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                }
                else
                {
                    WpfMessageBox.Show(CultRes.StringsRes.Information, CultRes.StringsRes.DeleteCan + " " + CurrentTbl93Comment.Info + " " + CultRes.StringsRes.DeleteCan1,
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                }
            }
            catch (DbEntityValidationException ex)
            {
                _entityException.EntityException(ex);
                                Log.Error(ex);
            }

            Tbl93CommentsList = new ObservableCollection<Tbl93Comment>(_businessLayer.ListTbl93CommentsByFiSpeciesId(CurrentTbl69FiSpecies.FiSpeciesID));

            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            CommentsView.Refresh();
        }
        //----------------------------------------------------------------------            
     
        private void SaveComment(object o)
        {
            if (CurrentTbl93Comment == null)
            {
                WpfMessageBox.Show(CultRes.StringsRes.DatasetNew,
                    CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                return;
            }

            CurrentTbl93Comment.FiSpeciesID = CurrentTbl69FiSpecies.FiSpeciesID;

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
                            return;
                    }

                    if (dataset.Count == 0 && CurrentTbl93Comment.CommentID == 0 ||
                        dataset.Count != 0 && CurrentTbl93Comment.CommentID != 0 ||
                        dataset.Count == 0 && CurrentTbl93Comment.CommentID != 0) //new dataset and update
                    {
                        if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTbl93Comment.Info,
                                MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes)
                            return;
                        {
                            try
                            {
                                _businessLayer.UpdateComment(comment);
                            }
                            catch (DbUpdateException e)
                            {
                                if (e.InnerException != null)
                                    System.Windows.MessageBox.Show(e.InnerException.ToString(), CultRes.StringsRes.FailedToSave,
                                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);

                                Log.Error(e);
                                return;
                            }
                            catch (Exception e)
                            {
                                System.Windows.MessageBox.Show(e.Message, CultRes.StringsRes.Error,
                                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);
                                Log.Error(e);
                                return;
                            }
                                    WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess,
                                        CurrentTbl93Comment.CommentID == 0
                                            ? CultRes.StringsRes.DatasetNew
                                            : CurrentTbl93Comment.Info,
                                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        }
                    }
                }
            }
            catch (DbEntityValidationException ex)
            {
                _entityException.EntityException(ex);
                                Log.Error(ex);
                   return;
            }

            Tbl93CommentsList = new ObservableCollection<Tbl93Comment>(_businessLayer.ListTbl93CommentsByFiSpeciesId(CurrentTbl69FiSpecies.FiSpeciesID));          
       
            SelectedMainTabIndex = 7;
            SelectedDetailSubTabIndex = 7;

            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            CommentsView.Refresh();
        }
        #endregion "Public Commands"                             
 
             
 //    Part 9    

   
     
        #region "Public Commands Connected Tables by DoubleClick"

        private RelayCommand _getConnectedTablesCommand;
        public  ICommand GetConnectedTablesCommand
        {
            get { return _getConnectedTablesCommand ?? (_getConnectedTablesCommand = new RelayCommand(delegate { GetConnectedTablesById(null); })); }
        }

        public void GetConnectedTablesById(object o)
        {
            SelectedMainTabIndex = 0;  //change to Connect tab
            SelectedDetailTabIndex = 8;
            SelectedDetailSubTabIndex = 0;

            Tbl66GenussesList = new ObservableCollection<Tbl66Genus>(_businessLayer.ListTbl66GenussesByGenusId(CurrentTbl69FiSpecies.GenusID));

            GenussesView = CollectionViewSource.GetDefaultView(Tbl66GenussesList);
            GenussesView.Refresh();
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
        public  int SelectedMainTabIndex
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
                    SelectedDetailTabIndex = 8;
                    SelectedDetailSubTabIndex = 1;
                }
                if (_selectedMainTabIndex == 2)
                {
                    SelectedDetailTabIndex = 8;
                    SelectedDetailSubTabIndex = 2;
                }
                if (_selectedMainTabIndex == 3)
                {
                    SelectedDetailTabIndex = 8;
                    SelectedDetailSubTabIndex = 3;
                }
                if (_selectedMainTabIndex == 4)
                {
                    SelectedDetailTabIndex = 8;
                    SelectedDetailSubTabIndex = 4;
                }
                if (_selectedMainTabIndex == 5)
                {
                    SelectedDetailTabIndex = 8;
                    SelectedDetailSubTabIndex = 5;
                }
                if (_selectedMainTabIndex == 6)
                {
                    SelectedDetailTabIndex = 8;
                    SelectedDetailSubTabIndex = 6;
                }
                if (_selectedMainTabIndex == 7)
                {
                    SelectedDetailTabIndex = 8;
                    SelectedDetailSubTabIndex = 7;
                }
            }
        }

        public  int SelectedMainSubRefTabIndex
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


        public  int SelectedDetailTabIndex
        {
            get => _selectedDetailTabIndex;
            set
            {
                if (value == _selectedDetailTabIndex) return;
                _selectedDetailTabIndex = value; RaisePropertyChanged();
                SelectedMainTabIndex = 0;
            }
        }

        public  int SelectedDetailSubTabIndex
        {
            get => _selectedDetailSubTabIndex;
            set
            {
                if (value == _selectedDetailSubTabIndex) return;
                _selectedDetailSubTabIndex = value;
                RaisePropertyChanged();
                if (_selectedDetailSubTabIndex == 0)
                {
                    Tbl66GenussesList = new ObservableCollection<Tbl66Genus>(
                        _businessLayer.ListTbl66GenussesByGenusId(CurrentTbl69FiSpecies.GenusID));

                    GenussesView = CollectionViewSource.GetDefaultView(Tbl66GenussesList);
                    GenussesView.Refresh();

                    SelectedMainTabIndex = 0;
                    SelectedDetailTabIndex = 8;
                }
                if (_selectedDetailSubTabIndex == 1)
                {
                    Tbl68SpeciesgroupsList = new ObservableCollection<Tbl68Speciesgroup>(
                        _businessLayer.ListTbl68SpeciesgroupsBySpeciesgroupId(CurrentTbl69FiSpecies.SpeciesgroupID));

                    SpeciesgroupsView = CollectionViewSource.GetDefaultView(Tbl68SpeciesgroupsList);
                    SpeciesgroupsView.Refresh();

                    SelectedMainTabIndex = 1;
                    SelectedDetailTabIndex = 8;
                }
                if (_selectedDetailSubTabIndex == 2)
                {
                    Tbl78NamesList = new ObservableCollection<Tbl78Name>(
                        _businessLayer.ListTbl78NamesByFiSpeciesId(CurrentTbl69FiSpecies.FiSpeciesID));

                    NamesView = CollectionViewSource.GetDefaultView(Tbl78NamesList);
                    NamesView.Refresh();

                    SelectedMainTabIndex = 2;
                    SelectedDetailTabIndex = 8;
                }
                if (_selectedDetailSubTabIndex == 3)
                {
                    Tbl81ImagesList = new ObservableCollection<Tbl81Image>(
                        _businessLayer.ListTbl81ImagesByFiSpeciesId(CurrentTbl69FiSpecies.FiSpeciesID));

                    ImagesView = CollectionViewSource.GetDefaultView(Tbl81ImagesList);
                    ImagesView.Refresh();

                    SelectedMainTabIndex = 3;
                    SelectedDetailTabIndex = 8;
                }
                if (_selectedDetailSubTabIndex == 4)
                {
                    Tbl84SynonymsList = new ObservableCollection<Tbl84Synonym>(
                        _businessLayer.ListTbl84SynonymsByFiSpeciesId(CurrentTbl69FiSpecies.FiSpeciesID));

                    SynonymsView = CollectionViewSource.GetDefaultView(Tbl84SynonymsList);
                    SynonymsView.Refresh();

                    SelectedMainTabIndex = 4;
                    SelectedDetailTabIndex = 8;
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
                    SelectedDetailTabIndex = 8;
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
                    SelectedDetailTabIndex = 8;
                }
                if (_selectedDetailSubTabIndex == 7)
                {
                    Tbl93CommentsList = new ObservableCollection<Tbl93Comment>(
                        _businessLayer.ListTbl93CommentsByFiSpeciesId(CurrentTbl69FiSpecies.FiSpeciesID));

                    CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
                    CommentsView.Refresh();

                    SelectedMainTabIndex = 7;
                    SelectedDetailTabIndex = 8;
                }
            }
        }

        public  int SelectedDetailSubRefTabIndex
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
                        _businessLayer.ListTbl90ReferenceListRefSourcesByFiSpeciesId(CurrentTbl69FiSpecies.FiSpeciesID));

                    ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
                    ReferenceSourcesView.Refresh();

                    SelectedMainSubRefTabIndex = 1;
                }
                if (_selectedDetailSubRefTabIndex == 2)
                {
                    Tbl90AuthorsAllList = new ObservableCollection<Tbl90RefAuthor>(
                        _businessLayer.ListTbl90RefAuthors());

                    Tbl90ReferenceAuthorsList = new ObservableCollection<Tbl90Reference>(
                        _businessLayer.ListTbl90ReferenceListRefAuthorsByFiSpeciesId(CurrentTbl69FiSpecies.FiSpeciesID));

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

        private string _searchFiSpeciesName = "";
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

        public  ICollectionView GenussesView;
        private Tbl66Genus CurrentTbl66Genus => GenussesView?.CurrentItem as Tbl66Genus;

        private ObservableCollection<Tbl66Genus> _tbl66GenussesList;
        public  ObservableCollection<Tbl66Genus> Tbl66GenussesList
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

        public  ICollectionView SpeciesgroupsView;
        private  Tbl68Speciesgroup CurrentTbl68Speciesgroup => SpeciesgroupsView?.CurrentItem as Tbl68Speciesgroup;           

        private ObservableCollection<Tbl68Speciesgroup> _tbl68SpeciesgroupsList;
        public   ObservableCollection<Tbl68Speciesgroup> Tbl68SpeciesgroupsList
        {
            get => _tbl68SpeciesgroupsList; 
            set { _tbl68SpeciesgroupsList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl68Speciesgroup> _tbl68SpeciesgroupsAllList;
        public  ObservableCollection<Tbl68Speciesgroup> Tbl68SpeciesgroupsAllList
        {
            get => _tbl68SpeciesgroupsAllList; 
            set { _tbl68SpeciesgroupsAllList = value; RaisePropertyChanged(); }       
        }

        #endregion "Public Properties"   
        
        #region "Public Properties Tbl78Name"

        public ICollectionView NamesView;
        private Tbl78Name CurrentTbl78Name => NamesView?.CurrentItem as Tbl78Name;           

        private ObservableCollection<Tbl78Name> _tbl78NamesList;
        public  ObservableCollection<Tbl78Name> Tbl78NamesList
        {
            get => _tbl78NamesList; 
            set { _tbl78NamesList = value; RaisePropertyChanged(); }
        }
        #endregion "Public Properties"     
        
        #region "Public Properties Tbl81Image"

        public ICollectionView ImagesView;
        private Tbl81Image CurrentTbl81Image => ImagesView?.CurrentItem as Tbl81Image;           

        private ObservableCollection<Tbl81Image> _tbl81ImagesList;
        public  ObservableCollection<Tbl81Image> Tbl81ImagesList
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
        
        #region "Public Properties Tbl63Infratribus"

        private ObservableCollection<Tbl63Infratribus> _tbl63InfratribussesAllList;
        public  ObservableCollection<Tbl63Infratribus> Tbl63InfratribussesAllList
        {
            get => _tbl63InfratribussesAllList; 
            set { _tbl63InfratribussesAllList = value; RaisePropertyChanged(); }       
        }

        #endregion "Public Properties"     
        
        #region "Public Properties Tbl60Subtribus"

        private ObservableCollection<Tbl60Subtribus> _tbl60SubtribussesAllList;
        public  ObservableCollection<Tbl60Subtribus> Tbl60SubtribussesAllList
        {
            get => _tbl60SubtribussesAllList; 
            set { _tbl60SubtribussesAllList = value; RaisePropertyChanged(); }       
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
        public  ObservableCollection<Tbl90RefAuthor> Tbl90AuthorsAllList
        {
            get => _tbl90AuthorsAllList; 
            set { _tbl90AuthorsAllList = value; RaisePropertyChanged(); }
        }

        #endregion "Public Properties "

        #region "Public Properties Tbl90Source"

        private ObservableCollection<Tbl90RefSource> _tbl90SourcesAllList;
        public  ObservableCollection<Tbl90RefSource> Tbl90SourcesAllList
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

        #region "Public Properties Tbl90ReferenceAuthor"

        public ICollectionView ReferenceAuthorsView;
        private Tbl90Reference CurrentTbl90ReferenceAuthor => ReferenceAuthorsView?.CurrentItem as Tbl90Reference;

        private ObservableCollection<Tbl90Reference> _tbl90ReferenceAuthorsList;
        public ObservableCollection<Tbl90Reference> Tbl90ReferenceAuthorsList
        {
            get => _tbl90ReferenceAuthorsList; 
            set { _tbl90ReferenceAuthorsList = value; RaisePropertyChanged(); }
        }

        #endregion "Public Properties"

        #region "Public Properties Tbl90ReferenceSource"

        public ICollectionView ReferenceSourcesView;
        private Tbl90Reference CurrentTbl90ReferenceSource => ReferenceSourcesView?.CurrentItem as Tbl90Reference;

        private ObservableCollection<Tbl90Reference> _tbl90ReferenceSourcesList;
        public ObservableCollection<Tbl90Reference> Tbl90ReferenceSourcesList
        {
            get => _tbl90ReferenceSourcesList; 
            set { _tbl90ReferenceSourcesList = value; RaisePropertyChanged(); }
        }

        #endregion "Public Properties"

        #region "Public Properties Tbl90ReferenceExpert"

        public ICollectionView ReferenceExpertsView;
        private Tbl90Reference CurrentTbl90ReferenceExpert => ReferenceExpertsView?.CurrentItem as Tbl90Reference;

        private ObservableCollection<Tbl90Reference> _tbl90ReferenceExpertsList;
        public ObservableCollection<Tbl90Reference> Tbl90ReferenceExpertsList
        {
            get => _tbl90ReferenceExpertsList; 
            set { _tbl90ReferenceExpertsList = value; RaisePropertyChanged(); }
        }

        #endregion "Public Properties"   
         
        #region "Public Properties Tbl93Comment"

        public ICollectionView CommentsView;
        private Tbl93Comment CurrentTbl93Comment => CommentsView?.CurrentItem as Tbl93Comment;

        private ObservableCollection<Tbl93Comment> _tbl93CommentsList;
        public ObservableCollection<Tbl93Comment> Tbl93CommentsList
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
