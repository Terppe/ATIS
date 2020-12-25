using System;
using System.Collections.ObjectModel;
using System.ComponentModel;  

    
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using ATIS.Dal.Models;
using ATIS.Ui.Core;
using ATIS.Ui.Helper;
using ATIS.Ui.Views.Database.DatabaseHelper;
using log4net;
using Microsoft.EntityFrameworkCore;          
    
         //    SpeciesgroupsViewModel Skriptdatum:  15.12.2020  10:32    

namespace ATIS.Ui.Views.Database.D68Speciesgroup
{     
    
    public class SpeciesgroupsViewModel : ViewModelBase                     
    {  
        // Version with Generic Unit Of Work and AtisDbContext for general use   
    
        #region [Private Data Members]
        private static readonly ILog Log = LogManager.GetLogger(typeof(SpeciesgroupsViewModel));
        private readonly UnitOfWork _uow = new UnitOfWork(new AtisDbContext());
        private readonly CrudFunctions _extCrud = new CrudFunctions();

        private readonly AllMessageBoxes _allMessageBoxes = new AllMessageBoxes();
        private readonly GenericMessageBoxes<Tbl68Speciesgroup> _genSpeciesgroupMessageBoxes = new GenericMessageBoxes<Tbl68Speciesgroup>();
        private readonly GenericMessageBoxes<Tbl69FiSpecies> _genFiSpeciesMessageBoxes = new GenericMessageBoxes<Tbl69FiSpecies>();
        private readonly GenericMessageBoxes<Tbl72PlSpecies> _genPlSpeciesMessageBoxes = new GenericMessageBoxes<Tbl72PlSpecies>();
        private int _position;   
         
        #endregion [Private Data Members]               
      
        #region [Constructor]

        public SpeciesgroupsViewModel()
        {
            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.
            }
            else
            {          
        
                // Code runs "for real" 
                Tbl68SpeciesgroupsList = new ObservableCollection<Tbl68Speciesgroup>();    
            }
        }     
        public bool IsInDesignMode { get; set; }

        #endregion [Constructor]          
 

 //    Part 1    

         

        #region [Commands Speciesgroup]

        private RelayCommand _getSpeciesgroupsByNameOrIdCommand;
        public ICommand GetSpeciesgroupsByNameOrIdCommand => _getSpeciesgroupsByNameOrIdCommand ??= new RelayCommand(delegate {ExecuteGetSpeciesgroupsByNameOrId(SearchSpeciesgroupName); });    
             
        private RelayCommand _addSpeciesgroupCommand;
        public ICommand AddSpeciesgroupCommand => _addSpeciesgroupCommand ??= new RelayCommand(delegate { ExecuteAddSpeciesgroup(null); });

        private RelayCommand _copySpeciesgroupCommand;
        public ICommand CopySpeciesgroupCommand => _copySpeciesgroupCommand ??= new RelayCommand(delegate { ExecuteCopySpeciesgroup(null); });      
             
        private RelayCommand _deleteSpeciesgroupCommand;
        public ICommand DeleteSpeciesgroupCommand => _deleteSpeciesgroupCommand ??= new RelayCommand(delegate { ExecuteDeleteSpeciesgroup(SearchSpeciesgroupName); });    
             
        private RelayCommand _saveSpeciesgroupCommand;
        public ICommand SaveSpeciesgroupCommand => _saveSpeciesgroupCommand ??= new RelayCommand(delegate { ExecuteSaveSpeciesgroup(SearchSpeciesgroupName); });    

        #endregion [Commands Speciesgroup]       

        
        #region [Methods Speciesgroup]

        private void ExecuteGetSpeciesgroupsByNameOrId(string searchName)
       {
            Tbl68SpeciesgroupsList = _extCrud.GetCollectionFromSearchNameOrIdOrderBy<Tbl68Speciesgroup>(SearchSpeciesgroupName, "speciesgroup");

            SelectedMainTabIndex = 0;
            SelectedDetailTabIndex = 0;

            SpeciesgroupsView = CollectionViewSource.GetDefaultView(Tbl68SpeciesgroupsList);
            SpeciesgroupsView.Refresh();
        }                       
        
