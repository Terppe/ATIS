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

    
         //    Tbl18SuperclassesViewModel Skriptdatum:  04.11.2020  12:32    

namespace WPFUI.Views.Database
{     
    
    public class Tbl18SuperclassesViewModel : Tbl03RegnumsViewModel
    {     
        
        #region "Private Data Members"

        private readonly AllListVm _allListVm = new AllListVm();
        private readonly Repository<Tbl18Superclass, int> _tbl18SuperclassesRepository = new Repository<Tbl18Superclass, int>();   
           
        private readonly Repository<Tbl12Subphylum, int> _tbl12SubphylumsRepository = new Repository<Tbl12Subphylum, int>();   
           
        private readonly Repository<Tbl15Subdivision, int> _tbl15SubdivisionsRepository = new Repository<Tbl15Subdivision, int>();   
           
        private readonly Repository<Tbl21Class, int> _tbl21ClassesRepository = new Repository<Tbl21Class, int>();   
           
        private readonly Repository<Tbl24Subclass, int> _tbl24SubclassesRepository = new Repository<Tbl24Subclass, int>();   
           
        private readonly Repository<Tbl06Phylum, int> _tbl06PhylumsRepository = new Repository<Tbl06Phylum, int>();   
           
        private readonly Repository<Tbl09Division, int> _tbl09DivisionsRepository = new Repository<Tbl09Division, int>();   
          
        private readonly Repository<Tbl90Reference, int> _tbl90ReferencesRepository = new Repository<Tbl90Reference, int>();
        private readonly Repository<Tbl93Comment, int> _tbl93CommentsRepository = new Repository<Tbl93Comment, int>();    

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

        private RelayCommand _getSuperclassByNameOrIdCommand;     
    
        public ICommand GetSuperclassByNameOrIdCommand    
    
        {
            get { return _getSuperclassByNameOrIdCommand ?? (_getSuperclassByNameOrIdCommand = new RelayCommand(delegate { GetSuperclassByNameOrId(null); })); }   
        }

        private void GetSuperclassByNameOrId(object o)       
        {   
    
            int id;
            if (int.TryParse(SearchSuperclassName, out id))
                Tbl18SuperclassesList = new ObservableCollection<Tbl18Superclass> { _tbl18SuperclassesRepository.Get(id) };
            else           
                Tbl18SuperclassesList = _allListVm.GetValueTbl18SuperclassesList(SearchSuperclassName);      
Tbl12SubphylumsAllList = _allListVm.GetValueTbl12SubphylumsAllList();      
  Tbl15SubdivisionsAllList = _allListVm.GetValueTbl15SubdivisionsAllList();      
  SuperclassesView = CollectionViewSource.GetDefaultView(Tbl18SuperclassesList);
            SuperclassesView.Refresh();
        }
        //------------------------------------------------------------------------------                
           
        private RelayCommand _addSuperclassCommand;           
    
        public ICommand AddSuperclassCommand       
    
        {
            get { return _addSuperclassCommand ?? (_addSuperclassCommand = new RelayCommand(delegate { AddSuperclass(null); })); }
        }

        private void AddSuperclass(object o)
        {
            Tbl18SuperclassesList = new ObservableCollection<Tbl18Superclass>();   
Tbl18SuperclassesList.Insert(0, new Tbl18Superclass{ SuperclassName= CultRes.StringsRes.DatasetNew });  

            Tbl12SubphylumsAllList = _allListVm.GetValueTbl12SubphylumsAllList();    
            Tbl15SubdivisionsAllList = _allListVm.GetValueTbl15SubdivisionsAllList();      
SuperclassesView = CollectionViewSource.GetDefaultView(Tbl18SuperclassesList);
            SuperclassesView.Refresh();
        }
        //---------------------------------------------------------------------------------------  
           
        private RelayCommand _copySuperclassCommand;              
    
        public ICommand CopySuperclassCommand             
         
        {
            get { return _copySuperclassCommand ?? (_copySuperclassCommand = new RelayCommand(delegate { CopySuperclass(null); })); }
        }

        private void CopySuperclass(object o)
        {
            Tbl18SuperclassesList = new ObservableCollection<Tbl18Superclass>();

            var superclass = _tbl18SuperclassesRepository.Get(CurrentTbl18Superclass.SuperclassID);

            Tbl18SuperclassesList.Insert(0, new Tbl18Superclass
            {                 
SubphylumID = superclass.SubphylumID,              
                            SubdivisionID = superclass.SubdivisionID,              
                            SuperclassName = CultRes.StringsRes.DatasetNew,              
                            Valid = superclass.Valid,
                            ValidYear = superclass.ValidYear,
                            Synonym = superclass.Synonym,
                            Author = superclass.Author,
                            AuthorYear = superclass.AuthorYear,
                            Info = superclass.Info,
                            EngName = superclass.EngName,
                            GerName = superclass.GerName,
                            FraName = superclass.FraName,
                            PorName = superclass.PorName,
                            Memo = superclass.Memo                            
        
            });

            SuperclassesView = CollectionViewSource.GetDefaultView(Tbl18SuperclassesList);
            SuperclassesView.Refresh();
        }
        //---------------------------------------------------------------------------------------                  
           
        private RelayCommand _deleteSuperclassCommand;              
    
        public ICommand DeleteSuperclassCommand             
         
        {
            get { return _deleteSuperclassCommand ?? (_deleteSuperclassCommand = new RelayCommand(delegate { DeleteSuperclass(null); })); }
        }

