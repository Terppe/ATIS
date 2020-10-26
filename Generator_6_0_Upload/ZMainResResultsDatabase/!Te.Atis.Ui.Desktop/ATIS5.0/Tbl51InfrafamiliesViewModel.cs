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

    
         //    Tbl51InfrafamiliesViewModel Skriptdatum:  08.11.2018  10:32    

namespace Te.Atis.Ui.Desktop.Views.Database
{     
    
    public class Tbl51InfrafamiliesViewModel : ViewModelBase                     
    {     
         
        #region "Private Data Members"
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private static IBusinessLayer _businessLayer;
        private static DbEntityException _entityException;
        private int _position;   
         
        #endregion "Private Data Members"               
      
        #region "Constructor"

        public Tbl51InfrafamiliesViewModel()
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

             
        #region "Public Commands Basic Tbl51Infrafamily"
        //-------------------------------------------------------------------------
        private RelayCommand _clearInfrafamilyCommand;

        public ICommand ClearInfrafamilyCommand => _clearInfrafamilyCommand ??
                                                  (_clearInfrafamilyCommand = new RelayCommand(delegate { ClearInfrafamily(null); }));         
             
        private RelayCommand _getInfrafamiliesByNameOrIdCommand;  

        public  ICommand GetInfrafamiliesByNameOrIdCommand => _getInfrafamiliesByNameOrIdCommand ??
                                                           (_getInfrafamiliesByNameOrIdCommand = new RelayCommand(delegate { GetInfrafamiliesByNameOrId(null); }));        
             
        private RelayCommand _addInfrafamilyCommand;

        public ICommand AddInfrafamilyCommand => _addInfrafamilyCommand ??
                                                (_addInfrafamilyCommand = new RelayCommand(delegate { AddInfrafamily(null); }));

        private RelayCommand _copyInfrafamilyCommand;

        public ICommand CopyInfrafamilyCommand => _copyInfrafamilyCommand ??
                                                 (_copyInfrafamilyCommand = new RelayCommand(delegate { CopyInfrafamily(null); }));      
             
        private RelayCommand _deleteInfrafamilyCommand;

        public ICommand DeleteInfrafamilyCommand => _deleteInfrafamilyCommand ??
                                                   (_deleteInfrafamilyCommand = new RelayCommand(delegate { DeleteInfrafamily(null); }));    
             
        private RelayCommand _saveInfrafamilyCommand;

        public ICommand SaveInfrafamilyCommand => _saveInfrafamilyCommand ??
                                                 (_saveInfrafamilyCommand = new RelayCommand(delegate { SaveInfrafamily(null); }));
        //-------------------------------------------------------------------------          
     
        private void ClearInfrafamily(object o)
        {
            SearchInfrafamilyName = "";

            SelectedMainTabIndex = 0;  //change tab
            SelectedDetailTabIndex = 0;
            SelectedDetailSubTabIndex = 0;
            SelectedDetailSubRefTabIndex = 0;

            Tbl48SubfamiliesList?.Clear();
            Tbl51InfrafamiliesList?.Clear();
            Tbl54SupertribussesList?.Clear();
            Tbl90ReferenceExpertsList?.Clear();
            Tbl90ReferenceSourcesList?.Clear();
            Tbl90ReferenceAuthorsList?.Clear();
            Tbl93CommentsList?.Clear();
        }
        //----------------------------------------------------------------------                  
     
