using ATIS.Dal.Models;
using ATIS.Ui.Core;
using ATIS.Ui.Helper;
using ATIS.Ui.Views.Database.CrudHelper;
using ATIS.Ui.Views.Database.DatabaseHelper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using ATIS.Ui.Views.Database.D03Regnum;
using ATIS.Ui.Views.Database.D15Subdivision;
using Common.Logging;

namespace ATIS.Ui.Views.Database.D06Phylum
{
    public class PhylumsViewModel : ViewModelBase
    {

        #region "Private Data Members"

        private static readonly ILog Log =
            LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        //     private static IBusinessLayer _businessLayer;
        //     private static DbEntityException _entityException;
        private readonly UnitOfWork _uow = new UnitOfWork(new AtisDbContext());
        private readonly AtisDbContext _context = new AtisDbContext();

        private readonly AllMessageBoxes _allMessageBoxes = new AllMessageBoxes();
        private readonly GenericMessageBoxes<Tbl06Phylum> _genPhylumMessageBoxes = new GenericMessageBoxes<Tbl06Phylum>();
        private readonly GenericMessageBoxes<Tbl03Regnum> _genRegnumMessageBoxes = new GenericMessageBoxes<Tbl03Regnum>();
        private readonly GenericMessageBoxes<Tbl12Subphylum> _genSubphylumMessageBoxes = new GenericMessageBoxes<Tbl12Subphylum>();
        private readonly GenericMessageBoxes<Tbl90Reference> _genExpertMessageBoxes = new GenericMessageBoxes<Tbl90Reference>();
        private readonly GenericMessageBoxes<Tbl90Reference> _genSourceMessageBoxes = new GenericMessageBoxes<Tbl90Reference>();
        private readonly GenericMessageBoxes<Tbl90Reference> _genAuthorMessageBoxes = new GenericMessageBoxes<Tbl90Reference>();
        private readonly GenericMessageBoxes<Tbl93Comment> _genCommentMessageBoxes = new GenericMessageBoxes<Tbl93Comment>();
        private readonly BasicGet _extGet = new BasicGet();
        private readonly BasicCopy _extCopy = new BasicCopy();
        private readonly BasicDelete _extDelete = new BasicDelete();
        private readonly BasicSave _extSave = new BasicSave();

        private int _position;

        #endregion "Private Data Members"

        #region "Constructor"

        public PhylumsViewModel()
        {
            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.
            }
            else
            {

                LoadCollections();

                // Code runs "for real" 
                //          _entityException = new DbEntityException();
            }
        }

        public bool IsInDesignMode { get; set; }

        private void LoadCollections()
        {
            //Tbl03RegnumsAllList = new ObservableCollection<Tbl03Regnum>(_context.Tbl03Regnums
            //    .OrderBy(a => a.RegnumName)
            //    .ThenBy(a => a.Subregnum));
            //RegnumsAllCollection = new ObservableCollection<Tbl03Regnum>(_uow.Tbl03Regnums.GetAll());
            //PhylumsAllCollection = new ObservableCollection<Tbl06Phylum>(_uow.Tbl06Phylums.GetAll());
            //DivisionsAllCollection = new ObservableCollection<Tbl09Division>(_uow.Tbl09Divisions.GetAll());

            //RegnumsCollection = new ObservableCollection<Tbl03Regnum>();
            Tbl06PhylumsList = new ObservableCollection<Tbl06Phylum>();
            //DivisionsCollection = new ObservableCollection<Tbl09Division>();
            //SubphylumsCollection = new ObservableCollection<Tbl12Subphylum>();
            //SubdivisionsCollection = new ObservableCollection<Tbl15Subdivision>();
            //ReferencesCollection = new ObservableCollection<Tbl90Reference>();
            //ReferenceExpertsCollection = new ObservableCollection<Tbl90Reference>();
            //ReferenceSourcesCollection = new ObservableCollection<Tbl90Reference>();
            //ReferenceAuthorsCollection = new ObservableCollection<Tbl90Reference>();
            //ExpertsCollection = new ObservableCollection<Tbl90RefExpert>(_uow.Tbl90RefExperts.ListTbl90RefExpertsOrderBy());
            //SourcesCollection = new ObservableCollection<Tbl90RefSource>(_uow.Tbl90RefSources.ListTbl90RefSourcesOrderBy());
            //AuthorsCollection = new ObservableCollection<Tbl90RefAuthor>(_uow.Tbl90RefAuthors.ListTbl90RefAuthorsOrderBy());

            //CommentsCollection = new ObservableCollection<Tbl93Comment>();
            //TabIndexDetail = 1;

        }

        #endregion "Constructor"


        //    Part 1    

        #region [Commands Phylum]

        #region "Public Commands Basic Tbl06Phylum"

        //-------------------------------------------------------------------------
        //private RelayCommand _clearPhylumCommand;

        //public ICommand ClearPhylumCommand => _clearPhylumCommand ??= new RelayCommand(delegate { ClearPhylum(null); });

        private RelayCommand _getPhylumsByNameOrIdCommand;
        public ICommand GetPhylumsByNameOrIdCommand => _getPhylumsByNameOrIdCommand ??= new RelayCommand(delegate { ExecuteGetPhylumsByNameOrId(SearchPhylumName); });

        private RelayCommand _addPhylumCommand;
        public ICommand AddPhylumCommand => _addPhylumCommand ??= new RelayCommand(delegate { ExecuteAddPhylum(null); });

        private RelayCommand _copyPhylumCommand;
        public ICommand CopyPhylumCommand => _copyPhylumCommand ??= new RelayCommand(delegate { ExecuteCopyPhylum(null); });

        private RelayCommand _deletePhylumCommand;
        public ICommand DeletePhylumCommand => _deletePhylumCommand ??= new RelayCommand(delegate { ExecuteDeletePhylum(SearchPhylumName); });

        private RelayCommand _savePhylumCommand;
        public ICommand SavePhylumCommand => _savePhylumCommand ??= new RelayCommand(delegate { ExecuteSavePhylum(SearchPhylumName); });

        #endregion [Commands Phylum]

        #endregion [Commands Phylum]

        //-------------------------------------------------------------------------         

        #region [Methods Phylum]


        private void ClearPhylum(object o)
        {
            SearchPhylumName = "";

            SelectedMainTabIndex = 0; //change tab
            SelectedDetailTabIndex = 0;
            SelectedDetailSubTabIndex = 0;
            SelectedDetailSubRefTabIndex = 0;

            Tbl03RegnumsList?.Clear();
            Tbl06PhylumsList?.Clear();
            Tbl12SubphylumsList?.Clear();
            Tbl90ReferenceExpertsList?.Clear();
            Tbl90ReferenceSourcesList?.Clear();
            Tbl90ReferenceAuthorsList?.Clear();
            Tbl93CommentsList?.Clear();
        }
        //----------------------------------------------------------------------                  

        private void ExecuteGetPhylumsByNameOrId(string searchName)
        {
            Tbl03RegnumsAllList = _extGet.AllCollection<Tbl03Regnum>("regnum");

            Tbl06PhylumsList = _extGet.SearchNameAndIdReturnCollection<Tbl06Phylum>(SearchPhylumName, "phylum");

            PhylumsView = CollectionViewSource.GetDefaultView(Tbl06PhylumsList);
            PhylumsView.Refresh();
        }
        //------------------------------------------------------------------------------------                          

        private void ExecuteAddPhylum(object o)
        {
            Tbl06PhylumsList.Insert(0, new Tbl06Phylum {PhylumName = CultRes.StringsRes.DatasetNew});

            Tbl03RegnumsAllList = _extGet.AllCollection<Tbl03Regnum>("regnum");

            PhylumsView = CollectionViewSource.GetDefaultView(Tbl06PhylumsList);
            PhylumsView.MoveCurrentToFirst();
        }
        //------------------------------------------------------------------------------------                               

        private void ExecuteCopyPhylum(object o)
        {
            if (_genPhylumMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl06Phylum)) return;

            Tbl06PhylumsList = _extCopy.CopyPhylum(CurrentTbl06Phylum);

            // evtl verbundene tabellen-Datensätze auch kopieren Expert, Source, Author und Comment

