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

//    Tbl18SuperclassesViewModel Skriptdatum:  21.02.2016  12:32    

namespace WPFUI.Views.Database
{   


    
    public class Tbl18SuperclassesViewModel : Tbl15SubdivisionsViewModel                     
    {     
        
        #region "Private Data Members"   
          
        protected readonly Tbl12SubphylumsRepository Tbl12SubphylumsRepository = new Tbl12SubphylumsRepository();   
           
        protected readonly Tbl21ClassesRepository Tbl21ClassesRepository = new Tbl21ClassesRepository();   
          
          #endregion "Private Data Members"            
    
        #region "Constructor"

        public Tbl18SuperclassesViewModel()
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

       
        #region "Public Commands Basic Tbl18Superclass"

        private RelayCommand _getSuperclassByNameCommand;
        public new ICommand GetSuperclassByNameCommand
        {
            get { return _getSuperclassByNameCommand ?? (_getSuperclassByNameCommand = new RelayCommand(delegate { GetSuperclassByNameOrId(null); })); }   
        }

        private void GetSuperclassByNameOrId(object o)       
        {   
Tbl18SuperclassesList =  new ObservableCollection<Tbl18Superclass>
                                                       (from x in Tbl18SuperclassesRepository.Tbl18Superclasses
                                                        where x.SuperclassName.StartsWith(SearchSuperclassName)
                                                        orderby x.SuperclassName
                                                        select x);

            Tbl18SuperclassesAllList =  new ObservableCollection<Tbl18Superclass>
                                                       (from y in Tbl18SuperclassesRepository.Tbl18Superclasses
                                                        orderby y.SuperclassName
                                                        select y);

            Tbl15SubdivisionsAllList =  new ObservableCollection<Tbl15Subdivision>
                                                       (from z in Tbl15SubdivisionsRepository.Tbl15Subdivisions
                                                        orderby z.SubdivisionName, z.Subregnum
                                                        select z);

            Tbl12SubphylumsAllList =  new ObservableCollection<Tbl12Subphylum>
                                                       (from z in Tbl12SubphylumsRepository.Tbl12Subphylums
                                                        orderby z.SubphylumName
                                                        select z);

            Tbl09DivisionsAllList =   new ObservableCollection<Tbl09Division>
                                                       (from z in Tbl09DivisionsRepository.Tbl09Divisions
                                                        orderby z.DivisionName, z.Subregnum
                                                        select z);

            Tbl06PhylumsAllList =   new ObservableCollection<Tbl06Phylum>
                                                       (from z in Tbl06PhylumsRepository.Tbl06Phylums
                                                        orderby z.PhylumName
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

  
  
                Tbl06PhylumsList = null;                 
                Tbl09DivisionsList = null;                       
                Tbl12SubphylumsList = null;                       
                Tbl15SubdivisionsList = null;                       
SuperclassesView = CollectionViewSource.GetDefaultView(Tbl18SuperclassesList);
            if (SuperclassesView != null)
                SuperclassesView.CurrentChanged += tbl18SuperclassView_CurrentChanged;                   
            RaisePropertyChanged();
        }
        //------------------------------------------------------------------------------
        private RelayCommand _addSuperclassCommand;
        public new ICommand AddSuperclassCommand
        {
            get { return _addSuperclassCommand ?? (_addSuperclassCommand = new RelayCommand(AddSuperclass)); }
        }

        private void AddSuperclass(object o)
        {
            if (Tbl18SuperclassesList == null)
                Tbl18SuperclassesList = new ObservableCollection<Tbl18Superclass>();
            Tbl18SuperclassesList.Add(new Tbl18Superclass{ SuperclassName= CultRes.StringsRes.DatasetNew });
            SuperclassesView = CollectionViewSource.GetDefaultView(Tbl18SuperclassesList);
            if (SuperclassesView != null)
                SuperclassesView.CurrentChanged += tbl18SuperclassView_CurrentChanged;
            RaisePropertyChanged();
        }
        //---------------------------------------------------------------------------------------
  
       
        private RelayCommand _deleteSuperclassCommand;
        public new ICommand DeleteSuperclassCommand
        {
            get { return _deleteSuperclassCommand ?? (_deleteSuperclassCommand = new RelayCommand(delegate { DeleteSuperclass(null); })); }
        }

        private void DeleteSuperclass(object o)
        {
            try
            {
                var superclass= Tbl18SuperclassesRepository.Tbl18Superclasses.FirstOrDefault(x => x.SuperclassID== CurrentTbl18Superclass.SuperclassID);
                if (superclass!= null)
                {
                    if (MessageBox.Show(CultRes.StringsRes.DeleteQuestion
                                        + " " +  CurrentTbl18Superclass.SuperclassName, CultRes.StringsRes.DeleteQuestion1, MessageBoxButton.YesNo) !=
                        MessageBoxResult.Yes) return;
                    Tbl18SuperclassesRepository.Delete(superclass);
                    Tbl18SuperclassesRepository.Save();
                    MessageBox.Show(CurrentTbl18Superclass.SuperclassName + " " + CultRes.StringsRes.DeleteSuccess);
                    GetSuperclassByNameOrId(o); //Refresh
                }
                else
                {
                    MessageBox.Show(CultRes.StringsRes.DeleteCan + " " + CurrentTbl18Superclass.SuperclassName+ " " + CultRes.StringsRes.DeleteCan1);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //-------------------------------------------------------------------------------------------------
        private RelayCommand _saveSuperclassCommand;
        public new ICommand SaveSuperclassCommand
        {
            get { return _saveSuperclassCommand ?? (_saveSuperclassCommand = new RelayCommand(delegate { SaveSuperclass(null); })); }
        }

        private void SaveSuperclass(object o)
        {
            try
            {
                var superclass= Tbl18SuperclassesRepository.Tbl18Superclasses.FirstOrDefault(x => x.SuperclassID== CurrentTbl18Superclass.SuperclassID);
                if (CurrentTbl18Superclass == null)
                {
                    MessageBox.Show(CultRes.StringsRes.DatasetNotExist);
                }
                else
                {
                    if (CurrentTbl18Superclass.SuperclassID!= 0)
                    {
                        if (superclass!= null) //update
                        {
                            superclass.Updater = Environment.UserName;
                            superclass.UpdaterDate = DateTime.Now;
                        }
                    }
                    else
                    {
                        Tbl18SuperclassesRepository.Add(new Tbl18Superclass
                        {
                            SubdivisionID= CurrentTbl18Superclass.SubdivisionID,              
                            SuperclassName= CurrentTbl18Superclass.SuperclassName,              
                            CountID = RandomHelper.Randomnumber(),
                            Valid = CurrentTbl18Superclass.Valid,
                            ValidYear = CurrentTbl18Superclass.ValidYear,
                            Synonym = CurrentTbl18Superclass.Synonym,
                            Author = CurrentTbl18Superclass.Author,
                            AuthorYear = CurrentTbl18Superclass.AuthorYear,
                            Info = CurrentTbl18Superclass.Info,
                            EngName = CurrentTbl18Superclass.EngName,
                            GerName = CurrentTbl18Superclass.GerName,
                            FraName = CurrentTbl18Superclass.FraName,
                            PorName = CurrentTbl18Superclass.PorName,
                            Writer = Environment.UserName,
                            WriterDate = DateTime.Now,
                            Updater = Environment.UserName,
                            UpdaterDate = DateTime.Now,
                            Memo = CurrentTbl18Superclass.Memo
                        });
                    }
                    {
                        Tbl18SuperclassesRepository.Save();
                        MessageBox.Show(CurrentTbl18Superclass.SuperclassName+ " " + CultRes.StringsRes.SaveSuccess);
                        GetSuperclassByNameOrId(o);  //Refresh
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


        
        #region "Public Commands Connect <== Tbl15Subdivision"                 

        private RelayCommand _getSubdivisionByNameCommand;
        public new ICommand GetSubdivisionByNameCommand
        {
            get { return _getSubdivisionByNameCommand ?? (_getSubdivisionByNameCommand = new RelayCommand(delegate { GetSubdivisionByNameOrId(null); })); }
        }

        private void GetSubdivisionByNameOrId(object o)
        {
            Tbl15SubdivisionsList =
                new ObservableCollection<Tbl15Subdivision>((from subdivision in Tbl15SubdivisionsRepository.Tbl15Subdivisions
                                                       where subdivision.SubdivisionName.StartsWith(SearchSubdivisionName)
                                                       orderby subdivision.SubdivisionName
                                                       select subdivision));

            SubdivisionsView = CollectionViewSource.GetDefaultView(Tbl15SubdivisionsList);
            if (SubdivisionsView != null)
                SubdivisionsView.CurrentChanged += tbl15SubdivisionView_CurrentChanged;                   
            RaisePropertyChanged();
        }
        //------------------------------------------------------------------------------
        private RelayCommand _addSubdivisionCommand;
        public new ICommand AddSubdivisionCommand
        {
            get { return _addSubdivisionCommand ?? (_addSubdivisionCommand = new RelayCommand(AddSubdivision)); }
        }

        private void AddSubdivision()
        {
            if (Tbl15SubdivisionsList == null)
                Tbl15SubdivisionsList = new ObservableCollection<Tbl15Subdivision>();
            Tbl15SubdivisionsList.Add(new Tbl15Subdivision{ SubdivisionName= "New " });                   
            SubdivisionsView = CollectionViewSource.GetDefaultView(Tbl15SubdivisionsList);
            if (SubdivisionsView != null)
                SubdivisionsView.CurrentChanged += tbl15SubdivisionView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl15Subdivision");
        }
        //----------------------------------------------------------------------------------------------------------
        private RelayCommand _deleteSubdivisionCommand;
        public ICommand SubdivisionPhylumCommand
        {
            get { return _deleteSubdivisionCommand ?? (_deleteSubdivisionCommand = new RelayCommand(DeleteSubdivision)); }
        }

        private void DeleteSubdivision()
        {
            try
            {
                var subdivision= Tbl15SubdivisionsRepository.Tbl15Subdivisions.FirstOrDefault(x => x.SubdivisionID== CurrentTbl15Subdivision.SubdivisionID);
                if (subdivision!= null)
                {
                    if (MessageBox.Show("Are you really shure to delete this "
                                        + CurrentTbl15Subdivision.SubdivisionName, "Question", MessageBoxButton.YesNo) !=
                        MessageBoxResult.Yes) return;
                    Tbl15SubdivisionsRepository.Delete(subdivision);
                    Tbl15SubdivisionsRepository.Save();
                    MessageBox.Show(CurrentTbl15Subdivision.SubdivisionName+ " was deleted successfully");
                    if (SearchSubdivisionName == null)
                    {
                        GetConnectedTablesById(); //refresh doubleClick                    
                    }
                    else
                    {
                        GetSubdivisionByName(); //search
                    }
                }
                else
                {
                    MessageBox.Show("Only " + CurrentTbl15Subdivision.SubdivisionName+ " can be deleted");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //-------------------------------------------------------------------------------------------------
        private RelayCommand _saveSubdivisionCommand;   
        public new ICommand SaveSubdivisionCommand
        {
            get { return _saveSubdivisionCommand ?? (_saveSubdivisionCommand = new RelayCommand(SaveSubdivision)); }
        }

        private void SaveSubdivision()
        {
            try
            {
                var subdivision= Tbl15SubdivisionsRepository.Tbl15Subdivisions.FirstOrDefault(x => x.SubdivisionID== CurrentTbl15Subdivision.SubdivisionID);
                if (CurrentTbl15Subdivision == null)
                {
                    MessageBox.Show("subdivision was not found");
                }
                else
                {
                    if (CurrentTbl15Subdivision.SubdivisionID!= 0)
                    {
                        if (subdivision!= null) //update
                        {
                            subdivision.Updater = Environment.UserName;
                            subdivision.UpdaterDate = DateTime.Now;
                        }
                    }
                    else
                    {
                        Tbl15SubdivisionsRepository.Add(new Tbl15Subdivision()
                        {
                            SubdivisionName= CurrentTbl15Subdivision.SubdivisionName,              
                            CountID = TblCountersRepository.Counter(),
                            Valid = CurrentTbl15Subdivision.Valid,
                            ValidYear = CurrentTbl15Subdivision.ValidYear,
                            Synonym = CurrentTbl15Subdivision.Synonym,
                            Author = CurrentTbl15Subdivision.Author,
                            AuthorYear = CurrentTbl15Subdivision.AuthorYear,
                            Info = CurrentTbl15Subdivision.Info,
                            EngName = CurrentTbl15Subdivision.EngName,
                            GerName = CurrentTbl15Subdivision.GerName,
                            FraName = CurrentTbl15Subdivision.FraName,
                            PorName = CurrentTbl15Subdivision.PorName,
                            Writer = Environment.UserName,
                            WriterDate = DateTime.Now,
                            Updater = Environment.UserName,
                            UpdaterDate = DateTime.Now,
                            Memo = CurrentTbl15Subdivision.Memo
                        });
                        }
                        {
                            Tbl15SubdivisionsRepository.Save();
                            MessageBox.Show(CurrentTbl15Subdivision.SubdivisionName+  " was successfully saved ");
                            GetSubdivisionByName();  //Refresh
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


        
        #region "Public Commands Connect ==> Tbl21Class"                 

        private RelayCommand _getClassByNameCommand;
        public ICommand GetClassByNameCommand
        {
            get { return _getClassByNameCommand ?? (_getClassByNameCommand = new RelayCommand(delegate { GetClassByNameOrId(null); })); }
        }

        private void GetClassByNameOrId(object o)
        {
            int id;
            if (int.TryParse(SearchClassName, out id))
                Tbl21ClassesList = new ObservableCollection<Tbl21Class> { Tbl21ClassesRepository.Get(id) };
            else
            Tbl21ClassesList =  new ObservableCollection<Tbl21Class>
                                                      (from x in Tbl21ClassesRepository.FindAll()
                                                       where x.ClassName.StartsWith(SearchClassName)
                                                       orderby x.ClassName
                                                       select x);

            ClassesView = CollectionViewSource.GetDefaultView(Tbl21ClassesList);
            if (ClassesView != null)
                ClassesView.CurrentChanged += tbl21ClassView_CurrentChanged;                   
            RaisePropertyChanged();
        }
        //------------------------------------------------------------------------------
        private RelayCommand _addClassCommand;
        public ICommand AddClassCommand
        {
            get { return _addClassCommand ?? (_addClassCommand = new RelayCommand(delegate { AddClass(null); })); }
        }

        private void AddClass(object o)
        {
            if (Tbl21ClassesList == null)
                Tbl21ClassesList = new ObservableCollection<Tbl21Class>();
            Tbl21ClassesList.Add(new Tbl21Class{ ClassName= CultRes.StringsRes.DatasetNew });                   
            ClassesView = CollectionViewSource.GetDefaultView(Tbl21ClassesList);
            if (ClassesView != null)
                ClassesView.CurrentChanged += tbl21ClassView_CurrentChanged;
            RaisePropertyChanged();
        }
        //---------------------------------------------------------------------------------------
        private RelayCommand _deleteClassCommand;
        public ICommand DeleteClassCommand
        {
            get { return _deleteClassCommand ?? (_deleteClassCommand = new RelayCommand(delegate { DeleteClass(null); })); }
        }

        private void DeleteClass(object o)
        {
            try
            {
                var classe = Tbl21ClassesRepository.Tbl21Classes.FirstOrDefault(x => x.ClassID== CurrentTbl21Class.ClassID);
                if (classe != null)
                {
                    if (MessageBox.Show(CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl21Class.ClassName, CultRes.StringsRes.DeleteQuestion1, MessageBoxButton.YesNo) !=
                        MessageBoxResult.Yes) return;
                    Tbl21ClassesRepository.Delete(classe);
                    Tbl21ClassesRepository.Save();
                    MessageBox.Show(CurrentTbl21Class.ClassName + " " + CultRes.StringsRes.DeleteSuccess);
                    GetClassByNameOrId(o);  //Refresh
                }
                else
                {
                    MessageBox.Show(CultRes.StringsRes.DeleteCan + " " + CurrentTbl21Class.ClassName+ " " + CultRes.StringsRes.DeleteCan1);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //-------------------------------------------------------------------------------------------------
        private RelayCommand _saveClassCommand;   
        public ICommand SaveClassCommand
        {
            get { return _saveClassCommand ?? (_saveClassCommand = new RelayCommand(delegate { SaveClass(null); })); }
        }

        private void SaveClass(object o)
        {
            try
            {
                var classe = Tbl21ClassesRepository.Tbl21Classes.FirstOrDefault(x => x.ClassID== CurrentTbl21Class.ClassID);
                if (CurrentTbl21Class == null)
                {
                    MessageBox.Show(CultRes.StringsRes.DatasetNotExist);
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
                        Tbl21ClassesRepository.Add(new Tbl21Class
                        {
                            SuperclassID= CurrentTbl21Class.SuperclassID,              
                            ClassName= CurrentTbl21Class.ClassName,              
                            CountID = RandomHelper.Randomnumber(),
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
                        if (CurrentTbl21Class != null)
                            MessageBox.Show(CurrentTbl21Class.ClassName+ " " + CultRes.StringsRes.SaveSuccess);
                        GetClassByNameOrId(o);  //Refresh      
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
                            SuperclassID= CurrentTbl90RefAuthor.SuperclassID,
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
                            SuperclassID= CurrentTbl90RefSource.SuperclassID,
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
                            SuperclassID= CurrentTbl90RefExpert.SuperclassID,
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
                            SuperclassID= CurrentTbl93Comment.SuperclassID,                
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
            SearchSubphylumName = null;
            SearchSubdivisionName = null;
            SearchClassName = null;
            SearchCommentInfo = null;
            SearchRefExpertName = null;
            SearchRefSourceName = null;
            SearchRefAuthorName = null;

            //-----------------------------------------------------------------------------------
            Tbl12SubphylumsList =
                new ObservableCollection<Tbl12Subphylum>((from subphy in Tbl12SubphylumsRepository.Tbl12Subphylums
                                                          where subphy.SubphylumID == CurrentTbl18Superclass.SubphylumID
                                                          orderby subphy.SubphylumName
                                                          select subphy));


            SubphylumsView = CollectionViewSource.GetDefaultView(Tbl12SubphylumsList);
            if (SubphylumsView != null)
                SubphylumsView.CurrentChanged += tbl12SubphylumView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl12Subphylum");
            //-----------------------------------------------------------------------------------
            Tbl15SubdivisionsList =
                new ObservableCollection<Tbl15Subdivision>((from subdivision in Tbl15SubdivisionsRepository.Tbl15Subdivisions
                                                            where subdivision.SubdivisionID == CurrentTbl18Superclass.SubdivisionID
                                                            orderby subdivision.SubdivisionName
                                                            select subdivision));

            SubdivisionsView = CollectionViewSource.GetDefaultView(Tbl15SubdivisionsList);
            if (SubdivisionsView != null)
                SubdivisionsView.CurrentChanged += tbl15SubdivisionView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl15Subdivisions");
            //-----------------------------------------------------------------------------------
            Tbl21ClassesList =
                new ObservableCollection<Tbl21Class>((from classe in Tbl21ClassesRepository.Tbl21Classes
                                                      where classe.SuperclassID == CurrentTbl18Superclass.SuperclassID
                                                      orderby classe.ClassName
                                                      select classe));


            ClassesView = CollectionViewSource.GetDefaultView(Tbl21ClassesList);
            if (ClassesView != null)
                ClassesView.CurrentChanged += tbl21ClassView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl21Classes");
            //-----------------------------------------------------------------------------------
            Tbl90RefAuthorsList =
                new ObservableCollection<Tbl90Reference>((from refAu in Tbl90ReferencesRepository.Tbl90References
                                                          where refAu.SuperclassID == CurrentTbl18Superclass.SuperclassID
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
                                                          where refSo.SuperclassID == CurrentTbl18Superclass.SuperclassID
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
                                                          where refEx.SuperclassID == CurrentTbl18Superclass.SuperclassID
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
                                                        where comm.SuperclassID == CurrentTbl18Superclass.SuperclassID
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



     
        #region "Public Properties Tbl18Superclass"

        public new ICollectionView SuperclassesView;
        public new Tbl18Superclass CurrentTbl18Superclass
        {
            get
            {
                if (SuperclassesView != null)
                    return SuperclassesView.CurrentItem as Tbl18Superclass;
                return null;
            }
        }
        //--------------------------------------------
        private string _searchSuperclassName;
        public new string SearchSuperclassName
        {
            get { return _searchSuperclassName; }
            set
            {
                if (value == _searchSuperclassName) return;
                _searchSuperclassName = value;
                RaisePropertyChanged("SearchSuperclassName");
            }
        }

        private ObservableCollection<Tbl18Superclass> _tbl18SuperclassesList;
        public new ObservableCollection<Tbl18Superclass> Tbl18SuperclassesList
        {
            get { return _tbl18SuperclassesList; }
            set
            {
                if (_tbl18SuperclassesList == value) return;
                _tbl18SuperclassesList = value;
                RaisePropertyChanged("Tbl18SuperclassesList");

                //Clear Search-TextBox
                SearchSubdivisionName = null;                                
                SearchSubphylumName = null;                                
                SearchClassName = null;
                SearchCommentInfo = null;
                SearchRefExpertName = null;
                SearchRefSourceName = null;
                SearchRefAuthorName = null;
            }
        }

        private ObservableCollection<Tbl18Superclass> _tbl18SuperclassesAllList;
        public ObservableCollection<Tbl18Superclass> Tbl18SuperclassesAllList
        {
            get { return _tbl18SuperclassesAllList; }
            set
            {
                if (_tbl18SuperclassesAllList == value) return;
                _tbl18SuperclassesAllList = value;
                RaisePropertyChanged("Tbl18SuperclassesAllList");
            }
        }

        private ObservableCollection<Tbl12Subphylum> _tbl12SubphylumsAllList;
        public ObservableCollection<Tbl12Subphylum> Tbl12SubphylumsAllList
        {
            get { return _tbl12SubphylumsAllList; }
            set
            {
                if (_tbl12SubphylumsAllList == value) return;
                _tbl12SubphylumsAllList = value;
                RaisePropertyChanged("Tbl12SubphylumsAllList");
            }
        }

        private ObservableCollection<Tbl06Phylum> _tbl06PhylumsAllList;
        public ObservableCollection<Tbl06Phylum> Tbl06PhylumsAllList
        {
            get { return _tbl06PhylumsAllList; }
            set
            {
                if (_tbl06PhylumsAllList == value) return;
                _tbl06PhylumsAllList = value;
                RaisePropertyChanged("Tbl06PhylumsAllList");
            }
        }

        #endregion "Public Properties"
   

       
        #region "Public Properties Tbl15Subdivision"

        public  ICollectionView SubdivisionsView;
        public  Tbl15Subdivision CurrentTbl15Subdivision
        {
            get
            {
                if (SubdivisionsView != null)
                    return SubdivisionsView.CurrentItem as Tbl15Subdivision;
                return null;
            }
        }
        //--------------------------------------------                                               

        private string _searchSubdivisionName;
        public  string SearchSubdivisionName
        {
            get { return _searchSubdivisionName; }
            set
            {
                if (value == _searchSubdivisionName) return;
                _searchSubdivisionName = value;
                RaisePropertyChanged("SearchSubdivisionName");
            }
        }

        private ObservableCollection<Tbl15Subdivision> _tbl15SubdivisionsList;
        public  ObservableCollection<Tbl15Subdivision> Tbl15SubdivisionsList
        {
            get { return _tbl15SubdivisionsList; }
            set
            {
                if (_tbl15SubdivisionsList == value) return;
                _tbl15SubdivisionsList = value;
                RaisePropertyChanged("Tbl15SubdivisionsList");
            }
        }

        #endregion "Public Properties"
   
  
       
        #region "Public Properties Tbl12Subphylum"

        public  ICollectionView SubphylumsView;
        public  Tbl12Subphylum CurrentTbl12Subphylum
        {
            get
            {
                if (SubphylumsView != null)
                    return SubphylumsView.CurrentItem as Tbl12Subphylum;
                return null;
            }
        }
        //--------------------------------------------                                               

        private string _searchSubphylumName;
        public  string SearchSubphylumName
        {
            get { return _searchSubphylumName; }
            set
            {
                if (value == _searchSubphylumName) return;
                _searchSubphylumName = value;
                RaisePropertyChanged("SearchSubphylumName");
            }
        }

        private ObservableCollection<Tbl12Subphylum> _tbl12SubphylumsList;
        public  ObservableCollection<Tbl12Subphylum> Tbl12SubphylumsList
        {
            get { return _tbl12SubphylumsList; }
            set
            {
                if (_tbl12SubphylumsList == value) return;
                _tbl12SubphylumsList = value;
                RaisePropertyChanged("Tbl12SubphylumsList");
            }
        }

   
  
       
        #region "Public Properties Tbl21Class"

        public ICollectionView ClassesView;
        public Tbl21Class CurrentTbl21Class
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
        public string SearchClassName
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
        public ObservableCollection<Tbl21Class> Tbl21ClassesList
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
   
   

 //    Part 11    

        
        #region "Private Methods"

        public void tbl12SubphylumView_CurrentChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged("CurrentTbl12Subphylum");
        }

        public void tbl21ClassView_CurrentChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged("CurrentTbl21Class");
        }
        #endregion "Private Methods"
   
      }
}   
