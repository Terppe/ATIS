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

//    Tbl90RefExpertsViewModel Skriptdatum:  29.12.2011  10:32    

namespace WPFUI.Views.Database
{   


    
    public class Tbl90RefExpertsViewModel : NULLViewModel                     
    {     
         
      #region "Private Data Members"

        protected readonly Tbl69FiSpeciessesRepository Tbl69FiSpeciessesRepository = new Tbl69FiSpeciessesRepository();   
          
          #endregion "Private Data Members"            
    
        #region "Constructor"

        public Tbl90RefExpertsViewModel()
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

       
        #region "Public Commands Basic Tbl90RefExpert"

        private RelayCommand _getRefExpertByNameCommand;
        public new ICommand GetRefExpertByNameCommand
        {
            get { return _getRefExpertByNameCommand ?? (_getRefExpertByNameCommand = new RelayCommand(delegate { GetRefExpertByNameOrId(null); })); }   
        }

        private void GetRefExpertByNameOrId(object o)       
        {   
Tbl90RefExpertsList =  new ObservableCollection<Tbl90RefExpert>
                                                       (from x in Tbl90RefExpertsRepository.Tbl90RefExperts
                                                        where x.RefExpertName.StartsWith(SearchRefExpertName)
                                                        orderby x.RefExpertName
                                                        select x);

            Tbl90RefExpertsAllList =  new ObservableCollection<Tbl90RefExpert>
                                                       (from y in Tbl90RefExpertsRepository.Tbl90RefExperts
                                                        orderby y.RefExpertName
                                                        select y);

            NULLAllList =  new ObservableCollection<NULL>
                                                       (from z in NULLRepository.NULL
                                                        orderby z.NULL
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

  
Tbl69FiSpeciessesList = null;               
  View = CollectionViewSource.GetDefaultView(Tbl90RefExpertsList);
            if (View != null)
                View.CurrentChanged += tbl90RefExpertView_CurrentChanged;                   
            RaisePropertyChanged();
        }
        //------------------------------------------------------------------------------
        private RelayCommand _addRefExpertCommand;
        public new ICommand AddRefExpertCommand
        {
            get { return _addRefExpertCommand ?? (_addRefExpertCommand = new RelayCommand(AddRefExpert)); }
        }

        private void AddRefExpert(object o)
        {
            if (Tbl90RefExpertsList == null)
                Tbl90RefExpertsList = new ObservableCollection<Tbl90RefExpert>();
            Tbl90RefExpertsList.Add(new Tbl90RefExpert{ RefExpertName= CultRes.StringsRes.DatasetNew });
            View = CollectionViewSource.GetDefaultView(Tbl90RefExpertsList);
            if (View != null)
                View.CurrentChanged += tbl90RefExpertView_CurrentChanged;
            RaisePropertyChanged();
        }
        //---------------------------------------------------------------------------------------
  
       
        private RelayCommand _deleteRefExpertCommand;
        public new ICommand DeleteRefExpertCommand
        {
            get { return _deleteRefExpertCommand ?? (_deleteRefExpertCommand = new RelayCommand(delegate { DeleteRefExpert(null); })); }
        }

        private void DeleteRefExpert(object o)
        {
            try
            {
                var refExpert= Tbl90RefExpertsRepository.Tbl90RefExperts.FirstOrDefault(x => x.RefExpertID== CurrentTbl90RefExpert.RefExpertID);
                if (refExpert!= null)
                {
                    if (MessageBox.Show(CultRes.StringsRes.DeleteQuestion
                                        + " " +  CurrentTbl90RefExpert.RefExpertName, CultRes.StringsRes.DeleteQuestion1, MessageBoxButton.YesNo) !=
                        MessageBoxResult.Yes) return;
                    Tbl90RefExpertsRepository.Delete(refExpert);
                    Tbl90RefExpertsRepository.Save();
                    MessageBox.Show(CurrentTbl90RefExpert.RefExpertName + " " + CultRes.StringsRes.DeleteSuccess);
                    GetRefExpertByNameOrId(o); //Refresh
                }
                else
                {
                    MessageBox.Show(CultRes.StringsRes.DeleteCan + " " + CurrentTbl90RefExpert.RefExpertName+ " " + CultRes.StringsRes.DeleteCan1);
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
            get { return _saveRefExpertCommand ?? (_saveRefExpertCommand = new RelayCommand(delegate { SaveRefExpert(null); })); }
        }

        private void SaveRefExpert(object o)
        {
            try
            {
                var refExpert= Tbl90RefExpertsRepository.Tbl90RefExperts.FirstOrDefault(x => x.RefExpertID== CurrentTbl90RefExpert.RefExpertID);
                if (CurrentTbl90RefExpert == null)
                {
                    MessageBox.Show(CultRes.StringsRes.DatasetNotExist);
                }
                else
                {
                    if (CurrentTbl90RefExpert.RefExpertID!= 0)
                    {
                        if (refExpert!= null) //update
                        {
                            refExpert.Updater = Environment.UserName;
                            refExpert.UpdaterDate = DateTime.Now;
                        }
                    }
                    else
                    {
                        Tbl90RefExpertsRepository.Add(new Tbl90RefExpert
                        {
                            NULL= CurrentTbl90RefExpert.NULL,              
                            RefExpertName= CurrentTbl90RefExpert.RefExpertName,              
                            CountID = RandomHelper.Randomnumber(),
                            Valid = CurrentTbl90RefExpert.Valid,
                            ValidYear = CurrentTbl90RefExpert.ValidYear,
                            Synonym = CurrentTbl90RefExpert.Synonym,
                            Author = CurrentTbl90RefExpert.Author,
                            AuthorYear = CurrentTbl90RefExpert.AuthorYear,
                            Info = CurrentTbl90RefExpert.Info,
                            EngName = CurrentTbl90RefExpert.EngName,
                            GerName = CurrentTbl90RefExpert.GerName,
                            FraName = CurrentTbl90RefExpert.FraName,
                            PorName = CurrentTbl90RefExpert.PorName,
                            Writer = Environment.UserName,
                            WriterDate = DateTime.Now,
                            Updater = Environment.UserName,
                            UpdaterDate = DateTime.Now,
                            Memo = CurrentTbl90RefExpert.Memo
                        });
                    }
                    {
                        Tbl90RefExpertsRepository.Save();
                        MessageBox.Show(CurrentTbl90RefExpert.RefExpertName+ " " + CultRes.StringsRes.SaveSuccess);
                        GetRefExpertByNameOrId(o);  //Refresh
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


        
        #region "Public Commands Connect ==> NULL"                 

        private RelayCommand _getFiSpeciesByNameCommand;
        public ICommand GetFiSpeciesByNameCommand
        {
            get { return _getFiSpeciesByNameCommand ?? (_getFiSpeciesByNameCommand = new RelayCommand(delegate { GetFiSpeciesByNameOrId(null); })); }
        }

        private void GetFiSpeciesByNameOrId(object o)
        {
            int id;
            if (int.TryParse(SearchFiSpeciesName, out id))
                Tbl69FiSpeciessesList = new ObservableCollection<NULL> { Tbl69FiSpeciessesRepository.Get(id) };
            else
            Tbl69FiSpeciessesList =  new ObservableCollection<NULL>
                                                      (from x in Tbl69FiSpeciessesRepository.FindAll()
                                                       where x.NULL.StartsWith(SearchFiSpeciesName)
                                                       orderby x.NULL
                                                       select x);

            View = CollectionViewSource.GetDefaultView(Tbl69FiSpeciessesList);
            if (View != null)
                View.CurrentChanged += NULLView_CurrentChanged;                   
            RaisePropertyChanged();
        }
        //------------------------------------------------------------------------------
        private RelayCommand _addFiSpeciesCommand;
        public ICommand AddFiSpeciesCommand
        {
            get { return _addFiSpeciesCommand ?? (_addFiSpeciesCommand = new RelayCommand(delegate { AddFiSpecies(null); })); }
        }

        private void AddFiSpecies(object o)
        {
            if (Tbl69FiSpeciessesList == null)
                Tbl69FiSpeciessesList = new ObservableCollection<NULL>();
            Tbl69FiSpeciessesList.Add(new NULL{ NULL= CultRes.StringsRes.DatasetNew });                   
            View = CollectionViewSource.GetDefaultView(Tbl69FiSpeciessesList);
            if (View != null)
                View.CurrentChanged += NULLView_CurrentChanged;
            RaisePropertyChanged();
        }
        //---------------------------------------------------------------------------------------
        private RelayCommand _deleteFiSpeciesCommand;
        public ICommand DeleteFiSpeciesCommand
        {
            get { return _deleteFiSpeciesCommand ?? (_deleteFiSpeciesCommand = new RelayCommand(delegate { DeleteFiSpecies(null); })); }
        }

        private void DeleteFiSpecies(object o)
        {
            try
            {
                var NULL = Tbl69FiSpeciessesRepository.Tbl69FiSpeciesses.FirstOrDefault(x => x.NULL== CurrentNULL.NULL);
                if (NULL != null)
                {
                    if (MessageBox.Show(CultRes.StringsRes.DeleteQuestion + " " + CurrentNULL.NULL, CultRes.StringsRes.DeleteQuestion1, MessageBoxButton.YesNo) !=
                        MessageBoxResult.Yes) return;
                    Tbl69FiSpeciessesRepository.Delete(NULL);
                    Tbl69FiSpeciessesRepository.Save();
                    MessageBox.Show(CurrentNULL.NULL + " " + CultRes.StringsRes.DeleteSuccess);
                    GetFiSpeciesByNameOrId(o);  //Refresh
                }
                else
                {
                    MessageBox.Show(CultRes.StringsRes.DeleteCan + " " + CurrentNULL.NULL+ " " + CultRes.StringsRes.DeleteCan1);
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
            get { return _saveFiSpeciesCommand ?? (_saveFiSpeciesCommand = new RelayCommand(delegate { SaveFiSpecies(null); })); }
        }

        private void SaveFiSpecies(object o)
        {
            try
            {
                var NULL = Tbl69FiSpeciessesRepository.Tbl69FiSpeciesses.FirstOrDefault(x => x.NULL== CurrentNULL.NULL);
                if (CurrentNULL == null)
                {
                    MessageBox.Show(CultRes.StringsRes.DatasetNotExist);
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
                            RefExpertID= CurrentNULL.RefExpertID,              
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
                        Tbl69FiSpeciessesRepository.Save();
                        if (CurrentNULL != null)
                            MessageBox.Show(CurrentNULL.NULL+ " " + CultRes.StringsRes.SaveSuccess);
                        GetFiSpeciesByNameOrId(o);  //Refresh      
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


        
        #region "Public Commands Connect ==> NULL"                 

        private RelayCommand _getPlSpeciesByNameCommand;
        public ICommand GetPlSpeciesByNameCommand
        {
            get { return _getPlSpeciesByNameCommand ?? (_getPlSpeciesByNameCommand = new RelayCommand(delegate { GetPlSpeciesByNameOrId(null); })); }
        }

        private void GetPlSpeciesByNameOrId(object o)
        {
            int id;
            if (int.TryParse(SearchPlSpeciesName, out id))
                Tbl72PlSpeciessesList = new ObservableCollection<NULL> { Tbl72PlSpeciessesRepository.Get(id) };
            else
            Tbl72PlSpeciessesList =  new ObservableCollection<NULL>
                                                      (from x in Tbl72PlSpeciessesRepository.FindAll()
                                                       where x.NULL.StartsWith(SearchPlSpeciesName)
                                                       orderby x.NULL
                                                       select x);

            View = CollectionViewSource.GetDefaultView(Tbl72PlSpeciessesList);
            if (View != null)
                View.CurrentChanged += NULLView_CurrentChanged;                   
            RaisePropertyChanged();
        }
        //------------------------------------------------------------------------------
        private RelayCommand _addPlSpeciesCommand;
        public ICommand AddPlSpeciesCommand
        {
            get { return _addPlSpeciesCommand ?? (_addPlSpeciesCommand = new RelayCommand(delegate { AddPlSpecies(null); })); }
        }

        private void AddPlSpecies(object o)
        {
            if (Tbl72PlSpeciessesList == null)
                Tbl72PlSpeciessesList = new ObservableCollection<NULL>();
            Tbl72PlSpeciessesList.Add(new NULL{ NULL= CultRes.StringsRes.DatasetNew });                   
            View = CollectionViewSource.GetDefaultView(Tbl72PlSpeciessesList);
            if (View != null)
                View.CurrentChanged += NULLView_CurrentChanged;
            RaisePropertyChanged();
        }
        //---------------------------------------------------------------------------------------
        private RelayCommand _deletePlSpeciesCommand;
        public ICommand DeletePlSpeciesCommand
        {
            get { return _deletePlSpeciesCommand ?? (_deletePlSpeciesCommand = new RelayCommand(delegate { DeletePlSpecies(null); })); }
        }

        private void DeletePlSpecies(object o)
        {
            try
            {
                var NULL = Tbl72PlSpeciessesRepository.Tbl72PlSpeciesses.FirstOrDefault(x => x.NULL== CurrentNULL.NULL);
                if (NULL != null)
                {
                    if (MessageBox.Show(CultRes.StringsRes.DeleteQuestion + " " + CurrentNULL.NULL, CultRes.StringsRes.DeleteQuestion1, MessageBoxButton.YesNo) !=
                        MessageBoxResult.Yes) return;
                    Tbl72PlSpeciessesRepository.Delete(NULL);
                    Tbl72PlSpeciessesRepository.Save();
                    MessageBox.Show(CurrentNULL.NULL + " " + CultRes.StringsRes.DeleteSuccess);
                    GetPlSpeciesByNameOrId(o);  //Refresh
                }
                else
                {
                    MessageBox.Show(CultRes.StringsRes.DeleteCan + " " + CurrentNULL.NULL+ " " + CultRes.StringsRes.DeleteCan1);
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
            get { return _savePlSpeciesCommand ?? (_savePlSpeciesCommand = new RelayCommand(delegate { SavePlSpecies(null); })); }
        }

        private void SavePlSpecies(object o)
        {
            try
            {
                var NULL = Tbl72PlSpeciessesRepository.Tbl72PlSpeciesses.FirstOrDefault(x => x.NULL== CurrentNULL.NULL);
                if (CurrentNULL == null)
                {
                    MessageBox.Show(CultRes.StringsRes.DatasetNotExist);
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
                            RefExpertID= CurrentNULL.RefExpertID,              
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
                        Tbl72PlSpeciessesRepository.Save();
                        if (CurrentNULL != null)
                            MessageBox.Show(CurrentNULL.NULL+ " " + CultRes.StringsRes.SaveSuccess);
                        GetPlSpeciesByNameOrId(o);  //Refresh      
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
                            RefExpertID= CurrentTbl90RefAuthor.RefExpertID,
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
                            RefExpertID= CurrentTbl90RefSource.RefExpertID,
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
                            RefExpertID= CurrentTbl90RefExpert.RefExpertID,
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
                            RefExpertID= CurrentTbl93Comment.RefExpertID,                
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
                                                       where NULL.NULL== CurrentTbl90RefExpert.NULL
                                                       orderby NULL.NULL
                                                       select NULL));

            View = CollectionViewSource.GetDefaultView(NULLList);
            if (View != null)
                View.CurrentChanged += NULLView_CurrentChanged;
            RaisePropertyChanged("CurrentNULL");
            //-----------------------------------------------------------------------------------
            Tbl69FiSpeciessesList =
                new ObservableCollection<NULL>((from NULL in Tbl69FiSpeciessesRepository.Tbl69FiSpeciesses
                                                       where NULL.RefExpertID== CurrentTbl90RefExpert.RefExpertID
                                                       orderby NULL.Tbl90RefExperts.RefExpertName
                                                       select NULL));


            View = CollectionViewSource.GetDefaultView(Tbl69FiSpeciessesList);
            if (View != null)
                View.CurrentChanged += NULLView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl69FiSpeciesses");
            //-----------------------------------------------------------------------------------
            Tbl90RefAuthorsList =
                new ObservableCollection<Tbl90Reference>((from refAu in Tbl90ReferencesRepository.Tbl90References
                                                          where refAu.RefExpertID== CurrentTbl90RefExpert.RefExpertID
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
                                                          where refSo.RefExpertID== CurrentTbl90RefExpert.RefExpertID
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
                                                          where refEx.RefExpertID== CurrentTbl90RefExpert.RefExpertID
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
                                                          where comm.RefExpertID== CurrentTbl90RefExpert.RefExpertID
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



     
        #region "Public Properties Tbl90RefExpert"

        public new ICollectionView View;
        public new Tbl90RefExpert CurrentTbl90RefExpert
        {
            get
            {
                if (View != null)
                    return View.CurrentItem as Tbl90RefExpert;
                return null;
            }
        }
        //--------------------------------------------
        private string _searchRefExpertName;
        public new string SearchRefExpertName
        {
            get { return _searchRefExpertName; }
            set
            {
                if (value == _searchRefExpertName) return;
                _searchRefExpertName = value;
                RaisePropertyChanged("SearchRefExpertName");
            }
        }

        private ObservableCollection<Tbl90RefExpert> _tbl90RefExpertsList;
        public new ObservableCollection<Tbl90RefExpert> Tbl90RefExpertsList
        {
            get { return _tbl90RefExpertsList; }
            set
            {
                if (_tbl90RefExpertsList == value) return;
                _tbl90RefExpertsList = value;
                RaisePropertyChanged("Tbl90RefExpertsList");

                //Clear Search-TextBox
                SearchNULL = null;                                
                SearchNULL = null;
                SearchCommentInfo = null;
                SearchRefExpertName = null;
                SearchRefSourceName = null;
                SearchRefAuthorName = null;
            }
        }

        private ObservableCollection<Tbl90RefExpert> _tbl90RefExpertsAllList;
        public ObservableCollection<Tbl90RefExpert> Tbl90RefExpertsAllList
        {
            get { return _tbl90RefExpertsAllList; }
            set
            {
                if (_tbl90RefExpertsAllList == value) return;
                _tbl90RefExpertsAllList = value;
                RaisePropertyChanged("Tbl90RefExpertsAllList");
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
   
   

 //    Part 11    

      }
}   
