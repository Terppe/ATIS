using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using Atis.WpfUi.Model;
using Atis.WpfUi.Repositories;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

//    Tbl90RefAuthorViewModel Skriptdatum:  29.12.2011  10:32    

namespace Atis.WpfUi.ViewModel
{   


    
    public class Tbl90RefAuthorsViewModel : NULLViewModel                     
    {     
         
      #region "Private Data Members"

        protected readonly Tbl69FiSpeciessesRepository Tbl69FiSpeciessesRepository = new Tbl69FiSpeciessesRepository();   
          
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

        #endregion "Constructor"           
       
        #region "Public Commands Basic Tbl90RefAuthor"

        private RelayCommand _getAuthorByNameCommand;
        public new ICommand GetAuthorByNameCommand
        {
            get { return _getAuthorByNameCommand ?? (_getAuthorByNameCommand = new RelayCommand(GetAuthorByName)); }
        }

        private void GetAuthorByName()
        {   
Tbl90RefAuthorsList =
                 new ObservableCollection<Tbl90RefAuthor>((from x in Tbl90RefAuthorsRepository.Tbl90RefAuthors
                                                        where x.RefAuthorName.StartsWith(SearchRefAuthorName)
                                                        orderby x.RefAuthorName
                                                        select x));

            Tbl90RefAuthorsAllList =
                 new ObservableCollection<Tbl90RefAuthor>((from y in Tbl90RefAuthorsRepository.Tbl90RefAuthors
                                                        orderby y.RefAuthorName
                                                        select y));

            NULLAllList =
                 new ObservableCollection<NULL>((from z in NULLRepository.NULL
                                                        orderby z.NULL
                                                        select z));

              
         
            Tbl90AuthorsAllList =
                 new ObservableCollection<Tbl90RefAuthor>((from auth in Tbl90RefAuthorsRepository.Tbl90RefAuthors
                                                        orderby auth.RefAuthorName, auth.BookName, auth.Page
                                                        select auth));

            Tbl90SourcesAllList =
                new ObservableCollection<Tbl90RefSource>((from sour in Tbl90RefSourcesRepository.Tbl90RefSources
                                                        orderby sour.RefSourceName
                                                        select sour));

            Tbl90ExpertsAllList =
                new ObservableCollection<Tbl90RefExpert>((from exp in Tbl90RefExpertsRepository.Tbl90RefExperts
                                                        orderby exp.RefExpertName
                                                        select exp));

            //All List to null
            Tbl93CommentsList = null;
            Tbl90RefExpertsList = null;
            Tbl90RefAuthorsList = null;
            Tbl90RefSourcesList = null;

  
Tbl69FiSpeciessesList = null;     
             
  View = CollectionViewSource.GetDefaultView(Tbl90RefAuthorsList);
            if (View != null)
                View.CurrentChanged += tbl90RefAuthorView_CurrentChanged;                   
            RaisePropertyChanged("CurrentTbl90RefAuthor");
        }
        //------------------------------------------------------------------------------
        private RelayCommand _addAuthorCommand;
        public new ICommand AddAuthorCommand
        {
            get { return _addAuthorCommand ?? (_addAuthorCommand = new RelayCommand(AddAuthor)); }
        }

        private void AddAuthor()
        {
            if (Tbl90RefAuthorsList == null)
                Tbl90RefAuthorsList = new ObservableCollection<Tbl90RefAuthor>();
            Tbl90RefAuthorsList.Add(new Tbl90RefAuthor{ RefAuthorName= "New " });
            View = CollectionViewSource.GetDefaultView(Tbl90RefAuthorsList);
            if (View != null)
                View.CurrentChanged += tbl90RefAuthorView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl90RefAuthor");
        }
        //---------------------------------------------------------------------------------------
  
       
        private RelayCommand _deleteAuthorCommand;
        public new ICommand DeleteAuthorCommand
        {
            get { return _deleteAuthorCommand ?? (_deleteAuthorCommand = new RelayCommand(DeleteAuthor)); }
        }

