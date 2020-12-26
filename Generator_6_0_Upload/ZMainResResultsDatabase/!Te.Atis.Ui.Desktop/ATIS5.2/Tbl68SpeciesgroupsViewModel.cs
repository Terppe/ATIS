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

    
         //    Tbl68SpeciesgroupsViewModel Skriptdatum:  09.11.2018  10:32    

namespace Te.Atis.Ui.Desktop.Views.Database
{     
    
    public class Tbl68SpeciesgroupsViewModel : ViewModelBase                     
    {     
         
        #region "Private Data Members"
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static IBusinessLayer _businessLayer;
        private static DbEntityException _entityException;
        private int _position;   
         
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
                _entityException = new DbEntityException();
            }
        }     
        #endregion "Constructor"         
 

 //    Part 1    

             
        #region "Public Commands Basic Tbl68Speciesgroup"
        //-------------------------------------------------------------------------
        private RelayCommand _clearSpeciesgroupCommand;

        public ICommand ClearSpeciesgroupCommand => _clearSpeciesgroupCommand ??
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
             
        private RelayCommand _deleteSpeciesgroupCommand;

        public ICommand DeleteSpeciesgroupCommand => _deleteSpeciesgroupCommand ??
                                                   (_deleteSpeciesgroupCommand = new RelayCommand(delegate { DeleteSpeciesgroup(null); }));    
             
        private RelayCommand _saveSpeciesgroupCommand;

        public ICommand SaveSpeciesgroupCommand => _saveSpeciesgroupCommand ??
                                                 (_saveSpeciesgroupCommand = new RelayCommand(delegate { SaveSpeciesgroup(null); }));
        //-------------------------------------------------------------------------          
        
        private void ClearSpeciesgroup(object o)
        {
            SearchSpeciesgroupName = "";

            SelectedMainTabIndex = 0;  //change tab
            SelectedDetailTabIndex = 0;
            SelectedDetailSubTabIndex = 0;

            Tbl68SpeciesgroupsList?.Clear();
            Tbl69FiSpeciessesList?.Clear();
            Tbl72PlSpeciessesList?.Clear();
        }
        //----------------------------------------------------------------------                  
        
        private void GetSpeciesgroupsByNameOrId(object o)
        {
            if (SearchSpeciesgroupName != "")
            {
                Tbl68SpeciesgroupsList?.Clear();
                if (SearchSpeciesgroupName == "*") // show whole table
                {
                    SearchSpeciesgroupName = "";
                    _businessLayer = new BusinessLayer.BusinessLayer();
                        Tbl66GenussesAllList = new ObservableCollection<Tbl66Genus>(_businessLayer.ListTbl66Genusses());
                    Tbl68SpeciesgroupsList = new ObservableCollection<Tbl68Speciesgroup>(_businessLayer.ListTbl68SpeciesgroupsBySpeciesgroupName(SearchSpeciesgroupName));
                    SearchSpeciesgroupName = "*";
                }
                else
                {
                    _businessLayer = new BusinessLayer.BusinessLayer();
                        Tbl66GenussesAllList = new ObservableCollection<Tbl66Genus>(_businessLayer.ListTbl66Genusses());
                    Tbl68SpeciesgroupsList = int.TryParse(SearchSpeciesgroupName, out var id) ?
                        new ObservableCollection<Tbl68Speciesgroup>(_businessLayer.ListTbl68SpeciesgroupsBySpeciesgroupId(id)) :
                        new ObservableCollection<Tbl68Speciesgroup>(_businessLayer.ListTbl68SpeciesgroupsBySpeciesgroupName(SearchSpeciesgroupName));
                }

                if (Tbl68SpeciesgroupsList.Count == 0)
                {
                    WpfMessageBox.Show(CultRes.StringsRes.Tables, CultRes.StringsRes.DatasetNot,
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                }
                else
                {
                    Tbl66GenussesList?.Clear();
                    Tbl69FiSpeciessesList?.Clear();
                    Tbl72PlSpeciessesList?.Clear();
                }
            }
            else
            {
                WpfMessageBox.Show(CultRes.StringsRes.SearchNameOrId, CultRes.StringsRes.InputRequested,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
            }

            SpeciesgroupsView = CollectionViewSource.GetDefaultView(Tbl68SpeciesgroupsList);
            SpeciesgroupsView.Refresh();
        }
        //------------------------------------------------------------------------------------                          
        
        private void AddSpeciesgroup(object o)
        {  
             if (Tbl68SpeciesgroupsList == null)
                Tbl68SpeciesgroupsList =  new ObservableCollection<Tbl68Speciesgroup>( );

            Tbl68SpeciesgroupsList.Insert(0, new Tbl68Speciesgroup {   SpeciesgroupName = CultRes.StringsRes.DatasetNew}  );

                    _businessLayer = new BusinessLayer.BusinessLayer();
                Tbl66GenussesAllList = new ObservableCollection<Tbl66Genus>(_businessLayer.ListTbl66Genusses());

            SpeciesgroupsView = CollectionViewSource.GetDefaultView(Tbl68SpeciesgroupsList);
            SpeciesgroupsView.MoveCurrentToFirst();
        }
        //------------------------------------------------------------------------------------                               
        
        private void CopySpeciesgroup(object o)
        {
            if (CurrentTbl68Speciesgroup == null)
            {
                WpfMessageBox.Show(CultRes.StringsRes.DatasetNew,
                    CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                return;
            }
            _businessLayer = new BusinessLayer.BusinessLayer();

            var speciesgroup = _businessLayer.SingleListTbl68SpeciesgroupsBySpeciesgroupId(CurrentTbl68Speciesgroup.SpeciesgroupID);

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
            SpeciesgroupsView.MoveCurrentToFirst();
        }
        //---------------------------------------------------------------------------------------                            
        
        private void DeleteSpeciesgroup(object o)
        {
            if (CurrentTbl68Speciesgroup == null)
            {
                WpfMessageBox.Show(CultRes.StringsRes.DatasetNew,
                    CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                return;
            }
            _businessLayer = new BusinessLayer.BusinessLayer();

            var ret = false;
            //check if in Tbl69FiSpeciesses or Tbl72PlSpeciesses connected datasets, than return
            Tbl69FiSpeciessesList = new ObservableCollection<Tbl69FiSpecies>(_businessLayer.ListTbl69FiSpeciessesBySpeciesgroupId(CurrentTbl68Speciesgroup.SpeciesgroupID));
            if (Tbl69FiSpeciessesList.Count != 0)
            {
                WpfMessageBox.Show(CultRes.StringsRes.ConnectedTable, CultRes.StringsRes.FiSpecies + " " + CultRes.StringsRes.ConnectedDataset,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                 ret = true;              
            }
            Tbl72PlSpeciessesList = new ObservableCollection<Tbl72PlSpecies>(_businessLayer.ListTbl72PlSpeciessesBySpeciesgroupId(CurrentTbl68Speciesgroup.SpeciesgroupID));
            if (Tbl72PlSpeciessesList.Count != 0)
            {
                WpfMessageBox.Show(CultRes.StringsRes.ConnectedTable, CultRes.StringsRes.PlSpecies + " " + CultRes.StringsRes.ConnectedDataset,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                 ret = true;              
            }
            if (ret)  return;
            {
            try
            {
                var speciesgroup = _businessLayer.SingleListTbl68SpeciesgroupsBySpeciesgroupId(CurrentTbl68Speciesgroup.SpeciesgroupID);
                if (speciesgroup != null)
                {
                    if (WpfMessageBox.Show(CultRes.StringsRes.DeleteQuestion1, CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl68Speciesgroup.SpeciesgroupName,
                            MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes)
                        return;
                    speciesgroup.EntityState = EntityState.Deleted;
                    _businessLayer.RemoveSpeciesgroup(speciesgroup);

                    WpfMessageBox.Show(CultRes.StringsRes.DeleteSuccess, CurrentTbl68Speciesgroup.SpeciesgroupName,
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                }
                else
                {
                    WpfMessageBox.Show(CultRes.StringsRes.Information, CultRes.StringsRes.DeleteCan + " " + CurrentTbl68Speciesgroup.SpeciesgroupName  + " " + CultRes.StringsRes.DeleteCan1,
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                }
            }
            catch (DbEntityValidationException ex)
            {
                _entityException.EntityException(ex);
                    Log.Error(ex);
            }
        }
            if (SearchSpeciesgroupName != "")
            {
                if (SearchSpeciesgroupName == "*")  //show all datasets
                {
                    SearchSpeciesgroupName = "";
                    Tbl68SpeciesgroupsList.Clear();
                    
                Tbl68SpeciesgroupsList = new ObservableCollection<Tbl68Speciesgroup>(_businessLayer.ListTbl68SpeciesgroupsBySpeciesgroupName(SearchSpeciesgroupName));            
                    SearchSpeciesgroupName = "*";
                }
                else
                {               
                    Tbl68SpeciesgroupsList =  new ObservableCollection<Tbl68Speciesgroup>(_businessLayer.ListTbl68SpeciesgroupsBySpeciesgroupName(SearchSpeciesgroupName));

                }
                SpeciesgroupsView = CollectionViewSource.GetDefaultView(Tbl68SpeciesgroupsList);
                SpeciesgroupsView.Refresh();
            }
            else  //SearchName = empty
            {
                Tbl68SpeciesgroupsList = new ObservableCollection<Tbl68Speciesgroup>(_businessLayer.ListTbl68SpeciesgroupsBySpeciesgroupName(SearchSpeciesgroupName));

                SpeciesgroupsView = CollectionViewSource.GetDefaultView(Tbl68SpeciesgroupsList);
                SpeciesgroupsView.MoveCurrentToFirst();
             }
        }
        //-------------------------------------------------------------------------------------------------                    
        
        private void SaveSpeciesgroup(object o)
        {
            if (CurrentTbl68Speciesgroup == null)
            {
                WpfMessageBox.Show(CultRes.StringsRes.DatasetNew,
                    CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                return;
            }
            _businessLayer = new BusinessLayer.BusinessLayer();

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
                                _position = SpeciesgroupsView.CurrentPosition;
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

            if (SearchSpeciesgroupName != "")
            {
                if (SearchSpeciesgroupName == "*")  //show all datasets
                {
                    SearchSpeciesgroupName = "";
                    Tbl68SpeciesgroupsList.Clear();
                    
                Tbl68SpeciesgroupsList = new ObservableCollection<Tbl68Speciesgroup>(_businessLayer.ListTbl68SpeciesgroupsBySpeciesgroupName(SearchSpeciesgroupName));            
                    SearchSpeciesgroupName = "*";
                }
                else
                {               
                    Tbl68SpeciesgroupsList = int.TryParse(SearchSpeciesgroupName, out var id)
                        ? new ObservableCollection<Tbl68Speciesgroup>(_businessLayer.ListTbl68SpeciesgroupsBySpeciesgroupId(id))
                        : new ObservableCollection<Tbl68Speciesgroup>(_businessLayer.ListTbl68SpeciesgroupsBySpeciesgroupName(SearchSpeciesgroupName));

                }
                SpeciesgroupsView = CollectionViewSource.GetDefaultView(Tbl68SpeciesgroupsList);
                SpeciesgroupsView.MoveCurrentToPosition(_position);
            }
            else  
            {
                Tbl68SpeciesgroupsList = new ObservableCollection<Tbl68Speciesgroup>(_businessLayer.ListTbl68SpeciesgroupsBySpeciesgroupName(CurrentTbl68Speciesgroup.SpeciesgroupName));

                SpeciesgroupsView= CollectionViewSource.GetDefaultView(Tbl68SpeciesgroupsList);
                SpeciesgroupsView.Refresh();
            }
        }
        #endregion "Public Commands"                  
 
 

 //    Part 2    

                                                          

 //    Part 3    

                                                          



 //    Part 4    

           
        #region "Public Commands Connect ==> Tbl69FiSpecies"                 
        //-------------------------------------------------------------------------
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
            
        private void AddFiSpecies(object o)      
        {
            if (Tbl69FiSpeciessesList == null)
                Tbl69FiSpeciessesList =  new ObservableCollection<Tbl69FiSpecies>( );

            Tbl69FiSpeciessesList.Insert(0, new Tbl69FiSpecies  { FiSpeciesName = CultRes.StringsRes.DatasetNew});

            FiSpeciessesView = CollectionViewSource.GetDefaultView(Tbl69FiSpeciessesList);
            FiSpeciessesView.MoveCurrentToFirst();
        }
        //----------------------------------------------------------------------              
             
        private void CopyFiSpecies(object o)
        {
            if (CurrentTbl69FiSpecies == null)
            {
                WpfMessageBox.Show(CultRes.StringsRes.DatasetNew,
                    CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                return;
            }

            var fispecies = _businessLayer.SingleListTbl69FiSpeciessesByFiSpeciesId(CurrentTbl69FiSpecies.FiSpeciesID);

            Tbl69FiSpeciessesList.Insert(0, new Tbl69FiSpecies
            {
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
            FiSpeciessesView.MoveCurrentToFirst();
        }
        //----------------------------------------------------------------------            
             
        private void DeleteFiSpecies(object o)
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
                if (fispecies!= null)
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
                    Log.Error(ex);
            }
         
            Tbl69FiSpeciessesList = new ObservableCollection<Tbl69FiSpecies>(_businessLayer.ListTbl69FiSpeciessesBySpeciesgroupId(CurrentTbl68Speciesgroup.SpeciesgroupID));

            FiSpeciessesView = CollectionViewSource.GetDefaultView(Tbl69FiSpeciessesList);
            FiSpeciessesView.Refresh();
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

            CurrentTbl69FiSpecies.SpeciesgroupID = CurrentTbl68Speciesgroup.SpeciesgroupID;

            try
            {
                var fispecies = _businessLayer.SingleListTbl69FiSpeciessesByFiSpeciesId(CurrentTbl69FiSpecies.FiSpeciesID);
                if (CurrentTbl69FiSpecies.FiSpeciesID != 0)
                {
                    if (fispecies != null) //update
                    {
                            fispecies.GenusID = CurrentTbl69FiSpecies.GenusID;
                            fispecies.SpeciesgroupID = CurrentTbl69FiSpecies.SpeciesgroupID;
                            fispecies.FiSpeciesName = CurrentTbl69FiSpecies.FiSpeciesName;
                            fispecies.Subspecies = CurrentTbl69FiSpecies.Subspecies;
                            fispecies.Divers = CurrentTbl69FiSpecies.Divers;
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
                    //GenusID may be not 0
                    if (CurrentTbl69FiSpecies.GenusID == 0)          

                    {
                        WpfMessageBox.Show(CultRes.StringsRes.RequiredGenealogyConnect, CultRes.StringsRes.RequiredInput,
                            MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        return;
                    }

                    //check if dataset with Name and GenusId and SpeciesgroupId already exist       
                    var dataset = _businessLayer.ListTbl69FiSpeciessesByFiSpeciesNameAndSubspeciesAndDiversAndGenusIdAndSpeciesgroupId(CurrentTbl69FiSpecies.FiSpeciesName, CurrentTbl69FiSpecies.Subspecies, CurrentTbl69FiSpecies.Divers, CurrentTbl69FiSpecies.GenusID, CurrentTbl69FiSpecies.SpeciesgroupID);

                    if (dataset.Count != 0 && CurrentTbl69FiSpecies.FiSpeciesID == 0)  //dataset exist
                    {
                        WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTbl69FiSpecies.FiSpeciesName + " " +  CurrentTbl69FiSpecies.Subspecies + " " +  CurrentTbl69FiSpecies.Divers,
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        return;
                    }
                    if (dataset.Count == 0 && CurrentTbl69FiSpecies.FiSpeciesID == 0 ||
                        dataset.Count != 0 && CurrentTbl69FiSpecies.FiSpeciesID != 0 ||
                        dataset.Count == 0 && CurrentTbl69FiSpecies.FiSpeciesID != 0) //new dataset and update
                    {
                        if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTbl69FiSpecies.FiSpeciesName + " " +  CurrentTbl69FiSpecies.Subspecies + " " +  CurrentTbl69FiSpecies.Divers,
                                MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes)
                            return;
                        {
                            try
                            {
                                _businessLayer.UpdateFiSpecies(fispecies);
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
                                            : CurrentTbl69FiSpecies.FiSpeciesName + " " +  CurrentTbl69FiSpecies.Subspecies + " " +  CurrentTbl69FiSpecies.Divers,
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

            Tbl69FiSpeciessesList = new ObservableCollection<Tbl69FiSpecies>(_businessLayer.ListTbl69FiSpeciessesBySpeciesgroupId(CurrentTbl68Speciesgroup.SpeciesgroupID));            

            SelectedMainTabIndex = 1;
            FiSpeciessesView = CollectionViewSource.GetDefaultView(Tbl69FiSpeciessesList);
            FiSpeciessesView.Refresh();
        }
        #endregion "Public Commands"                                                                                                                              
                                                          


 //    Part 5    

                       
        #region "Public Commands Connect ==> Tbl72PlSpecies"                 
        //-------------------------------------------------------------------------
        private RelayCommand _addPlSpeciesCommand;

        public ICommand AddPlSpeciesCommand => _addPlSpeciesCommand ??
                                                (_addPlSpeciesCommand = new RelayCommand(delegate { AddPlSpecies(null); }));

        private RelayCommand _copyPlSpeciesCommand;

        public ICommand CopyPlSpeciesCommand => _copyPlSpeciesCommand ??
                                                 (_copyPlSpeciesCommand = new RelayCommand(delegate { CopyPlSpecies(null); }));

        private RelayCommand _deletePlSpeciesCommand;

        public ICommand DeletePlSpeciesCommand => _deletePlSpeciesCommand ??
                                                 (_deletePlSpeciesCommand = new RelayCommand(delegate { DeletePlSpecies(null); }));

        private RelayCommand _savePlSpeciesCommand;

        public ICommand SavePlSpeciesCommand => _savePlSpeciesCommand ??
                                                 (_savePlSpeciesCommand = new RelayCommand(delegate { SavePlSpecies(null); }));

        //-------------------------------------------------------------------------          
                         
        private void AddPlSpecies(object o)      
        {
            if (Tbl72PlSpeciessesList == null)
                Tbl72PlSpeciessesList = new ObservableCollection<Tbl72PlSpecies>();

            Tbl72PlSpeciessesList.Insert(0, new Tbl72PlSpecies   { PlSpeciesName = CultRes.StringsRes.DatasetNew });

            PlSpeciessesView = CollectionViewSource.GetDefaultView(Tbl72PlSpeciessesList);
            PlSpeciessesView.MoveCurrentToFirst();
        }
        //----------------------------------------------------------------------            
                         
        private void CopyPlSpecies(object o)
        {
            if (CurrentTbl72PlSpecies == null)
            {
                WpfMessageBox.Show(CultRes.StringsRes.DatasetNew,
                    CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                return;
            }

            var plspecies = _businessLayer.SingleListTbl72PlSpeciessesByPlSpeciesId(CurrentTbl72PlSpecies.PlSpeciesID);

            Tbl72PlSpeciessesList.Insert(0, new Tbl72PlSpecies
            {
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
            PlSpeciessesView.MoveCurrentToFirst();
        }
        //----------------------------------------------------------------------            
                          
        private void DeletePlSpecies(object o)
        {
            if (CurrentTbl72PlSpecies == null)
            {
                WpfMessageBox.Show(CultRes.StringsRes.DatasetNew,
                    CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                return;
            }

            try
            {
                var plspecies = _businessLayer.SingleListTbl72PlSpeciessesByPlSpeciesId(CurrentTbl72PlSpecies.PlSpeciesID);
                if (plspecies!= null)
                {
                    if (WpfMessageBox.Show(CultRes.StringsRes.DeleteQuestion1, CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl72PlSpecies.PlSpeciesName,
                         MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes) 
                    return;
                    plspecies.EntityState = EntityState.Deleted;
                    _businessLayer.RemovePlSpecies(plspecies);

                    WpfMessageBox.Show(CultRes.StringsRes.DeleteSuccess, CurrentTbl72PlSpecies.PlSpeciesName,
                       MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);  
                }
                else
                {
                    WpfMessageBox.Show(CultRes.StringsRes.Information, CultRes.StringsRes.DeleteCan + " " + CurrentTbl72PlSpecies.PlSpeciesName + " " + CultRes.StringsRes.DeleteCan1,
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                }
            }
            catch (DbEntityValidationException ex)
            {
                _entityException.EntityException(ex);
                    Log.Error(ex);
            }
         
            Tbl72PlSpeciessesList = new ObservableCollection<Tbl72PlSpecies>(_businessLayer.ListTbl72PlSpeciessesBySpeciesgroupId(CurrentTbl68Speciesgroup.SpeciesgroupID));

            PlSpeciessesView = CollectionViewSource.GetDefaultView(Tbl72PlSpeciessesList);
            PlSpeciessesView.Refresh();
        }
        //-------------------------------------------------------------------------------------------------                    
                       
        private void SavePlSpecies(object o)
        {
            if (CurrentTbl72PlSpecies == null)
            {
                WpfMessageBox.Show(CultRes.StringsRes.DatasetNew,
                    CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                return;
            }

            CurrentTbl72PlSpecies.SpeciesgroupID = CurrentTbl68Speciesgroup.SpeciesgroupID;

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
                            UpdaterDate = DateTime.Now,
                            EntityState = EntityState.Added
                    };
                }
                {
                    //GenusID may be not 0
                    if (CurrentTbl72PlSpecies.GenusID == 0 )          

                    {
                        WpfMessageBox.Show(CultRes.StringsRes.RequiredGenealogyConnect, CultRes.StringsRes.RequiredInput,
                            MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        return;
                    }

                    //check if dataset with Name and GenusID and SpeciesgroupID already exist       
                    var dataset = _businessLayer.ListTbl72PlSpeciessesByPlSpeciesNameAndSubspeciesAndDiversAndGenusIdAndSpeciesgroupId(CurrentTbl72PlSpecies.PlSpeciesName,  CurrentTbl72PlSpecies.Subspecies,  CurrentTbl72PlSpecies.Divers, CurrentTbl72PlSpecies.GenusID, CurrentTbl72PlSpecies.SpeciesgroupID);

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
                            try
                            {
                                _businessLayer.UpdatePlSpecies(plspecies);
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
                    Log.Error(ex);
                  return;
            }

            Tbl72PlSpeciessesList = new ObservableCollection<Tbl72PlSpecies>(_businessLayer.ListTbl72PlSpeciessesBySpeciesgroupId(CurrentTbl68Speciesgroup.SpeciesgroupID));            

            SelectedMainTabIndex = 2;
            PlSpeciessesView = CollectionViewSource.GetDefaultView(Tbl72PlSpeciessesList);
            PlSpeciessesView.Refresh();
        }
        #endregion "Public Commands"                  
                                                            
                      
 //    Part 6    

 
            

 //    Part 7    

 

 //    Part 8    

 
             
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
            SelectedDetailTabIndex = 1;  
            SelectedDetailSubTabIndex = 0;  

            Tbl69FiSpeciessesList =  new ObservableCollection<Tbl69FiSpecies>(
                    _businessLayer.ListTbl69FiSpeciessesBySpeciesgroupId(CurrentTbl68Speciesgroup.SpeciesgroupID));


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

        public int SelectedMainTabIndex
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

        public int SelectedDetailTabIndex
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

        public int SelectedDetailSubTabIndex
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
                    Tbl72PlSpeciessesList =  new ObservableCollection<Tbl72PlSpecies>(
                        _businessLayer.ListTbl72PlSpeciessesBySpeciesgroupId(CurrentTbl68Speciesgroup.SpeciesgroupID));

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

     
        #region "Public Properties Tbl68Speciesgroup"

        private string _searchSpeciesgroupName = "";
        public string SearchSpeciesgroupName
        {
            get => _searchSpeciesgroupName; 
            set { _searchSpeciesgroupName = value; RaisePropertyChanged();  }
        }

        public  ICollectionView SpeciesgroupsView;
        private   Tbl68Speciesgroup CurrentTbl68Speciesgroup => SpeciesgroupsView?.CurrentItem as Tbl68Speciesgroup;

        private ObservableCollection<Tbl68Speciesgroup> _tbl68SpeciesgroupsList;
        public  ObservableCollection<Tbl68Speciesgroup> Tbl68SpeciesgroupsList
        {
            get => _tbl68SpeciesgroupsList; 
            set {  _tbl68SpeciesgroupsList = value; RaisePropertyChanged();   }
        }

        private ObservableCollection<Tbl68Speciesgroup> _tbl68SpeciesgroupsAllList;
        public  ObservableCollection<Tbl68Speciesgroup> Tbl68SpeciesgroupsAllList
        {
            get => _tbl68SpeciesgroupsAllList; 
            set {  _tbl68SpeciesgroupsAllList = value; RaisePropertyChanged();   }
        }

        #endregion "Public Properties"   
       
        #region "Public Properties Tbl66Genus"

        private ObservableCollection<Tbl66Genus> _tbl66GenussesList;
        public  ObservableCollection<Tbl66Genus> Tbl66GenussesList
        {
            get => _tbl66GenussesList;
            set { _tbl66GenussesList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl66Genus> _tbl66GenussesAllList;
        public ObservableCollection<Tbl66Genus> Tbl66GenussesAllList
        {
            get => _tbl66GenussesAllList;
            set { _tbl66GenussesAllList = value; RaisePropertyChanged(); }
        }

        #endregion "Public Properties"   
        
        #region "Public Properties Tbl69FiSpecies"

        public ICollectionView FiSpeciessesView;
        private Tbl69FiSpecies CurrentTbl69FiSpecies => FiSpeciessesView?.CurrentItem as Tbl69FiSpecies;           

        private ObservableCollection<Tbl69FiSpecies> _tbl69FiSpeciessesList;
        public  ObservableCollection<Tbl69FiSpecies> Tbl69FiSpeciessesList
        {
            get => _tbl69FiSpeciessesList; 
            set { _tbl69FiSpeciessesList = value; RaisePropertyChanged(); }
        }
        #endregion "Public Properties"     
        
        #region "Public Properties Tbl72PlSpecies"

        public ICollectionView PlSpeciessesView;
        private Tbl72PlSpecies CurrentTbl72PlSpecies => PlSpeciessesView?.CurrentItem as Tbl72PlSpecies;           

        private ObservableCollection<Tbl72PlSpecies> _tbl72PlSpeciessesList;
        public  ObservableCollection<Tbl72PlSpecies> Tbl72PlSpeciessesList
        {
            get => _tbl72PlSpeciessesList; 
            set { _tbl72PlSpeciessesList = value; RaisePropertyChanged(); }
        }
        #endregion "Public Properties"     
   

 



   }
}   
