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
    
using System.Collections.Generic;
using System.IO;
using System.Windows.Media.Imaging;
using Microsoft.Win32;   
    
         //    FiSpeciessesViewModel Skriptdatum:  17.12.2020  10:32    

namespace ATIS.Ui.Views.Database.D69FiSpecies
{     
    
    public class FiSpeciessesViewModel : ViewModelBase                     
    {  
        // Version with Generic Unit Of Work and AtisDbContext for general use   
    
        #region [Private Data Members]
        private static readonly ILog Log = LogManager.GetLogger(typeof(FiSpeciessesViewModel));
        private readonly UnitOfWork _uow = new UnitOfWork(new AtisDbContext());
        private readonly CrudFunctions _extCrud = new CrudFunctions();

        private readonly AllMessageBoxes _allMessageBoxes = new AllMessageBoxes();
        private readonly GenericMessageBoxes<Tbl69FiSpecies> _genFiSpeciesMessageBoxes = new GenericMessageBoxes<Tbl69FiSpecies>();
        private readonly GenericMessageBoxes<Tbl66Genus> _genGenusMessageBoxes = new GenericMessageBoxes<Tbl66Genus>();
        private readonly GenericMessageBoxes<Tbl68Speciesgroup> _genSpeciesgroupMessageBoxes = new GenericMessageBoxes<Tbl68Speciesgroup>();
        private readonly GenericMessageBoxes<Tbl78Name> _genNameMessageBoxes = new GenericMessageBoxes<Tbl78Name>();
        private readonly GenericMessageBoxes<Tbl81Image> _genImageMessageBoxes = new GenericMessageBoxes<Tbl81Image>();
        private readonly GenericMessageBoxes<Tbl84Synonym> _genSynonymMessageBoxes = new GenericMessageBoxes<Tbl84Synonym>();
        private readonly GenericMessageBoxes<Tbl87Geographic> _genGeographicMessageBoxes = new GenericMessageBoxes<Tbl87Geographic>();
        private readonly GenericMessageBoxes<Tbl90Reference> _genExpertMessageBoxes = new GenericMessageBoxes<Tbl90Reference>();
        private readonly GenericMessageBoxes<Tbl90Reference> _genSourceMessageBoxes = new GenericMessageBoxes<Tbl90Reference>();
        private readonly GenericMessageBoxes<Tbl90Reference> _genAuthorMessageBoxes = new GenericMessageBoxes<Tbl90Reference>();
        private readonly GenericMessageBoxes<Tbl93Comment> _genCommentMessageBoxes = new GenericMessageBoxes<Tbl93Comment>();
        private int _position;   
         
        #endregion [Private Data Members]               
      
        #region [Constructor]

        public FiSpeciessesViewModel()
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
        public bool IsInDesignMode { get; set; }

        #endregion [Constructor]          
 

 //    Part 1    

         

        #region [Commands FiSpecies]

        private RelayCommand _getFiSpeciessesByNameOrIdCommand;
        public ICommand GetFiSpeciessesByNameOrIdCommand => _getFiSpeciessesByNameOrIdCommand ??= new RelayCommand(delegate {ExecuteGetFiSpeciessesByNameOrId(SearchFiSpeciesName); });    
             
        private RelayCommand _addFiSpeciesCommand;
        public ICommand AddFiSpeciesCommand => _addFiSpeciesCommand ??= new RelayCommand(delegate { ExecuteAddFiSpecies(null); });

        private RelayCommand _copyFiSpeciesCommand;
        public ICommand CopyFiSpeciesCommand => _copyFiSpeciesCommand ??= new RelayCommand(delegate { ExecuteCopyFiSpecies(null); });      
             
        private RelayCommand _deleteFiSpeciesCommand;
        public ICommand DeleteFiSpeciesCommand => _deleteFiSpeciesCommand ??= new RelayCommand(delegate { ExecuteDeleteFiSpecies(SearchFiSpeciesName); });    
             
        private RelayCommand _saveFiSpeciesCommand;
        public ICommand SaveFiSpeciesCommand => _saveFiSpeciesCommand ??= new RelayCommand(delegate { ExecuteSaveFiSpecies(SearchFiSpeciesName); });    

        #endregion [Commands FiSpecies]       

        
        #region [Methods FiSpecies]

        private void ExecuteGetFiSpeciessesByNameOrId(string searchName)
       {
            Tbl66GenussesAllList = _extCrud.GetCollectionAllOrderBy<Tbl66Genus>("genus");
            Tbl68SpeciesgroupsAllList = _extCrud.GetCollectionAllOrderBy<Tbl68Speciesgroup>("speciesgroup");
            Tbl69FiSpeciessesList = _extCrud.GetCollectionFromSearchNameOrIdOrderBy<Tbl69FiSpecies>(SearchFiSpeciesName, "fispecies");

            SelectedMainTabIndex = 0;
            SelectedDetailTabIndex = 2;

            FiSpeciessesView = CollectionViewSource.GetDefaultView(Tbl69FiSpeciessesList);
            FiSpeciessesView.Refresh();
        }                       
        
        private void ExecuteAddFiSpecies(object o)
        {  
            Tbl69FiSpeciessesList = new ObservableCollection<Tbl69FiSpecies>();
            Tbl69FiSpeciessesList.Insert(0, new Tbl69FiSpecies {   FiSpeciesName = CultRes.StringsRes.DatasetNew}  );

            Tbl66GenussesAllList = _extCrud.GetCollectionAllOrderBy<Tbl66Genus>("genus");
            Tbl68SpeciesgroupsAllList = _extCrud.GetCollectionAllOrderBy<Tbl68Speciesgroup>("speciesgroup");

            FiSpeciessesView = CollectionViewSource.GetDefaultView(Tbl69FiSpeciessesList);
            FiSpeciessesView.MoveCurrentToFirst();
        }                            
     
        private void ExecuteCopyFiSpecies(object o)
        {
            if (_genFiSpeciesMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl69FiSpecies)) return;

            Tbl69FiSpeciessesList = _extCrud.CopyFiSpecies(CurrentTbl69FiSpecies);

            // evtl verbundene tabellen-Datensätze auch kopieren Expert, Source, Author und Comment

            FiSpeciessesView = CollectionViewSource.GetDefaultView(Tbl69FiSpeciessesList);
            FiSpeciessesView.MoveCurrentToFirst();
        }                         
     
        private void ExecuteDeleteFiSpecies(string searchName)
        {
            if (_genFiSpeciesMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl69FiSpecies)) return;               
        
            //check if in Tbl78Names, Tbl81Images, Tbl84Synonyms, Tbl87Geographics connected datasets no delete possible, Expert, Sources, Authors and Comment delete and than return

            Tbl81ImagesList = _extCrud.SearchForConnectedDatasetsWithFiSpeciesIdInTableImage(CurrentTbl69FiSpecies);

            if (_allMessageBoxes.DoNotDeleteDatasetInfoMessageBox(Tbl81ImagesList.Count, "Image")) return;

            Tbl84SynonymsList = _extCrud.SearchForConnectedDatasetsWithFiSpeciesIdInTableSynonym(CurrentTbl69FiSpecies);

            if (_allMessageBoxes.DoNotDeleteDatasetInfoMessageBox(Tbl84SynonymsList.Count, "Synonym")) return;

            Tbl87GeographicsList = _extCrud.SearchForConnectedDatasetsWithFiSpeciesIdInTableGeographic(CurrentTbl69FiSpecies);

            if (_allMessageBoxes.DoNotDeleteDatasetInfoMessageBox(Tbl87GeographicsList.Count, "Geographic")) return;

            Tbl78NamesList = _extCrud.SearchForConnectedDatasetsWithFiSpeciesIdInTableName(CurrentTbl69FiSpecies);      
     
            if (_allMessageBoxes.DoNotDeleteDatasetInfoMessageBox(Tbl78NamesList.Count, "Name")) return;

