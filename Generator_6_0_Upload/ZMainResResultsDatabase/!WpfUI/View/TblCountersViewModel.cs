using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using WPFUI.Helper;
using WPFUI.Models;
using MessageBoxImage = System.Windows.MessageBoxImage;

    
         //    TblCountersViewModel Skriptdatum:  3.1.2012  12:32      

namespace WPFUI.Views.Database
{     
    
    public class TblCountersViewModel : Tbl03RegnumsViewModel
    {     
    
       #region "Private Data Members"  

   
        private readonly Repository<TblCounter, int> _tblCountersRepository = new Repository<TblCounter, int>();   
          
        private readonly Repository<Tbl90Reference, int> _tbl90ReferencesRepository = new Repository<Tbl90Reference, int>();
        private readonly Repository<Tbl90RefAuthor, int> _tbl90RefAuthorsRepository = new Repository<Tbl90RefAuthor, int>();
        private readonly Repository<Tbl90RefSource, int> _tbl90RefSourcesRepository = new Repository<Tbl90RefSource, int>();
        private readonly Repository<Tbl90RefExpert, int> _tbl90RefExpertsRepository = new Repository<Tbl90RefExpert, int>();
        private readonly Repository<Tbl93Comment, int> _tbl93CommentsRepository = new Repository<Tbl93Comment, int>();    

        #endregion "Private Data Members"               
    
        #region "Constructor"

        public TblCountersViewModel()
        {
            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.
            }
            else
            {
                // Code runs "for real"
            }
        }
        private new bool IsInDesignMode { get; set; }

        #endregion "Constructor"           
 

 //    Part 1    

           
        #region "Public Commands Basic TblCounter"

        private RelayCommand _getCounterByNameOrIdCommand;     
    
        public ICommand GetCounterByNameOrIdCommand    
    
        {
            get { return _getCounterByNameOrIdCommand ?? (_getCounterByNameOrIdCommand = new RelayCommand(delegate { GetCounterByNameOrId(null); })); }   
        }

        private void GetCounterByNameOrId(object o)       
        {   
    
            int id;
            if (int.TryParse(SearchCounterName, out id))
                TblCountersList = new ObservableCollection<TblCounter> { _tblCountersRepository.Get(id) };
            else
                TblCountersList =  new ObservableCollection<TblCounter>
                                                       (from x in _tblCountersRepository.GetAll()
                                                        where x.CounterName.StartsWith(SearchCounterName)
                                                        orderby x.CounterName
                                                        select x);

            if (TblCountersAllList == null)
                TblCountersAllList =  new ObservableCollection<TblCounter>
                                                       (from y in _tblCountersRepository.GetAll()
                                                        orderby y.CounterName
                                                        select y);             
  

            Tbl93CommentsList = null;
            Tbl90RefExpertsList = null;
            Tbl90RefAuthorsList = null;
            Tbl90RefSourcesList = null; 
            View = CollectionViewSource.GetDefaultView(TblCountersList);
            View.Refresh();
        }
        //------------------------------------------------------------------------------                
           
        private RelayCommand _addCounterCommand;           
    
        public ICommand AddCounterCommand       
    
        {
            get { return _addCounterCommand ?? (_addCounterCommand = new RelayCommand(delegate { AddCounter(null); })); }
        }

        private void AddCounter(object o)
        {
            TblCountersList = new ObservableCollection<TblCounter>();   
TblCountersList.Insert(0, new TblCounter{ CounterName= CultRes.StringsRes.DatasetNew });  

            if (TblCountersAllList == null)
                TblCountersAllList =  new ObservableCollection<TblCounter>
                                                       (from y in _tblCountersRepository.GetAll()
                                                        orderby y.CounterName
                                                        select y);               
  View = CollectionViewSource.GetDefaultView(TblCountersList);
            View.Refresh();
        }
        //---------------------------------------------------------------------------------------  
           
        private RelayCommand _copyCounterCommand;              
    
        public ICommand CopyCounterCommand             
         
        {
            get { return _copyCounterCommand ?? (_copyCounterCommand = new RelayCommand(delegate { CopyCounter(null); })); }
        }

        private void CopyCounter(object o)
        {
            TblCountersList = new ObservableCollection<TblCounter>();

            var counter = _tblCountersRepository.Get(CurrentTblCounter.CounterID);

            TblCountersList.Insert(0, new TblCounter
            {                 
NULL = counter.NULL,              
                            CounterName = CultRes.StringsRes.DatasetNew,              
                            Valid = counter.Valid,
                            ValidYear = counter.ValidYear,
                            Synonym = counter.Synonym,
                            Author = counter.Author,
                            AuthorYear = counter.AuthorYear,
                            Info = counter.Info,
                            EngName = counter.EngName,
                            GerName = counter.GerName,
                            FraName = counter.FraName,
                            PorName = counter.PorName,
                            Memo = counter.Memo                    
        
            });

            View = CollectionViewSource.GetDefaultView(TblCountersList);
            View.Refresh();
        }
        //---------------------------------------------------------------------------------------                  
           
        private RelayCommand _deleteCounterCommand;              
    
        public ICommand DeleteCounterCommand             
         
        {
            get { return _deleteCounterCommand ?? (_deleteCounterCommand = new RelayCommand(delegate { DeleteCounter(null); })); }
        }

