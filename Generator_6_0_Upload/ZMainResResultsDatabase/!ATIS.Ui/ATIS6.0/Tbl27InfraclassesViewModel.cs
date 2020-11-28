using System;
using System.Collections.ObjectModel;
using System.ComponentModel;  

    
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using ATIS.Dal.Models;
using ATIS.Ui.Core;
using ATIS.Ui.Helper;
using ATIS.Ui.Views.Database.CrudHelper;
using ATIS.Ui.Views.Database.DatabaseHelper;
using log4net;
using Microsoft.EntityFrameworkCore;          
    
         //    InfraclassesViewModel Skriptdatum:  08.11.2018  18:32    

namespace ATIS.Ui.Views.Database.ListDetails
{     
    
    public class InfraclassesViewModel : ViewModelBase                     
    {  
        // Version with Generic Unit Of Work and AtisDbContext for general use   
         
        #region [Private Data Members]
        private static readonly ILog Log = LogManager.GetLogger(typeof(InfraclassesViewModel));
        private readonly UnitOfWork _uow = new UnitOfWork(new AtisDbContext());
        private readonly AtisDbContext _context = new AtisDbContext();

        private readonly AllMessageBoxes _allMessageBoxes = new AllMessageBoxes();
        private readonly GenericMessageBoxes<Tbl27Infraclass> _genInfraclassMessageBoxes = new GenericMessageBoxes<Tbl27Infraclass>();
        private readonly GenericMessageBoxes<Tbl24Subclass> _genSubclassMessageBoxes = new GenericMessageBoxes<Tbl24Subclass>();
        private readonly GenericMessageBoxes<Tbl30Legio> _genLegioMessageBoxes = new GenericMessageBoxes<Tbl30Legio>();
        private readonly GenericMessageBoxes<Tbl90Reference> _genExpertMessageBoxes = new GenericMessageBoxes<Tbl90Reference>();
        private readonly GenericMessageBoxes<Tbl90Reference> _genSourceMessageBoxes = new GenericMessageBoxes<Tbl90Reference>();
        private readonly GenericMessageBoxes<Tbl90Reference> _genAuthorMessageBoxes = new GenericMessageBoxes<Tbl90Reference>();
        private readonly GenericMessageBoxes<Tbl93Comment> _genCommentMessageBoxes = new GenericMessageBoxes<Tbl93Comment>();
        private readonly BasicGet _extGet = new BasicGet();
        private readonly BasicCopy _extCopy = new BasicCopy();
        private readonly BasicDelete _extDelete = new BasicDelete();
        private readonly BasicSave _extSave = new BasicSave();        
        private int _position;   
         
        #endregion [Private Data Members]               
      
        #region [Constructor]

        public InfraclassesViewModel()
        {
            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.
            }
            else
            {          
        
                // Code runs "for real" 
                Tbl27InfraclassesList = new ObservableCollection<Tbl27Infraclass>();    
            }
        }     
        public bool IsInDesignMode { get; set; }

        #endregion [Constructor]         
 

 //    Part 1    

         

        #region [Commands Infraclass]

        private RelayCommand _getInfraclassesByNameOrIdCommand;
        public ICommand GetInfraclassesByNameOrIdCommand => _getInfraclassesByNameOrIdCommand ??= new RelayCommand(delegate {ExecuteGetInfraclassesByNameOrId(SearchInfraclassName); });    
             
        private RelayCommand _addInfraclassCommand;
        public ICommand AddInfraclassCommand => _addInfraclassCommand ??= new RelayCommand(delegate { ExecuteAddInfraclass(null); });

        private RelayCommand _copyInfraclassCommand;
        public ICommand CopyInfraclassCommand => _copyInfraclassCommand ??= new RelayCommand(delegate { ExecuteCopyInfraclass(null); });      
             
        private RelayCommand _deleteInfraclassCommand;
        public ICommand DeleteInfraclassCommand => _deleteInfraclassCommand ??= new RelayCommand(delegate { ExecuteDeleteInfraclass(SearchInfraclassName); });    
             
        private RelayCommand _saveInfraclassCommand;
        public ICommand SaveInfraclassCommand => _saveInfraclassCommand ??= new RelayCommand(delegate { ExecuteSaveInfraclass(SearchInfraclassName); });    

        #endregion [Commands Infraclass]       

     
        #region [Methods Infraclass]

        private void ExecuteGetInfraclassesByNameOrId(string searchName)
        {
            Tbl24SubclassesAllList = _extGet.AllCollection<Tbl24Subclass>("subclass");
            Tbl27InfraclassesList = _extGet.SearchNameAndIdReturnCollection<Tbl27Infraclass>(SearchInfraclassName, "infraclass");

            SelectedMainTabIndex = 0;
            SelectedDetailTabIndex = 1;

            InfraclassesView = CollectionViewSource.GetDefaultView(Tbl27InfraclassesList);
            InfraclassesView.Refresh();
        }                     
     
        private void ExecuteAddInfraclass(object o)
        {
            Tbl27InfraclassesList.Insert(0, new Tbl27Infraclass   {   InfraclassName = CultRes.StringsRes.DatasetNew  }  );
            Tbl24SubclassesAllList = _extGet.AllCollection<Tbl24Subclass>("subclass");

            InfraclassesView = CollectionViewSource.GetDefaultView(Tbl27InfraclassesList);
            InfraclassesView.MoveCurrentToFirst();
        }                       
     
        private void ExecuteCopyInfraclass(object o)
        {
            if (_genInfraclassMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl27Infraclass)) return;

            Tbl27InfraclassesList = _extCopy.CopyInfraclass(CurrentTbl27Infraclass);

