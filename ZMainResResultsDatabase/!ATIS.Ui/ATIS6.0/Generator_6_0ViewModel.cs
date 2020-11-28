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
    
         //    RegnumsViewModel Skriptdatum:  27.11.2020  12:32      

namespace ATIS.Ui.Views.Database.D03Regnum
{     
    
    public class RegnumsViewModel : ViewModelBase                     
    {  
        // Version with Generic Unit Of Work and AtisDbContext for general use   
       
        #region [Private Data Members]
        private static readonly ILog Log = LogManager.GetLogger(typeof(RegnumsViewModel));
        private readonly UnitOfWork _uow = new UnitOfWork(new AtisDbContext());
        private readonly AtisDbContext _context = new AtisDbContext();

        private readonly AllMessageBoxes _allMessageBoxes = new AllMessageBoxes();
        private readonly GenericMessageBoxes<Tbl03Regnum> _genRegnumMessageBoxes = new GenericMessageBoxes<Tbl03Regnum>();
        private readonly GenericMessageBoxes<Tbl06Phylum> _genPhylumMessageBoxes = new GenericMessageBoxes<Tbl06Phylum>();
        private readonly GenericMessageBoxes<Tbl09Division> _genDivisionMessageBoxes = new GenericMessageBoxes<Tbl09Division>();
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

        public RegnumsViewModel()
        {
            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.
            }
            else
            {          
        
                // Code runs "for real" 
                Tbl03RegnumsList = new ObservableCollection<Tbl03Regnum>();    
            }
        }     
        public bool IsInDesignMode { get; set; }

        #endregion [Constructor]         
 

 //    Part 1    

         

        #region [Commands Regnum]

        private RelayCommand _getRegnumsByNameOrIdCommand;
        public ICommand GetRegnumsByNameOrIdCommand => _getRegnumsByNameOrIdCommand ??= new RelayCommand(delegate {ExecuteGetRegnumsByNameOrId(SearchRegnumName); });    
             
        private RelayCommand _addRegnumCommand;
        public ICommand AddRegnumCommand => _addRegnumCommand ??= new RelayCommand(delegate { ExecuteAddRegnum(null); });

        private RelayCommand _copyRegnumCommand;
        public ICommand CopyRegnumCommand => _copyRegnumCommand ??= new RelayCommand(delegate { ExecuteCopyRegnum(null); });      
             
        private RelayCommand _deleteRegnumCommand;
        public ICommand DeleteRegnumCommand => _deleteRegnumCommand ??= new RelayCommand(delegate { ExecuteDeleteRegnum(SearchRegnumName); });    
             
        private RelayCommand _saveRegnumCommand;
        public ICommand SaveRegnumCommand => _saveRegnumCommand ??= new RelayCommand(delegate { ExecuteSaveRegnum(SearchRegnumName); });    

        #endregion [Commands Regnum]       

        
        #region [Methods Regnum]

        private void ExecuteGetRegnumsByNameOrId(string searchName)
       {
            Tbl03RegnumsList = _extGet.SearchNameAndIdReturnCollection<Tbl03Regnum>(SearchRegnumName, "regnum");

            SelectedMainTabIndex = 0;
            SelectedDetailTabIndex = 0;

            RegnumsView = CollectionViewSource.GetDefaultView(Tbl03RegnumsList);
            RegnumsView.Refresh();
        }                       
        
        private void ExecuteAddRegnum(object o)
        {
            Tbl03RegnumsList.Insert(0, new Tbl03Regnum   {   RegnumName = CultRes.StringsRes.DatasetNew  }  );

            RegnumsView = CollectionViewSource.GetDefaultView(Tbl03RegnumsList);
            RegnumsView.MoveCurrentToFirst();
        }                          
     
        private void ExecuteCopyRegnum(object o)
        {
            if (_genRegnumMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl03Regnum)) return;

            Tbl03RegnumsList = _extCopy.CopyRegnum(CurrentTbl03Regnum);

            // evtl verbundene tabellen-Datensätze auch kopieren Expert, Source, Author und Comment

