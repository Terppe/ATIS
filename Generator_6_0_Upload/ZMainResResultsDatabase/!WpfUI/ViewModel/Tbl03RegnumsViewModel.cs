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

//    Tbl03RegnumsViewModel Skriptdatum:  11.03.2016  12:32      

namespace WPFUI.Views.Database
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
        private new bool IsInDesignMode { get; set; }

        #endregion "Constructor"           
 

 //    Part 1    

    
        #region "Public Commands Basic Tbl03Regnum"

        private RelayCommand _getRegnumByNameCommand;
        public ICommand GetRegnumByNameCommand
        {
            get { return _getRegnumByNameCommand ?? (_getRegnumByNameCommand = new RelayCommand(delegate { GetRegnumByNameOrId(null); })); }   
         }

        private void GetRegnumByNameOrId(object o)       
       {   
   
            int id;
            if (int.TryParse(SearchRegnumName, out id))
                Tbl03RegnumsList = new ObservableCollection<Tbl03Regnum> { Tbl03RegnumsRepository.Get(id) };
            else
                Tbl03RegnumsList = new ObservableCollection<Tbl03Regnum>
                                (from a in Tbl03RegnumsRepository.FindAll()
                                 where a.RegnumName.StartsWith(SearchRegnumName)
                                 orderby a.RegnumName, a.Subregnum
                                 select a);

            Tbl03RegnumsAllList = new ObservableCollection<Tbl03Regnum>
                                (from b in Tbl03RegnumsRepository.FindAllSort()
                                 select b);

  
       
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

  
  
                Tbl06PhylumsList = null;                 
                Tbl09DivisionsList = null;                       
RegnumsView = CollectionViewSource.GetDefaultView(Tbl03RegnumsList);
            if (RegnumsView != null)
                RegnumsView.CurrentChanged += tbl03RegnumView_CurrentChanged;                   
            RaisePropertyChanged();
        }
        //------------------------------------------------------------------------------
        private RelayCommand _addRegnumCommand;
        public ICommand AddRegnumCommand
        {
            get { return _addRegnumCommand ?? (_addRegnumCommand = new RelayCommand(delegate { AddRegnum(null); })); }
        }

        private void AddRegnum(object o)
        {
            if (Tbl03RegnumsList == null)
                Tbl03RegnumsList = new ObservableCollection<Tbl03Regnum>();
            Tbl03RegnumsList.Add(new Tbl03Regnum{ RegnumName= CultRes.StringsRes.DatasetNew });
            RegnumsView = CollectionViewSource.GetDefaultView(Tbl03RegnumsList);
            if (RegnumsView != null)
                RegnumsView.CurrentChanged += tbl03RegnumView_CurrentChanged;
            RaisePropertyChanged();
        }
        //---------------------------------------------------------------------------------------  
   
        private RelayCommand _deleteRegnumCommand;
        public ICommand DeleteRegnumCommand
        {
            get { return _deleteRegnumCommand ?? (_deleteRegnumCommand = new RelayCommand(delegate { DeleteRegnum(null); })); }
        }

        private void DeleteRegnum(object o)
        {
            try
            {
                var regnum = Tbl03RegnumsRepository.Tbl03Regnums.FirstOrDefault(x => x.RegnumID == CurrentTbl03Regnum.RegnumID);
                if (regnum != null)
                {
                    if (MessageBox.Show(CultRes.StringsRes.DeleteQuestion
                                        + " " + CurrentTbl03Regnum.RegnumName + " " + CurrentTbl03Regnum.Subregnum, CultRes.StringsRes.DeleteQuestion1, MessageBoxButton.YesNo) !=
                        MessageBoxResult.Yes) return;
                    Tbl03RegnumsRepository.Delete(regnum);
                    Tbl03RegnumsRepository.Save();
                    MessageBox.Show(CurrentTbl03Regnum.RegnumName + " " + CurrentTbl03Regnum.Subregnum + " " + CultRes.StringsRes.DeleteSuccess);
                    GetRegnumByNameOrId(o);  //Refresh
                }
                else
                {
                    MessageBox.Show(CultRes.StringsRes.DeleteCan + " " + CurrentTbl03Regnum.RegnumName + " " + CurrentTbl03Regnum.Subregnum + " " + CultRes.StringsRes.DeleteCan1);
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
            get { return _saveRegnumCommand ?? (_saveRegnumCommand = new RelayCommand(delegate { SaveRegnum(null); })); }
       }

        private void SaveRegnum(object o)
        {
            try
            {
                var regnum = Tbl03RegnumsRepository.Tbl03Regnums.FirstOrDefault(x => x.RegnumID == CurrentTbl03Regnum.RegnumID);
                if (CurrentTbl03Regnum == null)
                {
                    MessageBox.Show(CultRes.StringsRes.DatasetNotExist);
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
                            CountID = RandomHelper.Randomnumber(),
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
                        MessageBox.Show(CurrentTbl03Regnum.RegnumName + " " + CurrentTbl03Regnum.Subregnum + " " + CultRes.StringsRes.SaveSuccess);
                        GetRegnumByNameOrId(o);  //Refresh
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

    

 //    Part 3    

    

 //    Part 4    


        
        #region "Public Commands Connect ==> Tbl06Phylum"                 

        private RelayCommand _getPhylumByNameCommand;
        public ICommand GetPhylumByNameCommand
        {
            get { return _getPhylumByNameCommand ?? (_getPhylumByNameCommand = new RelayCommand(delegate { GetPhylumByNameOrId(null); })); }
        }

        private void GetPhylumByNameOrId(object o)
        {
            int id;
            if (int.TryParse(SearchPhylumName, out id))
                Tbl06PhylumsList = new ObservableCollection<Tbl06Phylum> { Tbl06PhylumsRepository.Get(id) };
            else
            Tbl06PhylumsList =  new ObservableCollection<Tbl06Phylum>
                                                      (from x in Tbl06PhylumsRepository.FindAll()
                                                       where x.PhylumName.StartsWith(SearchPhylumName)
                                                       orderby x.PhylumName
                                                       select x);

            PhylumsView = CollectionViewSource.GetDefaultView(Tbl06PhylumsList);
            if (PhylumsView != null)
                PhylumsView.CurrentChanged += tbl06PhylumView_CurrentChanged;                   
            RaisePropertyChanged();
        }
        //------------------------------------------------------------------------------
        private RelayCommand _addPhylumCommand;
        public ICommand AddPhylumCommand
        {
            get { return _addPhylumCommand ?? (_addPhylumCommand = new RelayCommand(delegate { AddPhylum(null); })); }
        }

        private void AddPhylum(object o)
        {
            if (Tbl06PhylumsList == null)
                Tbl06PhylumsList = new ObservableCollection<Tbl06Phylum>();
            Tbl06PhylumsList.Add(new Tbl06Phylum{ PhylumName= CultRes.StringsRes.DatasetNew });                   
            PhylumsView = CollectionViewSource.GetDefaultView(Tbl06PhylumsList);
            if (PhylumsView != null)
                PhylumsView.CurrentChanged += tbl06PhylumView_CurrentChanged;
            RaisePropertyChanged();
        }
        //---------------------------------------------------------------------------------------
        private RelayCommand _deletePhylumCommand;
        public ICommand DeletePhylumCommand
        {
            get { return _deletePhylumCommand ?? (_deletePhylumCommand = new RelayCommand(delegate { DeletePhylum(null); })); }
        }

        private void DeletePhylum(object o)
        {
            try
            {
                var phylum = Tbl06PhylumsRepository.Tbl06Phylums.FirstOrDefault(x => x.PhylumID== CurrentTbl06Phylum.PhylumID);
                if (phylum != null)
                {
                    if (MessageBox.Show(CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl06Phylum.PhylumName, CultRes.StringsRes.DeleteQuestion1, MessageBoxButton.YesNo) !=
                        MessageBoxResult.Yes) return;
                    Tbl06PhylumsRepository.Delete(phylum);
                    Tbl06PhylumsRepository.Save();
                    MessageBox.Show(CurrentTbl06Phylum.PhylumName + " " + CultRes.StringsRes.DeleteSuccess);
                    GetPhylumByNameOrId(o);  //Refresh
                }
                else
                {
                    MessageBox.Show(CultRes.StringsRes.DeleteCan + " " + CurrentTbl06Phylum.PhylumName+ " " + CultRes.StringsRes.DeleteCan1);
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
            get { return _savePhylumCommand ?? (_savePhylumCommand = new RelayCommand(delegate { SavePhylum(null); })); }
        }

        private void SavePhylum(object o)
        {
            try
            {
                var phylum = Tbl06PhylumsRepository.Tbl06Phylums.FirstOrDefault(x => x.PhylumID== CurrentTbl06Phylum.PhylumID);
                if (CurrentTbl06Phylum == null)
                {
                    MessageBox.Show(CultRes.StringsRes.DatasetNotExist);
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
                            CountID = RandomHelper.Randomnumber(),
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
                        if (CurrentTbl06Phylum != null)
                            MessageBox.Show(CurrentTbl06Phylum.PhylumName+ " " + CultRes.StringsRes.SaveSuccess);
                        GetPhylumByNameOrId(o);  //Refresh      
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


        
        #region "Public Commands Connect ==> Tbl09Division"                 

        private RelayCommand _getDivisionByNameCommand;
        public ICommand GetDivisionByNameCommand
        {
            get { return _getDivisionByNameCommand ?? (_getDivisionByNameCommand = new RelayCommand(delegate { GetDivisionByNameOrId(null); })); }
        }

        private void GetDivisionByNameOrId(object o)
        {
            int id;
            if (int.TryParse(SearchDivisionName, out id))
                Tbl09DivisionsList = new ObservableCollection<Tbl09Division> { Tbl09DivisionsRepository.Get(id) };
            else
            Tbl09DivisionsList =  new ObservableCollection<Tbl09Division>
                                                      (from x in Tbl09DivisionsRepository.FindAll()
                                                       where x.DivisionName.StartsWith(SearchDivisionName)
                                                       orderby x.DivisionName
                                                       select x);

            DivisionsView = CollectionViewSource.GetDefaultView(Tbl09DivisionsList);
            if (DivisionsView != null)
                DivisionsView.CurrentChanged += tbl09DivisionView_CurrentChanged;                   
            RaisePropertyChanged();
        }
        //------------------------------------------------------------------------------
        private RelayCommand _addDivisionCommand;
        public ICommand AddDivisionCommand
        {
            get { return _addDivisionCommand ?? (_addDivisionCommand = new RelayCommand(delegate { AddDivision(null); })); }
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
        public ICommand DeleteDivisionCommand
        {
            get { return _deleteDivisionCommand ?? (_deleteDivisionCommand = new RelayCommand(delegate { DeleteDivision(null); })); }
        }

        private void DeleteDivision(object o)
        {
            try
            {
                var division = Tbl09DivisionsRepository.Tbl09Divisions.FirstOrDefault(x => x.DivisionID== CurrentTbl09Division.DivisionID);
                if (division != null)
                {
                    if (MessageBox.Show(CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl09Division.DivisionName, CultRes.StringsRes.DeleteQuestion1, MessageBoxButton.YesNo) !=
                        MessageBoxResult.Yes) return;
                    Tbl09DivisionsRepository.Delete(division);
                    Tbl09DivisionsRepository.Save();
                    MessageBox.Show(CurrentTbl09Division.DivisionName + " " + CultRes.StringsRes.DeleteSuccess);
                    GetDivisionByNameOrId(o);  //Refresh
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
        public ICommand SaveDivisionCommand
        {
            get { return _saveDivisionCommand ?? (_saveDivisionCommand = new RelayCommand(delegate { SaveDivision(null); })); }
        }

        private void SaveDivision(object o)
        {
            try
            {
                var division = Tbl09DivisionsRepository.Tbl09Divisions.FirstOrDefault(x => x.DivisionID== CurrentTbl09Division.DivisionID);
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
                        if (CurrentTbl09Division != null)
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
    
      

 //    Part 6    

 

 //    Part 7    

 

 //    Part 8    


    
        #region "Public Commands Connect ==> Tbl90RefAuthor"

        private RelayCommand _getRefAuthorByNameCommand;
        public ICommand GetRefAuthorByNameCommand
        {
            get { return _getRefAuthorByNameCommand ?? (_getRefAuthorByNameCommand = new RelayCommand(delegate { GetRefAuthorByNameOrId(null); })); }
        }

        public void GetRefAuthorByNameOrId(object o)
        {
            int id;
            if (int.TryParse(SearchRefAuthorName, out id))
                Tbl90RefAuthorsList = new ObservableCollection<Tbl90Reference> { Tbl90ReferencesRepository.Get(id) };

            else
                Tbl90RefAuthorsList = new ObservableCollection<Tbl90Reference>
                                (from refAuthor in Tbl90ReferencesRepository.FindAll()
                                 where refAuthor.Tbl90RefAuthors.RefAuthorName.StartsWith(SearchRefAuthorName)
                                       && refAuthor.Tbl90RefExperts == null
                                       && refAuthor.Tbl90RefSources == null
                                 orderby refAuthor.Tbl90RefAuthors.RefAuthorName, refAuthor.Tbl90RefAuthors.BookName, refAuthor.Tbl90RefAuthors.Page1
                                 select refAuthor);

            RefAuthorsView = CollectionViewSource.GetDefaultView(Tbl90RefAuthorsList);
            if (RefAuthorsView != null)
                RefAuthorsView.CurrentChanged += tbl90RefAuthorView_CurrentChanged;
            RaisePropertyChanged();
        }

        //----------------------------------------------------
        private RelayCommand _addRefAuthorCommand;
        public ICommand AddRefAuthorCommand
        {
            get { return _addRefAuthorCommand ?? (_addRefAuthorCommand = new RelayCommand(delegate { AddRefAuthor(null); })); }
        }

        public void AddRefAuthor(object o)
        {
            if (Tbl90RefAuthorsList == null)
                Tbl90RefAuthorsList = new ObservableCollection<Tbl90Reference>();
            Tbl90RefAuthorsList.Add(new Tbl90Reference { Info = CultRes.StringsRes.DatasetNew });
            RefAuthorsView = CollectionViewSource.GetDefaultView(Tbl90RefAuthorsList);
            if (RefAuthorsView != null)
                RefAuthorsView.CurrentChanged += tbl90RefAuthorView_CurrentChanged;
            RaisePropertyChanged();
        }

        //---------------------------------------------------------------------------------------

        private RelayCommand _deleteRefAuthorCommand;
        public ICommand DeleteRefAuthorCommand
        {
            get { return _deleteRefAuthorCommand ?? (_deleteRefAuthorCommand = new RelayCommand(delegate { DeleteRefAuthor(null); })); }
        }

        public void DeleteRefAuthor(object o)
        {
            try
            {
                var refAuthor = Tbl90ReferencesRepository.Tbl90References.FirstOrDefault(x => x.ReferenceID == CurrentTbl90RefAuthor.ReferenceID);
                if (refAuthor != null)
                {
                    if (MessageBox.Show(CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl90RefAuthor.Info, CultRes.StringsRes.DeleteQuestion1, MessageBoxButton.YesNo) !=
                        MessageBoxResult.Yes) return;
                    Tbl90ReferencesRepository.Delete(refAuthor);
                    Tbl90ReferencesRepository.Save();
                    MessageBox.Show(CurrentTbl90RefAuthor.Info + " " + CultRes.StringsRes.DeleteSuccess);
                    GetRefAuthorByNameOrId(o); //Refresch
                }
                else
                {
                    MessageBox.Show(CultRes.StringsRes.DeleteCan + " " + CurrentTbl90RefAuthor.Info + " " + CultRes.StringsRes.DeleteCan1);
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
            get { return _saveRefAuthorCommand ?? (_saveRefAuthorCommand = new RelayCommand(delegate { SaveRefAuthor(null); })); }
        }

        public void SaveRefAuthor(object o)
        {
            try
            {
                var refAuthor = Tbl90ReferencesRepository.Tbl90References.FirstOrDefault(x => x.ReferenceID == CurrentTbl90RefAuthor.ReferenceID);
                if (CurrentTbl90RefAuthor == null)
                {
                    MessageBox.Show(CultRes.StringsRes.DatasetNotExist);
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
                        Tbl90ReferencesRepository.Save();
                        MessageBox.Show(CurrentTbl90RefAuthor.Info + " " + CultRes.StringsRes.SaveSuccess);
                        GetRefAuthorByNameOrId(o);  //Refresh
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
            get { return _getRefSourceByNameCommand ?? (_getRefSourceByNameCommand = new RelayCommand(delegate { GetRefSourceByNameOrId(null); })); }
        }

        public void GetRefSourceByNameOrId(object o)
        {
            int id;
            if (int.TryParse(SearchRefSourceName, out id))
                Tbl90RefSourcesList = new ObservableCollection<Tbl90Reference> { Tbl90ReferencesRepository.Get(id) };

            else
                Tbl90RefSourcesList = new ObservableCollection<Tbl90Reference>
                                (from refSource in Tbl90ReferencesRepository.FindAll()
                                 where refSource.Tbl90RefSources.RefSourceName.StartsWith(SearchRefSourceName)
                                                          && refSource.Tbl90RefExperts == null
                                                          && refSource.Tbl90RefAuthors == null
                                 orderby refSource.Tbl90RefSources.RefSourceName, refSource.Tbl90RefSources.Author, refSource.Tbl90RefSources.SourceYear
                                 select refSource);

            RefSourcesView = CollectionViewSource.GetDefaultView(Tbl90RefSourcesList);
            if (RefSourcesView != null)
                RefSourcesView.CurrentChanged += tbl90RefSourceView_CurrentChanged;
            RaisePropertyChanged();
        }

        //----------------------------------------------------
        private RelayCommand _addRefSourceCommand;
        public ICommand AddRefSourceCommand
        {
            get { return _addRefSourceCommand ?? (_addRefSourceCommand = new RelayCommand(delegate { AddRefSource(null); })); }
        }

        public void AddRefSource(object o)
        {
            if (Tbl90RefSourcesList == null)
                Tbl90RefSourcesList = new ObservableCollection<Tbl90Reference>();
            Tbl90RefSourcesList.Add(new Tbl90Reference { Info = CultRes.StringsRes.DatasetNew });
            RefSourcesView = CollectionViewSource.GetDefaultView(Tbl90RefSourcesList);
            if (RefSourcesView != null)
                RefSourcesView.CurrentChanged += tbl90RefSourceView_CurrentChanged;
            RaisePropertyChanged();
        }

        //---------------------------------------------------------------------------------------

        private RelayCommand _deleteRefSourceCommand;
        public ICommand DeleteRefSourceCommand
        {
            get { return _deleteRefSourceCommand ?? (_deleteRefSourceCommand = new RelayCommand(delegate { DeleteRefSource(null); })); }
        }

        public void DeleteRefSource(object o)
        {
            try
            {
                var refSource = Tbl90ReferencesRepository.Tbl90References.FirstOrDefault(x => x.ReferenceID == CurrentTbl90RefSource.ReferenceID);
                if (refSource != null)
                {
                    if (MessageBox.Show(CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl90RefSource.Info, CultRes.StringsRes.DeleteQuestion1, MessageBoxButton.YesNo) !=
                        MessageBoxResult.Yes) return;
                    Tbl90ReferencesRepository.Delete(refSource);
                    Tbl90ReferencesRepository.Save();
                    MessageBox.Show(CurrentTbl90RefSource.Info + " " + CultRes.StringsRes.DeleteSuccess);
                    GetRefSourceByNameOrId(o); //Refresh
                }
                else
                {
                    MessageBox.Show(CultRes.StringsRes.DeleteCan + " " + CurrentTbl90RefSource.Info + " " + CultRes.StringsRes.DeleteCan1);
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
            get { return _saveRefSourceCommand ?? (_saveRefSourceCommand = new RelayCommand(delegate { SaveRefSource(null); })); }
        }

        public void SaveRefSource(object o)
        {
            try
            {
                var refSource = Tbl90ReferencesRepository.Tbl90References.FirstOrDefault(x => x.ReferenceID == CurrentTbl90RefSource.ReferenceID);
                if (CurrentTbl90RefSource == null)
                {
                    MessageBox.Show(CultRes.StringsRes.DatasetNotExist);
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
                        Tbl90ReferencesRepository.Save();
                        MessageBox.Show(CurrentTbl90RefSource.Info + " " + CultRes.StringsRes.SaveSuccess);
                        GetRefSourceByNameOrId(o); //Refresh                                                            
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
            get { return _getRefExpertByNameCommand ?? (_getRefExpertByNameCommand = new RelayCommand(delegate { GetRefExpertByNameOrId(null); })); }
        }

        public void GetRefExpertByNameOrId(object o)
        {
            int id;
            if (int.TryParse(SearchRefExpertName, out id))
                Tbl90RefExpertsList = new ObservableCollection<Tbl90Reference> { Tbl90ReferencesRepository.Get(id) };

            else
                Tbl90RefExpertsList = new ObservableCollection<Tbl90Reference>
                                (from refExpert in Tbl90ReferencesRepository.FindAll()
                                 where refExpert.Tbl90RefExperts.RefExpertName.StartsWith(SearchRefExpertName)
                                 && refExpert.Tbl90RefSources == null
                                 && refExpert.Tbl90RefAuthors == null
                                 orderby refExpert.Tbl90RefExperts.RefExpertName, refExpert.Tbl90RefExperts.Info
                                 select refExpert);

            RefExpertsView = CollectionViewSource.GetDefaultView(Tbl90RefExpertsList);
            if (RefExpertsView != null)
                RefExpertsView.CurrentChanged += tbl90RefExpertView_CurrentChanged;
            RaisePropertyChanged();
        }

        //----------------------------------------------------
        private RelayCommand _addRefExpertCommand;
        public ICommand AddRefExpertCommand
        {
            get { return _addRefExpertCommand ?? (_addRefExpertCommand = new RelayCommand(delegate { AddRefExpert(null); })); }
        }

        public void AddRefExpert(object o)
        {
            if (Tbl90RefExpertsList == null)
                Tbl90RefExpertsList = new ObservableCollection<Tbl90Reference>();
            Tbl90RefExpertsList.Add(new Tbl90Reference { Info = CultRes.StringsRes.DatasetNew });
            RefExpertsView = CollectionViewSource.GetDefaultView(Tbl90RefExpertsList);
            if (RefExpertsView != null)
                RefExpertsView.CurrentChanged += tbl90RefExpertView_CurrentChanged;
            RaisePropertyChanged();
        }

        //---------------------------------------------------------------------------------------
        private RelayCommand _deleteRefExpertCommand;
        public ICommand DeleteRefExpertCommand
        {
            get { return _deleteRefExpertCommand ?? (_deleteRefExpertCommand = new RelayCommand(delegate { DeleteRefExpert(null); })); }
        }

        public void DeleteRefExpert(object o)
        {
            try
            {
                var refExpert = Tbl90ReferencesRepository.Tbl90References.FirstOrDefault(x => x.ReferenceID == CurrentTbl90RefExpert.ReferenceID);
                if (refExpert != null)
                {
                    if (MessageBox.Show(CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl90RefExpert.Info, CultRes.StringsRes.DeleteQuestion1, MessageBoxButton.YesNo) !=
                        MessageBoxResult.Yes) return;
                    Tbl90ReferencesRepository.Delete(refExpert);
                    Tbl90ReferencesRepository.Save();
                    MessageBox.Show(CurrentTbl90RefExpert.Info + " " + CultRes.StringsRes.DeleteSuccess);
                    GetRefExpertByNameOrId(o); //Refresh                                
                }
                else
                {
                    MessageBox.Show(CultRes.StringsRes.DeleteCan + " " + CurrentTbl90RefExpert.Info + " " + CultRes.StringsRes.DeleteCan1);
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
            get { return _saveRefExpertCommand ?? (_saveRefExpertCommand = new RelayCommand(delegate { SaveRefExpert(null); })); }
        }

        public void SaveRefExpert(object o)
        {
            try
            {
                var refExpert = Tbl90ReferencesRepository.Tbl90References.FirstOrDefault(x => x.ReferenceID == CurrentTbl90RefExpert.ReferenceID);
                if (CurrentTbl90RefExpert == null)
                {
                    MessageBox.Show(CultRes.StringsRes.DatasetNotExist);
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
                        Tbl90ReferencesRepository.Save();
                        MessageBox.Show(CurrentTbl90RefExpert.Info + " " + CultRes.StringsRes.SaveSuccess);
                        GetRefExpertByNameOrId(o); //Refresh                                                                    
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
            get { return _getCommentByNameCommand ?? (_getCommentByNameCommand = new RelayCommand(delegate { GetCommentByName(null); })); }
        }

        public void GetCommentByName(object o)
        {
            int id;
            if (int.TryParse(SearchCommentInfo, out id))
                Tbl93CommentsList = new ObservableCollection<Tbl93Comment> { Tbl93CommentsRepository.Get(id) };

            else
                Tbl93CommentsList = new ObservableCollection<Tbl93Comment>
                                (from comment in Tbl93CommentsRepository.FindAll()
                                 where comment.Info.StartsWith(SearchCommentInfo)
                                 select comment);

            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            if (CommentsView != null)
                CommentsView.CurrentChanged += tbl93CommentView_CurrentChanged;
            RaisePropertyChanged();
        }
        //------------------------------------------------------------------------------

        private RelayCommand _addCommentCommand;
        public ICommand AddCommentCommand
        {
            get { return _addCommentCommand ?? (_addCommentCommand = new RelayCommand(delegate { AddComment(null); })); }
        }

        public void AddComment(object o)
        {
            if (Tbl93CommentsList == null)
                Tbl93CommentsList = new ObservableCollection<Tbl93Comment>();
            Tbl93CommentsList.Add(new Tbl93Comment { Info = CultRes.StringsRes.DatasetNew });

            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            if (CommentsView != null)
                CommentsView.CurrentChanged += tbl93CommentView_CurrentChanged;
            RaisePropertyChanged();
        }

        //---------------------------------------------------------------------------------------

        private RelayCommand _deleteCommentCommand;
        public ICommand DeleteCommentCommand
        {
            get { return _deleteCommentCommand ?? (_deleteCommentCommand = new RelayCommand(delegate { DeleteComment(null); })); }
        }

        private void DeleteComment(object o)
        {
            try
            {
                var comment = Tbl93CommentsRepository.Tbl93Comments.FirstOrDefault(x => x.CommentID == CurrentTbl93Comment.CommentID);
                if (comment != null)
                {
                    if (MessageBox.Show(CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl93Comment.CommentID
                                         , CultRes.StringsRes.DeleteQuestion1, MessageBoxButton.YesNo) !=
                        MessageBoxResult.Yes) return;
                    Tbl93CommentsRepository.Delete(comment);
                    Tbl93CommentsRepository.Save();
                    MessageBox.Show(CurrentTbl93Comment.CommentID + " " + CultRes.StringsRes.DeleteSuccess);
                    GetCommentByName(o); //Refresh                                          
                }
                else
                {
                    MessageBox.Show(CultRes.StringsRes.DeleteCan + " " + CurrentTbl93Comment.CommentID + " " + CultRes.StringsRes.DeleteCan1);
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
            get { return _saveCommentCommand ?? (_saveCommentCommand = new RelayCommand(delegate { SaveComment(null); })); }
        }

        private void SaveComment(object o)
        {
            try
            {
                var comment = Tbl93CommentsRepository.Tbl93Comments.FirstOrDefault(x => x.CommentID == CurrentTbl93Comment.CommentID);
                if (CurrentTbl93Comment == null)
                {
                    MessageBox.Show(CultRes.StringsRes.DatasetNotExist);
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
                        Tbl93CommentsRepository.Save();
                        MessageBox.Show(CurrentTbl93Comment.CommentID + " " + CultRes.StringsRes.SaveSuccess);
                        GetCommentByName(o); //Refresh                                                   
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
        public ICommand GetConnectedTablesCommand
        {
            get { return _getConnectedTablesCommand ?? (_getConnectedTablesCommand = new RelayCommand(delegate { GetConnectedTablesById(null); })); }
        }

        private void GetConnectedTablesById(object o)
        {
            //Clear Search-TextBox
            SearchPhylumName = null;
            SearchDivisionName = null;
            SearchCommentInfo = null;
            SearchRefExpertName = null;
            SearchRefSourceName = null;
            SearchRefAuthorName = null;

            Tbl06PhylumsList = new ObservableCollection<Tbl06Phylum>
                (from phy in Tbl06PhylumsRepository.Tbl06Phylums
                where phy.RegnumID == CurrentTbl03Regnum.RegnumID
                orderby phy.PhylumName
                select phy);

            PhylumsView = CollectionViewSource.GetDefaultView(Tbl06PhylumsList);
            if (PhylumsView != null)
                PhylumsView.CurrentChanged += tbl06PhylumView_CurrentChanged;
            RaisePropertyChanged();
            //-----------------------------------------------------------------------------------
            Tbl09DivisionsList = new ObservableCollection<Tbl09Division>
                (from div in Tbl09DivisionsRepository.Tbl09Divisions
                where div.RegnumID == CurrentTbl03Regnum.RegnumID
                orderby div.DivisionName
                select div);

            DivisionsView = CollectionViewSource.GetDefaultView(Tbl09DivisionsList);
            if (DivisionsView != null)
                DivisionsView.CurrentChanged += tbl09DivisionView_CurrentChanged;
            RaisePropertyChanged();
             //-----------------------------------------------------------------------------------
            Tbl90RefAuthorsList = new ObservableCollection<Tbl90Reference>
                (from refAu in Tbl90ReferencesRepository.Tbl90References
                where refAu.RegnumID == CurrentTbl03Regnum.RegnumID
                && refAu.Tbl90RefExperts == null
                && refAu.Tbl90RefSources == null
                orderby refAu.Tbl90RefAuthors.RefAuthorName, refAu.Tbl90RefAuthors.BookName, refAu.Tbl90RefAuthors.Page1
                select refAu);

            RefAuthorsView = CollectionViewSource.GetDefaultView(Tbl90RefAuthorsList);
            if (RefAuthorsView != null)
                RefAuthorsView.CurrentChanged += tbl90RefAuthorView_CurrentChanged;
            RaisePropertyChanged();
            //--------------------------------------------------------------------------------------
            Tbl90RefSourcesList = new ObservableCollection<Tbl90Reference>
                (from refSo in Tbl90ReferencesRepository.Tbl90References
                where refSo.RegnumID == CurrentTbl03Regnum.RegnumID
                && refSo.Tbl90RefExperts == null
                && refSo.Tbl90RefAuthors == null
                orderby refSo.Tbl90RefSources.RefSourceName
                select refSo);

            RefSourcesView = CollectionViewSource.GetDefaultView(Tbl90RefSourcesList);
            if (RefSourcesView != null)
                RefSourcesView.CurrentChanged += tbl90RefSourceView_CurrentChanged;
            RaisePropertyChanged();
            //--------------------------------------------------------------------------------------
            Tbl90RefExpertsList = new ObservableCollection<Tbl90Reference>
                (from refEx in Tbl90ReferencesRepository.Tbl90References
                where refEx.RegnumID == CurrentTbl03Regnum.RegnumID
                && refEx.Tbl90RefAuthors == null
                && refEx.Tbl90RefSources == null
                orderby refEx.Tbl90RefExperts.RefExpertName
                select refEx);

            RefExpertsView = CollectionViewSource.GetDefaultView(Tbl90RefExpertsList);
            if (RefExpertsView != null)
                RefExpertsView.CurrentChanged += tbl90RefExpertView_CurrentChanged;
            RaisePropertyChanged();
            //-----------------------------------------------------------------------------------
            Tbl93CommentsList = new ObservableCollection<Tbl93Comment>
                (from comm in Tbl93CommentsRepository.Tbl93Comments
                where comm.RegnumID == CurrentTbl03Regnum.RegnumID
                orderby comm.Info
                select comm);

            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            if (CommentsView != null)
                CommentsView.CurrentChanged += tbl93CommentView_CurrentChanged;
            RaisePropertyChanged();
            //--------------------------------------------------------------
        }

        #endregion "Public Commands Connected Tables by DoubleClick"
   
 

 //    Part 10    

     
        #region "Public Commands to open Detail TabItems"

        private int _selectedMainTabIndex;
        public int SelectedMainTabIndex
        {
            get { return _selectedMainTabIndex; }
            set
            {
                if (value == _selectedMainTabIndex) return;
                _selectedMainTabIndex = value; RaisePropertyChanged();
                if (_selectedMainTabIndex == 0)
                    SelectedDetailTabIndex = 0;
                if (_selectedMainTabIndex == 1)
                    SelectedDetailTabIndex = 1;
                if (_selectedMainTabIndex == 2)
                    SelectedDetailTabIndex = 2;
                if (_selectedMainTabIndex == 3)
                    SelectedDetailTabIndex = 3;
                if (_selectedMainTabIndex == 4)
                    SelectedDetailTabIndex = 4;
                if (_selectedMainTabIndex == 5)
                    SelectedDetailTabIndex = 5;
                if (_selectedMainTabIndex == 6)
                    SelectedDetailTabIndex = 6;
                if (_selectedMainTabIndex == 7)
                    SelectedDetailTabIndex = 7;
            }
        }

        private int _selectedMainSubTabIndex;
        public int SelectedMainSubTabIndex
        {
            get { return _selectedMainSubTabIndex; }
            set
            {
                if (value == _selectedMainSubTabIndex) return;
                _selectedMainSubTabIndex = value; RaisePropertyChanged();
                if (_selectedMainSubTabIndex == 0)
                    SelectedDetailSubTabIndex = 0;
                if (_selectedMainSubTabIndex == 1)
                    SelectedDetailSubTabIndex = 1;
                if (_selectedMainSubTabIndex == 2)
                    SelectedDetailSubTabIndex = 2;
            }
        }

        private int _selectedMainSubRefTabIndex;
        public int SelectedMainSubRefTabIndex
        {
            get { return _selectedMainSubRefTabIndex; }
            set
            {
                if (value == _selectedMainSubRefTabIndex) return;
                _selectedMainSubRefTabIndex = value; RaisePropertyChanged();
                if (_selectedMainSubRefTabIndex == 0)
                    SelectedDetailSubRefTabIndex = 0;
                if (_selectedMainSubRefTabIndex == 1)
                    SelectedDetailSubRefTabIndex = 1;
                if (_selectedMainSubRefTabIndex == 2)
                    SelectedDetailSubRefTabIndex = 2;
            }
        }

        private int _selectedDetailTabIndex;
        public int SelectedDetailTabIndex
        {
            get { return _selectedDetailTabIndex; }
            set
            {
                if (value == _selectedDetailTabIndex) return;
                _selectedDetailTabIndex = value; RaisePropertyChanged();
                if (_selectedDetailTabIndex == 0)
                    SelectedMainTabIndex = 0;
                if (_selectedDetailTabIndex == 1)
                    SelectedMainTabIndex = 1;
                if (_selectedDetailTabIndex == 2)
                    SelectedMainTabIndex = 2;
                if (_selectedDetailTabIndex == 3)
                    SelectedMainTabIndex = 3;
                if (_selectedDetailTabIndex == 4)
                    SelectedMainTabIndex = 4;
                if (_selectedDetailTabIndex == 5)
                    SelectedMainTabIndex = 5;
                if (_selectedDetailTabIndex == 6)
                    SelectedMainTabIndex = 6;
                if (_selectedDetailTabIndex == 7)
                    SelectedMainTabIndex = 7;
            }
        }

        private int _selectedDetailSubTabIndex;
        public int SelectedDetailSubTabIndex
        {
            get { return _selectedDetailSubTabIndex; }
            set
            {
                if (value == _selectedDetailSubTabIndex) return;
                _selectedDetailSubTabIndex = value; RaisePropertyChanged();
                if (_selectedDetailSubTabIndex == 0)
                    SelectedMainSubTabIndex = 0;
                if (_selectedDetailSubTabIndex == 1)
                    SelectedMainSubTabIndex = 1;
                if (_selectedDetailSubTabIndex == 2)
                    SelectedMainSubTabIndex = 2;
                if (_selectedDetailSubTabIndex == 3)
                    SelectedMainSubTabIndex = 3;

            }
        }

        private int _selectedDetailSubRefTabIndex;
        public int SelectedDetailSubRefTabIndex
        {
            get { return _selectedDetailSubRefTabIndex; }
            set
            {
                if (value == _selectedDetailSubRefTabIndex) return;
                _selectedDetailSubRefTabIndex = value; RaisePropertyChanged();
                if (_selectedDetailSubRefTabIndex == 0)
                    SelectedMainSubRefTabIndex = 0;
                if (_selectedDetailSubRefTabIndex == 1)
                    SelectedMainSubRefTabIndex = 1;
                if (_selectedDetailSubRefTabIndex == 2)
                    SelectedMainSubRefTabIndex = 2;
            }
        }

        #endregion "Public Commands to open Detail TabItems"
 

 //    Part 11    


     
        #region "Public Properties Tbl03Regnum"

        private string _searchRegnumName;
        public string SearchRegnumName
        {
            get { return _searchRegnumName; }
            set { _searchRegnumName = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl03Regnum> _tbl03RegnumsList;
        public ObservableCollection<Tbl03Regnum> Tbl03RegnumsList
        {
            get { return _tbl03RegnumsList; }
            set { _tbl03RegnumsList = value; RaisePropertyChanged(); }
        }

        public ICollectionView RegnumsView;
        public Tbl03Regnum CurrentTbl03Regnum => RegnumsView?.CurrentItem as Tbl03Regnum;           

        private ObservableCollection<Tbl03Regnum> _tbl03RegnumsAllList;
        public ObservableCollection<Tbl03Regnum> Tbl03RegnumsAllList
        {
            get { return _tbl03RegnumsAllList; }
            set { _tbl03RegnumsAllList = value; RaisePropertyChanged(); }       
        }

        #endregion "Public Properties"
   

       
        #region "Public Properties Tbl06Phylum"

        private string _searchPhylumName;
        public string SearchPhylumName
        {
            get { return _searchPhylumName; }
            set { _searchPhylumName = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl06Phylum> _tbl06PhylumsList;
        public ObservableCollection<Tbl06Phylum> Tbl06PhylumsList
        {
            get { return _tbl06PhylumsList; }
            set { _tbl06PhylumsList = value; RaisePropertyChanged(); }
        }

        public ICollectionView PhylumsView;
        public Tbl06Phylum CurrentTbl06Phylum => PhylumsView?.CurrentItem as Tbl06Phylum;           

        #endregion "Public Properties"
   
  
       
        #region "Public Properties Tbl09Division"

        private string _searchDivisionName;
        public string SearchDivisionName
        {
            get { return _searchDivisionName; }
            set { _searchDivisionName = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl09Division> _tbl09DivisionsList;
        public ObservableCollection<Tbl09Division> Tbl09DivisionsList
        {
            get { return _tbl09DivisionsList; }
            set { _tbl09DivisionsList = value; RaisePropertyChanged(); }
        }

        public ICollectionView DivisionsView;
        public Tbl09Division CurrentTbl09Division => DivisionsView?.CurrentItem as Tbl09Division;           

        #endregion "Public Properties"
   
  
     
        #region "Public Properties Tbl90Author"

        private ObservableCollection<Tbl90RefAuthor> _tbl90AuthorsAllList;
        public ObservableCollection<Tbl90RefAuthor> Tbl90AuthorsAllList
        {
            get { return _tbl90AuthorsAllList; }
            set { _tbl90AuthorsAllList = value; RaisePropertyChanged(); }
        }

        #endregion "Public Properties "

        #region "Public Properties Tbl90Source"

        private ObservableCollection<Tbl90RefSource> _tbl90SourcesAllList;
        public ObservableCollection<Tbl90RefSource> Tbl90SourcesAllList
        {
            get { return _tbl90SourcesAllList; }
            set { _tbl90SourcesAllList = value; RaisePropertyChanged(); }
        }

        #endregion "Public Properties "

        #region "Public Properties Tbl90Expert"

        private ObservableCollection<Tbl90RefExpert> _tbl90ExpertsAllList;
        public ObservableCollection<Tbl90RefExpert> Tbl90ExpertsAllList
        {
            get { return _tbl90ExpertsAllList; }
            set { _tbl90ExpertsAllList = value; RaisePropertyChanged(); }
        }

        #endregion "Public Properties "

        #region "Public Properties Tbl90RefAuthor"

        private string _searchRefAuthorName;
        public string SearchRefAuthorName
        {
            get { return _searchRefAuthorName; }
            set { _searchRefAuthorName = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl90Reference> _tbl90RefAuthorsList;
        public ObservableCollection<Tbl90Reference> Tbl90RefAuthorsList
        {
            get { return _tbl90RefAuthorsList; }
            set { _tbl90RefAuthorsList = value; RaisePropertyChanged(); }
        }

        public ICollectionView RefAuthorsView;
        public Tbl90Reference CurrentTbl90RefAuthor => RefAuthorsView?.CurrentItem as Tbl90Reference;

        #endregion "Public Properties"

        #region "Public Properties Tbl90RefSource"

        private string _searchRefSourceName;
        public string SearchRefSourceName
        {
            get { return _searchRefSourceName; }
            set { _searchRefSourceName = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl90Reference> _tbl90RefSourcesList;
        public ObservableCollection<Tbl90Reference> Tbl90RefSourcesList
        {
            get { return _tbl90RefSourcesList; }
            set { _tbl90RefSourcesList = value; RaisePropertyChanged(); }
        }

        public ICollectionView RefSourcesView;
        public Tbl90Reference CurrentTbl90RefSource => RefSourcesView?.CurrentItem as Tbl90Reference;

        #endregion "Public Properties"

        #region "Public Properties Tbl90RefExpert"

        private string _searchRefExpertName;
        public string SearchRefExpertName
        {
            get { return _searchRefExpertName; }
            set { _searchRefExpertName = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl90Reference> _tbl90RefExpertsList;
        public ObservableCollection<Tbl90Reference> Tbl90RefExpertsList
        {
            get { return _tbl90RefExpertsList; }
            set { _tbl90RefExpertsList = value; RaisePropertyChanged(); }
        }

        public ICollectionView RefExpertsView;
        public Tbl90Reference CurrentTbl90RefExpert => RefExpertsView?.CurrentItem as Tbl90Reference;

        #endregion "Public Properties"
   

     
        #region "Public Properties Tbl93Comment"

        private string _searchCommentInfo;
        public string SearchCommentInfo
        {
            get { return _searchCommentInfo; }
            set { _searchCommentInfo = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl93Comment> _tbl93CommentsList;
        public ObservableCollection<Tbl93Comment> Tbl93CommentsList
        {
            get { return _tbl93CommentsList; }
            set { _tbl93CommentsList = value; RaisePropertyChanged(); }
        }

        public ICollectionView CommentsView;
        public Tbl93Comment CurrentTbl93Comment => CommentsView?.CurrentItem as Tbl93Comment;

        #endregion "Public Properties"
   
 

 //    Part 12    


     
        #region "Private Methods"

        public void tbl03RegnumView_CurrentChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged();
        }

   
        
        public void tbl06PhylumView_CurrentChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged();
        }
   
          
        public void tbl09DivisionView_CurrentChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged();
        }
   
  
     

        public void tbl90RefAuthorView_CurrentChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged();
        }

        public void tbl90RefSourceView_CurrentChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged();
        }

        public void tbl90RefExpertView_CurrentChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged();
        }

        public void tbl90AuthorView_CurrentChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged();
        }

        public void tbl90SourceView_CurrentChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged();
        }

        private void tbl90ExpertView_CurrentChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged();
        }

        public void tbl93CommentView_CurrentChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged();
        }

        #endregion "Private Methods"
   
      }
}   
