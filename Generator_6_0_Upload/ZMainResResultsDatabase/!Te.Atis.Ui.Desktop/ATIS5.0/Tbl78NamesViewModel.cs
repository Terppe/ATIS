using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity.Validation;
using System.Reflection;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using Te.Atis.DomainModel;
using Te.Atis.Ui.Desktop.BusinessLayer;
using Te.Atis.Ui.Desktop.Domain;
using Te.Atis.Ui.Desktop.Domain.Helper;
using Te.Atis.Ui.Desktop.MessageBox;    

    
using System.Collections.Generic;  
    
         //    Tbl78NamesViewModel Skriptdatum:  12.11.2018  10:32    

namespace Te.Atis.Ui.Desktop.Views.Database
{     
    
    public class Tbl78NamesViewModel : ViewModelBase                     
    {     
         
        #region "Private Data Members"
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private static IBusinessLayer _businessLayer;
        private static DbEntityException _entityException;
        private int _position;   
         
        #endregion "Private Data Members"               
      
        #region "Constructor"

        public Tbl78NamesViewModel()
        {
            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.
            }
            else
            {    
    
                GetValueLanguage();
                _entityException = new DbEntityException();
            }
        }
        #endregion "Constructor"          
 

 //    Part 1    

             
        #region "Public Commands Basic Tbl78Name"
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
             
        private RelayCommand _deleteNameCommand;

        public ICommand DeleteNameCommand => _deleteNameCommand ??
                                                   (_deleteNameCommand = new RelayCommand(delegate { DeleteName(null); }));    
             
        private RelayCommand _saveNameCommand;

        public ICommand SaveNameCommand => _saveNameCommand ??
                                                 (_saveNameCommand = new RelayCommand(delegate { SaveName(null); }));
        //-------------------------------------------------------------------------          
        
        private void ClearName(object o)
        {
            SearchNameName = "";

            SelectedMainTabIndex = 0;  //change tab
            SelectedDetailTabIndex = 0;
            SelectedDetailSubTabIndex = 0;

            Tbl78NamesList?.Clear();
            Tbl69FiSpeciessesList?.Clear();
            Tbl72PlSpeciessesList?.Clear();
        }
        //----------------------------------------------------------------------                  
        
