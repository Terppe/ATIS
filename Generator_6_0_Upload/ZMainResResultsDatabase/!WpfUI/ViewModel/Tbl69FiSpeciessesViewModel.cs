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

//    Tbl69FiSpeciessesViewModel Skriptdatum:  04.03.2016  10:32    

namespace WPFUI.Views.Database
{   


    
    public class Tbl69FiSpeciessesViewModel : Tbl66GenussesViewModel                     
    {     
        
        #region "Private Data Members"  
           
        protected readonly Tbl78NamesRepository Tbl78NamesRepository = new Tbl78NamesRepository();   
           
        protected readonly Tbl81ImagesRepository Tbl81ImagesRepository = new Tbl81ImagesRepository();   
           
        protected readonly Tbl84SynonymsRepository Tbl84SynonymsRepository = new Tbl84SynonymsRepository();   
           
        protected readonly Tbl87GeographicsRepository Tbl87GeographicsRepository = new Tbl87GeographicsRepository();   
          
          #endregion "Private Data Members"            
    
        #region "Constructor"

        public Tbl69FiSpeciessesViewModel()
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

       
        #region "Public Commands Basic Tbl69FiSpecies"

        private RelayCommand _getFiSpeciesByNameCommand;
        public new ICommand GetFiSpeciesByNameCommand
        {
            get { return _getFiSpeciesByNameCommand ?? (_getFiSpeciesByNameCommand = new RelayCommand(delegate { GetFiSpeciesByNameOrId(null); })); }   
        }

