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
    
         //    SubclassesViewModel Skriptdatum:  13.12.2019  18:32    

namespace ATIS.Ui.Views.Database.ListDetails
{     
    
    public class SubclassesViewModel : ViewModelBase                     
    {  
        // Version with Generic Unit Of Work and AtisDbContext for general use   
         
        #region [Private Data Members]
        private static readonly ILog Log = LogManager.GetLogger(typeof(SubclassesViewModel));
        private readonly UnitOfWork _uow = new UnitOfWork(new AtisDbContext());
        private readonly AtisDbContext _context = new AtisDbContext();

        private readonly AllMessageBoxes _allMessageBoxes = new AllMessageBoxes();
        private readonly GenericMessageBoxes<Tbl24Subclass> _genSubclassMessageBoxes = new GenericMessageBoxes<Tbl24Subclass>();
        private readonly GenericMessageBoxes<Tbl21Class> _genClassMessageBoxes = new GenericMessageBoxes<Tbl21Class>();
        private readonly GenericMessageBoxes<Tbl27Infraclass> _genInfraclassMessageBoxes = new GenericMessageBoxes<Tbl27Infraclass>();
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

        public SubclassesViewModel()
        {
            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.
            }
            else
            {          
        
                // Code runs "for real" 
                Tbl24SubclassesList = new ObservableCollection<Tbl24Subclass>();    
            }
        }     
        public bool IsInDesignMode { get; set; }

        #endregion [Constructor]         
 

 //    Part 1    

         

        #region [Commands Subclass]

        private RelayCommand _getSubclassesByNameOrIdCommand;
        public ICommand GetSubclassesByNameOrIdCommand => _getSubclassesByNameOrIdCommand ??= new RelayCommand(delegate {ExecuteGetSubclassesByNameOrId(SearchSubclassName); });    
             
        private RelayCommand _addSubclassCommand;
        public ICommand AddSubclassCommand => _addSubclassCommand ??= new RelayCommand(delegate { ExecuteAddSubclass(null); });

        private RelayCommand _copySubclassCommand;
        public ICommand CopySubclassCommand => _copySubclassCommand ??= new RelayCommand(delegate { ExecuteCopySubclass(null); });      
             
        private RelayCommand _deleteSubclassCommand;
        public ICommand DeleteSubclassCommand => _deleteSubclassCommand ??= new RelayCommand(delegate { ExecuteDeleteSubclass(SearchSubclassName); });    
             
        private RelayCommand _saveSubclassCommand;
        public ICommand SaveSubclassCommand => _saveSubclassCommand ??= new RelayCommand(delegate { ExecuteSaveSubclass(SearchSubclassName); });    

        #endregion [Commands Subclass]       

     
        #region [Methods Subclass]

        private void ExecuteGetSubclassesByNameOrId(string searchName)
        {
            Tbl21ClassesAllList = _extGet.AllCollection<Tbl21Class>("classe");
            Tbl24SubclassesList = _extGet.SearchNameAndIdReturnCollection<Tbl24Subclass>(SearchSubclassName, "subclass");

            SelectedMainTabIndex = 0;
            SelectedDetailTabIndex = 1;

            SubclassesView = CollectionViewSource.GetDefaultView(Tbl24SubclassesList);
            SubclassesView.Refresh();
        }                     
     
        private void ExecuteAddSubclass(object o)
        {
            Tbl24SubclassesList.Insert(0, new Tbl24Subclass   {   SubclassName = CultRes.StringsRes.DatasetNew  }  );
            Tbl21ClassesAllList = _extGet.AllCollection<Tbl21Class>("classe");

            SubclassesView = CollectionViewSource.GetDefaultView(Tbl24SubclassesList);
            SubclassesView.MoveCurrentToFirst();
        }                       
     
        private void ExecuteCopySubclass(object o)
        {
            if (_genSubclassMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl24Subclass)) return;

            Tbl24SubclassesList = _extCopy.CopySubclass(CurrentTbl24Subclass);

            // evtl verbundene tabellen-Datensätze auch kopieren Expert, Source, Author und Comment

