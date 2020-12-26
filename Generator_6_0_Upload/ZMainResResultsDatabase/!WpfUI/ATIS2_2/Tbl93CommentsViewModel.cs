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

    
         //    Tbl93CommentsViewModel Skriptdatum:  14.11.2017  10:32    

namespace WPFUI.Views.Database
{     
    
    public partial class Tbl93CommentsViewModel : ViewModelBase                     
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
        private  new bool IsInDesignMode { get; set; }

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
        //------------------------------------------------------------------------------------                
           
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
        //------------------------------------------------------------------------------------                
           
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
        //---------------------------------------------------------------------------------------                  
           
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
                if (comment != null)
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
                
                    WpfMessageBox.Show(CultRes.StringsRes.Information, CultRes.StringsRes.DeleteCan + " " + CurrentTbl93Comment.Info + " " + CultRes.StringsRes.DeleteCan1,
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
                    if (CurrentTbl93Comment.CommentID != 0)   
                    {
                        if (comment != null) //update
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
             
                        // Info and FK-Keys may be not 0
                        var dataset = new ObservableCollection<Tbl93Comment>
                        (from a in _tbl93CommentsRepository.GetAll()
                         where
                         a.Info.Trim() == CurrentTbl93Comment.Info.Trim() &&  
             
                            a.RegnumID == CurrentTbl93Comment.RegnumID &&
                            a.PhylumID == CurrentTbl93Comment.PhylumID &&
                            a.DivisionID == CurrentTbl93Comment.DivisionID &&
                            a.SubphylumID == CurrentTbl93Comment.SubphylumID &&
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
                            Tbl93CommentsList = new ObservableCollection<Tbl93Comment>
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

 




   }
}   