        private void GetFiSpeciesByNameOrId(object o)       
        {   
   
            //All List to null
            Tbl69FiSpeciessesList = null;
            Tbl69FiSpeciessesAllList = null;

            Tbl93CommentsList = null;
            Tbl90RefExpertsList = null;
            Tbl90RefAuthorsList = null;
            Tbl90RefSourcesList = null;


            Tbl66GenussesList = null;
            Tbl78NamesList = null;

            Tbl69FiSpeciessesList =  new ObservableCollection<Tbl69FiSpecies>
                                                       (from x in Tbl69FiSpeciessesRepository.Tbl69FiSpeciesses
                                                        where x.Tbl66Genusses.GenusName.StartsWith(SearchFiSpeciesName)
                                                        orderby x.Tbl66Genusses.GenusName, x.FiSpeciesName, x.Subspecies, x.Divers
                                                        select x);

            Tbl69FiSpeciessesAllList =  new ObservableCollection<Tbl69FiSpecies>
                                                       (from y in Tbl69FiSpeciessesRepository.Tbl69FiSpeciesses
                                                        orderby y.Tbl66Genusses.GenusName, y.FiSpeciesName, y.Subspecies, y.Divers
                                                        select y);

            Tbl66GenussesAllList =   new ObservableCollection<Tbl66Genus>
                                                       (from z in Tbl66GenussesRepository.Tbl66Genusses
                                                        orderby z.GenusName
                                                        select z);

            Tbl68SpeciesgroupsAllList =  new ObservableCollection<Tbl68Speciesgroup>
                                                       (from z in Tbl68SpeciesgroupsRepository.Tbl68Speciesgroups
                                                        orderby z.SpeciesgroupName
                                                        select z);

            Tbl63InfratribussesAllList =  new ObservableCollection<Tbl63Infratribus>
                                                       (from z in Tbl63InfratribussesRepository.Tbl63Infratribusses
                                                        orderby z.InfratribusName
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
  
FiSpeciessesView = CollectionViewSource.GetDefaultView(Tbl69FiSpeciessesList);
            if (FiSpeciessesView != null)
                FiSpeciessesView.CurrentChanged += tbl69FiSpeciesView_CurrentChanged;                   
            RaisePropertyChanged();
        }
        //------------------------------------------------------------------------------
        private RelayCommand _addFiSpeciesCommand;
        public new ICommand AddFiSpeciesCommand
        {
            get { return _addFiSpeciesCommand ?? (_addFiSpeciesCommand = new RelayCommand(AddFiSpecies)); }
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
        public new ICommand DeleteFiSpeciesCommand
        {
            get { return _deleteFiSpeciesCommand ?? (_deleteFiSpeciesCommand = new RelayCommand(delegate { DeleteFiSpecies(null); })); }
        }

        private void DeleteFiSpecies(object o)
        {
            try
            {
                var fispecies= Tbl69FiSpeciessesRepository.Tbl69FiSpeciesses.FirstOrDefault(x => x.FiSpeciesID== CurrentTbl69FiSpecies.FiSpeciesID);
                if (fispecies!= null)
                {
                    if (MessageBox.Show(CultRes.StringsRes.DeleteQuestion
                                        + " " +  CurrentTbl69FiSpecies.FiSpeciesName, CultRes.StringsRes.DeleteQuestion1, MessageBoxButton.YesNo) !=
                        MessageBoxResult.Yes) return;
                    Tbl69FiSpeciessesRepository.Delete(fispecies);
                    Tbl69FiSpeciessesRepository.Save();
                    MessageBox.Show(CurrentTbl69FiSpecies.FiSpeciesName + " " + CultRes.StringsRes.DeleteSuccess);
                    GetFiSpeciesByNameOrId(o); //Refresh
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
        public new ICommand SaveFiSpeciesCommand
        {
            get { return _saveFiSpeciesCommand ?? (_saveFiSpeciesCommand = new RelayCommand(delegate { SaveFiSpecies(null); })); }
        }

        private void SaveFiSpecies(object o)
        {
            try
            {
                var fispecies= Tbl69FiSpeciessesRepository.Tbl69FiSpeciesses.FirstOrDefault(x => x.FiSpeciesID== CurrentTbl69FiSpecies.FiSpeciesID);
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
                            SpeciesgroupID= CurrentTbl69FiSpecies.SpeciesgroupID,              
                            FiSpeciesName= CurrentTbl69FiSpecies.FiSpeciesName,              
                            Subspecies = CurrentTbl69FiSpecies.Subspecies,
                            Divers = CurrentTbl69FiSpecies.Divers,
                            CountID = RandomHelper.Randomnumber(),
                            Valid = CurrentTbl69FiSpecies.Valid,
                            ValidYear = CurrentTbl69FiSpecies.ValidYear,
                            MemoSpecies = CurrentTbl69FiSpecies.MemoSpecies,
                            TradeName = CurrentTbl69FiSpecies.TradeName,
                            Author = CurrentTbl69FiSpecies.Author,
                            AuthorYear = CurrentTbl69FiSpecies.AuthorYear,
                            Importer = CurrentTbl69FiSpecies.Importer,
                            ImportingYear = CurrentTbl69FiSpecies.ImportingYear,
                            TypeSpecies = CurrentTbl69FiSpecies.TypeSpecies,
                            LNumber = CurrentTbl69FiSpecies.LNumber,
                            LOrigin = CurrentTbl69FiSpecies.LOrigin,
                            LDAOrigin = CurrentTbl69FiSpecies.LDAOrigin,
                            LDANumber = CurrentTbl69FiSpecies.LDANumber,
                            BasinLength = CurrentTbl69FiSpecies.BasinLength,
                            FishLength = CurrentTbl69FiSpecies.FishLength,
                            Karnivore = CurrentTbl69FiSpecies.Karnivore,
                            Herbivore = CurrentTbl69FiSpecies.Herbivore,
                            Limnivore = CurrentTbl69FiSpecies.Limnivore,
                            Omnivore = CurrentTbl69FiSpecies.Omnivore,
                            MemoFoods = CurrentTbl69FiSpecies.MemoFoods,
                            Difficult1 = CurrentTbl69FiSpecies.Difficult1,
                            Difficult2 = CurrentTbl69FiSpecies.Difficult2,
                            Difficult3 = CurrentTbl69FiSpecies.Difficult3,
                            Difficult4 = CurrentTbl69FiSpecies.Difficult4,
                            RegionTop = CurrentTbl69FiSpecies.RegionTop,
                            RegionMiddle = CurrentTbl69FiSpecies.RegionMiddle,
                            RegionBottom = CurrentTbl69FiSpecies.RegionBottom,
                            MemoRegion = CurrentTbl69FiSpecies.MemoRegion,
                            MemoTech = CurrentTbl69FiSpecies.MemoTech,
                            Ph1 = CurrentTbl69FiSpecies.Ph1,
                            Ph2 = CurrentTbl69FiSpecies.Ph2,
                            Temp1 = CurrentTbl69FiSpecies.Temp1,
                            Temp2 = CurrentTbl69FiSpecies.Temp2,
                            Hardness1 = CurrentTbl69FiSpecies.Hardness1,
                            Hardness2 = CurrentTbl69FiSpecies.Hardness2,
                            CarboHardness1 = CurrentTbl69FiSpecies.CarboHardness1,
                            CarboHardness2 = CurrentTbl69FiSpecies.CarboHardness2,
                            MemoHusbandry = CurrentTbl69FiSpecies.MemoHusbandry,
                            MemoBuilt = CurrentTbl69FiSpecies.MemoBuilt,
                            MemoColor = CurrentTbl69FiSpecies.MemoColor,
                            MemoSozial = CurrentTbl69FiSpecies.MemoSozial,
                            MemoDomorphism = CurrentTbl69FiSpecies.MemoDomorphism,
                            MemoSpecial = CurrentTbl69FiSpecies.MemoSpecial,
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
    

 //    Part 2    


        
        #region "Public Commands Connect <== Tbl66Genus"                 

        private RelayCommand _getGenusByNameCommand;
        public new ICommand GetGenusByNameCommand
        {
            get { return _getGenusByNameCommand ?? (_getGenusByNameCommand = new RelayCommand(delegate { GetGenusByNameOrId(null); })); }
        }

        private void GetGenusByNameOrId(object o)
        {
            Tbl66GenussesList =
                new ObservableCollection<Tbl66Genus>((from genus in Tbl66GenussesRepository.Tbl66Genusses
                                                       where genus.GenusName.StartsWith(SearchGenusName)
                                                       orderby genus.GenusName
                                                       select genus));

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

        private void AddGenus()
        {
            if (Tbl66GenussesList == null)
                Tbl66GenussesList = new ObservableCollection<Tbl66Genus>();
            Tbl66GenussesList.Add(new Tbl66Genus{ GenusName= "New " });                   
            GenussesView = CollectionViewSource.GetDefaultView(Tbl66GenussesList);
            if (GenussesView != null)
                GenussesView.CurrentChanged += tbl66GenusView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl66Genus");
        }
        //----------------------------------------------------------------------------------------------------------
        private RelayCommand _deleteGenusCommand;
        public ICommand GenusPhylumCommand
        {
            get { return _deleteGenusCommand ?? (_deleteGenusCommand = new RelayCommand(DeleteGenus)); }
        }

        private void DeleteGenus()
        {
            try
            {
                var genus= Tbl66GenussesRepository.Tbl66Genusses.FirstOrDefault(x => x.GenusID== CurrentTbl66Genus.GenusID);
                if (genus!= null)
                {
                    if (MessageBox.Show("Are you really shure to delete this "
                                        + CurrentTbl66Genus.GenusName, "Question", MessageBoxButton.YesNo) !=
                        MessageBoxResult.Yes) return;
                    Tbl66GenussesRepository.Delete(genus);
                    Tbl66GenussesRepository.Save();
                    MessageBox.Show(CurrentTbl66Genus.GenusName+ " was deleted successfully");
                    if (SearchGenusName == null)
                    {
                        GetConnectedTablesById(); //refresh doubleClick                    
                    }
                    else
                    {
                        GetGenusByName(); //search
                    }
                }
                else
                {
                    MessageBox.Show("Only " + CurrentTbl66Genus.GenusName+ " can be deleted");
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
            get { return _saveGenusCommand ?? (_saveGenusCommand = new RelayCommand(SaveGenus)); }
        }

        private void SaveGenus()
        {
            try
            {
                var genus= Tbl66GenussesRepository.Tbl66Genusses.FirstOrDefault(x => x.GenusID== CurrentTbl66Genus.GenusID);
                if (CurrentTbl66Genus == null)
                {
                    MessageBox.Show("genus was not found");
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
                        Tbl66GenussesRepository.Add(new Tbl66Genus()
                        {
                            GenusName= CurrentTbl66Genus.GenusName,              
                            CountID = TblCountersRepository.Counter(),
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
                            MessageBox.Show(CurrentTbl66Genus.GenusName+  " was successfully saved ");
                            GetGenusByName();  //Refresh
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


        
        #region "Public Commands Connect <== Tbl68Speciesgroup"                 

        private RelayCommand _getSpeciesgroupByNameCommand;
        public ICommand GetSpeciesgroupByNameCommand
        {
            get { return _getSpeciesgroupByNameCommand ?? (_getSpeciesgroupByNameCommand = new RelayCommand(delegate { GetSpeciesgroupByNameOrId(null); })); }
        }

        private void GetSpeciesgroupByNameOrId(object o)
        {
            Tbl68SpeciesgroupsList =
                new ObservableCollection<Tbl68Speciesgroup>((from speciesgroup in Tbl68SpeciesgroupsRepository.Tbl68Speciesgroups
                                                       where speciesgroup.SpeciesgroupName.StartsWith(SearchSpeciesgroupName)
                                                       orderby speciesgroup.SpeciesgroupName
                                                       select speciesgroup));

            SpeciesgroupsView = CollectionViewSource.GetDefaultView(Tbl68SpeciesgroupsList);
            if (SpeciesgroupsView != null)
                SpeciesgroupsView.CurrentChanged += tbl68SpeciesgroupView_CurrentChanged;                   
            RaisePropertyChanged();
        }
        //------------------------------------------------------------------------------
        private RelayCommand _addSpeciesgroupCommand;
        public ICommand AddSpeciesgroupCommand
        {
            get { return _addSpeciesgroupCommand ?? (_addSpeciesgroupCommand = new RelayCommand(AddSpeciesgroup)); }
        }

        private void AddSpeciesgroup()
        {
            if (Tbl68SpeciesgroupsList == null)
                Tbl68SpeciesgroupsList = new ObservableCollection<Tbl68Speciesgroup>();
            Tbl68SpeciesgroupsList.Add(new Tbl68Speciesgroup{ SpeciesgroupName= "New " });                   
            SpeciesgroupsView = CollectionViewSource.GetDefaultView(Tbl68SpeciesgroupsList);
            if (SpeciesgroupsView != null)
                SpeciesgroupsView.CurrentChanged += tbl68SpeciesgroupView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl68Speciesgroup");
        }
        //----------------------------------------------------------------------------------------------------------
        private RelayCommand _deleteSpeciesgroupCommand;
        public ICommand SpeciesgroupPhylumCommand
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
                    MessageBox.Show(CurrentTbl68Speciesgroup.SpeciesgroupName+ " was deleted successfully");
                    if (SearchSpeciesgroupName == null)
                    {
                        GetConnectedTablesById(); //refresh doubleClick                    
                    }
                    else
                    {
                        GetSpeciesgroupByName(); //search
                    }
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
        public ICommand SaveSpeciesgroupCommand
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
                        Tbl68SpeciesgroupsRepository.Add(new Tbl68Speciesgroup()
                        {
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
    
      

 //    Part 4    


        
        #region "Public Commands Connect ==> Tbl78Name"                 

        private RelayCommand _getNameByNameCommand;
        public ICommand GetNameByNameCommand
        {
            get { return _getNameByNameCommand ?? (_getNameByNameCommand = new RelayCommand(delegate { GetNameByNameOrId(null); })); }
        }

        private void GetNameByNameOrId(object o)
        {
            Tbl78NamesList =
                new ObservableCollection<Tbl78Name>((from name in Tbl78NamesRepository.Tbl78Names
                                                       where name.NameName.StartsWith(SearchNameName)
                                                       orderby name.NameName
                                                       select name));

            NamesView = CollectionViewSource.GetDefaultView(Tbl78NamesList);
            if (NamesView != null)
                NamesView.CurrentChanged += tbl78NameView_CurrentChanged;                   
            RaisePropertyChanged();
        }
        //------------------------------------------------------------------------------
        private RelayCommand _addNameCommand;
        public ICommand AddNameCommand
        {
            get { return _addNameCommand ?? (_addNameCommand = new RelayCommand(delegate { AddName(null); })); }
        }

        private void AddName(object o)
        {
            if (Tbl78NamesList == null)
                Tbl78NamesList = new ObservableCollection<Tbl78Name>();
            Tbl78NamesList.Add(new Tbl78Name{ NameName= CultRes.StringsRes.DatasetNew });                   
            NamesView = CollectionViewSource.GetDefaultView(Tbl78NamesList);
            if (NamesView != null)
                NamesView.CurrentChanged += tbl78NameView_CurrentChanged;
            RaisePropertyChanged();
        }
        //---------------------------------------------------------------------------------------
        private RelayCommand _deleteNameCommand;
        public ICommand DeleteNameCommand
        {
            get { return _deleteNameCommand ?? (_deleteNameCommand = new RelayCommand(delegate { DeleteName(null); })); }
        }

        private void DeleteName(object o)
        {
            try
            {
                var name = Tbl78NamesRepository.Tbl78Names.FirstOrDefault(x => x.NameID== CurrentTbl78Name.NameID);
                if (name != null)
                {
                    if (MessageBox.Show(CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl78Name.NameName, CultRes.StringsRes.DeleteQuestion1, MessageBoxButton.YesNo) !=
                        MessageBoxResult.Yes) return;
                    Tbl78NamesRepository.Delete(name);
                    Tbl78NamesRepository.Save();
                    MessageBox.Show(CurrentTbl78Name.NameName + " " + CultRes.StringsRes.DeleteSuccess);
                    GetNameByNameOrId(o);  //Refresh
                }
                else
                {
                    MessageBox.Show(CultRes.StringsRes.DeleteCan + " " + CurrentTbl78Name.NameName+ " " + CultRes.StringsRes.DeleteCan1);
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
            get { return _saveNameCommand ?? (_saveNameCommand = new RelayCommand(delegate { SaveName(null); })); }
        }

        private void SaveName(object o)
        {
            try
            {
                var name = Tbl78NamesRepository.Tbl78Names.FirstOrDefault(x => x.NameID== CurrentTbl78Name.NameID);
                if (CurrentTbl78Name == null)
                {
                    MessageBox.Show(CultRes.StringsRes.DatasetNotExist);
                }
                else
                {
                    if (CurrentTbl78Name.NameID!= 0)
                    {
                        if (name!= null) //update
                        {
                            name.Updater = Environment.UserName;
                            name.UpdaterDate = DateTime.Now;
                        }
                    }
                    else
                    {
                        Tbl78NamesRepository.Add(new Tbl78Name
                        {
                            FiSpeciesID= CurrentTbl78Name.FiSpeciesID,              
                            PlSpeciesID = 2,
                            NameName= CurrentTbl78Name.NameName,              
                            CountID = RandomHelper.Randomnumber(),
                            Valid = CurrentTbl78Name.Valid,
                            ValidYear = CurrentTbl78Name.ValidYear,
                            Language = CurrentTbl78Name.Language,
                            Info = CurrentTbl78Name.Info,
                            Writer = Environment.UserName,
                            WriterDate = DateTime.Now,
                            Updater = Environment.UserName,
                            UpdaterDate = DateTime.Now,
                            Memo = CurrentTbl78Name.Memo
                        });
                    }
                    {
                        Tbl78NamesRepository.Save();
                        if (CurrentTbl78Name != null)
                            MessageBox.Show(CurrentTbl78Name.NameName+ " " + CultRes.StringsRes.SaveSuccess);
                        GetNameByNameOrId(o);  //Refresh      
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


        
        #region "Public Commands Connect ==> Tbl81Image"                 

        private RelayCommand _getImageByIDCommand;
        public ICommand GetImageByIDCommand
        {
            get { return _getImageByNameCommand ?? (_getImageByNameCommand = new RelayCommand(delegate { GetImageByNameOrId(null); })); }
        }

        private void GetImageByID(object o)
        {
            Tbl81ImagesList =  new ObservableCollection<Tbl81Image>
                                                      (from image in Tbl81ImagesRepository.Tbl81Images
                                                       where image.ImageID == SearchImageId
                                          //             where image.ImageName.StartsWith(SearchImageName)
                                                       orderby image.ImageID
                                                       select image);

            ImagesView = CollectionViewSource.GetDefaultView(Tbl81ImagesList);
            if (ImagesView != null)
                ImagesView.CurrentChanged += tbl81ImageView_CurrentChanged;                   
            RaisePropertyChanged();
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
                Tbl81ImagesList = new ObservableCollection<Tbl81Image>();
            Tbl81ImagesList.Add(new Tbl81Image{ Info= "New " });                   
            ImagesView = CollectionViewSource.GetDefaultView(Tbl81ImagesList);
            if (ImagesView != null)
                ImagesView.CurrentChanged += tbl81ImageView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl81Image");
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
                var image= Tbl81ImagesRepository.Tbl81Images.FirstOrDefault(x => x.ImageID== CurrentTbl81Image.ImageID);
                if (image!= null)
                {
                    if (MessageBox.Show("Are you really shure to delete this "
                                        + CurrentTbl81Image.ImageID, "Question", MessageBoxButton.YesNo) !=
                        MessageBoxResult.Yes) return;
                    Tbl81ImagesRepository.Delete(image);
                    Tbl81ImagesRepository.Save();
                    MessageBox.Show(CurrentTbl81Image.ImageID+ " was deleted successfully");
 
                        GetImageByID(); //search
                   
                }
                else
                {
                    MessageBox.Show("Only " + CurrentTbl81Image.ImageID+ " can be deleted");
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
                var image= Tbl81ImagesRepository.Tbl81Images.FirstOrDefault(x => x.ImageID== CurrentTbl81Image.ImageID);
                if (CurrentTbl81Image == null)
                {
                    MessageBox.Show("image was not found");
                }
                else
                {
                    if (CurrentTbl81Image.ImageID!= 0)
                    {
                        if (image!= null) //update
                        {
                            image.Updater = Environment.UserName;
                            image.UpdaterDate = DateTime.Now;
                        }
                    }
                    else
                    {
                        Tbl81ImagesRepository.Add(new Tbl81Image
                        {
                            FiSpeciesID= CurrentTbl81Image.FiSpeciesID,              
                            PlSpeciesID = 2,
                            CountID = TblCountersRepository.Counter(),
                            Valid = CurrentTbl81Image.Valid,
                            ValidYear = CurrentTbl81Image.ValidYear,
                            Info = CurrentTbl81Image.Info,
                            ShotDate = CurrentTbl81Image.ShotDate,
                            Writer = Environment.UserName,
                            WriterDate = DateTime.Now,
                            Updater = Environment.UserName,
                            UpdaterDate = DateTime.Now,
                            ImageData = CurrentTbl81Image.ImageData,
                            ImageMimeType = CurrentTbl81Image.ImageMimeType,
                            Filestream = CurrentTbl81Image.Filestream,
                            FilestreamID = CurrentTbl81Image.FilestreamID,
                            Memo = CurrentTbl81Image.Memo
                        });
                    }
                    {
                        Tbl81ImagesRepository.Save();
                        MessageBox.Show(CurrentTbl81Image.ImageID+  " was successfully saved ");              
                            GetImageByID(); //search
                              
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


        
        #region "Public Commands Connect ==> Tbl84Synonym"                 

        private RelayCommand _getSynonymByNameCommand;
        public ICommand GetSynonymByNameCommand
        {
            get { return _getSynonymByNameCommand ?? (_getSynonymByNameCommand = new RelayCommand(GetSynonymByName)); }
        }

        private void GetSynonymByName()
        {
            Tbl84SynonymsList =
                new ObservableCollection<Tbl84Synonym>((from synonym in Tbl84SynonymsRepository.Tbl84Synonyms
                                                       where synonym.SynonymName.StartsWith(SearchSynonymName)
                                                       orderby synonym.SynonymName
                                                       select synonym));

            SynonymsView = CollectionViewSource.GetDefaultView(Tbl84SynonymsList);
            if (SynonymsView != null)
                SynonymsView.CurrentChanged += tbl84SynonymView_CurrentChanged;                   
            RaisePropertyChanged("CurrentTbl84Synonym");
        }
        //------------------------------------------------------------------------------
        private RelayCommand _addSynonymCommand;
        public ICommand AddSynonymCommand
        {
            get { return _addSynonymCommand ?? (_addSynonymCommand = new RelayCommand(AddSynonym)); }
        }

        private void AddSynonym()
        {
            if (Tbl84SynonymsList == null)
                Tbl84SynonymsList = new ObservableCollection<Tbl84Synonym>();
            Tbl84SynonymsList.Add(new Tbl84Synonym{ SynonymName= "New " });                   
            SynonymsView = CollectionViewSource.GetDefaultView(Tbl84SynonymsList);
            if (SynonymsView != null)
                SynonymsView.CurrentChanged += tbl84SynonymView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl84Synonym");
        }
        //---------------------------------------------------------------------------------------
        private RelayCommand _deleteSynonymCommand;
        public ICommand DeleteSynonymCommand
        {
            get { return _deleteSynonymCommand ?? (_deleteSynonymCommand = new RelayCommand(DeleteSynonym)); }
        }

        private void DeleteSynonym()
        {
            try
            {
                var synonym = Tbl84SynonymsRepository.Tbl84Synonyms.FirstOrDefault(x => x.SynonymID== CurrentTbl84Synonym.SynonymID);
                if (synonym != null)
                {
                    if (MessageBox.Show("Are you really shure to delete this "
                                        + CurrentTbl84Synonym.SynonymName, "Question", MessageBoxButton.YesNo) !=
                        MessageBoxResult.Yes) return;
                    Tbl84SynonymsRepository.Delete(synonym);
                    Tbl84SynonymsRepository.Save();
                    MessageBox.Show(CurrentTbl84Synonym.SynonymName+ " was deleted successfully");
                    if (SearchSynonymName == null)
                    {
                        GetConnectedTablesById(); //refresh doubleClick                    
                    }
                    else
                    {
                        GetSynonymByName(); //search
                    }
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
        public ICommand SaveSynonymCommand
        {
            get { return _saveSynonymCommand ?? (_saveSynonymCommand = new RelayCommand(SaveSynonym)); }
        }

        private void SaveSynonym()
        {
            try
            {
                var synonym = Tbl84SynonymsRepository.Tbl84Synonyms.FirstOrDefault(x => x.SynonymID== CurrentTbl84Synonym.SynonymID);
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
                            PlSpeciesID = 2,
                            SynonymName= CurrentTbl84Synonym.SynonymName,              
                            CountID = TblCountersRepository.Counter(),
                            Valid = CurrentTbl84Synonym.Valid,
                            ValidYear = CurrentTbl84Synonym.ValidYear,
                            Info = CurrentTbl84Synonym.Info,
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
                        if (SearchSynonymName == null)
                        {
                            GetConnectedTablesById(); //refresh doubleClick                    
                        }
                        else
                        {
                            GetSynonymByName(); //search
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
    
   

 //    Part 7    


        
        #region "Public Commands Connect ==> Tbl87Geographic"                 

        private RelayCommand _getGeographicByIDCommand;
        public ICommand GetGeographicByIDCommand
        {
            get { return _getGeographicByIDCommand ?? (_getGeographicByIDCommand = new RelayCommand(GetGeographicByID)); }
        }

        private void GetGeographicByID()
        {
            Tbl87GeographicsList =
                new ObservableCollection<Tbl87Geographic>((from geographic in Tbl87GeographicsRepository.Tbl87Geographics
                                                       where geographic.GeographicID == SearchGeographicId
                                         //              where geographic.GeographicName.StartsWith(SearchGeographicName)
                                                       orderby geographic.GeographicID
                                                       select geographic));

            GeographicsView = CollectionViewSource.GetDefaultView(Tbl87GeographicsList);
            if (GeographicsView != null)
                GeographicsView.CurrentChanged += tbl87GeographicView_CurrentChanged;                   
            RaisePropertyChanged("CurrentTbl87Geographic");
        }
        //------------------------------------------------------------------------------
        private RelayCommand _addGeographicCommand;
        public ICommand AddGeographicCommand
        {
            get { return _addGeographicCommand ?? (_addGeographicCommand = new RelayCommand(AddGeographic)); }
        }

        private void AddGeographic()
        {
            if (Tbl87GeographicsList == null)
                Tbl87GeographicsList = new ObservableCollection<Tbl87Geographic>();
            Tbl87GeographicsList.Add(new Tbl87Geographic{ Info= "New " });                   
            GeographicsView = CollectionViewSource.GetDefaultView(Tbl87GeographicsList);
            if (GeographicsView != null)
                GeographicsView.CurrentChanged += tbl87GeographicView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl87Geographic");
        }
        //---------------------------------------------------------------------------------------
        private RelayCommand _deleteGeographicCommand;
        public ICommand DeleteGeographicCommand
        {
            get { return _deleteGeographicCommand ?? (_deleteGeographicCommand = new RelayCommand(DeleteGeographic)); }
        }

        private void DeleteGeographic()
        {
            try
            {
                var geographic = Tbl87GeographicsRepository.Tbl87Geographics.FirstOrDefault(x => x.GeographicID== CurrentTbl87Geographic.GeographicID);
                if (geographic != null)
                {
                    if (MessageBox.Show("Are you really shure to delete this "
                                        + CurrentTbl87Geographic.GeographicID, "Question", MessageBoxButton.YesNo) !=
                        MessageBoxResult.Yes) return;
                    Tbl87GeographicsRepository.Delete(geographic);
                    Tbl87GeographicsRepository.Save();
                    MessageBox.Show(CurrentTbl87Geographic.GeographicID+ " was deleted successfully");
                        GetGeographicByID(); //search
                }
                else
                {
                    MessageBox.Show("Only " + CurrentTbl87Geographic.GeographicID+ " can be deleted");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //-------------------------------------------------------------------------------------------------
        private RelayCommand _saveGeographicCommand;   
        public ICommand SaveGeographicCommand
        {
            get { return _saveGeographicCommand ?? (_saveGeographicCommand = new RelayCommand(SaveGeographic)); }
        }

        private void SaveGeographic()
        {
            try
            {
                var geographic = Tbl87GeographicsRepository.Tbl87Geographics.FirstOrDefault(x => x.GeographicID== CurrentTbl87Geographic.GeographicID);
                if (CurrentTbl87Geographic == null)
                {
                    MessageBox.Show("geographic was not found");
                }
                else
                {
                    if (CurrentTbl87Geographic.GeographicID!= 0)
                    {
                        if (geographic!= null) //update
                        {
                            geographic.Updater = Environment.UserName;
                            geographic.UpdaterDate = DateTime.Now;
                        }
                    }
                    else
                    {
                        Tbl87GeographicsRepository.Add(new Tbl87Geographic
                        {
                            FiSpeciesID= CurrentTbl87Geographic.FiSpeciesID,              
                            PlSpeciesID = 2,
                            CountID = TblCountersRepository.Counter(),
                            Address = CurrentTbl87Geographic.Address,
                            Continent = CurrentTbl87Geographic.Continent ,
                            Country = CurrentTbl87Geographic.Country ,
                            Http = CurrentTbl87Geographic.Http,
                            Latitude = CurrentTbl87Geographic.Latitude ,
                            Longitude = CurrentTbl87Geographic.Longitude ,
                            Valid = CurrentTbl87Geographic.Valid,
                            ValidYear = CurrentTbl87Geographic.ValidYear,
                            Info = CurrentTbl87Geographic.Info,
                            Writer = Environment.UserName,
                            WriterDate = DateTime.Now,
                            Updater = Environment.UserName,
                            UpdaterDate = DateTime.Now,
                            Memo = CurrentTbl87Geographic.Memo
                        });
                    }
                    {
                        Tbl87GeographicsRepository.Save();
                        MessageBox.Show(CurrentTbl87Geographic.GeographicID+  " was successfully saved ");
                            GetGeographicByID(); //search
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion "Public Commands"
    
   

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
                            FiSpeciesID= CurrentTbl90RefAuthor.FiSpeciesID,
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
                            FiSpeciesID= CurrentTbl90RefSource.FiSpeciesID,
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
                            FiSpeciesID= CurrentTbl90RefExpert.FiSpeciesID,
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
                            FiSpeciesID= CurrentTbl93Comment.FiSpeciesID,                
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
            SearchGenusName = null;                       
            SearchNameName = null;
            SearchCommentInfo = null;
            SearchRefExpertName = null;
            SearchRefSourceName = null;
            SearchRefAuthorName = null;

            Tbl66GenussesList =
                new ObservableCollection<Tbl66Genus>((from genus in Tbl66GenussesRepository.Tbl66Genusses
                                                       where genus.GenusID== CurrentTbl69FiSpecies.GenusID
                                                       orderby genus.GenusName
                                                       select genus));

            GenussesView = CollectionViewSource.GetDefaultView(Tbl66GenussesList);
            if (GenussesView != null)
                GenussesView.CurrentChanged += tbl66GenusView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl66Genusses");
            //-----------------------------------------------------------------------------------
            Tbl78NamesList =
                new ObservableCollection<Tbl78Name>((from name in Tbl78NamesRepository.Tbl78Names
                                                       where name.FiSpeciesID== CurrentTbl69FiSpecies.FiSpeciesID
                                                       orderby name.Tbl69FiSpeciesses.FiSpeciesName
                                                       select name));


            NamesView = CollectionViewSource.GetDefaultView(Tbl78NamesList);
            if (NamesView != null)
                NamesView.CurrentChanged += tbl78NameView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl78Names");
            //-----------------------------------------------------------------------------------
            Tbl90RefAuthorsList =
                new ObservableCollection<Tbl90Reference>((from refAu in Tbl90ReferencesRepository.Tbl90References
                                                          where refAu.FiSpeciesID== CurrentTbl69FiSpecies.FiSpeciesID
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
                                                          where refSo.FiSpeciesID== CurrentTbl69FiSpecies.FiSpeciesID
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
                                                          where refEx.FiSpeciesID== CurrentTbl69FiSpecies.FiSpeciesID
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
                                                          where comm.FiSpeciesID== CurrentTbl69FiSpecies.FiSpeciesID
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

     
        #region "Public Commands to open Detail TabItems"

        private int _selectedMainTabIndex;
        public new int SelectedMainTabIndex
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
        public new int SelectedMainSubTabIndex
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
        public new int SelectedDetailTabIndex
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
        public new int SelectedDetailSubTabIndex
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

 

 //    Part 11    



     
        #region "Public Properties Tbl69FiSpecies"

        public new ICollectionView FiSpeciessesView;
        public new Tbl69FiSpecies CurrentTbl69FiSpecies
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

                //Clear Search-TextBox
                SearchGenusName = null;                                
                SearchNameName = null;
                SearchCommentInfo = null;
                SearchRefExpertName = null;
                SearchRefSourceName = null;
                SearchRefAuthorName = null;
            }
        }

        private ObservableCollection<Tbl69FiSpecies> _tbl69FiSpeciessesAllList;
        public ObservableCollection<Tbl69FiSpecies> Tbl69FiSpeciessesAllList
        {
            get { return _tbl69FiSpeciessesAllList; }
            set
            {
                if (_tbl69FiSpeciessesAllList == value) return;
                _tbl69FiSpeciessesAllList = value;
                RaisePropertyChanged("Tbl69FiSpeciessesAllList");
            }
        }

        #endregion "Public Properties"   

       
        #region "Public Properties Tbl66Genus"

        public new  ICollectionView GenussesView;
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
        public new  string SearchGenusName
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
            }
        }

        #endregion "Public Properties"
   
  
       
        #region "Public Properties Tbl68Speciesgroup"

        public  ICollectionView SpeciesgroupsView;
        public  Tbl68Speciesgroup CurrentTbl68Speciesgroup
        {
            get
            {
                if (SpeciesgroupsView != null)
                    return SpeciesgroupsView.CurrentItem as Tbl68Speciesgroup;
                return null;
            }
        }
        //--------------------------------------------                                               

        private string _searchSpeciesgroupName;
        public  string SearchSpeciesgroupName
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
        public  ObservableCollection<Tbl68Speciesgroup> Tbl68SpeciesgroupsList
        {
            get { return _tbl68SpeciesgroupsList; }
            set
            {
                if (_tbl68SpeciesgroupsList == value) return;
                _tbl68SpeciesgroupsList = value;
                RaisePropertyChanged("Tbl68SpeciesgroupsList");
            }
        }

   
  
       
        #region "Public Properties Tbl78Name"

        public ICollectionView NamesView;
        public Tbl78Name CurrentTbl78Name
        {
            get
            {
                if (NamesView != null)
                    return NamesView.CurrentItem as Tbl78Name;
                return null;
            }
        }
        //--------------------------------------------                                               

        private string _searchNameName;
        public string SearchNameName
        {
            get { return _searchNameName; }
            set
            {
                if (value == _searchNameName) return;
                _searchNameName = value;
                RaisePropertyChanged("SearchNameName");
            }
        }

        private ObservableCollection<Tbl78Name> _tbl78NamesList;
        public ObservableCollection<Tbl78Name> Tbl78NamesList
        {
            get { return _tbl78NamesList; }
            set
            {
                if (_tbl78NamesList == value) return;
                _tbl78NamesList = value;
                RaisePropertyChanged("Tbl78NamesList");
            }
        }

        #endregion "Public Properties"
   
  
       
        #region "Public Properties Tbl81Image"

        public ICollectionView ImagesView;
        public Tbl81Image CurrentTbl81Image
        {
            get
            {
                if (ImagesView != null)
                    return ImagesView.CurrentItem as Tbl81Image;
                return null;
            }
        }
        //--------------------------------------------                                               

        private int _searchImageId;
        public int SearchImageId
        {
            get { return _searchImageId; }
            set
            {
                if (value == _searchImageId) return;
                _searchImageId = value;
                RaisePropertyChanged("SearchImageId");
            }
        }

        private ObservableCollection<Tbl81Image> _tbl81ImagesList;
        public ObservableCollection<Tbl81Image> Tbl81ImagesList
        {
            get { return _tbl81ImagesList; }
            set
            {
                if (_tbl81ImagesList == value) return;
                _tbl81ImagesList = value;
                RaisePropertyChanged("Tbl81ImagesList");
            }
        }

        #endregion "Public Properties"
   
  
       
        #region "Public Properties Tbl84Synonym"

        public ICollectionView SynonymsView;
        public Tbl84Synonym CurrentTbl84Synonym
        {
            get
            {
                if (SynonymsView != null)
                    return SynonymsView.CurrentItem as Tbl84Synonym;
                return null;
            }
        }
        //--------------------------------------------                                               

        private string _searchSynonymName;
        public string SearchSynonymName
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
        public ObservableCollection<Tbl84Synonym> Tbl84SynonymsList
        {
            get { return _tbl84SynonymsList; }
            set
            {
                if (_tbl84SynonymsList == value) return;
                _tbl84SynonymsList = value;
                RaisePropertyChanged("Tbl84SynonymsList");
            }
        }

        #endregion "Public Properties"
   
  
       
        #region "Public Properties Tbl87Geographic"

        public ICollectionView GeographicsView;
        public Tbl87Geographic CurrentTbl87Geographic
        {
            get
            {
                if (GeographicsView != null)
                    return GeographicsView.CurrentItem as Tbl87Geographic;
                return null;
            }
        }
        //--------------------------------------------                                               

        private int _searchGeographicId;
        public int SearchGeographicId
        {
            get { return _searchGeographicId; }
            set
            {
                if (value == _searchGeographicId) return;
                _searchGeographicId = value;
                RaisePropertyChanged("SearchGeographicId");
            }
        }

        private ObservableCollection<Tbl87Geographic> _tbl87GeographicsList;
        public ObservableCollection<Tbl87Geographic> Tbl87GeographicsList
        {
            get { return _tbl87GeographicsList; }
            set
            {
                if (_tbl87GeographicsList == value) return;
                _tbl87GeographicsList = value;
                RaisePropertyChanged("Tbl87GeographicsList");
            }
        }

        #endregion "Public Properties"
   
   

 //    Part 11    

       
        public void tbl68SpeciesgroupView_CurrentChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged("CurrentTbl68Speciesgroup");
        }
   
          
        #region "Private Methods"

        public void tbl78NameView_CurrentChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged("CurrentTbl78Name");
        }

        public void tbl81ImageView_CurrentChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged("CurrentTbl81Image");
        }
        
        public void tbl84SynonymView_CurrentChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged("CurrentTbl84Synonym");
        }
        
        public void tbl87GeographicView_CurrentChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged("CurrentTbl87Geographic");
        }
        #endregion "Private Methods"
   
      }
}   
