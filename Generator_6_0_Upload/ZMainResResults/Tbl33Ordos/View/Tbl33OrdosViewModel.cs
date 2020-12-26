using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity.Validation;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using DAL;
using DAL.Helper;
using DAL.Models;
using WPFUI.ViewModel;
using GalaSoft.MvvmLight.Command;
using MessageBoxImage = System.Windows.MessageBoxImage;

    
         //    Tbl33OrdosViewModel Skriptdatum:  10.12.2020  10:32    

namespace WPFUI.Views.Database
{     
    
    public class Tbl33OrdosViewModel : Tbl03RegnumsViewModel
    {     
    
       #region "Private Data Members"  

        private readonly AllListVm _allListVm = new AllListVm();
           
        private readonly Repository<Tbl30Legio, int> _tbl30LegiosRepository = new Repository<Tbl30Legio, int>();   
   
        private readonly Repository<Tbl33Ordo, int> _tbl33OrdosRepository = new Repository<Tbl33Ordo, int>();   
           
        private readonly Repository<Tbl36Subordo, int> _tbl36SubordosRepository = new Repository<Tbl36Subordo, int>();   
           
        private readonly Repository<Tbl39Infraordo, int> _tbl39InfraordosRepository = new Repository<Tbl39Infraordo, int>();   
          
        private readonly Repository<Tbl90Reference, int> _tbl90ReferencesRepository = new Repository<Tbl90Reference, int>();
        private readonly Repository<Tbl93Comment, int> _tbl93CommentsRepository = new Repository<Tbl93Comment, int>();    

        #endregion "Private Data Members"               
    
        #region "Constructor"

        public Tbl33OrdosViewModel()
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

           
        #region "Public Commands Basic Tbl33Ordo"

        private RelayCommand _getOrdoByNameOrIdCommand;     
    
        public ICommand GetOrdoByNameOrIdCommand    
    
        {
            get { return _getOrdoByNameOrIdCommand ?? (_getOrdoByNameOrIdCommand = new RelayCommand(delegate { GetOrdoByNameOrId(null); })); }   
        }

        private void GetOrdoByNameOrId(object o)       
        {   
    
            int id;
            if (int.TryParse(SearchOrdoName, out id))
                Tbl33OrdosList = new ObservableCollection<Tbl33Ordo> { _tbl33OrdosRepository.Get(id) };
            else           
                Tbl33OrdosList = _allListVm.GetValueTbl33OrdosList(SearchOrdoName);      
Tbl30LegiosAllList = _allListVm.GetValueTbl30LegiosAllList();      
  OrdosView = CollectionViewSource.GetDefaultView(Tbl33OrdosList);
            OrdosView.Refresh();
        }
        //------------------------------------------------------------------------------                
           
        private RelayCommand _addOrdoCommand;           
    
        public ICommand AddOrdoCommand       
    
        {
            get { return _addOrdoCommand ?? (_addOrdoCommand = new RelayCommand(delegate { AddOrdo(null); })); }
        }

        private void AddOrdo(object o)
        {
            Tbl33OrdosList = new ObservableCollection<Tbl33Ordo>();   
Tbl33OrdosList.Insert(0, new Tbl33Ordo{ OrdoName= CultRes.StringsRes.DatasetNew });  

            Tbl30LegiosAllList = _allListVm.GetValueTbl30LegiosAllList();      
OrdosView = CollectionViewSource.GetDefaultView(Tbl33OrdosList);
            OrdosView.Refresh();
        }
        //---------------------------------------------------------------------------------------  
           
        private RelayCommand _copyOrdoCommand;              
    
        public ICommand CopyOrdoCommand             
         
        {
            get { return _copyOrdoCommand ?? (_copyOrdoCommand = new RelayCommand(delegate { CopyOrdo(null); })); }
        }

        private void CopyOrdo(object o)
        {
            Tbl33OrdosList = new ObservableCollection<Tbl33Ordo>();

            var ordo = _tbl33OrdosRepository.Get(CurrentTbl33Ordo.OrdoID);

            Tbl33OrdosList.Insert(0, new Tbl33Ordo
            {                 
LegioID = ordo.LegioID,              
                            OrdoName = CultRes.StringsRes.DatasetNew,              
                            Valid = ordo.Valid,
                            ValidYear = ordo.ValidYear,
                            Synonym = ordo.Synonym,
                            Author = ordo.Author,
                            AuthorYear = ordo.AuthorYear,
                            Info = ordo.Info,
                            EngName = ordo.EngName,
                            GerName = ordo.GerName,
                            FraName = ordo.FraName,
                            PorName = ordo.PorName,
                            Memo = ordo.Memo                    
        
            });

            OrdosView = CollectionViewSource.GetDefaultView(Tbl33OrdosList);
            OrdosView.Refresh();
        }
        //---------------------------------------------------------------------------------------                  
           
        private RelayCommand _deleteOrdoCommand;              
    
        public ICommand DeleteOrdoCommand             
         
        {
            get { return _deleteOrdoCommand ?? (_deleteOrdoCommand = new RelayCommand(delegate { DeleteOrdo(null); })); }
        }

