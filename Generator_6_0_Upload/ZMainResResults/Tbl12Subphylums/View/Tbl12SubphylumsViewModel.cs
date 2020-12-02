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

    
         //    Tbl12SubphylumsViewModel Skriptdatum:  01.12.2020  12:32    

namespace WPFUI.Views.Database
{     
    
    public class Tbl12SubphylumsViewModel : Tbl03RegnumsViewModel
    {     
        
        #region "Private Data Members"

        private readonly AllListVm _allListVm = new AllListVm();
        private readonly Repository<Tbl03Regnum, int> _tbl03RegnumsRepository = new Repository<Tbl03Regnum, int>();   
        private readonly Repository<Tbl12Subphylum, int> _tbl12SubphylumsRepository = new Repository<Tbl12Subphylum, int>();   
           
        private readonly Repository<Tbl06Phylum, int> _tbl06PhylumsRepository = new Repository<Tbl06Phylum, int>();   
           
        private readonly Repository<Tbl18Superclass, int> _tbl18SuperclassesRepository = new Repository<Tbl18Superclass, int>();   
           
        private readonly Repository<Tbl21Class, int> _tbl21ClassesRepository = new Repository<Tbl21Class, int>();   
          
        private readonly Repository<Tbl90Reference, int> _tbl90ReferencesRepository = new Repository<Tbl90Reference, int>();
        private readonly Repository<Tbl93Comment, int> _tbl93CommentsRepository = new Repository<Tbl93Comment, int>();    

        #endregion "Private Data Members"               
    
        #region "Constructor"

        public Tbl12SubphylumsViewModel()
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

           
        #region "Public Commands Basic Tbl12Subphylum"

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
Tbl06PhylumsAllList = _allListVm.GetValueTbl06PhylumsAllList();      
  Tbl03RegnumsAllList = _allListVm.GetValueTbl03RegnumsAllList();      
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
Tbl12SubphylumsList.Insert(0, new Tbl12Subphylum{ SubphylumName= CultRes.StringsRes.DatasetNew });  

            Tbl06PhylumsAllList = _allListVm.GetValueTbl06PhylumsAllList();    
  
            if (Tbl03RegnumsAllList == null)
                Tbl03RegnumsAllList = new ObservableCollection<Tbl03Regnum>
                                                                 (from z in _tbl03RegnumsRepository.GetAll()
                                                                  orderby z.RegnumName, z.Subregnum
                                                                  select z);  
SubphylumsView = CollectionViewSource.GetDefaultView(Tbl12SubphylumsList);
            SubphylumsView.Refresh();
        }
        //---------------------------------------------------------------------------------------  
           
        private RelayCommand _copySubphylumCommand;              
                
        public  ICommand CopySubphylumCommand             
         
        {
            get { return _copySubphylumCommand ?? (_copySubphylumCommand = new RelayCommand(delegate { CopySubphylum(null); })); }
        }