            // evtl verbundene tabellen-Datensätze auch kopieren Expert, Source, Author und Comment

            InfraclassesView = CollectionViewSource.GetDefaultView(Tbl27InfraclassesList);
            InfraclassesView.MoveCurrentToFirst();
        }                         
     
        private void ExecuteDeleteInfraclass(string searchName)
        {
            if (_genInfraclassMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl27Infraclass)) return;               
 
    
            //check if in Tbl30Legios connected datasets no delete possible, Expert, Sources, Authors and Comment delete and than return

            Tbl30LegiosList = _extDelete.SearchForConnectedDatasetsWithInfraclassIdInTableLegio(CurrentTbl27Infraclass);     
     
            if (_allMessageBoxes.DoNotDeleteDatasetInfoMessageBox(Tbl30LegiosList.Count, "Legio")) return;

            //Delete all References Experts, Sources, Authors  ----------------------------------------------------
            Tbl90ReferencesList = _extDelete.DeleteDatasetsWithInfraclassIdInTableReference(CurrentTbl27Infraclass);
            if (Tbl90ReferencesList.Count > 0)
            {
                if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.ReferenceAuthor + " " + CultRes.StringsRes.ReferenceSource + " " + CultRes.StringsRes.ReferenceSource)) return;

                _extDelete.DeleteReferences(Tbl90ReferencesList);

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CultRes.StringsRes.Reference);
            }

            //Delete all Comments  ----------------------------------------------------
            Tbl93CommentsList = _extDelete.DeleteDatasetsWithInfraclassIdInTableComment(CurrentTbl27Infraclass);
            if (Tbl93CommentsList.Count > 0)
            {
                if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.Comment)) return;

                _extDelete.DeleteComments(Tbl93CommentsList);

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CultRes.StringsRes.Comment);
            }
            try
            {
                var infraclass= _uow.Tbl27Infraclasses.GetById(CurrentTbl27Infraclass.InfraclassId);
                if (infraclass!= null)
                {
                    if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl27Infraclass.InfraclassName)) return;

                    _extDelete.DeleteInfraclass(infraclass);

                    _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CurrentTbl27Infraclass.InfraclassName);
                }
                else _allMessageBoxes.InfoMessageBox("Not To Delete", CultRes.StringsRes.DeleteCan + " " + CurrentTbl27Infraclass.InfraclassName + " " + CultRes.StringsRes.DeleteCan1);
            }
            catch (Exception e)
            {
                _allMessageBoxes.InfoMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }

            ExecuteGetInfraclassesByNameOrId(searchName);

            InfraclassesView.MoveCurrentToFirst();
        }                
     
        private void ExecuteSaveInfraclass(string searchName)
        {
            if (_genInfraclassMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl27Infraclass)) return;      
       
            //Combobox select SubclassID  may be not 0
            if (CurrentTbl27Infraclass.SubclassId == 0)
            {
                MessageBox.Show(CultRes.StringsRes.RequiredGenealogyConnect, CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }     
     
            try
            {
                var infraclass = _uow.Tbl27Infraclasses .GetById(CurrentTbl27Infraclass.InfraclassId);
                //   var phylum = _context.Tbl27Infraclasses.AsNoTracking().FirstOrDefault(a=>a.InfraclassId == CurrentTbl27Infraclass.InfraclassId);
                //          _context.Entry(infraclass).State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;

                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(CurrentTbl27Infraclass.InfraclassName))
                    return;

                if (CurrentTbl27Infraclass.InfraclassId == 0)
                    infraclass = _extSave.InfraclassAdd(CurrentTbl27Infraclass);
                else
                    infraclass = _extSave.InfraclassUpdate(infraclass, CurrentTbl27Infraclass);

                _position = InfraclassesView.CurrentPosition;

                try
                {
                    _extSave.InfraclassSave(infraclass, CurrentTbl27Infraclass);
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

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.SaveSuccess, CurrentTbl27Infraclass.InfraclassId == 0
                    ? "DatasetNew"
                    : CurrentTbl27Infraclass.InfraclassName);
            }
            catch (Exception e)
            {
                _allMessageBoxes.WarningMessageBox(e.Message, CultRes.StringsRes.Error); 
                Log.Error(e);
            }
            ExecuteGetInfraclassesByNameOrId(searchName);
            InfraclassesView.MoveCurrentToPosition(_position);
        }
        #endregion [Methods Infraclass]                
 
 

 //    Part 2    

           
        #region "Public Commands Connect <== Tbl24Subclass"                 
        

        private RelayCommand _saveSubclassCommand;

        public ICommand SaveSubclassCommand => _saveSubclassCommand ??= new RelayCommand(delegate { ExecuteSaveSubclass(null); });        
           
        private void ExecuteSaveSubclass(string searchName)
        {
            if (_genSubclassMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl24Subclass)) return;

            try
            {
                var subclass = _uow.Tbl24Subclasses.GetById(CurrentTbl24Subclass.SubclassId);

                if (CurrentTbl24Subclass.SubclassId == 0)
                    subclass = _extSave.SubclassAdd(CurrentTbl24Subclass);
                else
                    subclass = _extSave.SubclassUpdate(subclass, CurrentTbl24Subclass);

                _position = InfraclassesView.CurrentPosition;   
       
                var cap = CurrentTbl24Subclass.SubclassName;
                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(cap))        return;               
       
                try
                {
                    _extSave.SubclassSave(subclass, CurrentTbl24Subclass);
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
      
                _allMessageBoxes.InfoMessageBox("Save Successfull", CurrentTbl24Subclass.SubclassId == 0
                    ? CultRes.StringsRes.DatasetNew
                    : CurrentTbl24Subclass.SubclassName);
            }       
     
            catch (Exception e)
            {
                _allMessageBoxes.WarningMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }
            ExecuteGetInfraclassesByNameOrId(searchName);
            InfraclassesView.MoveCurrentToPosition(_position);
        }

        #endregion "Public Commands"                  
                                                          

 //    Part 3    

                                                          



 //    Part 4    

           
        #region [Public Commands Connect ==> Tbl30Legio]                 
        
        private RelayCommand _addLegioCommand;
        public ICommand AddLegioCommand => _addLegioCommand ??= new RelayCommand(delegate { ExecuteAddLegio(null); });

        private RelayCommand _copyLegioCommand;
        public ICommand CopyLegioCommand => _copyLegioCommand ??= new RelayCommand(delegate { ExecuteCopyLegio(null); });

        private RelayCommand _deleteLegioCommand;
        public ICommand DeleteLegioCommand => _deleteLegioCommand ??= new RelayCommand(delegate { ExecuteDeleteLegio(SearchInfraclassName); });

        private RelayCommand _saveLegioCommand;
        public ICommand SaveLegioCommand => _saveLegioCommand ??= new RelayCommand(delegate { ExecuteSaveLegio(SearchInfraclassName); });    

        #endregion [Public Commands Connect ==> Tbl30Legio]    

        #region [Public Methods Connect ==> Tbl30Legio]                   
     
        private void ExecuteAddLegio(object o)      
        {
            Tbl30LegiosList.Insert(0, new Tbl30Legio  { LegioName = CultRes.StringsRes.DatasetNew});
            Tbl27InfraclassesAllList = _extGet.AllCollection<Tbl27Infraclass>("infraclass");

            LegiosView = CollectionViewSource.GetDefaultView(Tbl30LegiosList);
            LegiosView.MoveCurrentToFirst();
        }         
     
        private void ExecuteCopyLegio(object o)
        {
            if (_genLegioMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl30Legio)) return;

            Tbl30LegiosList = _extCopy.CopyLegio(CurrentTbl30Legio);

            // evtl verbundene tabellen-Datensätze auch kopieren Expert, Source, Author und Comment

            LegiosView = CollectionViewSource.GetDefaultView(Tbl30LegiosList);
            LegiosView.MoveCurrentToFirst();
        }        
                  
        private void ExecuteDeleteLegio(string searchName)
        {
             if (_genLegioMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl30Legio)) return;

            //check if in Tbl33Ordos connected datasets no delete possible, Expert, Sources, Authors and Comment delete and than return
            Tbl33OrdosList = _extDelete.SearchForConnectedDatasetsWithLegioIdInTableOrdo(CurrentTbl30Legio);
            if (_allMessageBoxes.DoNotDeleteDatasetInfoMessageBox(Tbl33OrdosList.Count, "Ordo")) return;                                                                                                                   
           
            //Delete all References Experts, Sources, Authors  ----------------------------------------------------
            Tbl90ReferencesList = _extDelete.DeleteDatasetsWithLegioIdInTableReference(CurrentTbl30Legio);
            if (Tbl90ReferencesList.Count > 0)
            {
                if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.ReferenceAuthor + " " + CultRes.StringsRes.ReferenceSource + " " + CultRes.StringsRes.ReferenceSource)) return;

                _extDelete.DeleteReferences(Tbl90ReferencesList);

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CultRes.StringsRes.Reference);
            }

            //Delete all Comments  ----------------------------------------------------
            Tbl93CommentsList = _extDelete.DeleteDatasetsWithLegioIdInTableComment(CurrentTbl30Legio);
            if (Tbl93CommentsList.Count > 0)
            {
                if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.Comment)) return;

                _extDelete.DeleteComments(Tbl93CommentsList);

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CultRes.StringsRes.Comment);
            }

            try 
            {
                var legio = _uow.Tbl30Legios.GetById(CurrentTbl30Legio.LegioId);
                if (legio != null)
                {
                    if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl30Legio.LegioName)) return;

                    _extDelete.DeleteLegio(legio);

                    _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CurrentTbl30Legio.LegioName);
                }
                else _allMessageBoxes.InfoMessageBox("Not To Delete", CultRes.StringsRes.DeleteCan + " " + CurrentTbl30Legio.LegioName + " " + CultRes.StringsRes.DeleteCan1);
            }
            catch (Exception e)
            {
                _allMessageBoxes.InfoMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }

            Tbl30LegiosList = _extGet.GetLegiosCollectionOrderByFromInfraclassId<Tbl30Legio>(CurrentTbl30Legio.InfraclassId);

            LegiosView = CollectionViewSource.GetDefaultView(Tbl30LegiosList);
            LegiosView.MoveCurrentToFirst();
        }                 
                  
        private void ExecuteSaveLegio(string searchName)
        {
             if (_genLegioMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl30Legio)) return;

            CurrentTbl30Legio.InfraclassId = CurrentTbl27Infraclass.InfraclassId;                                                                                                                      
              
            try
            {
                var legio = _uow.Tbl30Legios.GetById(CurrentTbl30Legio.LegioId);

                if (CurrentTbl30Legio.LegioId == 0)
                    legio = _extSave.LegioAdd(CurrentTbl30Legio);
                else
                    legio = _extSave.LegioUpdate(legio, CurrentTbl30Legio);

              //  _position = LegiosView.CurrentPosition;

                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(CurrentTbl30Legio.LegioName))  return;

                try
                {
                    _extSave.LegioSave(legio, CurrentTbl30Legio);
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

                _allMessageBoxes.InfoMessageBox("Save Successfull", CurrentTbl30Legio.LegioId == 0
                    ? CultRes.StringsRes.DatasetNew
                    : CurrentTbl30Legio.LegioName);
            }
            catch (Exception e)
            {
                _allMessageBoxes.WarningMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }

            Tbl30LegiosList = _extGet.GetLegiosCollectionOrderByFromInfraclassId<Tbl30Legio>(CurrentTbl30Legio.InfraclassId);

            LegiosView = CollectionViewSource.GetDefaultView(Tbl30LegiosList);
            LegiosView.MoveCurrentToFirst();
        }

        #endregion [Public Methods  Connect ==> Tbl30Legio]                                                                                                                                            
                                                          


 //    Part 5    

                                                          
                      
 //    Part 6    

 
            

 //    Part 7    

 

 //    Part 8    

           
        #region [Commands Infraclass ==> Tbl90Reference Author]

        private RelayCommand _addReferenceAuthorCommand;

        public ICommand AddReferenceAuthorCommand => _addReferenceAuthorCommand ??= new RelayCommand(delegate { ExecuteAddReferenceAuthor(null); });

        private RelayCommand _copyReferenceAuthorCommand;

        public ICommand CopyReferenceAuthorCommand => _copyReferenceAuthorCommand ??= new RelayCommand(delegate { ExecuteCopyReferenceAuthor(null); });

        private RelayCommand _deleteReferenceAuthorCommand;

        public ICommand DeleteReferenceAuthorCommand => _deleteReferenceAuthorCommand ??= new RelayCommand(delegate { ExecuteDeleteReferenceAuthor(null); });

        private RelayCommand _saveReferenceAuthorCommand;

        public ICommand SaveReferenceAuthorCommand => _saveReferenceAuthorCommand ??= new RelayCommand(delegate { ExecuteSaveReferenceAuthor(null); });        

        #endregion [Commands Infraclass ==> Tbl90Reference Author]                
     
        #region [Methods Infraclass ==> Tbl90Reference Author]

        public void ExecuteAddReferenceAuthor(object o)
        {
            Tbl90ReferenceAuthorsList ??= new ObservableCollection<Tbl90Reference>();

            Tbl90AuthorsAllList = _extGet.AllCollection<Tbl90RefAuthor>("author");
            Tbl90ReferenceAuthorsList.Insert(0, new Tbl90Reference   { Info = CultRes.StringsRes.DatasetNew });

            ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
            ReferenceAuthorsView.MoveCurrentToFirst();
         }         
     
        public void ExecuteCopyReferenceAuthor(object o)
        {
            if (_genAuthorMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90ReferenceAuthor)) return;

            Tbl90ReferenceAuthorsList = _extCopy.CopyReferenceInfraclass(CurrentTbl90ReferenceAuthor, "Author");

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

                    _extDelete.DeleteReference(reference);

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

            CurrentTbl90ReferenceAuthor.InfraclassId = CurrentTbl27Infraclass.InfraclassId;

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
                    reference = _extSave.ReferenceAuthorInfraclassAdd(CurrentTbl90ReferenceAuthor);

                else
                    reference = _extSave.ReferenceAuthorInfraclassUpdate(reference, CurrentTbl90ReferenceAuthor);

                //    _position = InfraclassesView.CurrentPosition;

                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(CurrentTbl90ReferenceAuthor.Info))  return;

                try
                {
                    _extSave.ReferenceAuthorSave(reference, CurrentTbl90ReferenceAuthor);
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
           Tbl90ReferenceAuthorsList = _extGet.GetReferenceAuthorsCollectionOrderByFromInfraclassIdAndRefSourceIdIsNullAndRefExpertIdIsNull<Tbl90Reference>(CurrentTbl27Infraclass.InfraclassId);            
     

            ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
            ReferenceAuthorsView.Refresh();
        }
        #endregion [Methods Infraclass ==> Tbl90Reference Author]              
           
        #region [Commands Infraclass ==> Tbl90Reference Source]      

        private RelayCommand _addReferenceSourceCommand;

        public ICommand AddReferenceSourceCommand => _addReferenceSourceCommand ??= new RelayCommand(delegate { ExecuteAddReferenceSource(null); });

        private RelayCommand _copyReferenceSourceCommand;

        public ICommand CopyReferenceSourceCommand => _copyReferenceSourceCommand ??= new RelayCommand(delegate {ExecuteCopyReferenceSource(null); });

        private RelayCommand _deleteReferenceSourceCommand;

        public ICommand DeleteReferenceSourceCommand => _deleteReferenceSourceCommand ??= new RelayCommand(delegate { ExecuteDeleteReferenceSource(null); });

        private RelayCommand _saveReferenceSourceCommand;

        public ICommand SaveReferenceSourceCommand => _saveReferenceSourceCommand ??= new RelayCommand(delegate { ExecuteSaveReferenceSource(null); });

            
        #endregion [Commands Infraclass ==> Tbl90Reference Source]         
     
        #region [Methods Infraclass ==> Tbl90Reference Source]      

        public void ExecuteAddReferenceSource(object o)
        {
            Tbl90ReferenceSourcesList ??= new ObservableCollection<Tbl90Reference>();

            Tbl90SourcesAllList = _extGet.AllCollection<Tbl90RefSource>("source");

            Tbl90ReferenceSourcesList .Insert(0, new Tbl90Reference  { Info = CultRes.StringsRes.DatasetNew });

            ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
            ReferenceSourcesView.MoveCurrentToFirst();
         }         
     
        public void ExecuteCopyReferenceSource(object o)
        {
            if (_genSourceMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90ReferenceSource)) return;

            Tbl90ReferenceAuthorsList = _extCopy.CopyReferenceInfraclass(CurrentTbl90ReferenceSource, "Source");

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

                    _extDelete.DeleteReference(reference);

                    _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CurrentTbl90ReferenceSource.Info);
                }
                else _allMessageBoxes.InfoMessageBox("Not To Delete", CultRes.StringsRes.DeleteCan + " " + CurrentTbl90ReferenceSource.Info + " " + CultRes.StringsRes.DeleteCan1);
            }
            catch (Exception e)
            {
                _allMessageBoxes.InfoMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }

           Tbl90ReferenceSourcesList = _extGet.GetReferenceSourcesCollectionOrderByFromInfraclassIdAndRefAuthorIdIsNullAndRefExpertIdIsNull<Tbl90Reference>(CurrentTbl27Infraclass.InfraclassId);          

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

            CurrentTbl90ReferenceSource.InfraclassId = CurrentTbl27Infraclass.InfraclassId;

            try
            {
                var reference = _uow.Tbl90References.GetById(CurrentTbl90ReferenceSource.ReferenceId);


                if (CurrentTbl90ReferenceSource.ReferenceId == 0)
                    reference = _extSave.ReferenceSourceInfraclassAdd(CurrentTbl90ReferenceSource);
                else
                    reference = _extSave.ReferenceSourceInfraclassUpdate(reference, CurrentTbl90ReferenceSource);

        //        _position = InfraclassesView.CurrentPosition;

                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(CurrentTbl90ReferenceSource.Info))  return;

                try
                {
                    _extSave.ReferenceSourceSave(reference, CurrentTbl90ReferenceSource);

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

           Tbl90ReferenceSourcesList = _extGet.GetReferenceSourcesCollectionOrderByFromInfraclassIdAndRefAuthorIdIsNullAndRefExpertIdIsNull<Tbl90Reference>(CurrentTbl27Infraclass.InfraclassId);            

     

            ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
            ReferenceSourcesView.MoveCurrentToFirst();
        }
        #endregion [Methods Infraclass ==> Tbl90Reference Source]                    
           
        #region [Commands Infraclass ==> Tbl90Reference Expert]                 
 
        private RelayCommand _addReferenceExpertCommand;

        public ICommand AddReferenceExpertCommand => _addReferenceExpertCommand ??= new RelayCommand(delegate { ExecuteAddReferenceExpert(null); });

        private RelayCommand _copyReferenceExpertCommand;

        public ICommand CopyReferenceExpertCommand => _copyReferenceExpertCommand ??= new RelayCommand(delegate { ExecuteCopyReferenceExpert(null); });

        private RelayCommand _deleteReferenceExpertCommand;

        public ICommand DeleteReferenceExpertCommand => _deleteReferenceExpertCommand ??= new RelayCommand(delegate { ExecuteDeleteReferenceExpert(null); });
        private RelayCommand _saveReferenceExpertCommand;

        public ICommand SaveReferenceExpertCommand => _saveReferenceExpertCommand ??= new RelayCommand(delegate { ExecuteSaveReferenceExpert(null); });

        #endregion [Commands Infraclass ==> Tbl90Reference Expert]                    
     
     
        #region [Methods Infraclass ==> Tbl90Reference Expert]                 

        public void ExecuteAddReferenceExpert(object o)
        {
            Tbl90ReferenceExpertsList ??= new ObservableCollection<Tbl90Reference>();

            Tbl90ExpertsAllList = _extGet.AllCollection<Tbl90RefExpert>("expert");
            Tbl90ReferenceExpertsList .Insert(0, new Tbl90Reference   { Info = CultRes.StringsRes.DatasetNew });

            ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
            ReferenceExpertsView.MoveCurrentToFirst();
         }          
     
        public void ExecuteCopyReferenceExpert(object o)
        {
            if (_genExpertMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90ReferenceExpert)) return;

            Tbl90ReferenceExpertsList = _extCopy.CopyReferenceInfraclass(CurrentTbl90ReferenceExpert, "Expert");

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

                    _extDelete.DeleteReference(reference);

                    _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CurrentTbl90ReferenceExpert.Info);
                }
                else _allMessageBoxes.InfoMessageBox("Not To Delete", CultRes.StringsRes.DeleteCan + " " + CurrentTbl90ReferenceExpert.Info + " " + CultRes.StringsRes.DeleteCan1);
            }
            catch (Exception e)
            {
                _allMessageBoxes.InfoMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }

           Tbl90ReferenceExpertsList= _extGet.GetReferenceExpertsCollectionOrderByFromInfraclassIdAndRefAuthorIdIsNullAndRefSourceIdIsNull<Tbl90Reference>(CurrentTbl27Infraclass.InfraclassId);           

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

            CurrentTbl90ReferenceExpert.InfraclassId = CurrentTbl27Infraclass.InfraclassId;

            try
            {
                var reference = _uow.Tbl90References.GetById(CurrentTbl90ReferenceExpert.ReferenceId);


                if (CurrentTbl90ReferenceExpert.ReferenceId == 0)
                    reference = _extSave.ReferenceExpertInfraclassAdd(CurrentTbl90ReferenceExpert);
                else
                    reference = _extSave.ReferenceExpertInfraclassUpdate(reference, CurrentTbl90ReferenceExpert);

                //        _position = PhylumsView.CurrentPosition;

                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(CurrentTbl90ReferenceExpert.Info))  return;

                try
                {
                    _extSave.ReferenceExpertSave(reference, CurrentTbl90ReferenceExpert);
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

           Tbl90ReferenceExpertsList= _extGet.GetReferenceExpertsCollectionOrderByFromInfraclassIdAndRefAuthorIdIsNullAndRefSourceIdIsNull<Tbl90Reference>(CurrentTbl27Infraclass.InfraclassId);                     
     

            ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
            ReferenceExpertsView.MoveCurrentToFirst();
        }
        #endregion [Methods Infraclass ==> Tbl90Reference Expert]                               
           
       #region [Commands Infraclass ==> Tbl93Comments]        
   
       private RelayCommand _addCommentCommand;

        public ICommand AddCommentCommand => _addCommentCommand ??= new RelayCommand(delegate { ExecuteAddComment(null); });

        private RelayCommand _copyCommentCommand;

        public ICommand CopyCommentCommand => _copyCommentCommand ??= new RelayCommand(delegate { ExecuteCopyComment(null); });

        private RelayCommand _deleteCommentCommand;

        public ICommand DeleteCommentCommand => _deleteCommentCommand ??= new RelayCommand(delegate { ExecuteDeleteComment(null); });

        private RelayCommand _saveCommentCommand;

        public ICommand SaveCommentCommand => _saveCommentCommand ??= new RelayCommand(delegate { ExecuteSaveComment(null); });

       #endregion [Commands Infraclass ==> Tbl93Comments]        
   
     

       #region [Methods Infraclass ==> Tbl93Comments]        

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

            Tbl93CommentsList = _extCopy.CopyComment(CurrentTbl93Comment, "Comment");

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

                    _extDelete.DeleteComment(comment);

                    _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CurrentTbl93Comment.Info);
                }
                else _allMessageBoxes.InfoMessageBox("Not To Delete", CultRes.StringsRes.DeleteCan + " " + CurrentTbl93Comment.Info + " " + CultRes.StringsRes.DeleteCan1);
            }
            catch (Exception e)
            {
                _allMessageBoxes.InfoMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }

            Tbl93CommentsList = _extGet.GetCommentsCollectionOrderByFromInfraclassId<Tbl93Comment>(CurrentTbl93Comment.InfraclassId);

            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            CommentsView.Refresh();
        }        
     
        private void ExecuteSaveComment(object o)
        {
            if (_genCommentMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl93Comment)) return;

            CurrentTbl93Comment.InfraclassId = CurrentTbl27Infraclass.InfraclassId;

            try
            {
                var comment = _uow.Tbl93Comments.GetById(CurrentTbl93Comment.CommentId);


                if (CurrentTbl93Comment.CommentId == 0)
                    comment = _extSave.CommentInfraclassAdd(CurrentTbl93Comment);
                else
                    comment = _extSave.CommentInfraclassUpdate(comment, CurrentTbl93Comment);

                //        _position = InfraclassesView.CurrentPosition;

                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(CurrentTbl93Comment.Info))
                    return;

                try
                {
                    _extSave.CommentSave(comment, CurrentTbl93Comment);
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

            Tbl93CommentsList = _extGet.GetCommentsCollectionOrderByFromInfraclassId<Tbl93Comment>(CurrentTbl93Comment.InfraclassId);                 
     

            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            CommentsView.MoveCurrentToFirst();
        }
        #endregion [Methods Infraclass ==> Tbl93Comments]                 
 
             
 //    Part 9    


     
        #region "Public Commands Connected Tables by DoubleClick"

        private RelayCommand _getConnectedTablesCommand;
        public ICommand GetConnectedTablesCommand => _getConnectedTablesCommand ??= new RelayCommand(delegate { GetConnectedTablesById(null); });

        #endregion "Public Commands Connected Tables by DoubleClick"

        #region "Public Method Connected Tables by DoubleClick"

        private void GetConnectedTablesById(object o)
        {           
Tbl24SubclassesList = _extGet.GetSubclassesCollectionOrderByFromSubclassId<Tbl24Subclass>(CurrentTbl27Infraclass.SubclassId);

            Tbl21ClassesAllList = _extGet.AllCollection<Tbl21Class>("");

            SubclassesView = CollectionViewSource.GetDefaultView(Tbl24SubclassesList);
            SubclassesView.Refresh();     
     
        }

        #endregion "Public Method Connected Tables by DoubleClick"     
 


 //    Part 10    

     
        #region "Public Commands to open Detail TabItems"

        private int _selectedMainTabIndex;
        private int _selectedMainSubRefTabIndex;
        private int _selectedDetailTabIndex;

        public  int SelectedMainTabIndex
        {
            get => _selectedMainTabIndex; 
            set
            {
                if (value == _selectedMainTabIndex) return;
                _selectedMainTabIndex = value; RaisePropertyChanged("");        
     
                if (_selectedMainTabIndex == 0)             
                {
                    if (CurrentTbl27Infraclass != null)
                    {
                        Tbl24SubclassesList = _extGet.GetSubclassesCollectionOrderByFromSubclassId<Tbl24Subclass>(CurrentTbl27Infraclass.SubclassId);

                        Tbl21ClassesAllList = _extGet.AllCollection<Tbl21Class>("");

                        SubclassesView = CollectionViewSource.GetDefaultView(Tbl24SubclassesList);
                        SubclassesView.Refresh();
                    }
                    SelectedDetailTabIndex = 0;
                }         
     
                if (_selectedMainTabIndex == 1)
                {
                    if (CurrentTbl27Infraclass != null)
                    {
                        Tbl30LegiosList = _extGet.GetLegiosCollectionOrderByFromInfraclassId<Tbl30Legio>(CurrentTbl27Infraclass.InfraclassId);

                        Tbl27InfraclassesAllList = _extGet.AllCollection<Tbl27Infraclass>("infraclass");

                        LegiosView = CollectionViewSource.GetDefaultView(Tbl30LegiosList);
                        LegiosView.Refresh();
                    }
                    SelectedDetailTabIndex = 2;   
               }      
     
                if (_selectedMainTabIndex == 2)
                {
                        SelectedDetailTabIndex = 3;
                        SelectedMainSubRefTabIndex = 0;                  
                }           
     
                if (_selectedMainTabIndex == 3)
                {
                    if (CurrentTbl27Infraclass != null)
                    {
                        Tbl93CommentsList = _extGet.GetCommentsCollectionOrderByFromInfraclassId<Tbl93Comment>(CurrentTbl27Infraclass.InfraclassId);

                        CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
                        CommentsView.Refresh();
                    }
                    SelectedDetailTabIndex = 6;
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
                    if (CurrentTbl27Infraclass != null)
                    {
                        Tbl24SubclassesList = _extGet.GetSubclassesCollectionOrderByFromSubclassId<Tbl24Subclass>(CurrentTbl27Infraclass.SubclassId);

                        SubclassesView = CollectionViewSource.GetDefaultView(Tbl24SubclassesList);
                        SubclassesView.Refresh();
                    }
                    SelectedMainTabIndex = 0;  
               }     
     
                if (_selectedDetailTabIndex == 1)                
                {
                    SelectedMainTabIndex = 0;
                }    
     
                if (_selectedDetailTabIndex == 2)                
                {
                    if (CurrentTbl27Infraclass != null)
                    {
                        Tbl30LegiosList = _extGet.GetLegiosCollectionOrderByFromInfraclassId<Tbl30Legio>(CurrentTbl27Infraclass.InfraclassId);

                        Tbl27InfraclassesAllList = _extGet.AllCollection<Tbl27Infraclass>("infraclass");

                        LegiosView = CollectionViewSource.GetDefaultView(Tbl30LegiosList);
                        LegiosView.Refresh();
                    }
                    SelectedMainTabIndex = 1;
               }    
     
                if (_selectedDetailTabIndex == 3)
                {
                    if (CurrentTbl27Infraclass != null)
                    {
                        Tbl90ExpertsAllList = new ObservableCollection<Tbl90RefExpert>(_uow.Tbl90RefExperts.ListTbl90RefExpertsOrderBy());

                        Tbl90ReferenceExpertsList = _extGet.GetReferenceExpertsCollectionOrderByFromInfraclassIdAndRefAuthorIdIsNullAndRefSourceIdIsNull<Tbl90Reference>(CurrentTbl27Infraclass.InfraclassId);

                        ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
                        ReferenceExpertsView.Refresh();
                    }
                    SelectedMainTabIndex = 2;
                    SelectedMainSubRefTabIndex = 0;
                }        
     
                if (_selectedDetailTabIndex == 4)
                {
                    if (CurrentTbl27Infraclass != null)
                    {
                        Tbl90SourcesAllList = new ObservableCollection<Tbl90RefSource>(_uow.Tbl90RefSources.ListTbl90RefSourcesOrderBy());

                        Tbl90ReferenceSourcesList = _extGet.GetReferenceSourcesCollectionOrderByFromInfraclassIdAndRefAuthorIdIsNullAndRefExpertIdIsNull<Tbl90Reference>(CurrentTbl27Infraclass.InfraclassId);

                        ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
                        ReferenceSourcesView.Refresh();
                    }
                    SelectedMainTabIndex = 2;
                    SelectedMainSubRefTabIndex = 1;
                }        
     
                if (_selectedDetailTabIndex == 5)
                {
                    if (CurrentTbl27Infraclass != null)
                    {
                        Tbl90AuthorsAllList = new ObservableCollection<Tbl90RefAuthor>(_uow.Tbl90RefAuthors.ListTbl90RefAuthorsOrderBy());

                        Tbl90ReferenceAuthorsList = _extGet.GetReferenceAuthorsCollectionOrderByFromInfraclassIdAndRefSourceIdIsNullAndRefExpertIdIsNull<Tbl90Reference>(CurrentTbl27Infraclass.InfraclassId);

                        ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
                        ReferenceAuthorsView.Refresh();
                    }
                    SelectedMainTabIndex = 2;
                    SelectedMainSubRefTabIndex = 2;
                }       
     
                if (_selectedDetailTabIndex == 6)
                {
                    if (CurrentTbl27Infraclass != null)
                    {
                        Tbl93CommentsList = _extGet.GetCommentsCollectionOrderByFromInfraclassId<Tbl93Comment>(CurrentTbl27Infraclass.InfraclassId);

                        CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
                        CommentsView.Refresh();
                    }
                    SelectedMainTabIndex = 3;
                }       
     
                if (_selectedDetailTabIndex == 7)
                {
                    if (CurrentTbl27Infraclass != null)
                    {
                        Tbl93CommentsList = _extGet.GetCommentsCollectionOrderByFromInfraclassId<Tbl93Comment>(CurrentTbl27Infraclass.InfraclassId);

                        CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
                        CommentsView.Refresh();
                    }
                    SelectedMainTabIndex = 4;
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
                    if (CurrentTbl27Infraclass != null)
                    {
                        Tbl90ExpertsAllList = new ObservableCollection<Tbl90RefExpert>(_uow.Tbl90RefExperts.ListTbl90RefExpertsOrderBy());

                        Tbl90ReferenceExpertsList = _extGet.GetReferenceExpertsCollectionOrderByFromInfraclassIdAndRefAuthorIdIsNullAndRefSourceIdIsNull<Tbl90Reference>(CurrentTbl27Infraclass.InfraclassId);

                        ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
                        ReferenceExpertsView.Refresh();
                    }
                    SelectedDetailTabIndex = 3;
                    SelectedMainTabIndex = 2;
                }        
     
                if (_selectedMainSubRefTabIndex == 1)
                {
                    if (CurrentTbl27Infraclass != null)
                    {
                        Tbl90SourcesAllList = new ObservableCollection<Tbl90RefSource>(_uow.Tbl90RefSources.ListTbl90RefSourcesOrderBy());

                        Tbl90ReferenceSourcesList = _extGet.GetReferenceSourcesCollectionOrderByFromInfraclassIdAndRefAuthorIdIsNullAndRefExpertIdIsNull<Tbl90Reference>(CurrentTbl27Infraclass.InfraclassId);

                        ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
                        ReferenceSourcesView.Refresh();
                    }
                    SelectedDetailTabIndex = 4;
                    SelectedMainTabIndex = 2;
                }      
     
                if (_selectedMainSubRefTabIndex == 2)
                {
                    if (CurrentTbl27Infraclass != null)
                    {
                        Tbl90AuthorsAllList = new ObservableCollection<Tbl90RefAuthor>(_uow.Tbl90RefAuthors.ListTbl90RefAuthorsOrderBy());

                        Tbl90ReferenceAuthorsList = _extGet.GetReferenceAuthorsCollectionOrderByFromInfraclassIdAndRefSourceIdIsNullAndRefExpertIdIsNull<Tbl90Reference>(CurrentTbl27Infraclass.InfraclassId);

                        ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
                        ReferenceAuthorsView.Refresh();
                    }
                    SelectedDetailTabIndex = 5;
                    SelectedMainTabIndex = 2;
                }      
                     
            }
        }    
        #endregion "Public Commands to open Detail TabItems"          
 

 //    Part 11    

     
        #region "Public Properties Tbl27Infraclass"

        private string _searchInfraclassName = "";
        public string SearchInfraclassName
        {
            get => _searchInfraclassName; 
            set { _searchInfraclassName = value; RaisePropertyChanged("");  }
        }

        public  ICollectionView InfraclassesView;
        private   Tbl27Infraclass CurrentTbl27Infraclass => InfraclassesView?.CurrentItem as Tbl27Infraclass;

        private ObservableCollection<Tbl27Infraclass> _tbl27InfraclassesList;
        public  ObservableCollection<Tbl27Infraclass> Tbl27InfraclassesList
        {
            get => _tbl27InfraclassesList; 
            set {  _tbl27InfraclassesList = value; RaisePropertyChanged("");   }
        }

        private ObservableCollection<Tbl27Infraclass> _tbl27InfraclassesAllList;
        public  ObservableCollection<Tbl27Infraclass> Tbl27InfraclassesAllList
        {
            get => _tbl27InfraclassesAllList; 
            set {  _tbl27InfraclassesAllList = value; RaisePropertyChanged("");   }
        }

        private ObservableCollection<Tbl30Legio> _tbl30LegiosAllList;
        public  ObservableCollection<Tbl30Legio> Tbl30LegiosAllList
        {
            get => _tbl30LegiosAllList; 
            set {  _tbl30LegiosAllList = value; RaisePropertyChanged("");   }
        }

        #endregion "Public Properties"   
       
        #region "Public Properties Tbl24Subclass"

        public  ICollectionView SubclassesView;
        private Tbl24Subclass CurrentTbl24Subclass => SubclassesView?.CurrentItem as Tbl24Subclass;           

        private ObservableCollection<Tbl24Subclass> _tbl24SubclassesList;
        public  ObservableCollection<Tbl24Subclass> Tbl24SubclassesList
        {
            get => _tbl24SubclassesList; 
            set { _tbl24SubclassesList = value; RaisePropertyChanged(""); }
        }

        private ObservableCollection<Tbl24Subclass> _tbl24SubclassesAllList;
        public  ObservableCollection<Tbl24Subclass> Tbl24SubclassesAllList
        {
            get => _tbl24SubclassesAllList; 
            set { _tbl24SubclassesAllList = value; RaisePropertyChanged(""); }       
        }

        #endregion "Public Properties"   
        
        #region "Public Properties Tbl30Legio"

        public ICollectionView LegiosView;
        private Tbl30Legio CurrentTbl30Legio => LegiosView?.CurrentItem as Tbl30Legio;           

        private ObservableCollection<Tbl30Legio> _tbl30LegiosList;
        public  ObservableCollection<Tbl30Legio> Tbl30LegiosList
        {
            get => _tbl30LegiosList; 
            set { _tbl30LegiosList = value; RaisePropertyChanged(""); }
        }
        #endregion "Public Properties"     
        
        #region "Public Properties Tbl33Ordo"

        public ICollectionView OrdosView;
        private Tbl33Ordo CurrentTbl33Ordo => OrdosView?.CurrentItem as Tbl33Ordo;           

        private ObservableCollection<Tbl33Ordo> _tbl33OrdosList;
        public  ObservableCollection<Tbl33Ordo> Tbl33OrdosList
        {
            get => _tbl33OrdosList; 
            set { _tbl33OrdosList = value; RaisePropertyChanged(""); }
        }
        #endregion "Public Properties"     
        
        #region "Public Properties Tbl21Class"

        private ObservableCollection<Tbl21Class> _tbl21ClassesAllList;
        public  ObservableCollection<Tbl21Class> Tbl21ClassesAllList
        {
            get => _tbl21ClassesAllList; 
            set { _tbl21ClassesAllList = value; RaisePropertyChanged(""); }       
        }

        #endregion "Public Properties"     
           
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
 

   }
}   
