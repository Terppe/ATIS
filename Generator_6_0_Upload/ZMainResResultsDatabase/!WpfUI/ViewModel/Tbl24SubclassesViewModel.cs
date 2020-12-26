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

//    Tbl24SubclassesViewModel Skriptdatum:  22.02.2016  18:32    

namespace WPFUI.Views.Database
{   


    
    public class Tbl24SubclassesViewModel : Tbl21ClassesViewModel                     
    {     
         
      #region "Private Data Members"

        protected readonly Tbl27InfraclassesRepository Tbl27InfraclassesRepository = new Tbl27InfraclassesRepository();   
          
          #endregion "Private Data Members"            
    
        #region "Constructor"

        public Tbl24SubclassesViewModel()
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

       
        #region "Public Commands Basic Tbl24Subclass"

        private RelayCommand _getSubclassByNameCommand;
        public new ICommand GetSubclassByNameCommand
        {
            get { return _getSubclassByNameCommand ?? (_getSubclassByNameCommand = new RelayCommand(delegate { GetSubclassByNameOrId(null); })); }   
        }

        private void GetSubclassByNameOrId(object o)       
        {   
Tbl24SubclassesList =  new ObservableCollection<Tbl24Subclass>
                                                       (from x in Tbl24SubclassesRepository.Tbl24Subclasses
                                                        where x.SubclassName.StartsWith(SearchSubclassName)
                                                        orderby x.SubclassName
                                                        select x);

            Tbl24SubclassesAllList =  new ObservableCollection<Tbl24Subclass>
                                                       (from y in Tbl24SubclassesRepository.Tbl24Subclasses
                                                        orderby y.SubclassName
                                                        select y);

            Tbl21ClassesAllList =  new ObservableCollection<Tbl21Class>
                                                       (from z in Tbl21ClassesRepository.Tbl21Classes
                                                        orderby z.ClassName
                                                        select z);

              
  Tbl18SuperclassesAllList =  new ObservableCollection<Tbl18Superclass>
                                                       (from z in Tbl18SuperclassesRepository.Tbl18Superclasses
                                                        orderby z.SuperclassName
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

  
Tbl21ClassesList = null;                  
  Tbl27InfraclassesList = null;               
  SubclassesView = CollectionViewSource.GetDefaultView(Tbl24SubclassesList);
            if (SubclassesView != null)
                SubclassesView.CurrentChanged += tbl24SubclassView_CurrentChanged;                   
            RaisePropertyChanged();
        }
        //------------------------------------------------------------------------------
        private RelayCommand _addSubclassCommand;
        public new ICommand AddSubclassCommand
        {
            get { return _addSubclassCommand ?? (_addSubclassCommand = new RelayCommand(AddSubclass)); }
        }

        private void AddSubclass(object o)
        {
            if (Tbl24SubclassesList == null)
                Tbl24SubclassesList = new ObservableCollection<Tbl24Subclass>();
            Tbl24SubclassesList.Add(new Tbl24Subclass{ SubclassName= CultRes.StringsRes.DatasetNew });
            SubclassesView = CollectionViewSource.GetDefaultView(Tbl24SubclassesList);
            if (SubclassesView != null)
                SubclassesView.CurrentChanged += tbl24SubclassView_CurrentChanged;
            RaisePropertyChanged();
        }
        //---------------------------------------------------------------------------------------
  
       
        private RelayCommand _deleteSubclassCommand;
        public new ICommand DeleteSubclassCommand
        {
            get { return _deleteSubclassCommand ?? (_deleteSubclassCommand = new RelayCommand(delegate { DeleteSubclass(null); })); }
        }

        private void DeleteSubclass(object o)
        {
            try
            {
                var subclass= Tbl24SubclassesRepository.Tbl24Subclasses.FirstOrDefault(x => x.SubclassID== CurrentTbl24Subclass.SubclassID);
                if (subclass!= null)
                {
                    if (MessageBox.Show(CultRes.StringsRes.DeleteQuestion
                                        + " " +  CurrentTbl24Subclass.SubclassName, CultRes.StringsRes.DeleteQuestion1, MessageBoxButton.YesNo) !=
                        MessageBoxResult.Yes) return;
                    Tbl24SubclassesRepository.Delete(subclass);
                    Tbl24SubclassesRepository.Save();
                    MessageBox.Show(CurrentTbl24Subclass.SubclassName + " " + CultRes.StringsRes.DeleteSuccess);
                    GetSubclassByNameOrId(o); //Refresh
                }
                else
                {
                    MessageBox.Show(CultRes.StringsRes.DeleteCan + " " + CurrentTbl24Subclass.SubclassName+ " " + CultRes.StringsRes.DeleteCan1);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //-------------------------------------------------------------------------------------------------
        private RelayCommand _saveSubclassCommand;
        public new ICommand SaveSubclassCommand
        {
            get { return _saveSubclassCommand ?? (_saveSubclassCommand = new RelayCommand(delegate { SaveSubclass(null); })); }
        }

        private void SaveSubclass(object o)
        {
            try
            {
                var subclass= Tbl24SubclassesRepository.Tbl24Subclasses.FirstOrDefault(x => x.SubclassID== CurrentTbl24Subclass.SubclassID);
                if (CurrentTbl24Subclass == null)
                {
                    MessageBox.Show(CultRes.StringsRes.DatasetNotExist);
                }
                else
                {
                    if (CurrentTbl24Subclass.SubclassID!= 0)
                    {
                        if (subclass!= null) //update
                        {
                            subclass.Updater = Environment.UserName;
                            subclass.UpdaterDate = DateTime.Now;
                        }
                    }
                    else
                    {
                        Tbl24SubclassesRepository.Add(new Tbl24Subclass
                        {
                            ClassID= CurrentTbl24Subclass.ClassID,              
                            SubclassName= CurrentTbl24Subclass.SubclassName,              
                            CountID = RandomHelper.Randomnumber(),
                            Valid = CurrentTbl24Subclass.Valid,
                            ValidYear = CurrentTbl24Subclass.ValidYear,
                            Synonym = CurrentTbl24Subclass.Synonym,
                            Author = CurrentTbl24Subclass.Author,
                            AuthorYear = CurrentTbl24Subclass.AuthorYear,
                            Info = CurrentTbl24Subclass.Info,
                            EngName = CurrentTbl24Subclass.EngName,
                            GerName = CurrentTbl24Subclass.GerName,
                            FraName = CurrentTbl24Subclass.FraName,
                            PorName = CurrentTbl24Subclass.PorName,
                            Writer = Environment.UserName,
                            WriterDate = DateTime.Now,
                            Updater = Environment.UserName,
                            UpdaterDate = DateTime.Now,
                            Memo = CurrentTbl24Subclass.Memo
                        });
                    }
                    {
                        Tbl24SubclassesRepository.Save();
                        MessageBox.Show(CurrentTbl24Subclass.SubclassName+ " " + CultRes.StringsRes.SaveSuccess);
                        GetSubclassByNameOrId(o);  //Refresh
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


        
        #region "Public Commands Connect <== Tbl21Class"                 

        private RelayCommand _getClassByNameCommand;
        public new ICommand GetClassByNameCommand
        {
            get { return _getClassByNameCommand ?? (_getClassByNameCommand = new RelayCommand(delegate { GetClassByNameOrId(null); })); }
        }

        private void GetClassByNameOrId(object o)
        {
            Tbl21ClassesList =
                new ObservableCollection<Tbl21Class>((from classe in Tbl21ClassesRepository.Tbl21Classes
                                                       where classe.ClassName.StartsWith(SearchClassName)
                                                       orderby classe.ClassName
                                                       select classe));

            ClassesView = CollectionViewSource.GetDefaultView(Tbl21ClassesList);
            if (ClassesView != null)
                ClassesView.CurrentChanged += tbl21ClassView_CurrentChanged;                   
            RaisePropertyChanged();
        }
        //------------------------------------------------------------------------------
        private RelayCommand _addClassCommand;
        public new ICommand AddClassCommand
        {
            get { return _addClassCommand ?? (_addClassCommand = new RelayCommand(AddClass)); }
        }

        private void AddClass()
        {
            if (Tbl21ClassesList == null)
                Tbl21ClassesList = new ObservableCollection<Tbl21Class>();
            Tbl21ClassesList.Add(new Tbl21Class{ ClassName= "New " });                   
            ClassesView = CollectionViewSource.GetDefaultView(Tbl21ClassesList);
            if (ClassesView != null)
                ClassesView.CurrentChanged += tbl21ClassView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl21Class");
        }
        //----------------------------------------------------------------------------------------------------------
        private RelayCommand _deleteClassCommand;
        public ICommand ClassPhylumCommand
        {
            get { return _deleteClassCommand ?? (_deleteClassCommand = new RelayCommand(DeleteClass)); }
        }

        private void DeleteClass()
        {
            try
            {
                var classe= Tbl21ClassesRepository.Tbl21Classes.FirstOrDefault(x => x.ClassID== CurrentTbl21Class.ClassID);
                if (classe!= null)
                {
                    if (MessageBox.Show("Are you really shure to delete this "
                                        + CurrentTbl21Class.ClassName, "Question", MessageBoxButton.YesNo) !=
                        MessageBoxResult.Yes) return;
                    Tbl21ClassesRepository.Delete(classe);
                    Tbl21ClassesRepository.Save();
                    MessageBox.Show(CurrentTbl21Class.ClassName+ " was deleted successfully");
                    if (SearchClassName == null)
                    {
                        GetConnectedTablesById(); //refresh doubleClick                    
                    }
                    else
                    {
                        GetClassByName(); //search
                    }
                }
                else
                {
                    MessageBox.Show("Only " + CurrentTbl21Class.ClassName+ " can be deleted");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //-------------------------------------------------------------------------------------------------
        private RelayCommand _saveClassCommand;   
        public new ICommand SaveClassCommand
        {
            get { return _saveClassCommand ?? (_saveClassCommand = new RelayCommand(SaveClass)); }
        }

        private void SaveClass()
        {
            try
            {
                var classe= Tbl21ClassesRepository.Tbl21Classes.FirstOrDefault(x => x.ClassID== CurrentTbl21Class.ClassID);
                if (CurrentTbl21Class == null)
                {
                    MessageBox.Show("classe was not found");
                }
                else
                {
                    if (CurrentTbl21Class.ClassID!= 0)
                    {
                        if (classe!= null) //update
                        {
                            classe.Updater = Environment.UserName;
                            classe.UpdaterDate = DateTime.Now;
                        }
                    }
                    else
                    {
                        Tbl21ClassesRepository.Add(new Tbl21Class()
                        {
                            ClassName= CurrentTbl21Class.ClassName,              
                            CountID = TblCountersRepository.Counter(),
                            Valid = CurrentTbl21Class.Valid,
                            ValidYear = CurrentTbl21Class.ValidYear,
                            Synonym = CurrentTbl21Class.Synonym,
                            Author = CurrentTbl21Class.Author,
                            AuthorYear = CurrentTbl21Class.AuthorYear,
                            Info = CurrentTbl21Class.Info,
                            EngName = CurrentTbl21Class.EngName,
                            GerName = CurrentTbl21Class.GerName,
                            FraName = CurrentTbl21Class.FraName,
                            PorName = CurrentTbl21Class.PorName,
                            Writer = Environment.UserName,
                            WriterDate = DateTime.Now,
                            Updater = Environment.UserName,
                            UpdaterDate = DateTime.Now,
                            Memo = CurrentTbl21Class.Memo
                        });
                        }
                        {
                            Tbl21ClassesRepository.Save();
                            MessageBox.Show(CurrentTbl21Class.ClassName+  " was successfully saved ");
                            GetClassByName();  //Refresh
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


        
        #region "Public Commands Connect ==> Tbl27Infraclass"                 

        private RelayCommand _getInfraclassByNameCommand;
        public ICommand GetInfraclassByNameCommand
        {
            get { return _getInfraclassByNameCommand ?? (_getInfraclassByNameCommand = new RelayCommand(delegate { GetInfraclassByNameOrId(null); })); }
        }

        private void GetInfraclassByNameOrId(object o)
        {
            int id;
            if (int.TryParse(SearchInfraclassName, out id))
                Tbl27InfraclassesList = new ObservableCollection<Tbl27Infraclass> { Tbl27InfraclassesRepository.Get(id) };
            else
            Tbl27InfraclassesList =  new ObservableCollection<Tbl27Infraclass>
                                                      (from x in Tbl27InfraclassesRepository.FindAll()
                                                       where x.InfraclassName.StartsWith(SearchInfraclassName)
                                                       orderby x.InfraclassName
                                                       select x);

            InfraclassesView = CollectionViewSource.GetDefaultView(Tbl27InfraclassesList);
            if (InfraclassesView != null)
                InfraclassesView.CurrentChanged += tbl27InfraclassView_CurrentChanged;                   
            RaisePropertyChanged();
        }
        //------------------------------------------------------------------------------
        private RelayCommand _addInfraclassCommand;
        public ICommand AddInfraclassCommand
        {
            get { return _addInfraclassCommand ?? (_addInfraclassCommand = new RelayCommand(delegate { AddInfraclass(null); })); }
        }

        private void AddInfraclass(object o)
        {
            if (Tbl27InfraclassesList == null)
                Tbl27InfraclassesList = new ObservableCollection<Tbl27Infraclass>();
            Tbl27InfraclassesList.Add(new Tbl27Infraclass{ InfraclassName= CultRes.StringsRes.DatasetNew });                   
            InfraclassesView = CollectionViewSource.GetDefaultView(Tbl27InfraclassesList);
            if (InfraclassesView != null)
                InfraclassesView.CurrentChanged += tbl27InfraclassView_CurrentChanged;
            RaisePropertyChanged();
        }
        //---------------------------------------------------------------------------------------
        private RelayCommand _deleteInfraclassCommand;
        public ICommand DeleteInfraclassCommand
        {
            get { return _deleteInfraclassCommand ?? (_deleteInfraclassCommand = new RelayCommand(delegate { DeleteInfraclass(null); })); }
        }

        private void DeleteInfraclass(object o)
        {
            try
            {
                var infraclass = Tbl27InfraclassesRepository.Tbl27Infraclasses.FirstOrDefault(x => x.InfraclassID== CurrentTbl27Infraclass.InfraclassID);
                if (infraclass != null)
                {
                    if (MessageBox.Show(CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl27Infraclass.InfraclassName, CultRes.StringsRes.DeleteQuestion1, MessageBoxButton.YesNo) !=
                        MessageBoxResult.Yes) return;
                    Tbl27InfraclassesRepository.Delete(infraclass);
                    Tbl27InfraclassesRepository.Save();
                    MessageBox.Show(CurrentTbl27Infraclass.InfraclassName + " " + CultRes.StringsRes.DeleteSuccess);
                    GetInfraclassByNameOrId(o);  //Refresh
                }
                else
                {
                    MessageBox.Show(CultRes.StringsRes.DeleteCan + " " + CurrentTbl27Infraclass.InfraclassName+ " " + CultRes.StringsRes.DeleteCan1);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //-------------------------------------------------------------------------------------------------
        private RelayCommand _saveInfraclassCommand;   
        public ICommand SaveInfraclassCommand
        {
            get { return _saveInfraclassCommand ?? (_saveInfraclassCommand = new RelayCommand(delegate { SaveInfraclass(null); })); }
        }

        private void SaveInfraclass(object o)
        {
            try
            {
                var infraclass = Tbl27InfraclassesRepository.Tbl27Infraclasses.FirstOrDefault(x => x.InfraclassID== CurrentTbl27Infraclass.InfraclassID);
                if (CurrentTbl27Infraclass == null)
                {
                    MessageBox.Show(CultRes.StringsRes.DatasetNotExist);
                }
                else
                {
                    if (CurrentTbl27Infraclass.InfraclassID!= 0)
                    {
                        if (infraclass!= null) //update
                        {
                            infraclass.Updater = Environment.UserName;
                            infraclass.UpdaterDate = DateTime.Now;
                        }
                    }
                    else
                    {
                        Tbl27InfraclassesRepository.Add(new Tbl27Infraclass
                        {
                            SubclassID= CurrentTbl27Infraclass.SubclassID,              
                            InfraclassName= CurrentTbl27Infraclass.InfraclassName,              
                            CountID = RandomHelper.Randomnumber(),
                            Valid = CurrentTbl27Infraclass.Valid,
                            ValidYear = CurrentTbl27Infraclass.ValidYear,
                            Synonym = CurrentTbl27Infraclass.Synonym,
                            Author = CurrentTbl27Infraclass.Author,
                            AuthorYear = CurrentTbl27Infraclass.AuthorYear,
                            Info = CurrentTbl27Infraclass.Info,
                            EngName = CurrentTbl27Infraclass.EngName,
                            GerName = CurrentTbl27Infraclass.GerName,
                            FraName = CurrentTbl27Infraclass.FraName,
                            PorName = CurrentTbl27Infraclass.PorName,
                            Writer = Environment.UserName,
                            WriterDate = DateTime.Now,
                            Updater = Environment.UserName,
                            UpdaterDate = DateTime.Now,
                            Memo = CurrentTbl27Infraclass.Memo
                        });
                    }
                    {
                        Tbl27InfraclassesRepository.Save();
                        if (CurrentTbl27Infraclass != null)
                            MessageBox.Show(CurrentTbl27Infraclass.InfraclassName+ " " + CultRes.StringsRes.SaveSuccess);
                        GetInfraclassByNameOrId(o);  //Refresh      
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
                            SubclassID= CurrentTbl90RefAuthor.SubclassID,
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
                            SubclassID= CurrentTbl90RefSource.SubclassID,
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
                            SubclassID= CurrentTbl90RefExpert.SubclassID,
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
                            SubclassID= CurrentTbl93Comment.SubclassID,                
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
            SearchClassName = null;                       
            SearchInfraclassName = null;
            SearchCommentInfo = null;
            SearchRefExpertName = null;
            SearchRefSourceName = null;
            SearchRefAuthorName = null;

            Tbl21ClassesList =
                new ObservableCollection<Tbl21Class>((from classe in Tbl21ClassesRepository.Tbl21Classes
                                                       where classe.ClassID== CurrentTbl24Subclass.ClassID
                                                       orderby classe.ClassName
                                                       select classe));

            ClassesView = CollectionViewSource.GetDefaultView(Tbl21ClassesList);
            if (ClassesView != null)
                ClassesView.CurrentChanged += tbl21ClassView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl21Classes");
            //-----------------------------------------------------------------------------------
            Tbl27InfraclassesList =
                new ObservableCollection<Tbl27Infraclass>((from infraclass in Tbl27InfraclassesRepository.Tbl27Infraclasses
                                                       where infraclass.SubclassID== CurrentTbl24Subclass.SubclassID
                                                       orderby infraclass.Tbl24Subclasses.SubclassName
                                                       select infraclass));


            InfraclassesView = CollectionViewSource.GetDefaultView(Tbl27InfraclassesList);
            if (InfraclassesView != null)
                InfraclassesView.CurrentChanged += tbl27InfraclassView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl27Infraclasses");
            //-----------------------------------------------------------------------------------
            Tbl90RefAuthorsList =
                new ObservableCollection<Tbl90Reference>((from refAu in Tbl90ReferencesRepository.Tbl90References
                                                          where refAu.SubclassID== CurrentTbl24Subclass.SubclassID
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
                                                          where refSo.SubclassID== CurrentTbl24Subclass.SubclassID
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
                                                          where refEx.SubclassID== CurrentTbl24Subclass.SubclassID
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
                                                          where comm.SubclassID== CurrentTbl24Subclass.SubclassID
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



     
        #region "Public Properties Tbl24Subclass"

        public new ICollectionView SubclassesView;
        public new Tbl24Subclass CurrentTbl24Subclass
        {
            get
            {
                if (SubclassesView != null)
                    return SubclassesView.CurrentItem as Tbl24Subclass;
                return null;
            }
        }
        //--------------------------------------------
        private string _searchSubclassName;
        public new string SearchSubclassName
        {
            get { return _searchSubclassName; }
            set
            {
                if (value == _searchSubclassName) return;
                _searchSubclassName = value;
                RaisePropertyChanged("SearchSubclassName");
            }
        }

        private ObservableCollection<Tbl24Subclass> _tbl24SubclassesList;
        public new ObservableCollection<Tbl24Subclass> Tbl24SubclassesList
        {
            get { return _tbl24SubclassesList; }
            set
            {
                if (_tbl24SubclassesList == value) return;
                _tbl24SubclassesList = value;
                RaisePropertyChanged("Tbl24SubclassesList");

                //Clear Search-TextBox
                SearchClassName = null;                                
                SearchInfraclassName = null;
                SearchCommentInfo = null;
                SearchRefExpertName = null;
                SearchRefSourceName = null;
                SearchRefAuthorName = null;
            }
        }

        private ObservableCollection<Tbl24Subclass> _tbl24SubclassesAllList;
        public ObservableCollection<Tbl24Subclass> Tbl24SubclassesAllList
        {
            get { return _tbl24SubclassesAllList; }
            set
            {
                if (_tbl24SubclassesAllList == value) return;
                _tbl24SubclassesAllList = value;
                RaisePropertyChanged("Tbl24SubclassesAllList");
            }
        }

        #endregion "Public Properties"   

       
        #region "Public Properties Tbl21Class"

        public  ICollectionView ClassesView;
        public  Tbl21Class CurrentTbl21Class
        {
            get
            {
                if (ClassesView != null)
                    return ClassesView.CurrentItem as Tbl21Class;
                return null;
            }
        }
        //--------------------------------------------                                               

        private string _searchClassName;
        public  string SearchClassName
        {
            get { return _searchClassName; }
            set
            {
                if (value == _searchClassName) return;
                _searchClassName = value;
                RaisePropertyChanged("SearchClassName");
            }
        }

        private ObservableCollection<Tbl21Class> _tbl21ClassesList;
        public  ObservableCollection<Tbl21Class> Tbl21ClassesList
        {
            get { return _tbl21ClassesList; }
            set
            {
                if (_tbl21ClassesList == value) return;
                _tbl21ClassesList = value;
                RaisePropertyChanged("Tbl21ClassesList");
            }
        }

        #endregion "Public Properties"
   
  
       
        #region "Public Properties Tbl27Infraclass"

        public ICollectionView InfraclassesView;
        public Tbl27Infraclass CurrentTbl27Infraclass
        {
            get
            {
                if (InfraclassesView != null)
                    return InfraclassesView.CurrentItem as Tbl27Infraclass;
                return null;
            }
        }
        //--------------------------------------------                                               

        private string _searchInfraclassName;
        public string SearchInfraclassName
        {
            get { return _searchInfraclassName; }
            set
            {
                if (value == _searchInfraclassName) return;
                _searchInfraclassName = value;
                RaisePropertyChanged("SearchInfraclassName");
            }
        }

        private ObservableCollection<Tbl27Infraclass> _tbl27InfraclassesList;
        public ObservableCollection<Tbl27Infraclass> Tbl27InfraclassesList
        {
            get { return _tbl27InfraclassesList; }
            set
            {
                if (_tbl27InfraclassesList == value) return;
                _tbl27InfraclassesList = value;
                RaisePropertyChanged("Tbl27InfraclassesList");
            }
        }

        #endregion "Public Properties"
   
   

 //    Part 11    

      }
}   
