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

//    Tbl66GenussesViewModel Skriptdatum:  01.04.2014  10:32    

namespace WPFUI.Views.Database
{   


    
    public class Tbl66GenussesViewModel : Tbl63InfratribussesViewModel                     
    {     
        
        #region "Private Data Members"
 
        protected readonly Tbl68SpeciesgroupsRepository Tbl68SpeciesgroupsRepository = new Tbl68SpeciesgroupsRepository();    
           
        protected readonly Tbl69FiSpeciessesRepository Tbl69FiSpeciessesRepository = new Tbl69FiSpeciessesRepository();   
           
        protected readonly Tbl72PlSpeciessesRepository Tbl72PlSpeciessesRepository = new Tbl72PlSpeciessesRepository();   
          
          #endregion "Private Data Members"            
    
        #region "Constructor"

        public Tbl66GenussesViewModel()
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

       
        #region "Public Commands Basic Tbl66Genus"

        private RelayCommand _getGenusByNameCommand;
        public new ICommand GetGenusByNameCommand
        {
            get { return _getGenusByNameCommand ?? (_getGenusByNameCommand = new RelayCommand(delegate { GetGenusByNameOrId(null); })); }   
        }

        private void GetGenusByNameOrId(object o)       
        {   
   
            Tbl68SpeciesgroupsAllList =
                 new ObservableCollection<Tbl68Speciesgroup>((from v in Tbl68SpeciesgroupsRepository.Tbl68Speciesgroups
                                                       orderby v.SpeciesgroupName, v.Subspeciesgroup
                                                       select v));

            Tbl66GenussesList =  new ObservableCollection<Tbl66Genus>
                                                       (from x in Tbl66GenussesRepository.Tbl66Genusses
                                                        where x.GenusName.StartsWith(SearchGenusName)
                                                        orderby x.GenusName
                                                        select x);

            Tbl66GenussesAllList =  new ObservableCollection<Tbl66Genus>
                                                       (from y in Tbl66GenussesRepository.Tbl66Genusses
                                                        orderby y.GenusName
                                                        select y);

            Tbl63InfratribussesAllList =  new ObservableCollection<Tbl63Infratribus>
                                                       (from z in Tbl63InfratribussesRepository.Tbl63Infratribusses
                                                        orderby z.InfratribusName, z.Subregnum
                                                        select z);

            Tbl60SubtribussesAllList =   new ObservableCollection<Tbl60Subtribus>
                                                       (from z in Tbl60SubtribussesRepository.Tbl60Subtribusses
                                                        orderby z.SubtribusName, z.Subregnum
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
                Tbl72PlSpeciessesList = null; 
                      
GenussesView = CollectionViewSource.GetDefaultView(Tbl66GenussesList);
            if (GenussesView != null)
                GenussesView.CurrentChanged += tbl66GenusView_CurrentChanged;                   
            RaisePropertyChanged();
        }
        //------------------------------------------------------------------------------
        private RelayCommand _addGenusCommand;
        public new ICommand AddGenusCommand
        {
            get { return _addGenusCommand ?? (_addGenusCommand = new RelayCommand(AddGenus)); }
        }

        private void AddGenus(object o)
        {
            if (Tbl66GenussesList == null)
                Tbl66GenussesList = new ObservableCollection<Tbl66Genus>();
            Tbl66GenussesList.Add(new Tbl66Genus{ GenusName= CultRes.StringsRes.DatasetNew });
            GenussesView = CollectionViewSource.GetDefaultView(Tbl66GenussesList);
            if (GenussesView != null)
                GenussesView.CurrentChanged += tbl66GenusView_CurrentChanged;
            RaisePropertyChanged();
        }
        //---------------------------------------------------------------------------------------
  
       
        private RelayCommand _deleteGenusCommand;
        public new ICommand DeleteGenusCommand
        {
            get { return _deleteGenusCommand ?? (_deleteGenusCommand = new RelayCommand(delegate { DeleteGenus(null); })); }
        }

        private void DeleteGenus(object o)
        {
            try
            {
                var genus= Tbl66GenussesRepository.Tbl66Genusses.FirstOrDefault(x => x.GenusID== CurrentTbl66Genus.GenusID);
                if (genus!= null)
                {
                    if (MessageBox.Show(CultRes.StringsRes.DeleteQuestion
                                        + " " +  CurrentTbl66Genus.GenusName, CultRes.StringsRes.DeleteQuestion1, MessageBoxButton.YesNo) !=
                        MessageBoxResult.Yes) return;
                    Tbl66GenussesRepository.Delete(genus);
                    Tbl66GenussesRepository.Save();
                    MessageBox.Show(CurrentTbl66Genus.GenusName + " " + CultRes.StringsRes.DeleteSuccess);
                    GetGenusByNameOrId(o); //Refresh
                }
                else
                {
                    MessageBox.Show(CultRes.StringsRes.DeleteCan + " " + CurrentTbl66Genus.GenusName+ " " + CultRes.StringsRes.DeleteCan1);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //-------------------------------------------------------------------------------------------------
        private RelayCommand _saveGenusCommand;
        public new ICommand SaveGenusCommand
        {
            get { return _saveGenusCommand ?? (_saveGenusCommand = new RelayCommand(delegate { SaveGenus(null); })); }
        }

        private void SaveGenus(object o)
        {
            try
            {
                var genus= Tbl66GenussesRepository.Tbl66Genusses.FirstOrDefault(x => x.GenusID== CurrentTbl66Genus.GenusID);
                if (CurrentTbl66Genus == null)
                {
                    MessageBox.Show(CultRes.StringsRes.DatasetNotExist);
                }
                else
                {
                    if (CurrentTbl66Genus.GenusID!= 0)
                    {
                        if (genus!= null) //update
                        {
                            genus.Updater = Environment.UserName;
                            genus.UpdaterDate = DateTime.Now;
                        }
                    }
                    else
                    {
                        Tbl66GenussesRepository.Add(new Tbl66Genus
                        {
                            InfratribusID= CurrentTbl66Genus.InfratribusID,              
                            GenusName= CurrentTbl66Genus.GenusName,              
                            CountID = RandomHelper.Randomnumber(),
                            Valid = CurrentTbl66Genus.Valid,
                            ValidYear = CurrentTbl66Genus.ValidYear,
                            Synonym = CurrentTbl66Genus.Synonym,
                            Author = CurrentTbl66Genus.Author,
                            AuthorYear = CurrentTbl66Genus.AuthorYear,
                            Info = CurrentTbl66Genus.Info,
                            EngName = CurrentTbl66Genus.EngName,
                            GerName = CurrentTbl66Genus.GerName,
                            FraName = CurrentTbl66Genus.FraName,
                            PorName = CurrentTbl66Genus.PorName,
                            Writer = Environment.UserName,
                            WriterDate = DateTime.Now,
                            Updater = Environment.UserName,
                            UpdaterDate = DateTime.Now,
                            Memo = CurrentTbl66Genus.Memo
                        });
                    }
                    {
                        Tbl66GenussesRepository.Save();
                        MessageBox.Show(CurrentTbl66Genus.GenusName+ " " + CultRes.StringsRes.SaveSuccess);
                        GetGenusByNameOrId(o);  //Refresh
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


        
        #region "Public Commands Connect <== Tbl63Infratribus"                 

        private RelayCommand _getInfratribusByNameCommand;
        public new ICommand GetInfratribusByNameCommand
        {
            get { return _getInfratribusByNameCommand ?? (_getInfratribusByNameCommand = new RelayCommand(delegate { GetInfratribusByNameOrId(null); })); }
        }

        private void GetInfratribusByNameOrId(object o)
        {
            Tbl63InfratribussesList =
                new ObservableCollection<Tbl63Infratribus>((from infratribus in Tbl63InfratribussesRepository.Tbl63Infratribusses
                                                       where infratribus.InfratribusName.StartsWith(SearchInfratribusName)
                                                       orderby infratribus.InfratribusName
                                                       select infratribus));

            InfratribussesView = CollectionViewSource.GetDefaultView(Tbl63InfratribussesList);
            if (InfratribussesView != null)
                InfratribussesView.CurrentChanged += tbl63InfratribusView_CurrentChanged;                   
            RaisePropertyChanged();
        }
        //------------------------------------------------------------------------------
        private RelayCommand _addInfratribusCommand;
        public new ICommand AddInfratribusCommand
        {
            get { return _addInfratribusCommand ?? (_addInfratribusCommand = new RelayCommand(AddInfratribus)); }
        }

        private void AddInfratribus()
        {
            if (Tbl63InfratribussesList == null)
                Tbl63InfratribussesList = new ObservableCollection<Tbl63Infratribus>();
            Tbl63InfratribussesList.Add(new Tbl63Infratribus{ InfratribusName= "New " });                   
            InfratribussesView = CollectionViewSource.GetDefaultView(Tbl63InfratribussesList);
            if (InfratribussesView != null)
                InfratribussesView.CurrentChanged += tbl63InfratribusView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl63Infratribus");
        }
        //----------------------------------------------------------------------------------------------------------
        private RelayCommand _deleteInfratribusCommand;
        public ICommand InfratribusPhylumCommand
        {
            get { return _deleteInfratribusCommand ?? (_deleteInfratribusCommand = new RelayCommand(DeleteInfratribus)); }
        }

        private void DeleteInfratribus()
        {
            try
            {
                var infratribus= Tbl63InfratribussesRepository.Tbl63Infratribusses.FirstOrDefault(x => x.InfratribusID== CurrentTbl63Infratribus.InfratribusID);
                if (infratribus!= null)
                {
                    if (MessageBox.Show("Are you really shure to delete this "
                                        + CurrentTbl63Infratribus.InfratribusName, "Question", MessageBoxButton.YesNo) !=
                        MessageBoxResult.Yes) return;
                    Tbl63InfratribussesRepository.Delete(infratribus);
                    Tbl63InfratribussesRepository.Save();
                    MessageBox.Show(CurrentTbl63Infratribus.InfratribusName+ " was deleted successfully");
                    if (SearchInfratribusName == null)
                    {
                        GetConnectedTablesById(); //refresh doubleClick                    
                    }
                    else
                    {
                        GetInfratribusByName(); //search
                    }
                }
                else
                {
                    MessageBox.Show("Only " + CurrentTbl63Infratribus.InfratribusName+ " can be deleted");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //-------------------------------------------------------------------------------------------------
        private RelayCommand _saveInfratribusCommand;   
        public new ICommand SaveInfratribusCommand
        {
            get { return _saveInfratribusCommand ?? (_saveInfratribusCommand = new RelayCommand(SaveInfratribus)); }
        }

        private void SaveInfratribus()
        {
            try
            {
                var infratribus= Tbl63InfratribussesRepository.Tbl63Infratribusses.FirstOrDefault(x => x.InfratribusID== CurrentTbl63Infratribus.InfratribusID);
                if (CurrentTbl63Infratribus == null)
                {
                    MessageBox.Show("infratribus was not found");
                }
                else
                {
                    if (CurrentTbl63Infratribus.InfratribusID!= 0)
                    {
                        if (infratribus!= null) //update
                        {
                            infratribus.Updater = Environment.UserName;
                            infratribus.UpdaterDate = DateTime.Now;
                        }
                    }
                    else
                    {
                        Tbl63InfratribussesRepository.Add(new Tbl63Infratribus()
                        {
                            InfratribusName= CurrentTbl63Infratribus.InfratribusName,              
                            CountID = TblCountersRepository.Counter(),
                            Valid = CurrentTbl63Infratribus.Valid,
                            ValidYear = CurrentTbl63Infratribus.ValidYear,
                            Synonym = CurrentTbl63Infratribus.Synonym,
                            Author = CurrentTbl63Infratribus.Author,
                            AuthorYear = CurrentTbl63Infratribus.AuthorYear,
                            Info = CurrentTbl63Infratribus.Info,
                            EngName = CurrentTbl63Infratribus.EngName,
                            GerName = CurrentTbl63Infratribus.GerName,
                            FraName = CurrentTbl63Infratribus.FraName,
                            PorName = CurrentTbl63Infratribus.PorName,
                            Writer = Environment.UserName,
                            WriterDate = DateTime.Now,
                            Updater = Environment.UserName,
                            UpdaterDate = DateTime.Now,
                            Memo = CurrentTbl63Infratribus.Memo
                        });
                        }
                        {
                            Tbl63InfratribussesRepository.Save();
                            MessageBox.Show(CurrentTbl63Infratribus.InfratribusName+  " was successfully saved ");
                            GetInfratribusByName();  //Refresh
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


        
        #region "Public Commands Connect ==> Tbl69FiSpecies"                 

        private RelayCommand _getFiSpeciesByNameCommand;
        public ICommand GetFiSpeciesByNameCommand
        {
            get { return _getFiSpeciesByNameCommand ?? (_getFiSpeciesByNameCommand = new RelayCommand(delegate { GetFiSpeciesByNameOrId(null); })); }
        }

        private void GetFiSpeciesByNameOrId(object o)
        {
            Tbl69FiSpeciessesList =
                new ObservableCollection<Tbl69FiSpecies>((from fispecies in Tbl69FiSpeciessesRepository.Tbl69FiSpeciesses
                                                       where fispecies.FiSpeciesName.StartsWith(SearchFiSpeciesName)
                                                       orderby fispecies.FiSpeciesName
                                                       select fispecies));

            FiSpeciessesView = CollectionViewSource.GetDefaultView(Tbl69FiSpeciessesList);
            if (FiSpeciessesView != null)
                FiSpeciessesView.CurrentChanged += tbl69FiSpeciesView_CurrentChanged;                   
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
                Tbl69FiSpeciessesList = new ObservableCollection<Tbl69FiSpecies>();
            Tbl69FiSpeciessesList.Add(new Tbl69FiSpecies{ FiSpeciesName= CultRes.StringsRes.DatasetNew });                   
            FiSpeciessesView = CollectionViewSource.GetDefaultView(Tbl69FiSpeciessesList);
            if (FiSpeciessesView != null)
                FiSpeciessesView.CurrentChanged += tbl69FiSpeciesView_CurrentChanged;
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
                var fispecies = Tbl69FiSpeciessesRepository.Tbl69FiSpeciesses.FirstOrDefault(x => x.FiSpeciesID== CurrentTbl69FiSpecies.FiSpeciesID);
                if (fispecies != null)
                {
                    if (MessageBox.Show(CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl69FiSpecies.FiSpeciesName, CultRes.StringsRes.DeleteQuestion1, MessageBoxButton.YesNo) !=
                        MessageBoxResult.Yes) return;
                    Tbl69FiSpeciessesRepository.Delete(fispecies);
                    Tbl69FiSpeciessesRepository.Save();
                    MessageBox.Show(CurrentTbl69FiSpecies.FiSpeciesName + " " + CultRes.StringsRes.DeleteSuccess);
                    GetFiSpeciesByNameOrId(o);  //Refresh
                }
                else
                {
                    MessageBox.Show(CultRes.StringsRes.DeleteCan + " " + CurrentTbl69FiSpecies.FiSpeciesName+ " " + CultRes.StringsRes.DeleteCan1);
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
                var fispecies = Tbl69FiSpeciessesRepository.Tbl69FiSpeciesses.FirstOrDefault(x => x.FiSpeciesID== CurrentTbl69FiSpecies.FiSpeciesID);
                if (CurrentTbl69FiSpecies == null)
                {
                    MessageBox.Show(CultRes.StringsRes.DatasetNotExist);
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
                            GenusID= CurrentTbl69FiSpecies.GenusID,              
                            FiSpeciesName= CurrentTbl69FiSpecies.FiSpeciesName,              
                            CountID = RandomHelper.Randomnumber(),
                            Valid = CurrentTbl69FiSpecies.Valid,
                            ValidYear = CurrentTbl69FiSpecies.ValidYear,
                            Author = CurrentTbl69FiSpecies.Author,
                            AuthorYear = CurrentTbl69FiSpecies.AuthorYear,
                            BasinLength = CurrentTbl69FiSpecies.BasinLength,
                            Writer = Environment.UserName,
                            WriterDate = DateTime.Now,
                            Updater = Environment.UserName,
                            UpdaterDate = DateTime.Now,
                            MemoBreeding = CurrentTbl69FiSpecies.MemoBreeding
                        });
                    }
                    {
                        Tbl69FiSpeciessesRepository.Save();
                        MessageBox.Show(CurrentTbl69FiSpecies.FiSpeciesName+ " " + CultRes.StringsRes.SaveSuccess);
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


        
        #region "Public Commands Connect ==> Tbl72PlSpecies"                 

        private RelayCommand _getPlSpeciesByNameCommand;
        public ICommand GetPlSpeciesByNameCommand
        {
            get { return _getPlSpeciesByNameCommand ?? (_getPlSpeciesByNameCommand = new RelayCommand(delegate { GetPlSpeciesByNameOrId(null); })); }
        }

        private void GetPlSpeciesByName(object o)
        {
            Tbl72PlSpeciessesList =   new ObservableCollection<Tbl72PlSpecies>
                                                      (from x  in Tbl72PlSpeciessesRepository.FindAll()
                                                       where x.PlSpeciesName.StartsWith(SearchPlSpeciesName)
                                                       orderby x.PlSpeciesName
                                                       select x);

            PlSpeciessesView = CollectionViewSource.GetDefaultView(Tbl72PlSpeciessesList);
            if (PlSpeciessesView != null)
                PlSpeciessesView.CurrentChanged += tbl72PlSpeciesView_CurrentChanged;                   
            RaisePropertyChanged();
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
            PlSpeciessesView = CollectionViewSource.GetDefaultView(Tbl72PlSpeciessesList);
            if (PlSpeciessesView != null)
                PlSpeciessesView.CurrentChanged += tbl72PlSpeciesView_CurrentChanged;
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
                            GenusID= CurrentTbl72PlSpecies.GenusID,              
                            PlSpeciesName= CurrentTbl72PlSpecies.PlSpeciesName,              
                            CountID = TblCountersRepository.Counter(),
                            Valid = CurrentTbl72PlSpecies.Valid,
                            ValidYear = CurrentTbl72PlSpecies.ValidYear,
                            Author = CurrentTbl72PlSpecies.Author,
                            AuthorYear = CurrentTbl72PlSpecies.AuthorYear,
                            BasinHeight = CurrentTbl72PlSpecies.BasinHeight,
                            Writer = Environment.UserName,
                            WriterDate = DateTime.Now,
                            Updater = Environment.UserName,
                            UpdaterDate = DateTime.Now,
                            MemoBuilt = CurrentTbl72PlSpecies.MemoBuilt
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
                            GenusID= CurrentTbl90RefAuthor.GenusID,
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
                            GenusID= CurrentTbl90RefSource.GenusID,
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
                            GenusID= CurrentTbl90RefExpert.GenusID,
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
                            GenusID= CurrentTbl93Comment.GenusID,                
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
            SearchInfratribusName = null;
            SearchFiSpeciesName = null;
            SearchPlSpeciesName = null;
            SearchCommentInfo = null;
            SearchRefExpertName = null;
            SearchRefSourceName = null;
            SearchRefAuthorName = null;

            Tbl63InfratribussesList =
                new ObservableCollection<Tbl63Infratribus>((from infratribus in Tbl63InfratribussesRepository.Tbl63Infratribusses
                                                            where infratribus.InfratribusID == CurrentTbl66Genus.InfratribusID
                                                            orderby infratribus.InfratribusName
                                                            select infratribus));

            InfratribussesView = CollectionViewSource.GetDefaultView(Tbl63InfratribussesList);
            if (InfratribussesView != null)
                InfratribussesView.CurrentChanged += tbl63InfratribusView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl63Infratribusses");
            //-----------------------------------------------------------------------------------
            Tbl69FiSpeciessesList =
                new ObservableCollection<Tbl69FiSpecies>((from x in Tbl69FiSpeciessesRepository.Tbl69FiSpeciesses
                                                       where x.GenusID == CurrentTbl66Genus.GenusID
                                                       orderby  x.FiSpeciesName, x.Subspecies, x.Divers
                                                       select x));

            FiSpeciessesView = CollectionViewSource.GetDefaultView(Tbl69FiSpeciessesList);
            if (FiSpeciessesView != null)
                FiSpeciessesView .CurrentChanged += tbl69FiSpeciesView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl69FiSpeciesses");
            //-----------------------------------------------------------------------------------
            Tbl72PlSpeciessesList =
                new ObservableCollection<Tbl72PlSpecies>((from y in Tbl72PlSpeciessesRepository.Tbl72PlSpeciesses
                                                       where y.GenusID == CurrentTbl66Genus.GenusID
                                                       orderby  y.PlSpeciesName, y.Subspecies, y.Divers
                                                       select y));

            PlSpeciessesView = CollectionViewSource.GetDefaultView(Tbl72PlSpeciessesList);
            if (PlSpeciessesView != null)
                PlSpeciessesView .CurrentChanged += tbl72PlSpeciesView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl72PlSpeciesses");
            //-----------------------------------------------------------------------------------
            Tbl90RefAuthorsList =
                new ObservableCollection<Tbl90Reference>((from refAu in Tbl90ReferencesRepository.Tbl90References
                                                          where refAu.GenusID == CurrentTbl66Genus.GenusID
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
                                                          where refSo.GenusID == CurrentTbl66Genus.GenusID
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
                                                          where refEx.GenusID == CurrentTbl66Genus.GenusID
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
                                                        where comm .GenusID == CurrentTbl66Genus.GenusID
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



     
        #region "Public Properties Tbl66Genus"

        public new ICollectionView GenussesView;
        public new Tbl66Genus CurrentTbl66Genus
        {
            get
            {
                if (GenussesView != null)
                    return GenussesView.CurrentItem as Tbl66Genus;
                return null;
            }
        }
        //--------------------------------------------
        private string _searchGenusName;
        public new string SearchGenusName
        {
            get { return _searchGenusName; }
            set
            {
                if (value == _searchGenusName) return;
                _searchGenusName = value;
                RaisePropertyChanged("SearchGenusName");
            }
        }

        private ObservableCollection<Tbl66Genus> _tbl66GenussesList;
        public new ObservableCollection<Tbl66Genus> Tbl66GenussesList
        {
            get { return _tbl66GenussesList; }
            set
            {
                if (_tbl66GenussesList == value) return;
                _tbl66GenussesList = value;
                RaisePropertyChanged("Tbl66GenussesList");

                //Clear Search-TextBox
                SearchInfratribusName = null;                                
                SearchFiSpeciesName = null;
                SearchPlSpeciesName = null;                                
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

        private ObservableCollection<Tbl66Genus> _tbl66GenussesAllList;
        public ObservableCollection<Tbl66Genus> Tbl66GenussesAllList
        {
            get { return _tbl66GenussesAllList; }
            set
            {
                if (_tbl66GenussesAllList == value) return;
                _tbl66GenussesAllList = value;
                RaisePropertyChanged("Tbl66GenussesAllList");
            }
        }

        private ObservableCollection<Tbl60Subtribus> _tbl60SubtribussesAllList;
        public new ObservableCollection<Tbl60Subtribus> Tbl60SubtribussesAllList
        {
            get { return _tbl60SubtribussesAllList; }
            set
            {
                if (_tbl60SubtribussesAllList == value) return;
                _tbl60SubtribussesAllList = value;
                RaisePropertyChanged("Tbl60SubtribussesAllList");
            }
        }


        #endregion "Public Properties"
   

       
        #region "Public Properties Tbl63Infratribus"

        public  ICollectionView InfratribussesView;
        public  Tbl63Infratribus CurrentTbl63Infratribus
        {
            get
            {
                if (InfratribussesView != null)
                    return InfratribussesView.CurrentItem as Tbl63Infratribus;
                return null;
            }
        }
        //--------------------------------------------                                               

        private string _searchInfratribusName;
        public  string SearchInfratribusName
        {
            get { return _searchInfratribusName; }
            set
            {
                if (value == _searchInfratribusName) return;
                _searchInfratribusName = value;
                RaisePropertyChanged("SearchInfratribusName");
            }
        }

        private ObservableCollection<Tbl63Infratribus> _tbl63InfratribussesList;
        public  ObservableCollection<Tbl63Infratribus> Tbl63InfratribussesList
        {
            get { return _tbl63InfratribussesList; }
            set
            {
                if (_tbl63InfratribussesList == value) return;
                _tbl63InfratribussesList = value;
                RaisePropertyChanged("Tbl63InfratribussesList");
            }
        }

        #endregion "Public Properties"
   
  
       
        #region "Public Properties Tbl69FiSpecies"

        public ICollectionView FiSpeciessesView;
        public Tbl69FiSpecies CurrentTbl69FiSpecies
        {
            get
            {
                if (FiSpeciessesView != null)
                    return FiSpeciessesView.CurrentItem as Tbl69FiSpecies;
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

        private ObservableCollection<Tbl69FiSpecies> _tbl69FiSpeciesList;
        public ObservableCollection<Tbl69FiSpecies> Tbl69FiSpeciessesList
        {
            get { return _tbl69FiSpeciesList; }
            set
            {
                if (_tbl69FiSpeciesList == value) return;
                _tbl69FiSpeciesList = value;
                RaisePropertyChanged("Tbl69FiSpeciessesList");
            }
        }

        #endregion "Public Properties"
   
  
       
        #region "Public Properties Tbl72PlSpecies"

        public ICollectionView PlSpeciessesView;
        public Tbl72PlSpecies CurrentTbl72PlSpecies
        {
            get
            {
                if (PlSpeciessesView != null)
                    return PlSpeciessesView.CurrentItem as Tbl72PlSpecies;
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

        private ObservableCollection<Tbl72PlSpecies> _tbl72PlSpeciesList;
        public ObservableCollection<Tbl72PlSpecies> Tbl72PlSpeciessesList
        {
            get { return _tbl72PlSpeciesList; }
            set
            {
                if (_tbl72PlSpeciesList == value) return;
                _tbl72PlSpeciesList = value;
                RaisePropertyChanged("Tbl72PlSpeciessesList");
            }
        }

        #endregion "Public Properties"
   
   

 //    Part 11    

        
        #region "Private Methods"

        public void tbl69FiSpeciesView_CurrentChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged("CurrentTbl69FiSpecies");
        }

        public void tbl72PlSpeciesView_CurrentChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged("CurrentTbl72PlSpecies");
        }
        #endregion "Private Methods"
   
      }
}   
