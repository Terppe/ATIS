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

    
         //    Tbl57TribussesViewModel Skriptdatum:  08.11.2018  10:32    

namespace Te.Atis.Ui.Desktop.Views.Database
{     
    
    public class Tbl57TribussesViewModel : ViewModelBase                     
    {     
         
        #region "Private Data Members"
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private static IBusinessLayer _businessLayer;
        private static DbEntityException _entityException;
        private int _position;   
         
        #endregion "Private Data Members"               
      
        #region "Constructor"

        public Tbl57TribussesViewModel()
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

             
        #region "Public Commands Basic Tbl57Tribus"
        //-------------------------------------------------------------------------
        private RelayCommand _clearTribusCommand;

        public ICommand ClearTribusCommand => _clearTribusCommand ??
                                                  (_clearTribusCommand = new RelayCommand(delegate { ClearTribus(null); }));         
             
        private RelayCommand _getTribussesByNameOrIdCommand;  

        public  ICommand GetTribussesByNameOrIdCommand => _getTribussesByNameOrIdCommand ??
                                                           (_getTribussesByNameOrIdCommand = new RelayCommand(delegate { GetTribussesByNameOrId(null); }));        
             
        private RelayCommand _addTribusCommand;

        public ICommand AddTribusCommand => _addTribusCommand ??
                                                (_addTribusCommand = new RelayCommand(delegate { AddTribus(null); }));

        private RelayCommand _copyTribusCommand;

        public ICommand CopyTribusCommand => _copyTribusCommand ??
                                                 (_copyTribusCommand = new RelayCommand(delegate { CopyTribus(null); }));      
             
        private RelayCommand _deleteTribusCommand;

        public ICommand DeleteTribusCommand => _deleteTribusCommand ??
                                                   (_deleteTribusCommand = new RelayCommand(delegate { DeleteTribus(null); }));    
             
        private RelayCommand _saveTribusCommand;

        public ICommand SaveTribusCommand => _saveTribusCommand ??
                                                 (_saveTribusCommand = new RelayCommand(delegate { SaveTribus(null); }));
        //-------------------------------------------------------------------------          
     
        private void ClearTribus(object o)
        {
            SearchTribusName = "";

            SelectedMainTabIndex = 0;  //change tab
            SelectedDetailTabIndex = 0;
            SelectedDetailSubTabIndex = 0;
            SelectedDetailSubRefTabIndex = 0;

            Tbl54SupertribussesList?.Clear();
            Tbl57TribussesList?.Clear();
            Tbl60SubtribussesList?.Clear();
            Tbl90ReferenceExpertsList?.Clear();
            Tbl90ReferenceSourcesList?.Clear();
            Tbl90ReferenceAuthorsList?.Clear();
            Tbl93CommentsList?.Clear();
        }
        //----------------------------------------------------------------------                  
     
