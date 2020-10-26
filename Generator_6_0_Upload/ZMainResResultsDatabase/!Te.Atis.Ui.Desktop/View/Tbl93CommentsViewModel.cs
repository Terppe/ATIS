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

    
using GalaSoft.MvvmLight; 
    
         //    Tbl93CommentsViewModel Skriptdatum:  29.11.2018  10:32    

namespace WPFUI.Views.Database
{     
    
    public class Tbl93CommentsViewModel : ViewModelBase                     
    {     
    
      #region "Private Data Members"

        private readonly AllListVm _allListVm = new AllListVm();
        private readonly Repository<Tbl93Comment, int> _tbl93CommentsRepository = new Repository<Tbl93Comment, int>();

        #endregion "Private Data Members"            
    
        #region "Constructor"

        public Tbl93CommentsViewModel()
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

           
        #region "Public Commands Basic Tbl93Comment"

        private RelayCommand _getCommentByNameOrIdCommand;     
    
        public ICommand GetCommentByNameOrIdCommand    
    
        {
            get { return _getCommentByNameOrIdCommand ?? (_getCommentByNameOrIdCommand = new RelayCommand(delegate { GetCommentByNameOrId(null); })); }   
        }

        private void GetCommentByNameOrId(object o)       
        {   
       
            int id;
            if (int.TryParse(SearchCommentInfo, out id))
                Tbl93CommentsList = new ObservableCollection<Tbl93Comment> { _tbl93CommentsRepository.Get(id) };
            else   
                Tbl93CommentsList = _allListVm.GetValueTbl93CommentsList(SearchCommentInfo);      
        
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
CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            CommentsView.Refresh();
        }
        //------------------------------------------------------------------------------                
           
        private RelayCommand _addCommentCommand;           
    
        public ICommand AddCommentCommand       
    
        {
            get { return _addCommentCommand ?? (_addCommentCommand = new RelayCommand(delegate { AddComment(null); })); }
        }

        private void AddComment(object o)
        {
            Tbl93CommentsList = new ObservableCollection<Tbl93Comment>();   
Tbl93CommentsList.Insert(0, new Tbl93Comment{ CommentID = 0 });     
               
            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            CommentsView.Refresh();
        }
        //------------------------------------------------------------------------------                
           
        private RelayCommand _copyCommentCommand;              
    
        public ICommand CopyCommentCommand             
         
        {
            get { return _copyCommentCommand ?? (_copyCommentCommand = new RelayCommand(delegate { CopyComment(null); })); }
        }

        private void CopyComment(object o)
        {
            Tbl93CommentsList = new ObservableCollection<Tbl93Comment>();

            var comment = _tbl93CommentsRepository.Get(CurrentTbl93Comment.CommentID);

            Tbl93CommentsList.Insert(0, new Tbl93Comment
            {                 
       
                            Valid = comment.Valid,
                            ValidYear = comment.ValidYear,              
                            Info = comment.Info,
                            Memo = comment.Memo        
               
            });
            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            CommentsView.Refresh();
        }
        //------------------------------------------------------------------------------                
           
        private RelayCommand _deleteCommentCommand;              
    
        public ICommand DeleteCommentCommand             
                
        {
            get { return _deleteCommentCommand ?? (_deleteCommentCommand = new RelayCommand(delegate { DeleteComment(null); })); }
        }

