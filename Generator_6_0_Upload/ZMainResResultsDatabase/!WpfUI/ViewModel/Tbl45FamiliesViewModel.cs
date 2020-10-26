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

//    Tbl45FamiliesViewModel Skriptdatum:  31.03.2014  10:32    

namespace WPFUI.Views.Database
{   


    
    public class Tbl45FamiliesViewModel : Tbl42SuperfamiliesViewModel                     
    {     
         
      #region "Private Data Members"

        protected readonly Tbl48SubfamiliesRepository Tbl48SubfamiliesRepository = new Tbl48SubfamiliesRepository();   
          
          #endregion "Private Data Members"            
    
        #region "Constructor"

        public Tbl45FamiliesViewModel()
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

       
        #region "Public Commands Basic Tbl45Family"

        private RelayCommand _getFamilyByNameCommand;
        public new ICommand GetFamilyByNameCommand
        {
            get { return _getFamilyByNameCommand ?? (_getFamilyByNameCommand = new RelayCommand(delegate { GetFamilyByNameOrId(null); })); }   
        }

        private void GetFamilyByNameOrId(object o)       
        {   
Tbl45FamiliesList =  new ObservableCollection<Tbl45Family>
                                                       (from x in Tbl45FamiliesRepository.Tbl45Families
                                                        where x.FamilyName.StartsWith(SearchFamilyName)
                                                        orderby x.FamilyName
                                                        select x);

            Tbl45FamiliesAllList =  new ObservableCollection<Tbl45Family>
                                                       (from y in Tbl45FamiliesRepository.Tbl45Families
                                                        orderby y.FamilyName
                                                        select y);

            Tbl42SuperfamiliesAllList =  new ObservableCollection<Tbl42Superfamily>
                                                       (from z in Tbl42SuperfamiliesRepository.Tbl42Superfamilies
                                                        orderby z.SuperfamilyName
                                                        select z);

              
  Tbl39InfraordosAllList =  new ObservableCollection<Tbl39Infraordo>
                                                       (from z in Tbl39InfraordosRepository.Tbl39Infraordos
                                                        orderby z.InfraordoName
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

  
Tbl42SuperfamiliesList = null;                  
  Tbl48SubfamiliesList = null;               
  FamiliesView = CollectionViewSource.GetDefaultView(Tbl45FamiliesList);
            if (FamiliesView != null)
                FamiliesView.CurrentChanged += tbl45FamilyView_CurrentChanged;                   
            RaisePropertyChanged();
        }
        //------------------------------------------------------------------------------
        private RelayCommand _addFamilyCommand;
        public new ICommand AddFamilyCommand
        {
            get { return _addFamilyCommand ?? (_addFamilyCommand = new RelayCommand(AddFamily)); }
        }

        private void AddFamily(object o)
        {
            if (Tbl45FamiliesList == null)
                Tbl45FamiliesList = new ObservableCollection<Tbl45Family>();
            Tbl45FamiliesList.Add(new Tbl45Family{ FamilyName= CultRes.StringsRes.DatasetNew });
            FamiliesView = CollectionViewSource.GetDefaultView(Tbl45FamiliesList);
            if (FamiliesView != null)
                FamiliesView.CurrentChanged += tbl45FamilyView_CurrentChanged;
            RaisePropertyChanged();
        }
        //---------------------------------------------------------------------------------------
  
       
        private RelayCommand _deleteFamilyCommand;
        public new ICommand DeleteFamilyCommand
        {
            get { return _deleteFamilyCommand ?? (_deleteFamilyCommand = new RelayCommand(delegate { DeleteFamily(null); })); }
        }

        private void DeleteFamily(object o)
        {
            try
            {
                var family= Tbl45FamiliesRepository.Tbl45Families.FirstOrDefault(x => x.FamilyID== CurrentTbl45Family.FamilyID);
                if (family!= null)
                {
                    if (MessageBox.Show(CultRes.StringsRes.DeleteQuestion
                                        + " " +  CurrentTbl45Family.FamilyName, CultRes.StringsRes.DeleteQuestion1, MessageBoxButton.YesNo) !=
                        MessageBoxResult.Yes) return;
                    Tbl45FamiliesRepository.Delete(family);
                    Tbl45FamiliesRepository.Save();
                    MessageBox.Show(CurrentTbl45Family.FamilyName + " " + CultRes.StringsRes.DeleteSuccess);
                    GetFamilyByNameOrId(o); //Refresh
                }
                else
                {
                    MessageBox.Show(CultRes.StringsRes.DeleteCan + " " + CurrentTbl45Family.FamilyName+ " " + CultRes.StringsRes.DeleteCan1);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //-------------------------------------------------------------------------------------------------
        private RelayCommand _saveFamilyCommand;
        public new ICommand SaveFamilyCommand
        {
            get { return _saveFamilyCommand ?? (_saveFamilyCommand = new RelayCommand(delegate { SaveFamily(null); })); }
        }

        private void SaveFamily(object o)
        {
            try
            {
                var family= Tbl45FamiliesRepository.Tbl45Families.FirstOrDefault(x => x.FamilyID== CurrentTbl45Family.FamilyID);
                if (CurrentTbl45Family == null)
                {
                    MessageBox.Show(CultRes.StringsRes.DatasetNotExist);
                }
                else
                {
                    if (CurrentTbl45Family.FamilyID!= 0)
                    {
                        if (family!= null) //update
                        {
                            family.Updater = Environment.UserName;
                            family.UpdaterDate = DateTime.Now;
                        }
                    }
                    else
                    {
                        Tbl45FamiliesRepository.Add(new Tbl45Family
                        {
                            SuperfamilyID= CurrentTbl45Family.SuperfamilyID,              
                            FamilyName= CurrentTbl45Family.FamilyName,              
                            CountID = RandomHelper.Randomnumber(),
                            Valid = CurrentTbl45Family.Valid,
                            ValidYear = CurrentTbl45Family.ValidYear,
                            Synonym = CurrentTbl45Family.Synonym,
                            Author = CurrentTbl45Family.Author,
                            AuthorYear = CurrentTbl45Family.AuthorYear,
                            Info = CurrentTbl45Family.Info,
                            EngName = CurrentTbl45Family.EngName,
                            GerName = CurrentTbl45Family.GerName,
                            FraName = CurrentTbl45Family.FraName,
                            PorName = CurrentTbl45Family.PorName,
                            Writer = Environment.UserName,
                            WriterDate = DateTime.Now,
                            Updater = Environment.UserName,
                            UpdaterDate = DateTime.Now,
                            Memo = CurrentTbl45Family.Memo
                        });
                    }
                    {
                        Tbl45FamiliesRepository.Save();
                        MessageBox.Show(CurrentTbl45Family.FamilyName+ " " + CultRes.StringsRes.SaveSuccess);
                        GetFamilyByNameOrId(o);  //Refresh
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


        
        #region "Public Commands Connect <== Tbl42Superfamily"                 

        private RelayCommand _getSuperfamilyByNameCommand;
        public new ICommand GetSuperfamilyByNameCommand
        {
            get { return _getSuperfamilyByNameCommand ?? (_getSuperfamilyByNameCommand = new RelayCommand(delegate { GetSuperfamilyByNameOrId(null); })); }
        }

        private void GetSuperfamilyByNameOrId(object o)
        {
            Tbl42SuperfamiliesList =
                new ObservableCollection<Tbl42Superfamily>((from superfamily in Tbl42SuperfamiliesRepository.Tbl42Superfamilies
                                                       where superfamily.SuperfamilyName.StartsWith(SearchSuperfamilyName)
                                                       orderby superfamily.SuperfamilyName
                                                       select superfamily));

            SuperfamiliesView = CollectionViewSource.GetDefaultView(Tbl42SuperfamiliesList);
            if (SuperfamiliesView != null)
                SuperfamiliesView.CurrentChanged += tbl42SuperfamilyView_CurrentChanged;                   
            RaisePropertyChanged();
        }
        //------------------------------------------------------------------------------
        private RelayCommand _addSuperfamilyCommand;
        public new ICommand AddSuperfamilyCommand
        {
            get { return _addSuperfamilyCommand ?? (_addSuperfamilyCommand = new RelayCommand(AddSuperfamily)); }
        }

        private void AddSuperfamily()
        {
            if (Tbl42SuperfamiliesList == null)
                Tbl42SuperfamiliesList = new ObservableCollection<Tbl42Superfamily>();
            Tbl42SuperfamiliesList.Add(new Tbl42Superfamily{ SuperfamilyName= "New " });                   
            SuperfamiliesView = CollectionViewSource.GetDefaultView(Tbl42SuperfamiliesList);
            if (SuperfamiliesView != null)
                SuperfamiliesView.CurrentChanged += tbl42SuperfamilyView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl42Superfamily");
        }
        //----------------------------------------------------------------------------------------------------------
        private RelayCommand _deleteSuperfamilyCommand;
        public ICommand SuperfamilyPhylumCommand
        {
            get { return _deleteSuperfamilyCommand ?? (_deleteSuperfamilyCommand = new RelayCommand(DeleteSuperfamily)); }
        }

        private void DeleteSuperfamily()
        {
            try
            {
                var superfamily= Tbl42SuperfamiliesRepository.Tbl42Superfamilies.FirstOrDefault(x => x.SuperfamilyID== CurrentTbl42Superfamily.SuperfamilyID);
                if (superfamily!= null)
                {
                    if (MessageBox.Show("Are you really shure to delete this "
                                        + CurrentTbl42Superfamily.SuperfamilyName, "Question", MessageBoxButton.YesNo) !=
                        MessageBoxResult.Yes) return;
                    Tbl42SuperfamiliesRepository.Delete(superfamily);
                    Tbl42SuperfamiliesRepository.Save();
                    MessageBox.Show(CurrentTbl42Superfamily.SuperfamilyName+ " was deleted successfully");
                    if (SearchSuperfamilyName == null)
                    {
                        GetConnectedTablesById(); //refresh doubleClick                    
                    }
                    else
                    {
                        GetSuperfamilyByName(); //search
                    }
                }
                else
                {
                    MessageBox.Show("Only " + CurrentTbl42Superfamily.SuperfamilyName+ " can be deleted");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //-------------------------------------------------------------------------------------------------
        private RelayCommand _saveSuperfamilyCommand;   
        public new ICommand SaveSuperfamilyCommand
        {
            get { return _saveSuperfamilyCommand ?? (_saveSuperfamilyCommand = new RelayCommand(SaveSuperfamily)); }
        }

        private void SaveSuperfamily()
        {
            try
            {
                var superfamily= Tbl42SuperfamiliesRepository.Tbl42Superfamilies.FirstOrDefault(x => x.SuperfamilyID== CurrentTbl42Superfamily.SuperfamilyID);
                if (CurrentTbl42Superfamily == null)
                {
                    MessageBox.Show("superfamily was not found");
                }
                else
                {
                    if (CurrentTbl42Superfamily.SuperfamilyID!= 0)
                    {
                        if (superfamily!= null) //update
                        {
                            superfamily.Updater = Environment.UserName;
                            superfamily.UpdaterDate = DateTime.Now;
                        }
                    }
                    else
                    {
                        Tbl42SuperfamiliesRepository.Add(new Tbl42Superfamily()
                        {
                            SuperfamilyName= CurrentTbl42Superfamily.SuperfamilyName,              
                            CountID = TblCountersRepository.Counter(),
                            Valid = CurrentTbl42Superfamily.Valid,
                            ValidYear = CurrentTbl42Superfamily.ValidYear,
                            Synonym = CurrentTbl42Superfamily.Synonym,
                            Author = CurrentTbl42Superfamily.Author,
                            AuthorYear = CurrentTbl42Superfamily.AuthorYear,
                            Info = CurrentTbl42Superfamily.Info,
                            EngName = CurrentTbl42Superfamily.EngName,
                            GerName = CurrentTbl42Superfamily.GerName,
                            FraName = CurrentTbl42Superfamily.FraName,
                            PorName = CurrentTbl42Superfamily.PorName,
                            Writer = Environment.UserName,
                            WriterDate = DateTime.Now,
                            Updater = Environment.UserName,
                            UpdaterDate = DateTime.Now,
                            Memo = CurrentTbl42Superfamily.Memo
                        });
                        }
                        {
                            Tbl42SuperfamiliesRepository.Save();
                            MessageBox.Show(CurrentTbl42Superfamily.SuperfamilyName+  " was successfully saved ");
                            GetSuperfamilyByName();  //Refresh
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


        
        #region "Public Commands Connect ==> Tbl48Subfamily"                 

        private RelayCommand _getSubfamilyByNameCommand;
        public ICommand GetSubfamilyByNameCommand
        {
            get { return _getSubfamilyByNameCommand ?? (_getSubfamilyByNameCommand = new RelayCommand(delegate { GetSubfamilyByNameOrId(null); })); }
        }

        private void GetSubfamilyByNameOrId(object o)
        {
            int id;
            if (int.TryParse(SearchSubfamilyName, out id))
                Tbl48SubfamiliesList = new ObservableCollection<Tbl48Subfamily> { Tbl48SubfamiliesRepository.Get(id) };
            else
            Tbl48SubfamiliesList =  new ObservableCollection<Tbl48Subfamily>
                                                      (from x in Tbl48SubfamiliesRepository.FindAll()
                                                       where x.SubfamilyName.StartsWith(SearchSubfamilyName)
                                                       orderby x.SubfamilyName
                                                       select x);

            SubfamiliesView = CollectionViewSource.GetDefaultView(Tbl48SubfamiliesList);
            if (SubfamiliesView != null)
                SubfamiliesView.CurrentChanged += tbl48SubfamilyView_CurrentChanged;                   
            RaisePropertyChanged();
        }
        //------------------------------------------------------------------------------
        private RelayCommand _addSubfamilyCommand;
        public ICommand AddSubfamilyCommand
        {
            get { return _addSubfamilyCommand ?? (_addSubfamilyCommand = new RelayCommand(delegate { AddSubfamily(null); })); }
        }

        private void AddSubfamily(object o)
        {
            if (Tbl48SubfamiliesList == null)
                Tbl48SubfamiliesList = new ObservableCollection<Tbl48Subfamily>();
            Tbl48SubfamiliesList.Add(new Tbl48Subfamily{ SubfamilyName= CultRes.StringsRes.DatasetNew });                   
            SubfamiliesView = CollectionViewSource.GetDefaultView(Tbl48SubfamiliesList);
            if (SubfamiliesView != null)
                SubfamiliesView.CurrentChanged += tbl48SubfamilyView_CurrentChanged;
            RaisePropertyChanged();
        }
        //---------------------------------------------------------------------------------------
        private RelayCommand _deleteSubfamilyCommand;
        public ICommand DeleteSubfamilyCommand
        {
            get { return _deleteSubfamilyCommand ?? (_deleteSubfamilyCommand = new RelayCommand(delegate { DeleteSubfamily(null); })); }
        }

        private void DeleteSubfamily(object o)
        {
            try
            {
                var subfamily = Tbl48SubfamiliesRepository.Tbl48Subfamilies.FirstOrDefault(x => x.SubfamilyID== CurrentTbl48Subfamily.SubfamilyID);
                if (subfamily != null)
                {
                    if (MessageBox.Show(CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl48Subfamily.SubfamilyName, CultRes.StringsRes.DeleteQuestion1, MessageBoxButton.YesNo) !=
                        MessageBoxResult.Yes) return;
                    Tbl48SubfamiliesRepository.Delete(subfamily);
                    Tbl48SubfamiliesRepository.Save();
                    MessageBox.Show(CurrentTbl48Subfamily.SubfamilyName + " " + CultRes.StringsRes.DeleteSuccess);
                    GetSubfamilyByNameOrId(o);  //Refresh
                }
                else
                {
                    MessageBox.Show(CultRes.StringsRes.DeleteCan + " " + CurrentTbl48Subfamily.SubfamilyName+ " " + CultRes.StringsRes.DeleteCan1);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //-------------------------------------------------------------------------------------------------
        private RelayCommand _saveSubfamilyCommand;   
        public ICommand SaveSubfamilyCommand
        {
            get { return _saveSubfamilyCommand ?? (_saveSubfamilyCommand = new RelayCommand(delegate { SaveSubfamily(null); })); }
        }

        private void SaveSubfamily(object o)
        {
            try
            {
                var subfamily = Tbl48SubfamiliesRepository.Tbl48Subfamilies.FirstOrDefault(x => x.SubfamilyID== CurrentTbl48Subfamily.SubfamilyID);
                if (CurrentTbl48Subfamily == null)
                {
                    MessageBox.Show(CultRes.StringsRes.DatasetNotExist);
                }
                else
                {
                    if (CurrentTbl48Subfamily.SubfamilyID!= 0)
                    {
                        if (subfamily!= null) //update
                        {
                            subfamily.Updater = Environment.UserName;
                            subfamily.UpdaterDate = DateTime.Now;
                        }
                    }
                    else
                    {
                        Tbl48SubfamiliesRepository.Add(new Tbl48Subfamily
                        {
                            FamilyID= CurrentTbl48Subfamily.FamilyID,              
                            SubfamilyName= CurrentTbl48Subfamily.SubfamilyName,              
                            CountID = RandomHelper.Randomnumber(),
                            Valid = CurrentTbl48Subfamily.Valid,
                            ValidYear = CurrentTbl48Subfamily.ValidYear,
                            Synonym = CurrentTbl48Subfamily.Synonym,
                            Author = CurrentTbl48Subfamily.Author,
                            AuthorYear = CurrentTbl48Subfamily.AuthorYear,
                            Info = CurrentTbl48Subfamily.Info,
                            EngName = CurrentTbl48Subfamily.EngName,
                            GerName = CurrentTbl48Subfamily.GerName,
                            FraName = CurrentTbl48Subfamily.FraName,
                            PorName = CurrentTbl48Subfamily.PorName,
                            Writer = Environment.UserName,
                            WriterDate = DateTime.Now,
                            Updater = Environment.UserName,
                            UpdaterDate = DateTime.Now,
                            Memo = CurrentTbl48Subfamily.Memo
                        });
                    }
                    {
                        Tbl48SubfamiliesRepository.Save();
                        if (CurrentTbl48Subfamily != null)
                            MessageBox.Show(CurrentTbl48Subfamily.SubfamilyName+ " " + CultRes.StringsRes.SaveSuccess);
                        GetSubfamilyByNameOrId(o);  //Refresh      
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
                            FamilyID= CurrentTbl90RefAuthor.FamilyID,
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
                            FamilyID= CurrentTbl90RefSource.FamilyID,
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
                            FamilyID= CurrentTbl90RefExpert.FamilyID,
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
                            FamilyID= CurrentTbl93Comment.FamilyID,                
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
            SearchSuperfamilyName = null;                       
            SearchSubfamilyName = null;
            SearchCommentInfo = null;
            SearchRefExpertName = null;
            SearchRefSourceName = null;
            SearchRefAuthorName = null;

            Tbl42SuperfamiliesList =
                new ObservableCollection<Tbl42Superfamily>((from superfamily in Tbl42SuperfamiliesRepository.Tbl42Superfamilies
                                                       where superfamily.SuperfamilyID== CurrentTbl45Family.SuperfamilyID
                                                       orderby superfamily.SuperfamilyName
                                                       select superfamily));

            SuperfamiliesView = CollectionViewSource.GetDefaultView(Tbl42SuperfamiliesList);
            if (SuperfamiliesView != null)
                SuperfamiliesView.CurrentChanged += tbl42SuperfamilyView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl42Superfamilies");
            //-----------------------------------------------------------------------------------
            Tbl48SubfamiliesList =
                new ObservableCollection<Tbl48Subfamily>((from subfamily in Tbl48SubfamiliesRepository.Tbl48Subfamilies
                                                       where subfamily.FamilyID== CurrentTbl45Family.FamilyID
                                                       orderby subfamily.Tbl45Families.FamilyName
                                                       select subfamily));


            SubfamiliesView = CollectionViewSource.GetDefaultView(Tbl48SubfamiliesList);
            if (SubfamiliesView != null)
                SubfamiliesView.CurrentChanged += tbl48SubfamilyView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl48Subfamilies");
            //-----------------------------------------------------------------------------------
            Tbl90RefAuthorsList =
                new ObservableCollection<Tbl90Reference>((from refAu in Tbl90ReferencesRepository.Tbl90References
                                                          where refAu.FamilyID== CurrentTbl45Family.FamilyID
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
                                                          where refSo.FamilyID== CurrentTbl45Family.FamilyID
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
                                                          where refEx.FamilyID== CurrentTbl45Family.FamilyID
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
                                                          where comm.FamilyID== CurrentTbl45Family.FamilyID
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



     
        #region "Public Properties Tbl45Family"

        public new ICollectionView FamiliesView;
        public new Tbl45Family CurrentTbl45Family
        {
            get
            {
                if (FamiliesView != null)
                    return FamiliesView.CurrentItem as Tbl45Family;
                return null;
            }
        }
        //--------------------------------------------
        private string _searchFamilyName;
        public new string SearchFamilyName
        {
            get { return _searchFamilyName; }
            set
            {
                if (value == _searchFamilyName) return;
                _searchFamilyName = value;
                RaisePropertyChanged("SearchFamilyName");
            }
        }

        private ObservableCollection<Tbl45Family> _tbl45FamiliesList;
        public new ObservableCollection<Tbl45Family> Tbl45FamiliesList
        {
            get { return _tbl45FamiliesList; }
            set
            {
                if (_tbl45FamiliesList == value) return;
                _tbl45FamiliesList = value;
                RaisePropertyChanged("Tbl45FamiliesList");

                //Clear Search-TextBox
                SearchSuperfamilyName = null;                                
                SearchSubfamilyName = null;
                SearchCommentInfo = null;
                SearchRefExpertName = null;
                SearchRefSourceName = null;
                SearchRefAuthorName = null;
            }
        }

        private ObservableCollection<Tbl45Family> _tbl45FamiliesAllList;
        public ObservableCollection<Tbl45Family> Tbl45FamiliesAllList
        {
            get { return _tbl45FamiliesAllList; }
            set
            {
                if (_tbl45FamiliesAllList == value) return;
                _tbl45FamiliesAllList = value;
                RaisePropertyChanged("Tbl45FamiliesAllList");
            }
        }

        #endregion "Public Properties"   

       
        #region "Public Properties Tbl42Superfamily"

        public  ICollectionView SuperfamiliesView;
        public  Tbl42Superfamily CurrentTbl42Superfamily
        {
            get
            {
                if (SuperfamiliesView != null)
                    return SuperfamiliesView.CurrentItem as Tbl42Superfamily;
                return null;
            }
        }
        //--------------------------------------------                                               

        private string _searchSuperfamilyName;
        public  string SearchSuperfamilyName
        {
            get { return _searchSuperfamilyName; }
            set
            {
                if (value == _searchSuperfamilyName) return;
                _searchSuperfamilyName = value;
                RaisePropertyChanged("SearchSuperfamilyName");
            }
        }

        private ObservableCollection<Tbl42Superfamily> _tbl42SuperfamiliesList;
        public  ObservableCollection<Tbl42Superfamily> Tbl42SuperfamiliesList
        {
            get { return _tbl42SuperfamiliesList; }
            set
            {
                if (_tbl42SuperfamiliesList == value) return;
                _tbl42SuperfamiliesList = value;
                RaisePropertyChanged("Tbl42SuperfamiliesList");
            }
        }

        #endregion "Public Properties"
   
  
       
        #region "Public Properties Tbl48Subfamily"

        public ICollectionView SubfamiliesView;
        public Tbl48Subfamily CurrentTbl48Subfamily
        {
            get
            {
                if (SubfamiliesView != null)
                    return SubfamiliesView.CurrentItem as Tbl48Subfamily;
                return null;
            }
        }
        //--------------------------------------------                                               

        private string _searchSubfamilyName;
        public string SearchSubfamilyName
        {
            get { return _searchSubfamilyName; }
            set
            {
                if (value == _searchSubfamilyName) return;
                _searchSubfamilyName = value;
                RaisePropertyChanged("SearchSubfamilyName");
            }
        }

        private ObservableCollection<Tbl48Subfamily> _tbl48SubfamiliesList;
        public ObservableCollection<Tbl48Subfamily> Tbl48SubfamiliesList
        {
            get { return _tbl48SubfamiliesList; }
            set
            {
                if (_tbl48SubfamiliesList == value) return;
                _tbl48SubfamiliesList = value;
                RaisePropertyChanged("Tbl48SubfamiliesList");
            }
        }

        #endregion "Public Properties"
   
   

 //    Part 11    

      }
}   
