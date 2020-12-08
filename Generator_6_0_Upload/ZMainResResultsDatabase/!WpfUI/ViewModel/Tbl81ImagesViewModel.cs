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

//    Tbl81ImagesViewModel Skriptdatum:  02.06.2014  10:32    

namespace WPFUI.Views.Database
{   


    
    public class Tbl81ImagesViewModel : Tbl03RegnumsViewModel
    {     
        
        #region "Private Data Members"

        protected readonly Tbl81ImagesRepository Tbl81ImagesRepository = new Tbl81ImagesRepository();   
           
        protected readonly Tbl69FiSpeciessesRepository Tbl69FiSpeciessesRepository = new Tbl69FiSpeciessesRepository();   
           
        protected readonly Tbl72PlSpeciessesRepository Tbl72PlSpeciessesRepository = new Tbl72PlSpeciessesRepository();   
           
        protected readonly Tbl68SpeciesgroupsRepository Tbl68SpeciesgroupsRepository = new Tbl68SpeciesgroupsRepository();   
           
        protected readonly Tbl66GenussesRepository Tbl66GenussesRepository = new Tbl66GenussesRepository();   
      

        #endregion "Private Data Members"            
    
        #region "Constructor"

        public Tbl81ImagesViewModel()
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

    
        #region "Public Commands Basic Tbl81Image"

        private RelayCommand _getImageByIdCommand;
        public  ICommand GetImageByIdCommand
        {
            get { return _getImageByNameCommand ?? (_getImageByNameCommand = new RelayCommand(delegate { GetImageById(null); })); }   
        }

        private void GetImageById(object o)       
        {   
   
            Tbl81ImagesList = null;
            Tbl81ImagesAllList = null;
            Tbl69FiSpeciessesAllList = null;
            Tbl72PlSpeciessesAllList = null;
            Tbl69FiSpeciessesList = null;
            Tbl72PlSpeciessesList = null;

            Tbl81ImagesList =  new ObservableCollection<Tbl81Image>
                                                       (from x in Tbl81ImagesRepository.Tbl81Images
                                                        where x.ImageID.Equals(SearchImageId)
                                                        select x);

            Tbl81ImagesAllList =  new ObservableCollection<Tbl81Image>
                                                      (from y in Tbl81ImagesRepository.Tbl81Images
                                                        orderby y.ImageName
                                                        select y);

            Tbl69FiSpeciessesAllList =  new ObservableCollection<Tbl69FiSpecies>
                                                       (from z in Tbl69FiSpeciessesRepository.Tbl69FiSpeciesses
                                                        orderby z.Tbl66Genusses.GenusName, z.FiSpeciesName, z.Subspecies, z.Divers
                                                        select z);        
  
            Tbl72PlSpeciessesAllList =  new ObservableCollection<Tbl72PlSpecies>
                                                       (from z in Tbl72PlSpeciessesRepository.Tbl72PlSpeciesses
                                                        orderby z.Tbl66Genusses.GenusName, z.PlSpeciesName, z.Subspecies, z.Divers
                                                        select z);    
       
ImagesView = CollectionViewSource.GetDefaultView(Tbl81ImagesList);
            if (ImagesView != null)
                ImagesView.CurrentChanged += tbl81ImageView_CurrentChanged;                   
            RaisePropertyChanged("CurrentTbl81Image");
        }
        //------------------------------------------------------------------------------
        private RelayCommand _addImageCommand;
        public ICommand AddImageCommand
        {
            get { return _addImageCommand ?? (_addImageCommand = new RelayCommand(delegate { AddImage(null); })); }
        }

        private void AddImage(object o)
        {
            if (Tbl81ImagesList == null)
                Tbl81ImagesList = new ObservableCollection<Tbl81Image>();
            Tbl81ImagesList.Add(new Tbl81Image{ ImageID= 0 });
            ImagesView = CollectionViewSource.GetDefaultView(Tbl81ImagesList);
            if (ImagesView != null)
                ImagesView.CurrentChanged += tbl81ImageView_CurrentChanged;
            RaisePropertyChanged();
        }
        //---------------------------------------------------------------------------------------  
       
        private RelayCommand _deleteImageCommand;
        public  ICommand DeleteImageCommand
        {
            get { return _deleteImageCommand ?? (_deleteImageCommand = new RelayCommand(delegate { DeleteImage(null); })); }
        }