        private void GetNamesByNameOrId(object o)
        {
            if (SearchNameName != "")
            {
                Tbl78NamesList?.Clear();
                if (SearchNameName == "*") // show whole table
                {
                    SearchNameName = "";
                    _businessLayer = new BusinessLayer.BusinessLayer();
                        Tbl69FiSpeciessesAllList = new ObservableCollection<Tbl69FiSpecies>(_businessLayer.ListTbl69FiSpeciesses());
                        Tbl72PlSpeciessesAllList = new ObservableCollection<Tbl72PlSpecies>(_businessLayer.ListTbl72PlSpeciesses());
                    Tbl78NamesList = new ObservableCollection<Tbl78Name>(_businessLayer.ListTbl78NamesByNameName(SearchNameName));
                    SearchNameName = "*";
                }
                else
                {
                    _businessLayer = new BusinessLayer.BusinessLayer();
                        Tbl69FiSpeciessesAllList = new ObservableCollection<Tbl69FiSpecies>(_businessLayer.ListTbl69FiSpeciesses());
                        Tbl72PlSpeciessesAllList = new ObservableCollection<Tbl72PlSpecies>(_businessLayer.ListTbl72PlSpeciesses());
                    Tbl78NamesList = int.TryParse(SearchNameName, out var id) ?
                        new ObservableCollection<Tbl78Name>(_businessLayer.ListTbl78NamesByNameId(id)) :
                        new ObservableCollection<Tbl78Name>(_businessLayer.ListTbl78NamesByNameName(SearchNameName));
                }

                if (Tbl78NamesList.Count == 0)
                {
                    WpfMessageBox.Show(CultRes.StringsRes.Tables, CultRes.StringsRes.DatasetNot,
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                }
                else
                {
                    Tbl69FiSpeciessesList?.Clear();
                    Tbl72PlSpeciessesList?.Clear();
                }
            }
            else
            {
                WpfMessageBox.Show(CultRes.StringsRes.SearchNameOrId, CultRes.StringsRes.InputRequested,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
            }

            NamesView = CollectionViewSource.GetDefaultView(Tbl78NamesList);
            NamesView.Refresh();
        }
        //------------------------------------------------------------------------------------                          
        
        private void AddName(object o)
        {
            if (Tbl78NamesList == null)
                Tbl78NamesList =  new ObservableCollection<Tbl78Name>( );

            Tbl78NamesList.Insert(0, new Tbl78Name   {   NameName = CultRes.StringsRes.DatasetNew  }  );

                    _businessLayer = new BusinessLayer.BusinessLayer();
                Tbl69FiSpeciessesAllList = new ObservableCollection<Tbl69FiSpecies>(_businessLayer.ListTbl69FiSpeciesses());
                Tbl72PlSpeciessesAllList = new ObservableCollection<Tbl72PlSpecies>(_businessLayer.ListTbl72PlSpeciesses());

            NamesView = CollectionViewSource.GetDefaultView(Tbl78NamesList);
            NamesView.MoveCurrentToFirst();
        }
        //------------------------------------------------------------------------------------                               
        
        private void CopyName(object o)
        {
            if (CurrentTbl78Name == null)
            {
                WpfMessageBox.Show(CultRes.StringsRes.DatasetNew,
                    CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                return;
            }
            _businessLayer = new BusinessLayer.BusinessLayer();

            var name = _businessLayer.SingleListTbl78NamesByNameId(CurrentTbl78Name.NameID);

            Tbl78NamesList.Insert(0, new Tbl78Name
            {
                 FiSpeciesID =  name. FiSpeciesID,
                 PlSpeciesID =  name. PlSpeciesID,
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
        //---------------------------------------------------------------------------------------                            
        
        private void DeleteName(object o)
        {
            if (CurrentTbl78Name == null)
            {
                WpfMessageBox.Show(CultRes.StringsRes.DatasetNew,
                    CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                return;
            }
            _businessLayer = new BusinessLayer.BusinessLayer();

            try
            {
                var name = _businessLayer.SingleListTbl78NamesByNameId(CurrentTbl78Name.NameID);
                if (name != null)
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
            }

            if (SearchNameName != "")
            {
                if (SearchNameName == "*")  //show all datasets
                {
                    SearchNameName = "";
                    Tbl78NamesList.Clear();
                    
                Tbl78NamesList = new ObservableCollection<Tbl78Name>(_businessLayer.ListTbl78NamesByNameName(SearchNameName));            
                    SearchNameName = "*";
                }
                else
                {               
                    Tbl78NamesList =  new ObservableCollection<Tbl78Name>(_businessLayer.ListTbl78NamesByNameName(SearchNameName));

                }
                NamesView = CollectionViewSource.GetDefaultView(Tbl78NamesList);
                NamesView.Refresh();
            }
            else  //SearchName = empty
            {
                Tbl78NamesList = new ObservableCollection<Tbl78Name>(_businessLayer.ListTbl78NamesByNameName(SearchNameName));

                NamesView = CollectionViewSource.GetDefaultView(Tbl78NamesList);
                NamesView.MoveCurrentToFirst();
             }
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
            _businessLayer = new BusinessLayer.BusinessLayer();

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
                        name.Language  = CurrentTbl78Name.Language ;
                        name.Info = CurrentTbl78Name.Info;
                        name.Updater = Environment.UserName;
                        name.UpdaterDate = DateTime.Now;
                        name.Memo = CurrentTbl78Name.Memo;
                        name.EntityState = EntityState.Modified;
                    }
                }
                else
                {
                    name = new Tbl78Name   //add new
                    {
                        NameName = CurrentTbl78Name.NameName,
                        FiSpeciesID = CurrentTbl78Name.FiSpeciesID,
                        PlSpeciesID = CurrentTbl78Name.PlSpeciesID,

                        CountID = RandomHelper.Randomnumber(),
                        Valid = CurrentTbl78Name.Valid,
                        ValidYear = CurrentTbl78Name.ValidYear,
                        Language  = CurrentTbl78Name.Language,
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
                    //FiSpeciesID && PlSpeciesID  may be not 0
                    if (CurrentTbl78Name.FiSpeciesID == 0 && CurrentTbl78Name.PlSpeciesID == 0)          

                    {
                        WpfMessageBox.Show(CultRes.StringsRes.RequiredGenealogyConnect, CultRes.StringsRes.RequiredInput,
                            MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        return;
                    }

                    //check if dataset with Name and FiSpeciesId already exist       
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
                                _businessLayer.UpdateName(name);
                                _position = NamesView.CurrentPosition;

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
            }

            if (SearchNameName != "")
            {
                if (SearchNameName == "*")  //show all datasets
                {
                    SearchNameName = "";
                    Tbl78NamesList.Clear();
                    
                Tbl78NamesList = new ObservableCollection<Tbl78Name>(_businessLayer.ListTbl78NamesByNameName(SearchNameName));            
                    SearchNameName = "*";
                }
                else
                {               
                    Tbl78NamesList = int.TryParse(SearchNameName, out var id)
                        ? new ObservableCollection<Tbl78Name>(_businessLayer.ListTbl78NamesByNameId(id))
                        : new ObservableCollection<Tbl78Name>(_businessLayer.ListTbl78NamesByNameName(SearchNameName));

                }
                NamesView = CollectionViewSource.GetDefaultView(Tbl78NamesList);
                NamesView.MoveCurrentToPosition(_position);
            }
            else  
            {
                Tbl78NamesList = new ObservableCollection<Tbl78Name>(_businessLayer.ListTbl78NamesByNameName(CurrentTbl78Name.NameName));

                NamesView= CollectionViewSource.GetDefaultView(Tbl78NamesList);
                NamesView.Refresh();
            }
        }
        #endregion "Public Commands"                   
 
 

 //    Part 2    

           
        #region "Public Commands Connect <== Tbl69FiSpecies"                 
        //-------------------------------------------------------------------------

        private RelayCommand _saveFiSpeciesCommand;

        public ICommand SaveFiSpeciesCommand => _saveFiSpeciesCommand ??
                                                 (_saveFiSpeciesCommand = new RelayCommand(delegate { SaveFiSpecies(null); }));

        //-------------------------------------------------------------------------          
       
        private void SaveFiSpecies(object o)
        {
            if (CurrentTbl69FiSpecies == null)
            {
                WpfMessageBox.Show(CultRes.StringsRes.DatasetNew,
                    CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                return;
            }

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
                        fispecies.EntityState = EntityState.Modified;
                    }
                }
                else
                {
                    fispecies = new Tbl69FiSpecies   //add new
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
                                _businessLayer.UpdateFiSpecies(fispecies);

                                    WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess,
                                        CurrentTbl69FiSpecies.FiSpeciesID == 0
                                            ? CultRes.StringsRes.DatasetNew
                                            : CurrentTbl69FiSpecies.FiSpeciesName + " " + CurrentTbl69FiSpecies.Subspecies + " " + CurrentTbl69FiSpecies.Divers,
                                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        }
                    }
                }
            }
            catch (DbEntityValidationException ex)
            {
                _entityException.EntityException(ex);
            }

                Tbl69FiSpeciessesList = new ObservableCollection<Tbl69FiSpecies>(_businessLayer.ListTbl69FiSpeciessesByFiSpeciesId(CurrentTbl78Name.FiSpeciesID));            

            SelectedMainTabIndex = 0;
            FiSpeciessesView = CollectionViewSource.GetDefaultView(Tbl69FiSpeciessesList);
            FiSpeciessesView.Refresh();
        }
        #endregion "Public Commands"                        
                                                          

 //    Part 3    

           
        #region "Public Commands Connect <== Tbl72PlSpecies"                 
        //-------------------------------------------------------------------------
        private RelayCommand _savePlSpeciesCommand;

        public ICommand SavePlSpeciesCommand => _savePlSpeciesCommand ??
                                                 (_savePlSpeciesCommand = new RelayCommand(delegate { SavePlSpecies(null); }));

        //-------------------------------------------------------------------------          
       
        private void SavePlSpecies(object o)
        {           
             if (CurrentTbl69FiSpecies == null)
            {
                WpfMessageBox.Show(CultRes.StringsRes.DatasetNew,
                    CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                return;
            }

            try
            {
                var plspecies = _businessLayer.SingleListTbl72PlSpeciessesByPlSpeciesId(CurrentTbl72PlSpecies.PlSpeciesID);
                if (CurrentTbl72PlSpecies.PlSpeciesID != 0)
                {
                    if (plspecies != null) //update
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
                            plspecies.EntityState = EntityState.Modified;
                        }
                    }
                    else
                    {
                        plspecies = new Tbl72PlSpecies   //add new
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
                            UpdaterDate = DateTime.Now,
                            EntityState = EntityState.Added
                        };
                    }
                    {   
                    //GenusID and SpeciesgroupID may be not 0
                    if (CurrentTbl72PlSpecies.GenusID == 0 || CurrentTbl72PlSpecies.SpeciesgroupID == 0)

                    {
                        WpfMessageBox.Show(CultRes.StringsRes.RequiredGenealogyConnect, CultRes.StringsRes.RequiredInput,
                            MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        return;
                    }

                    //check if dataset with Name and GenusId and SpeciesgroupId already exist       
                    var dataset = _businessLayer.ListTbl72PlSpeciessesByPlSpeciesNameAndSubspeciesAndDiversAndGenusIdAndSpeciesgroupId(CurrentTbl72PlSpecies.PlSpeciesName, CurrentTbl72PlSpecies.Subspecies, CurrentTbl72PlSpecies.Divers, CurrentTbl72PlSpecies.GenusID, CurrentTbl72PlSpecies.SpeciesgroupID);

                    if (dataset.Count != 0 && CurrentTbl72PlSpecies.PlSpeciesID == 0)  //dataset exist
                    {
                        WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTbl72PlSpecies.PlSpeciesName + " " + CurrentTbl72PlSpecies.Subspecies + " " + CurrentTbl72PlSpecies.Divers,
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        return;
                    }
                    if (dataset.Count == 0 && CurrentTbl72PlSpecies.PlSpeciesID == 0 ||
                        dataset.Count != 0 && CurrentTbl72PlSpecies.PlSpeciesID != 0 ||
                        dataset.Count == 0 && CurrentTbl72PlSpecies.PlSpeciesID != 0) //new dataset and update
                    {
                        if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTbl72PlSpecies.PlSpeciesName + " " + CurrentTbl72PlSpecies.Subspecies + " " + CurrentTbl72PlSpecies.Divers,
                                MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes)
                            return;
                        {
                                _businessLayer.UpdatePlSpecies(plspecies);

                                    WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess,
                                        CurrentTbl72PlSpecies.PlSpeciesID == 0
                                            ? CultRes.StringsRes.DatasetNew
                                            : CurrentTbl72PlSpecies.PlSpeciesName + " " + CurrentTbl72PlSpecies.Subspecies + " " + CurrentTbl72PlSpecies.Divers,
                                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        }
                    }
                }
            }
            catch (DbEntityValidationException ex)
            {
                _entityException.EntityException(ex);
            }

                Tbl72PlSpeciessesList = new ObservableCollection<Tbl72PlSpecies>(_businessLayer.ListTbl72PlSpeciessesByPlSpeciesId(CurrentTbl78Name.PlSpeciesID));            

            SelectedMainTabIndex = 1;
            PlSpeciessesView = CollectionViewSource.GetDefaultView(Tbl72PlSpeciessesList);
            PlSpeciessesView.Refresh();
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
        public  ICommand GetConnectedTablesCommand => _getConnectedTablesCommand ??
                                                         (_getConnectedTablesCommand = new RelayCommand(delegate { GetConnectedTablesById(null); }));

        private void GetConnectedTablesById(object o)
        {
            SelectedMainTabIndex = 0;  //change to Connect tab
            SelectedDetailTabIndex = 1;
            SelectedDetailSubTabIndex = 0;

            _businessLayer = new BusinessLayer.BusinessLayer();
            Tbl68SpeciesgroupsAllList = new ObservableCollection<Tbl68Speciesgroup>(_businessLayer.ListTbl68Speciesgroups());
            Tbl66GenussesAllList = new ObservableCollection<Tbl66Genus>( _businessLayer.ListTbl66Genusses());

            Tbl69FiSpeciessesList = new ObservableCollection<Tbl69FiSpecies>(
                _businessLayer.ListTbl69FiSpeciessesByFiSpeciesId(CurrentTbl78Name.FiSpeciesID)); 

            FiSpeciessesView = CollectionViewSource.GetDefaultView(Tbl69FiSpeciessesList);
            FiSpeciessesView.Refresh();
        }

        #endregion "Public Commands Connected Tables by DoubleClick"     
 

 //    Part 10    

     
        #region "Public Commands to open Detail TabItems"

        private int _selectedMainTabIndex;
        private int _selectedDetailTabIndex;
        private int _selectedDetailSubTabIndex;
        private int _selectedDetailSubFiSpeciesTabIndex;
        private int _selectedDetailSubPlSpeciesTabIndex;

        public  int SelectedMainTabIndex
        {
            get => _selectedMainTabIndex;
            set
            {
                if (value == _selectedMainTabIndex) return;
                _selectedMainTabIndex = value;
                RaisePropertyChanged();
                if (_selectedMainTabIndex == 0)
                {
                    SelectedDetailTabIndex = 1;
                    SelectedDetailSubTabIndex = 0;
                }
                if (_selectedMainTabIndex == 1)
                {
                    SelectedDetailTabIndex = 1;
                    SelectedDetailSubTabIndex = 1;
                }
            }
        }


        public  int SelectedDetailTabIndex
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
                    SelectedDetailSubTabIndex = 0;
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
                    SelectedMainTabIndex = 0;
                }
                if (_selectedDetailSubTabIndex == 1)
                {
                    Tbl68SpeciesgroupsAllList = new ObservableCollection<Tbl68Speciesgroup>(
                        _businessLayer.ListTbl68Speciesgroups());

                    Tbl66GenussesAllList = new ObservableCollection<Tbl66Genus>(
                        _businessLayer.ListTbl66Genusses());

                    Tbl72PlSpeciessesList = new ObservableCollection<Tbl72PlSpecies>(
                        _businessLayer.ListTbl72PlSpeciessesByPlSpeciesId(CurrentTbl78Name.PlSpeciesID));

                    PlSpeciessesView = CollectionViewSource.GetDefaultView(Tbl72PlSpeciessesList);
                    PlSpeciessesView.Refresh();

                    SelectedMainTabIndex = 1;
                }
            }
        }

