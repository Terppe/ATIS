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

//    Tbl06PhylumViewModel Skriptdatum:  14.03.2014  12:32    

namespace Atis.WpfUi.ViewModel
{   


    
    public class Tbl06PhylumsViewModel : Tbl03RegnumsViewModel                     
    {     
         
      #region "Private Data Members"

        protected readonly Tbl12SubphylumsRepository Tbl12SubphylumsRepository = new Tbl12SubphylumsRepository();   
          
          #endregion "Private Data Members"            
    
        #region "Constructor"

        public Tbl06PhylumsViewModel()
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
       
        #region "Public Commands Basic Tbl06Phylum"

        private RelayCommand _getPhylumByNameCommand;
        public new ICommand GetPhylumByNameCommand
        {
            get { return _getPhylumByNameCommand ?? (_getPhylumByNameCommand = new RelayCommand(GetPhylumByName)); }
        }

        private void GetPhylumByName()
        {   
Tbl06PhylumsList =
                 new ObservableCollection<Tbl06Phylum>((from x in Tbl06PhylumsRepository.Tbl06Phylums
                                                        where x.PhylumName.StartsWith(SearchPhylumName)
                                                        orderby x.PhylumName
                                                        select x));

            Tbl06PhylumsAllList =
                 new ObservableCollection<Tbl06Phylum>((from y in Tbl06PhylumsRepository.Tbl06Phylums
                                                        orderby y.PhylumName
                                                        select y));

            Tbl03RegnumsAllList =
                 new ObservableCollection<Tbl03Regnum>((from z in Tbl03RegnumsRepository.Tbl03Regnums
                                                        orderby z.RegnumName, z.Subregnum
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

  
Tbl03RegnumsList = null;                  
  Tbl12SubphylumsList = null;     
             
  PhylumsView = CollectionViewSource.GetDefaultView(Tbl06PhylumsList);
            if (PhylumsView != null)
                PhylumsView.CurrentChanged += tbl06PhylumView_CurrentChanged;                   
            RaisePropertyChanged("CurrentTbl06Phylum");
        }
        //------------------------------------------------------------------------------
        private RelayCommand _addPhylumCommand;
        public new ICommand AddPhylumCommand
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
        public new ICommand DeletePhylumCommand
        {
            get { return _deletePhylumCommand ?? (_deletePhylumCommand = new RelayCommand(DeletePhylum)); }
        }

        private void DeletePhylum()
        {
            try
            {
                var phylum= Tbl06PhylumsRepository.Tbl06Phylums.FirstOrDefault(x => x.PhylumID== CurrentTbl06Phylum.PhylumID);
                if (phylum!= null)
                {
                    if (MessageBox.Show("Are you really shure to delete this "
                                        + CurrentTbl06Phylum.PhylumName, "Question", MessageBoxButton.YesNo) !=
                        MessageBoxResult.Yes) return;
                    Tbl06PhylumsRepository.Delete(phylum);
                    Tbl06PhylumsRepository.Save();
                    MessageBox.Show(CurrentTbl06Phylum.PhylumName + " was deleted successfully");
                    GetPhylumByName(); //Refresh
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
        public new ICommand SavePhylumCommand
        {
            get { return _savePhylumCommand ?? (_savePhylumCommand = new RelayCommand(SavePhylum)); }
        }

        private void SavePhylum()
        {
            try
            {
                var phylum= Tbl06PhylumsRepository.Tbl06Phylums.FirstOrDefault(x => x.PhylumID== CurrentTbl06Phylum.PhylumID);
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
                        GetPhylumByName();  //Refresh
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion "Public Commands"
  
    
        
        #region "Public Commands Connect <== Tbl03Regnum"                 

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

            RegnumsView = CollectionViewSource.GetDefaultView(Tbl03RegnumsList);
            if (RegnumsView != null)
                RegnumsView.CurrentChanged += tbl03RegnumView_CurrentChanged;                   
            RaisePropertyChanged("CurrentTbl03Regnum");
        }
        //------------------------------------------------------------------------------
        private RelayCommand _addRegnumCommand;
        public new ICommand AddRegnumCommand
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
        public new ICommand DeleteRegnumCommand
        {
            get { return _deleteRegnumCommand ?? (_deleteRegnumCommand = new RelayCommand(DeleteRegnum)); }
        }

        private void DeleteRegnum()
        {
            try
            {
                var regnum= Tbl03RegnumsRepository.Tbl03Regnums.FirstOrDefault(x => x.RegnumID== CurrentTbl03Regnum.RegnumID);
                if (regnum!= null)
                {
                    if (MessageBox.Show("Are you really shure to delete this "
                                        + CurrentTbl03Regnum.RegnumName + " " + CurrentTbl03Regnum.Subregnum, "Question", MessageBoxButton.YesNo) !=
                        MessageBoxResult.Yes) return;
                    Tbl03RegnumsRepository.Delete(regnum);
                    Tbl03RegnumsRepository.Save();
                    MessageBox.Show(CurrentTbl03Regnum.RegnumName+ " " + CurrentTbl03Regnum.Subregnum + " was deleted successfully");
                    if (SearchRegnumName == null)
                    {
                        GetConnectedTablesById(); //refresh doubleClick                    
                    }
                    else
                    {
                        GetRegnumByName(); //search
                    }
                }
                else
                {
                    MessageBox.Show("Only " + CurrentTbl03Regnum.RegnumName+ " " + CurrentTbl03Regnum.Subregnum + " can be deleted");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //-------------------------------------------------------------------------------------------------
        private RelayCommand _saveRegnumCommand;   
        public new ICommand SaveRegnumCommand
        {
            get { return _saveRegnumCommand ?? (_saveRegnumCommand = new RelayCommand(SaveRegnum)); }
        }

        private void SaveRegnum()
        {
            try
            {
                var regnum= Tbl03RegnumsRepository.Tbl03Regnums.FirstOrDefault(x => x.RegnumID== CurrentTbl03Regnum.RegnumID);
                if (CurrentTbl03Regnum == null)
                {
                    MessageBox.Show("regnum- subregnum was not found");
                }
                else
                {
                    if (CurrentTbl03Regnum.RegnumID!= 0)
                    {
                        if (regnum!= null) //update
                        {
                            regnum.Updater = Environment.UserName;
                            regnum.UpdaterDate = DateTime.Now;
                        }
                    }
                    else
                    {
                        Tbl03RegnumsRepository.Add(new Tbl03Regnum()
                        {
                            RegnumName= CurrentTbl03Regnum.RegnumName,              
                            Subregnum= CurrentTbl03Regnum.Subregnum,              
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
                        MessageBox.Show(CurrentTbl03Regnum.RegnumName+ " " + CurrentTbl03Regnum.Subregnum + " was successfully saved ");
                           if (SearchRegnumName == null)
                         {
                             GetConnectedTablesById(); //refresh doubleClick                    
                         }
                         else
                         {
                             GetRegnumByName(); //search
                         }                  
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion "Public Commands"
                                          

        
        #region "Public Commands Connect ==> Tbl12Subphylum"                 

        private RelayCommand _getSubphylumByNameCommand;
        public ICommand GetSubphylumByNameCommand
        {
            get { return _getSubphylumByNameCommand ?? (_getSubphylumByNameCommand = new RelayCommand(GetSubphylumByName)); }
        }

        private void GetSubphylumByName()
        {
            Tbl12SubphylumsList =
                new ObservableCollection<Tbl12Subphylum>((from subphylum in Tbl12SubphylumsRepository.Tbl12Subphylums
                                                       where subphylum.SubphylumName.StartsWith(SearchSubphylumName)
                                                       orderby subphylum.SubphylumName
                                                       select subphylum));

            SubphylumsView = CollectionViewSource.GetDefaultView(Tbl12SubphylumsList);
            if (SubphylumsView != null)
                SubphylumsView.CurrentChanged += tbl12SubphylumView_CurrentChanged;                   
            RaisePropertyChanged("CurrentTbl12Subphylum");
        }
        //------------------------------------------------------------------------------
        private RelayCommand _addSubphylumCommand;
        public ICommand AddSubphylumCommand
        {
            get { return _addSubphylumCommand ?? (_addSubphylumCommand = new RelayCommand(AddSubphylum)); }
        }

        private void AddSubphylum()
        {
            if (Tbl12SubphylumsList == null)
                Tbl12SubphylumsList = new ObservableCollection<Tbl12Subphylum>();
            Tbl12SubphylumsList.Add(new Tbl12Subphylum{ SubphylumName= "New " });                   
            SubphylumsView = CollectionViewSource.GetDefaultView(Tbl12SubphylumsList);
            if (SubphylumsView != null)
                SubphylumsView.CurrentChanged += tbl12SubphylumView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl12Subphylum");
        }
        //---------------------------------------------------------------------------------------
        private RelayCommand _deleteSubphylumCommand;
        public ICommand DeleteSubphylumCommand
        {
            get { return _deleteSubphylumCommand ?? (_deleteSubphylumCommand = new RelayCommand(DeleteSubphylum)); }
        }

        private void DeleteSubphylum()
        {
            try
            {
                var subphylum = Tbl12SubphylumsRepository.Tbl12Subphylums.FirstOrDefault(x => x.SubphylumID== CurrentTbl12Subphylum.SubphylumID);
                if (subphylum != null)
                {
                    if (MessageBox.Show("Are you really shure to delete this "
                                        + CurrentTbl12Subphylum.SubphylumName, "Question", MessageBoxButton.YesNo) !=
                        MessageBoxResult.Yes) return;
                    Tbl12SubphylumsRepository.Delete(subphylum);
                    Tbl12SubphylumsRepository.Save();
                    MessageBox.Show(CurrentTbl12Subphylum.SubphylumName+ " was deleted successfully");
                    if (SearchSubphylumName == null)
                    {
                        GetConnectedTablesById(); //refresh doubleClick                    
                    }
                    else
                    {
                        GetSubphylumByName(); //search
                    }
                }
                else
                {
                    MessageBox.Show("Only " + CurrentTbl12Subphylum.SubphylumName+ " can be deleted");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //-------------------------------------------------------------------------------------------------
        private RelayCommand _saveSubphylumCommand;   
        public ICommand SaveSubphylumCommand
        {
            get { return _saveSubphylumCommand ?? (_saveSubphylumCommand = new RelayCommand(SaveSubphylum)); }
        }

        private void SaveSubphylum()
        {
            try
            {
                var subphylum = Tbl12SubphylumsRepository.Tbl12Subphylums.FirstOrDefault(x => x.SubphylumID== CurrentTbl12Subphylum.SubphylumID);
                if (CurrentTbl12Subphylum == null)
                {
                    MessageBox.Show("subphylum was not found");
                }
                else
                {
                    if (CurrentTbl12Subphylum.SubphylumID!= 0)
                    {
                        if (subphylum!= null) //update
                        {
                            subphylum.Updater = Environment.UserName;
                            subphylum.UpdaterDate = DateTime.Now;
                        }
                    }
                    else
                    {
                        Tbl12SubphylumsRepository.Add(new Tbl12Subphylum
                        {
                            PhylumID= CurrentTbl12Subphylum.PhylumID,              
                            SubphylumName= CurrentTbl12Subphylum.SubphylumName,              
                            CountID = TblCountersRepository.Counter(),
                            Valid = CurrentTbl12Subphylum.Valid,
                            ValidYear = CurrentTbl12Subphylum.ValidYear,
                            Synonym = CurrentTbl12Subphylum.Synonym,
                            Author = CurrentTbl12Subphylum.Author,
                            AuthorYear = CurrentTbl12Subphylum.AuthorYear,
                            Info = CurrentTbl12Subphylum.Info,
                            EngName = CurrentTbl12Subphylum.EngName,
                            GerName = CurrentTbl12Subphylum.GerName,
                            FraName = CurrentTbl12Subphylum.FraName,
                            PorName = CurrentTbl12Subphylum.PorName,
                            Writer = Environment.UserName,
                            WriterDate = DateTime.Now,
                            Updater = Environment.UserName,
                            UpdaterDate = DateTime.Now,
                            Memo = CurrentTbl12Subphylum.Memo
                        });
                    }
                    {
                        Tbl12SubphylumsRepository.Save();
                        MessageBox.Show(CurrentTbl12Subphylum.SubphylumName+  " was successfully saved ");
                        if (SearchSubphylumName == null)
                        {
                            GetConnectedTablesById(); //refresh doubleClick                    
                        }
                        else
                        {
                            GetSubphylumByName(); //search
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
                            PhylumID= CurrentTbl90RefAuthor.PhylumID,
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
                            PhylumID= CurrentTbl90RefSource.PhylumID,
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
                            PhylumID= CurrentTbl90RefExpert.PhylumID,
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
                            PhylumID= CurrentTbl93Comment.PhylumID,                
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

        public void GetConnectedTablesById()
        {
            //Clear Search-TextBox
            SearchRegnumName = null;
            SearchSubphylumName = null;
            SearchCommentInfo = null;
            SearchRefExpertName = null;
            SearchRefSourceName = null;
            SearchRefAuthorName = null;

            Tbl03RegnumsList =
                new ObservableCollection<Tbl03Regnum>((from reg in Tbl03RegnumsRepository.Tbl03Regnums
                                           where reg.RegnumID == CurrentTbl06Phylum.RegnumID
                                           orderby reg.RegnumName, reg.Subregnum
                                           select reg));

            RegnumsView = CollectionViewSource.GetDefaultView(Tbl03RegnumsList);
            if (RegnumsView != null)
                RegnumsView.CurrentChanged += tbl03RegnumView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl03Regnums");           
            //-----------------------------------------------------------------------------------
             Tbl12SubphylumsList =
                 new ObservableCollection<Tbl12Subphylum>((from subph in Tbl12SubphylumsRepository.Tbl12Subphylums
                                                           where subph.PhylumID == CurrentTbl06Phylum.PhylumID
                                                           orderby subph.Tbl06Phylums.PhylumName
                                                           select subph));


             SubphylumsView = CollectionViewSource.GetDefaultView(Tbl12SubphylumsList);
             if (SubphylumsView != null)
                 SubphylumsView.CurrentChanged += tbl12SubphylumView_CurrentChanged;
             RaisePropertyChanged("CurrentTbl12Subphylum"); 
            //-----------------------------------------------------------------------------------
            Tbl90RefAuthorsList =
                new ObservableCollection<Tbl90Reference>((from refAu in Tbl90ReferencesRepository.Tbl90References
                                                           where refAu.PhylumID == CurrentTbl06Phylum.PhylumID
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
                                                           where refSo.PhylumID == CurrentTbl06Phylum.PhylumID
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
                                                           where refEx.PhylumID == CurrentTbl06Phylum.PhylumID
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
                                                           where comm.PhylumID == CurrentTbl06Phylum.PhylumID
                                                           orderby comm.Info
                                                           select comm));

            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            if (CommentsView != null)
                CommentsView.CurrentChanged += tbl93CommentView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl93Comment");
            //--------------------------------------------------------------


        }

        #endregion "Public Commands Connected Tables by DoubleClick"
   

     
        #region "Public Properties Tbl06Phylum"

        public new ICollectionView PhylumsView;
        public new Tbl06Phylum CurrentTbl06Phylum
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
        public new string SearchPhylumName
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
        public new ObservableCollection<Tbl06Phylum> Tbl06PhylumsList
        {
            get { return _tbl06PhylumsList; }
            set
            {
                if (_tbl06PhylumsList == value) return;
                _tbl06PhylumsList = value;
                RaisePropertyChanged("Tbl06PhylumsList");

                //Clear Search-TextBox
                SearchRegnumName = null;                                
                SearchSubphylumName = null;
                SearchCommentInfo = null;
                SearchRefExpertName = null;
                SearchRefSourceName = null;
                SearchRefAuthorName = null;
            }
        }

        private ObservableCollection<Tbl06Phylum> _tbl06PhylumsAllList;
        public ObservableCollection<Tbl06Phylum> Tbl06PhylumsAllList
        {
            get { return _tbl06PhylumsAllList; }
            set
            {
                if (_tbl06PhylumsAllList == value) return;
                _tbl06PhylumsAllList = value;
                RaisePropertyChanged("Tbl06PhylumsAllList");
            }
        }

        #endregion "Public Properties"
   

       
        #region "Public Properties Tbl03Regnum"

        public new ICollectionView RegnumsView;
        public new Tbl03Regnum CurrentTbl03Regnum
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
        public new string SearchRegnumName
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
        public new ObservableCollection<Tbl03Regnum> Tbl03RegnumsList
        {
            get { return _tbl03RegnumsList; }
            set
            {
                if (_tbl03RegnumsList == value) return;
                _tbl03RegnumsList = value;
                RaisePropertyChanged("Tbl03RegnumsList");
            }
        }

        #endregion "Public Properties"
   
  
       
        #region "Public Properties Tbl12Subphylum"

        public ICollectionView SubphylumsView;
        public Tbl12Subphylum CurrentTbl12Subphylum
        {
            get
            {
                if (SubphylumsView != null)
                    return SubphylumsView.CurrentItem as Tbl12Subphylum;
                return null;
            }
        }
        //--------------------------------------------                                               

        private string _searchSubphylumName;
        public string SearchSubphylumName
        {
            get { return _searchSubphylumName; }
            set
            {
                if (value == _searchSubphylumName) return;
                _searchSubphylumName = value;
                RaisePropertyChanged("SearchSubphylumName");
            }
        }

        private ObservableCollection<Tbl12Subphylum> _tbl12SubphylumsList;
        public ObservableCollection<Tbl12Subphylum> Tbl12SubphylumsList
        {
            get { return _tbl12SubphylumsList; }
            set
            {
                if (_tbl12SubphylumsList == value) return;
                _tbl12SubphylumsList = value;
                RaisePropertyChanged("Tbl12SubphylumsList");
            }
        }

        #endregion "Public Properties"
   
          
        #region "Private Methods"

        public void tbl12SubphylumView_CurrentChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged("CurrentTbl12Subphylum");
        }
        #endregion "Private Methods"
   
   

   
    }
}   
