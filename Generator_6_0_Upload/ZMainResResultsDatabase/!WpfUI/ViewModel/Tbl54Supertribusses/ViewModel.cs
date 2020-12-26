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

//    Tbl54SupertribusViewModel Skriptdatum:  7.1.2012  10:32    

namespace Atis.WpfUi.ViewModel
{   


    
    public class Tbl54SupertribussesViewModel : Tbl51InfrafamiliesViewModel                     
    {     
         
      #region "Private Data Members"

        protected readonly Tbl57TribussesRepository Tbl57TribussesRepository = new Tbl57TribussesRepository();   
          
          #endregion "Private Data Members"            
    
        #region "Constructor"

        public Tbl54SupertribussesViewModel()
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
       
        #region "Public Commands Basic Tbl54Supertribus"

        private RelayCommand _getSupertribusByNameCommand;
        public new ICommand GetSupertribusByNameCommand
        {
            get { return _getSupertribusByNameCommand ?? (_getSupertribusByNameCommand = new RelayCommand(GetSupertribusByName)); }
        }

        private void GetSupertribusByName()
        {   
Tbl54SupertribussesList =
                 new ObservableCollection<Tbl54Supertribus>((from x in Tbl54SupertribussesRepository.Tbl54Supertribusses
                                                        where x.SupertribusName.StartsWith(SearchSupertribusName)
                                                        orderby x.SupertribusName
                                                        select x));

            Tbl54SupertribussesAllList =
                 new ObservableCollection<Tbl54Supertribus>((from y in Tbl54SupertribussesRepository.Tbl54Supertribusses
                                                        orderby y.SupertribusName
                                                        select y));

            Tbl51InfrafamiliesAllList =
                 new ObservableCollection<Tbl51Infrafamily>((from z in Tbl51InfrafamiliesRepository.Tbl51Infrafamilies
                                                        orderby z.InfrafamilyName
                                                        select z));

              
  Tbl48SubfamiliesAllList =
                 new ObservableCollection<Tbl48Subfamily>((from z in Tbl48SubfamiliesRepository.Tbl48Subfamilies
                                                        orderby z.SubfamilyName
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

  
Tbl51InfrafamiliesList = null;                  
  Tbl57TribussesList = null;     
             
  View = CollectionViewSource.GetDefaultView(Tbl54SupertribussesList);
            if (View != null)
                View.CurrentChanged += tbl54SupertribusView_CurrentChanged;                   
            RaisePropertyChanged("CurrentTbl54Supertribus");
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
            View = CollectionViewSource.GetDefaultView(Tbl54SupertribussesList);
            if (View != null)
                View.CurrentChanged += tbl54SupertribusView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl54Supertribus");
        }
        //---------------------------------------------------------------------------------------
  
       
        private RelayCommand _deleteSupertribusCommand;
        public new ICommand DeleteSupertribusCommand
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
                    MessageBox.Show(CurrentTbl54Supertribus.SupertribusName + " was deleted successfully");
                    GetSupertribusByName(); //Refresh
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
                        Tbl54SupertribussesRepository.Add(new Tbl54Supertribus
                        {
                            InfrafamilyID= CurrentTbl54Supertribus.InfrafamilyID,              
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
  

        
        #region "Public Commands Connect <== Tbl51Infrafamily"                 

        private RelayCommand _getInfrafamilyByNameCommand;
        public new ICommand GetInfrafamilyByNameCommand
        {
            get { return _getInfrafamilyByNameCommand ?? (_getInfrafamilyByNameCommand = new RelayCommand(GetInfrafamilyByName)); }
        }

        private void GetInfrafamilyByName()
        {
            Tbl51InfrafamiliesList =
                new ObservableCollection<Tbl51Infrafamily>((from infrafamily in Tbl51InfrafamiliesRepository.Tbl51Infrafamilies
                                                       where infrafamily.InfrafamilyName.StartsWith(SearchInfrafamilyName)
                                                       orderby infrafamily.InfrafamilyName
                                                       select infrafamily));

            View = CollectionViewSource.GetDefaultView(Tbl51InfrafamiliesList);
            if (View != null)
                View.CurrentChanged += tbl51InfrafamilyView_CurrentChanged;                   
            RaisePropertyChanged("CurrentTbl51Infrafamily");
        }
        //------------------------------------------------------------------------------
        private RelayCommand _addInfrafamilyCommand;
        public new ICommand AddInfrafamilyCommand
        {
            get { return _addInfrafamilyCommand ?? (_addInfrafamilyCommand = new RelayCommand(AddInfrafamily)); }
        }

        private void AddInfrafamily()
        {
            if (Tbl51InfrafamiliesList == null)
                Tbl51InfrafamiliesList = new ObservableCollection<Tbl51Infrafamily>();
            Tbl51InfrafamiliesList.Add(new Tbl51Infrafamily{ InfrafamilyName= "New " });                   
            View = CollectionViewSource.GetDefaultView(Tbl51InfrafamiliesList);
            if (View != null)
                View.CurrentChanged += tbl51InfrafamilyView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl51Infrafamily");
        }
        //----------------------------------------------------------------------------------------------------------
        private RelayCommand _deleteInfrafamilyCommand;
        public ICommand InfrafamilyPhylumCommand
        {
            get { return _deleteInfrafamilyCommand ?? (_deleteInfrafamilyCommand = new RelayCommand(DeleteInfrafamily)); }
        }

        private void DeleteInfrafamily()
        {
            try
            {
                var infrafamily= Tbl51InfrafamiliesRepository.Tbl51Infrafamilies.FirstOrDefault(x => x.InfrafamilyID== CurrentTbl51Infrafamily.InfrafamilyID);
                if (infrafamily!= null)
                {
                    if (MessageBox.Show("Are you really shure to delete this "
                                        + CurrentTbl51Infrafamily.InfrafamilyName, "Question", MessageBoxButton.YesNo) !=
                        MessageBoxResult.Yes) return;
                    Tbl51InfrafamiliesRepository.Delete(infrafamily);
                    Tbl51InfrafamiliesRepository.Save();
                    MessageBox.Show(CurrentTbl51Infrafamily.InfrafamilyName+ " was deleted successfully");
                    if (SearchInfrafamilyName == null)
                    {
                        GetConnectedTablesById(); //refresh doubleClick                    
                    }
                    else
                    {
                        GetInfrafamilyByName(); //search
                    }
                }
                else
                {
                    MessageBox.Show("Only " + CurrentTbl51Infrafamily.InfrafamilyName+ " can be deleted");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //-------------------------------------------------------------------------------------------------
        private RelayCommand _saveInfrafamilyCommand;   
        public new ICommand SaveInfrafamilyCommand
        {
            get { return _saveInfrafamilyCommand ?? (_saveInfrafamilyCommand = new RelayCommand(SaveInfrafamily)); }
        }

        private void SaveInfrafamily()
        {
            try
            {
                var infrafamily= Tbl51InfrafamiliesRepository.Tbl51Infrafamilies.FirstOrDefault(x => x.InfrafamilyID== CurrentTbl51Infrafamily.InfrafamilyID);
                if (CurrentTbl51Infrafamily == null)
                {
                    MessageBox.Show("infrafamily was not found");
                }
                else
                {
                    if (CurrentTbl51Infrafamily.InfrafamilyID!= 0)
                    {
                        if (infrafamily!= null) //update
                        {
                            infrafamily.Updater = Environment.UserName;
                            infrafamily.UpdaterDate = DateTime.Now;
                        }
                    }
                    else
                    {
                        Tbl51InfrafamiliesRepository.Add(new Tbl51Infrafamily()
                        {
                            InfrafamilyName= CurrentTbl51Infrafamily.InfrafamilyName,              
                            CountID = TblCountersRepository.Counter(),
                            Valid = CurrentTbl51Infrafamily.Valid,
                            ValidYear = CurrentTbl51Infrafamily.ValidYear,
                            Synonym = CurrentTbl51Infrafamily.Synonym,
                            Author = CurrentTbl51Infrafamily.Author,
                            AuthorYear = CurrentTbl51Infrafamily.AuthorYear,
                            Info = CurrentTbl51Infrafamily.Info,
                            EngName = CurrentTbl51Infrafamily.EngName,
                            GerName = CurrentTbl51Infrafamily.GerName,
                            FraName = CurrentTbl51Infrafamily.FraName,
                            PorName = CurrentTbl51Infrafamily.PorName,
                            Writer = Environment.UserName,
                            WriterDate = DateTime.Now,
                            Updater = Environment.UserName,
                            UpdaterDate = DateTime.Now,
                            Memo = CurrentTbl51Infrafamily.Memo
                        });
                    }
                    {
                        Tbl51InfrafamiliesRepository.Save();
                        MessageBox.Show(CurrentTbl51Infrafamily.InfrafamilyName+  " was successfully saved ");
                        GetInfrafamilyByName();  //Refresh
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion "Public Commands"
    
  
        
        #region "Public Commands Connect ==> Tbl57Tribus"                 

        private RelayCommand _getTribusByNameCommand;
        public ICommand GetTribusByNameCommand
        {
            get { return _getTribusByNameCommand ?? (_getTribusByNameCommand = new RelayCommand(GetTribusByName)); }
        }

        private void GetTribusByName()
        {
            Tbl57TribussesList =
                new ObservableCollection<Tbl57Tribus>((from tribus in Tbl57TribussesRepository.Tbl57Tribusses
                                                       where tribus.TribusName.StartsWith(SearchTribusName)
                                                       orderby tribus.TribusName
                                                       select tribus));

            View = CollectionViewSource.GetDefaultView(Tbl57TribussesList);
            if (View != null)
                View.CurrentChanged += tbl57TribusView_CurrentChanged;                   
            RaisePropertyChanged("CurrentTbl57Tribus");
        }
        //------------------------------------------------------------------------------
        private RelayCommand _addTribusCommand;
        public ICommand AddTribusCommand
        {
            get { return _addTribusCommand ?? (_addTribusCommand = new RelayCommand(AddTribus)); }
        }

        private void AddTribus()
        {
            if (Tbl57TribussesList == null)
                Tbl57TribussesList = new ObservableCollection<Tbl57Tribus>();
            Tbl57TribussesList.Add(new Tbl57Tribus{ TribusName= "New " });                   
            View = CollectionViewSource.GetDefaultView(Tbl57TribussesList);
            if (View != null)
                View.CurrentChanged += tbl57TribusView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl57Tribus");
        }
        //---------------------------------------------------------------------------------------
        private RelayCommand _deleteTribusCommand;
        public ICommand DeleteTribusCommand
        {
            get { return _deleteTribusCommand ?? (_deleteTribusCommand = new RelayCommand(DeleteTribus)); }
        }

        private void DeleteTribus()
        {
            try
            {
                var tribus = Tbl57TribussesRepository.Tbl57Tribusses.FirstOrDefault(x => x.TribusID== CurrentTbl57Tribus.TribusID);
                if (tribus != null)
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
        public ICommand SaveTribusCommand
        {
            get { return _saveTribusCommand ?? (_saveTribusCommand = new RelayCommand(SaveTribus)); }
        }

        private void SaveTribus()
        {
            try
            {
                var tribus = Tbl57TribussesRepository.Tbl57Tribusses.FirstOrDefault(x => x.TribusID== CurrentTbl57Tribus.TribusID);
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
                        Tbl57TribussesRepository.Add(new Tbl57Tribus
                        {
                            SupertribusID= CurrentTbl57Tribus.SupertribusID,              
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
                        if (SearchTribusName == null)
                        {
                            GetConnectedTablesById(); //refresh doubleClick                    
                        }
                        else
                        {
                            GetTribusByName(); //search
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
    
  
        
        #region "Public Commands Connect ==> Tbl60Subtribus"                 

        private RelayCommand _getNULLByNameCommand;
        public ICommand GetNULLByNameCommand
        {
            get { return _getNULLByNameCommand ?? (_getNULLByNameCommand = new RelayCommand(GetNULLByName)); }
        }

        private void GetNULLByName()
        {
            Tbl60SubtribussesList =
                new ObservableCollection<Tbl60Subtribus>((from  in Tbl60SubtribussesRepository.Tbl60Subtribusses
                                                       where .SubtribusName.StartsWith(SearchNULLName)
                                                       orderby .SubtribusName
                                                       select ));

            View = CollectionViewSource.GetDefaultView(Tbl60SubtribussesList);
            if (View != null)
                View.CurrentChanged += View_CurrentChanged;                   
            RaisePropertyChanged("CurrentTbl60Subtribus");
        }
        //------------------------------------------------------------------------------
        private RelayCommand _addNULLCommand;
        public ICommand AddNULLCommand
        {
            get { return _addNULLCommand ?? (_addNULLCommand = new RelayCommand(AddNULL)); }
        }

        private void AddNULL()
        {
            if (Tbl60SubtribussesList == null)
                Tbl60SubtribussesList = new ObservableCollection<Tbl60Subtribus>();
            Tbl60SubtribussesList.Add(new Tbl60Subtribus{ SubtribusName= "New " });                   
            View = CollectionViewSource.GetDefaultView(Tbl60SubtribussesList);
            if (View != null)
                View.CurrentChanged += View_CurrentChanged;
            RaisePropertyChanged("CurrentTbl60Subtribus");
        }
        //---------------------------------------------------------------------------------------
        private RelayCommand _deleteNULLCommand;
        public ICommand DeleteNULLCommand
        {
            get { return _deleteNULLCommand ?? (_deleteNULLCommand = new RelayCommand(DeleteNULL)); }
        }

        private void DeleteNULL()
        {
            try
            {
                var = Tbl60SubtribussesRepository.Tbl60Subtribusses.FirstOrDefault(x => x.SubtribusID== CurrentTbl60Subtribus.SubtribusID);
                if (!= null)
                {
                    if (MessageBox.Show("Are you really shure to delete this "
                                        + CurrentTbl60Subtribus.SubtribusName, "Question", MessageBoxButton.YesNo) !=
                        MessageBoxResult.Yes) return;
                    Tbl60SubtribussesRepository.Delete();
                    Tbl60SubtribussesRepository.Save();
                    MessageBox.Show(CurrentTbl60Subtribus.SubtribusName+ " was deleted successfully");
                    if (SearchNULLName == null)
                    {
                        GetConnectedTablesById(); //refresh doubleClick                    
                    }
                    else
                    {
                        GetNULLByName(); //search
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
        private RelayCommand _saveNULLCommand;   
        public ICommand SaveNULLCommand
        {
            get { return _saveNULLCommand ?? (_saveNULLCommand = new RelayCommand(SaveNULL)); }
        }

        private void SaveNULL()
        {
            try
            {
                var = Tbl60SubtribussesRepository.Tbl60Subtribusses.FirstOrDefault(x => x.SubtribusID== CurrentTbl60Subtribus.SubtribusID);
                if (CurrentTbl60Subtribus == null)
                {
                    MessageBox.Show(" was not found");
                }
                else
                {
                    if (CurrentTbl60Subtribus.SubtribusID!= 0)
                    {
                        if (!= null) //update
                        {
                            .Updater = Environment.UserName;
                            .UpdaterDate = DateTime.Now;
                        }
                    }
                    else
                    {
                        Tbl60SubtribussesRepository.Add(new Tbl60Subtribus
                        {
                            SupertribusID= CurrentTbl60Subtribus.SupertribusID,              
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
                        if (SearchNULLName == null)
                        {
                            GetConnectedTablesById(); //refresh doubleClick                    
                        }
                        else
                        {
                            GetNULLByName(); //search
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
                            SupertribusID= CurrentTbl90RefAuthor.SupertribusID,
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
                            SupertribusID= CurrentTbl90RefSource.SupertribusID,
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
                            SupertribusID= CurrentTbl90RefExpert.SupertribusID,
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
                            SupertribusID= CurrentTbl93Comment.SupertribusID,                
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
            SearchInfrafamilyName = null;                       
            SearchTribusName = null;
            SearchCommentInfo = null;
            SearchRefExpertName = null;
            SearchRefSourceName = null;
            SearchRefAuthorName = null;

            Tbl51InfrafamiliesList =
                new ObservableCollection<Tbl51Infrafamily>((from infrafamily in Tbl51InfrafamiliesRepository.Tbl51Infrafamilies
                                                       where infrafamily.InfrafamilyID== CurrentTbl54Supertribus.InfrafamilyID
                                                       orderby infrafamily.InfrafamilyName
                                                       select infrafamily));

            View = CollectionViewSource.GetDefaultView(Tbl51InfrafamiliesList);
            if (View != null)
                View.CurrentChanged += tbl51InfrafamilyView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl51Infrafamilies");
            //-----------------------------------------------------------------------------------
            Tbl57TribussesList =
                new ObservableCollection<Tbl57Tribus>((from tribus in Tbl57TribussesRepository.Tbl57Tribusses
                                                       where tribus.SupertribusID== CurrentTbl54Supertribus.SupertribusID
                                                       orderby tribus.Tbl54Supertribusses.SupertribusName
                                                       select tribus));


            View = CollectionViewSource.GetDefaultView(Tbl57TribussesList);
            if (View != null)
                View.CurrentChanged += tbl57TribusView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl57Tribusses");
            //-----------------------------------------------------------------------------------
            Tbl90RefAuthorsList =
                new ObservableCollection<Tbl90Reference>((from refAu in Tbl90ReferencesRepository.Tbl90References
                                                          where refAu.SupertribusID== CurrentTbl54Supertribus.SupertribusID
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
                                                          where refSo.SupertribusID== CurrentTbl54Supertribus.SupertribusID
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
                                                          where refEx.SupertribusID== CurrentTbl54Supertribus.SupertribusID
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
                                                          where comm.SupertribusID== CurrentTbl54Supertribus.SupertribusID
                                                        orderby comm.Info
                                                        select comm));

            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            if (CommentsView != null)
                CommentsView.CurrentChanged += tbl93CommentView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl93Comment");
            //--------------------------------------------------------------

        }

        #endregion "Public Commands Connected Tables by DoubleClick"
   

     
        #region "Public Properties Tbl54Supertribus"

        public new ICollectionView View;
        public new Tbl54Supertribus CurrentTbl54Supertribus
        {
            get
            {
                if (View != null)
                    return View.CurrentItem as Tbl54Supertribus;
                return null;
            }
        }
        //--------------------------------------------
        private string _searchSupertribusName;
        public new string SearchSupertribusName
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
        public new ObservableCollection<Tbl54Supertribus> Tbl54SupertribussesList
        {
            get { return _tbl54SupertribussesList; }
            set
            {
                if (_tbl54SupertribussesList == value) return;
                _tbl54SupertribussesList = value;
                RaisePropertyChanged("Tbl54SupertribussesList");

                //Clear Search-TextBox
                SearchInfrafamilyName = null;                                
                SearchTribusName = null;
                SearchCommentInfo = null;
                SearchRefExpertName = null;
                SearchRefSourceName = null;
                SearchRefAuthorName = null;
            }
        }

        private ObservableCollection<Tbl54Supertribus> _tbl54SupertribussesAllList;
        public ObservableCollection<Tbl54Supertribus> Tbl54SupertribussesAllList
        {
            get { return _tbl54SupertribussesAllList; }
            set
            {
                if (_tbl54SupertribussesAllList == value) return;
                _tbl54SupertribussesAllList = value;
                RaisePropertyChanged("Tbl54SupertribussesAllList");
            }
        }

        #endregion "Public Properties"
   

       
        #region "Public Properties Tbl51Infrafamily"

        public new ICollectionView View;
        public new Tbl51Infrafamily CurrentTbl51Infrafamily
        {
            get
            {
                if (View != null)
                    return View.CurrentItem as Tbl51Infrafamily;
                return null;
            }
        }
        //--------------------------------------------                                               

        private string _searchInfrafamilyName;
        public new string SearchInfrafamilyName
        {
            get { return _searchInfrafamilyName; }
            set
            {
                if (value == _searchInfrafamilyName) return;
                _searchInfrafamilyName = value;
                RaisePropertyChanged("SearchInfrafamilyName");
            }
        }

        private ObservableCollection<Tbl51Infrafamily> _tbl51InfrafamiliesList;
        public new ObservableCollection<Tbl51Infrafamily> Tbl51InfrafamiliesList
        {
            get { return _tbl51InfrafamiliesList; }
            set
            {
                if (_tbl51InfrafamiliesList == value) return;
                _tbl51InfrafamiliesList = value;
                RaisePropertyChanged("Tbl51InfrafamiliesList");
            }
        }

        #endregion "Public Properties"
   
  
       
        #region "Public Properties Tbl57Tribus"

        public ICollectionView View;
        public Tbl57Tribus CurrentTbl57Tribus
        {
            get
            {
                if (View != null)
                    return View.CurrentItem as Tbl57Tribus;
                return null;
            }
        }
        //--------------------------------------------                                               

        private string _searchTribusName;
        public string SearchTribusName
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
        public ObservableCollection<Tbl57Tribus> Tbl57TribussesList
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
   
  
       
        #region "Public Properties Tbl60Subtribus"

        public ICollectionView View;
        public Tbl60Subtribus CurrentTbl60Subtribus
        {
            get
            {
                if (View != null)
                    return View.CurrentItem as Tbl60Subtribus;
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

        private ObservableCollection<Tbl60Subtribus> List;
        public ObservableCollection<Tbl60Subtribus> Tbl60SubtribussesList
        {
            get { return List; }
            set
            {
                if (List == value) return;
                List = value;
                RaisePropertyChanged("Tbl60SubtribussesList");
            }
        }

        #endregion "Public Properties"
   
          
        #region "Private Methods"

        public void tbl57TribusView_CurrentChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged("CurrentTbl57Tribus");
        }
        #endregion "Private Methods"
   
   

   
    }
}   