            //Delete all References Experts, Sources, Authors  ----------------------------------------------------
            Tbl90ReferencesList = _extCrud.DeleteDatasetsWithFiSpeciesIdInTableReference(CurrentTbl69FiSpecies);
            if (Tbl90ReferencesList.Count > 0)
            {
                if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.ReferenceAuthor + " " + 
                                              CultRes.StringsRes.ReferenceSource + " " + CultRes.StringsRes.ReferenceSource)) return;

                _extCrud.DeleteReferences(Tbl90ReferencesList);

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CultRes.StringsRes.Reference);
            }

            //Delete all Comments  ----------------------------------------------------
            Tbl93CommentsList = _extCrud.DeleteDatasetsWithFiSpeciesIdInTableComment(CurrentTbl69FiSpecies);
            if (Tbl93CommentsList.Count > 0)
            {
                if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.Comment)) return;

                _extCrud.DeleteComments(Tbl93CommentsList);

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CultRes.StringsRes.Comment);
            }
            try
            {
                var fispecies= _uow.Tbl69FiSpeciesses.GetById(CurrentTbl69FiSpecies.FiSpeciesId);
                if (fispecies!= null)
                {
                    if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.DeleteQuestion + " " + 
                                          CurrentTbl69FiSpecies.FiSpeciesName)) return;

                    _extCrud.DeleteFiSpecies(fispecies);

                    _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, 
                                         CurrentTbl69FiSpecies.FiSpeciesName);
                }
                else 
                        _allMessageBoxes.InfoMessageBox("Not To Delete", 
                                         CultRes.StringsRes.DeleteCan + " " + CurrentTbl69FiSpecies.FiSpeciesName + " " + 
                                         CultRes.StringsRes.DeleteCan1);
            }
            catch (Exception e)
            {
                _allMessageBoxes.InfoMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }

            ExecuteGetFiSpeciessesByNameOrId(searchName);

            FiSpeciessesView.MoveCurrentToFirst();
        }                
     
        private void ExecuteSaveFiSpecies(string searchName)
        {
            if (_genFiSpeciesMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl69FiSpecies)) return;      
       
            //Combobox select GenusID  may be not 0
            if (CurrentTbl69FiSpecies.GenusId == 0)
            {
                MessageBox.Show(CultRes.StringsRes.RequiredGenealogyConnect, CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }     
     
            try
            {
                var fispecies = _uow.Tbl69FiSpeciesses .GetById(CurrentTbl69FiSpecies.FiSpeciesId);
                //   var phylum = _context.Tbl69FiSpeciesses.AsNoTracking().FirstOrDefault(a=>a.FiSpeciesId == CurrentTbl69FiSpecies.FiSpeciesId);
                //          _context.Entry(fispecies).State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;

                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(CurrentTbl69FiSpecies.FiSpeciesName))
                    return;

                if (CurrentTbl69FiSpecies.FiSpeciesId == 0)
                    fispecies = _extCrud.FiSpeciesAdd(CurrentTbl69FiSpecies);
                else
                    fispecies = _extCrud.FiSpeciesUpdate(fispecies, CurrentTbl69FiSpecies);

                _position = FiSpeciessesView.CurrentPosition;

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
                    Log.Error(e);
                    return;
                }

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.SaveSuccess, CurrentTbl69FiSpecies.FiSpeciesId == 0
                    ? "DatasetNew"
                    : CurrentTbl69FiSpecies.FiSpeciesName);
            }
            catch (Exception e)
            {
                _allMessageBoxes.WarningMessageBox(e.Message, CultRes.StringsRes.Error); 
                Log.Error(e);
            }
            ExecuteGetFiSpeciessesByNameOrId(searchName);
            FiSpeciessesView.MoveCurrentToPosition(_position);
        }
        #endregion [Methods FiSpecies]                
 
 

 //    Part 2    

           
        #region "Public Commands Connect <== Tbl66Genus"                 
        

        private RelayCommand _saveGenusCommand;

        public ICommand SaveGenusCommand => 
                                      _saveGenusCommand ??= new RelayCommand(delegate { ExecuteSaveGenus(null); });        
           
        private void ExecuteSaveGenus(string searchName)
        {
            if (_genGenusMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl66Genus)) return;

            try
            {
                var genus = _uow.Tbl66Genusses.GetById(CurrentTbl66Genus.GenusId);

                if (CurrentTbl66Genus.GenusId == 0)
                    genus = _extCrud.GenusAdd(CurrentTbl66Genus);
                else
                    genus = _extCrud.GenusUpdate(genus, CurrentTbl66Genus);

                _position = FiSpeciessesView.CurrentPosition;   
       
                var cap = CurrentTbl66Genus.GenusName;
                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(cap))        return;               
       
                try
                {
                    _extCrud.GenusSave(genus, CurrentTbl66Genus);
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
      
                _allMessageBoxes.InfoMessageBox("Save Successfull", CurrentTbl66Genus.GenusId == 0
                    ? CultRes.StringsRes.DatasetNew
                    : CurrentTbl66Genus.GenusName);
            }       
     
            catch (Exception e)
            {
                _allMessageBoxes.WarningMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }
            ExecuteGetFiSpeciessesByNameOrId(searchName);
            FiSpeciessesView.MoveCurrentToPosition(_position);
        }

        #endregion "Public Commands"                  
                                                          

 //    Part 3    

           
        #region "Public Commands Connect <== Tbl68Speciesgroup"                 
       
        private RelayCommand _saveSpeciesgroupCommand;

        public ICommand SaveSpeciesgroupCommand => 
                            _saveSpeciesgroupCommand ??= new RelayCommand(delegate { ExecuteSaveSpeciesgroup(null); });
       
        
        private void ExecuteSaveSpeciesgroup(string searchName)
        {
            if (_genSpeciesgroupMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl68Speciesgroup)) return;

            try
            {
                var speciesgroup = _uow.Tbl68Speciesgroups.GetById(CurrentTbl68Speciesgroup.SpeciesgroupId);

                if (CurrentTbl68Speciesgroup.SpeciesgroupId == 0)
                    speciesgroup = _extCrud.SpeciesgroupAdd(CurrentTbl68Speciesgroup);
                else
                    speciesgroup = _extCrud.SpeciesgroupUpdate(speciesgroup, CurrentTbl68Speciesgroup);

                _position = FiSpeciessesView.CurrentPosition;

                var cap = CurrentTbl68Speciesgroup.SpeciesgroupName;
                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(cap))                return;

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
                    //         Log.Error(e);
                    return;
                }

                _allMessageBoxes.InfoMessageBox("Save Successfull", CurrentTbl68Speciesgroup.SpeciesgroupId == 0
                    ? CultRes.StringsRes.DatasetNew
                    : CurrentTbl68Speciesgroup.SpeciesgroupName);
            }

            catch (Exception e)
            {
                _allMessageBoxes.WarningMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }

            ExecuteGetFiSpeciessesByNameOrId(searchName);
            FiSpeciessesView.MoveCurrentToPosition(_position);
        }
        
        #endregion "Public Commands"                  
                                                          



 //    Part 4    

           
        #region [Public Commands Connect ==> Tbl78Name]                 
        
        private RelayCommand _addNameCommand;
        public ICommand AddNameCommand => _addNameCommand ??= new RelayCommand(delegate { ExecuteAddName(null); });

        private RelayCommand _copyNameCommand;
        public ICommand CopyNameCommand => _copyNameCommand ??= new RelayCommand(delegate { ExecuteCopyName(null); });

        private RelayCommand _deleteNameCommand;
        public ICommand DeleteNameCommand => _deleteNameCommand ??= new RelayCommand(delegate { ExecuteDeleteName(SearchFiSpeciesName); });

        private RelayCommand _saveNameCommand;
        public ICommand SaveNameCommand => _saveNameCommand ??= new RelayCommand(delegate { ExecuteSaveName(SearchFiSpeciesName); });    

        #endregion [Public Commands Connect ==> Tbl78Name]    

        #region [Public Methods Connect ==> Tbl78Name]                   
            
        private void ExecuteAddName(object o)      
        {
            Tbl78NamesList.Insert(0, new Tbl78Name  { NameName = CultRes.StringsRes.DatasetNew});

            NamesView = CollectionViewSource.GetDefaultView(Tbl78NamesList);
            NamesView.MoveCurrentToFirst();
        }             
             
        private void ExecuteCopyName(object o)
        {
            if (_genNameMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl78Name)) return;

            Tbl78NamesList = _extCrud.CopyName(CurrentTbl78Name);

            NamesView = CollectionViewSource.GetDefaultView(Tbl78NamesList);
            NamesView.MoveCurrentToFirst();
        }        
                     
        private void ExecuteDeleteName(string searchName)
        {
             if (_genNameMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl78Name)) return;                                                                                                                       
               
            try 
            {
                var name = _uow.Tbl78Names.GetById(CurrentTbl78Name.NameId);
                if (name != null)
                {
                    if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl78Name.NameName)) return;

                    _extCrud.DeleteName(name);

                    _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CurrentTbl78Name.NameName);
                }
                else _allMessageBoxes.InfoMessageBox("Not To Delete", CultRes.StringsRes.DeleteCan + " " + CurrentTbl78Name.NameName + " " + CultRes.StringsRes.DeleteCan1);
            }
            catch (Exception e)
            {
                _allMessageBoxes.InfoMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }

            Tbl78NamesList = _extCrud.GetNamesCollectionFromFiSpeciesIdOrderBy<Tbl78Name>(CurrentTbl78Name.FiSpeciesId);

            NamesView = CollectionViewSource.GetDefaultView(Tbl78NamesList);
            NamesView.MoveCurrentToFirst();
        }                      
      
    
        private void ExecuteSaveName(string searchName)
        {

            CurrentTbl78Name.FiSpeciesId = CurrentTbl69FiSpecies.FiSpeciesId;

            //Search for CurrentTbl78Name.PlSpeciesId with Plantae#Regnum# 

            //     var plantaeRegnum = _context.Tbl72PlSpeciesses.FirstOrDefault(p => p.PlSpeciesName == "Plantae#Regnum#");
            //     var plantaeRegnum = _uow.Tbl72PlSpeciesses.Find(p => p.PlSpeciesName == "Plantae#Regnum#").FirstOrDefault();
         //   var plantaeRegnumId = _extCrud.GetPlSpeciesIdFromPlSpeciessesCollectionSelectByName("Plantae#Regnum#");

            //Fehler um PlSpeciesId zu ermitteln !!!!!!!!!!!!!!!!!!
            //   if (plantaeRegnum != null) CurrentTbl78Name.PlSpeciesId = plantaeRegnum.PlSpeciesId;
            //Fehler !!!!
            CurrentTbl78Name.PlSpeciesId = 1;
            try
            {
                var name = _uow.Tbl78Names.GetById(CurrentTbl78Name.NameId);

                if (CurrentTbl78Name.NameId == 0)
                    name = _extCrud.NameAdd(CurrentTbl78Name);
                else
                    name = _extCrud.NameUpdate(name, CurrentTbl78Name);

                _position = NamesView.CurrentPosition;

                var cap = CurrentTbl78Name.NameName;
                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(cap)) return;

                try
                {
                    _extCrud.NameSave(name, CurrentTbl78Name);
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

                _allMessageBoxes.InfoMessageBox("Save Successfull", CurrentTbl78Name.NameId == 0
                    ? CultRes.StringsRes.DatasetNew
                    : CurrentTbl78Name.NameName);
            }

            catch (Exception e)
            {
                _allMessageBoxes.WarningMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }

            Tbl78NamesList = _extCrud.GetNamesCollectionFromFiSpeciesIdOrderBy<Tbl78Name>(CurrentTbl78Name.FiSpeciesId);

            NamesView = CollectionViewSource.GetDefaultView(Tbl78NamesList);
            NamesView.MoveCurrentToPosition(_position);
        }
        #endregion [Public Methods Connect ==> Tbl78Name]        
                                                          


 //    Part 5    

                       
        #region [Public Commands Connect ==> Tbl81Image]                 
        
        private RelayCommand _addImageCommand;

        public ICommand AddImageCommand => _addImageCommand ??= new RelayCommand(delegate { ExecuteAddImage(null); });

        private RelayCommand _copyImageCommand;

        public ICommand CopyImageCommand => _copyImageCommand ??= new RelayCommand(delegate { ExecuteCopyImage(null); });

        private RelayCommand _deleteImageCommand;

        public ICommand DeleteImageCommand => _deleteImageCommand ??= new RelayCommand(delegate { ExecuteDeleteImage(SearchFiSpeciesName); });

        private RelayCommand _saveImageCommand;

        public ICommand SaveImageCommand => _saveImageCommand ??= new RelayCommand(delegate { ExecuteSaveImage(SearchFiSpeciesName); });        
        #endregion [Public Commands Connect ==> Tbl81Image]                

        #region [Public Methods Connect ==> Tbl81Image]                        
              
        private void ExecuteAddImage(object o)      
        {
            if (Tbl81ImagesList == null)
                Tbl81ImagesList =  new ObservableCollection<Tbl81Image>( );

            Tbl81ImagesList.Insert(0, new Tbl81Image   { Info = CultRes.StringsRes.DatasetNew});

            ImagesView = CollectionViewSource.GetDefaultView(Tbl81ImagesList);
            ImagesView.MoveCurrentToFirst();
        }
        //----------------------------------------------------------------------              
                       
        private void ExecuteCopyImage(object o)
        {
            if (_genImageMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl81Image)) return;

            Tbl81ImagesList = _extCrud.CopyImage(CurrentTbl81Image);

            // evtl verbundene tabellen-Datensätze auch kopieren Names, Images, Synonyms und Geographics
          
            ImagesView = CollectionViewSource.GetDefaultView(Tbl81ImagesList);
            ImagesView.MoveCurrentToFirst();
        }
        //----------------------------------------------------------------------            
                          
        private void ExecuteDeleteImage(string searchName)
        {
             if (_genImageMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl81Image)) return;

            try 
            {
                var image = _uow.Tbl81Images.GetById(CurrentTbl81Image.ImageId);
                if (image != null)
                {
                    if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl81Image.ImageId)) return;

                    _extCrud.DeleteImage(image);

                    _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CurrentTbl81Image.ImageId.ToString());
                }
                else _allMessageBoxes.InfoMessageBox("Not To Delete", CultRes.StringsRes.DeleteCan + " " + CurrentTbl81Image.ImageId + " " + CultRes.StringsRes.DeleteCan1);
            }
            catch (Exception e)
            {
                _allMessageBoxes.InfoMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }

            Tbl81ImagesList = _extCrud.GetImagesCollectionFromFiSpeciesIdOrderBy<Tbl81Image>(CurrentTbl81Image.FiSpeciesId);

            ImagesView = CollectionViewSource.GetDefaultView(Tbl81ImagesList);
            ImagesView.MoveCurrentToFirst();
        }                  
                       
        private void ExecuteSaveImage(string searchName)
        {

             if (_genImageMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl81Image)) return;

            CurrentTbl81Image.FiSpeciesId = CurrentTbl69FiSpecies.FiSpeciesId;                                                                                                                    

            //Search for CurrentTbl81Image.PlSpeciesID with Plantae#Regnum# 
         //   var plantaeRegnum = _businessLayer.SingleListTbl72PlSpeciessesByPlSpeciesName("Plantae#Regnum#");
          //  CurrentTbl81Image.PlSpeciesID = plantaeRegnum.PlSpeciesID;
          //Fehler !!!
          CurrentTbl81Image.PlSpeciesId = 1;

            try
            {
                var image = _uow.Tbl81Images.GetById(CurrentTbl81Image.ImageId);

                if (CurrentTbl81Image.ImageId == 0)
                    image = _extCrud.ImageAdd(CurrentTbl81Image);
                else
                    image = _extCrud.ImageUpdate(image, CurrentTbl81Image);

              //  _position = ImagesView.CurrentPosition;

                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(CurrentTbl81Image.ImageId.ToString())) return;

                try
                {
                    _extCrud.ImageSave(image, CurrentTbl81Image);
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

                _allMessageBoxes.InfoMessageBox("Save Successfull", CurrentTbl81Image.ImageId == 0
                    ? CultRes.StringsRes.DatasetNew
                    : CurrentTbl81Image.ImageId.ToString());
            }
            catch (Exception e)
            {
                _allMessageBoxes.WarningMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }

            Tbl81ImagesList = _extCrud.GetImagesCollectionFromFiSpeciesIdOrderBy<Tbl81Image>(CurrentTbl81Image.FiSpeciesId);
        
            ImagesView = CollectionViewSource.GetDefaultView(Tbl81ImagesList);
            ImagesView.MoveCurrentToFirst();
        }

        #endregion [Public Methods  Connect ==> Tbl81Image]                                                                                               
                                                            
                      
 //    Part 6    

                       
        #region [Public Commands Connect ==> Tbl84Synonym]                 
        
        private RelayCommand _addSynonymCommand;

        public ICommand AddSynonymCommand => _addSynonymCommand ??= new RelayCommand(delegate { ExecuteAddSynonym(null); });

        private RelayCommand _copySynonymCommand;

        public ICommand CopySynonymCommand => _copySynonymCommand ??= new RelayCommand(delegate { ExecuteCopySynonym(null); });

        private RelayCommand _deleteSynonymCommand;

        public ICommand DeleteSynonymCommand => _deleteSynonymCommand ??= new RelayCommand(delegate { ExecuteDeleteSynonym(SearchFiSpeciesName); });

        private RelayCommand _saveSynonymCommand;

        public ICommand SaveSynonymCommand => _saveSynonymCommand ??= new RelayCommand(delegate { ExecuteSaveSynonym(SearchFiSpeciesName); });        
        #endregion [Public Commands Connect ==> Tbl84Synonym]                

        #region [Public Methods Connect ==> Tbl84Synonym]                        
              
        private void ExecuteAddSynonym(object o)      
        {
             Tbl84SynonymsList.Insert(0, new Tbl84Synonym  { SynonymName = CultRes.StringsRes.DatasetNew});

            SynonymsView = CollectionViewSource.GetDefaultView(Tbl84SynonymsList);
            SynonymsView.MoveCurrentToFirst();
        }
        //----------------------------------------------------------------------              
             
        private void ExecuteCopySynonym(object o)
        {
            if (_genSynonymMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl84Synonym)) return;

            Tbl84SynonymsList = _extCrud.CopySynonym(CurrentTbl84Synonym);

            SynonymsView = CollectionViewSource.GetDefaultView(Tbl84SynonymsList);
            SynonymsView.MoveCurrentToFirst();
        }        
                          
        private void ExecuteDeleteSynonym(string searchName)
        {
            if (_genSynonymMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl84Synonym)) return;

            try 
            {
                var synonym = _uow.Tbl84Synonyms.GetById(CurrentTbl84Synonym.SynonymId);
                if (synonym != null)
                {
                    if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl84Synonym.SynonymName)) return;

                    _extCrud.DeleteSynonym(synonym);

                    _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CurrentTbl84Synonym.SynonymName);
                }
                else _allMessageBoxes.InfoMessageBox("Not To Delete", CultRes.StringsRes.DeleteCan + " " + CurrentTbl84Synonym.SynonymName + " " + CultRes.StringsRes.DeleteCan1);
            }
            catch (Exception e)
            {
                _allMessageBoxes.InfoMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }

            Tbl84SynonymsList = _extCrud.GetSynonymsCollectionFromFiSpeciesIdOrderBy<Tbl84Synonym>(CurrentTbl84Synonym.FiSpeciesId);

            SynonymsView = CollectionViewSource.GetDefaultView(Tbl84SynonymsList);
            SynonymsView.MoveCurrentToFirst();
        }                      
             
        private void ExecuteSaveSynonym(string searchName)
        {

            CurrentTbl84Synonym.FiSpeciesId = CurrentTbl69FiSpecies.FiSpeciesId;

            //Search for CurrentTbl84Synonym.PlSpeciesId with Plantae#Regnum# 

            //     var plantaeRegnum = _context.Tbl72PlSpeciesses.FirstOrDefault(p => p.PlSpeciesName == "Plantae#Regnum#");
            //     var plantaeRegnum = _uow.Tbl72PlSpeciesses.Find(p => p.PlSpeciesName == "Plantae#Regnum#").FirstOrDefault();
            //  var plantaeRegnumId = _extCrud.PlSpeciesIdFromPlSpeciessesCollectionSelectByName("Plantae#Regnum#");

            //Fehler um PlSpeciesId zu ermitteln !!!!!!!!!!!!!!!!!!
            //   if (plantaeRegnum != null) CurrentTbl78Name.PlSpeciesId = plantaeRegnum.PlSpeciesId;
            //Fehler !!!!
            CurrentTbl84Synonym.PlSpeciesId = 1;
            try
            {
                var synonym = _uow.Tbl84Synonyms.GetById(CurrentTbl84Synonym.SynonymId);

                if (CurrentTbl84Synonym.SynonymId == 0)
                    synonym = _extCrud.SynonymAdd(CurrentTbl84Synonym);
                else
                    synonym = _extCrud.SynonymUpdate(synonym, CurrentTbl84Synonym);

                _position = SynonymsView.CurrentPosition;

                var cap = CurrentTbl84Synonym.SynonymName;
                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(cap)) return;

                try
                {
                    _extCrud.SynonymSave(synonym, CurrentTbl84Synonym);
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

                _allMessageBoxes.InfoMessageBox("Save Successfull", CurrentTbl84Synonym.SynonymId == 0
                    ? CultRes.StringsRes.DatasetNew
                    : CurrentTbl84Synonym.SynonymName);
            }

            catch (Exception e)
            {
                _allMessageBoxes.WarningMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }

            Tbl84SynonymsList = _extCrud.GetSynonymsCollectionFromFiSpeciesIdOrderBy<Tbl84Synonym>(CurrentTbl84Synonym.FiSpeciesId);

            SynonymsView = CollectionViewSource.GetDefaultView(Tbl84SynonymsList);
            SynonymsView.MoveCurrentToPosition(_position);
        }

        #endregion [Public Methods Connect ==> Tbl84Synonym]           
   
            

 //    Part 7    

             
       #region [Public Commands Connect ==> Tbl87Geographic]                
        
        private RelayCommand _addGeographicCommand;

        public ICommand AddGeographicCommand => _addGeographicCommand ??= new RelayCommand(delegate { ExecuteAddGeographic(null); });

        private RelayCommand _copyGeographicCommand;

        public ICommand CopyGeographicCommand => _copyGeographicCommand ??= new RelayCommand(delegate { ExecuteCopyGeographic(null); });

        private RelayCommand _deleteGeographicCommand;

        public ICommand DeleteGeographicCommand => _deleteGeographicCommand ??= new RelayCommand(delegate { ExecuteDeleteGeographic(SearchFiSpeciesName); });

        private RelayCommand _saveGeographicCommand;

        public ICommand SaveGeographicCommand => _saveGeographicCommand ??= new RelayCommand(delegate { ExecuteSaveGeographic(SearchFiSpeciesName); });

        #endregion [Public Commands Connect ==> Tbl87Geographic]                       
              
        #region [Public Methods Connect ==> Tbl87Geographic]
        private void ExecuteAddGeographic(object o)      
        {
            Tbl87GeographicsList.Insert(0, new Tbl87Geographic  { Info = CultRes.StringsRes.DatasetNew});

            GeographicsView = CollectionViewSource.GetDefaultView(Tbl87GeographicsList);
            GeographicsView.MoveCurrentToFirst();
        }           
             
        private void ExecuteCopyGeographic(object o)
        {
            if (_genGeographicMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl87Geographic)) return;

            Tbl87GeographicsList = _extCrud.CopyGeographic(CurrentTbl87Geographic);

            GeographicsView = CollectionViewSource.GetDefaultView(Tbl87GeographicsList);
            GeographicsView.MoveCurrentToFirst();
        }         
                          
        private void ExecuteDeleteGeographic(string searchName)
        {
            if (_genGeographicMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl87Geographic)) return;

            try
            {
                var geographic = _uow.Tbl87Geographics.GetById(CurrentTbl87Geographic.GeographicId);
                if (geographic != null)
                {
                    if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl87Geographic.GeographicId)) return;

                    _extCrud.DeleteGeographic(geographic);

                    _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CurrentTbl87Geographic.GeographicId.ToString());
                }
                else _allMessageBoxes.InfoMessageBox("Not To Delete", CultRes.StringsRes.DeleteCan + " " + CurrentTbl87Geographic.GeographicId + " " + CultRes.StringsRes.DeleteCan1);
            }
            catch (Exception e)
            {
                _allMessageBoxes.InfoMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }

            Tbl87GeographicsList = _extCrud.GetGeographicsCollectionFromFiSpeciesIdOrderBy<Tbl87Geographic>(CurrentTbl87Geographic.FiSpeciesId);

            GeographicsView = CollectionViewSource.GetDefaultView(Tbl87GeographicsList);
            GeographicsView.MoveCurrentToFirst();
        }               
             
        private void ExecuteSaveGeographic(string searchName)
        {
            if (_genGeographicMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl87Geographic)) return;

            CurrentTbl87Geographic.FiSpeciesId = CurrentTbl69FiSpecies.FiSpeciesId;

            //Search for CurrentTbl81Image.PlSpeciesID with Plantae#Regnum# 
            //   var plantaeRegnum = _businessLayer.SingleListTbl72PlSpeciessesByPlSpeciesName("Plantae#Regnum#");
            //  CurrentTbl81Image.PlSpeciesID = plantaeRegnum.PlSpeciesID;
            //Fehler !!!
            CurrentTbl87Geographic.PlSpeciesId = 1;

            try
            {
                var geographic = _uow.Tbl87Geographics.GetById(CurrentTbl87Geographic.GeographicId);

                if (CurrentTbl87Geographic.GeographicId == 0)
                    geographic = _extCrud.GeographicAdd(CurrentTbl87Geographic);
                else
                    geographic = _extCrud.GeographicUpdate(geographic, CurrentTbl87Geographic);

                //  _position = ImagesView.CurrentPosition;

                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(CurrentTbl87Geographic.GeographicId.ToString())) return;

                try
                {
                    _extCrud.GeographicSave(geographic, CurrentTbl87Geographic);
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

                _allMessageBoxes.InfoMessageBox("Save Successfull", CurrentTbl87Geographic.GeographicId == 0
                    ? CultRes.StringsRes.DatasetNew
                    : CurrentTbl87Geographic.GeographicId.ToString());
            }
            catch (Exception e)
            {
                _allMessageBoxes.WarningMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }

            Tbl87GeographicsList = _extCrud.GetGeographicsCollectionFromFiSpeciesIdOrderBy<Tbl87Geographic>(CurrentTbl87Geographic.FiSpeciesId);

            GeographicsView = CollectionViewSource.GetDefaultView(Tbl87GeographicsList);
            GeographicsView.MoveCurrentToFirst();
        }
        #endregion [Public Methods  Connect ==> Tbl87Geographics]                                                                                                        
   

 //    Part 8    

           
        #region [Commands FiSpecies ==> Tbl90Reference Author]

        private RelayCommand _addReferenceAuthorCommand;

        public ICommand AddReferenceAuthorCommand => _addReferenceAuthorCommand ??= new RelayCommand(delegate { ExecuteAddReferenceAuthor(null); });

        private RelayCommand _copyReferenceAuthorCommand;

        public ICommand CopyReferenceAuthorCommand => _copyReferenceAuthorCommand ??= new RelayCommand(delegate { ExecuteCopyReferenceAuthor(null); });

        private RelayCommand _deleteReferenceAuthorCommand;

        public ICommand DeleteReferenceAuthorCommand => _deleteReferenceAuthorCommand ??= new RelayCommand(delegate { ExecuteDeleteReferenceAuthor(null); });

        private RelayCommand _saveReferenceAuthorCommand;

        public ICommand SaveReferenceAuthorCommand => _saveReferenceAuthorCommand ??= new RelayCommand(delegate { ExecuteSaveReferenceAuthor(null); });        

        #endregion [Commands FiSpecies ==> Tbl90Reference Author]                
     
        #region [Methods FiSpecies ==> Tbl90Reference Author]

        public void ExecuteAddReferenceAuthor(object o)
        {
            Tbl90ReferenceAuthorsList ??= new ObservableCollection<Tbl90Reference>();

            Tbl90AuthorsAllList = _extCrud.GetCollectionAllOrderBy<Tbl90RefAuthor>("author");
            Tbl90ReferenceAuthorsList.Insert(0, new Tbl90Reference   { Info = CultRes.StringsRes.DatasetNew });

            ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
            ReferenceAuthorsView.MoveCurrentToFirst();
         }         
     
        public void ExecuteCopyReferenceAuthor(object o)
        {
            if (_genAuthorMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90ReferenceAuthor)) return;

            Tbl90ReferenceAuthorsList = _extCrud.CopyReferenceFiSpecies(CurrentTbl90ReferenceAuthor, "Author");

            ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
            ReferenceAuthorsView.MoveCurrentToFirst();
        }          
     
        private void ExecuteDeleteReferenceAuthor(string searchName)
        {
            if (_genAuthorMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90ReferenceAuthor)) return;

            try
            {
                var reference = _uow.Tbl90References.GetById(CurrentTbl90ReferenceAuthor.ReferenceId);
                if (reference != null)
                {
                    if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl90ReferenceAuthor.Info)) return;

                    _extCrud.DeleteReference(reference);

                    _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CurrentTbl90ReferenceAuthor.Info);
                }
                else _allMessageBoxes.InfoMessageBox("Not To Delete", CultRes.StringsRes.DeleteCan + " " + CurrentTbl90ReferenceAuthor.Info + " " + CultRes.StringsRes.DeleteCan1);
            }
            catch (Exception e)
            {
                _allMessageBoxes.InfoMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }

            ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
            ReferenceAuthorsView.Refresh();
        }          
     
        public void ExecuteSaveReferenceAuthor(string searchName)
        {
            if (_genAuthorMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90ReferenceAuthor)) return;

            CurrentTbl90ReferenceAuthor.FiSpeciesId = CurrentTbl69FiSpecies.FiSpeciesId;

            //Combobox select RefExpertId or RefSourceId or RefAuthorId may be not null
            if (CurrentTbl90ReferenceAuthor.RefExpertId == null &&
                CurrentTbl90ReferenceAuthor.RefSourceId == null &&
                CurrentTbl90ReferenceAuthor.RefAuthorId == null)
            {
                MessageBox.Show(CultRes.StringsRes.RequiredGenealogyConnect, CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            try
            {
                var reference = _uow.Tbl90References.GetById(CurrentTbl90ReferenceAuthor.ReferenceId);


                if (CurrentTbl90ReferenceAuthor.ReferenceId == 0)
                    reference = _extCrud.ReferenceAuthorFiSpeciesAdd(CurrentTbl90ReferenceAuthor);

                else
                    reference = _extCrud.ReferenceAuthorFiSpeciesUpdate(reference, CurrentTbl90ReferenceAuthor);

                //    _position = FiSpeciessesView.CurrentPosition;

                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(CurrentTbl90ReferenceAuthor.Info))  return;

                try
                {
                    _extCrud.ReferenceAuthorSave(reference, CurrentTbl90ReferenceAuthor);
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

                _allMessageBoxes.InfoMessageBox("Save Successfull", CurrentTbl90ReferenceAuthor.ReferenceId == 0
                    ? CultRes.StringsRes.DatasetNew
                    : CurrentTbl90ReferenceAuthor.Info);
            }
            catch (Exception e)
            {
                _allMessageBoxes.WarningMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }
           Tbl90ReferenceAuthorsList = _extCrud.GetReferenceAuthorsCollectionFromFiSpeciesIdAndRefSourceIdIsNullAndRefExpertIdIsNullOrderBy<Tbl90Reference>(CurrentTbl69FiSpecies.FiSpeciesId);            
      

            SelectedMainTabIndex = 6;
    //        SelectedDetailSubTabIndex = 6;
            SelectedMainSubRefTabIndex = 2;

            ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
            ReferenceAuthorsView.Refresh();
        }
        #endregion "Public Commands"                               
           
        #region [Commands FiSpecies ==> Tbl90Reference Source]      

        private RelayCommand _addReferenceSourceCommand;

        public ICommand AddReferenceSourceCommand => _addReferenceSourceCommand ??= new RelayCommand(delegate { ExecuteAddReferenceSource(null); });

        private RelayCommand _copyReferenceSourceCommand;

        public ICommand CopyReferenceSourceCommand => _copyReferenceSourceCommand ??= new RelayCommand(delegate {ExecuteCopyReferenceSource(null); });

        private RelayCommand _deleteReferenceSourceCommand;

        public ICommand DeleteReferenceSourceCommand => _deleteReferenceSourceCommand ??= new RelayCommand(delegate { ExecuteDeleteReferenceSource(null); });

        private RelayCommand _saveReferenceSourceCommand;

        public ICommand SaveReferenceSourceCommand => _saveReferenceSourceCommand ??= new RelayCommand(delegate { ExecuteSaveReferenceSource(null); });

            
        #endregion [Commands FiSpecies ==> Tbl90Reference Source]         
     
        #region [Methods FiSpecies ==> Tbl90Reference Source]      

        public void ExecuteAddReferenceSource(object o)
        {
            Tbl90ReferenceSourcesList ??= new ObservableCollection<Tbl90Reference>();

            Tbl90SourcesAllList = _extCrud.GetCollectionAllOrderBy<Tbl90RefSource>("source");

            Tbl90ReferenceSourcesList .Insert(0, new Tbl90Reference  { Info = CultRes.StringsRes.DatasetNew });

            ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
            ReferenceSourcesView.MoveCurrentToFirst();
         }         
     
        public void ExecuteCopyReferenceSource(object o)
        {
            if (_genSourceMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90ReferenceSource)) return;

            Tbl90ReferenceAuthorsList = _extCrud.CopyReferenceFiSpecies(CurrentTbl90ReferenceSource, "Source");

            ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
            ReferenceSourcesView.MoveCurrentToFirst();
        }           
     
        private void ExecuteDeleteReferenceSource(object o)
        {
            if (_genSourceMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90ReferenceSource)) return;

            try
            {
                var reference = _uow.Tbl90References.GetById(CurrentTbl90ReferenceSource.ReferenceId);
                if (reference != null)
                {
                    if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl90ReferenceSource.Info)) return;

                    _extCrud.DeleteReference(reference);

                    _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CurrentTbl90ReferenceSource.Info);
                }
                else _allMessageBoxes.InfoMessageBox("Not To Delete", CultRes.StringsRes.DeleteCan + " " + CurrentTbl90ReferenceSource.Info + " " + CultRes.StringsRes.DeleteCan1);
            }
            catch (Exception e)
            {
                _allMessageBoxes.InfoMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }

           Tbl90ReferenceSourcesList = _extCrud.GetReferenceSourcesCollectionFromFiSpeciesIdAndRefAuthorIdIsNullAndRefExpertIdIsNullOrderBy<Tbl90Reference>(CurrentTbl69FiSpecies.FiSpeciesId);          

            ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
            ReferenceSourcesView.MoveCurrentToFirst();
        }                  
     
        public void ExecuteSaveReferenceSource(object o)
        { 
           if (_genSourceMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90ReferenceSource)) return;

            //RefExpertId or RefSourceId or RefAuthorId may be not 0
            if (CurrentTbl90ReferenceSource.RefExpertId == null &&
                CurrentTbl90ReferenceSource.RefSourceId == null &&
                CurrentTbl90ReferenceSource.RefAuthorId == null)
            {
                MessageBox.Show(CultRes.StringsRes.RequiredGenealogyConnect, CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            CurrentTbl90ReferenceSource.FiSpeciesId = CurrentTbl69FiSpecies.FiSpeciesId;

            try
            {
                var reference = _uow.Tbl90References.GetById(CurrentTbl90ReferenceSource.ReferenceId);


                if (CurrentTbl90ReferenceSource.ReferenceId == 0)
                    reference = _extCrud.ReferenceSourceFiSpeciesAdd(CurrentTbl90ReferenceSource);
                else
                    reference = _extCrud.ReferenceSourceFiSpeciesUpdate(reference, CurrentTbl90ReferenceSource);

        //        _position = FiSpeciessesView.CurrentPosition;

                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(CurrentTbl90ReferenceSource.Info))  return;

                try
                {
                    _extCrud.ReferenceSourceSave(reference, CurrentTbl90ReferenceSource);

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

                _allMessageBoxes.InfoMessageBox("Save Successfull", CurrentTbl90ReferenceSource.ReferenceId == 0
                    ? CultRes.StringsRes.DatasetNew
                    : CurrentTbl90ReferenceSource.Info);
            }
            catch (Exception e)
            {
                _allMessageBoxes.WarningMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }

           Tbl90ReferenceSourcesList = _extCrud.GetReferenceSourcesCollectionFromFiSpeciesIdAndRefAuthorIdIsNullAndRefExpertIdIsNullOrderBy<Tbl90Reference>(CurrentTbl69FiSpecies.FiSpeciesId);            

      
            SelectedMainTabIndex = 6;
    //        SelectedDetailSubTabIndex = 6;
            SelectedMainSubRefTabIndex = 1;

            ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
            ReferenceSourcesView.Refresh();
        }
        #endregion "Public Commands"                              
           
        #region [Commands FiSpecies ==> Tbl90Reference Expert]                 
 
        private RelayCommand _addReferenceExpertCommand;

        public ICommand AddReferenceExpertCommand => _addReferenceExpertCommand ??= new RelayCommand(delegate { ExecuteAddReferenceExpert(null); });

        private RelayCommand _copyReferenceExpertCommand;

        public ICommand CopyReferenceExpertCommand => _copyReferenceExpertCommand ??= new RelayCommand(delegate { ExecuteCopyReferenceExpert(null); });

        private RelayCommand _deleteReferenceExpertCommand;

        public ICommand DeleteReferenceExpertCommand => _deleteReferenceExpertCommand ??= new RelayCommand(delegate { ExecuteDeleteReferenceExpert(null); });
        private RelayCommand _saveReferenceExpertCommand;

        public ICommand SaveReferenceExpertCommand => _saveReferenceExpertCommand ??= new RelayCommand(delegate { ExecuteSaveReferenceExpert(null); });

        #endregion [Commands FiSpecies ==> Tbl90Reference Expert]                    
     
     
        #region [Methods FiSpecies ==> Tbl90Reference Expert]                 

        public void ExecuteAddReferenceExpert(object o)
        {
            Tbl90ReferenceExpertsList ??= new ObservableCollection<Tbl90Reference>();

            Tbl90ExpertsAllList = _extCrud.GetCollectionAllOrderBy<Tbl90RefExpert>("expert");
            Tbl90ReferenceExpertsList .Insert(0, new Tbl90Reference   { Info = CultRes.StringsRes.DatasetNew });

            ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
            ReferenceExpertsView.MoveCurrentToFirst();
         }          
     
        public void ExecuteCopyReferenceExpert(object o)
        {
            if (_genExpertMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90ReferenceExpert)) return;

            Tbl90ReferenceExpertsList = _extCrud.CopyReferenceFiSpecies(CurrentTbl90ReferenceExpert, "Expert");

            ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
            ReferenceExpertsView.MoveCurrentToFirst();
        }         
     
        private void ExecuteDeleteReferenceExpert(object o)
        {
            if (_genExpertMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90ReferenceExpert)) return;

            try
            {
                var reference = _uow.Tbl90References.GetById(CurrentTbl90ReferenceExpert.ReferenceId);
                if (reference != null)
                {
                    if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl90ReferenceExpert.Info)) return;

                    _extCrud.DeleteReference(reference);

                    _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CurrentTbl90ReferenceExpert.Info);
                }
                else _allMessageBoxes.InfoMessageBox("Not To Delete", CultRes.StringsRes.DeleteCan + " " + CurrentTbl90ReferenceExpert.Info + " " + CultRes.StringsRes.DeleteCan1);
            }
            catch (Exception e)
            {
                _allMessageBoxes.InfoMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }

           Tbl90ReferenceExpertsList= _extCrud.GetReferenceExpertsCollectionFromFiSpeciesIdAndRefAuthorIdIsNullAndRefSourceIdIsNullOrderBy<Tbl90Reference>(CurrentTbl69FiSpecies.FiSpeciesId);           

            ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
            ReferenceExpertsView.Refresh();
        }          
     
        public void ExecuteSaveReferenceExpert(object o)
        {
             if (_genExpertMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90ReferenceExpert)) return;

            //RefExpertId or RefSourceId or RefAuthorId may be not 0
            if (CurrentTbl90ReferenceExpert.RefExpertId == null &&
                CurrentTbl90ReferenceExpert.RefSourceId == null &&
                CurrentTbl90ReferenceExpert.RefAuthorId == null)
            {
                MessageBox.Show(CultRes.StringsRes.RequiredGenealogyConnect, CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            CurrentTbl90ReferenceExpert.FiSpeciesId = CurrentTbl69FiSpecies.FiSpeciesId;

            try
            {
                var reference = _uow.Tbl90References.GetById(CurrentTbl90ReferenceExpert.ReferenceId);


                if (CurrentTbl90ReferenceExpert.ReferenceId == 0)
                    reference = _extCrud.ReferenceExpertFiSpeciesAdd(CurrentTbl90ReferenceExpert);
                else
                    reference = _extCrud.ReferenceExpertFiSpeciesUpdate(reference, CurrentTbl90ReferenceExpert);

                //        _position = PhylumsView.CurrentPosition;

                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(CurrentTbl90ReferenceExpert.Info))  return;

                try
                {
                    _extCrud.ReferenceExpertSave(reference, CurrentTbl90ReferenceExpert);
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

                _allMessageBoxes.InfoMessageBox("Save Successfull", CurrentTbl90ReferenceExpert.ReferenceId == 0
                    ? CultRes.StringsRes.DatasetNew
                    : CurrentTbl90ReferenceExpert.Info);
            }
            catch (Exception e)
            {
                _allMessageBoxes.WarningMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }

           Tbl90ReferenceExpertsList= _extCrud.GetReferenceExpertsCollectionFromFiSpeciesIdAndRefAuthorIdIsNullAndRefSourceIdIsNullOrderBy<Tbl90Reference>(CurrentTbl69FiSpecies.FiSpeciesId);                     
         
            SelectedMainTabIndex = 6;
     //       SelectedDetailSubTabIndex = 6;
            SelectedMainSubRefTabIndex = 0;

            ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
            ReferenceExpertsView.Refresh();
        }
        #endregion "Public Commands"                           
           
       #region [Commands FiSpecies ==> Tbl93Comments]        
   
       private RelayCommand _addCommentCommand;

        public ICommand AddCommentCommand => _addCommentCommand ??= new RelayCommand(delegate { ExecuteAddComment(null); });

        private RelayCommand _copyCommentCommand;

        public ICommand CopyCommentCommand => _copyCommentCommand ??= new RelayCommand(delegate { ExecuteCopyComment(null); });

        private RelayCommand _deleteCommentCommand;

        public ICommand DeleteCommentCommand => _deleteCommentCommand ??= new RelayCommand(delegate { ExecuteDeleteComment(null); });

        private RelayCommand _saveCommentCommand;

        public ICommand SaveCommentCommand => _saveCommentCommand ??= new RelayCommand(delegate { ExecuteSaveComment(null); });

       #endregion [Commands FiSpecies ==> Tbl93Comments]        
   
     

       #region [Methods FiSpecies ==> Tbl93Comments]        

        public void ExecuteAddComment(object o)
        {
            Tbl93CommentsList ??= new ObservableCollection<Tbl93Comment>();

            Tbl93CommentsList .Insert(0, new Tbl93Comment  { Info = CultRes.StringsRes.DatasetNew });

            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            CommentsView.MoveCurrentToFirst();
         }          
     
        public void ExecuteCopyComment(object o)
        {

            if (_genCommentMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl93Comment)) return;

            Tbl93CommentsList = _extCrud.CopyComment(CurrentTbl93Comment, "Comment");

            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            CommentsView.MoveCurrentToFirst();
        }         
     
        private void ExecuteDeleteComment(object o)
        {
            if (_genCommentMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl93Comment)) return;

            try
            {
                var comment = _uow.Tbl93Comments.GetById(CurrentTbl93Comment.CommentId);
                if (comment != null)
                {
                    if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl93Comment.Info)) return;

                    _extCrud.DeleteComment(comment);

                    _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CurrentTbl93Comment.Info);
                }
                else _allMessageBoxes.InfoMessageBox("Not To Delete", CultRes.StringsRes.DeleteCan + " " + CurrentTbl93Comment.Info + " " + CultRes.StringsRes.DeleteCan1);
            }
            catch (Exception e)
            {
                _allMessageBoxes.InfoMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }

            Tbl93CommentsList = _extCrud.GetCommentsCollectionFromFiSpeciesIdOrderBy<Tbl93Comment>(CurrentTbl93Comment.FiSpeciesId);

            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            CommentsView.Refresh();
        }        
     
        private void ExecuteSaveComment(object o)
        {
            if (_genCommentMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl93Comment)) return;

            CurrentTbl93Comment.FiSpeciesId = CurrentTbl69FiSpecies.FiSpeciesId;

            try
            {
                var comment = _uow.Tbl93Comments.GetById(CurrentTbl93Comment.CommentId);


                if (CurrentTbl93Comment.CommentId == 0)
                    comment = _extCrud.CommentFiSpeciesAdd(CurrentTbl93Comment);
                else
                    comment = _extCrud.CommentFiSpeciesUpdate(comment, CurrentTbl93Comment);

                //        _position = FiSpeciessesView.CurrentPosition;

                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(CurrentTbl93Comment.Info))
                    return;

                try
                {
                    _extCrud.CommentSave(comment, CurrentTbl93Comment);
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

                _allMessageBoxes.InfoMessageBox("Save Successfull", CurrentTbl93Comment.CommentId == 0
                    ? CultRes.StringsRes.DatasetNew
                    : CurrentTbl93Comment.Info);
            }
            catch (Exception e)
            {
                _allMessageBoxes.WarningMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }

            Tbl93CommentsList = _extCrud.GetCommentsCollectionFromFiSpeciesIdOrderBy<Tbl93Comment>(CurrentTbl93Comment.FiSpeciesId);                 
       
            SelectedMainTabIndex = 7;
     //       SelectedDetailSubTabIndex = 7;

            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            CommentsView.Refresh();
        }
        #endregion "Public Commands"                             
 
             
 //    Part 9    


     
        #region "Public Commands Connected Tables by DoubleClick"

        private RelayCommand _getConnectedTablesCommand;
        public ICommand GetConnectedTablesCommand => _getConnectedTablesCommand ??= new RelayCommand(delegate { GetConnectedTablesById(null); });

        #endregion "Public Commands Connected Tables by DoubleClick"

        #region "Public Method Connected Tables by DoubleClick"

        private void GetConnectedTablesById(object o)
        {           
        
            Tbl66GenussesList = _extCrud.GetGenussesCollectionFromGenusIdOrderBy<Tbl66Genus>(CurrentTbl69FiSpecies.GenusId);

            Tbl63InfratribussesAllList = _extCrud.GetCollectionAllOrderBy<Tbl63Infratribus>("infratribus");

            GenussesView = CollectionViewSource.GetDefaultView(Tbl66GenussesList);
            GenussesView.Refresh();

            SelectedMainTabIndex = 0;
            SelectedDetailTabIndex = 2;   
     
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
                    if (CurrentTbl69FiSpecies != null)
                    {
                        Tbl66GenussesList = _extCrud.GetGenussesCollectionFromGenusIdOrderBy<Tbl66Genus>(CurrentTbl69FiSpecies.GenusId);

                        Tbl63InfratribussesAllList = _extCrud.GetCollectionAllOrderBy<Tbl63Infratribus>("infratribus");

                        GenussesView = CollectionViewSource.GetDefaultView(Tbl66GenussesList);
                        GenussesView.Refresh();
                    }
                    SelectedDetailTabIndex = 0;
                }         
       
                if (_selectedMainTabIndex == 1)             
                {
                    if (CurrentTbl69FiSpecies != null)
                    {
                        Tbl68SpeciesgroupsList = _extCrud.GetSpeciesgroupsCollectionFromSpeciesgroupIdOrderBy<Tbl68Speciesgroup>(CurrentTbl69FiSpecies.SpeciesgroupId);

                        SpeciesgroupsView = CollectionViewSource.GetDefaultView(Tbl68SpeciesgroupsList);
                        SpeciesgroupsView.Refresh();
                    }
                    SelectedDetailTabIndex = 1;
                }         
      
                if (_selectedMainTabIndex == 2)
                {
                    if (CurrentTbl69FiSpecies != null)
                    {
                        Tbl78NamesList = _extCrud.GetNamesCollectionFromFiSpeciesIdOrderBy<Tbl78Name>(CurrentTbl69FiSpecies.FiSpeciesId);

                        Tbl69FiSpeciessesAllList = _extCrud.GetCollectionAllOrderBy<Tbl69FiSpecies>("fispecies");

                        NamesView = CollectionViewSource.GetDefaultView(Tbl78NamesList);
                        NamesView.Refresh();
                    }
                    SelectedDetailTabIndex = 3;
                }           
       
                if (_selectedMainTabIndex == 3)
                {
                    if (CurrentTbl69FiSpecies != null)
                    {
                        Tbl81ImagesList = _extCrud.GetImagesCollectionFromFiSpeciesIdOrderBy<Tbl81Image>(CurrentTbl69FiSpecies.FiSpeciesId);

                        Tbl69FiSpeciessesAllList = _extCrud.GetCollectionAllOrderBy<Tbl69FiSpecies>("fispecies");

                        ImagesView = CollectionViewSource.GetDefaultView(Tbl81ImagesList);
                        ImagesView.Refresh();
                    }
                    SelectedDetailTabIndex = 4;
                }           
       
                if (_selectedMainTabIndex == 4)
                {
                    if (CurrentTbl69FiSpecies != null)
                    {
                        Tbl84SynonymsList = _extCrud.GetSynonymsCollectionFromFiSpeciesIdOrderBy<Tbl84Synonym>(CurrentTbl69FiSpecies.FiSpeciesId);

                        Tbl69FiSpeciessesAllList = _extCrud.GetCollectionAllOrderBy<Tbl69FiSpecies>("fispecies");

                        SynonymsView = CollectionViewSource.GetDefaultView(Tbl84SynonymsList);
                        SynonymsView.Refresh();
                    }
                    SelectedDetailTabIndex = 5;
                }           
       
                if (_selectedMainTabIndex == 5)
                {
                    if (CurrentTbl69FiSpecies != null)
                    {
                        Tbl87GeographicsList = _extCrud.GetGeographicsCollectionFromFiSpeciesIdOrderBy<Tbl87Geographic>(CurrentTbl69FiSpecies.FiSpeciesId);

                        Tbl69FiSpeciessesAllList = _extCrud.GetCollectionAllOrderBy<Tbl69FiSpecies>("fispecies");

                        GeographicsView = CollectionViewSource.GetDefaultView(Tbl87GeographicsList);
                        GeographicsView.Refresh();
                    }
                    SelectedDetailTabIndex = 6;
                }           
       
                if (_selectedMainTabIndex == 6)
                {
                    if (CurrentTbl69FiSpecies != null)
                    {
                    }
                    SelectedDetailTabIndex = 7;
                    SelectedMainSubRefTabIndex = 0;
                }           
       
                if (_selectedMainTabIndex == 7)
                {
                    if (CurrentTbl69FiSpecies != null)
                    {
                        Tbl93CommentsList = _extCrud.GetCommentsCollectionFromFiSpeciesIdOrderBy<Tbl93Comment>(CurrentTbl69FiSpecies.FiSpeciesId);

                        CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
                        CommentsView.Refresh();
                    }
                    SelectedDetailTabIndex = 8;
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
                RaisePropertyChanged("");        
     
                if (_selectedDetailTabIndex == 0)
                {
                    if (CurrentTbl69FiSpecies != null)
                    {
                        Tbl66GenussesList = _extCrud.GetGenussesCollectionFromGenusIdOrderBy<Tbl66Genus>(CurrentTbl69FiSpecies.GenusId);

                        GenussesView = CollectionViewSource.GetDefaultView(Tbl66GenussesList);
                        GenussesView.Refresh();
                    }
                    SelectedMainTabIndex = 0;  
               }     
       
                if (_selectedDetailTabIndex == 1)
                {
                    if (CurrentTbl69FiSpecies != null)
                    {
                        Tbl68SpeciesgroupsList = _extCrud.GetSpeciesgroupsCollectionFromSpeciesgroupIdOrderBy<Tbl68Speciesgroup>(CurrentTbl69FiSpecies.SpeciesgroupId);

                        SpeciesgroupsView = CollectionViewSource.GetDefaultView(Tbl68SpeciesgroupsList);
                        SpeciesgroupsView.Refresh(); 
                   }
                    SelectedMainTabIndex = 1;
                }           
       
                if (_selectedDetailTabIndex == 2)                
                {
                    if (CurrentTbl69FiSpecies != null)
                    {
                    }
               }    
       
                if (_selectedDetailTabIndex == 3)
                {
                    if (CurrentTbl69FiSpecies != null)
                    {
                        Tbl78NamesList = _extCrud.GetNamesCollectionFromFiSpeciesIdOrderBy<Tbl78Name>(CurrentTbl69FiSpecies.FiSpeciesId);

                        Tbl69FiSpeciessesAllList = _extCrud.GetCollectionAllOrderBy<Tbl69FiSpecies>("fispecies");

                        NamesView = CollectionViewSource.GetDefaultView(Tbl78NamesList);
                        NamesView.Refresh();
                    }
                    SelectedMainTabIndex = 2;
                    SelectedMainSubRefTabIndex = 0;
                }           
       
                if (_selectedDetailTabIndex == 4)
                {
                    if (CurrentTbl69FiSpecies != null)
                    {
                        Tbl81ImagesList = _extCrud.GetImagesCollectionFromFiSpeciesIdOrderBy<Tbl81Image>(CurrentTbl69FiSpecies.FiSpeciesId);

                        Tbl69FiSpeciessesAllList = _extCrud.GetCollectionAllOrderBy<Tbl69FiSpecies>("fispecies");

                        ImagesView = CollectionViewSource.GetDefaultView(Tbl81ImagesList);
                        ImagesView.Refresh();
                    }
                    SelectedMainTabIndex = 3;
                }           
       
                if (_selectedDetailTabIndex == 5)
                {
                    if (CurrentTbl69FiSpecies != null)
                    {
                        Tbl84SynonymsList = _extCrud.GetSynonymsCollectionFromFiSpeciesIdOrderBy<Tbl84Synonym>(CurrentTbl69FiSpecies.FiSpeciesId);

                        Tbl69FiSpeciessesAllList = _extCrud.GetCollectionAllOrderBy<Tbl69FiSpecies>("fispecies");

                        SynonymsView = CollectionViewSource.GetDefaultView(Tbl84SynonymsList);
                        SynonymsView.Refresh();
                    }
                    SelectedMainTabIndex = 4;
                }           
       
                if (_selectedDetailTabIndex == 6)
                {
                    if (CurrentTbl69FiSpecies != null)
                    {
                        Tbl87GeographicsList = _extCrud.GetGeographicsCollectionFromFiSpeciesIdOrderBy<Tbl87Geographic>(CurrentTbl69FiSpecies.FiSpeciesId);

                        Tbl69FiSpeciessesAllList = _extCrud.GetCollectionAllOrderBy<Tbl69FiSpecies>("fispecies");

                        GeographicsView = CollectionViewSource.GetDefaultView(Tbl87GeographicsList);
                        GeographicsView.Refresh();
                    }
                    SelectedMainTabIndex = 5;
                }           
       
                if (_selectedDetailTabIndex == 7)
                {
                    if (CurrentTbl69FiSpecies != null)
                    {
                        Tbl90ExpertsAllList = new ObservableCollection<Tbl90RefExpert>(_uow.Tbl90RefExperts.ListTbl90RefExpertsOrderBy());

                        Tbl90ReferenceExpertsList = _extCrud
                            .GetReferenceExpertsCollectionFromFiSpeciesIdAndRefAuthorIdIsNullAndRefSourceIdIsNullOrderBy
                                <Tbl90Reference>(CurrentTbl69FiSpecies.FiSpeciesId);

                        ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
                        ReferenceExpertsView.Refresh();
                    }
                    SelectedMainSubRefTabIndex = 0;
                    SelectedMainTabIndex = 6;
                }       
       
                if (_selectedDetailTabIndex == 8)
                {
                    if (CurrentTbl69FiSpecies != null)
                    {
                        Tbl93CommentsList = _extCrud.GetCommentsCollectionFromFiSpeciesIdOrderBy<Tbl93Comment>(CurrentTbl69FiSpecies.FiSpeciesId);

                        CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
                        CommentsView.Refresh();
                    }

                    SelectedMainTabIndex = 7;
                }       
     
            }
        }

        public int SelectedMainSubRefTabIndex
        {
            get => _selectedMainSubRefTabIndex;
            set
            {
                if (value == _selectedMainSubRefTabIndex) return;
                _selectedMainSubRefTabIndex = value;  RaisePropertyChanged("");     
       
                if (_selectedMainSubRefTabIndex == 0)
                {
                    if (CurrentTbl69FiSpecies != null)
                    {
                        Tbl90ExpertsAllList =
                            new ObservableCollection<Tbl90RefExpert>(_uow.Tbl90RefExperts.ListTbl90RefExpertsOrderBy());

                        Tbl90ReferenceExpertsList = _extCrud
                                .GetReferenceExpertsCollectionFromFiSpeciesIdAndRefAuthorIdIsNullAndRefSourceIdIsNullOrderBy
                                    <Tbl90Reference>(CurrentTbl69FiSpecies.FiSpeciesId);

                        ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
                        ReferenceExpertsView.Refresh();
                    }

                    SelectedDetailTabIndex = 7;
                    SelectedMainTabIndex = 6;
                    SelectedDetailSubRefTabIndex = 0;
                }        
       
                if (_selectedMainSubRefTabIndex == 1)
                {
                    if (CurrentTbl69FiSpecies != null)
                    {
                        Tbl90SourcesAllList =
                            new ObservableCollection<Tbl90RefSource>(_uow.Tbl90RefSources.ListTbl90RefSourcesOrderBy());

                        Tbl90ReferenceSourcesList =
                            _extCrud
                                .GetReferenceSourcesCollectionFromFiSpeciesIdAndRefAuthorIdIsNullAndRefExpertIdIsNullOrderBy
                                    <Tbl90Reference>(CurrentTbl69FiSpecies.FiSpeciesId);

                        ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
                        ReferenceSourcesView.Refresh();
                    }

                    SelectedDetailTabIndex = 7;
                    SelectedMainTabIndex = 6;
                    SelectedDetailSubRefTabIndex = 1;
                }      
       
                if (_selectedMainSubRefTabIndex == 2)
                {
                    if (CurrentTbl69FiSpecies != null)
                    {
                        Tbl90AuthorsAllList =
                            new ObservableCollection<Tbl90RefAuthor>(_uow.Tbl90RefAuthors.ListTbl90RefAuthorsOrderBy());

                        Tbl90ReferenceAuthorsList =
                            _extCrud
                                .GetReferenceAuthorsCollectionFromFiSpeciesIdAndRefSourceIdIsNullAndRefExpertIdIsNullOrderBy
                                    <Tbl90Reference>(CurrentTbl69FiSpecies.FiSpeciesId);

                        ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
                        ReferenceAuthorsView.Refresh();
                    }

                    SelectedDetailTabIndex = 7;
                    SelectedMainTabIndex = 6;
                    SelectedDetailSubRefTabIndex = 2;
                }      
       
            }
        }           
       
        public int SelectedDetailSubRefTabIndex
        {
            get => _selectedDetailSubRefTabIndex;
            set
            {
                if (value == _selectedDetailSubRefTabIndex) return;
                _selectedDetailSubRefTabIndex = value; RaisePropertyChanged("");
                if (_selectedDetailSubRefTabIndex == 0)
                {
                    Tbl90ExpertsAllList =
                        new ObservableCollection<Tbl90RefExpert>(_uow.Tbl90RefExperts.ListTbl90RefExpertsOrderBy());

                    Tbl90ReferenceExpertsList =
                        _extCrud
                            .GetReferenceExpertsCollectionFromFiSpeciesIdAndRefAuthorIdIsNullAndRefSourceIdIsNullOrderBy
                                <Tbl90Reference>(CurrentTbl69FiSpecies.FiSpeciesId);

                    ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
                    ReferenceExpertsView.Refresh();

                    SelectedMainSubRefTabIndex = 0;
                }
                if (_selectedDetailSubRefTabIndex == 1)
                {
                    Tbl90SourcesAllList =
                        new ObservableCollection<Tbl90RefSource>(_uow.Tbl90RefSources.ListTbl90RefSourcesOrderBy());

                    Tbl90ReferenceSourcesList =
                        _extCrud
                            .GetReferenceSourcesCollectionFromFiSpeciesIdAndRefAuthorIdIsNullAndRefExpertIdIsNullOrderBy
                                <Tbl90Reference>(CurrentTbl69FiSpecies.FiSpeciesId);

                    ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
                    ReferenceSourcesView.Refresh();

                    SelectedMainSubRefTabIndex = 1;
                }
                if (_selectedDetailSubRefTabIndex == 2)
                {
                    Tbl90AuthorsAllList =
                        new ObservableCollection<Tbl90RefAuthor>(_uow.Tbl90RefAuthors.ListTbl90RefAuthorsOrderBy());

                    Tbl90ReferenceAuthorsList =
                        _extCrud
                            .GetReferenceAuthorsCollectionFromFiSpeciesIdAndRefSourceIdIsNullAndRefExpertIdIsNullOrderBy
                                <Tbl90Reference>(CurrentTbl69FiSpecies.FiSpeciesId);

                    ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
                    ReferenceAuthorsView.Refresh();

                    SelectedMainSubRefTabIndex = 2;
                }    
                     
            }
        }    
        #endregion "Public Commands to open Detail TabItems"          
 

 //    Part 11    

      
        #region "Public Properties Tbl69FiSpecies"

        private string _searchFiSpeciesName = "";
        public string SearchFiSpeciesName
        {
            get => _searchFiSpeciesName; 
            set { _searchFiSpeciesName = value; RaisePropertyChanged("");  }
        }

        public  ICollectionView FiSpeciessesView;
        private   Tbl69FiSpecies CurrentTbl69FiSpecies => FiSpeciessesView?.CurrentItem as Tbl69FiSpecies;

        private ObservableCollection<Tbl69FiSpecies> _tbl69FiSpeciessesList;
        public  ObservableCollection<Tbl69FiSpecies> Tbl69FiSpeciessesList
        {
            get => _tbl69FiSpeciessesList; 
            set {  _tbl69FiSpeciessesList = value; RaisePropertyChanged("");   }
        }

        private ObservableCollection<Tbl69FiSpecies> _tbl69FiSpeciessesAllList;
        public  ObservableCollection<Tbl69FiSpecies> Tbl69FiSpeciessesAllList
        {
            get => _tbl69FiSpeciessesAllList; 
            set {  _tbl69FiSpeciessesAllList = value; RaisePropertyChanged("");   }
        }

        #endregion "Public Properties"   
       
        #region "Public Properties Tbl66Genus"

        public  ICollectionView GenussesView;
        private Tbl66Genus CurrentTbl66Genus => GenussesView?.CurrentItem as Tbl66Genus;

        private ObservableCollection<Tbl66Genus> _tbl66GenussesList;
        public  ObservableCollection<Tbl66Genus> Tbl66GenussesList
        {
            get => _tbl66GenussesList;
            set { _tbl66GenussesList = value; RaisePropertyChanged(""); }
        }

        private ObservableCollection<Tbl66Genus> _tbl66GenussesAllList;
        public  ObservableCollection<Tbl66Genus> Tbl66GenussesAllList
        {
            get => _tbl66GenussesAllList;
            set { _tbl66GenussesAllList = value; RaisePropertyChanged(""); }
        }
        #endregion "Public Properties"   
  
       
        #region "Public Properties Tbl68Speciesgroup"

        public  ICollectionView SpeciesgroupsView;
        private  Tbl68Speciesgroup CurrentTbl68Speciesgroup => SpeciesgroupsView?.CurrentItem as Tbl68Speciesgroup;           

        private ObservableCollection<Tbl68Speciesgroup> _tbl68SpeciesgroupsList;
        public   ObservableCollection<Tbl68Speciesgroup> Tbl68SpeciesgroupsList
        {
            get => _tbl68SpeciesgroupsList; 
            set { _tbl68SpeciesgroupsList = value; RaisePropertyChanged(""); }
        }

        private ObservableCollection<Tbl68Speciesgroup> _tbl68SpeciesgroupsAllList;
        public  ObservableCollection<Tbl68Speciesgroup> Tbl68SpeciesgroupsAllList
        {
            get => _tbl68SpeciesgroupsAllList; 
            set { _tbl68SpeciesgroupsAllList = value; RaisePropertyChanged(""); }       
        }

        #endregion "Public Properties"   
        
        #region "Public Properties Tbl78Name"

        public ICollectionView NamesView;
        private Tbl78Name CurrentTbl78Name => NamesView?.CurrentItem as Tbl78Name;           

        private ObservableCollection<Tbl78Name> _tbl78NamesList;
        public  ObservableCollection<Tbl78Name> Tbl78NamesList
        {
            get => _tbl78NamesList; 
            set { _tbl78NamesList = value; RaisePropertyChanged(""); }
        }
        #endregion "Public Properties"     
        
        #region "Public Properties Tbl81Image"

        public ICollectionView ImagesView;
        private Tbl81Image CurrentTbl81Image => ImagesView?.CurrentItem as Tbl81Image;           

        private ObservableCollection<Tbl81Image> _tbl81ImagesList;
        public  ObservableCollection<Tbl81Image> Tbl81ImagesList
        {
            get => _tbl81ImagesList; 
            set { _tbl81ImagesList = value; RaisePropertyChanged(""); }
        }
        #endregion "Public Properties"     
        
        #region "Public Properties Tbl84Synonym"

        private string _searchSynonymName = string.Empty;
        public string SearchSynonymName
        {
            get => _searchSynonymName; 
            set { _searchSynonymName = value; RaisePropertyChanged(""); }
        }

        public ICollectionView SynonymsView;
        private Tbl84Synonym CurrentTbl84Synonym => SynonymsView?.CurrentItem as Tbl84Synonym;           

        private ObservableCollection<Tbl84Synonym> _tbl84SynonymsList;
        public ObservableCollection<Tbl84Synonym> Tbl84SynonymsList
        {
            get => _tbl84SynonymsList; 
            set { _tbl84SynonymsList = value; RaisePropertyChanged(""); }
        }
        private ObservableCollection<Tbl84Synonym> _tbl84SynonymsAllList;
        public  ObservableCollection<Tbl84Synonym> Tbl84SynonymsAllList
        {
            get => _tbl84SynonymsAllList; 
            set { _tbl84SynonymsAllList = value; RaisePropertyChanged(""); }       
        }

        #endregion "Public Properties"     
        
        #region "Public Properties Tbl87Geographic"

        private string _searchGeographicName = string.Empty;
        public string SearchGeographicName
        {
            get => _searchGeographicName; 
            set { _searchGeographicName = value; RaisePropertyChanged(""); }
        }

        public ICollectionView GeographicsView;
        private Tbl87Geographic CurrentTbl87Geographic => GeographicsView?.CurrentItem as Tbl87Geographic;           

        private ObservableCollection<Tbl87Geographic> _tbl87GeographicsList;
        public ObservableCollection<Tbl87Geographic> Tbl87GeographicsList
        {
            get => _tbl87GeographicsList; 
            set { _tbl87GeographicsList = value; RaisePropertyChanged(""); }
        }
        private ObservableCollection<Tbl87Geographic> _tbl87GeographicsAllList;
        public  ObservableCollection<Tbl87Geographic> Tbl87GeographicsAllList
        {
            get => _tbl87GeographicsAllList; 
            set { _tbl87GeographicsAllList = value; RaisePropertyChanged(""); }       
        }

        #endregion "Public Properties"     
        
        #region "Public Properties Tbl63Infratribus"

        private ObservableCollection<Tbl63Infratribus> _tbl63InfratribussesAllList;
        public  ObservableCollection<Tbl63Infratribus> Tbl63InfratribussesAllList
        {
            get => _tbl63InfratribussesAllList; 
            set { _tbl63InfratribussesAllList = value; RaisePropertyChanged(""); }       
        }

        #endregion "Public Properties"     
        
        #region "Public Properties Tbl60Subtribus"

        private ObservableCollection<Tbl60Subtribus> _tbl60SubtribussesAllList;
        public  ObservableCollection<Tbl60Subtribus> Tbl60SubtribussesAllList
        {
            get => _tbl60SubtribussesAllList; 
            set { _tbl60SubtribussesAllList = value; RaisePropertyChanged(""); }       
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
            set {  _continents = value;  RaisePropertyChanged(""); }
        }

        private Continent _selectedContinent;
        public Continent SelectedContinent
        {
            get => _selectedContinent;
            set  {  _selectedContinent = value;  RaisePropertyChanged("");  }
        }

        public class Continent
        {
            public string Name { get; set; }
        }

        private ObservableCollection<TblCountry> _tblCountriesList;
        public ObservableCollection<TblCountry> TblCountriesList
        {
            get => _tblCountriesList;
            set  {  _tblCountriesList = value;  RaisePropertyChanged("");  }
        }

        #endregion "Private Methods"       
         
        #region Public Properties Tbl90References

        private ObservableCollection<Tbl90Reference> _tbl90ReferencesList;

        public ObservableCollection<Tbl90Reference> Tbl90ReferencesList
        {
            get => _tbl90ReferencesList;
            set { _tbl90ReferencesList = value; RaisePropertyChanged(""); }
        }

        #endregion

        #region "Public Properties Tbl90Author"

        private ObservableCollection<Tbl90RefAuthor> _tbl90AuthorsAllList;
        public  ObservableCollection<Tbl90RefAuthor> Tbl90AuthorsAllList
        {
            get => _tbl90AuthorsAllList; 
            set { _tbl90AuthorsAllList = value; RaisePropertyChanged(""); }
        }

        #endregion "Public Properties "

        #region "Public Properties Tbl90Source"

        private ObservableCollection<Tbl90RefSource> _tbl90SourcesAllList;
        public  ObservableCollection<Tbl90RefSource> Tbl90SourcesAllList
        {
            get => _tbl90SourcesAllList; 
            set { _tbl90SourcesAllList = value; RaisePropertyChanged(""); }
        }

        #endregion "Public Properties "

        #region "Public Properties Tbl90Expert"

        private ObservableCollection<Tbl90RefExpert> _tbl90ExpertsAllList;
        public ObservableCollection<Tbl90RefExpert> Tbl90ExpertsAllList
        {
            get => _tbl90ExpertsAllList; 
            set { _tbl90ExpertsAllList = value; RaisePropertyChanged(""); }
        }

        #endregion "Public Properties "

        #region "Public Properties Tbl90ReferenceAuthor"

        public ICollectionView ReferenceAuthorsView;
        private Tbl90Reference CurrentTbl90ReferenceAuthor => ReferenceAuthorsView?.CurrentItem as Tbl90Reference;

        private ObservableCollection<Tbl90Reference> _tbl90ReferenceAuthorsList;
        public ObservableCollection<Tbl90Reference> Tbl90ReferenceAuthorsList
        {
            get => _tbl90ReferenceAuthorsList; 
            set { _tbl90ReferenceAuthorsList = value; RaisePropertyChanged(""); }
        }

        #endregion "Public Properties"

        #region "Public Properties Tbl90ReferenceSource"

        public ICollectionView ReferenceSourcesView;
        private Tbl90Reference CurrentTbl90ReferenceSource => ReferenceSourcesView?.CurrentItem as Tbl90Reference;

        private ObservableCollection<Tbl90Reference> _tbl90ReferenceSourcesList;
        public ObservableCollection<Tbl90Reference> Tbl90ReferenceSourcesList
        {
            get => _tbl90ReferenceSourcesList; 
            set { _tbl90ReferenceSourcesList = value; RaisePropertyChanged(""); }
        }

        #endregion "Public Properties"

        #region "Public Properties Tbl90ReferenceExpert"

        public ICollectionView ReferenceExpertsView;
        private Tbl90Reference CurrentTbl90ReferenceExpert => ReferenceExpertsView?.CurrentItem as Tbl90Reference;

        private ObservableCollection<Tbl90Reference> _tbl90ReferenceExpertsList;
        public ObservableCollection<Tbl90Reference> Tbl90ReferenceExpertsList
        {
            get => _tbl90ReferenceExpertsList; 
            set { _tbl90ReferenceExpertsList = value; RaisePropertyChanged(""); }
        }

        #endregion "Public Properties"   
         
        #region "Public Properties Tbl93Comment"

        public ICollectionView CommentsView;
        private Tbl93Comment CurrentTbl93Comment => CommentsView?.CurrentItem as Tbl93Comment;

        private ObservableCollection<Tbl93Comment> _tbl93CommentsList;
        public ObservableCollection<Tbl93Comment> Tbl93CommentsList
        {
            get => _tbl93CommentsList; 
            set { _tbl93CommentsList = value; RaisePropertyChanged(""); }
        }

        #endregion "Public Properties"     
      
        #region "Public Property  TblCountry"

        private ObservableCollection<TblCountry> _tblCountriesAllList;
        public ObservableCollection<TblCountry> TblCountriesAllList
        {
            get { return _tblCountriesAllList; }
            set { _tblCountriesAllList = value; RaisePropertyChanged(""); }
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
                 set { _languages = value; RaisePropertyChanged(""); }
             }

             private Language _selectedLanguage;

             public Language SelectedLanguage
             {
                 get => _selectedLanguage;
                 set { _selectedLanguage = value; RaisePropertyChanged(""); }
             }

             public class Language
             {
            public string Name   {    get;   set;    }
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
                 set {  _mimeTypes = value;  RaisePropertyChanged("");  }
             }

             private MimeType _selectedMimeType;
             public MimeType SelectedMimeType
             {
                 get => _selectedMimeType;
                 set  {  _selectedMimeType = value;  RaisePropertyChanged(""); }
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
            set { _selectedPath = value; RaisePropertyChanged(""); }
        }

        private BitmapImage _imageSource;

        public BitmapImage ImageSource
        {
            get => _imageSource;
            set { _imageSource = value; RaisePropertyChanged(""); }
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
