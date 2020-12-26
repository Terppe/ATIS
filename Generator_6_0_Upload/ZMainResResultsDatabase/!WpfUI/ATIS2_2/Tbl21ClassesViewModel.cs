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

    
         //    Tbl21ClassesViewModel Skriptdatum:  21.12.2017  18:32    

namespace WPFUI.Views.Database
{     
    
    public partial class Tbl21ClassesViewModel : Tbl03RegnumsViewModel
    {     
         
        #region "Private Data Members"

        private readonly AllListVm _allListVm = new AllListVm();
        private readonly Repository<Tbl21Class, int> _tbl21ClassesRepository = new Repository<Tbl21Class, int>();   
         
        private readonly Repository<Tbl18Superclass, int> _tbl18SuperclassesRepository = new Repository<Tbl18Superclass, int>();   
           
        private readonly Repository<Tbl24Subclass, int> _tbl24SubclassesRepository = new Repository<Tbl24Subclass, int>();   
           
        private readonly Repository<Tbl90Reference, int> _tbl90ReferencesRepository = new Repository<Tbl90Reference, int>();
        private readonly Repository<Tbl93Comment, int> _tbl93CommentsRepository = new Repository<Tbl93Comment, int>();    

        #endregion "Private Data Members"               
      
        #region "Constructor"

        public Tbl21ClassesViewModel()
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

             
        #region "Public Commands Basic Tbl21Class"

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
Tbl18SuperclassesAllList?.Clear();
            Tbl18SuperclassesAllList = _allListVm.GetValueTbl18SuperclassesAllList();  
ClassesView = CollectionViewSource.GetDefaultView(Tbl21ClassesList);
            ClassesView.Refresh();
        }
        //------------------------------------------------------------------------------------                
           
        private RelayCommand _addClassCommand;           
    
        public ICommand AddClassCommand       
    
        {
            get { return _addClassCommand ?? (_addClassCommand = new RelayCommand(delegate { AddClass(null); })); }
        }

        private void AddClass(object o)
        {
            Tbl21ClassesList = new ObservableCollection<Tbl21Class>();   
Tbl21ClassesList.Insert(0, new Tbl21Class{ ClassName= CultRes.StringsRes.DatasetNew });  

            Tbl18SuperclassesAllList?.Clear();
            Tbl18SuperclassesAllList = _allListVm.GetValueTbl18SuperclassesAllList();             
ClassesView = CollectionViewSource.GetDefaultView(Tbl21ClassesList);
            ClassesView.Refresh();
        }
        //------------------------------------------------------------------------------------                
           
        private RelayCommand _copyClassCommand;              
    
        public ICommand CopyClassCommand             
           
        {
            get { return _copyClassCommand ?? (_copyClassCommand = new RelayCommand(delegate { CopyClass(null); })); }
        }