        private void DeleteImage(object o)
        {
            try
            {
                var image= Tbl81ImagesRepository.Tbl81Images.FirstOrDefault(x => x.ImageID== CurrentTbl81Image.ImageID);
                if (image!= null)
                {
                    if (MessageBox.Show(CultRes.StringsRes.DeleteQuestion
                                        + " " +  CurrentTbl81Image.ImageID, CultRes.StringsRes.DeleteQuestion1, MessageBoxButton.YesNo) !=
                        MessageBoxResult.Yes) return;
                    Tbl81ImagesRepository.Delete(image);
                    Tbl81ImagesRepository.Save();
                    MessageBox.Show(CurrentTbl81Image.ImageID + " " + CultRes.StringsRes.DeleteSuccess);
                    GetImageById(o); //Refresh
                }
                else
                {
                    MessageBox.Show(CultRes.StringsRes.DeleteCan + " " + CurrentTbl81Image.ImageID+ " " + CultRes.StringsRes.DeleteCan1);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //-------------------------------------------------------------------------------------------------
        private RelayCommand _saveImageCommand;
        public  ICommand SaveImageCommand
        {
            get { return _saveImageCommand ?? (_saveImageCommand = new RelayCommand(delegate { SaveImage(null); })); }
        }

        private void SaveImage(object o)
        {
            try
            {
                var image= Tbl81ImagesRepository.Tbl81Images.FirstOrDefault(x => x.ImageID== CurrentTbl81Image.ImageID);
                if (CurrentTbl81Image == null)
                {
                    MessageBox.Show(CultRes.StringsRes.DatasetNotExist);
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
                            PlSpeciesID= CurrentTbl81Image.PlSpeciesID,              
                            CountID = RandomHelper.Randomnumber(),
                            Valid = CurrentTbl81Image.Valid,
                            ValidYear = CurrentTbl81Image.ValidYear,
                            ShotDate = CurrentTbl81Image.ShotDate,
                            Info = CurrentTbl81Image.Info,
                            Memo = CurrentTbl81Image.Memo,
                            ImageData = CurrentTbl81Image.ImageData ,
                            ImageMimeType = CurrentTbl81Image.ImageMimeType ,
                            Filestream = CurrentTbl81Image.Filestream ,
                            FilestreamID = CurrentTbl81Image.FilestreamID ,
                            Writer = Environment.UserName,
                            WriterDate = DateTime.Now,
                            Updater = Environment.UserName,
                            UpdaterDate = DateTime.Now
                        });
                    }
                    {
                        Tbl81ImagesRepository.Save();
                        MessageBox.Show(CurrentTbl81Image.ImageID+ " " + CultRes.StringsRes.SaveSuccess);
                        GetImageById(o);  //Refresh
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

    
        
        #region "Public Commands Connect <== Tbl69FiSpecies"                 

        private RelayCommand _getFiSpeciesByNameCommand;
        public  ICommand GetFiSpeciesByNameCommand
        {
            get { return _getFiSpeciesByNameCommand ?? (_getFiSpeciesByNameCommand = new RelayCommand(GetFiSpeciesByName)); }
        }

        private void GetFiSpeciesByName()
        {
            Tbl69FiSpeciessesList =
                new ObservableCollection<Tbl69FiSpecies>((from fispecies in Tbl69FiSpeciessesRepository.Tbl69FiSpeciesses
                                                       where fispecies.FiSpeciesName.StartsWith(SearchFiSpeciesName)
                                                       orderby fispecies.FiSpeciesName, fispecies.Subspecies, fispecies.Divers
                                                       select fispecies));

            FiSpeciessesView = CollectionViewSource.GetDefaultView(Tbl69FiSpeciessesList);
            if (FiSpeciessesView != null)
                FiSpeciessesView.CurrentChanged += tbl69FiSpeciesView_CurrentChanged;                   
            RaisePropertyChanged("CurrentTbl69FiSpecies");
        }
        //------------------------------------------------------------------------------
        private RelayCommand _addFiSpeciesCommand;
        public  ICommand AddFiSpeciesCommand
        {
            get { return _addFiSpeciesCommand ?? (_addFiSpeciesCommand = new RelayCommand(AddFiSpecies)); }
        }

        private void AddFiSpecies()
        {
            if (Tbl69FiSpeciessesList == null)
                Tbl69FiSpeciessesList = new ObservableCollection<Tbl69FiSpecies>();
            Tbl69FiSpeciessesList.Add(new Tbl69FiSpecies{ FiSpeciesName= "New " });                   
            FiSpeciessesView = CollectionViewSource.GetDefaultView(Tbl69FiSpeciessesList);
            if (FiSpeciessesView != null)
                FiSpeciessesView.CurrentChanged += tbl69FiSpeciesView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl69FiSpecies");
        }
        //---------------------------------------------------------------------------------------
        private RelayCommand _deleteFiSpeciesCommand;
        public  ICommand DeleteFiSpeciesCommand
        {
            get { return _deleteFiSpeciesCommand ?? (_deleteFiSpeciesCommand = new RelayCommand(DeleteFiSpecies)); }
        }

        private void DeleteFiSpecies()
        {
            try
            {
                var fispecies= Tbl69FiSpeciessesRepository.Tbl69FiSpeciesses.FirstOrDefault(x => x.FiSpeciesID== CurrentTbl69FiSpecies.FiSpeciesID);
                if (fispecies!= null)
                {
                    if (MessageBox.Show("Are you really shure to delete this "
                                        + CurrentTbl69FiSpecies.FiSpeciesName + CurrentTbl69FiSpecies.Subspecies+ " " + CurrentTbl69FiSpecies.Divers, "Question", MessageBoxButton.YesNo) !=
                        MessageBoxResult.Yes) return;
                    Tbl69FiSpeciessesRepository.Delete(fispecies);
                    Tbl69FiSpeciessesRepository.Save();
                    MessageBox.Show(CurrentTbl69FiSpecies.FiSpeciesName + CurrentTbl69FiSpecies.Subspecies+ " " + CurrentTbl69FiSpecies.Divers + " was deleted successfully");
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
                    MessageBox.Show("Only " + CurrentTbl69FiSpecies.FiSpeciesName + CurrentTbl69FiSpecies.Subspecies+ " " + CurrentTbl69FiSpecies.Divers + " can be deleted");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //-------------------------------------------------------------------------------------------------
        private RelayCommand _saveFiSpeciesCommand;   
        public  ICommand SaveFiSpeciesCommand
        {
            get { return _saveFiSpeciesCommand ?? (_saveFiSpeciesCommand = new RelayCommand(SaveFiSpecies)); }
        }

        private void SaveFiSpecies()
        {
            try
            {
                var fispecies= Tbl69FiSpeciessesRepository.Tbl69FiSpeciesses.FirstOrDefault(x => x.FiSpeciesID== CurrentTbl69FiSpecies.FiSpeciesID);
                if (CurrentTbl69FiSpecies == null)
                {
                    MessageBox.Show("regnum- subregnum was not found");
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
                        Tbl69FiSpeciessesRepository.Add(new Tbl69FiSpecies()
                        {
                            GenusID = CurrentTbl69FiSpecies.GenusID,
                            SpeciesgroupID = CurrentTbl69FiSpecies.SpeciesgroupID,
                            FiSpeciesName = CurrentTbl69FiSpecies.FiSpeciesName,
                            Subspecies = CurrentTbl69FiSpecies.Subspecies,
                            Divers = CurrentTbl69FiSpecies.Divers,
                            CountID = TblCountersRepository.Counter(),
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
                        MessageBox.Show(CurrentTbl69FiSpecies.FiSpeciesName + CurrentTbl69FiSpecies.Subspecies+ " " + CurrentTbl69FiSpecies.Divers + " was successfully saved ");
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
    

 //    Part 3    


        
        #region "Public Commands Connect <== Tbl72PlSpecies"                 

        private RelayCommand _getPlSpeciesByNameCommand;
        public  ICommand GetPlSpeciesByNameCommand
        {
            get { return _getPlSpeciesByNameCommand ?? (_getPlSpeciesByNameCommand = new RelayCommand(GetPlSpeciesByName)); }
        }

        private void GetPlSpeciesByName()
        {
            Tbl72PlSpeciessesList =
                new ObservableCollection<Tbl72PlSpecies>((from plspecies in Tbl72PlSpeciessesRepository.Tbl72PlSpeciesses
                                                       where plspecies.PlSpeciesName.StartsWith(SearchPlSpeciesName)
                                                       orderby plspecies.PlSpeciesName, plspecies.Subspecies, plspecies.Divers
                                                       select plspecies));

            PlSpeciessesView = CollectionViewSource.GetDefaultView(Tbl72PlSpeciessesList);
            if (PlSpeciessesView != null)
                PlSpeciessesView.CurrentChanged += tbl72PlSpeciesView_CurrentChanged;                   
            RaisePropertyChanged("CurrentTbl72PlSpecies");
        }
        //------------------------------------------------------------------------------
        private RelayCommand _addPlSpeciesCommand;
        public  ICommand AddPlSpeciesCommand
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
        //----------------------------------------------------------------------------------------------------------
        private RelayCommand _deletePlSpeciesCommand;
        public ICommand PlSpeciesPhylumCommand
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
                                        + CurrentTbl72PlSpecies.PlSpeciesName + CurrentTbl72PlSpecies.Subspecies + CurrentTbl72PlSpecies.Divers, "Question", MessageBoxButton.YesNo) !=
                        MessageBoxResult.Yes) return;
                    Tbl72PlSpeciessesRepository.Delete(plspecies);
                    Tbl72PlSpeciessesRepository.Save();
                    MessageBox.Show(CurrentTbl72PlSpecies.PlSpeciesName + CurrentTbl72PlSpecies.Subspecies + CurrentTbl72PlSpecies.Divers + " was deleted successfully");
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
                    MessageBox.Show("Only " + CurrentTbl72PlSpecies.PlSpeciesName + CurrentTbl72PlSpecies.Subspecies + CurrentTbl72PlSpecies.Divers + " can be deleted");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //-------------------------------------------------------------------------------------------------
        private RelayCommand _savePlSpeciesCommand;   
        public  ICommand SavePlSpeciesCommand
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
                        Tbl72PlSpeciessesRepository.Add(new Tbl72PlSpecies()
                        {
                            GenusID = CurrentTbl72PlSpecies.GenusID,
                            SpeciesgroupID = CurrentTbl72PlSpecies.SpeciesgroupID,
                            PlSpeciesName = CurrentTbl72PlSpecies.PlSpeciesName,
                            Subspecies = CurrentTbl72PlSpecies.Subspecies,
                            Divers = CurrentTbl72PlSpecies.Divers,
                            CountID = TblCountersRepository.Counter(),
                            Valid = CurrentTbl72PlSpecies.Valid,
                            ValidYear = CurrentTbl72PlSpecies.ValidYear,
                            MemoSpecies = CurrentTbl72PlSpecies.MemoSpecies,
                            TradeName = CurrentTbl72PlSpecies.TradeName,
                            Author = CurrentTbl72PlSpecies.Author,
                            AuthorYear = CurrentTbl72PlSpecies.AuthorYear,
                            Importer = CurrentTbl72PlSpecies.Importer,
                            ImportingYear = CurrentTbl72PlSpecies.ImportingYear,
                            BasinHeight = CurrentTbl72PlSpecies.BasinHeight,
                            PlantLength = CurrentTbl72PlSpecies.PlantLength,
                            Difficult1 = CurrentTbl72PlSpecies.Difficult1,
                            Difficult2 = CurrentTbl72PlSpecies.Difficult2,
                            Difficult3 = CurrentTbl72PlSpecies.Difficult3,
                            Difficult4 = CurrentTbl72PlSpecies.Difficult4,
                            MemoTech = CurrentTbl72PlSpecies.MemoTech,
                            Ph1 = CurrentTbl72PlSpecies.Ph1,
                            Ph2 = CurrentTbl72PlSpecies.Ph2,
                            Temp1 = CurrentTbl72PlSpecies.Temp1,
                            Temp2 = CurrentTbl72PlSpecies.Temp2,
                            Hardness1 = CurrentTbl72PlSpecies.Hardness1,
                            Hardness2 = CurrentTbl72PlSpecies.Hardness2,
                            CarboHardness1 = CurrentTbl72PlSpecies.CarboHardness1,
                            CarboHardness2 = CurrentTbl72PlSpecies.CarboHardness2,
                            MemoBuilt = CurrentTbl72PlSpecies.MemoBuilt,
                            MemoColor = CurrentTbl72PlSpecies.MemoColor,
                            MemoReproduction = CurrentTbl72PlSpecies.MemoReproduction,
                            MemoCulture = CurrentTbl72PlSpecies.MemoCulture,
                            MemoGlobal = CurrentTbl72PlSpecies.MemoGlobal,
                            Writer = Environment.UserName,
                            WriterDate = DateTime.Now,
                            Updater = Environment.UserName,
                            UpdaterDate = DateTime.Now
                        });
                        }
                        {
                            Tbl72PlSpeciessesRepository.Save();
                            MessageBox.Show(CurrentTbl72PlSpecies.PlSpeciesName + CurrentTbl72PlSpecies.Subspecies + CurrentTbl72PlSpecies.Divers +  " was successfully saved ");
                            GetPlSpeciesByName();  //Refresh
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

    

 //    Part 5    

    

 //    Part 6    

 

 //    Part 7    

 

 //    Part 8    

    

 //    Part 9    



     
        #region "Public Commands Connected Tables by DoubleClick"

        private RelayCommand _getConnectedTablesCommand;
        public new ICommand GetConnectedTablesCommand
        {
            get { return _getConnectedTablesCommand ?? (_getConnectedTablesCommand = new RelayCommand(GetConnectedTablesById)); }
        }

        public  void GetConnectedTablesById()
        {
            //Clear Search-TextBox
            SearchFiSpeciesName = null;
            SearchPlSpeciesName = null;

            Tbl69FiSpeciessesList =
                new ObservableCollection<Tbl69FiSpecies>((from x in Tbl69FiSpeciessesRepository.Tbl69FiSpeciesses
                                                       where x.FiSpeciesID == CurrentTbl81Image.FiSpeciesID
                                                       orderby  x.FiSpeciesName, x.Subspecies, x.Divers
                                                       select x));

            FiSpeciessesView = CollectionViewSource.GetDefaultView(Tbl69FiSpeciessesList);
            if (FiSpeciessesView != null)
                FiSpeciessesView .CurrentChanged += tbl69FiSpeciesView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl69FiSpeciesses");
            //-----------------------------------------------------------------------------------
            Tbl72PlSpeciessesList =
                new ObservableCollection<Tbl72PlSpecies>((from y in Tbl72PlSpeciessesRepository.Tbl72PlSpeciesses
                                                       where y.PlSpeciesID == CurrentTbl81Image.PlSpeciesID
                                                       orderby  y.PlSpeciesName, y.Subspecies, y.Divers
                                                       select y));

            PlSpeciessesView = CollectionViewSource.GetDefaultView(Tbl72PlSpeciessesList);
            if (PlSpeciessesView != null)
                PlSpeciessesView .CurrentChanged += tbl72PlSpeciesView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl72PlSpeciesses");
            //-----------------------------------------------------------------------------------
            Tbl66GenussesTbl69FiSpeciessesAllList =
                new ObservableCollection<Tbl66Genus>((from z in Tbl66GenussesRepository.Tbl66Genusses
                                                      where z.GenusID == CurrentTbl81Image.Tbl69FiSpeciesses.GenusID
                                                      orderby z.GenusName
                                                      select z));

            //-----------------------------------------------------------------------------------
            Tbl66GenussesTbl72PlSpeciessesAllList =
                new ObservableCollection<Tbl66Genus>((from z in Tbl66GenussesRepository.Tbl66Genusses
                                                      where z.GenusID == CurrentTbl81Image.Tbl72PlSpeciesses.GenusID
                                                      orderby z.GenusName
                                                      select z));

            //-----------------------------------------------------------------------------------
            Tbl68SpeciesgroupsTbl69FiSpeciessesAllList =
                 new ObservableCollection<Tbl68Speciesgroup>((from z in Tbl68SpeciesgroupsRepository.Tbl68Speciesgroups
                                                              where z.SpeciesgroupID == CurrentTbl81Image.Tbl69FiSpeciesses.SpeciesgroupID
                                                              orderby z.SpeciesgroupName
                                                              select z));

            //-----------------------------------------------------------------------------------
            Tbl68SpeciesgroupsTbl72PlSpeciessesAllList =
                 new ObservableCollection<Tbl68Speciesgroup>((from z in Tbl68SpeciesgroupsRepository.Tbl68Speciesgroups
                                                              where z.SpeciesgroupID == CurrentTbl81Image.Tbl72PlSpeciesses.SpeciesgroupID
                                                              orderby z.SpeciesgroupName
                                                              select z));

        }

        #endregion "Public Commands Connected Tables by DoubleClick"
   
 

 //    Part 10    

 

 //    Part 11    



     
        #region "Public Properties Tbl81Image"

        public  ICollectionView ImagesView;
        public  Tbl81Image CurrentTbl81Image
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
        public   int SearchImageId
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
        public  ObservableCollection<Tbl81Image> Tbl81ImagesList
        {
            get { return _tbl81ImagesList; }
            set
            {
                if (_tbl81ImagesList == value) return;
                _tbl81ImagesList = value;
                RaisePropertyChanged("Tbl81ImagesList");

                //Clear Search-TextBox
                SearchFiSpeciesName = null;                                
                SearchPlSpeciesName = null;
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

        private ObservableCollection<Tbl72PlSpecies> _tbl72PlSpeciessesAllList;
        public ObservableCollection<Tbl72PlSpecies> Tbl72PlSpeciessesAllList
        {
            get { return _tbl72PlSpeciessesAllList; }
            set
            {
                if (_tbl72PlSpeciessesAllList == value) return;
                _tbl72PlSpeciessesAllList = value;
                RaisePropertyChanged("Tbl72PlSpeciessesAllList");
            }
        }

        private ObservableCollection<Tbl68Speciesgroup> _tbl68SpeciesgroupsTbl69FiSpeciessesAllList;
        public ObservableCollection<Tbl68Speciesgroup> Tbl68SpeciesgroupsTbl69FiSpeciessesAllList
        {
            get { return _tbl68SpeciesgroupsTbl69FiSpeciessesAllList; }
            set
            {
                if (_tbl68SpeciesgroupsTbl69FiSpeciessesAllList == value) return;
                _tbl68SpeciesgroupsTbl69FiSpeciessesAllList = value;
                RaisePropertyChanged("Tbl68SpeciesgroupsTbl69FiSpeciessesAllList");
            }
        }

        private ObservableCollection<Tbl68Speciesgroup> _tbl68SpeciesgroupsTbl72PlSpeciessesAllList;
        public ObservableCollection<Tbl68Speciesgroup> Tbl68SpeciesgroupsTbl72PlSpeciessesAllList
        {
            get { return _tbl68SpeciesgroupsTbl72PlSpeciessesAllList; }
            set
            {
                if (_tbl68SpeciesgroupsTbl72PlSpeciessesAllList== value) return;
                _tbl68SpeciesgroupsTbl72PlSpeciessesAllList= value;
                RaisePropertyChanged("Tbl68SpeciesgroupsTbl72PlSpeciessesAllList");
            }
        }

        private ObservableCollection<Tbl81Image> _tbl81ImagesAllList;
        public ObservableCollection<Tbl81Image> Tbl81ImagesAllList
        {
            get { return _tbl81ImagesAllList; }
            set
            {
                if (_tbl81ImagesAllList == value) return;
                _tbl81ImagesAllList = value;
                RaisePropertyChanged("Tbl81ImagesAllList");
            }
        }

        private ObservableCollection<Tbl66Genus> _tbl66GenussesTbl69FiSpeciessesAllList;
        public  ObservableCollection<Tbl66Genus> Tbl66GenussesTbl69FiSpeciessesAllList
        {
            get { return _tbl66GenussesTbl69FiSpeciessesAllList; }
            set
            {
                if (_tbl66GenussesTbl69FiSpeciessesAllList == value) return;
                _tbl66GenussesTbl69FiSpeciessesAllList = value;
                RaisePropertyChanged("Tbl66GenussesTbl69FiSpeciessesAllList");
            }
        }


        private ObservableCollection<Tbl66Genus> _tbl66GenussesTbl72PlSpeciessesAllList;
        public  ObservableCollection<Tbl66Genus> Tbl66GenussesTbl72PlSpeciessesAllList
        {
            get { return _tbl66GenussesTbl72PlSpeciessesAllList; }
            set
            {
                if (_tbl66GenussesTbl72PlSpeciessesAllList == value) return;
                _tbl66GenussesTbl72PlSpeciessesAllList = value;
                RaisePropertyChanged("Tbl66GenussesTbl72PlSpeciessesAllList");
            }
        }
        #endregion "Public Properties"
   

       
        #region "Public Properties Tbl69FiSpecies"

        public  ICollectionView FiSpeciessesView;
        public  Tbl69FiSpecies CurrentTbl69FiSpecies
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
        public  string SearchFiSpeciesName
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
        public  ObservableCollection<Tbl69FiSpecies> Tbl69FiSpeciessesList
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

        public  ICollectionView PlSpeciessesView;
        public  Tbl72PlSpecies CurrentTbl72PlSpecies
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
        public  string SearchPlSpeciesName
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
        public  ObservableCollection<Tbl72PlSpecies> Tbl72PlSpeciessesList
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
 

 //    Part 11    

        
        #region "Private Methods"

        public void tbl81ImageView_CurrentChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged("CurrentTbl81Image");
        }

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