        private void DeleteSuperclass(object o)
        {
            try
            {
                var superclass = _tbl18SuperclassesRepository.Get(CurrentTbl18Superclass.SuperclassID);
                if (superclass!= null)
                {
                    if (WpfMessageBox.Show(CultRes.StringsRes.DeleteQuestion1, CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl18Superclass.SuperclassName,
                         MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes) 
                    return;
                    _tbl18SuperclassesRepository.Delete(superclass);
                    _tbl18SuperclassesRepository.Save();

                    WpfMessageBox.Show(CultRes.StringsRes.DeleteSuccess, CurrentTbl18Superclass.SuperclassName, 
                        MessageBoxButton.OK, MessageBoxImage.Information);

                    if (SearchSuperclassName == null)                   
                        GetConnectedTablesById(o); //refresh doubleClick                                       
                    else
                    {
                        Tbl18SuperclassesList = _allListVm.GetValueTbl18SuperclassesList(SearchSuperclassName); 
                    }    
SuperclassesView = CollectionViewSource.GetDefaultView(Tbl18SuperclassesList);
                         SuperclassesView.Refresh();
                }
                else
                {
                    WpfMessageBox.Show(CultRes.StringsRes.Information, CultRes.StringsRes.DeleteCan + " " + CurrentTbl18Superclass.SuperclassName+ " " + CultRes.StringsRes.DeleteCan1,
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
           
        private RelayCommand _saveSuperclassCommand;              
     
        public ICommand SaveSuperclassCommand             
         
        {
            get { return _saveSuperclassCommand ?? (_saveSuperclassCommand = new RelayCommand(delegate { SaveSuperclass(null); })); }
        }

        private void SaveSuperclass(object o)
        {
            try
            {
                var superclass = _tbl18SuperclassesRepository.Get(CurrentTbl18Superclass.SuperclassID);
                if (CurrentTbl18Superclass == null)              
                    WpfMessageBox.Show(CultRes.StringsRes.Warning, CultRes.StringsRes.DatasetNotExist, 
                        MessageBoxButton.OK, MessageBoxImage.Warning);               
                else
                {
                    if (CurrentTbl18Superclass.SuperclassID!= 0)
                    {
                        if (superclass!= null) //update
                        {   
superclass.SuperclassName = CurrentTbl18Superclass.SuperclassName;  
superclass.SubphylumID = CurrentTbl18Superclass.SubphylumID;
                            superclass.Valid = CurrentTbl18Superclass.Valid;
                            superclass.ValidYear = CurrentTbl18Superclass.ValidYear;
                            superclass.Synonym = CurrentTbl18Superclass.Synonym;
                            superclass.Author = CurrentTbl18Superclass.Author;
                            superclass.AuthorYear = CurrentTbl18Superclass.AuthorYear;
                            superclass.Info = CurrentTbl18Superclass.Info;
                            superclass.EngName = CurrentTbl18Superclass.EngName;
                            superclass.GerName = CurrentTbl18Superclass.GerName;
                            superclass.FraName = CurrentTbl18Superclass.FraName;
                            superclass.PorName = CurrentTbl18Superclass.PorName;
                            superclass.Updater = Environment.UserName;
                            superclass.UpdaterDate = DateTime.Now; 
                            superclass.Memo = CurrentTbl18Superclass.Memo;  
         
                        }
                    }
                    else
                    {
                        _tbl18SuperclassesRepository.Add(new Tbl18Superclass     //add new
                        {   
SubphylumID= CurrentTbl18Superclass.SubphylumID,              
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
                        //SubphylumID && SubdivisionID may be not 0
                        if (CurrentTbl18Superclass.SubphylumID == 0 && CurrentTbl18Superclass.SubdivisionID == 0)
                        {
                            WpfMessageBox.Show(CultRes.StringsRes.RequiredGenealogyConnect, CultRes.StringsRes.RequiredInput,
                                MessageBoxButton.OK, MessageBoxImage.Information);
                            return;
                        }

                        var dataset = new ObservableCollection<Tbl18Superclass>
                        (from a in _tbl18SuperclassesRepository.GetAll()
                         where
                         a.SuperclassName.Trim() == CurrentTbl18Superclass.SuperclassName.Trim() &&                
                         a.SubphylumID== CurrentTbl18Superclass.SubphylumID  &&
                         a.SubdivisionID== CurrentTbl18Superclass.SubdivisionID
                         select a);

                        if (dataset.Count != 0 && CurrentTbl18Superclass.SuperclassID== 0)  //dataset exist
                        {
                              WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTbl18Superclass.SuperclassName,
                              MessageBoxButton.OK, MessageBoxImage.Information);
                        }

                        if (dataset.Count == 0 && CurrentTbl18Superclass.SuperclassID== 0 ||
                            dataset.Count != 0 && CurrentTbl18Superclass.SuperclassID != 0  ||
                            dataset.Count == 0 && CurrentTbl18Superclass.SuperclassID != 0) //new dataset and update
                        {
                            if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTbl18Superclass.SuperclassName,
                                    MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)                            
                                return;                            
                            {
                                _tbl18SuperclassesRepository.Save();

                                WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess, CurrentTbl18Superclass.SuperclassName,
                                    MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                        }   
         
                        if (SearchSuperclassName == null && CurrentTbl18Superclass.SuperclassID == 0)  //new Dataset                        
                            Tbl18SuperclassesList = _allListVm.GetValueTbl18SuperclassesList();  //last Dataset
                        if (SearchSuperclassName == null && CurrentTbl18Superclass.SuperclassID != 0)   //update 
                            Tbl18SuperclassesList = _allListVm.GetValueTbl18SuperclassesList(CurrentTbl18Superclass.SuperclassID);
                        if (SearchSuperclassName != null && CurrentTbl18Superclass.SuperclassID == 0)  //new Dataset                        
                            Tbl18SuperclassesList = _allListVm.GetValueTbl18SuperclassesList();  //last Dataset
                        if (SearchSuperclassName != null && CurrentTbl18Superclass.SuperclassID != 0)   //update 
                            Tbl18SuperclassesList = _allListVm.GetValueTbl18SuperclassesList(CurrentTbl18Superclass.SuperclassID);

                            SuperclassesView = CollectionViewSource.GetDefaultView(Tbl18SuperclassesList);
                            SuperclassesView.Refresh();                          
         
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

           
        #region "Public Commands Connect <== Tbl12Subphylum"                 

        private RelayCommand _getSubphylumByNameOrIdCommand;     
    
        public ICommand GetSubphylumByNameOrIdCommand    
    
        {
            get { return _getSubphylumByNameOrIdCommand ?? (_getSubphylumByNameOrIdCommand = new RelayCommand(delegate { GetSubphylumByNameOrId(null); })); }   
        }

        private void GetSubphylumByNameOrId(object o)       
        {   
    
            int id;
            if (int.TryParse(SearchSubphylumName, out id))
                Tbl12SubphylumsList = new ObservableCollection<Tbl12Subphylum> { _tbl12SubphylumsRepository.Get(id) };
            else
                Tbl12SubphylumsList = _allListVm.GetValueTbl12SubphylumsList(SearchSubphylumName);     
SubphylumsView = CollectionViewSource.GetDefaultView(Tbl12SubphylumsList);
            SubphylumsView.Refresh();
        }
        //------------------------------------------------------------------------------                
           
        private RelayCommand _addSubphylumCommand;      
    
        public ICommand AddSubphylumCommand    
    
        {
            get { return _addSubphylumCommand ?? (_addSubphylumCommand = new RelayCommand(delegate { AddSubphylum(null); })); }
        }

        private void AddSubphylum(object o)
        {
            Tbl12SubphylumsList = new ObservableCollection<Tbl12Subphylum>();   
Tbl12SubphylumsList.Insert(0, new Tbl12Subphylum{ SubphylumName = CultRes.StringsRes.DatasetNew });   

            if (Tbl06PhylumsAllList == null)
            Tbl06PhylumsAllList = _allListVm.GetValueTbl06PhylumsAllList();    
SubphylumsView = CollectionViewSource.GetDefaultView(Tbl12SubphylumsList);
            SubphylumsView.Refresh();
        }
        //---------------------------------------------------------------------------------------  
           
        private RelayCommand _copySubphylumCommand;            
    
        public ICommand CopySubphylumCommand          
         
        {
            get { return _copySubphylumCommand ?? (_copySubphylumCommand = new RelayCommand(delegate { CopySubphylum(null); })); }
        }

        private void CopySubphylum(object o)
        {
            Tbl12SubphylumsList = new ObservableCollection<Tbl12Subphylum>();

            var subphylum = _tbl12SubphylumsRepository.Get(CurrentTbl12Subphylum.SubphylumID);

            Tbl12SubphylumsList.Insert(0, new Tbl12Subphylum
            {                 
                
                DivisionID = subphylum.DivisionID,
                SubphylumName = CultRes.StringsRes.DatasetNew,     
                Valid = subphylum.Valid,
                ValidYear = subphylum.ValidYear,
                Synonym = subphylum.Synonym,
                Author = subphylum.Author,
                AuthorYear = subphylum.AuthorYear,
                Info = subphylum.Info,
                EngName = subphylum.EngName,
                GerName = subphylum.GerName,
                FraName = subphylum.FraName,
                PorName = subphylum.PorName,
                Memo = subphylum.Memo           
        
            });

            SubphylumsView = CollectionViewSource.GetDefaultView(Tbl12SubphylumsList);
            SubphylumsView.Refresh();
        }
        //---------------------------------------------------------------------------------------                  
           
        private RelayCommand _deleteSubphylumCommand;              
    
        public ICommand DeleteSubphylumCommand             
         
        {
            get { return _deleteSubphylumCommand ?? (_deleteSubphylumCommand = new RelayCommand(delegate { DeleteSubphylum(null); })); }
        }

        private void DeleteSubphylum(object o)
        {
            try
            {
                var subphylum = _tbl12SubphylumsRepository.Get(CurrentTbl12Subphylum.SubphylumID);
                if (subphylum!= null)
                {  
         
                    if (WpfMessageBox.Show(CultRes.StringsRes.DeleteQuestion1, CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl12Subphylum.SubphylumName,
                         MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes) 
                    return;

                    _tbl12SubphylumsRepository.Delete(subphylum);
                    _tbl12SubphylumsRepository.Save();

                    WpfMessageBox.Show(CultRes.StringsRes.DeleteSuccess, CurrentTbl12Subphylum.SubphylumName, 
                        MessageBoxButton.OK, MessageBoxImage.Information);  
         
                        if (SearchSubphylumName == null)                       
                            GetConnectedTablesById(o); //refresh doubleClick                                          
                        else
                        {
                        Tbl12SubphylumsList = _allListVm.GetValueTbl12SubphylumsList(SearchSubphylumName);  
SubphylumsView = CollectionViewSource.GetDefaultView(Tbl12SubphylumsList);
                            SubphylumsView.Refresh();
                    }
                }
                else
                {   
    
                    WpfMessageBox.Show(CultRes.StringsRes.Information, CultRes.StringsRes.DeleteCan + " " + CurrentTbl12Subphylum.SubphylumName+ " " + CultRes.StringsRes.DeleteCan1,
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
           
        private RelayCommand _saveSubphylumCommand;              
    
        public ICommand SaveSubphylumCommand             
         
        {
            get { return _saveSubphylumCommand ?? (_saveSubphylumCommand = new RelayCommand(delegate { SaveSubphylum(null); })); }
        }

        private void SaveSubphylum(object o)
        {
            try
            {
                var subphylum = _tbl12SubphylumsRepository.Get(CurrentTbl12Subphylum.SubphylumID);
                if (CurrentTbl12Subphylum == null)              
                    WpfMessageBox.Show(CultRes.StringsRes.Warning, CultRes.StringsRes.DatasetNotExist, MessageBoxButton.OK, MessageBoxImage.Warning);               
                else
                {
                    if (CurrentTbl12Subphylum.SubphylumID!= 0)
                    {
                        if (subphylum!= null) //update
                        {   
subphylum.PhylumID = CurrentTbl12Subphylum.PhylumID;
                            subphylum.SubphylumName= CurrentTbl12Subphylum.SubphylumName;             
subphylum.Valid = CurrentTbl12Subphylum.Valid;
                            subphylum.ValidYear = CurrentTbl12Subphylum.ValidYear;
                            subphylum.Synonym = CurrentTbl12Subphylum.Synonym;
                            subphylum.Author = CurrentTbl12Subphylum.Author;
                            subphylum.AuthorYear = CurrentTbl12Subphylum.AuthorYear;
                            subphylum.Info = CurrentTbl12Subphylum.Info;
                            subphylum.EngName = CurrentTbl12Subphylum.EngName;
                            subphylum.GerName = CurrentTbl12Subphylum.GerName;
                            subphylum.FraName = CurrentTbl12Subphylum.FraName;
                            subphylum.PorName = CurrentTbl12Subphylum.PorName;
                            subphylum.Updater = Environment.UserName;
                            subphylum.UpdaterDate = DateTime.Now; 
                            subphylum.Memo = CurrentTbl12Subphylum.Memo;   
         
                        }
                    }
                    else
                    {
                        _tbl12SubphylumsRepository.Add(new Tbl12Subphylum     //add new
                        {   
                
                            DivisionID = CurrentTbl12Subphylum.DivisionID,              
                            SubphylumName = CurrentTbl12Subphylum.SubphylumName,              
                            CountID = RandomHelper.Randomnumber(),
                            Valid = CurrentTbl12Subphylum.Valid,
                            ValidYear = CurrentTbl12Subphylum.ValidYear,
                            Synonym = CurrentTbl12Subphylum.Synonym,
                            Author = CurrentTbl12Subphylum.Author,
                            AuthorYear = CurrentTbl12Subphylum.AuthorYear,
                            Info = CurrentTbl12Subphylum.Info,
                            EngName = CurrentTbl12Subphylum.EngName,
                            GerName = CurrentTbl12Subphylum.GerName,
                            FraName = CurrentTbl12Subphylum.FraName,
                            PorName = CurrentTbl12Subphylum.PorName,
                            Writer = Environment.UserName,
                            WriterDate = DateTime.Now,
                            Updater = Environment.UserName,
                            UpdaterDate = DateTime.Now,
                            Memo = CurrentTbl12Subphylum.Memo   
         
                        });
                    }
                    {
                        //PhylumID may be not 0
                        if (CurrentTbl12Subphylum.PhylumID == 0)
                        {
                            WpfMessageBox.Show(CultRes.StringsRes.RequiredGenealogyConnect, CultRes.StringsRes.RequiredInput,
                                MessageBoxButton.OK, MessageBoxImage.Information);
                            return;
                        }

                        //check if dataset with Name and vb-name already exist
                        var dataset = new ObservableCollection<Tbl12Subphylum>
                        (from a in _tbl12SubphylumsRepository.GetAll()
                         where
                         a.SubphylumName.Trim() == CurrentTbl12Subphylum.SubphylumName.Trim() &&                
                         a.PhylumID == CurrentTbl12Subphylum.PhylumID
                         select a);

                        if (dataset.Count != 0 && CurrentTbl12Subphylum.SubphylumID== 0)  //dataset exist
                        {
                              WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTbl12Subphylum.SubphylumName,
                              MessageBoxButton.OK, MessageBoxImage.Information);
                        }

                        if (dataset.Count == 0 && CurrentTbl12Subphylum.SubphylumID == 0 ||
                            dataset.Count != 0 && CurrentTbl12Subphylum.SubphylumID != 0  ||
                            dataset.Count == 0 && CurrentTbl12Subphylum.SubphylumID != 0) //new dataset and update
                        {
                            if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTbl12Subphylum.SubphylumName,
                                    MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)                            
                                return;                            
                            {
                                _tbl12SubphylumsRepository.Save();

                                WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess, CurrentTbl12Subphylum.SubphylumName,
                                    MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                        }  
        
                        if (SearchSubphylumName == null && CurrentTbl12Subphylum.SubphylumID == 0)  //new Dataset                        
                            GetConnectedTablesById(o); //refresh doubleClick                                          
                        if (SearchSubphylumName == null && CurrentTbl12Subphylum.SubphylumID != 0)  //update                     
                            Tbl12SubphylumsList = _allListVm.GetValueTbl12SubphylumsList(CurrentTbl12Subphylum.SubphylumID);
                        if (SearchSubphylumName != null && CurrentTbl12Subphylum.SubphylumID == 0)  //new Dataset                        
                            GetConnectedTablesById(o); //refresh doubleClick                                          
                        if (SearchSubphylumName != null && CurrentTbl12Subphylum.SubphylumID != 0)  //update                     
                            Tbl12SubphylumsList = _allListVm.GetValueTbl12SubphylumsList(CurrentTbl12Subphylum.SubphylumID); 

                        SubphylumsView = CollectionViewSource.GetDefaultView(Tbl12SubphylumsList);
                        SubphylumsView.Refresh();         
      
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

             
        #region "Public Commands Connect <== Tbl15Subdivision"                 

        private RelayCommand _getSubdivisionByNameOrIdCommand;     
                    
        public ICommand GetSubdivisionByNameOrIdCommand   
             
        {
            get { return _getSubdivisionByNameOrIdCommand ?? (_getSubdivisionByNameOrIdCommand = new RelayCommand(delegate { GetSubdivisionByNameOrId(null); })); }   
        }

        private void GetSubdivisionByNameOrId(object o)       
        {   
                
            int id;
            if (int.TryParse(SearchSubdivisionName, out id))
                Tbl15SubdivisionsList = new ObservableCollection<Tbl15Subdivision> { _tbl15SubdivisionsRepository.Get(id) };
            else
                Tbl15SubdivisionsList = _allListVm.GetValueTbl15SubdivisionsList(SearchSubdivisionName);   

            Tbl09DivisionsAllList = _allListVm.GetValueTbl09DivisionsAllList();      
  SubdivisionsView = CollectionViewSource.GetDefaultView(Tbl15SubdivisionsList);
            SubdivisionsView.Refresh();
        }
        //------------------------------------------------------------------------------     
                      
        private RelayCommand _addSubdivisionCommand;      
                       
        public ICommand AddSubdivisionCommand    
                        
        {
            get { return _addSubdivisionCommand ?? (_addSubdivisionCommand = new RelayCommand(delegate { AddSubdivision(null); })); }
        }

        private void AddSubdivision(object o)
        {
            Tbl15SubdivisionsList = new ObservableCollection<Tbl15Subdivision>();   
  Tbl15SubdivisionsList.Insert(0, new Tbl15Subdivision{ SubdivisionName= CultRes.StringsRes.DatasetNew });  
    
            Tbl09DivisionsAllList = _allListVm.GetValueTbl09DivisionsAllList();    
  SubdivisionsView = CollectionViewSource.GetDefaultView(Tbl15SubdivisionsList);
            SubdivisionsView.Refresh();
        }
        //---------------------------------------------------------------------------------------  
                              
        private RelayCommand _copySubdivisionCommand;            
                       
        public ICommand CopySubdivisionCommand          
                                 
        {
            get { return _copySubdivisionCommand ?? (_copySubdivisionCommand = new RelayCommand(delegate { CopySubdivision(null); })); }
        }

        private void CopySubdivision(object o)
        {
            Tbl15SubdivisionsList = new ObservableCollection<Tbl15Subdivision>();

            var subdivision = _tbl15SubdivisionsRepository.Get(CurrentTbl15Subdivision.SubdivisionID);

            Tbl15SubdivisionsList.Insert(0, new Tbl15Subdivision
            {                 
                                   
                PhylumID = subphylum.PhylumID,
                SubdivisionName = CultRes.StringsRes.DatasetNew,     
                Valid = subdivision.Valid,
                ValidYear = subdivision.ValidYear,
                Synonym = subdivision.Synonym,
                Author = subdivision.Author,
                AuthorYear = subdivision.AuthorYear,
                Info = subdivision.Info,
                EngName = subdivision.EngName,
                GerName = subdivision.GerName,
                FraName = subdivision.FraName,
                PorName = subdivision.PorName,
                Memo = subdivision.Memo           
                                     
            });

            SubdivisionsView = CollectionViewSource.GetDefaultView(Tbl15SubdivisionsList);
            SubdivisionsView.Refresh();
        }
        //---------------------------------------------------------------------------------------     
                                           
        private RelayCommand _deleteSubdivisionCommand;              
                       
        public ICommand DeleteSubdivisionCommand             
                                                
        {
            get { return _deleteSubdivisionCommand ?? (_deleteSubdivisionCommand = new RelayCommand(delegate { DeleteSubdivision(null); })); }
        }

        private void DeleteSubdivision(object o)
        {
            try
            {
                var subdivision = _tbl15SubdivisionsRepository.Get(CurrentTbl15Subdivision.SubdivisionID);
                if (subdivision!= null)
                {  
                                                    
                    if (WpfMessageBox.Show(CultRes.StringsRes.DeleteQuestion1, CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl15Subdivision.SubdivisionName,
                         MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes) 
                    return;

                    _tbl15SubdivisionsRepository.Delete(subdivision);
                    _tbl15SubdivisionsRepository.Save();

                    WpfMessageBox.Show(CultRes.StringsRes.DeleteSuccess, CurrentTbl15Subdivision.SubdivisionName,
                       MessageBoxButton.OK, MessageBoxImage.Information);  
                                                        
                        if (SearchSubdivisionName == null)                       
                            GetConnectedTablesById(o); //refresh doubleClick                                          
                        else
                        {
                            Tbl15SubdivisionsList = new ObservableCollection<Tbl15Subdivision>
                                                  (from x in _tbl15SubdivisionsRepository.GetAll()
                                                   where x.SubdivisionName.StartsWith(SearchSubdivisionName)   
                                                         
                                                   orderby x.SubdivisionName                                                       
                                                   select x);

                            SubdivisionsView = CollectionViewSource.GetDefaultView(Tbl15SubdivisionsList);
                            SubdivisionsView.Refresh();
                    }
                }
                else
                {   
                                                          
                    WpfMessageBox.Show(CultRes.StringsRes.Information, CultRes.StringsRes.DeleteCan + " " + CurrentTbl15Subdivision.SubdivisionName+ " " + CultRes.StringsRes.DeleteCan1,
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
                                                               
        private RelayCommand _saveSubdivisionCommand;              
                       
        public ICommand SaveSubdivisionCommand             
                                                                     
        {
            get { return _saveSubdivisionCommand ?? (_saveSubdivisionCommand = new RelayCommand(delegate { SaveSubdivision(null); })); }
        }

        private void SaveSubdivision(object o)
        {
            try
            {
                var subdivision = _tbl15SubdivisionsRepository.Get(CurrentTbl15Subdivision.SubdivisionID);
                if (CurrentTbl15Subdivision == null)              
                    WpfMessageBox.Show(CultRes.StringsRes.Warning, CultRes.StringsRes.DatasetNotExist, MessageBoxButton.OK, MessageBoxImage.Warning);               
                else
                {
                    if (CurrentTbl15Subdivision.SubdivisionID!= 0)
                    {
                        if (subdivision!= null) //update
                        {   
                                                                                 
                            subphylum.PhylumID = CurrentTbl12Subphylum.PhylumID;          
                            subphylum.SubphylumName= CurrentTbl12Subphylum.SubphylumName;  
                            subphylum.Valid = CurrentTbl12Subphylum.Valid;
                            subphylum.ValidYear = CurrentTbl12Subphylum.ValidYear;
                            subphylum.Synonym = CurrentTbl12Subphylum.Synonym;
                            subphylum.Author = CurrentTbl12Subphylum.Author;
                            subphylum.AuthorYear = CurrentTbl12Subphylum.AuthorYear;
                            subphylum.Info = CurrentTbl12Subphylum.Info;
                            subphylum.EngName = CurrentTbl12Subphylum.EngName;
                            subphylum.GerName = CurrentTbl12Subphylum.GerName;
                            subphylum.FraName = CurrentTbl12Subphylum.FraName;
                            subphylum.PorName = CurrentTbl12Subphylum.PorName;
                            subphylum.Updater = Environment.UserName;
                            subphylum.UpdaterDate = DateTime.Now;
                            subphylum.Memo = CurrentTbl12Subphylum.Memo;       
      
                                                                          
                        }
                    }
                    else
                    {
                        _tbl15SubdivisionsRepository.Add(new Tbl15Subdivision   //add new
                        {   
  DivisionID= CurrentTbl15Subdivision.DivisionID,              
                            SubdivisionName= CurrentTbl15Subdivision.SubdivisionName,     
                            CountID = RandomHelper.Randomnumber(),
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
                        //check about double Name   
                        var dataset = new ObservableCollection<Tbl15Subdivision>
                        (from a in _tbl15SubdivisionsRepository.GetAll()
                         where
                         a.SubdivisionName.Trim() == CurrentTbl15Subdivision.SubdivisionName.Trim() &&                
                         a.DivisionID == CurrentTbl15Subdivision.DivisionID
                         select a);

                        if (dataset.Count == 0 && CurrentTbl15Subdivision.SubdivisionID== 0 ||
                            dataset.Count != 0 && CurrentTbl15Subdivision.SubdivisionID != 0  ||
                            dataset.Count == 0 && CurrentTbl15Subdivision.SubdivisionID != 0) //new dataset and update
                        {
                            if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTbl15Subdivision.SubdivisionName,
                                    MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)                            
                                return;                            
                            {
                                _tbl15SubdivisionsRepository.Save();

                                WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess, CurrentTbl15Subdivision.SubdivisionName,
                                    MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                        }

                        if (dataset.Count != 0 && CurrentTbl15Subdivision.SubdivisionID== 0)  //dataset exist
                        {
                              WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTbl15Subdivision.SubdivisionName,
                              MessageBoxButton.OK, MessageBoxImage.Information);
                        }  
                                                                                       
                        if (SearchSubdivisionName == null && CurrentTbl15Subdivision.SubdivisionID == 0)  //new Dataset                        
                            GetConnectedTablesById(o); //refresh doubleClick                                          
                        if (SearchSubdivisionName == null && CurrentTbl15Subdivision.SubdivisionID != 0)  //update                     
                            Tbl15SubdivisionsList = _allListVm.GetValueTbl15SubdivisionsList(CurrentTbl15Subdivision.SubdivisionID);
                        if (SearchSubdivisionName != null && CurrentTbl15Subdivision.SubdivisionID == 0)  //new Dataset                        
                            GetConnectedTablesById(o); //refresh doubleClick                                          
                        if (SearchSubdivisionName != null && CurrentTbl15Subdivision.SubdivisionID != 0)  //update                     
                            Tbl15SubdivisionsList = _allListVm.GetValueTbl15SubdivisionsList(CurrentTbl15Subdivision.SubdivisionID); 

                        SubdivisionsView = CollectionViewSource.GetDefaultView(Tbl15SubdivisionsList);
                        SubdivisionsView.Refresh();  
                                                                                          
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
        

 //    Part 4    

             
        #region "Public Commands Connect ==> Tbl21Class"                 

        private RelayCommand _getClassByNameOrIdCommand;     
               
        public ICommand GetClassByNameOrIdCommand   
               
        {
            get { return _getClassByNameOrIdCommand ?? (_getClassByNameOrIdCommand = new RelayCommand(delegate { GetClassByNameOrId(null); })); }   
        }

        private void GetClassByNameOrId(object o)       
        {   
                
            int id;
            if (int.TryParse(SearchClassName, out id))
                Tbl21ClassesList = new ObservableCollection<Tbl21Class> { _tbl21ClassesRepository.Get(id) };
            else 
                Tbl21ClassesList = _allListVm.GetValueTbl21ClassesList(SearchClassName);       
  ClassesView = CollectionViewSource.GetDefaultView(Tbl21ClassesList);
            ClassesView.Refresh();
        }
        //------------------------------------------------------------------------------     
                      
        private RelayCommand _addClassCommand;      
                       
        public ICommand AddClassCommand    
                        
        {
            get { return _addClassCommand ?? (_addClassCommand = new RelayCommand(delegate { AddClass(null); })); }
        }

        private void AddClass(object o)
        {
            Tbl21ClassesList = new ObservableCollection<Tbl21Class>();   
  Tbl21ClassesList.Insert(0, new Tbl21Class{ ClassName= CultRes.StringsRes.DatasetNew });  

            if (Tbl18SuperclassesAllList == null)
            Tbl18SuperclassesAllList = _allListVm.GetValueTbl18SuperclassesAllList();     
  ClassesView = CollectionViewSource.GetDefaultView(Tbl21ClassesList);
            ClassesView.Refresh();
        }
        //---------------------------------------------------------------------------------------  
                              
        private RelayCommand _copyClassCommand;            
                              
        public ICommand CopyClassCommand          
                                 
        {
            get { return _copyClassCommand ?? (_copyClassCommand = new RelayCommand(delegate { CopyClass(null); })); }
        }

        private void CopyClass(object o)
        {
            Tbl21ClassesList = new ObservableCollection<Tbl21Class>();

            var classe = _tbl21ClassesRepository.Get(CurrentTbl21Class.ClassID);

            Tbl21ClassesList.Insert(0, new Tbl21Class
            {                 
  SuperclassID = classe.SuperclassID,
                ClassName = CultRes.StringsRes.DatasetNew,     
                Valid = classe.Valid,
                ValidYear = classe.ValidYear,
                Synonym = classe.Synonym,
                Author = classe.Author,
                AuthorYear = classe.AuthorYear,
                Info = classe.Info,
                EngName = classe.EngName,
                GerName = classe.GerName,
                FraName = classe.FraName,
                PorName = classe.PorName,
                Memo = classe.Memo         
                                     
            });

            ClassesView = CollectionViewSource.GetDefaultView(Tbl21ClassesList);
            ClassesView.Refresh();
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
                var classe = _tbl21ClassesRepository.Get(CurrentTbl21Class.ClassID);
                if (classe!= null)
                {  
                                                    
                    if (WpfMessageBox.Show(CultRes.StringsRes.DeleteQuestion1, CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl21Class.ClassName,
                         MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes) 
                    return;

                    _tbl21ClassesRepository.Delete(classe);
                    _tbl21ClassesRepository.Save();

                    WpfMessageBox.Show(CultRes.StringsRes.DeleteSuccess, CurrentTbl21Class.ClassName,
                       MessageBoxButton.OK, MessageBoxImage.Information);  
                                                        
                        if (SearchClassName == null)                       
                            GetConnectedTablesById(o); //refresh doubleClick                                          
                        else
                        {
                        Tbl21ClassesList = _allListVm.GetValueTbl21ClassesList(SearchClassName);  
  ClassesView = CollectionViewSource.GetDefaultView(Tbl21ClassesList);
                            ClassesView.Refresh();
                    }
                }
                else
                {   
                                                          
                    WpfMessageBox.Show(CultRes.StringsRes.Information, CultRes.StringsRes.DeleteCan + " " + CurrentTbl21Class.ClassName+ " " + CultRes.StringsRes.DeleteCan1,
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
                                                               
        private RelayCommand _saveClassCommand;              
                                                                 
        public ICommand SaveClassCommand             
                                                                     
        {
            get { return _saveClassCommand ?? (_saveClassCommand = new RelayCommand(delegate { SaveClass(null); })); }
        }

        private void SaveClass(object o)
        {
            try
            {
                var classe = _tbl21ClassesRepository.Get(CurrentTbl21Class.ClassID);
                if (CurrentTbl21Class == null)               
                    WpfMessageBox.Show(CultRes.StringsRes.Warning, CultRes.StringsRes.DatasetNotExist, 
                        MessageBoxButton.OK, MessageBoxImage.Warning);              
                else
                {
                    if (CurrentTbl21Class.ClassID!= 0)
                    {
                        if (classe!= null) //update
                        {   
  classe.SuperclassID= CurrentTbl21Class.SuperclassID;            
                            classe.ClassName= CurrentTbl21Class.ClassName;
                            classe.Valid = CurrentTbl21Class.Valid;
                            classe.ValidYear = CurrentTbl21Class.ValidYear;
                            classe.Synonym = CurrentTbl21Class.Synonym;
                            classe.Author = CurrentTbl21Class.Author;
                            classe.AuthorYear = CurrentTbl21Class.AuthorYear;
                            classe.Info = CurrentTbl21Class.Info;
                            classe.EngName = CurrentTbl21Class.EngName;
                            classe.GerName = CurrentTbl21Class.GerName;
                            classe.FraName = CurrentTbl21Class.FraName;
                            classe.PorName = CurrentTbl21Class.PorName;
                            classe.Updater = Environment.UserName;
                            classe.UpdaterDate = DateTime.Now;
                            classe.Memo = CurrentTbl21Class.Memo;                                                              
                                                                            
                        }
                    }
                    else
                    {
                        _tbl21ClassesRepository.Add(new Tbl21Class    // add new
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
                        //SuperclassID may be not 0
                        if (CurrentTbl21Class.SuperclassID == 0)
                        {
                            WpfMessageBox.Show(CultRes.StringsRes.RequiredGenealogyConnect, CultRes.StringsRes.RequiredInput,
                                MessageBoxButton.OK, MessageBoxImage.Information);
                            return;
                        }

                        //check if dataset with Name and vb-name already exist
                        var dataset = new ObservableCollection<Tbl21Class>
                        (from a in _tbl21ClassesRepository.GetAll()
                         where
                         a.ClassName.Trim() == CurrentTbl21Class.ClassName.Trim() &&                
                         a.SuperclassID == CurrentTbl21Class.SuperclassID
                         select a);

                        if (dataset.Count != 0 && CurrentTbl21Class.ClassID== 0)  //dataset exist
                        {
                              WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTbl21Class.ClassName,
                              MessageBoxButton.OK, MessageBoxImage.Information);
                        }

                        if (dataset.Count == 0 && CurrentTbl21Class.ClassID == 0 ||
                            dataset.Count != 0 && CurrentTbl21Class.ClassID != 0  ||
                            dataset.Count == 0 && CurrentTbl21Class.ClassID != 0) //new dataset and update
                        {
                            if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTbl21Class.ClassName,
                                    MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)                            
                                return;                            
                            {
                                _tbl21ClassesRepository.Save();

                                WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess, CurrentTbl21Class.ClassName,
                                    MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                        }  
                
                        if (SearchClassName == null && CurrentTbl21Class.ClassID == 0)  //new Dataset                        
                            Tbl21ClassesList = _allListVm.GetValueTbl21ClassesList();  //last Dataset
                        if (SearchClassName == null && CurrentTbl21Class.ClassID != 0)   //update
                            Tbl21ClassesList = _allListVm.GetValueTbl21ClassesList(CurrentTbl21Class.ClassID);
                        if (SearchClassName != null && CurrentTbl21Class.ClassID == 0)  //new Dataset
                            Tbl21ClassesList = _allListVm.GetValueTbl21ClassesList();  //last Dataset
                        if (SearchClassName != null && CurrentTbl21Class.ClassID != 0)   //update
                            Tbl21ClassesList = _allListVm.GetValueTbl21ClassesList(CurrentTbl21Class.ClassID);
                                                                       

                        ClassesView = CollectionViewSource.GetDefaultView(Tbl21ClassesList);
                        ClassesView.Refresh();             
                                                                                          
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


             
        #region "Public Commands Connect ==> Tbl24Subclass"                 

        private RelayCommand _getSubclassByNameOrIdCommand;     
               
        public ICommand GetSubclassByNameOrIdCommand   
               
        {
            get { return _getSubclassByNameOrIdCommand ?? (_getSubclassByNameOrIdCommand = new RelayCommand(delegate { GetSubclassByNameOrId(null); })); }   
        }

        private void GetSubclassByNameOrId(object o)       
        {   
                
            int id;
            if (int.TryParse(SearchSubclassName, out id))
                Tbl24SubclassesList = new ObservableCollection<Tbl24Subclass> { _tbl24SubclassesRepository.Get(id) };
            else
                Tbl24SubclassesList = _allListVm.GetValueTbl24SubclassesList (SearchSubclassName);      
  SubclassesView = CollectionViewSource.GetDefaultView(Tbl24SubclassesList);
            SubclassesView.Refresh();
        }
        //------------------------------------------------------------------------------     
                      
        private RelayCommand _addSubclassCommand;      
                       
        public ICommand AddSubclassCommand    
                        
        {
            get { return _addSubclassCommand ?? (_addSubclassCommand = new RelayCommand(delegate { AddSubclass(null); })); }
        }

        private void AddSubclass(object o)
        {
            Tbl24SubclassesList = new ObservableCollection<Tbl24Subclass>();   
  Tbl24SubclassesList.Insert(0, new Tbl24Subclass{ SubclassName= CultRes.StringsRes.DatasetNew });   
            Tbl18SuperclassesAllList = _allListVm.GetValueTbl18SuperclassesAllList();    
  SubclassesView = CollectionViewSource.GetDefaultView(Tbl24SubclassesList);
            SubclassesView.Refresh();
        }
        //---------------------------------------------------------------------------------------  
                              
        private RelayCommand _copySubclassCommand;            
                              
        public ICommand CopySubclassCommand          
                                 
        {
            get { return _copySubclassCommand ?? (_copySubclassCommand = new RelayCommand(delegate { CopySubclass(null); })); }
        }

        private void CopySubclass(object o)
        {
            Tbl24SubclassesList = new ObservableCollection<Tbl24Subclass>();

            var subclass = _tbl24SubclassesRepository.Get(CurrentTbl24Subclass.SubclassID);

            Tbl24SubclassesList.Insert(0, new Tbl24Subclass
            {                 
  SuperclassID = subclass.SuperclassID,     
                SubclassName = CultRes.StringsRes.DatasetNew,     
                Valid = subclass.Valid,
                ValidYear = subclass.ValidYear,
                Synonym = subclass.Synonym,
                Author = subclass.Author,
                AuthorYear = subclass.AuthorYear,
                Info = subclass.Info,
                EngName = subclass.EngName,
                GerName = subclass.GerName,
                FraName = subclass.FraName,
                PorName = subclass.PorName,
                Memo = subclass.Memo           
                                     
            });

            SubclassesView = CollectionViewSource.GetDefaultView(Tbl24SubclassesList);
            SubclassesView.Refresh();
        }
        //---------------------------------------------------------------------------------------    
                                           
        private RelayCommand _deleteSubclassCommand;              
                                           
        public ICommand DeleteSubclassCommand             
                                                
        {
            get { return _deleteSubclassCommand ?? (_deleteSubclassCommand = new RelayCommand(delegate { DeleteSubclass(null); })); }
        }

        private void DeleteSubclass(object o)
        {
            try
            {
                var subclass = _tbl24SubclassesRepository.Get(CurrentTbl24Subclass.SubclassID);
                if (subclass!= null)
                {  
                                                    
                    if (WpfMessageBox.Show(CultRes.StringsRes.DeleteQuestion1, CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl24Subclass.SubclassName,
                         MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes) 
                    return;

                    _tbl24SubclassesRepository.Delete(subclass);
                    _tbl24SubclassesRepository.Save();

                    WpfMessageBox.Show(CultRes.StringsRes.DeleteSuccess, CurrentTbl24Subclass.SubclassName,
                       MessageBoxButton.OK, MessageBoxImage.Information);  
                                                        
                        if (SearchSubclassName == null)                       
                            GetConnectedTablesById(o); //refresh doubleClick                                          
                        else
                        {
                            Tbl24SubclassesList = _allListVm.GetValueTbl24SubclassesList(SearchSubclassName);  
  SubclassesView = CollectionViewSource.GetDefaultView(Tbl24SubclassesList);
                            SubclassesView.Refresh();
                    }
                }
                else
                {   
                                                          
                    WpfMessageBox.Show(CultRes.StringsRes.Information, CultRes.StringsRes.DeleteCan + " " + CurrentTbl24Subclass.SubclassName+ " " + CultRes.StringsRes.DeleteCan1,
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
                                                               
        private RelayCommand _saveSubclassCommand;              
                                                                 
        public ICommand SaveSubclassCommand             
                                                                     
        {
            get { return _saveSubclassCommand ?? (_saveSubclassCommand = new RelayCommand(delegate { SaveSubclass(null); })); }
        }

        private void SaveSubclass(object o)
        {
            try
            {
                var subclass = _tbl24SubclassesRepository.Get(CurrentTbl24Subclass.SubclassID);
                if (CurrentTbl24Subclass == null)                
                    WpfMessageBox.Show(CultRes.StringsRes.Warning, CultRes.StringsRes.DatasetNotExist, MessageBoxButton.OK, MessageBoxImage.Warning);              
                else
                {
                    if (CurrentTbl24Subclass.SubclassID!= 0)
                    {
                        if (subclass!= null) //update
                        {   
  subclass.SuperclassID= CurrentTbl24Subclass.SuperclassID;            
                            subclass.SubclassName= CurrentTbl24Subclass.SubclassName;
                            subclass.Valid = CurrentTbl24Subclass.Valid;
                            subclass.ValidYear = CurrentTbl24Subclass.ValidYear;
                            subclass.Synonym = CurrentTbl24Subclass.Synonym;
                            subclass.Author = CurrentTbl24Subclass.Author;
                            subclass.AuthorYear = CurrentTbl24Subclass.AuthorYear;
                            subclass.Info = CurrentTbl24Subclass.Info;
                            subclass.EngName = CurrentTbl24Subclass.EngName;
                            subclass.GerName = CurrentTbl24Subclass.GerName;
                            subclass.FraName = CurrentTbl24Subclass.FraName;
                            subclass.PorName = CurrentTbl24Subclass.PorName;
                            subclass.Updater = Environment.UserName;
                            subclass.UpdaterDate = DateTime.Now;
                            subclass.Memo = CurrentTbl24Subclass.Memo;                                                              
                                                                            
                        }
                    }
                    else
                    {
                        _tbl24SubclassesRepository.Add(new Tbl24Subclass     //add new
                        {   
  SuperclassID = CurrentTbl24Subclass.SuperclassID,              
                            SubclassName = CurrentTbl24Subclass.SubclassName,              
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
                        //SuperclassID may be not 0
                        if (CurrentTbl24Subclass.SuperclassID == 0)
                        {
                            WpfMessageBox.Show(CultRes.StringsRes.RequiredGenealogyConnect, CultRes.StringsRes.RequiredInput,
                                MessageBoxButton.OK, MessageBoxImage.Information);
                            return;
                        }

                        //check if dataset with Name and vb-name already exist
                        var dataset = new ObservableCollection<Tbl24Subclass>
                        (from a in _tbl24SubclassesRepository.GetAll()
                         where
                         a.SubclassName.Trim() == CurrentTbl24Subclass.SubclassName.Trim() &&                
                         a.SuperclassID == CurrentTbl24Subclass.SuperclassID
                         select a);

                        if (dataset.Count != 0 && CurrentTbl24Subclass.SubclassID == 0)  //dataset exist
                        {
                              WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTbl24Subclass.SubclassName,
                              MessageBoxButton.OK, MessageBoxImage.Information);
                        }

                        if (dataset.Count == 0 && CurrentTbl24Subclass.SubclassID== 0 ||
                            dataset.Count != 0 && CurrentTbl24Subclass.SubclassID != 0  ||
                            dataset.Count == 0 && CurrentTbl24Subclass.SubclassID != 0) //new dataset and update
                        {
                            if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTbl24Subclass.SubclassName,
                                    MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)                            
                                return;                            
                            {
                                _tbl24SubclassesRepository.Save();

                                WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess, CurrentTbl24Subclass.SubclassName,
                                    MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                        }  
                 

                        if (SearchSubclassName == null && CurrentTbl24Subclass.SubclassID == 0)  //new Dataset                        
                            Tbl24SubclassesList = _allListVm.GetValueTbl24SubclassesList();  //last Dataset
                        if (SearchSubclassName == null && CurrentTbl24Subclass.SubclassID != 0)   //update
                            Tbl24SubclassesList = _allListVm.GetValueTbl24SubclassesList(CurrentTbl24Subclass.SubclassID);
                        if (SearchSubclassName != null && CurrentTbl24Subclass.SubclassID == 0)  //new Dataset
                            Tbl24SubclassesList = _allListVm.GetValueTbl24SubclassesList();  //last Dataset
                        if (SearchSubclassName != null && CurrentTbl24Subclass.SubclassID != 0)   //update
                            Tbl24SubclassesList = _allListVm.GetValueTbl24SubclassesList(CurrentTbl24Subclass.SubclassID);     
                                                                 
                        SubclassesView = CollectionViewSource.GetDefaultView(Tbl24SubclassesList);
                        SubclassesView.Refresh();           
                                                                                          
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

           Tbl18SuperclassesAllList = _allListVm.GetValueTbl18SuperclassesAllList();
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
SuperclassID = refAuthor.SuperclassID,              
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
         
                            refAuthor.SuperclassID = CurrentTbl90RefAuthor.SuperclassID;  
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
SuperclassID = CurrentTbl90RefAuthor.SuperclassID,              
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
                        //SuperclassID may be not 0
                        if (CurrentTbl90RefAuthor.SuperclassID == 0 || CurrentTbl90RefAuthor.RefAuthorID == 0)
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
                         a.SuperclassID == CurrentTbl90RefAuthor.SuperclassID &&
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

            Tbl18SuperclassesAllList = _allListVm.GetValueTbl18SuperclassesAllList();
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
SuperclassID = refSource.SuperclassID,              
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
         
                            refSource.SuperclassID = CurrentTbl90RefSource.SuperclassID;            
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
SuperclassID = CurrentTbl90RefSource.SuperclassID,              
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
                        //SuperclassID may be not 0
                        if (CurrentTbl90RefSource.SuperclassID == 0 || CurrentTbl90RefSource.RefSourceID == 0)
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
                         a.SuperclassID == CurrentTbl90RefSource.SuperclassID &&
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

            Tbl18SuperclassesAllList = _allListVm.GetValueTbl18SuperclassesAllList();
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
SuperclassID = refExpert.SuperclassID,              
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
      
                            refExpert.SuperclassID = CurrentTbl90RefExpert.SuperclassID;           
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
SuperclassID = CurrentTbl90RefExpert.SuperclassID,              
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
                        //SuperclassID may be not 0
                        if (CurrentTbl90RefExpert.SuperclassID == 0 || CurrentTbl90RefExpert.RefExpertID == 0)   
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
                         a.SuperclassID == CurrentTbl90RefExpert.SuperclassID &&
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

            Tbl18SuperclassesAllList = _allListVm.GetValueTbl18SuperclassesAllList();      
    

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
SuperclassID = comment.SuperclassID,              
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
      
                            comment.SuperclassID = CurrentTbl93Comment.SuperclassID;            
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
SuperclassID = CurrentTbl93Comment.SuperclassID,              
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
                        //SuperclassID may be not 0
                        if (CurrentTbl93Comment.SuperclassID == 0)
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
                         a.SuperclassID == CurrentTbl93Comment.SuperclassID
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

        public void GetConnectedTablesById(object o)
        {
            SelectedDetailThreeRefTabIndex = 1;  //change to Connect tab

            Tbl12SubphylumsList =   new ObservableCollection<Tbl12Subphylum>
                                                         (from x in _tbl12SubphylumsRepository.GetAll()
                                                          where x.SubphylumID == CurrentTbl18Superclass.SubphylumID
                                                          orderby x.SubphylumName
                                                          select x);

            SubphylumsView = CollectionViewSource.GetDefaultView(Tbl12SubphylumsList);
            SubphylumsView.Refresh();
            //-----------------------------------------------------------------------------------
            Tbl15SubdivisionsList =
                new ObservableCollection<Tbl15Subdivision>
                                                           (from y in _tbl15SubdivisionsRepository.GetAll()
                                                            where y.SubdivisionID == CurrentTbl18Superclass.SubdivisionID
                                                            orderby y.SubdivisionName
                                                            select y);

            SubdivisionsView = CollectionViewSource.GetDefaultView(Tbl15SubdivisionsList);
            SubdivisionsView.Refresh();
            //-----------------------------------------------------------------------------------
            Tbl21ClassesList =
                new ObservableCollection<Tbl21Class>
                                                     (from z in _tbl21ClassesRepository.GetAll()
                                                      where z.SuperclassID == CurrentTbl18Superclass.SuperclassID
                                                      orderby z.ClassName
                                                      select z);


            ClassesView = CollectionViewSource.GetDefaultView(Tbl21ClassesList);
            ClassesView.Refresh();
            //-----------------------------------------------------------------------------------
            Tbl90RefAuthorsList =
                new ObservableCollection<Tbl90Reference>((from refAu in _tbl90ReferencesRepository.GetAll()
                                                          where refAu.SuperclassID == CurrentTbl18Superclass.SuperclassID
                                                          && refAu.RefExpertID == null
                                                          && refAu.RefSourceID == null
                                                          orderby refAu.Tbl90RefAuthors.RefAuthorName, refAu.Tbl90RefAuthors.BookName, refAu.Tbl90RefAuthors.Page1
                                                          select refAu));

            RefAuthorsView = CollectionViewSource.GetDefaultView(Tbl90RefAuthorsList);
            RefAuthorsView.Refresh();
            //--------------------------------------------------------------------------------------
            Tbl90RefSourcesList =
                new ObservableCollection<Tbl90Reference>((from refSo in _tbl90ReferencesRepository.GetAll()
                                                          where refSo.SuperclassID == CurrentTbl18Superclass.SuperclassID
                                                          && refSo.RefExpertID == null
                                                          && refSo.RefAuthorID == null
                                                          orderby refSo.Tbl90RefSources.RefSourceName
                                                          select refSo));

            RefSourcesView = CollectionViewSource.GetDefaultView(Tbl90RefSourcesList);
            RefSourcesView.Refresh();
            //--------------------------------------------------------------------------------------
            Tbl90RefExpertsList =
                new ObservableCollection<Tbl90Reference>((from refEx in _tbl90ReferencesRepository.GetAll()
                                                          where refEx.SuperclassID == CurrentTbl18Superclass.SuperclassID
                                                          && refEx.RefAuthorID == null
                                                          && refEx.RefSourceID == null
                                                          orderby refEx.Tbl90RefExperts.RefExpertName
                                                          select refEx));

            RefExpertsView = CollectionViewSource.GetDefaultView(Tbl90RefExpertsList);
            RefExpertsView.Refresh();
            //-----------------------------------------------------------------------------------
            Tbl93CommentsList =
                new ObservableCollection<Tbl93Comment>((from comm in _tbl93CommentsRepository.GetAll()
                                                        where comm.SuperclassID == CurrentTbl18Superclass.SuperclassID
                                                        orderby comm.Info
                                                        select comm));

            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            CommentsView.Refresh();
            //------------------------------------------------------------------------------------    

  Tbl09DivisionsAllList = _allListVm.GetValueTbl09DivisionsAllList();   
  Tbl06PhylumsAllList = _allListVm.GetValueTbl06PhylumsAllList();

            Tbl18SuperclassesAllList = _allListVm.GetValueTbl18SuperclassesAllList();

            Tbl90AuthorsAllList = _allListVm.GetValueTbl90AuthorsAllList();

            Tbl90SourcesAllList = _allListVm.GetValueTbl90SourcesAllList();

            Tbl90ExpertsAllList = _allListVm.GetValueTbl90ExpertsAllList();
        }

        #endregion "Public Commands Connected Tables by DoubleClick"
   
 

 //    Part 10    

 

 //    Part 11    


     
        #region "Public Properties Tbl18Superclass"

        private string _searchSuperclassName;
        public string SearchSuperclassName
        {
            get => _searchSuperclassName; 
            set { _searchSuperclassName = value; RaisePropertyChanged(); }
        }

        public ICollectionView SuperclassesView;
        public Tbl18Superclass CurrentTbl18Superclass => SuperclassesView?.CurrentItem as Tbl18Superclass;           

        private ObservableCollection<Tbl18Superclass> _tbl18SuperclassesList;
        public  ObservableCollection<Tbl18Superclass> Tbl18SuperclassesList
        {
            get => _tbl18SuperclassesList; 
            set { _tbl18SuperclassesList = value; RaisePropertyChanged();     }
        }

        private ObservableCollection<Tbl18Superclass> _tbl18SuperclassesAllList;
        public ObservableCollection<Tbl18Superclass> Tbl18SuperclassesAllList
        {
            get => _tbl18SuperclassesAllList; 
            set { _tbl18SuperclassesAllList = value; RaisePropertyChanged(); }       
        }

        #endregion "Public Properties"
   
       
        #region "Public Properties Tbl12Subphylum"

        private string _searchSubphylumName;
        public string SearchSubphylumName
        {
            get => _searchSubphylumName; 
            set { _searchSubphylumName = value; RaisePropertyChanged(); }
        }

        public  ICollectionView SubphylumsView;
        public  Tbl12Subphylum CurrentTbl12Subphylum => SubphylumsView?.CurrentItem as Tbl12Subphylum;           

        private ObservableCollection<Tbl12Subphylum> _tbl12SubphylumsList;
        public  ObservableCollection<Tbl12Subphylum> Tbl12SubphylumsList
        {
            get => _tbl12SubphylumsList; 
            set { _tbl12SubphylumsList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl12Subphylum> _tbl12SubphylumsAllList;
        public  ObservableCollection<Tbl12Subphylum> Tbl12SubphylumsAllList
        {
            get => _tbl12SubphylumsAllList; 
            set { _tbl12SubphylumsAllList = value; RaisePropertyChanged(); }       
        }

        #endregion "Public Properties"
   
  
       
        #region "Public Properties Tbl15Subdivision"

        private string _searchSubdivisionName;
        public  string SearchSubdivisionName
        {
            get => _searchSubdivisionName; 
            set { _searchSubdivisionName = value; RaisePropertyChanged(); }
        }

        public  ICollectionView SubdivisionsView;
        public  Tbl15Subdivision CurrentTbl15Subdivision => SubdivisionsView?.CurrentItem as Tbl15Subdivision;           

        private ObservableCollection<Tbl15Subdivision> _tbl15SubdivisionsList;
        public  ObservableCollection<Tbl15Subdivision> Tbl15SubdivisionsList
        {
            get => _tbl15SubdivisionsList; 
            set { _tbl15SubdivisionsList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl15Subdivision> _tbl15SubdivisionsAllList;
        public ObservableCollection<Tbl15Subdivision> Tbl15SubdivisionsAllList
        {
            get => _tbl15SubdivisionsAllList; 
            set { _tbl15SubdivisionsAllList = value; RaisePropertyChanged(); }       
        }

        #endregion "Public Properties"
   
        
        #region "Public Properties Tbl21Class"

        private string _searchClassName;
        public string SearchClassName
        {
            get => _searchClassName; 
            set { _searchClassName = value; RaisePropertyChanged(); }
        }

        public ICollectionView ClassesView;
        public Tbl21Class CurrentTbl21Class => ClassesView?.CurrentItem as Tbl21Class;           

        private ObservableCollection<Tbl21Class> _tbl21ClassesList;
        public ObservableCollection<Tbl21Class> Tbl21ClassesList
        {
            get => _tbl21ClassesList; 
            set { _tbl21ClassesList = value; RaisePropertyChanged(); }
        }

        #endregion "Public Properties"
   
  
       
        #region "Public Properties Tbl24Subclass"

        private string _searchSubclassName;
        public string SearchSubclassName
        {
            get => _searchSubclassName; 
            set { _searchSubclassName = value; RaisePropertyChanged(); }
        }

        public ICollectionView SubclassesView;
        public Tbl24Subclass CurrentTbl24Subclass => SubclassesView?.CurrentItem as Tbl24Subclass;           

        private ObservableCollection<Tbl24Subclass> _tbl24SubclassesList;
        public ObservableCollection<Tbl24Subclass> Tbl24SubclassesList
        {
            get => _tbl24SubclassesList; 
            set { _tbl24SubclassesList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl24Subclass> _tbl24SubclassesAllList;
        public ObservableCollection<Tbl24Subclass> Tbl24SubclassesAllList
        {
            get => _tbl24SubclassesAllList; 
            set { _tbl24SubclassesAllList = value; RaisePropertyChanged(); }       
        }

        #endregion "Public Properties"
   
  
       
        #region "Public Properties Tbl06Phylum"

        private ObservableCollection<Tbl06Phylum> _tbl06PhylumsAllList;
        public  ObservableCollection<Tbl06Phylum> Tbl06PhylumsAllList
        {
            get => _tbl06PhylumsAllList; 
            set { _tbl06PhylumsAllList = value; RaisePropertyChanged(); }
        }

        #endregion "Public Properties"
   
  
       
        #region "Public Properties Tbl09Division"

        private ObservableCollection<Tbl09Division> _tbl09DivisionsAllList;
        public  ObservableCollection<Tbl09Division> Tbl09DivisionsAllList
        {
            get => _tbl09DivisionsAllList; 
            set { _tbl09DivisionsAllList = value; RaisePropertyChanged(); }
        }

        #endregion "Public Properties"
   
   

 //    Part 12    

 

   }
}   
