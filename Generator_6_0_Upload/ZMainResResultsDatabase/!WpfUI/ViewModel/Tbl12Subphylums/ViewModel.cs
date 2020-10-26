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

//    Tbl12SubphylumViewModel Skriptdatum:  17.03.2014  12:32    

namespace Atis.WpfUi.ViewModel
{   


    
    public class Tbl12SubphylumsViewModel : Tbl06PhylumsViewModel                     
    {     
         
      #region "Private Data Members"

        protected readonly Tbl18SuperclassesRepository Tbl18SuperclassesRepository = new Tbl18SuperclassesRepository();   
          
          #endregion "Private Data Members"            
    
        #region "Constructor"

        public Tbl12SubphylumsViewModel()
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
       
        #region "Public Commands Basic Tbl12Subphylum"

        private RelayCommand _getSubphylumByNameCommand;
        public new ICommand GetSubphylumByNameCommand
        {
            get { return _getSubphylumByNameCommand ?? (_getSubphylumByNameCommand = new RelayCommand(GetSubphylumByName)); }
        }

        private void GetSubphylumByName()
        {   
Tbl12SubphylumsList =
                 new ObservableCollection<Tbl12Subphylum>((from x in Tbl12SubphylumsRepository.Tbl12Subphylums
                                                        where x.SubphylumName.StartsWith(SearchSubphylumName)
                                                        orderby x.SubphylumName
                                                        select x));

            Tbl12SubphylumsAllList =
                 new ObservableCollection<Tbl12Subphylum>((from y in Tbl12SubphylumsRepository.Tbl12Subphylums
                                                        orderby y.SubphylumName
                                                        select y));

            Tbl06PhylumsAllList =
                 new ObservableCollection<Tbl06Phylum>((from z in Tbl06PhylumsRepository.Tbl06Phylums
                                                        orderby z.PhylumName
                                                        select z));

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

  
Tbl06PhylumsList = null;                  
  Tbl18SuperclassesList = null;     
             
  SubphylumsView = CollectionViewSource.GetDefaultView(Tbl12SubphylumsList);
            if (SubphylumsView != null)
                SubphylumsView.CurrentChanged += tbl12SubphylumView_CurrentChanged;                   
            RaisePropertyChanged("CurrentTbl12Subphylum");
        }
        //------------------------------------------------------------------------------
        private RelayCommand _addSubphylumCommand;
        public new ICommand AddSubphylumCommand
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
        public new ICommand DeleteSubphylumCommand
        {
            get { return _deleteSubphylumCommand ?? (_deleteSubphylumCommand = new RelayCommand(DeleteSubphylum)); }
        }