        private void ExecuteAddSpeciesgroup(object o)
        {  
            Tbl68SpeciesgroupsList = new ObservableCollection<Tbl68Speciesgroup>();
            Tbl68SpeciesgroupsList.Insert(0, new Tbl68Speciesgroup {   SpeciesgroupName = CultRes.StringsRes.DatasetNew}  );

            Tbl66GenussesAllList = _extCrud.GetCollectionAllOrderBy<Tbl66Genus>("genus");

            SpeciesgroupsView = CollectionViewSource.GetDefaultView(Tbl68SpeciesgroupsList);
            SpeciesgroupsView.MoveCurrentToFirst();
        }                            
     
        private void ExecuteCopySpeciesgroup(object o)
        {
            if (_genSpeciesgroupMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl68Speciesgroup)) return;

            Tbl68SpeciesgroupsList = _extCrud.CopySpeciesgroup(CurrentTbl68Speciesgroup);

            // evtl verbundene tabellen-Datensätze auch kopieren Expert, Source, Author und Comment

            SpeciesgroupsView = CollectionViewSource.GetDefaultView(Tbl68SpeciesgroupsList);
            SpeciesgroupsView.MoveCurrentToFirst();
        }                         
     
        private void ExecuteDeleteSpeciesgroup(string searchName)
        {
            if (_genSpeciesgroupMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl68Speciesgroup)) return;               
 
    
            //check if in Tbl69FiSpeciesses connected datasets no delete possible, Expert, Sources, Authors and Comment delete and than return

            Tbl69FiSpeciessesList = _extCrud.SearchForConnectedDatasetsWithSpeciesgroupIdInTableFiSpecies(CurrentTbl68Speciesgroup);     
      
            if (_allMessageBoxes.DoNotDeleteDatasetInfoMessageBox(Tbl69FiSpeciessesList.Count, "FiSpecies")) return;

            try
            {
                var speciesgroup= _uow.Tbl68Speciesgroups.GetById(CurrentTbl68Speciesgroup.SpeciesgroupId);
                if (speciesgroup!= null)
                {
                    if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl68Speciesgroup.SpeciesgroupName)) return;

                    _extCrud.DeleteSpeciesgroup(speciesgroup);

                    _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CurrentTbl68Speciesgroup.SpeciesgroupName);
                }
                else _allMessageBoxes.InfoMessageBox("Not To Delete", CultRes.StringsRes.DeleteCan + " " + CurrentTbl68Speciesgroup.SpeciesgroupName + " " + CultRes.StringsRes.DeleteCan1);
            }
            catch (Exception e)
            {
                _allMessageBoxes.InfoMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }

            ExecuteGetSpeciesgroupsByNameOrId(searchName);

            SpeciesgroupsView.MoveCurrentToFirst();
        }                
     
        private void ExecuteSaveSpeciesgroup(string searchName)
        {
            if (_genSpeciesgroupMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl68Speciesgroup)) return;      
     
            try
            {
                var speciesgroup = _uow.Tbl68Speciesgroups .GetById(CurrentTbl68Speciesgroup.SpeciesgroupId);
                //   var phylum = _context.Tbl68Speciesgroups.AsNoTracking().FirstOrDefault(a=>a.SpeciesgroupId == CurrentTbl68Speciesgroup.SpeciesgroupId);
                //          _context.Entry(speciesgroup).State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;

                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(CurrentTbl68Speciesgroup.SpeciesgroupName))
                    return;

                if (CurrentTbl68Speciesgroup.SpeciesgroupId == 0)
                    speciesgroup = _extCrud.SpeciesgroupAdd(CurrentTbl68Speciesgroup);
                else
                    speciesgroup = _extCrud.SpeciesgroupUpdate(speciesgroup, CurrentTbl68Speciesgroup);

                _position = SpeciesgroupsView.CurrentPosition;

                try
                {
                    _extCrud.SpeciesgroupSave(speciesgroup, CurrentTbl68Speciesgroup);
                }
                catch (DbUpdateException e)
                {
                    if (e.InnerException != null)
                        _allMessageBoxes.WarningMessageBox(e.InnerException.ToString(),
                            CultRes.StringsRes.FailedToSave); 
                    Log.Error(e);
                    return;
                }
                catch (Exception e)
                {
                    _allMessageBoxes.InfoMessageBox(e.Message, CultRes.StringsRes.Error); 
                    Log.Error(e);
                    return;
                }

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.SaveSuccess, CurrentTbl68Speciesgroup.SpeciesgroupId == 0
                    ? "DatasetNew"
                    : CurrentTbl68Speciesgroup.SpeciesgroupName);
            }
            catch (Exception e)
            {
                _allMessageBoxes.WarningMessageBox(e.Message, CultRes.StringsRes.Error); 
                Log.Error(e);
            }
            ExecuteGetSpeciesgroupsByNameOrId(searchName);
            SpeciesgroupsView.MoveCurrentToPosition(_position);
        }
        #endregion [Methods Speciesgroup]                
 
 

 //    Part 2    

                                                          

 //    Part 3    

                                                          



 //    Part 4    

           
        #region [Public Commands Connect ==> Tbl69FiSpecies]                 
        
        private RelayCommand _addFiSpeciesCommand;
        public ICommand AddFiSpeciesCommand => _addFiSpeciesCommand ??= new RelayCommand(delegate { ExecuteAddFiSpecies(null); });

        private RelayCommand _copyFiSpeciesCommand;
        public ICommand CopyFiSpeciesCommand => _copyFiSpeciesCommand ??= new RelayCommand(delegate { ExecuteCopyFiSpecies(null); });

        private RelayCommand _deleteFiSpeciesCommand;
        public ICommand DeleteFiSpeciesCommand => _deleteFiSpeciesCommand ??= new RelayCommand(delegate { ExecuteDeleteFiSpecies(SearchSpeciesgroupName); });

        private RelayCommand _saveFiSpeciesCommand;
        public ICommand SaveFiSpeciesCommand => _saveFiSpeciesCommand ??= new RelayCommand(delegate { ExecuteSaveFiSpecies(SearchSpeciesgroupName); });    

        #endregion [Public Commands Connect ==> Tbl69FiSpecies]    

        #region [Public Methods Connect ==> Tbl69FiSpecies]                   
            
        private void ExecuteAddFiSpecies(object o)      
        {
            Tbl69FiSpeciessesList.Insert(0, new Tbl69FiSpecies  { FiSpeciesName = CultRes.StringsRes.DatasetNew});
            Tbl66GenussesAllList = _extCrud.GetCollectionAllOrderBy<Tbl66Genus>("genus");

            FiSpeciessesView = CollectionViewSource.GetDefaultView(Tbl69FiSpeciessesList);
            FiSpeciessesView.MoveCurrentToFirst();
        }             
             
        private void ExecuteCopyFiSpecies(object o)
        {
            if (_genFiSpeciesMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl69FiSpecies)) return;

            // evtl verbundene tabellen-Datensätze auch kopieren Names, Images, Synonyms und Geographics

            FiSpeciessesView = CollectionViewSource.GetDefaultView(Tbl69FiSpeciessesList);
            FiSpeciessesView.MoveCurrentToFirst();
        }        
                   
        private void ExecuteDeleteFiSpecies(string searchName)
        {
             if (_genFiSpeciesMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl69FiSpecies)) return;

            //check if in Tbl69FiSpeciesses connected datasets no delete possible, Names, Images, Synonyms and Geographics delete and than return
            Tbl78NamesList = _extCrud.SearchForConnectedDatasetsWithFiSpeciesIdInTableName(CurrentTbl69FiSpecies);
            if (_allMessageBoxes.DoNotDeleteDatasetInfoMessageBox(Tbl78namesList.Count, "Name")) return;
            Tbl81ImagesList = _extCrud.SearchForConnectedDatasetsWithFiSpeciesIdInTableImage(CurrentTbl69FiSpecies);
            if (_allMessageBoxes.DoNotDeleteDatasetInfoMessageBox(Tbl81ImagesList.Count, "Image")) return;
            Tbl84SynonymsList = _extCrud.SearchForConnectedDatasetsWithFiSpeciesIdInTableSynonym(CurrentTbl69FiSpecies);
            if (_allMessageBoxes.DoNotDeleteDatasetInfoMessageBox(Tbl84SynonymsList.Count, "Synonym")) return;
            Tbl87GeographicsList = _extCrud.SearchForConnectedDatasetsWithFiSpeciesIdInTableGeographic(CurrentTbl69FiSpecies);
            if (_allMessageBoxes.DoNotDeleteDatasetInfoMessageBox(Tbl87GeographicsList.Count, "Geographic")) return;
                         
               
            try 
            {
                var fispecies = _uow.Tbl69FiSpeciesses.GetById(CurrentTbl69FiSpecies.FiSpeciesId);
                if (fispecies != null)
                {
                    if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl69FiSpecies.FiSpeciesName)) return;

                    _extCrud.DeleteFiSpecies(fispecies);

                    _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CurrentTbl69FiSpecies.FiSpeciesName);
                }
                else _allMessageBoxes.InfoMessageBox("Not To Delete", CultRes.StringsRes.DeleteCan + " " + CurrentTbl69FiSpecies.FiSpeciesName + " " + CultRes.StringsRes.DeleteCan1);
            }
            catch (Exception e)
            {
                _allMessageBoxes.InfoMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }

            Tbl69FiSpeciessesList = _extCrud.GetFiSpeciessesCollectionFromSpeciesgroupIdOrderBy<Tbl69FiSpecies>(CurrentTbl69FiSpecies.SpeciesgroupId);

            FiSpeciessesView = CollectionViewSource.GetDefaultView(Tbl69FiSpeciessesList);
            FiSpeciessesView.MoveCurrentToFirst();
        }                      
                  
        private void ExecuteSaveFiSpecies(string searchName)
        {
             if (_genFiSpeciesMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl69FiSpecies)) return;

            CurrentTbl69FiSpecies.SpeciesgroupId = CurrentTbl68Speciesgroup.SpeciesgroupId;                                                                                                                      
              
            try
            {
                var fispecies = _uow.Tbl69FiSpeciesses.GetById(CurrentTbl69FiSpecies.FiSpeciesId);

                if (CurrentTbl69FiSpecies.FiSpeciesId == 0)
                    fispecies = _extCrud.FiSpeciesAdd(CurrentTbl69FiSpecies);
                else
                    fispecies = _extCrud.FiSpeciesUpdate(fispecies, CurrentTbl69FiSpecies);

              //  _position = FiSpeciessesView.CurrentPosition;

                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(CurrentTbl69FiSpecies.FiSpeciesName))  return;

                try
                {
                    _extCrud.FiSpeciesSave(fispecies, CurrentTbl69FiSpecies);
                }
                catch (DbUpdateException e)
                {
                    if (e.InnerException != null)
                        _allMessageBoxes.WarningMessageBox(e.InnerException.ToString(),
                            CultRes.StringsRes.FailedToSave);
                    Log.Error(e);
                    return;
                }
                catch (Exception e)
                {
                    _allMessageBoxes.InfoMessageBox(e.Message, CultRes.StringsRes.Error);
                    //         Log.Error(e);
                    return;
                }

                _allMessageBoxes.InfoMessageBox("Save Successfull", CurrentTbl69FiSpecies.FiSpeciesId == 0
                    ? CultRes.StringsRes.DatasetNew
                    : CurrentTbl69FiSpecies.FiSpeciesName);
            }
            catch (Exception e)
            {
                _allMessageBoxes.WarningMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }

            Tbl69FiSpeciessesList = _extCrud.GetFiSpeciessesCollectionFromSpeciesgroupIdOrderBy<Tbl69FiSpecies>(CurrentTbl69FiSpecies.SpeciesgroupId);

            FiSpeciessesView = CollectionViewSource.GetDefaultView(Tbl69FiSpeciessesList);
            FiSpeciessesView.MoveCurrentToFirst();
        }

        #endregion [Public Methods  Connect ==> Tbl69FiSpecies]                                                                                                                                            
                                                          


 //    Part 5    

                       
        #region [Public Commands Connect ==> Tbl72PlSpecies]                 
    
        private RelayCommand _addPlSpeciesCommand;

        public ICommand AddPlSpeciesCommand => _addPlSpeciesCommand ??= new RelayCommand(delegate { ExecuteAddPlSpecies(null); });

        private RelayCommand _copyPlSpeciesCommand;

        public ICommand CopyPlSpeciesCommand => _copyPlSpeciesCommand ??= new RelayCommand(delegate { ExecuteCopyPlSpecies(null); });

        private RelayCommand _deletePlSpeciesCommand;

        public ICommand DeletePlSpeciesCommand => _deletePlSpeciesCommand ??= new RelayCommand(delegate { ExecuteDeletePlSpecies(SearchSpeciesgroupName); });

        private RelayCommand _savePlSpeciesCommand;

        public ICommand SavePlSpeciesCommand => _savePlSpeciesCommand ??= new RelayCommand(delegate { ExecuteSavePlSpecies(SearchSpeciesgroupName); });        
        #endregion [Public Commands Connect ==> Tbl72PlSpecies]                

        #region [Public Methods Connect ==> Tbl72PlSpecies]                        
                         
        private void ExecuteAddPlSpecies(object o)      
        {
            Tbl72PlSpeciessesList.Insert(0, new Tbl72PlSpecies  { PlSpeciesName = CultRes.StringsRes.DatasetNew });

            Tbl66GenussesAllList = _extCrud.GetCollectionAllOrderBy<Tbl66Genus>("genus");
            Tbl68SpeciesgroupsAllList = _extCrud.GetCollectionAllOrderBy<Tbl68Speciesgroup>("speciesgroup");

            PlSpeciessesView = CollectionViewSource.GetDefaultView(Tbl72PlSpeciessesList);
            PlSpeciessesView.MoveCurrentToFirst();
        }
        //----------------------------------------------------------------------            
                          
        private void ExecuteCopyPlSpecies(object o)
        {
            if (_genPlSpeciesMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl72PlSpecies)) return;

            Tbl72PlSpeciessesList = _extCrud.CopyPlSpecies(CurrentTbl72PlSpecies);

            // evtl verbundene tabellen-Datensätze auch kopieren Names, Images, Synonyms und Geographics

            PlSpeciessesView = CollectionViewSource.GetDefaultView(Tbl72PlSpeciessesList);
            PlSpeciessesView.MoveCurrentToFirst();
        }          
                        
       private void ExecuteDeletePlSpecies(string searchName)
        {
             if (_genPlSpeciesMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl72PlSpecies)) return;
            //check if in Tbl72PlSpeciesses connected datasets no delete possible, Names, Images, Synonyms and Geographics delete and than return
            Tbl78NamesList = _extCrud.SearchForConnectedDatasetsWithFiSpeciesIdInTableName(CurrentTbl72PlSpecies);
            if (_allMessageBoxes.DoNotDeleteDatasetInfoMessageBox(Tbl78namesList.Count, "Name")) return;
            Tbl81ImagesList = _extCrud.SearchForConnectedDatasetsWithFiSpeciesIdInTableImage(CurrentTbl72PlSpecies);
            if (_allMessageBoxes.DoNotDeleteDatasetInfoMessageBox(Tbl81ImagesList.Count, "Image")) return;
            Tbl84SynonymsList = _extCrud.SearchForConnectedDatasetsWithFiSpeciesIdInTableSynonym(CurrentTbl72PlSpecies);
            if (_allMessageBoxes.DoNotDeleteDatasetInfoMessageBox(Tbl84SynonymsList.Count, "Synonym")) return;
            Tbl87GeographicsList = _extCrud.SearchForConnectedDatasetsWithFiSpeciesIdInTableGeographic(CurrentTbl72PlSpecies);
            if (_allMessageBoxes.DoNotDeleteDatasetInfoMessageBox(Tbl87GeographicsList.Count, "Geographic")) return;

            try 
            {
                var plspecies = _uow.Tbl72PlSpeciesses.GetById(CurrentTbl72PlSpecies.PlSpeciesId);
                if (plspecies != null)
                {
                    if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl72PlSpecies.PlSpeciesName)) return;

                    _extCrud.DeletePlSpecies(plspecies);

                    _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CurrentTbl72PlSpecies.PlSpeciesName);
                }
                else _allMessageBoxes.InfoMessageBox("Not To Delete", CultRes.StringsRes.DeleteCan + " " + CurrentTbl72PlSpecies.PlSpeciesName + " " + CultRes.StringsRes.DeleteCan1);
            }
            catch (Exception e)
            {
                _allMessageBoxes.InfoMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }

            Tbl72PlSpeciessesList = _extCrud.GetPlSpeciessesCollectionFromSpeciesgroupIdOrderBy<Tbl72PlSpecies>(CurrentTbl72PlSpecies.SpeciesgroupId);

            PlSpeciessesView = CollectionViewSource.GetDefaultView(Tbl72PlSpeciessesList);
            PlSpeciessesView.MoveCurrentToFirst();
        }                  
            
        private void ExecuteSavePlSpecies(string searchName)
        {
             if (_genPlSpeciesMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl72PlSpecies)) return;

            CurrentTbl72PlSpecies.SpeciesgroupId = CurrentTbl68Speciesgroup.SpeciesgroupId;                                                                                                                    

            try
            {
                var plspecies = _uow.Tbl72PlSpeciesses.GetById(CurrentTbl72PlSpecies.PlSpeciesId);

                if (CurrentTbl72PlSpecies.PlSpeciesId == 0)
                    plspecies = _extCrud.PlSpeciesAdd(CurrentTbl72PlSpecies);
                else
                    plspecies = _extCrud.PlSpeciesUpdate(plspecies, CurrentTbl72PlSpecies);

              //  _position = PlSpeciessesView.CurrentPosition;

                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(CurrentTbl72PlSpecies.PlSpeciesName))  return;

                try
                {
                    _extCrud.PlSpeciesSave(plspecies, CurrentTbl72PlSpecies);
                }
                catch (DbUpdateException e)
                {
                    if (e.InnerException != null)
                        _allMessageBoxes.WarningMessageBox(e.InnerException.ToString(),
                            CultRes.StringsRes.FailedToSave);
                    Log.Error(e);
                    return;
                }
                catch (Exception e)
                {
                    _allMessageBoxes.InfoMessageBox(e.Message, CultRes.StringsRes.Error);
                    //         Log.Error(e);
                    return;
                }

                _allMessageBoxes.InfoMessageBox("Save Successfull", CurrentTbl72PlSpecies.PlSpeciesId == 0
                    ? CultRes.StringsRes.DatasetNew
                    : CurrentTbl72PlSpecies.PlSpeciesName);
            }
            catch (Exception e)
            {
                _allMessageBoxes.WarningMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }

            Tbl72PlSpeciessesList = _extCrud.GetPlSpeciessesCollectionFromSpeciesgroupIdOrderBy<Tbl72PlSpecies>(CurrentTbl72PlSpecies.SpeciesgroupId);
        
            PlSpeciessesView = CollectionViewSource.GetDefaultView(Tbl72PlSpeciessesList);
            PlSpeciessesView.MoveCurrentToFirst();
        }
        #endregion [Public Methods  Connect ==> Tbl72PlSpecies]                                                                                        
                                                          
                      
 //    Part 6    

 
            

 //    Part 7    

 

 //    Part 8    

 
             
 //    Part 9    


     
        #region "Public Commands Connected Tables by DoubleClick"

        private RelayCommand _getConnectedTablesCommand;
        public ICommand GetConnectedTablesCommand => _getConnectedTablesCommand ??= new RelayCommand(delegate { GetConnectedTablesById(null); });

        #endregion "Public Commands Connected Tables by DoubleClick"

        #region "Public Method Connected Tables by DoubleClick"

        private void GetConnectedTablesById(object o)
        {           
     
            Tbl69FiSpeciessesList = _extCrud.GetFiSpeciessesCollectionFromSpeciesgroupIdOrderBy<Tbl69FiSpecies>(CurrentTbl68Speciesgroup.SpeciesgroupId);
            Tbl66GenussesAllList = _extCrud.GetCollectionAllOrderBy<Tbl66Genus>("genus");
            Tbl68SpeciesgroupsAllList = _extCrud.GetCollectionAllOrderBy<Tbl68Speciesgroup>("speciesgroup");

            FiSpeciessesView = CollectionViewSource.GetDefaultView(Tbl69FiSpeciessesList);
            FiSpeciessesView.Refresh();   
     
        }

        #endregion "Public Method Connected Tables by DoubleClick"     
 


 //    Part 10    

     
        #region "Public Commands to open Detail TabItems"

        private int _selectedMainTabIndex;
        private int _selectedMainSubRefTabIndex;
        private int _selectedDetailTabIndex;
        private int _selectedDetailSubRefTabIndex;

        public  int SelectedMainTabIndex
        {
            get => _selectedMainTabIndex; 
            set
            {
                if (value == _selectedMainTabIndex) return;
                _selectedMainTabIndex = value; RaisePropertyChanged("");        
       
                if (_selectedMainTabIndex == 0)             
                {
                    if (CurrentTbl68Speciesgroup != null)
                    {
                        Tbl69FiSpeciessesList = _extCrud.GetFiSpeciessesCollectionFromSpeciesgroupIdOrderBy<Tbl69FiSpecies>(CurrentTbl68Speciesgroup.SpeciesgroupId);

                        Tbl66GenussesAllList = _extCrud.GetCollectionAllOrderBy<Tbl66Genus>("genus");
                        Tbl68SpeciesgroupsAllList = _extCrud.GetCollectionAllOrderBy<Tbl68Speciesgroup>("speciesgroup");

                        FiSpeciessesView = CollectionViewSource.GetDefaultView(Tbl69FiSpeciessesList);
                        FiSpeciessesView.Refresh();
                    }
                    SelectedDetailTabIndex = 1;
                }         
       
                if (_selectedMainTabIndex == 1)             
                {
                    if (CurrentTbl68Speciesgroup != null)
                    {
                        Tbl72PlSpeciessesList = _extCrud.GetPlSpeciessesCollectionFromGenusIdOrderBy<Tbl72PlSpecies>(CurrentTbl68Speciesgroup.SpeciesgroupId);

                        Tbl66GenussesAllList = _extCrud.GetCollectionAllOrderBy<Tbl66Genus>("genus");
                        Tbl68SpeciesgroupsAllList = _extCrud.GetCollectionAllOrderBy<Tbl68Speciesgroup>("speciesgroup");

                        PlSpeciessesView = CollectionViewSource.GetDefaultView(Tbl72PlSpeciessesList);
                        PlSpeciessesView.Refresh();
                    }
                    SelectedDetailTabIndex = 2;
                }         
     
            }
        }

        public  int SelectedDetailTabIndex
        {
            get => _selectedDetailTabIndex; 
            set
            {
                if (value == _selectedDetailTabIndex) return;
                _selectedDetailTabIndex = value;    RaisePropertyChanged("");       
       
                if (_selectedDetailTabIndex == 0)
                {
                    if (CurrentTbl68Speciesgroup != null)
                    {

                    }
                    SelectedMainTabIndex = 0;  
                 }       
       
                if (_selectedDetailTabIndex == 1)
                {
                    if (CurrentTbl68Speciesgroup != null)
                    {
                    Tbl69FiSpeciessesList = _extCrud.GetFiSpeciessesCollectionFromSpeciesgroupIdOrderBy<Tbl69FiSpecies>(CurrentTbl68Speciesgroup.SpeciesgroupId);

                    Tbl66GenussesAllList = _extCrud.GetCollectionAllOrderBy<Tbl66Genus>("genus");
                    Tbl68SpeciesgroupsAllList = _extCrud.GetCollectionAllOrderBy<Tbl68Speciesgroup>("speciesgroup");

                    FiSpeciessesView = CollectionViewSource.GetDefaultView(Tbl69FiSpeciessesList);
                    FiSpeciessesView.Refresh();
                    }
                    SelectedMainTabIndex = 0;
                }           
       
                if (_selectedDetailTabIndex == 2)                
                {
                    if (CurrentTbl68Speciesgroup != null)
                    {
                        Tbl72PlSpeciessesList = _extCrud.GetPlSpeciessesCollectionFromGenusIdOrderBy<Tbl72PlSpecies>(CurrentTbl68Speciesgroup.SpeciesgroupId);

                        Tbl66GenussesAllList = _extCrud.GetCollectionAllOrderBy<Tbl66Genus>("genus");
                        Tbl68SpeciesgroupsAllList = _extCrud.GetCollectionAllOrderBy<Tbl68Speciesgroup>("speciesgroup");

                        PlSpeciessesView = CollectionViewSource.GetDefaultView(Tbl72PlSpeciessesList);
                        PlSpeciessesView.Refresh();
                    }
                    SelectedMainTabIndex = 1;
               }    
                     
            }
        }    
        #endregion "Public Commands to open Detail TabItems"          
 

 //    Part 11    

     
        #region "Public Properties Tbl68Speciesgroup"

        private string _searchSpeciesgroupName = "";
        public string SearchSpeciesgroupName
        {
            get => _searchSpeciesgroupName; 
            set { _searchSpeciesgroupName = value; RaisePropertyChanged("");  }
        }

        public  ICollectionView SpeciesgroupsView;
        private   Tbl68Speciesgroup CurrentTbl68Speciesgroup => SpeciesgroupsView?.CurrentItem as Tbl68Speciesgroup;

        private ObservableCollection<Tbl68Speciesgroup> _tbl68SpeciesgroupsList;
        public  ObservableCollection<Tbl68Speciesgroup> Tbl68SpeciesgroupsList
        {
            get => _tbl68SpeciesgroupsList; 
            set {  _tbl68SpeciesgroupsList = value; RaisePropertyChanged("");   }
        }

        private ObservableCollection<Tbl68Speciesgroup> _tbl68SpeciesgroupsAllList;
        public  ObservableCollection<Tbl68Speciesgroup> Tbl68SpeciesgroupsAllList
        {
            get => _tbl68SpeciesgroupsAllList; 
            set {  _tbl68SpeciesgroupsAllList = value; RaisePropertyChanged("");   }
        }

        private ObservableCollection<Tbl69FiSpecies> _tbl69FiSpeciessesAllList;
        public  ObservableCollection<Tbl69FiSpecies> Tbl69FiSpeciessesAllList
        {
            get => _tbl69FiSpeciessesAllList; 
            set {  _tbl69FiSpeciessesAllList = value; RaisePropertyChanged("");   }
        }

        #endregion "Public Properties"   
       
        #region "Public Properties Tbl66Genus"

        private ObservableCollection<Tbl66Genus> _tbl66GenussesList;
        public  ObservableCollection<Tbl66Genus> Tbl66GenussesList
        {
            get => _tbl66GenussesList;
            set { _tbl66GenussesList = value; RaisePropertyChanged(""); }
        }

        private ObservableCollection<Tbl66Genus> _tbl66GenussesAllList;
        public ObservableCollection<Tbl66Genus> Tbl66GenussesAllList
        {
            get => _tbl66GenussesAllList;
            set { _tbl66GenussesAllList = value; RaisePropertyChanged(""); }
        }

        #endregion "Public Properties"   
        
        #region "Public Properties Tbl69FiSpecies"

        public ICollectionView FiSpeciessesView;
        private Tbl69FiSpecies CurrentTbl69FiSpecies => FiSpeciessesView?.CurrentItem as Tbl69FiSpecies;           

        private ObservableCollection<Tbl69FiSpecies> _tbl69FiSpeciessesList;
        public  ObservableCollection<Tbl69FiSpecies> Tbl69FiSpeciessesList
        {
            get => _tbl69FiSpeciessesList; 
            set { _tbl69FiSpeciessesList = value; RaisePropertyChanged(""); }
        }
        #endregion "Public Properties"     
        
        #region "Public Properties Tbl72PlSpecies"

        public ICollectionView PlSpeciessesView;
        private Tbl72PlSpecies CurrentTbl72PlSpecies => PlSpeciessesView?.CurrentItem as Tbl72PlSpecies;           

        private ObservableCollection<Tbl72PlSpecies> _tbl72PlSpeciessesList;
        public  ObservableCollection<Tbl72PlSpecies> Tbl72PlSpeciessesList
        {
            get => _tbl72PlSpeciessesList; 
            set { _tbl72PlSpeciessesList = value; RaisePropertyChanged(""); }
        }
        #endregion "Public Properties"     
   

   }
}   
