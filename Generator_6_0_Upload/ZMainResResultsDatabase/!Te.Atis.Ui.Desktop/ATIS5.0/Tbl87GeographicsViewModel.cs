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

    
using Microsoft.Win32;
using System.Globalization;
using System.Collections.Generic;
using System.Windows.Media.Imaging;    
    
         //    Tbl87GeographicsViewModel Skriptdatum:  13.11.2018  10:32    

namespace Te.Atis.Ui.Desktop.Views.Database
{     
    
    public class Tbl87GeographicsViewModel : ViewModelBase                     
    {     
         
        #region "Private Data Members"
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private static IBusinessLayer _businessLayer;
        private static DbEntityException _entityException;
        private int _position;   
         
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
                _entityException = new DbEntityException();

                TblCountriesList = new ObservableCollection<TblCountry>(_businessLayer.ListTblCountries());
            }
        }
        private new bool IsInDesignMode { get; set; }

        #endregion "Constructor"                       
 

 //    Part 1    

             
        #region "Public Commands Basic Tbl87Geographic"
        //-------------------------------------------------------------------------
        private RelayCommand _clearGeographicCommand;

        public ICommand ClearGeographicCommand => _clearGeographicCommand ??
                                                  (_clearGeographicCommand = new RelayCommand(delegate { ClearGeographic(null); }));         
    
        private RelayCommand _getGeographicsByIdCommand;     

        public  ICommand GetGeographicsByIdCommand => _getGeographicsByIdCommand ??
                                                           (_getGeographicsByIdCommand = new RelayCommand(delegate { GetGeographicsById(null); }));        
             
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
        
        private void ClearGeographic(object o)
        {
            SearchGeographicId = 0;

            SelectedMainTabIndex = 0;  //change tab
            SelectedDetailTabIndex = 0;
            SelectedDetailSubTabIndex = 0;

            Tbl87GeographicsList?.Clear();
            Tbl69FiSpeciessesList?.Clear();
            Tbl72PlSpeciessesList?.Clear();
        }
        //----------------------------------------------------------------------                  
        
        private void GetGeographicsById(object o)
        {
            if (SearchGeographicId != 0)
            {
                Tbl87GeographicsList?.Clear();

                Tbl69FiSpeciessesList?.Clear();
                Tbl72PlSpeciessesList?.Clear();

                 if(int.TryParse(SearchGeographicId.ToString(CultureInfo.InvariantCulture), out var id))

                    _businessLayer = new BusinessLayer.BusinessLayer();
                    Tbl69FiSpeciessesAllList = new ObservableCollection<Tbl69FiSpecies>(_businessLayer.ListTbl69FiSpeciesses());
                    Tbl72PlSpeciessesAllList = new ObservableCollection<Tbl72PlSpecies>(_businessLayer.ListTbl72PlSpeciesses());
                    Tbl87GeographicsList = new ObservableCollection<Tbl87Geographic>(_businessLayer.ListTbl87GeographicsByGeographicId(id));

                if (Tbl87GeographicsList.Count == 0)
                {
                    WpfMessageBox.Show(CultRes.StringsRes.Tables, CultRes.StringsRes.DatasetNot,
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                }
            }
            else
            {
                WpfMessageBox.Show(CultRes.StringsRes.SearchNameOrId, CultRes.StringsRes.InputRequested,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
            }
            GeographicsView = CollectionViewSource.GetDefaultView(Tbl87GeographicsList);
            GeographicsView.Refresh();
        }
        //------------------------------------------------------------------------------------                          
        
        private void AddGeographic(object o)
        {
            if (Tbl87GeographicsList == null)
                Tbl87GeographicsList =  new ObservableCollection<Tbl87Geographic>( );

            Tbl87GeographicsList.Insert(0, new Tbl87Geographic   {   Info = CultRes.StringsRes.DatasetNew  }  );

                    _businessLayer = new BusinessLayer.BusinessLayer();
                Tbl69FiSpeciessesAllList = new ObservableCollection<Tbl69FiSpecies>(_businessLayer.ListTbl69FiSpeciesses());
                Tbl72PlSpeciessesAllList = new ObservableCollection<Tbl72PlSpecies>(_businessLayer.ListTbl72PlSpeciesses());

            GeographicsView = CollectionViewSource.GetDefaultView(Tbl87GeographicsList);
            GeographicsView.MoveCurrentToFirst();
        }
        //------------------------------------------------------------------------------------                               
        
        private void CopyGeographic(object o)
        {
            if (CurrentTbl87Geographic == null)
            {
                WpfMessageBox.Show(CultRes.StringsRes.DatasetNew,
                    CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                return;
            }
            _businessLayer = new BusinessLayer.BusinessLayer();

            var geographic = _businessLayer.SingleListTbl87GeographicsByGeographicId(CurrentTbl87Geographic.GeographicID);

            Tbl87GeographicsList.Insert(0, new Tbl87Geographic
            {
                 FiSpeciesID =  geographic. FiSpeciesID,
                 PlSpeciesID =  geographic. PlSpeciesID,
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
        //---------------------------------------------------------------------------------------                            
        
        private void DeleteGeographic(object o)
        {
            if (CurrentTbl87Geographic == null)
            {
                WpfMessageBox.Show(CultRes.StringsRes.DatasetNew,
                    CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                return;
            }
            _businessLayer = new BusinessLayer.BusinessLayer();

            try
            {
                var geographic = _businessLayer.SingleListTbl87GeographicsByGeographicId(CurrentTbl87Geographic.GeographicID);
                if (geographic != null)
                {
                    if (WpfMessageBox.Show(CultRes.StringsRes.DeleteQuestion1, CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl87Geographic.GeographicID.ToString(),
                            MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes)
                        return;
                    geographic.EntityState = EntityState.Deleted;
                    _businessLayer.RemoveGeographic(geographic);

                    WpfMessageBox.Show(CultRes.StringsRes.DeleteSuccess, CurrentTbl87Geographic.GeographicID.ToString(),
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                }
                else
                {
                    WpfMessageBox.Show(CultRes.StringsRes.Information, CultRes.StringsRes.DeleteCan + " " + CurrentTbl87Geographic.GeographicID.ToString() + " " + CultRes.StringsRes.DeleteCan1,
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                }
            }
            catch (DbEntityValidationException ex)
            {
                _entityException.EntityException(ex);
            }

            Tbl87GeographicsList = new ObservableCollection<Tbl87Geographic>(_businessLayer.ListTbl87GeographicsByGeographicId(SearchGeographicId));

            GeographicsView = CollectionViewSource.GetDefaultView(Tbl87GeographicsList);
            GeographicsView.MoveCurrentToFirst();            
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
            _businessLayer = new BusinessLayer.BusinessLayer();

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
                        geographic.Author = CurrentTbl87Geographic.Author;
                        geographic.AuthorYear = CurrentTbl87Geographic.AuthorYear;
                        geographic.Info = CurrentTbl87Geographic.Info;
                        geographic.Memo = CurrentTbl87Geographic.Memo;
                        geographic.Updater = Environment.UserName;
                        geographic.UpdaterDate = DateTime.Now;
                        geographic.EntityState = EntityState.Modified;
                    }
                }
                else
                {
                            geographic = new Tbl87Geographic   //add new
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
                                Author = CurrentTbl87Geographic.Author,
                                AuthorYear = CurrentTbl87Geographic.AuthorYear,
                                Info = CurrentTbl87Geographic.Info,
                                Memo = CurrentTbl87Geographic.Memo,
                                Writer = Environment.UserName,
                                WriterDate = DateTime.Now,
                                Updater = Environment.UserName,
                                UpdaterDate = DateTime.Now,
                        EntityState = EntityState.Added
                    };
                }
                {
                    //FiSpeciesID && PlSpeciesID  may be not 0
                    if (CurrentTbl87Geographic.FiSpeciesID == 0 && CurrentTbl87Geographic.PlSpeciesID == 0)          

                    {
                        WpfMessageBox.Show(CultRes.StringsRes.RequiredGenealogyConnect, CultRes.StringsRes.RequiredInput,
                            MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        return;
                    }
                    //check if dataset with ImageId and FiSpeciesId and PlSpeciesId  already exist       
                    var dataset = _businessLayer.ListTbl87GeographicsByGeographicIdAndFiSpeciesIdAndPlSpeciesId(CurrentTbl87Geographic.GeographicID, CurrentTbl87Geographic.FiSpeciesID, CurrentTbl87Geographic.PlSpeciesID);

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
                                _businessLayer.UpdateGeographic(geographic);
                                _position = GeographicsView.CurrentPosition;

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
            }

            if (CurrentTbl87Geographic.GeographicID == 0)
                Tbl87GeographicsList = new ObservableCollection<Tbl87Geographic>(
                    _businessLayer.ListTbl87GeographicsByGeographicId(CurrentTbl87Geographic.GeographicID));
            else
                Tbl87GeographicsList = new ObservableCollection<Tbl87Geographic>(
                        _businessLayer.ListTbl87GeographicsByGeographicId(SearchGeographicId));

            GeographicsView = CollectionViewSource.GetDefaultView(Tbl87GeographicsList);
            GeographicsView.Refresh();
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

                Tbl69FiSpeciessesList = new ObservableCollection<Tbl69FiSpecies>(_businessLayer.ListTbl69FiSpeciessesByFiSpeciesId(CurrentTbl87Geographic.FiSpeciesID));            

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

                Tbl72PlSpeciessesList = new ObservableCollection<Tbl72PlSpecies>(_businessLayer.ListTbl72PlSpeciessesByPlSpeciesId(CurrentTbl87Geographic.PlSpeciesID));            

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
            SelectedDetailTabIndex = 3;
            SelectedDetailSubTabIndex = 0;

            _businessLayer = new BusinessLayer.BusinessLayer();
            Tbl68SpeciesgroupsAllList = new ObservableCollection<Tbl68Speciesgroup>(_businessLayer.ListTbl68Speciesgroups());
            Tbl66GenussesAllList = new ObservableCollection<Tbl66Genus>(_businessLayer.ListTbl66Genusses());

            Tbl69FiSpeciessesList = new ObservableCollection<Tbl69FiSpecies>(
                _businessLayer.ListTbl69FiSpeciessesByFiSpeciesId(CurrentTbl87Geographic.FiSpeciesID));

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
                    Tbl68SpeciesgroupsAllList = new ObservableCollection<Tbl68Speciesgroup>(
                        _businessLayer.ListTbl68Speciesgroups());

                    Tbl66GenussesAllList = new ObservableCollection<Tbl66Genus>(
                        _businessLayer.ListTbl66Genusses());

                    Tbl69FiSpeciessesList = new ObservableCollection<Tbl69FiSpecies>(
                        _businessLayer.ListTbl69FiSpeciessesByFiSpeciesId(CurrentTbl87Geographic.FiSpeciesID));

                    FiSpeciessesView = CollectionViewSource.GetDefaultView(Tbl69FiSpeciessesList);
                    FiSpeciessesView.Refresh();

                    SelectedDetailTabIndex = 3;
                    SelectedMainTabIndex = 0;
                }
                if (_selectedDetailSubTabIndex == 1)
                {
                    Tbl68SpeciesgroupsAllList = new ObservableCollection<Tbl68Speciesgroup>(
                        _businessLayer.ListTbl68Speciesgroups());

                    Tbl66GenussesAllList = new ObservableCollection<Tbl66Genus>(
                        _businessLayer.ListTbl66Genusses());

                    Tbl72PlSpeciessesList = new ObservableCollection<Tbl72PlSpecies>(
                        _businessLayer.ListTbl72PlSpeciessesByPlSpeciesId(CurrentTbl87Geographic.PlSpeciesID));

                    PlSpeciessesView = CollectionViewSource.GetDefaultView(Tbl72PlSpeciessesList);
                    PlSpeciessesView.Refresh();

                   SelectedDetailTabIndex = 3;
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

      
        #region "Public Properties Tbl87Geographic"

        private int _searchGeographicId = 0;
        public int SearchGeographicId
        {
            get => _searchGeographicId ;
            set { _searchGeographicId = value; RaisePropertyChanged(); }
        }

        public  ICollectionView GeographicsView;
        private   Tbl87Geographic CurrentTbl87Geographic => GeographicsView?.CurrentItem as Tbl87Geographic;

        private ObservableCollection<Tbl87Geographic> _tbl87GeographicsList;
        public  ObservableCollection<Tbl87Geographic> Tbl87GeographicsList
        {
            get => _tbl87GeographicsList; 
            set {  _tbl87GeographicsList = value; RaisePropertyChanged();   }
        }

        private ObservableCollection<Tbl87Geographic> _tbl87GeographicsAllList;
        public  ObservableCollection<Tbl87Geographic> Tbl87GeographicsAllList
        {
            get => _tbl87GeographicsAllList; 
            set { _tbl87GeographicsAllList = value; RaisePropertyChanged(); }
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

        #region "Private Continent, Country"

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
