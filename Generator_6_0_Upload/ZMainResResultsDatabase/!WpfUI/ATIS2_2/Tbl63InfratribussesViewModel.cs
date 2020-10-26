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

    
         //    Tbl63InfratribussesViewModel Skriptdatum:  23.12.2017  10:32    

namespace WPFUI.Views.Database
{     
    
    public partial class Tbl63InfratribussesViewModel : Tbl03RegnumsViewModel
    {     
         
        #region "Private Data Members"

        private readonly AllListVm _allListVm = new AllListVm();
        private readonly Repository<Tbl63Infratribus, int> _tbl63InfratribussesRepository = new Repository<Tbl63Infratribus, int>();   
         
        private readonly Repository<Tbl60Subtribus, int> _tbl60SubtribussesRepository = new Repository<Tbl60Subtribus, int>();   
           
        private readonly Repository<Tbl66Genus, int> _tbl66GenussesRepository = new Repository<Tbl66Genus, int>();   
           
        private readonly Repository<Tbl90Reference, int> _tbl90ReferencesRepository = new Repository<Tbl90Reference, int>();
        private readonly Repository<Tbl93Comment, int> _tbl93CommentsRepository = new Repository<Tbl93Comment, int>();    

        #endregion "Private Data Members"               
      
        #region "Constructor"

        public Tbl63InfratribussesViewModel()
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

             
        #region "Public Commands Basic Tbl63Infratribus"

        private RelayCommand _getInfratribusByNameOrIdCommand;    
    
        public ICommand GetInfratribusByNameOrIdCommand    
    
        {
            get { return _getInfratribusByNameOrIdCommand ?? (_getInfratribusByNameOrIdCommand = new RelayCommand(delegate { GetInfratribusByNameOrId(null); })); }   
        }

        private void GetInfratribusByNameOrId(object o)       
        {   
    
            int id;
            if (int.TryParse(SearchInfratribusName, out id))
                Tbl63InfratribussesList = new ObservableCollection<Tbl63Infratribus> { _tbl63InfratribussesRepository.Get(id) };
            else           
                Tbl63InfratribussesList = _allListVm.GetValueTbl63InfratribussesList(SearchInfratribusName);      
Tbl60SubtribussesAllList?.Clear();
            Tbl60SubtribussesAllList = _allListVm.GetValueTbl60SubtribussesAllList();  
InfratribussesView = CollectionViewSource.GetDefaultView(Tbl63InfratribussesList);
            InfratribussesView.Refresh();
        }
        //------------------------------------------------------------------------------------                
           
        private RelayCommand _addInfratribusCommand;           
    
        public ICommand AddInfratribusCommand       
    
        {
            get { return _addInfratribusCommand ?? (_addInfratribusCommand = new RelayCommand(delegate { AddInfratribus(null); })); }
        }

        private void AddInfratribus(object o)
        {
            Tbl63InfratribussesList = new ObservableCollection<Tbl63Infratribus>();   
Tbl63InfratribussesList.Insert(0, new Tbl63Infratribus{ InfratribusName= CultRes.StringsRes.DatasetNew });  

            Tbl60SubtribussesAllList?.Clear();
            Tbl60SubtribussesAllList = _allListVm.GetValueTbl60SubtribussesAllList();             
InfratribussesView = CollectionViewSource.GetDefaultView(Tbl63InfratribussesList);
            InfratribussesView.Refresh();
        }
        //------------------------------------------------------------------------------------                
           
        private RelayCommand _copyInfratribusCommand;              
    
        public ICommand CopyInfratribusCommand             
           
        {
            get { return _copyInfratribusCommand ?? (_copyInfratribusCommand = new RelayCommand(delegate { CopyInfratribus(null); })); }
        }

        private void CopyInfratribus(object o)
        {
            Tbl63InfratribussesList = new ObservableCollection<Tbl63Infratribus>();

            var infratribus = _tbl63InfratribussesRepository.Get(CurrentTbl63Infratribus.InfratribusID);

            Tbl63InfratribussesList.Insert(0, new Tbl63Infratribus
            {    
SubtribusID = infratribus.SubtribusID,   
InfratribusName = CultRes.StringsRes.DatasetNew,
                Valid = infratribus.Valid,
                ValidYear = infratribus.ValidYear,
                Synonym = infratribus.Synonym,
                Author = infratribus.Author,
                AuthorYear = infratribus.AuthorYear,
                Info = infratribus.Info,
                EngName = infratribus.EngName,
                GerName = infratribus.GerName,
                FraName = infratribus.FraName,
                PorName = infratribus.PorName,
                Memo = infratribus.Memo
              
          
            });

            InfratribussesView = CollectionViewSource.GetDefaultView(Tbl63InfratribussesList);
            InfratribussesView.Refresh();
        }
        //---------------------------------------------------------------------------------------                  
           
        private RelayCommand _deleteInfratribusCommand;              
    
        public ICommand DeleteInfratribusCommand             
             
        {
            get { return _deleteInfratribusCommand ?? (_deleteInfratribusCommand = new RelayCommand(delegate { DeleteInfratribus(null); })); }
        }

