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

//    Tbl03RegnumViewModel Skriptdatum:  14.03.2014  12:32      

namespace Atis.WpfUi.ViewModel
{   


    
    public class Tbl03RegnumsViewModel : ViewModelBase                     
    {     
        
        #region "Private Data Members"

        protected readonly Tbl03RegnumsRepository Tbl03RegnumsRepository = new Tbl03RegnumsRepository();   
           
        protected readonly Tbl06PhylumsRepository Tbl06PhylumsRepository = new Tbl06PhylumsRepository();   
           
        protected readonly Tbl09DivisionsRepository Tbl09DivisionsRepository = new Tbl09DivisionsRepository();   
      
        protected readonly Tbl90ReferencesRepository Tbl90ReferencesRepository = new Tbl90ReferencesRepository();
        protected readonly Tbl90RefAuthorsRepository Tbl90RefAuthorsRepository = new Tbl90RefAuthorsRepository();
        protected readonly Tbl90RefSourcesRepository Tbl90RefSourcesRepository = new Tbl90RefSourcesRepository();
        protected readonly Tbl90RefExpertsRepository Tbl90RefExpertsRepository = new Tbl90RefExpertsRepository();
        protected readonly Tbl93CommentsRepository Tbl93CommentsRepository = new Tbl93CommentsRepository();
        protected readonly TblCountersRepository TblCountersRepository = new TblCountersRepository();

        #endregion "Private Data Members"            
    
        #region "Constructor"

        public Tbl03RegnumsViewModel()
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
       
        #region "Public Commands Basic Tbl03Regnum"

        private RelayCommand _getRegnumByNameCommand;
        public new ICommand GetRegnumByNameCommand
        {
            get { return _getRegnumByNameCommand ?? (_getRegnumByNameCommand = new RelayCommand(GetRegnumByName)); }
        }

        private void GetRegnumByName()
        {   
   
            Tbl03RegnumsList =
                 new ObservableCollection<Tbl03Regnum>((from regnum in Tbl03RegnumsRepository.Tbl03Regnums
                                                        where regnum.RegnumName.StartsWith(SearchRegnumName)
                                                        orderby regnum.RegnumName, regnum.Subregnum
                                                        select regnum));

            Tbl03RegnumsAllList =
                 new ObservableCollection<Tbl03Regnum>((from reg in Tbl03RegnumsRepository.Tbl03Regnums
                                                        orderby reg.RegnumName, reg.Subregnum
                                                        select reg));
  
       
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

  
  
                Tbl06PhylumsList = null;                 
                Tbl09DivisionsList = null;                       
RegnumsView = CollectionViewSource.GetDefaultView(Tbl03RegnumsList);
            if (RegnumsView != null)
                RegnumsView.CurrentChanged += tbl03RegnumView_CurrentChanged;                   
            RaisePropertyChanged("CurrentTbl03Regnum");
        }
        //------------------------------------------------------------------------------
        private RelayCommand _addRegnumCommand;
        public ICommand AddRegnumCommand
        {
            get { return _addRegnumCommand ?? (_addRegnumCommand = new RelayCommand(AddRegnum)); }
        }

        private void AddRegnum()
        {
            if (Tbl03RegnumsList == null)
                Tbl03RegnumsList = new ObservableCollection<Tbl03Regnum>();
            Tbl03RegnumsList.Add(new Tbl03Regnum{ RegnumName= "New " });
            RegnumsView = CollectionViewSource.GetDefaultView(Tbl03RegnumsList);
            if (RegnumsView != null)
                RegnumsView.CurrentChanged += tbl03RegnumView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl03Regnum");
        }
        //---------------------------------------------------------------------------------------
     
        private RelayCommand _deleteRegnumCommand;
        public ICommand DeleteRegnumCommand
        {
            get { return _deleteRegnumCommand ?? (_deleteRegnumCommand = new RelayCommand(DeleteRegnum)); }
        }