        public int SelectedDetailSubFiSpeciesTabIndex
        {
            get => _selectedDetailSubFiSpeciesTabIndex;
            set { _selectedDetailSubFiSpeciesTabIndex = value; RaisePropertyChanged(); }
        }

        public int SelectedDetailSubPlSpeciesTabIndex
        {
            get => _selectedDetailSubPlSpeciesTabIndex;
            set { _selectedDetailSubPlSpeciesTabIndex = value; RaisePropertyChanged(); }
        }

        #endregion "Public Commands to open Detail TabItems"
 

 //    Part 11    

     
        #region "Public Properties Tbl78Name"

        private string _searchNameName = "";
        public string SearchNameName
        {
            get => _searchNameName; 
            set { _searchNameName = value; RaisePropertyChanged();  }
        }

        public  ICollectionView NamesView;
        private   Tbl78Name CurrentTbl78Name => NamesView?.CurrentItem as Tbl78Name;

        private ObservableCollection<Tbl78Name> _tbl78NamesList;
        public  ObservableCollection<Tbl78Name> Tbl78NamesList
        {
            get => _tbl78NamesList; 
            set {  _tbl78NamesList = value; RaisePropertyChanged();   }
        }

        private ObservableCollection<Tbl78Name> _tbl78NamesAllList;
        public  ObservableCollection<Tbl78Name> Tbl78NamesAllList
        {
            get => _tbl78NamesAllList; 
            set {  _tbl78NamesAllList = value; RaisePropertyChanged();   }
        }