        private void DeleteInfratribus(object o)
        {
            try
            {
                var infratribus = _tbl63InfratribussesRepository.Get(CurrentTbl63Infratribus.InfratribusID);
                if (infratribus != null)
                {   
          
                    if (WpfMessageBox.Show(CultRes.StringsRes.DeleteQuestion1, CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl63Infratribus.InfratribusName,
                         MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
                        return;    
_tbl63InfratribussesRepository.Delete(infratribus);
                    _tbl63InfratribussesRepository.Save();     
          
                    WpfMessageBox.Show(CultRes.StringsRes.DeleteSuccess, CurrentTbl63Infratribus.InfratribusName,
                        MessageBoxButton.OK, MessageBoxImage.Information);   
             
                    if (SearchInfratribusName == null)
                        GetConnectedTablesById(o); //refresh doubleClick                                       
                    else
                    {
                        Tbl63InfratribussesList = _allListVm.GetValueTbl63InfratribussesList(SearchInfratribusName);   
             
                    }
                    InfratribussesView = CollectionViewSource.GetDefaultView(Tbl63InfratribussesList);
                    InfratribussesView.Refresh();
                }
                else
                {    
             
                    WpfMessageBox.Show(CultRes.StringsRes.Information, CultRes.StringsRes.DeleteCan + " " + CurrentTbl63Infratribus.InfratribusName+ " " + CultRes.StringsRes.DeleteCan1,
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
           
        private RelayCommand _saveInfratribusCommand;              
     
        public ICommand SaveInfratribusCommand             
           
        {
            get { return _saveInfratribusCommand ?? (_saveInfratribusCommand = new RelayCommand(delegate { SaveInfratribus(null); })); }
        }

        private void SaveInfratribus(object o)
        {
            try
            {
                var infratribus = _tbl63InfratribussesRepository.Get(CurrentTbl63Infratribus.InfratribusID);
                if (CurrentTbl63Infratribus == null)
                    WpfMessageBox.Show(CultRes.StringsRes.Warning, CultRes.StringsRes.DatasetNotExist,
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                else
                {
                    if (CurrentTbl63Infratribus.InfratribusID != 0)   
                    {
                        if (infratribus != null) //update
                        {  
infratribus.InfratribusName = CurrentTbl63Infratribus.InfratribusName;
                            infratribus.SubtribusID = CurrentTbl63Infratribus.SubtribusID;           
infratribus.Valid = CurrentTbl63Infratribus.Valid;
                            infratribus.ValidYear = CurrentTbl63Infratribus.ValidYear;
                            infratribus.Synonym = CurrentTbl63Infratribus.Synonym;
                            infratribus.Author = CurrentTbl63Infratribus.Author;
                            infratribus.AuthorYear = CurrentTbl63Infratribus.AuthorYear;
                            infratribus.Info = CurrentTbl63Infratribus.Info;
                            infratribus.EngName = CurrentTbl63Infratribus.EngName;
                            infratribus.GerName = CurrentTbl63Infratribus.GerName;
                            infratribus.FraName = CurrentTbl63Infratribus.FraName;
                            infratribus.PorName = CurrentTbl63Infratribus.PorName;
                            infratribus.Updater = Environment.UserName;
                            infratribus.UpdaterDate = DateTime.Now;
                            infratribus.Memo = CurrentTbl63Infratribus.Memo;     
         
                        }
                    }
                    else
                    {
                        _tbl63InfratribussesRepository.Add(new Tbl63Infratribus     //add new
                        {   
InfratribusName = CurrentTbl63Infratribus.InfratribusName,
                            SubtribusID = CurrentTbl63Infratribus.SubtribusID,    
          
                            CountID = RandomHelper.Randomnumber(),
                            Valid = CurrentTbl63Infratribus.Valid,
                            ValidYear = CurrentTbl63Infratribus.ValidYear,
                            Synonym = CurrentTbl63Infratribus.Synonym,
                            Author = CurrentTbl63Infratribus.Author,
                            AuthorYear = CurrentTbl63Infratribus.AuthorYear,
                            Info = CurrentTbl63Infratribus.Info,
                            EngName = CurrentTbl63Infratribus.EngName,
                            GerName = CurrentTbl63Infratribus.GerName,
                            FraName = CurrentTbl63Infratribus.FraName,
                            PorName = CurrentTbl63Infratribus.PorName,
                            Writer = Environment.UserName,
                            WriterDate = DateTime.Now,
                            Updater = Environment.UserName,
                            UpdaterDate = DateTime.Now,
                            Memo = CurrentTbl63Infratribus.Memo    
        
                        });
                    }
                    {    
         
                        //SubtribusID may be not 0
                        if (CurrentTbl63Infratribus.SubtribusID == 0)   
          
                        {
                            WpfMessageBox.Show(CultRes.StringsRes.RequiredGenealogyConnect, CultRes.StringsRes.RequiredInput,
                                MessageBoxButton.OK, MessageBoxImage.Information);
                            return;
                        }   
          
                        //check if dataset with Name and vb-name already exist       
                        var dataset = new ObservableCollection<Tbl63Infratribus>
                        (from a in _tbl63InfratribussesRepository.GetAll()
                         where
                         a.InfratribusName.Trim() == CurrentTbl63Infratribus.InfratribusName.Trim() &&   
          
                         a.SubtribusID == CurrentTbl63Infratribus.SubtribusID         
                         select a);

                        if (dataset.Count != 0 && CurrentTbl63Infratribus.InfratribusID == 0)  //dataset exist
                        {       
         
                            WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTbl63Infratribus.InfratribusName,
                            MessageBoxButton.OK, MessageBoxImage.Information);     
             
                        }
                        if (dataset.Count == 0 && CurrentTbl63Infratribus.InfratribusID == 0 ||
                            dataset.Count != 0 && CurrentTbl63Infratribus.InfratribusID != 0 ||
                            dataset.Count == 0 && CurrentTbl63Infratribus.InfratribusID != 0) //new dataset and update
                        {    
             
                            if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTbl63Infratribus.InfratribusName,
                                    MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
                                return;  
               
                            {
                                _tbl63InfratribussesRepository.Save();     
             
                                WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess, CurrentTbl63Infratribus.InfratribusName,
                                    MessageBoxButton.OK, MessageBoxImage.Information);      
                   
                            }
                        }

                        if (SearchInfratribusName == null && CurrentTbl63Infratribus.InfratribusID == 0)  //new Dataset                        
                            Tbl63InfratribussesList = _allListVm.GetValueTbl63InfratribussesList();  //last Dataset
                        if (SearchInfratribusName == null && CurrentTbl63Infratribus.InfratribusID != 0)   //update 
                            Tbl63InfratribussesList = _allListVm.GetValueTbl63InfratribussesList(CurrentTbl63Infratribus.InfratribusID);
                        if (SearchInfratribusName != null && CurrentTbl63Infratribus.InfratribusID == 0)  //new Dataset                        
                            Tbl63InfratribussesList = _allListVm.GetValueTbl63InfratribussesList();  //last Dataset
                        if (SearchInfratribusName != null && CurrentTbl63Infratribus.InfratribusID != 0)   //update 
                            Tbl63InfratribussesList = _allListVm.GetValueTbl63InfratribussesList(CurrentTbl63Infratribus.InfratribusID);     
InfratribussesView = CollectionViewSource.GetDefaultView(Tbl63InfratribussesList);
                        InfratribussesView.Refresh();

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

           
        #region "Public Commands Connect <== Tbl60Subtribus"                 

        private RelayCommand _getSubtribusByNameOrIdCommand;     
    
        public ICommand GetSubtribusByNameOrIdCommand    
           
        {
            get { return _getSubtribusByNameOrIdCommand ?? (_getSubtribusByNameOrIdCommand = new RelayCommand(delegate { GetSubtribusByNameOrId(null); })); }
        }

        private void GetSubtribusByNameOrId(object o)    
        {

            int id;
            if (int.TryParse(SearchSubtribusName, out id))
                Tbl60SubtribussesList = new ObservableCollection<Tbl60Subtribus> { _tbl60SubtribussesRepository.Get(id) };
            else
                Tbl60SubtribussesList = _allListVm.GetValueTbl60SubtribussesList(SearchSubtribusName);

            SubtribussesView = CollectionViewSource.GetDefaultView(Tbl60SubtribussesList);
            SubtribussesView.Refresh();
        }

        //------------------------------------------------------------------------------------                
           
        private RelayCommand _addSubtribusCommand;      
    
        public ICommand AddSubtribusCommand    
    
        {
            get { return _addSubtribusCommand ?? (_addSubtribusCommand = new RelayCommand(delegate { AddSubtribus(null); })); }
        }

        private void AddSubtribus(object o)
        {
            Tbl60SubtribussesList = new ObservableCollection<Tbl60Subtribus>();   
Tbl60SubtribussesList.Insert(0, new Tbl60Subtribus{ SubtribusName = CultRes.StringsRes.DatasetNew });   

            Tbl57TribussesAllList?.Clear();
            Tbl57TribussesAllList = _allListVm.GetValueTbl57TribussesAllList();    
SubtribussesView = CollectionViewSource.GetDefaultView(Tbl60SubtribussesList);
            SubtribussesView.Refresh();
        }
        //------------------------------------------------------------------------------------                
           
        private RelayCommand _copySubtribusCommand;            
    
        public ICommand CopySubtribusCommand          
         
        {
            get { return _copySubtribusCommand ?? (_copySubtribusCommand = new RelayCommand(delegate { CopySubtribus(null); })); }
        }

        private void CopySubtribus(object o)
        {
            Tbl60SubtribussesList = new ObservableCollection<Tbl60Subtribus>();

            var subtribus = _tbl60SubtribussesRepository.Get(CurrentTbl60Subtribus.SubtribusID);

            Tbl60SubtribussesList.Insert(0, new Tbl60Subtribus
            {                 
TribusID = subtribus.TribusID,
                SubtribusName = CultRes.StringsRes.DatasetNew,
                Valid = subtribus.Valid,
                ValidYear = subtribus.ValidYear,
                Synonym = subtribus.Synonym,
                Author = subtribus.Author,
                AuthorYear = subtribus.AuthorYear,
                Info = subtribus.Info,
                EngName = subtribus.EngName,
                GerName = subtribus.GerName,
                FraName = subtribus.FraName,
                PorName = subtribus.PorName,
                Memo = subtribus.Memo         
        
            });

            SubtribussesView = CollectionViewSource.GetDefaultView(Tbl60SubtribussesList);
            SubtribussesView.Refresh();
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
                var subtribus = _tbl60SubtribussesRepository.Get(CurrentTbl60Subtribus.SubtribusID);
                if (subtribus!= null)
                {  
         
                    if (WpfMessageBox.Show(CultRes.StringsRes.DeleteQuestion1, CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl60Subtribus.SubtribusName,
                         MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes) 
                    return;

                    _tbl60SubtribussesRepository.Delete(subtribus);
                    _tbl60SubtribussesRepository.Save();

                    WpfMessageBox.Show(CultRes.StringsRes.DeleteSuccess, CurrentTbl60Subtribus.SubtribusName, 
                        MessageBoxButton.OK, MessageBoxImage.Information);  
         
                        if (SearchSubtribusName == null)                       
                            GetConnectedTablesById(o); //refresh doubleClick                                          
                        else
                        {
                        Tbl60SubtribussesList = _allListVm.GetValueTbl60SubtribussesList(SearchSubtribusName);  
    
                    }
                            SubtribussesView = CollectionViewSource.GetDefaultView(Tbl60SubtribussesList);
                            SubtribussesView.Refresh();
                }
                else
                {   
    
                    WpfMessageBox.Show(CultRes.StringsRes.Information, CultRes.StringsRes.DeleteCan + " " + CurrentTbl60Subtribus.SubtribusName+ " " + CultRes.StringsRes.DeleteCan1,
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
           
        private RelayCommand _saveSubtribusCommand;              
    
        public ICommand SaveSubtribusCommand             
         
        {
            get { return _saveSubtribusCommand ?? (_saveSubtribusCommand = new RelayCommand(delegate { SaveSubtribus(null); })); }
        }

        private void SaveSubtribus(object o)
        {
            try
            {
                var subtribus = _tbl60SubtribussesRepository.Get(CurrentTbl60Subtribus.SubtribusID);
                if (CurrentTbl60Subtribus == null)              
                    WpfMessageBox.Show(CultRes.StringsRes.Warning, CultRes.StringsRes.DatasetNotExist, 
                       MessageBoxButton.OK, MessageBoxImage.Warning);               
                else
                {
                    if (CurrentTbl60Subtribus.SubtribusID!= 0)
                    {
                        if (subtribus!= null) //update
                        {   
subtribus.SubtribusName= CurrentTbl60Subtribus.SubtribusName; 
                            subtribus.TribusID = CurrentTbl60Subtribus.TribusID;              
subtribus.Valid = CurrentTbl60Subtribus.Valid;
                            subtribus.ValidYear = CurrentTbl60Subtribus.ValidYear;
                            subtribus.Synonym = CurrentTbl60Subtribus.Synonym;
                            subtribus.Author = CurrentTbl60Subtribus.Author;
                            subtribus.AuthorYear = CurrentTbl60Subtribus.AuthorYear;
                            subtribus.Info = CurrentTbl60Subtribus.Info;
                            subtribus.EngName = CurrentTbl60Subtribus.EngName;
                            subtribus.GerName = CurrentTbl60Subtribus.GerName;
                            subtribus.FraName = CurrentTbl60Subtribus.FraName;
                            subtribus.PorName = CurrentTbl60Subtribus.PorName;
                            subtribus.Updater = Environment.UserName;
                            subtribus.UpdaterDate = DateTime.Now; 
                            subtribus.Memo = CurrentTbl60Subtribus.Memo;   
         
                        }
                    }
                    else
                    {
                        _tbl60SubtribussesRepository.Add(new Tbl60Subtribus     //add new
                        {   
SubtribusName= CurrentTbl60Subtribus.SubtribusName,              
                            TribusID = CurrentTbl60Subtribus.TribusID,     
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
                        //TribusID may be not 0
                        if (CurrentTbl60Subtribus.TribusID == 0)
                        {
                            WpfMessageBox.Show(CultRes.StringsRes.RequiredGenealogyConnect, CultRes.StringsRes.RequiredInput,
                                MessageBoxButton.OK, MessageBoxImage.Information);
                            return;
                        }     
           
                        //check if dataset with Name and vb-name already exist       
                        var dataset = new ObservableCollection<Tbl60Subtribus>
                        (from a in _tbl60SubtribussesRepository.GetAll()
                         where
                         a.SubtribusName.Trim() == CurrentTbl60Subtribus.SubtribusName.Trim() &&
                         a.TribusID == CurrentTbl60Subtribus.TribusID
                         select a);     
           
                        if (dataset.Count != 0 && CurrentTbl60Subtribus.SubtribusID == 0)  //dataset exist
                        {
                            WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTbl60Subtribus.SubtribusName,
                            MessageBoxButton.OK, MessageBoxImage.Information);
                        }   
           
                        if (dataset.Count == 0 && CurrentTbl60Subtribus.SubtribusID == 0 ||
                            dataset.Count != 0 && CurrentTbl60Subtribus.SubtribusID != 0 ||
                            dataset.Count == 0 && CurrentTbl60Subtribus.SubtribusID != 0) //new dataset and update
                        {  
           
                            if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTbl60Subtribus.SubtribusName,
                                    MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
                                return;
                            {
                                _tbl60SubtribussesRepository.Save();

                                WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess, CurrentTbl60Subtribus.SubtribusName,
                                    MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                        }   
           
                        if (SearchSubtribusName == null && CurrentTbl60Subtribus.SubtribusID == 0)  //new Dataset                        
                            GetConnectedTablesById(o); //refresh doubleClick                                          
                        if (SearchSubtribusName == null && CurrentTbl60Subtribus.SubtribusID != 0)   //update 
                            Tbl60SubtribussesList = _allListVm.GetValueTbl60SubtribussesList(CurrentTbl60Subtribus.SubtribusID);
                        if (SearchSubtribusName != null && CurrentTbl60Subtribus.SubtribusID == 0)  //new Dataset                        
                            GetConnectedTablesById(o); //refresh doubleClick                                          
                        if (SearchSubtribusName != null && CurrentTbl60Subtribus.SubtribusID != 0)   //update 
                            Tbl60SubtribussesList = _allListVm.GetValueTbl60SubtribussesList(CurrentTbl60Subtribus.SubtribusID);   
SubtribussesView = CollectionViewSource.GetDefaultView(Tbl60SubtribussesList);
                        SubtribussesView.Refresh();

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

 

 //    Part 4    

           

        #region "Public Commands Connect ==> Tbl66Genus"               

        private RelayCommand _getGenusByNameOrIdCommand;

        public ICommand GetGenusByNameOrIdCommand

        {
            get { return _getGenusByNameOrIdCommand ?? (_getGenusByNameOrIdCommand = new RelayCommand(delegate { GetGenusByNameOrId(null); })); }
        }

        private void GetGenusByNameOrId(object o)    
        {

            int id;
            if (int.TryParse(SearchGenusName, out id))
                Tbl66GenussesList = new ObservableCollection<Tbl66Genus> { _tbl66GenussesRepository.Get(id) };
            else
                Tbl66GenussesList = _allListVm.GetValueTbl66GenussesList(SearchGenusName);

            GenussesView = CollectionViewSource.GetDefaultView(Tbl66GenussesList);
            GenussesView.Refresh();
        }

        //------------------------------------------------------------------------------------                
             
        private RelayCommand _addGenusCommand;

        public ICommand AddGenusCommand

        {
            get { return _addGenusCommand ?? (_addGenusCommand = new RelayCommand(delegate { AddGenus(null); })); }
        }

        private void AddGenus(object o)
        {
            Tbl66GenussesList = new ObservableCollection<Tbl66Genus>();
            Tbl66GenussesList.Insert(0, new Tbl66Genus{ GenusName = CultRes.StringsRes.DatasetNew });

            Tbl63InfratribussesAllList?.Clear();
            Tbl63InfratribussesAllList = _allListVm.GetValueTbl63InfratribussesAllList();

            GenussesView = CollectionViewSource.GetDefaultView(Tbl66GenussesList);
            GenussesView.Refresh();
        }
        //------------------------------------------------------------------------------------                
              
        private RelayCommand _copyGenusCommand;

        public ICommand CopyGenusCommand

        {
            get { return _copyGenusCommand ?? (_copyGenusCommand = new RelayCommand(delegate { CopyGenus(null); })); }
        }

        private void CopyGenus(object o)
        {
            Tbl66GenussesList = new ObservableCollection<Tbl66Genus>();

            var genus = _tbl66GenussesRepository.Get(CurrentTbl66Genus.GenusID);

            Tbl66GenussesList.Insert(0, new Tbl66Genus
            {    
InfratribusID = genus.InfratribusID,
                GenusName = CultRes.StringsRes.DatasetNew,     
                Valid = genus.Valid,
                ValidYear = genus.ValidYear,
                Synonym = genus.Synonym,
                Author = genus.Author,
                AuthorYear = genus.AuthorYear,
                Info = genus.Info,
                EngName = genus.EngName,
                GerName = genus.GerName,
                FraName = genus.FraName,
                PorName = genus.PorName,
                Memo = genus.Memo         
                                   
            });

            GenussesView = CollectionViewSource.GetDefaultView(Tbl66GenussesList);
            GenussesView.Refresh();
        }
        //---------------------------------------------------------------------------------------    
                                           
        private RelayCommand _deleteGenusCommand;              
                                           
        public ICommand DeleteGenusCommand             
                                                
        {
            get { return _deleteGenusCommand ?? (_deleteGenusCommand = new RelayCommand(delegate { DeleteGenus(null); })); }
        }

        private void DeleteGenus(object o)
        {
            try
            {
                var genus = _tbl66GenussesRepository.Get(CurrentTbl66Genus.GenusID);
                if (genus!= null)
                {  
                                                    
                    if (WpfMessageBox.Show(CultRes.StringsRes.DeleteQuestion1, CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl66Genus.GenusName,
                         MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes) 
                    return;

                    _tbl66GenussesRepository.Delete(genus);
                    _tbl66GenussesRepository.Save();

                    WpfMessageBox.Show(CultRes.StringsRes.DeleteSuccess, CurrentTbl66Genus.GenusName,
                       MessageBoxButton.OK, MessageBoxImage.Information);  
                                                        
                        if (SearchGenusName == null)                       
                            GetConnectedTablesById(o); //refresh doubleClick                                          
                        else
                        {
                        Tbl66GenussesList = _allListVm.GetValueTbl66GenussesList(SearchGenusName);  
                                                         
                    }
                            GenussesView = CollectionViewSource.GetDefaultView(Tbl66GenussesList);
                            GenussesView.Refresh();
                }
                else
                {   
                                                          
                    WpfMessageBox.Show(CultRes.StringsRes.Information, CultRes.StringsRes.DeleteCan + " " + CurrentTbl66Genus.GenusName+ " " + CultRes.StringsRes.DeleteCan1,
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
               
        private RelayCommand _saveGenusCommand;

        public ICommand SaveGenusCommand

        {
            get { return _saveGenusCommand ?? (_saveGenusCommand = new RelayCommand(delegate { SaveGenus(null); })); }
        }

        private void SaveGenus(object o)
        {
            try
            {
                var genus = _tbl66GenussesRepository.Get(CurrentTbl66Genus.GenusID);
                if (CurrentTbl66Genus == null)
                    WpfMessageBox.Show(CultRes.StringsRes.Warning, CultRes.StringsRes.DatasetNotExist,
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                else
                {
                    if (CurrentTbl66Genus.GenusID != 0)   
                    {
                        if (genus != null) //update
                        {   
genus.GenusName = CurrentTbl66Genus.GenusName;
                            genus.InfratribusID = CurrentTbl63Infratribus.InfratribusID;   
                            genus.Valid = CurrentTbl66Genus.Valid;
                            genus.ValidYear = CurrentTbl66Genus.ValidYear;
                            genus.Synonym = CurrentTbl66Genus.Synonym;
                            genus.Author = CurrentTbl66Genus.Author;
                            genus.AuthorYear = CurrentTbl66Genus.AuthorYear;
                            genus.Info = CurrentTbl66Genus.Info;
                            genus.EngName = CurrentTbl66Genus.EngName;
                            genus.GerName = CurrentTbl66Genus.GerName;
                            genus.FraName = CurrentTbl66Genus.FraName;
                            genus.PorName = CurrentTbl66Genus.PorName;
                            genus.Updater = Environment.UserName;
                            genus.UpdaterDate = DateTime.Now;
                            genus.Memo = CurrentTbl66Genus.Memo;
                        }
                    }
                    else
                    {
                        _tbl66GenussesRepository.Add(new Tbl66Genus     //add new
                        {
                            GenusName = CurrentTbl66Genus.GenusName,
                            InfratribusID = CurrentTbl63Infratribus.InfratribusID,
                            CountID = RandomHelper.Randomnumber(),
                            Valid = CurrentTbl66Genus.Valid,
                            ValidYear = CurrentTbl66Genus.ValidYear,
                            Synonym = CurrentTbl66Genus.Synonym,
                            Author = CurrentTbl66Genus.Author,
                            AuthorYear = CurrentTbl66Genus.AuthorYear,
                            Info = CurrentTbl66Genus.Info,
                            EngName = CurrentTbl66Genus.EngName,
                            GerName = CurrentTbl66Genus.GerName,
                            FraName = CurrentTbl66Genus.FraName,
                            PorName = CurrentTbl66Genus.PorName,
                            Writer = Environment.UserName,
                            WriterDate = DateTime.Now,
                            Updater = Environment.UserName,
                            UpdaterDate = DateTime.Now,
                            Memo = CurrentTbl66Genus.Memo                
           
                        });
                    }
                    {
                        //InfratribusID may be not 0
                        if (CurrentTbl63Infratribus.InfratribusID == 0)
                        {
                            WpfMessageBox.Show(CultRes.StringsRes.RequiredGenealogyConnect, CultRes.StringsRes.RequiredInput,
                                MessageBoxButton.OK, MessageBoxImage.Information);
                            return;
                        }

                        //check if dataset with Name and vb-name already exist       
                        var dataset = new ObservableCollection<Tbl66Genus>
                        (from a in _tbl66GenussesRepository.GetAll()
                         where
                         a.GenusName.Trim() == CurrentTbl66Genus.GenusName.Trim() &&
                         a.InfratribusID == CurrentTbl63Infratribus.InfratribusID
                         select a); 
        
                        if (dataset.Count != 0 && CurrentTbl66Genus.GenusID == 0)  //dataset exist
                        {
                            WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTbl66Genus.GenusName,
                            MessageBoxButton.OK, MessageBoxImage.Information);
                        }      
        
                        if (dataset.Count == 0 && CurrentTbl66Genus.GenusID == 0 ||
                            dataset.Count != 0 && CurrentTbl66Genus.GenusID != 0 ||
                            dataset.Count == 0 && CurrentTbl66Genus.GenusID != 0) //new dataset and update
                        { 
        
                            if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTbl66Genus.GenusName,
                                    MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
                                return;
                            {
                                _tbl66GenussesRepository.Save();

                                WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess, CurrentTbl66Genus.GenusName,
                                    MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                        } 
        
                        if (SearchGenusName == null && CurrentTbl66Genus.GenusID == 0)  //new Dataset                        
                            GetConnectedTablesById(o); //refresh doubleClick                                          
                        if (SearchGenusName == null && CurrentTbl66Genus.GenusID != 0)   //update 
                            Tbl66GenussesList = _allListVm.GetValueTbl66GenussesList(CurrentTbl66Genus.GenusID);
                        if (SearchGenusName != null && CurrentTbl66Genus.GenusID == 0)  //new Dataset                        
                            GetConnectedTablesById(o); //refresh doubleClick                                          
                        if (SearchGenusName != null && CurrentTbl66Genus.GenusID != 0)   //update 
                            Tbl66GenussesList = _allListVm.GetValueTbl66GenussesList(CurrentTbl66Genus.GenusID); 
GenussesView = CollectionViewSource.GetDefaultView(Tbl66GenussesList);
                        GenussesView.Refresh();
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

           Tbl63InfratribussesAllList?.Clear();
           Tbl63InfratribussesAllList = _allListVm.GetValueTbl63InfratribussesAllList();
           Tbl90AuthorsAllList?.Clear();
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
InfratribusID = refAuthor.InfratribusID,              
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
         
                            refAuthor.InfratribusID = CurrentTbl63Infratribus.InfratribusID;  
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
InfratribusID = CurrentTbl63Infratribus.InfratribusID,              
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
                        //InfratribusID may be not 0
                        if (CurrentTbl63Infratribus.InfratribusID == 0 || CurrentTbl90RefAuthor.RefAuthorID == 0)
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
                         a.InfratribusID == CurrentTbl63Infratribus.InfratribusID &&
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

            Tbl63InfratribussesAllList?.Clear();  
            Tbl63InfratribussesAllList = _allListVm.GetValueTbl63InfratribussesAllList();
            Tbl90SourcesAllList?.Clear();
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
InfratribusID = refSource.InfratribusID,              
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
         
                            refSource.InfratribusID = CurrentTbl63Infratribus.InfratribusID;            
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
InfratribusID = CurrentTbl63Infratribus.InfratribusID,              
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
                        //InfratribusID may be not 0
                        if (CurrentTbl63Infratribus.InfratribusID == 0 || CurrentTbl90RefSource.RefSourceID == 0)
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
                         a.InfratribusID == CurrentTbl63Infratribus.InfratribusID &&
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
                            GetConnectedTablesById(o); //refresh doubleClick                                          
                        if (SearchRefSourceName == null && CurrentTbl90RefSource.ReferenceID != 0)   //update
                            Tbl90RefSourcesList = _allListVm.GetValueTbl90RefSourcesList(CurrentTbl90RefSource.ReferenceID);
                        if (SearchRefSourceName != null && CurrentTbl90RefSource.ReferenceID == 0)  //new Dataset
                            GetConnectedTablesById(o); //refresh doubleClick                                          
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

            Tbl63InfratribussesAllList?.Clear();  
            Tbl63InfratribussesAllList = _allListVm.GetValueTbl63InfratribussesAllList();
            Tbl90ExpertsAllList?.Clear();
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
InfratribusID = refExpert.InfratribusID,              
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
      
                            refExpert.InfratribusID = CurrentTbl63Infratribus.InfratribusID;           
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
InfratribusID = CurrentTbl63Infratribus.InfratribusID,              
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
                        //InfratribusID may be not 0
                        if (CurrentTbl63Infratribus.InfratribusID == 0 || CurrentTbl90RefExpert.RefExpertID == 0)   
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
                         a.InfratribusID == CurrentTbl63Infratribus.InfratribusID &&
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
                            GetConnectedTablesById(o); //refresh doubleClick                                          
                        if (SearchRefExpertName == null && CurrentTbl90RefExpert.ReferenceID != 0)   //update
                            Tbl90RefExpertsList = _allListVm.GetValueTbl90RefExpertsList(CurrentTbl90RefExpert.ReferenceID);
                        if (SearchRefExpertName != null && CurrentTbl90RefExpert.ReferenceID == 0)  //new Dataset
                            GetConnectedTablesById(o); //refresh doubleClick                                          
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

            Tbl63InfratribussesAllList?.Clear();  
            Tbl63InfratribussesAllList = _allListVm.GetValueTbl63InfratribussesAllList();      
    

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
InfratribusID = comment.InfratribusID,              
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
      
                            comment.InfratribusID = CurrentTbl63Infratribus.InfratribusID;            
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
InfratribusID = CurrentTbl63Infratribus.InfratribusID,              
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
                        //InfratribusID may be not 0
                        if (CurrentTbl63Infratribus.InfratribusID == 0)
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
                         a.InfratribusID == CurrentTbl63Infratribus.InfratribusID
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
                            GetConnectedTablesById(o); //refresh doubleClick                                          
                        if (SearchCommentInfo == null && CurrentTbl93Comment.CommentID != 0)   //update
                            Tbl93CommentsList = _allListVm.GetValueTbl93CommentsList(CurrentTbl93Comment.CommentID);
                        if (SearchCommentInfo != null && CurrentTbl93Comment.CommentID == 0)  //new Dataset
                            GetConnectedTablesById(o); //refresh doubleClick                                          
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
            Tbl63InfratribussesAllList?.Clear();
            Tbl63InfratribussesAllList = _allListVm.GetValueTbl63InfratribussesAllList();

            Tbl60SubtribussesList =  new ObservableCollection<Tbl60Subtribus>
                 (from x in _tbl60SubtribussesRepository.GetAll()
                 where x.SubtribusID == CurrentTbl63Infratribus.SubtribusID
                  orderby x.SubtribusName
                  select x);

            SubtribussesView = CollectionViewSource.GetDefaultView(Tbl60SubtribussesList);
            SubtribussesView.Refresh();

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

        public new int SelectedMainTabIndex
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
                if (_selectedMainTabIndex == 2)
                {
                    SelectedDetailTabIndex = 1;
                    SelectedDetailSubTabIndex = 2;
                }
                if (_selectedMainTabIndex == 3)
                {
                    SelectedDetailTabIndex = 1;
                    SelectedDetailSubTabIndex = 3;
                }
            }
        }

        public new int SelectedMainSubRefTabIndex
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

        public new int SelectedDetailTabIndex
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
                }
                if (_selectedDetailTabIndex == 1)                
                    SelectedDetailSubTabIndex = 1;                
                if (_selectedDetailTabIndex == 2)                
                    SelectedDetailSubTabIndex = 2;               
                if (_selectedDetailTabIndex == 3)
                    SelectedDetailSubTabIndex = 3;
            }
        }

        public new int SelectedDetailSubTabIndex
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
                    Tbl63InfratribussesAllList?.Clear();
                    Tbl63InfratribussesAllList = _allListVm.GetValueTbl63InfratribussesAllList();

                    Tbl66GenussesList?.Clear();
                    Tbl66GenussesList =  new ObservableCollection<Tbl66Genus>
                              (from x in _tbl66GenussesRepository.GetAll()
                              where x.InfratribusID == CurrentTbl63Infratribus.InfratribusID
                              orderby x.GenusName
                               select x);

                    GenussesView = CollectionViewSource.GetDefaultView(Tbl66GenussesList);
                    GenussesView.Refresh();

                    SelectedMainTabIndex = 1;
                }
                if (_selectedDetailSubTabIndex == 2)
                {
                    Tbl63InfratribussesAllList?.Clear();
                    Tbl63InfratribussesAllList = _allListVm.GetValueTbl63InfratribussesAllList();
                    Tbl90ExpertsAllList?.Clear();
                    Tbl90ExpertsAllList = _allListVm.GetValueTbl90ExpertsAllList();

                    Tbl90RefExpertsList?.Clear();
                    Tbl90RefExpertsList = new ObservableCollection<Tbl90Reference>
                    (from refEx in _tbl90ReferencesRepository.GetAll()
                     where refEx.InfratribusID == CurrentTbl63Infratribus.InfratribusID
                           && refEx.RefAuthorID == null
                           && refEx.RefSourceID == null
                     orderby refEx.Tbl90RefExperts.RefExpertName
                     select refEx);

                    RefExpertsView = CollectionViewSource.GetDefaultView(Tbl90RefExpertsList);
                    RefExpertsView.Refresh();

                    SelectedMainTabIndex = 2;
                }
                if (_selectedDetailSubTabIndex == 3)
                {
                    Tbl63InfratribussesAllList?.Clear();
                    Tbl63InfratribussesAllList = _allListVm.GetValueTbl63InfratribussesAllList();

                    Tbl93CommentsList?.Clear();
                    Tbl93CommentsList = new ObservableCollection<Tbl93Comment>
                    (from comm in _tbl93CommentsRepository.GetAll()
                     where comm.InfratribusID == CurrentTbl63Infratribus.InfratribusID
                     orderby comm.Info
                     select comm);

                    CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
                    CommentsView.Refresh();

                    SelectedMainTabIndex = 3;
                }
            }
        }

        public new int SelectedDetailSubRefTabIndex
        {
            get => _selectedDetailSubRefTabIndex;
            set
            {
                if (value == _selectedDetailSubRefTabIndex) return;
                _selectedDetailSubRefTabIndex = value;
                RaisePropertyChanged();
                if (_selectedDetailSubRefTabIndex == 0)
                {
                    Tbl90ExpertsAllList?.Clear();
                    Tbl90ExpertsAllList = _allListVm.GetValueTbl90ExpertsAllList();
                    Tbl63InfratribussesAllList?.Clear();
                    Tbl63InfratribussesAllList = _allListVm.GetValueTbl63InfratribussesAllList();

                    Tbl90RefExpertsList?.Clear();
                    Tbl90RefExpertsList = new ObservableCollection<Tbl90Reference>
                    (from refEx in _tbl90ReferencesRepository.GetAll()
                     where refEx.InfratribusID == CurrentTbl63Infratribus.InfratribusID
                           && refEx.RefAuthorID == null
                           && refEx.RefSourceID == null
                     orderby refEx.Tbl90RefExperts.RefExpertName
                     select refEx);

                    RefExpertsView = CollectionViewSource.GetDefaultView(Tbl90RefExpertsList);
                    RefExpertsView.Refresh();

                    SelectedMainSubRefTabIndex = 0;
                }
                if (_selectedDetailSubRefTabIndex == 1)
                {
                    Tbl90SourcesAllList?.Clear();
                    Tbl90SourcesAllList = _allListVm.GetValueTbl90SourcesAllList();
                    Tbl63InfratribussesAllList?.Clear();
                    Tbl63InfratribussesAllList = _allListVm.GetValueTbl63InfratribussesAllList();

                    Tbl90RefSourcesList?.Clear();
                    Tbl90RefSourcesList = new ObservableCollection<Tbl90Reference>
                    (from refSo in _tbl90ReferencesRepository.GetAll()
                     where refSo.InfratribusID == CurrentTbl63Infratribus.InfratribusID
                           && refSo.RefExpertID == null
                           && refSo.RefAuthorID == null
                     orderby refSo.Tbl90RefSources.RefSourceName
                     select refSo);

                    RefSourcesView = CollectionViewSource.GetDefaultView(Tbl90RefSourcesList);
                    RefSourcesView.Refresh();

                    SelectedMainSubRefTabIndex = 1;
                }
                if (_selectedDetailSubRefTabIndex == 2)
                {
                    Tbl90AuthorsAllList?.Clear();
                    Tbl90AuthorsAllList = _allListVm.GetValueTbl90AuthorsAllList();
                    Tbl63InfratribussesAllList?.Clear();
                    Tbl63InfratribussesAllList = _allListVm.GetValueTbl63InfratribussesAllList();

                    Tbl90RefAuthorsList?.Clear();
                    Tbl90RefAuthorsList = new ObservableCollection<Tbl90Reference>
                    (from refAu in _tbl90ReferencesRepository.GetAll()
                     where refAu.InfratribusID == CurrentTbl63Infratribus.InfratribusID
                           && refAu.RefExpertID == null
                           && refAu.RefSourceID == null
                     orderby refAu.Tbl90RefAuthors.RefAuthorName, refAu.Tbl90RefAuthors.BookName, refAu
                         .Tbl90RefAuthors.Page1
                     select refAu);

                    RefAuthorsView = CollectionViewSource.GetDefaultView(Tbl90RefAuthorsList);
                    RefAuthorsView.Refresh();

                    SelectedMainSubRefTabIndex = 2;
                }
            }
        }

        #endregion "Public Commands to open Detail TabItems"
 




   }
}   
