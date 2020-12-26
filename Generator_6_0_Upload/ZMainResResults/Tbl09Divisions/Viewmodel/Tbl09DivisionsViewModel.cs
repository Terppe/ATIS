using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using DAL.Helper;
using DAL.Models;
using DAL.Repositories.Tbl03Regnums;
using DAL.Repositories.Tbl06Phylums;
using DAL.Repositories.Tbl09Divisions;
using DAL.Repositories.Tbl90RefAuthors;
using DAL.Repositories.Tbl90References;
using DAL.Repositories.Tbl90RefExperts;
using DAL.Repositories.Tbl90RefSources;
using DAL.Repositories.Tbl93Comments;
using DAL.Repositories.TblCounters;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

//    Tbl09DivisionsViewModel Skriptdatum:  22.02.2016  12:32    

namespace WPFUI.Views.Database
{   


    
    public class Tbl09DivisionsViewModel : Tbl03RegnumsViewModel                     
    {     
         
      #region "Private Data Members"

        protected readonly Tbl15SubdivisionsRepository Tbl15SubdivisionsRepository = new Tbl15SubdivisionsRepository();   
          
          #endregion "Private Data Members"            
    
        #region "Constructor"

        public Tbl09DivisionsViewModel()
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

       
        #region "Public Commands Basic Tbl09Division"

        private RelayCommand _getDivisionByNameCommand;
        public new ICommand GetDivisionByNameCommand
        {
            get { return _getDivisionByNameCommand ?? (_getDivisionByNameCommand = new RelayCommand(delegate { GetDivisionByNameOrId(null); })); }   
        }

        private void GetDivisionByNameOrId(object o)       
        {   
Tbl09DivisionsList =  new ObservableCollection<Tbl09Division>
                                                       (from x in Tbl09DivisionsRepository.Tbl09Divisions
                                                        where x.DivisionName.StartsWith(SearchDivisionName)
                                                        orderby x.DivisionName
                                                        select x);

            Tbl09DivisionsAllList =  new ObservableCollection<Tbl09Division>
                                                       (from y in Tbl09DivisionsRepository.Tbl09Divisions
                                                        orderby y.DivisionName
                                                        select y);

            Tbl03RegnumsAllList =  new ObservableCollection<Tbl03Regnum>
                                                       (from z in Tbl03RegnumsRepository.Tbl03Regnums
                                                        orderby z.RegnumName, z.Subregnum
                                                        select z);
  
       
            Tbl90AuthorsAllList =  new ObservableCollection<Tbl90RefAuthor>
                                                       (from auth in Tbl90RefAuthorsRepository.Tbl90RefAuthors
                                                        orderby auth.RefAuthorName, auth.BookName, auth.Page1
                                                        select auth);
 
           Tbl90SourcesAllList =  new ObservableCollection<Tbl90RefSource>
                                                       (from sour in Tbl90RefSourcesRepository.Tbl90RefSources
                                                        orderby sour.RefSourceName
                                                        select sour);

            Tbl90ExpertsAllList =  new ObservableCollection<Tbl90RefExpert>
                                                       (from exp in Tbl90RefExpertsRepository.Tbl90RefExperts
                                                        orderby exp.RefExpertName
                                                        select exp);

            //All Lists to null
            Tbl93CommentsList = null;
            Tbl90RefExpertsList = null;
            Tbl90RefAuthorsList = null;
            Tbl90RefSourcesList = null;

  
Tbl03RegnumsList = null;                  
  Tbl15SubdivisionsList = null;               
  DivisionsView = CollectionViewSource.GetDefaultView(Tbl09DivisionsList);
            if (DivisionsView != null)
                DivisionsView.CurrentChanged += tbl09DivisionView_CurrentChanged;                   
            RaisePropertyChanged();
        }
        //------------------------------------------------------------------------------
        private RelayCommand _addDivisionCommand;
        public new ICommand AddDivisionCommand
        {
            get { return _addDivisionCommand ?? (_addDivisionCommand = new RelayCommand(AddDivision)); }
        }

