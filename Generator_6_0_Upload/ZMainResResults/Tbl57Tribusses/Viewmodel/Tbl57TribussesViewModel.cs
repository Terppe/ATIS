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

//    Tbl57TribussesViewModel Skriptdatum:  01.04.2014  10:32    

namespace WPFUI.Views.Database
{   


    
    public class Tbl57TribussesViewModel : Tbl54SupertribussesViewModel                     
    {     
         
      #region "Private Data Members"

        protected readonly Tbl60SubtribussesRepository Tbl60SubtribussesRepository = new Tbl60SubtribussesRepository();   
          
          #endregion "Private Data Members"            
    
        #region "Constructor"

        public Tbl57TribussesViewModel()
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

       
        #region "Public Commands Basic Tbl57Tribus"

        private RelayCommand _getTribusByNameCommand;
        public new ICommand GetTribusByNameCommand
        {
            get { return _getTribusByNameCommand ?? (_getTribusByNameCommand = new RelayCommand(delegate { GetTribusByNameOrId(null); })); }   
        }

        private void GetTribusByNameOrId(object o)       
        {   
Tbl57TribussesList =  new ObservableCollection<Tbl57Tribus>
                                                       (from x in Tbl57TribussesRepository.Tbl57Tribusses
                                                        where x.TribusName.StartsWith(SearchTribusName)
                                                        orderby x.TribusName
                                                        select x);

            Tbl57TribussesAllList =  new ObservableCollection<Tbl57Tribus>
                                                       (from y in Tbl57TribussesRepository.Tbl57Tribusses
                                                        orderby y.TribusName
                                                        select y);

            Tbl54SupertribussesAllList =  new ObservableCollection<Tbl54Supertribus>
                                                       (from z in Tbl54SupertribussesRepository.Tbl54Supertribusses
                                                        orderby z.SupertribusName
                                                        select z);

              
  Tbl51InfrafamiliesAllList =  new ObservableCollection<Tbl51Infrafamily>
                                                       (from z in Tbl51InfrafamiliesRepository.Tbl51Infrafamilies
                                                        orderby z.InfrafamilyName
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

  
Tbl54SupertribussesList = null;                  
  Tbl60SubtribussesList = null;               
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

        private void AddTribus(object o)
        {
            if (Tbl57TribussesList == null)
                Tbl57TribussesList = new ObservableCollection<Tbl57Tribus>();
            Tbl57TribussesList.Add(new Tbl57Tribus{ TribusName= CultRes.StringsRes.DatasetNew });
            TribussesView = CollectionViewSource.GetDefaultView(Tbl57TribussesList);
            if (TribussesView != null)
                TribussesView.CurrentChanged += tbl57TribusView_CurrentChanged;
            RaisePropertyChanged();
        }
        //---------------------------------------------------------------------------------------
  
       
        private RelayCommand _deleteTribusCommand;
        public new ICommand DeleteTribusCommand
        {
            get { return _deleteTribusCommand ?? (_deleteTribusCommand = new RelayCommand(delegate { DeleteTribus(null); })); }
        }

        private void DeleteTribus(object o)
        {
            try
            {
                var tribus= Tbl57TribussesRepository.Tbl57Tribusses.FirstOrDefault(x => x.TribusID== CurrentTbl57Tribus.TribusID);
                if (tribus!= null)
                {
                    if (MessageBox.Show(CultRes.StringsRes.DeleteQuestion
                                        + " " +  CurrentTbl57Tribus.TribusName, CultRes.StringsRes.DeleteQuestion1, MessageBoxButton.YesNo) !=
                        MessageBoxResult.Yes) return;
                    Tbl57TribussesRepository.Delete(tribus);
                    Tbl57TribussesRepository.Save();
                    MessageBox.Show(CurrentTbl57Tribus.TribusName + " " + CultRes.StringsRes.DeleteSuccess);
                    GetTribusByNameOrId(o); //Refresh
                }
                else
                {
                    MessageBox.Show(CultRes.StringsRes.DeleteCan + " " + CurrentTbl57Tribus.TribusName+ " " + CultRes.StringsRes.DeleteCan1);
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
            get { return _saveTribusCommand ?? (_saveTribusCommand = new RelayCommand(delegate { SaveTribus(null); })); }
        }

        private void SaveTribus(object o)
        {
            try
            {
                var tribus= Tbl57TribussesRepository.Tbl57Tribusses.FirstOrDefault(x => x.TribusID== CurrentTbl57Tribus.TribusID);
                if (CurrentTbl57Tribus == null)
                {
                    MessageBox.Show(CultRes.StringsRes.DatasetNotExist);
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
                        Tbl57TribussesRepository.Add(new Tbl57Tribus
                        {
                            SupertribusID= CurrentTbl57Tribus.SupertribusID,              
                            TribusName= CurrentTbl57Tribus.TribusName,              
                            CountID = RandomHelper.Randomnumber(),
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
                        MessageBox.Show(CurrentTbl57Tribus.TribusName+ " " + CultRes.StringsRes.SaveSuccess);
                        GetTribusByNameOrId(o);  //Refresh
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


        
        #region "Public Commands Connect <== Tbl54Supertribus"                 

        private RelayCommand _getSupertribusByNameCommand;
        public new ICommand GetSupertribusByNameCommand
        {
            get { return _getSupertribusByNameCommand ?? (_getSupertribusByNameCommand = new RelayCommand(delegate { GetSupertribusByNameOrId(null); })); }
        }

        private void GetSupertribusByNameOrId(object o)
        {
            Tbl54SupertribussesList =
                new ObservableCollection<Tbl54Supertribus>((from supertribus in Tbl54SupertribussesRepository.Tbl54Supertribusses
                                                       where supertribus.SupertribusName.StartsWith(SearchSupertribusName)
                                                       orderby supertribus.SupertribusName
                                                       select supertribus));

            SupertribussesView = CollectionViewSource.GetDefaultView(Tbl54SupertribussesList);
            if (SupertribussesView != null)
                SupertribussesView.CurrentChanged += tbl54SupertribusView_CurrentChanged;                   
            RaisePropertyChanged();
        }
        //------------------------------------------------------------------------------
        private RelayCommand _addSupertribusCommand;
        public new ICommand AddSupertribusCommand
        {
            get { return _addSupertribusCommand ?? (_addSupertribusCommand = new RelayCommand(AddSupertribus)); }
        }

        private void AddSupertribus()
        {
            if (Tbl54SupertribussesList == null)
                Tbl54SupertribussesList = new ObservableCollection<Tbl54Supertribus>();
            Tbl54SupertribussesList.Add(new Tbl54Supertribus{ SupertribusName= "New " });                   
            SupertribussesView = CollectionViewSource.GetDefaultView(Tbl54SupertribussesList);
            if (SupertribussesView != null)
                SupertribussesView.CurrentChanged += tbl54SupertribusView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl54Supertribus");
        }
        //----------------------------------------------------------------------------------------------------------
        private RelayCommand _deleteSupertribusCommand;
        public ICommand SupertribusPhylumCommand
        {
            get { return _deleteSupertribusCommand ?? (_deleteSupertribusCommand = new RelayCommand(DeleteSupertribus)); }
        }

        private void DeleteSupertribus()
        {
            try
            {
                var supertribus= Tbl54SupertribussesRepository.Tbl54Supertribusses.FirstOrDefault(x => x.SupertribusID== CurrentTbl54Supertribus.SupertribusID);
                if (supertribus!= null)
                {
                    if (MessageBox.Show("Are you really shure to delete this "
                                        + CurrentTbl54Supertribus.SupertribusName, "Question", MessageBoxButton.YesNo) !=
                        MessageBoxResult.Yes) return;
                    Tbl54SupertribussesRepository.Delete(supertribus);
                    Tbl54SupertribussesRepository.Save();
                    MessageBox.Show(CurrentTbl54Supertribus.SupertribusName+ " was deleted successfully");
                    if (SearchSupertribusName == null)
                    {
                        GetConnectedTablesById(); //refresh doubleClick                    
                    }
                    else
                    {
                        GetSupertribusByName(); //search
                    }
                }
                else
                {
                    MessageBox.Show("Only " + CurrentTbl54Supertribus.SupertribusName+ " can be deleted");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //-------------------------------------------------------------------------------------------------
        private RelayCommand _saveSupertribusCommand;   
        public new ICommand SaveSupertribusCommand
        {
            get { return _saveSupertribusCommand ?? (_saveSupertribusCommand = new RelayCommand(SaveSupertribus)); }
        }

        private void SaveSupertribus()
        {
            try
            {
                var supertribus= Tbl54SupertribussesRepository.Tbl54Supertribusses.FirstOrDefault(x => x.SupertribusID== CurrentTbl54Supertribus.SupertribusID);
                if (CurrentTbl54Supertribus == null)
                {
                    MessageBox.Show("supertribus was not found");
                }
                else
                {
                    if (CurrentTbl54Supertribus.SupertribusID!= 0)
                    {
                        if (supertribus!= null) //update
                        {
                            supertribus.Updater = Environment.UserName;
                            supertribus.UpdaterDate = DateTime.Now;
                        }
                    }
                    else
                    {
                        Tbl54SupertribussesRepository.Add(new Tbl54Supertribus()
                        {
                            SupertribusName= CurrentTbl54Supertribus.SupertribusName,              
                            CountID = TblCountersRepository.Counter(),
                            Valid = CurrentTbl54Supertribus.Valid,
                            ValidYear = CurrentTbl54Supertribus.ValidYear,
                            Synonym = CurrentTbl54Supertribus.Synonym,
                            Author = CurrentTbl54Supertribus.Author,
                            AuthorYear = CurrentTbl54Supertribus.AuthorYear,
                            Info = CurrentTbl54Supertribus.Info,
                            EngName = CurrentTbl54Supertribus.EngName,
                            GerName = CurrentTbl54Supertribus.GerName,
                            FraName = CurrentTbl54Supertribus.FraName,
                            PorName = CurrentTbl54Supertribus.PorName,
                            Writer = Environment.UserName,
                            WriterDate = DateTime.Now,
                            Updater = Environment.UserName,
                            UpdaterDate = DateTime.Now,
                            Memo = CurrentTbl54Supertribus.Memo
                        });
                        }
                        {
                            Tbl54SupertribussesRepository.Save();
                            MessageBox.Show(CurrentTbl54Supertribus.SupertribusName+  " was successfully saved ");
                            GetSupertribusByName();  //Refresh
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


        
        #region "Public Commands Connect ==> Tbl60Subtribus"                 

        private RelayCommand _getSubtribusByNameCommand;
        public ICommand GetSubtribusByNameCommand
        {
            get { return _getSubtribusByNameCommand ?? (_getSubtribusByNameCommand = new RelayCommand(delegate { GetSubtribusByNameOrId(null); })); }
        }

        private void GetSubtribusByNameOrId(object o)
        {
            int id;
            if (int.TryParse(SearchSubtribusName, out id))
                Tbl60SubtribussesList = new ObservableCollection<Tbl60Subtribus> { Tbl60SubtribussesRepository.Get(id) };
            else
            Tbl60SubtribussesList =  new ObservableCollection<Tbl60Subtribus>
                                                      (from x in Tbl60SubtribussesRepository.FindAll()
                                                       where x.SubtribusName.StartsWith(SearchSubtribusName)
                                                       orderby x.SubtribusName
                                                       select x);

            SubtribussesView = CollectionViewSource.GetDefaultView(Tbl60SubtribussesList);
            if (SubtribussesView != null)
                SubtribussesView.CurrentChanged += tbl60SubtribusView_CurrentChanged;                   
            RaisePropertyChanged();
        }
        //------------------------------------------------------------------------------
        private RelayCommand _addSubtribusCommand;
        public ICommand AddSubtribusCommand
        {
            get { return _addSubtribusCommand ?? (_addSubtribusCommand = new RelayCommand(delegate { AddSubtribus(null); })); }
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
        public ICommand DeleteSubtribusCommand
        {
            get { return _deleteSubtribusCommand ?? (_deleteSubtribusCommand = new RelayCommand(delegate { DeleteSubtribus(null); })); }
        }

        private void DeleteSubtribus(object o)
        {
            try
            {
                var subtribus = Tbl60SubtribussesRepository.Tbl60Subtribusses.FirstOrDefault(x => x.SubtribusID== CurrentTbl60Subtribus.SubtribusID);
                if (subtribus != null)
                {
                    if (MessageBox.Show(CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl60Subtribus.SubtribusName, CultRes.StringsRes.DeleteQuestion1, MessageBoxButton.YesNo) !=
                        MessageBoxResult.Yes) return;
                    Tbl60SubtribussesRepository.Delete(subtribus);
                    Tbl60SubtribussesRepository.Save();
                    MessageBox.Show(CurrentTbl60Subtribus.SubtribusName + " " + CultRes.StringsRes.DeleteSuccess);
                    GetSubtribusByNameOrId(o);  //Refresh
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
        public ICommand SaveSubtribusCommand
        {
            get { return _saveSubtribusCommand ?? (_saveSubtribusCommand = new RelayCommand(delegate { SaveSubtribus(null); })); }
        }

        private void SaveSubtribus(object o)
        {
            try
            {
                var subtribus = Tbl60SubtribussesRepository.Tbl60Subtribusses.FirstOrDefault(x => x.SubtribusID== CurrentTbl60Subtribus.SubtribusID);
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
                        if (CurrentTbl60Subtribus != null)
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
                            TribusID= CurrentTbl90RefAuthor.TribusID,
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
                            TribusID= CurrentTbl90RefSource.TribusID,
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
                            TribusID= CurrentTbl90RefExpert.TribusID,
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
                            TribusID= CurrentTbl93Comment.TribusID,                
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
            SearchSupertribusName = null;                       
            SearchSubtribusName = null;
            SearchCommentInfo = null;
            SearchRefExpertName = null;
            SearchRefSourceName = null;
            SearchRefAuthorName = null;

            Tbl54SupertribussesList =
                new ObservableCollection<Tbl54Supertribus>((from supertribus in Tbl54SupertribussesRepository.Tbl54Supertribusses
                                                       where supertribus.SupertribusID== CurrentTbl57Tribus.SupertribusID
                                                       orderby supertribus.SupertribusName
                                                       select supertribus));

            SupertribussesView = CollectionViewSource.GetDefaultView(Tbl54SupertribussesList);
            if (SupertribussesView != null)
                SupertribussesView.CurrentChanged += tbl54SupertribusView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl54Supertribusses");
            //-----------------------------------------------------------------------------------
            Tbl60SubtribussesList =
                new ObservableCollection<Tbl60Subtribus>((from subtribus in Tbl60SubtribussesRepository.Tbl60Subtribusses
                                                       where subtribus.TribusID== CurrentTbl57Tribus.TribusID
                                                       orderby subtribus.Tbl57Tribusses.TribusName
                                                       select subtribus));


            SubtribussesView = CollectionViewSource.GetDefaultView(Tbl60SubtribussesList);
            if (SubtribussesView != null)
                SubtribussesView.CurrentChanged += tbl60SubtribusView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl60Subtribusses");
            //-----------------------------------------------------------------------------------
            Tbl90RefAuthorsList =
                new ObservableCollection<Tbl90Reference>((from refAu in Tbl90ReferencesRepository.Tbl90References
                                                          where refAu.TribusID== CurrentTbl57Tribus.TribusID
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
                                                          where refSo.TribusID== CurrentTbl57Tribus.TribusID
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
                                                          where refEx.TribusID== CurrentTbl57Tribus.TribusID
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
                                                          where comm.TribusID== CurrentTbl57Tribus.TribusID
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



     
        #region "Public Properties Tbl57Tribus"

        public new ICollectionView TribussesView;
        public new Tbl57Tribus CurrentTbl57Tribus
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
        public new string SearchTribusName
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
        public new ObservableCollection<Tbl57Tribus> Tbl57TribussesList
        {
            get { return _tbl57TribussesList; }
            set
            {
                if (_tbl57TribussesList == value) return;
                _tbl57TribussesList = value;
                RaisePropertyChanged("Tbl57TribussesList");

                //Clear Search-TextBox
                SearchSupertribusName = null;                                
                SearchSubtribusName = null;
                SearchCommentInfo = null;
                SearchRefExpertName = null;
                SearchRefSourceName = null;
                SearchRefAuthorName = null;
            }
        }

        private ObservableCollection<Tbl57Tribus> _tbl57TribussesAllList;
        public ObservableCollection<Tbl57Tribus> Tbl57TribussesAllList
        {
            get { return _tbl57TribussesAllList; }
            set
            {
                if (_tbl57TribussesAllList == value) return;
                _tbl57TribussesAllList = value;
                RaisePropertyChanged("Tbl57TribussesAllList");
            }
        }

        #endregion "Public Properties"   

       
        #region "Public Properties Tbl54Supertribus"

        public  ICollectionView SupertribussesView;
        public  Tbl54Supertribus CurrentTbl54Supertribus
        {
            get
            {
                if (SupertribussesView != null)
                    return SupertribussesView.CurrentItem as Tbl54Supertribus;
                return null;
            }
        }
        //--------------------------------------------                                               

        private string _searchSupertribusName;
        public  string SearchSupertribusName
        {
            get { return _searchSupertribusName; }
            set
            {
                if (value == _searchSupertribusName) return;
                _searchSupertribusName = value;
                RaisePropertyChanged("SearchSupertribusName");
            }
        }

        private ObservableCollection<Tbl54Supertribus> _tbl54SupertribussesList;
        public  ObservableCollection<Tbl54Supertribus> Tbl54SupertribussesList
        {
            get { return _tbl54SupertribussesList; }
            set
            {
                if (_tbl54SupertribussesList == value) return;
                _tbl54SupertribussesList = value;
                RaisePropertyChanged("Tbl54SupertribussesList");
            }
        }

        #endregion "Public Properties"
   
  
       
        #region "Public Properties Tbl60Subtribus"

        public ICollectionView SubtribussesView;
        public Tbl60Subtribus CurrentTbl60Subtribus
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
        public string SearchSubtribusName
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
        public ObservableCollection<Tbl60Subtribus> Tbl60SubtribussesList
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
   
   

 //    Part 11    

      }
}   