        private void DeleteSubphylum()
        {
            try
            {
                var subphylum= Tbl12SubphylumsRepository.Tbl12Subphylums.FirstOrDefault(x => x.SubphylumID== CurrentTbl12Subphylum.SubphylumID);
                if (subphylum!= null)
                {
                    if (MessageBox.Show("Are you really shure to delete this "
                                        + CurrentTbl12Subphylum.SubphylumName, "Question", MessageBoxButton.YesNo) !=
                        MessageBoxResult.Yes) return;
                    Tbl12SubphylumsRepository.Delete(subphylum);
                    Tbl12SubphylumsRepository.Save();
                    MessageBox.Show(CurrentTbl12Subphylum.SubphylumName + " was deleted successfully");
                    GetSubphylumByName(); //Refresh
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
        public new ICommand SaveSubphylumCommand
        {
            get { return _saveSubphylumCommand ?? (_saveSubphylumCommand = new RelayCommand(SaveSubphylum)); }
        }

        private void SaveSubphylum()
        {
            try
            {
                var subphylum= Tbl12SubphylumsRepository.Tbl12Subphylums.FirstOrDefault(x => x.SubphylumID== CurrentTbl12Subphylum.SubphylumID);
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
                        GetSubphylumByName();  //Refresh
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion "Public Commands"
  

        
        #region "Public Commands Connect <== Tbl06Phylum"                 

        private RelayCommand _getPhylumByNameCommand;
        public new ICommand GetPhylumByNameCommand
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
        //----------------------------------------------------------------------------------------------------------
        private RelayCommand _deletePhylumCommand;
        public ICommand PhylumPhylumCommand
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
                        Tbl06PhylumsRepository.Add(new Tbl06Phylum()
                        {
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
    
  
        
        #region "Public Commands Connect ==> Tbl18Superclass"                 

        private RelayCommand _getSuperclassByNameCommand;
        public ICommand GetSuperclassByNameCommand
        {
            get { return _getSuperclassByNameCommand ?? (_getSuperclassByNameCommand = new RelayCommand(GetSuperclassByName)); }
        }

        private void GetSuperclassByName()
        {
            Tbl18SuperclassesList =
                new ObservableCollection<Tbl18Superclass>((from superclass in Tbl18SuperclassesRepository.Tbl18Superclasses
                                                       where superclass.SuperclassName.StartsWith(SearchSuperclassName)
                                                       orderby superclass.SuperclassName
                                                       select superclass));

            SuperclassesView = CollectionViewSource.GetDefaultView(Tbl18SuperclassesList);
            if (SuperclassesView != null)
                SuperclassesView.CurrentChanged += tbl18SuperclassView_CurrentChanged;                   
            RaisePropertyChanged("CurrentTbl18Superclass");
        }
        //------------------------------------------------------------------------------
        private RelayCommand _addSuperclassCommand;
        public ICommand AddSuperclassCommand
        {
            get { return _addSuperclassCommand ?? (_addSuperclassCommand = new RelayCommand(AddSuperclass)); }
        }

        private void AddSuperclass()
        {
            if (Tbl18SuperclassesList == null)
                Tbl18SuperclassesList = new ObservableCollection<Tbl18Superclass>();
            Tbl18SuperclassesList.Add(new Tbl18Superclass{ SuperclassName= "New " });                   
            SuperclassesView = CollectionViewSource.GetDefaultView(Tbl18SuperclassesList);
            if (SuperclassesView != null)
                SuperclassesView.CurrentChanged += tbl18SuperclassView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl18Superclass");
        }
        //---------------------------------------------------------------------------------------
        private RelayCommand _deleteSuperclassCommand;
        public ICommand DeleteSuperclassCommand
        {
            get { return _deleteSuperclassCommand ?? (_deleteSuperclassCommand = new RelayCommand(DeleteSuperclass)); }
        }

        private void DeleteSuperclass()
        {
            try
            {
                var superclass = Tbl18SuperclassesRepository.Tbl18Superclasses.FirstOrDefault(x => x.SuperclassID== CurrentTbl18Superclass.SuperclassID);
                if (superclass != null)
                {
                    if (MessageBox.Show("Are you really shure to delete this "
                                        + CurrentTbl18Superclass.SuperclassName, "Question", MessageBoxButton.YesNo) !=
                        MessageBoxResult.Yes) return;
                    Tbl18SuperclassesRepository.Delete(superclass);
                    Tbl18SuperclassesRepository.Save();
                    MessageBox.Show(CurrentTbl18Superclass.SuperclassName+ " was deleted successfully");
                    if (SearchSuperclassName == null)
                    {
                        GetConnectedTablesById(); //refresh doubleClick                    
                    }
                    else
                    {
                        GetSuperclassByName(); //search
                    }
                }
                else
                {
                    MessageBox.Show("Only " + CurrentTbl18Superclass.SuperclassName+ " can be deleted");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //-------------------------------------------------------------------------------------------------
        private RelayCommand _saveSuperclassCommand;   
        public ICommand SaveSuperclassCommand
        {
            get { return _saveSuperclassCommand ?? (_saveSuperclassCommand = new RelayCommand(SaveSuperclass)); }
        }

        private void SaveSuperclass()
        {
            try
            {
                var superclass = Tbl18SuperclassesRepository.Tbl18Superclasses.FirstOrDefault(x => x.SuperclassID== CurrentTbl18Superclass.SuperclassID);
                if (CurrentTbl18Superclass == null)
                {
                    MessageBox.Show("superclass was not found");
                }
                else
                {
                    if (CurrentTbl18Superclass.SuperclassID!= 0)
                    {
                        if (superclass!= null) //update
                        {
                            superclass.Updater = Environment.UserName;
                            superclass.UpdaterDate = DateTime.Now;
                        }
                    }
                    else
                    {
                        Tbl18SuperclassesRepository.Add(new Tbl18Superclass
                        {
                            SubphylumID= CurrentTbl18Superclass.SubphylumID,              
                            SuperclassName= CurrentTbl18Superclass.SuperclassName,              
                            CountID = TblCountersRepository.Counter(),
                            Valid = CurrentTbl18Superclass.Valid,
                            ValidYear = CurrentTbl18Superclass.ValidYear,
                            Synonym = CurrentTbl18Superclass.Synonym,
                            Author = CurrentTbl18Superclass.Author,
                            AuthorYear = CurrentTbl18Superclass.AuthorYear,
                            Info = CurrentTbl18Superclass.Info,
                            EngName = CurrentTbl18Superclass.EngName,
                            GerName = CurrentTbl18Superclass.GerName,
                            FraName = CurrentTbl18Superclass.FraName,
                            PorName = CurrentTbl18Superclass.PorName,
                            Writer = Environment.UserName,
                            WriterDate = DateTime.Now,
                            Updater = Environment.UserName,
                            UpdaterDate = DateTime.Now,
                            Memo = CurrentTbl18Superclass.Memo
                        });
                    }
                    {
                        Tbl18SuperclassesRepository.Save();
                        MessageBox.Show(CurrentTbl18Superclass.SuperclassName+  " was successfully saved ");
                        if (SearchSuperclassName == null)
                        {
                            GetConnectedTablesById(); //refresh doubleClick                    
                        }
                        else
                        {
                            GetSuperclassByName(); //search
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
                            SubphylumID= CurrentTbl90RefAuthor.SubphylumID,
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
                            SubphylumID= CurrentTbl90RefSource.SubphylumID,
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
                            SubphylumID= CurrentTbl90RefExpert.SubphylumID,
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
                            SubphylumID= CurrentTbl93Comment.SubphylumID,                
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
            SearchPhylumName = null;                       
            SearchSuperclassName = null;
            SearchCommentInfo = null;
            SearchRefExpertName = null;
            SearchRefSourceName = null;
            SearchRefAuthorName = null;

            Tbl06PhylumsList =
                new ObservableCollection<Tbl06Phylum>((from phylum in Tbl06PhylumsRepository.Tbl06Phylums
                                                       where phylum.PhylumID== CurrentTbl12Subphylum.PhylumID
                                                       orderby phylum.PhylumName
                                                       select phylum));

            PhylumsView = CollectionViewSource.GetDefaultView(Tbl06PhylumsList);
            if (PhylumsView != null)
                PhylumsView.CurrentChanged += tbl06PhylumView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl06Phylums");
            //-----------------------------------------------------------------------------------
            Tbl18SuperclassesList =
                new ObservableCollection<Tbl18Superclass>((from superclass in Tbl18SuperclassesRepository.Tbl18Superclasses
                                                       where superclass.SubphylumID== CurrentTbl12Subphylum.SubphylumID
                                                       orderby superclass.Tbl12Subphylums.SubphylumName
                                                       select superclass));


            SuperclassesView = CollectionViewSource.GetDefaultView(Tbl18SuperclassesList);
            if (SuperclassesView != null)
                SuperclassesView.CurrentChanged += tbl18SuperclassView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl18Superclasses");
            //-----------------------------------------------------------------------------------
            Tbl90RefAuthorsList =
                new ObservableCollection<Tbl90Reference>((from refAu in Tbl90ReferencesRepository.Tbl90References
                                                          where refAu.SubphylumID== CurrentTbl12Subphylum.SubphylumID
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
                                                          where refSo.SubphylumID== CurrentTbl12Subphylum.SubphylumID
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
                                                          where refEx.SubphylumID== CurrentTbl12Subphylum.SubphylumID
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
                                                          where comm.SubphylumID== CurrentTbl12Subphylum.SubphylumID
                                                        orderby comm.Info
                                                        select comm));

            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            if (CommentsView != null)
                CommentsView.CurrentChanged += tbl93CommentView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl93Comment");
            //--------------------------------------------------------------

        }

        #endregion "Public Commands Connected Tables by DoubleClick"
   

     
        #region "Public Properties Tbl12Subphylum"

        public new ICollectionView SubphylumsView;
        public new Tbl12Subphylum CurrentTbl12Subphylum
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
        public new string SearchSubphylumName
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
        public new ObservableCollection<Tbl12Subphylum> Tbl12SubphylumsList
        {
            get { return _tbl12SubphylumsList; }
            set
            {
                if (_tbl12SubphylumsList == value) return;
                _tbl12SubphylumsList = value;
                RaisePropertyChanged("Tbl12SubphylumsList");

                //Clear Search-TextBox
                SearchPhylumName = null;                                
                SearchSuperclassName = null;
                SearchCommentInfo = null;
                SearchRefExpertName = null;
                SearchRefSourceName = null;
                SearchRefAuthorName = null;
            }
        }

        private ObservableCollection<Tbl12Subphylum> _tbl12SubphylumsAllList;
        public ObservableCollection<Tbl12Subphylum> Tbl12SubphylumsAllList
        {
            get { return _tbl12SubphylumsAllList; }
            set
            {
                if (_tbl12SubphylumsAllList == value) return;
                _tbl12SubphylumsAllList = value;
                RaisePropertyChanged("Tbl12SubphylumsAllList");
            }
        }

        #endregion "Public Properties"
   

       
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
            }
        }

        #endregion "Public Properties"
   
  
       
        #region "Public Properties Tbl18Superclass"

        public ICollectionView SuperclassesView;
        public Tbl18Superclass CurrentTbl18Superclass
        {
            get
            {
                if (SuperclassesView != null)
                    return SuperclassesView.CurrentItem as Tbl18Superclass;
                return null;
            }
        }
        //--------------------------------------------                                               

        private string _searchSuperclassName;
        public string SearchSuperclassName
        {
            get { return _searchSuperclassName; }
            set
            {
                if (value == _searchSuperclassName) return;
                _searchSuperclassName = value;
                RaisePropertyChanged("SearchSuperclassName");
            }
        }

        private ObservableCollection<Tbl18Superclass> _tbl18SuperclassesList;
        public ObservableCollection<Tbl18Superclass> Tbl18SuperclassesList
        {
            get { return _tbl18SuperclassesList; }
            set
            {
                if (_tbl18SuperclassesList == value) return;
                _tbl18SuperclassesList = value;
                RaisePropertyChanged("Tbl18SuperclassesList");
            }
        }

        #endregion "Public Properties"
   
          
        #region "Private Methods"

        public void tbl18SuperclassView_CurrentChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged("CurrentTbl18Superclass");
        }
        #endregion "Private Methods"
   
   

   
    }
}   
