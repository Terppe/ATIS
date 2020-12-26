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

    
         //    Tbl90RefAuthorsViewModel Skriptdatum:  29.11.2017  10:32    

namespace Te.Atis.Ui.Desktop.Views.Database
{     
    
    public class Tbl90RefAuthorsViewModel : ViewModelBase                     
    {     
         
        #region "Private Data Members"
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static IBusinessLayer _businessLayer;
        private static DbEntityException _entityException;
        private int _position;   
         
        #endregion "Private Data Members"               
      
        #region "Constructor"

        public Tbl90RefAuthorsViewModel()
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

             
        #region "Public Commands Basic Tbl90RefAuthor"
        //-------------------------------------------------------------------------
        private RelayCommand _clearRefAuthorCommand;

        public ICommand ClearRefAuthorCommand => _clearRefAuthorCommand ??
                                                  (_clearRefAuthorCommand = new RelayCommand(delegate { ClearRefAuthor(null); }));         
             
        private RelayCommand _getRefAuthorsByNameOrIdCommand;  

        public  ICommand GetRefAuthorsByNameOrIdCommand => _getRefAuthorsByNameOrIdCommand ??
                                                           (_getRefAuthorsByNameOrIdCommand = new RelayCommand(delegate { GetRefAuthorsByNameOrId(null); }));        
             
        private RelayCommand _addRefAuthorCommand;

        public ICommand AddRefAuthorCommand => _addRefAuthorCommand ??
                                                (_addRefAuthorCommand = new RelayCommand(delegate { AddRefAuthor(null); }));

        private RelayCommand _copyRefAuthorCommand;

        public ICommand CopyRefAuthorCommand => _copyRefAuthorCommand ??
                                                 (_copyRefAuthorCommand = new RelayCommand(delegate { CopyRefAuthor(null); }));      
             
        private RelayCommand _deleteRefAuthorCommand;

        public ICommand DeleteRefAuthorCommand => _deleteRefAuthorCommand ??
                                                   (_deleteRefAuthorCommand = new RelayCommand(delegate { DeleteRefAuthor(null); }));    
             
        private RelayCommand _saveRefAuthorCommand;

        public ICommand SaveRefAuthorCommand => _saveRefAuthorCommand ??
                                                 (_saveRefAuthorCommand = new RelayCommand(delegate { SaveRefAuthor(null); }));
        //-------------------------------------------------------------------------          
        
        private void ClearRefAuthor(object o)
        {
            SearchRefAuthorName = "";

            Tbl90RefAuthorsList?.Clear();
        }
        //----------------------------------------------------------------------                  
        