        private void DeleteComment(object o)
        {
            try
            {
                var comment = _tbl93CommentsRepository.Get(CurrentTbl93Comment.CommentID);
                if (comment!= null)
                {
                    if (WpfMessageBox.Show(CultRes.StringsRes.DeleteQuestion1, CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl93Comment.Info,
                         MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes) 
                    return;

                    _tbl93CommentsRepository.Delete(comment);
                    _tbl93CommentsRepository.Save();

                    WpfMessageBox.Show(CultRes.StringsRes.DeleteSuccess, CurrentTbl93Comment.Info, 
                        MessageBoxButton.OK, MessageBoxImage.Information);

                    Tbl93CommentsList = _allListVm.GetValueTbl93CommentsList(SearchCommentInfo);                                                            
CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
                        CommentsView.Refresh();
                }
                else
                {
                    WpfMessageBox.Show(CultRes.StringsRes.Information, CultRes.StringsRes.DeleteCan + " " + CurrentTbl93Comment.Info+ " " + CultRes.StringsRes.DeleteCan1,
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
     
        public ICommand SaveCommentCommand             
         
        {
            get { return _saveCommentCommand ?? (_saveCommentCommand = new RelayCommand(delegate { SaveComment(null); })); }
        }

        private void SaveComment(object o)
        {
            try
            {
                var comment = _tbl93CommentsRepository.Get(CurrentTbl93Comment.CommentID);
                if (CurrentTbl93Comment == null)              
                    WpfMessageBox.Show(CultRes.StringsRes.Warning, CultRes.StringsRes.DatasetNotExist, 
                        MessageBoxButton.OK, MessageBoxImage.Warning);               
                else
                {
                    if (CurrentTbl93Comment.CommentID!= 0)
                    {
                        if (comment!= null) //update
                        {   
               
                            comment.RegnumID = CurrentTbl93Comment.RegnumID;
                            comment.PhylumID = CurrentTbl93Comment.PhylumID;
                            comment.DivisionID = CurrentTbl93Comment.DivisionID;
                            comment.SubphylumID = CurrentTbl93Comment.SubphylumID;
                            comment.SubdivisionID = CurrentTbl93Comment.SubdivisionID;
                            comment.SuperclassID = CurrentTbl93Comment.SuperclassID;
                            comment.ClassID = CurrentTbl93Comment.ClassID;
                            comment.SubclassID = CurrentTbl93Comment.SubclassID;
                            comment.InfraclassID = CurrentTbl93Comment.InfraclassID;
                            comment.LegioID = CurrentTbl93Comment.LegioID;
                            comment.OrdoID = CurrentTbl93Comment.OrdoID;
                            comment.SubordoID = CurrentTbl93Comment.SubordoID;
                            comment.InfraordoID = CurrentTbl93Comment.InfraordoID;
                            comment.SuperfamilyID = CurrentTbl93Comment.SuperfamilyID;
                            comment.FamilyID = CurrentTbl93Comment.FamilyID;
                            comment.SubfamilyID = CurrentTbl93Comment.SubfamilyID;
                            comment.InfrafamilyID = CurrentTbl93Comment.InfrafamilyID;
                            comment.SupertribusID = CurrentTbl93Comment.SupertribusID;
                            comment.TribusID = CurrentTbl93Comment.TribusID;
                            comment.SubtribusID = CurrentTbl93Comment.SubtribusID;
                            comment.InfratribusID = CurrentTbl93Comment.InfratribusID;
                            comment.GenusID = CurrentTbl93Comment.GenusID;
                            comment.PlSpeciesID = CurrentTbl93Comment.PlSpeciesID;
                            comment.FiSpeciesID = CurrentTbl93Comment.FiSpeciesID;
                            comment.Valid = CurrentTbl93Comment.Valid;
                            comment.ValidYear = CurrentTbl93Comment.ValidYear;
                            comment.Info = CurrentTbl93Comment.Info;
                            comment.Memo = CurrentTbl93Comment.Memo;
                            comment.Updater = Environment.UserName;
                            comment.UpdaterDate = DateTime.Now;                                                  
         
                        }
                    }
                    else
                    {
                        _tbl93CommentsRepository.Add(new Tbl93Comment     //add new
                        {   
                
                            RegnumID = CurrentTbl93Comment.RegnumID,
                            PhylumID = CurrentTbl93Comment.PhylumID,
                            DivisionID = CurrentTbl93Comment.DivisionID,
                            SubphylumID = CurrentTbl93Comment.SubphylumID,
                            SubdivisionID = CurrentTbl93Comment.SubdivisionID,
                            SuperclassID = CurrentTbl93Comment.SuperclassID,
                            ClassID = CurrentTbl93Comment.ClassID,
                            SubclassID = CurrentTbl93Comment.SubclassID,
                            InfraclassID = CurrentTbl93Comment.InfraclassID,
                            LegioID = CurrentTbl93Comment.LegioID,
                            OrdoID = CurrentTbl93Comment.OrdoID,
                            SubordoID = CurrentTbl93Comment.SubordoID,
                            InfraordoID = CurrentTbl93Comment.InfraordoID,
                            SuperfamilyID = CurrentTbl93Comment.SuperfamilyID,
                            FamilyID = CurrentTbl93Comment.FamilyID,
                            SubfamilyID = CurrentTbl93Comment.SubfamilyID,
                            InfrafamilyID = CurrentTbl93Comment.InfrafamilyID,
                            SupertribusID = CurrentTbl93Comment.SupertribusID,
                            TribusID = CurrentTbl93Comment.TribusID,
                            SubtribusID = CurrentTbl93Comment.SubtribusID,
                            InfratribusID = CurrentTbl93Comment.InfratribusID,
                            GenusID = CurrentTbl93Comment.GenusID,
                            PlSpeciesID = CurrentTbl93Comment.PlSpeciesID,
                            FiSpeciesID = CurrentTbl93Comment.FiSpeciesID,
                            CountID = RandomHelper.Randomnumber(),
                            Valid = CurrentTbl93Comment.Valid,
                            ValidYear = CurrentTbl93Comment.ValidYear,
                            Info = CurrentTbl93Comment.Info,
                            Memo = CurrentTbl93Comment.Memo,
                            Writer = Environment.UserName,
                            WriterDate = DateTime.Now,
                            Updater = Environment.UserName,
                            UpdaterDate = DateTime.Now  
                
                        });
                    }
                    {
                        //check about double Name
                        var dataset = new ObservableCollection<Tbl93Comment>
                        (from a in _tbl93CommentsRepository.GetAll()
                         where
                         a.Info.Trim() == CurrentTbl93Comment.Info.Trim() &&
                            a.RegnumID == CurrentTbl93Comment.RegnumID &&
                            a.PhylumID == CurrentTbl93Comment.PhylumID &&
                            a.DivisionID == CurrentTbl93Comment.DivisionID &&
                            a.SubphylumID == CurrentTbl93Comment.SubphylumID  &&
                            a.SubdivisionID == CurrentTbl93Comment.SubdivisionID &&
                            a.SuperclassID == CurrentTbl93Comment.SuperclassID &&
                            a.ClassID == CurrentTbl93Comment.ClassID &&
                            a.SubclassID == CurrentTbl93Comment.SubclassID &&
                            a.InfraclassID == CurrentTbl93Comment.InfraclassID &&
                            a.LegioID == CurrentTbl93Comment.LegioID &&
                            a.OrdoID == CurrentTbl93Comment.OrdoID &&
                            a.SubordoID == CurrentTbl93Comment.SubordoID &&
                            a.InfraordoID == CurrentTbl93Comment.InfraordoID &&
                            a.SuperfamilyID == CurrentTbl93Comment.SuperfamilyID &&
                            a.FamilyID == CurrentTbl93Comment.FamilyID &&
                            a.SubfamilyID == CurrentTbl93Comment.SubfamilyID &&
                            a.InfrafamilyID == CurrentTbl93Comment.InfrafamilyID &&
                            a.SupertribusID == CurrentTbl93Comment.SupertribusID &&
                            a.TribusID == CurrentTbl93Comment.TribusID &&
                            a.SubtribusID == CurrentTbl93Comment.SubtribusID &&
                            a.InfratribusID == CurrentTbl93Comment.InfratribusID &&
                            a.GenusID == CurrentTbl93Comment.GenusID &&
                            a.PlSpeciesID == CurrentTbl93Comment.PlSpeciesID &&
                         a.FiSpeciesID == CurrentTbl93Comment.FiSpeciesID
                         select a);

                        if (dataset.Count != 0 && CurrentTbl93Comment.CommentID == 0)  //dataset exist
                        {
                            WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTbl93Comment.Info,
                            MessageBoxButton.OK, MessageBoxImage.Information);
                        }

                        if (dataset.Count == 0 && CurrentTbl93Comment.CommentID == 0 ||
                            dataset.Count != 0 && CurrentTbl93Comment.CommentID != 0 ||
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
                
                        if (CurrentTbl93Comment.CommentID == 0)                        
                        {
                            Tbl93CommentsList= new ObservableCollection<Tbl93Comment>
                              { new ObservableCollection<Tbl93Comment>
                                  (from x in _tbl93CommentsRepository.Tables
                                   select x).LastOrDefault()
                              };
                            //last newest Dataset
                        }
                        else
                        {
                            Tbl93CommentsList = new ObservableCollection<Tbl93Comment>
                                                  (from x in _tbl93CommentsRepository.GetAll()
                                                   where x.CommentID == CurrentTbl93Comment.CommentID
                                                   select x);
                        }

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
      

 //    Part 2    

      

 //    Part 3    

      

 //    Part 4    

      

 //    Part 5    


      

 //    Part 6    

      

 //    Part 7    

      

 //    Part 8    

      

 //    Part 9    

 

 //    Part 10    

    
        #region "Public Commands to open Detail TabItems"

        private int _selectedMainTabIndex;
        public int SelectedMainTabIndex
        {
            get => _selectedMainTabIndex; 
            set
            {
                if (value == _selectedMainTabIndex) return;
                    _selectedMainTabIndex = value;  RaisePropertyChanged();
                if (_selectedMainTabIndex == 0)
                    SelectedDetailTabIndex = 0;
                if (_selectedMainTabIndex == 1)
                    SelectedDetailTabIndex = 1;
                if (_selectedMainTabIndex == 2)
                    SelectedDetailTabIndex = 2;
                if (_selectedMainTabIndex == 3)
                    SelectedDetailTabIndex = 3;
                if (_selectedMainTabIndex == 4)
                    SelectedDetailTabIndex = 4;
                if (_selectedMainTabIndex == 5)
                    SelectedDetailTabIndex = 5;
                if (_selectedMainTabIndex == 6)
                    SelectedDetailTabIndex = 6;
                if (_selectedMainTabIndex == 7)
                    SelectedDetailTabIndex = 7;
            }
        }

        private int _selectedMainSubTabIndex;
        public int SelectedMainSubTabIndex
        {
            get => _selectedMainSubTabIndex; 
            set
            {
                if (value == _selectedMainSubTabIndex) return;
                    _selectedMainSubTabIndex= value;  RaisePropertyChanged();
                if (_selectedMainSubTabIndex == 0)
                    SelectedDetailSubTabIndex = 0;
                if (_selectedMainSubTabIndex == 1)
                    SelectedDetailSubTabIndex = 1;
                if (_selectedMainSubTabIndex == 2)
                    SelectedDetailSubTabIndex = 2;
            }
        }

        private int _selectedMainSubRefTabIndex;
        public int SelectedMainSubRefTabIndex
        {
            get => _selectedMainSubRefTabIndex; 
            set
            {
                if (value == _selectedMainSubRefTabIndex) return;
                    _selectedMainSubRefTabIndex = value;  RaisePropertyChanged();
                if (_selectedMainSubRefTabIndex == 0)
                    SelectedDetailSubRefTabIndex = 0;
                if (_selectedMainSubRefTabIndex == 1)
                    SelectedDetailSubRefTabIndex = 1;
                if (_selectedMainSubRefTabIndex == 2)
                    SelectedDetailSubRefTabIndex = 2;
            }
        }

        private int _selectedDetailTabIndex;
        public int SelectedDetailTabIndex
        {
            get => _selectedDetailTabIndex; 
            set
            {
                if (value == _selectedDetailTabIndex) return;
                    _selectedDetailTabIndex= value;  RaisePropertyChanged();
                if (_selectedDetailTabIndex == 0)
                    SelectedMainTabIndex = 0;
                if (_selectedDetailTabIndex == 1)
                    SelectedMainTabIndex = 1;
                if (_selectedDetailTabIndex == 2)
                    SelectedMainTabIndex = 2;
                if (_selectedDetailTabIndex == 3)
                    SelectedMainTabIndex = 3;
                if (_selectedDetailTabIndex == 4)
                    SelectedMainTabIndex = 4;
                if (_selectedDetailTabIndex == 5)
                    SelectedMainTabIndex = 5;
                if (_selectedDetailTabIndex == 6)
                    SelectedMainTabIndex = 6;
                if (_selectedDetailTabIndex == 7)
                    SelectedMainTabIndex = 7;
            }
        }

        private int _selectedDetailSubTabIndex;
        public  int SelectedDetailSubTabIndex
        {
            get => _selectedDetailSubTabIndex; 
            set
            {
                if (value == _selectedDetailSubTabIndex) return;
                    _selectedDetailSubTabIndex= value;  RaisePropertyChanged();
                if (_selectedDetailSubTabIndex == 0)
                    SelectedMainSubTabIndex = 0;
                if (_selectedDetailSubTabIndex == 1)
                    SelectedMainSubTabIndex = 1;
                if (_selectedDetailSubTabIndex == 2)
                    SelectedMainSubTabIndex = 2;
                if (_selectedDetailSubTabIndex == 3)
                    SelectedMainSubTabIndex = 3;
                
            }
        }

        private int _selectedDetailSubRefTabIndex;
        public int SelectedDetailSubRefTabIndex
        {
            get => _selectedDetailSubRefTabIndex; 
            set
            {
                if (value == _selectedDetailSubRefTabIndex) return;
                    _selectedDetailSubRefTabIndex = value;  RaisePropertyChanged();
                if (_selectedDetailSubRefTabIndex == 0)
                    SelectedMainSubRefTabIndex = 0;
                if (_selectedDetailSubRefTabIndex == 1)
                    SelectedMainSubRefTabIndex = 1;
                if (_selectedDetailSubRefTabIndex == 2)
                    SelectedMainSubRefTabIndex = 2;
            }
        }

        private int _selectedDetailThreeRefTabIndex;
        public int SelectedDetailThreeRefTabIndex
        {
            get => _selectedDetailThreeRefTabIndex; 
            set
            {
                if (value == _selectedDetailThreeRefTabIndex) return;
                _selectedDetailThreeRefTabIndex = value; RaisePropertyChanged();
                if (_selectedDetailThreeRefTabIndex == 0)
                    SelectedMainSubRefTabIndex = 0;
                if (_selectedDetailThreeRefTabIndex == 1)
                    SelectedMainSubRefTabIndex = 1;
                if (_selectedDetailThreeRefTabIndex == 2)
                    SelectedMainSubRefTabIndex = 2;
            }
        }
        #endregion "Public Commands to open Detail TabItems"

 

 //    Part 11    


     
        #region "Public Properties Tbl93Comment"

        private string _searchCommentInfo;
        public string SearchCommentInfo
        {
            get => _searchCommentInfo; 
            set { _searchCommentInfo = value; RaisePropertyChanged(); }
        }

        public ICollectionView CommentsView;
        public Tbl93Comment CurrentTbl93Comment => CommentsView?.CurrentItem as Tbl93Comment;     
   
        private ObservableCollection<Tbl93Comment> _tbl93CommentsList;
        public  ObservableCollection<Tbl93Comment> Tbl93CommentsList
        {
            get => _tbl93CommentsList; 
            set { _tbl93CommentsList = value; RaisePropertyChanged();   }
        }

        private ObservableCollection<Tbl93Comment> _tbl93CommentsAllList;
        public ObservableCollection<Tbl93Comment> Tbl93CommentsAllList
        {
            get => _tbl93CommentsAllList; 
            set { _tbl93CommentsAllList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl03Regnum> _tbl03RegnumsAllList;
        public ObservableCollection<Tbl03Regnum> Tbl03RegnumsAllList
        {
            get => _tbl03RegnumsAllList; 
            set { _tbl03RegnumsAllList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl06Phylum> _tbl06PhylumsAllList;
        public ObservableCollection<Tbl06Phylum> Tbl06PhylumsAllList
        {
            get => _tbl06PhylumsAllList; 
            set { _tbl06PhylumsAllList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl09Division> _tbl09DivisionsAllList;
        public ObservableCollection<Tbl09Division> Tbl09DivisionsAllList
        {
            get => _tbl09DivisionsAllList; 
            set { _tbl09DivisionsAllList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl12Subphylum> _tbl12SubphylumsAllList;
        public ObservableCollection<Tbl12Subphylum> Tbl12SubphylumsAllList
        {
            get => _tbl12SubphylumsAllList; 
            set { _tbl12SubphylumsAllList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl15Subdivision> _tbl15SubdivisionsAllList;
        public ObservableCollection<Tbl15Subdivision> Tbl15SubdivisionsAllList
        {
            get => _tbl15SubdivisionsAllList; 
            set { _tbl15SubdivisionsAllList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl18Superclass> _tbl18SuperclassesAllList;
        public ObservableCollection<Tbl18Superclass> Tbl18SuperclassesAllList
        {
            get => _tbl18SuperclassesAllList; 
            set { _tbl18SuperclassesAllList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl21Class> _tbl21ClassesAllList;
        public ObservableCollection<Tbl21Class> Tbl21ClassesAllList
        {
            get => _tbl21ClassesAllList; 
            set { _tbl21ClassesAllList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl24Subclass> _tbl24SubclassesAllList;
        public ObservableCollection<Tbl24Subclass> Tbl24SubclassesAllList
        {
            get => _tbl24SubclassesAllList; 
            set { _tbl24SubclassesAllList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl27Infraclass> _tbl27InfraclassesAllList;
        public ObservableCollection<Tbl27Infraclass> Tbl27InfraclassesAllList
        {
            get => _tbl27InfraclassesAllList; 
            set { _tbl27InfraclassesAllList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl30Legio> _tbl30LegiosAllList;
        public ObservableCollection<Tbl30Legio> Tbl30LegiosAllList
        {
            get => _tbl30LegiosAllList; 
            set { _tbl30LegiosAllList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl33Ordo> _tbl33OrdosAllList;
        public ObservableCollection<Tbl33Ordo> Tbl33OrdosAllList
        {
            get => _tbl33OrdosAllList; 
            set { _tbl33OrdosAllList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl36Subordo> _tbl36SubordosAllList;
        public ObservableCollection<Tbl36Subordo> Tbl36SubordosAllList
        {
            get => _tbl36SubordosAllList; 
            set { _tbl36SubordosAllList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl39Infraordo> _tbl39InfraordosAllList;
        public ObservableCollection<Tbl39Infraordo> Tbl39InfraordosAllList
        {
            get => _tbl39InfraordosAllList; 
            set { _tbl39InfraordosAllList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl42Superfamily> _tbl42SuperfamiliesAllList;
        public ObservableCollection<Tbl42Superfamily> Tbl42SuperfamiliesAllList
        {
            get => _tbl42SuperfamiliesAllList; 
            set { _tbl42SuperfamiliesAllList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl45Family> _tbl45FamiliesAllList;
        public ObservableCollection<Tbl45Family> Tbl45FamiliesAllList
        {
            get => _tbl45FamiliesAllList; 
            set { _tbl45FamiliesAllList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl48Subfamily> _tbl48SubfamiliesAllList;
        public ObservableCollection<Tbl48Subfamily> Tbl48SubfamiliesAllList
        {
            get => _tbl48SubfamiliesAllList; 
            set { _tbl48SubfamiliesAllList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl51Infrafamily> _tbl51InfrafamiliesAllList;
        public ObservableCollection<Tbl51Infrafamily> Tbl51InfrafamiliesAllList
        {
            get => _tbl51InfrafamiliesAllList; 
            set { _tbl51InfrafamiliesAllList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl54Supertribus> _tbl54SupertribussesAllList;
        public ObservableCollection<Tbl54Supertribus> Tbl54SupertribussesAllList
        {
            get => _tbl54SupertribussesAllList; 
            set { _tbl54SupertribussesAllList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl57Tribus> _tbl57TribussesAllList;
        public ObservableCollection<Tbl57Tribus> Tbl57TribussesAllList
        {
            get => _tbl57TribussesAllList; 
            set { _tbl57TribussesAllList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl60Subtribus> _tbl60SubtribussesAllList;
        public ObservableCollection<Tbl60Subtribus> Tbl60SubtribussesAllList
        {
            get => _tbl60SubtribussesAllList; 
            set { _tbl60SubtribussesAllList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl63Infratribus> _tbl63InfratribussesAllList;
        public ObservableCollection<Tbl63Infratribus> Tbl63InfratribussesAllList
        {
            get => _tbl63InfratribussesAllList; 
            set { _tbl63InfratribussesAllList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl66Genus> _tbl66GenussesAllList;
        public ObservableCollection<Tbl66Genus> Tbl66GenussesAllList
        {
            get => _tbl66GenussesAllList; 
            set { _tbl66GenussesAllList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl69FiSpecies> _tbl69FiSpeciessesAllList;
        public ObservableCollection<Tbl69FiSpecies> Tbl69FiSpeciessesAllList
        {
            get => _tbl69FiSpeciessesAllList; 
            set { _tbl69FiSpeciessesAllList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl72PlSpecies> _tbl72PlSpeciessesAllList;
        public ObservableCollection<Tbl72PlSpecies> Tbl72PlSpeciessesAllList
        {
            get => _tbl72PlSpeciessesAllList; 
            set { _tbl72PlSpeciessesAllList = value; RaisePropertyChanged(); }
        }

        #endregion "Public Properties"
   
 

 //    Part 12    

 

   }
}   
