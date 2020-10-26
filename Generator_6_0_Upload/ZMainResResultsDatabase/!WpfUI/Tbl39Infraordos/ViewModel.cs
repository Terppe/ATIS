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

//    Tbl39InfraordoViewModel Skriptdatum:  7.1.2012  10:32    

namespace Atis.WpfUi.ViewModel
{   


    
    public class Tbl39InfraordosViewModel : Tbl36SubordosViewModel                     
    {     
         
      #region "Private Data Members"

        protected readonly Tbl42SuperfamiliesRepository Tbl42SuperfamiliesRepository = new Tbl42SuperfamiliesRepository();   
          
          #endregion "Private Data Members"            
    
        #region "Constructor"

        public Tbl39InfraordosViewModel()
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
       
        #region "Public Commands Basic Tbl39Infraordo"

        private RelayCommand _getInfraordoByNameCommand;
        public new ICommand GetInfraordoByNameCommand
        {
            get { return _getInfraordoByNameCommand ?? (_getInfraordoByNameCommand = new RelayCommand(GetInfraordoByName)); }
        }

        private void GetInfraordoByName()
        {   
Tbl39InfraordosList =
                 new ObservableCollection<Tbl39Infraordo>((from x in Tbl39InfraordosRepository.Tbl39Infraordos
                                                        where x.InfraordoName.StartsWith(SearchInfraordoName)
                                                        orderby x.InfraordoName
                                                        select x));

            Tbl39InfraordosAllList =
                 new ObservableCollection<Tbl39Infraordo>((from y in Tbl39InfraordosRepository.Tbl39Infraordos
                                                        orderby y.InfraordoName
                                                        select y));

            Tbl36SubordosAllList =
                 new ObservableCollection<Tbl36Subordo>((from z in Tbl36SubordosRepository.Tbl36Subordos
                                                        orderby z.SubordoName
                                                        select z));

              
  Tbl33OrdosAllList =
                 new ObservableCollection<Tbl33Ordo>((from z in Tbl33OrdosRepository.Tbl33Ordos
                                                        orderby z.OrdoName
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

  
Tbl36SubordosList = null;                  
  Tbl42SuperfamiliesList = null;     
             
  View = CollectionViewSource.GetDefaultView(Tbl39InfraordosList);
            if (View != null)
                View.CurrentChanged += tbl39InfraordoView_CurrentChanged;                   
            RaisePropertyChanged("CurrentTbl39Infraordo");
        }
        //------------------------------------------------------------------------------
        private RelayCommand _addInfraordoCommand;
        public new ICommand AddInfraordoCommand
        {
            get { return _addInfraordoCommand ?? (_addInfraordoCommand = new RelayCommand(AddInfraordo)); }
        }

        private void AddInfraordo()
        {
            if (Tbl39InfraordosList == null)
                Tbl39InfraordosList = new ObservableCollection<Tbl39Infraordo>();
            Tbl39InfraordosList.Add(new Tbl39Infraordo{ InfraordoName= "New " });
            View = CollectionViewSource.GetDefaultView(Tbl39InfraordosList);
            if (View != null)
                View.CurrentChanged += tbl39InfraordoView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl39Infraordo");
        }
        //---------------------------------------------------------------------------------------
  
       
        private RelayCommand _deleteInfraordoCommand;
        public new ICommand DeleteInfraordoCommand
        {
            get { return _deleteInfraordoCommand ?? (_deleteInfraordoCommand = new RelayCommand(DeleteInfraordo)); }
        }

        private void DeleteInfraordo()
        {
            try
            {
                var infraordo= Tbl39InfraordosRepository.Tbl39Infraordos.FirstOrDefault(x => x.InfraordoID== CurrentTbl39Infraordo.InfraordoID);
                if (infraordo!= null)
                {
                    if (MessageBox.Show("Are you really shure to delete this "
                                        + CurrentTbl39Infraordo.InfraordoName, "Question", MessageBoxButton.YesNo) !=
                        MessageBoxResult.Yes) return;
                    Tbl39InfraordosRepository.Delete(infraordo);
                    Tbl39InfraordosRepository.Save();
                    MessageBox.Show(CurrentTbl39Infraordo.InfraordoName + " was deleted successfully");
                    GetInfraordoByName(); //Refresh
                }
                else
                {
                    MessageBox.Show("Only " + CurrentTbl39Infraordo.InfraordoName+ " can be deleted");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //-------------------------------------------------------------------------------------------------
        private RelayCommand _saveInfraordoCommand;
        public new ICommand SaveInfraordoCommand
        {
            get { return _saveInfraordoCommand ?? (_saveInfraordoCommand = new RelayCommand(SaveInfraordo)); }
        }

        private void SaveInfraordo()
        {
            try
            {
                var infraordo= Tbl39InfraordosRepository.Tbl39Infraordos.FirstOrDefault(x => x.InfraordoID== CurrentTbl39Infraordo.InfraordoID);
                if (CurrentTbl39Infraordo == null)
                {
                    MessageBox.Show("infraordo was not found");
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
                            CountID = TblCountersRepository.Counter(),
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
                        MessageBox.Show(CurrentTbl39Infraordo.InfraordoName+  " was successfully saved ");
                        GetInfraordoByName();  //Refresh
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion "Public Commands"
  

        
        #region "Public Commands Connect <== Tbl36Subordo"                 

        private RelayCommand _getSubordoByNameCommand;
        public new ICommand GetSubordoByNameCommand
        {
            get { return _getSubordoByNameCommand ?? (_getSubordoByNameCommand = new RelayCommand(GetSubordoByName)); }
        }

        private void GetSubordoByName()
        {
            Tbl36SubordosList =
                new ObservableCollection<Tbl36Subordo>((from subordo in Tbl36SubordosRepository.Tbl36Subordos
                                                       where subordo.SubordoName.StartsWith(SearchSubordoName)
                                                       orderby subordo.SubordoName
                                                       select subordo));

            View = CollectionViewSource.GetDefaultView(Tbl36SubordosList);
            if (View != null)
                View.CurrentChanged += tbl36SubordoView_CurrentChanged;                   
            RaisePropertyChanged("CurrentTbl36Subordo");
        }
        //------------------------------------------------------------------------------
        private RelayCommand _addSubordoCommand;
        public new ICommand AddSubordoCommand
        {
            get { return _addSubordoCommand ?? (_addSubordoCommand = new RelayCommand(AddSubordo)); }
        }

        private void AddSubordo()
        {
            if (Tbl36SubordosList == null)
                Tbl36SubordosList = new ObservableCollection<Tbl36Subordo>();
            Tbl36SubordosList.Add(new Tbl36Subordo{ SubordoName= "New " });                   
            View = CollectionViewSource.GetDefaultView(Tbl36SubordosList);
            if (View != null)
                View.CurrentChanged += tbl36SubordoView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl36Subordo");
        }
        //----------------------------------------------------------------------------------------------------------
        private RelayCommand _deleteSubordoCommand;
        public ICommand SubordoPhylumCommand
        {
            get { return _deleteSubordoCommand ?? (_deleteSubordoCommand = new RelayCommand(DeleteSubordo)); }
        }

        private void DeleteSubordo()
        {
            try
            {
                var subordo= Tbl36SubordosRepository.Tbl36Subordos.FirstOrDefault(x => x.SubordoID== CurrentTbl36Subordo.SubordoID);
                if (subordo!= null)
                {
                    if (MessageBox.Show("Are you really shure to delete this "
                                        + CurrentTbl36Subordo.SubordoName, "Question", MessageBoxButton.YesNo) !=
                        MessageBoxResult.Yes) return;
                    Tbl36SubordosRepository.Delete(subordo);
                    Tbl36SubordosRepository.Save();
                    MessageBox.Show(CurrentTbl36Subordo.SubordoName+ " was deleted successfully");
                    if (SearchSubordoName == null)
                    {
                        GetConnectedTablesById(); //refresh doubleClick                    
                    }
                    else
                    {
                        GetSubordoByName(); //search
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
        private RelayCommand _saveSubordoCommand;   
        public new ICommand SaveSubordoCommand
        {
            get { return _saveSubordoCommand ?? (_saveSubordoCommand = new RelayCommand(SaveSubordo)); }
        }

        private void SaveSubordo()
        {
            try
            {
                var subordo= Tbl36SubordosRepository.Tbl36Subordos.FirstOrDefault(x => x.SubordoID== CurrentTbl36Subordo.SubordoID);
                if (CurrentTbl36Subordo == null)
                {
                    MessageBox.Show("subordo was not found");
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
                        Tbl36SubordosRepository.Add(new Tbl36Subordo()
                        {
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
                        GetSubordoByName();  //Refresh
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion "Public Commands"
    
  
        
        #region "Public Commands Connect ==> Tbl42Superfamily"                 

        private RelayCommand _getSuperfamilyByNameCommand;
        public ICommand GetSuperfamilyByNameCommand
        {
            get { return _getSuperfamilyByNameCommand ?? (_getSuperfamilyByNameCommand = new RelayCommand(GetSuperfamilyByName)); }
        }

        private void GetSuperfamilyByName()
        {
            Tbl42SuperfamiliesList =
                new ObservableCollection<Tbl42Superfamily>((from superfamily in Tbl42SuperfamiliesRepository.Tbl42Superfamilies
                                                       where superfamily.SuperfamilyName.StartsWith(SearchSuperfamilyName)
                                                       orderby superfamily.SuperfamilyName
                                                       select superfamily));

            View = CollectionViewSource.GetDefaultView(Tbl42SuperfamiliesList);
            if (View != null)
                View.CurrentChanged += tbl42SuperfamilyView_CurrentChanged;                   
            RaisePropertyChanged("CurrentTbl42Superfamily");
        }
        //------------------------------------------------------------------------------
        private RelayCommand _addSuperfamilyCommand;
        public ICommand AddSuperfamilyCommand
        {
            get { return _addSuperfamilyCommand ?? (_addSuperfamilyCommand = new RelayCommand(AddSuperfamily)); }
        }

        private void AddSuperfamily()
        {
            if (Tbl42SuperfamiliesList == null)
                Tbl42SuperfamiliesList = new ObservableCollection<Tbl42Superfamily>();
            Tbl42SuperfamiliesList.Add(new Tbl42Superfamily{ SuperfamilyName= "New " });                   
            View = CollectionViewSource.GetDefaultView(Tbl42SuperfamiliesList);
            if (View != null)
                View.CurrentChanged += tbl42SuperfamilyView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl42Superfamily");
        }
        //---------------------------------------------------------------------------------------
        private RelayCommand _deleteSuperfamilyCommand;
        public ICommand DeleteSuperfamilyCommand
        {
            get { return _deleteSuperfamilyCommand ?? (_deleteSuperfamilyCommand = new RelayCommand(DeleteSuperfamily)); }
        }

        private void DeleteSuperfamily()
        {
            try
            {
                var superfamily = Tbl42SuperfamiliesRepository.Tbl42Superfamilies.FirstOrDefault(x => x.SuperfamilyID== CurrentTbl42Superfamily.SuperfamilyID);
                if (superfamily != null)
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
        public ICommand SaveSuperfamilyCommand
        {
            get { return _saveSuperfamilyCommand ?? (_saveSuperfamilyCommand = new RelayCommand(SaveSuperfamily)); }
        }

        private void SaveSuperfamily()
        {
            try
            {
                var superfamily = Tbl42SuperfamiliesRepository.Tbl42Superfamilies.FirstOrDefault(x => x.SuperfamilyID== CurrentTbl42Superfamily.SuperfamilyID);
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
                        Tbl42SuperfamiliesRepository.Add(new Tbl42Superfamily
                        {
                            InfraordoID= CurrentTbl42Superfamily.InfraordoID,              
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
                        if (SearchSuperfamilyName == null)
                        {
                            GetConnectedTablesById(); //refresh doubleClick                    
                        }
                        else
                        {
                            GetSuperfamilyByName(); //search
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
    
  
        
        #region "Public Commands Connect ==> Tbl45Family"                 

        private RelayCommand _getNULLByNameCommand;
        public ICommand GetNULLByNameCommand
        {
            get { return _getNULLByNameCommand ?? (_getNULLByNameCommand = new RelayCommand(GetNULLByName)); }
        }

        private void GetNULLByName()
        {
            Tbl45FamiliesList =
                new ObservableCollection<Tbl45Family>((from  in Tbl45FamiliesRepository.Tbl45Families
                                                       where .FamilyName.StartsWith(SearchNULLName)
                                                       orderby .FamilyName
                                                       select ));

            View = CollectionViewSource.GetDefaultView(Tbl45FamiliesList);
            if (View != null)
                View.CurrentChanged += View_CurrentChanged;                   
            RaisePropertyChanged("CurrentTbl45Family");
        }
        //------------------------------------------------------------------------------
        private RelayCommand _addNULLCommand;
        public ICommand AddNULLCommand
        {
            get { return _addNULLCommand ?? (_addNULLCommand = new RelayCommand(AddNULL)); }
        }

        private void AddNULL()
        {
            if (Tbl45FamiliesList == null)
                Tbl45FamiliesList = new ObservableCollection<Tbl45Family>();
            Tbl45FamiliesList.Add(new Tbl45Family{ FamilyName= "New " });                   
            View = CollectionViewSource.GetDefaultView(Tbl45FamiliesList);
            if (View != null)
                View.CurrentChanged += View_CurrentChanged;
            RaisePropertyChanged("CurrentTbl45Family");
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
                var = Tbl45FamiliesRepository.Tbl45Families.FirstOrDefault(x => x.FamilyID== CurrentTbl45Family.FamilyID);
                if (!= null)
                {
                    if (MessageBox.Show("Are you really shure to delete this "
                                        + CurrentTbl45Family.FamilyName, "Question", MessageBoxButton.YesNo) !=
                        MessageBoxResult.Yes) return;
                    Tbl45FamiliesRepository.Delete();
                    Tbl45FamiliesRepository.Save();
                    MessageBox.Show(CurrentTbl45Family.FamilyName+ " was deleted successfully");
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
                    MessageBox.Show("Only " + CurrentTbl45Family.FamilyName+ " can be deleted");
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
                var = Tbl45FamiliesRepository.Tbl45Families.FirstOrDefault(x => x.FamilyID== CurrentTbl45Family.FamilyID);
                if (CurrentTbl45Family == null)
                {
                    MessageBox.Show(" was not found");
                }
                else
                {
                    if (CurrentTbl45Family.FamilyID!= 0)
                    {
                        if (!= null) //update
                        {
                            .Updater = Environment.UserName;
                            .UpdaterDate = DateTime.Now;
                        }
                    }
                    else
                    {
                        Tbl45FamiliesRepository.Add(new Tbl45Family
                        {
                            InfraordoID= CurrentTbl45Family.InfraordoID,              
                            FamilyName= CurrentTbl45Family.FamilyName,              
                            CountID = TblCountersRepository.Counter(),
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
                        MessageBox.Show(CurrentTbl45Family.FamilyName+  " was successfully saved ");              
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
                            InfraordoID= CurrentTbl90RefAuthor.InfraordoID,
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
                            InfraordoID= CurrentTbl90RefSource.InfraordoID,
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
                            InfraordoID= CurrentTbl90RefExpert.InfraordoID,
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
                            InfraordoID= CurrentTbl93Comment.InfraordoID,                
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
            SearchSubordoName = null;                       
            SearchSuperfamilyName = null;
            SearchCommentInfo = null;
            SearchRefExpertName = null;
            SearchRefSourceName = null;
            SearchRefAuthorName = null;

            Tbl36SubordosList =
                new ObservableCollection<Tbl36Subordo>((from subordo in Tbl36SubordosRepository.Tbl36Subordos
                                                       where subordo.SubordoID== CurrentTbl39Infraordo.SubordoID
                                                       orderby subordo.SubordoName
                                                       select subordo));

            View = CollectionViewSource.GetDefaultView(Tbl36SubordosList);
            if (View != null)
                View.CurrentChanged += tbl36SubordoView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl36Subordos");
            //-----------------------------------------------------------------------------------
            Tbl42SuperfamiliesList =
                new ObservableCollection<Tbl42Superfamily>((from superfamily in Tbl42SuperfamiliesRepository.Tbl42Superfamilies
                                                       where superfamily.InfraordoID== CurrentTbl39Infraordo.InfraordoID
                                                       orderby superfamily.Tbl39Infraordos.InfraordoName
                                                       select superfamily));


            View = CollectionViewSource.GetDefaultView(Tbl42SuperfamiliesList);
            if (View != null)
                View.CurrentChanged += tbl42SuperfamilyView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl42Superfamilies");
            //-----------------------------------------------------------------------------------
            Tbl90RefAuthorsList =
                new ObservableCollection<Tbl90Reference>((from refAu in Tbl90ReferencesRepository.Tbl90References
                                                          where refAu.InfraordoID== CurrentTbl39Infraordo.InfraordoID
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
                                                          where refSo.InfraordoID== CurrentTbl39Infraordo.InfraordoID
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
                                                          where refEx.InfraordoID== CurrentTbl39Infraordo.InfraordoID
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
                                                          where comm.InfraordoID== CurrentTbl39Infraordo.InfraordoID
                                                        orderby comm.Info
                                                        select comm));

            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            if (CommentsView != null)
                CommentsView.CurrentChanged += tbl93CommentView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl93Comment");
            //--------------------------------------------------------------

        }

        #endregion "Public Commands Connected Tables by DoubleClick"
   

     
        #region "Public Properties Tbl39Infraordo"

        public new ICollectionView View;
        public new Tbl39Infraordo CurrentTbl39Infraordo
        {
            get
            {
                if (View != null)
                    return View.CurrentItem as Tbl39Infraordo;
                return null;
            }
        }
        //--------------------------------------------
        private string _searchInfraordoName;
        public new string SearchInfraordoName
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
        public new ObservableCollection<Tbl39Infraordo> Tbl39InfraordosList
        {
            get { return _tbl39InfraordosList; }
            set
            {
                if (_tbl39InfraordosList == value) return;
                _tbl39InfraordosList = value;
                RaisePropertyChanged("Tbl39InfraordosList");

                //Clear Search-TextBox
                SearchSubordoName = null;                                
                SearchSuperfamilyName = null;
                SearchCommentInfo = null;
                SearchRefExpertName = null;
                SearchRefSourceName = null;
                SearchRefAuthorName = null;
            }
        }

        private ObservableCollection<Tbl39Infraordo> _tbl39InfraordosAllList;
        public ObservableCollection<Tbl39Infraordo> Tbl39InfraordosAllList
        {
            get { return _tbl39InfraordosAllList; }
            set
            {
                if (_tbl39InfraordosAllList == value) return;
                _tbl39InfraordosAllList = value;
                RaisePropertyChanged("Tbl39InfraordosAllList");
            }
        }

        #endregion "Public Properties"
   

       
        #region "Public Properties Tbl36Subordo"

        public new ICollectionView View;
        public new Tbl36Subordo CurrentTbl36Subordo
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
            }
        }

        #endregion "Public Properties"
   
  
       
        #region "Public Properties Tbl42Superfamily"

        public ICollectionView View;
        public Tbl42Superfamily CurrentTbl42Superfamily
        {
            get
            {
                if (View != null)
                    return View.CurrentItem as Tbl42Superfamily;
                return null;
            }
        }
        //--------------------------------------------                                               

        private string _searchSuperfamilyName;
        public string SearchSuperfamilyName
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
        public ObservableCollection<Tbl42Superfamily> Tbl42SuperfamiliesList
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
   
  
       
        #region "Public Properties Tbl45Family"

        public ICollectionView View;
        public Tbl45Family CurrentTbl45Family
        {
            get
            {
                if (View != null)
                    return View.CurrentItem as Tbl45Family;
                return null;
            }
        }
        //--------------------------------------------                                               

        private string _searchFamilyName;
        public string SearchFamilyName
        {
            get { return _searchFamilyName; }
            set
            {
                if (value == _searchFamilyName) return;
                _searchFamilyName = value;
                RaisePropertyChanged("SearchFamilyName");
            }
        }

        private ObservableCollection<Tbl45Family> List;
        public ObservableCollection<Tbl45Family> Tbl45FamiliesList
        {
            get { return List; }
            set
            {
                if (List == value) return;
                List = value;
                RaisePropertyChanged("Tbl45FamiliesList");
            }
        }

        #endregion "Public Properties"
   
          
        #region "Private Methods"

        public void tbl42SuperfamilyView_CurrentChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged("CurrentTbl42Superfamily");
        }
        #endregion "Private Methods"
   
   

   
    }
}   