        private void GetRefAuthorsByNameOrId(object o)
        {
            if (SearchRefAuthorName != "")
            {
                Tbl90RefAuthorsList?.Clear();
                if (SearchRefAuthorName == "*") // show whole table
                {
                    SearchRefAuthorName = "";
                    _businessLayer = new BusinessLayer.BusinessLayer();
                    Tbl90RefAuthorsList = new ObservableCollection<Tbl90RefAuthor>(_businessLayer.ListTbl90RefAuthorsByRefAuthorName(SearchRefAuthorName));
                    SearchRefAuthorName = "*";
                }
                else
                {
                    _businessLayer = new BusinessLayer.BusinessLayer();
                    Tbl90RefAuthorsList = int.TryParse(SearchRefAuthorName, out var id) ?
                        new ObservableCollection<Tbl90RefAuthor>(_businessLayer.ListTbl90RefAuthorsByRefAuthorId(id)) :
                        new ObservableCollection<Tbl90RefAuthor>(_businessLayer.ListTbl90RefAuthorsByRefAuthorName(SearchRefAuthorName));
                }

                if (Tbl90RefAuthorsList.Count == 0)
                {
                    WpfMessageBox.Show(CultRes.StringsRes.Tables, CultRes.StringsRes.DatasetNot,
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                }
            }
            else
            {
                WpfMessageBox.Show(CultRes.StringsRes.SearchNameOrId, CultRes.StringsRes.InputRequested,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
            }
            RefAuthorsView = CollectionViewSource.GetDefaultView(Tbl90RefAuthorsList);
            RefAuthorsView.Refresh();
        }
        //------------------------------------------------------------------------------------                          
        
        private void AddRefAuthor(object o)
        {
            if (Tbl90RefAuthorsList == null)
                Tbl90RefAuthorsList =  new ObservableCollection<Tbl90RefAuthor>( );
 
            Tbl90RefAuthorsList.Insert(0, new Tbl90RefAuthor {   RefAuthorName = CultRes.StringsRes.DatasetNew}  );

            RefAuthorsView = CollectionViewSource.GetDefaultView(Tbl90RefAuthorsList);
            RefAuthorsView.MoveCurrentToFirst();
        }
        //------------------------------------------------------------------------------------                               
        
        private void CopyRefAuthor(object o)
        {
            if (CurrentTbl90RefAuthor == null)
            {
                WpfMessageBox.Show(CultRes.StringsRes.DatasetNew,
                    CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                return;
            }
            _businessLayer = new BusinessLayer.BusinessLayer();

            var refAuthor = _businessLayer.SingleListTbl90RefAuthorsByRefAuthorId(CurrentTbl90RefAuthor.RefAuthorID);

            Tbl90RefAuthorsList.Insert(0, new Tbl90RefAuthor
            {
                RefAuthorName = CultRes.StringsRes.DatasetNew,
                Valid = refAuthor.Valid,
                ValidYear = refAuthor.ValidYear,
                PublicationYear = refAuthor.PublicationYear,
                ArticelTitle = refAuthor.ArticelTitle,
                BookName = refAuthor.BookName,
                Info = refAuthor.Info,
                Page1 = refAuthor.Page1,
                Publisher = refAuthor.Publisher,
                PublicationPlace = refAuthor.PublicationPlace,
                ISBN = refAuthor.ISBN,
                Notes = refAuthor.Notes,
                Memo = refAuthor.Memo
            });

            RefAuthorsView = CollectionViewSource.GetDefaultView(Tbl90RefAuthorsList);
            RefAuthorsView.MoveCurrentToFirst();
        }
        //---------------------------------------------------------------------------------------                            
        
        private void DeleteRefAuthor(object o)
        {
            if (CurrentTbl90RefAuthor == null)
            {
                WpfMessageBox.Show(CultRes.StringsRes.DatasetNew,
                    CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                return;
            }
            _businessLayer = new BusinessLayer.BusinessLayer();

            try
            {
                var refAuthor = _businessLayer.SingleListTbl90RefAuthorsByRefAuthorId(CurrentTbl90RefAuthor.RefAuthorID);
                if (refAuthor != null)
                {
                    if (WpfMessageBox.Show(CultRes.StringsRes.DeleteQuestion1, CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl90RefAuthor.RefAuthorName,
                            MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes)
                        return;
                    refAuthor.EntityState = EntityState.Deleted;
                    _businessLayer.RemoveRefAuthor(refAuthor);

                    WpfMessageBox.Show(CultRes.StringsRes.DeleteSuccess, CurrentTbl90RefAuthor.RefAuthorName,
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                }
                else
                {
                    WpfMessageBox.Show(CultRes.StringsRes.Information, CultRes.StringsRes.DeleteCan + " " + CurrentTbl90RefAuthor.RefAuthorName + " " + CultRes.StringsRes.DeleteCan1,
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                }
            }
            catch (DbEntityValidationException ex)
            {
                _entityException.EntityException(ex);
                    Log.Error(ex);
            }

            if (SearchRefAuthorName != "")
            {
                if (SearchRefAuthorName == "*")  //show all datasets
                {
                    SearchRefAuthorName = "";
                    Tbl90RefAuthorsList.Clear();
                    
                Tbl90RefAuthorsList = new ObservableCollection<Tbl90RefAuthor>(_businessLayer.ListTbl90RefAuthorsByRefAuthorName(SearchRefAuthorName));            
                    SearchRefAuthorName = "*";
                }
                else
                {               
                    Tbl90RefAuthorsList =  new ObservableCollection<Tbl90RefAuthor>(_businessLayer.ListTbl90RefAuthorsByRefAuthorName(SearchRefAuthorName));

                }
                RefAuthorsView = CollectionViewSource.GetDefaultView(Tbl90RefAuthorsList);
                RefAuthorsView.Refresh();
            }
            else  //SearchName = empty
            {
                Tbl90RefAuthorsList = new ObservableCollection<Tbl90RefAuthor>(_businessLayer.ListTbl90RefAuthorsByRefAuthorName(SearchRefAuthorName));

                RefAuthorsView = CollectionViewSource.GetDefaultView(Tbl90RefAuthorsList);
                RefAuthorsView.MoveCurrentToFirst();
             }
        }
        //-------------------------------------------------------------------------------------------------                    
       
        private void SaveRefAuthor(object o)
        {
            if (CurrentTbl90RefAuthor == null)
            {
                WpfMessageBox.Show(CultRes.StringsRes.DatasetNew,
                    CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                return;
            }
            _businessLayer = new BusinessLayer.BusinessLayer();

            try
            {
                var refAuthor = _businessLayer.SingleListTbl90RefAuthorsByRefAuthorId(CurrentTbl90RefAuthor.RefAuthorID);
                if (CurrentTbl90RefAuthor.RefAuthorID != 0)
                {
                    if (refAuthor != null) //update
                    {
                            refAuthor.RefAuthorName = CurrentTbl90RefAuthor.RefAuthorName;
                            refAuthor.Valid = CurrentTbl90RefAuthor.Valid;
                            refAuthor.ValidYear = CurrentTbl90RefAuthor.ValidYear;
                            refAuthor.PublicationYear = CurrentTbl90RefAuthor.PublicationYear;
                            refAuthor.ArticelTitle = CurrentTbl90RefAuthor.ArticelTitle;
                            refAuthor.BookName = CurrentTbl90RefAuthor.BookName;
                            refAuthor.Info = CurrentTbl90RefAuthor.Info;
                            refAuthor.Page1 = CurrentTbl90RefAuthor.Page1;
                            refAuthor.Publisher = CurrentTbl90RefAuthor.Publisher;
                            refAuthor.PublicationPlace = CurrentTbl90RefAuthor.PublicationPlace;
                            refAuthor.ISBN = CurrentTbl90RefAuthor.ISBN;
                            refAuthor.Notes = CurrentTbl90RefAuthor.Notes;
                            refAuthor.Updater = Environment.UserName;
                            refAuthor.UpdaterDate = DateTime.Now;
                            refAuthor.Memo = CurrentTbl90RefAuthor.Memo;
                            refAuthor.EntityState = EntityState.Modified;
                    }
                }
                else
                {
                            refAuthor = new Tbl90RefAuthor   //add new
                            {
                            RefAuthorName = CurrentTbl90RefAuthor.RefAuthorName,
                            CountID = RandomHelper.Randomnumber(),
                            Valid = CurrentTbl90RefAuthor.Valid,
                            ValidYear = CurrentTbl90RefAuthor.ValidYear,
                            PublicationYear = CurrentTbl90RefAuthor.PublicationYear,
                            ArticelTitle = CurrentTbl90RefAuthor.ArticelTitle,
                            BookName = CurrentTbl90RefAuthor.BookName,
                            Info = CurrentTbl90RefAuthor.Info,
                            Page1 = CurrentTbl90RefAuthor.Page1,
                            Publisher = CurrentTbl90RefAuthor.Publisher,
                            PublicationPlace = CurrentTbl90RefAuthor.PublicationPlace,
                            ISBN = CurrentTbl90RefAuthor.ISBN,
                            Notes = CurrentTbl90RefAuthor.Notes,
                            Writer = Environment.UserName,
                            WriterDate = DateTime.Now,
                            Updater = Environment.UserName,
                            UpdaterDate = DateTime.Now,
                            Memo = CurrentTbl90RefAuthor.Memo,
                            EntityState = EntityState.Added
                    };
                }
                {
                    //check if dataset with RefAuthorName and ArticelTitle and BookName and Page 1 and Publisher and PublicationPlace already exist       
                    var dataset = _businessLayer.ListTbl90RefAuthorsByRefAuthorNameAndArticelTitleAndBookNameAndPage1AndPublisherAndPublicationPlace(CurrentTbl90RefAuthor.RefAuthorName, CurrentTbl90RefAuthor.ArticelTitle, CurrentTbl90RefAuthor.BookName,  CurrentTbl90RefAuthor.Page1, CurrentTbl90RefAuthor.Publisher, CurrentTbl90RefAuthor.PublicationPlace);

                    if (dataset.Count != 0 && CurrentTbl90RefAuthor.RefAuthorID == 0)  //dataset exist
                    {
                        WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTbl90RefAuthor.RefAuthorName,
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        return;
                    }
                    if (dataset.Count == 0 && CurrentTbl90RefAuthor.RefAuthorID == 0 ||
                        dataset.Count != 0 && CurrentTbl90RefAuthor.RefAuthorID != 0 ||
                        dataset.Count == 0 && CurrentTbl90RefAuthor.RefAuthorID != 0) //new dataset and update
                    {
                        if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTbl90RefAuthor.RefAuthorName,
                                MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes)
                            return;
                        {
                            try
                            {
                                _businessLayer.UpdateRefAuthor(refAuthor);
                                _position = RefAuthorsView.CurrentPosition;
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
                                        CurrentTbl90RefAuthor.RefAuthorID == 0
                                            ? CultRes.StringsRes.DatasetNew
                                            : CurrentTbl90RefAuthor.RefAuthorName,
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

            if (SearchRefAuthorName != "")
            {
                if (SearchRefAuthorName == "*")  //show all datasets
                {
                    SearchRefAuthorName = "";
                    Tbl90RefAuthorsList.Clear();
                    
                Tbl90RefAuthorsList = new ObservableCollection<Tbl90RefAuthor>(_businessLayer.ListTbl90RefAuthorsByRefAuthorName(SearchRefAuthorName));            
                    SearchRefAuthorName = "*";
                }
                else
                {               
                    Tbl90RefAuthorsList = int.TryParse(SearchRefAuthorName, out var id)
                        ? new ObservableCollection<Tbl90RefAuthor>(_businessLayer.ListTbl90RefAuthorsByRefAuthorId(id))
                        : new ObservableCollection<Tbl90RefAuthor>(_businessLayer.ListTbl90RefAuthorsByRefAuthorName(SearchRefAuthorName));

                }
                RefAuthorsView = CollectionViewSource.GetDefaultView(Tbl90RefAuthorsList);
                RefAuthorsView.MoveCurrentToPosition(_position);
            }
            else  
            {
                Tbl90RefAuthorsList = new ObservableCollection<Tbl90RefAuthor>(_businessLayer.ListTbl90RefAuthorsByRefAuthorName(CurrentTbl90RefAuthor.RefAuthorName));

                RefAuthorsView= CollectionViewSource.GetDefaultView(Tbl90RefAuthorsList);
                RefAuthorsView.Refresh();
            }
        }
        #endregion "Public Commands"                   
 
 

 //    Part 2    

                                                          

 //    Part 3    

                                                          



 //    Part 4    

                                                          


 //    Part 5    

                                                          
                      
 //    Part 6    

 
            

 //    Part 7    

 

 //    Part 8    

 
             
 //    Part 9    

 

 //    Part 10    

 

 //    Part 11    

     
        #region "Public Properties Tbl90RefAuthor"

        private string _searchRefAuthorName = "";
        public string SearchRefAuthorName
        {
            get => _searchRefAuthorName; 
            set { _searchRefAuthorName = value; RaisePropertyChanged();  }
        }

        public  ICollectionView RefAuthorsView;
        private   Tbl90RefAuthor CurrentTbl90RefAuthor => RefAuthorsView?.CurrentItem as Tbl90RefAuthor;

        private ObservableCollection<Tbl90RefAuthor> _tbl90RefAuthorsList;
        public  ObservableCollection<Tbl90RefAuthor> Tbl90RefAuthorsList
        {
            get => _tbl90RefAuthorsList; 
            set {  _tbl90RefAuthorsList = value; RaisePropertyChanged();   }
        }

        private ObservableCollection<Tbl90RefAuthor> _tbl90RefAuthorsAllList;
        public  ObservableCollection<Tbl90RefAuthor> Tbl90RefAuthorsAllList
        {
            get => _tbl90RefAuthorsAllList; 
            set {  _tbl90RefAuthorsAllList = value; RaisePropertyChanged();   }
        }

        #endregion "Public Properties"   
 

 



   }
}   
