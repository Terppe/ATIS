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

//    Tbl68SpeciesgroupViewModel Skriptdatum:  15.03.2012  10:32    

namespace Atis.WpfUi.ViewModel
{   


    
    public class Tbl68SpeciesgroupsViewModel : NULLViewModel                     
    {     
         
      #region "Private Data Members"

        protected readonly Tbl69FiSpeciessesRepository Tbl69FiSpeciessesRepository = new Tbl69FiSpeciessesRepository();   
          
          #endregion "Private Data Members"            
    
        #region "Constructor"

        public Tbl68SpeciesgroupsViewModel()
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
       
        #region "Public Commands Basic Tbl68Speciesgroup"

        private RelayCommand _getSpeciesgroupByNameCommand;
        public new ICommand GetSpeciesgroupByNameCommand
        {
            get { return _getSpeciesgroupByNameCommand ?? (_getSpeciesgroupByNameCommand = new RelayCommand(GetSpeciesgroupByName)); }
        }

        private void GetSpeciesgroupByName()
        {   
Tbl68SpeciesgroupsList =
                 new ObservableCollection<Tbl68Speciesgroup>((from x in Tbl68SpeciesgroupsRepository.Tbl68Speciesgroups
                                                        where x.SpeciesgroupName.StartsWith(SearchSpeciesgroupName)
                                                        orderby x.SpeciesgroupName
                                                        select x));

            Tbl68SpeciesgroupsAllList =
                 new ObservableCollection<Tbl68Speciesgroup>((from y in Tbl68SpeciesgroupsRepository.Tbl68Speciesgroups
                                                        orderby y.SpeciesgroupName
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
             
  View = CollectionViewSource.GetDefaultView(Tbl68SpeciesgroupsList);
            if (View != null)
                View.CurrentChanged += tbl68SpeciesgroupView_CurrentChanged;                   
            RaisePropertyChanged("CurrentTbl68Speciesgroup");
        }
        //------------------------------------------------------------------------------
        private RelayCommand _addSpeciesgroupCommand;
        public new ICommand AddSpeciesgroupCommand
        {
            get { return _addSpeciesgroupCommand ?? (_addSpeciesgroupCommand = new RelayCommand(AddSpeciesgroup)); }
        }

        private void AddSpeciesgroup()
        {
            if (Tbl68SpeciesgroupsList == null)
                Tbl68SpeciesgroupsList = new ObservableCollection<Tbl68Speciesgroup>();
            Tbl68SpeciesgroupsList.Add(new Tbl68Speciesgroup{ SpeciesgroupName= "New " });
            View = CollectionViewSource.GetDefaultView(Tbl68SpeciesgroupsList);
            if (View != null)
                View.CurrentChanged += tbl68SpeciesgroupView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl68Speciesgroup");
        }
        //---------------------------------------------------------------------------------------
  
       
        private RelayCommand _deleteSpeciesgroupCommand;
        public new ICommand DeleteSpeciesgroupCommand
        {
            get { return _deleteSpeciesgroupCommand ?? (_deleteSpeciesgroupCommand = new RelayCommand(DeleteSpeciesgroup)); }
        }

        private void DeleteSpeciesgroup()
        {
            try
            {
                var speciesgroup= Tbl68SpeciesgroupsRepository.Tbl68Speciesgroups.FirstOrDefault(x => x.SpeciesgroupID== CurrentTbl68Speciesgroup.SpeciesgroupID);
                if (speciesgroup!= null)
                {
                    if (MessageBox.Show("Are you really shure to delete this "
                                        + CurrentTbl68Speciesgroup.SpeciesgroupName, "Question", MessageBoxButton.YesNo) !=
                        MessageBoxResult.Yes) return;
                    Tbl68SpeciesgroupsRepository.Delete(speciesgroup);
                    Tbl68SpeciesgroupsRepository.Save();
                    MessageBox.Show(CurrentTbl68Speciesgroup.SpeciesgroupName + " was deleted successfully");
                    GetSpeciesgroupByName(); //Refresh
                }
                else
                {
                    MessageBox.Show("Only " + CurrentTbl68Speciesgroup.SpeciesgroupName+ " can be deleted");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //-------------------------------------------------------------------------------------------------
        private RelayCommand _saveSpeciesgroupCommand;
        public new ICommand SaveSpeciesgroupCommand
        {
            get { return _saveSpeciesgroupCommand ?? (_saveSpeciesgroupCommand = new RelayCommand(SaveSpeciesgroup)); }
        }

        private void SaveSpeciesgroup()
        {
            try
            {
                var speciesgroup= Tbl68SpeciesgroupsRepository.Tbl68Speciesgroups.FirstOrDefault(x => x.SpeciesgroupID== CurrentTbl68Speciesgroup.SpeciesgroupID);
                if (CurrentTbl68Speciesgroup == null)
                {
                    MessageBox.Show("speciesgroup was not found");
                }
                else
                {
                    if (CurrentTbl68Speciesgroup.SpeciesgroupID!= 0)
                    {
                        if (speciesgroup!= null) //update
                        {
                            speciesgroup.Updater = Environment.UserName;
                            speciesgroup.UpdaterDate = DateTime.Now;
                        }
                    }
                    else
                    {
                        Tbl68SpeciesgroupsRepository.Add(new Tbl68Speciesgroup
                        {
                            NULL= CurrentTbl68Speciesgroup.NULL,              
                            SpeciesgroupName= CurrentTbl68Speciesgroup.SpeciesgroupName,              
                            CountID = TblCountersRepository.Counter(),
                            Valid = CurrentTbl68Speciesgroup.Valid,
                            ValidYear = CurrentTbl68Speciesgroup.ValidYear,
                            Synonym = CurrentTbl68Speciesgroup.Synonym,
                            Author = CurrentTbl68Speciesgroup.Author,
                            AuthorYear = CurrentTbl68Speciesgroup.AuthorYear,
                            Info = CurrentTbl68Speciesgroup.Info,
                            EngName = CurrentTbl68Speciesgroup.EngName,
                            GerName = CurrentTbl68Speciesgroup.GerName,
                            FraName = CurrentTbl68Speciesgroup.FraName,
                            PorName = CurrentTbl68Speciesgroup.PorName,
                            Writer = Environment.UserName,
                            WriterDate = DateTime.Now,
                            Updater = Environment.UserName,
                            UpdaterDate = DateTime.Now,
                            Memo = CurrentTbl68Speciesgroup.Memo
                        });
                    }
                    {
                        Tbl68SpeciesgroupsRepository.Save();
                        MessageBox.Show(CurrentTbl68Speciesgroup.SpeciesgroupName+  " was successfully saved ");
                        GetSpeciesgroupByName();  //Refresh
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion "Public Commands"
  

        
        #region "Public Commands Connect ==> Tbl69FiSpecies"                 

        private RelayCommand _getFiSpeciesByNameCommand;
        public ICommand GetFiSpeciesByNameCommand
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
        public ICommand AddFiSpeciesCommand
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
                var fispecies = Tbl69FiSpeciessesRepository.Tbl69FiSpeciesses.FirstOrDefault(x => x.FiSpeciesID== CurrentTbl69FiSpecies.FiSpeciesID);
                if (fispecies != null)
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
        public ICommand SaveFiSpeciesCommand
        {
            get { return _saveFiSpeciesCommand ?? (_saveFiSpeciesCommand = new RelayCommand(SaveFiSpecies)); }
        }

        private void SaveFiSpecies()
        {
            try
            {
                var fispecies = Tbl69FiSpeciessesRepository.Tbl69FiSpeciesses.FirstOrDefault(x => x.FiSpeciesID== CurrentTbl69FiSpecies.FiSpeciesID);
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
                        Tbl69FiSpeciessesRepository.Add(new Tbl69FiSpecies
                        {
                            SpeciesgroupID= CurrentTbl69FiSpecies.SpeciesgroupID,              
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
    
  
        
        #region "Public Commands Connect ==> Tbl72PlSpecies"                 

        private RelayCommand _getPlSpeciesByNameCommand;
        public ICommand GetPlSpeciesByNameCommand
        {
            get { return _getPlSpeciesByNameCommand ?? (_getPlSpeciesByNameCommand = new RelayCommand(GetPlSpeciesByName)); }
        }

        private void GetPlSpeciesByName()
        {
            Tbl72PlSpeciessesList =
                new ObservableCollection<Tbl72PlSpecies>((from plspecies in Tbl72PlSpeciessesRepository.Tbl72PlSpeciesses
                                                       where plspecies.PlSpeciesName.StartsWith(SearchPlSpeciesName)
                                                       orderby plspecies.PlSpeciesName
                                                       select plspecies));

            View = CollectionViewSource.GetDefaultView(Tbl72PlSpeciessesList);
            if (View != null)
                View.CurrentChanged += tbl72PlSpeciesView_CurrentChanged;                   
            RaisePropertyChanged("CurrentTbl72PlSpecies");
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
                Tbl72PlSpeciessesList = new ObservableCollection<Tbl72PlSpecies>();
            Tbl72PlSpeciessesList.Add(new Tbl72PlSpecies{ PlSpeciesName= "New " });                   
            View = CollectionViewSource.GetDefaultView(Tbl72PlSpeciessesList);
            if (View != null)
                View.CurrentChanged += tbl72PlSpeciesView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl72PlSpecies");
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
                var plspecies= Tbl72PlSpeciessesRepository.Tbl72PlSpeciesses.FirstOrDefault(x => x.PlSpeciesID== CurrentTbl72PlSpecies.PlSpeciesID);
                if (plspecies!= null)
                {
                    if (MessageBox.Show("Are you really shure to delete this "
                                        + CurrentTbl72PlSpecies.PlSpeciesName, "Question", MessageBoxButton.YesNo) !=
                        MessageBoxResult.Yes) return;
                    Tbl72PlSpeciessesRepository.Delete(plspecies);
                    Tbl72PlSpeciessesRepository.Save();
                    MessageBox.Show(CurrentTbl72PlSpecies.PlSpeciesName+ " was deleted successfully");
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
                    MessageBox.Show("Only " + CurrentTbl72PlSpecies.PlSpeciesName+ " can be deleted");
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
                var plspecies= Tbl72PlSpeciessesRepository.Tbl72PlSpeciesses.FirstOrDefault(x => x.PlSpeciesID== CurrentTbl72PlSpecies.PlSpeciesID);
                if (CurrentTbl72PlSpecies == null)
                {
                    MessageBox.Show("plspecies was not found");
                }
                else
                {
                    if (CurrentTbl72PlSpecies.PlSpeciesID!= 0)
                    {
                        if (plspecies!= null) //update
                        {
                            plspecies.Updater = Environment.UserName;
                            plspecies.UpdaterDate = DateTime.Now;
                        }
                    }
                    else
                    {
                        Tbl72PlSpeciessesRepository.Add(new Tbl72PlSpecies
                        {
                            SpeciesgroupID= CurrentTbl72PlSpecies.SpeciesgroupID,              
                            PlSpeciesName= CurrentTbl72PlSpecies.PlSpeciesName,              
                            CountID = TblCountersRepository.Counter(),
                            Valid = CurrentTbl72PlSpecies.Valid,
                            ValidYear = CurrentTbl72PlSpecies.ValidYear,
                            Synonym = CurrentTbl72PlSpecies.Synonym,
                            Author = CurrentTbl72PlSpecies.Author,
                            AuthorYear = CurrentTbl72PlSpecies.AuthorYear,
                            Info = CurrentTbl72PlSpecies.Info,
                            EngName = CurrentTbl72PlSpecies.EngName,
                            GerName = CurrentTbl72PlSpecies.GerName,
                            FraName = CurrentTbl72PlSpecies.FraName,
                            PorName = CurrentTbl72PlSpecies.PorName,
                            Writer = Environment.UserName,
                            WriterDate = DateTime.Now,
                            Updater = Environment.UserName,
                            UpdaterDate = DateTime.Now,
                            Memo = CurrentTbl72PlSpecies.Memo
                        });
                    }
                    {
                        Tbl72PlSpeciessesRepository.Save();
                        MessageBox.Show(CurrentTbl72PlSpecies.PlSpeciesName+  " was successfully saved ");              
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
                            SpeciesgroupID= CurrentTbl90RefAuthor.SpeciesgroupID,
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
                            SpeciesgroupID= CurrentTbl90RefSource.SpeciesgroupID,
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
                            SpeciesgroupID= CurrentTbl90RefExpert.SpeciesgroupID,
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
                            SpeciesgroupID= CurrentTbl93Comment.SpeciesgroupID,                
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
                                                       where NULL.NULL== CurrentTbl68Speciesgroup.NULL
                                                       orderby NULL.NULL
                                                       select NULL));

            View = CollectionViewSource.GetDefaultView(NULLList);
            if (View != null)
                View.CurrentChanged += NULLView_CurrentChanged;
            RaisePropertyChanged("CurrentNULL");
            //-----------------------------------------------------------------------------------
            Tbl69FiSpeciessesList =
                new ObservableCollection<Tbl69FiSpecies>((from fispecies in Tbl69FiSpeciessesRepository.Tbl69FiSpeciesses
                                                       where fispecies.SpeciesgroupID== CurrentTbl68Speciesgroup.SpeciesgroupID
                                                       orderby fispecies.Tbl68Speciesgroups.SpeciesgroupName
                                                       select fispecies));


            View = CollectionViewSource.GetDefaultView(Tbl69FiSpeciessesList);
            if (View != null)
                View.CurrentChanged += tbl69FiSpeciesView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl69FiSpeciesses");
            //-----------------------------------------------------------------------------------
            Tbl90RefAuthorsList =
                new ObservableCollection<Tbl90Reference>((from refAu in Tbl90ReferencesRepository.Tbl90References
                                                          where refAu.SpeciesgroupID== CurrentTbl68Speciesgroup.SpeciesgroupID
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
                                                          where refSo.SpeciesgroupID== CurrentTbl68Speciesgroup.SpeciesgroupID
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
                                                          where refEx.SpeciesgroupID== CurrentTbl68Speciesgroup.SpeciesgroupID
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
                                                          where comm.SpeciesgroupID== CurrentTbl68Speciesgroup.SpeciesgroupID
                                                        orderby comm.Info
                                                        select comm));

            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            if (CommentsView != null)
                CommentsView.CurrentChanged += tbl93CommentView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl93Comment");
            //--------------------------------------------------------------

        }

        #endregion "Public Commands Connected Tables by DoubleClick"
   

     
        #region "Public Properties Tbl68Speciesgroup"

        public new ICollectionView View;
        public new Tbl68Speciesgroup CurrentTbl68Speciesgroup
        {
            get
            {
                if (View != null)
                    return View.CurrentItem as Tbl68Speciesgroup;
                return null;
            }
        }
        //--------------------------------------------
        private string _searchSpeciesgroupName;
        public new string SearchSpeciesgroupName
        {
            get { return _searchSpeciesgroupName; }
            set
            {
                if (value == _searchSpeciesgroupName) return;
                _searchSpeciesgroupName = value;
                RaisePropertyChanged("SearchSpeciesgroupName");
            }
        }

        private ObservableCollection<Tbl68Speciesgroup> _tbl68SpeciesgroupsList;
        public new ObservableCollection<Tbl68Speciesgroup> Tbl68SpeciesgroupsList
        {
            get { return _tbl68SpeciesgroupsList; }
            set
            {
                if (_tbl68SpeciesgroupsList == value) return;
                _tbl68SpeciesgroupsList = value;
                RaisePropertyChanged("Tbl68SpeciesgroupsList");

                //Clear Search-TextBox
                SearchNULL = null;                                
                SearchFiSpeciesName = null;
                SearchCommentInfo = null;
                SearchRefExpertName = null;
                SearchRefSourceName = null;
                SearchRefAuthorName = null;
            }
        }

        private ObservableCollection<Tbl68Speciesgroup> _tbl68SpeciesgroupsAllList;
        public ObservableCollection<Tbl68Speciesgroup> Tbl68SpeciesgroupsAllList
        {
            get { return _tbl68SpeciesgroupsAllList; }
            set
            {
                if (_tbl68SpeciesgroupsAllList == value) return;
                _tbl68SpeciesgroupsAllList = value;
                RaisePropertyChanged("Tbl68SpeciesgroupsAllList");
            }
        }

        #endregion "Public Properties"
   

       
        #region "Public Properties Tbl69FiSpecies"

        public ICollectionView View;
        public Tbl69FiSpecies CurrentTbl69FiSpecies
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
        public string SearchFiSpeciesName
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
        public ObservableCollection<Tbl69FiSpecies> Tbl69FiSpeciessesList
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

        public ICollectionView View;
        public Tbl72PlSpecies CurrentTbl72PlSpecies
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
        public string SearchPlSpeciesName
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
        public ObservableCollection<Tbl72PlSpecies> Tbl72PlSpeciessesList
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
   
          
        #region "Private Methods"

        public void tbl69FiSpeciesView_CurrentChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged("CurrentTbl69FiSpecies");
        }
        #endregion "Private Methods"
   
   

   
    }
}   
