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

//    Tbl15SubdivisionViewModel Skriptdatum:  17.03.2014  12:32    

namespace Atis.WpfUi.ViewModel
{   


    
    public class Tbl15SubdivisionsViewModel : Tbl09DivisionsViewModel                     
    {     
         
      #region "Private Data Members"

        protected readonly Tbl18SuperclassesRepository Tbl18SuperclassesRepository = new Tbl18SuperclassesRepository();   
          
          #endregion "Private Data Members"            
    
        #region "Constructor"

        public Tbl15SubdivisionsViewModel()
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
       
        #region "Public Commands Basic Tbl15Subdivision"

        private RelayCommand _getSubdivisionByNameCommand;
        public new ICommand GetSubdivisionByNameCommand
        {
            get { return _getSubdivisionByNameCommand ?? (_getSubdivisionByNameCommand = new RelayCommand(GetSubdivisionByName)); }
        }

        private void GetSubdivisionByName()
        {   
Tbl15SubdivisionsList =
                 new ObservableCollection<Tbl15Subdivision>((from x in Tbl15SubdivisionsRepository.Tbl15Subdivisions
                                                        where x.SubdivisionName.StartsWith(SearchSubdivisionName)
                                                        orderby x.SubdivisionName
                                                        select x));

            Tbl15SubdivisionsAllList =
                 new ObservableCollection<Tbl15Subdivision>((from y in Tbl15SubdivisionsRepository.Tbl15Subdivisions
                                                        orderby y.SubdivisionName
                                                        select y));

            Tbl09DivisionsAllList =
                 new ObservableCollection<Tbl09Division>((from z in Tbl09DivisionsRepository.Tbl09Divisions
                                                        orderby z.DivisionName
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

  
Tbl09DivisionsList = null;                  
  Tbl18SuperclassesList = null;     
             
  SubdivisionsView = CollectionViewSource.GetDefaultView(Tbl15SubdivisionsList);
            if (SubdivisionsView != null)
                SubdivisionsView.CurrentChanged += tbl15SubdivisionView_CurrentChanged;                   
            RaisePropertyChanged("CurrentTbl15Subdivision");
        }
        //------------------------------------------------------------------------------
        private RelayCommand _addSubdivisionCommand;
        public new ICommand AddSubdivisionCommand
        {
            get { return _addSubdivisionCommand ?? (_addSubdivisionCommand = new RelayCommand(AddSubdivision)); }
        }

        private void AddSubdivision()
        {
            if (Tbl15SubdivisionsList == null)
                Tbl15SubdivisionsList = new ObservableCollection<Tbl15Subdivision>();
            Tbl15SubdivisionsList.Add(new Tbl15Subdivision{ SubdivisionName= "New " });
            SubdivisionsView = CollectionViewSource.GetDefaultView(Tbl15SubdivisionsList);
            if (SubdivisionsView != null)
                SubdivisionsView.CurrentChanged += tbl15SubdivisionView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl15Subdivision");
        }
        //---------------------------------------------------------------------------------------
  
       
        private RelayCommand _deleteSubdivisionCommand;
        public new ICommand DeleteSubdivisionCommand
        {
            get { return _deleteSubdivisionCommand ?? (_deleteSubdivisionCommand = new RelayCommand(DeleteSubdivision)); }
        }

        private void DeleteSubdivision()
        {
            try
            {
                var subdivision= Tbl15SubdivisionsRepository.Tbl15Subdivisions.FirstOrDefault(x => x.SubdivisionID== CurrentTbl15Subdivision.SubdivisionID);
                if (subdivision!= null)
                {
                    if (MessageBox.Show("Are you really shure to delete this "
                                        + CurrentTbl15Subdivision.SubdivisionName, "Question", MessageBoxButton.YesNo) !=
                        MessageBoxResult.Yes) return;
                    Tbl15SubdivisionsRepository.Delete(subdivision);
                    Tbl15SubdivisionsRepository.Save();
                    MessageBox.Show(CurrentTbl15Subdivision.SubdivisionName + " was deleted successfully");
                    GetSubdivisionByName(); //Refresh
                }
                else
                {
                    MessageBox.Show("Only " + CurrentTbl15Subdivision.SubdivisionName+ " can be deleted");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //-------------------------------------------------------------------------------------------------
        private RelayCommand _saveSubdivisionCommand;
        public new ICommand SaveSubdivisionCommand
        {
            get { return _saveSubdivisionCommand ?? (_saveSubdivisionCommand = new RelayCommand(SaveSubdivision)); }
        }

        private void SaveSubdivision()
        {
            try
            {
                var subdivision= Tbl15SubdivisionsRepository.Tbl15Subdivisions.FirstOrDefault(x => x.SubdivisionID== CurrentTbl15Subdivision.SubdivisionID);
                if (CurrentTbl15Subdivision == null)
                {
                    MessageBox.Show("subdivision was not found");
                }
                else
                {
                    if (CurrentTbl15Subdivision.SubdivisionID!= 0)
                    {
                        if (subdivision!= null) //update
                        {
                            subdivision.Updater = Environment.UserName;
                            subdivision.UpdaterDate = DateTime.Now;
                        }
                    }
                    else
                    {
                        Tbl15SubdivisionsRepository.Add(new Tbl15Subdivision
                        {
                            DivisionID= CurrentTbl15Subdivision.DivisionID,              
                            SubdivisionName= CurrentTbl15Subdivision.SubdivisionName,              
                            CountID = TblCountersRepository.Counter(),
                            Valid = CurrentTbl15Subdivision.Valid,
                            ValidYear = CurrentTbl15Subdivision.ValidYear,
                            Synonym = CurrentTbl15Subdivision.Synonym,
                            Author = CurrentTbl15Subdivision.Author,
                            AuthorYear = CurrentTbl15Subdivision.AuthorYear,
                            Info = CurrentTbl15Subdivision.Info,
                            EngName = CurrentTbl15Subdivision.EngName,
                            GerName = CurrentTbl15Subdivision.GerName,
                            FraName = CurrentTbl15Subdivision.FraName,
                            PorName = CurrentTbl15Subdivision.PorName,
                            Writer = Environment.UserName,
                            WriterDate = DateTime.Now,
                            Updater = Environment.UserName,
                            UpdaterDate = DateTime.Now,
                            Memo = CurrentTbl15Subdivision.Memo
                        });
                    }
                    {
                        Tbl15SubdivisionsRepository.Save();
                        MessageBox.Show(CurrentTbl15Subdivision.SubdivisionName+  " was successfully saved ");
                        GetSubdivisionByName();  //Refresh
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion "Public Commands"
  

        
        #region "Public Commands Connect <== Tbl09Division"                 

        private RelayCommand _getDivisionByNameCommand;
        public new ICommand GetDivisionByNameCommand
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
        public new ICommand AddDivisionCommand
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
        //----------------------------------------------------------------------------------------------------------
        private RelayCommand _deleteDivisionCommand;
        public ICommand DivisionPhylumCommand
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
        public new ICommand SaveDivisionCommand
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
                        Tbl09DivisionsRepository.Add(new Tbl09Division()
                        {
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
                        GetDivisionByName();  //Refresh
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
                            SubdivisionID= CurrentTbl18Superclass.SubdivisionID,              
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
                            SubdivisionID= CurrentTbl90RefAuthor.SubdivisionID,
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
                            SubdivisionID= CurrentTbl90RefSource.SubdivisionID,
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
                            SubdivisionID= CurrentTbl90RefExpert.SubdivisionID,
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
                            SubdivisionID= CurrentTbl93Comment.SubdivisionID,                
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
            SearchDivisionName = null;                       
            SearchSuperclassName = null;
            SearchCommentInfo = null;
            SearchRefExpertName = null;
            SearchRefSourceName = null;
            SearchRefAuthorName = null;

            Tbl09DivisionsList =
                new ObservableCollection<Tbl09Division>((from division in Tbl09DivisionsRepository.Tbl09Divisions
                                                       where division.DivisionID== CurrentTbl15Subdivision.DivisionID
                                                       orderby division.DivisionName
                                                       select division));

            DivisionsView = CollectionViewSource.GetDefaultView(Tbl09DivisionsList);
            if (DivisionsView != null)
                DivisionsView.CurrentChanged += tbl09DivisionView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl09Divisions");
            //-----------------------------------------------------------------------------------
            Tbl18SuperclassesList =
                new ObservableCollection<Tbl18Superclass>((from superclass in Tbl18SuperclassesRepository.Tbl18Superclasses
                                                       where superclass.SubdivisionID== CurrentTbl15Subdivision.SubdivisionID
                                                       orderby superclass.Tbl15Subdivisions.SubdivisionName
                                                       select superclass));


            SuperclassesView = CollectionViewSource.GetDefaultView(Tbl18SuperclassesList);
            if (SuperclassesView != null)
                SuperclassesView.CurrentChanged += tbl18SuperclassView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl18Superclasses");
            //-----------------------------------------------------------------------------------
            Tbl90RefAuthorsList =
                new ObservableCollection<Tbl90Reference>((from refAu in Tbl90ReferencesRepository.Tbl90References
                                                          where refAu.SubdivisionID== CurrentTbl15Subdivision.SubdivisionID
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
                                                          where refSo.SubdivisionID== CurrentTbl15Subdivision.SubdivisionID
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
                                                          where refEx.SubdivisionID== CurrentTbl15Subdivision.SubdivisionID
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
                                                          where comm.SubdivisionID== CurrentTbl15Subdivision.SubdivisionID
                                                        orderby comm.Info
                                                        select comm));

            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            if (CommentsView != null)
                CommentsView.CurrentChanged += tbl93CommentView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl93Comment");
            //--------------------------------------------------------------

        }

        #endregion "Public Commands Connected Tables by DoubleClick"
   

     
        #region "Public Properties Tbl15Subdivision"

        public new ICollectionView SubdivisionsView;
        public new Tbl15Subdivision CurrentTbl15Subdivision
        {
            get
            {
                if (SubdivisionsView != null)
                    return SubdivisionsView.CurrentItem as Tbl15Subdivision;
                return null;
            }
        }
        //--------------------------------------------
        private string _searchSubdivisionName;
        public new string SearchSubdivisionName
        {
            get { return _searchSubdivisionName; }
            set
            {
                if (value == _searchSubdivisionName) return;
                _searchSubdivisionName = value;
                RaisePropertyChanged("SearchSubdivisionName");
            }
        }

        private ObservableCollection<Tbl15Subdivision> _tbl15SubdivisionsList;
        public new ObservableCollection<Tbl15Subdivision> Tbl15SubdivisionsList
        {
            get { return _tbl15SubdivisionsList; }
            set
            {
                if (_tbl15SubdivisionsList == value) return;
                _tbl15SubdivisionsList = value;
                RaisePropertyChanged("Tbl15SubdivisionsList");

                //Clear Search-TextBox
                SearchDivisionName = null;                                
                SearchSuperclassName = null;
                SearchCommentInfo = null;
                SearchRefExpertName = null;
                SearchRefSourceName = null;
                SearchRefAuthorName = null;
            }
        }

        private ObservableCollection<Tbl15Subdivision> _tbl15SubdivisionsAllList;
        public ObservableCollection<Tbl15Subdivision> Tbl15SubdivisionsAllList
        {
            get { return _tbl15SubdivisionsAllList; }
            set
            {
                if (_tbl15SubdivisionsAllList == value) return;
                _tbl15SubdivisionsAllList = value;
                RaisePropertyChanged("Tbl15SubdivisionsAllList");
            }
        }

        #endregion "Public Properties"
   

       
        #region "Public Properties Tbl09Division"

        public new ICollectionView DivisionsView;
        public new Tbl09Division CurrentTbl09Division
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
        public new string SearchDivisionName
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
        public new ObservableCollection<Tbl09Division> Tbl09DivisionsList
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