        private void GetTribussesByNameOrId(object o)
        {
            if (SearchTribusName != "")
            {
                Tbl57TribussesList?.Clear();
                if (SearchTribusName == "*") // show whole table
                {
                    SearchTribusName = "";
                    _businessLayer = new BusinessLayer.BusinessLayer();
                    Tbl54SupertribussesAllList = new ObservableCollection<Tbl54Supertribus>(_businessLayer.ListTbl54Supertribusses());
                    Tbl57TribussesList = new ObservableCollection<Tbl57Tribus>(_businessLayer.ListTbl57TribussesByTribusName(SearchTribusName));
                    SearchTribusName = "*";
                }
                else
                {
                    _businessLayer = new BusinessLayer.BusinessLayer();
                    Tbl54SupertribussesAllList = new ObservableCollection<Tbl54Supertribus>(_businessLayer.ListTbl54Supertribusses());
                    Tbl57TribussesList = int.TryParse(SearchTribusName, out var id) ?
                        new ObservableCollection<Tbl57Tribus>(_businessLayer.ListTbl57TribussesByTribusId(id)) :
                        new ObservableCollection<Tbl57Tribus>(_businessLayer.ListTbl57TribussesByTribusName(SearchTribusName));
                }

                if (Tbl57TribussesList.Count == 0)
                {
                    WpfMessageBox.Show(CultRes.StringsRes.Tables, CultRes.StringsRes.DatasetNot,
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                }
                else
                {
                    Tbl54SupertribussesList?.Clear();
                    Tbl60SubtribussesList?.Clear();
                    Tbl90ReferenceExpertsList?.Clear();
                    Tbl90ReferenceSourcesList?.Clear();
                    Tbl90ReferenceAuthorsList?.Clear();
                    Tbl93CommentsList?.Clear();
                }
            }
            else
            {
                WpfMessageBox.Show(CultRes.StringsRes.SearchNameOrId, CultRes.StringsRes.InputRequested,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
            }
            TribussesView = CollectionViewSource.GetDefaultView(Tbl57TribussesList);
            TribussesView.Refresh();
        }
        //------------------------------------------------------------------------------------                          
     
        private void AddTribus(object o)
        {
            if (Tbl57TribussesList == null)
                Tbl57TribussesList =  new ObservableCollection<Tbl57Tribus>( );

            Tbl57TribussesList.Insert(0, new Tbl57Tribus   {   TribusName = CultRes.StringsRes.DatasetNew  }  );

                    _businessLayer = new BusinessLayer.BusinessLayer();
                Tbl54SupertribussesAllList = new ObservableCollection<Tbl54Supertribus>(_businessLayer.ListTbl54Supertribusses());

            TribussesView = CollectionViewSource.GetDefaultView(Tbl57TribussesList);
            TribussesView.MoveCurrentToFirst();
        }
        //------------------------------------------------------------------------------------                               
     
        private void CopyTribus(object o)
        {
            if (CurrentTbl57Tribus == null)
            {
                WpfMessageBox.Show(CultRes.StringsRes.DatasetNew,
                    CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                return;
            }
            _businessLayer = new BusinessLayer.BusinessLayer();

            var tribus = _businessLayer.SingleListTbl57TribussesByTribusId(CurrentTbl57Tribus.TribusID);

            Tbl57TribussesList.Insert(0, new Tbl57Tribus
            {
                 SupertribusID =  tribus. SupertribusID,
                 TribusName = CultRes.StringsRes.DatasetNew,
                Valid =  tribus.Valid,
                ValidYear =  tribus.ValidYear,
                Synonym =  tribus.Synonym,
                Author =  tribus.Author,
                AuthorYear =  tribus.AuthorYear,
                Info =  tribus.Info,
                EngName =  tribus.EngName,
                GerName =  tribus.GerName,
                FraName =  tribus.FraName,
                PorName =  tribus.PorName,
                Memo =  tribus.Memo
            });

            TribussesView = CollectionViewSource.GetDefaultView(Tbl57TribussesList);
            TribussesView.MoveCurrentToFirst();
        }
        //---------------------------------------------------------------------------------------                            
     
        private void DeleteTribus(object o)
        {
            if (CurrentTbl57Tribus == null)
            {
                WpfMessageBox.Show(CultRes.StringsRes.DatasetNew,
                    CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                return;
            }
            _businessLayer = new BusinessLayer.BusinessLayer();

            var ret = false;
            //check if in Tbl60Subtribusses connected datasets, than return
            Tbl60SubtribussesList = new ObservableCollection<Tbl60Subtribus>(_businessLayer.ListTbl60SubtribussesByTribusId(CurrentTbl57Tribus.TribusID));
            if (Tbl60SubtribussesList.Count != 0)
            {
                WpfMessageBox.Show(CultRes.StringsRes.ConnectedTable, CultRes.StringsRes.Subtribus + " " + CultRes.StringsRes.ConnectedDataset,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                 ret = true;              
            }
            Tbl90ReferenceAuthorsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ReferenceListRefAuthorsByTribusId(CurrentTbl57Tribus.TribusID));
            if (Tbl90ReferenceAuthorsList.Count != 0)
            {
                WpfMessageBox.Show(CultRes.StringsRes.ConnectedTable, CultRes.StringsRes.ReferenceAuthor + " " + CultRes.StringsRes.ConnectedDataset,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                ret = true;
            }
            Tbl90ReferenceSourcesList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ReferenceListRefSourcesByTribusId(CurrentTbl57Tribus.TribusID));
            if (Tbl90ReferenceSourcesList.Count != 0)
            {
                WpfMessageBox.Show(CultRes.StringsRes.ConnectedTable, CultRes.StringsRes.ReferenceSource + " " + CultRes.StringsRes.ConnectedDataset,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                ret = true;
            }
            Tbl90ReferenceExpertsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ReferenceListRefExpertsByTribusId(CurrentTbl57Tribus.TribusID));
            if (Tbl90ReferenceExpertsList.Count != 0)
            {
                WpfMessageBox.Show(CultRes.StringsRes.ConnectedTable, CultRes.StringsRes.ReferenceExpert + " " + CultRes.StringsRes.ConnectedDataset,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                ret = true;
            }
            Tbl93CommentsList = new ObservableCollection<Tbl93Comment>(_businessLayer.ListTbl93CommentsByTribusId(CurrentTbl57Tribus.TribusID));
            if (Tbl93CommentsList.Count != 0)
            {
                WpfMessageBox.Show(CultRes.StringsRes.ConnectedTable, CultRes.StringsRes.Comment + " " + CultRes.StringsRes.ConnectedDataset,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                ret = true;
            }
            if (ret)  return;
            {
            try
            {
                var tribus = _businessLayer.SingleListTbl57TribussesByTribusId(CurrentTbl57Tribus.TribusID);
                if (tribus != null)
                {
                    if (WpfMessageBox.Show(CultRes.StringsRes.DeleteQuestion1, CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl57Tribus.TribusName,
                            MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes)
                        return;
                    tribus.EntityState = EntityState.Deleted;
                    _businessLayer.RemoveTribus(tribus);

                    WpfMessageBox.Show(CultRes.StringsRes.DeleteSuccess, CurrentTbl57Tribus.TribusName,
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                }
                else
                {
                    WpfMessageBox.Show(CultRes.StringsRes.Information, CultRes.StringsRes.DeleteCan + " " + CurrentTbl57Tribus.TribusName + " " + CultRes.StringsRes.DeleteCan1,
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                }
            }
            catch (DbEntityValidationException ex)
            {
                _entityException.EntityException(ex);
            }
         }
            if (SearchTribusName != "")
            {
                if (SearchTribusName == "*")  //show all datasets
                {
                    SearchTribusName = "";
                    Tbl57TribussesList.Clear();
                    
                Tbl57TribussesList = new ObservableCollection<Tbl57Tribus>(_businessLayer.ListTbl57TribussesByTribusName(SearchTribusName));            
                    SearchTribusName = "*";
                }
                else
                {               
                    Tbl57TribussesList =  new ObservableCollection<Tbl57Tribus>(_businessLayer.ListTbl57TribussesByTribusName(SearchTribusName));

                }
                TribussesView = CollectionViewSource.GetDefaultView(Tbl57TribussesList);
                TribussesView.Refresh();
            }
            else  //SearchName = empty
            {
                Tbl57TribussesList = new ObservableCollection<Tbl57Tribus>(_businessLayer.ListTbl57TribussesByTribusName(SearchTribusName));

                TribussesView = CollectionViewSource.GetDefaultView(Tbl57TribussesList);
                TribussesView.MoveCurrentToFirst();
             }
        }
        //-------------------------------------------------------------------------------------------------                    
     
        private void SaveTribus(object o)
        {
            if (CurrentTbl57Tribus == null)
            {
                WpfMessageBox.Show(CultRes.StringsRes.DatasetNew,
                    CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                return;
            }
            _businessLayer = new BusinessLayer.BusinessLayer();

            try
            {
                var tribus = _businessLayer.SingleListTbl57TribussesByTribusId(CurrentTbl57Tribus.TribusID);
                if (CurrentTbl57Tribus.TribusID != 0)
                {
                    if (tribus != null) //update
                    {
                        tribus.TribusName = CurrentTbl57Tribus.TribusName;
                        tribus.SupertribusID = CurrentTbl57Tribus.SupertribusID;
                        tribus.Valid = CurrentTbl57Tribus.Valid;
                        tribus.ValidYear = CurrentTbl57Tribus.ValidYear;       
                        tribus.Synonym = CurrentTbl57Tribus.Synonym;
                        tribus.Author = CurrentTbl57Tribus.Author;
                        tribus.AuthorYear = CurrentTbl57Tribus.AuthorYear;
                        tribus.Info = CurrentTbl57Tribus.Info;
                        tribus.EngName = CurrentTbl57Tribus.EngName;
                        tribus.GerName = CurrentTbl57Tribus.GerName;
                        tribus.FraName = CurrentTbl57Tribus.FraName;
                        tribus.PorName = CurrentTbl57Tribus.PorName;
                        tribus.Updater = Environment.UserName;
                        tribus.UpdaterDate = DateTime.Now;
                        tribus.Memo = CurrentTbl57Tribus.Memo;
                        tribus.EntityState = EntityState.Modified;
                    }
                }
                else
                {
                    tribus = new Tbl57Tribus   //add new
                    {
                        TribusName = CurrentTbl57Tribus.TribusName,
                        SupertribusID = CurrentTbl57Tribus.SupertribusID,

                        CountID = RandomHelper.Randomnumber(),
                        Valid = CurrentTbl57Tribus.Valid,
                        ValidYear = CurrentTbl57Tribus.ValidYear,
                        Synonym = CurrentTbl57Tribus.Synonym,
                        Author = CurrentTbl57Tribus.Author,
                        AuthorYear = CurrentTbl57Tribus.AuthorYear,
                        Info = CurrentTbl57Tribus.Info,
                        EngName = CurrentTbl57Tribus.EngName,
                        GerName = CurrentTbl57Tribus.GerName,
                        FraName = CurrentTbl57Tribus.FraName,
                        PorName = CurrentTbl57Tribus.PorName,
                        Writer = Environment.UserName,
                        WriterDate = DateTime.Now,
                        Updater = Environment.UserName,
                        UpdaterDate = DateTime.Now,
                        Memo = CurrentTbl57Tribus.Memo,
                        EntityState = EntityState.Added
                    };
                }
                {
                    //SupertribusID may be not 0
                    if (CurrentTbl57Tribus.SupertribusID == 0)          

                    {
                        WpfMessageBox.Show(CultRes.StringsRes.RequiredGenealogyConnect, CultRes.StringsRes.RequiredInput,
                            MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        return;
                    }

                    //check if dataset with Name and SupertribusId already exist       
                    var dataset = _businessLayer.ListTbl57TribussesByTribusNameAndSupertribusId(CurrentTbl57Tribus.TribusName, CurrentTbl57Tribus.SupertribusID);

                    if (dataset.Count != 0 && CurrentTbl57Tribus.TribusID == 0)  //dataset exist
                    {
                        WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTbl57Tribus.TribusName,
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        return;
                    }
                    if (dataset.Count == 0 && CurrentTbl57Tribus.TribusID == 0 ||
                        dataset.Count != 0 && CurrentTbl57Tribus.TribusID != 0 ||
                        dataset.Count == 0 && CurrentTbl57Tribus.TribusID != 0) //new dataset and update
                    {
                        if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTbl57Tribus.TribusName,
                                MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes)
                            return;
                        {
                                _businessLayer.UpdateTribus(tribus);
                                _position = TribussesView.CurrentPosition;

                                    WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess,
                                        CurrentTbl57Tribus.TribusID == 0
                                            ? CultRes.StringsRes.DatasetNew
                                            : CurrentTbl57Tribus.TribusName,
                                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                          }
                    }
                }
            }
            catch (DbEntityValidationException ex)
            {
                _entityException.EntityException(ex);
            }

            if (SearchTribusName != "")
            {
                if (SearchTribusName == "*")  //show all datasets
                {
                    SearchTribusName = "";
                    Tbl57TribussesList.Clear();
                    
                Tbl57TribussesList = new ObservableCollection<Tbl57Tribus>(_businessLayer.ListTbl57TribussesByTribusName(SearchTribusName));            
                    SearchTribusName = "*";
                }
                else
                {               
                    Tbl57TribussesList = int.TryParse(SearchTribusName, out var id)
                        ? new ObservableCollection<Tbl57Tribus>(_businessLayer.ListTbl57TribussesByTribusId(id))
                        : new ObservableCollection<Tbl57Tribus>(_businessLayer.ListTbl57TribussesByTribusName(SearchTribusName));

                }
                TribussesView = CollectionViewSource.GetDefaultView(Tbl57TribussesList);
                TribussesView.MoveCurrentToPosition(_position);
            }
            else  
            {
                Tbl57TribussesList = new ObservableCollection<Tbl57Tribus>(_businessLayer.ListTbl57TribussesByTribusName(CurrentTbl57Tribus.TribusName));

                TribussesView= CollectionViewSource.GetDefaultView(Tbl57TribussesList);
                TribussesView.Refresh();
            }
        }
        #endregion "Public Commands"                  
 
 

 //    Part 2    

           
        #region "Public Commands Connect <== Tbl54Supertribus"                 
        //-------------------------------------------------------------------------

        private RelayCommand _saveSupertribusCommand;

        public ICommand SaveSupertribusCommand => _saveSupertribusCommand ??
                                                 (_saveSupertribusCommand = new RelayCommand(delegate { SaveSupertribus(null); }));

        //-------------------------------------------------------------------------          
     
        private void SaveSupertribus(object o)
        {
            if (CurrentTbl54Supertribus == null)
            {
                WpfMessageBox.Show(CultRes.StringsRes.DatasetNew,
                    CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                return;
            }

            try
            {
                var supertribus = _businessLayer.SingleListTbl54SupertribussesBySupertribusId(CurrentTbl54Supertribus.SupertribusID);
                if (CurrentTbl54Supertribus.SupertribusID != 0)
                {
                    if (supertribus != null) //update
                    {
                        supertribus.SupertribusName = CurrentTbl54Supertribus.SupertribusName;
                        supertribus.Valid = CurrentTbl54Supertribus.Valid;
                        supertribus.ValidYear = CurrentTbl54Supertribus.ValidYear;       
                        supertribus.Synonym = CurrentTbl54Supertribus.Synonym;
                        supertribus.Author = CurrentTbl54Supertribus.Author;
                        supertribus.AuthorYear = CurrentTbl54Supertribus.AuthorYear;
                        supertribus.Info = CurrentTbl54Supertribus.Info;
                        supertribus.EngName = CurrentTbl54Supertribus.EngName;
                        supertribus.GerName = CurrentTbl54Supertribus.GerName;
                        supertribus.FraName = CurrentTbl54Supertribus.FraName;
                        supertribus.PorName = CurrentTbl54Supertribus.PorName;
                        supertribus.Updater = Environment.UserName;
                        supertribus.UpdaterDate = DateTime.Now;
                        supertribus.Memo = CurrentTbl54Supertribus.Memo;
                        supertribus.EntityState = EntityState.Modified;
                    }
                }
                else
                {
                    supertribus = new Tbl54Supertribus   //add new
                    {
                        SupertribusName = CurrentTbl54Supertribus.SupertribusName,              
                        InfrafamilyID = CurrentTbl54Supertribus.InfrafamilyID,     
                        CountID = RandomHelper.Randomnumber(),

                        Valid = CurrentTbl54Supertribus.Valid,
                        ValidYear = CurrentTbl54Supertribus.ValidYear,
                        Synonym = CurrentTbl54Supertribus.Synonym,
                        Author = CurrentTbl54Supertribus.Author,
                        AuthorYear = CurrentTbl54Supertribus.AuthorYear,
                        Info = CurrentTbl54Supertribus.Info,
                        EngName = CurrentTbl54Supertribus.EngName,
                        GerName = CurrentTbl54Supertribus.GerName,
                        FraName = CurrentTbl54Supertribus.FraName,
                        PorName = CurrentTbl54Supertribus.PorName,
                        Writer = Environment.UserName,
                        WriterDate = DateTime.Now,
                        Updater = Environment.UserName,
                        UpdaterDate = DateTime.Now,
                        Memo = CurrentTbl54Supertribus.Memo,
                        EntityState = EntityState.Added
                    };
                }
                {
                    //InfrafamilyID may be not 0
                    if (CurrentTbl54Supertribus.InfrafamilyID == 0)          

                    {
                        WpfMessageBox.Show(CultRes.StringsRes.RequiredGenealogyConnect, CultRes.StringsRes.RequiredInput,
                            MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        return;
                    }

                    //check if dataset with Name and SupertribusId already exist       
                    var dataset = _businessLayer.ListTbl54SupertribussesBySupertribusNameAndInfrafamilyId(CurrentTbl54Supertribus.SupertribusName, CurrentTbl54Supertribus.InfrafamilyID);

                    if (dataset.Count != 0 && CurrentTbl54Supertribus.SupertribusID == 0)  //dataset exist
                    {
                        WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTbl54Supertribus.SupertribusName,
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        return;
                    }
                    if (dataset.Count == 0 && CurrentTbl54Supertribus.SupertribusID == 0 ||
                        dataset.Count != 0 && CurrentTbl54Supertribus.SupertribusID != 0 ||
                        dataset.Count == 0 && CurrentTbl54Supertribus.SupertribusID != 0) //new dataset and update
                    {
                        if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTbl54Supertribus.SupertribusName,
                                MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes)
                            return;
                        {
                                _businessLayer.UpdateSupertribus(supertribus);

                                    WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess,
                                        CurrentTbl54Supertribus.SupertribusID == 0
                                            ? CultRes.StringsRes.DatasetNew
                                            : CurrentTbl54Supertribus.SupertribusName,
                                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        }
                    }
                }
            }
            catch (DbEntityValidationException ex)
            {
                _entityException.EntityException(ex);
            }

                Tbl54SupertribussesList = new ObservableCollection<Tbl54Supertribus>(_businessLayer.ListTbl54SupertribussesBySupertribusId(CurrentTbl57Tribus.SupertribusID));            

            SelectedMainTabIndex = 0;
            SupertribussesView = CollectionViewSource.GetDefaultView(Tbl54SupertribussesList);
            SupertribussesView.Refresh();
        }
        #endregion "Public Commands"                  
                                                          

 //    Part 3    

                                                          



 //    Part 4    

           
        #region "Public Commands Connect ==> Tbl60Subtribus"                 
        //-------------------------------------------------------------------------
        private RelayCommand _addSubtribusCommand;

        public ICommand AddSubtribusCommand => _addSubtribusCommand ??
                                                (_addSubtribusCommand = new RelayCommand(delegate { AddSubtribus(null); }));

        private RelayCommand _copySubtribusCommand;

        public ICommand CopySubtribusCommand => _copySubtribusCommand ??
                                                 (_copySubtribusCommand = new RelayCommand(delegate { CopySubtribus(null); }));

        private RelayCommand _deleteSubtribusCommand;

        public ICommand DeleteSubtribusCommand => _deleteSubtribusCommand ??
                                                 (_deleteSubtribusCommand = new RelayCommand(delegate { DeleteSubtribus(null); }));

        private RelayCommand _saveSubtribusCommand;

        public ICommand SaveSubtribusCommand => _saveSubtribusCommand ??
                                                 (_saveSubtribusCommand = new RelayCommand(delegate { SaveSubtribus(null); }));

        //-------------------------------------------------------------------------          
     
        private void AddSubtribus(object o)      
        {
            if (Tbl60SubtribussesList == null)
                Tbl60SubtribussesList =  new ObservableCollection<Tbl60Subtribus>( );

            Tbl60SubtribussesList.Insert(0, new Tbl60Subtribus  { SubtribusName = CultRes.StringsRes.DatasetNew});

            _businessLayer = new BusinessLayer.BusinessLayer();
                Tbl57TribussesAllList = new ObservableCollection<Tbl57Tribus>(_businessLayer.ListTbl57Tribusses());

            SubtribussesView = CollectionViewSource.GetDefaultView(Tbl60SubtribussesList);
            SubtribussesView.MoveCurrentToFirst();
        }
        //----------------------------------------------------------------------            
     
        private void CopySubtribus(object o)
        {
            if (CurrentTbl60Subtribus == null)
            {
                WpfMessageBox.Show(CultRes.StringsRes.DatasetNew,
                    CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                return;
            }

            var subtribus = _businessLayer.SingleListTbl60SubtribussesBySubtribusId(CurrentTbl60Subtribus.SubtribusID);

            Tbl60SubtribussesList.Insert(0, new Tbl60Subtribus
            {
                SubtribusName = CultRes.StringsRes.DatasetNew,
                Valid =  subtribus.Valid,
                ValidYear =  subtribus.ValidYear,
                Synonym =  subtribus.Synonym,
                Author =  subtribus.Author,
                AuthorYear =  subtribus.AuthorYear,
                Info =  subtribus.Info,
                EngName =  subtribus.EngName,
                GerName =  subtribus.GerName,
                FraName =  subtribus.FraName,
                PorName =  subtribus.PorName,
                Memo =  subtribus.Memo
            });

            SubtribussesView = CollectionViewSource.GetDefaultView(Tbl60SubtribussesList);
            SubtribussesView.MoveCurrentToFirst();
        }
        //----------------------------------------------------------------------            
           
        private void DeleteSubtribus(object o)
        {
            if (CurrentTbl60Subtribus == null)
            {
                WpfMessageBox.Show(CultRes.StringsRes.DatasetNew,
                    CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                return;
            }

            var ret = false;
            //check if in Tbl63Infratribusses connected datasets, than return
            Tbl63InfratribussesList = new ObservableCollection<Tbl63Infratribus>(_businessLayer.ListTbl63InfratribussesBySubtribusId(CurrentTbl60Subtribus.SubtribusID));
            if (Tbl63InfratribussesList.Count != 0)
            {
                WpfMessageBox.Show(CultRes.StringsRes.ConnectedTable, CultRes.StringsRes.Infratribus + " " + CultRes.StringsRes.ConnectedDataset,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                 ret = true;              
            }
            Tbl90ReferenceAuthorsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ReferenceListRefAuthorsBySubtribusId(CurrentTbl60Subtribus.SubtribusID));
            if (Tbl90ReferenceAuthorsList.Count != 0)
            {
                WpfMessageBox.Show(CultRes.StringsRes.ConnectedTable, CultRes.StringsRes.ReferenceAuthor + " " + CultRes.StringsRes.ConnectedDataset,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                ret = true;
            }
            Tbl90ReferenceSourcesList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ReferenceListRefSourcesBySubtribusId(CurrentTbl60Subtribus.SubtribusID));
            if (Tbl90ReferenceSourcesList.Count != 0)
            {
                WpfMessageBox.Show(CultRes.StringsRes.ConnectedTable, CultRes.StringsRes.ReferenceSource + " " + CultRes.StringsRes.ConnectedDataset,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                ret = true;
            }
            Tbl90ReferenceExpertsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ReferenceListRefExpertsBySubtribusId(CurrentTbl60Subtribus.SubtribusID));
            if (Tbl90ReferenceExpertsList.Count != 0)
            {
                WpfMessageBox.Show(CultRes.StringsRes.ConnectedTable, CultRes.StringsRes.ReferenceExpert + " " + CultRes.StringsRes.ConnectedDataset,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                ret = true;
            }
            Tbl93CommentsList = new ObservableCollection<Tbl93Comment>(_businessLayer.ListTbl93CommentsBySubtribusId(CurrentTbl60Subtribus.SubtribusID));
            if (Tbl93CommentsList.Count != 0)
            {
                WpfMessageBox.Show(CultRes.StringsRes.ConnectedTable, CultRes.StringsRes.Comment + " " + CultRes.StringsRes.ConnectedDataset,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                ret = true;
            }
            if (ret)  return;
            {
            try
            {
                var subtribus = _businessLayer.SingleListTbl60SubtribussesBySubtribusId(CurrentTbl60Subtribus.SubtribusID);
                if (subtribus!= null)
                {
                    if (WpfMessageBox.Show(CultRes.StringsRes.DeleteQuestion1, CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl60Subtribus.SubtribusName,
                         MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes) 
                    return;
                    subtribus.EntityState = EntityState.Deleted;
                    _businessLayer.RemoveSubtribus(subtribus);

                    WpfMessageBox.Show(CultRes.StringsRes.DeleteSuccess, CurrentTbl60Subtribus.SubtribusName,
                       MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);  
                }
                else
                {
                    WpfMessageBox.Show(CultRes.StringsRes.Information, CultRes.StringsRes.DeleteCan + " " + CurrentTbl60Subtribus.SubtribusName + " " + CultRes.StringsRes.DeleteCan1,
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                }
            }
            catch (DbEntityValidationException ex)
            {
                _entityException.EntityException(ex);
            }
         }
            Tbl60SubtribussesList = new ObservableCollection<Tbl60Subtribus>(_businessLayer.ListTbl60SubtribussesByTribusId(CurrentTbl57Tribus.TribusID));

            SubtribussesView = CollectionViewSource.GetDefaultView(Tbl60SubtribussesList);
            SubtribussesView.Refresh();
        }
        //-------------------------------------------------------------------------------------------------                    
     
        private void SaveSubtribus(object o)
        {
            if (CurrentTbl60Subtribus == null)
            {
                WpfMessageBox.Show(CultRes.StringsRes.DatasetNew,
                    CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                return;
            }

            CurrentTbl60Subtribus.TribusID = CurrentTbl57Tribus.TribusID;

            try
            {
                var subtribus = _businessLayer.SingleListTbl60SubtribussesBySubtribusId(CurrentTbl60Subtribus.SubtribusID);
                if (CurrentTbl60Subtribus.SubtribusID != 0)
                {
                    if (subtribus != null) //update
                    {
                        subtribus.SubtribusName = CurrentTbl60Subtribus.SubtribusName;
                        subtribus.TribusID = CurrentTbl60Subtribus.TribusID;
                        subtribus.Valid = CurrentTbl60Subtribus.Valid;
                        subtribus.ValidYear = CurrentTbl60Subtribus.ValidYear;       
                        subtribus.Synonym = CurrentTbl60Subtribus.Synonym;
                        subtribus.Author = CurrentTbl60Subtribus.Author;
                        subtribus.AuthorYear = CurrentTbl60Subtribus.AuthorYear;
                        subtribus.Info = CurrentTbl60Subtribus.Info;
                        subtribus.EngName = CurrentTbl60Subtribus.EngName;
                        subtribus.GerName = CurrentTbl60Subtribus.GerName;
                        subtribus.FraName = CurrentTbl60Subtribus.FraName;
                        subtribus.PorName = CurrentTbl60Subtribus.PorName;
                        subtribus.Updater = Environment.UserName;
                        subtribus.UpdaterDate = DateTime.Now;
                        subtribus.Memo = CurrentTbl60Subtribus.Memo;
                        subtribus.EntityState = EntityState.Modified;
                    }
                }
                else
                {
                    subtribus = new Tbl60Subtribus   //add new
                    {
                        SubtribusName = CurrentTbl60Subtribus.SubtribusName,              
                        TribusID = CurrentTbl60Subtribus.TribusID,     
                        CountID = RandomHelper.Randomnumber(),
                        Valid = CurrentTbl60Subtribus.Valid,
                        ValidYear = CurrentTbl60Subtribus.ValidYear,
                        Synonym = CurrentTbl60Subtribus.Synonym,
                        Author = CurrentTbl60Subtribus.Author,
                        AuthorYear = CurrentTbl60Subtribus.AuthorYear,
                        Info = CurrentTbl60Subtribus.Info,
                        EngName = CurrentTbl60Subtribus.EngName,
                        GerName = CurrentTbl60Subtribus.GerName,
                        FraName = CurrentTbl60Subtribus.FraName,
                        PorName = CurrentTbl60Subtribus.PorName,
                        Writer = Environment.UserName,
                        WriterDate = DateTime.Now,
                        Updater = Environment.UserName,
                        UpdaterDate = DateTime.Now,
                        Memo = CurrentTbl60Subtribus.Memo,
                        EntityState = EntityState.Added
                    };
                }
                {
                    //TribusID may be not 0
                    if (CurrentTbl60Subtribus.TribusID == 0)          

                    {
                        WpfMessageBox.Show(CultRes.StringsRes.RequiredGenealogyConnect, CultRes.StringsRes.RequiredInput,
                            MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        return;
                    }

                    //check if dataset with Name and TribusId already exist       
                    var dataset = _businessLayer.ListTbl60SubtribussesBySubtribusNameAndTribusId(CurrentTbl60Subtribus.SubtribusName, CurrentTbl60Subtribus.TribusID);

                    if (dataset.Count != 0 && CurrentTbl60Subtribus.SubtribusID == 0)  //dataset exist
                    {
                        WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTbl60Subtribus.SubtribusName,
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        return;
                    }
                    if (dataset.Count == 0 && CurrentTbl60Subtribus.SubtribusID == 0 ||
                        dataset.Count != 0 && CurrentTbl60Subtribus.SubtribusID != 0 ||
                        dataset.Count == 0 && CurrentTbl60Subtribus.SubtribusID != 0) //new dataset and update
                    {
                        if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTbl60Subtribus.SubtribusName,
                                MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes)
                            return;
                        {
                                _businessLayer.UpdateSubtribus(subtribus);

                                    WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess,
                                        CurrentTbl60Subtribus.SubtribusID == 0
                                            ? CultRes.StringsRes.DatasetNew
                                            : CurrentTbl60Subtribus.SubtribusName,
                                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        }
                    }
                }
            }
            catch (DbEntityValidationException ex)
            {
                _entityException.EntityException(ex);
            }

            Tbl60SubtribussesList = new ObservableCollection<Tbl60Subtribus>(_businessLayer.ListTbl60SubtribussesByTribusId(CurrentTbl57Tribus.TribusID));            

            SelectedMainTabIndex = 1;
            SubtribussesView = CollectionViewSource.GetDefaultView(Tbl60SubtribussesList);
            SubtribussesView.Refresh();
        }
        #endregion "Public Commands"                  
                                                          


 //    Part 5    

                                                          
                      
 //    Part 6    

 
            

 //    Part 7    

 

 //    Part 8    

           
        #region "Public Commands Connect ==> Tbl90ReferenceAuthor"
        //-------------------------------------------------------------------------
        private RelayCommand _addReferenceAuthorCommand;

        public ICommand AddReferenceAuthorCommand => _addReferenceAuthorCommand ??
                                                    (_addReferenceAuthorCommand = new RelayCommand(delegate { AddReferenceAuthor(null); }));

        private RelayCommand _copyReferenceAuthorCommand;

        public ICommand CopyReferenceAuthorCommand => _copyReferenceAuthorCommand ??
                        (_copyReferenceAuthorCommand = new RelayCommand(delegate { CopyReferenceAuthor(null); }));

        private RelayCommand _deleteReferenceAuthorCommand;

        public ICommand DeleteReferenceAuthorCommand => _deleteReferenceAuthorCommand ??
                                               (_deleteReferenceAuthorCommand = new RelayCommand(delegate { DeleteReferenceAuthor(null); }));

        private RelayCommand _saveReferenceAuthorCommand;

        public ICommand SaveReferenceAuthorCommand => _saveReferenceAuthorCommand ??
                     (_saveReferenceAuthorCommand = new RelayCommand(delegate { SaveReferenceAuthor(null); }));
        //-------------------------------------------------------------------------                    
     
        public void AddReferenceAuthor(object o)
        {
            if (Tbl90ReferenceAuthorsList == null)
                Tbl90ReferenceAuthorsList = new ObservableCollection<Tbl90Reference>();

            Tbl90ReferenceAuthorsList.Insert(0, new Tbl90Reference   { Info = CultRes.StringsRes.DatasetNew });

            ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
            ReferenceAuthorsView.MoveCurrentToFirst();
         }
        //----------------------------------------------------------------------            
     
        public void CopyReferenceAuthor(object o)
        {
            if (CurrentTbl90ReferenceAuthor == null)
            {
                WpfMessageBox.Show(CultRes.StringsRes.DatasetNew,
                    CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                return;
            }

            var reference = _businessLayer.SingleListTbl90ReferencesByReferenceId(CurrentTbl90ReferenceAuthor.ReferenceID);

            Tbl90ReferenceAuthorsList.Insert(0, new Tbl90Reference
            {
                RefAuthorID = reference.RefAuthorID,
                Valid = reference.Valid,
                ValidYear = reference.ValidYear,
                Info = CultRes.StringsRes.DatasetNew,
                Memo = reference.Memo
            });

            ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
            ReferenceAuthorsView.MoveCurrentToFirst();
        }
        //----------------------------------------------------------------------            
     
        private void DeleteReferenceAuthor(object o)
        {
            if (CurrentTbl90ReferenceAuthor == null)
            {
                WpfMessageBox.Show(CultRes.StringsRes.DatasetNew,
                    CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                return;
            }

                 try
                {
                    var reference = _businessLayer.SingleListTbl90ReferencesByReferenceId(CurrentTbl90ReferenceAuthor.ReferenceID);
                    if (reference != null)
                    {
                        if (WpfMessageBox.Show(CultRes.StringsRes.DeleteQuestion1, CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl90ReferenceAuthor.Info,
                                MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes)
                            return;
                        reference.EntityState = EntityState.Deleted;
                        _businessLayer.RemoveReference(reference);

                        WpfMessageBox.Show(CultRes.StringsRes.DeleteSuccess, CurrentTbl90ReferenceAuthor.Info,
                            MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                    }
                    else
                    {
                        WpfMessageBox.Show(CultRes.StringsRes.Information, CultRes.StringsRes.DeleteCan + " " + CurrentTbl90ReferenceAuthor.Info + " " + CultRes.StringsRes.DeleteCan1,
                            MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                    }
                }
                catch (DbEntityValidationException ex)
                {
                    _entityException.EntityException(ex);
                }

            Tbl90ReferenceAuthorsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ReferenceListRefAuthorsByTribusId(CurrentTbl57Tribus.TribusID));

            ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
            ReferenceAuthorsView.Refresh();
        }

        //----------------------------------------------------------------------            
     
        public void SaveReferenceAuthor(object o)
        {
            if (CurrentTbl90ReferenceAuthor == null)
            {
                WpfMessageBox.Show(CultRes.StringsRes.DatasetNew,
                    CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                return;
            }

            CurrentTbl90ReferenceAuthor.TribusID = CurrentTbl57Tribus.TribusID;

            try
            {
                var reference = _businessLayer.SingleListTbl90ReferencesByReferenceId(CurrentTbl90ReferenceAuthor.ReferenceID);
                if (CurrentTbl90ReferenceAuthor.ReferenceID != 0)
                {
                    if (reference != null) //update
                    {
                        reference.RefExpertID = CurrentTbl90ReferenceAuthor.RefExpertID;
                        reference.RefAuthorID = CurrentTbl90ReferenceAuthor.RefAuthorID;
                        reference.RefSourceID = CurrentTbl90ReferenceAuthor.RefSourceID;
                        reference.RegnumID = CurrentTbl90ReferenceAuthor.RegnumID;
                        reference.PhylumID = CurrentTbl90ReferenceAuthor.PhylumID;
                        reference.DivisionID = CurrentTbl90ReferenceAuthor.DivisionID;
                        reference.SubphylumID = CurrentTbl90ReferenceAuthor.SubphylumID;
                        reference.SubdivisionID = CurrentTbl90ReferenceAuthor.SubdivisionID;
                        reference.SuperclassID = CurrentTbl90ReferenceAuthor.SuperclassID;
                        reference.ClassID = CurrentTbl90ReferenceAuthor.ClassID;
                        reference.SubclassID = CurrentTbl90ReferenceAuthor.SubclassID;
                        reference.InfraclassID = CurrentTbl90ReferenceAuthor.InfraclassID;
                        reference.LegioID = CurrentTbl90ReferenceAuthor.LegioID;
                        reference.OrdoID = CurrentTbl90ReferenceAuthor.OrdoID;
                        reference.SubordoID = CurrentTbl90ReferenceAuthor.SubordoID;
                        reference.InfraordoID = CurrentTbl90ReferenceAuthor.InfraordoID;
                        reference.SuperfamilyID = CurrentTbl90ReferenceAuthor.SuperfamilyID;
                        reference.FamilyID = CurrentTbl90ReferenceAuthor.FamilyID;
                        reference.SubfamilyID = CurrentTbl90ReferenceAuthor.SubfamilyID;
                        reference.InfrafamilyID = CurrentTbl90ReferenceAuthor.InfrafamilyID;
                        reference.SupertribusID = CurrentTbl90ReferenceAuthor.SupertribusID;
                        reference.TribusID = CurrentTbl90ReferenceAuthor.TribusID;
                        reference.SubtribusID = CurrentTbl90ReferenceAuthor.SubtribusID;
                        reference.InfratribusID = CurrentTbl90ReferenceAuthor.InfratribusID;
                        reference.GenusID = CurrentTbl90ReferenceAuthor.GenusID;
                        reference.PlSpeciesID = CurrentTbl90ReferenceAuthor.PlSpeciesID;
                        reference.FiSpeciesID = CurrentTbl90ReferenceAuthor.FiSpeciesID;
                        reference.Valid = CurrentTbl90ReferenceAuthor.Valid;
                        reference.ValidYear = CurrentTbl90ReferenceAuthor.ValidYear;
                        reference.Info = CurrentTbl90ReferenceAuthor.Info;
                        reference.Updater = Environment.UserName;
                        reference.UpdaterDate = DateTime.Now;
                        reference.Memo = CurrentTbl90ReferenceAuthor.Memo;

                        reference.EntityState = EntityState.Modified;
                    }
                }
                else
                {
                    reference = new Tbl90Reference     //add new
                    {
                        RefAuthorID = CurrentTbl90ReferenceAuthor.RefAuthorID,
                        RefSourceID = CurrentTbl90ReferenceAuthor.RefSourceID,
                        RefExpertID = CurrentTbl90ReferenceAuthor.RefExpertID,
                        RegnumID = CurrentTbl90ReferenceAuthor.RegnumID,
                        PhylumID = CurrentTbl90ReferenceAuthor.PhylumID,
                        DivisionID = CurrentTbl90ReferenceAuthor.DivisionID,
                        SubphylumID = CurrentTbl90ReferenceAuthor.SubphylumID,
                        SubdivisionID = CurrentTbl90ReferenceAuthor.SubdivisionID,
                        SuperclassID = CurrentTbl90ReferenceAuthor.SuperclassID,
                        ClassID = CurrentTbl90ReferenceAuthor.ClassID,
                        SubclassID = CurrentTbl90ReferenceAuthor.SubclassID,
                        InfraclassID = CurrentTbl90ReferenceAuthor.InfraclassID,
                        LegioID = CurrentTbl90ReferenceAuthor.LegioID,
                        OrdoID = CurrentTbl90ReferenceAuthor.OrdoID,
                        SubordoID = CurrentTbl90ReferenceAuthor.SubordoID,
                        InfraordoID = CurrentTbl90ReferenceAuthor.InfraordoID,
                        SuperfamilyID = CurrentTbl90ReferenceAuthor.SuperfamilyID,
                        FamilyID = CurrentTbl90ReferenceAuthor.FamilyID,
                        SubfamilyID = CurrentTbl90ReferenceAuthor.SubfamilyID,
                        InfrafamilyID = CurrentTbl90ReferenceAuthor.InfrafamilyID,
                        SupertribusID = CurrentTbl90ReferenceAuthor.SupertribusID,
                        TribusID = CurrentTbl90ReferenceAuthor.TribusID,
                        SubtribusID = CurrentTbl90ReferenceAuthor.SubtribusID,
                        InfratribusID = CurrentTbl90ReferenceAuthor.InfratribusID,
                        GenusID = CurrentTbl90ReferenceAuthor.GenusID,
                        PlSpeciesID = CurrentTbl90ReferenceAuthor.PlSpeciesID,
                        FiSpeciesID = CurrentTbl90ReferenceAuthor.FiSpeciesID,
                        CountID = RandomHelper.Randomnumber(),
                        Valid = CurrentTbl90ReferenceAuthor.Valid,
                        ValidYear = CurrentTbl90ReferenceAuthor.ValidYear,
                        Info = CurrentTbl90ReferenceAuthor.Info,
                        Memo = CurrentTbl90ReferenceAuthor.Memo,
                        Writer = Environment.UserName,
                        WriterDate = DateTime.Now,
                        Updater = Environment.UserName,
                        UpdaterDate = DateTime.Now,
                        EntityState = EntityState.Added
                    };
                }
                {
                    //RefExpertID or RefSourceID or RefAuthorID may be not 0
                        if (CurrentTbl90ReferenceAuthor.RefExpertID == null &&
                            CurrentTbl90ReferenceAuthor.RefSourceID == null &&
                            CurrentTbl90ReferenceAuthor.RefAuthorID == null)
                    {
                        WpfMessageBox.Show(CultRes.StringsRes.RequiredGenealogyConnect, CultRes.StringsRes.RequiredInput,
                            MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        return;
                    }

                    //check if dataset with vb-name already exist   
                    var dataset = _businessLayer.ListTbl90ReferencesByRefExpertIdAndRefSourceIdAndRefAuthorIdAndInfo(CurrentTbl90ReferenceAuthor);

                    if (dataset.Count != 0 && CurrentTbl90ReferenceAuthor.ReferenceID == 0)  //dataset exist
                    {
                        WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTbl90ReferenceAuthor.ReferenceID.ToString(),
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        return;
                    }
                    if (dataset.Count == 0 && CurrentTbl90ReferenceAuthor.ReferenceID == 0 ||
                        dataset.Count != 0 && CurrentTbl90ReferenceAuthor.ReferenceID != 0 ||
                        dataset.Count == 0 && CurrentTbl90ReferenceAuthor.ReferenceID != 0) //new dataset and update
                    {
                        if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTbl90ReferenceAuthor.Info,
                                MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes)
                            return;
                        {
                                _businessLayer.UpdateReference(reference);

                                    WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess,
                                        CurrentTbl90ReferenceAuthor.ReferenceID == 0
                                            ? CultRes.StringsRes.DatasetNew
                                            : CurrentTbl90ReferenceAuthor.ReferenceID.ToString(),
                                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        }
                    }
                }
            }
            catch (DbEntityValidationException ex)
            {
                _entityException.EntityException(ex);
            }

            Tbl90ReferenceAuthorsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ReferenceListRefAuthorsByTribusId(CurrentTbl57Tribus.TribusID));           
     

            SelectedMainSubRefTabIndex = 2;

            ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
            ReferenceAuthorsView.Refresh();
        }
        #endregion "Public Commands"                  
           
        #region "Public Commands Connect ==> Tbl90ReferenceSource" 
        //-------------------------------------------------------------------------
        private RelayCommand _addReferenceSourceCommand;

        public ICommand AddReferenceSourceCommand => _addReferenceSourceCommand ??
                                                    (_addReferenceSourceCommand = new RelayCommand(delegate { AddReferenceSource(null); }));

        private RelayCommand _copyReferenceSourceCommand;

        public ICommand CopyReferenceSourceCommand => _copyReferenceSourceCommand ??
                        (_copyReferenceSourceCommand = new RelayCommand(delegate { CopyReferenceSource(null); }));

        private RelayCommand _deleteReferenceSourceCommand;

        public ICommand DeleteReferenceSourceCommand => _deleteReferenceSourceCommand ??
                                                        (_deleteReferenceSourceCommand = new RelayCommand(delegate { DeleteReferenceSource(null); }));

        private RelayCommand _saveReferenceSourceCommand;

        public ICommand SaveReferenceSourceCommand => _saveReferenceSourceCommand ??
                     (_saveReferenceSourceCommand = new RelayCommand(delegate { SaveReferenceSource(null); }));

        //-------------------------------------------------------------------------          
     
        public void AddReferenceSource(object o)
        {
            if (Tbl90ReferenceSourcesList == null)
                Tbl90ReferenceSourcesList = new ObservableCollection<Tbl90Reference>();

            Tbl90ReferenceSourcesList .Insert(0, new Tbl90Reference  { Info = CultRes.StringsRes.DatasetNew });

            ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
            ReferenceSourcesView.MoveCurrentToFirst();
         }
        //----------------------------------------------------------------------            
     
        public void CopyReferenceSource(object o)
        {
            if (CurrentTbl90ReferenceSource == null)
            {
                WpfMessageBox.Show(CultRes.StringsRes.DatasetNew,
                    CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                return;
            }

            var reference = _businessLayer.SingleListTbl90ReferencesByReferenceId(CurrentTbl90ReferenceSource.ReferenceID);

            Tbl90ReferenceSourcesList.Insert(0, new Tbl90Reference
            {
                RefSourceID = reference.RefSourceID,
                Valid = reference.Valid,
                ValidYear = reference.ValidYear,
                Info = CultRes.StringsRes.DatasetNew,
                Memo = reference.Memo
            });

            ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
            ReferenceSourcesView.MoveCurrentToFirst();
        }
        //----------------------------------------------------------------------            
     
        private void DeleteReferenceSource(object o)
        {
            if (CurrentTbl90ReferenceSource == null)
            {
                WpfMessageBox.Show(CultRes.StringsRes.DatasetNew,
                    CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                return;
            }

            try
            {
                var reference = _businessLayer.SingleListTbl90ReferencesByReferenceId(CurrentTbl90ReferenceSource.ReferenceID);
                if (reference != null)
                {
                    if (WpfMessageBox.Show(CultRes.StringsRes.DeleteQuestion1, CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl90ReferenceSource.Info,
                            MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes)
                        return;
                    reference.EntityState = EntityState.Deleted;
                    _businessLayer.RemoveReference(reference);

                    WpfMessageBox.Show(CultRes.StringsRes.DeleteSuccess, CurrentTbl90ReferenceSource.Info,
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                }
                else
                {
                    WpfMessageBox.Show(CultRes.StringsRes.Information, CultRes.StringsRes.DeleteCan + " " + CurrentTbl90ReferenceSource.Info + " " + CultRes.StringsRes.DeleteCan1,
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                }
            }
            catch (DbEntityValidationException ex)
            {
                _entityException.EntityException(ex);
            }

            Tbl90ReferenceSourcesList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ReferenceListRefSourcesByTribusId(CurrentTbl57Tribus.TribusID));

            ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
            ReferenceSourcesView.Refresh();
        }        
        //----------------------------------------------------------------------            
     
        public void SaveReferenceSource(object o)
        {
            if (CurrentTbl90ReferenceSource == null)
            {
                WpfMessageBox.Show(CultRes.StringsRes.DatasetNew,
                    CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                return;
            }

            CurrentTbl90ReferenceSource.TribusID = CurrentTbl57Tribus.TribusID;

            try
            {
                var reference = _businessLayer.SingleListTbl90ReferencesByReferenceId(CurrentTbl90ReferenceSource.ReferenceID);
                if (CurrentTbl90ReferenceSource.ReferenceID != 0)
                {
                    if (reference != null) //update
                    {
                        reference.RefExpertID = CurrentTbl90ReferenceSource.RefExpertID;
                        reference.RefAuthorID = CurrentTbl90ReferenceSource.RefAuthorID;
                        reference.RefSourceID = CurrentTbl90ReferenceSource.RefSourceID;
                        reference.RegnumID = CurrentTbl90ReferenceSource.RegnumID;
                        reference.PhylumID = CurrentTbl90ReferenceSource.PhylumID;
                        reference.DivisionID = CurrentTbl90ReferenceSource.DivisionID;
                        reference.SubphylumID = CurrentTbl90ReferenceSource.SubphylumID;
                        reference.SubdivisionID = CurrentTbl90ReferenceSource.SubdivisionID;
                        reference.SuperclassID = CurrentTbl90ReferenceSource.SuperclassID;
                        reference.ClassID = CurrentTbl90ReferenceSource.ClassID;
                        reference.SubclassID = CurrentTbl90ReferenceSource.SubclassID;
                        reference.InfraclassID = CurrentTbl90ReferenceSource.InfraclassID;
                        reference.LegioID = CurrentTbl90ReferenceSource.LegioID;
                        reference.OrdoID = CurrentTbl90ReferenceSource.OrdoID;
                        reference.SubordoID = CurrentTbl90ReferenceSource.SubordoID;
                        reference.InfraordoID = CurrentTbl90ReferenceSource.InfraordoID;
                        reference.SuperfamilyID = CurrentTbl90ReferenceSource.SuperfamilyID;
                        reference.FamilyID = CurrentTbl90ReferenceSource.FamilyID;
                        reference.SubfamilyID = CurrentTbl90ReferenceSource.SubfamilyID;
                        reference.InfrafamilyID = CurrentTbl90ReferenceSource.InfrafamilyID;
                        reference.SupertribusID = CurrentTbl90ReferenceSource.SupertribusID;
                        reference.TribusID = CurrentTbl90ReferenceSource.TribusID;
                        reference.SubtribusID = CurrentTbl90ReferenceSource.SubtribusID;
                        reference.InfratribusID = CurrentTbl90ReferenceSource.InfratribusID;
                        reference.GenusID = CurrentTbl90ReferenceSource.GenusID;
                        reference.PlSpeciesID = CurrentTbl90ReferenceSource.PlSpeciesID;
                        reference.FiSpeciesID = CurrentTbl90ReferenceSource.FiSpeciesID;
                        reference.Valid = CurrentTbl90ReferenceSource.Valid;
                        reference.ValidYear = CurrentTbl90ReferenceSource.ValidYear;
                        reference.Info = CurrentTbl90ReferenceSource.Info;
                        reference.Updater = Environment.UserName;
                        reference.UpdaterDate = DateTime.Now;
                        reference.Memo = CurrentTbl90ReferenceSource.Memo;

                        reference.EntityState = EntityState.Modified;
                    }
                }
                else
                {
                    reference = new Tbl90Reference     //add new
                    {
                        RefAuthorID = CurrentTbl90ReferenceSource.RefAuthorID,
                        RefSourceID = CurrentTbl90ReferenceSource.RefSourceID,
                        RefExpertID = CurrentTbl90ReferenceSource.RefExpertID,
                        RegnumID = CurrentTbl90ReferenceSource.RegnumID,
                        PhylumID = CurrentTbl90ReferenceSource.PhylumID,
                        DivisionID = CurrentTbl90ReferenceSource.DivisionID,
                        SubphylumID = CurrentTbl90ReferenceSource.SubphylumID,
                        SubdivisionID = CurrentTbl90ReferenceSource.SubdivisionID,
                        SuperclassID = CurrentTbl90ReferenceSource.SuperclassID,
                        ClassID = CurrentTbl90ReferenceSource.ClassID,
                        SubclassID = CurrentTbl90ReferenceSource.SubclassID,
                        InfraclassID = CurrentTbl90ReferenceSource.InfraclassID,
                        LegioID = CurrentTbl90ReferenceSource.LegioID,
                        OrdoID = CurrentTbl90ReferenceSource.OrdoID,
                        SubordoID = CurrentTbl90ReferenceSource.SubordoID,
                        InfraordoID = CurrentTbl90ReferenceSource.InfraordoID,
                        SuperfamilyID = CurrentTbl90ReferenceSource.SuperfamilyID,
                        FamilyID = CurrentTbl90ReferenceSource.FamilyID,
                        SubfamilyID = CurrentTbl90ReferenceSource.SubfamilyID,
                        InfrafamilyID = CurrentTbl90ReferenceSource.InfrafamilyID,
                        SupertribusID = CurrentTbl90ReferenceSource.SupertribusID,
                        TribusID = CurrentTbl90ReferenceSource.TribusID,
                        SubtribusID = CurrentTbl90ReferenceSource.SubtribusID,
                        InfratribusID = CurrentTbl90ReferenceSource.InfratribusID,
                        GenusID = CurrentTbl90ReferenceSource.GenusID,
                        PlSpeciesID = CurrentTbl90ReferenceSource.PlSpeciesID,
                        FiSpeciesID = CurrentTbl90ReferenceSource.FiSpeciesID,
                        CountID = RandomHelper.Randomnumber(),
                        Valid = CurrentTbl90ReferenceSource.Valid,
                        ValidYear = CurrentTbl90ReferenceSource.ValidYear,
                        Info = CurrentTbl90ReferenceSource.Info,
                        Memo = CurrentTbl90ReferenceSource.Memo,
                        Writer = Environment.UserName,
                        WriterDate = DateTime.Now,
                        Updater = Environment.UserName,
                        UpdaterDate = DateTime.Now,
                        EntityState = EntityState.Added
                    };
                }
                {
                    //RefExpertID or RefSourceID or RefAuthorID may be not 0
                    if (CurrentTbl90ReferenceSource.RefExpertID == null && CurrentTbl90ReferenceSource.RefSourceID == null && CurrentTbl90ReferenceSource.RefAuthorID == null)
                    {
                        WpfMessageBox.Show(CultRes.StringsRes.RequiredGenealogyConnect, CultRes.StringsRes.RequiredInput,
                            MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        return;
                    }

                    //check if dataset with vb-name already exist   
                    var dataset = _businessLayer.ListTbl90ReferencesByRefExpertIdAndRefSourceIdAndRefAuthorIdAndInfo(CurrentTbl90ReferenceSource);

                    if (dataset.Count != 0 && CurrentTbl90ReferenceSource.ReferenceID == 0)  //dataset exist
                    {
                        WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTbl90ReferenceSource.ReferenceID.ToString(),
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        return;
                    }
                    if (dataset.Count == 0 && CurrentTbl90ReferenceSource.ReferenceID == 0 ||
                        dataset.Count != 0 && CurrentTbl90ReferenceSource.ReferenceID != 0 ||
                        dataset.Count == 0 && CurrentTbl90ReferenceSource.ReferenceID != 0) //new dataset and update
                    {
                        if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTbl90ReferenceSource.Info,
                                MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes)
                            return;
                        {
                                _businessLayer.UpdateReference(reference);

                                    WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess,
                                        CurrentTbl90ReferenceSource.ReferenceID == 0
                                            ? CultRes.StringsRes.DatasetNew
                                             : CurrentTbl90ReferenceSource.ReferenceID.ToString(),
                                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        }
                    }
                }
            }
            catch (DbEntityValidationException ex)
            {
                _entityException.EntityException(ex);
            }

            Tbl90ReferenceSourcesList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ReferenceListRefSourcesByTribusId(CurrentTbl57Tribus.TribusID));           
     
            SelectedMainSubRefTabIndex = 1;

            ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
            ReferenceSourcesView.Refresh();
        }
        #endregion "Public Commands"                  
           
        #region "Public Commands Connect ==> Tbl90ReferenceExpert"
        //-------------------------------------------------------------------------
 
        private RelayCommand _addReferenceExpertCommand;

        public ICommand AddReferenceExpertCommand => _addReferenceExpertCommand ??
                                                    (_addReferenceExpertCommand = new RelayCommand(delegate { AddReferenceExpert(null); }));

        private RelayCommand _copyReferenceExpertCommand;

        public ICommand CopyReferenceExpertCommand => _copyReferenceExpertCommand ??
                        (_copyReferenceExpertCommand = new RelayCommand(delegate { CopyReferenceExpert(null); }));

        private RelayCommand _deleteReferenceExpertCommand;

        public ICommand DeleteReferenceExpertCommand => _deleteReferenceExpertCommand ??
                                                        (_deleteReferenceExpertCommand = new RelayCommand(delegate { DeleteReferenceExpert(null); }));
        private RelayCommand _saveReferenceExpertCommand;

        public ICommand SaveReferenceExpertCommand => _saveReferenceExpertCommand ??
                     (_saveReferenceExpertCommand = new RelayCommand(delegate { SaveReferenceExpert(null); }));
        //-------------------------------------------------------------------------          
     
        public void AddReferenceExpert(object o)
        {
            if (Tbl90ReferenceExpertsList == null)
                Tbl90ReferenceExpertsList = new ObservableCollection<Tbl90Reference>();

            Tbl90ReferenceExpertsList .Insert(0, new Tbl90Reference   { Info = CultRes.StringsRes.DatasetNew });

            ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
            ReferenceExpertsView.MoveCurrentToFirst();
         }
        //----------------------------------------------------------------------            
     
        public void CopyReferenceExpert(object o)
        {
            if (CurrentTbl90ReferenceExpert == null)
            {
                WpfMessageBox.Show(CultRes.StringsRes.DatasetNew,
                    CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                return;
            }

            var reference = _businessLayer.SingleListTbl90ReferencesByReferenceId(CurrentTbl90ReferenceExpert.ReferenceID);

            Tbl90ReferenceExpertsList.Insert(0, new Tbl90Reference
            {
                RefExpertID = reference.RefExpertID,
                Valid = reference.Valid,
                ValidYear = reference.ValidYear,
                Info = CultRes.StringsRes.DatasetNew,
                Memo = reference.Memo
            });

            ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
            ReferenceExpertsView.MoveCurrentToFirst();
        }
        //----------------------------------------------------------------------            
     
        private void DeleteReferenceExpert(object o)
        {
            if (CurrentTbl90ReferenceExpert == null)
            {
                WpfMessageBox.Show(CultRes.StringsRes.DatasetNew,
                    CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                return;
            }

            try
            {
                var reference = _businessLayer.SingleListTbl90ReferencesByReferenceId(CurrentTbl90ReferenceExpert.ReferenceID);
                if (reference != null)
                {
                    if (WpfMessageBox.Show(CultRes.StringsRes.DeleteQuestion1, CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl90ReferenceExpert.Info,
                            MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes)
                        return;
                    reference.EntityState = EntityState.Deleted;
                    _businessLayer.RemoveReference(reference);

                    WpfMessageBox.Show(CultRes.StringsRes.DeleteSuccess, CurrentTbl90ReferenceExpert.Info,
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                }
                else
                {
                    WpfMessageBox.Show(CultRes.StringsRes.Information, CultRes.StringsRes.DeleteCan + " " + CurrentTbl90ReferenceExpert.Info + " " + CultRes.StringsRes.DeleteCan1,
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                }
            }
            catch (DbEntityValidationException ex)
            {
                _entityException.EntityException(ex);
            }

            Tbl90ReferenceExpertsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ReferenceListRefExpertsByTribusId(CurrentTbl57Tribus.TribusID));

            ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
            ReferenceExpertsView.Refresh();
        }
        //----------------------------------------------------------------------            
     
        public void SaveReferenceExpert(object o)
        {
            if (CurrentTbl90ReferenceExpert == null)
            {
                WpfMessageBox.Show(CultRes.StringsRes.DatasetNew,
                    CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                return;
            }

            CurrentTbl90ReferenceExpert.TribusID = CurrentTbl57Tribus.TribusID;

            try
            {
                var reference = _businessLayer.SingleListTbl90ReferencesByReferenceId(CurrentTbl90ReferenceExpert.ReferenceID);
                if (CurrentTbl90ReferenceExpert.ReferenceID != 0)
                {
                    if (reference != null) //update
                    {
                        reference.RefExpertID = CurrentTbl90ReferenceExpert.RefExpertID;
                        reference.RefAuthorID = CurrentTbl90ReferenceExpert.RefAuthorID;
                        reference.RefSourceID = CurrentTbl90ReferenceExpert.RefSourceID;
                        reference.RegnumID = CurrentTbl90ReferenceExpert.RegnumID;
                        reference.PhylumID = CurrentTbl90ReferenceExpert.PhylumID;
                        reference.DivisionID = CurrentTbl90ReferenceExpert.DivisionID;
                        reference.SubphylumID = CurrentTbl90ReferenceExpert.SubphylumID;
                        reference.SubdivisionID = CurrentTbl90ReferenceExpert.SubdivisionID;
                        reference.SuperclassID = CurrentTbl90ReferenceExpert.SuperclassID;
                        reference.ClassID = CurrentTbl90ReferenceExpert.ClassID;
                        reference.SubclassID = CurrentTbl90ReferenceExpert.SubclassID;
                        reference.InfraclassID = CurrentTbl90ReferenceExpert.InfraclassID;
                        reference.LegioID = CurrentTbl90ReferenceExpert.LegioID;
                        reference.OrdoID = CurrentTbl90ReferenceExpert.OrdoID;
                        reference.SubordoID = CurrentTbl90ReferenceExpert.SubordoID;
                        reference.InfraordoID = CurrentTbl90ReferenceExpert.InfraordoID;
                        reference.SuperfamilyID = CurrentTbl90ReferenceExpert.SuperfamilyID;
                        reference.FamilyID = CurrentTbl90ReferenceExpert.FamilyID;
                        reference.SubfamilyID = CurrentTbl90ReferenceExpert.SubfamilyID;
                        reference.InfrafamilyID = CurrentTbl90ReferenceExpert.InfrafamilyID;
                        reference.SupertribusID = CurrentTbl90ReferenceExpert.SupertribusID;
                        reference.TribusID = CurrentTbl90ReferenceExpert.TribusID;
                        reference.SubtribusID = CurrentTbl90ReferenceExpert.SubtribusID;
                        reference.InfratribusID = CurrentTbl90ReferenceExpert.InfratribusID;
                        reference.GenusID = CurrentTbl90ReferenceExpert.GenusID;
                        reference.PlSpeciesID = CurrentTbl90ReferenceExpert.PlSpeciesID;
                        reference.FiSpeciesID = CurrentTbl90ReferenceExpert.FiSpeciesID;
                        reference.Valid = CurrentTbl90ReferenceExpert.Valid;
                        reference.ValidYear = CurrentTbl90ReferenceExpert.ValidYear;
                        reference.Info = CurrentTbl90ReferenceExpert.Info;
                        reference.Updater = Environment.UserName;
                        reference.UpdaterDate = DateTime.Now;
                        reference.Memo = CurrentTbl90ReferenceExpert.Memo;

                        reference.EntityState = EntityState.Modified;
                    }
                }
                else
                {
                    reference = new Tbl90Reference     //add new
                    {
                        RefAuthorID = CurrentTbl90ReferenceExpert.RefAuthorID,
                        RefSourceID = CurrentTbl90ReferenceExpert.RefSourceID,
                        RefExpertID = CurrentTbl90ReferenceExpert.RefExpertID,
                        RegnumID = CurrentTbl90ReferenceExpert.RegnumID,
                        PhylumID = CurrentTbl90ReferenceExpert.PhylumID,
                        DivisionID = CurrentTbl90ReferenceExpert.DivisionID,
                        SubphylumID = CurrentTbl90ReferenceExpert.SubphylumID,
                        SubdivisionID = CurrentTbl90ReferenceExpert.SubdivisionID,
                        SuperclassID = CurrentTbl90ReferenceExpert.SuperclassID,
                        ClassID = CurrentTbl90ReferenceExpert.ClassID,
                        SubclassID = CurrentTbl90ReferenceExpert.SubclassID,
                        InfraclassID = CurrentTbl90ReferenceExpert.InfraclassID,
                        LegioID = CurrentTbl90ReferenceExpert.LegioID,
                        OrdoID = CurrentTbl90ReferenceExpert.OrdoID,
                        SubordoID = CurrentTbl90ReferenceExpert.SubordoID,
                        InfraordoID = CurrentTbl90ReferenceExpert.InfraordoID,
                        SuperfamilyID = CurrentTbl90ReferenceExpert.SuperfamilyID,
                        FamilyID = CurrentTbl90ReferenceExpert.FamilyID,
                        SubfamilyID = CurrentTbl90ReferenceExpert.SubfamilyID,
                        InfrafamilyID = CurrentTbl90ReferenceExpert.InfrafamilyID,
                        SupertribusID = CurrentTbl90ReferenceExpert.SupertribusID,
                        TribusID = CurrentTbl90ReferenceExpert.TribusID,
                        SubtribusID = CurrentTbl90ReferenceExpert.SubtribusID,
                        InfratribusID = CurrentTbl90ReferenceExpert.InfratribusID,
                        GenusID = CurrentTbl90ReferenceExpert.GenusID,
                        PlSpeciesID = CurrentTbl90ReferenceExpert.PlSpeciesID,
                        FiSpeciesID = CurrentTbl90ReferenceExpert.FiSpeciesID,
                        CountID = RandomHelper.Randomnumber(),
                        Valid = CurrentTbl90ReferenceExpert.Valid,
                        ValidYear = CurrentTbl90ReferenceExpert.ValidYear,
                        Info = CurrentTbl90ReferenceExpert.Info,
                        Memo = CurrentTbl90ReferenceExpert.Memo,
                        Writer = Environment.UserName,
                        WriterDate = DateTime.Now,
                        Updater = Environment.UserName,
                        UpdaterDate = DateTime.Now,
                        EntityState = EntityState.Added
                    };
                }
                {
                    //RefExpertID or RefSourceID or RefAuthorID may be not 0
                    if (CurrentTbl90ReferenceExpert.RefExpertID == null && CurrentTbl90ReferenceExpert.RefSourceID == null && CurrentTbl90ReferenceExpert.RefAuthorID == null)
                    {
                        WpfMessageBox.Show(CultRes.StringsRes.RequiredGenealogyConnect, CultRes.StringsRes.RequiredInput,
                            MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        return;
                    }

                    //check if dataset with vb-name already exist   
                    var dataset = _businessLayer.ListTbl90ReferencesByRefExpertIdAndRefSourceIdAndRefAuthorIdAndInfo(CurrentTbl90ReferenceExpert);

                    if (dataset.Count != 0 && CurrentTbl90ReferenceExpert.ReferenceID == 0)  //dataset exist
                    {
                        WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTbl90ReferenceExpert.ReferenceID.ToString(),
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        return;
                    }
                    if (dataset.Count == 0 && CurrentTbl90ReferenceExpert.ReferenceID == 0 ||
                        dataset.Count != 0 && CurrentTbl90ReferenceExpert.ReferenceID != 0 ||
                        dataset.Count == 0 && CurrentTbl90ReferenceExpert.ReferenceID != 0) //new dataset and update
                    {
                        if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTbl90ReferenceExpert.Info,
                                MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes)
                            return;
                        {
                                _businessLayer.UpdateReference(reference);

                                    WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess,
                                        CurrentTbl90ReferenceExpert.ReferenceID == 0
                                            ? CultRes.StringsRes.DatasetNew
                                             : CurrentTbl90ReferenceExpert.ReferenceID.ToString(),
                                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        }
                    }
                }
            }
            catch (DbEntityValidationException ex)
            {
                _entityException.EntityException(ex);
            }

            Tbl90ReferenceExpertsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ReferenceListRefExpertsByTribusId(CurrentTbl57Tribus.TribusID));     
     
            SelectedMainSubRefTabIndex = 0;

            ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
            ReferenceExpertsView.Refresh();
        }
        #endregion "Public Commands"                  
           
        #region "Public Commands Connect ==> Tbl93Comment"

        //-------------------------------------------------------------------------
        private RelayCommand _addCommentCommand;

        public ICommand AddCommentCommand => _addCommentCommand ??
                                                 (_addCommentCommand = new RelayCommand(delegate { AddComment(null); }));

        private RelayCommand _copyCommentCommand;

        public ICommand CopyCommentCommand => _copyCommentCommand ??
                                                  (_copyCommentCommand = new RelayCommand(delegate { CopyComment(null); }));

        private RelayCommand _deleteCommentCommand;

        public ICommand DeleteCommentCommand => _deleteCommentCommand ??
                                                        (_deleteCommentCommand = new RelayCommand(delegate { DeleteComment(null); }));

        private RelayCommand _saveCommentCommand;

        public ICommand SaveCommentCommand => _saveCommentCommand ??
                                                  (_saveCommentCommand = new RelayCommand(delegate { SaveComment(null); }));
        //-------------------------------------------------------------------------          
     
        public void AddComment(object o)
        {
            if (Tbl93CommentsList == null)
                Tbl93CommentsList = new ObservableCollection<Tbl93Comment>();

            Tbl93CommentsList .Insert(0, new Tbl93Comment  { Info = CultRes.StringsRes.DatasetNew });

            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            CommentsView.MoveCurrentToFirst();
         }
        //----------------------------------------------------------------------            
     
        public void CopyComment(object o)
        {
            if (CurrentTbl93Comment == null)
            {
                WpfMessageBox.Show(CultRes.StringsRes.DatasetNew,
                    CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                return;
            }

            var comment = _businessLayer.SingleListTbl93CommentsByCommentId(CurrentTbl93Comment.CommentID);

            Tbl93CommentsList.Insert(0, new Tbl93Comment
            {
                Valid = comment.Valid,
                ValidYear = comment.ValidYear,
                Info = CultRes.StringsRes.DatasetNew,
                Memo = comment.Memo
            });

            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            CommentsView.MoveCurrentToFirst();
        }
        //----------------------------------------------------------------------            
     
        private void DeleteComment(object o)
        {
            if (CurrentTbl93Comment == null)
            {
                WpfMessageBox.Show(CultRes.StringsRes.DatasetNew,
                    CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                return;
            }

            try
            {
                var comment = _businessLayer.SingleListTbl93CommentsByCommentId(CurrentTbl93Comment.CommentID);
                if (comment != null)
                {
                    if (WpfMessageBox.Show(CultRes.StringsRes.DeleteQuestion1, CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl93Comment.Info,
                            MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes)
                        return;
                    comment.EntityState = EntityState.Deleted;
                    _businessLayer.RemoveComment(comment);

                    WpfMessageBox.Show(CultRes.StringsRes.DeleteSuccess, CurrentTbl93Comment.Info,
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                }
                else
                {
                    WpfMessageBox.Show(CultRes.StringsRes.Information, CultRes.StringsRes.DeleteCan + " " + CurrentTbl93Comment.Info + " " + CultRes.StringsRes.DeleteCan1,
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                }
            }
            catch (DbEntityValidationException ex)
            {
                _entityException.EntityException(ex);
            }

            Tbl93CommentsList = new ObservableCollection<Tbl93Comment>(_businessLayer.ListTbl93CommentsByTribusId(CurrentTbl57Tribus.TribusID));

            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            CommentsView.Refresh();
        }
        //----------------------------------------------------------------------            
     
        private void SaveComment(object o)
        {
            if (CurrentTbl93Comment == null)
            {
                WpfMessageBox.Show(CultRes.StringsRes.DatasetNew,
                    CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                return;
            }

            CurrentTbl93Comment.TribusID = CurrentTbl57Tribus.TribusID;

            try
            {
                var comment = _businessLayer.SingleListTbl93CommentsByCommentId(CurrentTbl93Comment.CommentID);
                if (CurrentTbl93Comment.CommentID != 0)
                {
                    if (comment != null) //update
                    {
                        comment.RegnumID = CurrentTbl93Comment.RegnumID;
                        comment.PhylumID = CurrentTbl93Comment.PhylumID;
                        comment.DivisionID = CurrentTbl93Comment.DivisionID;
                        comment.SubphylumID = CurrentTbl93Comment.SubphylumID;
                        comment.SubdivisionID = CurrentTbl93Comment.SubdivisionID;
                        comment.SuperclassID = CurrentTbl93Comment.SuperclassID;
                        comment.ClassID = CurrentTbl93Comment.ClassID;
                        comment.SubclassID = CurrentTbl93Comment.SubclassID;
                        comment.InfraclassID = CurrentTbl93Comment.InfraclassID;
                        comment.LegioID = CurrentTbl93Comment.LegioID;
                        comment.OrdoID = CurrentTbl93Comment.OrdoID;
                        comment.SubordoID = CurrentTbl93Comment.SubordoID;
                        comment.InfraordoID = CurrentTbl93Comment.InfraordoID;
                        comment.SuperfamilyID = CurrentTbl93Comment.SuperfamilyID;
                        comment.FamilyID = CurrentTbl93Comment.FamilyID;
                        comment.SubfamilyID = CurrentTbl93Comment.SubfamilyID;
                        comment.InfrafamilyID = CurrentTbl93Comment.InfrafamilyID;
                        comment.SupertribusID = CurrentTbl93Comment.SupertribusID;
                        comment.TribusID = CurrentTbl93Comment.TribusID;
                        comment.SubtribusID = CurrentTbl93Comment.SubtribusID;
                        comment.InfratribusID = CurrentTbl93Comment.InfratribusID;
                        comment.GenusID = CurrentTbl93Comment.GenusID;
                        comment.PlSpeciesID = CurrentTbl93Comment.PlSpeciesID;
                        comment.FiSpeciesID = CurrentTbl93Comment.FiSpeciesID;
                        comment.Valid = CurrentTbl93Comment.Valid;
                        comment.ValidYear = CurrentTbl93Comment.ValidYear;
                        comment.Info = CurrentTbl93Comment.Info;
                        comment.Memo = CurrentTbl93Comment.Memo;
                        comment.Updater = Environment.UserName;
                        comment.UpdaterDate = DateTime.Now;
                        comment.EntityState = EntityState.Modified;
                    }
                }
                else
                {
                    comment = new Tbl93Comment     //add new
                    {
                        RegnumID = CurrentTbl93Comment.RegnumID,
                        PhylumID = CurrentTbl93Comment.PhylumID,
                        DivisionID = CurrentTbl93Comment.DivisionID,
                        SubphylumID = CurrentTbl93Comment.SubphylumID,
                        SubdivisionID = CurrentTbl93Comment.SubdivisionID,
                        SuperclassID = CurrentTbl93Comment.SuperclassID,
                        ClassID = CurrentTbl93Comment.ClassID,
                        SubclassID = CurrentTbl93Comment.SubclassID,
                        InfraclassID = CurrentTbl93Comment.InfraclassID,
                        LegioID = CurrentTbl93Comment.LegioID,
                        OrdoID = CurrentTbl93Comment.OrdoID,
                        SubordoID = CurrentTbl93Comment.SubordoID,
                        InfraordoID = CurrentTbl93Comment.InfraordoID,
                        SuperfamilyID = CurrentTbl93Comment.SuperfamilyID,
                        FamilyID = CurrentTbl93Comment.FamilyID,
                        SubfamilyID = CurrentTbl93Comment.SubfamilyID,
                        InfrafamilyID = CurrentTbl93Comment.InfrafamilyID,
                        SupertribusID = CurrentTbl93Comment.SupertribusID,
                        TribusID = CurrentTbl93Comment.TribusID,
                        SubtribusID = CurrentTbl93Comment.SubtribusID,
                        InfratribusID = CurrentTbl93Comment.InfratribusID,
                        GenusID = CurrentTbl93Comment.GenusID,
                        PlSpeciesID = CurrentTbl93Comment.PlSpeciesID,
                        FiSpeciesID = CurrentTbl93Comment.FiSpeciesID,
                        CountID = RandomHelper.Randomnumber(),
                        Valid = CurrentTbl93Comment.Valid,
                        ValidYear = CurrentTbl93Comment.ValidYear,
                        Info = CurrentTbl93Comment.Info,
                        Memo = CurrentTbl93Comment.Memo,
                        Writer = Environment.UserName,
                        WriterDate = DateTime.Now,
                        Updater = Environment.UserName,
                        UpdaterDate = DateTime.Now,
                        EntityState = EntityState.Added
                    };
                }
                {
                    //check if dataset with Name and VbIds already exist       
                    var dataset = _businessLayer.ListTbl93CommentsByCurrentItem(CurrentTbl93Comment);

                    if (dataset.Count != 0 && CurrentTbl93Comment.CommentID == 0)  //dataset exist
                    {
                        WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTbl93Comment.Info,
                            MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                            return;
                    }

                    if (dataset.Count == 0 && CurrentTbl93Comment.CommentID == 0 ||
                        dataset.Count != 0 && CurrentTbl93Comment.CommentID != 0 ||
                        dataset.Count == 0 && CurrentTbl93Comment.CommentID != 0) //new dataset and update
                    {
                        if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTbl93Comment.Info,
                                MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes)
                            return;
                        {
                                _businessLayer.UpdateComment(comment);

                                    WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess,
                                        CurrentTbl93Comment.CommentID == 0
                                            ? CultRes.StringsRes.DatasetNew
                                            : CurrentTbl93Comment.Info,
                                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        }
                    }
                }
            }
            catch (DbEntityValidationException ex)
            {
                _entityException.EntityException(ex);
            }

            Tbl93CommentsList = new ObservableCollection<Tbl93Comment>(_businessLayer.ListTbl93CommentsByTribusId(CurrentTbl57Tribus.TribusID));          
     
            SelectedMainTabIndex = 3;

            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            CommentsView.Refresh();
        }
        #endregion "Public Commands"                  
 
             
 //    Part 9    

      
        #region "Public Commands Connected Tables by DoubleClick"

        private RelayCommand _getConnectedTablesCommand;
        public ICommand GetConnectedTablesCommand => _getConnectedTablesCommand ??
                                                         (_getConnectedTablesCommand = new RelayCommand(delegate { GetConnectedTablesById(null); }));

        private void GetConnectedTablesById(object o)
        {
            Tbl60SubtribussesList?.Clear();
            Tbl90ReferenceExpertsList?.Clear();
            Tbl90ReferenceSourcesList?.Clear();
            Tbl90ReferenceAuthorsList?.Clear();
            Tbl93CommentsList?.Clear();

            SelectedMainTabIndex = 0;  //change to Connect tab
            SelectedMainSubRefTabIndex = 0;
            SelectedDetailTabIndex = 1;
            SelectedDetailSubTabIndex = 0;
            SelectedDetailSubRefTabIndex = 0;

            _businessLayer = new BusinessLayer.BusinessLayer();
             Tbl51InfrafamiliesAllList =  new ObservableCollection<Tbl51Infrafamily>(_businessLayer.ListTbl51Infrafamilies());

             Tbl54SupertribussesList =  new ObservableCollection<Tbl54Supertribus>(
                       _businessLayer.ListTbl54SupertribussesBySupertribusId(CurrentTbl57Tribus.SupertribusID));
 
            SupertribussesView = CollectionViewSource.GetDefaultView(Tbl54SupertribussesList);
            SupertribussesView.Refresh();
        }

        #endregion "Public Commands Connected Tables by DoubleClick"     
 

 //    Part 10    

    
        #region "Public Commands to open Detail TabItems"

        private int _selectedMainTabIndex;
        private int _selectedMainSubRefTabIndex;
        private int _selectedDetailTabIndex;
        private int _selectedDetailSubTabIndex;
        private int _selectedDetailSubRefTabIndex;

        public  int SelectedMainTabIndex
        {
            get => _selectedMainTabIndex; 
            set
            {
                if (value == _selectedMainTabIndex) return;
                _selectedMainTabIndex = value; 
                RaisePropertyChanged();
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

        public  int SelectedMainSubRefTabIndex
        {
            get => _selectedMainSubRefTabIndex; 
            set
            {
                if (value == _selectedMainSubRefTabIndex) return;
                _selectedMainSubRefTabIndex = value; 
                 RaisePropertyChanged();
                if (_selectedMainSubRefTabIndex == 0)
                    SelectedDetailSubRefTabIndex = 0;
                if (_selectedMainSubRefTabIndex == 1)
                    SelectedDetailSubRefTabIndex = 1;
                if (_selectedMainSubRefTabIndex == 2)
                    SelectedDetailSubRefTabIndex = 2;
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
                    SelectedDetailSubTabIndex = 1;                
                if (_selectedDetailTabIndex == 2)                
                    SelectedDetailSubTabIndex = 2;               
                if (_selectedDetailTabIndex == 3)
                    SelectedDetailSubTabIndex = 3;
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
                    Tbl54SupertribussesList =  new ObservableCollection<Tbl54Supertribus>(
                        _businessLayer.ListTbl54SupertribussesBySupertribusId(CurrentTbl57Tribus.SupertribusID));
 
                    Tbl51InfrafamiliesAllList =  new ObservableCollection<Tbl51Infrafamily>(
                        _businessLayer.ListTbl51Infrafamilies());

                    SupertribussesView = CollectionViewSource.GetDefaultView(Tbl54SupertribussesList);
                    SupertribussesView.Refresh();

                    SelectedMainTabIndex = 0;
                }
                if (_selectedDetailSubTabIndex == 1)
                {
                    Tbl60SubtribussesList =  new ObservableCollection<Tbl60Subtribus>(
                        _businessLayer.ListTbl60SubtribussesByTribusId(CurrentTbl57Tribus.TribusID));

                    SubtribussesView = CollectionViewSource.GetDefaultView(Tbl60SubtribussesList);
                    SubtribussesView.Refresh();

                    SelectedMainTabIndex = 1;
                }
                if (_selectedDetailSubTabIndex == 2)
                {
                    Tbl90ExpertsAllList = new ObservableCollection<Tbl90RefExpert>(
                        _businessLayer.ListTbl90RefExperts());
                    Tbl90ReferenceExpertsList = new ObservableCollection<Tbl90Reference>(
                        _businessLayer.ListTbl90ReferenceListRefExpertsByTribusId(CurrentTbl57Tribus.TribusID));

                    ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
                    ReferenceExpertsView.Refresh();

                    SelectedMainTabIndex = 2;
                }
                if (_selectedDetailSubTabIndex == 3)
                {
                    Tbl93CommentsList = new ObservableCollection<Tbl93Comment>(
                        _businessLayer.ListTbl93CommentsByTribusId(CurrentTbl57Tribus.TribusID));

                    CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
                    CommentsView.Refresh();

                    SelectedMainTabIndex = 3;
                }
            }
        }

        public  int SelectedDetailSubRefTabIndex
        {
            get => _selectedDetailSubRefTabIndex;
            set
            {
                if (value == _selectedDetailSubRefTabIndex) return;
                _selectedDetailSubRefTabIndex = value;
                RaisePropertyChanged();
                if (_selectedDetailSubRefTabIndex == 0)
                {
                    Tbl90ExpertsAllList = new ObservableCollection<Tbl90RefExpert>(
                        _businessLayer.ListTbl90RefExperts());
                    Tbl90ReferenceExpertsList = new ObservableCollection<Tbl90Reference>(
                        _businessLayer.ListTbl90ReferenceListRefExpertsByTribusId(CurrentTbl57Tribus.TribusID));

                    ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
                    ReferenceExpertsView.Refresh();

                    SelectedMainSubRefTabIndex = 0;
                }
                if (_selectedDetailSubRefTabIndex == 1)
                {
                    Tbl90SourcesAllList = new ObservableCollection<Tbl90RefSource>(
                        _businessLayer.ListTbl90RefSources());

                    Tbl90ReferenceSourcesList = new ObservableCollection<Tbl90Reference>(
                        _businessLayer.ListTbl90ReferenceListRefSourcesByTribusId(CurrentTbl57Tribus.TribusID));

                    ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
                    ReferenceSourcesView.Refresh();

                    SelectedMainSubRefTabIndex = 1;
                }
                if (_selectedDetailSubRefTabIndex == 2)
                {
                    Tbl90AuthorsAllList = new ObservableCollection<Tbl90RefAuthor>(
                        _businessLayer.ListTbl90RefAuthors());

                    Tbl90ReferenceAuthorsList = new ObservableCollection<Tbl90Reference>(
                        _businessLayer.ListTbl90ReferenceListRefAuthorsByTribusId(CurrentTbl57Tribus.TribusID));

                    ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
                    ReferenceAuthorsView.Refresh();

                    SelectedMainSubRefTabIndex = 2;
                }
            }
        }

        #endregion "Public Commands to open Detail TabItems"
 

 //    Part 11    

     
        #region "Public Properties Tbl57Tribus"

        private string _searchTribusName = "";
        public string SearchTribusName
        {
            get => _searchTribusName; 
            set { _searchTribusName = value; RaisePropertyChanged();  }
        }

        public  ICollectionView TribussesView;
        private   Tbl57Tribus CurrentTbl57Tribus => TribussesView?.CurrentItem as Tbl57Tribus;

        private ObservableCollection<Tbl57Tribus> _tbl57TribussesList;
        public  ObservableCollection<Tbl57Tribus> Tbl57TribussesList
        {
            get => _tbl57TribussesList; 
            set {  _tbl57TribussesList = value; RaisePropertyChanged();   }
        }

        private ObservableCollection<Tbl57Tribus> _tbl57TribussesAllList;
        public  ObservableCollection<Tbl57Tribus> Tbl57TribussesAllList
        {
            get => _tbl57TribussesAllList; 
            set {  _tbl57TribussesAllList = value; RaisePropertyChanged();   }
        }

        #endregion "Public Properties"   
       
        #region "Public Properties Tbl54Supertribus"

        public  ICollectionView SupertribussesView;
        private Tbl54Supertribus CurrentTbl54Supertribus => SupertribussesView?.CurrentItem as Tbl54Supertribus;           

        private ObservableCollection<Tbl54Supertribus> _tbl54SupertribussesList;
        public  ObservableCollection<Tbl54Supertribus> Tbl54SupertribussesList
        {
            get => _tbl54SupertribussesList; 
            set { _tbl54SupertribussesList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl54Supertribus> _tbl54SupertribussesAllList;
        public  ObservableCollection<Tbl54Supertribus> Tbl54SupertribussesAllList
        {
            get => _tbl54SupertribussesAllList; 
            set { _tbl54SupertribussesAllList = value; RaisePropertyChanged(); }       
        }

        #endregion "Public Properties"   
        
        #region "Public Properties Tbl60Subtribus"

        public ICollectionView SubtribussesView;
        private Tbl60Subtribus CurrentTbl60Subtribus => SubtribussesView?.CurrentItem as Tbl60Subtribus;           

        private ObservableCollection<Tbl60Subtribus> _tbl60SubtribussesList;
        public  ObservableCollection<Tbl60Subtribus> Tbl60SubtribussesList
        {
            get => _tbl60SubtribussesList; 
            set { _tbl60SubtribussesList = value; RaisePropertyChanged(); }
        }
        #endregion "Public Properties"     
        
        #region "Public Properties Tbl63Infratribus"

        public ICollectionView InfratribussesView;
        private Tbl63Infratribus CurrentTbl63Infratribus => InfratribussesView?.CurrentItem as Tbl63Infratribus;           

        private ObservableCollection<Tbl63Infratribus> _tbl63InfratribussesList;
        public  ObservableCollection<Tbl63Infratribus> Tbl63InfratribussesList
        {
            get => _tbl63InfratribussesList; 
            set { _tbl63InfratribussesList = value; RaisePropertyChanged(); }
        }
        #endregion "Public Properties"     
        
        #region "Public Properties Tbl51Infrafamily"

        private ObservableCollection<Tbl51Infrafamily> _tbl51InfrafamiliesAllList;
        public  ObservableCollection<Tbl51Infrafamily> Tbl51InfrafamiliesAllList
        {
            get => _tbl51InfrafamiliesAllList; 
            set { _tbl51InfrafamiliesAllList = value; RaisePropertyChanged(); }       
        }

        #endregion "Public Properties"     
           
        #region "Public Properties Tbl90Author"

        private ObservableCollection<Tbl90RefAuthor> _tbl90AuthorsAllList;
        public  ObservableCollection<Tbl90RefAuthor> Tbl90AuthorsAllList
        {
            get => _tbl90AuthorsAllList; 
            set { _tbl90AuthorsAllList = value; RaisePropertyChanged(); }
        }

        #endregion "Public Properties "

        #region "Public Properties Tbl90Source"

        private ObservableCollection<Tbl90RefSource> _tbl90SourcesAllList;
        public  ObservableCollection<Tbl90RefSource> Tbl90SourcesAllList
        {
            get => _tbl90SourcesAllList; 
            set { _tbl90SourcesAllList = value; RaisePropertyChanged(); }
        }

        #endregion "Public Properties "

        #region "Public Properties Tbl90Expert"

        private ObservableCollection<Tbl90RefExpert> _tbl90ExpertsAllList;
        public ObservableCollection<Tbl90RefExpert> Tbl90ExpertsAllList
        {
            get => _tbl90ExpertsAllList; 
            set { _tbl90ExpertsAllList = value; RaisePropertyChanged(); }
        }

        #endregion "Public Properties "

        #region "Public Properties Tbl90ReferenceAuthor"

        public ICollectionView ReferenceAuthorsView;
        private Tbl90Reference CurrentTbl90ReferenceAuthor => ReferenceAuthorsView?.CurrentItem as Tbl90Reference;

        private ObservableCollection<Tbl90Reference> _tbl90ReferenceAuthorsList;
        public ObservableCollection<Tbl90Reference> Tbl90ReferenceAuthorsList
        {
            get => _tbl90ReferenceAuthorsList; 
            set { _tbl90ReferenceAuthorsList = value; RaisePropertyChanged(); }
        }

        #endregion "Public Properties"

        #region "Public Properties Tbl90ReferenceSource"

        public ICollectionView ReferenceSourcesView;
        private Tbl90Reference CurrentTbl90ReferenceSource => ReferenceSourcesView?.CurrentItem as Tbl90Reference;

        private ObservableCollection<Tbl90Reference> _tbl90ReferenceSourcesList;
        public ObservableCollection<Tbl90Reference> Tbl90ReferenceSourcesList
        {
            get => _tbl90ReferenceSourcesList; 
            set { _tbl90ReferenceSourcesList = value; RaisePropertyChanged(); }
        }

        #endregion "Public Properties"

        #region "Public Properties Tbl90ReferenceExpert"

        public ICollectionView ReferenceExpertsView;
        private Tbl90Reference CurrentTbl90ReferenceExpert => ReferenceExpertsView?.CurrentItem as Tbl90Reference;

        private ObservableCollection<Tbl90Reference> _tbl90ReferenceExpertsList;
        public ObservableCollection<Tbl90Reference> Tbl90ReferenceExpertsList
        {
            get => _tbl90ReferenceExpertsList; 
            set { _tbl90ReferenceExpertsList = value; RaisePropertyChanged(); }
        }

        #endregion "Public Properties"   
         
        #region "Public Properties Tbl93Comment"

        public ICollectionView CommentsView;
        private Tbl93Comment CurrentTbl93Comment => CommentsView?.CurrentItem as Tbl93Comment;

        private ObservableCollection<Tbl93Comment> _tbl93CommentsList;
        public ObservableCollection<Tbl93Comment> Tbl93CommentsList
        {
            get => _tbl93CommentsList; 
            set { _tbl93CommentsList = value; RaisePropertyChanged(); }
        }

        #endregion "Public Properties"     
 

 



   }
}   