            SubclassesView = CollectionViewSource.GetDefaultView(Tbl24SubclassesList);
            SubclassesView.MoveCurrentToFirst();
        }                         
     
        private void ExecuteDeleteSubclass(string searchName)
        {
            if (_genSubclassMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl24Subclass)) return;               
 
    
            //check if in Tbl27Infraclasses connected datasets no delete possible, Expert, Sources, Authors and Comment delete and than return

            Tbl27InfraclassesList = _extDelete.SearchForConnectedDatasetsWithSubclassIdInTableInfraclass(CurrentTbl24Subclass);     
     
            if (_allMessageBoxes.DoNotDeleteDatasetInfoMessageBox(Tbl27InfraclassesList.Count, "Infraclass")) return;

            //Delete all References Experts, Sources, Authors  ----------------------------------------------------
            Tbl90ReferencesList = _extDelete.DeleteDatasetsWithSubclassIdInTableReference(CurrentTbl24Subclass);
            if (Tbl90ReferencesList.Count > 0)
            {
                if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.ReferenceAuthor + " " + CultRes.StringsRes.ReferenceSource + " " + CultRes.StringsRes.ReferenceSource)) return;

                _extDelete.DeleteReferences(Tbl90ReferencesList);

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CultRes.StringsRes.Reference);
            }

            //Delete all Comments  ----------------------------------------------------
            Tbl93CommentsList = _extDelete.DeleteDatasetsWithSubclassIdInTableComment(CurrentTbl24Subclass);
            if (Tbl93CommentsList.Count > 0)
            {
                if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.Comment)) return;

                _extDelete.DeleteComments(Tbl93CommentsList);

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CultRes.StringsRes.Comment);
            }
            try
            {
                var subclass= _uow.Tbl24Subclasses.GetById(CurrentTbl24Subclass.SubclassId);
                if (subclass!= null)
                {
                    if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl24Subclass.SubclassName)) return;

                    _extDelete.DeleteSubclass(subclass);

                    _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CurrentTbl24Subclass.SubclassName);
                }
                else _allMessageBoxes.InfoMessageBox("Not To Delete", CultRes.StringsRes.DeleteCan + " " + CurrentTbl24Subclass.SubclassName + " " + CultRes.StringsRes.DeleteCan1);
            }
            catch (Exception e)
            {
                _allMessageBoxes.InfoMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }

            ExecuteGetSubclassesByNameOrId(searchName);

            SubclassesView.MoveCurrentToFirst();
        }                
     
        private void ExecuteSaveSubclass(string searchName)
        {
            if (_genSubclassMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl24Subclass)) return;      
       
            //Combobox select ClassID  may be not 0
            if (CurrentTbl24Subclass.ClassId == 0)
            {
                MessageBox.Show(CultRes.StringsRes.RequiredGenealogyConnect, CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }     
     
            try
            {
                var subclass = _uow.Tbl24Subclasses .GetById(CurrentTbl24Subclass.SubclassId);
                //   var phylum = _context.Tbl24Subclasses.AsNoTracking().FirstOrDefault(a=>a.SubclassId == CurrentTbl24Subclass.SubclassId);
                //          _context.Entry(subclass).State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;

                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(CurrentTbl24Subclass.SubclassName))
                    return;

                if (CurrentTbl24Subclass.SubclassId == 0)
                    subclass = _extSave.SubclassAdd(CurrentTbl24Subclass);
                else
                    subclass = _extSave.SubclassUpdate(subclass, CurrentTbl24Subclass);

                _position = SubclassesView.CurrentPosition;

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
                    Log.Error(e);
                    return;
                }

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.SaveSuccess, CurrentTbl24Subclass.SubclassId == 0
                    ? "DatasetNew"
                    : CurrentTbl24Subclass.SubclassName);
            }
            catch (Exception e)
            {
                _allMessageBoxes.WarningMessageBox(e.Message, CultRes.StringsRes.Error); 
                Log.Error(e);
            }
            ExecuteGetSubclassesByNameOrId(searchName);
            SubclassesView.MoveCurrentToPosition(_position);
        }
        #endregion [Methods Subclass]                
 
 

 //    Part 2    

           
        #region "Public Commands Connect <== Tbl21Class"                 
        

        private RelayCommand _saveClassCommand;

        public ICommand SaveClassCommand => _saveClassCommand ??= new RelayCommand(delegate { ExecuteSaveClass(null); });        
           
        private void ExecuteSaveClass(string searchName)
        {
            if (_genClassMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl21Class)) return;

            try
            {
                var classe = _uow.Tbl21Classes.GetById(CurrentTbl21Class.ClassId);

                if (CurrentTbl21Class.ClassId == 0)
                    classe = _extSave.ClassAdd(CurrentTbl21Class);
                else
                    classe = _extSave.ClassUpdate(classe, CurrentTbl21Class);

                _position = SubclassesView.CurrentPosition;   
       
                var cap = CurrentTbl21Class.ClassName;
                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(cap))        return;               
       
                try
                {
                    _extSave.ClassSave(classe, CurrentTbl21Class);
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
      
                _allMessageBoxes.InfoMessageBox("Save Successfull", CurrentTbl21Class.ClassId == 0
                    ? CultRes.StringsRes.DatasetNew
                    : CurrentTbl21Class.ClassName);
            }       
     
            catch (Exception e)
            {
                _allMessageBoxes.WarningMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }
            ExecuteGetSubclassesByNameOrId(searchName);
            SubclassesView.MoveCurrentToPosition(_position);
        }

        #endregion "Public Commands"                  
                                                          

 //    Part 3    

                                                          



 //    Part 4    

           
        #region [Public Commands Connect ==> Tbl27Infraclass]                 
        
        private RelayCommand _addInfraclassCommand;
        public ICommand AddInfraclassCommand => _addInfraclassCommand ??= new RelayCommand(delegate { ExecuteAddInfraclass(null); });

        private RelayCommand _copyInfraclassCommand;
        public ICommand CopyInfraclassCommand => _copyInfraclassCommand ??= new RelayCommand(delegate { ExecuteCopyInfraclass(null); });

        private RelayCommand _deleteInfraclassCommand;
        public ICommand DeleteInfraclassCommand => _deleteInfraclassCommand ??= new RelayCommand(delegate { ExecuteDeleteInfraclass(SearchSubclassName); });

        private RelayCommand _saveInfraclassCommand;
        public ICommand SaveInfraclassCommand => _saveInfraclassCommand ??= new RelayCommand(delegate { ExecuteSaveInfraclass(SearchSubclassName); });    

        #endregion [Public Commands Connect ==> Tbl27Infraclass]    

        #region [Public Methods Connect ==> Tbl27Infraclass]                   
     
        private void ExecuteAddInfraclass(object o)      
        {
            Tbl27InfraclassesList.Insert(0, new Tbl27Infraclass  { InfraclassName = CultRes.StringsRes.DatasetNew});
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
                var infraclass = _uow.Tbl27Infraclasses.GetById(CurrentTbl27Infraclass.InfraclassId);
                if (infraclass != null)
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

            Tbl27InfraclassesList = _extGet.GetInfraclassesCollectionOrderByFromSubclassId<Tbl27Infraclass>(CurrentTbl27Infraclass.SubclassId);

            InfraclassesView = CollectionViewSource.GetDefaultView(Tbl27InfraclassesList);
            InfraclassesView.MoveCurrentToFirst();
        }                 
                  
        private void ExecuteSaveInfraclass(string searchName)
        {
             if (_genInfraclassMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl27Infraclass)) return;

            CurrentTbl27Infraclass.SubclassId = CurrentTbl24Subclass.SubclassId;                                                                                                                      
              
            try
            {
                var infraclass = _uow.Tbl27Infraclasses.GetById(CurrentTbl27Infraclass.InfraclassId);

                if (CurrentTbl27Infraclass.InfraclassId == 0)
                    infraclass = _extSave.InfraclassAdd(CurrentTbl27Infraclass);
                else
                    infraclass = _extSave.InfraclassUpdate(infraclass, CurrentTbl27Infraclass);

              //  _position = InfraclassesView.CurrentPosition;

                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(CurrentTbl27Infraclass.InfraclassName))  return;

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
                    //         Log.Error(e);
                    return;
                }

                _allMessageBoxes.InfoMessageBox("Save Successfull", CurrentTbl27Infraclass.InfraclassId == 0
                    ? CultRes.StringsRes.DatasetNew
                    : CurrentTbl27Infraclass.InfraclassName);
            }
            catch (Exception e)
            {
                _allMessageBoxes.WarningMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }

            Tbl27InfraclassesList = _extGet.GetInfraclassesCollectionOrderByFromSubclassId<Tbl27Infraclass>(CurrentTbl27Infraclass.SubclassId);

            InfraclassesView = CollectionViewSource.GetDefaultView(Tbl27InfraclassesList);
            InfraclassesView.MoveCurrentToFirst();
        }

        #endregion [Public Methods  Connect ==> Tbl27Infraclass]                                                                                                                                            
                                                          


 //    Part 5    

                                                          
                      
 //    Part 6    

 
            

 //    Part 7    

 

 //    Part 8    

           
        #region [Commands Subclass ==> Tbl90Reference Author]

        private RelayCommand _addReferenceAuthorCommand;

        public ICommand AddReferenceAuthorCommand => _addReferenceAuthorCommand ??= new RelayCommand(delegate { ExecuteAddReferenceAuthor(null); });

        private RelayCommand _copyReferenceAuthorCommand;

        public ICommand CopyReferenceAuthorCommand => _copyReferenceAuthorCommand ??= new RelayCommand(delegate { ExecuteCopyReferenceAuthor(null); });

        private RelayCommand _deleteReferenceAuthorCommand;

        public ICommand DeleteReferenceAuthorCommand => _deleteReferenceAuthorCommand ??= new RelayCommand(delegate { ExecuteDeleteReferenceAuthor(null); });

        private RelayCommand _saveReferenceAuthorCommand;

        public ICommand SaveReferenceAuthorCommand => _saveReferenceAuthorCommand ??= new RelayCommand(delegate { ExecuteSaveReferenceAuthor(null); });        

        #endregion [Commands Subclass ==> Tbl90Reference Author]                
     
        #region [Methods Subclass ==> Tbl90Reference Author]

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

            Tbl90ReferenceAuthorsList = _extCopy.CopyReferenceSubclass(CurrentTbl90ReferenceAuthor, "Author");

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

            CurrentTbl90ReferenceAuthor.SubclassId = CurrentTbl24Subclass.SubclassId;

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
                    reference = _extSave.ReferenceAuthorSubclassAdd(CurrentTbl90ReferenceAuthor);

                else
                    reference = _extSave.ReferenceAuthorSubclassUpdate(reference, CurrentTbl90ReferenceAuthor);

                //    _position = SubclassesView.CurrentPosition;

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
           Tbl90ReferenceAuthorsList = _extGet.GetReferenceAuthorsCollectionOrderByFromSubclassIdAndRefSourceIdIsNullAndRefExpertIdIsNull<Tbl90Reference>(CurrentTbl24Subclass.SubclassId);            
     

            ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
            ReferenceAuthorsView.Refresh();
        }
        #endregion [Methods Subclass ==> Tbl90Reference Author]              
           
        #region [Commands Subclass ==> Tbl90Reference Source]      

        private RelayCommand _addReferenceSourceCommand;

        public ICommand AddReferenceSourceCommand => _addReferenceSourceCommand ??= new RelayCommand(delegate { ExecuteAddReferenceSource(null); });

        private RelayCommand _copyReferenceSourceCommand;

        public ICommand CopyReferenceSourceCommand => _copyReferenceSourceCommand ??= new RelayCommand(delegate {ExecuteCopyReferenceSource(null); });

        private RelayCommand _deleteReferenceSourceCommand;

        public ICommand DeleteReferenceSourceCommand => _deleteReferenceSourceCommand ??= new RelayCommand(delegate { ExecuteDeleteReferenceSource(null); });

        private RelayCommand _saveReferenceSourceCommand;

        public ICommand SaveReferenceSourceCommand => _saveReferenceSourceCommand ??= new RelayCommand(delegate { ExecuteSaveReferenceSource(null); });

            
        #endregion [Commands Subclass ==> Tbl90Reference Source]         
     
        #region [Methods Subclass ==> Tbl90Reference Source]      

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

            Tbl90ReferenceAuthorsList = _extCopy.CopyReferenceSubclass(CurrentTbl90ReferenceSource, "Source");

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

           Tbl90ReferenceSourcesList = _extGet.GetReferenceSourcesCollectionOrderByFromSubclassIdAndRefAuthorIdIsNullAndRefExpertIdIsNull<Tbl90Reference>(CurrentTbl24Subclass.SubclassId);          

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

            CurrentTbl90ReferenceSource.SubclassId = CurrentTbl24Subclass.SubclassId;

            try
            {
                var reference = _uow.Tbl90References.GetById(CurrentTbl90ReferenceSource.ReferenceId);


                if (CurrentTbl90ReferenceSource.ReferenceId == 0)
                    reference = _extSave.ReferenceSourceSubclassAdd(CurrentTbl90ReferenceSource);
                else
                    reference = _extSave.ReferenceSourceSubclassUpdate(reference, CurrentTbl90ReferenceSource);

        //        _position = SubclassesView.CurrentPosition;

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

           Tbl90ReferenceSourcesList = _extGet.GetReferenceSourcesCollectionOrderByFromSubclassIdAndRefAuthorIdIsNullAndRefExpertIdIsNull<Tbl90Reference>(CurrentTbl24Subclass.SubclassId);            

     

            ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
            ReferenceSourcesView.MoveCurrentToFirst();
        }
        #endregion [Methods Subclass ==> Tbl90Reference Source]                    
           
        #region [Commands Subclass ==> Tbl90Reference Expert]                 
 
        private RelayCommand _addReferenceExpertCommand;

        public ICommand AddReferenceExpertCommand => _addReferenceExpertCommand ??= new RelayCommand(delegate { ExecuteAddReferenceExpert(null); });

        private RelayCommand _copyReferenceExpertCommand;

        public ICommand CopyReferenceExpertCommand => _copyReferenceExpertCommand ??= new RelayCommand(delegate { ExecuteCopyReferenceExpert(null); });

        private RelayCommand _deleteReferenceExpertCommand;

        public ICommand DeleteReferenceExpertCommand => _deleteReferenceExpertCommand ??= new RelayCommand(delegate { ExecuteDeleteReferenceExpert(null); });
        private RelayCommand _saveReferenceExpertCommand;

        public ICommand SaveReferenceExpertCommand => _saveReferenceExpertCommand ??= new RelayCommand(delegate { ExecuteSaveReferenceExpert(null); });

        #endregion [Commands Subclass ==> Tbl90Reference Expert]                    
     
     
        #region [Methods Subclass ==> Tbl90Reference Expert]                 

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

            Tbl90ReferenceExpertsList = _extCopy.CopyReferenceSubclass(CurrentTbl90ReferenceExpert, "Expert");

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

           Tbl90ReferenceExpertsList= _extGet.GetReferenceExpertsCollectionOrderByFromSubclassIdAndRefAuthorIdIsNullAndRefSourceIdIsNull<Tbl90Reference>(CurrentTbl24Subclass.SubclassId);           

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

            CurrentTbl90ReferenceExpert.SubclassId = CurrentTbl24Subclass.SubclassId;

            try
            {
                var reference = _uow.Tbl90References.GetById(CurrentTbl90ReferenceExpert.ReferenceId);


                if (CurrentTbl90ReferenceExpert.ReferenceId == 0)
                    reference = _extSave.ReferenceExpertSubclassAdd(CurrentTbl90ReferenceExpert);
                else
                    reference = _extSave.ReferenceExpertSubclassUpdate(reference, CurrentTbl90ReferenceExpert);

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

           Tbl90ReferenceExpertsList= _extGet.GetReferenceExpertsCollectionOrderByFromSubclassIdAndRefAuthorIdIsNullAndRefSourceIdIsNull<Tbl90Reference>(CurrentTbl24Subclass.SubclassId);                     
     

            ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
            ReferenceExpertsView.MoveCurrentToFirst();
        }
        #endregion [Methods Subclass ==> Tbl90Reference Expert]                               
           
       #region [Commands Subclass ==> Tbl93Comments]        
   
       private RelayCommand _addCommentCommand;

        public ICommand AddCommentCommand => _addCommentCommand ??= new RelayCommand(delegate { ExecuteAddComment(null); });

        private RelayCommand _copyCommentCommand;

        public ICommand CopyCommentCommand => _copyCommentCommand ??= new RelayCommand(delegate { ExecuteCopyComment(null); });

        private RelayCommand _deleteCommentCommand;

        public ICommand DeleteCommentCommand => _deleteCommentCommand ??= new RelayCommand(delegate { ExecuteDeleteComment(null); });

        private RelayCommand _saveCommentCommand;

        public ICommand SaveCommentCommand => _saveCommentCommand ??= new RelayCommand(delegate { ExecuteSaveComment(null); });

       #endregion [Commands Subclass ==> Tbl93Comments]        
   
     

       #region [Methods Subclass ==> Tbl93Comments]        

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

            Tbl93CommentsList = _extGet.GetCommentsCollectionOrderByFromSubclassId<Tbl93Comment>(CurrentTbl93Comment.SubclassId);

            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            CommentsView.Refresh();
        }        
     
        private void ExecuteSaveComment(object o)
        {
            if (_genCommentMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl93Comment)) return;

            CurrentTbl93Comment.SubclassId = CurrentTbl24Subclass.SubclassId;

            try
            {
                var comment = _uow.Tbl93Comments.GetById(CurrentTbl93Comment.CommentId);


                if (CurrentTbl93Comment.CommentId == 0)
                    comment = _extSave.CommentSubclassAdd(CurrentTbl93Comment);
                else
                    comment = _extSave.CommentSubclassUpdate(comment, CurrentTbl93Comment);

                //        _position = SubclassesView.CurrentPosition;

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

            Tbl93CommentsList = _extGet.GetCommentsCollectionOrderByFromSubclassId<Tbl93Comment>(CurrentTbl93Comment.SubclassId);                 
     

            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            CommentsView.MoveCurrentToFirst();
        }
        #endregion [Methods Subclass ==> Tbl93Comments]                 
 
             
 //    Part 9    


     
        #region "Public Commands Connected Tables by DoubleClick"

        private RelayCommand _getConnectedTablesCommand;
        public ICommand GetConnectedTablesCommand => _getConnectedTablesCommand ??= new RelayCommand(delegate { GetConnectedTablesById(null); });

        #endregion "Public Commands Connected Tables by DoubleClick"

        #region "Public Method Connected Tables by DoubleClick"

        private void GetConnectedTablesById(object o)
        {           
Tbl21ClassesList = _extGet.GetClassesCollectionOrderByFromClassId<Tbl21Class>(CurrentTbl24Subclass.ClassId);

            Tbl18SuperclassesAllList = _extGet.AllCollection<Tbl18Superclass>("");

            ClassesView = CollectionViewSource.GetDefaultView(Tbl21ClassesList);
            ClassesView.Refresh();     
     
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
                    if (CurrentTbl24Subclass != null)
                    {
                        Tbl21ClassesList = _extGet.GetClassesCollectionOrderByFromClassId<Tbl21Class>(CurrentTbl24Subclass.ClassId);

                        Tbl18SuperclassesAllList = _extGet.AllCollection<Tbl18Superclass>("");

                        ClassesView = CollectionViewSource.GetDefaultView(Tbl21ClassesList);
                        ClassesView.Refresh();
                    }
                    SelectedDetailTabIndex = 0;
                }         
     
                if (_selectedMainTabIndex == 1)
                {
                    if (CurrentTbl24Subclass != null)
                    {
                        Tbl27InfraclassesList = _extGet.GetInfraclassesCollectionOrderByFromSubclassId<Tbl27Infraclass>(CurrentTbl24Subclass.SubclassId);

                        Tbl24SubclassesAllList = _extGet.AllCollection<Tbl24Subclass>("subclass");

                        InfraclassesView = CollectionViewSource.GetDefaultView(Tbl27InfraclassesList);
                        InfraclassesView.Refresh();
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
                    if (CurrentTbl24Subclass != null)
                    {
                        Tbl93CommentsList = _extGet.GetCommentsCollectionOrderByFromSubclassId<Tbl93Comment>(CurrentTbl24Subclass.SubclassId);

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
                    if (CurrentTbl24Subclass != null)
                    {
                        Tbl21ClassesList = _extGet.GetClassesCollectionOrderByFromClassId<Tbl21Class>(CurrentTbl24Subclass.ClassId);

                        ClassesView = CollectionViewSource.GetDefaultView(Tbl21ClassesList);
                        ClassesView.Refresh();
                    }
                    SelectedMainTabIndex = 0;  
               }     
     
                if (_selectedDetailTabIndex == 1)                
                {
                    SelectedMainTabIndex = 0;
                }    
     
                if (_selectedDetailTabIndex == 2)                
                {
                    if (CurrentTbl24Subclass != null)
                    {
                        Tbl27InfraclassesList = _extGet.GetInfraclassesCollectionOrderByFromSubclassId<Tbl27Infraclass>(CurrentTbl24Subclass.SubclassId);

                        Tbl24SubclassesAllList = _extGet.AllCollection<Tbl24Subclass>("subclass");

                        InfraclassesView = CollectionViewSource.GetDefaultView(Tbl27InfraclassesList);
                        InfraclassesView.Refresh();
                    }
                    SelectedMainTabIndex = 1;
               }    
     
                if (_selectedDetailTabIndex == 3)
                {
                    if (CurrentTbl24Subclass != null)
                    {
                        Tbl90ExpertsAllList = new ObservableCollection<Tbl90RefExpert>(_uow.Tbl90RefExperts.ListTbl90RefExpertsOrderBy());

                        Tbl90ReferenceExpertsList = _extGet.GetReferenceExpertsCollectionOrderByFromSubclassIdAndRefAuthorIdIsNullAndRefSourceIdIsNull<Tbl90Reference>(CurrentTbl24Subclass.SubclassId);

                        ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
                        ReferenceExpertsView.Refresh();
                    }
                    SelectedMainTabIndex = 2;
                    SelectedMainSubRefTabIndex = 0;
                }        
     
                if (_selectedDetailTabIndex == 4)
                {
                    if (CurrentTbl24Subclass != null)
                    {
                        Tbl90SourcesAllList = new ObservableCollection<Tbl90RefSource>(_uow.Tbl90RefSources.ListTbl90RefSourcesOrderBy());

                        Tbl90ReferenceSourcesList = _extGet.GetReferenceSourcesCollectionOrderByFromSubclassIdAndRefAuthorIdIsNullAndRefExpertIdIsNull<Tbl90Reference>(CurrentTbl24Subclass.SubclassId);

                        ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
                        ReferenceSourcesView.Refresh();
                    }
                    SelectedMainTabIndex = 2;
                    SelectedMainSubRefTabIndex = 1;
                }        
     
                if (_selectedDetailTabIndex == 5)
                {
                    if (CurrentTbl24Subclass != null)
                    {
                        Tbl90AuthorsAllList = new ObservableCollection<Tbl90RefAuthor>(_uow.Tbl90RefAuthors.ListTbl90RefAuthorsOrderBy());

                        Tbl90ReferenceAuthorsList = _extGet.GetReferenceAuthorsCollectionOrderByFromSubclassIdAndRefSourceIdIsNullAndRefExpertIdIsNull<Tbl90Reference>(CurrentTbl24Subclass.SubclassId);

                        ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
                        ReferenceAuthorsView.Refresh();
                    }
                    SelectedMainTabIndex = 2;
                    SelectedMainSubRefTabIndex = 2;
                }       
     
                if (_selectedDetailTabIndex == 6)
                {
                    if (CurrentTbl24Subclass != null)
                    {
                        Tbl93CommentsList = _extGet.GetCommentsCollectionOrderByFromSubclassId<Tbl93Comment>(CurrentTbl24Subclass.SubclassId);

                        CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
                        CommentsView.Refresh();
                    }
                    SelectedMainTabIndex = 3;
                }       
     
                if (_selectedDetailTabIndex == 7)
                {
                    if (CurrentTbl24Subclass != null)
                    {
                        Tbl93CommentsList = _extGet.GetCommentsCollectionOrderByFromSubclassId<Tbl93Comment>(CurrentTbl24Subclass.SubclassId);

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
                    if (CurrentTbl24Subclass != null)
                    {
                        Tbl90ExpertsAllList = new ObservableCollection<Tbl90RefExpert>(_uow.Tbl90RefExperts.ListTbl90RefExpertsOrderBy());

                        Tbl90ReferenceExpertsList = _extGet.GetReferenceExpertsCollectionOrderByFromSubclassIdAndRefAuthorIdIsNullAndRefSourceIdIsNull<Tbl90Reference>(CurrentTbl24Subclass.SubclassId);

                        ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
                        ReferenceExpertsView.Refresh();
                    }
                    SelectedDetailTabIndex = 3;
                    SelectedMainTabIndex = 2;
                }        
     
                if (_selectedMainSubRefTabIndex == 1)
                {
                    if (CurrentTbl24Subclass != null)
                    {
                        Tbl90SourcesAllList = new ObservableCollection<Tbl90RefSource>(_uow.Tbl90RefSources.ListTbl90RefSourcesOrderBy());

                        Tbl90ReferenceSourcesList = _extGet.GetReferenceSourcesCollectionOrderByFromSubclassIdAndRefAuthorIdIsNullAndRefExpertIdIsNull<Tbl90Reference>(CurrentTbl24Subclass.SubclassId);

                        ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
                        ReferenceSourcesView.Refresh();
                    }
                    SelectedDetailTabIndex = 4;
                    SelectedMainTabIndex = 2;
                }      
     
                if (_selectedMainSubRefTabIndex == 2)
                {
                    if (CurrentTbl24Subclass != null)
                    {
                        Tbl90AuthorsAllList = new ObservableCollection<Tbl90RefAuthor>(_uow.Tbl90RefAuthors.ListTbl90RefAuthorsOrderBy());

                        Tbl90ReferenceAuthorsList = _extGet.GetReferenceAuthorsCollectionOrderByFromSubclassIdAndRefSourceIdIsNullAndRefExpertIdIsNull<Tbl90Reference>(CurrentTbl24Subclass.SubclassId);

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

     
        #region "Public Properties Tbl24Subclass"

        private string _searchSubclassName = "";
        public string SearchSubclassName
        {
            get => _searchSubclassName; 
            set { _searchSubclassName = value; RaisePropertyChanged("");  }
        }

        public  ICollectionView SubclassesView;
        private   Tbl24Subclass CurrentTbl24Subclass => SubclassesView?.CurrentItem as Tbl24Subclass;

        private ObservableCollection<Tbl24Subclass> _tbl24SubclassesList;
        public  ObservableCollection<Tbl24Subclass> Tbl24SubclassesList
        {
            get => _tbl24SubclassesList; 
            set {  _tbl24SubclassesList = value; RaisePropertyChanged("");   }
        }

        private ObservableCollection<Tbl24Subclass> _tbl24SubclassesAllList;
        public  ObservableCollection<Tbl24Subclass> Tbl24SubclassesAllList
        {
            get => _tbl24SubclassesAllList; 
            set {  _tbl24SubclassesAllList = value; RaisePropertyChanged("");   }
        }

        private ObservableCollection<Tbl27Infraclass> _tbl27InfraclassesAllList;
        public  ObservableCollection<Tbl27Infraclass> Tbl27InfraclassesAllList
        {
            get => _tbl27InfraclassesAllList; 
            set {  _tbl27InfraclassesAllList = value; RaisePropertyChanged("");   }
        }

        #endregion "Public Properties"   
       
        #region "Public Properties Tbl21Class"

        public  ICollectionView ClassesView;
        private Tbl21Class CurrentTbl21Class => ClassesView?.CurrentItem as Tbl21Class;           

        private ObservableCollection<Tbl21Class> _tbl21ClassesList;
        public  ObservableCollection<Tbl21Class> Tbl21ClassesList
        {
            get => _tbl21ClassesList; 
            set { _tbl21ClassesList = value; RaisePropertyChanged(""); }
        }

        private ObservableCollection<Tbl21Class> _tbl21ClassesAllList;
        public  ObservableCollection<Tbl21Class> Tbl21ClassesAllList
        {
            get => _tbl21ClassesAllList; 
            set { _tbl21ClassesAllList = value; RaisePropertyChanged(""); }       
        }

        #endregion "Public Properties"   
        
        #region "Public Properties Tbl27Infraclass"

        public ICollectionView InfraclassesView;
        private Tbl27Infraclass CurrentTbl27Infraclass => InfraclassesView?.CurrentItem as Tbl27Infraclass;           

        private ObservableCollection<Tbl27Infraclass> _tbl27InfraclassesList;
        public  ObservableCollection<Tbl27Infraclass> Tbl27InfraclassesList
        {
            get => _tbl27InfraclassesList; 
            set { _tbl27InfraclassesList = value; RaisePropertyChanged(""); }
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
        
        #region "Public Properties Tbl18Superclass"

        private ObservableCollection<Tbl18Superclass> _tbl18SuperclassesAllList;
        public  ObservableCollection<Tbl18Superclass> Tbl18SuperclassesAllList
        {
            get => _tbl18SuperclassesAllList; 
            set { _tbl18SuperclassesAllList = value; RaisePropertyChanged(""); }       
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