        private void CopyClass(object o)
        {
            Tbl21ClassesList = new ObservableCollection<Tbl21Class>();

            var class = _tbl21ClassesRepository.Get(CurrentTbl21Class.ClassID);

            Tbl21ClassesList.Insert(0, new Tbl21Class
            {    
SuperclassID = class.SuperclassID,   
ClassName = CultRes.StringsRes.DatasetNew,
                Valid = class.Valid,
                ValidYear = class.ValidYear,
                Synonym = class.Synonym,
                Author = class.Author,
                AuthorYear = class.AuthorYear,
                Info = class.Info,
                EngName = class.EngName,
                GerName = class.GerName,
                FraName = class.FraName,
                PorName = class.PorName,
                Memo = class.Memo
              
          
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
                var class = _tbl21ClassesRepository.Get(CurrentTbl21Class.ClassID);
                if (class != null)
                {   
          
                    if (WpfMessageBox.Show(CultRes.StringsRes.DeleteQuestion1, CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl21Class.ClassName,
                         MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
                        return;    
_tbl21ClassesRepository.Delete(class);
                    _tbl21ClassesRepository.Save();     
          
                    WpfMessageBox.Show(CultRes.StringsRes.DeleteSuccess, CurrentTbl21Class.ClassName,
                        MessageBoxButton.OK, MessageBoxImage.Information);   
             
                    if (SearchClassName == null)
                        GetConnectedTablesById(o); //refresh doubleClick                                       
                    else
                    {
                        Tbl21ClassesList = _allListVm.GetValueTbl21ClassesList(SearchClassName);   
             
                    }
                    ClassesView = CollectionViewSource.GetDefaultView(Tbl21ClassesList);
                    ClassesView.Refresh();
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
                var class = _tbl21ClassesRepository.Get(CurrentTbl21Class.ClassID);
                if (CurrentTbl21Class == null)
                    WpfMessageBox.Show(CultRes.StringsRes.Warning, CultRes.StringsRes.DatasetNotExist,
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                else
                {
                    if (CurrentTbl21Class.ClassID != 0)   
                    {
                        if (class != null) //update
                        {  
class.ClassName = CurrentTbl21Class.ClassName;
                            class.SuperclassID = CurrentTbl21Class.SuperclassID;           
class.Valid = CurrentTbl21Class.Valid;
                            class.ValidYear = CurrentTbl21Class.ValidYear;
                            class.Synonym = CurrentTbl21Class.Synonym;
                            class.Author = CurrentTbl21Class.Author;
                            class.AuthorYear = CurrentTbl21Class.AuthorYear;
                            class.Info = CurrentTbl21Class.Info;
                            class.EngName = CurrentTbl21Class.EngName;
                            class.GerName = CurrentTbl21Class.GerName;
                            class.FraName = CurrentTbl21Class.FraName;
                            class.PorName = CurrentTbl21Class.PorName;
                            class.Updater = Environment.UserName;
                            class.UpdaterDate = DateTime.Now;
                            class.Memo = CurrentTbl21Class.Memo;     
         
                        }
                    }
                    else
                    {
                        _tbl21ClassesRepository.Add(new Tbl21Class     //add new
                        {   
ClassName = CurrentTbl21Class.ClassName,
                            SuperclassID = CurrentTbl21Class.SuperclassID,    
          
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

                        if (dataset.Count != 0 && CurrentTbl21Class.ClassID == 0)  //dataset exist
                        {       
         
                            WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTbl21Class.ClassName,
                            MessageBoxButton.OK, MessageBoxImage.Information);     
             
                        }
                        if (dataset.Count == 0 && CurrentTbl21Class.ClassID == 0 ||
                            dataset.Count != 0 && CurrentTbl21Class.ClassID != 0 ||
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

           
        #region "Public Commands Connect <== Tbl18Superclass"                 

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

            SuperclassesView = CollectionViewSource.GetDefaultView(Tbl18SuperclassesList);
            SuperclassesView.Refresh();
        }

        //------------------------------------------------------------------------------------                
           
        private RelayCommand _addSuperclassCommand;      
    
        public ICommand AddSuperclassCommand    
    
        {
            get { return _addSuperclassCommand ?? (_addSuperclassCommand = new RelayCommand(delegate { AddSuperclass(null); })); }
        }

        private void AddSuperclass(object o)
        {
            Tbl18SuperclassesList = new ObservableCollection<Tbl18Superclass>();   
Tbl18SuperclassesList.Insert(0, new Tbl18Superclass{ SuperclassName = CultRes.StringsRes.DatasetNew });   

            Tbl15SubdivisionsAllList?.Clear();
            Tbl15SubdivisionsAllList = _allListVm.GetValueTbl15SubdivisionsAllList();    
SuperclassesView = CollectionViewSource.GetDefaultView(Tbl18SuperclassesList);
            SuperclassesView.Refresh();
        }
        //------------------------------------------------------------------------------------                
           
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
                
                SubdivisionID = superclass.SubdivisionID,
                SubphylumID = superclass.SubphylumID ,
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
superclass.SuperclassName= CurrentTbl18Superclass.SuperclassName; 
                            superclass.SubdivisionID = CurrentTbl18Superclass.SubdivisionID;              
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
SuperclassName = CurrentTbl18Superclass.SuperclassName,              
                            SubdivisionID = CurrentTbl18Superclass.SubdivisionID,              
                            SubphylumID = CurrentTbl18Superclass.SubphylumID,              
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
                        //SubdivisionID may be not 0
                        if (CurrentTbl18Superclass.SubdivisionID == 0)
                        {
                            WpfMessageBox.Show(CultRes.StringsRes.RequiredGenealogyConnect, CultRes.StringsRes.RequiredInput,
                                MessageBoxButton.OK, MessageBoxImage.Information);
                            return;
                        }     
           
                        //check if dataset with Name and vb-name already exist       
                        var dataset = new ObservableCollection<Tbl18Superclass>
                        (from a in _tbl18SuperclassesRepository.GetAll()
                         where
                         a.SuperclassName.Trim() == CurrentTbl18Superclass.SuperclassName.Trim() &&
                         a.SubdivisionID == CurrentTbl18Superclass.SubdivisionID
                         select a);     
           
                        if (dataset.Count != 0 && CurrentTbl18Superclass.SuperclassID == 0)  //dataset exist
                        {
                            WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTbl18Superclass.SuperclassName,
                            MessageBoxButton.OK, MessageBoxImage.Information);
                        }   
           
                        if (dataset.Count == 0 && CurrentTbl18Superclass.SuperclassID == 0 ||
                            dataset.Count != 0 && CurrentTbl18Superclass.SuperclassID != 0 ||
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
                            GetConnectedTablesById(o); //refresh doubleClick                                          
                        if (SearchSuperclassName == null && CurrentTbl18Superclass.SuperclassID != 0)   //update 
                            Tbl18SuperclassesList = _allListVm.GetValueTbl18SuperclassesList(CurrentTbl18Superclass.SuperclassID);
                        if (SearchSuperclassName != null && CurrentTbl18Superclass.SuperclassID == 0)  //new Dataset                        
                            GetConnectedTablesById(o); //refresh doubleClick                                          
                        if (SearchSuperclassName != null && CurrentTbl18Superclass.SuperclassID != 0)   //update 
                            Tbl18SuperclassesList = _allListVm.GetValueTbl18SuperclassesList(CurrentTbl18Superclass.SuperclassID);   
SuperclassesView = CollectionViewSource.GetDefaultView(Tbl18SuperclassesList);
                        SuperclassesView.Refresh();

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
                Tbl24SubclassesList = _allListVm.GetValueTbl24SubclassesList(SearchSubclassName);

            SubclassesView = CollectionViewSource.GetDefaultView(Tbl24SubclassesList);
            SubclassesView.Refresh();
        }

        //------------------------------------------------------------------------------------                
             
        private RelayCommand _addSubclassCommand;

        public ICommand AddSubclassCommand

        {
            get { return _addSubclassCommand ?? (_addSubclassCommand = new RelayCommand(delegate { AddSubclass(null); })); }
        }

        private void AddSubclass(object o)
        {
            Tbl24SubclassesList = new ObservableCollection<Tbl24Subclass>();
            Tbl24SubclassesList.Insert(0, new Tbl24Subclass{ SubclassName = CultRes.StringsRes.DatasetNew });

            Tbl21ClassesAllList?.Clear();
            Tbl21ClassesAllList = _allListVm.GetValueTbl21ClassesAllList();

            SubclassesView = CollectionViewSource.GetDefaultView(Tbl24SubclassesList);
            SubclassesView.Refresh();
        }
        //------------------------------------------------------------------------------------                
              
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
ClassID = subclass.ClassID,
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
                                                         
                    }
                            SubclassesView = CollectionViewSource.GetDefaultView(Tbl24SubclassesList);
                            SubclassesView.Refresh();
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
                    WpfMessageBox.Show(CultRes.StringsRes.Warning, CultRes.StringsRes.DatasetNotExist,
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                else
                {
                    if (CurrentTbl24Subclass.SubclassID != 0)   
                    {
                        if (subclass != null) //update
                        {   
subclass.SubclassName = CurrentTbl24Subclass.SubclassName;
                            subclass.ClassID = CurrentTbl21Class.ClassID;   
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
                            SubclassName = CurrentTbl24Subclass.SubclassName,
                            ClassID = CurrentTbl21Class.ClassID,
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
                        //ClassID may be not 0
                        if (CurrentTbl21Class.ClassID == 0)
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
                         a.ClassID == CurrentTbl21Class.ClassID
                         select a); 
        
                        if (dataset.Count != 0 && CurrentTbl24Subclass.SubclassID == 0)  //dataset exist
                        {
                            WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTbl24Subclass.SubclassName,
                            MessageBoxButton.OK, MessageBoxImage.Information);
                        }      
        
                        if (dataset.Count == 0 && CurrentTbl24Subclass.SubclassID == 0 ||
                            dataset.Count != 0 && CurrentTbl24Subclass.SubclassID != 0 ||
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
                            GetConnectedTablesById(o); //refresh doubleClick                                          
                        if (SearchSubclassName == null && CurrentTbl24Subclass.SubclassID != 0)   //update 
                            Tbl24SubclassesList = _allListVm.GetValueTbl24SubclassesList(CurrentTbl24Subclass.SubclassID);
                        if (SearchSubclassName != null && CurrentTbl24Subclass.SubclassID == 0)  //new Dataset                        
                            GetConnectedTablesById(o); //refresh doubleClick                                          
                        if (SearchSubclassName != null && CurrentTbl24Subclass.SubclassID != 0)   //update 
                            Tbl24SubclassesList = _allListVm.GetValueTbl24SubclassesList(CurrentTbl24Subclass.SubclassID); 
SubclassesView = CollectionViewSource.GetDefaultView(Tbl24SubclassesList);
                        SubclassesView.Refresh();
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

           Tbl21ClassesAllList?.Clear();
           Tbl21ClassesAllList = _allListVm.GetValueTbl21ClassesAllList();
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
ClassID = refAuthor.ClassID,              
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
         
                            refAuthor.ClassID = CurrentTbl21Class.ClassID;  
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
ClassID = CurrentTbl21Class.ClassID,              
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
                        //ClassID may be not 0
                        if (CurrentTbl21Class.ClassID == 0 || CurrentTbl90RefAuthor.RefAuthorID == 0)
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
                         a.ClassID == CurrentTbl21Class.ClassID &&
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

            Tbl21ClassesAllList?.Clear();  
            Tbl21ClassesAllList = _allListVm.GetValueTbl21ClassesAllList();
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
ClassID = refSource.ClassID,              
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
         
                            refSource.ClassID = CurrentTbl21Class.ClassID;            
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
ClassID = CurrentTbl21Class.ClassID,              
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
                        //ClassID may be not 0
                        if (CurrentTbl21Class.ClassID == 0 || CurrentTbl90RefSource.RefSourceID == 0)
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
                         a.ClassID == CurrentTbl21Class.ClassID &&
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

            Tbl21ClassesAllList?.Clear();  
            Tbl21ClassesAllList = _allListVm.GetValueTbl21ClassesAllList();
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
ClassID = refExpert.ClassID,              
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
      
                            refExpert.ClassID = CurrentTbl21Class.ClassID;           
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
ClassID = CurrentTbl21Class.ClassID,              
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
                        //ClassID may be not 0
                        if (CurrentTbl21Class.ClassID == 0 || CurrentTbl90RefExpert.RefExpertID == 0)   
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
                         a.ClassID == CurrentTbl21Class.ClassID &&
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

            Tbl21ClassesAllList?.Clear();  
            Tbl21ClassesAllList = _allListVm.GetValueTbl21ClassesAllList();      
    

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
ClassID = comment.ClassID,              
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
      
                            comment.ClassID = CurrentTbl21Class.ClassID;            
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
ClassID = CurrentTbl21Class.ClassID,              
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
                        //ClassID may be not 0
                        if (CurrentTbl21Class.ClassID == 0)
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
                         a.ClassID == CurrentTbl21Class.ClassID
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
            Tbl21ClassesAllList?.Clear();
            Tbl21ClassesAllList = _allListVm.GetValueTbl21ClassesAllList();

            Tbl18SuperclassesList =  new ObservableCollection<Tbl18Superclass>
                 (from x in _tbl18SuperclassesRepository.GetAll()
                 where x.SuperclassID == CurrentTbl21Class.SuperclassID
                  orderby x.SuperclassName
                  select x);

            SuperclassesView = CollectionViewSource.GetDefaultView(Tbl18SuperclassesList);
            SuperclassesView.Refresh();

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
                    Tbl21ClassesAllList?.Clear();
                    Tbl21ClassesAllList = _allListVm.GetValueTbl21ClassesAllList();

                    Tbl24SubclassesList?.Clear();
                    Tbl24SubclassesList =  new ObservableCollection<Tbl24Subclass>
                              (from x in _tbl24SubclassesRepository.GetAll()
                              where x.ClassID == CurrentTbl21Class.ClassID
                              orderby x.SubclassName
                               select x);

                    SubclassesView = CollectionViewSource.GetDefaultView(Tbl24SubclassesList);
                    SubclassesView.Refresh();

                    SelectedMainTabIndex = 1;
                }
                if (_selectedDetailSubTabIndex == 2)
                {
                    Tbl21ClassesAllList?.Clear();
                    Tbl21ClassesAllList = _allListVm.GetValueTbl21ClassesAllList();
                    Tbl90ExpertsAllList?.Clear();
                    Tbl90ExpertsAllList = _allListVm.GetValueTbl90ExpertsAllList();

                    Tbl90RefExpertsList?.Clear();
                    Tbl90RefExpertsList = new ObservableCollection<Tbl90Reference>
                    (from refEx in _tbl90ReferencesRepository.GetAll()
                     where refEx.ClassID == CurrentTbl21Class.ClassID
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
                    Tbl21ClassesAllList?.Clear();
                    Tbl21ClassesAllList = _allListVm.GetValueTbl21ClassesAllList();

                    Tbl93CommentsList?.Clear();
                    Tbl93CommentsList = new ObservableCollection<Tbl93Comment>
                    (from comm in _tbl93CommentsRepository.GetAll()
                     where comm.ClassID == CurrentTbl21Class.ClassID
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
                    Tbl21ClassesAllList?.Clear();
                    Tbl21ClassesAllList = _allListVm.GetValueTbl21ClassesAllList();

                    Tbl90RefExpertsList?.Clear();
                    Tbl90RefExpertsList = new ObservableCollection<Tbl90Reference>
                    (from refEx in _tbl90ReferencesRepository.GetAll()
                     where refEx.ClassID == CurrentTbl21Class.ClassID
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
                    Tbl21ClassesAllList?.Clear();
                    Tbl21ClassesAllList = _allListVm.GetValueTbl21ClassesAllList();

                    Tbl90RefSourcesList?.Clear();
                    Tbl90RefSourcesList = new ObservableCollection<Tbl90Reference>
                    (from refSo in _tbl90ReferencesRepository.GetAll()
                     where refSo.ClassID == CurrentTbl21Class.ClassID
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
                    Tbl21ClassesAllList?.Clear();
                    Tbl21ClassesAllList = _allListVm.GetValueTbl21ClassesAllList();

                    Tbl90RefAuthorsList?.Clear();
                    Tbl90RefAuthorsList = new ObservableCollection<Tbl90Reference>
                    (from refAu in _tbl90ReferencesRepository.GetAll()
                     where refAu.ClassID == CurrentTbl21Class.ClassID
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