        #endregion "Public Properties"   
       
        #region "Public Properties Tbl69FiSpecies"

        public  ICollectionView FiSpeciessesView;
        private Tbl69FiSpecies CurrentTbl69FiSpecies => FiSpeciessesView?.CurrentItem as Tbl69FiSpecies;           

        private ObservableCollection<Tbl69FiSpecies> _tbl69FiSpeciessesList;
        public  ObservableCollection<Tbl69FiSpecies> Tbl69FiSpeciessesList
        {
            get => _tbl69FiSpeciessesList; 
            set { _tbl69FiSpeciessesList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl69FiSpecies> _tbl69FiSpeciessesAllList;
        public  ObservableCollection<Tbl69FiSpecies> Tbl69FiSpeciessesAllList
        {
            get => _tbl69FiSpeciessesAllList; 
            set { _tbl69FiSpeciessesAllList = value; RaisePropertyChanged(); }       
        }

        #endregion "Public Properties"   
  
       
        #region "Public Properties Tbl72PlSpecies"

        public  ICollectionView PlSpeciessesView;
        private  Tbl72PlSpecies CurrentTbl72PlSpecies => PlSpeciessesView?.CurrentItem as Tbl72PlSpecies;           

        private ObservableCollection<Tbl72PlSpecies> _tbl72PlSpeciessesList;
        public   ObservableCollection<Tbl72PlSpecies> Tbl72PlSpeciessesList
        {
            get => _tbl72PlSpeciessesList; 
            set { _tbl72PlSpeciessesList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl72PlSpecies> _tbl72PlSpeciessesAllList;
        public  ObservableCollection<Tbl72PlSpecies> Tbl72PlSpeciessesAllList
        {
            get => _tbl72PlSpeciessesAllList; 
            set { _tbl72PlSpeciessesAllList = value; RaisePropertyChanged(); }       
        }

        #endregion "Public Properties"   
          
        #region "Public Properties Tbl68Speciesgroup"

        private ObservableCollection<Tbl68Speciesgroup> _tbl68SpeciesgroupsAllList;
        public ObservableCollection<Tbl68Speciesgroup> Tbl68SpeciesgroupsAllList
        {
            get => _tbl68SpeciesgroupsAllList;
            set { _tbl68SpeciesgroupsAllList = value; RaisePropertyChanged(); }
        }
        #endregion "Public Properties Tbl68Speciesgroup"

        #region "Public Properties Tbl66Genus"

        private ObservableCollection<Tbl66Genus> _tbl66GenussesAllList;
        public ObservableCollection<Tbl66Genus> Tbl66GenussesAllList
        {
            get => _tbl66GenussesAllList;
            set { _tbl66GenussesAllList = value; RaisePropertyChanged(); }
        }
        #endregion "Public Properties Tbl66Genus"

        #region Language

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

        #endregion Language  
 

 



   }
}   
