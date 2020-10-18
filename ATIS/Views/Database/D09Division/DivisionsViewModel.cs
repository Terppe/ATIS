using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using ATIS.Dal.Models;
using ATIS.Ui.Core;
using ATIS.Ui.Helper;
using Common.Logging;
using Microsoft.EntityFrameworkCore;

namespace ATIS.Ui.Views.Database.D09Division
{
    public class DivisionsViewModel : ViewModelBase
    {

        #region "Private Data Members"
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        //      private static IBusinessLayer _businessLayer;
        //    private static DbEntityException _entityException;
        private readonly UnitOfWork _uow = new UnitOfWork(new AtisDbContext());
        private readonly AtisDbContext _context = new AtisDbContext();
        private int _position;

        #endregion "Private Data Members"               

        #region "Constructor"

        public DivisionsViewModel()
        {
            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.
            }
            else
            {

                // Code runs "for real" 
       //         _entityException = new DbEntityException();
            }
        }

        public bool IsInDesignMode { get; set; }

        #endregion "Constructor"         


        //    Part 1    


        #region "Public Commands Basic Tbl09Division"
        //-------------------------------------------------------------------------
        private RelayCommand _clearDivisionCommand;

        public ICommand ClearDivisionCommand => _clearDivisionCommand ??= new RelayCommand(delegate { ClearDivision(null); });

        private RelayCommand _getDivisionsByNameOrIdCommand;

        public ICommand GetDivisionsByNameOrIdCommand => _getDivisionsByNameOrIdCommand ??= new RelayCommand(delegate { GetDivisionsByNameOrId(null); });

        private RelayCommand _addDivisionCommand;

        public ICommand AddDivisionCommand => _addDivisionCommand ??= new RelayCommand(delegate { AddDivision(null); });

        private RelayCommand _copyDivisionCommand;

        public ICommand CopyDivisionCommand => _copyDivisionCommand ??= new RelayCommand(delegate { CopyDivision(null); });

        private RelayCommand _deleteDivisionCommand;

        public ICommand DeleteDivisionCommand => _deleteDivisionCommand ??= new RelayCommand(delegate { DeleteDivision(null); });

        private RelayCommand _saveDivisionCommand;

        public ICommand SaveDivisionCommand => _saveDivisionCommand ??= new RelayCommand(delegate { SaveDivision(null); });
        //-------------------------------------------------------------------------          

        private void ClearDivision(object o)
        {
            SearchDivisionName = "";

            SelectedMainTabIndex = 0;  //change tab
            SelectedDetailTabIndex = 0;
            SelectedDetailSubTabIndex = 0;
            SelectedDetailSubRefTabIndex = 0;

            Tbl03RegnumsList?.Clear();
            Tbl09DivisionsList?.Clear();
            Tbl15SubdivisionsList?.Clear();
            Tbl90ReferenceExpertsList?.Clear();
            Tbl90ReferenceSourcesList?.Clear();
            Tbl90ReferenceAuthorsList?.Clear();
            Tbl93CommentsList?.Clear();
        }
        //----------------------------------------------------------------------                  