            RegnumsView = CollectionViewSource.GetDefaultView(Tbl03RegnumsList);
            RegnumsView.MoveCurrentToFirst();
        }                         
     
        private void ExecuteDeleteRegnum(string searchName)
        {
            if (_genRegnumMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl03Regnum)) return;               
        
            //check if in Tbl06Phylums connected datasets no delete possible, Expert, Sources, Authors and Comment delete and than return

            Tbl06PhylumsList = _extDelete.SearchForConnectedDatasetsWithRegnumIdInTablePhylum(CurrentTbl03Regnum);
            Tbl09DivisionsList = _extDelete.SearchForConnectedDatasetsWithRegnumIdInTableDivision(CurrentTbl03Regnum);      
     
            if (_allMessageBoxes.DoNotDeleteDatasetInfoMessageBox(Tbl06PhylumsList.Count, "Phylum")) return;

            //Delete all References Experts, Sources, Authors  ----------------------------------------------------
            Tbl90ReferencesList = _extDelete.DeleteDatasetsWithRegnumIdInTableReference(CurrentTbl03Regnum);
            if (Tbl90ReferencesList.Count > 0)
            {
                if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.ReferenceAuthor + " " + CultRes.StringsRes.ReferenceSource + " " + CultRes.StringsRes.ReferenceSource)) return;

                _extDelete.DeleteReferences(Tbl90ReferencesList);

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CultRes.StringsRes.Reference);
            }

            //Delete all Comments  ----------------------------------------------------
            Tbl93CommentsList = _extDelete.DeleteDatasetsWithRegnumIdInTableComment(CurrentTbl03Regnum);
            if (Tbl93CommentsList.Count > 0)
            {
                if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.Comment)) return;

                _extDelete.DeleteComments(Tbl93CommentsList);

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CultRes.StringsRes.Comment);
            }
            try
            {
                var regnum= _uow.Tbl03Regnums.GetById(CurrentTbl03Regnum.RegnumId);
                if (regnum!= null)
                {
                    if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl03Regnum.RegnumName)) return;

                    _extDelete.DeleteRegnum(regnum);

                    _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CurrentTbl03Regnum.RegnumName);
                }
                else _allMessageBoxes.InfoMessageBox("Not To Delete", CultRes.StringsRes.DeleteCan + " " + CurrentTbl03Regnum.RegnumName + " " + CultRes.StringsRes.DeleteCan1);
            }
            catch (Exception e)
            {
                _allMessageBoxes.InfoMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }

            ExecuteGetRegnumsByNameOrId(searchName);

            RegnumsView.MoveCurrentToFirst();
        }                
     
        private void ExecuteSaveRegnum(string searchName)
        {
            if (_genRegnumMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl03Regnum)) return;      
     
            try
            {
                var regnum = _uow.Tbl03Regnums .GetById(CurrentTbl03Regnum.RegnumId);
                //   var phylum = _context.Tbl03Regnums.AsNoTracking().FirstOrDefault(a=>a.RegnumId == CurrentTbl03Regnum.RegnumId);
                //          _context.Entry(regnum).State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;

                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(CurrentTbl03Regnum.RegnumName))
                    return;

                if (CurrentTbl03Regnum.RegnumId == 0)
                    regnum = _extSave.RegnumAdd(CurrentTbl03Regnum);
                else
                    regnum = _extSave.RegnumUpdate(regnum, CurrentTbl03Regnum);

                _position = RegnumsView.CurrentPosition;

                try
                {
                    _extSave.RegnumSave(regnum, CurrentTbl03Regnum);
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

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.SaveSuccess, CurrentTbl03Regnum.RegnumId == 0
                    ? "DatasetNew"
                    : CurrentTbl03Regnum.RegnumName);
            }
            catch (Exception e)
            {
                _allMessageBoxes.WarningMessageBox(e.Message, CultRes.StringsRes.Error); 
                Log.Error(e);
            }
            ExecuteGetRegnumsByNameOrId(searchName);
            RegnumsView.MoveCurrentToPosition(_position);
        }
        #endregion [Methods Regnum]                
 
 

 //    Part 2    

                                                          

 //    Part 3    

                                                          



 //    Part 4    

           
        #region [Public Commands Connect ==> Tbl06Phylum]                 
        
        private RelayCommand _addPhylumCommand;
        public ICommand AddPhylumCommand => _addPhylumCommand ??= new RelayCommand(delegate { ExecuteAddPhylum(null); });

        private RelayCommand _copyPhylumCommand;
        public ICommand CopyPhylumCommand => _copyPhylumCommand ??= new RelayCommand(delegate { ExecuteCopyPhylum(null); });

        private RelayCommand _deletePhylumCommand;
        public ICommand DeletePhylumCommand => _deletePhylumCommand ??= new RelayCommand(delegate { ExecuteDeletePhylum(SearchRegnumName); });

        private RelayCommand _savePhylumCommand;
        public ICommand SavePhylumCommand => _savePhylumCommand ??= new RelayCommand(delegate { ExecuteSavePhylum(SearchRegnumName); });    

        #endregion [Public Commands Connect ==> Tbl06Phylum]    

        #region [Public Methods Connect ==> Tbl06Phylum]                   
     
        private void ExecuteAddPhylum(object o)      
        {
            Tbl06PhylumsList.Insert(0, new Tbl06Phylum  { PhylumName = CultRes.StringsRes.DatasetNew});
            Tbl03RegnumsAllList = _extGet.AllCollection<Tbl03Regnum>("regnum");

            PhylumsView = CollectionViewSource.GetDefaultView(Tbl06PhylumsList);
            PhylumsView.MoveCurrentToFirst();
        }         
     
        private void ExecuteCopyPhylum(object o)
        {
            if (_genPhylumMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl06Phylum)) return;

            Tbl06PhylumsList = _extCopy.CopyPhylum(CurrentTbl06Phylum);

            // evtl verbundene tabellen-Datensätze auch kopieren Expert, Source, Author und Comment

            PhylumsView = CollectionViewSource.GetDefaultView(Tbl06PhylumsList);
            PhylumsView.MoveCurrentToFirst();
        }        
                     
        private void ExecuteDeletePhylum(string searchName)
        {
             if (_genPhylumMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl06Phylum)) return;

            //check if in Tbl12Subphylums connected datasets no delete possible, Expert, Sources, Authors and Comment delete and than return
            Tbl12SubphylumsList = _extDelete.SearchForConnectedDatasetsWithPhylumIdInTableSubphylum(CurrentTbl06Phylum);
            if (_allMessageBoxes.DoNotDeleteDatasetInfoMessageBox(Tbl12SubphylumsList.Count, "Subphylum")) return;                                                                                                                   
           
            //Delete all References Experts, Sources, Authors  ----------------------------------------------------
            Tbl90ReferencesList = _extDelete.DeleteDatasetsWithPhylumIdInTableReference(CurrentTbl06Phylum);
            if (Tbl90ReferencesList.Count > 0)
            {
                if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.ReferenceAuthor + " " + CultRes.StringsRes.ReferenceSource + " " + CultRes.StringsRes.ReferenceSource)) return;

                _extDelete.DeleteReferences(Tbl90ReferencesList);

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CultRes.StringsRes.Reference);
            }

            //Delete all Comments  ----------------------------------------------------
            Tbl93CommentsList = _extDelete.DeleteDatasetsWithPhylumIdInTableComment(CurrentTbl06Phylum);
            if (Tbl93CommentsList.Count > 0)
            {
                if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.Comment)) return;

                _extDelete.DeleteComments(Tbl93CommentsList);

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CultRes.StringsRes.Comment);
            }

            try 
            {
                var phylum = _uow.Tbl06Phylums.GetById(CurrentTbl06Phylum.PhylumId);
                if (phylum != null)
                {
                    if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl06Phylum.PhylumName)) return;

                    _extDelete.DeletePhylum(phylum);

                    _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CurrentTbl06Phylum.PhylumName);
                }
                else _allMessageBoxes.InfoMessageBox("Not To Delete", CultRes.StringsRes.DeleteCan + " " + CurrentTbl06Phylum.PhylumName + " " + CultRes.StringsRes.DeleteCan1);
            }
            catch (Exception e)
            {
                _allMessageBoxes.InfoMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }

            Tbl06PhylumsList = _extGet.GetPhylumsCollectionOrderByFromRegnumId<Tbl06Phylum>(CurrentTbl06Phylum.RegnumId);

            PhylumsView = CollectionViewSource.GetDefaultView(Tbl06PhylumsList);
            PhylumsView.MoveCurrentToFirst();
        }                 
                  
        private void ExecuteSavePhylum(string searchName)
        {
             if (_genPhylumMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl06Phylum)) return;

            CurrentTbl06Phylum.RegnumId = CurrentTbl03Regnum.RegnumId;                                                                                                                      
              
            try
            {
                var phylum = _uow.Tbl06Phylums.GetById(CurrentTbl06Phylum.PhylumId);

                if (CurrentTbl06Phylum.PhylumId == 0)
                    phylum = _extSave.PhylumAdd(CurrentTbl06Phylum);
                else
                    phylum = _extSave.PhylumUpdate(phylum, CurrentTbl06Phylum);

              //  _position = PhylumsView.CurrentPosition;

                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(CurrentTbl06Phylum.PhylumName))  return;

                try
                {
                    _extSave.PhylumSave(phylum, CurrentTbl06Phylum);
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

                _allMessageBoxes.InfoMessageBox("Save Successfull", CurrentTbl06Phylum.PhylumId == 0
                    ? CultRes.StringsRes.DatasetNew
                    : CurrentTbl06Phylum.PhylumName);
            }
            catch (Exception e)
            {
                _allMessageBoxes.WarningMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }

            Tbl06PhylumsList = _extGet.GetPhylumsCollectionOrderByFromRegnumId<Tbl06Phylum>(CurrentTbl06Phylum.RegnumId);

            PhylumsView = CollectionViewSource.GetDefaultView(Tbl06PhylumsList);
            PhylumsView.MoveCurrentToFirst();
        }

        #endregion [Public Methods  Connect ==> Tbl06Phylum]                                                                                                                                            
                                                          


 //    Part 5    

        
               
        #region [Public Commands Connect ==> Tbl09Division]                
        
        private RelayCommand _addDivisionCommand;

        public ICommand AddDivisionCommand => _addDivisionCommand ??= new RelayCommand(delegate { ExecuteAddDivision(null); });

        private RelayCommand _copyDivisionCommand;

        public ICommand CopyDivisionCommand => _copyDivisionCommand ??= new RelayCommand(delegate { ExecuteCopyDivision(null); });

        private RelayCommand _deleteDivisionCommand;

        public ICommand DeleteDivisionCommand => _deleteDivisionCommand ??= new RelayCommand(delegate { ExecuteDeleteDivision(null); });

        private RelayCommand _saveDivisionCommand;

        public ICommand SaveDivisionCommand => _saveDivisionCommand ??= new RelayCommand(delegate { ExecuteSaveDivision(null); });

        #endregion [Public Commands Connect ==> Tbl09Division]                

        #region [Public Methods Connect ==> Tbl09Division]                        
                         
        private void ExecuteAddDivision(object o)      
        {
            Tbl09DivisionsList.Insert(0, new Tbl09Division  { DivisionName = CultRes.StringsRes.DatasetNew });

            Tbl03RegnumsAllList = _extGet.AllCollection<Tbl03Regnum>("regnum");

            DivisionsView = CollectionViewSource.GetDefaultView(Tbl09DivisionsList);
            DivisionsView.MoveCurrentToFirst();
        }          
                          
        private void ExecuteCopyDivision(object o)
        {
            if (_genDivisionMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl09Division)) return;

            Tbl09DivisionsList = _extCopy.CopyDivision(CurrentTbl09Division);

            // evtl verbundene tabellen-Datensätze auch kopieren Expert, Source, Author und Comment

            DivisionsView = CollectionViewSource.GetDefaultView(Tbl09DivisionsList);
            DivisionsView.MoveCurrentToFirst();
        }          
                        
        private void ExecuteDeleteDivision(string searchName)
        {
             if (_genDivisionMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl09Division)) return;

            //check if in Tbl15Subdivisions connected datasets no delete possible, Expert, Sources, Authors and Comment delete and than return
            Tbl15SubdivisionsList = _extDelete.SearchForConnectedDatasetsWithDivisionIdInTableSubdivision(CurrentTbl09Division);
            if (_allMessageBoxes.DoNotDeleteDatasetInfoMessageBox(Tbl15SubdivisionsList.Count, "Subdivision")) return;

            //Delete all References Experts, Sources, Authors  ----------------------------------------------------
            Tbl90ReferencesList = _extDelete.DeleteDatasetsWithDivisionIdInTableReference(CurrentTbl09Division);
            if (Tbl90ReferencesList.Count > 0)
            {
                if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.ReferenceAuthor + " " + CultRes.StringsRes.ReferenceSource + " " + CultRes.StringsRes.ReferenceSource)) return;

                _extDelete.DeleteReferences(Tbl90ReferencesList);

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CultRes.StringsRes.Reference);
            }

            //Delete all Comments  ----------------------------------------------------
            Tbl93CommentsList = _extDelete.DeleteDatasetsWithDivisionIdInTableComment(CurrentTbl09Division);
            if (Tbl93CommentsList.Count > 0)
            {
                if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.Comment)) return;

                _extDelete.DeleteComments(Tbl93CommentsList);

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CultRes.StringsRes.Comment);
            }

            try 
            {
                var division = _uow.Tbl09Divisions.GetById(CurrentTbl09Division.DivisionId);
                if (division != null)
                {
                    if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl09Division.DivisionName)) return;

                    _extDelete.DeleteDivision(division);

                    _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CurrentTbl09Division.DivisionName);
                }
                else _allMessageBoxes.InfoMessageBox("Not To Delete", CultRes.StringsRes.DeleteCan + " " + CurrentTbl09Division.DivisionName + " " + CultRes.StringsRes.DeleteCan1);
            }
            catch (Exception e)
            {
                _allMessageBoxes.InfoMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }

            Tbl09DivisionsList = _extGet.GetDivisionsCollectionOrderByFromRegnumId<Tbl09Division>(CurrentTbl09Division.RegnumId);

            DivisionsView = CollectionViewSource.GetDefaultView(Tbl09DivisionsList);
            DivisionsView.MoveCurrentToFirst();
        }                  
              
        private void ExecuteSaveDivision(string searchName)
        {
             if (_genDivisionMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl09Division)) return;

            CurrentTbl09Division.RegnumId = CurrentTbl03Regnum.RegnumId;                                                                                                                    

            try
            {
                var division = _uow.Tbl09Divisions.GetById(CurrentTbl09Division.DivisionId);

                if (CurrentTbl09Division.DivisionId == 0)
                    division = _extSave.DivisionAdd(CurrentTbl09Division);
                else
                    division = _extSave.DivisionUpdate(division, CurrentTbl09Division);

              //  _position = DivisionsView.CurrentPosition;

                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(CurrentTbl09Division.DivisionName))  return;

                try
                {
                    _extSave.DivisionSave(division, CurrentTbl09Division);
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

                _allMessageBoxes.InfoMessageBox("Save Successfull", CurrentTbl09Division.DivisionId == 0
                    ? CultRes.StringsRes.DatasetNew
                    : CurrentTbl09Division.DivisionName);
            }
            catch (Exception e)
            {
                _allMessageBoxes.WarningMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }

            Tbl09DivisionsList = _extGet.GetDivisionsCollectionOrderByFromRegnumId<Tbl09Division>(CurrentTbl09Division.RegnumId);
        
            SelectedMainTabIndex = 2;
            DivisionsView = CollectionViewSource.GetDefaultView(Tbl09DivisionsList);
            DivisionsView.MoveCurrentToFirst();
        }
        #endregion [Public Methods  Connect ==> Tbl09Division]                                                                                                                                                  
                                                          
                      
 //    Part 6    

 
            

 //    Part 7    

 

 //    Part 8    

           
        #region [Commands Regnum ==> Tbl90Reference Author]

        private RelayCommand _addReferenceAuthorCommand;

        public ICommand AddReferenceAuthorCommand => _addReferenceAuthorCommand ??= new RelayCommand(delegate { ExecuteAddReferenceAuthor(null); });

        private RelayCommand _copyReferenceAuthorCommand;

        public ICommand CopyReferenceAuthorCommand => _copyReferenceAuthorCommand ??= new RelayCommand(delegate { ExecuteCopyReferenceAuthor(null); });

        private RelayCommand _deleteReferenceAuthorCommand;

        public ICommand DeleteReferenceAuthorCommand => _deleteReferenceAuthorCommand ??= new RelayCommand(delegate { ExecuteDeleteReferenceAuthor(null); });

        private RelayCommand _saveReferenceAuthorCommand;

        public ICommand SaveReferenceAuthorCommand => _saveReferenceAuthorCommand ??= new RelayCommand(delegate { ExecuteSaveReferenceAuthor(null); });        

        #endregion [Commands Regnum ==> Tbl90Reference Author]                
     
        #region [Methods Regnum ==> Tbl90Reference Author]

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

            Tbl90ReferenceAuthorsList = _extCopy.CopyReferenceRegnum(CurrentTbl90ReferenceAuthor, "Author");

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

            CurrentTbl90ReferenceAuthor.RegnumId = CurrentTbl03Regnum.RegnumId;

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
                    reference = _extSave.ReferenceAuthorRegnumAdd(CurrentTbl90ReferenceAuthor);

                else
                    reference = _extSave.ReferenceAuthorRegnumUpdate(reference, CurrentTbl90ReferenceAuthor);

                //    _position = RegnumsView.CurrentPosition;

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
           Tbl90ReferenceAuthorsList = _extGet.GetReferenceAuthorsCollectionOrderByFromRegnumIdAndRefSourceIdIsNullAndRefExpertIdIsNull<Tbl90Reference>(CurrentTbl03Regnum.RegnumId);            
     

            ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
            ReferenceAuthorsView.Refresh();
        }
        #endregion [Methods Regnum ==> Tbl90Reference Author]              
           
        #region [Commands Regnum ==> Tbl90Reference Source]      

        private RelayCommand _addReferenceSourceCommand;

        public ICommand AddReferenceSourceCommand => _addReferenceSourceCommand ??= new RelayCommand(delegate { ExecuteAddReferenceSource(null); });

        private RelayCommand _copyReferenceSourceCommand;

        public ICommand CopyReferenceSourceCommand => _copyReferenceSourceCommand ??= new RelayCommand(delegate {ExecuteCopyReferenceSource(null); });

        private RelayCommand _deleteReferenceSourceCommand;

        public ICommand DeleteReferenceSourceCommand => _deleteReferenceSourceCommand ??= new RelayCommand(delegate { ExecuteDeleteReferenceSource(null); });

        private RelayCommand _saveReferenceSourceCommand;

        public ICommand SaveReferenceSourceCommand => _saveReferenceSourceCommand ??= new RelayCommand(delegate { ExecuteSaveReferenceSource(null); });

            
        #endregion [Commands Regnum ==> Tbl90Reference Source]         
     
        #region [Methods Regnum ==> Tbl90Reference Source]      

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

            Tbl90ReferenceAuthorsList = _extCopy.CopyReferenceRegnum(CurrentTbl90ReferenceSource, "Source");

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

           Tbl90ReferenceSourcesList = _extGet.GetReferenceSourcesCollectionOrderByFromRegnumIdAndRefAuthorIdIsNullAndRefExpertIdIsNull<Tbl90Reference>(CurrentTbl03Regnum.RegnumId);          

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

            CurrentTbl90ReferenceSource.RegnumId = CurrentTbl03Regnum.RegnumId;

            try
            {
                var reference = _uow.Tbl90References.GetById(CurrentTbl90ReferenceSource.ReferenceId);


                if (CurrentTbl90ReferenceSource.ReferenceId == 0)
                    reference = _extSave.ReferenceSourceRegnumAdd(CurrentTbl90ReferenceSource);
                else
                    reference = _extSave.ReferenceSourceRegnumUpdate(reference, CurrentTbl90ReferenceSource);

        //        _position = RegnumsView.CurrentPosition;

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

           Tbl90ReferenceSourcesList = _extGet.GetReferenceSourcesCollectionOrderByFromRegnumIdAndRefAuthorIdIsNullAndRefExpertIdIsNull<Tbl90Reference>(CurrentTbl03Regnum.RegnumId);            

     

            ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
            ReferenceSourcesView.MoveCurrentToFirst();
        }
        #endregion [Methods Regnum ==> Tbl90Reference Source]                    
           
        #region [Commands Regnum ==> Tbl90Reference Expert]                 
 
        private RelayCommand _addReferenceExpertCommand;

        public ICommand AddReferenceExpertCommand => _addReferenceExpertCommand ??= new RelayCommand(delegate { ExecuteAddReferenceExpert(null); });

        private RelayCommand _copyReferenceExpertCommand;

        public ICommand CopyReferenceExpertCommand => _copyReferenceExpertCommand ??= new RelayCommand(delegate { ExecuteCopyReferenceExpert(null); });

        private RelayCommand _deleteReferenceExpertCommand;

        public ICommand DeleteReferenceExpertCommand => _deleteReferenceExpertCommand ??= new RelayCommand(delegate { ExecuteDeleteReferenceExpert(null); });
        private RelayCommand _saveReferenceExpertCommand;

        public ICommand SaveReferenceExpertCommand => _saveReferenceExpertCommand ??= new RelayCommand(delegate { ExecuteSaveReferenceExpert(null); });

        #endregion [Commands Regnum ==> Tbl90Reference Expert]                    
     
     
        #region [Methods Regnum ==> Tbl90Reference Expert]                 

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

            Tbl90ReferenceExpertsList = _extCopy.CopyReferenceRegnum(CurrentTbl90ReferenceExpert, "Expert");

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

           Tbl90ReferenceExpertsList= _extGet.GetReferenceExpertsCollectionOrderByFromRegnumIdAndRefAuthorIdIsNullAndRefSourceIdIsNull<Tbl90Reference>(CurrentTbl03Regnum.RegnumId);           

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

            CurrentTbl90ReferenceExpert.RegnumId = CurrentTbl03Regnum.RegnumId;

            try
            {
                var reference = _uow.Tbl90References.GetById(CurrentTbl90ReferenceExpert.ReferenceId);


                if (CurrentTbl90ReferenceExpert.ReferenceId == 0)
                    reference = _extSave.ReferenceExpertRegnumAdd(CurrentTbl90ReferenceExpert);
                else
                    reference = _extSave.ReferenceExpertRegnumUpdate(reference, CurrentTbl90ReferenceExpert);

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

           Tbl90ReferenceExpertsList= _extGet.GetReferenceExpertsCollectionOrderByFromRegnumIdAndRefAuthorIdIsNullAndRefSourceIdIsNull<Tbl90Reference>(CurrentTbl03Regnum.RegnumId);                     
     

            ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
            ReferenceExpertsView.MoveCurrentToFirst();
        }
        #endregion [Methods Regnum ==> Tbl90Reference Expert]                               
           
       #region [Commands Regnum ==> Tbl93Comments]        
   
       private RelayCommand _addCommentCommand;

        public ICommand AddCommentCommand => _addCommentCommand ??= new RelayCommand(delegate { ExecuteAddComment(null); });

        private RelayCommand _copyCommentCommand;

        public ICommand CopyCommentCommand => _copyCommentCommand ??= new RelayCommand(delegate { ExecuteCopyComment(null); });

        private RelayCommand _deleteCommentCommand;

        public ICommand DeleteCommentCommand => _deleteCommentCommand ??= new RelayCommand(delegate { ExecuteDeleteComment(null); });

        private RelayCommand _saveCommentCommand;

        public ICommand SaveCommentCommand => _saveCommentCommand ??= new RelayCommand(delegate { ExecuteSaveComment(null); });

       #endregion [Commands Regnum ==> Tbl93Comments]        
   
     

       #region [Methods Regnum ==> Tbl93Comments]        

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

            Tbl93CommentsList = _extGet.GetCommentsCollectionOrderByFromRegnumId<Tbl93Comment>(CurrentTbl93Comment.RegnumId);

            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            CommentsView.Refresh();
        }        
     
        private void ExecuteSaveComment(object o)
        {
            if (_genCommentMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl93Comment)) return;

            CurrentTbl93Comment.RegnumId = CurrentTbl03Regnum.RegnumId;

            try
            {
                var comment = _uow.Tbl93Comments.GetById(CurrentTbl93Comment.CommentId);


                if (CurrentTbl93Comment.CommentId == 0)
                    comment = _extSave.CommentRegnumAdd(CurrentTbl93Comment);
                else
                    comment = _extSave.CommentRegnumUpdate(comment, CurrentTbl93Comment);

                //        _position = RegnumsView.CurrentPosition;

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

            Tbl93CommentsList = _extGet.GetCommentsCollectionOrderByFromRegnumId<Tbl93Comment>(CurrentTbl93Comment.RegnumId);                 
     

            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            CommentsView.MoveCurrentToFirst();
        }
        #endregion [Methods Regnum ==> Tbl93Comments]                 
 
             
 //    Part 9    


     
        #region "Public Commands Connected Tables by DoubleClick"

        private RelayCommand _getConnectedTablesCommand;
        public ICommand GetConnectedTablesCommand => _getConnectedTablesCommand ??= new RelayCommand(delegate { GetConnectedTablesById(null); });

        #endregion "Public Commands Connected Tables by DoubleClick"

        #region "Public Method Connected Tables by DoubleClick"

        private void GetConnectedTablesById(object o)
        {           
Tbl06PhylumsList = _extGet.GetPhylumsCollectionOrderByFromRegnumId<Tbl06Phylum>(CurrentTbl03Regnum.RegnumId);

            Tbl03RegnumsAllList = _extGet.AllCollection<Tbl03Regnum>("regnum");

            PhylumsView = CollectionViewSource.GetDefaultView(Tbl06PhylumsList);
            PhylumsView.Refresh();    

            SelectedMainTabIndex = 0;
            SelectedDetailTabIndex = 1;            
     
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
                    if (CurrentTbl03Regnum != null)
                    {
                        Tbl06PhylumsList = _extGet.GetPhylumsCollectionOrderByFromRegnumId<Tbl06Phylum>(CurrentTbl03Regnum.RegnumId);

                        Tbl03RegnumsAllList = _extGet.AllCollection<Tbl03Regnum>("regnum");

                        PhylumsView = CollectionViewSource.GetDefaultView(Tbl06PhylumsList);
                        PhylumsView.Refresh();
                    }
                    SelectedDetailTabIndex = 1;
                }         
       
                if (_selectedMainTabIndex == 1)             
                {
                    if (CurrentTbl03Regnum != null)
                    {
                        Tbl09DivisionsList = _extGet.GetDivisionsCollectionOrderByFromRegnumId<Tbl09Division>(CurrentTbl03Regnum.RegnumId);

                        Tbl03RegnumsAllList = _extGet.AllCollection<Tbl03Regnum>("regnum");

                        DivisionsView = CollectionViewSource.GetDefaultView(Tbl09DivisionsList);
                        DivisionsView.Refresh();
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
                    if (CurrentTbl03Regnum != null)
                    {
                        Tbl93CommentsList = _extGet.GetCommentsCollectionOrderByFromRegnumId<Tbl93Comment>(CurrentTbl03Regnum.RegnumId);

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
                    SelectedMainTabIndex = 0;  
                 }       
     
                if (_selectedDetailTabIndex == 1)                
                {
                    SelectedMainTabIndex = 0;
                }    
       
                if (_selectedDetailTabIndex == 2)                
                {
                    if (CurrentTbl03Regnum != null)
                    {
                        Tbl09DivisionsList = _extGet.GetDivisionsCollectionOrderByFromRegnumId<Tbl09Division>(CurrentTbl03Regnum.RegnumId);

                        Tbl03RegnumsAllList = _extGet.AllCollection<Tbl03Regnum>("regnum");

                        DivisionsView = CollectionViewSource.GetDefaultView(Tbl09DivisionsList);
                        DivisionsView.Refresh();
                    }
                    SelectedMainTabIndex = 1;
               }    
     
                if (_selectedDetailTabIndex == 3)
                {
                    if (CurrentTbl03Regnum != null)
                    {
                        Tbl90ExpertsAllList = new ObservableCollection<Tbl90RefExpert>(_uow.Tbl90RefExperts.ListTbl90RefExpertsOrderBy());

                        Tbl90ReferenceExpertsList = _extGet.GetReferenceExpertsCollectionOrderByFromRegnumIdAndRefAuthorIdIsNullAndRefSourceIdIsNull<Tbl90Reference>(CurrentTbl03Regnum.RegnumId);

                        ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
                        ReferenceExpertsView.Refresh();
                    }
                    SelectedMainTabIndex = 2;
                    SelectedMainSubRefTabIndex = 0;
                }        
     
                if (_selectedDetailTabIndex == 4)
                {
                    if (CurrentTbl03Regnum != null)
                    {
                        Tbl90SourcesAllList = new ObservableCollection<Tbl90RefSource>(_uow.Tbl90RefSources.ListTbl90RefSourcesOrderBy());

                        Tbl90ReferenceSourcesList = _extGet.GetReferenceSourcesCollectionOrderByFromRegnumIdAndRefAuthorIdIsNullAndRefExpertIdIsNull<Tbl90Reference>(CurrentTbl03Regnum.RegnumId);

                        ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
                        ReferenceSourcesView.Refresh();
                    }
                    SelectedMainTabIndex = 2;
                    SelectedMainSubRefTabIndex = 1;
                }        
     
                if (_selectedDetailTabIndex == 5)
                {
                    if (CurrentTbl03Regnum != null)
                    {
                        Tbl90AuthorsAllList = new ObservableCollection<Tbl90RefAuthor>(_uow.Tbl90RefAuthors.ListTbl90RefAuthorsOrderBy());

                        Tbl90ReferenceAuthorsList = _extGet.GetReferenceAuthorsCollectionOrderByFromRegnumIdAndRefSourceIdIsNullAndRefExpertIdIsNull<Tbl90Reference>(CurrentTbl03Regnum.RegnumId);

                        ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
                        ReferenceAuthorsView.Refresh();
                    }
                    SelectedMainTabIndex = 2;
                    SelectedMainSubRefTabIndex = 2;
                }       
     
                if (_selectedDetailTabIndex == 6)
                {
                    if (CurrentTbl03Regnum != null)
                    {
                        Tbl93CommentsList = _extGet.GetCommentsCollectionOrderByFromRegnumId<Tbl93Comment>(CurrentTbl03Regnum.RegnumId);

                        CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
                        CommentsView.Refresh();
                    }
                    SelectedMainTabIndex = 3;
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
                    if (CurrentTbl03Regnum != null)
                    {
                        Tbl90ExpertsAllList = new ObservableCollection<Tbl90RefExpert>(_uow.Tbl90RefExperts.ListTbl90RefExpertsOrderBy());

                        Tbl90ReferenceExpertsList = _extGet.GetReferenceExpertsCollectionOrderByFromRegnumIdAndRefAuthorIdIsNullAndRefSourceIdIsNull<Tbl90Reference>(CurrentTbl03Regnum.RegnumId);

                        ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
                        ReferenceExpertsView.Refresh();
                    }
                    SelectedDetailTabIndex = 3;
                    SelectedMainTabIndex = 2;
                }        
     
                if (_selectedMainSubRefTabIndex == 1)
                {
                    if (CurrentTbl03Regnum != null)
                    {
                        Tbl90SourcesAllList = new ObservableCollection<Tbl90RefSource>(_uow.Tbl90RefSources.ListTbl90RefSourcesOrderBy());

                        Tbl90ReferenceSourcesList = _extGet.GetReferenceSourcesCollectionOrderByFromRegnumIdAndRefAuthorIdIsNullAndRefExpertIdIsNull<Tbl90Reference>(CurrentTbl03Regnum.RegnumId);

                        ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
                        ReferenceSourcesView.Refresh();
                    }
                    SelectedDetailTabIndex = 4;
                    SelectedMainTabIndex = 2;
                }      
     
                if (_selectedMainSubRefTabIndex == 2)
                {
                    if (CurrentTbl03Regnum != null)
                    {
                        Tbl90AuthorsAllList = new ObservableCollection<Tbl90RefAuthor>(_uow.Tbl90RefAuthors.ListTbl90RefAuthorsOrderBy());

                        Tbl90ReferenceAuthorsList = _extGet.GetReferenceAuthorsCollectionOrderByFromRegnumIdAndRefSourceIdIsNullAndRefExpertIdIsNull<Tbl90Reference>(CurrentTbl03Regnum.RegnumId);

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

     
        #region "Public Properties Tbl03Regnum"

        private string _searchRegnumName = "";
        public string SearchRegnumName
        {
            get => _searchRegnumName; 
            set { _searchRegnumName = value; RaisePropertyChanged("");  }
        }

        public  ICollectionView RegnumsView;
        private   Tbl03Regnum CurrentTbl03Regnum => RegnumsView?.CurrentItem as Tbl03Regnum;

        private ObservableCollection<Tbl03Regnum> _tbl03RegnumsList;
        public  ObservableCollection<Tbl03Regnum> Tbl03RegnumsList
        {
            get => _tbl03RegnumsList; 
            set {  _tbl03RegnumsList = value; RaisePropertyChanged("");   }
        }

        private ObservableCollection<Tbl03Regnum> _tbl03RegnumsAllList;
        public  ObservableCollection<Tbl03Regnum> Tbl03RegnumsAllList
        {
            get => _tbl03RegnumsAllList; 
            set {  _tbl03RegnumsAllList = value; RaisePropertyChanged("");   }
        }

        private ObservableCollection<Tbl06Phylum> _tbl06PhylumsAllList;
        public  ObservableCollection<Tbl06Phylum> Tbl06PhylumsAllList
        {
            get => _tbl06PhylumsAllList; 
            set {  _tbl06PhylumsAllList = value; RaisePropertyChanged("");   }
        }

        #endregion "Public Properties"   
      
        #region "Public Properties Tbl06Phylum"

        public ICollectionView PhylumsView;
        private Tbl06Phylum CurrentTbl06Phylum => PhylumsView?.CurrentItem as Tbl06Phylum;           

        private ObservableCollection<Tbl06Phylum> _tbl06PhylumsList;
        public  ObservableCollection<Tbl06Phylum> Tbl06PhylumsList
        {
            get => _tbl06PhylumsList; 
            set { _tbl06PhylumsList = value; RaisePropertyChanged(""); }
        }
        #endregion "Public Properties"     
        
        #region "Public Properties Tbl09Division"

        public ICollectionView DivisionsView;
        private Tbl09Division CurrentTbl09Division => DivisionsView?.CurrentItem as Tbl09Division;           

        private ObservableCollection<Tbl09Division> _tbl09DivisionsList;
        public  ObservableCollection<Tbl09Division> Tbl09DivisionsList
        {
            get => _tbl09DivisionsList; 
            set { _tbl09DivisionsList = value; RaisePropertyChanged(""); }
        }
        #endregion "Public Properties"     
        
        #region "Public Properties Tbl12Subphylum"

        public ICollectionView SubphylumsView;
        private Tbl12Subphylum CurrentTbl12Subphylum => SubphylumsView?.CurrentItem as Tbl12Subphylum;           

        private ObservableCollection<Tbl12Subphylum> _tbl12SubphylumsList;
        public ObservableCollection<Tbl12Subphylum> Tbl12SubphylumsList
        {
            get => _tbl12SubphylumsList; 
            set { _tbl12SubphylumsList = value; RaisePropertyChanged(""); }
        }

        #endregion "Public Properties"     
        
        #region "Public Properties Tbl15Subdivision"

        private ObservableCollection<Tbl15Subdivision> _tbl15SubdivisionsList;
        public ObservableCollection<Tbl15Subdivision> Tbl15SubdivisionsList
        {
            get => _tbl15SubdivisionsList; 
            set { _tbl15SubdivisionsList = value; RaisePropertyChanged(""); }
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