        private void DeleteOrdo(object o)
        {
            try
            {
                var ordo = _tbl33OrdosRepository.Get(CurrentTbl33Ordo.OrdoID);
                if (ordo!= null)
                {
                    if (WpfMessageBox.Show(CultRes.StringsRes.DeleteQuestion1, CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl33Ordo.OrdoName,
                         MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes) 
                    return;
                    _tbl33OrdosRepository.Delete(ordo);
                    _tbl33OrdosRepository.Save();

                    WpfMessageBox.Show(CultRes.StringsRes.DeleteSuccess, CurrentTbl33Ordo.OrdoName, 
                        MessageBoxButton.OK, MessageBoxImage.Information);

                    if (SearchOrdoName == null)                   
                        GetConnectedTablesById(o); //refresh doubleClick                                       
                    else
                    {
                        Tbl33OrdosList = _allListVm.GetValueTbl33OrdosList(SearchOrdoName); 
                    }    
OrdosView = CollectionViewSource.GetDefaultView(Tbl33OrdosList);
                         OrdosView.Refresh();
                }
                else
                {
                    WpfMessageBox.Show(CultRes.StringsRes.Information, CultRes.StringsRes.DeleteCan + " " + CurrentTbl33Ordo.OrdoName+ " " + CultRes.StringsRes.DeleteCan1,
                         MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (DbEntityValidationException ex)
            {
                //Retrieve the Error messages as a list of strings
                var errorMessages = ex.EntityValidationErrors
                    .SelectMany(x => x.ValidationErrors)
                    .Select(x => x.ErrorMessage);
                //Join the list to a single string
                var fullErrorMessage = string.Join("; ", errorMessages);
                //Combine the original exeption message with the new one.
                var exeptionMessage = string.Concat(ex.Message, CultRes.StringsRes.ValidationErrors, fullErrorMessage);
                //throw a new DbEntityValidationException
                throw new DbEntityValidationException(exeptionMessage, ex.EntityValidationErrors);
            }
        }
        //-------------------------------------------------------------------------------------------------    
           
        private RelayCommand _saveOrdoCommand;              
     
        public ICommand SaveOrdoCommand             
         
        {
            get { return _saveOrdoCommand ?? (_saveOrdoCommand = new RelayCommand(delegate { SaveOrdo(null); })); }
        }

        private void SaveOrdo(object o)
        {
            try
            {
                var ordo = _tbl33OrdosRepository.Get(CurrentTbl33Ordo.OrdoID);
                if (CurrentTbl33Ordo == null)              
                    WpfMessageBox.Show(CultRes.StringsRes.Warning, CultRes.StringsRes.DatasetNotExist, 
                        MessageBoxButton.OK, MessageBoxImage.Warning);               
                else
                {
                    if (CurrentTbl33Ordo.OrdoID!= 0)
                    {
                        if (ordo!= null) //update
                        {   
ordo.OrdoName = CurrentTbl33Ordo.OrdoName;  
ordo.LegioID = CurrentTbl33Ordo.LegioID;
                            ordo.Valid = CurrentTbl33Ordo.Valid;
                            ordo.ValidYear = CurrentTbl33Ordo.ValidYear;
                            ordo.Synonym = CurrentTbl33Ordo.Synonym;
                            ordo.Author = CurrentTbl33Ordo.Author;
                            ordo.AuthorYear = CurrentTbl33Ordo.AuthorYear;
                            ordo.Info = CurrentTbl33Ordo.Info;
                            ordo.EngName = CurrentTbl33Ordo.EngName;
                            ordo.GerName = CurrentTbl33Ordo.GerName;
                            ordo.FraName = CurrentTbl33Ordo.FraName;
                            ordo.PorName = CurrentTbl33Ordo.PorName;
                            ordo.Updater = Environment.UserName;
                            ordo.UpdaterDate = DateTime.Now; 
                            ordo.Memo = CurrentTbl33Ordo.Memo;  
         
                        }
                    }
                    else
                    {
                        _tbl33OrdosRepository.Add(new Tbl33Ordo     //add new
                        {   
LegioID= CurrentTbl33Ordo.LegioID,              
                            OrdoName= CurrentTbl33Ordo.OrdoName,              
                            CountID = RandomHelper.Randomnumber(),
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
                        //LegioID may be not 0
                        if (CurrentTbl33Ordo.LegioID == 0)
                        {
                            WpfMessageBox.Show(CultRes.StringsRes.RequiredGenealogyConnect, CultRes.StringsRes.RequiredInput,
                                MessageBoxButton.OK, MessageBoxImage.Information);
                            return;
                        }

                        //check if dataset with Name and vb-name already exist
                        var dataset = new ObservableCollection<Tbl33Ordo>
                        (from a in _tbl33OrdosRepository.GetAll()
                         where
                         a.OrdoName.Trim() == CurrentTbl33Ordo.OrdoName.Trim() &&                
                         a.LegioID == CurrentTbl33Ordo.LegioID
                         select a);

                        if (dataset.Count != 0 && CurrentTbl33Ordo.OrdoID== 0)  //dataset exist
                        {
                              WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTbl33Ordo.OrdoName,
                              MessageBoxButton.OK, MessageBoxImage.Information);
                        } 

                        if (dataset.Count == 0 && CurrentTbl33Ordo.OrdoID== 0 ||
                            dataset.Count != 0 && CurrentTbl33Ordo.OrdoID != 0  ||
                            dataset.Count == 0 && CurrentTbl33Ordo.OrdoID != 0) //new dataset and update
                        {
                            if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTbl33Ordo.OrdoName,
                                    MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)                            
                                return;                            
                            {
                                _tbl33OrdosRepository.Save();

                                WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess, CurrentTbl33Ordo.OrdoName,
                                    MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                        } 
         
                        if (SearchOrdoName == null && CurrentTbl33Ordo.OrdoID == 0)  //new Dataset                        
                            Tbl33OrdosList = _allListVm.GetValueTbl33OrdosList();  //last Dataset
                        if (SearchOrdoName == null && CurrentTbl33Ordo.OrdoID != 0)   //update 
                            Tbl33OrdosList = _allListVm.GetValueTbl33OrdosList(CurrentTbl33Ordo.OrdoID);
                        if (SearchOrdoName != null && CurrentTbl33Ordo.OrdoID == 0)  //new Dataset                        
                            Tbl33OrdosList = _allListVm.GetValueTbl33OrdosList();  //last Dataset
                        if (SearchOrdoName != null && CurrentTbl33Ordo.OrdoID != 0)   //update 
                            Tbl33OrdosList = _allListVm.GetValueTbl33OrdosList(CurrentTbl33Ordo.OrdoID);

                            OrdosView = CollectionViewSource.GetDefaultView(Tbl33OrdosList);
                            OrdosView.Refresh();                          
         
                    }
                }
            }
            catch (DbEntityValidationException ex)
            {
                //  WpfMessageBox.Show(CultRes.StringsRes.Error, ex.Message, MessageBoxButton.OK, MessageBoxImage.Error);
                //Retrieve the Error messages as a list of strings
                var errorMessages = ex.EntityValidationErrors
                    .SelectMany(x => x.ValidationErrors)
                    .Select(x => x.ErrorMessage);
                //Join the list to a single string
                var fullErrorMessage = string.Join("; ", errorMessages);
                //Combine the original exeption message with the new one.
                var exeptionMessage = string.Concat(ex.Message, CultRes.StringsRes.ValidationErrors, fullErrorMessage);
                //throw a new DbEntityValidationException
                throw new DbEntityValidationException(exeptionMessage, ex.EntityValidationErrors);
            }
        }

        #endregion "Public Commands"  
      

 //    Part 2    

           
        #region "Public Commands Connect <== Tbl30Legio"                 

        private RelayCommand _getLegioByNameOrIdCommand;     
    
        public ICommand GetLegioByNameOrIdCommand    
    
        {
            get { return _getLegioByNameOrIdCommand ?? (_getLegioByNameOrIdCommand = new RelayCommand(delegate { GetLegioByNameOrId(null); })); }   
        }

        private void GetLegioByNameOrId(object o)       
        {   
    
            int id;
            if (int.TryParse(SearchLegioName, out id))
                Tbl30LegiosList = new ObservableCollection<Tbl30Legio> { _tbl30LegiosRepository.Get(id) };
            else
                Tbl30LegiosList = _allListVm.GetValueTbl30LegiosList(SearchLegioName);     
LegiosView = CollectionViewSource.GetDefaultView(Tbl30LegiosList);
            LegiosView.Refresh();
        }
        //------------------------------------------------------------------------------                
           
        private RelayCommand _addLegioCommand;      
    
        public ICommand AddLegioCommand    
    
        {
            get { return _addLegioCommand ?? (_addLegioCommand = new RelayCommand(delegate { AddLegio(null); })); }
        }

        private void AddLegio(object o)
        {
            Tbl30LegiosList = new ObservableCollection<Tbl30Legio>();   
Tbl30LegiosList.Insert(0, new Tbl30Legio{ LegioName = CultRes.StringsRes.DatasetNew });   

            if (Tbl27InfraclassesAllList == null)
            Tbl27InfraclassesAllList = _allListVm.GetValueTbl27InfraclassesAllList();    
LegiosView = CollectionViewSource.GetDefaultView(Tbl30LegiosList);
            LegiosView.Refresh();
        }
        //---------------------------------------------------------------------------------------  
           
        private RelayCommand _copyLegioCommand;            
    
        public ICommand CopyLegioCommand          
         
        {
            get { return _copyLegioCommand ?? (_copyLegioCommand = new RelayCommand(delegate { CopyLegio(null); })); }
        }

        private void CopyLegio(object o)
        {
            Tbl30LegiosList = new ObservableCollection<Tbl30Legio>();

            var legio = _tbl30LegiosRepository.Get(CurrentTbl30Legio.LegioID);

            Tbl30LegiosList.Insert(0, new Tbl30Legio
            {                 
InfraclassID = legio.InfraclassID,     
                LegioName = CultRes.StringsRes.DatasetNew,     
                Valid = legio.Valid,
                ValidYear = legio.ValidYear,
                Synonym = legio.Synonym,
                Author = legio.Author,
                AuthorYear = legio.AuthorYear,
                Info = legio.Info,
                EngName = legio.EngName,
                GerName = legio.GerName,
                FraName = legio.FraName,
                PorName = legio.PorName,
                Memo = legio.Memo           
        
            });

            LegiosView = CollectionViewSource.GetDefaultView(Tbl30LegiosList);
            LegiosView.Refresh();
        }
        //---------------------------------------------------------------------------------------                  
           
        private RelayCommand _deleteLegioCommand;              
    
        public ICommand DeleteLegioCommand             
         
        {
            get { return _deleteLegioCommand ?? (_deleteLegioCommand = new RelayCommand(delegate { DeleteLegio(null); })); }
        }

        private void DeleteLegio(object o)
        {
            try
            {
                var legio = _tbl30LegiosRepository.Get(CurrentTbl30Legio.LegioID);
                if (legio!= null)
                {  
         
                    if (WpfMessageBox.Show(CultRes.StringsRes.DeleteQuestion1, CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl30Legio.LegioName,
                         MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes) 
                    return;

                    _tbl30LegiosRepository.Delete(legio);
                    _tbl30LegiosRepository.Save();

                    WpfMessageBox.Show(CultRes.StringsRes.DeleteSuccess, CurrentTbl30Legio.LegioName, 
                        MessageBoxButton.OK, MessageBoxImage.Information);  
         
                        if (SearchLegioName == null)                       
                            GetConnectedTablesById(o); //refresh doubleClick                                          
                        else
                        {
                        Tbl30LegiosList = _allListVm.GetValueTbl30LegiosList(SearchLegioName);  
LegiosView = CollectionViewSource.GetDefaultView(Tbl30LegiosList);
                            LegiosView.Refresh();
                    }
                }
                else
                {   
    
                    WpfMessageBox.Show(CultRes.StringsRes.Information, CultRes.StringsRes.DeleteCan + " " + CurrentTbl30Legio.LegioName+ " " + CultRes.StringsRes.DeleteCan1,
                         MessageBoxButton.OK, MessageBoxImage.Information);   
    
                }
            }
            catch (DbEntityValidationException ex)
            {
                //Retrieve the Error messages as a list of strings
                var errorMessages = ex.EntityValidationErrors
                    .SelectMany(x => x.ValidationErrors)
                    .Select(x => x.ErrorMessage);
                //Join the list to a single string
                var fullErrorMessage = string.Join("; ", errorMessages);
                //Combine the original exeption message with the new one.
                var exeptionMessage = string.Concat(ex.Message, CultRes.StringsRes.ValidationErrors, fullErrorMessage);
                //throw a new DbEntityValidationException
                throw new DbEntityValidationException(exeptionMessage, ex.EntityValidationErrors);
            }
        }
        //-------------------------------------------------------------------------------------------------    
           
        private RelayCommand _saveLegioCommand;              
    
        public ICommand SaveLegioCommand             
         
        {
            get { return _saveLegioCommand ?? (_saveLegioCommand = new RelayCommand(delegate { SaveLegio(null); })); }
        }

        private void SaveLegio(object o)
        {
            try
            {
                var legio = _tbl30LegiosRepository.Get(CurrentTbl30Legio.LegioID);
                if (CurrentTbl30Legio == null)              
                    WpfMessageBox.Show(CultRes.StringsRes.Warning, CultRes.StringsRes.DatasetNotExist, MessageBoxButton.OK, MessageBoxImage.Warning);               
                else
                {
                    if (CurrentTbl30Legio.LegioID!= 0)
                    {
                        if (legio!= null) //update
                        {   
legio.InfraclassID = CurrentTbl30Legio.InfraclassID;
                            legio.LegioName= CurrentTbl30Legio.LegioName;             
legio.Valid = CurrentTbl30Legio.Valid;
                            legio.ValidYear = CurrentTbl30Legio.ValidYear;
                            legio.Synonym = CurrentTbl30Legio.Synonym;
                            legio.Author = CurrentTbl30Legio.Author;
                            legio.AuthorYear = CurrentTbl30Legio.AuthorYear;
                            legio.Info = CurrentTbl30Legio.Info;
                            legio.EngName = CurrentTbl30Legio.EngName;
                            legio.GerName = CurrentTbl30Legio.GerName;
                            legio.FraName = CurrentTbl30Legio.FraName;
                            legio.PorName = CurrentTbl30Legio.PorName;
                            legio.Updater = Environment.UserName;
                            legio.UpdaterDate = DateTime.Now; 
                            legio.Memo = CurrentTbl30Legio.Memo;   
         
                        }
                    }
                    else
                    {
                        _tbl30LegiosRepository.Add(new Tbl30Legio     //add new
                        {   
InfraclassID = CurrentTbl30Legio.InfraclassID,     
                            LegioName= CurrentTbl30Legio.LegioName,              
                            CountID = RandomHelper.Randomnumber(),
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
                        //InfraclassID may be not 0
                        if (CurrentTbl30Legio.InfraclassID == 0)
                        {
                            WpfMessageBox.Show(CultRes.StringsRes.RequiredGenealogyConnect, CultRes.StringsRes.RequiredInput,
                                MessageBoxButton.OK, MessageBoxImage.Information);
                            return;
                        }

                        //check if dataset with Name and vb-name already exist
                        var dataset = new ObservableCollection<Tbl30Legio>
                        (from a in _tbl30LegiosRepository.GetAll()
                         where
                         a.LegioName.Trim() == CurrentTbl30Legio.LegioName.Trim() &&                
                         a.InfraclassID == CurrentTbl30Legio.InfraclassID
                         select a);

                        if (dataset.Count != 0 && CurrentTbl30Legio.LegioID== 0)  //dataset exist
                        {
                              WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTbl30Legio.LegioName,
                              MessageBoxButton.OK, MessageBoxImage.Information);
                        }

                        if (dataset.Count == 0 && CurrentTbl30Legio.LegioID == 0 ||
                            dataset.Count != 0 && CurrentTbl30Legio.LegioID != 0  ||
                            dataset.Count == 0 && CurrentTbl30Legio.LegioID != 0) //new dataset and update
                        {
                            if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTbl30Legio.LegioName,
                                    MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)                            
                                return;                            
                            {
                                _tbl30LegiosRepository.Save();

                                WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess, CurrentTbl30Legio.LegioName,
                                    MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                        }  
        
                        if (SearchLegioName == null && CurrentTbl30Legio.LegioID == 0)  //new Dataset                        
                            GetConnectedTablesById(o); //refresh doubleClick                                          
                        if (SearchLegioName == null && CurrentTbl30Legio.LegioID != 0)  //update                     
                            Tbl30LegiosList = _allListVm.GetValueTbl30LegiosList(CurrentTbl30Legio.LegioID);
                        if (SearchLegioName != null && CurrentTbl30Legio.LegioID == 0)  //new Dataset                        
                            GetConnectedTablesById(o); //refresh doubleClick                                          
                        if (SearchLegioName != null && CurrentTbl30Legio.LegioID != 0)  //update                     
                            Tbl30LegiosList = _allListVm.GetValueTbl30LegiosList(CurrentTbl30Legio.LegioID); 

                        LegiosView = CollectionViewSource.GetDefaultView(Tbl30LegiosList);
                        LegiosView.Refresh();         
      
                    }
                }
            }
            catch (DbEntityValidationException ex)
            {
                //  WpfMessageBox.Show(CultRes.StringsRes.Error, ex.Message, MessageBoxButton.OK, MessageBoxImage.Error);
                //Retrieve the Error messages as a list of strings
                var errorMessages = ex.EntityValidationErrors
                    .SelectMany(x => x.ValidationErrors)
                    .Select(x => x.ErrorMessage);
                //Join the list to a single string
                var fullErrorMessage = string.Join("; ", errorMessages);
                //Combine the original exeption message with the new one.
                var exeptionMessage = string.Concat(ex.Message, CultRes.StringsRes.ValidationErrors, fullErrorMessage);
                //throw a new DbEntityValidationException
                throw new DbEntityValidationException(exeptionMessage, ex.EntityValidationErrors);
            }
        }

        #endregion "Public Commands"  
      

 //    Part 3    

      

 //    Part 4    

             
        #region "Public Commands Connect ==> Tbl36Subordo"                 

        private RelayCommand _getSubordoByNameOrIdCommand;     
               
        public ICommand GetSubordoByNameOrIdCommand   
               
        {
            get { return _getSubordoByNameOrIdCommand ?? (_getSubordoByNameOrIdCommand = new RelayCommand(delegate { GetSubordoByNameOrId(null); })); }   
        }

        private void GetSubordoByNameOrId(object o)       
        {   
                
            int id;
            if (int.TryParse(SearchSubordoName, out id))
                Tbl36SubordosList = new ObservableCollection<Tbl36Subordo> { _tbl36SubordosRepository.Get(id) };
            else 
                Tbl36SubordosList = _allListVm.GetValueTbl36SubordosList(SearchSubordoName);       
  SubordosView = CollectionViewSource.GetDefaultView(Tbl36SubordosList);
            SubordosView.Refresh();
        }
        //------------------------------------------------------------------------------     
                      
        private RelayCommand _addSubordoCommand;      
                       
        public ICommand AddSubordoCommand    
                        
        {
            get { return _addSubordoCommand ?? (_addSubordoCommand = new RelayCommand(delegate { AddSubordo(null); })); }
        }

        private void AddSubordo(object o)
        {
            Tbl36SubordosList = new ObservableCollection<Tbl36Subordo>();   
  Tbl36SubordosList.Insert(0, new Tbl36Subordo{ SubordoName= CultRes.StringsRes.DatasetNew });  

            if (Tbl33OrdosAllList == null)
            Tbl33OrdosAllList = _allListVm.GetValueTbl33OrdosAllList();     
  SubordosView = CollectionViewSource.GetDefaultView(Tbl36SubordosList);
            SubordosView.Refresh();
        }
        //---------------------------------------------------------------------------------------  
                              
        private RelayCommand _copySubordoCommand;            
                              
        public ICommand CopySubordoCommand          
                                 
        {
            get { return _copySubordoCommand ?? (_copySubordoCommand = new RelayCommand(delegate { CopySubordo(null); })); }
        }

        private void CopySubordo(object o)
        {
            Tbl36SubordosList = new ObservableCollection<Tbl36Subordo>();

            var subordo = _tbl36SubordosRepository.Get(CurrentTbl36Subordo.SubordoID);

            Tbl36SubordosList.Insert(0, new Tbl36Subordo
            {                 
  OrdoID = subordo.OrdoID,
                SubordoName = CultRes.StringsRes.DatasetNew,     
                Valid = subordo.Valid,
                ValidYear = subordo.ValidYear,
                Synonym = subordo.Synonym,
                Author = subordo.Author,
                AuthorYear = subordo.AuthorYear,
                Info = subordo.Info,
                EngName = subordo.EngName,
                GerName = subordo.GerName,
                FraName = subordo.FraName,
                PorName = subordo.PorName,
                Memo = subordo.Memo         
                                     
            });

            SubordosView = CollectionViewSource.GetDefaultView(Tbl36SubordosList);
            SubordosView.Refresh();
        }
        //---------------------------------------------------------------------------------------    
                                           
        private RelayCommand _deleteSubordoCommand;              
                                           
        public ICommand DeleteSubordoCommand             
                                                
        {
            get { return _deleteSubordoCommand ?? (_deleteSubordoCommand = new RelayCommand(delegate { DeleteSubordo(null); })); }
        }

        private void DeleteSubordo(object o)
        {
            try
            {
                var subordo = _tbl36SubordosRepository.Get(CurrentTbl36Subordo.SubordoID);
                if (subordo!= null)
                {  
                                                    
                    if (WpfMessageBox.Show(CultRes.StringsRes.DeleteQuestion1, CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl36Subordo.SubordoName,
                         MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes) 
                    return;

                    _tbl36SubordosRepository.Delete(subordo);
                    _tbl36SubordosRepository.Save();

                    WpfMessageBox.Show(CultRes.StringsRes.DeleteSuccess, CurrentTbl36Subordo.SubordoName,
                       MessageBoxButton.OK, MessageBoxImage.Information);  
                                                        
                        if (SearchSubordoName == null)                       
                            GetConnectedTablesById(o); //refresh doubleClick                                          
                        else
                        {
                        Tbl36SubordosList = _allListVm.GetValueTbl36SubordosList(SearchSubordoName);  
  SubordosView = CollectionViewSource.GetDefaultView(Tbl36SubordosList);
                            SubordosView.Refresh();
                    }
                }
                else
                {   
                                                          
                    WpfMessageBox.Show(CultRes.StringsRes.Information, CultRes.StringsRes.DeleteCan + " " + CurrentTbl36Subordo.SubordoName+ " " + CultRes.StringsRes.DeleteCan1,
                         MessageBoxButton.OK, MessageBoxImage.Information);   
                                                           
                }
            }
            catch (DbEntityValidationException ex)
            {
                //Retrieve the Error messages as a list of strings
                var errorMessages = ex.EntityValidationErrors
                    .SelectMany(x => x.ValidationErrors)
                    .Select(x => x.ErrorMessage);
                //Join the list to a single string
                var fullErrorMessage = string.Join("; ", errorMessages);
                //Combine the original exeption message with the new one.
                var exeptionMessage = string.Concat(ex.Message, CultRes.StringsRes.ValidationErrors, fullErrorMessage);
                //throw a new DbEntityValidationException
                throw new DbEntityValidationException(exeptionMessage, ex.EntityValidationErrors);
            }
        }
        //-------------------------------------------------------------------------------------------------    
                                                               
        private RelayCommand _saveSubordoCommand;              
                                                                 
        public ICommand SaveSubordoCommand             
                                                                     
        {
            get { return _saveSubordoCommand ?? (_saveSubordoCommand = new RelayCommand(delegate { SaveSubordo(null); })); }
        }

        private void SaveSubordo(object o)
        {
            try
            {
                var subordo = _tbl36SubordosRepository.Get(CurrentTbl36Subordo.SubordoID);
                if (CurrentTbl36Subordo == null)               
                    WpfMessageBox.Show(CultRes.StringsRes.Warning, CultRes.StringsRes.DatasetNotExist, 
                        MessageBoxButton.OK, MessageBoxImage.Warning);              
                else
                {
                    if (CurrentTbl36Subordo.SubordoID!= 0)
                    {
                        if (subordo!= null) //update
                        {   
  subordo.OrdoID= CurrentTbl36Subordo.OrdoID;            
                            subordo.SubordoName= CurrentTbl36Subordo.SubordoName;
                            subordo.Valid = CurrentTbl36Subordo.Valid;
                            subordo.ValidYear = CurrentTbl36Subordo.ValidYear;
                            subordo.Synonym = CurrentTbl36Subordo.Synonym;
                            subordo.Author = CurrentTbl36Subordo.Author;
                            subordo.AuthorYear = CurrentTbl36Subordo.AuthorYear;
                            subordo.Info = CurrentTbl36Subordo.Info;
                            subordo.EngName = CurrentTbl36Subordo.EngName;
                            subordo.GerName = CurrentTbl36Subordo.GerName;
                            subordo.FraName = CurrentTbl36Subordo.FraName;
                            subordo.PorName = CurrentTbl36Subordo.PorName;
                            subordo.Updater = Environment.UserName;
                            subordo.UpdaterDate = DateTime.Now;
                            subordo.Memo = CurrentTbl36Subordo.Memo;                                                              
                                                                            
                        }
                    }
                    else
                    {
                        _tbl36SubordosRepository.Add(new Tbl36Subordo    // add new
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
                        //OrdoID may be not 0
                        if (CurrentTbl36Subordo.OrdoID == 0)
                        {
                            WpfMessageBox.Show(CultRes.StringsRes.RequiredGenealogyConnect, CultRes.StringsRes.RequiredInput,
                                MessageBoxButton.OK, MessageBoxImage.Information);
                            return;
                        }

                        //check if dataset with Name and vb-name already exist
                        var dataset = new ObservableCollection<Tbl36Subordo>
                        (from a in _tbl36SubordosRepository.GetAll()
                         where
                         a.SubordoName.Trim() == CurrentTbl36Subordo.SubordoName.Trim() &&                
                         a.OrdoID == CurrentTbl36Subordo.OrdoID
                         select a);

                        if (dataset.Count != 0 && CurrentTbl36Subordo.SubordoID== 0)  //dataset exist
                        {
                              WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTbl36Subordo.SubordoName,
                              MessageBoxButton.OK, MessageBoxImage.Information);
                        }

                        if (dataset.Count == 0 && CurrentTbl36Subordo.SubordoID == 0 ||
                            dataset.Count != 0 && CurrentTbl36Subordo.SubordoID != 0  ||
                            dataset.Count == 0 && CurrentTbl36Subordo.SubordoID != 0) //new dataset and update
                        {
                            if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTbl36Subordo.SubordoName,
                                    MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)                            
                                return;                            
                            {
                                _tbl36SubordosRepository.Save();

                                WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess, CurrentTbl36Subordo.SubordoName,
                                    MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                        }  
                
                        if (SearchSubordoName == null && CurrentTbl36Subordo.SubordoID == 0)  //new Dataset                        
                            Tbl36SubordosList = _allListVm.GetValueTbl36SubordosList();  //last Dataset
                        if (SearchSubordoName == null && CurrentTbl36Subordo.SubordoID != 0)   //update
                            Tbl36SubordosList = _allListVm.GetValueTbl36SubordosList(CurrentTbl36Subordo.SubordoID);
                        if (SearchSubordoName != null && CurrentTbl36Subordo.SubordoID == 0)  //new Dataset
                            Tbl36SubordosList = _allListVm.GetValueTbl36SubordosList();  //last Dataset
                        if (SearchSubordoName != null && CurrentTbl36Subordo.SubordoID != 0)   //update
                            Tbl36SubordosList = _allListVm.GetValueTbl36SubordosList(CurrentTbl36Subordo.SubordoID);
                                                                       

                        SubordosView = CollectionViewSource.GetDefaultView(Tbl36SubordosList);
                        SubordosView.Refresh();             
                                                                                          
                    }
                }
            }
            catch (DbEntityValidationException ex)
            {
                //  WpfMessageBox.Show(CultRes.StringsRes.Error, ex.Message, MessageBoxButton.OK, MessageBoxImage.Error);
                //Retrieve the Error messages as a list of strings
                var errorMessages = ex.EntityValidationErrors
                    .SelectMany(x => x.ValidationErrors)
                    .Select(x => x.ErrorMessage);
                //Join the list to a single string
                var fullErrorMessage = string.Join("; ", errorMessages);
                //Combine the original exeption message with the new one.
                var exeptionMessage = string.Concat(ex.Message, CultRes.StringsRes.ValidationErrors, fullErrorMessage);
                //throw a new DbEntityValidationException
                throw new DbEntityValidationException(exeptionMessage, ex.EntityValidationErrors);
            }
        }

        #endregion "Public Commands"  
        

 //    Part 5    


             
        #region "Public Commands Connect ==> Tbl39Infraordo"                 

        private RelayCommand _getInfraordoByNameOrIdCommand;     
               
        public ICommand GetInfraordoByNameOrIdCommand   
               
        {
            get { return _getInfraordoByNameOrIdCommand ?? (_getInfraordoByNameOrIdCommand = new RelayCommand(delegate { GetInfraordoByNameOrId(null); })); }   
        }

        private void GetInfraordoByNameOrId(object o)       
        {   
                
            int id;
            if (int.TryParse(SearchInfraordoName, out id))
                Tbl39InfraordosList = new ObservableCollection<Tbl39Infraordo> { _tbl39InfraordosRepository.Get(id) };
            else
                Tbl39InfraordosList = _allListVm.GetValueTbl39InfraordosList (SearchInfraordoName);      
  InfraordosView = CollectionViewSource.GetDefaultView(Tbl39InfraordosList);
            InfraordosView.Refresh();
        }
        //------------------------------------------------------------------------------     
                      
        private RelayCommand _addInfraordoCommand;      
                       
        public ICommand AddInfraordoCommand    
                        
        {
            get { return _addInfraordoCommand ?? (_addInfraordoCommand = new RelayCommand(delegate { AddInfraordo(null); })); }
        }

        private void AddInfraordo(object o)
        {
            Tbl39InfraordosList = new ObservableCollection<Tbl39Infraordo>();   
  Tbl39InfraordosList.Insert(0, new Tbl39Infraordo{ InfraordoName= CultRes.StringsRes.DatasetNew });   
            Tbl33OrdosAllList = _allListVm.GetValueTbl33OrdosAllList();    
  InfraordosView = CollectionViewSource.GetDefaultView(Tbl39InfraordosList);
            InfraordosView.Refresh();
        }
        //---------------------------------------------------------------------------------------  
                              
        private RelayCommand _copyInfraordoCommand;            
                              
        public ICommand CopyInfraordoCommand          
                                 
        {
            get { return _copyInfraordoCommand ?? (_copyInfraordoCommand = new RelayCommand(delegate { CopyInfraordo(null); })); }
        }

        private void CopyInfraordo(object o)
        {
            Tbl39InfraordosList = new ObservableCollection<Tbl39Infraordo>();

            var  = _tbl39InfraordosRepository.Get(CurrentTbl39Infraordo.InfraordoID);

            Tbl39InfraordosList.Insert(0, new Tbl39Infraordo
            {                 
  OrdoID = .OrdoID,     
                InfraordoName = CultRes.StringsRes.DatasetNew,     
                Valid = .Valid,
                ValidYear = .ValidYear,
                Synonym = .Synonym,
                Author = .Author,
                AuthorYear = .AuthorYear,
                Info = .Info,
                EngName = .EngName,
                GerName = .GerName,
                FraName = .FraName,
                PorName = .PorName,
                Memo = .Memo           
                                     
            });

            InfraordosView = CollectionViewSource.GetDefaultView(Tbl39InfraordosList);
            InfraordosView.Refresh();
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
                var  = _tbl39InfraordosRepository.Get(CurrentTbl39Infraordo.InfraordoID);
                if (!= null)
                {  
                                                    
                    if (WpfMessageBox.Show(CultRes.StringsRes.DeleteQuestion1, CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl39Infraordo.InfraordoName,
                         MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes) 
                    return;

                    _tbl39InfraordosRepository.Delete();
                    _tbl39InfraordosRepository.Save();

                    WpfMessageBox.Show(CultRes.StringsRes.DeleteSuccess, CurrentTbl39Infraordo.InfraordoName,
                       MessageBoxButton.OK, MessageBoxImage.Information);  
                                                        
                        if (SearchInfraordoName == null)                       
                            GetConnectedTablesById(o); //refresh doubleClick                                          
                        else
                        {
                            Tbl39InfraordosList = _allListVm.GetValueTbl39InfraordosList(SearchInfraordoName);  
  InfraordosView = CollectionViewSource.GetDefaultView(Tbl39InfraordosList);
                            InfraordosView.Refresh();
                    }
                }
                else
                {   
                                                          
                    WpfMessageBox.Show(CultRes.StringsRes.Information, CultRes.StringsRes.DeleteCan + " " + CurrentTbl39Infraordo.InfraordoName+ " " + CultRes.StringsRes.DeleteCan1,
                         MessageBoxButton.OK, MessageBoxImage.Information);   
                                                           
                }
            }
            catch (DbEntityValidationException ex)
            {
                //Retrieve the Error messages as a list of strings
                var errorMessages = ex.EntityValidationErrors
                    .SelectMany(x => x.ValidationErrors)
                    .Select(x => x.ErrorMessage);
                //Join the list to a single string
                var fullErrorMessage = string.Join("; ", errorMessages);
                //Combine the original exeption message with the new one.
                var exeptionMessage = string.Concat(ex.Message, CultRes.StringsRes.ValidationErrors, fullErrorMessage);
                //throw a new DbEntityValidationException
                throw new DbEntityValidationException(exeptionMessage, ex.EntityValidationErrors);
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
                var  = _tbl39InfraordosRepository.Get(CurrentTbl39Infraordo.InfraordoID);
                if (CurrentTbl39Infraordo == null)                
                    WpfMessageBox.Show(CultRes.StringsRes.Warning, CultRes.StringsRes.DatasetNotExist, MessageBoxButton.OK, MessageBoxImage.Warning);              
                else
                {
                    if (CurrentTbl39Infraordo.InfraordoID!= 0)
                    {
                        if (!= null) //update
                        {   
  .OrdoID= CurrentTbl39Infraordo.OrdoID;            
                            .InfraordoName= CurrentTbl39Infraordo.InfraordoName;
                            .Valid = CurrentTbl39Infraordo.Valid;
                            .ValidYear = CurrentTbl39Infraordo.ValidYear;
                            .Synonym = CurrentTbl39Infraordo.Synonym;
                            .Author = CurrentTbl39Infraordo.Author;
                            .AuthorYear = CurrentTbl39Infraordo.AuthorYear;
                            .Info = CurrentTbl39Infraordo.Info;
                            .EngName = CurrentTbl39Infraordo.EngName;
                            .GerName = CurrentTbl39Infraordo.GerName;
                            .FraName = CurrentTbl39Infraordo.FraName;
                            .PorName = CurrentTbl39Infraordo.PorName;
                            .Updater = Environment.UserName;
                            .UpdaterDate = DateTime.Now;
                            .Memo = CurrentTbl39Infraordo.Memo;                                                              
                                                                            
                        }
                    }
                    else
                    {
                        _tbl39InfraordosRepository.Add(new Tbl39Infraordo     //add new
                        {   
  OrdoID = CurrentTbl39Infraordo.OrdoID,              
                            InfraordoName = CurrentTbl39Infraordo.InfraordoName,              
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
                        //OrdoID may be not 0
                        if (CurrentTbl39Infraordo.OrdoID == 0)
                        {
                            WpfMessageBox.Show(CultRes.StringsRes.RequiredGenealogyConnect, CultRes.StringsRes.RequiredInput,
                                MessageBoxButton.OK, MessageBoxImage.Information);
                            return;
                        }

                        //check if dataset with Name and vb-name already exist
                        var dataset = new ObservableCollection<Tbl39Infraordo>
                        (from a in _tbl39InfraordosRepository.GetAll()
                         where
                         a.InfraordoName.Trim() == CurrentTbl39Infraordo.InfraordoName.Trim() &&                
                         a.OrdoID == CurrentTbl39Infraordo.OrdoID
                         select a);

                        if (dataset.Count != 0 && CurrentTbl39Infraordo.InfraordoID == 0)  //dataset exist
                        {
                              WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTbl39Infraordo.InfraordoName,
                              MessageBoxButton.OK, MessageBoxImage.Information);
                        }

                        if (dataset.Count == 0 && CurrentTbl39Infraordo.InfraordoID== 0 ||
                            dataset.Count != 0 && CurrentTbl39Infraordo.InfraordoID != 0  ||
                            dataset.Count == 0 && CurrentTbl39Infraordo.InfraordoID != 0) //new dataset and update
                        {
                            if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTbl39Infraordo.InfraordoName,
                                    MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)                            
                                return;                            
                            {
                                _tbl39InfraordosRepository.Save();

                                WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess, CurrentTbl39Infraordo.InfraordoName,
                                    MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                        }  
                 

                        if (SearchInfraordoName == null && CurrentTbl39Infraordo.InfraordoID == 0)  //new Dataset                        
                            Tbl39InfraordosList = _allListVm.GetValueTbl39InfraordosList();  //last Dataset
                        if (SearchInfraordoName == null && CurrentTbl39Infraordo.InfraordoID != 0)   //update
                            Tbl39InfraordosList = _allListVm.GetValueTbl39InfraordosList(CurrentTbl39Infraordo.InfraordoID);
                        if (SearchInfraordoName != null && CurrentTbl39Infraordo.InfraordoID == 0)  //new Dataset
                            Tbl39InfraordosList = _allListVm.GetValueTbl39InfraordosList();  //last Dataset
                        if (SearchInfraordoName != null && CurrentTbl39Infraordo.InfraordoID != 0)   //update
                            Tbl39InfraordosList = _allListVm.GetValueTbl39InfraordosList(CurrentTbl39Infraordo.InfraordoID);     
                                                                 
                        InfraordosView = CollectionViewSource.GetDefaultView(Tbl39InfraordosList);
                        InfraordosView.Refresh();           
                                                                                          
                   }
                }
            }
            catch (DbEntityValidationException ex)
            {
                //  WpfMessageBox.Show(CultRes.StringsRes.Error, ex.Message, MessageBoxButton.OK, MessageBoxImage.Error);
                //Retrieve the Error messages as a list of strings
                var errorMessages = ex.EntityValidationErrors
                    .SelectMany(x => x.ValidationErrors)
                    .Select(x => x.ErrorMessage);
                //Join the list to a single string
                var fullErrorMessage = string.Join("; ", errorMessages);
                //Combine the original exeption message with the new one.
                var exeptionMessage = string.Concat(ex.Message, CultRes.StringsRes.ValidationErrors, fullErrorMessage);
                //throw a new DbEntityValidationException
                throw new DbEntityValidationException(exeptionMessage, ex.EntityValidationErrors);
            }
        }

        #endregion "Public Commands"  
        

 //    Part 6    

      

 //    Part 7    

      

 //    Part 8    

           
        #region "Public Commands Connect ==> Tbl90RefAuthor"
        private RelayCommand _getRefAuthorByNameOrIdCommand;    
    
        public new ICommand GetRefAuthorByNameOrIdCommand  
    
        {
            get { return _getRefAuthorByNameOrIdCommand ?? (_getRefAuthorByNameOrIdCommand = new RelayCommand(delegate { GetRefAuthorByNameOrId(null); })); }
        }

        public new void GetRefAuthorByNameOrId(object o)
        {   
    
            int id;
            if (int.TryParse(SearchRefAuthorName, out id))
                Tbl90RefAuthorsList = new ObservableCollection<Tbl90Reference> { _tbl90ReferencesRepository.Get(id) };
            else
                Tbl90RefAuthorsList = _allListVm.GetValueTbl90RefAuthorsList(SearchRefAuthorName);     
     
            RefAuthorsView = CollectionViewSource.GetDefaultView(Tbl90RefAuthorsList);
            RefAuthorsView.Refresh();
        }
        //------------------------------------------------------------------------------                
           
        private RelayCommand _addRefAuthorCommand;         
    
        public new ICommand AddRefAuthorCommand      
    
        {
            get { return _addRefAuthorCommand ?? (_addRefAuthorCommand = new RelayCommand(delegate { AddRefAuthor(null); })); }
        }

        public new void AddRefAuthor(object o)
        {
            Tbl90RefAuthorsList = new ObservableCollection<Tbl90Reference>();  
    
            Tbl90RefAuthorsList.Insert(0, new Tbl90Reference { Info = CultRes.StringsRes.DatasetNew }); 

           Tbl33OrdosAllList = _allListVm.GetValueTbl33OrdosAllList();
           Tbl90AuthorsAllList = _allListVm.GetValueTbl90AuthorsAllList();    
    

            RefAuthorsView = CollectionViewSource.GetDefaultView(Tbl90RefAuthorsList);
            RefAuthorsView.Refresh();
        }
        //---------------------------------------------------------------------------------------  
           
        private RelayCommand _copyRefAuthorCommand;            
    
        public new ICommand CopyRefAuthorCommand       
         
        {
            get { return _copyRefAuthorCommand ?? (_copyRefAuthorCommand = new RelayCommand(delegate { CopyRefAuthor(null); })); }
        }

        public new void CopyRefAuthor(object o)
        {
            Tbl90RefAuthorsList = new ObservableCollection<Tbl90Reference>();

            var refAuthor = _tbl90ReferencesRepository.Get(CurrentTbl90RefAuthor.ReferenceID);

            Tbl90RefAuthorsList.Insert(0, new Tbl90Reference
            {                 
OrdoID = refAuthor.OrdoID,              
                Info = CultRes.StringsRes.DatasetNew,
                Valid = refAuthor.Valid,
                ValidYear = refAuthor.ValidYear,
                Memo = refAuthor.Memo          
        
            });

            RefAuthorsView = CollectionViewSource.GetDefaultView(Tbl90RefAuthorsList);
            RefAuthorsView.Refresh();
        }
        //---------------------------------------------------------------------------------------                  
           
        private RelayCommand _deleteRefAuthorCommand;             
    
        public new ICommand DeleteRefAuthorCommand          
         
        {
            get { return _deleteRefAuthorCommand ?? (_deleteRefAuthorCommand = new RelayCommand(delegate { DeleteRefAuthor(null); })); }
        }

        public new void DeleteRefAuthor(object o)
        {
            try
            {
                var refAuthor = _tbl90ReferencesRepository.Get(CurrentTbl90RefAuthor.ReferenceID);
                if (refAuthor != null)
                {
                    if (WpfMessageBox.Show(CultRes.StringsRes.DeleteQuestion1, CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl90RefAuthor.Info,
                         MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes) 
                     return;

                    _tbl90ReferencesRepository.Delete(refAuthor);
                    _tbl90ReferencesRepository.Save();

                    WpfMessageBox.Show(CultRes.StringsRes.DeleteSuccess, CurrentTbl90RefAuthor.Info, 
                        MessageBoxButton.OK, MessageBoxImage.Information);

                    if (SearchRefAuthorName == null)                    
                        GetConnectedTablesById(o); //refresh doubleClick                                       
                    else
                    {
                        Tbl90RefAuthorsList = _allListVm.GetValueTbl90RefAuthorsList(SearchRefAuthorName);  
    
                        RefAuthorsView = CollectionViewSource.GetDefaultView(Tbl90RefAuthorsList);
                        RefAuthorsView.Refresh();
                    }
                }
                else
                {
                    WpfMessageBox.Show(CultRes.StringsRes.Information, CultRes.StringsRes.DeleteCan + " " + CurrentTbl90RefAuthor.Info + " " + CultRes.StringsRes.DeleteCan1,
                         MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (DbEntityValidationException ex)
            {
                //Retrieve the Error messages as a list of strings
                var errorMessages = ex.EntityValidationErrors
                    .SelectMany(x => x.ValidationErrors)
                    .Select(x => x.ErrorMessage);
                //Join the list to a single string
                var fullErrorMessage = string.Join("; ", errorMessages);
                //Combine the original exeption message with the new one.
                var exeptionMessage = string.Concat(ex.Message, CultRes.StringsRes.ValidationErrors, fullErrorMessage);
                //throw a new DbEntityValidationException
                throw new DbEntityValidationException(exeptionMessage, ex.EntityValidationErrors);            
            }
        }
        //-------------------------------------------------------------------------------------------------    
           
        private RelayCommand _saveRefAuthorCommand;            
    
        public new ICommand SaveRefAuthorCommand           
         
        {
            get { return _saveRefAuthorCommand ?? (_saveRefAuthorCommand = new RelayCommand(delegate { SaveRefAuthor(null); })); }
        }

        public new void SaveRefAuthor(object o)
        {
            try
            {
                var refAuthor = _tbl90ReferencesRepository.Get(CurrentTbl90RefAuthor.ReferenceID);
                if (CurrentTbl90RefAuthor == null)               
                    WpfMessageBox.Show(CultRes.StringsRes.Warning, CultRes.StringsRes.DatasetNotExist, 
                         MessageBoxButton.OK, MessageBoxImage.Warning);               
                else
                {
                    if (CurrentTbl90RefAuthor.ReferenceID != 0 && CurrentTbl90RefAuthor.RefAuthorID != 0)
                    {
                        if (refAuthor != null)  //update
                        {   
         
                            refAuthor.OrdoID = CurrentTbl90RefAuthor.OrdoID;  
                            refAuthor.RefAuthorID = CurrentTbl90RefAuthor.RefAuthorID;
                            refAuthor.Valid = CurrentTbl90RefAuthor.Valid;
                            refAuthor.ValidYear = CurrentTbl90RefAuthor.ValidYear;
                            refAuthor.Info = CurrentTbl90RefAuthor.Info;
                            refAuthor.Updater = Environment.UserName;
                            refAuthor.UpdaterDate = DateTime.Now;
                            refAuthor.Memo = CurrentTbl90RefAuthor.Memo;  
         
                        }
                    }
                    else
                    {
                        _tbl90ReferencesRepository.Add(new Tbl90Reference   //add new
                        {   
OrdoID = CurrentTbl90RefAuthor.OrdoID,              
                            RefAuthorID = CurrentTbl90RefAuthor.RefAuthorID,
                            CountID = RandomHelper.Randomnumber(),
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
                        //OrdoID may be not 0
                        if (CurrentTbl90RefAuthor.OrdoID == 0 || CurrentTbl90RefAuthor.RefAuthorID == 0)
                        {
                            WpfMessageBox.Show(CultRes.StringsRes.RequiredGenealogyConnect, CultRes.StringsRes.RequiredInput,
                                MessageBoxButton.OK, MessageBoxImage.Information);
                            return;
                        }

                        //check if dataset with Name and vb-name already exist
                        var dataset = new ObservableCollection<Tbl90Reference>
                        (from a in _tbl90ReferencesRepository.GetAll()
                         where
                         a.Info.Trim() == CurrentTbl90RefAuthor.Info.Trim() &&                
                         a.OrdoID == CurrentTbl90RefAuthor.OrdoID &&
                         a.RefAuthorID == CurrentTbl90RefAuthor.RefAuthorID
                         select a);

                        if (dataset.Count != 0 && CurrentTbl90RefAuthor.ReferenceID == 0)  //dataset exist
                        {
                              WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTbl90RefAuthor.Info,
                              MessageBoxButton.OK, MessageBoxImage.Information);
                        }

                        if (dataset.Count == 0 && CurrentTbl90RefAuthor.ReferenceID == 0 ||
                            dataset.Count != 0 && CurrentTbl90RefAuthor.ReferenceID != 0  ||
                            dataset.Count == 0 && CurrentTbl90RefAuthor.ReferenceID != 0) //new dataset and update
                        {
                            if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTbl90RefAuthor.Info,
                                    MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)                            
                                return;                            
                            {
                              _tbl90ReferencesRepository.Save();
                         
                                WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess, CurrentTbl90RefAuthor.Info,
                                    MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                       }  
      
                        if (SearchRefAuthorName == null && CurrentTbl90RefAuthor.ReferenceID == 0)  //new Dataset                        
                            GetConnectedTablesById(o); //refresh doubleClick                                          
                        if (SearchRefAuthorName == null && CurrentTbl90RefAuthor.ReferenceID != 0)   //update
                            Tbl90RefAuthorsList = _allListVm.GetValueTbl90RefAuthorsList(CurrentTbl90RefAuthor.ReferenceID);
                        if (SearchRefAuthorName != null && CurrentTbl90RefAuthor.ReferenceID == 0)  //new Dataset
                            GetConnectedTablesById(o); //refresh doubleClick                                          
                        if (SearchRefAuthorName != null && CurrentTbl90RefAuthor.ReferenceID != 0)   //update
                            Tbl90RefAuthorsList = _allListVm.GetValueTbl90RefAuthorsList(CurrentTbl90RefAuthor.ReferenceID);

                        RefAuthorsView = CollectionViewSource.GetDefaultView(Tbl90RefAuthorsList);
                        RefAuthorsView.Refresh();     
        
                    }
                }
            }
            catch (DbEntityValidationException ex)
            {
                //Retrieve the Error messages as a list of strings
                var errorMessages = ex.EntityValidationErrors
                    .SelectMany(x => x.ValidationErrors)
                    .Select(x => x.ErrorMessage);
                //Join the list to a single string
                var fullErrorMessage = string.Join("; ", errorMessages);
                //Combine the original exeption message with the new one.
                var exeptionMessage = string.Concat(ex.Message, CultRes.StringsRes.ValidationErrors, fullErrorMessage);
                //throw a new DbEntityValidationException
                throw new DbEntityValidationException(exeptionMessage, ex.EntityValidationErrors);            
            }
        }

        #endregion "Public Commands"  
           
        #region "Public Commands Connect ==> Tbl90RefSource"

        private RelayCommand _getRefSourceByNameOrIdCommand;   
    
        public new ICommand GetRefSourceByNameOrIdCommand  
    
        {
            get { return _getRefSourceByNameOrIdCommand ?? (_getRefSourceByNameOrIdCommand = new RelayCommand(delegate { GetRefSourceByNameOrId(null); })); }
        }

        public new void GetRefSourceByNameOrId(object o)
        {   
    
            int id;
            if (int.TryParse(SearchRefSourceName, out id))
                Tbl90RefSourcesList = new ObservableCollection<Tbl90Reference> { _tbl90ReferencesRepository.Get(id) };
            else
                Tbl90RefSourcesList = _allListVm.GetValueTbl90RefSourcesList(SearchRefSourceName);     
     
            RefSourcesView = CollectionViewSource.GetDefaultView(Tbl90RefSourcesList);
            RefSourcesView.Refresh();
        }
        //------------------------------------------------------------------------------                
           
        private RelayCommand _addRefSourceCommand;         
    
        public new ICommand AddRefSourceCommand      
    
        {
            get { return _addRefSourceCommand ?? (_addRefSourceCommand = new RelayCommand(delegate { AddRefSource(null); })); }
        }

        public new void AddRefSource(object o)
        {
            Tbl90RefSourcesList = new ObservableCollection<Tbl90Reference>();  
    
            Tbl90RefSourcesList.Insert(0, new Tbl90Reference { Info = CultRes.StringsRes.DatasetNew });    

            Tbl33OrdosAllList = _allListVm.GetValueTbl33OrdosAllList();
            Tbl90SourcesAllList = _allListVm.GetValueTbl90SourcesAllList();     
    

            RefSourcesView = CollectionViewSource.GetDefaultView(Tbl90RefSourcesList);
            RefSourcesView.Refresh();
        }
        //---------------------------------------------------------------------------------------  
           
        private RelayCommand _copyRefSourceCommand;            
    
        public new ICommand CopyRefSourceCommand       
         
        {
            get { return _copyRefSourceCommand ?? (_copyRefSourceCommand = new RelayCommand(delegate { CopyRefSource(null); })); }
        }

        public new void CopyRefSource(object o)
        {
            Tbl90RefSourcesList = new ObservableCollection<Tbl90Reference>();

            var refSource = _tbl90ReferencesRepository.Get(CurrentTbl90RefSource.ReferenceID);

            Tbl90RefSourcesList.Insert(0, new Tbl90Reference
            {                 
OrdoID = refSource.OrdoID,              
                Info = CultRes.StringsRes.DatasetNew,
                Valid = refSource.Valid,
                ValidYear = refSource.ValidYear,
                Memo = refSource.Memo          
        
            });

            RefSourcesView = CollectionViewSource.GetDefaultView(Tbl90RefSourcesList);
            RefSourcesView.Refresh();
        }
        //---------------------------------------------------------------------------------------                  
           
        private RelayCommand _deleteRefSourceCommand;             
    
        public new ICommand DeleteRefSourceCommand          
         
        {
            get { return _deleteRefSourceCommand ?? (_deleteRefSourceCommand = new RelayCommand(delegate { DeleteRefSource(null); })); }
        }

        public new void DeleteRefSource(object o)
        {
            try
            {
                var refSource = _tbl90ReferencesRepository.Get(CurrentTbl90RefSource.ReferenceID);
                if (refSource != null)
                {
                    if (WpfMessageBox.Show(CultRes.StringsRes.DeleteQuestion1, CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl90RefSource.Info,
                         MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes) 
                     return;

                    _tbl90ReferencesRepository.Delete(refSource);
                    _tbl90ReferencesRepository.Save();

                    WpfMessageBox.Show(CultRes.StringsRes.DeleteSuccess, CurrentTbl90RefSource.Info, 
                        MessageBoxButton.OK, MessageBoxImage.Information);

                    if (SearchRefSourceName == null)                   
                        GetConnectedTablesById(o); //refresh doubleClick                                       
                    else
                    {
                        Tbl90RefSourcesList = _allListVm.GetValueTbl90RefSourcesList(SearchRefSourceName);   
    
                        RefSourcesView = CollectionViewSource.GetDefaultView(Tbl90RefSourcesList);
                        RefSourcesView.Refresh();
                    }
                }
                else
                {
                    WpfMessageBox.Show(CultRes.StringsRes.Information, CultRes.StringsRes.DeleteCan + " " + CurrentTbl90RefSource.Info + " " + CultRes.StringsRes.DeleteCan1,
                         MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (DbEntityValidationException ex)
            {
                //Retrieve the Error messages as a list of strings
                var errorMessages = ex.EntityValidationErrors
                    .SelectMany(x => x.ValidationErrors)
                    .Select(x => x.ErrorMessage);
                //Join the list to a single string
                var fullErrorMessage = string.Join("; ", errorMessages);
                //Combine the original exeption message with the new one.
                var exeptionMessage = string.Concat(ex.Message, CultRes.StringsRes.ValidationErrors, fullErrorMessage);
                //throw a new DbEntityValidationException
                throw new DbEntityValidationException(exeptionMessage, ex.EntityValidationErrors);
            }
        }
        
           
        private RelayCommand _saveRefSourceCommand;            
    
        public new ICommand SaveRefSourceCommand           
         
        {
            get { return _saveRefSourceCommand ?? (_saveRefSourceCommand = new RelayCommand(delegate { SaveRefSource(null); })); }
        }

        public new void SaveRefSource(object o)
        {
            try
            {
                var refSource = _tbl90ReferencesRepository.Get(CurrentTbl90RefSource.ReferenceID);
                if (CurrentTbl90RefSource == null)                
                    WpfMessageBox.Show(CultRes.StringsRes.Warning, CultRes.StringsRes.DatasetNotExist, MessageBoxButton.OK, MessageBoxImage.Warning);                
                else
                {
                    if (CurrentTbl90RefSource.ReferenceID != 0)
                    {
                        if (refSource != null)  //update
                        {   
         
                            refSource.OrdoID = CurrentTbl90RefSource.OrdoID;            
                            refSource.RefSourceID = CurrentTbl90RefSource.RefSourceID;
                            refSource.Valid = CurrentTbl90RefSource.Valid;
                            refSource.ValidYear = CurrentTbl90RefSource.ValidYear;
                            refSource.Info = CurrentTbl90RefSource.Info;
                            refSource.Updater = Environment.UserName;
                            refSource.UpdaterDate = DateTime.Now;
                            refSource.Memo = CurrentTbl90RefSource.Memo;  
         
                        }
                    }
                    else
                    {
                        _tbl90ReferencesRepository.Add(new Tbl90Reference    //add new
                        {   
OrdoID = CurrentTbl90RefSource.OrdoID,              
                            RefSourceID = CurrentTbl90RefSource.RefSourceID,
                            CountID = RandomHelper.Randomnumber(),
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
                        //OrdoID may be not 0
                        if (CurrentTbl90RefSource.OrdoID == 0 || CurrentTbl90RefSource.RefSourceID == 0)
                        {
                            WpfMessageBox.Show(CultRes.StringsRes.RequiredGenealogyConnect, CultRes.StringsRes.RequiredInput,            
                                MessageBoxButton.OK, MessageBoxImage.Information);
                            return;
                        }

                        //check if dataset with Name and vb-name already exist
                        var dataset = new ObservableCollection<Tbl90Reference>
                        (from a in _tbl90ReferencesRepository.GetAll()
                         where
                         a.Info.Trim() == CurrentTbl90RefSource.Info.Trim() &&                
                         a.OrdoID == CurrentTbl90RefSource.OrdoID &&
                         a.RefSourceID == CurrentTbl90RefSource.RefSourceID
                         select a);

                        if (dataset.Count != 0 && CurrentTbl90RefSource.ReferenceID == 0)  //dataset exist
                        {
                              WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTbl90RefSource.Info,
                              MessageBoxButton.OK, MessageBoxImage.Information);
                        }

                        if (dataset.Count == 0 && CurrentTbl90RefSource.ReferenceID == 0 ||
                            dataset.Count != 0 && CurrentTbl90RefSource.ReferenceID != 0  ||
                            dataset.Count == 0 && CurrentTbl90RefSource.ReferenceID != 0) //new dataset and update
                        {
                            if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTbl90RefSource.Info,
                                    MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)                            
                                return;                            
                            {
                                 _tbl90ReferencesRepository.Save();

                                WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess, CurrentTbl90RefSource.Info,
                                    MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                        }      
         
                        if (SearchRefSourceName == null && CurrentTbl90RefSource.ReferenceID == 0)  //new Dataset                        
                            Tbl90RefSourcesList = _allListVm.GetValueTbl90RefSourcesList();  //last Dataset
                        if (SearchRefSourceName == null && CurrentTbl90RefSource.ReferenceID != 0)   //update
                            Tbl90RefSourcesList = _allListVm.GetValueTbl90RefSourcesList(CurrentTbl90RefSource.ReferenceID);
                        if (SearchRefSourceName != null && CurrentTbl90RefSource.ReferenceID == 0)  //new Dataset
                            Tbl90RefSourcesList = _allListVm.GetValueTbl90RefSourcesList();  //last Dataset
                        if (SearchRefSourceName != null && CurrentTbl90RefSource.ReferenceID != 0)   //update
                            Tbl90RefSourcesList = _allListVm.GetValueTbl90RefSourcesList(CurrentTbl90RefSource.ReferenceID);

                        RefSourcesView = CollectionViewSource.GetDefaultView(Tbl90RefSourcesList);
                        RefSourcesView.Refresh();     
         
                    }
                }
            }
            catch (DbEntityValidationException ex)
            {
                //Retrieve the Error messages as a list of strings
                var errorMessages = ex.EntityValidationErrors
                    .SelectMany(x => x.ValidationErrors)
                    .Select(x => x.ErrorMessage);
                //Join the list to a single string
                var fullErrorMessage = string.Join("; ", errorMessages);
                //Combine the original exeption message with the new one.
                var exeptionMessage = string.Concat(ex.Message, CultRes.StringsRes.ValidationErrors, fullErrorMessage);
                //throw a new DbEntityValidationException
                throw new DbEntityValidationException(exeptionMessage, ex.EntityValidationErrors);
            }
        }

        #endregion "Public Commands"  
           
        #region "Public Commands Connect ==> Tbl90RefExpert"

        private RelayCommand _getRefExpertByNameOrIdCommand;   
    
        public new ICommand GetRefExpertByNameOrIdCommand  
    
        {
            get { return _getRefExpertByNameOrIdCommand ?? (_getRefExpertByNameOrIdCommand = new RelayCommand(delegate { GetRefExpertByNameOrId(null); })); }
        }

        public new void GetRefExpertByNameOrId(object o)
        {   
    
            int id;
            if (int.TryParse(SearchRefExpertName, out id))
                Tbl90RefExpertsList = new ObservableCollection<Tbl90Reference> { _tbl90ReferencesRepository.Get(id) };
            else
                Tbl90RefExpertsList = _allListVm.GetValueTbl90RefExpertsList(SearchRefExpertName);      
     
            RefExpertsView = CollectionViewSource.GetDefaultView(Tbl90RefExpertsList);
            RefExpertsView.Refresh();
        }
        //------------------------------------------------------------------------------                
           
        private RelayCommand _addRefExpertCommand;         
    
        public new ICommand AddRefExpertCommand      
    
        {
            get { return _addRefExpertCommand ?? (_addRefExpertCommand = new RelayCommand(delegate { AddRefExpert(null); })); }
        }

        public new void AddRefExpert(object o)
        {
            Tbl90RefExpertsList = new ObservableCollection<Tbl90Reference>();      
    
            Tbl90RefExpertsList.Insert(0, new Tbl90Reference { Info = CultRes.StringsRes.DatasetNew });   

            Tbl33OrdosAllList = _allListVm.GetValueTbl33OrdosAllList();
            Tbl90ExpertsAllList = _allListVm.GetValueTbl90ExpertsAllList();        
    

            RefExpertsView = CollectionViewSource.GetDefaultView(Tbl90RefExpertsList);
            RefExpertsView.Refresh();
        }
        //---------------------------------------------------------------------------------------  
           
        private RelayCommand _copyRefExpertCommand;            
    
        public new ICommand CopyRefExpertCommand       
         
        {
            get { return _copyRefExpertCommand ?? (_copyRefExpertCommand = new RelayCommand(delegate { CopyRefExpert(null); })); }
        }

        public new void CopyRefExpert(object o)
        {
            Tbl90RefExpertsList = new ObservableCollection<Tbl90Reference>();

            var refExpert = _tbl90ReferencesRepository.Get(CurrentTbl90RefExpert.ReferenceID);

            Tbl90RefExpertsList.Insert(0, new Tbl90Reference
            {                 
OrdoID = refExpert.OrdoID,              
                Info = CultRes.StringsRes.DatasetNew,
                Valid = refExpert.Valid,
                ValidYear = refExpert.ValidYear,
                Memo = refExpert.Memo          
        
            });

            RefExpertsView = CollectionViewSource.GetDefaultView(Tbl90RefExpertsList);
            RefExpertsView.Refresh();
        }
        //---------------------------------------------------------------------------------------                  
           
        private RelayCommand _deleteRefExpertCommand;             
    
        public new ICommand DeleteRefExpertCommand          
         
        {
            get { return _deleteRefExpertCommand ?? (_deleteRefExpertCommand = new RelayCommand(delegate { DeleteRefExpert(null); })); }
        }

        public new void DeleteRefExpert(object o)
        {
            try
            {
                var refExpert = _tbl90ReferencesRepository.Get(CurrentTbl90RefExpert.ReferenceID);
                if (refExpert != null)
                {
                    if (WpfMessageBox.Show(CultRes.StringsRes.DeleteQuestion1, CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl90RefExpert.Info,
                         MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes) 
                    return;

                    _tbl90ReferencesRepository.Delete(refExpert);
                    _tbl90ReferencesRepository.Save();

                    WpfMessageBox.Show(CultRes.StringsRes.DeleteSuccess, CurrentTbl90RefExpert.Info, 
                    MessageBoxButton.OK, MessageBoxImage.Information);

                    if (SearchRefExpertName == null)                   
                        GetConnectedTablesById(o); //refresh doubleClick                                       
                    else
                    {
                        Tbl90RefExpertsList = _allListVm.GetValueTbl90RefExpertsList(SearchRefExpertName); 
    
                        RefExpertsView = CollectionViewSource.GetDefaultView(Tbl90RefExpertsList);
                        RefExpertsView.Refresh();
                    }
                }
                else
                {
                    WpfMessageBox.Show(CultRes.StringsRes.Information, CultRes.StringsRes.DeleteCan + " " + CurrentTbl90RefExpert.Info + " " + CultRes.StringsRes.DeleteCan1,
                         MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (DbEntityValidationException ex)
            {
                //Retrieve the Error messages as a list of strings
                var errorMessages = ex.EntityValidationErrors
                    .SelectMany(x => x.ValidationErrors)
                    .Select(x => x.ErrorMessage);
                //Join the list to a single string
                var fullErrorMessage = string.Join("; ", errorMessages);
                //Combine the original exeption message with the new one.
                var exeptionMessage = string.Concat(ex.Message, CultRes.StringsRes.ValidationErrors, fullErrorMessage);
                //throw a new DbEntityValidationException
                throw new DbEntityValidationException(exeptionMessage, ex.EntityValidationErrors);
            }
        }
          
           
        private RelayCommand _saveRefExpertCommand;            
    
        public new ICommand SaveRefExpertCommand           
         
        {
            get { return _saveRefExpertCommand ?? (_saveRefExpertCommand = new RelayCommand(delegate { SaveRefExpert(null); })); }
        }

        public new void SaveRefExpert(object o)
        {
            try
            {
                var refExpert = _tbl90ReferencesRepository.Get(CurrentTbl90RefExpert.ReferenceID);
                if (CurrentTbl90RefExpert == null)               
                    WpfMessageBox.Show(CultRes.StringsRes.Warning, CultRes.StringsRes.DatasetNotExist, MessageBoxButton.OK, MessageBoxImage.Warning);               
                else
                {
                    if (CurrentTbl90RefExpert.ReferenceID != 0 && CurrentTbl90RefExpert.RefExpertID != 0)
                    {
                        if (refExpert != null)	//update
                        {   
      
                            refExpert.OrdoID = CurrentTbl90RefExpert.OrdoID;           
                            refExpert.RefExpertID = CurrentTbl90RefExpert.RefExpertID;
                            refExpert.Valid = CurrentTbl90RefExpert.Valid;
                            refExpert.ValidYear = CurrentTbl90RefExpert.ValidYear;
                            refExpert.Info = CurrentTbl90RefExpert.Info;
                            refExpert.Updater = Environment.UserName;
                            refExpert.UpdaterDate = DateTime.Now;
                            refExpert.Memo = CurrentTbl90RefExpert.Memo;     
         
                        }
                    }
                    else
                    {
                        _tbl90ReferencesRepository.Add(new Tbl90Reference  //add new
                        {   
OrdoID = CurrentTbl90RefExpert.OrdoID,              
                            RefExpertID= CurrentTbl90RefExpert.RefExpertID,
                            CountID = RandomHelper.Randomnumber(),
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
                        //OrdoID may be not 0
                        if (CurrentTbl90RefExpert.OrdoID == 0 || CurrentTbl90RefExpert.RefExpertID == 0)   
                        {
                            WpfMessageBox.Show(CultRes.StringsRes.RequiredGenealogyConnect, CultRes.StringsRes.RequiredInput,          
                                MessageBoxButton.OK, MessageBoxImage.Information);
                            return;
                        }
                        //check if dataset with Name and vb-name already exist
                        var dataset = new ObservableCollection<Tbl90Reference>
                        (from a in _tbl90ReferencesRepository.GetAll()
                         where
                         a.Info.Trim() == CurrentTbl90RefExpert.Info.Trim() &&                
                         a.OrdoID == CurrentTbl90RefExpert.OrdoID &&
                         a.RefExpertID == CurrentTbl90RefExpert.RefExpertID
                         select a);

                        if (dataset.Count != 0 && CurrentTbl90RefExpert.ReferenceID == 0)  //dataset exist
                        {
                              WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTbl90RefExpert.Info,
                              MessageBoxButton.OK, MessageBoxImage.Information);
                        }

                        if (dataset.Count == 0 && CurrentTbl90RefExpert.ReferenceID == 0 ||
                            dataset.Count != 0 && CurrentTbl90RefExpert.ReferenceID != 0  ||
                            dataset.Count == 0 && CurrentTbl90RefExpert.ReferenceID != 0) //new dataset and update
                        {
                            if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTbl90RefExpert.Info,
                                    MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)                            
                                return;                            
                            {
                                    _tbl90ReferencesRepository.Save();

                                WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess, CurrentTbl90RefExpert.Info,
                                    MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                        }      
         
                        if (SearchRefExpertName == null && CurrentTbl90RefExpert.ReferenceID == 0)  //new Dataset                        
                            Tbl90RefExpertsList = _allListVm.GetValueTbl90RefExpertsList();  //last Dataset
                        if (SearchRefExpertName == null && CurrentTbl90RefExpert.ReferenceID != 0)   //update
                            Tbl90RefExpertsList = _allListVm.GetValueTbl90RefExpertsList(CurrentTbl90RefExpert.ReferenceID);
                        if (SearchRefExpertName != null && CurrentTbl90RefExpert.ReferenceID == 0)  //new Dataset
                            Tbl90RefExpertsList = _allListVm.GetValueTbl90RefExpertsList();  //last Dataset
                        if (SearchRefExpertName != null && CurrentTbl90RefExpert.ReferenceID != 0)   //update
                            Tbl90RefExpertsList = _allListVm.GetValueTbl90RefExpertsList(CurrentTbl90RefExpert.ReferenceID);

                        RefExpertsView = CollectionViewSource.GetDefaultView(Tbl90RefExpertsList);
                        RefExpertsView.Refresh();      
        
                    }
                }
            }
            catch (DbEntityValidationException ex)
            {
                //Retrieve the Error messages as a list of strings
                var errorMessages = ex.EntityValidationErrors
                    .SelectMany(x => x.ValidationErrors)
                    .Select(x => x.ErrorMessage);
                //Join the list to a single string
                var fullErrorMessage = string.Join("; ", errorMessages);
                //Combine the original exeption message with the new one.
                var exeptionMessage = string.Concat(ex.Message, CultRes.StringsRes.ValidationErrors, fullErrorMessage);
                //throw a new DbEntityValidationException
                throw new DbEntityValidationException(exeptionMessage, ex.EntityValidationErrors);
            }
        }

        #endregion "Public Commands"  
           
        #region "Public Commands Connect ==> Tbl93Comment"

        private RelayCommand _getCommentByNameOrIdCommand;   
    
        public new ICommand GetCommentByNameOrIdCommand  
    
        {
            get { return _getCommentByNameOrIdCommand ?? (_getCommentByNameOrIdCommand = new RelayCommand(delegate { GetCommentByNameOrId(null); })); }
        }

        public new void GetCommentByNameOrId(object o)
        {   
    
            int id;
            if (int.TryParse(SearchCommentInfo, out id))
                Tbl93CommentsList = new ObservableCollection<Tbl93Comment> { _tbl93CommentsRepository.Get(id) };
            else
                Tbl93CommentsList = _allListVm.GetValueTbl93CommentsList(SearchCommentInfo);    
     
            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            CommentsView.Refresh();
        }
        //------------------------------------------------------------------------------                
           
        private RelayCommand _addCommentCommand;         
    
        public new ICommand AddCommentCommand      
    
        {
            get { return _addCommentCommand ?? (_addCommentCommand = new RelayCommand(delegate { AddComment(null); })); }
        }

        public new void AddComment(object o)
        {
                Tbl93CommentsList = new ObservableCollection<Tbl93Comment>();  
    
            Tbl93CommentsList.Insert(0, new Tbl93Comment { Info = CultRes.StringsRes.DatasetNew });    

            Tbl33OrdosAllList = _allListVm.GetValueTbl33OrdosAllList();      
    

            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            CommentsView.Refresh();
        }
        //---------------------------------------------------------------------------------------  
           
        private RelayCommand _copyCommentCommand;            
    
        public new ICommand CopyCommentCommand       
         
        {
            get { return _copyCommentCommand ?? (_copyCommentCommand = new RelayCommand(delegate { CopyComment(null); })); }
        }

        public new void CopyComment(object o)
        {
            Tbl93CommentsList = new ObservableCollection<Tbl93Comment>();

            var comment = _tbl93CommentsRepository.Get(CurrentTbl93Comment.CommentID);

            Tbl93CommentsList.Insert(0, new Tbl93Comment
            {                 
OrdoID = comment.OrdoID,              
                Info = CultRes.StringsRes.DatasetNew,
                Valid = comment.Valid,
                ValidYear = comment.ValidYear,
                Memo = comment.Memo          
        
            });

            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            CommentsView.Refresh();
        }
        //---------------------------------------------------------------------------------------                  
           
        private RelayCommand _deleteCommentCommand;             
    
        public new ICommand DeleteCommentCommand          
         
        {
            get { return _deleteCommentCommand ?? (_deleteCommentCommand = new RelayCommand(delegate { DeleteComment(null); })); }
        }

        private void DeleteComment(object o)
        {
            try
            {
                var comment = _tbl93CommentsRepository.Get(CurrentTbl93Comment.CommentID);
                if (comment != null)
                {
                    if (WpfMessageBox.Show(CultRes.StringsRes.DeleteQuestion1, CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl93Comment.CommentID,
                         MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes) 
                     return;

                    _tbl93CommentsRepository.Delete(comment);
                    _tbl93CommentsRepository.Save();

                    WpfMessageBox.Show(CultRes.StringsRes.DeleteSuccess, CurrentTbl93Comment.CommentID.ToString(), 
                        MessageBoxButton.OK, MessageBoxImage.Information);

                    if (SearchCommentInfo == null)                    
                        GetConnectedTablesById(o); //refresh doubleClick                                       
                    else
                    {
                        Tbl93CommentsList = _allListVm.GetValueTbl93CommentsList(SearchCommentInfo);    
    

                        CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
                        CommentsView.Refresh();
                    }
                }
                else
                {
                    WpfMessageBox.Show(CultRes.StringsRes.Information, CultRes.StringsRes.DeleteCan + " " + CurrentTbl93Comment.CommentID + " " + CultRes.StringsRes.DeleteCan1,
                         MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (DbEntityValidationException ex)
            {
                //Retrieve the Error messages as a list of strings
                var errorMessages = ex.EntityValidationErrors
                    .SelectMany(x => x.ValidationErrors)
                    .Select(x => x.ErrorMessage);
                //Join the list to a single string
                var fullErrorMessage = string.Join("; ", errorMessages);
                //Combine the original exeption message with the new one.
                var exeptionMessage = string.Concat(ex.Message, CultRes.StringsRes.ValidationErrors, fullErrorMessage);
                //throw a new DbEntityValidationException
                throw new DbEntityValidationException(exeptionMessage, ex.EntityValidationErrors);
            }
        }
        //-------------------------------------------------------------------------------------------------    
           
        private RelayCommand _saveCommentCommand;            
    
        public new ICommand SaveCommentCommand           
         
        {
            get { return _saveCommentCommand ?? (_saveCommentCommand = new RelayCommand(delegate { SaveComment(null); })); }
        }

        private void SaveComment(object o)
        {
            try
            {
                var comment = _tbl93CommentsRepository.Get(CurrentTbl93Comment.CommentID);
                if (CurrentTbl93Comment == null)                
                    WpfMessageBox.Show(CultRes.StringsRes.Warning, CultRes.StringsRes.DatasetNotExist, MessageBoxButton.OK, MessageBoxImage.Warning);               
                else
                {
                    if (CurrentTbl93Comment.CommentID != 0)
                    {
                        if (comment != null)  //update
                        {   
      
                            comment.OrdoID = CurrentTbl93Comment.OrdoID;            
                            comment.Valid = CurrentTbl93Comment.Valid;
                            comment.ValidYear = CurrentTbl93Comment.ValidYear;
                            comment.Info = CurrentTbl93Comment.Info;
                            comment.Updater = Environment.UserName;
                            comment.UpdaterDate = DateTime.Now;
                            comment.Memo = CurrentTbl93Comment.Memo;     
         
                        }
                    }
                    else
                    {
                        _tbl93CommentsRepository.Add(new Tbl93Comment  //add new
                        {   
OrdoID = CurrentTbl93Comment.OrdoID,              
                            CountID = RandomHelper.Randomnumber(),
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
                        //OrdoID may be not 0
                        if (CurrentTbl93Comment.OrdoID == 0)
                        {
                            WpfMessageBox.Show(CultRes.StringsRes.RequiredGenealogyConnect, CultRes.StringsRes.RequiredInput,
                                MessageBoxButton.OK, MessageBoxImage.Information);
                            return;
                        }

                        //check if dataset with Name and vb-name already exist
                        var dataset = new ObservableCollection<Tbl93Comment>
                        (from a in _tbl93CommentsRepository.GetAll()
                         where
                         a.Info.Trim() == CurrentTbl93Comment.Info.Trim() &&                
                         a.OrdoID == CurrentTbl93Comment.OrdoID
                         select a);

                        if (dataset.Count != 0 && CurrentTbl93Comment.CommentID == 0)  //dataset exist
                        {
                              WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTbl93Comment.Info,
                              MessageBoxButton.OK, MessageBoxImage.Information);
                        }

                        if (dataset.Count == 0 && CurrentTbl93Comment.CommentID == 0 ||
                            dataset.Count != 0 && CurrentTbl93Comment.CommentID != 0  ||
                            dataset.Count == 0 && CurrentTbl93Comment.CommentID != 0) //new dataset and update
                        {
                            if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTbl93Comment.Info,
                                    MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)                            
                                return;                            
                            {
                        _tbl93CommentsRepository.Save();

                                WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess, CurrentTbl93Comment.Info,
                                    MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                        }   
         
                        if (SearchCommentInfo == null && CurrentTbl93Comment.CommentID == 0)  //new Dataset                        
                            Tbl93CommentsList = _allListVm.GetValueTbl93CommentsList();  //last Dataset
                        if (SearchCommentInfo == null && CurrentTbl93Comment.CommentID != 0)   //update
                            Tbl93CommentsList = _allListVm.GetValueTbl93CommentsList(CurrentTbl93Comment.CommentID);
                        if (SearchCommentInfo != null && CurrentTbl93Comment.CommentID == 0)  //new Dataset
                            Tbl93CommentsList = _allListVm.GetValueTbl93CommentsList();  //last Dataset
                        if (SearchCommentInfo != null && CurrentTbl93Comment.CommentID != 0)   //update
                            Tbl93CommentsList = _allListVm.GetValueTbl93CommentsList(CurrentTbl93Comment.CommentID);

                        CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
                        CommentsView.Refresh();      
       
                    }
                }
            }
            catch (DbEntityValidationException ex)
            {
                //  WpfMessageBox.Show(CultRes.StringsRes.Error, ex.Message, MessageBoxButton.OK, MessageBoxImage.Error);
                //Retrieve the Error messages as a list of strings
                var errorMessages = ex.EntityValidationErrors
                    .SelectMany(x => x.ValidationErrors)
                    .Select(x => x.ErrorMessage);
                //Join the list to a single string
                var fullErrorMessage = string.Join("; ", errorMessages);
                //Combine the original exeption message with the new one.
                var exeptionMessage = string.Concat(ex.Message, CultRes.StringsRes.ValidationErrors, fullErrorMessage);
                //throw a new DbEntityValidationException
                throw new DbEntityValidationException(exeptionMessage, ex.EntityValidationErrors);
            }
        }

        #endregion "Public Commands"  
      

 //    Part 9    

     
        #region "Public Commands Connected Tables by DoubleClick"                                         

        private RelayCommand _getConnectedTablesCommand;
        public new ICommand GetConnectedTablesCommand
        {
            get { return _getConnectedTablesCommand ?? (_getConnectedTablesCommand = new RelayCommand(delegate { GetConnectedTablesById(null); })); }
        }

        public  void GetConnectedTablesById(object o)
        {
            SelectedDetailThreeRefTabIndex = 1;  //change to Connect tab

            Tbl30LegiosList =  new ObservableCollection<Tbl30Legio>
                                                       (from x in _tbl30LegiosRepository.GetAll()
                                                       where x.LegioID == CurrentTbl33Ordo.LegioID
                                                       orderby x.LegioName
                                                       select x);

            LegiosView = CollectionViewSource.GetDefaultView(Tbl30LegiosList);
            LegiosView.Refresh();
            //-----------------------------------------------------------------------------------
            Tbl36SubordosList =  new ObservableCollection<Tbl36Subordo>
                                                       (from y in _tbl36SubordosRepository.GetAll()
                                                       where y.OrdoID == CurrentTbl33Ordo.OrdoID
                                                       orderby y.SubordoName
                                                       select y);


            SubordosView = CollectionViewSource.GetDefaultView(Tbl36SubordosList);
            SubordosView.Refresh();
            //-----------------------------------------------------------------------------------
            Tbl90RefAuthorsList =  new ObservableCollection<Tbl90Reference>
                                                          (from refAu in _tbl90ReferencesRepository.GetAll()
                                                          where refAu.OrdoID == CurrentTbl33Ordo.OrdoID
                                                          && refAu.RefExpertID == null
                                                          && refAu.RefSourceID == null
                                                          orderby refAu.Tbl90RefAuthors.RefAuthorName, refAu.Tbl90RefAuthors.BookName, refAu.Tbl90RefAuthors.Page1
                                                          select refAu);

            RefAuthorsView = CollectionViewSource.GetDefaultView(Tbl90RefAuthorsList);
            RefAuthorsView.Refresh();
            //--------------------------------------------------------------------------------------
            Tbl90RefSourcesList =  new ObservableCollection<Tbl90Reference>
                                                          (from refSo in _tbl90ReferencesRepository.GetAll()
                                                          where refSo.OrdoID == CurrentTbl33Ordo.OrdoID
                                                          && refSo.RefExpertID == null
                                                          && refSo.RefAuthorID == null
                                                          orderby refSo.Tbl90RefSources.RefSourceName
                                                          select refSo);

            RefSourcesView = CollectionViewSource.GetDefaultView(Tbl90RefSourcesList);
            RefSourcesView.Refresh();
            //--------------------------------------------------------------------------------------
            Tbl90RefExpertsList =   new ObservableCollection<Tbl90Reference>
                                                          (from refEx in _tbl90ReferencesRepository.GetAll()
                                                          where refEx.OrdoID == CurrentTbl33Ordo.OrdoID
                                                          && refEx.RefAuthorID == null
                                                          && refEx.RefSourceID == null
                                                          orderby refEx.Tbl90RefExperts.RefExpertName
                                                          select refEx);

            RefExpertsView = CollectionViewSource.GetDefaultView(Tbl90RefExpertsList);
            RefExpertsView.Refresh();
            //-----------------------------------------------------------------------------------
            Tbl93CommentsList =  new ObservableCollection<Tbl93Comment>
                                                        (from comm in _tbl93CommentsRepository.GetAll()
                                                        where comm.OrdoID == CurrentTbl33Ordo.OrdoID
                                                        orderby comm.Info
                                                        select comm);

            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            CommentsView.Refresh();
            //------------------------------------------------------------------------------------    

  Tbl27InfraclassesAllList = _allListVm.GetValueTbl27InfraclassesAllList();

            Tbl33OrdosAllList = _allListVm.GetValueTbl33OrdosAllList();

            Tbl90AuthorsAllList = _allListVm.GetValueTbl90AuthorsAllList();

            Tbl90SourcesAllList = _allListVm.GetValueTbl90SourcesAllList();

            Tbl90ExpertsAllList = _allListVm.GetValueTbl90ExpertsAllList();

        }
        #endregion "Public Commands Connected Tables by DoubleClick"   
 

 //    Part 10    

 

 //    Part 11    

     
        #region "Public Properties Tbl33Ordo"

        private string _searchOrdoName;
        public  string SearchOrdoName
        {
            get => _searchOrdoName; 
            set { _searchOrdoName = value; RaisePropertyChanged();  }
        }

        public  ICollectionView OrdosView;
        public  Tbl33Ordo CurrentTbl33Ordo => OrdosView?.CurrentItem as Tbl33Ordo;

        private ObservableCollection<Tbl33Ordo> _tbl33OrdosList;
        public  ObservableCollection<Tbl33Ordo> Tbl33OrdosList
        {
            get => _tbl33OrdosList; 
            set {  _tbl33OrdosList = value; RaisePropertyChanged();   }
        }

        private ObservableCollection<Tbl33Ordo> _tbl33OrdosAllList;
        public  ObservableCollection<Tbl33Ordo> Tbl33OrdosAllList
        {
            get => _tbl33OrdosAllList; 
            set { _tbl33OrdosAllList = value; RaisePropertyChanged(); }
        }

        #endregion "Public Properties"   
       
        #region "Public Properties Tbl30Legio"

        private string _searchLegioName;
        public string SearchLegioName
        {
            get  _searchLegioName; 
            set { _searchLegioName = value; RaisePropertyChanged(); }
        }

        public ICollectionView LegiosView;
        public Tbl30Legio CurrentTbl30Legio => LegiosView?.CurrentItem as Tbl30Legio;           

        private ObservableCollection<Tbl30Legio> _tbl30LegiosList;
        public ObservableCollection<Tbl30Legio> Tbl30LegiosList
        {
            get => _tbl30LegiosList; 
            set { _tbl30LegiosList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl30Legio> _tbl30LegiosAllList;
        public ObservableCollection<Tbl30Legio> Tbl30LegiosAllList
        {
            get => _tbl30LegiosAllList; 
            set { _tbl30LegiosAllList = value; RaisePropertyChanged(); }       
        }

        #endregion "Public Properties"
   
  
       
        #region "Public Properties Tbl36Subordo"

        private string _searchSubordoName;
        public string SearchSubordoName
        {
            get => _searchSubordoName; 
            set { _searchSubordoName = value; RaisePropertyChanged(); }
        }

        public ICollectionView SubordosView;
        public Tbl36Subordo CurrentTbl36Subordo => SubordosView?.CurrentItem as Tbl36Subordo;           

        private ObservableCollection<Tbl36Subordo> _tbl36SubordosList;
        public ObservableCollection<Tbl36Subordo> Tbl36SubordosList
        {
            get => _tbl36SubordosList; 
            set { _tbl36SubordosList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl36Subordo> _tbl36SubordosAllList;
        public ObservableCollection<Tbl36Subordo> Tbl36SubordosAllList
        {
            get => _tbl36SubordosAllList; 
            set { _tbl36SubordosAllList = value; RaisePropertyChanged(); }       
        }

        #endregion "Public Properties"
   
  
       
        #region "Public Properties Tbl39Infraordo"

        private string _searchInfraordoName;
        public string SearchInfraordoName
        {
            get => _searchInfraordoName; 
            set { _searchInfraordoName = value; RaisePropertyChanged(); }
        }

        public ICollectionView InfraordosView;
        public Tbl39Infraordo CurrentTbl39Infraordo => InfraordosView?.CurrentItem as Tbl39Infraordo;           

        private ObservableCollection<Tbl39Infraordo> _tbl39InfraordosList;
        public ObservableCollection<Tbl39Infraordo> Tbl39InfraordosList
        {
            get => _tbl39InfraordosList; 
            set { _tbl39InfraordosList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl39Infraordo> _tbl39InfraordosAllList;
        public ObservableCollection<Tbl39Infraordo> Tbl39InfraordosAllList
        {
            get => _tbl39InfraordosAllList; 
            set { _tbl39InfraordosAllList = value; RaisePropertyChanged(); }       
        }

        #endregion "Public Properties"
   
  
       
        #region "Public Properties Tbl27Infraclass"

        private ObservableCollection<Tbl27Infraclass> _tbl27InfraclassesAllList;
        public  ObservableCollection<Tbl27Infraclass> Tbl27InfraclassesAllList
        {
            get => _tbl27InfraclassesAllList; 
            set { _tbl27InfraclassesAllList = value; RaisePropertyChanged(); }
        }

        #endregion "Public Properties"
   
   

 //    Part 12    

 

   }
}   
