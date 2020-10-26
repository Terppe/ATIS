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

//    Tbl84SynonymViewModel Skriptdatum:  15.03.2012  10:32    

namespace Atis.WpfUi.ViewModel
{   


    
    public class Tbl84SynonymsViewModel : Tbl69FiSpeciessesViewModel                     
    {     
         
      #region "Private Data Members"

        protected readonly Tbl78NamesRepository Tbl78NamesRepository = new Tbl78NamesRepository();   
          
          #endregion "Private Data Members"            
    
        #region "Constructor"

        public Tbl84SynonymsViewModel()
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
       
        #region "Public Commands Basic Tbl84Synonym"

        private RelayCommand _getSynonymByNameCommand;
        public new ICommand GetSynonymByNameCommand
        {
            get { return _getSynonymByNameCommand ?? (_getSynonymByNameCommand = new RelayCommand(GetSynonymByName)); }
        }

        private void GetSynonymByName()
        {   
Tbl84SynonymsList =
                 new ObservableCollection<Tbl84Synonym>((from x in Tbl84SynonymsRepository.Tbl84Synonyms
                                                        where x.SynonymName.StartsWith(SearchSynonymName)
                                                        orderby x.SynonymName
                                                        select x));

            Tbl84SynonymsAllList =
                 new ObservableCollection<Tbl84Synonym>((from y in Tbl84SynonymsRepository.Tbl84Synonyms
                                                        orderby y.SynonymName
                                                        select y));

            Tbl69FiSpeciessesAllList =
                 new ObservableCollection<Tbl69FiSpecies>((from z in Tbl69FiSpeciessesRepository.Tbl69FiSpeciesses
                                                        orderby z.FiSpeciesName
                                                        select z));

              
  Tbl66GenussesAllList =
                 new ObservableCollection<Tbl66Genus>((from z in Tbl66GenussesRepository.Tbl66Genusses
                                                        orderby z.GenusName
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
  Tbl78NamesList = null;     
             
  View = CollectionViewSource.GetDefaultView(Tbl84SynonymsList);
            if (View != null)
                View.CurrentChanged += tbl84SynonymView_CurrentChanged;                   
            RaisePropertyChanged("CurrentTbl84Synonym");
        }
        //------------------------------------------------------------------------------
        private RelayCommand _addSynonymCommand;
        public new ICommand AddSynonymCommand
        {
            get { return _addSynonymCommand ?? (_addSynonymCommand = new RelayCommand(AddSynonym)); }
        }

        private void AddSynonym()
        {
            if (Tbl84SynonymsList == null)
                Tbl84SynonymsList = new ObservableCollection<Tbl84Synonym>();
            Tbl84SynonymsList.Add(new Tbl84Synonym{ SynonymName= "New " });
            View = CollectionViewSource.GetDefaultView(Tbl84SynonymsList);
            if (View != null)
                View.CurrentChanged += tbl84SynonymView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl84Synonym");
        }
        //---------------------------------------------------------------------------------------
  
       
        private RelayCommand _deleteSynonymCommand;
        public new ICommand DeleteSynonymCommand
        {
            get { return _deleteSynonymCommand ?? (_deleteSynonymCommand = new RelayCommand(DeleteSynonym)); }
        }

        private void DeleteSynonym()
        {
            try
            {
                var synonym= Tbl84SynonymsRepository.Tbl84Synonyms.FirstOrDefault(x => x.SynonymID== CurrentTbl84Synonym.SynonymID);
                if (synonym!= null)
                {
                    if (MessageBox.Show("Are you really shure to delete this "
                                        + CurrentTbl84Synonym.SynonymName, "Question", MessageBoxButton.YesNo) !=
                        MessageBoxResult.Yes) return;
                    Tbl84SynonymsRepository.Delete(synonym);
                    Tbl84SynonymsRepository.Save();
                    MessageBox.Show(CurrentTbl84Synonym.SynonymName + " was deleted successfully");
                    GetSynonymByName(); //Refresh
                }
                else
                {
                    MessageBox.Show("Only " + CurrentTbl84Synonym.SynonymName+ " can be deleted");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //-------------------------------------------------------------------------------------------------
        private RelayCommand _saveSynonymCommand;
        public new ICommand SaveSynonymCommand
        {
            get { return _saveSynonymCommand ?? (_saveSynonymCommand = new RelayCommand(SaveSynonym)); }
        }

        private void SaveSynonym()
        {
            try
            {
                var synonym= Tbl84SynonymsRepository.Tbl84Synonyms.FirstOrDefault(x => x.SynonymID== CurrentTbl84Synonym.SynonymID);
                if (CurrentTbl84Synonym == null)
                {
                    MessageBox.Show("synonym was not found");
                }
                else
                {
                    if (CurrentTbl84Synonym.SynonymID!= 0)
                    {
                        if (synonym!= null) //update
                        {
                            synonym.Updater = Environment.UserName;
                            synonym.UpdaterDate = DateTime.Now;
                        }
                    }
                    else
                    {
                        Tbl84SynonymsRepository.Add(new Tbl84Synonym
                        {
                            FiSpeciesID= CurrentTbl84Synonym.FiSpeciesID,              
                            SynonymName= CurrentTbl84Synonym.SynonymName,              
                            CountID = TblCountersRepository.Counter(),
                            Valid = CurrentTbl84Synonym.Valid,
                            ValidYear = CurrentTbl84Synonym.ValidYear,
                            Synonym = CurrentTbl84Synonym.Synonym,
                            Author = CurrentTbl84Synonym.Author,
                            AuthorYear = CurrentTbl84Synonym.AuthorYear,
                            Info = CurrentTbl84Synonym.Info,
                            EngName = CurrentTbl84Synonym.EngName,
                            GerName = CurrentTbl84Synonym.GerName,
                            FraName = CurrentTbl84Synonym.FraName,
                            PorName = CurrentTbl84Synonym.PorName,
                            Writer = Environment.UserName,
                            WriterDate = DateTime.Now,
                            Updater = Environment.UserName,
                            UpdaterDate = DateTime.Now,
                            Memo = CurrentTbl84Synonym.Memo
                        });
                    }
                    {
                        Tbl84SynonymsRepository.Save();
                        MessageBox.Show(CurrentTbl84Synonym.SynonymName+  " was successfully saved ");
                        GetSynonymByName();  //Refresh
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion "Public Commands"
  

        
        #region "Public Commands Connect <== Tbl69FiSpecies"                 

        private RelayCommand _getFiSpeciesByNameCommand;
        public new ICommand GetFiSpeciesByNameCommand
        {
            get { return _getFiSpeciesByNameCommand ?? (_getFiSpeciesByNameCommand = new RelayCommand(GetFiSpeciesByName)); }
        }

        private void GetFiSpeciesByName()
        {
            Tbl69FiSpeciessesList =
                new ObservableCollection<Tbl69FiSpecies>((from fispecies in Tbl69FiSpeciessesRepository.Tbl69FiSpeciesses
                                                       where fispecies.FiSpeciesName.StartsWith(SearchFiSpeciesName)
                                                       orderby fispecies.FiSpeciesName
                                                       select fispecies));

            View = CollectionViewSource.GetDefaultView(Tbl69FiSpeciessesList);
            if (View != null)
                View.CurrentChanged += tbl69FiSpeciesView_CurrentChanged;                   
            RaisePropertyChanged("CurrentTbl69FiSpecies");
        }
        //------------------------------------------------------------------------------
        private RelayCommand _addFiSpeciesCommand;
        public new ICommand AddFiSpeciesCommand
        {
            get { return _addFiSpeciesCommand ?? (_addFiSpeciesCommand = new RelayCommand(AddFiSpecies)); }
        }

        private void AddFiSpecies()
        {
            if (Tbl69FiSpeciessesList == null)
                Tbl69FiSpeciessesList = new ObservableCollection<Tbl69FiSpecies>();
            Tbl69FiSpeciessesList.Add(new Tbl69FiSpecies{ FiSpeciesName= "New " });                   
            View = CollectionViewSource.GetDefaultView(Tbl69FiSpeciessesList);
            if (View != null)
                View.CurrentChanged += tbl69FiSpeciesView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl69FiSpecies");
        }
        //----------------------------------------------------------------------------------------------------------
        private RelayCommand _deleteFiSpeciesCommand;
        public ICommand FiSpeciesPhylumCommand
        {
            get { return _deleteFiSpeciesCommand ?? (_deleteFiSpeciesCommand = new RelayCommand(DeleteFiSpecies)); }
        }

        private void DeleteFiSpecies()
        {
            try
            {
                var fispecies= Tbl69FiSpeciessesRepository.Tbl69FiSpeciesses.FirstOrDefault(x => x.FiSpeciesID== CurrentTbl69FiSpecies.FiSpeciesID);
                if (fispecies!= null)
                {
                    if (MessageBox.Show("Are you really shure to delete this "
                                        + CurrentTbl69FiSpecies.FiSpeciesName, "Question", MessageBoxButton.YesNo) !=
                        MessageBoxResult.Yes) return;
                    Tbl69FiSpeciessesRepository.Delete(fispecies);
                    Tbl69FiSpeciessesRepository.Save();
                    MessageBox.Show(CurrentTbl69FiSpecies.FiSpeciesName+ " was deleted successfully");
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
                    MessageBox.Show("Only " + CurrentTbl69FiSpecies.FiSpeciesName+ " can be deleted");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //-------------------------------------------------------------------------------------------------
        private RelayCommand _saveFiSpeciesCommand;   
        public new ICommand SaveFiSpeciesCommand
        {
            get { return _saveFiSpeciesCommand ?? (_saveFiSpeciesCommand = new RelayCommand(SaveFiSpecies)); }
        }

        private void SaveFiSpecies()
        {
            try
            {
                var fispecies= Tbl69FiSpeciessesRepository.Tbl69FiSpeciesses.FirstOrDefault(x => x.FiSpeciesID== CurrentTbl69FiSpecies.FiSpeciesID);
                if (CurrentTbl69FiSpecies == null)
                {
                    MessageBox.Show("fispecies was not found");
                }
                else
                {
                    if (CurrentTbl69FiSpecies.FiSpeciesID!= 0)
                    {
                        if (fispecies!= null) //update
                        {
                            fispecies.Updater = Environment.UserName;
                            fispecies.UpdaterDate = DateTime.Now;
                        }
                    }
                    else
                    {
                        Tbl69FiSpeciessesRepository.Add(new Tbl69FiSpecies()
                        {
                            FiSpeciesName= CurrentTbl69FiSpecies.FiSpeciesName,              
                            CountID = TblCountersRepository.Counter(),
                            Valid = CurrentTbl69FiSpecies.Valid,
                            ValidYear = CurrentTbl69FiSpecies.ValidYear,
                            Synonym = CurrentTbl69FiSpecies.Synonym,
                            Author = CurrentTbl69FiSpecies.Author,
                            AuthorYear = CurrentTbl69FiSpecies.AuthorYear,
                            Info = CurrentTbl69FiSpecies.Info,
                            EngName = CurrentTbl69FiSpecies.EngName,
                            GerName = CurrentTbl69FiSpecies.GerName,
                            FraName = CurrentTbl69FiSpecies.FraName,
                            PorName = CurrentTbl69FiSpecies.PorName,
                            Writer = Environment.UserName,
                            WriterDate = DateTime.Now,
                            Updater = Environment.UserName,
                            UpdaterDate = DateTime.Now,
                            Memo = CurrentTbl69FiSpecies.Memo
                        });
                    }
                    {
                        Tbl69FiSpeciessesRepository.Save();
                        MessageBox.Show(CurrentTbl69FiSpecies.FiSpeciesName+  " was successfully saved ");
                        GetFiSpeciesByName();  //Refresh
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

        private RelayCommand _getNameByNameCommand;
        public ICommand GetNameByNameCommand
        {
            get { return _getNameByNameCommand ?? (_getNameByNameCommand = new RelayCommand(GetNameByName)); }
        }

        private void GetNameByName()
        {
            Tbl78NamesList =
                new ObservableCollection<NULL>((from NULL in Tbl78NamesRepository.Tbl78Names
                                                       where NULL.NULL.StartsWith(SearchNameName)
                                                       orderby NULL.NULL
                                                       select NULL));

            View = CollectionViewSource.GetDefaultView(Tbl78NamesList);
            if (View != null)
                View.CurrentChanged += NULLView_CurrentChanged;                   
            RaisePropertyChanged("CurrentNULL");
        }
        //------------------------------------------------------------------------------
        private RelayCommand _addNameCommand;
        public ICommand AddNameCommand
        {
            get { return _addNameCommand ?? (_addNameCommand = new RelayCommand(AddName)); }
        }

        private void AddName()
        {
            if (Tbl78NamesList == null)
                Tbl78NamesList = new ObservableCollection<NULL>();
            Tbl78NamesList.Add(new NULL{ NULL= "New " });                   
            View = CollectionViewSource.GetDefaultView(Tbl78NamesList);
            if (View != null)
                View.CurrentChanged += NULLView_CurrentChanged;
            RaisePropertyChanged("CurrentNULL");
        }
        //---------------------------------------------------------------------------------------
        private RelayCommand _deleteNameCommand;
        public ICommand DeleteNameCommand
        {
            get { return _deleteNameCommand ?? (_deleteNameCommand = new RelayCommand(DeleteName)); }
        }

        private void DeleteName()
        {
            try
            {
                var NULL = Tbl78NamesRepository.Tbl78Names.FirstOrDefault(x => x.NULL== CurrentNULL.NULL);
                if (NULL != null)
                {
                    if (MessageBox.Show("Are you really shure to delete this "
                                        + CurrentNULL.NULL, "Question", MessageBoxButton.YesNo) !=
                        MessageBoxResult.Yes) return;
                    Tbl78NamesRepository.Delete(NULL);
                    Tbl78NamesRepository.Save();
                    MessageBox.Show(CurrentNULL.NULL+ " was deleted successfully");
                    if (SearchNameName == null)
                    {
                        GetConnectedTablesById(); //refresh doubleClick                    
                    }
                    else
                    {
                        GetNameByName(); //search
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
        private RelayCommand _saveNameCommand;   
        public ICommand SaveNameCommand
        {
            get { return _saveNameCommand ?? (_saveNameCommand = new RelayCommand(SaveName)); }
        }

        private void SaveName()
        {
            try
            {
                var NULL = Tbl78NamesRepository.Tbl78Names.FirstOrDefault(x => x.NULL== CurrentNULL.NULL);
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
                        Tbl78NamesRepository.Add(new NULL
                        {
                            SynonymID= CurrentNULL.SynonymID,              
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
                        Tbl78NamesRepository.Save();
                        MessageBox.Show(CurrentNULL.NULL+  " was successfully saved ");
                        if (SearchNameName == null)
                        {
                            GetConnectedTablesById(); //refresh doubleClick                    
                        }
                        else
                        {
                            GetNameByName(); //search
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

        private RelayCommand _getImageByNameCommand;
        public ICommand GetImageByNameCommand
        {
            get { return _getImageByNameCommand ?? (_getImageByNameCommand = new RelayCommand(GetImageByName)); }
        }

        private void GetImageByName()
        {
            Tbl81ImagesList =
                new ObservableCollection<NULL>((from NULL in Tbl81ImagesRepository.Tbl81Images
                                                       where NULL.NULL.StartsWith(SearchImageName)
                                                       orderby NULL.NULL
                                                       select NULL));

            View = CollectionViewSource.GetDefaultView(Tbl81ImagesList);
            if (View != null)
                View.CurrentChanged += NULLView_CurrentChanged;                   
            RaisePropertyChanged("CurrentNULL");
        }
        //------------------------------------------------------------------------------
        private RelayCommand _addImageCommand;
        public ICommand AddImageCommand
        {
            get { return _addImageCommand ?? (_addImageCommand = new RelayCommand(AddImage)); }
        }

        private void AddImage()
        {
            if (Tbl81ImagesList == null)
                Tbl81ImagesList = new ObservableCollection<NULL>();
            Tbl81ImagesList.Add(new NULL{ NULL= "New " });                   
            View = CollectionViewSource.GetDefaultView(Tbl81ImagesList);
            if (View != null)
                View.CurrentChanged += NULLView_CurrentChanged;
            RaisePropertyChanged("CurrentNULL");
        }
        //---------------------------------------------------------------------------------------
        private RelayCommand _deleteImageCommand;
        public ICommand DeleteImageCommand
        {
            get { return _deleteImageCommand ?? (_deleteImageCommand = new RelayCommand(DeleteImage)); }
        }

        private void DeleteImage()
        {
            try
            {
                var NULL= Tbl81ImagesRepository.Tbl81Images.FirstOrDefault(x => x.NULL== CurrentNULL.NULL);
                if (NULL!= null)
                {
                    if (MessageBox.Show("Are you really shure to delete this "
                                        + CurrentNULL.NULL, "Question", MessageBoxButton.YesNo) !=
                        MessageBoxResult.Yes) return;
                    Tbl81ImagesRepository.Delete(NULL);
                    Tbl81ImagesRepository.Save();
                    MessageBox.Show(CurrentNULL.NULL+ " was deleted successfully");
                    if (SearchImageName == null)
                    {
                        GetConnectedTablesById(); //refresh doubleClick                    
                    }
                    else
                    {
                        GetImageByName(); //search
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
        private RelayCommand _saveImageCommand;   
        public ICommand SaveImageCommand
        {
            get { return _saveImageCommand ?? (_saveImageCommand = new RelayCommand(SaveImage)); }
        }

        private void SaveImage()
        {
            try
            {
                var NULL= Tbl81ImagesRepository.Tbl81Images.FirstOrDefault(x => x.NULL== CurrentNULL.NULL);
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
                        Tbl81ImagesRepository.Add(new NULL
                        {
                            SynonymID= CurrentNULL.SynonymID,              
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
                        Tbl81ImagesRepository.Save();
                        MessageBox.Show(CurrentNULL.NULL+  " was successfully saved ");              
                        if (SearchImageName == null)
                        {
                            GetConnectedTablesById(); //refresh doubleClick                    
                        }
                        else
                        {
                            GetImageByName(); //search
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
                            SynonymID= CurrentTbl90RefAuthor.SynonymID,
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
                            SynonymID= CurrentTbl90RefSource.SynonymID,
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
                            SynonymID= CurrentTbl90RefExpert.SynonymID,
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
                            SynonymID= CurrentTbl93Comment.SynonymID,                
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
            SearchFiSpeciesName = null;                       
            SearchNameName = null;
            SearchCommentInfo = null;
            SearchRefExpertName = null;
            SearchRefSourceName = null;
            SearchRefAuthorName = null;

            Tbl69FiSpeciessesList =
                new ObservableCollection<Tbl69FiSpecies>((from fispecies in Tbl69FiSpeciessesRepository.Tbl69FiSpeciesses
                                                       where fispecies.FiSpeciesID== CurrentTbl84Synonym.FiSpeciesID
                                                       orderby fispecies.FiSpeciesName
                                                       select fispecies));

            View = CollectionViewSource.GetDefaultView(Tbl69FiSpeciessesList);
            if (View != null)
                View.CurrentChanged += tbl69FiSpeciesView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl69FiSpeciesses");
            //-----------------------------------------------------------------------------------
            Tbl78NamesList =
                new ObservableCollection<NULL>((from NULL in Tbl78NamesRepository.Tbl78Names
                                                       where NULL.SynonymID== CurrentTbl84Synonym.SynonymID
                                                       orderby NULL.Tbl84Synonyms.SynonymName
                                                       select NULL));


            View = CollectionViewSource.GetDefaultView(Tbl78NamesList);
            if (View != null)
                View.CurrentChanged += NULLView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl78Names");
            //-----------------------------------------------------------------------------------
            Tbl90RefAuthorsList =
                new ObservableCollection<Tbl90Reference>((from refAu in Tbl90ReferencesRepository.Tbl90References
                                                          where refAu.SynonymID== CurrentTbl84Synonym.SynonymID
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
                                                          where refSo.SynonymID== CurrentTbl84Synonym.SynonymID
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
                                                          where refEx.SynonymID== CurrentTbl84Synonym.SynonymID
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
                                                          where comm.SynonymID== CurrentTbl84Synonym.SynonymID
                                                        orderby comm.Info
                                                        select comm));

            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            if (CommentsView != null)
                CommentsView.CurrentChanged += tbl93CommentView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl93Comment");
            //--------------------------------------------------------------

        }

        #endregion "Public Commands Connected Tables by DoubleClick"
   

     
        #region "Public Properties Tbl84Synonym"

        public new ICollectionView View;
        public new Tbl84Synonym CurrentTbl84Synonym
        {
            get
            {
                if (View != null)
                    return View.CurrentItem as Tbl84Synonym;
                return null;
            }
        }
        //--------------------------------------------
        private string _searchSynonymName;
        public new string SearchSynonymName
        {
            get { return _searchSynonymName; }
            set
            {
                if (value == _searchSynonymName) return;
                _searchSynonymName = value;
                RaisePropertyChanged("SearchSynonymName");
            }
        }

        private ObservableCollection<Tbl84Synonym> _tbl84SynonymsList;
        public new ObservableCollection<Tbl84Synonym> Tbl84SynonymsList
        {
            get { return _tbl84SynonymsList; }
            set
            {
                if (_tbl84SynonymsList == value) return;
                _tbl84SynonymsList = value;
                RaisePropertyChanged("Tbl84SynonymsList");

                //Clear Search-TextBox
                SearchFiSpeciesName = null;                                
                SearchNULL = null;
                SearchCommentInfo = null;
                SearchRefExpertName = null;
                SearchRefSourceName = null;
                SearchRefAuthorName = null;
            }
        }

        private ObservableCollection<Tbl84Synonym> _tbl84SynonymsAllList;
        public ObservableCollection<Tbl84Synonym> Tbl84SynonymsAllList
        {
            get { return _tbl84SynonymsAllList; }
            set
            {
                if (_tbl84SynonymsAllList == value) return;
                _tbl84SynonymsAllList = value;
                RaisePropertyChanged("Tbl84SynonymsAllList");
            }
        }

        #endregion "Public Properties"
   

       
        #region "Public Properties Tbl69FiSpecies"

        public new ICollectionView View;
        public new Tbl69FiSpecies CurrentTbl69FiSpecies
        {
            get
            {
                if (View != null)
                    return View.CurrentItem as Tbl69FiSpecies;
                return null;
            }
        }
        //--------------------------------------------                                               

        private string _searchFiSpeciesName;
        public new string SearchFiSpeciesName
        {
            get { return _searchFiSpeciesName; }
            set
            {
                if (value == _searchFiSpeciesName) return;
                _searchFiSpeciesName = value;
                RaisePropertyChanged("SearchFiSpeciesName");
            }
        }

        private ObservableCollection<Tbl69FiSpecies> _tbl69FiSpeciessesList;
        public new ObservableCollection<Tbl69FiSpecies> Tbl69FiSpeciessesList
        {
            get { return _tbl69FiSpeciessesList; }
            set
            {
                if (_tbl69FiSpeciessesList == value) return;
                _tbl69FiSpeciessesList = value;
                RaisePropertyChanged("Tbl69FiSpeciessesList");
            }
        }

        #endregion "Public Properties"
   
  
       
        #region "Public Properties Tbl72PlSpecies"

        public new ICollectionView View;
        public new Tbl72PlSpecies CurrentTbl72PlSpecies
        {
            get
            {
                if (View != null)
                    return View.CurrentItem as Tbl72PlSpecies;
                return null;
            }
        }
        //--------------------------------------------                                               

        private string _searchPlSpeciesName;
        public new string SearchPlSpeciesName
        {
            get { return _searchPlSpeciesName; }
            set
            {
                if (value == _searchPlSpeciesName) return;
                _searchPlSpeciesName = value;
                RaisePropertyChanged("SearchPlSpeciesName");
            }
        }

        private ObservableCollection<Tbl72PlSpecies> _tbl72PlSpeciessesList;
        public new ObservableCollection<Tbl72PlSpecies> Tbl72PlSpeciessesList
        {
            get { return _tbl72PlSpeciessesList; }
            set
            {
                if (_tbl72PlSpeciessesList == value) return;
                _tbl72PlSpeciessesList = value;
                RaisePropertyChanged("Tbl72PlSpeciessesList");
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
        public ObservableCollection<NULL> Tbl78NamesList
        {
            get { return NULLList; }
            set
            {
                if (NULLList == value) return;
                NULLList = value;
                RaisePropertyChanged("Tbl78NamesList");
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
        public ObservableCollection<NULL> Tbl81ImagesList
        {
            get { return NULLList; }
            set
            {
                if (NULLList == value) return;
                NULLList = value;
                RaisePropertyChanged("Tbl81ImagesList");
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