            PhylumsView = CollectionViewSource.GetDefaultView(Tbl06PhylumsList);
            PhylumsView.MoveCurrentToFirst();
        }
        //---------------------------------------------------------------------------------------                            

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

            ExecuteGetPhylumsByNameOrId(searchName);

            PhylumsView.MoveCurrentToFirst();
        }
        //-------------------------------------------------------------------------------------------------                    

        private void ExecuteSavePhylum(string searchName)
        {
            if (_genPhylumMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl06Phylum)) return;

            try
            {
                var phylum = _uow.Tbl06Phylums.GetById(CurrentTbl06Phylum.PhylumId);
                //   var phylum = _context.Tbl06Phylums.AsNoTracking().FirstOrDefault(a=>a.PhylumId == CurrentTbl06Phylum.PhylumId);
                //          _context.Entry(phylum).State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;

                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(CurrentTbl06Phylum.PhylumName))
                    return;

                if (CurrentTbl06Phylum.PhylumId == 0)
                    phylum = _extSave.PhylumAdd(CurrentTbl06Phylum);
                else
                    phylum = _extSave.PhylumUpdate(phylum, CurrentTbl06Phylum);

                _position = PhylumsView.CurrentPosition;

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
                    Log.Error(e);
                    return;
                }

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.SaveSuccess, CurrentTbl06Phylum.PhylumId == 0
                    ? "DatasetNew"
                    : CurrentTbl06Phylum.PhylumName);
            }
            catch (Exception e)
            {
                _allMessageBoxes.WarningMessageBox(e.Message, CultRes.StringsRes.Error); 
                Log.Error(e);
            }
            ExecuteGetPhylumsByNameOrId(searchName);
            PhylumsView.MoveCurrentToPosition(_position);
        }

        #endregion [Methods Phylum]



        //    Part 2    


        #region "Public Commands Connect <== Tbl03Regnum"                 
        //-------------------------------------------------------------------------

        private RelayCommand _saveRegnumCommand;
        public ICommand SaveRegnumCommand => _saveRegnumCommand ??= new RelayCommand(delegate { ExecuteSaveRegnum(null); });

        ////-------------------------------------------------------------------------          

        private void ExecuteSaveRegnum(string searchName)
        {
            if (_genRegnumMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl03Regnum)) return;

            try
            {
                var regnum = _uow.Tbl03Regnums.GetById(CurrentTbl03Regnum.RegnumId);

                if (CurrentTbl03Regnum.RegnumId == 0)
                    regnum = _extSave.RegnumAdd(CurrentTbl03Regnum);
                else
                    regnum = _extSave.RegnumUpdate(regnum, CurrentTbl03Regnum);

                _position = PhylumsView.CurrentPosition;

                var cap = CurrentTbl03Regnum.RegnumName + " " + CurrentTbl03Regnum.Subregnum;
                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(cap))
                    return;

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
                    //         Log.Error(e);
                    return;
                }

                _allMessageBoxes.InfoMessageBox("Save Successfull", CurrentTbl03Regnum.RegnumId == 0
                    ? CultRes.StringsRes.DatasetNew
                    : CurrentTbl03Regnum.RegnumName + " " + CurrentTbl03Regnum.Subregnum);
            }
            catch (Exception e)
            {
                _allMessageBoxes.WarningMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }

            ExecuteGetPhylumsByNameOrId(searchName);
            PhylumsView.MoveCurrentToPosition(_position);
        }

        #endregion "Public Commands"                  


        //    Part 3    





        //    Part 4    


        #region "Public Commands Connect ==> Tbl12Subphylum"                 
        //-------------------------------------------------------------------------
        private RelayCommand _addSubphylumCommand;

        public ICommand AddSubphylumCommand => _addSubphylumCommand ??= new RelayCommand(delegate { ExecuteAddSubphylum(null); });

        private RelayCommand _copySubphylumCommand;

        public ICommand CopySubphylumCommand => _copySubphylumCommand ??= new RelayCommand(delegate { ExecuteCopySubphylum(null); });

        private RelayCommand _deleteSubphylumCommand;

        public ICommand DeleteSubphylumCommand => _deleteSubphylumCommand ??= new RelayCommand(delegate { ExecuteDeleteSubphylum(SearchPhylumName); });

        private RelayCommand _saveSubphylumCommand;

        public ICommand SaveSubphylumCommand => _saveSubphylumCommand ??= new RelayCommand(delegate { ExecuteSaveSubphylum(SearchPhylumName); });

        //-------------------------------------------------------------------------          

        private void ExecuteAddSubphylum(object o)
        {
            Tbl12SubphylumsList.Insert(0, new Tbl12Subphylum { SubphylumName = CultRes.StringsRes.DatasetNew });

            Tbl06PhylumsAllList = _extGet.AllCollection<Tbl06Phylum>("phylum");

            SubphylumsView = CollectionViewSource.GetDefaultView(Tbl12SubphylumsList);
            SubphylumsView.MoveCurrentToFirst();
        }
        //----------------------------------------------------------------------            

        private void ExecuteCopySubphylum(object o)
        {
            if (_genSubphylumMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl12Subphylum)) return;

            Tbl12SubphylumsList = _extCopy.CopySubphylum(CurrentTbl12Subphylum);

            // evtl verbundene tabellen-Datensätze auch kopieren Expert, Source, Author und Comment

            SubphylumsView = CollectionViewSource.GetDefaultView(Tbl12SubphylumsList);
            SubphylumsView.MoveCurrentToFirst();
        }
        ////----------------------------------------------------------------------            

        private void ExecuteDeleteSubphylum(string searchName)
        {
            if (_genSubphylumMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl12Subphylum)) return;


            //check if in Tbl18Superclasses connected datasets no delete possible, Expert, Sources, Authors and Comment delete and than return
            Tbl18SuperclassesList = _extDelete.SearchForConnectedDatasetsWithSubphylumIdInTableSuperclass(CurrentTbl12Subphylum);
            if (_allMessageBoxes.DoNotDeleteDatasetInfoMessageBox(Tbl18SuperclassesList.Count, "Superclass")) return;

            //Delete all References Experts, Sources, Authors  ----------------------------------------------------
            Tbl90ReferencesList = _extDelete.DeleteDatasetsWithSubphylumIdInTableReference(CurrentTbl12Subphylum);
            if (Tbl90ReferencesList.Count > 0)
            {
                if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.ReferenceAuthor + " " + CultRes.StringsRes.ReferenceSource + " " + CultRes.StringsRes.ReferenceSource)) return;

                _extDelete.DeleteReferences(Tbl90ReferencesList);

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CultRes.StringsRes.Reference);
            }

            //Delete all Comments  ----------------------------------------------------
            Tbl93CommentsList = _extDelete.DeleteDatasetsWithSubphylumIdInTableComment(CurrentTbl12Subphylum);
            if (Tbl93CommentsList.Count > 0)
            {
                if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.Comment)) return;

                _extDelete.DeleteComments(Tbl93CommentsList);

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CultRes.StringsRes.Comment);
            }

            try
            {
                var subphylum = _uow.Tbl12Subphylums.GetById(CurrentTbl12Subphylum.SubphylumId);
                if (subphylum != null)
                {
                    if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl12Subphylum.SubphylumName)) return;

                    _extDelete.DeleteSubphylum(subphylum);

                    _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CurrentTbl12Subphylum.SubphylumName);
                }
                else _allMessageBoxes.InfoMessageBox("Not To Delete", CultRes.StringsRes.DeleteCan + " " + CurrentTbl12Subphylum.SubphylumName + " " + CultRes.StringsRes.DeleteCan1);
            }
            catch (Exception e)
            {
                _allMessageBoxes.InfoMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }

            //       ExecuteGetPhylumsByNameOrId(searchName);

            //        SubphylumsView.MoveCurrentToFirst();

            //Tbl12SubphylumsList = new ObservableCollection<Tbl12Subphylum>(_businessLayer.ListTbl12SubphylumsByPhylumId(CurrentTbl06Phylum.PhylumID));

            //SubphylumsView = CollectionViewSource.GetDefaultView(Tbl12SubphylumsList);
            //SubphylumsView.Refresh();
            Tbl12SubphylumsList = new ObservableCollection<Tbl12Subphylum>(_context.Tbl12Subphylums
                .Where(a => a.PhylumId == CurrentTbl12Subphylum.PhylumId)
                .OrderBy(a=>a.SubphylumName));

            SubphylumsView = CollectionViewSource.GetDefaultView(Tbl12SubphylumsList);
            SubphylumsView.MoveCurrentToFirst();

        }
        ////-------------------------------------------------------------------------------------------------                    

        private void ExecuteSaveSubphylum(string searchName)
        {
            if (_genSubphylumMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl12Subphylum)) return;

            CurrentTbl12Subphylum.PhylumId = CurrentTbl06Phylum.PhylumId;

            try
            {
                var subphylum = _uow.Tbl12Subphylums.GetById(CurrentTbl12Subphylum.SubphylumId);

                if (CurrentTbl12Subphylum.SubphylumId == 0)
                    subphylum = _extSave.SubphylumAdd(CurrentTbl12Subphylum);
                else
                    subphylum = _extSave.SubphylumUpdate(subphylum, CurrentTbl12Subphylum);

              //  _position = SubphylumsView.CurrentPosition;

                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(CurrentTbl12Subphylum.SubphylumName))
                    return;

                try
                {
                    _extSave.SubphylumSave(subphylum, CurrentTbl12Subphylum);
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

                _allMessageBoxes.InfoMessageBox("Save Successfull", CurrentTbl12Subphylum.SubphylumId == 0
                    ? CultRes.StringsRes.DatasetNew
                    : CurrentTbl12Subphylum.SubphylumName);
            }
            catch (Exception e)
            {
                _allMessageBoxes.WarningMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }


             Tbl12SubphylumsList = new ObservableCollection<Tbl12Subphylum>(_context.Tbl12Subphylums
                .Where(a => a.PhylumId == CurrentTbl12Subphylum.PhylumId)
                .OrderBy(a => a.SubphylumName));

            SubphylumsView = CollectionViewSource.GetDefaultView(Tbl12SubphylumsList);
            SubphylumsView.MoveCurrentToFirst();

         }
        //#endregion "Public Commands"                  



        //    Part 5    



        //    Part 6    




        //    Part 7    



        //    Part 8    


        //#region "Public Commands Connect ==> Tbl90ReferenceAuthor"
        //-------------------------------------------------------------------------
        private RelayCommand _addReferenceAuthorCommand;

        public ICommand AddReferenceAuthorCommand => _addReferenceAuthorCommand ??= new RelayCommand(delegate { ExecuteAddReferenceAuthor(null); });

        private RelayCommand _copyReferenceAuthorCommand;

        public ICommand CopyReferenceAuthorCommand => _copyReferenceAuthorCommand ??= new RelayCommand(delegate { ExecuteCopyReferenceAuthor(null); });

        private RelayCommand _deleteReferenceAuthorCommand;

        public ICommand DeleteReferenceAuthorCommand => _deleteReferenceAuthorCommand ??= new RelayCommand(delegate { ExecuteDeleteReferenceAuthor(null); });

        private RelayCommand _saveReferenceAuthorCommand;

        public ICommand SaveReferenceAuthorCommand => _saveReferenceAuthorCommand ??= new RelayCommand(delegate { ExecuteSaveReferenceAuthor(null); });
        //-------------------------------------------------------------------------                    

        public void ExecuteAddReferenceAuthor(object o)
        {
            Tbl90ReferenceAuthorsList ??= new ObservableCollection<Tbl90Reference>();

            Tbl90ReferenceAuthorsList.Insert(0, new Tbl90Reference { Info = CultRes.StringsRes.DatasetNew });

            ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
            ReferenceAuthorsView.MoveCurrentToFirst();
        }
        //----------------------------------------------------------------------            

        public void ExecuteCopyReferenceAuthor(object o)
        {
            //if (CurrentTbl90ReferenceAuthor == null)
            //{
            //    MessageBox.Show(CultRes.StringsRes.DatasetNew,
            //        CultRes.StringsRes.RequiredInput,
            //        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
            //    return;
            //}
            if (_genAuthorMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90ReferenceAuthor)) return;

            //var reference = _businessLayer.SingleListTbl90ReferencesByReferenceId(CurrentTbl90ReferenceAuthor.ReferenceId);

            //Tbl90ReferenceAuthorsList.Insert(0, new Tbl90Reference
            //{
            //    RefAuthorId = reference.RefAuthorId,
            //    Valid = reference.Valid,
            //    ValidYear = reference.ValidYear,
            //    Info = CultRes.StringsRes.DatasetNew,
            //    Memo = reference.Memo
            //});



            Tbl90ReferenceAuthorsList = _extCopy.CopyReferencePhylum(CurrentTbl90ReferenceAuthor, "Author");

            ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
            ReferenceAuthorsView.MoveCurrentToFirst();

        }
        //----------------------------------------------------------------------            

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

            Tbl90ReferenceAuthorsList = new ObservableCollection<Tbl90Reference>(_context.Tbl90References
                .Where(a => a.PhylumId == CurrentTbl90ReferenceAuthor.PhylumId)
                .OrderBy(a => a.Info));

            ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
            ReferenceAuthorsView.MoveCurrentToFirst();

        }

        ////----------------------------------------------------------------------            

        public void ExecuteSaveReferenceAuthor(string searchName)
        {
            if (_genAuthorMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90ReferenceAuthor)) return;

                CurrentTbl90ReferenceAuthor.PhylumId = CurrentTbl06Phylum.PhylumId;
                try
            {
                var reference = _uow.Tbl90References.GetById(CurrentTbl90ReferenceAuthor.ReferenceId);


                if (CurrentTbl90ReferenceAuthor.ReferenceId == 0)
                    reference = _extSave.ReferenceAuthorPhylumAdd(CurrentTbl90ReferenceAuthor);
                else
                    reference = _extSave.ReferenceAuthorPhylumUpdate(reference, CurrentTbl90ReferenceAuthor);

                _position = PhylumsView.CurrentPosition;

                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(CurrentTbl90ReferenceAuthor.Info))
                    return;

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

                Tbl90ReferenceAuthorsList = new ObservableCollection<Tbl90Reference>(_context.Tbl90References
                    .Where(a => a.PhylumId == CurrentTbl90ReferenceAuthor.PhylumId)
                    .OrderBy(a => a.Info));


            ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
            ReferenceAuthorsView.MoveCurrentToFirst();
        }
        //#endregion "Public Commands"                  

        //#region "Public Commands Connect ==> Tbl90ReferenceSource" 
        ////-------------------------------------------------------------------------
        //private RelayCommand _addReferenceSourceCommand;

        //public ICommand AddReferenceSourceCommand => _addReferenceSourceCommand ??
        //                                            (_addReferenceSourceCommand = new RelayCommand(delegate { AddReferenceSource(null); }));

        //private RelayCommand _copyReferenceSourceCommand;

        //public ICommand CopyReferenceSourceCommand => _copyReferenceSourceCommand ??
        //                (_copyReferenceSourceCommand = new RelayCommand(delegate { CopyReferenceSource(null); }));

        //private RelayCommand _deleteReferenceSourceCommand;

        //public ICommand DeleteReferenceSourceCommand => _deleteReferenceSourceCommand ??
        //                                                (_deleteReferenceSourceCommand = new RelayCommand(delegate { DeleteReferenceSource(null); }));

        //private RelayCommand _saveReferenceSourceCommand;

        //public ICommand SaveReferenceSourceCommand => _saveReferenceSourceCommand ??
        //             (_saveReferenceSourceCommand = new RelayCommand(delegate { SaveReferenceSource(null); }));

        ////-------------------------------------------------------------------------          

        //public void AddReferenceSource(object o)
        //{
        //    if (Tbl90ReferenceSourcesList == null)
        //        Tbl90ReferenceSourcesList = new ObservableCollection<Tbl90Reference>();

        //    Tbl90ReferenceSourcesList.Insert(0, new Tbl90Reference { Info = CultRes.StringsRes.DatasetNew });

        //    ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
        //    ReferenceSourcesView.MoveCurrentToFirst();
        //}
        ////----------------------------------------------------------------------            

        //public void CopyReferenceSource(object o)
        //{
        //    if (CurrentTbl90ReferenceSource == null)
        //    {
        //        WpfMessageBox.Show(CultRes.StringsRes.DatasetNew,
        //            CultRes.StringsRes.RequiredInput,
        //            MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
        //        return;
        //    }

        //    var reference = _businessLayer.SingleListTbl90ReferencesByReferenceId(CurrentTbl90ReferenceSource.ReferenceID);

        //    Tbl90ReferenceSourcesList.Insert(0, new Tbl90Reference
        //    {
        //        RefSourceID = reference.RefSourceID,
        //        Valid = reference.Valid,
        //        ValidYear = reference.ValidYear,
        //        Info = CultRes.StringsRes.DatasetNew,
        //        Memo = reference.Memo
        //    });

        //    ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
        //    ReferenceSourcesView.MoveCurrentToFirst();
        //}
        ////----------------------------------------------------------------------            

        //private void DeleteReferenceSource(object o)
        //{
        //    if (CurrentTbl90ReferenceSource == null)
        //    {
        //        WpfMessageBox.Show(CultRes.StringsRes.DatasetNew,
        //            CultRes.StringsRes.RequiredInput,
        //            MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
        //        return;
        //    }

        //    try
        //    {
        //        var reference = _businessLayer.SingleListTbl90ReferencesByReferenceId(CurrentTbl90ReferenceSource.ReferenceID);
        //        if (reference != null)
        //        {
        //            if (WpfMessageBox.Show(CultRes.StringsRes.DeleteQuestion1, CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl90ReferenceSource.Info,
        //                    MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes)
        //                return;
        //            reference.EntityState = EntityState.Deleted;
        //            _businessLayer.RemoveReference(reference);

        //            WpfMessageBox.Show(CultRes.StringsRes.DeleteSuccess, CurrentTbl90ReferenceSource.Info,
        //                MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
        //        }
        //        else
        //        {
        //            WpfMessageBox.Show(CultRes.StringsRes.Information, CultRes.StringsRes.DeleteCan + " " + CurrentTbl90ReferenceSource.Info + " " + CultRes.StringsRes.DeleteCan1,
        //                MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
        //        }
        //    }
        //    catch (DbEntityValidationException ex)
        //    {
        //        _entityException.EntityException(ex);
        //        Log.Error(ex);
        //    }

        //    Tbl90ReferenceSourcesList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ReferenceListRefSourcesByPhylumId(CurrentTbl06Phylum.PhylumID));

        //    ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
        //    ReferenceSourcesView.Refresh();
        //}
        ////----------------------------------------------------------------------            

        //public void SaveReferenceSource(object o)
        //{
        //    if (CurrentTbl90ReferenceSource == null)
        //    {
        //        WpfMessageBox.Show(CultRes.StringsRes.DatasetNew,
        //            CultRes.StringsRes.RequiredInput,
        //            MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
        //        return;
        //    }

        //    CurrentTbl90ReferenceSource.PhylumID = CurrentTbl06Phylum.PhylumID;

        //    try
        //    {
        //        var reference = _businessLayer.SingleListTbl90ReferencesByReferenceId(CurrentTbl90ReferenceSource.ReferenceID);
        //        if (CurrentTbl90ReferenceSource.ReferenceID != 0)
        //        {
        //            if (reference != null) //update
        //            {
        //                reference.RefExpertID = CurrentTbl90ReferenceSource.RefExpertID;
        //                reference.RefAuthorID = CurrentTbl90ReferenceSource.RefAuthorID;
        //                reference.RefSourceID = CurrentTbl90ReferenceSource.RefSourceID;
        //                reference.RegnumID = CurrentTbl90ReferenceSource.RegnumID;
        //                reference.PhylumID = CurrentTbl90ReferenceSource.PhylumID;
        //                reference.DivisionID = CurrentTbl90ReferenceSource.DivisionID;
        //                reference.SubphylumID = CurrentTbl90ReferenceSource.SubphylumID;
        //                reference.SubdivisionID = CurrentTbl90ReferenceSource.SubdivisionID;
        //                reference.SuperclassID = CurrentTbl90ReferenceSource.SuperclassID;
        //                reference.ClassID = CurrentTbl90ReferenceSource.ClassID;
        //                reference.SubclassID = CurrentTbl90ReferenceSource.SubclassID;
        //                reference.InfraclassID = CurrentTbl90ReferenceSource.InfraclassID;
        //                reference.LegioID = CurrentTbl90ReferenceSource.LegioID;
        //                reference.OrdoID = CurrentTbl90ReferenceSource.OrdoID;
        //                reference.SubordoID = CurrentTbl90ReferenceSource.SubordoID;
        //                reference.InfraordoID = CurrentTbl90ReferenceSource.InfraordoID;
        //                reference.SuperfamilyID = CurrentTbl90ReferenceSource.SuperfamilyID;
        //                reference.FamilyID = CurrentTbl90ReferenceSource.FamilyID;
        //                reference.SubfamilyID = CurrentTbl90ReferenceSource.SubfamilyID;
        //                reference.InfrafamilyID = CurrentTbl90ReferenceSource.InfrafamilyID;
        //                reference.SupertribusID = CurrentTbl90ReferenceSource.SupertribusID;
        //                reference.TribusID = CurrentTbl90ReferenceSource.TribusID;
        //                reference.SubtribusID = CurrentTbl90ReferenceSource.SubtribusID;
        //                reference.InfratribusID = CurrentTbl90ReferenceSource.InfratribusID;
        //                reference.GenusID = CurrentTbl90ReferenceSource.GenusID;
        //                reference.PlSpeciesID = CurrentTbl90ReferenceSource.PlSpeciesID;
        //                reference.FiSpeciesID = CurrentTbl90ReferenceSource.FiSpeciesID;
        //                reference.Valid = CurrentTbl90ReferenceSource.Valid;
        //                reference.ValidYear = CurrentTbl90ReferenceSource.ValidYear;
        //                reference.Info = CurrentTbl90ReferenceSource.Info;
        //                reference.Updater = Environment.UserName;
        //                reference.UpdaterDate = DateTime.Now;
        //                reference.Memo = CurrentTbl90ReferenceSource.Memo;

        //                reference.EntityState = EntityState.Modified;
        //            }
        //        }
        //        else
        //        {
        //            reference = new Tbl90Reference     //add new
        //            {
        //                RefAuthorID = CurrentTbl90ReferenceSource.RefAuthorID,
        //                RefSourceID = CurrentTbl90ReferenceSource.RefSourceID,
        //                RefExpertID = CurrentTbl90ReferenceSource.RefExpertID,
        //                RegnumID = CurrentTbl90ReferenceSource.RegnumID,
        //                PhylumID = CurrentTbl90ReferenceSource.PhylumID,
        //                DivisionID = CurrentTbl90ReferenceSource.DivisionID,
        //                SubphylumID = CurrentTbl90ReferenceSource.SubphylumID,
        //                SubdivisionID = CurrentTbl90ReferenceSource.SubdivisionID,
        //                SuperclassID = CurrentTbl90ReferenceSource.SuperclassID,
        //                ClassID = CurrentTbl90ReferenceSource.ClassID,
        //                SubclassID = CurrentTbl90ReferenceSource.SubclassID,
        //                InfraclassID = CurrentTbl90ReferenceSource.InfraclassID,
        //                LegioID = CurrentTbl90ReferenceSource.LegioID,
        //                OrdoID = CurrentTbl90ReferenceSource.OrdoID,
        //                SubordoID = CurrentTbl90ReferenceSource.SubordoID,
        //                InfraordoID = CurrentTbl90ReferenceSource.InfraordoID,
        //                SuperfamilyID = CurrentTbl90ReferenceSource.SuperfamilyID,
        //                FamilyID = CurrentTbl90ReferenceSource.FamilyID,
        //                SubfamilyID = CurrentTbl90ReferenceSource.SubfamilyID,
        //                InfrafamilyID = CurrentTbl90ReferenceSource.InfrafamilyID,
        //                SupertribusID = CurrentTbl90ReferenceSource.SupertribusID,
        //                TribusID = CurrentTbl90ReferenceSource.TribusID,
        //                SubtribusID = CurrentTbl90ReferenceSource.SubtribusID,
        //                InfratribusID = CurrentTbl90ReferenceSource.InfratribusID,
        //                GenusID = CurrentTbl90ReferenceSource.GenusID,
        //                PlSpeciesID = CurrentTbl90ReferenceSource.PlSpeciesID,
        //                FiSpeciesID = CurrentTbl90ReferenceSource.FiSpeciesID,
        //                CountID = RandomHelper.Randomnumber(),
        //                Valid = CurrentTbl90ReferenceSource.Valid,
        //                ValidYear = CurrentTbl90ReferenceSource.ValidYear,
        //                Info = CurrentTbl90ReferenceSource.Info,
        //                Memo = CurrentTbl90ReferenceSource.Memo,
        //                Writer = Environment.UserName,
        //                WriterDate = DateTime.Now,
        //                Updater = Environment.UserName,
        //                UpdaterDate = DateTime.Now,
        //                EntityState = EntityState.Added
        //            };
        //        }
        //        {
        //            //RefExpertID or RefSourceID or RefAuthorID may be not 0
        //            if (CurrentTbl90ReferenceSource.RefExpertID == null && CurrentTbl90ReferenceSource.RefSourceID == null && CurrentTbl90ReferenceSource.RefAuthorID == null)
        //            {
        //                WpfMessageBox.Show(CultRes.StringsRes.RequiredGenealogyConnect, CultRes.StringsRes.RequiredInput,
        //                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
        //                return;
        //            }

        //            //check if dataset with vb-name already exist   
        //            var dataset = _businessLayer.ListTbl90ReferencesByRefExpertIdAndRefSourceIdAndRefAuthorIdAndInfo(CurrentTbl90ReferenceSource);

        //            if (dataset.Count != 0 && CurrentTbl90ReferenceSource.ReferenceID == 0)  //dataset exist
        //            {
        //                WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTbl90ReferenceSource.ReferenceID.ToString(),
        //                MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
        //                return;
        //            }
        //            if (dataset.Count == 0 && CurrentTbl90ReferenceSource.ReferenceID == 0 ||
        //                dataset.Count != 0 && CurrentTbl90ReferenceSource.ReferenceID != 0 ||
        //                dataset.Count == 0 && CurrentTbl90ReferenceSource.ReferenceID != 0) //new dataset and update
        //            {
        //                if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTbl90ReferenceSource.Info,
        //                        MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes)
        //                    return;
        //                {
        //                    try
        //                    {
        //                        _businessLayer.UpdateReference(reference);
        //                    }
        //                    catch (DbUpdateException e)
        //                    {
        //                        if (e.InnerException != null)
        //                            System.Windows.MessageBox.Show(e.InnerException.ToString(), CultRes.StringsRes.FailedToSave,
        //                                MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);

        //                        Log.Error(e);
        //                        return;
        //                    }
        //                    catch (Exception e)
        //                    {
        //                        System.Windows.MessageBox.Show(e.Message, CultRes.StringsRes.Error,
        //                            MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);
        //                        Log.Error(e);
        //                        return;
        //                    }
        //                    WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess,
        //                        CurrentTbl90ReferenceSource.ReferenceID == 0
        //                            ? CultRes.StringsRes.DatasetNew
        //                             : CurrentTbl90ReferenceSource.ReferenceID.ToString(),
        //                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
        //                }
        //            }
        //        }
        //    }
        //    catch (DbEntityValidationException ex)
        //    {
        //        _entityException.EntityException(ex);
        //        Log.Error(ex);
        //        return;
        //    }

        //    Tbl90ReferenceSourcesList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ReferenceListRefSourcesByPhylumId(CurrentTbl06Phylum.PhylumID));

        //    SelectedMainSubRefTabIndex = 1;

        //    ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
        //    ReferenceSourcesView.Refresh();
        //}
        //#endregion "Public Commands"                  

        //#region "Public Commands Connect ==> Tbl90ReferenceExpert"
        ////-------------------------------------------------------------------------

        //private RelayCommand _addReferenceExpertCommand;

        //public ICommand AddReferenceExpertCommand => _addReferenceExpertCommand ??
        //                                            (_addReferenceExpertCommand = new RelayCommand(delegate { AddReferenceExpert(null); }));

        //private RelayCommand _copyReferenceExpertCommand;

        //public ICommand CopyReferenceExpertCommand => _copyReferenceExpertCommand ??
        //                (_copyReferenceExpertCommand = new RelayCommand(delegate { CopyReferenceExpert(null); }));

        //private RelayCommand _deleteReferenceExpertCommand;

        //public ICommand DeleteReferenceExpertCommand => _deleteReferenceExpertCommand ??
        //                                                (_deleteReferenceExpertCommand = new RelayCommand(delegate { DeleteReferenceExpert(null); }));
        //private RelayCommand _saveReferenceExpertCommand;

        //public ICommand SaveReferenceExpertCommand => _saveReferenceExpertCommand ??
        //             (_saveReferenceExpertCommand = new RelayCommand(delegate { SaveReferenceExpert(null); }));
        ////-------------------------------------------------------------------------          

        //public void AddReferenceExpert(object o)
        //{
        //    if (Tbl90ReferenceExpertsList == null)
        //        Tbl90ReferenceExpertsList = new ObservableCollection<Tbl90Reference>();

        //    Tbl90ReferenceExpertsList.Insert(0, new Tbl90Reference { Info = CultRes.StringsRes.DatasetNew });

        //    ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
        //    ReferenceExpertsView.MoveCurrentToFirst();
        //}
        ////----------------------------------------------------------------------            

        //public void CopyReferenceExpert(object o)
        //{
        //    if (CurrentTbl90ReferenceExpert == null)
        //    {
        //        WpfMessageBox.Show(CultRes.StringsRes.DatasetNew,
        //            CultRes.StringsRes.RequiredInput,
        //            MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
        //        return;
        //    }

        //    var reference = _businessLayer.SingleListTbl90ReferencesByReferenceId(CurrentTbl90ReferenceExpert.ReferenceID);

        //    Tbl90ReferenceExpertsList.Insert(0, new Tbl90Reference
        //    {
        //        RefExpertID = reference.RefExpertID,
        //        Valid = reference.Valid,
        //        ValidYear = reference.ValidYear,
        //        Info = CultRes.StringsRes.DatasetNew,
        //        Memo = reference.Memo
        //    });

        //    ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
        //    ReferenceExpertsView.MoveCurrentToFirst();
        //}
        ////----------------------------------------------------------------------            

        //private void DeleteReferenceExpert(object o)
        //{
        //    if (CurrentTbl90ReferenceExpert == null)
        //    {
        //        WpfMessageBox.Show(CultRes.StringsRes.DatasetNew,
        //            CultRes.StringsRes.RequiredInput,
        //            MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
        //        return;
        //    }

        //    try
        //    {
        //        var reference = _businessLayer.SingleListTbl90ReferencesByReferenceId(CurrentTbl90ReferenceExpert.ReferenceID);
        //        if (reference != null)
        //        {
        //            if (WpfMessageBox.Show(CultRes.StringsRes.DeleteQuestion1, CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl90ReferenceExpert.Info,
        //                    MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes)
        //                return;
        //            reference.EntityState = EntityState.Deleted;
        //            _businessLayer.RemoveReference(reference);

        //            WpfMessageBox.Show(CultRes.StringsRes.DeleteSuccess, CurrentTbl90ReferenceExpert.Info,
        //                MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
        //        }
        //        else
        //        {
        //            WpfMessageBox.Show(CultRes.StringsRes.Information, CultRes.StringsRes.DeleteCan + " " + CurrentTbl90ReferenceExpert.Info + " " + CultRes.StringsRes.DeleteCan1,
        //                MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
        //        }
        //    }
        //    catch (DbEntityValidationException ex)
        //    {
        //        _entityException.EntityException(ex);
        //        Log.Error(ex);
        //    }

        //    Tbl90ReferenceExpertsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ReferenceListRefExpertsByPhylumId(CurrentTbl06Phylum.PhylumID));

        //    ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
        //    ReferenceExpertsView.Refresh();
        //}
        ////----------------------------------------------------------------------            

        //public void SaveReferenceExpert(object o)
        //{
        //    if (CurrentTbl90ReferenceExpert == null)
        //    {
        //        WpfMessageBox.Show(CultRes.StringsRes.DatasetNew,
        //            CultRes.StringsRes.RequiredInput,
        //            MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
        //        return;
        //    }

        //    CurrentTbl90ReferenceExpert.PhylumID = CurrentTbl06Phylum.PhylumID;

        //    try
        //    {
        //        var reference = _businessLayer.SingleListTbl90ReferencesByReferenceId(CurrentTbl90ReferenceExpert.ReferenceID);
        //        if (CurrentTbl90ReferenceExpert.ReferenceID != 0)
        //        {
        //            if (reference != null) //update
        //            {
        //                reference.RefExpertID = CurrentTbl90ReferenceExpert.RefExpertID;
        //                reference.RefAuthorID = CurrentTbl90ReferenceExpert.RefAuthorID;
        //                reference.RefSourceID = CurrentTbl90ReferenceExpert.RefSourceID;
        //                reference.RegnumID = CurrentTbl90ReferenceExpert.RegnumID;
        //                reference.PhylumID = CurrentTbl90ReferenceExpert.PhylumID;
        //                reference.DivisionID = CurrentTbl90ReferenceExpert.DivisionID;
        //                reference.SubphylumID = CurrentTbl90ReferenceExpert.SubphylumID;
        //                reference.SubdivisionID = CurrentTbl90ReferenceExpert.SubdivisionID;
        //                reference.SuperclassID = CurrentTbl90ReferenceExpert.SuperclassID;
        //                reference.ClassID = CurrentTbl90ReferenceExpert.ClassID;
        //                reference.SubclassID = CurrentTbl90ReferenceExpert.SubclassID;
        //                reference.InfraclassID = CurrentTbl90ReferenceExpert.InfraclassID;
        //                reference.LegioID = CurrentTbl90ReferenceExpert.LegioID;
        //                reference.OrdoID = CurrentTbl90ReferenceExpert.OrdoID;
        //                reference.SubordoID = CurrentTbl90ReferenceExpert.SubordoID;
        //                reference.InfraordoID = CurrentTbl90ReferenceExpert.InfraordoID;
        //                reference.SuperfamilyID = CurrentTbl90ReferenceExpert.SuperfamilyID;
        //                reference.FamilyID = CurrentTbl90ReferenceExpert.FamilyID;
        //                reference.SubfamilyID = CurrentTbl90ReferenceExpert.SubfamilyID;
        //                reference.InfrafamilyID = CurrentTbl90ReferenceExpert.InfrafamilyID;
        //                reference.SupertribusID = CurrentTbl90ReferenceExpert.SupertribusID;
        //                reference.TribusID = CurrentTbl90ReferenceExpert.TribusID;
        //                reference.SubtribusID = CurrentTbl90ReferenceExpert.SubtribusID;
        //                reference.InfratribusID = CurrentTbl90ReferenceExpert.InfratribusID;
        //                reference.GenusID = CurrentTbl90ReferenceExpert.GenusID;
        //                reference.PlSpeciesID = CurrentTbl90ReferenceExpert.PlSpeciesID;
        //                reference.FiSpeciesID = CurrentTbl90ReferenceExpert.FiSpeciesID;
        //                reference.Valid = CurrentTbl90ReferenceExpert.Valid;
        //                reference.ValidYear = CurrentTbl90ReferenceExpert.ValidYear;
        //                reference.Info = CurrentTbl90ReferenceExpert.Info;
        //                reference.Updater = Environment.UserName;
        //                reference.UpdaterDate = DateTime.Now;
        //                reference.Memo = CurrentTbl90ReferenceExpert.Memo;

        //                reference.EntityState = EntityState.Modified;
        //            }
        //        }
        //        else
        //        {
        //            reference = new Tbl90Reference     //add new
        //            {
        //                RefAuthorID = CurrentTbl90ReferenceExpert.RefAuthorID,
        //                RefSourceID = CurrentTbl90ReferenceExpert.RefSourceID,
        //                RefExpertID = CurrentTbl90ReferenceExpert.RefExpertID,
        //                RegnumID = CurrentTbl90ReferenceExpert.RegnumID,
        //                PhylumID = CurrentTbl90ReferenceExpert.PhylumID,
        //                DivisionID = CurrentTbl90ReferenceExpert.DivisionID,
        //                SubphylumID = CurrentTbl90ReferenceExpert.SubphylumID,
        //                SubdivisionID = CurrentTbl90ReferenceExpert.SubdivisionID,
        //                SuperclassID = CurrentTbl90ReferenceExpert.SuperclassID,
        //                ClassID = CurrentTbl90ReferenceExpert.ClassID,
        //                SubclassID = CurrentTbl90ReferenceExpert.SubclassID,
        //                InfraclassID = CurrentTbl90ReferenceExpert.InfraclassID,
        //                LegioID = CurrentTbl90ReferenceExpert.LegioID,
        //                OrdoID = CurrentTbl90ReferenceExpert.OrdoID,
        //                SubordoID = CurrentTbl90ReferenceExpert.SubordoID,
        //                InfraordoID = CurrentTbl90ReferenceExpert.InfraordoID,
        //                SuperfamilyID = CurrentTbl90ReferenceExpert.SuperfamilyID,
        //                FamilyID = CurrentTbl90ReferenceExpert.FamilyID,
        //                SubfamilyID = CurrentTbl90ReferenceExpert.SubfamilyID,
        //                InfrafamilyID = CurrentTbl90ReferenceExpert.InfrafamilyID,
        //                SupertribusID = CurrentTbl90ReferenceExpert.SupertribusID,
        //                TribusID = CurrentTbl90ReferenceExpert.TribusID,
        //                SubtribusID = CurrentTbl90ReferenceExpert.SubtribusID,
        //                InfratribusID = CurrentTbl90ReferenceExpert.InfratribusID,
        //                GenusID = CurrentTbl90ReferenceExpert.GenusID,
        //                PlSpeciesID = CurrentTbl90ReferenceExpert.PlSpeciesID,
        //                FiSpeciesID = CurrentTbl90ReferenceExpert.FiSpeciesID,
        //                CountID = RandomHelper.Randomnumber(),
        //                Valid = CurrentTbl90ReferenceExpert.Valid,
        //                ValidYear = CurrentTbl90ReferenceExpert.ValidYear,
        //                Info = CurrentTbl90ReferenceExpert.Info,
        //                Memo = CurrentTbl90ReferenceExpert.Memo,
        //                Writer = Environment.UserName,
        //                WriterDate = DateTime.Now,
        //                Updater = Environment.UserName,
        //                UpdaterDate = DateTime.Now,
        //                EntityState = EntityState.Added
        //            };
        //        }
        //        {
        //            //RefExpertID or RefSourceID or RefAuthorID may be not 0
        //            if (CurrentTbl90ReferenceExpert.RefExpertID == null && CurrentTbl90ReferenceExpert.RefSourceID == null && CurrentTbl90ReferenceExpert.RefAuthorID == null)
        //            {
        //                WpfMessageBox.Show(CultRes.StringsRes.RequiredGenealogyConnect, CultRes.StringsRes.RequiredInput,
        //                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
        //                return;
        //            }

        //            //check if dataset with vb-name already exist   
        //            var dataset = _businessLayer.ListTbl90ReferencesByRefExpertIdAndRefSourceIdAndRefAuthorIdAndInfo(CurrentTbl90ReferenceExpert);

        //            if (dataset.Count != 0 && CurrentTbl90ReferenceExpert.ReferenceID == 0)  //dataset exist
        //            {
        //                WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTbl90ReferenceExpert.ReferenceID.ToString(),
        //                MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
        //                return;
        //            }
        //            if (dataset.Count == 0 && CurrentTbl90ReferenceExpert.ReferenceID == 0 ||
        //                dataset.Count != 0 && CurrentTbl90ReferenceExpert.ReferenceID != 0 ||
        //                dataset.Count == 0 && CurrentTbl90ReferenceExpert.ReferenceID != 0) //new dataset and update
        //            {
        //                if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTbl90ReferenceExpert.Info,
        //                        MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes)
        //                    return;
        //                {
        //                    try
        //                    {
        //                        _businessLayer.UpdateReference(reference);
        //                    }
        //                    catch (DbUpdateException e)
        //                    {
        //                        if (e.InnerException != null)
        //                            System.Windows.MessageBox.Show(e.InnerException.ToString(), CultRes.StringsRes.FailedToSave,
        //                                MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);

        //                        Log.Error(e);
        //                        return;
        //                    }
        //                    catch (Exception e)
        //                    {
        //                        System.Windows.MessageBox.Show(e.Message, CultRes.StringsRes.Error,
        //                            MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);
        //                        Log.Error(e);
        //                        return;
        //                    }
        //                    WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess,
        //                        CurrentTbl90ReferenceExpert.ReferenceID == 0
        //                            ? CultRes.StringsRes.DatasetNew
        //                             : CurrentTbl90ReferenceExpert.ReferenceID.ToString(),
        //                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
        //                }
        //            }
        //        }
        //    }
        //    catch (DbEntityValidationException ex)
        //    {
        //        _entityException.EntityException(ex);
        //        Log.Error(ex);
        //        return;
        //    }

        //    Tbl90ReferenceExpertsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ReferenceListRefExpertsByPhylumId(CurrentTbl06Phylum.PhylumID));

        //    SelectedMainSubRefTabIndex = 0;

        //    ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
        //    ReferenceExpertsView.Refresh();
        //}
        //#endregion "Public Commands"                  

        //#region "Public Commands Connect ==> Tbl93Comment"

        ////-------------------------------------------------------------------------
        //private RelayCommand _addCommentCommand;

        //public ICommand AddCommentCommand => _addCommentCommand ??
        //                                         (_addCommentCommand = new RelayCommand(delegate { AddComment(null); }));

        //private RelayCommand _copyCommentCommand;

        //public ICommand CopyCommentCommand => _copyCommentCommand ??
        //                                          (_copyCommentCommand = new RelayCommand(delegate { CopyComment(null); }));

        //private RelayCommand _deleteCommentCommand;

        //public ICommand DeleteCommentCommand => _deleteCommentCommand ??
        //                                                (_deleteCommentCommand = new RelayCommand(delegate { DeleteComment(null); }));

        //private RelayCommand _saveCommentCommand;

        //public ICommand SaveCommentCommand => _saveCommentCommand ??
        //                                          (_saveCommentCommand = new RelayCommand(delegate { SaveComment(null); }));
        ////-------------------------------------------------------------------------          

        //public void AddComment(object o)
        //{
        //    if (Tbl93CommentsList == null)
        //        Tbl93CommentsList = new ObservableCollection<Tbl93Comment>();

        //    Tbl93CommentsList.Insert(0, new Tbl93Comment { Info = CultRes.StringsRes.DatasetNew });

        //    CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
        //    CommentsView.MoveCurrentToFirst();
        //}
        ////----------------------------------------------------------------------            

        //public void CopyComment(object o)
        //{
        //    if (CurrentTbl93Comment == null)
        //    {
        //        WpfMessageBox.Show(CultRes.StringsRes.DatasetNew,
        //            CultRes.StringsRes.RequiredInput,
        //            MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
        //        return;
        //    }

        //    var comment = _businessLayer.SingleListTbl93CommentsByCommentId(CurrentTbl93Comment.CommentID);

        //    Tbl93CommentsList.Insert(0, new Tbl93Comment
        //    {
        //        Valid = comment.Valid,
        //        ValidYear = comment.ValidYear,
        //        Info = CultRes.StringsRes.DatasetNew,
        //        Memo = comment.Memo
        //    });

        //    CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
        //    CommentsView.MoveCurrentToFirst();
        //}
        ////----------------------------------------------------------------------            

        //private void DeleteComment(object o)
        //{
        //    if (CurrentTbl93Comment == null)
        //    {
        //        WpfMessageBox.Show(CultRes.StringsRes.DatasetNew,
        //            CultRes.StringsRes.RequiredInput,
        //            MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
        //        return;
        //    }

        //    try
        //    {
        //        var comment = _businessLayer.SingleListTbl93CommentsByCommentId(CurrentTbl93Comment.CommentID);
        //        if (comment != null)
        //        {
        //            if (WpfMessageBox.Show(CultRes.StringsRes.DeleteQuestion1, CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl93Comment.Info,
        //                    MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes)
        //                return;
        //            comment.EntityState = EntityState.Deleted;
        //            _businessLayer.RemoveComment(comment);

        //            WpfMessageBox.Show(CultRes.StringsRes.DeleteSuccess, CurrentTbl93Comment.Info,
        //                MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
        //        }
        //        else
        //        {
        //            WpfMessageBox.Show(CultRes.StringsRes.Information, CultRes.StringsRes.DeleteCan + " " + CurrentTbl93Comment.Info + " " + CultRes.StringsRes.DeleteCan1,
        //                MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
        //        }
        //    }
        //    catch (DbEntityValidationException ex)
        //    {
        //        _entityException.EntityException(ex);
        //        Log.Error(ex);
        //    }

        //    Tbl93CommentsList = new ObservableCollection<Tbl93Comment>(_businessLayer.ListTbl93CommentsByPhylumId(CurrentTbl06Phylum.PhylumID));

        //    CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
        //    CommentsView.Refresh();
        //}
        ////----------------------------------------------------------------------            

        //private void SaveComment(object o)
        //{
        //    if (CurrentTbl93Comment == null)
        //    {
        //        WpfMessageBox.Show(CultRes.StringsRes.DatasetNew,
        //            CultRes.StringsRes.RequiredInput,
        //            MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
        //        return;
        //    }

        //    CurrentTbl93Comment.PhylumID = CurrentTbl06Phylum.PhylumID;

        //    try
        //    {
        //        var comment = _businessLayer.SingleListTbl93CommentsByCommentId(CurrentTbl93Comment.CommentID);
        //        if (CurrentTbl93Comment.CommentID != 0)
        //        {
        //            if (comment != null) //update
        //            {
        //                comment.RegnumID = CurrentTbl93Comment.RegnumID;
        //                comment.PhylumID = CurrentTbl93Comment.PhylumID;
        //                comment.DivisionID = CurrentTbl93Comment.DivisionID;
        //                comment.SubphylumID = CurrentTbl93Comment.SubphylumID;
        //                comment.SubdivisionID = CurrentTbl93Comment.SubdivisionID;
        //                comment.SuperclassID = CurrentTbl93Comment.SuperclassID;
        //                comment.ClassID = CurrentTbl93Comment.ClassID;
        //                comment.SubclassID = CurrentTbl93Comment.SubclassID;
        //                comment.InfraclassID = CurrentTbl93Comment.InfraclassID;
        //                comment.LegioID = CurrentTbl93Comment.LegioID;
        //                comment.OrdoID = CurrentTbl93Comment.OrdoID;
        //                comment.SubordoID = CurrentTbl93Comment.SubordoID;
        //                comment.InfraordoID = CurrentTbl93Comment.InfraordoID;
        //                comment.SuperfamilyID = CurrentTbl93Comment.SuperfamilyID;
        //                comment.FamilyID = CurrentTbl93Comment.FamilyID;
        //                comment.SubfamilyID = CurrentTbl93Comment.SubfamilyID;
        //                comment.InfrafamilyID = CurrentTbl93Comment.InfrafamilyID;
        //                comment.SupertribusID = CurrentTbl93Comment.SupertribusID;
        //                comment.TribusID = CurrentTbl93Comment.TribusID;
        //                comment.SubtribusID = CurrentTbl93Comment.SubtribusID;
        //                comment.InfratribusID = CurrentTbl93Comment.InfratribusID;
        //                comment.GenusID = CurrentTbl93Comment.GenusID;
        //                comment.PlSpeciesID = CurrentTbl93Comment.PlSpeciesID;
        //                comment.FiSpeciesID = CurrentTbl93Comment.FiSpeciesID;
        //                comment.Valid = CurrentTbl93Comment.Valid;
        //                comment.ValidYear = CurrentTbl93Comment.ValidYear;
        //                comment.Info = CurrentTbl93Comment.Info;
        //                comment.Memo = CurrentTbl93Comment.Memo;
        //                comment.Updater = Environment.UserName;
        //                comment.UpdaterDate = DateTime.Now;
        //                comment.EntityState = EntityState.Modified;
        //            }
        //        }
        //        else
        //        {
        //            comment = new Tbl93Comment     //add new
        //            {
        //                RegnumID = CurrentTbl93Comment.RegnumID,
        //                PhylumID = CurrentTbl93Comment.PhylumID,
        //                DivisionID = CurrentTbl93Comment.DivisionID,
        //                SubphylumID = CurrentTbl93Comment.SubphylumID,
        //                SubdivisionID = CurrentTbl93Comment.SubdivisionID,
        //                SuperclassID = CurrentTbl93Comment.SuperclassID,
        //                ClassID = CurrentTbl93Comment.ClassID,
        //                SubclassID = CurrentTbl93Comment.SubclassID,
        //                InfraclassID = CurrentTbl93Comment.InfraclassID,
        //                LegioID = CurrentTbl93Comment.LegioID,
        //                OrdoID = CurrentTbl93Comment.OrdoID,
        //                SubordoID = CurrentTbl93Comment.SubordoID,
        //                InfraordoID = CurrentTbl93Comment.InfraordoID,
        //                SuperfamilyID = CurrentTbl93Comment.SuperfamilyID,
        //                FamilyID = CurrentTbl93Comment.FamilyID,
        //                SubfamilyID = CurrentTbl93Comment.SubfamilyID,
        //                InfrafamilyID = CurrentTbl93Comment.InfrafamilyID,
        //                SupertribusID = CurrentTbl93Comment.SupertribusID,
        //                TribusID = CurrentTbl93Comment.TribusID,
        //                SubtribusID = CurrentTbl93Comment.SubtribusID,
        //                InfratribusID = CurrentTbl93Comment.InfratribusID,
        //                GenusID = CurrentTbl93Comment.GenusID,
        //                PlSpeciesID = CurrentTbl93Comment.PlSpeciesID,
        //                FiSpeciesID = CurrentTbl93Comment.FiSpeciesID,
        //                CountID = RandomHelper.Randomnumber(),
        //                Valid = CurrentTbl93Comment.Valid,
        //                ValidYear = CurrentTbl93Comment.ValidYear,
        //                Info = CurrentTbl93Comment.Info,
        //                Memo = CurrentTbl93Comment.Memo,
        //                Writer = Environment.UserName,
        //                WriterDate = DateTime.Now,
        //                Updater = Environment.UserName,
        //                UpdaterDate = DateTime.Now,
        //                EntityState = EntityState.Added
        //            };
        //        }
        //        {
        //            //check if dataset with Name and VbIds already exist       
        //            var dataset = _businessLayer.ListTbl93CommentsByCurrentItem(CurrentTbl93Comment);

        //            if (dataset.Count != 0 && CurrentTbl93Comment.CommentID == 0)  //dataset exist
        //            {
        //                WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTbl93Comment.Info,
        //                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
        //                return;
        //            }

        //            if (dataset.Count == 0 && CurrentTbl93Comment.CommentID == 0 ||
        //                dataset.Count != 0 && CurrentTbl93Comment.CommentID != 0 ||
        //                dataset.Count == 0 && CurrentTbl93Comment.CommentID != 0) //new dataset and update
        //            {
        //                if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTbl93Comment.Info,
        //                        MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes)
        //                    return;
        //                {
        //                    try
        //                    {
        //                        _businessLayer.UpdateComment(comment);
        //                    }
        //                    catch (DbUpdateException e)
        //                    {
        //                        if (e.InnerException != null)
        //                            System.Windows.MessageBox.Show(e.InnerException.ToString(), CultRes.StringsRes.FailedToSave,
        //                                MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);

        //                        Log.Error(e);
        //                        return;
        //                    }
        //                    catch (Exception e)
        //                    {
        //                        System.Windows.MessageBox.Show(e.Message, CultRes.StringsRes.Error,
        //                            MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);
        //                        Log.Error(e);
        //                        return;
        //                    }
        //                    WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess,
        //                        CurrentTbl93Comment.CommentID == 0
        //                            ? CultRes.StringsRes.DatasetNew
        //                            : CurrentTbl93Comment.Info,
        //                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
        //                }
        //            }
        //        }
        //    }
        //    catch (DbEntityValidationException ex)
        //    {
        //        _entityException.EntityException(ex);
        //        Log.Error(ex);
        //        return;
        //    }

        //    Tbl93CommentsList = new ObservableCollection<Tbl93Comment>(_businessLayer.ListTbl93CommentsByPhylumId(CurrentTbl06Phylum.PhylumID));

        //    SelectedMainTabIndex = 3;

        //    CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
        //    CommentsView.Refresh();
        //}
        //#endregion "Public Commands"                  


        //    Part 9    


        #region "Public Commands Connected Tables by DoubleClick"

        private RelayCommand _getConnectedTablesCommand;

        public ICommand GetConnectedTablesCommand => _getConnectedTablesCommand ??= new RelayCommand(delegate
        {
            GetConnectedTablesById(null);
        });

        private void GetConnectedTablesById(object o)
        {
            Tbl12SubphylumsList?.Clear();
            Tbl90ReferenceExpertsList?.Clear();
            Tbl90ReferenceSourcesList?.Clear();
            Tbl90ReferenceAuthorsList?.Clear();
            Tbl93CommentsList?.Clear();

            SelectedMainTabIndex = 0; //change to Connect tab
            SelectedMainSubRefTabIndex = 0;
            SelectedDetailTabIndex = 1;
            SelectedDetailSubTabIndex = 0;
            SelectedDetailSubRefTabIndex = 0;

            //Tbl03RegnumsList = new ObservableCollection<Tbl03Regnum>(
            //        _businessLayer.ListTbl03RegnumsByRegnumId(CurrentTbl06Phylum.RegnumId));


            RegnumsView = CollectionViewSource.GetDefaultView(Tbl03RegnumsList);
            RegnumsView.Refresh();
        }

        #endregion "Public Commands Connected Tables by DoubleClick"


        //    Part 10    


        #region "Public Commands to open Detail TabItems"

        private int _selectedMainTabIndex;
        private int _selectedMainSubRefTabIndex;
        private int _selectedDetailTabIndex;
        private int _selectedDetailSubTabIndex;
        private int _selectedDetailSubRefTabIndex;

        public int SelectedMainTabIndex
        {
            get => _selectedMainTabIndex;
            set
            {
                if (value == _selectedMainTabIndex) return;
                _selectedMainTabIndex = value;
                RaisePropertyChanged("");
                if (_selectedMainTabIndex == 0)
                    SelectedDetailSubTabIndex = 0;
                if (_selectedMainTabIndex == 1)
                {
                    SelectedDetailTabIndex = 1;
                    SelectedDetailSubTabIndex = 1;
                }

                if (_selectedMainTabIndex == 2)
                {
                    SelectedDetailTabIndex = 1;
                    SelectedDetailSubTabIndex = 2;
                }

                if (_selectedMainTabIndex == 3)
                {
                    SelectedDetailTabIndex = 1;
                    SelectedDetailSubTabIndex = 3;
                }
            }
        }

        public int SelectedMainSubRefTabIndex
        {
            get => _selectedMainSubRefTabIndex;
            set
            {
                if (value == _selectedMainSubRefTabIndex) return;
                _selectedMainSubRefTabIndex = value;
                RaisePropertyChanged("");
                if (_selectedMainSubRefTabIndex == 0)
                    SelectedDetailSubRefTabIndex = 0;
                if (_selectedMainSubRefTabIndex == 1)
                    SelectedDetailSubRefTabIndex = 1;
                if (_selectedMainSubRefTabIndex == 2)
                    SelectedDetailSubRefTabIndex = 2;
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
                    SelectedDetailSubTabIndex = 0;
                    SelectedMainTabIndex = 0;
                }

                if (_selectedDetailTabIndex == 1)
                    SelectedDetailSubTabIndex = 1;
                if (_selectedDetailTabIndex == 2)
                    SelectedDetailSubTabIndex = 2;
                if (_selectedDetailTabIndex == 3)
                    SelectedDetailSubTabIndex = 3;
            }
        }

        public int SelectedDetailSubTabIndex
        {
            get => _selectedDetailSubTabIndex;
            set
            {
                if (value == _selectedDetailSubTabIndex) return;
                _selectedDetailSubTabIndex = value;
                RaisePropertyChanged("");
                if (_selectedDetailSubTabIndex == 0)
                {
                    //             Tbl03RegnumsList = new ObservableCollection<Tbl03Regnum>(_businessLayer.ListTbl03RegnumsByRegnumId(CurrentTbl06Phylum.RegnumId));
                    Tbl03RegnumsList = new ObservableCollection<Tbl03Regnum>(_uow.Tbl03Regnums
                        .Find(e => e.RegnumId == CurrentTbl06Phylum.RegnumId)
                        .OrderBy(k => k.RegnumName)
                        .ThenBy(k => k.Subregnum));

                    RegnumsView = CollectionViewSource.GetDefaultView(Tbl03RegnumsList);
                    RegnumsView.Refresh();

                    SelectedMainTabIndex = 0;
                }

                if (_selectedDetailSubTabIndex == 1)
                {
                    //Tbl12SubphylumsList = new ObservableCollection<Tbl12Subphylum>(
                    //    _businessLayer.ListTbl12SubphylumsByPhylumId(CurrentTbl06Phylum.PhylumId));

                    Tbl12SubphylumsList = new ObservableCollection<Tbl12Subphylum>(_uow.Tbl12Subphylums
                        .Find(e => e.PhylumId == CurrentTbl06Phylum.PhylumId)
                        .OrderBy(k => k.SubphylumName));

                    Tbl06PhylumsAllList = _extGet.AllCollection<Tbl06Phylum>("phylum");

                    SubphylumsView = CollectionViewSource.GetDefaultView(Tbl12SubphylumsList);
                    SubphylumsView.Refresh();

                    SelectedMainTabIndex = 1;
                }

                if (_selectedDetailSubTabIndex == 2)
                {
                    //Tbl90ExpertsAllList = new ObservableCollection<Tbl90RefExpert>(
                    //    _businessLayer.ListTbl90RefExperts());
                    //Tbl90ReferenceExpertsList = new ObservableCollection<Tbl90Reference>(
                    //    _businessLayer.ListTbl90ReferenceListRefExpertsByPhylumId(CurrentTbl06Phylum.PhylumId));

                    //ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
                    //ReferenceExpertsView.Refresh();

                    SelectedMainTabIndex = 2;
                }

                if (_selectedDetailSubTabIndex == 3)
                {
                    //Tbl93CommentsList = new ObservableCollection<Tbl93Comment>(
                    //    _businessLayer.ListTbl93CommentsByPhylumId(CurrentTbl06Phylum.PhylumId));

                    Tbl93CommentsList = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments
                        .Find(e => e.PhylumId == CurrentTbl06Phylum.PhylumId)
                        .OrderBy(k => k.Info));

                    CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
                    CommentsView.Refresh();

                    SelectedMainTabIndex = 3;
                }
            }
        }

        public int SelectedDetailSubRefTabIndex
        {
            get => _selectedDetailSubRefTabIndex;
            set
            {
                if (value == _selectedDetailSubRefTabIndex) return;
                _selectedDetailSubRefTabIndex = value;
                RaisePropertyChanged("");
                if (_selectedDetailSubRefTabIndex == 0)
                {
                    //Tbl90ExpertsAllList = new ObservableCollection<Tbl90RefExpert>(
                    //    _businessLayer.ListTbl90RefExperts());

                    Tbl90ExpertsAllList =
                        new ObservableCollection<Tbl90RefExpert>(_uow.Tbl90RefExperts.ListTbl90RefExpertsOrderBy());

                    //Tbl90ReferenceExpertsList = new ObservableCollection<Tbl90Reference>(
                    //    _businessLayer.ListTbl90ReferenceListRefExpertsByPhylumId(CurrentTbl06Phylum.PhylumId));

                    //ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
                    //ReferenceExpertsView.Refresh();

                    SelectedMainSubRefTabIndex = 0;
                }

                if (_selectedDetailSubRefTabIndex == 1)
                {
                    //Tbl90SourcesAllList = new ObservableCollection<Tbl90RefSource>(
                    //    _businessLayer.ListTbl90RefSources());
                    Tbl90SourcesAllList =
                        new ObservableCollection<Tbl90RefSource>(_uow.Tbl90RefSources.ListTbl90RefSourcesOrderBy());

                    //Tbl90ReferenceSourcesList = new ObservableCollection<Tbl90Reference>(
                    //    _businessLayer.ListTbl90ReferenceListRefSourcesByPhylumId(CurrentTbl06Phylum.PhylumId));

                    //ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
                    //ReferenceSourcesView.Refresh();

                    SelectedMainSubRefTabIndex = 1;
                }

                if (_selectedDetailSubRefTabIndex == 2)
                {
                    //Tbl90AuthorsAllList = new ObservableCollection<Tbl90RefAuthor>(
                    //    _businessLayer.ListTbl90RefAuthors());
                    Tbl90AuthorsAllList =
                        new ObservableCollection<Tbl90RefAuthor>(_uow.Tbl90RefAuthors.ListTbl90RefAuthorsOrderBy());

                    //Tbl90ReferenceAuthorsList = new ObservableCollection<Tbl90Reference>(
                    //    _businessLayer.ListTbl90ReferenceListRefAuthorsByPhylumId(CurrentTbl06Phylum.PhylumId));

                    Tbl90ReferenceAuthorsList = new ObservableCollection<Tbl90Reference>(_uow.Tbl90References
                        .Find(e => e.PhylumId == CurrentTbl06Phylum.PhylumId)
                        .OrderBy(k => k.Info));

                    ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
                    ReferenceAuthorsView.Refresh();

                    SelectedMainSubRefTabIndex = 2;
                }
            }
        }

        #endregion "Public Commands to open Detail TabItems"


        //    Part 11    


        #region "Public Properties Tbl06Phylum"

        private string _searchPhylumName = "";

        public string SearchPhylumName
        {
            get => _searchPhylumName;
            set { _searchPhylumName = value; RaisePropertyChanged(""); }
        }

        public ICollectionView PhylumsView;
        private Tbl06Phylum CurrentTbl06Phylum => PhylumsView?.CurrentItem as Tbl06Phylum;

        //      public ObservableCollection<Tbl06Phylum> Tbl06PhylumsList { get; set; }
      //  public ObservableCollection<Tbl06Phylum> Tbl06PhylumsAllList { get; set; }


        private ObservableCollection<Tbl06Phylum> _tbl06PhylumsList;

        public ObservableCollection<Tbl06Phylum> Tbl06PhylumsList
        {
            get => _tbl06PhylumsList;
            set { _tbl06PhylumsList = value; RaisePropertyChanged(""); }
        }

        private ObservableCollection<Tbl06Phylum> _tbl06PhylumsAllList;
        public ObservableCollection<Tbl06Phylum> Tbl06PhylumsAllList
        {
            get => _tbl06PhylumsAllList;
            set { _tbl06PhylumsAllList = value; RaisePropertyChanged(""); }
        }

        #endregion "Public Properties"

        #region "Public Properties Tbl03Regnum"

        public ICollectionView RegnumsView;
        private Tbl03Regnum CurrentTbl03Regnum => RegnumsView?.CurrentItem as Tbl03Regnum;

        //public ObservableCollection<Tbl03Regnum> Tbl03RegnumsList { get; set; }
        //public ObservableCollection<Tbl03Regnum> Tbl03RegnumsAllList { get; set; }


        private ObservableCollection<Tbl03Regnum> _tbl03RegnumsList;

        public ObservableCollection<Tbl03Regnum> Tbl03RegnumsList
        {
            get => _tbl03RegnumsList;
            set { _tbl03RegnumsList = value; RaisePropertyChanged(""); }
        }

        private ObservableCollection<Tbl03Regnum> _tbl03RegnumsAllList;

        public ObservableCollection<Tbl03Regnum> Tbl03RegnumsAllList
        {
            get => _tbl03RegnumsAllList;
            set { _tbl03RegnumsAllList = value; RaisePropertyChanged(""); }
        }

        #endregion "Public Properties"

        #region "Public Properties Tbl12Subphylum"

        public ICollectionView SubphylumsView;
        private Tbl12Subphylum CurrentTbl12Subphylum => SubphylumsView?.CurrentItem as Tbl12Subphylum;

        //      public ObservableCollection<Tbl12Subphylum> Tbl12SubphylumsList { get; set; }


        private ObservableCollection<Tbl12Subphylum> _tbl12SubphylumsList;

        public ObservableCollection<Tbl12Subphylum> Tbl12SubphylumsList
        {
            get => _tbl12SubphylumsList;
            set { _tbl12SubphylumsList = value; RaisePropertyChanged(""); }
        }

        #endregion "Public Properties"

        #region "Public Properties Tbl18Superclass"

        public ICollectionView SuperclassesView;
        private Tbl18Superclass CurrentTbl18Superclass => SuperclassesView?.CurrentItem as Tbl18Superclass;

        //   public ObservableCollection<Tbl18Superclass> Tbl18SuperclassesList { get; set; }


        private ObservableCollection<Tbl18Superclass> _tbl18SuperclassesList;

        public ObservableCollection<Tbl18Superclass> Tbl18SuperclassesList
        {
            get => _tbl18SuperclassesList;
            set { _tbl18SuperclassesList = value; RaisePropertyChanged(""); }
        }

        #endregion "Public Properties"

        #region "Public Properties Tbl90Author"

        //    public ObservableCollection<Tbl90RefAuthor> Tbl90AuthorsAllList { get; set; }

        private ObservableCollection<Tbl90RefAuthor> _tbl90AuthorsAllList;

        public ObservableCollection<Tbl90RefAuthor> Tbl90AuthorsAllList
        {
            get => _tbl90AuthorsAllList;
            set { _tbl90AuthorsAllList = value; RaisePropertyChanged(""); }
        }

        #endregion "Public Properties "

        #region "Public Properties Tbl90Source"

        //    public ObservableCollection<Tbl90RefSource> Tbl90SourcesAllList { get; set; }

        private ObservableCollection<Tbl90RefSource> _tbl90SourcesAllList;

        public ObservableCollection<Tbl90RefSource> Tbl90SourcesAllList
        {
            get => _tbl90SourcesAllList;
            set { _tbl90SourcesAllList = value; RaisePropertyChanged(""); }
        }

        #endregion "Public Properties "

        #region "Public Properties Tbl90Expert"

        //    public ObservableCollection<Tbl90RefExpert> Tbl90ExpertsAllList { get; set; }

        private ObservableCollection<Tbl90RefExpert> _tbl90ExpertsAllList;

        public ObservableCollection<Tbl90RefExpert> Tbl90ExpertsAllList
        {
            get => _tbl90ExpertsAllList;
            set { _tbl90ExpertsAllList = value; RaisePropertyChanged(""); }
        }

        #endregion "Public Properties "

        #region Public Properties Tbl90References

        private ObservableCollection<Tbl90Reference> _tbl90ReferencesList;

        public ObservableCollection<Tbl90Reference> Tbl90ReferencesList
        {
            get => _tbl90ReferencesList;
            set { _tbl90ReferencesList = value; RaisePropertyChanged(""); }
        }


        #endregion
        #region "Public Properties Tbl90ReferenceAuthor"

        public ICollectionView ReferenceAuthorsView;
        private Tbl90Reference CurrentTbl90ReferenceAuthor => ReferenceAuthorsView?.CurrentItem as Tbl90Reference;

        //    public ObservableCollection<Tbl90Reference> Tbl90ReferenceAuthorsList { get; set; }

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

        //   public ObservableCollection<Tbl90Reference> Tbl90ReferenceSourcesList { get; set; }

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

        //    public ObservableCollection<Tbl90Reference> Tbl90ReferenceExpertsList { get; set; }

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

        //   public ObservableCollection<Tbl93Comment> Tbl93CommentsList { get; set; }

        private ObservableCollection<Tbl93Comment> _tbl93CommentsList;

        public ObservableCollection<Tbl93Comment> Tbl93CommentsList
        {
            get => _tbl93CommentsList;
            set { _tbl93CommentsList = value; RaisePropertyChanged(""); }
        }

        #endregion "Public Properties"
        #endregion "Public Properties"






    }
}