        private void DeleteAuthor()
        {
            try
            {
                var refAuthor= Tbl90RefAuthorsRepository.Tbl90RefAuthors.FirstOrDefault(x => x.RefAuthorID== CurrentTbl90RefAuthor.RefAuthorID);
                if (refAuthor!= null)
                {
                    if (MessageBox.Show("Are you really shure to delete this "
                                        + CurrentTbl90RefAuthor.RefAuthorName, "Question", MessageBoxButton.YesNo) !=
                        MessageBoxResult.Yes) return;
                    Tbl90RefAuthorsRepository.Delete(refAuthor);
                    Tbl90RefAuthorsRepository.Save();
                    MessageBox.Show(CurrentTbl90RefAuthor.RefAuthorName + " was deleted successfully");
                    GetAuthorByName(); //Refresh
                }
                else
                {
                    MessageBox.Show("Only " + CurrentTbl90RefAuthor.RefAuthorName+ " can be deleted");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //-------------------------------------------------------------------------------------------------
        private RelayCommand _saveAuthorCommand;
        public new ICommand SaveAuthorCommand
        {
            get { return _saveAuthorCommand ?? (_saveAuthorCommand = new RelayCommand(SaveAuthor)); }
        }

        private void SaveAuthor()
        {
            try
            {
                var refAuthor= Tbl90RefAuthorsRepository.Tbl90RefAuthors.FirstOrDefault(x => x.RefAuthorID== CurrentTbl90RefAuthor.RefAuthorID);
                if (CurrentTbl90RefAuthor == null)
                {
                    MessageBox.Show("refAuthor was not found");
                }
                else
                {
                    if (CurrentTbl90RefAuthor.RefAuthorID!= 0)
                    {
                        if (refAuthor!= null) //update
                        {
                            refAuthor.Updater = Environment.UserName;
                            refAuthor.UpdaterDate = DateTime.Now;
                        }
                    }
                    else
                    {
                        Tbl90RefAuthorsRepository.Add(new Tbl90RefAuthor
                        {
                            NULL= CurrentTbl90RefAuthor.NULL,              
                            RefAuthorName= CurrentTbl90RefAuthor.RefAuthorName,              
                            CountID = TblCountersRepository.Counter(),
                            Valid = CurrentTbl90RefAuthor.Valid,
                            ValidYear = CurrentTbl90RefAuthor.ValidYear,
                            Synonym = CurrentTbl90RefAuthor.Synonym,
                            Author = CurrentTbl90RefAuthor.Author,
                            AuthorYear = CurrentTbl90RefAuthor.AuthorYear,
                            Info = CurrentTbl90RefAuthor.Info,
                            EngName = CurrentTbl90RefAuthor.EngName,
                            GerName = CurrentTbl90RefAuthor.GerName,
                            FraName = CurrentTbl90RefAuthor.FraName,
                            PorName = CurrentTbl90RefAuthor.PorName,
                            Writer = Environment.UserName,
                            WriterDate = DateTime.Now,
                            Updater = Environment.UserName,
                            UpdaterDate = DateTime.Now,
                            Memo = CurrentTbl90RefAuthor.Memo
                        });
                    }
                    {
                        Tbl90RefAuthorsRepository.Save();
                        MessageBox.Show(CurrentTbl90RefAuthor.RefAuthorName+  " was successfully saved ");
                        GetAuthorByName();  //Refresh
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion "Public Commands"
  

        
        #region "Public Commands Connect ==> NULL"                 

        private RelayCommand _getFiSpeciesByNameCommand;
        public ICommand GetFiSpeciesByNameCommand
        {
            get { return _getFiSpeciesByNameCommand ?? (_getFiSpeciesByNameCommand = new RelayCommand(GetFiSpeciesByName)); }
        }

        private void GetFiSpeciesByName()
        {
            Tbl69FiSpeciessesList =
                new ObservableCollection<NULL>((from NULL in Tbl69FiSpeciessesRepository.Tbl69FiSpeciesses
                                                       where NULL.NULL.StartsWith(SearchFiSpeciesName)
                                                       orderby NULL.NULL
                                                       select NULL));

            View = CollectionViewSource.GetDefaultView(Tbl69FiSpeciessesList);
            if (View != null)
                View.CurrentChanged += NULLView_CurrentChanged;                   
            RaisePropertyChanged("CurrentNULL");
        }
        //------------------------------------------------------------------------------
        private RelayCommand _addFiSpeciesCommand;
        public ICommand AddFiSpeciesCommand
        {
            get { return _addFiSpeciesCommand ?? (_addFiSpeciesCommand = new RelayCommand(AddFiSpecies)); }
        }

        private void AddFiSpecies()
        {
            if (Tbl69FiSpeciessesList == null)
                Tbl69FiSpeciessesList = new ObservableCollection<NULL>();
            Tbl69FiSpeciessesList.Add(new NULL{ NULL= "New " });                   
            View = CollectionViewSource.GetDefaultView(Tbl69FiSpeciessesList);
            if (View != null)
                View.CurrentChanged += NULLView_CurrentChanged;
            RaisePropertyChanged("CurrentNULL");
        }
        //---------------------------------------------------------------------------------------
        private RelayCommand _deleteFiSpeciesCommand;
        public ICommand DeleteFiSpeciesCommand
        {
            get { return _deleteFiSpeciesCommand ?? (_deleteFiSpeciesCommand = new RelayCommand(DeleteFiSpecies)); }
        }

        private void DeleteFiSpecies()
        {
            try
            {
                var NULL = Tbl69FiSpeciessesRepository.Tbl69FiSpeciesses.FirstOrDefault(x => x.NULL== CurrentNULL.NULL);
                if (NULL != null)
                {
                    if (MessageBox.Show("Are you really shure to delete this "
                                        + CurrentNULL.NULL, "Question", MessageBoxButton.YesNo) !=
                        MessageBoxResult.Yes) return;
                    Tbl69FiSpeciessesRepository.Delete(NULL);
                    Tbl69FiSpeciessesRepository.Save();
                    MessageBox.Show(CurrentNULL.NULL+ " was deleted successfully");
                    if (SearchFiSpeciesName == null)
                    {
                        GetConnectedTablesById(); //refresh doubleClick                    
                    }
                    else
                    {
                        GetFiSpeciesByName(); //search
                    }
                }
                else
                {
                    MessageBox.Show("Only " + CurrentNULL.NULL+ " can be deleted");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //-------------------------------------------------------------------------------------------------
        private RelayCommand _saveFiSpeciesCommand;   
        public ICommand SaveFiSpeciesCommand
        {
            get { return _saveFiSpeciesCommand ?? (_saveFiSpeciesCommand = new RelayCommand(SaveFiSpecies)); }
        }

        private void SaveFiSpecies()
        {
            try
            {
                var NULL = Tbl69FiSpeciessesRepository.Tbl69FiSpeciesses.FirstOrDefault(x => x.NULL== CurrentNULL.NULL);
                if (CurrentNULL == null)
                {
                    MessageBox.Show("NULL was not found");
                }
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
                        Tbl69FiSpeciessesRepository.Add(new NULL
                        {
                            RefAuthorID= CurrentNULL.RefAuthorID,              
                            NULL= CurrentNULL.NULL,              
                            CountID = TblCountersRepository.Counter(),
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
                        Tbl69FiSpeciessesRepository.Save();
                        MessageBox.Show(CurrentNULL.NULL+  " was successfully saved ");
                        if (SearchFiSpeciesName == null)
                        {
                            GetConnectedTablesById(); //refresh doubleClick                    
                        }
                        else
                        {
                            GetFiSpeciesByName(); //search
                        }       
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion "Public Commands"
    
  
        
        #region "Public Commands Connect ==> NULL"                 

        private RelayCommand _getPlSpeciesByNameCommand;
        public ICommand GetPlSpeciesByNameCommand
        {
            get { return _getPlSpeciesByNameCommand ?? (_getPlSpeciesByNameCommand = new RelayCommand(GetPlSpeciesByName)); }
        }

        private void GetPlSpeciesByName()
        {
            Tbl72PlSpeciessesList =
                new ObservableCollection<NULL>((from NULL in Tbl72PlSpeciessesRepository.Tbl72PlSpeciesses
                                                       where NULL.NULL.StartsWith(SearchPlSpeciesName)
                                                       orderby NULL.NULL
                                                       select NULL));

            View = CollectionViewSource.GetDefaultView(Tbl72PlSpeciessesList);
            if (View != null)
                View.CurrentChanged += NULLView_CurrentChanged;                   
            RaisePropertyChanged("CurrentNULL");
        }
        //------------------------------------------------------------------------------
        private RelayCommand _addPlSpeciesCommand;
        public ICommand AddPlSpeciesCommand
        {
            get { return _addPlSpeciesCommand ?? (_addPlSpeciesCommand = new RelayCommand(AddPlSpecies)); }
        }

        private void AddPlSpecies()
        {
            if (Tbl72PlSpeciessesList == null)
                Tbl72PlSpeciessesList = new ObservableCollection<NULL>();
            Tbl72PlSpeciessesList.Add(new NULL{ NULL= "New " });                   
            View = CollectionViewSource.GetDefaultView(Tbl72PlSpeciessesList);
            if (View != null)
                View.CurrentChanged += NULLView_CurrentChanged;
            RaisePropertyChanged("CurrentNULL");
        }
        //---------------------------------------------------------------------------------------
        private RelayCommand _deletePlSpeciesCommand;
        public ICommand DeletePlSpeciesCommand
        {
            get { return _deletePlSpeciesCommand ?? (_deletePlSpeciesCommand = new RelayCommand(DeletePlSpecies)); }
        }

        private void DeletePlSpecies()
        {
            try
            {
                var NULL= Tbl72PlSpeciessesRepository.Tbl72PlSpeciesses.FirstOrDefault(x => x.NULL== CurrentNULL.NULL);
                if (NULL!= null)
                {
                    if (MessageBox.Show("Are you really shure to delete this "
                                        + CurrentNULL.NULL, "Question", MessageBoxButton.YesNo) !=
                        MessageBoxResult.Yes) return;
                    Tbl72PlSpeciessesRepository.Delete(NULL);
                    Tbl72PlSpeciessesRepository.Save();
                    MessageBox.Show(CurrentNULL.NULL+ " was deleted successfully");
                    if (SearchPlSpeciesName == null)
                    {
                        GetConnectedTablesById(); //refresh doubleClick                    
                    }
                    else
                    {
                        GetPlSpeciesByName(); //search
                    }
                }
                else
                {
                    MessageBox.Show("Only " + CurrentNULL.NULL+ " can be deleted");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //-------------------------------------------------------------------------------------------------
        private RelayCommand _savePlSpeciesCommand;   
        public ICommand SavePlSpeciesCommand
        {
            get { return _savePlSpeciesCommand ?? (_savePlSpeciesCommand = new RelayCommand(SavePlSpecies)); }
        }

        private void SavePlSpecies()
        {
            try
            {
                var NULL= Tbl72PlSpeciessesRepository.Tbl72PlSpeciesses.FirstOrDefault(x => x.NULL== CurrentNULL.NULL);
                if (CurrentNULL == null)
                {
                    MessageBox.Show("NULL was not found");
                }
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
                        Tbl72PlSpeciessesRepository.Add(new NULL
                        {
                            RefAuthorID= CurrentNULL.RefAuthorID,              
                            NULL= CurrentNULL.NULL,              
                            CountID = TblCountersRepository.Counter(),
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
                        Tbl72PlSpeciessesRepository.Save();
                        MessageBox.Show(CurrentNULL.NULL+  " was successfully saved ");              
                        if (SearchPlSpeciesName == null)
                        {
                            GetConnectedTablesById(); //refresh doubleClick                    
                        }
                        else
                        {
                            GetPlSpeciesByName(); //search
                        }       
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion "Public Commands"
    
     
    
        #region "Public Commands Connect ==> Tbl90RefAuthor"

        private RelayCommand _getRefAuthorByNameCommand;
        public new ICommand GetRefAuthorByNameCommand
        {
            get { return _getRefAuthorByNameCommand ?? (_getRefAuthorByNameCommand = new RelayCommand(GetRefAuthorByName)); }
        }

        //----------------------------------------------------
        private RelayCommand _addRefAuthorCommand;
        public new ICommand AddRefAuthorCommand
        {
            get { return _addRefAuthorCommand ?? (_addRefAuthorCommand = new RelayCommand(AddRefAuthor)); }
        }

        //---------------------------------------------------------------------------------------

        private RelayCommand _deleteRefAuthorCommand;
        public new ICommand DeleteRefAuthorCommand
        {
            get { return _deleteRefAuthorCommand ?? (_deleteRefAuthorCommand = new RelayCommand(DeleteRefAuthor)); }
        }

        public new void DeleteRefAuthor()
        {
            try
            {
                var refAuthor = Tbl90ReferencesRepository.Tbl90References.FirstOrDefault(x => x.ReferenceID == CurrentTbl90RefAuthor.ReferenceID);
                if (refAuthor != null)
                {
                    if (MessageBox.Show("Are you really shure to delete this "
                                        + CurrentTbl90RefAuthor.Info, "Question", MessageBoxButton.YesNo) !=
                        MessageBoxResult.Yes) return;
                    Tbl90ReferencesRepository.Delete(refAuthor);
                    Tbl90ReferencesRepository.Save();
                    MessageBox.Show(CurrentTbl90RefAuthor.Info + " was deleted successfully");
                    if (SearchRefAuthorName == null)
                    {
                        GetConnectedTablesById(); //refresh doubleClick                    
                    }
                    else
                    {
                        GetRefAuthorByName(); //search
                    }
                }
                else
                {
                    MessageBox.Show("Only " + CurrentTbl90RefAuthor.Info + " can be deleted");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //-------------------------------------------------------------------------------------------------

        private RelayCommand _saveRefAuthorCommand;
        public new ICommand SaveRefAuthorCommand
        {
            get { return _saveRefAuthorCommand ?? (_saveRefAuthorCommand = new RelayCommand(SaveRefAuthor)); }
        }

        public new void SaveRefAuthor()
        {
            try
            {
                var refAuthor = Tbl90ReferencesRepository.Tbl90References.FirstOrDefault(x => x.ReferenceID == CurrentTbl90RefAuthor.ReferenceID);
                if (CurrentTbl90RefAuthor == null)
                {
                    MessageBox.Show("reference Author was not found");
                }
                else
                {
                    if (CurrentTbl90RefAuthor.ReferenceID != 0)
                    {
                        if (refAuthor != null)
                        {
                            refAuthor.Updater = Environment.UserName;
                            refAuthor.UpdaterDate = DateTime.Now;
                        }
                    }
                    else
                    {
                        Tbl90ReferencesRepository.Add(new Tbl90Reference()
                        {
                            RefAuthorID = CurrentTbl90RefAuthor.RefAuthorID,
                            RefAuthorID= CurrentTbl90RefAuthor.RefAuthorID,
                            CountID = TblCountersRepository.Counter(),
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
                        Tbl90ReferencesRepository.Save();
                        MessageBox.Show(CurrentTbl90RefAuthor.Info + " was successfully saved ");
                        if (SearchRefAuthorName == null)
                        {
                            GetConnectedTablesById(); //refresh doubleClick                    
                        }
                        else
                        {
                            GetRefAuthorByName(); //search
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion "Public Commands"

        #region "Public Commands Connect ==> Tbl90RefSource"

        private RelayCommand _getRefSourceByNameCommand;
        public new ICommand GetRefSourceByNameCommand
        {
            get { return _getRefSourceByNameCommand ?? (_getRefSourceByNameCommand = new RelayCommand(GetRefSourceByName)); }
        }

        //----------------------------------------------------
        private RelayCommand _addRefSourceCommand;
        public new ICommand AddRefSourceCommand
        {
            get { return _addRefSourceCommand ?? (_addRefSourceCommand = new RelayCommand(AddRefSource)); }
        }

        //---------------------------------------------------------------------------------------

        private RelayCommand _deleteRefSourceCommand;
        public new ICommand DeleteRefSourceCommand
        {
            get { return _deleteRefSourceCommand ?? (_deleteRefSourceCommand = new RelayCommand(DeleteRefSource)); }
        }

        public new void DeleteRefSource()
        {
            try
            {
                var refSource = Tbl90ReferencesRepository.Tbl90References.FirstOrDefault(x => x.ReferenceID == CurrentTbl90RefSource.ReferenceID);
                if (refSource != null)
                {
                    if (MessageBox.Show("Are you really shure to delete this "
                                        + CurrentTbl90RefSource.Info, "Question", MessageBoxButton.YesNo) !=
                        MessageBoxResult.Yes) return;
                    Tbl90ReferencesRepository.Delete(refSource);
                    Tbl90ReferencesRepository.Save();
                    MessageBox.Show(CurrentTbl90RefSource.Info + " was deleted successfully");
                    if (SearchRefSourceName == null)
                    {
                        GetConnectedTablesById(); //refresh doubleClick                    
                    }
                    else
                    {
                        GetRefSourceByName(); //search
                    }
                }
                else
                {
                    MessageBox.Show("Only " + CurrentTbl90RefSource.Info + " can be deleted");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //-------------------------------------------------------------------------------------------------
        private RelayCommand _saveRefSourceCommand;
        public new ICommand SaveRefSourceCommand
        {
            get { return _saveRefSourceCommand ?? (_saveRefSourceCommand = new RelayCommand(SaveRefSource)); }
        }

        public new void SaveRefSource()
        {
            try
            {
                var refSource = Tbl90ReferencesRepository.Tbl90References.FirstOrDefault(x => x.ReferenceID == CurrentTbl90RefSource.ReferenceID);
                if (CurrentTbl90RefSource == null)
                {
                    MessageBox.Show("reference Source was not found");
                }
                else
                {
                    if (CurrentTbl90RefSource.ReferenceID != 0)
                    {
                        if (refSource != null)
                        {
                            refSource.Updater = Environment.UserName;
                            refSource.UpdaterDate = DateTime.Now;
                        }
                    }
                    else
                    {
                        Tbl90ReferencesRepository.Add(new Tbl90Reference()
                        {
                            RefSourceID = CurrentTbl90RefSource.RefSourceID,
                            RefAuthorID= CurrentTbl90RefSource.RefAuthorID,
                            CountID = TblCountersRepository.Counter(),
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
                        Tbl90ReferencesRepository.Save();
                        MessageBox.Show(CurrentTbl90RefSource.Info + " was successfully saved ");
                        if (SearchRefSourceName == null)
                        {
                            GetConnectedTablesById(); //refresh doubleClick                    
                        }
                        else
                        {
                            GetRefSourceByName(); //search
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion "Public Commands"

        #region "Public Commands Connect ==> Tbl90RefExpert"

        private RelayCommand _getRefExpertByNameCommand;
        public new ICommand GetRefExpertByNameCommand
        {
            get { return _getRefExpertByNameCommand ?? (_getRefExpertByNameCommand = new RelayCommand(GetRefExpertByName)); }
        }

        //----------------------------------------------------
        private RelayCommand _addRefExpertCommand;
        public new ICommand AddRefExpertCommand
        {
            get { return _addRefExpertCommand ?? (_addRefExpertCommand = new RelayCommand(AddRefExpert)); }
        }

        //---------------------------------------------------------------------------------------
        private RelayCommand _deleteRefExpertCommand;
        public new ICommand DeleteRefExpertCommand
        {
            get { return _deleteRefExpertCommand ?? (_deleteRefExpertCommand = new RelayCommand(DeleteRefExpert)); }
        }

        public new void DeleteRefExpert()
        {
            try
            {
                var refExpert = Tbl90ReferencesRepository.Tbl90References.FirstOrDefault(x => x.ReferenceID == CurrentTbl90RefExpert.ReferenceID);
                if (refExpert != null)
                {
                    if (MessageBox.Show("Are you really shure to delete this "
                                        + CurrentTbl90RefExpert.Info, "Question", MessageBoxButton.YesNo) !=
                        MessageBoxResult.Yes) return;
                    Tbl90ReferencesRepository.Delete(refExpert);
                    Tbl90ReferencesRepository.Save();
                    MessageBox.Show(CurrentTbl90RefExpert.Info + " was deleted successfully");
                    if (SearchRefExpertName == null)
                    {
                        GetConnectedTablesById(); //refresh doubleClick                    
                    }
                    else
                    {
                        GetRefExpertByName(); //search
                    }
                }
                else
                {
                    MessageBox.Show("Only " + CurrentTbl90RefExpert.Info + " can be deleted");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //-------------------------------------------------------------------------------------------------
        private RelayCommand _saveRefExpertCommand;
        public new ICommand SaveRefExpertCommand
        {
            get { return _saveRefExpertCommand ?? (_saveRefExpertCommand = new RelayCommand(SaveRefExpert)); }
        }

        private void SaveRefExpert()
        {
            try
            {
                var refExpert = Tbl90ReferencesRepository.Tbl90References.FirstOrDefault(x => x.ReferenceID == CurrentTbl90RefExpert.ReferenceID);
                if (CurrentTbl90RefExpert == null)
                {
                    MessageBox.Show("reference Expert was not found");
                }
                else
                {
                    if (CurrentTbl90RefExpert.ReferenceID != 0)
                    {
                        if (refExpert != null)
                        {
                            refExpert.Updater = Environment.UserName;
                            refExpert.UpdaterDate = DateTime.Now;
                        }
                    }
                    else
                    {
                        Tbl90ReferencesRepository.Add(new Tbl90Reference
                        {
                            RefExpertID = CurrentTbl90RefExpert.RefExpertID,
                            RefAuthorID= CurrentTbl90RefExpert.RefAuthorID,
                            CountID = TblCountersRepository.Counter(),
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
                        Tbl90ReferencesRepository.Save();
                        MessageBox.Show(CurrentTbl90RefExpert.Info + " was successfully saved ");
                        if (SearchRefExpertName == null)
                        {
                            GetConnectedTablesById(); //refresh doubleClick                    
                        }
                        else
                        {
                            GetRefExpertByName(); //search
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion "Public Commands"

        #region "Public Commands Connect ==> Tbl93Comment"

        private RelayCommand _getCommentByNameCommand;
        public new ICommand GetCommentByNameCommand
        {
            get { return _getCommentByNameCommand ?? (_getCommentByNameCommand = new RelayCommand(GetCommentByName)); }
        }

        //------------------------------------------------------------------------------

        private RelayCommand _addCommentCommand;
        public new ICommand AddCommentCommand
        {
            get { return _addCommentCommand ?? (_addCommentCommand = new RelayCommand(AddComment)); }
        }

        //---------------------------------------------------------------------------------------

        private RelayCommand _deleteCommentCommand;
        public new ICommand DeleteCommentCommand
        {
            get { return _deleteCommentCommand ?? (_deleteCommentCommand = new RelayCommand(DeleteComment)); }
        }

        public new void DeleteComment()
        {
            try
            {
                var comment = Tbl93CommentsRepository.Tbl93Comments.FirstOrDefault(x => x.CommentID == CurrentTbl93Comment.CommentID);
                if (comment != null)
                {
                    if (MessageBox.Show("Are you really shure to delete this CommentID "
                                        + CurrentTbl93Comment.CommentID
                                         , "Question", MessageBoxButton.YesNo) !=
                        MessageBoxResult.Yes) return;
                    Tbl93CommentsRepository.Delete(comment);
                    Tbl93CommentsRepository.Save();
                    MessageBox.Show("CommentID" + CurrentTbl93Comment.CommentID +
                          " was successfully deleted");
                    if (SearchCommentInfo == null)
                    {
                        GetConnectedTablesById(); //refresh doubleClick                    
                    }
                    else
                    {
                        GetCommentByName(); //search
                    }
                }
                else
                {
                    MessageBox.Show("Only CommentID " + CurrentTbl93Comment.CommentID +
                          " can be deleted");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //-------------------------------------------------------------------------------------------------

        private RelayCommand _saveCommentCommand;
        public new ICommand SaveCommentCommand
        {
            get { return _saveCommentCommand ?? (_saveCommentCommand = new RelayCommand(SaveComment)); }
        }

        public new void SaveComment()
        {
            try
            {
                var comment = Tbl93CommentsRepository.Tbl93Comments.FirstOrDefault(x => x.CommentID == CurrentTbl93Comment.CommentID);
                if (CurrentTbl93Comment == null)
                {
                    MessageBox.Show("comment was not found");
                }
                else
                {
                    if (CurrentTbl93Comment.CommentID != 0)
                    {
                        if (comment != null)
                        {
                            comment.Updater = Environment.UserName;
                            comment.UpdaterDate = DateTime.Now;
                        }
                    }
                    else
                    {
                        Tbl93CommentsRepository.Add(new Tbl93Comment()
                        {
                            RefAuthorID= CurrentTbl93Comment.RefAuthorID,                
                            CountID = TblCountersRepository.Counter(),
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
                        Tbl93CommentsRepository.Save();
                        MessageBox.Show("CommentID" + CurrentTbl93Comment.CommentID +
                                        " was successfully saved");
                        if (SearchCommentInfo == null)
                        {
                            GetConnectedTablesById(); //refresh doubleClick                    
                        }
                        else
                        {
                            GetCommentByName(); //search
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion "Public Commands"
  

     
        #region "Public Commands Connected Tables by DoubleClick"                                         

        private RelayCommand _getConnectedTablesCommand;
        public new ICommand GetConnectedTablesCommand
        {
            get { return _getConnectedTablesCommand ?? (_getConnectedTablesCommand = new RelayCommand(GetConnectedTablesById)); }
        }

        public new void GetConnectedTablesById()
        {
            //Clear Search-TextBox                                  
            SearchNULLName = null;                       
            SearchFiSpeciesName = null;
            SearchCommentInfo = null;
            SearchRefExpertName = null;
            SearchRefSourceName = null;
            SearchRefAuthorName = null;

            NULLList =
                new ObservableCollection<NULL>((from NULL in NULLRepository.NULL
                                                       where NULL.NULL== CurrentTbl90RefAuthor.NULL
                                                       orderby NULL.NULL
                                                       select NULL));

            View = CollectionViewSource.GetDefaultView(NULLList);
            if (View != null)
                View.CurrentChanged += NULLView_CurrentChanged;
            RaisePropertyChanged("CurrentNULL");
            //-----------------------------------------------------------------------------------
            Tbl69FiSpeciessesList =
                new ObservableCollection<NULL>((from NULL in Tbl69FiSpeciessesRepository.Tbl69FiSpeciesses
                                                       where NULL.RefAuthorID== CurrentTbl90RefAuthor.RefAuthorID
                                                       orderby NULL.Tbl90RefAuthors.RefAuthorName
                                                       select NULL));


            View = CollectionViewSource.GetDefaultView(Tbl69FiSpeciessesList);
            if (View != null)
                View.CurrentChanged += NULLView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl69FiSpeciesses");
            //-----------------------------------------------------------------------------------
            Tbl90RefAuthorsList =
                new ObservableCollection<Tbl90Reference>((from refAu in Tbl90ReferencesRepository.Tbl90References
                                                          where refAu.RefAuthorID== CurrentTbl90RefAuthor.RefAuthorID
                                                          && refAu.Tbl90RefExperts == null
                                                          && refAu.Tbl90RefSources == null
                                                          orderby refAu.Tbl90RefAuthors.RefAuthorName, refAu.Tbl90RefAuthors.BookName, refAu.Tbl90RefAuthors.Page1
                                                          select refAu));

            RefAuthorsView = CollectionViewSource.GetDefaultView(Tbl90RefAuthorsList);
            if (RefAuthorsView != null)
                RefAuthorsView.CurrentChanged += tbl90RefAuthorView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl90RefAuthor");
            //--------------------------------------------------------------------------------------
            Tbl90RefSourcesList =
                new ObservableCollection<Tbl90Reference>((from refSo in Tbl90ReferencesRepository.Tbl90References
                                                          where refSo.RefAuthorID== CurrentTbl90RefAuthor.RefAuthorID
                                                          && refSo.Tbl90RefExperts == null
                                                          && refSo.Tbl90RefAuthors == null
                                                          orderby refSo.Tbl90RefSources.RefSourceName
                                                          select refSo));

            RefSourcesView = CollectionViewSource.GetDefaultView(Tbl90RefSourcesList);
            if (RefSourcesView != null)
                RefSourcesView.CurrentChanged += tbl90RefSourceView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl90RefSource");
            //--------------------------------------------------------------------------------------
            Tbl90RefExpertsList =
                new ObservableCollection<Tbl90Reference>((from refEx in Tbl90ReferencesRepository.Tbl90References
                                                          where refEx.RefAuthorID== CurrentTbl90RefAuthor.RefAuthorID
                                                          && refEx.Tbl90RefAuthors == null
                                                          && refEx.Tbl90RefSources == null
                                                          orderby refEx.Tbl90RefExperts.RefExpertName
                                                          select refEx));

            RefExpertsView = CollectionViewSource.GetDefaultView(Tbl90RefExpertsList);
            if (RefExpertsView != null)
                RefExpertsView.CurrentChanged += tbl90RefExpertView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl90RefExpert");
            //-----------------------------------------------------------------------------------
            Tbl93CommentsList =
                new ObservableCollection<Tbl93Comment>((from comm in Tbl93CommentsRepository.Tbl93Comments
                                                          where comm.RefAuthorID== CurrentTbl90RefAuthor.RefAuthorID
                                                        orderby comm.Info
                                                        select comm));

            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            if (CommentsView != null)
                CommentsView.CurrentChanged += tbl93CommentView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl93Comment");
            //--------------------------------------------------------------

        }

        #endregion "Public Commands Connected Tables by DoubleClick"
   

     
        #region "Public Properties Tbl90RefAuthor"

        public new ICollectionView View;
        public new Tbl90RefAuthor CurrentTbl90RefAuthor
        {
            get
            {
                if (View != null)
                    return View.CurrentItem as Tbl90RefAuthor;
                return null;
            }
        }
        //--------------------------------------------
        private string _searchRefAuthorName;
        public new string SearchRefAuthorName
        {
            get { return _searchRefAuthorName; }
            set
            {
                if (value == _searchRefAuthorName) return;
                _searchRefAuthorName = value;
                RaisePropertyChanged("SearchRefAuthorName");
            }
        }

        private ObservableCollection<Tbl90RefAuthor> _tbl90RefAuthorsList;
        public new ObservableCollection<Tbl90RefAuthor> Tbl90RefAuthorsList
        {
            get { return _tbl90RefAuthorsList; }
            set
            {
                if (_tbl90RefAuthorsList == value) return;
                _tbl90RefAuthorsList = value;
                RaisePropertyChanged("Tbl90RefAuthorsList");

                //Clear Search-TextBox
                SearchNULL = null;                                
                SearchNULL = null;
                SearchCommentInfo = null;
                SearchRefExpertName = null;
                SearchRefSourceName = null;
                SearchRefAuthorName = null;
            }
        }

        private ObservableCollection<Tbl90RefAuthor> _tbl90RefAuthorsAllList;
        public ObservableCollection<Tbl90RefAuthor> Tbl90RefAuthorsAllList
        {
            get { return _tbl90RefAuthorsAllList; }
            set
            {
                if (_tbl90RefAuthorsAllList == value) return;
                _tbl90RefAuthorsAllList = value;
                RaisePropertyChanged("Tbl90RefAuthorsAllList");
            }
        }

        #endregion "Public Properties"
   

       
        #region "Public Properties NULL"

        public ICollectionView View;
        public NULL CurrentNULL
        {
            get
            {
                if (View != null)
                    return View.CurrentItem as NULL;
                return null;
            }
        }
        //--------------------------------------------                                               

        private string _searchNULL;
        public string SearchNULL
        {
            get { return _searchNULL; }
            set
            {
                if (value == _searchNULL) return;
                _searchNULL = value;
                RaisePropertyChanged("SearchNULL");
            }
        }

        private ObservableCollection<NULL> NULLList;
        public ObservableCollection<NULL> Tbl69FiSpeciessesList
        {
            get { return NULLList; }
            set
            {
                if (NULLList == value) return;
                NULLList = value;
                RaisePropertyChanged("Tbl69FiSpeciessesList");
            }
        }

        #endregion "Public Properties"
   
  
       
        #region "Public Properties NULL"

        public ICollectionView View;
        public NULL CurrentNULL
        {
            get
            {
                if (View != null)
                    return View.CurrentItem as NULL;
                return null;
            }
        }
        //--------------------------------------------                                               

        private string _searchNULL;
        public string SearchNULL
        {
            get { return _searchNULL; }
            set
            {
                if (value == _searchNULL) return;
                _searchNULL = value;
                RaisePropertyChanged("SearchNULL");
            }
        }

        private ObservableCollection<NULL> NULLList;
        public ObservableCollection<NULL> Tbl72PlSpeciessesList
        {
            get { return NULLList; }
            set
            {
                if (NULLList == value) return;
                NULLList = value;
                RaisePropertyChanged("Tbl72PlSpeciessesList");
            }
        }

        #endregion "Public Properties"
   
          
        #region "Private Methods"

        public void NULLView_CurrentChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged("CurrentNULL");
        }
        #endregion "Private Methods"
   
   

   
    }
}   