        private void AddDivision(object o)
        {
            if (Tbl09DivisionsList == null)
                Tbl09DivisionsList = new ObservableCollection<Tbl09Division>();
            Tbl09DivisionsList.Add(new Tbl09Division{ DivisionName= CultRes.StringsRes.DatasetNew });
            DivisionsView = CollectionViewSource.GetDefaultView(Tbl09DivisionsList);
            if (DivisionsView != null)
                DivisionsView.CurrentChanged += tbl09DivisionView_CurrentChanged;
            RaisePropertyChanged();
        }
        //---------------------------------------------------------------------------------------
  
       
        private RelayCommand _deleteDivisionCommand;
        public new ICommand DeleteDivisionCommand
        {
            get { return _deleteDivisionCommand ?? (_deleteDivisionCommand = new RelayCommand(delegate { DeleteDivision(null); })); }
        }

        private void DeleteDivision(object o)
        {
            try
            {
                var division= Tbl09DivisionsRepository.Tbl09Divisions.FirstOrDefault(x => x.DivisionID== CurrentTbl09Division.DivisionID);
                if (division!= null)
                {
                    if (MessageBox.Show(CultRes.StringsRes.DeleteQuestion
                                        + " " +  CurrentTbl09Division.DivisionName, CultRes.StringsRes.DeleteQuestion1, MessageBoxButton.YesNo) !=
                        MessageBoxResult.Yes) return;
                    Tbl09DivisionsRepository.Delete(division);
                    Tbl09DivisionsRepository.Save();
                    MessageBox.Show(CurrentTbl09Division.DivisionName + " " + CultRes.StringsRes.DeleteSuccess);
                    GetDivisionByNameOrId(o); //Refresh
                }
                else
                {
                    MessageBox.Show(CultRes.StringsRes.DeleteCan + " " + CurrentTbl09Division.DivisionName+ " " + CultRes.StringsRes.DeleteCan1);
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
            get { return _saveDivisionCommand ?? (_saveDivisionCommand = new RelayCommand(delegate { SaveDivision(null); })); }
        }

        private void SaveDivision(object o)
        {
            try
            {
                var division= Tbl09DivisionsRepository.Tbl09Divisions.FirstOrDefault(x => x.DivisionID== CurrentTbl09Division.DivisionID);
                if (CurrentTbl09Division == null)
                {
                    MessageBox.Show(CultRes.StringsRes.DatasetNotExist);
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
                            CountID = RandomHelper.Randomnumber(),
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
                        MessageBox.Show(CurrentTbl09Division.DivisionName+ " " + CultRes.StringsRes.SaveSuccess);
                        GetDivisionByNameOrId(o);  //Refresh
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion "Public Commands"
  
    

 //    Part 2    

    
        
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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion "Public Commands"                                          
    

 //    Part 3    

    

 //    Part 4    


        
        #region "Public Commands Connect ==> Tbl15Subdivision"                 

        private RelayCommand _getSubdivisionByNameCommand;
        public ICommand GetSubdivisionByNameCommand
        {
            get { return _getSubdivisionByNameCommand ?? (_getSubdivisionByNameCommand = new RelayCommand(delegate { GetSubdivisionByNameOrId(null); })); }
        }

        private void GetSubdivisionByNameOrId(object o)
        {
            int id;
            if (int.TryParse(SearchSubdivisionName, out id))
                Tbl15SubdivisionsList = new ObservableCollection<Tbl15Subdivision> { Tbl15SubdivisionsRepository.Get(id) };
            else
            Tbl15SubdivisionsList =  new ObservableCollection<Tbl15Subdivision>
                                                      (from x in Tbl15SubdivisionsRepository.FindAll()
                                                       where x.SubdivisionName.StartsWith(SearchSubdivisionName)
                                                       orderby x.SubdivisionName
                                                       select x);

            SubdivisionsView = CollectionViewSource.GetDefaultView(Tbl15SubdivisionsList);
            if (SubdivisionsView != null)
                SubdivisionsView.CurrentChanged += tbl15SubdivisionView_CurrentChanged;                   
            RaisePropertyChanged();
        }
        //------------------------------------------------------------------------------
        private RelayCommand _addSubdivisionCommand;
        public ICommand AddSubdivisionCommand
        {
            get { return _addSubdivisionCommand ?? (_addSubdivisionCommand = new RelayCommand(delegate { AddSubdivision(null); })); }
        }

        private void AddSubdivision(object o)
        {
            if (Tbl15SubdivisionsList == null)
                Tbl15SubdivisionsList = new ObservableCollection<Tbl15Subdivision>();
            Tbl15SubdivisionsList.Add(new Tbl15Subdivision{ SubdivisionName= CultRes.StringsRes.DatasetNew });                   
            SubdivisionsView = CollectionViewSource.GetDefaultView(Tbl15SubdivisionsList);
            if (SubdivisionsView != null)
                SubdivisionsView.CurrentChanged += tbl15SubdivisionView_CurrentChanged;
            RaisePropertyChanged();
        }
        //---------------------------------------------------------------------------------------
        private RelayCommand _deleteSubdivisionCommand;
        public ICommand DeleteSubdivisionCommand
        {
            get { return _deleteSubdivisionCommand ?? (_deleteSubdivisionCommand = new RelayCommand(delegate { DeleteSubdivision(null); })); }
        }

        private void DeleteSubdivision(object o)
        {
            try
            {
                var subdivision = Tbl15SubdivisionsRepository.Tbl15Subdivisions.FirstOrDefault(x => x.SubdivisionID== CurrentTbl15Subdivision.SubdivisionID);
                if (subdivision != null)
                {
                    if (MessageBox.Show(CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl15Subdivision.SubdivisionName, CultRes.StringsRes.DeleteQuestion1, MessageBoxButton.YesNo) !=
                        MessageBoxResult.Yes) return;
                    Tbl15SubdivisionsRepository.Delete(subdivision);
                    Tbl15SubdivisionsRepository.Save();
                    MessageBox.Show(CurrentTbl15Subdivision.SubdivisionName + " " + CultRes.StringsRes.DeleteSuccess);
                    GetSubdivisionByNameOrId(o);  //Refresh
                }
                else
                {
                    MessageBox.Show(CultRes.StringsRes.DeleteCan + " " + CurrentTbl15Subdivision.SubdivisionName+ " " + CultRes.StringsRes.DeleteCan1);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //-------------------------------------------------------------------------------------------------
        private RelayCommand _saveSubdivisionCommand;   
        public ICommand SaveSubdivisionCommand
        {
            get { return _saveSubdivisionCommand ?? (_saveSubdivisionCommand = new RelayCommand(delegate { SaveSubdivision(null); })); }
        }

        private void SaveSubdivision(object o)
        {
            try
            {
                var subdivision = Tbl15SubdivisionsRepository.Tbl15Subdivisions.FirstOrDefault(x => x.SubdivisionID== CurrentTbl15Subdivision.SubdivisionID);
                if (CurrentTbl15Subdivision == null)
                {
                    MessageBox.Show(CultRes.StringsRes.DatasetNotExist);
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
                            CountID = RandomHelper.Randomnumber(),
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
                        if (CurrentTbl15Subdivision != null)
                            MessageBox.Show(CurrentTbl15Subdivision.SubdivisionName+ " " + CultRes.StringsRes.SaveSuccess);
                        GetSubdivisionByNameOrId(o);  //Refresh      
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion "Public Commands"
    
      

 //    Part 5    

    

 //    Part 6    

 

 //    Part 7    

 

 //    Part 8    

   
    
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
                            DivisionID= CurrentTbl90RefAuthor.DivisionID,
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
                            DivisionID= CurrentTbl90RefSource.DivisionID,
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

        private new void SaveRefExpert()
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
                            DivisionID= CurrentTbl90RefExpert.DivisionID,
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
                            DivisionID= CurrentTbl93Comment.DivisionID,                
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
  
    

 //    Part 9    



     
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
            SearchSubdivisionName = null;
            SearchCommentInfo = null;
            SearchRefExpertName = null;
            SearchRefSourceName = null;
            SearchRefAuthorName = null;

            Tbl03RegnumsList =
                new ObservableCollection<Tbl03Regnum>((from reg in Tbl03RegnumsRepository.Tbl03Regnums
                                           where reg.RegnumID == CurrentTbl09Division.RegnumID
                                           orderby reg.RegnumName, reg.Subregnum
                                           select reg));

            RegnumsView = CollectionViewSource.GetDefaultView(Tbl03RegnumsList);
            if (RegnumsView != null)
                RegnumsView.CurrentChanged += tbl03RegnumView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl03Regnums");           
            //-----------------------------------------------------------------------------------
             Tbl15SubdivisionsList =
                 new ObservableCollection<Tbl15Subdivision>((from subdiv in Tbl15SubdivisionsRepository.Tbl15Subdivisions
                                                           where subdiv.DivisionID == CurrentTbl09Division.DivisionID
                                                           orderby subdiv.SubdivisionName
                                                           select subdiv));


             SubdivisionsView = CollectionViewSource.GetDefaultView(Tbl15SubdivisionsList);
             if (SubdivisionsView != null)
                 SubdivisionsView.CurrentChanged += tbl15SubdivisionView_CurrentChanged;
             RaisePropertyChanged("CurrentTbl15Subdivision"); 
            //-----------------------------------------------------------------------------------
            Tbl90RefAuthorsList =
                new ObservableCollection<Tbl90Reference>((from refAu in Tbl90ReferencesRepository.Tbl90References
                                                           where refAu.DivisionID == CurrentTbl09Division.DivisionID
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
                                                           where refSo.DivisionID == CurrentTbl09Division.DivisionID
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
                                                           where refEx.DivisionID == CurrentTbl09Division.DivisionID
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
                                                           where comm.DivisionID == CurrentTbl09Division.DivisionID
                                                           orderby comm.Info
                                                           select comm));

            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            if (CommentsView != null)
                CommentsView.CurrentChanged += tbl93CommentView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl93Comment");
            //--------------------------------------------------------------


        }

        #endregion "Public Commands Connected Tables by DoubleClick"
   
 

 //    Part 10    

 

 //    Part 11    



     
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

                //Clear Search-TextBox
                SearchRegnumName = null;                                
                SearchSubdivisionName = null;
                SearchCommentInfo = null;
                SearchRefExpertName = null;
                SearchRefSourceName = null;
                SearchRefAuthorName = null;
            }
        }

        private ObservableCollection<Tbl09Division> _tbl09DivisionsAllList;
        public ObservableCollection<Tbl09Division> Tbl09DivisionsAllList
        {
            get { return _tbl09DivisionsAllList; }
            set
            {
                if (_tbl09DivisionsAllList == value) return;
                _tbl09DivisionsAllList = value;
                RaisePropertyChanged("Tbl09DivisionsAllList");
            }
        }

        #endregion "Public Properties"   

       
        #region "Public Properties Tbl03Regnum"

        public  ICollectionView RegnumsView;
        public  Tbl03Regnum CurrentTbl03Regnum
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
        public  string SearchRegnumName
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
        public  ObservableCollection<Tbl03Regnum> Tbl03RegnumsList
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
   
  
       
        #region "Public Properties Tbl15Subdivision"

        public ICollectionView SubdivisionsView;
        public Tbl15Subdivision CurrentTbl15Subdivision
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
        public string SearchSubdivisionName
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
        public ObservableCollection<Tbl15Subdivision> Tbl15SubdivisionsList
        {
            get { return _tbl15SubdivisionsList; }
            set
            {
                if (_tbl15SubdivisionsList == value) return;
                _tbl15SubdivisionsList = value;
                RaisePropertyChanged("Tbl15SubdivisionsList");
            }
        }

        #endregion "Public Properties"
   
   

 //    Part 11    

      }
}   