        private void DeleteCounter(object o)
        {
            try
            {
                var counter = _tblCountersRepository.Get(CurrentTblCounter.CounterID);
                if (counter!= null)
                {
                    if (WpfMessageBox.Show(CultRes.StringsRes.DeleteQuestion1, CultRes.StringsRes.DeleteQuestion + " " + CurrentTblCounter.CounterName,
                         MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes) return;
                    _tblCountersRepository.Delete(counter);
                    _tblCountersRepository.Save();
                    WpfMessageBox.Show(CultRes.StringsRes.DeleteSuccess, CurrentTblCounter.CounterName, MessageBoxButton.OK, MessageBoxImage.Information);
                    if (SearchCounterName == null)                   
                        GetConnectedTablesById(o); //refresh doubleClick                                       
                    else
                    {
                            TblCountersList = new ObservableCollection<TblCounter>
                                                  (from x in _tblCountersRepository.GetAll()
                                                   where x.CounterName.StartsWith(SearchCounterName)       
    
                                                   orderby x.CounterName    
                                                   select x);

                            View = CollectionViewSource.GetDefaultView(TblCountersList);
                            View.Refresh();
                    }
                }
                else
                {
                    WpfMessageBox.Show(CultRes.StringsRes.Information, CultRes.StringsRes.DeleteCan + " " + CurrentTblCounter.CounterName+ " " + CultRes.StringsRes.DeleteCan1,
                         MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                WpfMessageBox.Show(CultRes.StringsRes.Error, ex.Message, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        //-------------------------------------------------------------------------------------------------    
           
        private RelayCommand _saveCounterCommand;              
     
        public ICommand SaveCounterCommand             
         
        {
            get { return _saveCounterCommand ?? (_saveCounterCommand = new RelayCommand(delegate { SaveCounter(null); })); }
        }

        private void SaveCounter(object o)
        {
            try
            {
                var counter = _tblCountersRepository.Get(CurrentTblCounter.CounterID);
                if (CurrentTblCounter == null)              
                    WpfMessageBox.Show(CultRes.StringsRes.Warning, CultRes.StringsRes.DatasetNotExist, MessageBoxButton.OK, MessageBoxImage.Warning);               
                else
                {
                    if (CurrentTblCounter.CounterID!= 0)
                    {
                        if (counter!= null) //update
                        {   
counter.Updater = Environment.UserName;
                            counter.UpdaterDate = DateTime.Now;    
         
                        }
                    }
                    else
                    {
                        _tblCountersRepository.Add(new TblCounter
                        {   
NULL= CurrentTblCounter.NULL,              
                            CounterName= CurrentTblCounter.CounterName,              
                            CountID = RandomHelper.Randomnumber(),
                            Valid = CurrentTblCounter.Valid,
                            ValidYear = CurrentTblCounter.ValidYear,
                            Synonym = CurrentTblCounter.Synonym,
                            Author = CurrentTblCounter.Author,
                            AuthorYear = CurrentTblCounter.AuthorYear,
                            Info = CurrentTblCounter.Info,
                            EngName = CurrentTblCounter.EngName,
                            GerName = CurrentTblCounter.GerName,
                            FraName = CurrentTblCounter.FraName,
                            PorName = CurrentTblCounter.PorName,
                            Writer = Environment.UserName,
                            WriterDate = DateTime.Now,
                            Updater = Environment.UserName,
                            UpdaterDate = DateTime.Now,
                            Memo = CurrentTblCounter.Memo   
         
                        });
                    }
                    {
                        //check about double Name
                        var dataset = new ObservableCollection<TblCounter>
                        (from a in _tblCountersRepository.GetAll()
                         where
                         a.CounterName.Trim() == CurrentTblCounter.CounterName.Trim() &&                
                         a.NULL== CurrentTblCounter.NULL
                         select a);

                        if (dataset.Count == 0 && CurrentTblCounter.CounterID== 0 ||
                            dataset.Count != 0 && CurrentTblCounter.CounterID != 0  ||
                            dataset.Count == 0 && CurrentTblCounter.CounterID != 0) //new dataset and update
                        {
                            if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTblCounter.CounterName,
                                    MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)                            
                                return;                            
                            {
                                _tblCountersRepository.Save();

                                WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess, CurrentTblCounter.CounterName,
                                    MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                        }

                        if (dataset.Count != 0 && CurrentTblCounter.CounterID== 0)  //dataset exist
                        {
                              WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTblCounter.CounterName,
                              MessageBoxButton.OK, MessageBoxImage.Information);
                        }  
         
                        if (SearchCounterName == null)
                            GetConnectedTablesById(o); //refresh doubleClick                                                                   
                        {
                            if (CurrentTblCounter.CounterID== 0)  //new Dataset
                                GetCounterByNameOrId(o); //search                             
                        {
                            TblCountersList = new ObservableCollection<TblCounter>
                                                  (from x in _tblCountersRepository.GetAll()
                                                   where x.CounterID == CurrentTblCounter.CounterID
                                                   select x);

                            View = CollectionViewSource.GetDefaultView(TblCountersList);
                            View.Refresh();
                        }  
         
                           
                       }
                    }
                }
            }
            catch (Exception ex)
            {
                WpfMessageBox.Show(CultRes.StringsRes.Error, ex.Message, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion "Public Commands"  
      

 //    Part 2    

           
        #region "Public Commands Connect <== NULL"                 

        private RelayCommand _getNULLByNameOrIdCommand;     
    
        public ICommand GetNULLByNameOrIdCommand    
    
        {
            get { return _getNULLByNameOrIdCommand ?? (_getNULLByNameOrIdCommand = new RelayCommand(delegate { GetNULLByNameOrId(null); })); }   
        }

        private void GetNULLByNameOrId(object o)       
        {   
    
            int id;
            if (int.TryParse(SearchNULLName, out id))
                NULLList = new ObservableCollection<NULL> { NULLRepository.Get(id) };
            
            NULLList =   new ObservableCollection<NULL>
                                                      (from x in NULLRepository.GetAll()
                                                       where x.NULL.StartsWith(SearchNULLName)
                                                       orderby x.NULL
                                                       select x);         
View = CollectionViewSource.GetDefaultView(NULLList);
            View.Refresh();
        }
        //------------------------------------------------------------------------------                
           
        private RelayCommand _addNULLCommand;      
    
        public ICommand AddNULLCommand    
    
        {
            get { return _addNULLCommand ?? (_addNULLCommand = new RelayCommand(delegate { AddNULL(null); })); }
        }

        private void AddNULL(object o)
        {
            NULLList = new ObservableCollection<NULL>();   
NULLList.Insert(0, new NULL{ NULL= CultRes.StringsRes.DatasetNew });       
View = CollectionViewSource.GetDefaultView(NULLList);
            View.Refresh();
        }
        //---------------------------------------------------------------------------------------  
           
        private RelayCommand _copyNULLCommand;            
    
        public ICommand CopyNULLCommand          
         
        {
            get { return _copyNULLCommand ?? (_copyNULLCommand = new RelayCommand(delegate { CopyNULL(null); })); }
        }

        private void CopyNULL(object o)
        {
            NULLList = new ObservableCollection<NULL>();

            var NULL = NULLRepository.Get(CurrentNULL.NULL);

            NULLList.Insert(0, new NULL
            {                 
NULL = NULL.NULL,     
                NULL = CultRes.StringsRes.DatasetNew,     
                Valid = NULL.Valid,
                ValidYear = NULL.ValidYear,
                Synonym = NULL.Synonym,
                Author = NULL.Author,
                AuthorYear = NULL.AuthorYear,
                Info = NULL.Info,
                EngName = NULL.EngName,
                GerName = NULL.GerName,
                FraName = NULL.FraName,
                PorName = NULL.PorName,
                Memo = NULL.Memo           
        
            });

            View = CollectionViewSource.GetDefaultView(NULLList);
            View.Refresh();
        }
        //---------------------------------------------------------------------------------------                  
           
        private RelayCommand _deleteNULLCommand;              
    
        public ICommand DeleteNULLCommand             
         
        {
            get { return _deleteNULLCommand ?? (_deleteNULLCommand = new RelayCommand(delegate { DeleteNULL(null); })); }
        }

        private void DeleteNULL(object o)
        {
            try
            {
                var NULL = NULLRepository.Get(CurrentNULL.NULL);
                if (NULL!= null)
                {  
         
                    if (WpfMessageBox.Show(CultRes.StringsRes.DeleteQuestion1, CultRes.StringsRes.DeleteQuestion + " " + CurrentNULL.NULL,
                         MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes) return;
                    NULLRepository.Delete(NULL);
                    NULLRepository.Save();
                    WpfMessageBox.Show(CultRes.StringsRes.DeleteSuccess, CurrentNULL.NULL, 
                        MessageBoxButton.OK, MessageBoxImage.Information);  
         
                        if (SearchNULLName == null)                       
                            GetConnectedTablesById(o); //refresh doubleClick                                          
                        else
                        {
                            NULLList = new ObservableCollection<NULL>
                                                  (from x in NULLRepository.GetAll()
                                                   where x.NULL.StartsWith(SearchNULL)   
    
                                                   orderby x.NULL    
                                                   select x);

                            View = CollectionViewSource.GetDefaultView(NULLList);
                            View.Refresh();
                    }
                }
                else
                {   
    
                    WpfMessageBox.Show(CultRes.StringsRes.Information, CultRes.StringsRes.DeleteCan + " " + CurrentNULL.NULL+ " " + CultRes.StringsRes.DeleteCan1,
                         MessageBoxButton.OK, MessageBoxImage.Information);   
    
                }
            }
            catch (Exception ex)
            {
                WpfMessageBox.Show(CultRes.StringsRes.Error, ex.Message, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        //-------------------------------------------------------------------------------------------------    
           
        private RelayCommand _saveNULLCommand;              
    
        public ICommand SaveNULLCommand             
         
        {
            get { return _saveNULLCommand ?? (_saveNULLCommand = new RelayCommand(delegate { SaveNULL(null); })); }
        }

        private void SaveNULL(object o)
        {
            try
            {
                var NULL = NULLRepository.Get(CurrentNULL.NULL);
                if (CurrentNULL == null)              
                    WpfMessageBox.Show(CultRes.StringsRes.Warning, CultRes.StringsRes.DatasetNotExist, MessageBoxButton.OK, MessageBoxImage.Warning);               
                else
                {
                    if (CurrentNULL.NULL!= 0)
                    {
                        if (NULL!= null) //update
                        {   
NULL.Updater = Environment.UserName;
                            NULL.UpdaterDate = DateTime.Now;    
         
                        }
                    }
                    else
                    {
                        NULLRepository.Add(new NULL
                        {   
NULL = CurrentNULL.NULL,     
                            NULL= CurrentNULL.NULL,              
                            CountID = RandomHelper.Randomnumber(),
                            Valid = CurrentNULL.Valid,
                            ValidYear = CurrentNULL.ValidYear,
                            Synonym = CurrentNULL.Synonym,
                            Author = CurrentNULL.Author,
                            AuthorYear = CurrentNULL.AuthorYear,
                            Info = CurrentNULL.Info,
                            EngName = CurrentNULL.EngName,
                            GerName = CurrentNULL.GerName,
                            FraName = CurrentNULL.FraName,
                            PorName = CurrentNULL.PorName,
                            Writer = Environment.UserName,
                            WriterDate = DateTime.Now,
                            Updater = Environment.UserName,
                            UpdaterDate = DateTime.Now,
                            Memo = CurrentNULL.Memo   
         
                        });
                    }
                    {
                        //check about double Name   
                        var dataset = new ObservableCollection<NULL>
                        (from a in NULLRepository.GetAll()
                         where
                         a.NULL.Trim() == CurrentNULL.NULL.Trim() &&                
                         a.NULL== CurrentNULL.NULL
                         select a);

                        if (dataset.Count == 0 && CurrentNULL.NULL== 0 ||
                            dataset.Count != 0 && CurrentNULL.NULL != 0  ||
                            dataset.Count == 0 && CurrentNULL.NULL != 0) //new dataset and update
                        {
                            if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentNULL.NULL,
                                    MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)                            
                                return;                            
                            {
                                NULLRepository.Save();

                                WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess, CurrentNULL.NULL,
                                    MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                        }

                        if (dataset.Count != 0 && CurrentNULL.NULL== 0)  //dataset exist
                        {
                              WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentNULL.NULL,
                              MessageBoxButton.OK, MessageBoxImage.Information);
                        }  
         
                        if (SearchNULL == null)
                            GetConnectedTablesById(o); //refresh doubleClick                                                                  
                        {
                            if (CurrentNULL.NULL== 0)  //new Dataset
                                GetNULLByNameOrId(o); //search      
                        
                        {
                                NULLList = new ObservableCollection<NULL>
                                                  (from x in NULLRepository.GetAll()
                                                   where x.NULL == CurrentNULL.NULL
                                                   select x);

                                View = CollectionViewSource.GetDefaultView(NULLList);
                                View.Refresh();
                        }  
      
                        }   
                    }
                }
            }
            catch (Exception ex)
            {
                WpfMessageBox.Show(CultRes.StringsRes.Error, ex.Message, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion "Public Commands"  
      

 //    Part 3    

      

 //    Part 4    

      

 //    Part 5    


      

 //    Part 6    

      

 //    Part 7    

      

 //    Part 8    

           
        #region "Public Commands Connect ==> Tbl90RefAuthor"
        private RelayCommand _getRefAuthorByNameOrIdCommand;    
    
        public new ICommand GetRefAuthorByNameOrIdCommand  
    
        {
            get { return _getRefAuthorByNameOrIdCommand ?? (_getRefAuthorByNameOrIdCommand = new RelayCommand(delegate { GetRefAuthorByNameOrId(null); })); }
        }

        public new void GetRefAuthorByNameOrId(object o)
        {   
    
            int id;
            if (int.TryParse(SearchRefAuthorName, out id))
                Tbl90RefAuthorsList = new ObservableCollection<Tbl90Reference> { _tbl90ReferencesRepository.Get(id) };
            
            Tbl90RefAuthorsList = new ObservableCollection<Tbl90Reference>
                                (from refAuthor in _tbl90ReferencesRepository.GetAll()
                                 where refAuthor.Tbl90RefAuthors.RefAuthorName.StartsWith(SearchRefAuthorName)
                                       && refAuthor.Tbl90RefExperts == null
                                       && refAuthor.Tbl90RefSources == null
                                 orderby refAuthor.Tbl90RefAuthors.RefAuthorName, refAuthor.Tbl90RefAuthors.BookName, refAuthor.Tbl90RefAuthors.Page1
                                 select refAuthor);       
     
            RefAuthorsView = CollectionViewSource.GetDefaultView(Tbl90RefAuthorsList);
            RefAuthorsView.Refresh();
        }
        //------------------------------------------------------------------------------                
           
        private RelayCommand _addRefAuthorCommand;         
    
        public new ICommand AddRefAuthorCommand      
    
        {
            get { return _addRefAuthorCommand ?? (_addRefAuthorCommand = new RelayCommand(delegate { AddRefAuthor(null); })); }
        }

        public new void AddRefAuthor(object o)
        {
            Tbl90RefAuthorsList = new ObservableCollection<Tbl90Reference>();  
    
            Tbl90RefAuthorsList.Insert(0, new Tbl90Reference { Info = CultRes.StringsRes.DatasetNew });     
    

            RefAuthorsView = CollectionViewSource.GetDefaultView(Tbl90RefAuthorsList);
            RefAuthorsView.Refresh();
        }
        //---------------------------------------------------------------------------------------  
           
        private RelayCommand _copyRefAuthorCommand;            
    
        public new ICommand CopyRefAuthorCommand       
         
        {
            get { return _copyRefAuthorCommand ?? (_copyRefAuthorCommand = new RelayCommand(delegate { CopyRefAuthor(null); })); }
        }

        public new void CopyRefAuthor(object o)
        {
            Tbl90RefAuthorsList = new ObservableCollection<Tbl90Reference>();

            var refAuthor = _tbl90ReferencesRepository.Get(CurrentTbl90RefAuthor.ReferenceID);

            Tbl90RefAuthorsList.Insert(0, new Tbl90Reference
            {                 
CounterID = refAuthor.CounterID,              
                Info = CultRes.StringsRes.DatasetNew,
                Valid = refAuthor.Valid,
                ValidYear = refAuthor.ValidYear,
                Memo = refAuthor.Memo          
        
            });

            RefAuthorsView = CollectionViewSource.GetDefaultView(Tbl90RefAuthorsList);
            RefAuthorsView.Refresh();
        }
        //---------------------------------------------------------------------------------------                  
           
        private RelayCommand _deleteRefAuthorCommand;             
    
        public new ICommand DeleteRefAuthorCommand          
         
        {
            get { return _deleteRefAuthorCommand ?? (_deleteRefAuthorCommand = new RelayCommand(delegate { DeleteRefAuthor(null); })); }
        }

        public new void DeleteRefAuthor(object o)
        {
            try
            {
                var refAuthor = _tbl90ReferencesRepository.Get(CurrentTbl90RefAuthor.ReferenceID);
                if (refAuthor != null)
                {
                    if (WpfMessageBox.Show(CultRes.StringsRes.DeleteQuestion1, CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl90RefAuthor.Info,
                         MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes) return;
                    _tbl90ReferencesRepository.Delete(refAuthor);
                    _tbl90ReferencesRepository.Save();
                    WpfMessageBox.Show(CultRes.StringsRes.DeleteSuccess, CurrentTbl90RefAuthor.Info, MessageBoxButton.OK, MessageBoxImage.Information);
                    if (SearchRefAuthorName == null)                    
                        GetConnectedTablesById(o); //refresh doubleClick                                       
                    else
                    {
                        Tbl90RefAuthorsList = new ObservableCollection<Tbl90Reference>
                            (from refAu in _tbl90ReferencesRepository.GetAll()
                             where refAu.Tbl90RefAuthors.RefAuthorName.StartsWith(SearchRefAuthorName)
                             && refAu.Tbl90RefExperts == null
                             && refAu.Tbl90RefSources == null    
    
                             orderby refAu.Tbl90RefAuthors.RefAuthorName, refAu.Tbl90RefAuthors.BookName, refAu.Tbl90RefAuthors.Page1       
    
                             select refAu);

                        RefAuthorsView = CollectionViewSource.GetDefaultView(Tbl90RefAuthorsList);
                        RefAuthorsView.Refresh();
                    }
                }
                else
                {
                    WpfMessageBox.Show(CultRes.StringsRes.Information, CultRes.StringsRes.DeleteCan + " " + CurrentTbl90RefAuthor.Info + " " + CultRes.StringsRes.DeleteCan1,
                         MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                WpfMessageBox.Show(CultRes.StringsRes.Error, ex.Message, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        //-------------------------------------------------------------------------------------------------    
           
        private RelayCommand _saveRefAuthorCommand;            
    
        public new ICommand SaveRefAuthorCommand           
         
        {
            get { return _saveRefAuthorCommand ?? (_saveRefAuthorCommand = new RelayCommand(delegate { SaveRefAuthor(null); })); }
        }

        public new void SaveRefAuthor(object o)
        {
            try
            {
                var refAuthor = _tbl90ReferencesRepository.Get(CurrentTbl90RefAuthor.ReferenceID);
                if (CurrentTbl90RefAuthor == null)               
                    WpfMessageBox.Show(CultRes.StringsRes.Warning, CultRes.StringsRes.DatasetNotExist, MessageBoxButton.OK, MessageBoxImage.Warning);               
                else
                {
                    if (CurrentTbl90RefAuthor.ReferenceID != 0)
                    {
                        if (refAuthor != null)  //update
                        {   
         
                            refAuthor.Updater = Environment.UserName;
                            refAuthor.UpdaterDate = DateTime.Now;    
         
                        }
                    }
                    else
                    {
                        _tbl90ReferencesRepository.Add(new Tbl90Reference
                        {   
CounterID = CurrentTbl90RefAuthor.CounterID,              
                            RefAuthorID = CurrentTbl90RefAuthor.RefAuthorID,
                            CountID = RandomHelper.Randomnumber(),
                            Valid = CurrentTbl90RefAuthor.Valid,
                            ValidYear = CurrentTbl90RefAuthor.ValidYear,
                            Info = CurrentTbl90RefAuthor.Info,
                            Writer = Environment.UserName,
                            WriterDate = DateTime.Now,
                            Updater = Environment.UserName,
                            UpdaterDate = DateTime.Now,
                            Memo = CurrentTbl90RefAuthor.Memo   
         
                        });
                    }
                    {
                        //check about double Name   
                        var dataset = new ObservableCollection<Tbl90Reference>
                        (from a in _tbl90ReferencesRepository.GetAll()
                         where
                         a.Info.Trim() == CurrentTbl90RefAuthor.Info.Trim() &&                
                         a.CounterID == CurrentTbl90RefAuthor.CounterID
                         select a);

                        if (dataset.Count == 0 && CurrentTbl90RefAuthor.ReferenceID == 0 ||
                            dataset.Count != 0 && CurrentTbl90RefAuthor.ReferenceID != 0  ||
                            dataset.Count == 0 && CurrentTbl90RefAuthor.ReferenceID != 0) //new dataset and update
                        {
                            if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTbl90RefAuthor.Info,
                                    MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)                            
                                return;                            
                            {
                        _tbl90ReferencesRepository.Save();

                                WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess, CurrentTbl90RefAuthor.Info,
                                    MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                        }

                        if (dataset.Count != 0 && CurrentTbl90RefAuthor.ReferenceID == 0)  //dataset exist
                        {
                              WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTbl90RefAuthor.Info,
                              MessageBoxButton.OK, MessageBoxImage.Information);
                        }  
      
                        if (SearchRefAuthorName == null)                       
                            GetConnectedTablesById(o); //refresh doubleClick                                            
                        {
                            if (CurrentTbl90RefAuthor.ReferenceID == 0)  //new Dataset
                                GetRefAuthorByNameOrId(o); //search      
                        
                        {
                            Tbl90RefAuthorsList = new ObservableCollection<Tbl90Reference>
                                (from refAu in _tbl90ReferencesRepository.GetAll()
                                 where refAu.RefAuthorID == CurrentTbl90RefAuthor.RefAuthorID
                                 select refAu);

                            RefAuthorsView = CollectionViewSource.GetDefaultView(Tbl90RefAuthorsList);
                            RefAuthorsView.Refresh();
                        }       
        
                        } 
                    }
                }
            }
            catch (Exception ex)
            {
                WpfMessageBox.Show(CultRes.StringsRes.Error, ex.Message, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion "Public Commands"  
           
        #region "Public Commands Connect ==> Tbl90RefSource"

        private RelayCommand _getRefSourceByNameOrIdCommand;   
    
        public new ICommand GetRefSourceByNameOrIdCommand  
    
        {
            get { return _getRefSourceByNameOrIdCommand ?? (_getRefSourceByNameOrIdCommand = new RelayCommand(delegate { GetRefSourceByNameOrId(null); })); }
        }

        public new void GetRefSourceByNameOrId(object o)
        {   
    
            int id;
            if (int.TryParse(SearchRefSourceName, out id))
                Tbl90RefSourcesList = new ObservableCollection<Tbl90Reference> { _tbl90ReferencesRepository.Get(id) };
          
            Tbl90RefSourcesList = new ObservableCollection<Tbl90Reference>
                                (from refSource in _tbl90ReferencesRepository.GetAll()
                                 where refSource.Tbl90RefSources.RefSourceName.StartsWith(SearchRefSourceName)
                                                          && refSource.Tbl90RefExperts == null
                                                          && refSource.Tbl90RefAuthors == null
                                 orderby refSource.Tbl90RefSources.RefSourceName, refSource.Tbl90RefSources.Author, refSource.Tbl90RefSources.SourceYear
                                 select refSource);     
     
            RefSourcesView = CollectionViewSource.GetDefaultView(Tbl90RefSourcesList);
            RefSourcesView.Refresh();
        }
        //------------------------------------------------------------------------------                
           
        private RelayCommand _addRefSourceCommand;         
    
        public new ICommand AddRefSourceCommand      
    
        {
            get { return _addRefSourceCommand ?? (_addRefSourceCommand = new RelayCommand(delegate { AddRefSource(null); })); }
        }

        public new void AddRefSource(object o)
        {
            Tbl90RefSourcesList = new ObservableCollection<Tbl90Reference>();  
    
            Tbl90RefSourcesList.Insert(0, new Tbl90Reference { Info = CultRes.StringsRes.DatasetNew });     
    

            RefSourcesView = CollectionViewSource.GetDefaultView(Tbl90RefSourcesList);
            RefSourcesView.Refresh();
        }
        //---------------------------------------------------------------------------------------  
           
        private RelayCommand _copyRefSourceCommand;            
    
        public new ICommand CopyRefSourceCommand       
         
        {
            get { return _copyRefSourceCommand ?? (_copyRefSourceCommand = new RelayCommand(delegate { CopyRefSource(null); })); }
        }

        public new void CopyRefSource(object o)
        {
            Tbl90RefSourcesList = new ObservableCollection<Tbl90Reference>();

            var refSource = _tbl90ReferencesRepository.Get(CurrentTbl90RefSource.ReferenceID);

            Tbl90RefSourcesList.Insert(0, new Tbl90Reference
            {                 
CounterID = refSource.CounterID,              
                Info = CultRes.StringsRes.DatasetNew,
                Valid = refSource.Valid,
                ValidYear = refSource.ValidYear,
                Memo = refSource.Memo          
        
            });

            RefSourcesView = CollectionViewSource.GetDefaultView(Tbl90RefSourcesList);
            RefSourcesView.Refresh();
        }
        //---------------------------------------------------------------------------------------                  
           
        private RelayCommand _deleteRefSourceCommand;             
    
        public new ICommand DeleteRefSourceCommand          
         
        {
            get { return _deleteRefSourceCommand ?? (_deleteRefSourceCommand = new RelayCommand(delegate { DeleteRefSource(null); })); }
        }

        public new void DeleteRefSource(object o)
        {
            try
            {
                var refSource = _tbl90ReferencesRepository.Get(CurrentTbl90RefSource.ReferenceID);
                if (refSource != null)
                {
                    if (WpfMessageBox.Show(CultRes.StringsRes.DeleteQuestion1, CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl90RefSource.Info,
                         MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes) return;
                    _tbl90ReferencesRepository.Delete(refSource);
                    _tbl90ReferencesRepository.Save();
                    WpfMessageBox.Show(CultRes.StringsRes.DeleteSuccess, CurrentTbl90RefSource.Info, MessageBoxButton.OK, MessageBoxImage.Information);
                    if (SearchRefSourceName == null)                   
                        GetConnectedTablesById(o); //refresh doubleClick                                       
                    else
                    {
                        Tbl90RefSourcesList = new ObservableCollection<Tbl90Reference>
                                        (from refSo in _tbl90ReferencesRepository.GetAll()
                                         where refSo.Tbl90RefSources.RefSourceName.StartsWith(SearchRefSourceName)
                                              && refSo.Tbl90RefExperts == null
                                              && refSo.Tbl90RefAuthors == null  
    
                                         orderby refSo.Tbl90RefSources.RefSourceName, refSo.Tbl90RefSources.Author, refSo.Tbl90RefSources.SourceYear   
    
                                         select refSo);

                        RefSourcesView = CollectionViewSource.GetDefaultView(Tbl90RefSourcesList);
                        RefSourcesView.Refresh();
                    }
                }
                else
                {
                    WpfMessageBox.Show(CultRes.StringsRes.Information, CultRes.StringsRes.DeleteCan + " " + CurrentTbl90RefSource.Info + " " + CultRes.StringsRes.DeleteCan1,
                         MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                WpfMessageBox.Show(CultRes.StringsRes.Error, ex.Message, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        //-------------------------------------------------------------------------------------------------    
           
        private RelayCommand _saveRefSourceCommand;            
    
        public new ICommand SaveRefSourceCommand           
         
        {
            get { return _saveRefSourceCommand ?? (_saveRefSourceCommand = new RelayCommand(delegate { SaveRefSource(null); })); }
        }

        public new void SaveRefSource(object o)
        {
            try
            {
                var refSource = _tbl90ReferencesRepository.Get(CurrentTbl90RefSource.ReferenceID);
                if (CurrentTbl90RefSource == null)                
                    WpfMessageBox.Show(CultRes.StringsRes.Warning, CultRes.StringsRes.DatasetNotExist, MessageBoxButton.OK, MessageBoxImage.Warning);                
                else
                {
                    if (CurrentTbl90RefSource.ReferenceID != 0)
                    {
                        if (refSource != null)  //update
                        {   
         
                            refSource.Updater = Environment.UserName;
                            refSource.UpdaterDate = DateTime.Now;    
         
                        }
                    }
                    else
                    {
                        _tbl90ReferencesRepository.Add(new Tbl90Reference
                        {   
CounterID = CurrentTbl90RefSource.CounterID,              
                            RefSourceID = CurrentTbl90RefSource.RefSourceID,
                            CountID = RandomHelper.Randomnumber(),
                            Valid = CurrentTbl90RefSource.Valid,
                            ValidYear = CurrentTbl90RefSource.ValidYear,
                            Info = CurrentTbl90RefSource.Info,
                            Writer = Environment.UserName,
                            WriterDate = DateTime.Now,
                            Updater = Environment.UserName,
                            UpdaterDate = DateTime.Now,
                            Memo = CurrentTbl90RefSource.Memo   
         
                        });
                    }
                    {
                        //check about double Name   
                        var dataset = new ObservableCollection<Tbl90Reference>
                        (from a in _tbl90ReferencesRepository.GetAll()
                         where
                         a.Info.Trim() == CurrentTbl90RefSource.Info.Trim() &&                
                         a.CounterID == CurrentTbl90RefSource.CounterID
                         select a);

                        if (dataset.Count == 0 && CurrentTbl90RefSource.ReferenceID == 0 ||
                            dataset.Count != 0 && CurrentTbl90RefSource.ReferenceID != 0  ||
                            dataset.Count == 0 && CurrentTbl90RefSource.ReferenceID != 0) //new dataset and update
                        {
                            if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTbl90RefSource.Info,
                                    MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)                            
                                return;                            
                            {
                        _tbl90ReferencesRepository.Save();

                                WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess, CurrentTbl90RefSource.Info,
                                    MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                        }

                        if (dataset.Count != 0 && CurrentTbl90RefSource.ReferenceID == 0)  //dataset exist
                        {
                              WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTbl90RefSource.Info,
                              MessageBoxButton.OK, MessageBoxImage.Information);
                        }   
         
                        if (SearchRefSourceName == null)                       
                            GetConnectedTablesById(o); //refresh doubleClick                                            
                        {
                            if (CurrentTbl90RefSource.ReferenceID == 0)  //new Dataset
                                GetRefSourceByNameOrId(o); //search      
                        
                        {
                            Tbl90RefSourcesList = new ObservableCollection<Tbl90Reference>
                                            (from refSo in _tbl90ReferencesRepository.GetAll()
                                             where refSo.RefSourceID == CurrentTbl90RefSource.RefSourceID
                                             select refSo);

                            RefSourcesView = CollectionViewSource.GetDefaultView(Tbl90RefSourcesList);
                            RefSourcesView.Refresh();
                        }       
         
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                WpfMessageBox.Show(CultRes.StringsRes.Error, ex.Message, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion "Public Commands"  
           
        #region "Public Commands Connect ==> Tbl90RefExpert"

        private RelayCommand _getRefExpertByNameOrIdCommand;   
    
        public new ICommand GetRefExpertByNameOrIdCommand  
    
        {
            get { return _getRefExpertByNameOrIdCommand ?? (_getRefExpertByNameOrIdCommand = new RelayCommand(delegate { GetRefExpertByNameOrId(null); })); }
        }

        public new void GetRefExpertByNameOrId(object o)
        {   
    
            int id;
            if (int.TryParse(SearchRefExpertName, out id))
                Tbl90RefExpertsList = new ObservableCollection<Tbl90Reference> { _tbl90ReferencesRepository.Get(id) };
            else
                Tbl90RefExpertsList = new ObservableCollection<Tbl90Reference>
                                (from refExpert in _tbl90ReferencesRepository.GetAll()
                                 where refExpert.Tbl90RefExperts.RefExpertName.StartsWith(SearchRefExpertName)
                                 && refExpert.Tbl90RefSources == null
                                 && refExpert.Tbl90RefAuthors == null
                                 orderby refExpert.Tbl90RefExperts.RefExpertName, refExpert.Tbl90RefExperts.Info
                                 select refExpert);              
     
            RefExpertsView = CollectionViewSource.GetDefaultView(Tbl90RefExpertsList);
            RefExpertsView.Refresh();
        }
        //------------------------------------------------------------------------------                
           
        private RelayCommand _addRefExpertCommand;         
    
        public new ICommand AddRefExpertCommand      
    
        {
            get { return _addRefExpertCommand ?? (_addRefExpertCommand = new RelayCommand(delegate { AddRefExpert(null); })); }
        }

        public new void AddRefExpert(object o)
        {
            Tbl90RefExpertsList = new ObservableCollection<Tbl90Reference>();      
    
            Tbl90RefExpertsList.Insert(0, new Tbl90Reference { Info = CultRes.StringsRes.DatasetNew });    
    

            RefExpertsView = CollectionViewSource.GetDefaultView(Tbl90RefExpertsList);
            RefExpertsView.Refresh();
        }
        //---------------------------------------------------------------------------------------  
           
        private RelayCommand _copyRefExpertCommand;            
    
        public new ICommand CopyRefExpertCommand       
         
        {
            get { return _copyRefExpertCommand ?? (_copyRefExpertCommand = new RelayCommand(delegate { CopyRefExpert(null); })); }
        }

        public new void CopyRefExpert(object o)
        {
            Tbl90RefExpertsList = new ObservableCollection<Tbl90Reference>();

            var refExpert = _tbl90ReferencesRepository.Get(CurrentTbl90RefExpert.ReferenceID);

            Tbl90RefExpertsList.Insert(0, new Tbl90Reference
            {                 
CounterID = refExpert.CounterID,              
                Info = CultRes.StringsRes.DatasetNew,
                Valid = refExpert.Valid,
                ValidYear = refExpert.ValidYear,
                Memo = refExpert.Memo          
        
            });

            RefExpertsView = CollectionViewSource.GetDefaultView(Tbl90RefExpertsList);
            RefExpertsView.Refresh();
        }
        //---------------------------------------------------------------------------------------                  
           
        private RelayCommand _deleteRefExpertCommand;             
    
        public new ICommand DeleteRefExpertCommand          
         
        {
            get { return _deleteRefExpertCommand ?? (_deleteRefExpertCommand = new RelayCommand(delegate { DeleteRefExpert(null); })); }
        }

        public new void DeleteRefExpert(object o)
        {
            try
            {
                var refExpert = _tbl90ReferencesRepository.Get(CurrentTbl90RefExpert.ReferenceID);
                if (refExpert != null)
                {
                    if (WpfMessageBox.Show(CultRes.StringsRes.DeleteQuestion1, CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl90RefExpert.Info,
                         MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes) return;
                    _tbl90ReferencesRepository.Delete(refExpert);
                    _tbl90ReferencesRepository.Save();
                    WpfMessageBox.Show(CultRes.StringsRes.DeleteSuccess, CurrentTbl90RefExpert.Info, MessageBoxButton.OK, MessageBoxImage.Information);
                    if (SearchRefExpertName == null)                   
                        GetConnectedTablesById(o); //refresh doubleClick                                       
                    else
                    {
                        Tbl90RefExpertsList = new ObservableCollection<Tbl90Reference>
                                        (from refEx in _tbl90ReferencesRepository.GetAll()
                                         where refEx.Tbl90RefExperts.RefExpertName.StartsWith(SearchRefExpertName)
                                         && refEx.Tbl90RefSources == null
                                         && refEx.Tbl90RefAuthors == null  
    
                                         orderby refEx.Tbl90RefExperts.RefExpertName, refEx.Tbl90RefExperts.Info  
    
                                         select refEx);

                        RefExpertsView = CollectionViewSource.GetDefaultView(Tbl90RefExpertsList);
                        RefExpertsView.Refresh();
                    }
                }
                else
                {
                    WpfMessageBox.Show(CultRes.StringsRes.Information, CultRes.StringsRes.DeleteCan + " " + CurrentTbl90RefExpert.Info + " " + CultRes.StringsRes.DeleteCan1,
                         MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                WpfMessageBox.Show(CultRes.StringsRes.Error, ex.Message, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        //-------------------------------------------------------------------------------------------------    
           
        private RelayCommand _saveRefExpertCommand;            
    
        public new ICommand SaveRefExpertCommand           
         
        {
            get { return _saveRefExpertCommand ?? (_saveRefExpertCommand = new RelayCommand(delegate { SaveRefExpert(null); })); }
        }

        public new void SaveRefExpert(object o)
        {
            try
            {
                var refExpert = _tbl90ReferencesRepository.Get(CurrentTbl90RefExpert.ReferenceID);
                if (CurrentTbl90RefExpert == null)               
                    WpfMessageBox.Show(CultRes.StringsRes.Warning, CultRes.StringsRes.DatasetNotExist, MessageBoxButton.OK, MessageBoxImage.Warning);               
                else
                {
                    if (CurrentTbl90RefExpert.ReferenceID != 0)
                    {
                        if (refExpert != null)	//update
                        {   
         
                            refExpert.Updater = Environment.UserName;
                            refExpert.UpdaterDate = DateTime.Now;    
         
                        }
                    }
                    else
                    {
                        _tbl90ReferencesRepository.Add(new Tbl90Reference
                        {   
CounterID = CurrentTbl90RefExpert.CounterID,              
                            RefExpertID= CurrentTbl90RefExpert.RefExpertID,
                            CountID = RandomHelper.Randomnumber(),
                            Valid = CurrentTbl90RefExpert.Valid,
                            ValidYear = CurrentTbl90RefExpert.ValidYear,
                            Info = CurrentTbl90RefExpert.Info,
                            Writer = Environment.UserName,
                            WriterDate = DateTime.Now,
                            Updater = Environment.UserName,
                            UpdaterDate = DateTime.Now,
                            Memo = CurrentTbl90RefExpert.Memo   
         
                        });
                    }
                    {
                        //check about double Name   
                        var dataset = new ObservableCollection<Tbl90Reference>
                        (from a in _tbl90ReferencesRepository.GetAll()
                         where
                         a.Info.Trim() == CurrentTbl90RefExpert.Info.Trim() &&                
                         a.CounterID == CurrentTbl90RefExpert.CounterID
                         select a);

                        if (dataset.Count == 0 && CurrentTbl90RefExpert.ReferenceID == 0 ||
                            dataset.Count != 0 && CurrentTbl90RefExpert.ReferenceID != 0  ||
                            dataset.Count == 0 && CurrentTbl90RefExpert.ReferenceID != 0) //new dataset and update
                        {
                            if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTbl90RefExpert.Info,
                                    MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)                            
                                return;                            
                            {
                        _tbl90ReferencesRepository.Save();

                                WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess, CurrentTbl90RefExpert.Info,
                                    MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                        }

                        if (dataset.Count != 0 && CurrentTbl90RefExpert.ReferenceID == 0)  //dataset exist
                        {
                              WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTbl90RefExpert.Info,
                              MessageBoxButton.OK, MessageBoxImage.Information);
                        }   
         
                        if (SearchRefExpertName == null)                       
                            GetConnectedTablesById(o); //refresh doubleClick                                            
                        {
                            if (CurrentTbl90RefExpert.ReferenceID == 0)  //new Dataset
                                GetRefExpertByNameOrId(o); //search      
                        
                        {
                            Tbl90RefExpertsList = new ObservableCollection<Tbl90Reference>
                                            (from refEx in _tbl90ReferencesRepository.GetAll()
                                             where refEx.RefExpertID == CurrentTbl90RefExpert.RefExpertID
                                             select refEx);

                            RefExpertsView = CollectionViewSource.GetDefaultView(Tbl90RefExpertsList);
                            RefExpertsView.Refresh();
                        }       
        
                        } 
                    }
                }
            }
            catch (Exception ex)
            {
                WpfMessageBox.Show(CultRes.StringsRes.Error, ex.Message, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion "Public Commands"  
           
        #region "Public Commands Connect ==> Tbl93Comment"

        private RelayCommand _getCommentByNameOrIdCommand;   
    
        public new ICommand GetCommentByNameOrIdCommand  
    
        {
            get { return _getCommentByNameOrIdCommand ?? (_getCommentByNameOrIdCommand = new RelayCommand(delegate { GetCommentByNameOrId(null); })); }
        }

        public new void GetCommentByNameOrId(object o)
        {   
    
            int id;
            if (int.TryParse(SearchCommentInfo, out id))
                Tbl93CommentsList = new ObservableCollection<Tbl93Comment> { _tbl93CommentsRepository.Get(id) };
            
            Tbl93CommentsList = new ObservableCollection<Tbl93Comment>
                                (from comment in _tbl93CommentsRepository.GetAll()
                                 where comment.Info.StartsWith(SearchCommentInfo)
                                orderby comment.Info
                                select comment);         
     
            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            CommentsView.Refresh();
        }
        //------------------------------------------------------------------------------                
           
        private RelayCommand _addCommentCommand;         
    
        public new ICommand AddCommentCommand      
    
        {
            get { return _addCommentCommand ?? (_addCommentCommand = new RelayCommand(delegate { AddComment(null); })); }
        }

        public new void AddComment(object o)
        {
                Tbl93CommentsList = new ObservableCollection<Tbl93Comment>();  
    
            Tbl93CommentsList.Insert(0, new Tbl93Comment { Info = CultRes.StringsRes.DatasetNew });     
    

            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            CommentsView.Refresh();
        }
        //---------------------------------------------------------------------------------------  
           
        private RelayCommand _copyCommentCommand;            
    
        public new ICommand CopyCommentCommand       
         
        {
            get { return _copyCommentCommand ?? (_copyCommentCommand = new RelayCommand(delegate { CopyComment(null); })); }
        }

        public new void CopyComment(object o)
        {
            Tbl93CommentsList = new ObservableCollection<Tbl93Comment>();

            var comment = _tbl93CommentsRepository.Get(CurrentTbl93Comment.CommentID);

            Tbl93CommentsList.Insert(0, new Tbl93Comment
            {                 
CounterID = comment.CounterID,              
                Info = CultRes.StringsRes.DatasetNew,
                Valid = comment.Valid,
                ValidYear = comment.ValidYear,
                Memo = comment.Memo          
        
            });

            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            CommentsView.Refresh();
        }
        //---------------------------------------------------------------------------------------                  
           
        private RelayCommand _deleteCommentCommand;             
    
        public new ICommand DeleteCommentCommand          
         
        {
            get { return _deleteCommentCommand ?? (_deleteCommentCommand = new RelayCommand(delegate { DeleteComment(null); })); }
        }

        private void DeleteComment(object o)
        {
            try
            {
                var comment = _tbl93CommentsRepository.Get(CurrentTbl93Comment.CommentID);
                if (comment != null)
                {
                    if (WpfMessageBox.Show(CultRes.StringsRes.DeleteQuestion1, CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl93Comment.CommentID,
                         MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes) return;
                    _tbl93CommentsRepository.Delete(comment);
                    _tbl93CommentsRepository.Save();
                    WpfMessageBox.Show(CultRes.StringsRes.DeleteSuccess, CurrentTbl93Comment.CommentID.ToString(), MessageBoxButton.OK, MessageBoxImage.Information);
                    if (SearchCommentInfo == null)                    
                        GetConnectedTablesById(o); //refresh doubleClick                                       
                    else
                    {
                        Tbl93CommentsList = new ObservableCollection<Tbl93Comment>
                                        (from comm in _tbl93CommentsRepository.GetAll()
                                         where comm.Info.StartsWith(SearchCommentInfo)
                                         orderby comm.Info
                                         select comm); 
    

                        CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
                        CommentsView.Refresh();
                    }
                }
                else
                {
                    WpfMessageBox.Show(CultRes.StringsRes.Information, CultRes.StringsRes.DeleteCan + " " + CurrentTbl93Comment.CommentID + " " + CultRes.StringsRes.DeleteCan1,
                         MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                WpfMessageBox.Show(CultRes.StringsRes.Error, ex.Message, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        //-------------------------------------------------------------------------------------------------    
           
        private RelayCommand _saveCommentCommand;            
    
        public new ICommand SaveCommentCommand           
         
        {
            get { return _saveCommentCommand ?? (_saveCommentCommand = new RelayCommand(delegate { SaveComment(null); })); }
        }

        private void SaveComment(object o)
        {
            try
            {
                var comment = _tbl93CommentsRepository.Get(CurrentTbl93Comment.CommentID);
                if (CurrentTbl93Comment == null)                
                    WpfMessageBox.Show(CultRes.StringsRes.Warning, CultRes.StringsRes.DatasetNotExist, MessageBoxButton.OK, MessageBoxImage.Warning);               
                else
                {
                    if (CurrentTbl93Comment.CommentID != 0)
                    {
                        if (comment != null)  //update
                        {   
         
                            comment.Updater = Environment.UserName;
                            comment.UpdaterDate = DateTime.Now;    
         
                        }
                    }
                    else
                    {
                        _tbl93CommentsRepository.Add(new Tbl93Comment
                        {   
CounterID = CurrentTbl93Comment.CounterID,              
                            CountID = RandomHelper.Randomnumber(),
                            Valid = CurrentTbl93Comment.Valid,
                            ValidYear = CurrentTbl93Comment.ValidYear,
                            Info = CurrentTbl93Comment.Info,
                            Writer = Environment.UserName,
                            WriterDate = DateTime.Now,
                            Updater = Environment.UserName,
                            UpdaterDate = DateTime.Now,
                            Memo = CurrentTbl93Comment.Memo   
         
                        });
                    }
                    {
                        //check about double Name   
                        var dataset = new ObservableCollection<Tbl93Comment>
                        (from a in _tbl93CommentsRepository.GetAll()
                         where
                         a.Info.Trim() == CurrentTbl93Comment.Info.Trim() &&                
                         a.CounterID == CurrentTbl93Comment.CounterID
                         select a);

                        if (dataset.Count == 0 && CurrentTbl93Comment.CommentID == 0 ||
                            dataset.Count != 0 && CurrentTbl93Comment.CommentID != 0  ||
                            dataset.Count == 0 && CurrentTbl93Comment.CommentID != 0) //new dataset and update
                        {
                            if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTbl93Comment.Info,
                                    MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)                            
                                return;                            
                            {
                        _tbl93CommentsRepository.Save();

                                WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess, CurrentTbl93Comment.Info,
                                    MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                        }

                        if (dataset.Count != 0 && CurrentTbl93Comment.CommentID == 0)  //dataset exist
                        {
                              WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTbl93Comment.Info,
                              MessageBoxButton.OK, MessageBoxImage.Information);
                        }   
         
                        if (SearchCommentInfo == null)                       
                            GetConnectedTablesById(o); //refresh doubleClick                                            
                        {
                            if (CurrentTbl93Comment.CommentID== 0)  //new Dataset
                                GetCommentByNameOrId(o); //search      
                        
                        {
                            Tbl93CommentsList = new ObservableCollection<Tbl93Comment>
                                            (from comm in _tbl93CommentsRepository.GetAll()
                                             where comm.CommentID == CurrentTbl93Comment.CommentID
                                             orderby comment.Info
                                             select comm);

                            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
                            CommentsView.Refresh();
                        }       
      
                         }   
                    }
                }
            }
            catch (Exception ex)
            {
                WpfMessageBox.Show(CultRes.StringsRes.Error, ex.Message, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion "Public Commands"  
      

 //    Part 9    

     
        #region "Public Commands Connected Tables by DoubleClick"                                         

        private RelayCommand _getConnectedTablesCommand;
        public new ICommand GetConnectedTablesCommand
        {
            get { return _getConnectedTablesCommand ?? (_getConnectedTablesCommand = new RelayCommand(delegate { GetConnectedTablesById(null); })); }
        }

        public  void GetConnectedTablesById(object o)
        {
            //Clear Search-TextBox                                  
            SearchNULLName = null;                       
            SearchNULLName = null;
            SearchCommentInfo = null;
            SearchRefExpertName = null;
            SearchRefSourceName = null;
            SearchRefAuthorName = null;

            SelectedDetailThreeRefTabIndex = 1;  //change to Connect tab

            NULLList =  new ObservableCollection<NULL>
                                                       (from NULL in NULLRepository.GetAll()
                                                       where NULL.NULL== CurrentTblCounter.NULL
                                                       orderby NULL.NULL
                                                       select NULL);

            if (Tbl90AuthorsAllList == null)
            Tbl90AuthorsAllList =  new ObservableCollection<Tbl90RefAuthor>
                                                       (from auth in _tbl90RefAuthorsRepository.GetAll()
                                                        orderby auth.RefAuthorName, auth.BookName, auth.Page1
                                                        select auth);
 
            if (Tbl90SourcesAllList == null)
           Tbl90SourcesAllList =  new ObservableCollection<Tbl90RefSource>
                                                       (from sour in _tbl90RefSourcesRepository.GetAll()
                                                        orderby sour.RefSourceName
                                                        select sour);

            if (Tbl90ExpertsAllList == null)
            Tbl90ExpertsAllList =  new ObservableCollection<Tbl90RefExpert>
                                                       (from exp in _tbl90RefExpertsRepository.GetAll()
                                                        orderby exp.RefExpertName
                                                        select exp);

            View = CollectionViewSource.GetDefaultView(NULLList);
            View.Refresh();
            //-----------------------------------------------------------------------------------
            NULLList =  new ObservableCollection<NULL>
                                                       (from NULL in NULLRepository.GetAll()
                                                       where NULL.CounterID== CurrentTblCounter.CounterID
                                                       orderby NULL.NULL
                                                       select NULL);


            View = CollectionViewSource.GetDefaultView(NULLList);
            View.Refresh();
            //-----------------------------------------------------------------------------------
            Tbl90RefAuthorsList =  new ObservableCollection<Tbl90Reference>
                                                          (from refAu in _tbl90ReferencesRepository.GetAll()
                                                          where refAu.CounterID== CurrentTblCounter.CounterID
                                                          && refAu.Tbl90RefExperts == null
                                                          && refAu.Tbl90RefSources == null
                                                          orderby refAu.Tbl90RefAuthors.RefAuthorName, refAu.Tbl90RefAuthors.BookName, refAu.Tbl90RefAuthors.Page1
                                                          select refAu);

            RefAuthorsView = CollectionViewSource.GetDefaultView(Tbl90RefAuthorsList);
            RefAuthorsView.Refresh();
            //--------------------------------------------------------------------------------------
            Tbl90RefSourcesList =  new ObservableCollection<Tbl90Reference>
                                                          (from refSo in _tbl90ReferencesRepository.GetAll()
                                                          where refSo.CounterID== CurrentTblCounter.CounterID
                                                          && refSo.Tbl90RefExperts == null
                                                          && refSo.Tbl90RefAuthors == null
                                                          orderby refSo.Tbl90RefSources.RefSourceName
                                                          select refSo);

            RefSourcesView = CollectionViewSource.GetDefaultView(Tbl90RefSourcesList);
            RefSourcesView.Refresh();
            //--------------------------------------------------------------------------------------
            Tbl90RefExpertsList =   new ObservableCollection<Tbl90Reference>
                                                          (from refEx in _tbl90ReferencesRepository.GetAll()
                                                          where refEx.CounterID== CurrentTblCounter.CounterID
                                                          && refEx.Tbl90RefAuthors == null
                                                          && refEx.Tbl90RefSources == null
                                                          orderby refEx.Tbl90RefExperts.RefExpertName
                                                          select refEx);

            RefExpertsView = CollectionViewSource.GetDefaultView(Tbl90RefExpertsList);
            RefExpertsView.Refresh();
            //-----------------------------------------------------------------------------------
            Tbl93CommentsList =  new ObservableCollection<Tbl93Comment>
                                                        (from comm in _tbl93CommentsRepository.GetAll()
                                                        where comm.CounterID== CurrentTblCounter.CounterID
                                                        orderby comm.Info
                                                        select comm);

            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            CommentsView.Refresh();
        }
        #endregion "Public Commands Connected Tables by DoubleClick"
   
 

 //    Part 10    

 

 //    Part 11    

     
        #region "Public Properties TblCounter"

        private string _searchCounterName;
        public  string SearchCounterName
        {
            get { return _searchCounterName; }
            set { _searchCounterName = value; RaisePropertyChanged();  }
        }

        public  ICollectionView View;
        public  TblCounter CurrentTblCounter => View?.CurrentItem as TblCounter;

        private ObservableCollection<TblCounter> _tblCountersList;
        public  ObservableCollection<TblCounter> TblCountersList
        {
            get { return _tblCountersList; }
            set {  _tblCountersList = value; RaisePropertyChanged();

                //Clear Search-TextBox
                SearchNULL = null;                                
                SearchNULL = null;
                SearchCommentInfo = null;
                SearchRefExpertName = null;
                SearchRefSourceName = null;
                SearchRefAuthorName = null;
            }
        }

        private ObservableCollection<TblCounter> _tblCountersAllList;
        public  ObservableCollection<TblCounter> TblCountersAllList
        {
            get { return _tblCountersAllList; }
            set { _tblCountersAllList = value; RaisePropertyChanged(); }
        }

        #endregion "Public Properties"   
 

 //    Part 12    

 

   }
}   