        private void GetInfrafamiliesByNameOrId(object o)
        {
            if (SearchInfrafamilyName != "")
            {
                Tbl51InfrafamiliesList?.Clear();
                if (SearchInfrafamilyName == "*") // show whole table
                {
                    SearchInfrafamilyName = "";
                    _businessLayer = new BusinessLayer.BusinessLayer();
                    Tbl48SubfamiliesAllList = new ObservableCollection<Tbl48Subfamily>(_businessLayer.ListTbl48Subfamilies());
                    Tbl51InfrafamiliesList = new ObservableCollection<Tbl51Infrafamily>(_businessLayer.ListTbl51InfrafamiliesByInfrafamilyName(SearchInfrafamilyName));
                    SearchInfrafamilyName = "*";
                }
                else
                {
                    _businessLayer = new BusinessLayer.BusinessLayer();
                    Tbl48SubfamiliesAllList = new ObservableCollection<Tbl48Subfamily>(_businessLayer.ListTbl48Subfamilies());
                    Tbl51InfrafamiliesList = int.TryParse(SearchInfrafamilyName, out var id) ?
                        new ObservableCollection<Tbl51Infrafamily>(_businessLayer.ListTbl51InfrafamiliesByInfrafamilyId(id)) :
                        new ObservableCollection<Tbl51Infrafamily>(_businessLayer.ListTbl51InfrafamiliesByInfrafamilyName(SearchInfrafamilyName));
                }

                if (Tbl51InfrafamiliesList.Count == 0)
                {
                    WpfMessageBox.Show(CultRes.StringsRes.Tables, CultRes.StringsRes.DatasetNot,
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                }
                else
                {
                    Tbl48SubfamiliesList?.Clear();
                    Tbl54SupertribussesList?.Clear();
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
            InfrafamiliesView = CollectionViewSource.GetDefaultView(Tbl51InfrafamiliesList);
            InfrafamiliesView.Refresh();
        }
        //------------------------------------------------------------------------------------                          
     
        private void AddInfrafamily(object o)
        {
            if (Tbl51InfrafamiliesList == null)
                Tbl51InfrafamiliesList =  new ObservableCollection<Tbl51Infrafamily>( );

            Tbl51InfrafamiliesList.Insert(0, new Tbl51Infrafamily   {   InfrafamilyName = CultRes.StringsRes.DatasetNew  }  );

                    _businessLayer = new BusinessLayer.BusinessLayer();
                Tbl48SubfamiliesAllList = new ObservableCollection<Tbl48Subfamily>(_businessLayer.ListTbl48Subfamilies());

            InfrafamiliesView = CollectionViewSource.GetDefaultView(Tbl51InfrafamiliesList);
            InfrafamiliesView.MoveCurrentToFirst();
        }
        //------------------------------------------------------------------------------------                               
     
        private void CopyInfrafamily(object o)
        {
            if (CurrentTbl51Infrafamily == null)
            {
                WpfMessageBox.Show(CultRes.StringsRes.DatasetNew,
                    CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                return;
            }
            _businessLayer = new BusinessLayer.BusinessLayer();

            var infrafamily = _businessLayer.SingleListTbl51InfrafamiliesByInfrafamilyId(CurrentTbl51Infrafamily.InfrafamilyID);

            Tbl51InfrafamiliesList.Insert(0, new Tbl51Infrafamily
            {
                 SubfamilyID =  infrafamily. SubfamilyID,
                 InfrafamilyName = CultRes.StringsRes.DatasetNew,
                Valid =  infrafamily.Valid,
                ValidYear =  infrafamily.ValidYear,
                Synonym =  infrafamily.Synonym,
                Author =  infrafamily.Author,
                AuthorYear =  infrafamily.AuthorYear,
                Info =  infrafamily.Info,
                EngName =  infrafamily.EngName,
                GerName =  infrafamily.GerName,
                FraName =  infrafamily.FraName,
                PorName =  infrafamily.PorName,
                Memo =  infrafamily.Memo
            });

            InfrafamiliesView = CollectionViewSource.GetDefaultView(Tbl51InfrafamiliesList);
            InfrafamiliesView.MoveCurrentToFirst();
        }
        //---------------------------------------------------------------------------------------                            
     
        private void DeleteInfrafamily(object o)
        {
            if (CurrentTbl51Infrafamily == null)
            {
                WpfMessageBox.Show(CultRes.StringsRes.DatasetNew,
                    CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                return;
            }
            _businessLayer = new BusinessLayer.BusinessLayer();

            var ret = false;
            //check if in Tbl54Supertribusses connected datasets, than return
            Tbl54SupertribussesList = new ObservableCollection<Tbl54Supertribus>(_businessLayer.ListTbl54SupertribussesByInfrafamilyId(CurrentTbl51Infrafamily.InfrafamilyID));
            if (Tbl54SupertribussesList.Count != 0)
            {
                WpfMessageBox.Show(CultRes.StringsRes.ConnectedTable, CultRes.StringsRes.Supertribus + " " + CultRes.StringsRes.ConnectedDataset,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                 ret = true;              
            }
            Tbl90ReferenceAuthorsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ReferenceListRefAuthorsByInfrafamilyId(CurrentTbl51Infrafamily.InfrafamilyID));
            if (Tbl90ReferenceAuthorsList.Count != 0)
            {
                WpfMessageBox.Show(CultRes.StringsRes.ConnectedTable, CultRes.StringsRes.ReferenceAuthor + " " + CultRes.StringsRes.ConnectedDataset,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                ret = true;
            }
            Tbl90ReferenceSourcesList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ReferenceListRefSourcesByInfrafamilyId(CurrentTbl51Infrafamily.InfrafamilyID));
            if (Tbl90ReferenceSourcesList.Count != 0)
            {
                WpfMessageBox.Show(CultRes.StringsRes.ConnectedTable, CultRes.StringsRes.ReferenceSource + " " + CultRes.StringsRes.ConnectedDataset,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                ret = true;
            }
            Tbl90ReferenceExpertsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ReferenceListRefExpertsByInfrafamilyId(CurrentTbl51Infrafamily.InfrafamilyID));
            if (Tbl90ReferenceExpertsList.Count != 0)
            {
                WpfMessageBox.Show(CultRes.StringsRes.ConnectedTable, CultRes.StringsRes.ReferenceExpert + " " + CultRes.StringsRes.ConnectedDataset,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                ret = true;
            }
            Tbl93CommentsList = new ObservableCollection<Tbl93Comment>(_businessLayer.ListTbl93CommentsByInfrafamilyId(CurrentTbl51Infrafamily.InfrafamilyID));
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
                var infrafamily = _businessLayer.SingleListTbl51InfrafamiliesByInfrafamilyId(CurrentTbl51Infrafamily.InfrafamilyID);
                if (infrafamily != null)
                {
                    if (WpfMessageBox.Show(CultRes.StringsRes.DeleteQuestion1, CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl51Infrafamily.InfrafamilyName,
                            MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes)
                        return;
                    infrafamily.EntityState = EntityState.Deleted;
                    _businessLayer.RemoveInfrafamily(infrafamily);

                    WpfMessageBox.Show(CultRes.StringsRes.DeleteSuccess, CurrentTbl51Infrafamily.InfrafamilyName,
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                }
                else
                {
                    WpfMessageBox.Show(CultRes.StringsRes.Information, CultRes.StringsRes.DeleteCan + " " + CurrentTbl51Infrafamily.InfrafamilyName + " " + CultRes.StringsRes.DeleteCan1,
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                }
            }
            catch (DbEntityValidationException ex)
            {
                _entityException.EntityException(ex);
            }
         }
            if (SearchInfrafamilyName != "")
            {
                if (SearchInfrafamilyName == "*")  //show all datasets
                {
                    SearchInfrafamilyName = "";
                    Tbl51InfrafamiliesList.Clear();
                    
                Tbl51InfrafamiliesList = new ObservableCollection<Tbl51Infrafamily>(_businessLayer.ListTbl51InfrafamiliesByInfrafamilyName(SearchInfrafamilyName));            
                    SearchInfrafamilyName = "*";
                }
                else
                {               
                    Tbl51InfrafamiliesList =  new ObservableCollection<Tbl51Infrafamily>(_businessLayer.ListTbl51InfrafamiliesByInfrafamilyName(SearchInfrafamilyName));

                }
                InfrafamiliesView = CollectionViewSource.GetDefaultView(Tbl51InfrafamiliesList);
                InfrafamiliesView.Refresh();
            }
            else  //SearchName = empty
            {
                Tbl51InfrafamiliesList = new ObservableCollection<Tbl51Infrafamily>(_businessLayer.ListTbl51InfrafamiliesByInfrafamilyName(SearchInfrafamilyName));

                InfrafamiliesView = CollectionViewSource.GetDefaultView(Tbl51InfrafamiliesList);
                InfrafamiliesView.MoveCurrentToFirst();
             }
        }
        //-------------------------------------------------------------------------------------------------                    
     
        private void SaveInfrafamily(object o)
        {
            if (CurrentTbl51Infrafamily == null)
            {
                WpfMessageBox.Show(CultRes.StringsRes.DatasetNew,
                    CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                return;
            }
            _businessLayer = new BusinessLayer.BusinessLayer();

            try
            {
                var infrafamily = _businessLayer.SingleListTbl51InfrafamiliesByInfrafamilyId(CurrentTbl51Infrafamily.InfrafamilyID);
                if (CurrentTbl51Infrafamily.InfrafamilyID != 0)
                {
                    if (infrafamily != null) //update
                    {
                        infrafamily.InfrafamilyName = CurrentTbl51Infrafamily.InfrafamilyName;
                        infrafamily.SubfamilyID = CurrentTbl51Infrafamily.SubfamilyID;
                        infrafamily.Valid = CurrentTbl51Infrafamily.Valid;
                        infrafamily.ValidYear = CurrentTbl51Infrafamily.ValidYear;       
                        infrafamily.Synonym = CurrentTbl51Infrafamily.Synonym;
                        infrafamily.Author = CurrentTbl51Infrafamily.Author;
                        infrafamily.AuthorYear = CurrentTbl51Infrafamily.AuthorYear;
                        infrafamily.Info = CurrentTbl51Infrafamily.Info;
                        infrafamily.EngName = CurrentTbl51Infrafamily.EngName;
                        infrafamily.GerName = CurrentTbl51Infrafamily.GerName;
                        infrafamily.FraName = CurrentTbl51Infrafamily.FraName;
                        infrafamily.PorName = CurrentTbl51Infrafamily.PorName;
                        infrafamily.Updater = Environment.UserName;
                        infrafamily.UpdaterDate = DateTime.Now;
                        infrafamily.Memo = CurrentTbl51Infrafamily.Memo;
                        infrafamily.EntityState = EntityState.Modified;
                    }
                }
                else
                {
                    infrafamily = new Tbl51Infrafamily   //add new
                    {
                        InfrafamilyName = CurrentTbl51Infrafamily.InfrafamilyName,
                        SubfamilyID = CurrentTbl51Infrafamily.SubfamilyID,

                        CountID = RandomHelper.Randomnumber(),
                        Valid = CurrentTbl51Infrafamily.Valid,
                        ValidYear = CurrentTbl51Infrafamily.ValidYear,
                        Synonym = CurrentTbl51Infrafamily.Synonym,
                        Author = CurrentTbl51Infrafamily.Author,
                        AuthorYear = CurrentTbl51Infrafamily.AuthorYear,
                        Info = CurrentTbl51Infrafamily.Info,
                        EngName = CurrentTbl51Infrafamily.EngName,
                        GerName = CurrentTbl51Infrafamily.GerName,
                        FraName = CurrentTbl51Infrafamily.FraName,
                        PorName = CurrentTbl51Infrafamily.PorName,
                        Writer = Environment.UserName,
                        WriterDate = DateTime.Now,
                        Updater = Environment.UserName,
                        UpdaterDate = DateTime.Now,
                        Memo = CurrentTbl51Infrafamily.Memo,
                        EntityState = EntityState.Added
                    };
                }
                {
                    //SubfamilyID may be not 0
                    if (CurrentTbl51Infrafamily.SubfamilyID == 0)          

                    {
                        WpfMessageBox.Show(CultRes.StringsRes.RequiredGenealogyConnect, CultRes.StringsRes.RequiredInput,
                            MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        return;
                    }

                    //check if dataset with Name and SubfamilyId already exist       
                    var dataset = _businessLayer.ListTbl51InfrafamiliesByInfrafamilyNameAndSubfamilyId(CurrentTbl51Infrafamily.InfrafamilyName, CurrentTbl51Infrafamily.SubfamilyID);

                    if (dataset.Count != 0 && CurrentTbl51Infrafamily.InfrafamilyID == 0)  //dataset exist
                    {
                        WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTbl51Infrafamily.InfrafamilyName,
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        return;
                    }
                    if (dataset.Count == 0 && CurrentTbl51Infrafamily.InfrafamilyID == 0 ||
                        dataset.Count != 0 && CurrentTbl51Infrafamily.InfrafamilyID != 0 ||
                        dataset.Count == 0 && CurrentTbl51Infrafamily.InfrafamilyID != 0) //new dataset and update
                    {
                        if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTbl51Infrafamily.InfrafamilyName,
                                MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes)
                            return;
                        {
                                _businessLayer.UpdateInfrafamily(infrafamily);
                                _position = InfrafamiliesView.CurrentPosition;

                                    WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess,
                                        CurrentTbl51Infrafamily.InfrafamilyID == 0
                                            ? CultRes.StringsRes.DatasetNew
                                            : CurrentTbl51Infrafamily.InfrafamilyName,
                                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                          }
                    }
                }
            }
            catch (DbEntityValidationException ex)
            {
                _entityException.EntityException(ex);
            }

            if (SearchInfrafamilyName != "")
            {
                if (SearchInfrafamilyName == "*")  //show all datasets
                {
                    SearchInfrafamilyName = "";
                    Tbl51InfrafamiliesList.Clear();
                    
                Tbl51InfrafamiliesList = new ObservableCollection<Tbl51Infrafamily>(_businessLayer.ListTbl51InfrafamiliesByInfrafamilyName(SearchInfrafamilyName));            
                    SearchInfrafamilyName = "*";
                }
                else
                {               
                    Tbl51InfrafamiliesList = int.TryParse(SearchInfrafamilyName, out var id)
                        ? new ObservableCollection<Tbl51Infrafamily>(_businessLayer.ListTbl51InfrafamiliesByInfrafamilyId(id))
                        : new ObservableCollection<Tbl51Infrafamily>(_businessLayer.ListTbl51InfrafamiliesByInfrafamilyName(SearchInfrafamilyName));

                }
                InfrafamiliesView = CollectionViewSource.GetDefaultView(Tbl51InfrafamiliesList);
                InfrafamiliesView.MoveCurrentToPosition(_position);
            }
            else  
            {
                Tbl51InfrafamiliesList = new ObservableCollection<Tbl51Infrafamily>(_businessLayer.ListTbl51InfrafamiliesByInfrafamilyName(CurrentTbl51Infrafamily.InfrafamilyName));

                InfrafamiliesView= CollectionViewSource.GetDefaultView(Tbl51InfrafamiliesList);
                InfrafamiliesView.Refresh();
            }
        }
        #endregion "Public Commands"                  
 
 

 //    Part 2    

           
        #region "Public Commands Connect <== Tbl48Subfamily"                 
        //-------------------------------------------------------------------------

        private RelayCommand _saveSubfamilyCommand;

        public ICommand SaveSubfamilyCommand => _saveSubfamilyCommand ??
                                                 (_saveSubfamilyCommand = new RelayCommand(delegate { SaveSubfamily(null); }));

        //-------------------------------------------------------------------------          
     
        private void SaveSubfamily(object o)
        {
            if (CurrentTbl48Subfamily == null)
            {
                WpfMessageBox.Show(CultRes.StringsRes.DatasetNew,
                    CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                return;
            }

            try
            {
                var subfamily = _businessLayer.SingleListTbl48SubfamiliesBySubfamilyId(CurrentTbl48Subfamily.SubfamilyID);
                if (CurrentTbl48Subfamily.SubfamilyID != 0)
                {
                    if (subfamily != null) //update
                    {
                        subfamily.SubfamilyName = CurrentTbl48Subfamily.SubfamilyName;
                        subfamily.Valid = CurrentTbl48Subfamily.Valid;
                        subfamily.ValidYear = CurrentTbl48Subfamily.ValidYear;       
                        subfamily.Synonym = CurrentTbl48Subfamily.Synonym;
                        subfamily.Author = CurrentTbl48Subfamily.Author;
                        subfamily.AuthorYear = CurrentTbl48Subfamily.AuthorYear;
                        subfamily.Info = CurrentTbl48Subfamily.Info;
                        subfamily.EngName = CurrentTbl48Subfamily.EngName;
                        subfamily.GerName = CurrentTbl48Subfamily.GerName;
                        subfamily.FraName = CurrentTbl48Subfamily.FraName;
                        subfamily.PorName = CurrentTbl48Subfamily.PorName;
                        subfamily.Updater = Environment.UserName;
                        subfamily.UpdaterDate = DateTime.Now;
                        subfamily.Memo = CurrentTbl48Subfamily.Memo;
                        subfamily.EntityState = EntityState.Modified;
                    }
                }
                else
                {
                    subfamily = new Tbl48Subfamily   //add new
                    {
                        SubfamilyName = CurrentTbl48Subfamily.SubfamilyName,              
                        FamilyID = CurrentTbl48Subfamily.FamilyID,     
                        CountID = RandomHelper.Randomnumber(),

                        Valid = CurrentTbl48Subfamily.Valid,
                        ValidYear = CurrentTbl48Subfamily.ValidYear,
                        Synonym = CurrentTbl48Subfamily.Synonym,
                        Author = CurrentTbl48Subfamily.Author,
                        AuthorYear = CurrentTbl48Subfamily.AuthorYear,
                        Info = CurrentTbl48Subfamily.Info,
                        EngName = CurrentTbl48Subfamily.EngName,
                        GerName = CurrentTbl48Subfamily.GerName,
                        FraName = CurrentTbl48Subfamily.FraName,
                        PorName = CurrentTbl48Subfamily.PorName,
                        Writer = Environment.UserName,
                        WriterDate = DateTime.Now,
                        Updater = Environment.UserName,
                        UpdaterDate = DateTime.Now,
                        Memo = CurrentTbl48Subfamily.Memo,
                        EntityState = EntityState.Added
                    };
                }
                {
                    //FamilyID may be not 0
                    if (CurrentTbl48Subfamily.FamilyID == 0)          

                    {
                        WpfMessageBox.Show(CultRes.StringsRes.RequiredGenealogyConnect, CultRes.StringsRes.RequiredInput,
                            MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        return;
                    }

                    //check if dataset with Name and SubfamilyId already exist       
                    var dataset = _businessLayer.ListTbl48SubfamiliesBySubfamilyNameAndFamilyId(CurrentTbl48Subfamily.SubfamilyName, CurrentTbl48Subfamily.FamilyID);

                    if (dataset.Count != 0 && CurrentTbl48Subfamily.SubfamilyID == 0)  //dataset exist
                    {
                        WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTbl48Subfamily.SubfamilyName,
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        return;
                    }
                    if (dataset.Count == 0 && CurrentTbl48Subfamily.SubfamilyID == 0 ||
                        dataset.Count != 0 && CurrentTbl48Subfamily.SubfamilyID != 0 ||
                        dataset.Count == 0 && CurrentTbl48Subfamily.SubfamilyID != 0) //new dataset and update
                    {
                        if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTbl48Subfamily.SubfamilyName,
                                MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes)
                            return;
                        {
                                _businessLayer.UpdateSubfamily(subfamily);

                                    WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess,
                                        CurrentTbl48Subfamily.SubfamilyID == 0
                                            ? CultRes.StringsRes.DatasetNew
                                            : CurrentTbl48Subfamily.SubfamilyName,
                                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        }
                    }
                }
            }
            catch (DbEntityValidationException ex)
            {
                _entityException.EntityException(ex);
            }

                Tbl48SubfamiliesList = new ObservableCollection<Tbl48Subfamily>(_businessLayer.ListTbl48SubfamiliesBySubfamilyId(CurrentTbl51Infrafamily.SubfamilyID));            

            SelectedMainTabIndex = 0;
            SubfamiliesView = CollectionViewSource.GetDefaultView(Tbl48SubfamiliesList);
            SubfamiliesView.Refresh();
        }
        #endregion "Public Commands"                  
                                                          

 //    Part 3    

                                                          



 //    Part 4    

           
        #region "Public Commands Connect ==> Tbl54Supertribus"                 
        //-------------------------------------------------------------------------
        private RelayCommand _addSupertribusCommand;

        public ICommand AddSupertribusCommand => _addSupertribusCommand ??
                                                (_addSupertribusCommand = new RelayCommand(delegate { AddSupertribus(null); }));

        private RelayCommand _copySupertribusCommand;

        public ICommand CopySupertribusCommand => _copySupertribusCommand ??
                                                 (_copySupertribusCommand = new RelayCommand(delegate { CopySupertribus(null); }));

        private RelayCommand _deleteSupertribusCommand;

        public ICommand DeleteSupertribusCommand => _deleteSupertribusCommand ??
                                                 (_deleteSupertribusCommand = new RelayCommand(delegate { DeleteSupertribus(null); }));

        private RelayCommand _saveSupertribusCommand;

        public ICommand SaveSupertribusCommand => _saveSupertribusCommand ??
                                                 (_saveSupertribusCommand = new RelayCommand(delegate { SaveSupertribus(null); }));

        //-------------------------------------------------------------------------          
     
        private void AddSupertribus(object o)      
        {
            if (Tbl54SupertribussesList == null)
                Tbl54SupertribussesList =  new ObservableCollection<Tbl54Supertribus>( );

            Tbl54SupertribussesList.Insert(0, new Tbl54Supertribus  { SupertribusName = CultRes.StringsRes.DatasetNew});

            _businessLayer = new BusinessLayer.BusinessLayer();
                Tbl51InfrafamiliesAllList = new ObservableCollection<Tbl51Infrafamily>(_businessLayer.ListTbl51Infrafamilies());

            SupertribussesView = CollectionViewSource.GetDefaultView(Tbl54SupertribussesList);
            SupertribussesView.MoveCurrentToFirst();
        }
        //----------------------------------------------------------------------            
     
        private void CopySupertribus(object o)
        {
            if (CurrentTbl54Supertribus == null)
            {
                WpfMessageBox.Show(CultRes.StringsRes.DatasetNew,
                    CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                return;
            }

            var supertribus = _businessLayer.SingleListTbl54SupertribussesBySupertribusId(CurrentTbl54Supertribus.SupertribusID);

            Tbl54SupertribussesList.Insert(0, new Tbl54Supertribus
            {
                SupertribusName = CultRes.StringsRes.DatasetNew,
                Valid =  supertribus.Valid,
                ValidYear =  supertribus.ValidYear,
                Synonym =  supertribus.Synonym,
                Author =  supertribus.Author,
                AuthorYear =  supertribus.AuthorYear,
                Info =  supertribus.Info,
                EngName =  supertribus.EngName,
                GerName =  supertribus.GerName,
                FraName =  supertribus.FraName,
                PorName =  supertribus.PorName,
                Memo =  supertribus.Memo
            });

            SupertribussesView = CollectionViewSource.GetDefaultView(Tbl54SupertribussesList);
            SupertribussesView.MoveCurrentToFirst();
        }
        //----------------------------------------------------------------------            
           
        private void DeleteSupertribus(object o)
        {
            if (CurrentTbl54Supertribus == null)
            {
                WpfMessageBox.Show(CultRes.StringsRes.DatasetNew,
                    CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                return;
            }

            var ret = false;
            //check if in Tbl57Tribusses connected datasets, than return
            Tbl57TribussesList = new ObservableCollection<Tbl57Tribus>(_businessLayer.ListTbl57TribussesBySupertribusId(CurrentTbl54Supertribus.SupertribusID));
            if (Tbl57TribussesList.Count != 0)
            {
                WpfMessageBox.Show(CultRes.StringsRes.ConnectedTable, CultRes.StringsRes.Tribus + " " + CultRes.StringsRes.ConnectedDataset,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                 ret = true;              
            }
            Tbl90ReferenceAuthorsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ReferenceListRefAuthorsBySupertribusId(CurrentTbl54Supertribus.SupertribusID));
            if (Tbl90ReferenceAuthorsList.Count != 0)
            {
                WpfMessageBox.Show(CultRes.StringsRes.ConnectedTable, CultRes.StringsRes.ReferenceAuthor + " " + CultRes.StringsRes.ConnectedDataset,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                ret = true;
            }
            Tbl90ReferenceSourcesList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ReferenceListRefSourcesBySupertribusId(CurrentTbl54Supertribus.SupertribusID));
            if (Tbl90ReferenceSourcesList.Count != 0)
            {
                WpfMessageBox.Show(CultRes.StringsRes.ConnectedTable, CultRes.StringsRes.ReferenceSource + " " + CultRes.StringsRes.ConnectedDataset,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                ret = true;
            }
            Tbl90ReferenceExpertsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ReferenceListRefExpertsBySupertribusId(CurrentTbl54Supertribus.SupertribusID));
            if (Tbl90ReferenceExpertsList.Count != 0)
            {
                WpfMessageBox.Show(CultRes.StringsRes.ConnectedTable, CultRes.StringsRes.ReferenceExpert + " " + CultRes.StringsRes.ConnectedDataset,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                ret = true;
            }
            Tbl93CommentsList = new ObservableCollection<Tbl93Comment>(_businessLayer.ListTbl93CommentsBySupertribusId(CurrentTbl54Supertribus.SupertribusID));
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
                var supertribus = _businessLayer.SingleListTbl54SupertribussesBySupertribusId(CurrentTbl54Supertribus.SupertribusID);
                if (supertribus!= null)
                {
                    if (WpfMessageBox.Show(CultRes.StringsRes.DeleteQuestion1, CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl54Supertribus.SupertribusName,
                         MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes) 
                    return;
                    supertribus.EntityState = EntityState.Deleted;
                    _businessLayer.RemoveSupertribus(supertribus);

                    WpfMessageBox.Show(CultRes.StringsRes.DeleteSuccess, CurrentTbl54Supertribus.SupertribusName,
                       MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);  
                }
                else
                {
                    WpfMessageBox.Show(CultRes.StringsRes.Information, CultRes.StringsRes.DeleteCan + " " + CurrentTbl54Supertribus.SupertribusName + " " + CultRes.StringsRes.DeleteCan1,
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                }
            }
            catch (DbEntityValidationException ex)
            {
                _entityException.EntityException(ex);
            }
         }
            Tbl54SupertribussesList = new ObservableCollection<Tbl54Supertribus>(_businessLayer.ListTbl54SupertribussesByInfrafamilyId(CurrentTbl51Infrafamily.InfrafamilyID));

            SupertribussesView = CollectionViewSource.GetDefaultView(Tbl54SupertribussesList);
            SupertribussesView.Refresh();
        }
        //-------------------------------------------------------------------------------------------------                    
     
        private void SaveSupertribus(object o)
        {
            if (CurrentTbl54Supertribus == null)
            {
                WpfMessageBox.Show(CultRes.StringsRes.DatasetNew,
                    CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                return;
            }

            CurrentTbl54Supertribus.InfrafamilyID = CurrentTbl51Infrafamily.InfrafamilyID;

            try
            {
                var supertribus = _businessLayer.SingleListTbl54SupertribussesBySupertribusId(CurrentTbl54Supertribus.SupertribusID);
                if (CurrentTbl54Supertribus.SupertribusID != 0)
                {
                    if (supertribus != null) //update
                    {
                        supertribus.SupertribusName = CurrentTbl54Supertribus.SupertribusName;
                        supertribus.InfrafamilyID = CurrentTbl54Supertribus.InfrafamilyID;
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

                    //check if dataset with Name and InfrafamilyId already exist       
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

            Tbl54SupertribussesList = new ObservableCollection<Tbl54Supertribus>(_businessLayer.ListTbl54SupertribussesByInfrafamilyId(CurrentTbl51Infrafamily.InfrafamilyID));            

            SelectedMainTabIndex = 1;
            SupertribussesView = CollectionViewSource.GetDefaultView(Tbl54SupertribussesList);
            SupertribussesView.Refresh();
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

            Tbl90ReferenceAuthorsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ReferenceListRefAuthorsByInfrafamilyId(CurrentTbl51Infrafamily.InfrafamilyID));

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

            CurrentTbl90ReferenceAuthor.InfrafamilyID = CurrentTbl51Infrafamily.InfrafamilyID;

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

            Tbl90ReferenceAuthorsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ReferenceListRefAuthorsByInfrafamilyId(CurrentTbl51Infrafamily.InfrafamilyID));           
     

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

            Tbl90ReferenceSourcesList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ReferenceListRefSourcesByInfrafamilyId(CurrentTbl51Infrafamily.InfrafamilyID));

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

            CurrentTbl90ReferenceSource.InfrafamilyID = CurrentTbl51Infrafamily.InfrafamilyID;

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

            Tbl90ReferenceSourcesList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ReferenceListRefSourcesByInfrafamilyId(CurrentTbl51Infrafamily.InfrafamilyID));           
     
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

            Tbl90ReferenceExpertsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ReferenceListRefExpertsByInfrafamilyId(CurrentTbl51Infrafamily.InfrafamilyID));

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

            CurrentTbl90ReferenceExpert.InfrafamilyID = CurrentTbl51Infrafamily.InfrafamilyID;

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

            Tbl90ReferenceExpertsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ReferenceListRefExpertsByInfrafamilyId(CurrentTbl51Infrafamily.InfrafamilyID));     
     
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

            Tbl93CommentsList = new ObservableCollection<Tbl93Comment>(_businessLayer.ListTbl93CommentsByInfrafamilyId(CurrentTbl51Infrafamily.InfrafamilyID));

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

            CurrentTbl93Comment.InfrafamilyID = CurrentTbl51Infrafamily.InfrafamilyID;

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

            Tbl93CommentsList = new ObservableCollection<Tbl93Comment>(_businessLayer.ListTbl93CommentsByInfrafamilyId(CurrentTbl51Infrafamily.InfrafamilyID));          
     
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
            Tbl54SupertribussesList?.Clear();
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
             Tbl45FamiliesAllList =  new ObservableCollection<Tbl45Family>(_businessLayer.ListTbl45Families());

             Tbl48SubfamiliesList =  new ObservableCollection<Tbl48Subfamily>(
                       _businessLayer.ListTbl48SubfamiliesBySubfamilyId(CurrentTbl51Infrafamily.SubfamilyID));
 
            SubfamiliesView = CollectionViewSource.GetDefaultView(Tbl48SubfamiliesList);
            SubfamiliesView.Refresh();
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
                    Tbl48SubfamiliesList =  new ObservableCollection<Tbl48Subfamily>(
                        _businessLayer.ListTbl48SubfamiliesBySubfamilyId(CurrentTbl51Infrafamily.SubfamilyID));
 
                    Tbl45FamiliesAllList =  new ObservableCollection<Tbl45Family>(
                        _businessLayer.ListTbl45Families());

                    SubfamiliesView = CollectionViewSource.GetDefaultView(Tbl48SubfamiliesList);
                    SubfamiliesView.Refresh();

                    SelectedMainTabIndex = 0;
                }
                if (_selectedDetailSubTabIndex == 1)
                {
                    Tbl54SupertribussesList =  new ObservableCollection<Tbl54Supertribus>(
                        _businessLayer.ListTbl54SupertribussesByInfrafamilyId(CurrentTbl51Infrafamily.InfrafamilyID));

                    SupertribussesView = CollectionViewSource.GetDefaultView(Tbl54SupertribussesList);
                    SupertribussesView.Refresh();

                    SelectedMainTabIndex = 1;
                }
                if (_selectedDetailSubTabIndex == 2)
                {
                    Tbl90ExpertsAllList = new ObservableCollection<Tbl90RefExpert>(
                        _businessLayer.ListTbl90RefExperts());
                    Tbl90ReferenceExpertsList = new ObservableCollection<Tbl90Reference>(
                        _businessLayer.ListTbl90ReferenceListRefExpertsByInfrafamilyId(CurrentTbl51Infrafamily.InfrafamilyID));

                    ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
                    ReferenceExpertsView.Refresh();

                    SelectedMainTabIndex = 2;
                }
                if (_selectedDetailSubTabIndex == 3)
                {
                    Tbl93CommentsList = new ObservableCollection<Tbl93Comment>(
                        _businessLayer.ListTbl93CommentsByInfrafamilyId(CurrentTbl51Infrafamily.InfrafamilyID));

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
                        _businessLayer.ListTbl90ReferenceListRefExpertsByInfrafamilyId(CurrentTbl51Infrafamily.InfrafamilyID));

                    ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
                    ReferenceExpertsView.Refresh();

                    SelectedMainSubRefTabIndex = 0;
                }
                if (_selectedDetailSubRefTabIndex == 1)
                {
                    Tbl90SourcesAllList = new ObservableCollection<Tbl90RefSource>(
                        _businessLayer.ListTbl90RefSources());

                    Tbl90ReferenceSourcesList = new ObservableCollection<Tbl90Reference>(
                        _businessLayer.ListTbl90ReferenceListRefSourcesByInfrafamilyId(CurrentTbl51Infrafamily.InfrafamilyID));

                    ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
                    ReferenceSourcesView.Refresh();

                    SelectedMainSubRefTabIndex = 1;
                }
                if (_selectedDetailSubRefTabIndex == 2)
                {
                    Tbl90AuthorsAllList = new ObservableCollection<Tbl90RefAuthor>(
                        _businessLayer.ListTbl90RefAuthors());

                    Tbl90ReferenceAuthorsList = new ObservableCollection<Tbl90Reference>(
                        _businessLayer.ListTbl90ReferenceListRefAuthorsByInfrafamilyId(CurrentTbl51Infrafamily.InfrafamilyID));

                    ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
                    ReferenceAuthorsView.Refresh();

                    SelectedMainSubRefTabIndex = 2;
                }
            }
        }

        #endregion "Public Commands to open Detail TabItems"
 

 //    Part 11    

     
        #region "Public Properties Tbl51Infrafamily"

        private string _searchInfrafamilyName = "";
        public string SearchInfrafamilyName
        {
            get => _searchInfrafamilyName; 
            set { _searchInfrafamilyName = value; RaisePropertyChanged();  }
        }

        public  ICollectionView InfrafamiliesView;
        private   Tbl51Infrafamily CurrentTbl51Infrafamily => InfrafamiliesView?.CurrentItem as Tbl51Infrafamily;

        private ObservableCollection<Tbl51Infrafamily> _tbl51InfrafamiliesList;
        public  ObservableCollection<Tbl51Infrafamily> Tbl51InfrafamiliesList
        {
            get => _tbl51InfrafamiliesList; 
            set {  _tbl51InfrafamiliesList = value; RaisePropertyChanged();   }
        }

        private ObservableCollection<Tbl51Infrafamily> _tbl51InfrafamiliesAllList;
        public  ObservableCollection<Tbl51Infrafamily> Tbl51InfrafamiliesAllList
        {
            get => _tbl51InfrafamiliesAllList; 
            set {  _tbl51InfrafamiliesAllList = value; RaisePropertyChanged();   }
        }

        #endregion "Public Properties"   
       
        #region "Public Properties Tbl48Subfamily"

        public  ICollectionView SubfamiliesView;
        private Tbl48Subfamily CurrentTbl48Subfamily => SubfamiliesView?.CurrentItem as Tbl48Subfamily;           

        private ObservableCollection<Tbl48Subfamily> _tbl48SubfamiliesList;
        public  ObservableCollection<Tbl48Subfamily> Tbl48SubfamiliesList
        {
            get => _tbl48SubfamiliesList; 
            set { _tbl48SubfamiliesList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl48Subfamily> _tbl48SubfamiliesAllList;
        public  ObservableCollection<Tbl48Subfamily> Tbl48SubfamiliesAllList
        {
            get => _tbl48SubfamiliesAllList; 
            set { _tbl48SubfamiliesAllList = value; RaisePropertyChanged(); }       
        }

        #endregion "Public Properties"   
        
        #region "Public Properties Tbl54Supertribus"

        public ICollectionView SupertribussesView;
        private Tbl54Supertribus CurrentTbl54Supertribus => SupertribussesView?.CurrentItem as Tbl54Supertribus;           

        private ObservableCollection<Tbl54Supertribus> _tbl54SupertribussesList;
        public  ObservableCollection<Tbl54Supertribus> Tbl54SupertribussesList
        {
            get => _tbl54SupertribussesList; 
            set { _tbl54SupertribussesList = value; RaisePropertyChanged(); }
        }
        #endregion "Public Properties"     
        
        #region "Public Properties Tbl57Tribus"

        public ICollectionView TribussesView;
        private Tbl57Tribus CurrentTbl57Tribus => TribussesView?.CurrentItem as Tbl57Tribus;           

        private ObservableCollection<Tbl57Tribus> _tbl57TribussesList;
        public  ObservableCollection<Tbl57Tribus> Tbl57TribussesList
        {
            get => _tbl57TribussesList; 
            set { _tbl57TribussesList = value; RaisePropertyChanged(); }
        }
        #endregion "Public Properties"     
        
        #region "Public Properties Tbl45Family"

        private ObservableCollection<Tbl45Family> _tbl45FamiliesAllList;
        public  ObservableCollection<Tbl45Family> Tbl45FamiliesAllList
        {
            get => _tbl45FamiliesAllList; 
            set { _tbl45FamiliesAllList = value; RaisePropertyChanged(); }       
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
