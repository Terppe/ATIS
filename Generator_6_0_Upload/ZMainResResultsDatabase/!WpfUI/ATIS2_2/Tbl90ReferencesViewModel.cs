using System;
using System.Collections.ObjectModel;
using System.Data.Entity.Validation;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using DAL;
using DAL.Helper;
using DAL.Models;
using GalaSoft.MvvmLight;
using WPFUI.MessageBox;
using WPFUI.Utility;
using WPFUI.Views.Main;
using MessageBoxImage = System.Windows.MessageBoxImage;      

    
         //    Tbl90ReferencesViewModel Skriptdatum:  14.11.2017  10:32    

namespace WPFUI.Views.Database
{     
    
    public partial class Tbl90ReferencesViewModel : ViewModelBase                     
    {     
         
        #region "Private Data Members"

        private readonly AllListVm _allListVm = new AllListVm();
        private readonly Repository<Tbl90Reference, int> _tbl90ReferencesRepository = new Repository<Tbl90Reference, int>();   
           

        private readonly Repository<Tbl90RefAuthor, int> _tbl90RefAuthorsRepository = new Repository<Tbl90RefAuthor, int>();
        private readonly Repository<Tbl90RefExpert, int> _tbl90RefExpertsRepository = new Repository<Tbl90RefExpert, int>();
        private readonly Repository<Tbl90RefSource, int> _tbl90RefSourcesRepository = new Repository<Tbl90RefSource, int>();     
         
        #endregion "Private Data Members"               
      
        #region "Constructor"

        public Tbl90ReferencesViewModel()
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
        private  new bool IsInDesignMode { get; set; }

        #endregion "Constructor"             
 

 //    Part 1    

             
        #region "Public Commands Basic Tbl90Reference"

        private RelayCommand _getReferenceByNameOrIdCommand;    
    
        public ICommand GetReferenceByNameOrIdCommand    
    
        {
            get { return _getReferenceByNameOrIdCommand ?? (_getReferenceByNameOrIdCommand = new RelayCommand(delegate { GetReferenceByNameOrId(null); })); }   
        }

        private void GetReferenceByNameOrId(object o)       
        {   
       
            int id;
            if (int.TryParse(SearchReferenceInfo, out id))
                Tbl90ReferencesList = new ObservableCollection<Tbl90Reference> { _tbl90ReferencesRepository.Get(id) };
            else      
                Tbl90ReferencesList = _allListVm.GetValueTbl90ReferencesList(SearchReferenceInfo);      
      
            Tbl03RegnumsAllList = _allListVm.GetValueTbl03RegnumsAllList();
            Tbl06PhylumsAllList = _allListVm.GetValueTbl06PhylumsAllList();
            Tbl09DivisionsAllList = _allListVm.GetValueTbl09DivisionsAllList();
            Tbl12SubphylumsAllList = _allListVm.GetValueTbl12SubphylumsAllList();
            Tbl15SubdivisionsAllList = _allListVm.GetValueTbl15SubdivisionsAllList();
            Tbl18SuperclassesAllList = _allListVm.GetValueTbl18SuperclassesAllList();
            Tbl21ClassesAllList = _allListVm.GetValueTbl21ClassesAllList();
            Tbl24SubclassesAllList = _allListVm.GetValueTbl24SubclassesAllList();
            Tbl27InfraclassesAllList = _allListVm.GetValueTbl27InfraclassesAllList();
            Tbl30LegiosAllList = _allListVm.GetValueTbl30LegiosAllList();
            Tbl33OrdosAllList = _allListVm.GetValueTbl33OrdosAllList();
            Tbl36SubordosAllList = _allListVm.GetValueTbl36SubordosAllList();
            Tbl39InfraordosAllList = _allListVm.GetValueTbl39InfraordosAllList();
            Tbl42SuperfamiliesAllList = _allListVm.GetValueTbl42SuperfamiliesAllList();
            Tbl45FamiliesAllList = _allListVm.GetValueTbl45FamiliesAllList();
            Tbl48SubfamiliesAllList = _allListVm.GetValueTbl48SubfamiliesAllList();
            Tbl51InfrafamiliesAllList = _allListVm.GetValueTbl51InfrafamiliesAllList();
            Tbl54SupertribussesAllList = _allListVm.GetValueTbl54SupertribussesAllList();
            Tbl57TribussesAllList = _allListVm.GetValueTbl57TribussesAllList();
            Tbl60SubtribussesAllList = _allListVm.GetValueTbl60SubtribussesAllList();
            Tbl63InfratribussesAllList = _allListVm.GetValueTbl63InfratribussesAllList();
            Tbl66GenussesAllList = _allListVm.GetValueTbl66GenussesAllList();
            Tbl69FiSpeciessesAllList = _allListVm.GetValueTbl69FiSpeciessesAllList();
            Tbl72PlSpeciessesAllList = _allListVm.GetValueTbl72PlSpeciessesAllList();
            Tbl90RefExpertsAllList = _allListVm.GetValueTbl90ExpertsAllList();
            Tbl90RefSourcesAllList = _allListVm.GetValueTbl90SourcesAllList();
            Tbl90RefAuthorsAllList = _allListVm.GetValueTbl90AuthorsAllList();                   
ReferencesView = CollectionViewSource.GetDefaultView(Tbl90ReferencesList);
            ReferencesView.Refresh();
        }
        //------------------------------------------------------------------------------------                
           
        private RelayCommand _addReferenceCommand;           
    
        public ICommand AddReferenceCommand       
    
        {
            get { return _addReferenceCommand ?? (_addReferenceCommand = new RelayCommand(delegate { AddReference(null); })); }
        }

        private void AddReference(object o)
        {
            Tbl90ReferencesList = new ObservableCollection<Tbl90Reference>();   
Tbl90ReferencesList.Insert(0, new Tbl90Reference{ ReferenceID = 0 });     
ReferencesView = CollectionViewSource.GetDefaultView(Tbl90ReferencesList);
            ReferencesView.Refresh();
        }
        //------------------------------------------------------------------------------------                
           
        private RelayCommand _copyReferenceCommand;              
    
        public ICommand CopyReferenceCommand             
           
        {
            get { return _copyReferenceCommand ?? (_copyReferenceCommand = new RelayCommand(delegate { CopyReference(null); })); }
        }

