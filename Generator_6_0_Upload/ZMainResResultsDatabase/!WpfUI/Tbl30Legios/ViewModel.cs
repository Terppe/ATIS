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

//    Tbl30LegioViewModel Skriptdatum:  7.1.2012  10:32    

namespace Atis.WpfUi.ViewModel
{   


    
    public class Tbl30LegiosViewModel : Tbl27InfraclassesViewModel                     
    {     
         
      #region "Private Data Members"

        protected readonly Tbl33OrdosRepository Tbl33OrdosRepository = new Tbl33OrdosRepository();   
          
          #endregion "Private Data Members"            
    
        #region "Constructor"

        public Tbl30LegiosViewModel()
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
       
        #region "Public Commands Basic Tbl30Legio"

        private RelayCommand _getLegioByNameCommand;
        public new ICommand GetLegioByNameCommand
        {
            get { return _getLegioByNameCommand ?? (_getLegioByNameCommand = new RelayCommand(GetLegioByName)); }
        }

        private void GetLegioByName()
        {   
Tbl30LegiosList =
                 new ObservableCollection<Tbl30Legio>((from x in Tbl30LegiosRepository.Tbl30Legios
                                                        where x.LegioName.StartsWith(SearchLegioName)
                                                        orderby x.LegioName
                                                        select x));

            Tbl30LegiosAllList =
                 new ObservableCollection<Tbl30Legio>((from y in Tbl30LegiosRepository.Tbl30Legios
                                                        orderby y.LegioName
                                                        select y));

            Tbl27InfraclassesAllList =
                 new ObservableCollection<Tbl27Infraclass>((from z in Tbl27InfraclassesRepository.Tbl27Infraclasses
                                                        orderby z.InfraclassName
                                                        select z));

              
  Tbl24SubclassesAllList =
                 new ObservableCollection<Tbl24Subclass>((from z in Tbl24SubclassesRepository.Tbl24Subclasses
                                                        orderby z.SubclassName
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

  
Tbl27InfraclassesList = null;                  
  Tbl33OrdosList = null;     
             
  View = CollectionViewSource.GetDefaultView(Tbl30LegiosList);
            if (View != null)
                View.CurrentChanged += tbl30LegioView_CurrentChanged;                   
            RaisePropertyChanged("CurrentTbl30Legio");
        }
        //------------------------------------------------------------------------------
        private RelayCommand _addLegioCommand;
        public new ICommand AddLegioCommand
        {
            get { return _addLegioCommand ?? (_addLegioCommand = new RelayCommand(AddLegio)); }
        }

        private void AddLegio()
        {
            if (Tbl30LegiosList == null)
                Tbl30LegiosList = new ObservableCollection<Tbl30Legio>();
            Tbl30LegiosList.Add(new Tbl30Legio{ LegioName= "New " });
            View = CollectionViewSource.GetDefaultView(Tbl30LegiosList);
            if (View != null)
                View.CurrentChanged += tbl30LegioView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl30Legio");
        }
        //---------------------------------------------------------------------------------------
  
       
        private RelayCommand _deleteLegioCommand;
        public new ICommand DeleteLegioCommand
        {
            get { return _deleteLegioCommand ?? (_deleteLegioCommand = new RelayCommand(DeleteLegio)); }
        }

        private void DeleteLegio()
        {
            try
            {
                var legio= Tbl30LegiosRepository.Tbl30Legios.FirstOrDefault(x => x.LegioID== CurrentTbl30Legio.LegioID);
                if (legio!= null)
                {
                    if (MessageBox.Show("Are you really shure to delete this "
                                        + CurrentTbl30Legio.LegioName, "Question", MessageBoxButton.YesNo) !=
                        MessageBoxResult.Yes) return;
                    Tbl30LegiosRepository.Delete(legio);
                    Tbl30LegiosRepository.Save();
                    MessageBox.Show(CurrentTbl30Legio.LegioName + " was deleted successfully");
                    GetLegioByName(); //Refresh
                }
                else
                {
                    MessageBox.Show("Only " + CurrentTbl30Legio.LegioName+ " can be deleted");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //-------------------------------------------------------------------------------------------------
        private RelayCommand _saveLegioCommand;
        public new ICommand SaveLegioCommand
        {
            get { return _saveLegioCommand ?? (_saveLegioCommand = new RelayCommand(SaveLegio)); }
        }

        private void SaveLegio()
        {
            try
            {
                var legio= Tbl30LegiosRepository.Tbl30Legios.FirstOrDefault(x => x.LegioID== CurrentTbl30Legio.LegioID);
                if (CurrentTbl30Legio == null)
                {
                    MessageBox.Show("legio was not found");
                }
                else
                {
                    if (CurrentTbl30Legio.LegioID!= 0)
                    {
                        if (legio!= null) //update
                        {
                            legio.Updater = Environment.UserName;
                            legio.UpdaterDate = DateTime.Now;
                        }
                    }
                    else
                    {
                        Tbl30LegiosRepository.Add(new Tbl30Legio
                        {
                            InfraclassID= CurrentTbl30Legio.InfraclassID,              
                            LegioName= CurrentTbl30Legio.LegioName,              
                            CountID = TblCountersRepository.Counter(),
                            Valid = CurrentTbl30Legio.Valid,
                            ValidYear = CurrentTbl30Legio.ValidYear,
                            Synonym = CurrentTbl30Legio.Synonym,
                            Author = CurrentTbl30Legio.Author,
                            AuthorYear = CurrentTbl30Legio.AuthorYear,
                            Info = CurrentTbl30Legio.Info,
                            EngName = CurrentTbl30Legio.EngName,
                            GerName = CurrentTbl30Legio.GerName,
                            FraName = CurrentTbl30Legio.FraName,
                            PorName = CurrentTbl30Legio.PorName,
                            Writer = Environment.UserName,
                            WriterDate = DateTime.Now,
                            Updater = Environment.UserName,
                            UpdaterDate = DateTime.Now,
                            Memo = CurrentTbl30Legio.Memo
                        });
                    }
                    {
                        Tbl30LegiosRepository.Save();
                        MessageBox.Show(CurrentTbl30Legio.LegioName+  " was successfully saved ");
                        GetLegioByName();  //Refresh
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion "Public Commands"
  

        
        #region "Public Commands Connect <== Tbl27Infraclass"                 

        private RelayCommand _getInfraclassByNameCommand;
        public new ICommand GetInfraclassByNameCommand
        {
            get { return _getInfraclassByNameCommand ?? (_getInfraclassByNameCommand = new RelayCommand(GetInfraclassByName)); }
        }

        private void GetInfraclassByName()
        {
            Tbl27InfraclassesList =
                new ObservableCollection<Tbl27Infraclass>((from infraclass in Tbl27InfraclassesRepository.Tbl27Infraclasses
                                                       where infraclass.InfraclassName.StartsWith(SearchInfraclassName)
                                                       orderby infraclass.InfraclassName
                                                       select infraclass));

            View = CollectionViewSource.GetDefaultView(Tbl27InfraclassesList);
            if (View != null)
                View.CurrentChanged += tbl27InfraclassView_CurrentChanged;                   
            RaisePropertyChanged("CurrentTbl27Infraclass");
        }
        //------------------------------------------------------------------------------
        private RelayCommand _addInfraclassCommand;
        public new ICommand AddInfraclassCommand
        {
            get { return _addInfraclassCommand ?? (_addInfraclassCommand = new RelayCommand(AddInfraclass)); }
        }

        private void AddInfraclass()
        {
            if (Tbl27InfraclassesList == null)
                Tbl27InfraclassesList = new ObservableCollection<Tbl27Infraclass>();
            Tbl27InfraclassesList.Add(new Tbl27Infraclass{ InfraclassName= "New " });                   
            View = CollectionViewSource.GetDefaultView(Tbl27InfraclassesList);
            if (View != null)
                View.CurrentChanged += tbl27InfraclassView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl27Infraclass");
        }
        //----------------------------------------------------------------------------------------------------------
        private RelayCommand _deleteInfraclassCommand;
        public ICommand InfraclassPhylumCommand
        {
            get { return _deleteInfraclassCommand ?? (_deleteInfraclassCommand = new RelayCommand(DeleteInfraclass)); }
        }

        private void DeleteInfraclass()
        {
            try
            {
                var infraclass= Tbl27InfraclassesRepository.Tbl27Infraclasses.FirstOrDefault(x => x.InfraclassID== CurrentTbl27Infraclass.InfraclassID);
                if (infraclass!= null)
                {
                    if (MessageBox.Show("Are you really shure to delete this "
                                        + CurrentTbl27Infraclass.InfraclassName, "Question", MessageBoxButton.YesNo) !=
                        MessageBoxResult.Yes) return;
                    Tbl27InfraclassesRepository.Delete(infraclass);
                    Tbl27InfraclassesRepository.Save();
                    MessageBox.Show(CurrentTbl27Infraclass.InfraclassName+ " was deleted successfully");
                    if (SearchInfraclassName == null)
                    {
                        GetConnectedTablesById(); //refresh doubleClick                    
                    }
                    else
                    {
                        GetInfraclassByName(); //search
                    }
                }
                else
                {
                    MessageBox.Show("Only " + CurrentTbl27Infraclass.InfraclassName+ " can be deleted");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //-------------------------------------------------------------------------------------------------
        private RelayCommand _saveInfraclassCommand;   
        public new ICommand SaveInfraclassCommand
        {
            get { return _saveInfraclassCommand ?? (_saveInfraclassCommand = new RelayCommand(SaveInfraclass)); }
        }

        private void SaveInfraclass()
        {
            try
            {
                var infraclass= Tbl27InfraclassesRepository.Tbl27Infraclasses.FirstOrDefault(x => x.InfraclassID== CurrentTbl27Infraclass.InfraclassID);
                if (CurrentTbl27Infraclass == null)
                {
                    MessageBox.Show("infraclass was not found");
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
                        Tbl27InfraclassesRepository.Add(new Tbl27Infraclass()
                        {
                            InfraclassName= CurrentTbl27Infraclass.InfraclassName,              
                            CountID = TblCountersRepository.Counter(),
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
                        MessageBox.Show(CurrentTbl27Infraclass.InfraclassName+  " was successfully saved ");
                        GetInfraclassByName();  //Refresh
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion "Public Commands"
    
  
        
        #region "Public Commands Connect ==> Tbl33Ordo"                 

        private RelayCommand _getOrdoByNameCommand;
        public ICommand GetOrdoByNameCommand
        {
            get { return _getOrdoByNameCommand ?? (_getOrdoByNameCommand = new RelayCommand(GetOrdoByName)); }
        }

        private void GetOrdoByName()
        {
            Tbl33OrdosList =
                new ObservableCollection<Tbl33Ordo>((from ordo in Tbl33OrdosRepository.Tbl33Ordos
                                                       where ordo.OrdoName.StartsWith(SearchOrdoName)
                                                       orderby ordo.OrdoName
                                                       select ordo));

            View = CollectionViewSource.GetDefaultView(Tbl33OrdosList);
            if (View != null)
                View.CurrentChanged += tbl33OrdoView_CurrentChanged;                   
            RaisePropertyChanged("CurrentTbl33Ordo");
        }
        //------------------------------------------------------------------------------
        private RelayCommand _addOrdoCommand;
        public ICommand AddOrdoCommand
        {
            get { return _addOrdoCommand ?? (_addOrdoCommand = new RelayCommand(AddOrdo)); }
        }

        private void AddOrdo()
        {
            if (Tbl33OrdosList == null)
                Tbl33OrdosList = new ObservableCollection<Tbl33Ordo>();
            Tbl33OrdosList.Add(new Tbl33Ordo{ OrdoName= "New " });                   
            View = CollectionViewSource.GetDefaultView(Tbl33OrdosList);
            if (View != null)
                View.CurrentChanged += tbl33OrdoView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl33Ordo");
        }
        //---------------------------------------------------------------------------------------
        private RelayCommand _deleteOrdoCommand;
        public ICommand DeleteOrdoCommand
        {
            get { return _deleteOrdoCommand ?? (_deleteOrdoCommand = new RelayCommand(DeleteOrdo)); }
        }

        private void DeleteOrdo()
        {
            try
            {
                var ordo = Tbl33OrdosRepository.Tbl33Ordos.FirstOrDefault(x => x.OrdoID== CurrentTbl33Ordo.OrdoID);
                if (ordo != null)
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
        public ICommand SaveOrdoCommand
        {
            get { return _saveOrdoCommand ?? (_saveOrdoCommand = new RelayCommand(SaveOrdo)); }
        }

        private void SaveOrdo()
        {
            try
            {
                var ordo = Tbl33OrdosRepository.Tbl33Ordos.FirstOrDefault(x => x.OrdoID== CurrentTbl33Ordo.OrdoID);
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
                        Tbl33OrdosRepository.Add(new Tbl33Ordo
                        {
                            LegioID= CurrentTbl33Ordo.LegioID,              
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
                        if (SearchOrdoName == null)
                        {
                            GetConnectedTablesById(); //refresh doubleClick                    
                        }
                        else
                        {
                            GetOrdoByName(); //search
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
    
  
        
        #region "Public Commands Connect ==> Tbl36Subordo"                 

        private RelayCommand _getNULLByNameCommand;
        public ICommand GetNULLByNameCommand
        {
            get { return _getNULLByNameCommand ?? (_getNULLByNameCommand = new RelayCommand(GetNULLByName)); }
        }

        private void GetNULLByName()
        {
            Tbl36SubordosList =
                new ObservableCollection<Tbl36Subordo>((from  in Tbl36SubordosRepository.Tbl36Subordos
                                                       where .SubordoName.StartsWith(SearchNULLName)
                                                       orderby .SubordoName
                                                       select ));

            View = CollectionViewSource.GetDefaultView(Tbl36SubordosList);
            if (View != null)
                View.CurrentChanged += View_CurrentChanged;                   
            RaisePropertyChanged("CurrentTbl36Subordo");
        }
        //------------------------------------------------------------------------------
        private RelayCommand _addNULLCommand;
        public ICommand AddNULLCommand
        {
            get { return _addNULLCommand ?? (_addNULLCommand = new RelayCommand(AddNULL)); }
        }

        private void AddNULL()
        {
            if (Tbl36SubordosList == null)
                Tbl36SubordosList = new ObservableCollection<Tbl36Subordo>();
            Tbl36SubordosList.Add(new Tbl36Subordo{ SubordoName= "New " });                   
            View = CollectionViewSource.GetDefaultView(Tbl36SubordosList);
            if (View != null)
                View.CurrentChanged += View_CurrentChanged;
            RaisePropertyChanged("CurrentTbl36Subordo");
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
                var = Tbl36SubordosRepository.Tbl36Subordos.FirstOrDefault(x => x.SubordoID== CurrentTbl36Subordo.SubordoID);
                if (!= null)
                {
                    if (MessageBox.Show("Are you really shure to delete this "
                                        + CurrentTbl36Subordo.SubordoName, "Question", MessageBoxButton.YesNo) !=
                        MessageBoxResult.Yes) return;
                    Tbl36SubordosRepository.Delete();
                    Tbl36SubordosRepository.Save();
                    MessageBox.Show(CurrentTbl36Subordo.SubordoName+ " was deleted successfully");
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
                    MessageBox.Show("Only " + CurrentTbl36Subordo.SubordoName+ " can be deleted");
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
                var = Tbl36SubordosRepository.Tbl36Subordos.FirstOrDefault(x => x.SubordoID== CurrentTbl36Subordo.SubordoID);
                if (CurrentTbl36Subordo == null)
                {
                    MessageBox.Show(" was not found");
                }
                else
                {
                    if (CurrentTbl36Subordo.SubordoID!= 0)
                    {
                        if (!= null) //update
                        {
                            .Updater = Environment.UserName;
                            .UpdaterDate = DateTime.Now;
                        }
                    }
                    else
                    {
                        Tbl36SubordosRepository.Add(new Tbl36Subordo
                        {
                            LegioID= CurrentTbl36Subordo.LegioID,              
                            SubordoName= CurrentTbl36Subordo.SubordoName,              
                            CountID = TblCountersRepository.Counter(),
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
                        MessageBox.Show(CurrentTbl36Subordo.SubordoName+  " was successfully saved ");              
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
                            LegioID= CurrentTbl90RefAuthor.LegioID,
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
                            LegioID= CurrentTbl90RefSource.LegioID,
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
                            LegioID= CurrentTbl90RefExpert.LegioID,
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
                            LegioID= CurrentTbl93Comment.LegioID,                
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
            SearchInfraclassName = null;                       
            SearchOrdoName = null;
            SearchCommentInfo = null;
            SearchRefExpertName = null;
            SearchRefSourceName = null;
            SearchRefAuthorName = null;

            Tbl27InfraclassesList =
                new ObservableCollection<Tbl27Infraclass>((from infraclass in Tbl27InfraclassesRepository.Tbl27Infraclasses
                                                       where infraclass.InfraclassID== CurrentTbl30Legio.InfraclassID
                                                       orderby infraclass.InfraclassName
                                                       select infraclass));

            View = CollectionViewSource.GetDefaultView(Tbl27InfraclassesList);
            if (View != null)
                View.CurrentChanged += tbl27InfraclassView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl27Infraclasses");
            //-----------------------------------------------------------------------------------
            Tbl33OrdosList =
                new ObservableCollection<Tbl33Ordo>((from ordo in Tbl33OrdosRepository.Tbl33Ordos
                                                       where ordo.LegioID== CurrentTbl30Legio.LegioID
                                                       orderby ordo.Tbl30Legios.LegioName
                                                       select ordo));


            View = CollectionViewSource.GetDefaultView(Tbl33OrdosList);
            if (View != null)
                View.CurrentChanged += tbl33OrdoView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl33Ordos");
            //-----------------------------------------------------------------------------------
            Tbl90RefAuthorsList =
                new ObservableCollection<Tbl90Reference>((from refAu in Tbl90ReferencesRepository.Tbl90References
                                                          where refAu.LegioID== CurrentTbl30Legio.LegioID
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
                                                          where refSo.LegioID== CurrentTbl30Legio.LegioID
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
                                                          where refEx.LegioID== CurrentTbl30Legio.LegioID
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
                                                          where comm.LegioID== CurrentTbl30Legio.LegioID
                                                        orderby comm.Info
                                                        select comm));

            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            if (CommentsView != null)
                CommentsView.CurrentChanged += tbl93CommentView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl93Comment");
            //--------------------------------------------------------------

        }

        #endregion "Public Commands Connected Tables by DoubleClick"
   

     
        #region "Public Properties Tbl30Legio"

        public new ICollectionView View;
        public new Tbl30Legio CurrentTbl30Legio
        {
            get
            {
                if (View != null)
                    return View.CurrentItem as Tbl30Legio;
                return null;
            }
        }
        //--------------------------------------------
        private string _searchLegioName;
        public new string SearchLegioName
        {
            get { return _searchLegioName; }
            set
            {
                if (value == _searchLegioName) return;
                _searchLegioName = value;
                RaisePropertyChanged("SearchLegioName");
            }
        }

        private ObservableCollection<Tbl30Legio> _tbl30LegiosList;
        public new ObservableCollection<Tbl30Legio> Tbl30LegiosList
        {
            get { return _tbl30LegiosList; }
            set
            {
                if (_tbl30LegiosList == value) return;
                _tbl30LegiosList = value;
                RaisePropertyChanged("Tbl30LegiosList");

                //Clear Search-TextBox
                SearchInfraclassName = null;                                
                SearchOrdoName = null;
                SearchCommentInfo = null;
                SearchRefExpertName = null;
                SearchRefSourceName = null;
                SearchRefAuthorName = null;
            }
        }

        private ObservableCollection<Tbl30Legio> _tbl30LegiosAllList;
        public ObservableCollection<Tbl30Legio> Tbl30LegiosAllList
        {
            get { return _tbl30LegiosAllList; }
            set
            {
                if (_tbl30LegiosAllList == value) return;
                _tbl30LegiosAllList = value;
                RaisePropertyChanged("Tbl30LegiosAllList");
            }
        }

        #endregion "Public Properties"
   

       
        #region "Public Properties Tbl27Infraclass"

        public new ICollectionView View;
        public new Tbl27Infraclass CurrentTbl27Infraclass
        {
            get
            {
                if (View != null)
                    return View.CurrentItem as Tbl27Infraclass;
                return null;
            }
        }
        //--------------------------------------------                                               

        private string _searchInfraclassName;
        public new string SearchInfraclassName
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
        public new ObservableCollection<Tbl27Infraclass> Tbl27InfraclassesList
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
   
  
       
        #region "Public Properties Tbl33Ordo"

        public ICollectionView View;
        public Tbl33Ordo CurrentTbl33Ordo
        {
            get
            {
                if (View != null)
                    return View.CurrentItem as Tbl33Ordo;
                return null;
            }
        }
        //--------------------------------------------                                               

        private string _searchOrdoName;
        public string SearchOrdoName
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
        public ObservableCollection<Tbl33Ordo> Tbl33OrdosList
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
   
  
       
        #region "Public Properties Tbl36Subordo"

        public ICollectionView View;
        public Tbl36Subordo CurrentTbl36Subordo
        {
            get
            {
                if (View != null)
                    return View.CurrentItem as Tbl36Subordo;
                return null;
            }
        }
        //--------------------------------------------                                               

        private string _searchSubordoName;
        public string SearchSubordoName
        {
            get { return _searchSubordoName; }
            set
            {
                if (value == _searchSubordoName) return;
                _searchSubordoName = value;
                RaisePropertyChanged("SearchSubordoName");
            }
        }

        private ObservableCollection<Tbl36Subordo> List;
        public ObservableCollection<Tbl36Subordo> Tbl36SubordosList
        {
            get { return List; }
            set
            {
                if (List == value) return;
                List = value;
                RaisePropertyChanged("Tbl36SubordosList");
            }
        }

        #endregion "Public Properties"
   
          
        #region "Private Methods"

        public void tbl33OrdoView_CurrentChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged("CurrentTbl33Ordo");
        }
        #endregion "Private Methods"
   
   

   
    }
}   