        private void CopySubphylum(object o)
        {
            Tbl12SubphylumsList = new ObservableCollection<Tbl12Subphylum>();

            var subphylum = _tbl12SubphylumsRepository.Get(CurrentTbl12Subphylum.SubphylumId);

            Tbl12SubphylumsList.Insert(0, new Tbl12Subphylum
            {                 
PhylumId = subphylum.PhylumId,              
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
                
        public  ICommand DeleteSubphylumCommand             
         
        {
            get { return _deleteSubphylumCommand ?? (_deleteSubphylumCommand = new RelayCommand(delegate { DeleteSubphylum(null); })); }
        }

        private void DeleteSubphylum(object o)
        {
            try
            {
                var subphylum = _tbl12SubphylumsRepository.Get(CurrentTbl12Subphylum.SubphylumId);
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
                    }    
SubphylumsView = CollectionViewSource.GetDefaultView(Tbl12SubphylumsList);
                         SubphylumsView.Refresh();
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
                var subphylum = _tbl12SubphylumsRepository.Get(CurrentTbl12Subphylum.SubphylumId);
                if (CurrentTbl12Subphylum == null)              
                    WpfMessageBox.Show(CultRes.StringsRes.Warning, CultRes.StringsRes.DatasetNotExist, 
                        MessageBoxButton.OK, MessageBoxImage.Warning);               
                else
                {
                    if (CurrentTbl12Subphylum.SubphylumId!= 0)
                    {
                        if (subphylum!= null) //update
                        {   
subphylum.SubphylumName = CurrentTbl12Subphylum.SubphylumName;  
subphylum.PhylumId = CurrentTbl12Subphylum.PhylumId;
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
PhylumId= CurrentTbl12Subphylum.PhylumId,              
                            SubphylumName= CurrentTbl12Subphylum.SubphylumName,              
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
                        //PhylumId may be not 0
                        if (CurrentTbl12Subphylum.PhylumId == 0)
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
                         a.PhylumId == CurrentTbl12Subphylum.PhylumId
                         select a);

                        if (dataset.Count != 0 && CurrentTbl12Subphylum.SubphylumId== 0)  //dataset exist
                        {
                              WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTbl12Subphylum.SubphylumName,
                              MessageBoxButton.OK, MessageBoxImage.Information);
                        } 

                        if (dataset.Count == 0 && CurrentTbl12Subphylum.SubphylumId== 0 ||
                            dataset.Count != 0 && CurrentTbl12Subphylum.SubphylumId != 0  ||
                            dataset.Count == 0 && CurrentTbl12Subphylum.SubphylumId != 0) //new dataset and update
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
         
                        if (SearchSubphylumName == null && CurrentTbl12Subphylum.SubphylumId == 0)  //new Dataset                        
                            Tbl12SubphylumsList = _allListVm.GetValueTbl12SubphylumsList();  //last Dataset
                        if (SearchSubphylumName == null && CurrentTbl12Subphylum.SubphylumId != 0)   //update 
                            Tbl12SubphylumsList = _allListVm.GetValueTbl12SubphylumsList(CurrentTbl12Subphylum.SubphylumId);
                        if (SearchSubphylumName != null && CurrentTbl12Subphylum.SubphylumId == 0)  //new Dataset                        
                            Tbl12SubphylumsList = _allListVm.GetValueTbl12SubphylumsList();  //last Dataset
                        if (SearchSubphylumName != null && CurrentTbl12Subphylum.SubphylumId != 0)   //update 
                            Tbl12SubphylumsList = _allListVm.GetValueTbl12SubphylumsList(CurrentTbl12Subphylum.SubphylumId);

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
      

 //    Part 2    

           
        #region "Public Commands Connect <== Tbl06Phylum"                 

        private RelayCommand _getPhylumByNameOrIdCommand;     
                
        public new ICommand GetPhylumByNameOrIdCommand      
    
        {
            get { return _getPhylumByNameOrIdCommand ?? (_getPhylumByNameOrIdCommand = new RelayCommand(delegate { GetPhylumByNameOrId(null); })); }   
        }

        private void GetPhylumByNameOrId(object o)       
        {   
    
            int id;
            if (int.TryParse(SearchPhylumName, out id))
                Tbl06PhylumsList = new ObservableCollection<Tbl06Phylum> { _tbl06PhylumsRepository.Get(id) };
            else
                Tbl06PhylumsList = _allListVm.GetValueTbl06PhylumsList(SearchPhylumName);     
PhylumsView = CollectionViewSource.GetDefaultView(Tbl06PhylumsList);
            PhylumsView.Refresh();
        }
        //------------------------------------------------------------------------------                
           
        private RelayCommand _addPhylumCommand;      
                
        public new ICommand AddPhylumCommand    
    
        {
            get { return _addPhylumCommand ?? (_addPhylumCommand = new RelayCommand(delegate { AddPhylum(null); })); }
        }

        private void AddPhylum(object o)
        {
            Tbl06PhylumsList = new ObservableCollection<Tbl06Phylum>();   
Tbl06PhylumsList.Insert(0, new Tbl06Phylum{ PhylumName = CultRes.StringsRes.DatasetNew });   

            if (Tbl03RegnumsAllList == null)
            Tbl03RegnumsAllList = _allListVm.GetValueTbl03RegnumsAllList();    
PhylumsView = CollectionViewSource.GetDefaultView(Tbl06PhylumsList);
            PhylumsView.Refresh();
        }
        //---------------------------------------------------------------------------------------  
           
        private RelayCommand _copyPhylumCommand;            
                
        public new ICommand CopyPhylumCommand          
         
        {
            get { return _copyPhylumCommand ?? (_copyPhylumCommand = new RelayCommand(delegate { CopyPhylum(null); })); }
        }

        private void CopyPhylum(object o)
        {
            Tbl06PhylumsList = new ObservableCollection<Tbl06Phylum>();

            var phylum = _tbl06PhylumsRepository.Get(CurrentTbl06Phylum.PhylumId);

            Tbl06PhylumsList.Insert(0, new Tbl06Phylum
            {                 
                
                RegnumID = phylum.RegnumID,
                PhylumName = CultRes.StringsRes.DatasetNew,     
                Valid = phylum.Valid,
                ValidYear = phylum.ValidYear,
                Synonym = phylum.Synonym,
                Author = phylum.Author,
                AuthorYear = phylum.AuthorYear,
                Info = phylum.Info,
                EngName = phylum.EngName,
                GerName = phylum.GerName,
                FraName = phylum.FraName,
                PorName = phylum.PorName,
                Memo = phylum.Memo           
        
            });

            PhylumsView = CollectionViewSource.GetDefaultView(Tbl06PhylumsList);
            PhylumsView.Refresh();
        }
        //---------------------------------------------------------------------------------------                  
           
        private RelayCommand _deletePhylumCommand;              
                
        public new ICommand DeletePhylumCommand             
         
        {
            get { return _deletePhylumCommand ?? (_deletePhylumCommand = new RelayCommand(delegate { DeletePhylum(null); })); }
        }

        private void DeletePhylum(object o)
        {
            try
            {
                var phylum = _tbl06PhylumsRepository.Get(CurrentTbl06Phylum.PhylumId);
                if (phylum!= null)
                {  
         
                    if (WpfMessageBox.Show(CultRes.StringsRes.DeleteQuestion1, CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl06Phylum.PhylumName,
                         MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes) 
                    return;

                    _tbl06PhylumsRepository.Delete(phylum);
                    _tbl06PhylumsRepository.Save();

                    WpfMessageBox.Show(CultRes.StringsRes.DeleteSuccess, CurrentTbl06Phylum.PhylumName, 
                        MessageBoxButton.OK, MessageBoxImage.Information);  
         
                        if (SearchPhylumName == null)                       
                            GetConnectedTablesById(o); //refresh doubleClick                                          
                        else
                        {
                        Tbl06PhylumsList = _allListVm.GetValueTbl06PhylumsList(SearchPhylumName);  
PhylumsView = CollectionViewSource.GetDefaultView(Tbl06PhylumsList);
                            PhylumsView.Refresh();
                    }
                }
                else
                {   
    
                    WpfMessageBox.Show(CultRes.StringsRes.Information, CultRes.StringsRes.DeleteCan + " " + CurrentTbl06Phylum.PhylumName+ " " + CultRes.StringsRes.DeleteCan1,
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
           
        private RelayCommand _savePhylumCommand;              
                
        public new ICommand SavePhylumCommand             
         
        {
            get { return _savePhylumCommand ?? (_savePhylumCommand = new RelayCommand(delegate { SavePhylum(null); })); }
        }

        private void SavePhylum(object o)
        {
            try
            {
                var phylum = _tbl06PhylumsRepository.Get(CurrentTbl06Phylum.PhylumId);
                if (CurrentTbl06Phylum == null)              
                    WpfMessageBox.Show(CultRes.StringsRes.Warning, CultRes.StringsRes.DatasetNotExist, MessageBoxButton.OK, MessageBoxImage.Warning);               
                else
                {
                    if (CurrentTbl06Phylum.PhylumId!= 0)
                    {
                        if (phylum!= null) //update
                        {   
phylum.RegnumId = CurrentTbl06Phylum.RegnumId;
                            phylum.PhylumName= CurrentTbl06Phylum.PhylumName;             
phylum.Valid = CurrentTbl06Phylum.Valid;
                            phylum.ValidYear = CurrentTbl06Phylum.ValidYear;
                            phylum.Synonym = CurrentTbl06Phylum.Synonym;
                            phylum.Author = CurrentTbl06Phylum.Author;
                            phylum.AuthorYear = CurrentTbl06Phylum.AuthorYear;
                            phylum.Info = CurrentTbl06Phylum.Info;
                            phylum.EngName = CurrentTbl06Phylum.EngName;
                            phylum.GerName = CurrentTbl06Phylum.GerName;
                            phylum.FraName = CurrentTbl06Phylum.FraName;
                            phylum.PorName = CurrentTbl06Phylum.PorName;
                            phylum.Updater = Environment.UserName;
                            phylum.UpdaterDate = DateTime.Now; 
                            phylum.Memo = CurrentTbl06Phylum.Memo;   
         
                        }
                    }
                    else
                    {
                        _tbl06PhylumsRepository.Add(new Tbl06Phylum     //add new
                        {   
RegnumId = CurrentTbl06Phylum.RegnumId,     
                            PhylumName= CurrentTbl06Phylum.PhylumName,              
                            CountID = RandomHelper.Randomnumber(),
                            Valid = CurrentTbl06Phylum.Valid,
                            ValidYear = CurrentTbl06Phylum.ValidYear,
                            Synonym = CurrentTbl06Phylum.Synonym,
                            Author = CurrentTbl06Phylum.Author,
                            AuthorYear = CurrentTbl06Phylum.AuthorYear,
                            Info = CurrentTbl06Phylum.Info,
                            EngName = CurrentTbl06Phylum.EngName,
                            GerName = CurrentTbl06Phylum.GerName,
                            FraName = CurrentTbl06Phylum.FraName,
                            PorName = CurrentTbl06Phylum.PorName,
                            Writer = Environment.UserName,
                            WriterDate = DateTime.Now,
                            Updater = Environment.UserName,
                            UpdaterDate = DateTime.Now,
                            Memo = CurrentTbl06Phylum.Memo   
         
                        });
                    }
                    {
                        //RegnumId may be not 0
                        if (CurrentTbl06Phylum.RegnumId == 0)
                        {
                            WpfMessageBox.Show(CultRes.StringsRes.RequiredGenealogyConnect, CultRes.StringsRes.RequiredInput,
                                MessageBoxButton.OK, MessageBoxImage.Information);
                            return;
                        }

                        //check if dataset with Name and vb-name already exist
                        var dataset = new ObservableCollection<Tbl06Phylum>
                        (from a in _tbl06PhylumsRepository.GetAll()
                         where
                         a.PhylumName.Trim() == CurrentTbl06Phylum.PhylumName.Trim() &&                
                         a.RegnumId == CurrentTbl06Phylum.RegnumId
                         select a);

                        if (dataset.Count != 0 && CurrentTbl06Phylum.PhylumId== 0)  //dataset exist
                        {
                              WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTbl06Phylum.PhylumName,
                              MessageBoxButton.OK, MessageBoxImage.Information);
                        }

                        if (dataset.Count == 0 && CurrentTbl06Phylum.PhylumId == 0 ||
                            dataset.Count != 0 && CurrentTbl06Phylum.PhylumId != 0  ||
                            dataset.Count == 0 && CurrentTbl06Phylum.PhylumId != 0) //new dataset and update
                        {
                            if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTbl06Phylum.PhylumName,
                                    MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)                            
                                return;                            
                            {
                                _tbl06PhylumsRepository.Save();

                                WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess, CurrentTbl06Phylum.PhylumName,
                                    MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                        }  
        
                        if (SearchPhylumName == null && CurrentTbl06Phylum.PhylumId == 0)  //new Dataset                        
                            GetConnectedTablesById(o); //refresh doubleClick                                          
                        if (SearchPhylumName == null && CurrentTbl06Phylum.PhylumId != 0)  //update                     
                            Tbl06PhylumsList = _allListVm.GetValueTbl06PhylumsList(CurrentTbl06Phylum.PhylumId);
                        if (SearchPhylumName != null && CurrentTbl06Phylum.PhylumId == 0)  //new Dataset                        
                            GetConnectedTablesById(o); //refresh doubleClick                                          
                        if (SearchPhylumName != null && CurrentTbl06Phylum.PhylumId != 0)  //update                     
                            Tbl06PhylumsList = _allListVm.GetValueTbl06PhylumsList(CurrentTbl06Phylum.PhylumId); 

                        PhylumsView = CollectionViewSource.GetDefaultView(Tbl06PhylumsList);
                        PhylumsView.Refresh();         
      
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

             
        #region "Public Commands Connect ==> Tbl18Superclass"                 

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

            if (Tbl12SubphylumsAllList == null)
            Tbl12SubphylumsAllList = _allListVm.GetValueTbl12SubphylumsAllList();     
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

            var superclass = _tbl18SuperclassesRepository.Get(CurrentTbl18Superclass.SuperclassId);

            Tbl18SuperclassesList.Insert(0, new Tbl18Superclass
            {                 
                                   
                SubphylumID = superclass.SubphylumID,
                SubdivisionID = 1,
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
                var superclass = _tbl18SuperclassesRepository.Get(CurrentTbl18Superclass.SuperclassId);
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
  SuperclassesView = CollectionViewSource.GetDefaultView(Tbl18SuperclassesList);
                            SuperclassesView.Refresh();
                    }
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
                var superclass = _tbl18SuperclassesRepository.Get(CurrentTbl18Superclass.SuperclassId);
                if (CurrentTbl18Superclass == null)               
                    WpfMessageBox.Show(CultRes.StringsRes.Warning, CultRes.StringsRes.DatasetNotExist, 
                        MessageBoxButton.OK, MessageBoxImage.Warning);              
                else
                {
                    if (CurrentTbl18Superclass.SuperclassId!= 0)
                    {
                        if (superclass!= null) //update
                        {   
  superclass.SubphylumId= CurrentTbl18Superclass.SubphylumId;            
                            superclass.SuperclassName= CurrentTbl18Superclass.SuperclassName;
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
                        _tbl18SuperclassesRepository.Add(new Tbl18Superclass    // add new
                        {   
                  
                            SubphylumID = CurrentTbl18Superclass.SubphylumID ,              
                            SubdivisionID = 1,
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
                        //SubphylumId may be not 0
                        if (CurrentTbl18Superclass.SubphylumId == 0)
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
                         a.SubphylumId == CurrentTbl18Superclass.SubphylumId
                         select a);

                        if (dataset.Count != 0 && CurrentTbl18Superclass.SuperclassId== 0)  //dataset exist
                        {
                              WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTbl18Superclass.SuperclassName,
                              MessageBoxButton.OK, MessageBoxImage.Information);
                        }

                        if (dataset.Count == 0 && CurrentTbl18Superclass.SuperclassId == 0 ||
                            dataset.Count != 0 && CurrentTbl18Superclass.SuperclassId != 0  ||
                            dataset.Count == 0 && CurrentTbl18Superclass.SuperclassId != 0) //new dataset and update
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
                
                        if (SearchSuperclassName == null && CurrentTbl18Superclass.SuperclassId == 0)  //new Dataset                        
                            Tbl18SuperclassesList = _allListVm.GetValueTbl18SuperclassesList();  //last Dataset
                        if (SearchSuperclassName == null && CurrentTbl18Superclass.SuperclassId != 0)   //update
                            Tbl18SuperclassesList = _allListVm.GetValueTbl18SuperclassesList(CurrentTbl18Superclass.SuperclassId);
                        if (SearchSuperclassName != null && CurrentTbl18Superclass.SuperclassId == 0)  //new Dataset
                            Tbl18SuperclassesList = _allListVm.GetValueTbl18SuperclassesList();  //last Dataset
                        if (SearchSuperclassName != null && CurrentTbl18Superclass.SuperclassId != 0)   //update
                            Tbl18SuperclassesList = _allListVm.GetValueTbl18SuperclassesList(CurrentTbl18Superclass.SuperclassId);
                                                                       

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
        

 //    Part 5    


             
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
                Tbl21ClassesList = _allListVm.GetValueTbl21ClassesList (SearchClassName);      
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
            Tbl12SubphylumsAllList = _allListVm.GetValueTbl12SubphylumsAllList();    
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

            var  = _tbl21ClassesRepository.Get(CurrentTbl21Class.ClassID);

            Tbl21ClassesList.Insert(0, new Tbl21Class
            {                 
  SubphylumId = .SubphylumId,     
                ClassName = CultRes.StringsRes.DatasetNew,     
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
                var  = _tbl21ClassesRepository.Get(CurrentTbl21Class.ClassID);
                if (!= null)
                {  
                                                    
                    if (WpfMessageBox.Show(CultRes.StringsRes.DeleteQuestion1, CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl21Class.ClassName,
                         MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes) 
                    return;

                    _tbl21ClassesRepository.Delete();
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
                var  = _tbl21ClassesRepository.Get(CurrentTbl21Class.ClassID);
                if (CurrentTbl21Class == null)                
                    WpfMessageBox.Show(CultRes.StringsRes.Warning, CultRes.StringsRes.DatasetNotExist, MessageBoxButton.OK, MessageBoxImage.Warning);              
                else
                {
                    if (CurrentTbl21Class.ClassID!= 0)
                    {
                        if (!= null) //update
                        {   
  .SubphylumId= CurrentTbl21Class.SubphylumId;            
                            .ClassName= CurrentTbl21Class.ClassName;
                            .Valid = CurrentTbl21Class.Valid;
                            .ValidYear = CurrentTbl21Class.ValidYear;
                            .Synonym = CurrentTbl21Class.Synonym;
                            .Author = CurrentTbl21Class.Author;
                            .AuthorYear = CurrentTbl21Class.AuthorYear;
                            .Info = CurrentTbl21Class.Info;
                            .EngName = CurrentTbl21Class.EngName;
                            .GerName = CurrentTbl21Class.GerName;
                            .FraName = CurrentTbl21Class.FraName;
                            .PorName = CurrentTbl21Class.PorName;
                            .Updater = Environment.UserName;
                            .UpdaterDate = DateTime.Now;
                            .Memo = CurrentTbl21Class.Memo;                                                              
                                                                            
                        }
                    }
                    else
                    {
                        _tbl21ClassesRepository.Add(new Tbl21Class     //add new
                        {   
  SubphylumId = CurrentTbl21Class.SubphylumId,              
                            ClassName = CurrentTbl21Class.ClassName,              
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
                        //SubphylumId may be not 0
                        if (CurrentTbl21Class.SubphylumId == 0)
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
                         a.SubphylumId == CurrentTbl21Class.SubphylumId
                         select a);

                        if (dataset.Count != 0 && CurrentTbl21Class.ClassID == 0)  //dataset exist
                        {
                              WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTbl21Class.ClassName,
                              MessageBoxButton.OK, MessageBoxImage.Information);
                        }

                        if (dataset.Count == 0 && CurrentTbl21Class.ClassID== 0 ||
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

           Tbl12SubphylumsAllList = _allListVm.GetValueTbl12SubphylumsAllList();
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
SubphylumId = refAuthor.SubphylumId,              
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
         
                            refAuthor.SubphylumId = CurrentTbl90RefAuthor.SubphylumId;  
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
SubphylumId = CurrentTbl90RefAuthor.SubphylumId,              
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
                        //SubphylumId may be not 0
                        if (CurrentTbl90RefAuthor.SubphylumId == 0 || CurrentTbl90RefAuthor.RefAuthorID == 0)
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
                         a.SubphylumId == CurrentTbl90RefAuthor.SubphylumId &&
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

            Tbl12SubphylumsAllList = _allListVm.GetValueTbl12SubphylumsAllList();
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
SubphylumId = refSource.SubphylumId,              
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
         
                            refSource.SubphylumId = CurrentTbl90RefSource.SubphylumId;            
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
SubphylumId = CurrentTbl90RefSource.SubphylumId,              
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
                        //SubphylumId may be not 0
                        if (CurrentTbl90RefSource.SubphylumId == 0 || CurrentTbl90RefSource.RefSourceID == 0)
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
                         a.SubphylumId == CurrentTbl90RefSource.SubphylumId &&
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

            Tbl12SubphylumsAllList = _allListVm.GetValueTbl12SubphylumsAllList();
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
SubphylumId = refExpert.SubphylumId,              
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
      
                            refExpert.SubphylumId = CurrentTbl90RefExpert.SubphylumId;           
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
SubphylumId = CurrentTbl90RefExpert.SubphylumId,              
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
                        //SubphylumId may be not 0
                        if (CurrentTbl90RefExpert.SubphylumId == 0 || CurrentTbl90RefExpert.RefExpertID == 0)   
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
                         a.SubphylumId == CurrentTbl90RefExpert.SubphylumId &&
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

            Tbl12SubphylumsAllList = _allListVm.GetValueTbl12SubphylumsAllList();      
    

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
SubphylumId = comment.SubphylumId,              
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
      
                            comment.SubphylumId = CurrentTbl93Comment.SubphylumId;            
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
SubphylumId = CurrentTbl93Comment.SubphylumId,              
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
                        //SubphylumId may be not 0
                        if (CurrentTbl93Comment.SubphylumId == 0)
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
                         a.SubphylumId == CurrentTbl93Comment.SubphylumId
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

        private  void GetConnectedTablesById(object o)
        {
            SelectedDetailThreeRefTabIndex = 1;  //change to Connect tab

            Tbl06PhylumsList =  new ObservableCollection<Tbl06Phylum>
                                                       (from x in _tbl06PhylumsRepository.GetAll()
                                                       where x.PhylumId== CurrentTbl12Subphylum.PhylumId
                                                       orderby x.PhylumName
                                                       select x);

            PhylumsView = CollectionViewSource.GetDefaultView(Tbl06PhylumsList);
            PhylumsView.Refresh();
            //-----------------------------------------------------------------------------------
            Tbl18SuperclassesList =  new ObservableCollection<Tbl18Superclass>
                                                       (from y in _tbl18SuperclassesRepository.GetAll()
                                                       where y.SubphylumId== CurrentTbl12Subphylum.SubphylumId
                                                       orderby y.Tbl12Subphylums.SubphylumName
                                                       select y);


            SuperclassesView = CollectionViewSource.GetDefaultView(Tbl18SuperclassesList);
            SuperclassesView.Refresh();
            //-----------------------------------------------------------------------------------
            Tbl90RefAuthorsList =  new ObservableCollection<Tbl90Reference>
                                                          (from refAu in _tbl90ReferencesRepository.GetAll()
                                                          where refAu.SubphylumId== CurrentTbl12Subphylum.SubphylumId
                                                          && refAu.RefExpertID == null
                                                          && refAu.RefSourceID == null
                                                          orderby refAu.Tbl90RefAuthors.RefAuthorName, refAu.Tbl90RefAuthors.BookName, refAu.Tbl90RefAuthors.Page1
                                                          select refAu);

            RefAuthorsView = CollectionViewSource.GetDefaultView(Tbl90RefAuthorsList);
            RefAuthorsView.Refresh();
            //--------------------------------------------------------------------------------------
            Tbl90RefSourcesList =  new ObservableCollection<Tbl90Reference>
                                                          (from refSo in _tbl90ReferencesRepository.GetAll()
                                                          where refSo.SubphylumId== CurrentTbl12Subphylum.SubphylumId
                                                          && refSo.RefExpertID == null
                                                          && refSo.RefAuthorID == null
                                                          orderby refSo.Tbl90RefSources.RefSourceName
                                                          select refSo);

            RefSourcesView = CollectionViewSource.GetDefaultView(Tbl90RefSourcesList);
            RefSourcesView.Refresh();
            //--------------------------------------------------------------------------------------
            Tbl90RefExpertsList =   new ObservableCollection<Tbl90Reference>
                                                          (from refEx in _tbl90ReferencesRepository.GetAll()
                                                          where refEx.SubphylumId== CurrentTbl12Subphylum.SubphylumId
                                                          && refEx.RefAuthorID == null
                                                          && refEx.RefSourceID == null
                                                          orderby refEx.Tbl90RefExperts.RefExpertName
                                                          select refEx);

            RefExpertsView = CollectionViewSource.GetDefaultView(Tbl90RefExpertsList);
            RefExpertsView.Refresh();
            //-----------------------------------------------------------------------------------
            Tbl93CommentsList =  new ObservableCollection<Tbl93Comment>
                                                        (from comm in _tbl93CommentsRepository.GetAll()
                                                        where comm.SubphylumId== CurrentTbl12Subphylum.SubphylumId
                                                        orderby comm.Info
                                                        select comm);

            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            CommentsView.Refresh();
            //------------------------------------------------------------------------------------
            Tbl03RegnumsAllList = _allListVm.GetValueTbl03RegnumsAllList();

            Tbl12SubphylumsAllList = _allListVm.GetValueTbl12SubphylumsAllList();

            Tbl90AuthorsAllList = _allListVm.GetValueTbl90AuthorsAllList();

            Tbl90SourcesAllList = _allListVm.GetValueTbl90SourcesAllList();

            Tbl90ExpertsAllList = _allListVm.GetValueTbl90ExpertsAllList();
        }
        #endregion "Public Commands Connected Tables by DoubleClick"
   
 

 //    Part 10    

 

 //    Part 11    

     
        #region "Public Properties Tbl12Subphylum"

        private string _searchSubphylumName;
        public string SearchSubphylumName
        {
            get => _searchSubphylumName; 
            set { _searchSubphylumName = value; RaisePropertyChanged();  }
        }

        public ICollectionView SubphylumsView;
        public Tbl12Subphylum CurrentTbl12Subphylum => SubphylumsView?.CurrentItem as Tbl12Subphylum;

        private ObservableCollection<Tbl12Subphylum> _tbl12SubphylumsList;
        public ObservableCollection<Tbl12Subphylum> Tbl12SubphylumsList
        {
            get => _tbl12SubphylumsList; 
            set {  _tbl12SubphylumsList = value; RaisePropertyChanged();   }
        }

        private ObservableCollection<Tbl12Subphylum> _tbl12SubphylumsAllList;
        public ObservableCollection<Tbl12Subphylum> Tbl12SubphylumsAllList
        {
            get => _tbl12SubphylumsAllList; 
            set { _tbl12SubphylumsAllList = value; RaisePropertyChanged(); }
        }

        #endregion "Public Properties"   
       
        #region "Public Properties Tbl06Phylum"

        private string _searchPhylumName;
        public new string SearchPhylumName
        {
            get => _searchPhylumName; 
            set { _searchPhylumName = value; RaisePropertyChanged(); }
        }

        public new ICollectionView PhylumsView;
        public new Tbl06Phylum CurrentTbl06Phylum => PhylumsView?.CurrentItem as Tbl06Phylum;           

        private ObservableCollection<Tbl06Phylum> _tbl06PhylumsList;
        public new ObservableCollection<Tbl06Phylum> Tbl06PhylumsList
        {
            get => _tbl06PhylumsList; 
            set { _tbl06PhylumsList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl06Phylum> _tbl06PhylumsAllList;
        public new ObservableCollection<Tbl06Phylum> Tbl06PhylumsAllList
        {
            get => _tbl06PhylumsAllList; 
            set { _tbl06PhylumsAllList = value; RaisePropertyChanged(); }       
        }

        #endregion "Public Properties"
   
  
       
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
        public ObservableCollection<Tbl18Superclass> Tbl18SuperclassesList
        {
            get => _tbl18SuperclassesList; 
            set { _tbl18SuperclassesList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl18Superclass> _tbl18SuperclassesAllList;
        public ObservableCollection<Tbl18Superclass> Tbl18SuperclassesAllList
        {
            get => _tbl18SuperclassesAllList; 
            set { _tbl18SuperclassesAllList = value; RaisePropertyChanged(); }       
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

        private ObservableCollection<Tbl21Class> _tbl21ClassesAllList;
        public ObservableCollection<Tbl21Class> Tbl21ClassesAllList
        {
            get => _tbl21ClassesAllList; 
            set { _tbl21ClassesAllList = value; RaisePropertyChanged(); }       
        }

        #endregion "Public Properties"
   
   

 //    Part 12    

 

   }
}   
