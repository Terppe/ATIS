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

//    Tbl63InfratribussesViewModel Skriptdatum:  01.04.2014  10:32    

namespace WPFUI.Views.Database
{   


    
    public class Tbl63InfratribussesViewModel : Tbl60SubtribussesViewModel                     
    {     
         
      #region "Private Data Members"

        protected readonly Tbl66GenussesRepository Tbl66GenussesRepository = new Tbl66GenussesRepository();   
          
          #endregion "Private Data Members"            
    
        #region "Constructor"

        public Tbl63InfratribussesViewModel()
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

       
        #region "Public Commands Basic Tbl63Infratribus"

        private RelayCommand _getInfratribusByNameCommand;
        public new ICommand GetInfratribusByNameCommand
        {
            get { return _getInfratribusByNameCommand ?? (_getInfratribusByNameCommand = new RelayCommand(delegate { GetInfratribusByNameOrId(null); })); }   
        }

        private void GetInfratribusByNameOrId(object o)       
        {   
Tbl63InfratribussesList =  new ObservableCollection<Tbl63Infratribus>
                                                       (from x in Tbl63InfratribussesRepository.Tbl63Infratribusses
                                                        where x.InfratribusName.StartsWith(SearchInfratribusName)
                                                        orderby x.InfratribusName
                                                        select x);

            Tbl63InfratribussesAllList =  new ObservableCollection<Tbl63Infratribus>
                                                       (from y in Tbl63InfratribussesRepository.Tbl63Infratribusses
                                                        orderby y.InfratribusName
                                                        select y);

            Tbl60SubtribussesAllList =  new ObservableCollection<Tbl60Subtribus>
                                                       (from z in Tbl60SubtribussesRepository.Tbl60Subtribusses
                                                        orderby z.SubtribusName
                                                        select z);

              
  Tbl57TribussesAllList =  new ObservableCollection<Tbl57Tribus>
                                                       (from z in Tbl57TribussesRepository.Tbl57Tribusses
                                                        orderby z.TribusName
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

  
Tbl60SubtribussesList = null;                  
  Tbl66GenussesList = null;               
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

        private void AddInfratribus(object o)
        {
            if (Tbl63InfratribussesList == null)
                Tbl63InfratribussesList = new ObservableCollection<Tbl63Infratribus>();
            Tbl63InfratribussesList.Add(new Tbl63Infratribus{ InfratribusName= CultRes.StringsRes.DatasetNew });
            InfratribussesView = CollectionViewSource.GetDefaultView(Tbl63InfratribussesList);
            if (InfratribussesView != null)
                InfratribussesView.CurrentChanged += tbl63InfratribusView_CurrentChanged;
            RaisePropertyChanged();
        }
        //---------------------------------------------------------------------------------------
  
       
        private RelayCommand _deleteInfratribusCommand;
        public new ICommand DeleteInfratribusCommand
        {
            get { return _deleteInfratribusCommand ?? (_deleteInfratribusCommand = new RelayCommand(delegate { DeleteInfratribus(null); })); }
        }

        private void DeleteInfratribus(object o)
        {
            try
            {
                var infratribus= Tbl63InfratribussesRepository.Tbl63Infratribusses.FirstOrDefault(x => x.InfratribusID== CurrentTbl63Infratribus.InfratribusID);
                if (infratribus!= null)
                {
                    if (MessageBox.Show(CultRes.StringsRes.DeleteQuestion
                                        + " " +  CurrentTbl63Infratribus.InfratribusName, CultRes.StringsRes.DeleteQuestion1, MessageBoxButton.YesNo) !=
                        MessageBoxResult.Yes) return;
                    Tbl63InfratribussesRepository.Delete(infratribus);
                    Tbl63InfratribussesRepository.Save();
                    MessageBox.Show(CurrentTbl63Infratribus.InfratribusName + " " + CultRes.StringsRes.DeleteSuccess);
                    GetInfratribusByNameOrId(o); //Refresh
                }
                else
                {
                    MessageBox.Show(CultRes.StringsRes.DeleteCan + " " + CurrentTbl63Infratribus.InfratribusName+ " " + CultRes.StringsRes.DeleteCan1);
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
            get { return _saveInfratribusCommand ?? (_saveInfratribusCommand = new RelayCommand(delegate { SaveInfratribus(null); })); }
        }

        private void SaveInfratribus(object o)
        {
            try
            {
                var infratribus= Tbl63InfratribussesRepository.Tbl63Infratribusses.FirstOrDefault(x => x.InfratribusID== CurrentTbl63Infratribus.InfratribusID);
                if (CurrentTbl63Infratribus == null)
                {
                    MessageBox.Show(CultRes.StringsRes.DatasetNotExist);
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
                        Tbl63InfratribussesRepository.Add(new Tbl63Infratribus
                        {
                            SubtribusID= CurrentTbl63Infratribus.SubtribusID,              
                            InfratribusName= CurrentTbl63Infratribus.InfratribusName,              
                            CountID = RandomHelper.Randomnumber(),
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
                        MessageBox.Show(CurrentTbl63Infratribus.InfratribusName+ " " + CultRes.StringsRes.SaveSuccess);
                        GetInfratribusByNameOrId(o);  //Refresh
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


        
        #region "Public Commands Connect <== Tbl60Subtribus"                 

        private RelayCommand _getSubtribusByNameCommand;
        public new ICommand GetSubtribusByNameCommand
        {
            get { return _getSubtribusByNameCommand ?? (_getSubtribusByNameCommand = new RelayCommand(delegate { GetSubtribusByNameOrId(null); })); }
        }

        private void GetSubtribusByNameOrId(object o)
        {
            Tbl60SubtribussesList =
                new ObservableCollection<Tbl60Subtribus>((from subtribus in Tbl60SubtribussesRepository.Tbl60Subtribusses
                                                       where subtribus.SubtribusName.StartsWith(SearchSubtribusName)
                                                       orderby subtribus.SubtribusName
                                                       select subtribus));

            SubtribussesView = CollectionViewSource.GetDefaultView(Tbl60SubtribussesList);
            if (SubtribussesView != null)
                SubtribussesView.CurrentChanged += tbl60SubtribusView_CurrentChanged;                   
            RaisePropertyChanged();
        }
        //------------------------------------------------------------------------------
        private RelayCommand _addSubtribusCommand;
        public new ICommand AddSubtribusCommand
        {
            get { return _addSubtribusCommand ?? (_addSubtribusCommand = new RelayCommand(AddSubtribus)); }
        }

        private void AddSubtribus()
        {
            if (Tbl60SubtribussesList == null)
                Tbl60SubtribussesList = new ObservableCollection<Tbl60Subtribus>();
            Tbl60SubtribussesList.Add(new Tbl60Subtribus{ SubtribusName= "New " });                   
            SubtribussesView = CollectionViewSource.GetDefaultView(Tbl60SubtribussesList);
            if (SubtribussesView != null)
                SubtribussesView.CurrentChanged += tbl60SubtribusView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl60Subtribus");
        }
        //----------------------------------------------------------------------------------------------------------
        private RelayCommand _deleteSubtribusCommand;
        public ICommand SubtribusPhylumCommand
        {
            get { return _deleteSubtribusCommand ?? (_deleteSubtribusCommand = new RelayCommand(DeleteSubtribus)); }
        }

        private void DeleteSubtribus()
        {
            try
            {
                var subtribus= Tbl60SubtribussesRepository.Tbl60Subtribusses.FirstOrDefault(x => x.SubtribusID== CurrentTbl60Subtribus.SubtribusID);
                if (subtribus!= null)
                {
                    if (MessageBox.Show("Are you really shure to delete this "
                                        + CurrentTbl60Subtribus.SubtribusName, "Question", MessageBoxButton.YesNo) !=
                        MessageBoxResult.Yes) return;
                    Tbl60SubtribussesRepository.Delete(subtribus);
                    Tbl60SubtribussesRepository.Save();
                    MessageBox.Show(CurrentTbl60Subtribus.SubtribusName+ " was deleted successfully");
                    if (SearchSubtribusName == null)
                    {
                        GetConnectedTablesById(); //refresh doubleClick                    
                    }
                    else
                    {
                        GetSubtribusByName(); //search
                    }
                }
                else
                {
                    MessageBox.Show("Only " + CurrentTbl60Subtribus.SubtribusName+ " can be deleted");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //-------------------------------------------------------------------------------------------------
        private RelayCommand _saveSubtribusCommand;   
        public new ICommand SaveSubtribusCommand
        {
            get { return _saveSubtribusCommand ?? (_saveSubtribusCommand = new RelayCommand(SaveSubtribus)); }
        }

        private void SaveSubtribus()
        {
            try
            {
                var subtribus= Tbl60SubtribussesRepository.Tbl60Subtribusses.FirstOrDefault(x => x.SubtribusID== CurrentTbl60Subtribus.SubtribusID);
                if (CurrentTbl60Subtribus == null)
                {
                    MessageBox.Show("subtribus was not found");
                }
                else
                {
                    if (CurrentTbl60Subtribus.SubtribusID!= 0)
                    {
                        if (subtribus!= null) //update
                        {
                            subtribus.Updater = Environment.UserName;
                            subtribus.UpdaterDate = DateTime.Now;
                        }
                    }
                    else
                    {
                        Tbl60SubtribussesRepository.Add(new Tbl60Subtribus()
                        {
                            SubtribusName= CurrentTbl60Subtribus.SubtribusName,              
                            CountID = TblCountersRepository.Counter(),
                            Valid = CurrentTbl60Subtribus.Valid,
                            ValidYear = CurrentTbl60Subtribus.ValidYear,
                            Synonym = CurrentTbl60Subtribus.Synonym,
                            Author = CurrentTbl60Subtribus.Author,
                            AuthorYear = CurrentTbl60Subtribus.AuthorYear,
                            Info = CurrentTbl60Subtribus.Info,
                            EngName = CurrentTbl60Subtribus.EngName,
                            GerName = CurrentTbl60Subtribus.GerName,
                            FraName = CurrentTbl60Subtribus.FraName,
                            PorName = CurrentTbl60Subtribus.PorName,
                            Writer = Environment.UserName,
                            WriterDate = DateTime.Now,
                            Updater = Environment.UserName,
                            UpdaterDate = DateTime.Now,
                            Memo = CurrentTbl60Subtribus.Memo
                        });
                        }
                        {
                            Tbl60SubtribussesRepository.Save();
                            MessageBox.Show(CurrentTbl60Subtribus.SubtribusName+  " was successfully saved ");
                            GetSubtribusByName();  //Refresh
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


        
        #region "Public Commands Connect ==> Tbl66Genus"                 

        private RelayCommand _getGenusByNameCommand;
        public ICommand GetGenusByNameCommand
        {
            get { return _getGenusByNameCommand ?? (_getGenusByNameCommand = new RelayCommand(delegate { GetGenusByNameOrId(null); })); }
        }

        private void GetGenusByNameOrId(object o)
        {
            int id;
            if (int.TryParse(SearchGenusName, out id))
                Tbl66GenussesList = new ObservableCollection<Tbl66Genus> { Tbl66GenussesRepository.Get(id) };
            else
            Tbl66GenussesList =  new ObservableCollection<Tbl66Genus>
                                                      (from x in Tbl66GenussesRepository.FindAll()
                                                       where x.GenusName.StartsWith(SearchGenusName)
                                                       orderby x.GenusName
                                                       select x);

            GenussesView = CollectionViewSource.GetDefaultView(Tbl66GenussesList);
            if (GenussesView != null)
                GenussesView.CurrentChanged += tbl66GenusView_CurrentChanged;                   
            RaisePropertyChanged();
        }
        //------------------------------------------------------------------------------
        private RelayCommand _addGenusCommand;
        public ICommand AddGenusCommand
        {
            get { return _addGenusCommand ?? (_addGenusCommand = new RelayCommand(delegate { AddGenus(null); })); }
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
        public ICommand DeleteGenusCommand
        {
            get { return _deleteGenusCommand ?? (_deleteGenusCommand = new RelayCommand(delegate { DeleteGenus(null); })); }
        }

        private void DeleteGenus(object o)
        {
            try
            {
                var genus = Tbl66GenussesRepository.Tbl66Genusses.FirstOrDefault(x => x.GenusID== CurrentTbl66Genus.GenusID);
                if (genus != null)
                {
                    if (MessageBox.Show(CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl66Genus.GenusName, CultRes.StringsRes.DeleteQuestion1, MessageBoxButton.YesNo) !=
                        MessageBoxResult.Yes) return;
                    Tbl66GenussesRepository.Delete(genus);
                    Tbl66GenussesRepository.Save();
                    MessageBox.Show(CurrentTbl66Genus.GenusName + " " + CultRes.StringsRes.DeleteSuccess);
                    GetGenusByNameOrId(o);  //Refresh
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
        public ICommand SaveGenusCommand
        {
            get { return _saveGenusCommand ?? (_saveGenusCommand = new RelayCommand(delegate { SaveGenus(null); })); }
        }

        private void SaveGenus(object o)
        {
            try
            {
                var genus = Tbl66GenussesRepository.Tbl66Genusses.FirstOrDefault(x => x.GenusID== CurrentTbl66Genus.GenusID);
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
                        if (CurrentTbl66Genus != null)
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
                            InfratribusID= CurrentTbl90RefAuthor.InfratribusID,
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
                            InfratribusID= CurrentTbl90RefSource.InfratribusID,
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
                            InfratribusID= CurrentTbl90RefExpert.InfratribusID,
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
                            InfratribusID= CurrentTbl93Comment.InfratribusID,                
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
            SearchSubtribusName = null;                       
            SearchGenusName = null;
            SearchCommentInfo = null;
            SearchRefExpertName = null;
            SearchRefSourceName = null;
            SearchRefAuthorName = null;

            Tbl60SubtribussesList =
                new ObservableCollection<Tbl60Subtribus>((from subtribus in Tbl60SubtribussesRepository.Tbl60Subtribusses
                                                       where subtribus.SubtribusID== CurrentTbl63Infratribus.SubtribusID
                                                       orderby subtribus.SubtribusName
                                                       select subtribus));

            SubtribussesView = CollectionViewSource.GetDefaultView(Tbl60SubtribussesList);
            if (SubtribussesView != null)
                SubtribussesView.CurrentChanged += tbl60SubtribusView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl60Subtribusses");
            //-----------------------------------------------------------------------------------
            Tbl66GenussesList =
                new ObservableCollection<Tbl66Genus>((from genus in Tbl66GenussesRepository.Tbl66Genusses
                                                       where genus.InfratribusID== CurrentTbl63Infratribus.InfratribusID
                                                       orderby genus.Tbl63Infratribusses.InfratribusName
                                                       select genus));


            GenussesView = CollectionViewSource.GetDefaultView(Tbl66GenussesList);
            if (GenussesView != null)
                GenussesView.CurrentChanged += tbl66GenusView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl66Genusses");
            //-----------------------------------------------------------------------------------
            Tbl90RefAuthorsList =
                new ObservableCollection<Tbl90Reference>((from refAu in Tbl90ReferencesRepository.Tbl90References
                                                          where refAu.InfratribusID== CurrentTbl63Infratribus.InfratribusID
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
                                                          where refSo.InfratribusID== CurrentTbl63Infratribus.InfratribusID
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
                                                          where refEx.InfratribusID== CurrentTbl63Infratribus.InfratribusID
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
                                                          where comm.InfratribusID== CurrentTbl63Infratribus.InfratribusID
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



     
        #region "Public Properties Tbl63Infratribus"

        public new ICollectionView InfratribussesView;
        public new Tbl63Infratribus CurrentTbl63Infratribus
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
        public new string SearchInfratribusName
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
        public new ObservableCollection<Tbl63Infratribus> Tbl63InfratribussesList
        {
            get { return _tbl63InfratribussesList; }
            set
            {
                if (_tbl63InfratribussesList == value) return;
                _tbl63InfratribussesList = value;
                RaisePropertyChanged("Tbl63InfratribussesList");

                //Clear Search-TextBox
                SearchSubtribusName = null;                                
                SearchGenusName = null;
                SearchCommentInfo = null;
                SearchRefExpertName = null;
                SearchRefSourceName = null;
                SearchRefAuthorName = null;
            }
        }

        private ObservableCollection<Tbl63Infratribus> _tbl63InfratribussesAllList;
        public ObservableCollection<Tbl63Infratribus> Tbl63InfratribussesAllList
        {
            get { return _tbl63InfratribussesAllList; }
            set
            {
                if (_tbl63InfratribussesAllList == value) return;
                _tbl63InfratribussesAllList = value;
                RaisePropertyChanged("Tbl63InfratribussesAllList");
            }
        }

        #endregion "Public Properties"   

       
        #region "Public Properties Tbl60Subtribus"

        public  ICollectionView SubtribussesView;
        public  Tbl60Subtribus CurrentTbl60Subtribus
        {
            get
            {
                if (SubtribussesView != null)
                    return SubtribussesView.CurrentItem as Tbl60Subtribus;
                return null;
            }
        }
        //--------------------------------------------                                               

        private string _searchSubtribusName;
        public  string SearchSubtribusName
        {
            get { return _searchSubtribusName; }
            set
            {
                if (value == _searchSubtribusName) return;
                _searchSubtribusName = value;
                RaisePropertyChanged("SearchSubtribusName");
            }
        }

        private ObservableCollection<Tbl60Subtribus> _tbl60SubtribussesList;
        public  ObservableCollection<Tbl60Subtribus> Tbl60SubtribussesList
        {
            get { return _tbl60SubtribussesList; }
            set
            {
                if (_tbl60SubtribussesList == value) return;
                _tbl60SubtribussesList = value;
                RaisePropertyChanged("Tbl60SubtribussesList");
            }
        }

        #endregion "Public Properties"
   
  
       
        #region "Public Properties Tbl66Genus"

        public ICollectionView GenussesView;
        public Tbl66Genus CurrentTbl66Genus
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
        public string SearchGenusName
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
        public ObservableCollection<Tbl66Genus> Tbl66GenussesList
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
   
   

 //    Part 11    

      }
}   
