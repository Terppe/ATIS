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

//    Tbl36SubordosViewModel Skriptdatum:  31.03.2014  10:32    

namespace WPFUI.Views.Database
{   


    
    public class Tbl36SubordosViewModel : Tbl33OrdosViewModel                     
    {     
         
      #region "Private Data Members"

        protected readonly Tbl39InfraordosRepository Tbl39InfraordosRepository = new Tbl39InfraordosRepository();   
          
          #endregion "Private Data Members"            
    
        #region "Constructor"

        public Tbl36SubordosViewModel()
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

       
        #region "Public Commands Basic Tbl36Subordo"

        private RelayCommand _getSubordoByNameCommand;
        public new ICommand GetSubordoByNameCommand
        {
            get { return _getSubordoByNameCommand ?? (_getSubordoByNameCommand = new RelayCommand(delegate { GetSubordoByNameOrId(null); })); }   
        }

        private void GetSubordoByNameOrId(object o)       
        {   
Tbl36SubordosList =  new ObservableCollection<Tbl36Subordo>
                                                       (from x in Tbl36SubordosRepository.Tbl36Subordos
                                                        where x.SubordoName.StartsWith(SearchSubordoName)
                                                        orderby x.SubordoName
                                                        select x);

            Tbl36SubordosAllList =  new ObservableCollection<Tbl36Subordo>
                                                       (from y in Tbl36SubordosRepository.Tbl36Subordos
                                                        orderby y.SubordoName
                                                        select y);

            Tbl33OrdosAllList =  new ObservableCollection<Tbl33Ordo>
                                                       (from z in Tbl33OrdosRepository.Tbl33Ordos
                                                        orderby z.OrdoName
                                                        select z);

              
  Tbl30LegiosAllList =  new ObservableCollection<Tbl30Legio>
                                                       (from z in Tbl30LegiosRepository.Tbl30Legios
                                                        orderby z.LegioName
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

  
Tbl33OrdosList = null;                  
  Tbl39InfraordosList = null;               
  SubordosView = CollectionViewSource.GetDefaultView(Tbl36SubordosList);
            if (SubordosView != null)
                SubordosView.CurrentChanged += tbl36SubordoView_CurrentChanged;                   
            RaisePropertyChanged();
        }
        //------------------------------------------------------------------------------
        private RelayCommand _addSubordoCommand;
        public new ICommand AddSubordoCommand
        {
            get { return _addSubordoCommand ?? (_addSubordoCommand = new RelayCommand(AddSubordo)); }
        }

        private void AddSubordo(object o)
        {
            if (Tbl36SubordosList == null)
                Tbl36SubordosList = new ObservableCollection<Tbl36Subordo>();
            Tbl36SubordosList.Add(new Tbl36Subordo{ SubordoName= CultRes.StringsRes.DatasetNew });
            SubordosView = CollectionViewSource.GetDefaultView(Tbl36SubordosList);
            if (SubordosView != null)
                SubordosView.CurrentChanged += tbl36SubordoView_CurrentChanged;
            RaisePropertyChanged();
        }
        //---------------------------------------------------------------------------------------
  
       
        private RelayCommand _deleteSubordoCommand;
        public new ICommand DeleteSubordoCommand
        {
            get { return _deleteSubordoCommand ?? (_deleteSubordoCommand = new RelayCommand(delegate { DeleteSubordo(null); })); }
        }

        private void DeleteSubordo(object o)
        {
            try
            {
                var subordo= Tbl36SubordosRepository.Tbl36Subordos.FirstOrDefault(x => x.SubordoID== CurrentTbl36Subordo.SubordoID);
                if (subordo!= null)
                {
                    if (MessageBox.Show(CultRes.StringsRes.DeleteQuestion
                                        + " " +  CurrentTbl36Subordo.SubordoName, CultRes.StringsRes.DeleteQuestion1, MessageBoxButton.YesNo) !=
                        MessageBoxResult.Yes) return;
                    Tbl36SubordosRepository.Delete(subordo);
                    Tbl36SubordosRepository.Save();
                    MessageBox.Show(CurrentTbl36Subordo.SubordoName + " " + CultRes.StringsRes.DeleteSuccess);
                    GetSubordoByNameOrId(o); //Refresh
                }
                else
                {
                    MessageBox.Show(CultRes.StringsRes.DeleteCan + " " + CurrentTbl36Subordo.SubordoName+ " " + CultRes.StringsRes.DeleteCan1);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //-------------------------------------------------------------------------------------------------
        private RelayCommand _saveSubordoCommand;
        public new ICommand SaveSubordoCommand
        {
            get { return _saveSubordoCommand ?? (_saveSubordoCommand = new RelayCommand(delegate { SaveSubordo(null); })); }
        }

        private void SaveSubordo(object o)
        {
            try
            {
                var subordo= Tbl36SubordosRepository.Tbl36Subordos.FirstOrDefault(x => x.SubordoID== CurrentTbl36Subordo.SubordoID);
                if (CurrentTbl36Subordo == null)
                {
                    MessageBox.Show(CultRes.StringsRes.DatasetNotExist);
                }
                else
                {
                    if (CurrentTbl36Subordo.SubordoID!= 0)
                    {
                        if (subordo!= null) //update
                        {
                            subordo.Updater = Environment.UserName;
                            subordo.UpdaterDate = DateTime.Now;
                        }
                    }
                    else
                    {
                        Tbl36SubordosRepository.Add(new Tbl36Subordo
                        {
                            OrdoID= CurrentTbl36Subordo.OrdoID,              
                            SubordoName= CurrentTbl36Subordo.SubordoName,              
                            CountID = RandomHelper.Randomnumber(),
                            Valid = CurrentTbl36Subordo.Valid,
                            ValidYear = CurrentTbl36Subordo.ValidYear,
                            Synonym = CurrentTbl36Subordo.Synonym,
                            Author = CurrentTbl36Subordo.Author,
                            AuthorYear = CurrentTbl36Subordo.AuthorYear,
                            Info = CurrentTbl36Subordo.Info,
                            EngName = CurrentTbl36Subordo.EngName,
                            GerName = CurrentTbl36Subordo.GerName,
                            FraName = CurrentTbl36Subordo.FraName,
                            PorName = CurrentTbl36Subordo.PorName,
                            Writer = Environment.UserName,
                            WriterDate = DateTime.Now,
                            Updater = Environment.UserName,
                            UpdaterDate = DateTime.Now,
                            Memo = CurrentTbl36Subordo.Memo
                        });
                    }
                    {
                        Tbl36SubordosRepository.Save();
                        MessageBox.Show(CurrentTbl36Subordo.SubordoName+ " " + CultRes.StringsRes.SaveSuccess);
                        GetSubordoByNameOrId(o);  //Refresh
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


        
        #region "Public Commands Connect <== Tbl33Ordo"                 

        private RelayCommand _getOrdoByNameCommand;
        public new ICommand GetOrdoByNameCommand
        {
            get { return _getOrdoByNameCommand ?? (_getOrdoByNameCommand = new RelayCommand(delegate { GetOrdoByNameOrId(null); })); }
        }

        private void GetOrdoByNameOrId(object o)
        {
            Tbl33OrdosList =
                new ObservableCollection<Tbl33Ordo>((from ordo in Tbl33OrdosRepository.Tbl33Ordos
                                                       where ordo.OrdoName.StartsWith(SearchOrdoName)
                                                       orderby ordo.OrdoName
                                                       select ordo));

            OrdosView = CollectionViewSource.GetDefaultView(Tbl33OrdosList);
            if (OrdosView != null)
                OrdosView.CurrentChanged += tbl33OrdoView_CurrentChanged;                   
            RaisePropertyChanged();
        }
        //------------------------------------------------------------------------------
        private RelayCommand _addOrdoCommand;
        public new ICommand AddOrdoCommand
        {
            get { return _addOrdoCommand ?? (_addOrdoCommand = new RelayCommand(AddOrdo)); }
        }

        private void AddOrdo()
        {
            if (Tbl33OrdosList == null)
                Tbl33OrdosList = new ObservableCollection<Tbl33Ordo>();
            Tbl33OrdosList.Add(new Tbl33Ordo{ OrdoName= "New " });                   
            OrdosView = CollectionViewSource.GetDefaultView(Tbl33OrdosList);
            if (OrdosView != null)
                OrdosView.CurrentChanged += tbl33OrdoView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl33Ordo");
        }
        //----------------------------------------------------------------------------------------------------------
        private RelayCommand _deleteOrdoCommand;
        public ICommand OrdoPhylumCommand
        {
            get { return _deleteOrdoCommand ?? (_deleteOrdoCommand = new RelayCommand(DeleteOrdo)); }
        }

        private void DeleteOrdo()
        {
            try
            {
                var ordo= Tbl33OrdosRepository.Tbl33Ordos.FirstOrDefault(x => x.OrdoID== CurrentTbl33Ordo.OrdoID);
                if (ordo!= null)
                {
                    if (MessageBox.Show("Are you really shure to delete this "
                                        + CurrentTbl33Ordo.OrdoName, "Question", MessageBoxButton.YesNo) !=
                        MessageBoxResult.Yes) return;
                    Tbl33OrdosRepository.Delete(ordo);
                    Tbl33OrdosRepository.Save();
                    MessageBox.Show(CurrentTbl33Ordo.OrdoName+ " was deleted successfully");
                    if (SearchOrdoName == null)
                    {
                        GetConnectedTablesById(); //refresh doubleClick                    
                    }
                    else
                    {
                        GetOrdoByName(); //search
                    }
                }
                else
                {
                    MessageBox.Show("Only " + CurrentTbl33Ordo.OrdoName+ " can be deleted");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //-------------------------------------------------------------------------------------------------
        private RelayCommand _saveOrdoCommand;   
        public new ICommand SaveOrdoCommand
        {
            get { return _saveOrdoCommand ?? (_saveOrdoCommand = new RelayCommand(SaveOrdo)); }
        }

        private void SaveOrdo()
        {
            try
            {
                var ordo= Tbl33OrdosRepository.Tbl33Ordos.FirstOrDefault(x => x.OrdoID== CurrentTbl33Ordo.OrdoID);
                if (CurrentTbl33Ordo == null)
                {
                    MessageBox.Show("ordo was not found");
                }
                else
                {
                    if (CurrentTbl33Ordo.OrdoID!= 0)
                    {
                        if (ordo!= null) //update
                        {
                            ordo.Updater = Environment.UserName;
                            ordo.UpdaterDate = DateTime.Now;
                        }
                    }
                    else
                    {
                        Tbl33OrdosRepository.Add(new Tbl33Ordo()
                        {
                            OrdoName= CurrentTbl33Ordo.OrdoName,              
                            CountID = TblCountersRepository.Counter(),
                            Valid = CurrentTbl33Ordo.Valid,
                            ValidYear = CurrentTbl33Ordo.ValidYear,
                            Synonym = CurrentTbl33Ordo.Synonym,
                            Author = CurrentTbl33Ordo.Author,
                            AuthorYear = CurrentTbl33Ordo.AuthorYear,
                            Info = CurrentTbl33Ordo.Info,
                            EngName = CurrentTbl33Ordo.EngName,
                            GerName = CurrentTbl33Ordo.GerName,
                            FraName = CurrentTbl33Ordo.FraName,
                            PorName = CurrentTbl33Ordo.PorName,
                            Writer = Environment.UserName,
                            WriterDate = DateTime.Now,
                            Updater = Environment.UserName,
                            UpdaterDate = DateTime.Now,
                            Memo = CurrentTbl33Ordo.Memo
                        });
                        }
                        {
                            Tbl33OrdosRepository.Save();
                            MessageBox.Show(CurrentTbl33Ordo.OrdoName+  " was successfully saved ");
                            GetOrdoByName();  //Refresh
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


        
        #region "Public Commands Connect ==> Tbl39Infraordo"                 

        private RelayCommand _getInfraordoByNameCommand;
        public ICommand GetInfraordoByNameCommand
        {
            get { return _getInfraordoByNameCommand ?? (_getInfraordoByNameCommand = new RelayCommand(delegate { GetInfraordoByNameOrId(null); })); }
        }

        private void GetInfraordoByNameOrId(object o)
        {
            int id;
            if (int.TryParse(SearchInfraordoName, out id))
                Tbl39InfraordosList = new ObservableCollection<Tbl39Infraordo> { Tbl39InfraordosRepository.Get(id) };
            else
            Tbl39InfraordosList =  new ObservableCollection<Tbl39Infraordo>
                                                      (from x in Tbl39InfraordosRepository.FindAll()
                                                       where x.InfraordoName.StartsWith(SearchInfraordoName)
                                                       orderby x.InfraordoName
                                                       select x);

            InfraordosView = CollectionViewSource.GetDefaultView(Tbl39InfraordosList);
            if (InfraordosView != null)
                InfraordosView.CurrentChanged += tbl39InfraordoView_CurrentChanged;                   
            RaisePropertyChanged();
        }
        //------------------------------------------------------------------------------
        private RelayCommand _addInfraordoCommand;
        public ICommand AddInfraordoCommand
        {
            get { return _addInfraordoCommand ?? (_addInfraordoCommand = new RelayCommand(delegate { AddInfraordo(null); })); }
        }

        private void AddInfraordo(object o)
        {
            if (Tbl39InfraordosList == null)
                Tbl39InfraordosList = new ObservableCollection<Tbl39Infraordo>();
            Tbl39InfraordosList.Add(new Tbl39Infraordo{ InfraordoName= CultRes.StringsRes.DatasetNew });                   
            InfraordosView = CollectionViewSource.GetDefaultView(Tbl39InfraordosList);
            if (InfraordosView != null)
                InfraordosView.CurrentChanged += tbl39InfraordoView_CurrentChanged;
            RaisePropertyChanged();
        }
        //---------------------------------------------------------------------------------------
        private RelayCommand _deleteInfraordoCommand;
        public ICommand DeleteInfraordoCommand
        {
            get { return _deleteInfraordoCommand ?? (_deleteInfraordoCommand = new RelayCommand(delegate { DeleteInfraordo(null); })); }
        }

        private void DeleteInfraordo(object o)
        {
            try
            {
                var infraordo = Tbl39InfraordosRepository.Tbl39Infraordos.FirstOrDefault(x => x.InfraordoID== CurrentTbl39Infraordo.InfraordoID);
                if (infraordo != null)
                {
                    if (MessageBox.Show(CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl39Infraordo.InfraordoName, CultRes.StringsRes.DeleteQuestion1, MessageBoxButton.YesNo) !=
                        MessageBoxResult.Yes) return;
                    Tbl39InfraordosRepository.Delete(infraordo);
                    Tbl39InfraordosRepository.Save();
                    MessageBox.Show(CurrentTbl39Infraordo.InfraordoName + " " + CultRes.StringsRes.DeleteSuccess);
                    GetInfraordoByNameOrId(o);  //Refresh
                }
                else
                {
                    MessageBox.Show(CultRes.StringsRes.DeleteCan + " " + CurrentTbl39Infraordo.InfraordoName+ " " + CultRes.StringsRes.DeleteCan1);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //-------------------------------------------------------------------------------------------------
        private RelayCommand _saveInfraordoCommand;   
        public ICommand SaveInfraordoCommand
        {
            get { return _saveInfraordoCommand ?? (_saveInfraordoCommand = new RelayCommand(delegate { SaveInfraordo(null); })); }
        }

        private void SaveInfraordo(object o)
        {
            try
            {
                var infraordo = Tbl39InfraordosRepository.Tbl39Infraordos.FirstOrDefault(x => x.InfraordoID== CurrentTbl39Infraordo.InfraordoID);
                if (CurrentTbl39Infraordo == null)
                {
                    MessageBox.Show(CultRes.StringsRes.DatasetNotExist);
                }
                else
                {
                    if (CurrentTbl39Infraordo.InfraordoID!= 0)
                    {
                        if (infraordo!= null) //update
                        {
                            infraordo.Updater = Environment.UserName;
                            infraordo.UpdaterDate = DateTime.Now;
                        }
                    }
                    else
                    {
                        Tbl39InfraordosRepository.Add(new Tbl39Infraordo
                        {
                            SubordoID= CurrentTbl39Infraordo.SubordoID,              
                            InfraordoName= CurrentTbl39Infraordo.InfraordoName,              
                            CountID = RandomHelper.Randomnumber(),
                            Valid = CurrentTbl39Infraordo.Valid,
                            ValidYear = CurrentTbl39Infraordo.ValidYear,
                            Synonym = CurrentTbl39Infraordo.Synonym,
                            Author = CurrentTbl39Infraordo.Author,
                            AuthorYear = CurrentTbl39Infraordo.AuthorYear,
                            Info = CurrentTbl39Infraordo.Info,
                            EngName = CurrentTbl39Infraordo.EngName,
                            GerName = CurrentTbl39Infraordo.GerName,
                            FraName = CurrentTbl39Infraordo.FraName,
                            PorName = CurrentTbl39Infraordo.PorName,
                            Writer = Environment.UserName,
                            WriterDate = DateTime.Now,
                            Updater = Environment.UserName,
                            UpdaterDate = DateTime.Now,
                            Memo = CurrentTbl39Infraordo.Memo
                        });
                    }
                    {
                        Tbl39InfraordosRepository.Save();
                        if (CurrentTbl39Infraordo != null)
                            MessageBox.Show(CurrentTbl39Infraordo.InfraordoName+ " " + CultRes.StringsRes.SaveSuccess);
                        GetInfraordoByNameOrId(o);  //Refresh      
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
                            SubordoID= CurrentTbl90RefAuthor.SubordoID,
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
                            SubordoID= CurrentTbl90RefSource.SubordoID,
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
                            SubordoID= CurrentTbl90RefExpert.SubordoID,
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
                            SubordoID= CurrentTbl93Comment.SubordoID,                
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
            SearchOrdoName = null;                       
            SearchInfraordoName = null;
            SearchCommentInfo = null;
            SearchRefExpertName = null;
            SearchRefSourceName = null;
            SearchRefAuthorName = null;

            Tbl33OrdosList =
                new ObservableCollection<Tbl33Ordo>((from ordo in Tbl33OrdosRepository.Tbl33Ordos
                                                       where ordo.OrdoID== CurrentTbl36Subordo.OrdoID
                                                       orderby ordo.OrdoName
                                                       select ordo));

            OrdosView = CollectionViewSource.GetDefaultView(Tbl33OrdosList);
            if (OrdosView != null)
                OrdosView.CurrentChanged += tbl33OrdoView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl33Ordos");
            //-----------------------------------------------------------------------------------
            Tbl39InfraordosList =
                new ObservableCollection<Tbl39Infraordo>((from infraordo in Tbl39InfraordosRepository.Tbl39Infraordos
                                                       where infraordo.SubordoID== CurrentTbl36Subordo.SubordoID
                                                       orderby infraordo.Tbl36Subordos.SubordoName
                                                       select infraordo));


            InfraordosView = CollectionViewSource.GetDefaultView(Tbl39InfraordosList);
            if (InfraordosView != null)
                InfraordosView.CurrentChanged += tbl39InfraordoView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl39Infraordos");
            //-----------------------------------------------------------------------------------
            Tbl90RefAuthorsList =
                new ObservableCollection<Tbl90Reference>((from refAu in Tbl90ReferencesRepository.Tbl90References
                                                          where refAu.SubordoID== CurrentTbl36Subordo.SubordoID
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
                                                          where refSo.SubordoID== CurrentTbl36Subordo.SubordoID
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
                                                          where refEx.SubordoID== CurrentTbl36Subordo.SubordoID
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
                                                          where comm.SubordoID== CurrentTbl36Subordo.SubordoID
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



     
        #region "Public Properties Tbl36Subordo"

        public new ICollectionView SubordosView;
        public new Tbl36Subordo CurrentTbl36Subordo
        {
            get
            {
                if (SubordosView != null)
                    return SubordosView.CurrentItem as Tbl36Subordo;
                return null;
            }
        }
        //--------------------------------------------
        private string _searchSubordoName;
        public new string SearchSubordoName
        {
            get { return _searchSubordoName; }
            set
            {
                if (value == _searchSubordoName) return;
                _searchSubordoName = value;
                RaisePropertyChanged("SearchSubordoName");
            }
        }

        private ObservableCollection<Tbl36Subordo> _tbl36SubordosList;
        public new ObservableCollection<Tbl36Subordo> Tbl36SubordosList
        {
            get { return _tbl36SubordosList; }
            set
            {
                if (_tbl36SubordosList == value) return;
                _tbl36SubordosList = value;
                RaisePropertyChanged("Tbl36SubordosList");

                //Clear Search-TextBox
                SearchOrdoName = null;                                
                SearchInfraordoName = null;
                SearchCommentInfo = null;
                SearchRefExpertName = null;
                SearchRefSourceName = null;
                SearchRefAuthorName = null;
            }
        }

        private ObservableCollection<Tbl36Subordo> _tbl36SubordosAllList;
        public ObservableCollection<Tbl36Subordo> Tbl36SubordosAllList
        {
            get { return _tbl36SubordosAllList; }
            set
            {
                if (_tbl36SubordosAllList == value) return;
                _tbl36SubordosAllList = value;
                RaisePropertyChanged("Tbl36SubordosAllList");
            }
        }

        #endregion "Public Properties"   

       
        #region "Public Properties Tbl33Ordo"

        public  ICollectionView OrdosView;
        public  Tbl33Ordo CurrentTbl33Ordo
        {
            get
            {
                if (OrdosView != null)
                    return OrdosView.CurrentItem as Tbl33Ordo;
                return null;
            }
        }
        //--------------------------------------------                                               

        private string _searchOrdoName;
        public  string SearchOrdoName
        {
            get { return _searchOrdoName; }
            set
            {
                if (value == _searchOrdoName) return;
                _searchOrdoName = value;
                RaisePropertyChanged("SearchOrdoName");
            }
        }

        private ObservableCollection<Tbl33Ordo> _tbl33OrdosList;
        public  ObservableCollection<Tbl33Ordo> Tbl33OrdosList
        {
            get { return _tbl33OrdosList; }
            set
            {
                if (_tbl33OrdosList == value) return;
                _tbl33OrdosList = value;
                RaisePropertyChanged("Tbl33OrdosList");
            }
        }

        #endregion "Public Properties"
   
  
       
        #region "Public Properties Tbl39Infraordo"

        public ICollectionView InfraordosView;
        public Tbl39Infraordo CurrentTbl39Infraordo
        {
            get
            {
                if (InfraordosView != null)
                    return InfraordosView.CurrentItem as Tbl39Infraordo;
                return null;
            }
        }
        //--------------------------------------------                                               

        private string _searchInfraordoName;
        public string SearchInfraordoName
        {
            get { return _searchInfraordoName; }
            set
            {
                if (value == _searchInfraordoName) return;
                _searchInfraordoName = value;
                RaisePropertyChanged("SearchInfraordoName");
            }
        }

        private ObservableCollection<Tbl39Infraordo> _tbl39InfraordosList;
        public ObservableCollection<Tbl39Infraordo> Tbl39InfraordosList
        {
            get { return _tbl39InfraordosList; }
            set
            {
                if (_tbl39InfraordosList == value) return;
                _tbl39InfraordosList = value;
                RaisePropertyChanged("Tbl39InfraordosList");
            }
        }

        #endregion "Public Properties"
   
   

 //    Part 11    

      }
}   