        private void DeleteRegnum()
        {
            try
            {
                var regnum = Tbl03RegnumsRepository.Tbl03Regnums.FirstOrDefault(x => x.RegnumID == CurrentTbl03Regnum.RegnumID);
                if (regnum != null)
                {
                    if (MessageBox.Show("Are you really shure to delete this "
                                        + CurrentTbl03Regnum.RegnumName + " " + CurrentTbl03Regnum.Subregnum, "Question", MessageBoxButton.YesNo) !=
                        MessageBoxResult.Yes) return;
                    Tbl03RegnumsRepository.Delete(regnum);
                    Tbl03RegnumsRepository.Save();
                    MessageBox.Show(CurrentTbl03Regnum.RegnumName + " " + CurrentTbl03Regnum.Subregnum + " was deleted successfully");
                    GetRegnumByName(); //Refresh
                }
                else
                {
                    MessageBox.Show("Only " + CurrentTbl03Regnum.RegnumName + " " + CurrentTbl03Regnum.Subregnum + " can be deleted");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //-------------------------------------------------------------------------------------------------
        private RelayCommand _saveRegnumCommand;
        public ICommand SaveRegnumCommand
        {
            get { return _saveRegnumCommand ?? (_saveRegnumCommand = new RelayCommand(SaveRegnum)); }
        }

        private void SaveRegnum()
        {
            try
            {
                var regnum = Tbl03RegnumsRepository.Tbl03Regnums.FirstOrDefault(x => x.RegnumID == CurrentTbl03Regnum.RegnumID);
                if (CurrentTbl03Regnum == null)
                {
                    MessageBox.Show("regnum-subregnum was not found");
                }
                else
                {
                    if (CurrentTbl03Regnum.RegnumID != 0)
                    {
                        if (regnum != null) //update
                        {
                            regnum.Updater = Environment.UserName;
                            regnum.UpdaterDate = DateTime.Now;
                        }
                    }
                    else
                    {
                        Tbl03RegnumsRepository.Add(new Tbl03Regnum()
                        {
                            RegnumName = CurrentTbl03Regnum.RegnumName,
                            Subregnum = CurrentTbl03Regnum.Subregnum,
                            CountID = TblCountersRepository.Counter(),
                            Valid = CurrentTbl03Regnum.Valid,
                            ValidYear = CurrentTbl03Regnum.ValidYear,
                            Synonym = CurrentTbl03Regnum.Synonym,
                            Author = CurrentTbl03Regnum.Author,
                            AuthorYear = CurrentTbl03Regnum.AuthorYear,
                            Info = CurrentTbl03Regnum.Info,
                            EngName = CurrentTbl03Regnum.EngName,
                            GerName = CurrentTbl03Regnum.GerName,
                            FraName = CurrentTbl03Regnum.FraName,
                            PorName = CurrentTbl03Regnum.PorName,
                            Writer = Environment.UserName,
                            WriterDate = DateTime.Now,
                            Updater = Environment.UserName,
                            UpdaterDate = DateTime.Now,
                            Memo = CurrentTbl03Regnum.Memo
                        });
                    }
                    {
                        Tbl03RegnumsRepository.Save();
                        MessageBox.Show(CurrentTbl03Regnum.RegnumName + " " + CurrentTbl03Regnum.Subregnum +
                                        " was successfully saved ");
                        GetRegnumByName();  //Refresh
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion "Public Commands"
  

        
        #region "Public Commands Connect ==> Tbl06Phylum"                 

        private RelayCommand _getPhylumByNameCommand;
        public ICommand GetPhylumByNameCommand
        {
            get { return _getPhylumByNameCommand ?? (_getPhylumByNameCommand = new RelayCommand(GetPhylumByName)); }
        }

        private void GetPhylumByName()
        {
            Tbl06PhylumsList =
                new ObservableCollection<Tbl06Phylum>((from phylum in Tbl06PhylumsRepository.Tbl06Phylums
                                                       where phylum.PhylumName.StartsWith(SearchPhylumName)
                                                       orderby phylum.PhylumName
                                                       select phylum));

            PhylumsView = CollectionViewSource.GetDefaultView(Tbl06PhylumsList);
            if (PhylumsView != null)
                PhylumsView.CurrentChanged += tbl06PhylumView_CurrentChanged;                   
            RaisePropertyChanged("CurrentTbl06Phylum");
        }
        //------------------------------------------------------------------------------
        private RelayCommand _addPhylumCommand;
        public ICommand AddPhylumCommand
        {
            get { return _addPhylumCommand ?? (_addPhylumCommand = new RelayCommand(AddPhylum)); }
        }

        private void AddPhylum()
        {
            if (Tbl06PhylumsList == null)
                Tbl06PhylumsList = new ObservableCollection<Tbl06Phylum>();
            Tbl06PhylumsList.Add(new Tbl06Phylum{ PhylumName= "New " });                   
            PhylumsView = CollectionViewSource.GetDefaultView(Tbl06PhylumsList);
            if (PhylumsView != null)
                PhylumsView.CurrentChanged += tbl06PhylumView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl06Phylum");
        }
        //---------------------------------------------------------------------------------------
        private RelayCommand _deletePhylumCommand;
        public ICommand DeletePhylumCommand
        {
            get { return _deletePhylumCommand ?? (_deletePhylumCommand = new RelayCommand(DeletePhylum)); }
        }

        private void DeletePhylum()
        {
            try
            {
                var phylum = Tbl06PhylumsRepository.Tbl06Phylums.FirstOrDefault(x => x.PhylumID== CurrentTbl06Phylum.PhylumID);
                if (phylum != null)
                {
                    if (MessageBox.Show("Are you really shure to delete this "
                                        + CurrentTbl06Phylum.PhylumName, "Question", MessageBoxButton.YesNo) !=
                        MessageBoxResult.Yes) return;
                    Tbl06PhylumsRepository.Delete(phylum);
                    Tbl06PhylumsRepository.Save();
                    MessageBox.Show(CurrentTbl06Phylum.PhylumName+ " was deleted successfully");
                    if (SearchPhylumName == null)
                    {
                        GetConnectedTablesById(); //refresh doubleClick                    
                    }
                    else
                    {
                        GetPhylumByName(); //search
                    }
                }
                else
                {
                    MessageBox.Show("Only " + CurrentTbl06Phylum.PhylumName+ " can be deleted");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //-------------------------------------------------------------------------------------------------
        private RelayCommand _savePhylumCommand;   
        public ICommand SavePhylumCommand
        {
            get { return _savePhylumCommand ?? (_savePhylumCommand = new RelayCommand(SavePhylum)); }
        }

        private void SavePhylum()
        {
            try
            {
                var phylum = Tbl06PhylumsRepository.Tbl06Phylums.FirstOrDefault(x => x.PhylumID== CurrentTbl06Phylum.PhylumID);
                if (CurrentTbl06Phylum == null)
                {
                    MessageBox.Show("phylum was not found");
                }
                else
                {
                    if (CurrentTbl06Phylum.PhylumID!= 0)
                    {
                        if (phylum!= null) //update
                        {
                            phylum.Updater = Environment.UserName;
                            phylum.UpdaterDate = DateTime.Now;
                        }
                    }
                    else
                    {
                        Tbl06PhylumsRepository.Add(new Tbl06Phylum
                        {
                            RegnumID= CurrentTbl06Phylum.RegnumID,              
                            PhylumName= CurrentTbl06Phylum.PhylumName,              
                            CountID = TblCountersRepository.Counter(),
                            Valid = CurrentTbl06Phylum.Valid,
                            ValidYear = CurrentTbl06Phylum.ValidYear,
                            Synonym = CurrentTbl06Phylum.Synonym,
                            Author = CurrentTbl06Phylum.Author,
                            AuthorYear = CurrentTbl06Phylum.AuthorYear,
                            Info = CurrentTbl06Phylum.Info,
                            EngName = CurrentTbl06Phylum.EngName,
                            GerName = CurrentTbl06Phylum.GerName,
                            FraName = CurrentTbl06Phylum.FraName,
                            PorName = CurrentTbl06Phylum.PorName,
                            Writer = Environment.UserName,
                            WriterDate = DateTime.Now,
                            Updater = Environment.UserName,
                            UpdaterDate = DateTime.Now,
                            Memo = CurrentTbl06Phylum.Memo
                        });
                    }
                    {
                        Tbl06PhylumsRepository.Save();
                        MessageBox.Show(CurrentTbl06Phylum.PhylumName+  " was successfully saved ");
                        if (SearchPhylumName == null)
                        {
                            GetConnectedTablesById(); //refresh doubleClick                    
                        }
                        else
                        {
                            GetPhylumByName(); //search
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
    
  
        
        #region "Public Commands Connect ==> Tbl09Division"                 

        private RelayCommand _getDivisionByNameCommand;
        public ICommand GetDivisionByNameCommand
        {
            get { return _getDivisionByNameCommand ?? (_getDivisionByNameCommand = new RelayCommand(GetDivisionByName)); }
        }

        private void GetDivisionByName()
        {
            Tbl09DivisionsList =
                new ObservableCollection<Tbl09Division>((from division in Tbl09DivisionsRepository.Tbl09Divisions
                                                       where division.DivisionName.StartsWith(SearchDivisionName)
                                                       orderby division.DivisionName
                                                       select division));

            DivisionsView = CollectionViewSource.GetDefaultView(Tbl09DivisionsList);
            if (DivisionsView != null)
                DivisionsView.CurrentChanged += tbl09DivisionView_CurrentChanged;                   
            RaisePropertyChanged("CurrentTbl09Division");
        }
        //------------------------------------------------------------------------------
        private RelayCommand _addDivisionCommand;
        public ICommand AddDivisionCommand
        {
            get { return _addDivisionCommand ?? (_addDivisionCommand = new RelayCommand(AddDivision)); }
        }

        private void AddDivision()
        {
            if (Tbl09DivisionsList == null)
                Tbl09DivisionsList = new ObservableCollection<Tbl09Division>();
            Tbl09DivisionsList.Add(new Tbl09Division{ DivisionName= "New " });                   
            DivisionsView = CollectionViewSource.GetDefaultView(Tbl09DivisionsList);
            if (DivisionsView != null)
                DivisionsView.CurrentChanged += tbl09DivisionView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl09Division");
        }
        //---------------------------------------------------------------------------------------
        private RelayCommand _deleteDivisionCommand;
        public ICommand DeleteDivisionCommand
        {
            get { return _deleteDivisionCommand ?? (_deleteDivisionCommand = new RelayCommand(DeleteDivision)); }
        }

        private void DeleteDivision()
        {
            try
            {
                var division= Tbl09DivisionsRepository.Tbl09Divisions.FirstOrDefault(x => x.DivisionID== CurrentTbl09Division.DivisionID);
                if (division!= null)
                {
                    if (MessageBox.Show("Are you really shure to delete this "
                                        + CurrentTbl09Division.DivisionName, "Question", MessageBoxButton.YesNo) !=
                        MessageBoxResult.Yes) return;
                    Tbl09DivisionsRepository.Delete(division);
                    Tbl09DivisionsRepository.Save();
                    MessageBox.Show(CurrentTbl09Division.DivisionName+ " was deleted successfully");
                    if (SearchDivisionName == null)
                    {
                        GetConnectedTablesById(); //refresh doubleClick                    
                    }
                    else
                    {
                        GetDivisionByName(); //search
                    }
                }
                else
                {
                    MessageBox.Show("Only " + CurrentTbl09Division.DivisionName+ " can be deleted");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //-------------------------------------------------------------------------------------------------
        private RelayCommand _saveDivisionCommand;   
        public ICommand SaveDivisionCommand
        {
            get { return _saveDivisionCommand ?? (_saveDivisionCommand = new RelayCommand(SaveDivision)); }
        }

        private void SaveDivision()
        {
            try
            {
                var division= Tbl09DivisionsRepository.Tbl09Divisions.FirstOrDefault(x => x.DivisionID== CurrentTbl09Division.DivisionID);
                if (CurrentTbl09Division == null)
                {
                    MessageBox.Show("division was not found");
                }
                else
                {
                    if (CurrentTbl09Division.DivisionID!= 0)
                    {
                        if (division!= null) //update
                        {
                            division.Updater = Environment.UserName;
                            division.UpdaterDate = DateTime.Now;
                        }
                    }
                    else
                    {
                        Tbl09DivisionsRepository.Add(new Tbl09Division
                        {
                            RegnumID= CurrentTbl09Division.RegnumID,              
                            DivisionName= CurrentTbl09Division.DivisionName,              
                            CountID = TblCountersRepository.Counter(),
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
                            Memo = CurrentTbl09Division.Memo
                        });
                    }
                    {
                        Tbl09DivisionsRepository.Save();
                        MessageBox.Show(CurrentTbl09Division.DivisionName+  " was successfully saved ");              
                        if (SearchDivisionName == null)
                        {
                            GetConnectedTablesById(); //refresh doubleClick                    
                        }
                        else
                        {
                            GetDivisionByName(); //search
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
        public ICommand GetRefAuthorByNameCommand
        {
            get { return _getRefAuthorByNameCommand ?? (_getRefAuthorByNameCommand = new RelayCommand(GetRefAuthorByName)); }
        }

        public void GetRefAuthorByName()
        {
            Tbl90RefAuthorsList =
                new ObservableCollection<Tbl90Reference>((from auth in Tbl90ReferencesRepository.Tbl90References
                                                          where auth.Tbl90RefAuthors.RefAuthorName.StartsWith(SearchRefAuthorName)
                                                            && auth.Tbl90RefExperts == null
                                                            && auth.Tbl90RefSources == null
                                                          orderby auth.Tbl90RefAuthors.RefAuthorName, auth.Tbl90RefAuthors.BookName, auth.Tbl90RefAuthors.Page1
                                                          select auth));

            RefAuthorsView = CollectionViewSource.GetDefaultView(Tbl90RefAuthorsList);
            if (RefAuthorsView != null)
                RefAuthorsView.CurrentChanged += tbl90RefAuthorView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl90RefAuthor");
        }

        //----------------------------------------------------
        private RelayCommand _addRefAuthorCommand;
        public ICommand AddRefAuthorCommand
        {
            get { return _addRefAuthorCommand ?? (_addRefAuthorCommand = new RelayCommand(AddRefAuthor)); }
        }

        public void AddRefAuthor()
        {
            if (Tbl90RefAuthorsList == null)
                Tbl90RefAuthorsList = new ObservableCollection<Tbl90Reference>();
            Tbl90RefAuthorsList.Add(new Tbl90Reference { Info = "New " });
            RefAuthorsView = CollectionViewSource.GetDefaultView(Tbl90RefAuthorsList);
            if (RefAuthorsView != null)
                RefAuthorsView.CurrentChanged += tbl90RefAuthorView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl90RefAuthor");
        }

        //---------------------------------------------------------------------------------------

        private RelayCommand _deleteRefAuthorCommand;
        public ICommand DeleteRefAuthorCommand
        {
            get { return _deleteRefAuthorCommand ?? (_deleteRefAuthorCommand = new RelayCommand(DeleteRefAuthor)); }
        }

        public void DeleteRefAuthor()
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
        public ICommand SaveRefAuthorCommand
        {
            get { return _saveRefAuthorCommand ?? (_saveRefAuthorCommand = new RelayCommand(SaveRefAuthor)); }
        }

        public void SaveRefAuthor()
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
                            RegnumID= CurrentTbl90RefAuthor.RegnumID,
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
        public ICommand GetRefSourceByNameCommand
        {
            get { return _getRefSourceByNameCommand ?? (_getRefSourceByNameCommand = new RelayCommand(GetRefSourceByName)); }
        }

        public void GetRefSourceByName()
        {
            Tbl90RefSourcesList =
                new ObservableCollection<Tbl90Reference>((from refe in Tbl90ReferencesRepository.Tbl90References
                                                          where refe.Tbl90RefSources.RefSourceName.StartsWith(SearchRefSourceName)
                                                          && refe.Tbl90RefExperts == null
                                                          && refe.Tbl90RefAuthors == null
                                                          orderby refe.Tbl90RefSources.RefSourceName, refe.Tbl90RefSources.Author, refe.Tbl90RefSources.SourceYear
                                                          select refe));

            RefSourcesView = CollectionViewSource.GetDefaultView(Tbl90RefSourcesList);
            if (RefSourcesView != null)
                RefSourcesView.CurrentChanged += tbl90RefSourceView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl90RefSource");
        }

        //----------------------------------------------------
        private RelayCommand _addRefSourceCommand;
        public ICommand AddRefSourceCommand
        {
            get { return _addRefSourceCommand ?? (_addRefSourceCommand = new RelayCommand(AddRefSource)); }
        }

        public void AddRefSource()
        {
            if (Tbl90RefSourcesList == null)
                Tbl90RefSourcesList = new ObservableCollection<Tbl90Reference>();
            Tbl90RefSourcesList.Add(new Tbl90Reference { Info = "New " });
            RefSourcesView = CollectionViewSource.GetDefaultView(Tbl90RefSourcesList);
            if (RefSourcesView != null)
                RefSourcesView.CurrentChanged += tbl90RefSourceView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl90RefSource");
        }

        //---------------------------------------------------------------------------------------

        private RelayCommand _deleteRefSourceCommand;
        public ICommand DeleteRefSourceCommand
        {
            get { return _deleteRefSourceCommand ?? (_deleteRefSourceCommand = new RelayCommand(DeleteRefSource)); }
        }

        public void DeleteRefSource()
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
        public ICommand SaveRefSourceCommand
        {
            get { return _saveRefSourceCommand ?? (_saveRefSourceCommand = new RelayCommand(SaveRefSource)); }
        }

        private void SaveRefSource()
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
                            RegnumID= CurrentTbl90RefSource.RegnumID,
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
        public ICommand GetRefExpertByNameCommand
        {
            get { return _getRefExpertByNameCommand ?? (_getRefExpertByNameCommand = new RelayCommand(GetRefExpertByName)); }
        }

        public void GetRefExpertByName()
        {
            Tbl90RefExpertsList =
                new ObservableCollection<Tbl90Reference>((from refe in Tbl90ReferencesRepository.Tbl90References
                                                          where refe.Tbl90RefExperts.RefExpertName.StartsWith(SearchRefExpertName)
                                                          && refe.Tbl90RefSources == null
                                                          && refe.Tbl90RefAuthors == null
                                                          orderby refe.Tbl90RefExperts.RefExpertName, refe.Tbl90RefExperts.Info
                                                          select refe));

            RefExpertsView = CollectionViewSource.GetDefaultView(Tbl90RefExpertsList);
            if (RefExpertsView != null)
                RefExpertsView.CurrentChanged += tbl90RefExpertView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl90RefExpert");
        }

        //----------------------------------------------------
        private RelayCommand _addRefExpertCommand;
        public ICommand AddRefExpertCommand
        {
            get { return _addRefExpertCommand ?? (_addRefExpertCommand = new RelayCommand(AddRefExpert)); }
        }

        public void AddRefExpert()
        {
            if (Tbl90RefExpertsList == null)
                Tbl90RefExpertsList = new ObservableCollection<Tbl90Reference>();
            Tbl90RefExpertsList.Add(new Tbl90Reference { Info = "New " });
            RefExpertsView = CollectionViewSource.GetDefaultView(Tbl90RefExpertsList);
            if (RefExpertsView != null)
                RefExpertsView.CurrentChanged += tbl90RefExpertView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl90RefExpert");
        }

        //---------------------------------------------------------------------------------------
        private RelayCommand _deleteRefExpertCommand;
        public ICommand DeleteRefExpertCommand
        {
            get { return _deleteRefExpertCommand ?? (_deleteRefExpertCommand = new RelayCommand(DeleteRefExpert)); }
        }

        public void DeleteRefExpert()
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
        public ICommand SaveRefExpertCommand
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
                            RegnumID= CurrentTbl90RefExpert.RegnumID,
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
        public ICommand GetCommentByNameCommand
        {
            get { return _getCommentByNameCommand ?? (_getCommentByNameCommand = new RelayCommand(GetCommentByName)); }
        }

        public void GetCommentByName()
        {
            Tbl93CommentsList =
                new ObservableCollection<Tbl93Comment>((from comment in Tbl93CommentsRepository.Tbl93Comments
                                                        where comment.Info.StartsWith(SearchCommentInfo)
                                                        select comment));

            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            if (CommentsView != null)
                CommentsView.CurrentChanged += tbl93CommentView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl93Comment");
        }
        //------------------------------------------------------------------------------

        private RelayCommand _addCommentCommand;
        public ICommand AddCommentCommand
        {
            get { return _addCommentCommand ?? (_addCommentCommand = new RelayCommand(AddComment)); }
        }

        public void AddComment()
        {
            if (Tbl93CommentsList == null)
                Tbl93CommentsList = new ObservableCollection<Tbl93Comment>();
            Tbl93CommentsList.Add(new Tbl93Comment { Info = "New " });

            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            if (CommentsView != null)
                CommentsView.CurrentChanged += tbl93CommentView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl93Comment");
        }

        //---------------------------------------------------------------------------------------

        private RelayCommand _deleteCommentCommand;
        public ICommand DeleteCommentCommand
        {
            get { return _deleteCommentCommand ?? (_deleteCommentCommand = new RelayCommand(DeleteComment)); }
        }

        private void DeleteComment()
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
        public ICommand SaveCommentCommand
        {
            get { return _saveCommentCommand ?? (_saveCommentCommand = new RelayCommand(SaveComment)); }
        }

        private void SaveComment()
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
                            RegnumID= CurrentTbl93Comment.RegnumID,                
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
        public ICommand GetConnectedTablesCommand
        {
            get { return _getConnectedTablesCommand ?? (_getConnectedTablesCommand = new RelayCommand(GetConnectedTablesById)); }
        }

        private void GetConnectedTablesById()
        {
            //Clear Search-TextBox
            SearchPhylumName = null;
            SearchDivisionName = null;
            SearchCommentInfo = null;
            SearchRefExpertName = null;
            SearchRefSourceName = null;
            SearchRefAuthorName = null;


            Tbl06PhylumsList =
                new ObservableCollection<Tbl06Phylum>((from phy in Tbl06PhylumsRepository.Tbl06Phylums
                                                       where phy.RegnumID == CurrentTbl03Regnum.RegnumID
                                                       orderby phy.Tbl03Regnums.RegnumName, phy.Tbl03Regnums.Subregnum
                                                       select phy));

            PhylumsView = CollectionViewSource.GetDefaultView(Tbl06PhylumsList);
            if (PhylumsView != null)
                PhylumsView.CurrentChanged += tbl06PhylumView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl06Phylums");
            //-----------------------------------------------------------------------------------
            Tbl09DivisionsList =
                new ObservableCollection<Tbl09Division>((from div in Tbl09DivisionsRepository.Tbl09Divisions
                                                         where div.RegnumID == CurrentTbl03Regnum.RegnumID
                                                         orderby div.Tbl03Regnums.RegnumName, div.Tbl03Regnums.Subregnum
                                                         select div));

            DivisionsView = CollectionViewSource.GetDefaultView(Tbl09DivisionsList);
            if (DivisionsView != null)
                DivisionsView.CurrentChanged += tbl09DivisionView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl09Division");
            //-----------------------------------------------------------------------------------
            Tbl90RefAuthorsList =
                new ObservableCollection<Tbl90Reference>((from refAu in Tbl90ReferencesRepository.Tbl90References
                                                          where refAu.RegnumID == CurrentTbl03Regnum.RegnumID
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
                                                          where refSo.RegnumID == CurrentTbl03Regnum.RegnumID
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
                                                          where refEx.RegnumID == CurrentTbl03Regnum.RegnumID
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
                                                        where comm.RegnumID == CurrentTbl03Regnum.RegnumID
                                                        orderby comm.Info
                                                        select comm));

            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            if (CommentsView != null)
                CommentsView.CurrentChanged += tbl93CommentView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl93Comment");
            //--------------------------------------------------------------


        }

        #endregion "Public Commands Connected Tables by DoubleClick"
   
     
        #region "Public Commands to open Detail TabItems"

        private int _selectedMainTabIndex;
        public int SelectedMainTabIndex
        {
            get { return _selectedMainTabIndex; }
            set
            {
                if (value == _selectedMainTabIndex) return;
                _selectedMainTabIndex = value;
                RaisePropertyChanged("SelectedMainTabIndex");
                if (_selectedMainTabIndex == 0)
                    SelectedDetailTabIndex = 0;
                if (_selectedMainTabIndex == 1)
                    SelectedDetailTabIndex = 1;
                if (_selectedMainTabIndex == 2)
                    SelectedDetailTabIndex = 2;
                if (_selectedMainTabIndex == 3)
                    SelectedDetailTabIndex = 3;
            }
        }

        private int _selectedMainSubTabIndex;
        public int SelectedMainSubTabIndex
        {
            get { return _selectedMainSubTabIndex; }
            set
            {
                if (value == _selectedMainSubTabIndex) return;
                _selectedMainSubTabIndex = value;
                RaisePropertyChanged("SelectedMainSubTabIndex");
                if (_selectedMainSubTabIndex == 0)
                    SelectedDetailSubTabIndex = 0;
                if (_selectedMainSubTabIndex == 1)
                    SelectedDetailSubTabIndex = 1;
                if (_selectedMainSubTabIndex == 2)
                    SelectedDetailSubTabIndex = 2;
            }
        }

        private int _selectedDetailTabIndex;
        public int SelectedDetailTabIndex
        {
            get { return _selectedDetailTabIndex; }
            set
            {
                if (value == _selectedDetailTabIndex) return;
                _selectedDetailTabIndex = value;
                RaisePropertyChanged("SelectedDetailTabIndex");
                if (_selectedDetailTabIndex == 0)
                    SelectedMainTabIndex = 0;
                if (_selectedDetailTabIndex == 1)
                    SelectedMainTabIndex = 1;
                if (_selectedDetailTabIndex == 2)
                    SelectedMainTabIndex = 2;
                if (_selectedDetailTabIndex == 3)
                    SelectedMainTabIndex = 3;
            }
        }

        private int _selectedDetailSubTabIndex;
        public int SelectedDetailSubTabIndex
        {
            get { return _selectedDetailSubTabIndex; }
            set
            {
                if (value == _selectedDetailSubTabIndex) return;
                _selectedDetailSubTabIndex = value;
                RaisePropertyChanged("SelectedDetailSubTabIndex");
                if (_selectedDetailSubTabIndex == 0)
                    SelectedMainSubTabIndex = 0;
                if (_selectedDetailSubTabIndex == 1)
                    SelectedMainSubTabIndex = 1;
                if (_selectedDetailSubTabIndex == 2)
                    SelectedMainSubTabIndex = 2;
            }
        }
        #endregion "Public Commands to open Detail TabItems"


     
        #region "Public Properties Tbl03Regnum"

        public ICollectionView RegnumsView;
        public Tbl03Regnum CurrentTbl03Regnum
        {
            get
            {
                if (RegnumsView != null)
                    return RegnumsView.CurrentItem as Tbl03Regnum;
                return null;
            }
        }
        //--------------------------------------------
        private string _searchRegnumName;
        public string SearchRegnumName
        {
            get { return _searchRegnumName; }
            set
            {
                if (value == _searchRegnumName) return;
                _searchRegnumName = value;
                RaisePropertyChanged("SearchRegnumName");
            }
        }

        private ObservableCollection<Tbl03Regnum> _tbl03RegnumsList;
        public ObservableCollection<Tbl03Regnum> Tbl03RegnumsList
        {
            get { return _tbl03RegnumsList; }
            set
            {
                if (_tbl03RegnumsList == value) return;
                _tbl03RegnumsList = value;
                RaisePropertyChanged("Tbl03RegnumsList");
            }
        }

        private ObservableCollection<Tbl03Regnum> _tbl03RegnumsAllList;
        public ObservableCollection<Tbl03Regnum> Tbl03RegnumsAllList
        {
            get { return _tbl03RegnumsAllList; }
            set
            {
                if (_tbl03RegnumsAllList == value) return;
                _tbl03RegnumsAllList = value;
                RaisePropertyChanged("Tbl03RegnumsAllList");
            }
        }

        #endregion "Public Properties"
   

       
        #region "Public Properties Tbl06Phylum"

        public ICollectionView PhylumsView;
        public Tbl06Phylum CurrentTbl06Phylum
        {
            get
            {
                if (PhylumsView != null)
                    return PhylumsView.CurrentItem as Tbl06Phylum;
                return null;
            }
        }
        //--------------------------------------------                                               

        private string _searchPhylumName;
        public string SearchPhylumName
        {
            get { return _searchPhylumName; }
            set
            {
                if (value == _searchPhylumName) return;
                _searchPhylumName = value;
                RaisePropertyChanged("SearchPhylumName");
            }
        }

        private ObservableCollection<Tbl06Phylum> _tbl06PhylumsList;
        public ObservableCollection<Tbl06Phylum> Tbl06PhylumsList
        {
            get { return _tbl06PhylumsList; }
            set
            {
                if (_tbl06PhylumsList == value) return;
                _tbl06PhylumsList = value;
                RaisePropertyChanged("Tbl06PhylumsList");
            }
        }

        #endregion "Public Properties"
   
  
       
        #region "Public Properties Tbl09Division"

        public ICollectionView DivisionsView;
        public Tbl09Division CurrentTbl09Division
        {
            get
            {
                if (DivisionsView != null)
                    return DivisionsView.CurrentItem as Tbl09Division;
                return null;
            }
        }
        //--------------------------------------------                                               

        private string _searchDivisionName;
        public string SearchDivisionName
        {
            get { return _searchDivisionName; }
            set
            {
                if (value == _searchDivisionName) return;
                _searchDivisionName = value;
                RaisePropertyChanged("SearchDivisionName");
            }
        }

        private ObservableCollection<Tbl09Division> _tbl09DivisionsList;
        public ObservableCollection<Tbl09Division> Tbl09DivisionsList
        {
            get { return _tbl09DivisionsList; }
            set
            {
                if (_tbl09DivisionsList == value) return;
                _tbl09DivisionsList = value;
                RaisePropertyChanged("Tbl09DivisionsList");
            }
        }

        #endregion "Public Properties"
   
  
     
        #region "Public Properties Tbl90Author"

        private ObservableCollection<Tbl90RefAuthor> _tbl90AuthorsAllList;
        public ObservableCollection<Tbl90RefAuthor> Tbl90AuthorsAllList
        {
            get { return _tbl90AuthorsAllList; }
            set
            {
                if (_tbl90AuthorsAllList == value) return;
                _tbl90AuthorsAllList = value;
                RaisePropertyChanged("Tbl90AuthorsAllList");
            }
        }

        #endregion "Public Properties Tbl90Author"

        #region "Public Properties Tbl90Source"

        private ObservableCollection<Tbl90RefSource> _tbl90SourcesAllList;
        public ObservableCollection<Tbl90RefSource> Tbl90SourcesAllList
        {

            get { return _tbl90SourcesAllList; }
            set
            {
                if (_tbl90SourcesAllList == value) return;
                _tbl90SourcesAllList = value;
                RaisePropertyChanged("Tbl90SourcesAllList");
            }
        }

        #endregion "Public Properties Tbl90Sourcer"

        #region "Public Properties Tbl90Expert"

        private ObservableCollection<Tbl90RefExpert> _tbl90ExpertsAllList;
        public ObservableCollection<Tbl90RefExpert> Tbl90ExpertsAllList
        {
            get { return _tbl90ExpertsAllList; }
            set
            {
                if (_tbl90ExpertsAllList == value) return;
                _tbl90ExpertsAllList = value;
                RaisePropertyChanged("Tbl90ExpertsAllList");
            }
        }

        #endregion "Public Properties Tbl90Sourcer"

        #region "Public Properties Tbl90RefAuthor"

        public ICollectionView RefAuthorsView;
        public Tbl90Reference CurrentTbl90RefAuthor
        {
            get
            {
                if (RefAuthorsView == null) return null;
                return RefAuthorsView.CurrentItem as Tbl90Reference;
            }
        }

        //--------------------------------------------

        private string _searchRefAuthorName;
        public string SearchRefAuthorName
        {
            get { return _searchRefAuthorName; }
            set
            {
                if (value == _searchRefAuthorName) return;
                _searchRefAuthorName = value;
                RaisePropertyChanged("SearchRefAuthorName");
            }
        }

        private ObservableCollection<Tbl90Reference> _tbl90RefAuthorsList;
        public ObservableCollection<Tbl90Reference> Tbl90RefAuthorsList
        {
            get { return _tbl90RefAuthorsList; }
            set
            {
                if (_tbl90RefAuthorsList == value) return;
                _tbl90RefAuthorsList = value;
                RaisePropertyChanged("Tbl90RefAuthorsList");
            }
        }

        #endregion "Public Properties"

        #region "Public Properties Tbl90RefSource"

        public ICollectionView RefSourcesView;
        public Tbl90Reference CurrentTbl90RefSource
        {
            get
            {
                if (RefSourcesView != null)
                    return RefSourcesView.CurrentItem as Tbl90Reference;
                return null;
            }
        }

        //--------------------------------------------

        private string _searchRefSourceName;
        public string SearchRefSourceName
        {
            get { return _searchRefSourceName; }
            set
            {
                if (value == _searchRefSourceName) return;
                _searchRefSourceName = value;
                RaisePropertyChanged("SearchRefSourceName");
            }
        }

        private ObservableCollection<Tbl90Reference> _tbl90RefSourcesList;
        public ObservableCollection<Tbl90Reference> Tbl90RefSourcesList
        {
            get { return _tbl90RefSourcesList; }
            set
            {
                if (_tbl90RefSourcesList == value) return;
                _tbl90RefSourcesList = value;
                RaisePropertyChanged("Tbl90RefSourcesList");
            }
        }

        #endregion "Public Properties"

        #region "Public Properties Tbl90RefExpert"

        public ICollectionView RefExpertsView;
        public Tbl90Reference CurrentTbl90RefExpert
        {
            get
            {
                if (RefExpertsView != null)
                    return RefExpertsView.CurrentItem as Tbl90Reference;
                return null;
            }
        }

        //--------------------------------------------

        private string _searchRefExpertName;
        public string SearchRefExpertName
        {
            get { return _searchRefExpertName; }
            set
            {
                if (value == _searchRefExpertName) return;
                _searchRefExpertName = value;
                RaisePropertyChanged("SearchRefExpertName");
            }
        }

        private ObservableCollection<Tbl90Reference> _tbl90RefExpertsList;
        public ObservableCollection<Tbl90Reference> Tbl90RefExpertsList
        {
            get { return _tbl90RefExpertsList; }
            set
            {
                if (_tbl90RefExpertsList == value) return;
                _tbl90RefExpertsList = value;
                RaisePropertyChanged("Tbl90RefExpertsList");
            }
        }

        #endregion "Public Properties"
   

     
        #region "Public Properties Tbl93Comment"

        public ICollectionView CommentsView;
        public Tbl93Comment CurrentTbl93Comment
        {
            get
            {
                if (CommentsView != null)
                    return CommentsView.CurrentItem as Tbl93Comment;
                return null;
            }
        }

        //--------------------------------------------

        private string _searchCommentInfo;
        public string SearchCommentInfo
        {
            get { return _searchCommentInfo; }
            set
            {
                if (value == _searchCommentInfo) return;
                _searchCommentInfo = value;
                RaisePropertyChanged("SearchCommentInfo");
            }
        }

        private ObservableCollection<Tbl93Comment> _tbl93CommentsList;
        public ObservableCollection<Tbl93Comment> Tbl93CommentsList
        {
            get { return _tbl93CommentsList; }
            set
            {
                if (_tbl93CommentsList == value) return;
                _tbl93CommentsList = value;
                RaisePropertyChanged("Tbl93CommentsList");
            }
        }

        #endregion "Public Properties"
   

     
        #region "Private Methods"

        public void tbl03RegnumView_CurrentChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged("CurrentTbl03Regnum");
        }

   
        
        public void tbl06PhylumView_CurrentChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged("CurrentTbl06Phylum");
        }
   
          
        public void tbl09DivisionView_CurrentChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged("CurrentTbl09Division");
        }
   
  
     

        public void tbl90RefAuthorView_CurrentChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged("CurrentTbl90RefAuthor");
        }

        public void tbl90RefSourceView_CurrentChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged("CurrentTbl90RefSource");
        }

        public void tbl90RefExpertView_CurrentChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged("CurrentTbl90RefExpert");
        }

        public void tbl90AuthorView_CurrentChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged("CurrentTbl90Author");
        }

        public void tbl90SourceView_CurrentChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged("CurrentTbl90Source");
        }

        private void tbl90ExpertView_CurrentChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged("CurrentTbl90Expert");
        }

        public void tbl93CommentView_CurrentChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged("CurrentTbl93Comment");
        }

        #endregion "Private Methods"
   
 

   
    }
}   