        private void GetDivisionsByNameOrId(object o)
        {
            if (SearchDivisionName != "")
            {
                Tbl09DivisionsList?.Clear();
                if (SearchDivisionName == "*") // show whole table
                {
                    SearchDivisionName = "";
             //       _businessLayer = new BusinessLayer.BusinessLayer();
                //    Tbl03RegnumsAllList = new ObservableCollection<Tbl03Regnum>(_businessLayer.ListTbl03Regnums());
                //    Tbl09DivisionsList = new ObservableCollection<Tbl09Division>(_businessLayer.ListTbl09DivisionsByDivisionName(SearchDivisionName));
                    Tbl03RegnumsAllList = new ObservableCollection<Tbl03Regnum>(_uow.Tbl03Regnums.GetAll());
                    Tbl09DivisionsList = new ObservableCollection<Tbl09Division>(_uow.Tbl09Divisions.GetAll());
                    SearchDivisionName = "*";
                }
                else
                {
               //     _businessLayer = new BusinessLayer.BusinessLayer();
                 //   Tbl03RegnumsAllList = new ObservableCollection<Tbl03Regnum>(_businessLayer.ListTbl03Regnums());
                    Tbl03RegnumsAllList = new ObservableCollection<Tbl03Regnum>(_uow.Tbl03Regnums.GetAll());
                    //Tbl09DivisionsList = int.TryParse(SearchDivisionName, out var id) ?
                    //    new ObservableCollection<Tbl09Division>(_businessLayer.ListTbl09DivisionsByDivisionId(id)) :
                    //    new ObservableCollection<Tbl09Division>(_businessLayer.ListTbl09DivisionsByDivisionName(SearchDivisionName));
                    Tbl09DivisionsList = int.TryParse(SearchDivisionName, out var id)
                        ? new ObservableCollection<Tbl09Division>(_uow.Tbl09Divisions
                            .Find(e => e.DivisionId == id))
                        : new ObservableCollection<Tbl09Division>(_uow.Tbl09Divisions.ListTbl09DivisionsOnlyPlantaeOrderBy(SearchDivisionName));

                }

                if (Tbl09DivisionsList.Count == 0)
                {
                    MessageBox.Show(CultRes.StringsRes.Tables, CultRes.StringsRes.DatasetNot,
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                }
                else
                {
                    Tbl03RegnumsList?.Clear();
                    Tbl15SubdivisionsList?.Clear();
                    Tbl90ReferenceExpertsList?.Clear();
                    Tbl90ReferenceSourcesList?.Clear();
                    Tbl90ReferenceAuthorsList?.Clear();
                    Tbl93CommentsList?.Clear();
                }
            }
            else
            {
                Tbl09DivisionsList = new ObservableCollection<Tbl09Division>();

                MessageBox.Show(CultRes.StringsRes.SearchNameOrId, CultRes.StringsRes.InputRequested,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
            }
            DivisionsView = CollectionViewSource.GetDefaultView(Tbl09DivisionsList);
            DivisionsView.Refresh();
        }
        //------------------------------------------------------------------------------------                          

        private void AddDivision(object o)
        {
            if (Tbl09DivisionsList == null)
                Tbl09DivisionsList = new ObservableCollection<Tbl09Division>();

            Tbl09DivisionsList.Insert(0, new Tbl09Division { DivisionName = CultRes.StringsRes.DatasetNew });

        //    _businessLayer = new BusinessLayer.BusinessLayer();
         //   Tbl03RegnumsAllList = new ObservableCollection<Tbl03Regnum>(_businessLayer.ListTbl03Regnums());
            Tbl03RegnumsAllList = new ObservableCollection<Tbl03Regnum>(_uow.Tbl03Regnums.GetAll());

            DivisionsView = CollectionViewSource.GetDefaultView(Tbl09DivisionsList);
            DivisionsView.MoveCurrentToFirst();
        }
        //------------------------------------------------------------------------------------                               

        private void CopyDivision(object o)
        {
            if (CurrentTbl09Division == null)
            {
                MessageBox.Show(CultRes.StringsRes.DatasetNew,
                    CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                return;
            }
         //   _businessLayer = new BusinessLayer.BusinessLayer();

        //    var division = _businessLayer.SingleListTbl09DivisionsByDivisionId(CurrentTbl09Division.DivisionId);
            var division = _uow.Tbl09Divisions.GetById(CurrentTbl09Division.DivisionId);

            Tbl09DivisionsList.Insert(0, new Tbl09Division
            {
                RegnumId = division.RegnumId,
                DivisionName = CultRes.StringsRes.DatasetNew,
                Valid = division.Valid,
                ValidYear = division.ValidYear,
                Synonym = division.Synonym,
                Author = division.Author,
                AuthorYear = division.AuthorYear,
                Info = division.Info,
                EngName = division.EngName,
                GerName = division.GerName,
                FraName = division.FraName,
                PorName = division.PorName,
                Memo = division.Memo
            });

            DivisionsView = CollectionViewSource.GetDefaultView(Tbl09DivisionsList);
            DivisionsView.MoveCurrentToFirst();
        }
        //---------------------------------------------------------------------------------------                            

        private void DeleteDivision(object o)
        {
            if (CurrentTbl09Division == null)
            {
                MessageBox.Show(CultRes.StringsRes.DatasetNew,
                    CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                return;
            }
          //  _businessLayer = new BusinessLayer.BusinessLayer();

            var ret = false;
            //check if in Tbl15Subdivisions connected datasets, than return
        //    Tbl15SubdivisionsList = new ObservableCollection<Tbl15Subdivision>(_businessLayer.ListTbl15SubdivisionsByDivisionId(CurrentTbl09Division.DivisionId));
            Tbl15SubdivisionsList = new ObservableCollection<Tbl15Subdivision>(_uow.Tbl15Subdivisions.Find(x => x.DivisionId == CurrentTbl09Division.DivisionId));
            if (Tbl15SubdivisionsList.Count != 0)
            {
                MessageBox.Show(CultRes.StringsRes.ConnectedTable, CultRes.StringsRes.Subdivision + " " + CultRes.StringsRes.ConnectedDataset,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                ret = true;
            }
            //    Tbl90ReferenceAuthorsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ReferenceListRefAuthorsByDivisionId(CurrentTbl09Division.DivisionId));
             Tbl90ReferenceAuthorsList = new ObservableCollection<Tbl90Reference>(_uow.Tbl90References
                .Find(x => x.DivisionId == CurrentTbl09Division.DivisionId && x.RefExpertId == null && x.RefSourceId == null));
            if (Tbl90ReferenceAuthorsList.Count != 0)
            {
                MessageBox.Show(CultRes.StringsRes.ConnectedTable, CultRes.StringsRes.ReferenceAuthor + " " + CultRes.StringsRes.ConnectedDataset,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                ret = true;
            }
         //   Tbl90ReferenceSourcesList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ReferenceListRefSourcesByDivisionId(CurrentTbl09Division.DivisionId));
            Tbl90ReferenceSourcesList = new ObservableCollection<Tbl90Reference>(_uow.Tbl90References
                .Find(x => x.DivisionId == CurrentTbl09Division.DivisionId && x.RefAuthorId == null && x.RefExpertId == null));
            if (Tbl90ReferenceSourcesList.Count != 0)
            {
                MessageBox.Show(CultRes.StringsRes.ConnectedTable, CultRes.StringsRes.ReferenceSource + " " + CultRes.StringsRes.ConnectedDataset,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                ret = true;
            }
       //     Tbl90ReferenceExpertsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ReferenceListRefExpertsByDivisionId(CurrentTbl09Division.DivisionId));
            Tbl90ReferenceExpertsList = new ObservableCollection<Tbl90Reference>(_uow.Tbl90References
                .Find(x => x.DivisionId == CurrentTbl09Division.DivisionId && x.RefAuthorId == null && x.RefSourceId == null));
            if (Tbl90ReferenceExpertsList.Count != 0)
            {
                MessageBox.Show(CultRes.StringsRes.ConnectedTable, CultRes.StringsRes.ReferenceExpert + " " + CultRes.StringsRes.ConnectedDataset,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                ret = true;
            }
         //   Tbl93CommentsList = new ObservableCollection<Tbl93Comment>(_businessLayer.ListTbl93CommentsByDivisionId(CurrentTbl09Division.DivisionId));
            Tbl93CommentsList = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments
                .Find(x => x.PhylumId == CurrentTbl09Division.DivisionId));
            if (Tbl93CommentsList.Count != 0)
            {
                MessageBox.Show(CultRes.StringsRes.ConnectedTable, CultRes.StringsRes.Comment + " " + CultRes.StringsRes.ConnectedDataset,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                ret = true;
            }
            if (ret) return;
            {
                try
                {
                  //  var division = _businessLayer.SingleListTbl09DivisionsByDivisionId(CurrentTbl09Division.DivisionId);
                    var division = _uow.Tbl09Divisions.GetById(CurrentTbl09Division.DivisionId);
                    if (division != null)
                    {
                        if (MessageBox.Show(CultRes.StringsRes.DeleteQuestion1, CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl09Division.DivisionName,
                                MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes)
                            return;
                    //    division.EntityState = EntityState.Deleted;
                     //   _businessLayer.RemoveDivision(division);

                        _uow.Tbl09Divisions.Remove(division);
                        _uow.Complete();

                        MessageBox.Show(CultRes.StringsRes.DeleteSuccess, CurrentTbl09Division.DivisionName,
                            MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show(CultRes.StringsRes.Information, CultRes.StringsRes.DeleteCan + " " + CurrentTbl09Division.DivisionName + " " + CultRes.StringsRes.DeleteCan1,
                            MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                    }
                }
                catch (Exception ex)
                {
               //     _entityException.EntityException(ex);
                    Log.Error(ex);
                }
            }
            if (SearchDivisionName != "")
            {
                if (SearchDivisionName == "*")  //show all datasets
                {
                    SearchDivisionName = "";
                    Tbl09DivisionsList.Clear();

                 //   Tbl09DivisionsList = new ObservableCollection<Tbl09Division>(_businessLayer.ListTbl09DivisionsByDivisionName(SearchDivisionName));
                    Tbl09DivisionsList = new ObservableCollection<Tbl09Division>(_uow.Tbl09Divisions.Find(x => x.DivisionName == SearchDivisionName));
                    SearchDivisionName = "*";
                }
                else
                {
         //           Tbl09DivisionsList = new ObservableCollection<Tbl09Division>(_businessLayer.ListTbl09DivisionsByDivisionName(SearchDivisionName));
                    Tbl09DivisionsList = new ObservableCollection<Tbl09Division>(_uow.Tbl09Divisions.Find(x => x.DivisionName == SearchDivisionName));

                }
                DivisionsView = CollectionViewSource.GetDefaultView(Tbl09DivisionsList);
                DivisionsView.Refresh();
            }
            else  //SearchName = empty
            {
          //      Tbl09DivisionsList = new ObservableCollection<Tbl09Division>(_businessLayer.ListTbl09DivisionsByDivisionName(SearchDivisionName));
                Tbl09DivisionsList = new ObservableCollection<Tbl09Division>(_uow.Tbl09Divisions.Find(x => x.DivisionName == SearchDivisionName));

                DivisionsView = CollectionViewSource.GetDefaultView(Tbl09DivisionsList);
                DivisionsView.MoveCurrentToFirst();
            }
        }
        //-------------------------------------------------------------------------------------------------                    

        private void SaveDivision(object o)
        {
            if (CurrentTbl09Division == null)
            {
                MessageBox.Show(CultRes.StringsRes.DatasetNew,
                    CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                return;
            }
        //    _businessLayer = new BusinessLayer.BusinessLayer();

            try
            {
             //   var division = _businessLayer.SingleListTbl09DivisionsByDivisionId(CurrentTbl09Division.DivisionId);
                var division = _uow.Tbl09Divisions.GetById(CurrentTbl09Division.DivisionId);
                if (CurrentTbl09Division.DivisionId != 0)
                {
                    if (division != null) //update
                    {
                        division.DivisionName = CurrentTbl09Division.DivisionName;
                        division.RegnumId = CurrentTbl09Division.RegnumId;
                        division.Valid = CurrentTbl09Division.Valid;
                        division.ValidYear = CurrentTbl09Division.ValidYear;
                        division.Synonym = CurrentTbl09Division.Synonym;
                        division.Author = CurrentTbl09Division.Author;
                        division.AuthorYear = CurrentTbl09Division.AuthorYear;
                        division.Info = CurrentTbl09Division.Info;
                        division.EngName = CurrentTbl09Division.EngName;
                        division.GerName = CurrentTbl09Division.GerName;
                        division.FraName = CurrentTbl09Division.FraName;
                        division.PorName = CurrentTbl09Division.PorName;
                        division.Updater = Environment.UserName;
                        division.UpdaterDate = DateTime.Now;
                        division.Memo = CurrentTbl09Division.Memo;
                     //   division.EntityState = EntityState.Modified;
                    }
                }
                else
                {
                    division = new Tbl09Division   //add new
                    {
                        DivisionName = CurrentTbl09Division.DivisionName,
                        RegnumId = CurrentTbl09Division.RegnumId,

                        CountId = RandomHelper.Randomnumber(),
                        Valid = CurrentTbl09Division.Valid,
                        ValidYear = CurrentTbl09Division.ValidYear,
                        Synonym = CurrentTbl09Division.Synonym,
                        Author = CurrentTbl09Division.Author,
                        AuthorYear = CurrentTbl09Division.AuthorYear,
                        Info = CurrentTbl09Division.Info,
                        EngName = CurrentTbl09Division.EngName,
                        GerName = CurrentTbl09Division.GerName,
                        FraName = CurrentTbl09Division.FraName,
                        PorName = CurrentTbl09Division.PorName,
                        Writer = Environment.UserName,
                        WriterDate = DateTime.Now,
                        Updater = Environment.UserName,
                        UpdaterDate = DateTime.Now,
                        Memo = CurrentTbl09Division.Memo,
                  //      EntityState = EntityState.Added
                    };
                }
                {
                    //RegnumID may be not 0
                    if (CurrentTbl09Division.RegnumId == 0)

                    {
                        MessageBox.Show(CultRes.StringsRes.RequiredGenealogyConnect, CultRes.StringsRes.RequiredInput,
                            MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        return;
                    }

                    //check if dataset with Name and RegnumId already exist       
               //     var dataset = _businessLayer.ListTbl09DivisionsByDivisionNameAndRegnumIdAndAuthor(CurrentTbl09Division.DivisionName, CurrentTbl09Division.RegnumId, CurrentTbl09Division.Author);
                   var dataset = new ObservableCollection<Tbl09Division>(_uow.Tbl09Divisions.Find(x => x.DivisionName == CurrentTbl09Division.DivisionName &&
                                                                                                       x.RegnumId == CurrentTbl09Division.RegnumId &&
                                                                                                       x.Author == CurrentTbl09Division.Author));

                    if (dataset.Count != 0 && CurrentTbl09Division.DivisionId == 0)  //dataset exist
                    {
                        MessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTbl09Division.DivisionName,
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        return;
                    }
                    if (dataset.Count == 0 && CurrentTbl09Division.DivisionId == 0 ||
                        dataset.Count != 0 && CurrentTbl09Division.DivisionId != 0 ||
                        dataset.Count == 0 && CurrentTbl09Division.DivisionId != 0) //new dataset and update
                    {
                        if (MessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTbl09Division.DivisionName,
                                MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes)
                            return;
                        {
                            try
                            {
                                //  _businessLayer.UpdateDivision(division);
                                if (CurrentTbl09Division.DivisionId != 0) //update
                                {
                                    _uow.Tbl09Divisions.Update(division);
                                }
                                else                                //add
                                    _uow.Tbl09Divisions.Add(division);
                                _uow.Complete();

                                _position = DivisionsView.CurrentPosition;
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
                            MessageBox.Show(CultRes.StringsRes.SaveSuccess,
                                CurrentTbl09Division.DivisionId == 0
                                    ? CultRes.StringsRes.DatasetNew
                                    : CurrentTbl09Division.DivisionName,
                                MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
        //        _entityException.EntityException(ex);
                Log.Error(ex);
                return;
            }

            if (SearchDivisionName != "")
            {
                if (SearchDivisionName == "*")  //show all datasets
                {
                    SearchDivisionName = "";
                    Tbl09DivisionsList.Clear();

                //    Tbl09DivisionsList = new ObservableCollection<Tbl09Division>(_businessLayer.ListTbl09DivisionsByDivisionName(SearchDivisionName));
                    Tbl09DivisionsList = new ObservableCollection<Tbl09Division>(_uow.Tbl09Divisions.Find(x => x.DivisionName.StartsWith(SearchDivisionName)));
                    SearchDivisionName = "*";
                }
                else
                {
                    //Tbl09DivisionsList = int.TryParse(SearchDivisionName, out var id)
                    //    ? new ObservableCollection<Tbl09Division>(_businessLayer.ListTbl09DivisionsByDivisionId(id))
                    //    : new ObservableCollection<Tbl09Division>(_businessLayer.ListTbl09DivisionsByDivisionName(SearchDivisionName));

                    Tbl09DivisionsList = int.TryParse(SearchDivisionName, out var id)
                        ? new ObservableCollection<Tbl09Division>(_uow.Tbl09Divisions
                            .Find(e => e.DivisionId == id))
                        : new ObservableCollection<Tbl09Division>(_uow.Tbl09Divisions.ListTbl09DivisionsOnlyPlantaeOrderBy(SearchDivisionName));

                }
                DivisionsView = CollectionViewSource.GetDefaultView(Tbl09DivisionsList);
                DivisionsView.MoveCurrentToPosition(_position);
            }
            else
            {
              //  Tbl09DivisionsList = new ObservableCollection<Tbl09Division>(_businessLayer.ListTbl09DivisionsByDivisionName(CurrentTbl09Division.DivisionName));
                Tbl09DivisionsList = new ObservableCollection<Tbl09Division>(_uow.Tbl09Divisions.Find(x => x.DivisionName == CurrentTbl09Division.DivisionName));

                DivisionsView = CollectionViewSource.GetDefaultView(Tbl09DivisionsList);
                DivisionsView.Refresh();
            }
        }
        #endregion "Public Commands"                  



        //    Part 2    


        //#region "Public Commands Connect <== Tbl03Regnum"                 
        ////-------------------------------------------------------------------------

        //private RelayCommand _saveRegnumCommand;

        //public ICommand SaveRegnumCommand => _saveRegnumCommand ??
        //                                         (_saveRegnumCommand = new RelayCommand(delegate { SaveRegnum(null); }));

        ////-------------------------------------------------------------------------          

        //private void SaveRegnum(object o)
        //{
        //    if (CurrentTbl03Regnum == null)
        //    {
        //        MessageBox.Show(CultRes.StringsRes.DatasetNew,
        //            CultRes.StringsRes.RequiredInput,
        //            MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
        //        return;
        //    }

        //    try
        //    {
        //        var regnum = _businessLayer.SingleListTbl03RegnumsByRegnumId(CurrentTbl03Regnum.RegnumId);
        //        if (CurrentTbl03Regnum.RegnumId != 0)
        //        {
        //            if (regnum != null) //update
        //            {
        //                regnum.RegnumName = CurrentTbl03Regnum.RegnumName;
        //                regnum.Subregnum = CurrentTbl03Regnum.Subregnum;
        //                regnum.Valid = CurrentTbl03Regnum.Valid;
        //                regnum.ValidYear = CurrentTbl03Regnum.ValidYear;
        //                regnum.Synonym = CurrentTbl03Regnum.Synonym;
        //                regnum.Author = CurrentTbl03Regnum.Author;
        //                regnum.AuthorYear = CurrentTbl03Regnum.AuthorYear;
        //                regnum.Info = CurrentTbl03Regnum.Info;
        //                regnum.EngName = CurrentTbl03Regnum.EngName;
        //                regnum.GerName = CurrentTbl03Regnum.GerName;
        //                regnum.FraName = CurrentTbl03Regnum.FraName;
        //                regnum.PorName = CurrentTbl03Regnum.PorName;
        //                regnum.Updater = Environment.UserName;
        //                regnum.UpdaterDate = DateTime.Now;
        //                regnum.Memo = CurrentTbl03Regnum.Memo;
        //          //      regnum.EntityState = EntityState.Modified;
        //            }
        //        }
        //        else
        //        {
        //            regnum = new Tbl03Regnum   //add new
        //            {
        //                RegnumName = CurrentTbl03Regnum.RegnumName,
        //                Subregnum = CurrentTbl03Regnum.Subregnum,
        //                CountId = RandomHelper.Randomnumber(),
        //                Valid = CurrentTbl03Regnum.Valid,
        //                ValidYear = CurrentTbl03Regnum.ValidYear,
        //                Synonym = CurrentTbl03Regnum.Synonym,
        //                Author = CurrentTbl03Regnum.Author,
        //                AuthorYear = CurrentTbl03Regnum.AuthorYear,
        //                Info = CurrentTbl03Regnum.Info,
        //                EngName = CurrentTbl03Regnum.EngName,
        //                GerName = CurrentTbl03Regnum.GerName,
        //                FraName = CurrentTbl03Regnum.FraName,
        //                PorName = CurrentTbl03Regnum.PorName,
        //                Writer = Environment.UserName,
        //                WriterDate = DateTime.Now,
        //                Updater = Environment.UserName,
        //                UpdaterDate = DateTime.Now,
        //                Memo = CurrentTbl03Regnum.Memo,
        //        //        EntityState = EntityState.Added
        //            };
        //        }
        //        {
        //            //check if dataset with Name already exist       
        //            var dataset = _businessLayer.ListTbl03RegnumsByRegnumNameAndSubregnum(CurrentTbl03Regnum.RegnumName, CurrentTbl03Regnum.Subregnum);

        //            if (dataset.Count != 0 && CurrentTbl03Regnum.RegnumId == 0)  //dataset exist
        //            {
        //                MessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTbl03Regnum.RegnumName + " " + CurrentTbl03Regnum.Subregnum,
        //                MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
        //                return;
        //            }
        //            if (dataset.Count == 0 && CurrentTbl03Regnum.RegnumId == 0 ||
        //                dataset.Count != 0 && CurrentTbl03Regnum.RegnumId != 0 ||
        //                dataset.Count == 0 && CurrentTbl03Regnum.RegnumId != 0) //new dataset and update
        //            {
        //                if (MessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTbl03Regnum.RegnumName + " " + CurrentTbl03Regnum.Subregnum,
        //                        MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes)
        //                    return;
        //                {
        //                    try
        //                    {
        //                        _businessLayer.UpdateRegnum(regnum);
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
        //                    MessageBox.Show(CultRes.StringsRes.SaveSuccess,
        //                        CurrentTbl03Regnum.RegnumId == 0
        //                            ? CultRes.StringsRes.DatasetNew
        //                            : CurrentTbl03Regnum.RegnumName + " " + CurrentTbl03Regnum.Subregnum,
        //                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        // //       _entityException.EntityException(ex);
        //        Log.Error(ex);
        //        return;
        //    }

        //    Tbl03RegnumsList = new ObservableCollection<Tbl03Regnum>(_businessLayer.ListTbl03RegnumsByRegnumId(CurrentTbl09Division.RegnumId));

        //    SelectedMainTabIndex = 0;
        //    RegnumsView = CollectionViewSource.GetDefaultView(Tbl03RegnumsList);
        //    RegnumsView.Refresh();
        //}
        //#endregion "Public Commands"                  


        //    Part 3    





        //    Part 4    


      //  #region "Public Commands Connect ==> Tbl15Subdivision"                 
      //  //-------------------------------------------------------------------------
      //  private RelayCommand _addSubdivisionCommand;

      //  public ICommand AddSubdivisionCommand => _addSubdivisionCommand ??
      //                                          (_addSubdivisionCommand = new RelayCommand(delegate { AddSubdivision(null); }));

      //  private RelayCommand _copySubdivisionCommand;

      //  public ICommand CopySubdivisionCommand => _copySubdivisionCommand ??
      //                                           (_copySubdivisionCommand = new RelayCommand(delegate { CopySubdivision(null); }));

      //  private RelayCommand _deleteSubdivisionCommand;

      //  public ICommand DeleteSubdivisionCommand => _deleteSubdivisionCommand ??
      //                                           (_deleteSubdivisionCommand = new RelayCommand(delegate { DeleteSubdivision(null); }));

      //  private RelayCommand _saveSubdivisionCommand;

      //  public ICommand SaveSubdivisionCommand => _saveSubdivisionCommand ??
      //                                           (_saveSubdivisionCommand = new RelayCommand(delegate { SaveSubdivision(null); }));

      //  //-------------------------------------------------------------------------          

      //  private void AddSubdivision(object o)
      //  {
      //      if (Tbl15SubdivisionsList == null)
      //          Tbl15SubdivisionsList = new ObservableCollection<Tbl15Subdivision>();

      //      Tbl15SubdivisionsList.Insert(0, new Tbl15Subdivision { SubdivisionName = CultRes.StringsRes.DatasetNew });

      ////      _businessLayer = new BusinessLayer.BusinessLayer();
      //      Tbl09DivisionsAllList = new ObservableCollection<Tbl09Division>(_businessLayer.ListTbl09Divisions());

      //      SubdivisionsView = CollectionViewSource.GetDefaultView(Tbl15SubdivisionsList);
      //      SubdivisionsView.MoveCurrentToFirst();
      //  }
      //  //----------------------------------------------------------------------            

      //  private void CopySubdivision(object o)
      //  {
      //      if (CurrentTbl15Subdivision == null)
      //      {
      //          MessageBox.Show(CultRes.StringsRes.DatasetNew,
      //              CultRes.StringsRes.RequiredInput,
      //              MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
      //          return;
      //      }

      //      var subdivision = _businessLayer.SingleListTbl15SubdivisionsBySubdivisionId(CurrentTbl15Subdivision.SubdivisionId);

      //      Tbl15SubdivisionsList.Insert(0, new Tbl15Subdivision
      //      {
      //          SubdivisionName = CultRes.StringsRes.DatasetNew,
      //          Valid = subdivision.Valid,
      //          ValidYear = subdivision.ValidYear,
      //          Synonym = subdivision.Synonym,
      //          Author = subdivision.Author,
      //          AuthorYear = subdivision.AuthorYear,
      //          Info = subdivision.Info,
      //          EngName = subdivision.EngName,
      //          GerName = subdivision.GerName,
      //          FraName = subdivision.FraName,
      //          PorName = subdivision.PorName,
      //          Memo = subdivision.Memo
      //      });

      //      SubdivisionsView = CollectionViewSource.GetDefaultView(Tbl15SubdivisionsList);
      //      SubdivisionsView.MoveCurrentToFirst();
      //  }
      //  //----------------------------------------------------------------------            

      //  private void DeleteSubdivision(object o)
      //  {
      //      if (CurrentTbl15Subdivision == null)
      //      {
      //          MessageBox.Show(CultRes.StringsRes.DatasetNew,
      //              CultRes.StringsRes.RequiredInput,
      //              MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
      //          return;
      //      }

      //      var ret = false;
      //      //check if in Tbl18Superclasses connected datasets, than return
      //      Tbl18SuperclassesList = new ObservableCollection<Tbl18Superclass>(_businessLayer.ListTbl18SuperclassesBySubdivisionId(CurrentTbl15Subdivision.SubdivisionId));
      //      if (Tbl18SuperclassesList.Count != 0)
      //      {
      //          MessageBox.Show(CultRes.StringsRes.ConnectedTable, CultRes.StringsRes.Superclass + " " + CultRes.StringsRes.ConnectedDataset,
      //              MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
      //          ret = true;
      //      }
      //      Tbl90ReferenceAuthorsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ReferenceListRefAuthorsBySubdivisionId(CurrentTbl15Subdivision.SubdivisionId));
      //      if (Tbl90ReferenceAuthorsList.Count != 0)
      //      {
      //          MessageBox.Show(CultRes.StringsRes.ConnectedTable, CultRes.StringsRes.ReferenceAuthor + " " + CultRes.StringsRes.ConnectedDataset,
      //              MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
      //          ret = true;
      //      }
      //      Tbl90ReferenceSourcesList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ReferenceListRefSourcesBySubdivisionId(CurrentTbl15Subdivision.SubdivisionId));
      //      if (Tbl90ReferenceSourcesList.Count != 0)
      //      {
      //          MessageBox.Show(CultRes.StringsRes.ConnectedTable, CultRes.StringsRes.ReferenceSource + " " + CultRes.StringsRes.ConnectedDataset,
      //              MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
      //          ret = true;
      //      }
      //      Tbl90ReferenceExpertsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ReferenceListRefExpertsBySubdivisionId(CurrentTbl15Subdivision.SubdivisionId));
      //      if (Tbl90ReferenceExpertsList.Count != 0)
      //      {
      //          MessageBox.Show(CultRes.StringsRes.ConnectedTable, CultRes.StringsRes.ReferenceExpert + " " + CultRes.StringsRes.ConnectedDataset,
      //              MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
      //          ret = true;
      //      }
      //      Tbl93CommentsList = new ObservableCollection<Tbl93Comment>(_businessLayer.ListTbl93CommentsBySubdivisionId(CurrentTbl15Subdivision.SubdivisionId));
      //      if (Tbl93CommentsList.Count != 0)
      //      {
      //          MessageBox.Show(CultRes.StringsRes.ConnectedTable, CultRes.StringsRes.Comment + " " + CultRes.StringsRes.ConnectedDataset,
      //              MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
      //          ret = true;
      //      }
      //      if (ret) return;
      //      {
      //          try
      //          {
      //              var subdivision = _businessLayer.SingleListTbl15SubdivisionsBySubdivisionId(CurrentTbl15Subdivision.SubdivisionId);
      //              if (subdivision != null)
      //              {
      //                  if (MessageBox.Show(CultRes.StringsRes.DeleteQuestion1, CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl15Subdivision.SubdivisionName,
      //                       MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes)
      //                      return;
      //                  subdivision.EntityState = EntityState.Deleted;
      //                  _businessLayer.RemoveSubdivision(subdivision);

      //                  MessageBox.Show(CultRes.StringsRes.DeleteSuccess, CurrentTbl15Subdivision.SubdivisionName,
      //                     MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
      //              }
      //              else
      //              {
      //                  MessageBox.Show(CultRes.StringsRes.Information, CultRes.StringsRes.DeleteCan + " " + CurrentTbl15Subdivision.SubdivisionName + " " + CultRes.StringsRes.DeleteCan1,
      //                      MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
      //              }
      //          }
      //          catch (Exception ex)
      //          {
      //    //          _entityException.EntityException(ex);
      //              Log.Error(ex);
      //          }
      //      }
      //      Tbl15SubdivisionsList = new ObservableCollection<Tbl15Subdivision>(_businessLayer.ListTbl15SubdivisionsByDivisionId(CurrentTbl09Division.DivisionId));

      //      SubdivisionsView = CollectionViewSource.GetDefaultView(Tbl15SubdivisionsList);
      //      SubdivisionsView.Refresh();
      //  }
      //  //-------------------------------------------------------------------------------------------------                    

      //  private void SaveSubdivision(object o)
      //  {
      //      if (CurrentTbl15Subdivision == null)
      //      {
      //          MessageBox.Show(CultRes.StringsRes.DatasetNew,
      //              CultRes.StringsRes.RequiredInput,
      //              MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
      //          return;
      //      }

      //      CurrentTbl15Subdivision.DivisionId = CurrentTbl09Division.DivisionId;

      //      try
      //      {
      //          var subdivision = _businessLayer.SingleListTbl15SubdivisionsBySubdivisionId(CurrentTbl15Subdivision.SubdivisionId);
      //          if (CurrentTbl15Subdivision.SubdivisionId != 0)
      //          {
      //              if (subdivision != null) //update
      //              {
      //                  subdivision.SubdivisionName = CurrentTbl15Subdivision.SubdivisionName;
      //                  subdivision.DivisionId = CurrentTbl15Subdivision.DivisionId;
      //                  subdivision.Valid = CurrentTbl15Subdivision.Valid;
      //                  subdivision.ValidYear = CurrentTbl15Subdivision.ValidYear;
      //                  subdivision.Synonym = CurrentTbl15Subdivision.Synonym;
      //                  subdivision.Author = CurrentTbl15Subdivision.Author;
      //                  subdivision.AuthorYear = CurrentTbl15Subdivision.AuthorYear;
      //                  subdivision.Info = CurrentTbl15Subdivision.Info;
      //                  subdivision.EngName = CurrentTbl15Subdivision.EngName;
      //                  subdivision.GerName = CurrentTbl15Subdivision.GerName;
      //                  subdivision.FraName = CurrentTbl15Subdivision.FraName;
      //                  subdivision.PorName = CurrentTbl15Subdivision.PorName;
      //                  subdivision.Updater = Environment.UserName;
      //                  subdivision.UpdaterDate = DateTime.Now;
      //                  subdivision.Memo = CurrentTbl15Subdivision.Memo;
      //                //  subdivision.EntityState = EntityState.Modified;
      //              }
      //          }
      //          else
      //          {
      //              subdivision = new Tbl15Subdivision   //add new
      //              {
      //                  SubdivisionName = CurrentTbl15Subdivision.SubdivisionName,
      //                  DivisionId = CurrentTbl15Subdivision.DivisionId,
      //                  CountId = RandomHelper.Randomnumber(),
      //                  Valid = CurrentTbl15Subdivision.Valid,
      //                  ValidYear = CurrentTbl15Subdivision.ValidYear,
      //                  Synonym = CurrentTbl15Subdivision.Synonym,
      //                  Author = CurrentTbl15Subdivision.Author,
      //                  AuthorYear = CurrentTbl15Subdivision.AuthorYear,
      //                  Info = CurrentTbl15Subdivision.Info,
      //                  EngName = CurrentTbl15Subdivision.EngName,
      //                  GerName = CurrentTbl15Subdivision.GerName,
      //                  FraName = CurrentTbl15Subdivision.FraName,
      //                  PorName = CurrentTbl15Subdivision.PorName,
      //                  Writer = Environment.UserName,
      //                  WriterDate = DateTime.Now,
      //                  Updater = Environment.UserName,
      //                  UpdaterDate = DateTime.Now,
      //                  Memo = CurrentTbl15Subdivision.Memo,
      //            //      EntityState = EntityState.Added
      //              };
      //          }
      //          {
      //              //DivisionID may be not 0
      //              if (CurrentTbl15Subdivision.DivisionId == 0)

      //              {
      //                  MessageBox.Show(CultRes.StringsRes.RequiredGenealogyConnect, CultRes.StringsRes.RequiredInput,
      //                      MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
      //                  return;
      //              }

      //              //check if dataset with Name and DivisionId already exist       
      //              var dataset = _businessLayer.ListTbl15SubdivisionsBySubdivisionNameAndDivisionIdAndAuthor(CurrentTbl15Subdivision.SubdivisionName, CurrentTbl15Subdivision.DivisionId, CurrentTbl15Subdivision.Author);

      //              if (dataset.Count != 0 && CurrentTbl15Subdivision.SubdivisionId == 0)  //dataset exist
      //              {
      //                  MessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTbl15Subdivision.SubdivisionName,
      //                  MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
      //                  return;
      //              }
      //              if (dataset.Count == 0 && CurrentTbl15Subdivision.SubdivisionId == 0 ||
      //                  dataset.Count != 0 && CurrentTbl15Subdivision.SubdivisionId != 0 ||
      //                  dataset.Count == 0 && CurrentTbl15Subdivision.SubdivisionId != 0) //new dataset and update
      //              {
      //                  if (MessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTbl15Subdivision.SubdivisionName,
      //                          MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes)
      //                      return;
      //                  {
      //                      try
      //                      {
      //                          _businessLayer.UpdateSubdivision(subdivision);
      //                      }
      //                      catch (DbUpdateException e)
      //                      {
      //                          if (e.InnerException != null)
      //                              System.Windows.MessageBox.Show(e.InnerException.ToString(), CultRes.StringsRes.FailedToSave,
      //                                  MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);

      //                          Log.Error(e);
      //                          return;
      //                      }
      //                      catch (Exception e)
      //                      {
      //                          System.Windows.MessageBox.Show(e.Message, CultRes.StringsRes.Error,
      //                              MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);
      //                          Log.Error(e);
      //                          return;
      //                      }
      //                      MessageBox.Show(CultRes.StringsRes.SaveSuccess,
      //                          CurrentTbl15Subdivision.SubdivisionId == 0
      //                              ? CultRes.StringsRes.DatasetNew
      //                              : CurrentTbl15Subdivision.SubdivisionName,
      //                          MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
      //                  }
      //              }
      //          }
      //      }
      //      catch (DbEntityValidationException ex)
      //      {
      //     //     _entityException.EntityException(ex);
      //          Log.Error(ex);
      //          return;
      //      }

      //      Tbl15SubdivisionsList = new ObservableCollection<Tbl15Subdivision>(_businessLayer.ListTbl15SubdivisionsByDivisionId(CurrentTbl09Division.DivisionId));

      //      SelectedMainTabIndex = 1;
      //      SubdivisionsView = CollectionViewSource.GetDefaultView(Tbl15SubdivisionsList);
      //      SubdivisionsView.Refresh();
      //  }
      //  #endregion "Public Commands"                  



        //    Part 5    



        //    Part 6    




        //    Part 7    



        //    Part 8    


      //  #region "Public Commands Connect ==> Tbl90ReferenceAuthor"
      //  //-------------------------------------------------------------------------
      //  private RelayCommand _addReferenceAuthorCommand;

      //  public ICommand AddReferenceAuthorCommand => _addReferenceAuthorCommand ??
      //                                              (_addReferenceAuthorCommand = new RelayCommand(delegate { AddReferenceAuthor(null); }));

      //  private RelayCommand _copyReferenceAuthorCommand;

      //  public ICommand CopyReferenceAuthorCommand => _copyReferenceAuthorCommand ??
      //                  (_copyReferenceAuthorCommand = new RelayCommand(delegate { CopyReferenceAuthor(null); }));

      //  private RelayCommand _deleteReferenceAuthorCommand;

      //  public ICommand DeleteReferenceAuthorCommand => _deleteReferenceAuthorCommand ??
      //                                         (_deleteReferenceAuthorCommand = new RelayCommand(delegate { DeleteReferenceAuthor(null); }));

      //  private RelayCommand _saveReferenceAuthorCommand;

      //  public ICommand SaveReferenceAuthorCommand => _saveReferenceAuthorCommand ??
      //               (_saveReferenceAuthorCommand = new RelayCommand(delegate { SaveReferenceAuthor(null); }));
      //  //-------------------------------------------------------------------------                    

      //  public void AddReferenceAuthor(object o)
      //  {
      //      if (Tbl90ReferenceAuthorsList == null)
      //          Tbl90ReferenceAuthorsList = new ObservableCollection<Tbl90Reference>();

      //      Tbl90ReferenceAuthorsList.Insert(0, new Tbl90Reference { Info = CultRes.StringsRes.DatasetNew });

      //      ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
      //      ReferenceAuthorsView.MoveCurrentToFirst();
      //  }
      //  //----------------------------------------------------------------------            

      //  public void CopyReferenceAuthor(object o)
      //  {
      //      if (CurrentTbl90ReferenceAuthor == null)
      //      {
      //          MessageBox.Show(CultRes.StringsRes.DatasetNew,
      //              CultRes.StringsRes.RequiredInput,
      //              MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
      //          return;
      //      }

      //      var reference = _businessLayer.SingleListTbl90ReferencesByReferenceId(CurrentTbl90ReferenceAuthor.ReferenceId);

      //      Tbl90ReferenceAuthorsList.Insert(0, new Tbl90Reference
      //      {
      //          RefAuthorId = reference.RefAuthorId,
      //          Valid = reference.Valid,
      //          ValidYear = reference.ValidYear,
      //          Info = CultRes.StringsRes.DatasetNew,
      //          Memo = reference.Memo
      //      });

      //      ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
      //      ReferenceAuthorsView.MoveCurrentToFirst();
      //  }
      //  //----------------------------------------------------------------------            

      //  private void DeleteReferenceAuthor(object o)
      //  {
      //      if (CurrentTbl90ReferenceAuthor == null)
      //      {
      //          MessageBox.Show(CultRes.StringsRes.DatasetNew,
      //              CultRes.StringsRes.RequiredInput,
      //              MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
      //          return;
      //      }

      //      try
      //      {
      //          var reference = _businessLayer.SingleListTbl90ReferencesByReferenceId(CurrentTbl90ReferenceAuthor.ReferenceId);
      //          if (reference != null)
      //          {
      //              if (MessageBox.Show(CultRes.StringsRes.DeleteQuestion1, CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl90ReferenceAuthor.Info,
      //                      MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes)
      //                  return;
      //              reference.EntityState = EntityState.Deleted;
      //              _businessLayer.RemoveReference(reference);

      //              MessageBox.Show(CultRes.StringsRes.DeleteSuccess, CurrentTbl90ReferenceAuthor.Info,
      //                  MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
      //          }
      //          else
      //          {
      //              MessageBox.Show(CultRes.StringsRes.Information, CultRes.StringsRes.DeleteCan + " " + CurrentTbl90ReferenceAuthor.Info + " " + CultRes.StringsRes.DeleteCan1,
      //                  MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
      //          }
      //      }
      //      catch (Exception ex)
      //      {
      ////          _entityException.EntityException(ex);
      //     //     Log.Error(ex);
      //      }

      //      Tbl90ReferenceAuthorsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ReferenceListRefAuthorsByDivisionId(CurrentTbl09Division.DivisionId));

      //      ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
      //      ReferenceAuthorsView.Refresh();
      //  }

      //  //----------------------------------------------------------------------            

      //  public void SaveReferenceAuthor(object o)
      //  {
      //      if (CurrentTbl90ReferenceAuthor == null)
      //      {
      //          MessageBox.Show(CultRes.StringsRes.DatasetNew,
      //              CultRes.StringsRes.RequiredInput,
      //              MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
      //          return;
      //      }

      //      CurrentTbl90ReferenceAuthor.DivisionId = CurrentTbl09Division.DivisionId;

      //      try
      //      {
      //          var reference = _businessLayer.SingleListTbl90ReferencesByReferenceId(CurrentTbl90ReferenceAuthor.ReferenceId);
      //          if (CurrentTbl90ReferenceAuthor.ReferenceId != 0)
      //          {
      //              if (reference != null) //update
      //              {
      //                  reference.RefExpertId = CurrentTbl90ReferenceAuthor.RefExpertId;
      //                  reference.RefAuthorId = CurrentTbl90ReferenceAuthor.RefAuthorId;
      //                  reference.RefSourceId = CurrentTbl90ReferenceAuthor.RefSourceId;
      //                  reference.RegnumID = CurrentTbl90ReferenceAuthor.RegnumId;
      //                  reference.PhylumID = CurrentTbl90ReferenceAuthor.PhylumId;
      //                  reference.DivisionID = CurrentTbl90ReferenceAuthor.DivisionId;
      //                  reference.SubphylumID = CurrentTbl90ReferenceAuthor.SubphylumID;
      //                  reference.SubdivisionID = CurrentTbl90ReferenceAuthor.SubdivisionId;
      //                  reference.SuperclassID = CurrentTbl90ReferenceAuthor.SuperclassId;
      //                  reference.ClassID = CurrentTbl90ReferenceAuthor.ClassID;
      //                  reference.SubclassID = CurrentTbl90ReferenceAuthor.SubclassID;
      //                  reference.InfraclassID = CurrentTbl90ReferenceAuthor.InfraclassID;
      //                  reference.LegioID = CurrentTbl90ReferenceAuthor.LegioID;
      //                  reference.OrdoID = CurrentTbl90ReferenceAuthor.OrdoID;
      //                  reference.SubordoID = CurrentTbl90ReferenceAuthor.SubordoID;
      //                  reference.InfraordoID = CurrentTbl90ReferenceAuthor.InfraordoID;
      //                  reference.SuperfamilyID = CurrentTbl90ReferenceAuthor.SuperfamilyID;
      //                  reference.FamilyID = CurrentTbl90ReferenceAuthor.FamilyID;
      //                  reference.SubfamilyID = CurrentTbl90ReferenceAuthor.SubfamilyID;
      //                  reference.InfrafamilyID = CurrentTbl90ReferenceAuthor.InfrafamilyID;
      //                  reference.SupertribusID = CurrentTbl90ReferenceAuthor.SupertribusID;
      //                  reference.TribusID = CurrentTbl90ReferenceAuthor.TribusID;
      //                  reference.SubtribusID = CurrentTbl90ReferenceAuthor.SubtribusID;
      //                  reference.InfratribusID = CurrentTbl90ReferenceAuthor.InfratribusID;
      //                  reference.GenusID = CurrentTbl90ReferenceAuthor.GenusID;
      //                  reference.PlSpeciesID = CurrentTbl90ReferenceAuthor.PlSpeciesID;
      //                  reference.FiSpeciesID = CurrentTbl90ReferenceAuthor.FiSpeciesID;
      //                  reference.Valid = CurrentTbl90ReferenceAuthor.Valid;
      //                  reference.ValidYear = CurrentTbl90ReferenceAuthor.ValidYear;
      //                  reference.Info = CurrentTbl90ReferenceAuthor.Info;
      //                  reference.Updater = Environment.UserName;
      //                  reference.UpdaterDate = DateTime.Now;
      //                  reference.Memo = CurrentTbl90ReferenceAuthor.Memo;

      //            //      reference.EntityState = EntityState.Modified;
      //              }
      //          }
      //          else
      //          {
      //              reference = new Tbl90Reference     //add new
      //              {
      //                  RefAuthorID = CurrentTbl90ReferenceAuthor.RefAuthorID,
      //                  RefSourceID = CurrentTbl90ReferenceAuthor.RefSourceID,
      //                  RefExpertID = CurrentTbl90ReferenceAuthor.RefExpertID,
      //                  RegnumID = CurrentTbl90ReferenceAuthor.RegnumID,
      //                  PhylumID = CurrentTbl90ReferenceAuthor.PhylumID,
      //                  DivisionID = CurrentTbl90ReferenceAuthor.DivisionID,
      //                  SubphylumID = CurrentTbl90ReferenceAuthor.SubphylumID,
      //                  SubdivisionID = CurrentTbl90ReferenceAuthor.SubdivisionID,
      //                  SuperclassID = CurrentTbl90ReferenceAuthor.SuperclassID,
      //                  ClassID = CurrentTbl90ReferenceAuthor.ClassID,
      //                  SubclassID = CurrentTbl90ReferenceAuthor.SubclassID,
      //                  InfraclassID = CurrentTbl90ReferenceAuthor.InfraclassID,
      //                  LegioID = CurrentTbl90ReferenceAuthor.LegioID,
      //                  OrdoID = CurrentTbl90ReferenceAuthor.OrdoID,
      //                  SubordoID = CurrentTbl90ReferenceAuthor.SubordoID,
      //                  InfraordoID = CurrentTbl90ReferenceAuthor.InfraordoID,
      //                  SuperfamilyID = CurrentTbl90ReferenceAuthor.SuperfamilyID,
      //                  FamilyID = CurrentTbl90ReferenceAuthor.FamilyID,
      //                  SubfamilyID = CurrentTbl90ReferenceAuthor.SubfamilyID,
      //                  InfrafamilyID = CurrentTbl90ReferenceAuthor.InfrafamilyID,
      //                  SupertribusID = CurrentTbl90ReferenceAuthor.SupertribusID,
      //                  TribusID = CurrentTbl90ReferenceAuthor.TribusID,
      //                  SubtribusID = CurrentTbl90ReferenceAuthor.SubtribusID,
      //                  InfratribusID = CurrentTbl90ReferenceAuthor.InfratribusID,
      //                  GenusID = CurrentTbl90ReferenceAuthor.GenusID,
      //                  PlSpeciesID = CurrentTbl90ReferenceAuthor.PlSpeciesID,
      //                  FiSpeciesID = CurrentTbl90ReferenceAuthor.FiSpeciesID,
      //                  CountID = RandomHelper.Randomnumber(),
      //                  Valid = CurrentTbl90ReferenceAuthor.Valid,
      //                  ValidYear = CurrentTbl90ReferenceAuthor.ValidYear,
      //                  Info = CurrentTbl90ReferenceAuthor.Info,
      //                  Memo = CurrentTbl90ReferenceAuthor.Memo,
      //                  Writer = Environment.UserName,
      //                  WriterDate = DateTime.Now,
      //                  Updater = Environment.UserName,
      //                  UpdaterDate = DateTime.Now,
      //           //       EntityState = EntityState.Added
      //              };
      //          }
      //          {
      //              //RefExpertID or RefSourceID or RefAuthorID may be not 0
      //              if (CurrentTbl90ReferenceAuthor.RefExpertId == null &&
      //                  CurrentTbl90ReferenceAuthor.RefSourceId == null &&
      //                  CurrentTbl90ReferenceAuthor.RefAuthorId == null)
      //              {
      //                  MessageBox.Show(CultRes.StringsRes.RequiredGenealogyConnect, CultRes.StringsRes.RequiredInput,
      //                      MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
      //                  return;
      //              }

      //              //check if dataset with vb-name already exist   
      //              var dataset = _businessLayer.ListTbl90ReferencesByRefExpertIdAndRefSourceIdAndRefAuthorIdAndInfo(CurrentTbl90ReferenceAuthor);

      //              if (dataset.Count != 0 && CurrentTbl90ReferenceAuthor.ReferenceId == 0)  //dataset exist
      //              {
      //                  MessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTbl90ReferenceAuthor.ReferenceId.ToString(),
      //                  MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
      //                  return;
      //              }
      //              if (dataset.Count == 0 && CurrentTbl90ReferenceAuthor.ReferenceId == 0 ||
      //                  dataset.Count != 0 && CurrentTbl90ReferenceAuthor.ReferenceId != 0 ||
      //                  dataset.Count == 0 && CurrentTbl90ReferenceAuthor.ReferenceId != 0) //new dataset and update
      //              {
      //                  if (MessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTbl90ReferenceAuthor.Info,
      //                          MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes)
      //                      return;
      //                  {
      //                      try
      //                      {
      //                          _businessLayer.UpdateReference(reference);
      //                      }
      //                      catch (DbUpdateException e)
      //                      {
      //                          if (e.InnerException != null)
      //                              System.Windows.MessageBox.Show(e.InnerException.ToString(), CultRes.StringsRes.FailedToSave,
      //                                  MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);

      //                          Log.Error(e);
      //                          return;
      //                      }
      //                      catch (Exception e)
      //                      {
      //                          System.Windows.MessageBox.Show(e.Message, CultRes.StringsRes.Error,
      //                              MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);
      //                          Log.Error(e);
      //                          return;
      //                      }
      //                      MessageBox.Show(CultRes.StringsRes.SaveSuccess,
      //                          CurrentTbl90ReferenceAuthor.ReferenceId == 0
      //                              ? CultRes.StringsRes.DatasetNew
      //                              : CurrentTbl90ReferenceAuthor.ReferenceId.ToString(),
      //                          MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
      //                  }
      //              }
      //          }
      //      }
      //      catch (Exception ex)
      //      {
      //        //  _entityException.EntityException(ex);
      //       //   Log.Error(ex);
      //          return;
      //      }

      //      Tbl90ReferenceAuthorsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ReferenceListRefAuthorsByDivisionId(CurrentTbl09Division.DivisionID));


      //      SelectedMainSubRefTabIndex = 2;

      //      ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
      //      ReferenceAuthorsView.Refresh();
      //  }
      //  #endregion "Public Commands"                  

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
        //        MessageBox.Show(CultRes.StringsRes.DatasetNew,
        //            CultRes.StringsRes.RequiredInput,
        //            MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
        //        return;
        //    }

        //    var reference = _businessLayer.SingleListTbl90ReferencesByReferenceId(CurrentTbl90ReferenceSource.ReferenceId);

        //    Tbl90ReferenceSourcesList.Insert(0, new Tbl90Reference
        //    {
        //        RefSourceId = reference.RefSourceId,
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
        //        MessageBox.Show(CultRes.StringsRes.DatasetNew,
        //            CultRes.StringsRes.RequiredInput,
        //            MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
        //        return;
        //    }

        //    try
        //    {
        //        var reference = _businessLayer.SingleListTbl90ReferencesByReferenceId(CurrentTbl90ReferenceSource.ReferenceId);
        //        if (reference != null)
        //        {
        //            if (MessageBox.Show(CultRes.StringsRes.DeleteQuestion1, CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl90ReferenceSource.Info,
        //                    MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes)
        //                return;
        //            reference.EntityState = EntityState.Deleted;
        //            _businessLayer.RemoveReference(reference);

        //            MessageBox.Show(CultRes.StringsRes.DeleteSuccess, CurrentTbl90ReferenceSource.Info,
        //                MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
        //        }
        //        else
        //        {
        //            MessageBox.Show(CultRes.StringsRes.Information, CultRes.StringsRes.DeleteCan + " " + CurrentTbl90ReferenceSource.Info + " " + CultRes.StringsRes.DeleteCan1,
        //                MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //  //      _entityException.EntityException(ex);
        // //       Log.Error(ex);
        //    }

        //    Tbl90ReferenceSourcesList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ReferenceListRefSourcesByDivisionId(CurrentTbl09Division.DivisionID));

        //    ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
        //    ReferenceSourcesView.Refresh();
        //}
        ////----------------------------------------------------------------------            

        //public void SaveReferenceSource(object o)
        //{
        //    if (CurrentTbl90ReferenceSource == null)
        //    {
        //        MessageBox.Show(CultRes.StringsRes.DatasetNew,
        //            CultRes.StringsRes.RequiredInput,
        //            MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
        //        return;
        //    }

        //    CurrentTbl90ReferenceSource.DivisionId = CurrentTbl09Division.DivisionId;

        //    try
        //    {
        //        var reference = _businessLayer.SingleListTbl90ReferencesByReferenceId(CurrentTbl90ReferenceSource.ReferenceId);
        //        if (CurrentTbl90ReferenceSource.ReferenceId != 0)
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
        //         //       EntityState = EntityState.Added
        //            };
        //        }
        //        {
        //            //RefExpertID or RefSourceID or RefAuthorID may be not 0
        //            if (CurrentTbl90ReferenceSource.RefExpertId == null && CurrentTbl90ReferenceSource.RefSourceId == null && CurrentTbl90ReferenceSource.RefAuthorId == null)
        //            {
        //                MessageBox.Show(CultRes.StringsRes.RequiredGenealogyConnect, CultRes.StringsRes.RequiredInput,
        //                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
        //                return;
        //            }

        //            //check if dataset with vb-name already exist   
        //            var dataset = _businessLayer.ListTbl90ReferencesByRefExpertIdAndRefSourceIdAndRefAuthorIdAndInfo(CurrentTbl90ReferenceSource);

        //            if (dataset.Count != 0 && CurrentTbl90ReferenceSource.ReferenceId == 0)  //dataset exist
        //            {
        //                MessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTbl90ReferenceSource.ReferenceId.ToString(),
        //                MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
        //                return;
        //            }
        //            if (dataset.Count == 0 && CurrentTbl90ReferenceSource.ReferenceId == 0 ||
        //                dataset.Count != 0 && CurrentTbl90ReferenceSource.ReferenceId != 0 ||
        //                dataset.Count == 0 && CurrentTbl90ReferenceSource.ReferenceId != 0) //new dataset and update
        //            {
        //                if (MessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTbl90ReferenceSource.Info,
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
        //                    MessageBox.Show(CultRes.StringsRes.SaveSuccess,
        //                        CurrentTbl90ReferenceSource.ReferenceId == 0
        //                            ? CultRes.StringsRes.DatasetNew
        //                             : CurrentTbl90ReferenceSource.ReferenceId.ToString(),
        //                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //     //   _entityException.EntityException(ex);
        //        Log.Error(ex);
        //        return;
        //    }

        //    Tbl90ReferenceSourcesList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ReferenceListRefSourcesByDivisionId(CurrentTbl09Division.DivisionID));

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
        //        MessageBox.Show(CultRes.StringsRes.DatasetNew,
        //            CultRes.StringsRes.RequiredInput,
        //            MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
        //        return;
        //    }

        //    var reference = _businessLayer.SingleListTbl90ReferencesByReferenceId(CurrentTbl90ReferenceExpert.ReferenceId);

        //    Tbl90ReferenceExpertsList.Insert(0, new Tbl90Reference
        //    {
        //        RefExpertId = reference.RefExpertId,
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
        //        MessageBox.Show(CultRes.StringsRes.DatasetNew,
        //            CultRes.StringsRes.RequiredInput,
        //            MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
        //        return;
        //    }

        //    try
        //    {
        //        var reference = _businessLayer.SingleListTbl90ReferencesByReferenceId(CurrentTbl90ReferenceExpert.ReferenceId);
        //        if (reference != null)
        //        {
        //            if (MessageBox.Show(CultRes.StringsRes.DeleteQuestion1, CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl90ReferenceExpert.Info,
        //                    MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes)
        //                return;
        //            reference.EntityState = EntityState.Deleted;
        //            _businessLayer.RemoveReference(reference);

        //            MessageBox.Show(CultRes.StringsRes.DeleteSuccess, CurrentTbl90ReferenceExpert.Info,
        //                MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
        //        }
        //        else
        //        {
        //            MessageBox.Show(CultRes.StringsRes.Information, CultRes.StringsRes.DeleteCan + " " + CurrentTbl90ReferenceExpert.Info + " " + CultRes.StringsRes.DeleteCan1,
        //                MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //     //   _entityException.EntityException(ex);
        //        Log.Error(ex);
        //    }

        //    Tbl90ReferenceExpertsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ReferenceListRefExpertsByDivisionId(CurrentTbl09Division.DivisionID));

        //    ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
        //    ReferenceExpertsView.Refresh();
        //}
        ////----------------------------------------------------------------------            

        //public void SaveReferenceExpert(object o)
        //{
        //    if (CurrentTbl90ReferenceExpert == null)
        //    {
        //        MessageBox.Show(CultRes.StringsRes.DatasetNew,
        //            CultRes.StringsRes.RequiredInput,
        //            MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
        //        return;
        //    }

        //    CurrentTbl90ReferenceExpert.DivisionId = CurrentTbl09Division.DivisionId;

        //    try
        //    {
        //        var reference = _businessLayer.SingleListTbl90ReferencesByReferenceId(CurrentTbl90ReferenceExpert.ReferenceId);
        //        if (CurrentTbl90ReferenceExpert.ReferenceId != 0)
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

        //       //         reference.EntityState = EntityState.Modified;
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
        //          //      EntityState = EntityState.Added
        //            };
        //        }
        //        {
        //            //RefExpertID or RefSourceID or RefAuthorID may be not 0
        //            if (CurrentTbl90ReferenceExpert.RefExpertId == null && CurrentTbl90ReferenceExpert.RefSourceId == null && CurrentTbl90ReferenceExpert.RefAuthorId == null)
        //            {
        //                MessageBox.Show(CultRes.StringsRes.RequiredGenealogyConnect, CultRes.StringsRes.RequiredInput,
        //                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
        //                return;
        //            }

        //            //check if dataset with vb-name already exist   
        //            var dataset = _businessLayer.ListTbl90ReferencesByRefExpertIdAndRefSourceIdAndRefAuthorIdAndInfo(CurrentTbl90ReferenceExpert);

        //            if (dataset.Count != 0 && CurrentTbl90ReferenceExpert.ReferenceId == 0)  //dataset exist
        //            {
        //                MessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTbl90ReferenceExpert.ReferenceId.ToString(),
        //                MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
        //                return;
        //            }
        //            if (dataset.Count == 0 && CurrentTbl90ReferenceExpert.ReferenceId == 0 ||
        //                dataset.Count != 0 && CurrentTbl90ReferenceExpert.ReferenceId != 0 ||
        //                dataset.Count == 0 && CurrentTbl90ReferenceExpert.ReferenceId != 0) //new dataset and update
        //            {
        //                if (MessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTbl90ReferenceExpert.Info,
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
        //                    MessageBox.Show(CultRes.StringsRes.SaveSuccess,
        //                        CurrentTbl90ReferenceExpert.ReferenceId == 0
        //                            ? CultRes.StringsRes.DatasetNew
        //                             : CurrentTbl90ReferenceExpert.ReferenceId.ToString(),
        //                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //    //    _entityException.EntityException(ex);
        //        Log.Error(ex);
        //        return;
        //    }

        //    Tbl90ReferenceExpertsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ReferenceListRefExpertsByDivisionId(CurrentTbl09Division.DivisionId));

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
        //        MessageBox.Show(CultRes.StringsRes.DatasetNew,
        //            CultRes.StringsRes.RequiredInput,
        //            MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
        //        return;
        //    }

        //    var comment = _businessLayer.SingleListTbl93CommentsByCommentId(CurrentTbl93Comment.CommentId);

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
        //        MessageBox.Show(CultRes.StringsRes.DatasetNew,
        //            CultRes.StringsRes.RequiredInput,
        //            MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
        //        return;
        //    }

        //    try
        //    {
        //        var comment = _businessLayer.SingleListTbl93CommentsByCommentId(CurrentTbl93Comment.CommentId);
        //        if (comment != null)
        //        {
        //            if (MessageBox.Show(CultRes.StringsRes.DeleteQuestion1, CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl93Comment.Info,
        //                    MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes)
        //                return;
        //            comment.EntityState = EntityState.Deleted;
        //            _businessLayer.RemoveComment(comment);

        //            MessageBox.Show(CultRes.StringsRes.DeleteSuccess, CurrentTbl93Comment.Info,
        //                MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
        //        }
        //        else
        //        {
        //            MessageBox.Show(CultRes.StringsRes.Information, CultRes.StringsRes.DeleteCan + " " + CurrentTbl93Comment.Info + " " + CultRes.StringsRes.DeleteCan1,
        //                MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        ////        _entityException.EntityException(ex);
        //        Log.Error(ex);
        //    }

        //    Tbl93CommentsList = new ObservableCollection<Tbl93Comment>(_businessLayer.ListTbl93CommentsByDivisionId(CurrentTbl09Division.DivisionId));

        //    CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
        //    CommentsView.Refresh();
        //}
        ////----------------------------------------------------------------------            

        //private void SaveComment(object o)
        //{
        //    if (CurrentTbl93Comment == null)
        //    {
        //        MessageBox.Show(CultRes.StringsRes.DatasetNew,
        //            CultRes.StringsRes.RequiredInput,
        //            MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
        //        return;
        //    }

        //    CurrentTbl93Comment.DivisionId = CurrentTbl09Division.DivisionId;

        //    try
        //    {
        //        var comment = _businessLayer.SingleListTbl93CommentsByCommentId(CurrentTbl93Comment.CommentId);
        //        if (CurrentTbl93Comment.CommentId != 0)
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
        //        //        comment.EntityState = EntityState.Modified;
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
        // //               EntityState = EntityState.Added
        //            };
        //        }
        //        {
        //            //check if dataset with Name and VbIds already exist       
        //            var dataset = _businessLayer.ListTbl93CommentsByCurrentItem(CurrentTbl93Comment);

        //            if (dataset.Count != 0 && CurrentTbl93Comment.CommentId == 0)  //dataset exist
        //            {
        //                MessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTbl93Comment.Info,
        //                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
        //                return;
        //            }

        //            if (dataset.Count == 0 && CurrentTbl93Comment.CommentId == 0 ||
        //                dataset.Count != 0 && CurrentTbl93Comment.CommentId != 0 ||
        //                dataset.Count == 0 && CurrentTbl93Comment.CommentId != 0) //new dataset and update
        //            {
        //                if (MessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTbl93Comment.Info,
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
        //                    MessageBox.Show(CultRes.StringsRes.SaveSuccess,
        //                        CurrentTbl93Comment.CommentId == 0
        //                            ? CultRes.StringsRes.DatasetNew
        //                            : CurrentTbl93Comment.Info,
        //                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //      //  _entityException.EntityException(ex);
        //        Log.Error(ex);
        //        return;
        //    }

        //    Tbl93CommentsList = new ObservableCollection<Tbl93Comment>(_businessLayer.ListTbl93CommentsByDivisionId(CurrentTbl09Division.DivisionId));

        //    SelectedMainTabIndex = 3;

        //    CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
        //    CommentsView.Refresh();
        //}
        //#endregion "Public Commands"                  


        //    Part 9    


        #region "Public Commands Connected Tables by DoubleClick"

        private RelayCommand _getConnectedTablesCommand;
        public ICommand GetConnectedTablesCommand => _getConnectedTablesCommand ??= new RelayCommand(delegate { GetConnectedTablesById(null); });

        private void GetConnectedTablesById(object o)
        {
            Tbl15SubdivisionsList?.Clear();
            Tbl90ReferenceExpertsList?.Clear();
            Tbl90ReferenceSourcesList?.Clear();
            Tbl90ReferenceAuthorsList?.Clear();
            Tbl93CommentsList?.Clear();

            SelectedMainTabIndex = 0;  //change to Connect tab
            SelectedMainSubRefTabIndex = 0;
            SelectedDetailTabIndex = 1;
            SelectedDetailSubTabIndex = 0;
            SelectedDetailSubRefTabIndex = 0;

          //  Tbl03RegnumsList = new ObservableCollection<Tbl03Regnum>(_businessLayer.ListTbl03RegnumsByRegnumId(CurrentTbl09Division.RegnumId));
            Tbl03RegnumsList = new ObservableCollection<Tbl03Regnum>(_uow.Tbl03Regnums.Find(x => x.RegnumId == CurrentTbl09Division.RegnumId));

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
            //        Tbl03RegnumsList = new ObservableCollection<Tbl03Regnum>(_businessLayer.ListTbl03RegnumsByRegnumId(CurrentTbl09Division.RegnumId));
                    Tbl03RegnumsList = new ObservableCollection<Tbl03Regnum>(_uow.Tbl03Regnums.Find(x => x.RegnumId == CurrentTbl09Division.RegnumId));

                    RegnumsView = CollectionViewSource.GetDefaultView(Tbl03RegnumsList);
                    RegnumsView.Refresh();

                    SelectedMainTabIndex = 0;
                }
                if (_selectedDetailSubTabIndex == 1)
                {
                  //  Tbl15SubdivisionsList = new ObservableCollection<Tbl15Subdivision>(_businessLayer.ListTbl15SubdivisionsByDivisionId(CurrentTbl09Division.DivisionId));
                    Tbl15SubdivisionsList = new ObservableCollection<Tbl15Subdivision>(_uow.Tbl15Subdivisions.Find(x => x.DivisionId == CurrentTbl09Division.DivisionId));

                    SubdivisionsView = CollectionViewSource.GetDefaultView(Tbl15SubdivisionsList);
                    SubdivisionsView.Refresh();

                    SelectedMainTabIndex = 1;
                }
                if (_selectedDetailSubTabIndex == 2)
                {
                    //Tbl90ExpertsAllList = new ObservableCollection<Tbl90RefExpert>(_businessLayer.ListTbl90RefExperts());

                    //Tbl90ReferenceExpertsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ReferenceListRefExpertsByDivisionId(CurrentTbl09Division.DivisionId));

                    ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
                    ReferenceExpertsView.Refresh();

                    SelectedMainTabIndex = 2;
                }
                if (_selectedDetailSubTabIndex == 3)
                {
             //       Tbl93CommentsList = new ObservableCollection<Tbl93Comment>(_businessLayer.ListTbl93CommentsByDivisionId(CurrentTbl09Division.DivisionId));

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
              //      Tbl90ExpertsAllList = new ObservableCollection<Tbl90RefExpert>(_businessLayer.ListTbl90RefExperts());

               //     Tbl90ReferenceExpertsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ReferenceListRefExpertsByDivisionId(CurrentTbl09Division.DivisionId));

                    ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
                    ReferenceExpertsView.Refresh();

                    SelectedMainSubRefTabIndex = 0;
                }
                if (_selectedDetailSubRefTabIndex == 1)
                {
                //    Tbl90SourcesAllList = new ObservableCollection<Tbl90RefSource>(_businessLayer.ListTbl90RefSources());

              //      Tbl90ReferenceSourcesList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ReferenceListRefSourcesByDivisionId(CurrentTbl09Division.DivisionId));

                    ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
                    ReferenceSourcesView.Refresh();

                    SelectedMainSubRefTabIndex = 1;
                }
                if (_selectedDetailSubRefTabIndex == 2)
                {
             //       Tbl90AuthorsAllList = new ObservableCollection<Tbl90RefAuthor>(_businessLayer.ListTbl90RefAuthors());

             //       Tbl90ReferenceAuthorsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ReferenceListRefAuthorsByDivisionId(CurrentTbl09Division.DivisionId));

                    ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
                    ReferenceAuthorsView.Refresh();

                    SelectedMainSubRefTabIndex = 2;
                }
            }
        }

        #endregion "Public Commands to open Detail TabItems"


        //    Part 11    


        #region "Public Properties Tbl09Division"

        private string _searchDivisionName = "";
        public string SearchDivisionName
        {
            get => _searchDivisionName;
            set { _searchDivisionName = value; RaisePropertyChanged("SearchDivisionName"); }
        }

        public ICollectionView DivisionsView;
        private Tbl09Division CurrentTbl09Division => DivisionsView?.CurrentItem as Tbl09Division;

        private ObservableCollection<Tbl09Division> _tbl09DivisionsList;
        public ObservableCollection<Tbl09Division> Tbl09DivisionsList
        {
            get => _tbl09DivisionsList;
            set { _tbl09DivisionsList = value; RaisePropertyChanged("Tbl09DivisionsList"); }
        }

        private ObservableCollection<Tbl09Division> _tbl09DivisionsAllList;
        public ObservableCollection<Tbl09Division> Tbl09DivisionsAllList
        {
            get => _tbl09DivisionsAllList;
            set { _tbl09DivisionsAllList = value; RaisePropertyChanged("Tbl09DivisionsAllList"); }
        }


        #endregion "Public Properties"   

        #region "Public Properties Tbl03Regnum"

        public ICollectionView RegnumsView;
        private Tbl03Regnum CurrentTbl03Regnum => RegnumsView?.CurrentItem as Tbl03Regnum;

        private ObservableCollection<Tbl03Regnum> _tbl03RegnumsList;
        public ObservableCollection<Tbl03Regnum> Tbl03RegnumsList
        {
            get => _tbl03RegnumsList;
            set { _tbl03RegnumsList = value; RaisePropertyChanged("Tbl03RegnumsList"); }
        }

        private ObservableCollection<Tbl03Regnum> _tbl03RegnumsAllList;
        public ObservableCollection<Tbl03Regnum> Tbl03RegnumsAllList
        {
            get => _tbl03RegnumsAllList;
            set { _tbl03RegnumsAllList = value; RaisePropertyChanged("Tbl03RegnumsAllList"); }
        }

        #endregion "Public Properties"   

        #region "Public Properties Tbl15Subdivision"

        public ICollectionView SubdivisionsView;
        private Tbl15Subdivision CurrentTbl15Subdivision => SubdivisionsView?.CurrentItem as Tbl15Subdivision;

        private ObservableCollection<Tbl15Subdivision> _tbl15SubdivisionsList;
        public ObservableCollection<Tbl15Subdivision> Tbl15SubdivisionsList
        {
            get => _tbl15SubdivisionsList;
            set { _tbl15SubdivisionsList = value; RaisePropertyChanged("Tbl15SubdivisionsList"); }
        }
        #endregion "Public Properties"     

        #region "Public Properties Tbl18Superclass"

        public ICollectionView SuperclassesView;
        private Tbl18Superclass CurrentTbl18Superclass => SuperclassesView?.CurrentItem as Tbl18Superclass;

        private ObservableCollection<Tbl18Superclass> _tbl18SuperclassesList;
        public ObservableCollection<Tbl18Superclass> Tbl18SuperclassesList
        {
            get => _tbl18SuperclassesList;
            set { _tbl18SuperclassesList = value; RaisePropertyChanged("Tbl18SuperclassesList"); }
        }
        #endregion "Public Properties"     

        #region "Public Properties Tbl90Author"

        private ObservableCollection<Tbl90RefAuthor> _tbl90AuthorsAllList;
        public ObservableCollection<Tbl90RefAuthor> Tbl90AuthorsAllList
        {
            get => _tbl90AuthorsAllList;
            set { _tbl90AuthorsAllList = value; RaisePropertyChanged("Tbl90AuthorsAllList"); }
        }

        #endregion "Public Properties "

        #region "Public Properties Tbl90Source"

        private ObservableCollection<Tbl90RefSource> _tbl90SourcesAllList;
        public ObservableCollection<Tbl90RefSource> Tbl90SourcesAllList
        {
            get => _tbl90SourcesAllList;
            set { _tbl90SourcesAllList = value; RaisePropertyChanged("Tbl90SourcesAllList"); }
        }

        #endregion "Public Properties "

        #region "Public Properties Tbl90Expert"

        private ObservableCollection<Tbl90RefExpert> _tbl90ExpertsAllList;
        public ObservableCollection<Tbl90RefExpert> Tbl90ExpertsAllList
        {
            get => _tbl90ExpertsAllList;
            set { _tbl90ExpertsAllList = value; RaisePropertyChanged("Tbl90ExpertsAllList"); }
        }

        #endregion "Public Properties "

        #region "Public Properties Tbl90ReferenceAuthor"

        public ICollectionView ReferenceAuthorsView;
        private Tbl90Reference CurrentTbl90ReferenceAuthor => ReferenceAuthorsView?.CurrentItem as Tbl90Reference;

        private ObservableCollection<Tbl90Reference> _tbl90ReferenceAuthorsList;
        public ObservableCollection<Tbl90Reference> Tbl90ReferenceAuthorsList
        {
            get => _tbl90ReferenceAuthorsList;
            set { _tbl90ReferenceAuthorsList = value; RaisePropertyChanged("Tbl90ReferenceAuthorsList"); }
        }

        #endregion "Public Properties"

        #region "Public Properties Tbl90ReferenceSource"

        public ICollectionView ReferenceSourcesView;
        private Tbl90Reference CurrentTbl90ReferenceSource => ReferenceSourcesView?.CurrentItem as Tbl90Reference;

        private ObservableCollection<Tbl90Reference> _tbl90ReferenceSourcesList;
        public ObservableCollection<Tbl90Reference> Tbl90ReferenceSourcesList
        {
            get => _tbl90ReferenceSourcesList;
            set { _tbl90ReferenceSourcesList = value; RaisePropertyChanged("Tbl90ReferenceSourcesList"); }
        }

        #endregion "Public Properties"

        #region "Public Properties Tbl90ReferenceExpert"

        public ICollectionView ReferenceExpertsView;
        private Tbl90Reference CurrentTbl90ReferenceExpert => ReferenceExpertsView?.CurrentItem as Tbl90Reference;

        private ObservableCollection<Tbl90Reference> _tbl90ReferenceExpertsList;
        public ObservableCollection<Tbl90Reference> Tbl90ReferenceExpertsList
        {
            get => _tbl90ReferenceExpertsList;
            set { _tbl90ReferenceExpertsList = value; RaisePropertyChanged("Tbl90ReferenceExpertsList"); }
        }

        #endregion "Public Properties"   

        #region "Public Properties Tbl93Comment"

        public ICollectionView CommentsView;
        private Tbl93Comment CurrentTbl93Comment => CommentsView?.CurrentItem as Tbl93Comment;

        private ObservableCollection<Tbl93Comment> _tbl93CommentsList;
        public ObservableCollection<Tbl93Comment> Tbl93CommentsList
        {
            get => _tbl93CommentsList;
            set { _tbl93CommentsList = value; RaisePropertyChanged("Tbl93CommentsList"); }
        }

        #endregion "Public Properties"     






    }
}
