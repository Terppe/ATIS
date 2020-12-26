using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity.Validation;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using DAL;
using DAL.Helper;
using DAL.Models;
using WPFUI.ViewModel;
using GalaSoft.MvvmLight.Command;
using MessageBoxImage = System.Windows.MessageBoxImage;

    
using GalaSoft.MvvmLight;  
    
         //    Tbl90RefAuthorsViewModel Skriptdatum:  30.03.2019  10:32    

namespace WPFUI.Views.Database
{     
    
    public class Tbl90RefAuthorsViewModel : ViewModelBase                     
    {     
        
        #region "Private Data Members"

        private readonly AllListVm _allListVm = new AllListVm();
        private readonly Repository<Tbl90RefAuthor, int> _tbl90RefAuthorsRepository = new Repository<Tbl90RefAuthor, int>();  
    

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
            }
        }
        private new bool IsInDesignMode { get; set; }

        #endregion "Constructor"           
 

 //    Part 1    

           
        #region "Public Commands Basic Tbl90RefAuthor"

        private RelayCommand _getRefAuthorByNameOrIdCommand;     
    
        public ICommand GetRefAuthorByNameOrIdCommand    
    
        {
            get { return _getRefAuthorByNameOrIdCommand ?? (_getRefAuthorByNameOrIdCommand = new RelayCommand(delegate { GetRefAuthorByNameOrId(null); })); }   
        }

        private void GetRefAuthorByNameOrId(object o)       
        {   
       
            int id;
            if (int.TryParse(SearchRefAuthorName, out id))
                Tbl90RefAuthorsList = new ObservableCollection<Tbl90RefAuthor> { _tbl90RefAuthorsRepository.Get(id) };
             else
                Tbl90RefAuthorsList = _allListVm.GetValueTbl90AuthorsList(SearchRefAuthorName);     
RefAuthorsView = CollectionViewSource.GetDefaultView(Tbl90RefAuthorsList);
            RefAuthorsView.Refresh();
        }
        //------------------------------------------------------------------------------                
           
        private RelayCommand _addRefAuthorCommand;           
    
        public ICommand AddRefAuthorCommand       
    
        {
            get { return _addRefAuthorCommand ?? (_addRefAuthorCommand = new RelayCommand(delegate { AddRefAuthor(null); })); }
        }

        private void AddRefAuthor(object o)
        {
            Tbl90RefAuthorsList = new ObservableCollection<Tbl90RefAuthor>();   
Tbl90RefAuthorsList.Insert(0, new Tbl90RefAuthor{ RefAuthorName= CultRes.StringsRes.DatasetNew });       
               
            AuthorsView = CollectionViewSource.GetDefaultView(Tbl90RefAuthorsList);
            AuthorsView.Refresh();
        }
        //------------------------------------------------------------------------------                
           
        private RelayCommand _copyRefAuthorCommand;              
    
        public ICommand CopyRefAuthorCommand             
         
        {
            get { return _copyRefAuthorCommand ?? (_copyRefAuthorCommand = new RelayCommand(delegate { CopyRefAuthor(null); })); }
        }

        private void CopyRefAuthor(object o)
        {
            Tbl90RefAuthorsList = new ObservableCollection<Tbl90RefAuthor>();

            var refAuthor = _tbl90RefAuthorsRepository.Get(CurrentTbl90RefAuthor.RefAuthorID);

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
            AuthorsView = CollectionViewSource.GetDefaultView(Tbl90RefAuthorsList);
            AuthorsView.Refresh();
        }
        //------------------------------------------------------------------------------                
           
        private RelayCommand _deleteRefAuthorCommand;              
    
        public ICommand DeleteRefAuthorCommand             
                
        {
            get { return _deleteRefAuthorCommand ?? (_deleteRefAuthorCommand = new RelayCommand(delegate { DeleteRefAuthor(null); })); }
        }

        private void DeleteRefAuthor(object o)
        {
            try
            {
                var refAuthor = _tbl90RefAuthorsRepository.Get(CurrentTbl90RefAuthor.RefAuthorID);
                if (refAuthor!= null)
                {
                    if (WpfMessageBox.Show(CultRes.StringsRes.DeleteQuestion1, CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl90RefAuthor.RefAuthorName,
                         MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes) 
                    return;

                    _tbl90RefAuthorsRepository.Delete(refAuthor);
                    _tbl90RefAuthorsRepository.Save();

                    WpfMessageBox.Show(CultRes.StringsRes.DeleteSuccess, CurrentTbl90RefAuthor.RefAuthorName, 
                        MessageBoxButton.OK, MessageBoxImage.Information);     

                    GetAuthorByNameOrId(o);  //search       
RefAuthorsView = CollectionViewSource.GetDefaultView(Tbl90RefAuthorsList);
                        RefAuthorsView.Refresh();
                }
                else
                {
                    WpfMessageBox.Show(CultRes.StringsRes.Information, CultRes.StringsRes.DeleteCan + " " + CurrentTbl90RefAuthor.RefAuthorName+ " " + CultRes.StringsRes.DeleteCan1,
                         MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (DbEntityValidationException ex)
            {
                //Retrieve the Error messages as a list of strings
                var errorMessages = ex.EntityValidationErrors
                    .SelectMany(x => x.ValidationErrors)
                    .Select(x => x.ErrorMessage);
                //Join the list to a single string
                var fullErrorMessage = string.Join("; ", errorMessages);
                //Combine the original exeption message with the new one.
                var exeptionMessage = string.Concat(ex.Message, CultRes.StringsRes.ValidationErrors, fullErrorMessage);
                //throw a new DbEntityValidationException
                throw new DbEntityValidationException(exeptionMessage, ex.EntityValidationErrors);
            }
        }
        //-------------------------------------------------------------------------------------------------    
           
        private RelayCommand _saveRefAuthorCommand;              
     
        public ICommand SaveRefAuthorCommand             
         
        {
            get { return _saveRefAuthorCommand ?? (_saveRefAuthorCommand = new RelayCommand(delegate { SaveRefAuthor(null); })); }
        }

        private void SaveRefAuthor(object o)
        {
            try
            {
                var refAuthor = _tbl90RefAuthorsRepository.Get(CurrentTbl90RefAuthor.RefAuthorID);
                if (CurrentTbl90RefAuthor == null)              
                    WpfMessageBox.Show(CultRes.StringsRes.Warning, CultRes.StringsRes.DatasetNotExist, 
                        MessageBoxButton.OK, MessageBoxImage.Warning);               
                else
                {
                    if (CurrentTbl90RefAuthor.RefAuthorID!= 0)
                    {
                        if (refAuthor!= null) //update
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
         
                        }
                    }
                    else
                    {
                        _tbl90RefAuthorsRepository.Add(new Tbl90RefAuthor     //add new
                        {   
RefAuthorName= CurrentTbl90RefAuthor.RefAuthorName,              
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
                            Memo = CurrentTbl90RefAuthor.Memo  
                
                        });
                    }
                    {
                        //check about double Name
                        var dataset = new ObservableCollection<Tbl90RefAuthor>
                        (from a in _tbl90RefAuthorsRepository.GetAll()
                         where
                         a.RefAuthorName == CurrentTbl90RefAuthor.RefAuthorName  &&
                         a.ArticelTitle == CurrentTbl90RefAuthor.ArticelTitle &&
                         a.BookName == CurrentTbl90RefAuthor.BookName  &&
                         a.Publisher == CurrentTbl90RefAuthor.Publisher  &&
                         a.PublicationPlace == CurrentTbl90RefAuthor.PublicationPlace
                         select a);

                        if (dataset.Count != 0 && CurrentTbl90RefAuthor.RefAuthorID == 0)  //dataset exist
                        {
                            WpfMessageBox.Show(CultRes.StringsRes.DatasetExist,
                                    CurrentTbl90RefAuthor.RefAuthorName + " " + CurrentTbl90RefAuthor.ArticelTitle + " " + CurrentTbl90RefAuthor.BookName + " " + CurrentTbl90RefAuthor.Publisher,
                            MessageBoxButton.OK, MessageBoxImage.Information);                       
                        }

                        if (dataset.Count == 0 && CurrentTbl90RefAuthor.RefAuthorID == 0 ||
                            dataset.Count != 0 && CurrentTbl90RefAuthor.RefAuthorID != 0 ||
                            dataset.Count == 0 && CurrentTbl90RefAuthor.RefAuthorID != 0) //new dataset and update
                        {
                            if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2,
                                    CurrentTbl90RefAuthor.RefAuthorName + " " + CurrentTbl90RefAuthor.ArticelTitle + " " + CurrentTbl90RefAuthor.BookName + " " + CurrentTbl90RefAuthor.Publisher,
                                    MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
                                return;
                            {
                                _tbl90RefAuthorsRepository.Save();

                                WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess,
                                    CurrentTbl90RefAuthor.RefAuthorName + " " + CurrentTbl90RefAuthor.ArticelTitle + " " + CurrentTbl90RefAuthor.BookName + " " + CurrentTbl90RefAuthor.Publisher,
                                    MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                        }  
                                      
                        if (CurrentTbl90RefAuthor.RefAuthorID== 0)  //new Dataset
                                GetRefAuthorByNameOrId(o); //search                             
                        {
                            Tbl90RefAuthorsList = new ObservableCollection<Tbl90RefAuthor>
                                                  (from x in _tbl90RefAuthorsRepository.GetAll()
                                                   where x.RefAuthorID == CurrentTbl90RefAuthor.RefAuthorID
                                                   select x);

                            AuthorsView = CollectionViewSource.GetDefaultView(Tbl90RefAuthorsList);
                            AuthorsView.Refresh();
                        }  
         
                    }
                }
            }
            catch (DbEntityValidationException ex)
            {
                //  WpfMessageBox.Show(CultRes.StringsRes.Error, ex.Message, MessageBoxButton.OK, MessageBoxImage.Error);
                //Retrieve the Error messages as a list of strings
                var errorMessages = ex.EntityValidationErrors
                    .SelectMany(x => x.ValidationErrors)
                    .Select(x => x.ErrorMessage);
                //Join the list to a single string
                var fullErrorMessage = string.Join("; ", errorMessages);
                //Combine the original exeption message with the new one.
                var exeptionMessage = string.Concat(ex.Message, CultRes.StringsRes.ValidationErrors, fullErrorMessage);
                //throw a new DbEntityValidationException
                throw new DbEntityValidationException(exeptionMessage, ex.EntityValidationErrors);
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

        public ICollectionView AuthorsView;
        public Tbl90RefAuthor CurrentTbl90RefAuthor => AuthorsView?.CurrentItem as Tbl90RefAuthor;
        
        private string _searchRefAuthorName;
        public string SearchRefAuthorName
        {
            get => _searchRefAuthorName; 
            set { _searchRefAuthorName = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl90RefAuthor> _tbl90RefAuthorsList;
        public  ObservableCollection<Tbl90RefAuthor> Tbl90RefAuthorsList
        {
            get => _tbl90RefAuthorsList; 
            set { _tbl90RefAuthorsList = value; RaisePropertyChanged();  }
        }

        private ObservableCollection<Tbl90RefAuthor> _tbl90RefAuthorsAllList;
        public ObservableCollection<Tbl90RefAuthor> Tbl90RefAuthorsAllList
        {
            get => _tbl90RefAuthorsAllList; 
            set { _tbl90RefAuthorsAllList = value; RaisePropertyChanged(); }
        }

        #endregion "Public Properties"
   
 

 //    Part 12    

 

   }
}   
