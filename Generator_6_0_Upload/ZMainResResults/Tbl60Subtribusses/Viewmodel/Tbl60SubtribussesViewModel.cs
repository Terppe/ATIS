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

//    Tbl60SubtribussesViewModel Skriptdatum:  01.04.2014  10:32    

namespace WPFUI.Views.Database
{   


    
    public class Tbl60SubtribussesViewModel : Tbl57TribussesViewModel                     
    {     
         
      #region "Private Data Members"

        protected readonly Tbl63InfratribussesRepository Tbl63InfratribussesRepository = new Tbl63InfratribussesRepository();   
          
          #endregion "Private Data Members"            
    
        #region "Constructor"

        public Tbl60SubtribussesViewModel()
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

       
        #region "Public Commands Basic Tbl60Subtribus"

        private RelayCommand _getSubtribusByNameCommand;
        public new ICommand GetSubtribusByNameCommand
        {
            get { return _getSubtribusByNameCommand ?? (_getSubtribusByNameCommand = new RelayCommand(delegate { GetSubtribusByNameOrId(null); })); }   
        }

        private void GetSubtribusByNameOrId(object o)       
        {   
Tbl60SubtribussesList =  new ObservableCollection<Tbl60Subtribus>
                                                       (from x in Tbl60SubtribussesRepository.Tbl60Subtribusses
                                                        where x.SubtribusName.StartsWith(SearchSubtribusName)
                                                        orderby x.SubtribusName
                                                        select x);

            Tbl60SubtribussesAllList =  new ObservableCollection<Tbl60Subtribus>
                                                       (from y in Tbl60SubtribussesRepository.Tbl60Subtribusses
                                                        orderby y.SubtribusName
                                                        select y);

            Tbl57TribussesAllList =  new ObservableCollection<Tbl57Tribus>
                                                       (from z in Tbl57TribussesRepository.Tbl57Tribusses
                                                        orderby z.TribusName
                                                        select z);

              
  Tbl54SupertribussesAllList =  new ObservableCollection<Tbl54Supertribus>
                                                       (from z in Tbl54SupertribussesRepository.Tbl54Supertribusses
                                                        orderby z.SupertribusName
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

  
Tbl57TribussesList = null;                  
  Tbl63InfratribussesList = null;               
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

        private void AddSubtribus(object o)
        {
            if (Tbl60SubtribussesList == null)
                Tbl60SubtribussesList = new ObservableCollection<Tbl60Subtribus>();
            Tbl60SubtribussesList.Add(new Tbl60Subtribus{ SubtribusName= CultRes.StringsRes.DatasetNew });
            SubtribussesView = CollectionViewSource.GetDefaultView(Tbl60SubtribussesList);
            if (SubtribussesView != null)
                SubtribussesView.CurrentChanged += tbl60SubtribusView_CurrentChanged;
            RaisePropertyChanged();
        }
        //---------------------------------------------------------------------------------------
  
       
        private RelayCommand _deleteSubtribusCommand;
        public new ICommand DeleteSubtribusCommand
        {
            get { return _deleteSubtribusCommand ?? (_deleteSubtribusCommand = new RelayCommand(delegate { DeleteSubtribus(null); })); }
        }

        private void DeleteSubtribus(object o)
        {
            try
            {
                var subtribus= Tbl60SubtribussesRepository.Tbl60Subtribusses.FirstOrDefault(x => x.SubtribusID== CurrentTbl60Subtribus.SubtribusID);
                if (subtribus!= null)
                {
                    if (MessageBox.Show(CultRes.StringsRes.DeleteQuestion
                                        + " " +  CurrentTbl60Subtribus.SubtribusName, CultRes.StringsRes.DeleteQuestion1, MessageBoxButton.YesNo) !=
                        MessageBoxResult.Yes) return;
                    Tbl60SubtribussesRepository.Delete(subtribus);
                    Tbl60SubtribussesRepository.Save();
                    MessageBox.Show(CurrentTbl60Subtribus.SubtribusName + " " + CultRes.StringsRes.DeleteSuccess);
                    GetSubtribusByNameOrId(o); //Refresh
                }
                else
                {
                    MessageBox.Show(CultRes.StringsRes.DeleteCan + " " + CurrentTbl60Subtribus.SubtribusName+ " " + CultRes.StringsRes.DeleteCan1);
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
            get { return _saveSubtribusCommand ?? (_saveSubtribusCommand = new RelayCommand(delegate { SaveSubtribus(null); })); }
        }

        private void SaveSubtribus(object o)
        {
            try
            {
                var subtribus= Tbl60SubtribussesRepository.Tbl60Subtribusses.FirstOrDefault(x => x.SubtribusID== CurrentTbl60Subtribus.SubtribusID);
                if (CurrentTbl60Subtribus == null)
                {
                    MessageBox.Show(CultRes.StringsRes.DatasetNotExist);
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
                        Tbl60SubtribussesRepository.Add(new Tbl60Subtribus
                        {
                            TribusID= CurrentTbl60Subtribus.TribusID,              
                            SubtribusName= CurrentTbl60Subtribus.SubtribusName,              
                            CountID = RandomHelper.Randomnumber(),
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
                        MessageBox.Show(CurrentTbl60Subtribus.SubtribusName+ " " + CultRes.StringsRes.SaveSuccess);
                        GetSubtribusByNameOrId(o);  //Refresh
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


        
        #region "Public Commands Connect <== Tbl57Tribus"                 

        private RelayCommand _getTribusByNameCommand;
        public new ICommand GetTribusByNameCommand
        {
            get { return _getTribusByNameCommand ?? (_getTribusByNameCommand = new RelayCommand(delegate { GetTribusByNameOrId(null); })); }
        }

        private void GetTribusByNameOrId(object o)
        {
            Tbl57TribussesList =
                new ObservableCollection<Tbl57Tribus>((from tribus in Tbl57TribussesRepository.Tbl57Tribusses
                                                       where tribus.TribusName.StartsWith(SearchTribusName)
                                                       orderby tribus.TribusName
                                                       select tribus));

            TribussesView = CollectionViewSource.GetDefaultView(Tbl57TribussesList);
            if (TribussesView != null)
                TribussesView.CurrentChanged += tbl57TribusView_CurrentChanged;                   
            RaisePropertyChanged();
        }
        //------------------------------------------------------------------------------
        private RelayCommand _addTribusCommand;
        public new ICommand AddTribusCommand
        {
            get { return _addTribusCommand ?? (_addTribusCommand = new RelayCommand(AddTribus)); }
        }

        private void AddTribus()
        {
            if (Tbl57TribussesList == null)
                Tbl57TribussesList = new ObservableCollection<Tbl57Tribus>();
            Tbl57TribussesList.Add(new Tbl57Tribus{ TribusName= "New " });                   
            TribussesView = CollectionViewSource.GetDefaultView(Tbl57TribussesList);
            if (TribussesView != null)
                TribussesView.CurrentChanged += tbl57TribusView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl57Tribus");
        }
        //----------------------------------------------------------------------------------------------------------
        private RelayCommand _deleteTribusCommand;
        public ICommand TribusPhylumCommand
        {
            get { return _deleteTribusCommand ?? (_deleteTribusCommand = new RelayCommand(DeleteTribus)); }
        }

        private void DeleteTribus()
        {
            try
            {
                var tribus= Tbl57TribussesRepository.Tbl57Tribusses.FirstOrDefault(x => x.TribusID== CurrentTbl57Tribus.TribusID);
                if (tribus!= null)
                {
                    if (MessageBox.Show("Are you really shure to delete this "
                                        + CurrentTbl57Tribus.TribusName, "Question", MessageBoxButton.YesNo) !=
                        MessageBoxResult.Yes) return;
                    Tbl57TribussesRepository.Delete(tribus);
                    Tbl57TribussesRepository.Save();
                    MessageBox.Show(CurrentTbl57Tribus.TribusName+ " was deleted successfully");
                    if (SearchTribusName == null)
                    {
                        GetConnectedTablesById(); //refresh doubleClick                    
                    }
                    else
                    {
                        GetTribusByName(); //search
                    }
                }
                else
                {
                    MessageBox.Show("Only " + CurrentTbl57Tribus.TribusName+ " can be deleted");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //-------------------------------------------------------------------------------------------------
        private RelayCommand _saveTribusCommand;   
        public new ICommand SaveTribusCommand
        {
            get { return _saveTribusCommand ?? (_saveTribusCommand = new RelayCommand(SaveTribus)); }
        }

        private void SaveTribus()
        {
            try
            {
                var tribus= Tbl57TribussesRepository.Tbl57Tribusses.FirstOrDefault(x => x.TribusID== CurrentTbl57Tribus.TribusID);
                if (CurrentTbl57Tribus == null)
                {
                    MessageBox.Show("tribus was not found");
                }
                else
                {
                    if (CurrentTbl57Tribus.TribusID!= 0)
                    {
                        if (tribus!= null) //update
                        {
                            tribus.Updater = Environment.UserName;
                            tribus.UpdaterDate = DateTime.Now;
                        }
                    }
                    else
                    {
                        Tbl57TribussesRepository.Add(new Tbl57Tribus()
                        {
                            TribusName= CurrentTbl57Tribus.TribusName,              
                            CountID = TblCountersRepository.Counter(),
                            Valid = CurrentTbl57Tribus.Valid,
                            ValidYear = CurrentTbl57Tribus.ValidYear,
                            Synonym = CurrentTbl57Tribus.Synonym,
                            Author = CurrentTbl57Tribus.Author,
                            AuthorYear = CurrentTbl57Tribus.AuthorYear,
                            Info = CurrentTbl57Tribus.Info,
                            EngName = CurrentTbl57Tribus.EngName,
                            GerName = CurrentTbl57Tribus.GerName,
                            FraName = CurrentTbl57Tribus.FraName,
                            PorName = CurrentTbl57Tribus.PorName,
                            Writer = Environment.UserName,
                            WriterDate = DateTime.Now,
                            Updater = Environment.UserName,
                            UpdaterDate = DateTime.Now,
                            Memo = CurrentTbl57Tribus.Memo
                        });
                        }
                        {
                            Tbl57TribussesRepository.Save();
                            MessageBox.Show(CurrentTbl57Tribus.TribusName+  " was successfully saved ");
                            GetTribusByName();  //Refresh
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


        
        #region "Public Commands Connect ==> Tbl63Infratribus"                 

        private RelayCommand _getInfratribusByNameCommand;
        public ICommand GetInfratribusByNameCommand
        {
            get { return _getInfratribusByNameCommand ?? (_getInfratribusByNameCommand = new RelayCommand(delegate { GetInfratribusByNameOrId(null); })); }
        }

        private void GetInfratribusByNameOrId(object o)
        {
            int id;
            if (int.TryParse(SearchInfratribusName, out id))
                Tbl63InfratribussesList = new ObservableCollection<Tbl63Infratribus> { Tbl63InfratribussesRepository.Get(id) };
            else
            Tbl63InfratribussesList =  new ObservableCollection<Tbl63Infratribus>
                                                      (from x in Tbl63InfratribussesRepository.FindAll()
                                                       where x.InfratribusName.StartsWith(SearchInfratribusName)
                                                       orderby x.InfratribusName
                                                       select x);

            InfratribussesView = CollectionViewSource.GetDefaultView(Tbl63InfratribussesList);
            if (InfratribussesView != null)
                InfratribussesView.CurrentChanged += tbl63InfratribusView_CurrentChanged;                   
            RaisePropertyChanged();
        }
        //------------------------------------------------------------------------------
        private RelayCommand _addInfratribusCommand;
        public ICommand AddInfratribusCommand
        {
            get { return _addInfratribusCommand ?? (_addInfratribusCommand = new RelayCommand(delegate { AddInfratribus(null); })); }
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
        public ICommand DeleteInfratribusCommand
        {
            get { return _deleteInfratribusCommand ?? (_deleteInfratribusCommand = new RelayCommand(delegate { DeleteInfratribus(null); })); }
        }

        private void DeleteInfratribus(object o)
        {
            try
            {
                var infratribus = Tbl63InfratribussesRepository.Tbl63Infratribusses.FirstOrDefault(x => x.InfratribusID== CurrentTbl63Infratribus.InfratribusID);
                if (infratribus != null)
                {
                    if (MessageBox.Show(CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl63Infratribus.InfratribusName, CultRes.StringsRes.DeleteQuestion1, MessageBoxButton.YesNo) !=
                        MessageBoxResult.Yes) return;
                    Tbl63InfratribussesRepository.Delete(infratribus);
                    Tbl63InfratribussesRepository.Save();
                    MessageBox.Show(CurrentTbl63Infratribus.InfratribusName + " " + CultRes.StringsRes.DeleteSuccess);
                    GetInfratribusByNameOrId(o);  //Refresh
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
        public ICommand SaveInfratribusCommand
        {
            get { return _saveInfratribusCommand ?? (_saveInfratribusCommand = new RelayCommand(delegate { SaveInfratribus(null); })); }
        }

        private void SaveInfratribus(object o)
        {
            try
            {
                var infratribus = Tbl63InfratribussesRepository.Tbl63Infratribusses.FirstOrDefault(x => x.InfratribusID== CurrentTbl63Infratribus.InfratribusID);
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
                        if (CurrentTbl63Infratribus != null)
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
                            SubtribusID= CurrentTbl90RefAuthor.SubtribusID,
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
                            SubtribusID= CurrentTbl90RefSource.SubtribusID,
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
                            SubtribusID= CurrentTbl90RefExpert.SubtribusID,
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
                            SubtribusID= CurrentTbl93Comment.SubtribusID,                
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
            SearchTribusName = null;                       
            SearchInfratribusName = null;
            SearchCommentInfo = null;
            SearchRefExpertName = null;
            SearchRefSourceName = null;
            SearchRefAuthorName = null;

            Tbl57TribussesList =
                new ObservableCollection<Tbl57Tribus>((from tribus in Tbl57TribussesRepository.Tbl57Tribusses
                                                       where tribus.TribusID== CurrentTbl60Subtribus.TribusID
                                                       orderby tribus.TribusName
                                                       select tribus));

            TribussesView = CollectionViewSource.GetDefaultView(Tbl57TribussesList);
            if (TribussesView != null)
                TribussesView.CurrentChanged += tbl57TribusView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl57Tribusses");
            //-----------------------------------------------------------------------------------
            Tbl63InfratribussesList =
                new ObservableCollection<Tbl63Infratribus>((from infratribus in Tbl63InfratribussesRepository.Tbl63Infratribusses
                                                       where infratribus.SubtribusID== CurrentTbl60Subtribus.SubtribusID
                                                       orderby infratribus.Tbl60Subtribusses.SubtribusName
                                                       select infratribus));


            InfratribussesView = CollectionViewSource.GetDefaultView(Tbl63InfratribussesList);
            if (InfratribussesView != null)
                InfratribussesView.CurrentChanged += tbl63InfratribusView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl63Infratribusses");
            //-----------------------------------------------------------------------------------
            Tbl90RefAuthorsList =
                new ObservableCollection<Tbl90Reference>((from refAu in Tbl90ReferencesRepository.Tbl90References
                                                          where refAu.SubtribusID== CurrentTbl60Subtribus.SubtribusID
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
                                                          where refSo.SubtribusID== CurrentTbl60Subtribus.SubtribusID
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
                                                          where refEx.SubtribusID== CurrentTbl60Subtribus.SubtribusID
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
                                                          where comm.SubtribusID== CurrentTbl60Subtribus.SubtribusID
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



     
        #region "Public Properties Tbl60Subtribus"

        public new ICollectionView SubtribussesView;
        public new Tbl60Subtribus CurrentTbl60Subtribus
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
        public new string SearchSubtribusName
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
        public new ObservableCollection<Tbl60Subtribus> Tbl60SubtribussesList
        {
            get { return _tbl60SubtribussesList; }
            set
            {
                if (_tbl60SubtribussesList == value) return;
                _tbl60SubtribussesList = value;
                RaisePropertyChanged("Tbl60SubtribussesList");

                //Clear Search-TextBox
                SearchTribusName = null;                                
                SearchInfratribusName = null;
                SearchCommentInfo = null;
                SearchRefExpertName = null;
                SearchRefSourceName = null;
                SearchRefAuthorName = null;
            }
        }

        private ObservableCollection<Tbl60Subtribus> _tbl60SubtribussesAllList;
        public ObservableCollection<Tbl60Subtribus> Tbl60SubtribussesAllList
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

       
        #region "Public Properties Tbl57Tribus"

        public  ICollectionView TribussesView;
        public  Tbl57Tribus CurrentTbl57Tribus
        {
            get
            {
                if (TribussesView != null)
                    return TribussesView.CurrentItem as Tbl57Tribus;
                return null;
            }
        }
        //--------------------------------------------                                               

        private string _searchTribusName;
        public  string SearchTribusName
        {
            get { return _searchTribusName; }
            set
            {
                if (value == _searchTribusName) return;
                _searchTribusName = value;
                RaisePropertyChanged("SearchTribusName");
            }
        }

        private ObservableCollection<Tbl57Tribus> _tbl57TribussesList;
        public  ObservableCollection<Tbl57Tribus> Tbl57TribussesList
        {
            get { return _tbl57TribussesList; }
            set
            {
                if (_tbl57TribussesList == value) return;
                _tbl57TribussesList = value;
                RaisePropertyChanged("Tbl57TribussesList");
            }
        }

        #endregion "Public Properties"
   
  
       
        #region "Public Properties Tbl63Infratribus"

        public ICollectionView InfratribussesView;
        public Tbl63Infratribus CurrentTbl63Infratribus
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
        public string SearchInfratribusName
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
        public ObservableCollection<Tbl63Infratribus> Tbl63InfratribussesList
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
   
   

 //    Part 11    

      }
}   
