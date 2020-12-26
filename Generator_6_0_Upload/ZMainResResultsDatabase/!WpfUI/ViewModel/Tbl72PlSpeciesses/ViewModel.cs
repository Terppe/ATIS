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

//    Tbl72PlSpeciesViewModel Skriptdatum:  15.03.2012  10:32    

namespace Atis.WpfUi.ViewModel
{   


    
    public class Tbl72PlSpeciessesViewModel : Tbl66GenussesViewModel                     
    {     
         
      #region "Private Data Members"

        protected readonly Tbl78NamesRepository Tbl78NamesRepository = new Tbl78NamesRepository();   
          
          #endregion "Private Data Members"            
    
        #region "Constructor"

        public Tbl72PlSpeciessesViewModel()
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
       
        #region "Public Commands Basic Tbl72PlSpecies"

        private RelayCommand _getPlSpeciesByNameCommand;
        public new ICommand GetPlSpeciesByNameCommand
        {
            get { return _getPlSpeciesByNameCommand ?? (_getPlSpeciesByNameCommand = new RelayCommand(GetPlSpeciesByName)); }
        }

        private void GetPlSpeciesByName()
        {   
Tbl72PlSpeciessesList =
                 new ObservableCollection<Tbl72PlSpecies>((from x in Tbl72PlSpeciessesRepository.Tbl72PlSpeciesses
                                                        where x.PlSpeciesName.StartsWith(SearchPlSpeciesName)
                                                        orderby x.PlSpeciesName
                                                        select x));

            Tbl72PlSpeciessesAllList =
                 new ObservableCollection<Tbl72PlSpecies>((from y in Tbl72PlSpeciessesRepository.Tbl72PlSpeciesses
                                                        orderby y.PlSpeciesName
                                                        select y));

            Tbl66GenussesAllList =
                 new ObservableCollection<Tbl66Genus>((from z in Tbl66GenussesRepository.Tbl66Genusses
                                                        orderby z.GenusName
                                                        select z));

              
  Tbl63InfratribussesAllList =
                 new ObservableCollection<Tbl63Infratribus>((from z in Tbl63InfratribussesRepository.Tbl63Infratribusses
                                                        orderby z.InfratribusName
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

  
Tbl66GenussesList = null;                  
  Tbl78NamesList = null;     
             
  View = CollectionViewSource.GetDefaultView(Tbl72PlSpeciessesList);
            if (View != null)
                View.CurrentChanged += tbl72PlSpeciesView_CurrentChanged;                   
            RaisePropertyChanged("CurrentTbl72PlSpecies");
        }
        //------------------------------------------------------------------------------
        private RelayCommand _addPlSpeciesCommand;
        public new ICommand AddPlSpeciesCommand
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
        public new ICommand DeletePlSpeciesCommand
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
                    MessageBox.Show(CurrentTbl72PlSpecies.PlSpeciesName + " was deleted successfully");
                    GetPlSpeciesByName(); //Refresh
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
        public new ICommand SavePlSpeciesCommand
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
  

        
        #region "Public Commands Connect <== Tbl66Genus"                 

        private RelayCommand _getGenusByNameCommand;
        public new ICommand GetGenusByNameCommand
        {
            get { return _getGenusByNameCommand ?? (_getGenusByNameCommand = new RelayCommand(GetGenusByName)); }
        }

        private void GetGenusByName()
        {
            Tbl66GenussesList =
                new ObservableCollection<Tbl66Genus>((from genus in Tbl66GenussesRepository.Tbl66Genusses
                                                       where genus.GenusName.StartsWith(SearchGenusName)
                                                       orderby genus.GenusName
                                                       select genus));

            View = CollectionViewSource.GetDefaultView(Tbl66GenussesList);
            if (View != null)
                View.CurrentChanged += tbl66GenusView_CurrentChanged;                   
            RaisePropertyChanged("CurrentTbl66Genus");
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
            View = CollectionViewSource.GetDefaultView(Tbl66GenussesList);
            if (View != null)
                View.CurrentChanged += tbl66GenusView_CurrentChanged;
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
    
  
        
        #region "Public Commands Connect ==> Tbl78Name"                 

        private RelayCommand _getNameByNameCommand;
        public ICommand GetNameByNameCommand
        {
            get { return _getNameByNameCommand ?? (_getNameByNameCommand = new RelayCommand(GetNameByName)); }
        }

        private void GetNameByName()
        {
            Tbl78NamesList =
                new ObservableCollection<Tbl78Name>((from name in Tbl78NamesRepository.Tbl78Names
                                                       where name.NameName.StartsWith(SearchNameName)
                                                       orderby name.NameName
                                                       select name));

            View = CollectionViewSource.GetDefaultView(Tbl78NamesList);
            if (View != null)
                View.CurrentChanged += tbl78NameView_CurrentChanged;                   
            RaisePropertyChanged("CurrentTbl78Name");
        }
        //------------------------------------------------------------------------------
        private RelayCommand _addNameCommand;
        public ICommand AddNameCommand
        {
            get { return _addNameCommand ?? (_addNameCommand = new RelayCommand(AddName)); }
        }

        private void AddName()
        {
            if (Tbl78NamesList == null)
                Tbl78NamesList = new ObservableCollection<Tbl78Name>();
            Tbl78NamesList.Add(new Tbl78Name{ NameName= "New " });                   
            View = CollectionViewSource.GetDefaultView(Tbl78NamesList);
            if (View != null)
                View.CurrentChanged += tbl78NameView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl78Name");
        }
        //---------------------------------------------------------------------------------------
        private RelayCommand _deleteNameCommand;
        public ICommand DeleteNameCommand
        {
            get { return _deleteNameCommand ?? (_deleteNameCommand = new RelayCommand(DeleteName)); }
        }

        private void DeleteName()
        {
            try
            {
                var name = Tbl78NamesRepository.Tbl78Names.FirstOrDefault(x => x.NameID== CurrentTbl78Name.NameID);
                if (name != null)
                {
                    if (MessageBox.Show("Are you really shure to delete this "
                                        + CurrentTbl78Name.NameName, "Question", MessageBoxButton.YesNo) !=
                        MessageBoxResult.Yes) return;
                    Tbl78NamesRepository.Delete(name);
                    Tbl78NamesRepository.Save();
                    MessageBox.Show(CurrentTbl78Name.NameName+ " was deleted successfully");
                    if (SearchNameName == null)
                    {
                        GetConnectedTablesById(); //refresh doubleClick                    
                    }
                    else
                    {
                        GetNameByName(); //search
                    }
                }
                else
                {
                    MessageBox.Show("Only " + CurrentTbl78Name.NameName+ " can be deleted");
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
            get { return _saveNameCommand ?? (_saveNameCommand = new RelayCommand(SaveName)); }
        }

        private void SaveName()
        {
            try
            {
                var name = Tbl78NamesRepository.Tbl78Names.FirstOrDefault(x => x.NameID== CurrentTbl78Name.NameID);
                if (CurrentTbl78Name == null)
                {
                    MessageBox.Show("name was not found");
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
                            PlSpeciesID= CurrentTbl78Name.PlSpeciesID,              
                            NameName= CurrentTbl78Name.NameName,              
                            CountID = TblCountersRepository.Counter(),
                            Valid = CurrentTbl78Name.Valid,
                            ValidYear = CurrentTbl78Name.ValidYear,
                            Synonym = CurrentTbl78Name.Synonym,
                            Author = CurrentTbl78Name.Author,
                            AuthorYear = CurrentTbl78Name.AuthorYear,
                            Info = CurrentTbl78Name.Info,
                            EngName = CurrentTbl78Name.EngName,
                            GerName = CurrentTbl78Name.GerName,
                            FraName = CurrentTbl78Name.FraName,
                            PorName = CurrentTbl78Name.PorName,
                            Writer = Environment.UserName,
                            WriterDate = DateTime.Now,
                            Updater = Environment.UserName,
                            UpdaterDate = DateTime.Now,
                            Memo = CurrentTbl78Name.Memo
                        });
                    }
                    {
                        Tbl78NamesRepository.Save();
                        MessageBox.Show(CurrentTbl78Name.NameName+  " was successfully saved ");
                        if (SearchNameName == null)
                        {
                            GetConnectedTablesById(); //refresh doubleClick                    
                        }
                        else
                        {
                            GetNameByName(); //search
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
    
  
        
        #region "Public Commands Connect ==> Tbl81Image"                 

        private RelayCommand _getImageByNameCommand;
        public ICommand GetImageByNameCommand
        {
            get { return _getImageByNameCommand ?? (_getImageByNameCommand = new RelayCommand(GetImageByName)); }
        }

        private void GetImageByName()
        {
            Tbl81ImagesList =
                new ObservableCollection<Tbl81Image>((from NULL in Tbl81ImagesRepository.Tbl81Images
                                                       where NULL.ImageName.StartsWith(SearchImageName)
                                                       orderby NULL.ImageName
                                                       select NULL));

            View = CollectionViewSource.GetDefaultView(Tbl81ImagesList);
            if (View != null)
                View.CurrentChanged += NULLView_CurrentChanged;                   
            RaisePropertyChanged("CurrentTbl81Image");
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
            Tbl81ImagesList.Add(new Tbl81Image{ ImageName= "New " });                   
            View = CollectionViewSource.GetDefaultView(Tbl81ImagesList);
            if (View != null)
                View.CurrentChanged += NULLView_CurrentChanged;
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
                var NULL= Tbl81ImagesRepository.Tbl81Images.FirstOrDefault(x => x.ImageID== CurrentTbl81Image.ImageID);
                if (NULL!= null)
                {
                    if (MessageBox.Show("Are you really shure to delete this "
                                        + CurrentTbl81Image.ImageName, "Question", MessageBoxButton.YesNo) !=
                        MessageBoxResult.Yes) return;
                    Tbl81ImagesRepository.Delete(NULL);
                    Tbl81ImagesRepository.Save();
                    MessageBox.Show(CurrentTbl81Image.ImageName+ " was deleted successfully");
                    if (SearchImageName == null)
                    {
                        GetConnectedTablesById(); //refresh doubleClick                    
                    }
                    else
                    {
                        GetImageByName(); //search
                    }
                }
                else
                {
                    MessageBox.Show("Only " + CurrentTbl81Image.ImageName+ " can be deleted");
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
                var NULL= Tbl81ImagesRepository.Tbl81Images.FirstOrDefault(x => x.ImageID== CurrentTbl81Image.ImageID);
                if (CurrentTbl81Image == null)
                {
                    MessageBox.Show("NULL was not found");
                }
                else
                {
                    if (CurrentTbl81Image.ImageID!= 0)
                    {
                        if (NULL!= null) //update
                        {
                            NULL.Updater = Environment.UserName;
                            NULL.UpdaterDate = DateTime.Now;
                        }
                    }
                    else
                    {
                        Tbl81ImagesRepository.Add(new Tbl81Image
                        {
                            PlSpeciesID= CurrentTbl81Image.PlSpeciesID,              
                            ImageName= CurrentTbl81Image.ImageName,              
                            CountID = TblCountersRepository.Counter(),
                            Valid = CurrentTbl81Image.Valid,
                            ValidYear = CurrentTbl81Image.ValidYear,
                            Synonym = CurrentTbl81Image.Synonym,
                            Author = CurrentTbl81Image.Author,
                            AuthorYear = CurrentTbl81Image.AuthorYear,
                            Info = CurrentTbl81Image.Info,
                            EngName = CurrentTbl81Image.EngName,
                            GerName = CurrentTbl81Image.GerName,
                            FraName = CurrentTbl81Image.FraName,
                            PorName = CurrentTbl81Image.PorName,
                            Writer = Environment.UserName,
                            WriterDate = DateTime.Now,
                            Updater = Environment.UserName,
                            UpdaterDate = DateTime.Now,
                            Memo = CurrentTbl81Image.Memo
                        });
                    }
                    {
                        Tbl81ImagesRepository.Save();
                        MessageBox.Show(CurrentTbl81Image.ImageName+  " was successfully saved ");              
                        if (SearchImageName == null)
                        {
                            GetConnectedTablesById(); //refresh doubleClick                    
                        }
                        else
                        {
                            GetImageByName(); //search
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
                            PlSpeciesID= CurrentTbl90RefAuthor.PlSpeciesID,
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
                            PlSpeciesID= CurrentTbl90RefSource.PlSpeciesID,
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
                            PlSpeciesID= CurrentTbl90RefExpert.PlSpeciesID,
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
                            PlSpeciesID= CurrentTbl93Comment.PlSpeciesID,                
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
            SearchGenusName = null;                       
            SearchNameName = null;
            SearchCommentInfo = null;
            SearchRefExpertName = null;
            SearchRefSourceName = null;
            SearchRefAuthorName = null;

            Tbl66GenussesList =
                new ObservableCollection<Tbl66Genus>((from genus in Tbl66GenussesRepository.Tbl66Genusses
                                                       where genus.GenusID== CurrentTbl72PlSpecies.GenusID
                                                       orderby genus.GenusName
                                                       select genus));

            View = CollectionViewSource.GetDefaultView(Tbl66GenussesList);
            if (View != null)
                View.CurrentChanged += tbl66GenusView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl66Genusses");
            //-----------------------------------------------------------------------------------
            Tbl78NamesList =
                new ObservableCollection<Tbl78Name>((from name in Tbl78NamesRepository.Tbl78Names
                                                       where name.PlSpeciesID== CurrentTbl72PlSpecies.PlSpeciesID
                                                       orderby name.Tbl72PlSpeciesses.PlSpeciesName
                                                       select name));


            View = CollectionViewSource.GetDefaultView(Tbl78NamesList);
            if (View != null)
                View.CurrentChanged += tbl78NameView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl78Names");
            //-----------------------------------------------------------------------------------
            Tbl90RefAuthorsList =
                new ObservableCollection<Tbl90Reference>((from refAu in Tbl90ReferencesRepository.Tbl90References
                                                          where refAu.PlSpeciesID== CurrentTbl72PlSpecies.PlSpeciesID
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
                                                          where refSo.PlSpeciesID== CurrentTbl72PlSpecies.PlSpeciesID
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
                                                          where refEx.PlSpeciesID== CurrentTbl72PlSpecies.PlSpeciesID
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
                                                          where comm.PlSpeciesID== CurrentTbl72PlSpecies.PlSpeciesID
                                                        orderby comm.Info
                                                        select comm));

            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            if (CommentsView != null)
                CommentsView.CurrentChanged += tbl93CommentView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl93Comment");
            //--------------------------------------------------------------

        }

        #endregion "Public Commands Connected Tables by DoubleClick"
   

     
        #region "Public Properties Tbl72PlSpecies"

        public new ICollectionView View;
        public new Tbl72PlSpecies CurrentTbl72PlSpecies
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
        public new string SearchPlSpeciesName
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
        public new ObservableCollection<Tbl72PlSpecies> Tbl72PlSpeciessesList
        {
            get { return _tbl72PlSpeciessesList; }
            set
            {
                if (_tbl72PlSpeciessesList == value) return;
                _tbl72PlSpeciessesList = value;
                RaisePropertyChanged("Tbl72PlSpeciessesList");

                //Clear Search-TextBox
                SearchGenusName = null;                                
                SearchNameName = null;
                SearchCommentInfo = null;
                SearchRefExpertName = null;
                SearchRefSourceName = null;
                SearchRefAuthorName = null;
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

        #endregion "Public Properties"
   

       
        #region "Public Properties Tbl66Genus"

        public new ICollectionView View;
        public new Tbl66Genus CurrentTbl66Genus
        {
            get
            {
                if (View != null)
                    return View.CurrentItem as Tbl66Genus;
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
            }
        }

        #endregion "Public Properties"
   
  
       
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
            }
        }

        #endregion "Public Properties"
   
  
       
        #region "Public Properties Tbl78Name"

        public ICollectionView View;
        public Tbl78Name CurrentTbl78Name
        {
            get
            {
                if (View != null)
                    return View.CurrentItem as Tbl78Name;
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

        public ICollectionView View;
        public Tbl81Image CurrentTbl81Image
        {
            get
            {
                if (View != null)
                    return View.CurrentItem as Tbl81Image;
                return null;
            }
        }
        //--------------------------------------------                                               

        private string _searchImageName;
        public string SearchImageName
        {
            get { return _searchImageName; }
            set
            {
                if (value == _searchImageName) return;
                _searchImageName = value;
                RaisePropertyChanged("SearchImageName");
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
   
          
        #region "Private Methods"

        public void tbl78NameView_CurrentChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged("CurrentTbl78Name");
        }
        #endregion "Private Methods"
   
   

   
    }
}   