        private void CopyReference(object o)
        {
            Tbl90ReferencesList = new ObservableCollection<Tbl90Reference>();

            var reference = _tbl90ReferencesRepository.Get(CurrentTbl90Reference.ReferenceID);

            Tbl90ReferencesList.Insert(0, new Tbl90Reference
            {    
       
                            Valid = reference.Valid,
                            ValidYear = reference.ValidYear,              
                            Info = reference.Info,
                            Memo = reference.Memo        
          
            });

            ReferencesView = CollectionViewSource.GetDefaultView(Tbl90ReferencesList);
            ReferencesView.Refresh();
        }
        //---------------------------------------------------------------------------------------                  
           
        private RelayCommand _deleteReferenceCommand;              
    
        public ICommand DeleteReferenceCommand             
             
        {
            get { return _deleteReferenceCommand ?? (_deleteReferenceCommand = new RelayCommand(delegate { DeleteReference(null); })); }
        }

        private void DeleteReference(object o)
        {
            try
            {
                var reference = _tbl90ReferencesRepository.Get(CurrentTbl90Reference.ReferenceID);
                if (reference != null)
                {   
                
                    if (WpfMessageBox.Show(CultRes.StringsRes.DeleteQuestion1, CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl90Reference.Info,
                         MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
                        return; 
_tbl90ReferencesRepository.Delete(reference);
                    _tbl90ReferencesRepository.Save();     
                
                    WpfMessageBox.Show(CultRes.StringsRes.DeleteSuccess, CurrentTbl90Reference.Info,
                        MessageBoxButton.OK, MessageBoxImage.Information); 
                
                    GetConnectedTablesById(o); //refresh doubleClick                                                                
ReferencesView = CollectionViewSource.GetDefaultView(Tbl90ReferencesList);
                    ReferencesView.Refresh();
                }
                else
                {    
                
                    WpfMessageBox.Show(CultRes.StringsRes.Information, CultRes.StringsRes.DeleteCan + " " + CurrentTbl90Reference.Info + " " + CultRes.StringsRes.DeleteCan1,
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
           
        private RelayCommand _saveReferenceCommand;              
     
        public ICommand SaveReferenceCommand             
           
        {
            get { return _saveReferenceCommand ?? (_saveReferenceCommand = new RelayCommand(delegate { SaveReference(null); })); }
        }

        private void SaveReference(object o)
        {
            try
            {
                var reference = _tbl90ReferencesRepository.Get(CurrentTbl90Reference.ReferenceID);
                if (CurrentTbl90Reference == null)
                    WpfMessageBox.Show(CultRes.StringsRes.Warning, CultRes.StringsRes.DatasetNotExist,
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                else
                {
                    if (CurrentTbl90Reference.ReferenceID != 0)   
                    {
                        if (reference != null) //update
                        {  
                
                            reference.RefExpertID = CurrentTbl90Reference.RefExpertID;
                            reference.RefAuthorID = CurrentTbl90Reference.RefAuthorID;
                            reference.RefSourceID = CurrentTbl90Reference.RefSourceID;
                            reference.RegnumID = CurrentTbl90Reference.RegnumID;
                            reference.PhylumID = CurrentTbl90Reference.PhylumID;
                            reference.DivisionID = CurrentTbl90Reference.DivisionID;
                            reference.SubphylumID = CurrentTbl90Reference.SubphylumID;
                            reference.SubdivisionID = CurrentTbl90Reference.SubdivisionID;
                            reference.SuperclassID = CurrentTbl90Reference.SuperclassID;
                            reference.ClassID = CurrentTbl90Reference.ClassID;
                            reference.SubclassID = CurrentTbl90Reference.SubclassID;
                            reference.InfraclassID = CurrentTbl90Reference.InfraclassID;
                            reference.LegioID = CurrentTbl90Reference.LegioID;
                            reference.OrdoID = CurrentTbl90Reference.OrdoID;
                            reference.SubordoID = CurrentTbl90Reference.SubordoID;
                            reference.InfraordoID = CurrentTbl90Reference.InfraordoID;
                            reference.SuperfamilyID = CurrentTbl90Reference.SuperfamilyID;
                            reference.FamilyID = CurrentTbl90Reference.FamilyID;
                            reference.SubfamilyID = CurrentTbl90Reference.SubfamilyID;
                            reference.InfrafamilyID = CurrentTbl90Reference.InfrafamilyID;
                            reference.SupertribusID = CurrentTbl90Reference.SupertribusID;
                            reference.TribusID = CurrentTbl90Reference.TribusID;
                            reference.SubtribusID = CurrentTbl90Reference.SubtribusID;
                            reference.InfratribusID = CurrentTbl90Reference.InfratribusID;
                            reference.GenusID = CurrentTbl90Reference.GenusID;
                            reference.PlSpeciesID = CurrentTbl90Reference.PlSpeciesID;
                            reference.FiSpeciesID = CurrentTbl90Reference.FiSpeciesID;
                            reference.Valid = CurrentTbl90Reference.Valid;
                            reference.ValidYear = CurrentTbl90Reference.ValidYear;
                            reference.Info = CurrentTbl90Reference.Info;
                            reference.Updater = Environment.UserName;
                            reference.UpdaterDate = DateTime.Now;
                            reference.Memo = CurrentTbl90Reference.Memo; 
         
                        }
                    }
                    else
                    {
                        _tbl90ReferencesRepository.Add(new Tbl90Reference     //add new
                        {   
                 
                            RefAuthorID = CurrentTbl90Reference.RefAuthorID,
                            RefSourceID = CurrentTbl90Reference.RefSourceID,
                            RefExpertID = CurrentTbl90Reference.RefExpertID,
                            RegnumID = CurrentTbl90Reference.RegnumID,
                            PhylumID = CurrentTbl90Reference.PhylumID,
                            DivisionID = CurrentTbl90Reference.DivisionID,
                            SubphylumID = CurrentTbl90Reference.SubphylumID,
                            SubdivisionID = CurrentTbl90Reference.SubdivisionID,
                            SuperclassID = CurrentTbl90Reference.SuperclassID,
                            ClassID = CurrentTbl90Reference.ClassID,
                            SubclassID = CurrentTbl90Reference.SubclassID,
                            InfraclassID = CurrentTbl90Reference.InfraclassID,
                            LegioID = CurrentTbl90Reference.LegioID,
                            OrdoID = CurrentTbl90Reference.OrdoID,
                            SubordoID = CurrentTbl90Reference.SubordoID,
                            InfraordoID = CurrentTbl90Reference.InfraordoID,
                            SuperfamilyID = CurrentTbl90Reference.SuperfamilyID,
                            FamilyID = CurrentTbl90Reference.FamilyID,
                            SubfamilyID = CurrentTbl90Reference.SubfamilyID,
                            InfrafamilyID = CurrentTbl90Reference.InfrafamilyID,
                            SupertribusID = CurrentTbl90Reference.SupertribusID,
                            TribusID = CurrentTbl90Reference.TribusID,
                            SubtribusID = CurrentTbl90Reference.SubtribusID,
                            InfratribusID = CurrentTbl90Reference.InfratribusID,
                            GenusID = CurrentTbl90Reference.GenusID,
                            PlSpeciesID = CurrentTbl90Reference.PlSpeciesID,
                            FiSpeciesID = CurrentTbl90Reference.FiSpeciesID,
                            CountID = RandomHelper.Randomnumber(),
                            Valid = CurrentTbl90Reference.Valid,
                            ValidYear = CurrentTbl90Reference.ValidYear,
                            Info = CurrentTbl90Reference.Info,
                            Memo = CurrentTbl90Reference.Memo,
                            Writer = Environment.UserName,
                            WriterDate = DateTime.Now,
                            Updater = Environment.UserName,
                            UpdaterDate = DateTime.Now   
        
                        });
                    }
                    {    
             
                        //check if dataset with vb-name already exist       
                        var dataset = new ObservableCollection<Tbl90Reference>
                        (from a in _tbl90ReferencesRepository.GetAll()
                         where
                         a.RefExpertID == CurrentTbl90Reference.RefExpertID &&
                         a.RefSourceID == CurrentTbl90Reference.RefSourceID &&
                         a.RefAuthorID == CurrentTbl90Reference.RefAuthorID   
         
                         select a);

                        if (dataset.Count != 0 && CurrentTbl90Reference.ReferenceID == 0)  //dataset exist
                        {       
             
                            WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTbl90Reference.ReferenceID.ToString(),
                            MessageBoxButton.OK, MessageBoxImage.Information);  
             
                        }
                        if (dataset.Count == 0 && CurrentTbl90Reference.ReferenceID == 0 ||
                            dataset.Count != 0 && CurrentTbl90Reference.ReferenceID != 0 ||
                            dataset.Count == 0 && CurrentTbl90Reference.ReferenceID != 0) //new dataset and update
                        {    
             
                            if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTbl90Reference.ReferenceID.ToString(),
                                    MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
                                return;  
               
                            {
                                _tbl90ReferencesRepository.Save();     
             
                                WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess, CurrentTbl90Reference.ReferenceID.ToString(),
                                    MessageBoxButton.OK, MessageBoxImage.Information);     
             
                            }
                        }

                        if (CurrentTbl90Reference.ReferenceID == 0)
                        {
                            Tbl90ReferencesList = new ObservableCollection<Tbl90Reference>
                              { new ObservableCollection<Tbl90Reference>
                                  (from x in _tbl90ReferencesRepository.Tables
                                   select x).LastOrDefault()
                              };
                            //last newest Dataset
                        }
                        else
                        {
                            Tbl90ReferencesList = new ObservableCollection<Tbl90Reference>
                                                  (from x in _tbl90ReferencesRepository.GetAll()
                                                   where x.ReferenceID == CurrentTbl90Reference.ReferenceID
                                                   select x);
                        }    
ReferencesView = CollectionViewSource.GetDefaultView(Tbl90ReferencesList);
                        ReferencesView.Refresh();

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
 

 //    Part 2    

           
        #region "Public Commands Connect <== Tbl90RefExpert"                 

        private RelayCommand _getRefExpertByNameOrIdCommand;     
    
        public ICommand GetRefExpertByNameOrIdCommand    
            
        {
            get { return _getRefExpertByNameOrIdCommand ?? (_getRefExpertByNameOrIdCommand = new RelayCommand(delegate { GetRefExpertByNameOrId(null); })); }
        }

        private void GetRefExpertByNameOrId(object o)    
        {

            int id;
            if (int.TryParse(SearchRefExpertName, out id))
                Tbl90RefExpertsList = new ObservableCollection<Tbl90RefExpert> { _tbl90RefExpertsRepository.Get(id) };
            else
                Tbl90RefExpertsList = _allListVm.GetValueTbl90ExpertsList(SearchRefExpertName);

            RefExpertsView = CollectionViewSource.GetDefaultView(Tbl90RefExpertsList);
            RefExpertsView.Refresh();
        }

        //------------------------------------------------------------------------------------                
           
        private RelayCommand _addRefExpertCommand;      
    
        public ICommand AddRefExpertCommand    
    
        {
            get { return _addRefExpertCommand ?? (_addRefExpertCommand = new RelayCommand(delegate { AddRefExpert(null); })); }
        }

        private void AddRefExpert(object o)
        {
            Tbl90RefExpertsList = new ObservableCollection<Tbl90RefExpert>();   
Tbl90RefExpertsList.Insert(0, new Tbl90RefExpert{ RefExpertName = CultRes.StringsRes.DatasetNew });      
RefExpertsView = CollectionViewSource.GetDefaultView(Tbl90RefExpertsList);
            RefExpertsView.Refresh();
        }
        //------------------------------------------------------------------------------------                
           
        private RelayCommand _copyRefExpertCommand;            
    
        public ICommand CopyRefExpertCommand          
         
        {
            get { return _copyRefExpertCommand ?? (_copyRefExpertCommand = new RelayCommand(delegate { CopyRefExpert(null); })); }
        }

        private void CopyRefExpert(object o)
        {
            Tbl90RefExpertsList = new ObservableCollection<Tbl90RefExpert>();

            var refexpert = _tbl90RefExpertsRepository.Get(CurrentTbl90RefExpert.RefExpertID);

            Tbl90RefExpertsList.Insert(0, new Tbl90RefExpert
            {                 
                
                RefExpertName = CultRes.StringsRes.DatasetNew,
                Valid = refexpert.Valid,
                ValidYear = refexpert.ValidYear,
                Info = refexpert.Info,
                Notes = refexpert.Notes,
                Memo = refexpert.Memo        
        
            });

            RefExpertsView = CollectionViewSource.GetDefaultView(Tbl90RefExpertsList);
            RefExpertsView.Refresh();
        }
        //---------------------------------------------------------------------------------------                  
           
        private RelayCommand _deleteRefExpertCommand;              
    
        public ICommand DeleteRefExpertCommand             
         
        {
            get { return _deleteRefExpertCommand ?? (_deleteRefExpertCommand = new RelayCommand(delegate { DeleteRefExpert(null); })); }
        }

        private void DeleteRefExpert(object o)
        {
            try
            {
                var refexpert = _tbl90RefExpertsRepository.Get(CurrentTbl90RefExpert.RefExpertID);
                if (refexpert!= null)
                {  
         
                    if (WpfMessageBox.Show(CultRes.StringsRes.DeleteQuestion1, CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl90RefExpert.RefExpertName,
                         MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes) 
                    return;

                    _tbl90RefExpertsRepository.Delete(refexpert);
                    _tbl90RefExpertsRepository.Save();

                    WpfMessageBox.Show(CultRes.StringsRes.DeleteSuccess, CurrentTbl90RefExpert.RefExpertName, 
                        MessageBoxButton.OK, MessageBoxImage.Information);  
             
                        if (SearchRefExpertName == null)                       
                            GetConnectedTablesById(o); //refresh doubleClick                                          
                        else
                        {
                            Tbl90RefExpertsList = _allListVm.GetValueTbl90ExpertsList(SearchRefExpertName);        
              
                    }
                        RefExpertsView = CollectionViewSource.GetDefaultView(Tbl90RefExpertsList);
                        RefExpertsView.Refresh();
                }
                else
                {   
    
                    WpfMessageBox.Show(CultRes.StringsRes.Information, CultRes.StringsRes.DeleteCan + " " + CurrentTbl90RefExpert.RefExpertName+ " " + CultRes.StringsRes.DeleteCan1,
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
           
        private RelayCommand _saveRefExpertCommand;              
    
        public ICommand SaveRefExpertCommand             
         
        {
            get { return _saveRefExpertCommand ?? (_saveRefExpertCommand = new RelayCommand(delegate { SaveRefExpert(null); })); }
        }

        private void SaveRefExpert(object o)
        {
            try
            {
                var refexpert = _tbl90RefExpertsRepository.Get(CurrentTbl90RefExpert.RefExpertID);
                if (CurrentTbl90RefExpert == null)              
                    WpfMessageBox.Show(CultRes.StringsRes.Warning, CultRes.StringsRes.DatasetNotExist, 
                       MessageBoxButton.OK, MessageBoxImage.Warning);               
                else
                {
                    if (CurrentTbl90RefExpert.RefExpertID!= 0)
                    {
                        if (refexpert!= null) //update
                        {   
refexpert.RefExpertName= CurrentTbl90RefExpert.RefExpertName;                    
              
                            refexpert.Valid = CurrentTbl90RefExpert.Valid;
                            refexpert.ValidYear = CurrentTbl90RefExpert.ValidYear;
                            refexpert.Info = CurrentTbl90RefExpert.Info;
                            refexpert.Notes = CurrentTbl90RefExpert.Notes;
                            refexpert.Updater = Environment.UserName;
                            refexpert.UpdaterDate = DateTime.Now;
                            refexpert.Memo = CurrentTbl90RefExpert.Memo;       
         
                        }
                    }
                    else
                    {
                        _tbl90RefExpertsRepository.Add(new Tbl90RefExpert     //add new
                        {   
                
                            RefExpertName = CurrentTbl90RefExpert.RefExpertName,
                            CountID = RandomHelper.Randomnumber(),
                            Valid = CurrentTbl90RefExpert.Valid,
                            ValidYear = CurrentTbl90RefExpert.ValidYear,
                            Info = CurrentTbl90RefExpert.Info,
                            Notes = CurrentTbl90RefExpert.Notes,
                            Writer = Environment.UserName,
                            WriterDate = DateTime.Now,
                            Updater = Environment.UserName,
                            UpdaterDate = DateTime.Now,
                            Memo = CurrentTbl90RefExpert.Memo 
           
                        });
                    }
                    {   
            
                        //check about double Name   
                        var dataset = new ObservableCollection<Tbl90RefExpert>
                        (from a in _tbl90RefExpertsRepository.GetAll()
                         where
                         a.RefExpertName.Trim() == CurrentTbl90RefExpert.RefExpertName.Trim()
                         select a);  
           
                        if (dataset.Count != 0 && CurrentTbl90RefExpert.RefExpertID == 0)  //dataset exist
                        {
                            WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTbl90RefExpert.RefExpertName,
                            MessageBoxButton.OK, MessageBoxImage.Information);
                        }   
           
                        if (dataset.Count == 0 && CurrentTbl90RefExpert.RefExpertID == 0 ||
                            dataset.Count != 0 && CurrentTbl90RefExpert.RefExpertID != 0 ||
                            dataset.Count == 0 && CurrentTbl90RefExpert.RefExpertID != 0) //new dataset and update
                        {  
           
                            if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTbl90RefExpert.RefExpertName,
                                    MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
                                return;
                            {
                                _tbl90RefExpertsRepository.Save();

                                WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess, CurrentTbl90RefExpert.RefExpertName,
                                    MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                        }   
              
                        if (SearchRefExpertName == null && CurrentTbl90RefExpert.RefExpertID == 0)  //new Dataset                        
                            GetConnectedTablesById(o); //refresh doubleClick                                          
                        if (SearchRefExpertName == null && CurrentTbl90RefExpert.RefExpertID != 0)   //update 
                            Tbl90RefExpertsList = _allListVm.GetValueTbl90ExpertsList(CurrentTbl90RefExpert.RefExpertID);
                        if (SearchRefExpertName != null && CurrentTbl90RefExpert.RefExpertID == 0)  //new Dataset                        
                            GetConnectedTablesById(o); //refresh doubleClick                                          
                        if (SearchRefExpertName != null && CurrentTbl90RefExpert.RefExpertID != 0)   //update 
                            Tbl90RefExpertsList = _allListVm.GetValueTbl90ExpertsList(CurrentTbl90RefExpert.RefExpertID);  
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
 

 //    Part 3    

             

        #region "Public Commands Connect <== Tbl90RefSource"               

        private RelayCommand _getRefSourceByNameOrIdCommand;

        public ICommand GetRefSourceByNameOrIdCommand

        {
            get { return _getRefSourceByNameOrIdCommand ?? (_getRefSourceByNameOrIdCommand = new RelayCommand(delegate { GetRefSourceByNameOrId(null); })); }
        }

        private void GetRefSourceByNameOrId(object o)    
        {

            int id;
            if (int.TryParse(SearchRefSourceName, out id))
                Tbl90RefSourcesList = new ObservableCollection<Tbl90RefSource> { _tbl90RefSourcesRepository.Get(id) };
            else
                Tbl90RefSourcesList = _allListVm.GetValueTbl90SourcesList(SearchRefSourceName);

            RefSourcesView = CollectionViewSource.GetDefaultView(Tbl90RefSourcesList);
            RefSourcesView.Refresh();
        }

        //------------------------------------------------------------------------------------                
               
        private RelayCommand  _addRefSourceCommand;

        public ICommand AddRefSourceCommand

        {
            get { return _addRefSourceCommand ?? (_addRefSourceCommand = new RelayCommand(delegate { AddRefSource(null); })); }
        }

        private void AddRefSource(object o)
        {
            Tbl90RefSourcesList = new ObservableCollection<Tbl90RefSource>();
            Tbl90RefSourcesList.Insert(0, new Tbl90RefSource{ RefSourceName = CultRes.StringsRes.DatasetNew });

            RefSourcesView = CollectionViewSource.GetDefaultView(Tbl90RefSourcesList);
            RefSourcesView.Refresh();
        }
        //------------------------------------------------------------------------------------                            
               
        private RelayCommand _copyRefSourceCommand;

        public ICommand CopyRefSourceCommand

        {
            get { return _copyRefSourceCommand ?? (_copyRefSourceCommand = new RelayCommand(delegate { CopyRefSource(null); })); }
        }

        private void CopyRefSource(object o)
        {
            Tbl90RefSourcesList = new ObservableCollection<Tbl90RefSource>();

            var refsource = _tbl90RefSourcesRepository.Get(CurrentTbl90RefSource.RefSourceID);

            Tbl90RefSourcesList.Insert(0, new Tbl90RefSource
            {
                RefSourceName = CultRes.StringsRes.DatasetNew,
                Valid = refsource.Valid,
                ValidYear = refsource.ValidYear,
                Info = refsource.Info,
                Notes = refsource.Notes,
                Memo = refsource.Memo
            });

            RefSourcesView = CollectionViewSource.GetDefaultView(Tbl90RefSourcesList);
            RefSourcesView.Refresh();
        }
        //---------------------------------------------------------------------------------------                                 
                    
        private RelayCommand _deleteRefSourceCommand;

        public ICommand DeleteRefSourceCommand

        {
            get { return _deleteRefSourceCommand ?? (_deleteRefSourceCommand = new RelayCommand(delegate { DeleteRefSource(null); })); }
        }

        private void DeleteRefSource(object o)
        {
            try
            {
                var refsource = _tbl90RefSourcesRepository.Get(CurrentTbl90RefSource.RefSourceID);
                if (refsource != null)
                {
                    if (WpfMessageBox.Show(CultRes.StringsRes.DeleteQuestion1, CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl90RefSource.RefSourceName,
                         MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
                        return;
                    _tbl90RefSourcesRepository.Delete(refsource);
                    _tbl90RefSourcesRepository.Save();

                    WpfMessageBox.Show(CultRes.StringsRes.DeleteSuccess, CurrentTbl90RefSource.RefSourceName,
                        MessageBoxButton.OK, MessageBoxImage.Information);

                    if (SearchRefSourceName == null)
                        GetConnectedTablesById(o); //refresh doubleClick                                       
                    else
                    {
                        Tbl90RefSourcesList = _allListVm.GetValueTbl90SourcesList(SearchRefSourceName);
                    }
                    RefSourcesView = CollectionViewSource.GetDefaultView(Tbl90RefSourcesList);
                    RefSourcesView.Refresh();
                }
                else
                {
                    WpfMessageBox.Show(CultRes.StringsRes.Information, CultRes.StringsRes.DeleteCan + " " + CurrentTbl90RefSource.RefSourceName+ " " + CultRes.StringsRes.DeleteCan1,
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
           
        private RelayCommand _saveRefSourceCommand;

        public ICommand SaveRefSourceCommand

        {
            get { return _saveRefSourceCommand ?? (_saveRefSourceCommand = new RelayCommand(delegate { SaveRefSource(null); })); }
        }

        private void SaveRefSource(object o)
        {
            try
            {
                var refsource = _tbl90RefSourcesRepository.Get(CurrentTbl90RefSource.RefSourceID);
                if (CurrentTbl90RefSource == null)
                    WpfMessageBox.Show(CultRes.StringsRes.Warning, CultRes.StringsRes.DatasetNotExist,
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                else
                {
                    if (CurrentTbl90RefSource.RefSourceID != 0)   
                    {
                        if (refsource != null) //update
                        {   
             
                            refsource.RefSourceName = CurrentTbl90RefSource.RefSourceName;
                            refsource.Valid = CurrentTbl90RefSource.Valid;
                            refsource.ValidYear = CurrentTbl90RefSource.ValidYear;
                            refsource.Info = CurrentTbl90RefSource.Info;
                            refsource.Notes = CurrentTbl90RefSource.Notes;
                            refsource.Updater = Environment.UserName;
                            refsource.UpdaterDate = DateTime.Now;
                            refsource.Memo = CurrentTbl90RefSource.Memo;
                        }
                    }
                    else
                    {
                        _tbl90RefSourcesRepository.Add(new Tbl90RefSource   //add new
                        {
                            RefSourceName = CurrentTbl90RefSource.RefSourceName,
                            CountID = RandomHelper.Randomnumber(),
                            Valid = CurrentTbl90RefSource.Valid,
                            ValidYear = CurrentTbl90RefSource.ValidYear,
                            Info = CurrentTbl90RefSource.Info,
                            Notes = CurrentTbl90RefSource.Notes,
                            Writer = Environment.UserName,
                            WriterDate = DateTime.Now,
                            Updater = Environment.UserName,
                            UpdaterDate = DateTime.Now,
                            Memo = CurrentTbl90RefSource.Memo
                        });
                    }
                    {   
            
                        //check about double Name   
                        var dataset = new ObservableCollection<Tbl90RefSource>
                        (from a in _tbl90RefSourcesRepository.GetAll()
                         where
                         a.RefSourceName.Trim() == CurrentTbl90RefSource.RefSourceName.Trim() 
                         select a);

                        if (dataset.Count != 0 && CurrentTbl90RefSource.RefSourceID == 0)  //dataset exist
                        {
                            WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTbl90RefSource.RefSourceName,
                            MessageBoxButton.OK, MessageBoxImage.Information);
                        }

                        if (dataset.Count == 0 && CurrentTbl90RefSource.RefSourceID == 0 ||
                            dataset.Count != 0 && CurrentTbl90RefSource.RefSourceID != 0 ||
                            dataset.Count == 0 && CurrentTbl90RefSource.RefSourceID != 0) //new dataset and update
                        {
                            if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTbl90RefSource.RefSourceName,
                                    MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
                                return;
                            {
                                _tbl90RefSourcesRepository.Save();

                                WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess, CurrentTbl90RefSource.RefSourceName,
                                    MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                        }

                        if (SearchRefSourceName == null && CurrentTbl90RefSource.RefSourceID == 0)  //new Dataset                        
                            GetConnectedTablesById(o); //refresh doubleClick                                          
                        if (SearchRefSourceName == null && CurrentTbl90RefSource.RefSourceID != 0)   //update 
                            Tbl90RefSourcesList = _allListVm.GetValueTbl90SourcesList(CurrentTbl90RefSource.RefSourceID);
                        if (SearchRefSourceName != null && CurrentTbl90RefSource.RefSourceID == 0)  //new Dataset                        
                            GetConnectedTablesById(o); //refresh doubleClick                                          
                        if (SearchRefSourceName != null && CurrentTbl90RefSource.RefSourceID != 0)   //update 
                            Tbl90RefSourcesList = _allListVm.GetValueTbl90SourcesList(CurrentTbl90RefSource.RefSourceID);

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
 

 //    Part 4    

                      
        #region "Public Commands Connect <== Tbl90RefAuthor"

        private RelayCommand _getRefAuthorByNameCommand;

        public ICommand GetRefAuthorByNameCommand

        {
            get { return _getRefAuthorByNameCommand ?? (_getRefAuthorByNameCommand = new RelayCommand(delegate { GetRefAuthorByNameOrId(null); })); }
        }

        private void GetRefAuthorByNameOrId(object o)
        {

            int id;
            if (int.TryParse(SearchRefAuthorName, out id))
                Tbl90RefAuthorsList = new ObservableCollection<Tbl90RefAuthor> { _tbl90RefAuthorsRepository.Get(id) };
            else
                Tbl90RefAuthorsList = _allListVm.GetValueTbl90AuthorsList(SearchRefAuthorName);

            RefAuthorsView = CollectionViewSource.GetDefaultView(Tbl90RefAuthorsList);
            RefAuthorsView.Refresh();
        }
        //------------------------------------------------------------------------------       
                   
        private RelayCommand _addRefAuthorCommand;

        public ICommand AddRefAuthorCommand

        {
            get { return _addRefAuthorCommand ?? (_addRefAuthorCommand = new RelayCommand(delegate { AddRefAuthor(null); })); }
        }

        private void AddRefAuthor(object o)
        {
            Tbl90RefAuthorsList = new ObservableCollection<Tbl90RefAuthor>();

            Tbl90RefAuthorsList.Insert(0, new Tbl90RefAuthor { RefAuthorName = CultRes.StringsRes.DatasetNew });

            RefAuthorsView = CollectionViewSource.GetDefaultView(Tbl90RefAuthorsList);
            RefAuthorsView.Refresh();
        }
        //------------------------------------------------------------------------------                  
                
        private RelayCommand _copyRefAuthorCommand;

        public ICommand CopyRefAuthorCommand

        {
            get { return _copyRefAuthorCommand ?? (_copyRefAuthorCommand = new RelayCommand(delegate { CopyRefAuthor(null); })); }
        }

        private void CopyRefAuthor(object o)
        {
            Tbl90RefAuthorsList = new ObservableCollection<Tbl90RefAuthor>();

            var refauthor = _tbl90RefAuthorsRepository.Get(CurrentTbl90RefAuthor.RefAuthorID);

            Tbl90RefAuthorsList.Insert(0, new Tbl90RefAuthor
            {  
                    
                RefAuthorName = CultRes.StringsRes.DatasetNew,
                Valid = refauthor.Valid,
                ValidYear = refauthor.ValidYear,
                PublicationYear = refauthor.PublicationYear,
                ArticelTitle = refauthor.ArticelTitle,
                BookName = refauthor.BookName,
                Page1 = refauthor.Page1,
                Publisher = refauthor.Publisher,
                PublicationPlace = refauthor.PublicationPlace,
                ISBN = refauthor.ISBN,
                Notes = refauthor.Notes,
                Info = refauthor.Info,
                Memo = refauthor.Memo            
                                  
            });

            RefAuthorsView = CollectionViewSource.GetDefaultView(Tbl90RefAuthorsList);
            RefAuthorsView.Refresh();
        }
        //---------------------------------------------------------------------------------------             
                                                   
        private RelayCommand _deleteRefAuthorCommand;     
                                                   
        public ICommand DeleteRefAuthorCommand   
                                                   
        {
            get { return _deleteRefAuthorCommand ?? (_deleteRefAuthorCommand = new RelayCommand(delegate { DeleteRefAuthor(null); })); }
        }

        private void DeleteRefAuthor(object o)
        {
            try
            {
                var refauthor = _tbl90RefAuthorsRepository.Get(CurrentTbl90RefAuthor.RefAuthorID);
                if (refauthor != null)
                {    
                                                   
                    if (WpfMessageBox.Show(CultRes.StringsRes.DeleteQuestion1, CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl90RefAuthor.RefAuthorName, 
                        MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes) 
                    return;

                    _tbl90RefAuthorsRepository.Delete(refauthor);
                    _tbl90RefAuthorsRepository.Save();

                    WpfMessageBox.Show(CultRes.StringsRes.DeleteSuccess, CurrentTbl90RefAuthor.RefAuthorName, 
                        MessageBoxButton.OK, MessageBoxImage.Information);  
                                                   
                    if (SearchRefAuthorName == null)
                    {
                        GetConnectedTablesById(o); //refresh doubleClick                    
                    }
                    else
                    {
                        Tbl90RefAuthorsList = _allListVm.GetValueTbl90AuthorsList(SearchRefAuthorName);   
                                                   
                    }
                        RefAuthorsView = CollectionViewSource.GetDefaultView(Tbl90RefAuthorsList);
                        RefAuthorsView.Refresh();
                }
                else
                {    
                                                   
                    WpfMessageBox.Show(CultRes.StringsRes.Information, CultRes.StringsRes.DeleteCan + " " + CurrentTbl90RefAuthor.RefAuthorName + " "
                        + CultRes.StringsRes.DeleteCan1, MessageBoxButton.OK, MessageBoxImage.Information);  
                                                        
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

        public ICommand SaveRefAuthorCommand

        {
            get { return _saveRefAuthorCommand ?? (_saveRefAuthorCommand = new RelayCommand(delegate { SaveRefAuthor(null); })); }
        }

        private void SaveRefAuthor(object o)
        {
            try
            {
                var refauthor = _tbl90RefAuthorsRepository.Get(CurrentTbl90RefAuthor.RefAuthorID);
                if (CurrentTbl90RefAuthor == null)
                {
                    WpfMessageBox.Show(CultRes.StringsRes.Warning, CultRes.StringsRes.DatasetNotExist, MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    if (CurrentTbl90RefAuthor.RefAuthorID != 0)
                    {
                        if (refauthor != null) //update
                        {          
             
                            refauthor.RefAuthorName = CurrentTbl90RefAuthor.RefAuthorName;
                            refauthor.Valid = CurrentTbl90RefAuthor.Valid;
                            refauthor.ValidYear = CurrentTbl90RefAuthor.ValidYear;
                            refauthor.PublicationYear = CurrentTbl90RefAuthor.PublicationYear;
                            refauthor.ArticelTitle = CurrentTbl90RefAuthor.ArticelTitle;
                            refauthor.BookName = CurrentTbl90RefAuthor.BookName;
                            refauthor.Info = CurrentTbl90RefAuthor.Info;
                            refauthor.Page1 = CurrentTbl90RefAuthor.Page1;
                            refauthor.Publisher = CurrentTbl90RefAuthor.Publisher;
                            refauthor.PublicationPlace = CurrentTbl90RefAuthor.PublicationPlace;
                            refauthor.ISBN = CurrentTbl90RefAuthor.ISBN;
                            refauthor.Notes = CurrentTbl90RefAuthor.Notes;
                            refauthor.Updater = Environment.UserName;
                            refauthor.UpdaterDate = DateTime.Now;
                            refauthor.Memo = CurrentTbl90RefAuthor.Memo;

                        }
                    }
                    else
                    {
                        _tbl90RefAuthorsRepository.Add(new Tbl90RefAuthor    // add new
                        {


                            RefAuthorName = CurrentTbl90RefAuthor.RefAuthorName,
                            CountID = RandomHelper.Randomnumber(),
                            Valid = CurrentTbl90RefAuthor.Valid,
                            ValidYear = CurrentTbl90RefAuthor.ValidYear,
                            PublicationYear = CurrentTbl90RefAuthor.PublicationYear,
                            ArticelTitle = CurrentTbl90RefAuthor.ArticelTitle,
                            BookName = CurrentTbl90RefAuthor.BookName,
                            Info = CurrentTbl90RefAuthor.Info,
                            Page1 = CurrentTbl90RefAuthor.Page1,
                            Publisher = CurrentTbl90RefAuthor.Publisher,
                            PublicationPlace = CurrentTbl90RefAuthor.PublicationPlace,
                            ISBN = CurrentTbl90RefAuthor.ISBN,
                            Notes = CurrentTbl90RefAuthor.Notes,
                            Writer = Environment.UserName,
                            WriterDate = DateTime.Now,
                            Updater = Environment.UserName,
                            UpdaterDate = DateTime.Now,
                            Memo = CurrentTbl90RefAuthor.Memo            
             
                        });
                    }
                    {
                        //check about double Name   
                        var dataset = new ObservableCollection<Tbl90RefAuthor>
                        (from a in _tbl90RefAuthorsRepository.GetAll()
                         where
                         a.RefAuthorName.Trim() == CurrentTbl90RefAuthor.RefAuthorName.Trim()
                         select a);            
             
                        if (dataset.Count != 0 && CurrentTbl90RefAuthor.RefAuthorID == 0) //dataset exist
                        {
                            WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTbl90RefAuthor.RefAuthorName,
                                MessageBoxButton.OK, MessageBoxImage.Information);
                        }           
             
                        if (dataset.Count == 0 && CurrentTbl90RefAuthor.RefAuthorID == 0 ||
                            dataset.Count != 0 && CurrentTbl90RefAuthor.RefAuthorID != 0 ||
                            dataset.Count == 0 && CurrentTbl90RefAuthor.RefAuthorID != 0) //new dataset and update
                        {           
             
                            if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTbl90RefAuthor.RefAuthorName,
                                    MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
                                return;
                            {
                                _tbl90RefAuthorsRepository.Save();

                                WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess, CurrentTbl90RefAuthor.RefAuthorName,
                                    MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                        }           
             
                        if (SearchRefAuthorName == null && CurrentTbl90RefAuthor.RefAuthorID == 0)  //new Dataset                        
                            GetConnectedTablesById(o); //refresh doubleClick                                          
                        if (SearchRefAuthorName == null && CurrentTbl90RefAuthor.RefAuthorID != 0)  //update                     
                            Tbl90RefAuthorsList = _allListVm.GetValueTbl90AuthorsList(CurrentTbl90RefAuthor.RefAuthorID);
                        if (SearchRefAuthorName != null && CurrentTbl90RefAuthor.RefAuthorID == 0)  //new Dataset                        
                            GetConnectedTablesById(o); //refresh doubleClick                                          
                        if (SearchRefAuthorName != null && CurrentTbl90RefAuthor.RefAuthorID != 0)  //update                     
                            Tbl90RefAuthorsList = _allListVm.GetValueTbl90AuthorsList(CurrentTbl90RefAuthor.RefAuthorID);             
             
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
 

 //    Part 5    

      

 //    Part 6    

      

 //    Part 7    

      

 //    Part 8    

      

 //    Part 9    

     
        #region "Public Commands Connected Tables by DoubleClick"

        private RelayCommand _getConnectedTablesCommand;
        public ICommand GetConnectedTablesCommand
        {
            get { return _getConnectedTablesCommand ?? (_getConnectedTablesCommand = new RelayCommand(delegate { GetConnectedTablesById(null); })); }
        }

        public void GetConnectedTablesById(object o)
        {

            Tbl90RefExpertsList?.Clear();
            Tbl90RefExpertsList =  new ObservableCollection<Tbl90RefExpert>
                                                        (from refexpert in _tbl90RefExpertsRepository.GetAll()
                                                         where refexpert.RefExpertID == CurrentTbl90Reference.RefExpertID
                                                         orderby refexpert.RefExpertName
                                                         select refexpert);

            RefExpertsView = CollectionViewSource.GetDefaultView(Tbl90RefExpertsList);
            RefExpertsView.Refresh();

            SelectedMainTabIndex = 0;  //change to Connect tab
            SelectedDetailTabIndex = 1;
            SelectedDetailSubTabIndex = 0;
        }

        #endregion "Public Commands Connected Tables by DoubleClick"
   
 

 //    Part 10    

     
        #region "Public Commands to open Detail TabItems"

        private int _selectedMainTabIndex;
        private int _selectedMainSubRefTabIndex;
        private int _selectedDetailTabIndex;
        private int _selectedDetailSubTabIndex;
        private int _selectedDetailSubRefTabIndex;

        public  int SelectedMainTabIndex
        {
            get => _selectedMainTabIndex; 
            set
            {
                if (value == _selectedMainTabIndex) return;
                _selectedMainTabIndex = value; 
                RaisePropertyChanged();
                if (_selectedMainTabIndex == 0)             
                    SelectedDetailSubTabIndex = 0;              
                if (_selectedMainTabIndex == 1)
                {
                    SelectedDetailTabIndex = 1;
                    SelectedDetailSubTabIndex = 1;
                }
            }
        }

        public  int SelectedMainSubRefTabIndex
        {
            get => _selectedMainSubRefTabIndex; 
            set
            {
                if (value == _selectedMainSubRefTabIndex) return;
                _selectedMainSubRefTabIndex = value; 
                 RaisePropertyChanged();
                if (_selectedMainSubRefTabIndex == 0)
                    SelectedDetailSubRefTabIndex = 0;
                if (_selectedMainSubRefTabIndex == 1)
                    SelectedDetailSubRefTabIndex = 1;
                if (_selectedMainSubRefTabIndex == 2)
                    SelectedDetailSubRefTabIndex = 2;
            }
        }

        public  int SelectedDetailTabIndex
        {
            get => _selectedDetailTabIndex; 
            set
            {
                if (value == _selectedDetailTabIndex) return;
                _selectedDetailTabIndex = value;
                RaisePropertyChanged();
                if (_selectedDetailTabIndex == 0)
                {
                    SelectedDetailSubTabIndex = 0;
                    SelectedMainTabIndex = 0;
                    SelectedDetailSubRefTabIndex = 0;
                }
                if (_selectedDetailTabIndex == 1)
                {
                    SelectedDetailSubTabIndex = 1;
                    SelectedMainTabIndex = 0;
                    SelectedDetailSubRefTabIndex = 0;
                }
                if (_selectedDetailTabIndex == 2)
                {
                    SelectedDetailSubTabIndex = 2;
                    SelectedMainTabIndex = 0;
                    SelectedDetailSubRefTabIndex = 0;
                }
            }
        }

        public  int SelectedDetailSubTabIndex
        {
            get => _selectedDetailSubTabIndex;
            set
            {
                if (value == _selectedDetailSubTabIndex) return;
                _selectedDetailSubTabIndex = value;
                RaisePropertyChanged();
                if (_selectedDetailSubTabIndex == 0)
                {
                    SelectedMainTabIndex = 0;
                }
                if (_selectedDetailSubTabIndex == 1)
                {
                    Tbl90RefSourcesList?.Clear();
                    Tbl90RefSourcesList =  new ObservableCollection<Tbl90RefSource>
                                                        (from refsource in _tbl90RefSourcesRepository.GetAll()
                                                         where refsource.RefSourceID == CurrentTbl90Reference.RefSourceID
                                                         orderby refsource.RefSourceName
                                                         select refsource);


                    RefSourcesView = CollectionViewSource.GetDefaultView(Tbl90RefSourcesList);
                    RefSourcesView.Refresh();

                    SelectedMainTabIndex = 1;
                }
                if (_selectedDetailSubTabIndex == 2)
                {
                    Tbl90RefAuthorsList?.Clear();
                    Tbl90RefAuthorsList =  new ObservableCollection<Tbl90RefAuthor>
                                                        (from refauthor in _tbl90RefAuthorsRepository.GetAll()
                                                         where refauthor.RefAuthorID == CurrentTbl90Reference.RefAuthorID
                                                         orderby refauthor.RefAuthorName, refauthor.BookName, refauthor.Page1
                                                         select refauthor);

                    RefAuthorsView = CollectionViewSource.GetDefaultView(Tbl90RefAuthorsList);
                    RefAuthorsView.Refresh();

                    SelectedMainTabIndex = 2;
                }
            }
        }

        public  int SelectedDetailSubRefTabIndex
        {
            get => _selectedDetailSubRefTabIndex;
            set
            {
                if (value == _selectedDetailSubRefTabIndex) return;
                _selectedDetailSubRefTabIndex = value;
                RaisePropertyChanged();
                if (_selectedDetailSubRefTabIndex == 0)
                {
                    SelectedMainSubRefTabIndex = 0;
                }
                if (_selectedDetailSubRefTabIndex == 1)
                {
                    SelectedMainSubRefTabIndex = 1;
                }
                if (_selectedDetailSubRefTabIndex == 2)
                {
                    SelectedMainSubRefTabIndex = 2;
                }
            }
        }
        #endregion "Public Commands to open Detail TabItems"
 




   }
}   
