using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using DAL.Helper;
using DAL.Models;
using DAL.Repositories.Tbl03Regnums;
using DAL.Repositories.Tbl06Phylums;
using DAL.Repositories.Tbl09Divisions;
using DAL.Repositories.Tbl90RefAuthors;
using DAL.Repositories.Tbl90References;
using DAL.Repositories.Tbl90RefExperts;
using DAL.Repositories.Tbl90RefSources;
using DAL.Repositories.Tbl93Comments;
using DAL.Repositories.TblCounters;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

//    Tbl90ReferencesViewModel Skriptdatum:  15.03.2012  10:32    

namespace WPFUI.Views.Database
{   


    
    public class Tbl90ReferencesViewModel : Tbl90RefExpertsViewModel                     
    {     
        
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
        private new bool IsInDesignMode { get; set; }

        #endregion "Constructor"           
 

 //    Part 1    

       
        #region "Public Commands Basic Tbl90Reference"

        private RelayCommand _getReferenceByNameCommand;
        public new ICommand GetReferenceByNameCommand
        {
            get { return _getReferenceByNameCommand ?? (_getReferenceByNameCommand = new RelayCommand(delegate { GetReferenceByNameOrId(null); })); }   
        }

        private void GetReferenceByNameOrId(object o)       
        {   
Tbl90ReferencesList =  new ObservableCollection<Tbl90Reference>
                                                       (from x in Tbl90ReferencesRepository.Tbl90References
                                                        where x.ReferenceName.StartsWith(SearchReferenceName)
                                                        orderby x.ReferenceName
                                                        select x);

            Tbl90ReferencesAllList =  new ObservableCollection<Tbl90Reference>
                                                       (from y in Tbl90ReferencesRepository.Tbl90References
                                                        orderby y.ReferenceName
                                                        select y);

            Tbl90RefExpertsAllList =  new ObservableCollection<Tbl90RefExpert>
                                                       (from z in Tbl90RefExpertsRepository.Tbl90RefExperts
                                                        orderby z.RefExpertName
                                                        select z);

              
  Tbl66GenussesAllList =  new ObservableCollection<Tbl66Genus>
                                                       (from z in Tbl66GenussesRepository.Tbl66Genusses
                                                        orderby z.GenusName
                                                        select z);
       
         
            Tbl90AuthorsAllList =  new ObservableCollection<Tbl90RefAuthor>
                                                       (from auth in Tbl90RefAuthorsRepository.Tbl90RefAuthors
                                                        orderby auth.RefAuthorName, auth.BookName, auth.Page1
                                                        select auth);
 
           Tbl90SourcesAllList =  new ObservableCollection<Tbl90RefSource>
                                                       (from sour in Tbl90RefSourcesRepository.Tbl90RefSources
                                                        orderby sour.RefSourceName
                                                        select sour);

            Tbl90ExpertsAllList =  new ObservableCollection<Tbl90RefExpert>
                                                       (from exp in Tbl90RefExpertsRepository.Tbl90RefExperts
                                                        orderby exp.RefExpertName
                                                        select exp);

            //All Lists to null
            Tbl93CommentsList = null;
            Tbl90RefExpertsList = null;
            Tbl90RefAuthorsList = null;
            Tbl90RefSourcesList = null;

  
Tbl90RefExpertsList = null;                  
  View = CollectionViewSource.GetDefaultView(Tbl90ReferencesList);
            if (View != null)
                View.CurrentChanged += tbl90ReferenceView_CurrentChanged;                   
            RaisePropertyChanged();
        }
        //------------------------------------------------------------------------------
        private RelayCommand _addReferenceCommand;
        public new ICommand AddReferenceCommand
        {
            get { return _addReferenceCommand ?? (_addReferenceCommand = new RelayCommand(AddReference)); }
        }

        private void AddReference(object o)
        {
            if (Tbl90ReferencesList == null)
                Tbl90ReferencesList = new ObservableCollection<Tbl90Reference>();
            Tbl90ReferencesList.Add(new Tbl90Reference{ ReferenceName= CultRes.StringsRes.DatasetNew });
            View = CollectionViewSource.GetDefaultView(Tbl90ReferencesList);
            if (View != null)
                View.CurrentChanged += tbl90ReferenceView_CurrentChanged;
            RaisePropertyChanged();
        }
        //---------------------------------------------------------------------------------------
  
       
        private RelayCommand _deleteReferenceCommand;
        public new ICommand DeleteReferenceCommand
        {
            get { return _deleteReferenceCommand ?? (_deleteReferenceCommand = new RelayCommand(delegate { DeleteReference(null); })); }
        }

        private void DeleteReference(object o)
        {
            try
            {
                var reference= Tbl90ReferencesRepository.Tbl90References.FirstOrDefault(x => x.ReferenceID== CurrentTbl90Reference.ReferenceID);
                if (reference!= null)
                {
                    if (MessageBox.Show(CultRes.StringsRes.DeleteQuestion
                                        + " " +  CurrentTbl90Reference.ReferenceName, CultRes.StringsRes.DeleteQuestion1, MessageBoxButton.YesNo) !=
                        MessageBoxResult.Yes) return;
                    Tbl90ReferencesRepository.Delete(reference);
                    Tbl90ReferencesRepository.Save();
                    MessageBox.Show(CurrentTbl90Reference.ReferenceName + " " + CultRes.StringsRes.DeleteSuccess);
                    GetReferenceByNameOrId(o); //Refresh
                }
                else
                {
                    MessageBox.Show(CultRes.StringsRes.DeleteCan + " " + CurrentTbl90Reference.ReferenceName+ " " + CultRes.StringsRes.DeleteCan1);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //-------------------------------------------------------------------------------------------------
        private RelayCommand _saveReferenceCommand;
        public new ICommand SaveReferenceCommand
        {
            get { return _saveReferenceCommand ?? (_saveReferenceCommand = new RelayCommand(delegate { SaveReference(null); })); }
        }

        private void SaveReference(object o)
        {
            try
            {
                var reference= Tbl90ReferencesRepository.Tbl90References.FirstOrDefault(x => x.ReferenceID== CurrentTbl90Reference.ReferenceID);
                if (CurrentTbl90Reference == null)
                {
                    MessageBox.Show(CultRes.StringsRes.DatasetNotExist);
                }
                else
                {
                    if (CurrentTbl90Reference.ReferenceID!= 0)
                    {
                        if (reference!= null) //update
                        {
                            reference.Updater = Environment.UserName;
                            reference.UpdaterDate = DateTime.Now;
                        }
                    }
                    else
                    {
                        Tbl90ReferencesRepository.Add(new Tbl90Reference
                        {
                            RefExpertID= CurrentTbl90Reference.RefExpertID,              
                            ReferenceName= CurrentTbl90Reference.ReferenceName,              
                            CountID = RandomHelper.Randomnumber(),
                            Valid = CurrentTbl90Reference.Valid,
                            ValidYear = CurrentTbl90Reference.ValidYear,
                            Synonym = CurrentTbl90Reference.Synonym,
                            Author = CurrentTbl90Reference.Author,
                            AuthorYear = CurrentTbl90Reference.AuthorYear,
                            Info = CurrentTbl90Reference.Info,
                            EngName = CurrentTbl90Reference.EngName,
                            GerName = CurrentTbl90Reference.GerName,
                            FraName = CurrentTbl90Reference.FraName,
                            PorName = CurrentTbl90Reference.PorName,
                            Writer = Environment.UserName,
                            WriterDate = DateTime.Now,
                            Updater = Environment.UserName,
                            UpdaterDate = DateTime.Now,
                            Memo = CurrentTbl90Reference.Memo
                        });
                    }
                    {
                        Tbl90ReferencesRepository.Save();
                        MessageBox.Show(CurrentTbl90Reference.ReferenceName+ " " + CultRes.StringsRes.SaveSuccess);
                        GetReferenceByNameOrId(o);  //Refresh
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion "Public Commands"
  
    

 //    Part 2    


        
        #region "Public Commands Connect <== Tbl90RefExpert"                 

        private RelayCommand _getRefExpertByNameCommand;
        public new ICommand GetRefExpertByNameCommand
        {
            get { return _getRefExpertByNameCommand ?? (_getRefExpertByNameCommand = new RelayCommand(delegate { GetRefExpertByNameOrId(null); })); }
        }

        private void GetRefExpertByNameOrId(object o)
        {
            Tbl90RefExpertsList =
                new ObservableCollection<Tbl90RefExpert>((from refexpert in Tbl90RefExpertsRepository.Tbl90RefExperts
                                                       where refexpert.RefExpertName.StartsWith(SearchRefExpertName)
                                                       orderby refexpert.RefExpertName
                                                       select refexpert));

            View = CollectionViewSource.GetDefaultView(Tbl90RefExpertsList);
            if (View != null)
                View.CurrentChanged += tbl69FiSpeciesView_CurrentChanged;                   
            RaisePropertyChanged();
        }
        //------------------------------------------------------------------------------
        private RelayCommand _addRefExpertCommand;
        public new ICommand AddRefExpertCommand
        {
            get { return _addRefExpertCommand ?? (_addRefExpertCommand = new RelayCommand(AddRefExpert)); }
        }

        private void AddRefExpert()
        {
            if (Tbl90RefExpertsList == null)
                Tbl90RefExpertsList = new ObservableCollection<Tbl90RefExpert>();
            Tbl90RefExpertsList.Add(new Tbl90RefExpert{ RefExpertName= "New " });                   
            View = CollectionViewSource.GetDefaultView(Tbl90RefExpertsList);
            if (View != null)
                View.CurrentChanged += tbl69FiSpeciesView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl90RefExpert");
        }
        //----------------------------------------------------------------------------------------------------------
        private RelayCommand _deleteRefExpertCommand;
        public ICommand RefExpertPhylumCommand
        {
            get { return _deleteRefExpertCommand ?? (_deleteRefExpertCommand = new RelayCommand(DeleteRefExpert)); }
        }

        private void DeleteRefExpert()
        {
            try
            {
                var refexpert= Tbl90RefExpertsRepository.Tbl90RefExperts.FirstOrDefault(x => x.RefExpertID== CurrentTbl90RefExpert.RefExpertID);
                if (refexpert!= null)
                {
                    if (MessageBox.Show("Are you really shure to delete this "
                                        + CurrentTbl90RefExpert.RefExpertName, "Question", MessageBoxButton.YesNo) !=
                        MessageBoxResult.Yes) return;
                    Tbl90RefExpertsRepository.Delete(refexpert);
                    Tbl90RefExpertsRepository.Save();
                    MessageBox.Show(CurrentTbl90RefExpert.RefExpertName+ " was deleted successfully");
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
                    MessageBox.Show("Only " + CurrentTbl90RefExpert.RefExpertName+ " can be deleted");
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
                var refexpert= Tbl90RefExpertsRepository.Tbl90RefExperts.FirstOrDefault(x => x.RefExpertID== CurrentTbl90RefExpert.RefExpertID);
                if (CurrentTbl90RefExpert == null)
                {
                    MessageBox.Show("refexpert was not found");
                }
                else
                {
                    if (CurrentTbl90RefExpert.RefExpertID!= 0)
                    {
                        if (refexpert!= null) //update
                        {
                            refexpert.Updater = Environment.UserName;
                            refexpert.UpdaterDate = DateTime.Now;
                        }
                    }
                    else
                    {
                        Tbl90RefExpertsRepository.Add(new Tbl90RefExpert()
                        {
                            RefExpertName= CurrentTbl90RefExpert.RefExpertName,              
                            CountID = TblCountersRepository.Counter(),
                            Valid = CurrentTbl90RefExpert.Valid,
                            ValidYear = CurrentTbl90RefExpert.ValidYear,
                            Synonym = CurrentTbl90RefExpert.Synonym,
                            Author = CurrentTbl90RefExpert.Author,
                            AuthorYear = CurrentTbl90RefExpert.AuthorYear,
                            Info = CurrentTbl90RefExpert.Info,
                            EngName = CurrentTbl90RefExpert.EngName,
                            GerName = CurrentTbl90RefExpert.GerName,
                            FraName = CurrentTbl90RefExpert.FraName,
                            PorName = CurrentTbl90RefExpert.PorName,
                            Writer = Environment.UserName,
                            WriterDate = DateTime.Now,
                            Updater = Environment.UserName,
                            UpdaterDate = DateTime.Now,
                            Memo = CurrentTbl90RefExpert.Memo
                        });
                        }
                        {
                            Tbl90RefExpertsRepository.Save();
                            MessageBox.Show(CurrentTbl90RefExpert.RefExpertName+  " was successfully saved ");
                            GetRefExpertByName();  //Refresh
                         }   
                     }               
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion "Public Commands"
    
      

 //    Part 3    

    

 //    Part 4    

    

 //    Part 5    

    

 //    Part 6    

 

 //    Part 7    

 

 //    Part 8    

   
    
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
                            ReferenceID= CurrentTbl90RefAuthor.ReferenceID,
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
                            ReferenceID= CurrentTbl90RefSource.ReferenceID,
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

        private new void SaveRefExpert()
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
                            ReferenceID= CurrentTbl90RefExpert.ReferenceID,
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
                            ReferenceID= CurrentTbl93Comment.ReferenceID,                
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
  
    

 //    Part 9    



     
        #region "Public Commands Connected Tables by DoubleClick"                                         

        private RelayCommand _getConnectedTablesCommand;
        public new ICommand GetConnectedTablesCommand
        {
            get { return _getConnectedTablesCommand ?? (_getConnectedTablesCommand = new RelayCommand(GetConnectedTablesById)); }
        }

        public new void GetConnectedTablesById()
        {
            //Clear Search-TextBox                                  
            SearchRefExpertName = null;                       
            SearchNULLName = null;
            SearchCommentInfo = null;
            SearchRefExpertName = null;
            SearchRefSourceName = null;
            SearchRefAuthorName = null;

            Tbl90RefExpertsList =
                new ObservableCollection<Tbl90RefExpert>((from refexpert in Tbl90RefExpertsRepository.Tbl90RefExperts
                                                       where refexpert.RefExpertID== CurrentTbl90Reference.RefExpertID
                                                       orderby refexpert.RefExpertName
                                                       select refexpert));

            View = CollectionViewSource.GetDefaultView(Tbl90RefExpertsList);
            if (View != null)
                View.CurrentChanged += tbl69FiSpeciesView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl90RefExperts");
            //-----------------------------------------------------------------------------------
            NULLList =
                new ObservableCollection<NULL>((from NULL in NULLRepository.NULL
                                                       where NULL.ReferenceID== CurrentTbl90Reference.ReferenceID
                                                       orderby NULL.Tbl90References.ReferenceName
                                                       select NULL));


            View = CollectionViewSource.GetDefaultView(NULLList);
            if (View != null)
                View.CurrentChanged += NULLView_CurrentChanged;
            RaisePropertyChanged("CurrentNULL");
            //-----------------------------------------------------------------------------------
            Tbl90RefAuthorsList =
                new ObservableCollection<Tbl90Reference>((from refAu in Tbl90ReferencesRepository.Tbl90References
                                                          where refAu.ReferenceID== CurrentTbl90Reference.ReferenceID
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
                                                          where refSo.ReferenceID== CurrentTbl90Reference.ReferenceID
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
                                                          where refEx.ReferenceID== CurrentTbl90Reference.ReferenceID
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
                                                          where comm.ReferenceID== CurrentTbl90Reference.ReferenceID
                                                        orderby comm.Info
                                                        select comm));

            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            if (CommentsView != null)
                CommentsView.CurrentChanged += tbl93CommentView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl93Comment");
            //--------------------------------------------------------------

        }

        #endregion "Public Commands Connected Tables by DoubleClick"
   
 

 //    Part 10    

 

 //    Part 11    



     
        #region "Public Properties Tbl90Reference"

        public new ICollectionView View;
        public new Tbl90Reference CurrentTbl90Reference
        {
            get
            {
                if (View != null)
                    return View.CurrentItem as Tbl90Reference;
                return null;
            }
        }
        //--------------------------------------------
        private string _searchReferenceName;
        public new string SearchReferenceName
        {
            get { return _searchReferenceName; }
            set
            {
                if (value == _searchReferenceName) return;
                _searchReferenceName = value;
                RaisePropertyChanged("SearchReferenceName");
            }
        }

        private ObservableCollection<Tbl90Reference> _tbl90ReferencesList;
        public new ObservableCollection<Tbl90Reference> Tbl90ReferencesList
        {
            get { return _tbl90ReferencesList; }
            set
            {
                if (_tbl90ReferencesList == value) return;
                _tbl90ReferencesList = value;
                RaisePropertyChanged("Tbl90ReferencesList");

                //Clear Search-TextBox
                SearchRefExpertName = null;                                
                SearchNULL = null;
                SearchCommentInfo = null;
                SearchRefExpertName = null;
                SearchRefSourceName = null;
                SearchRefAuthorName = null;
            }
        }

        private ObservableCollection<Tbl90Reference> _tbl90ReferencesAllList;
        public ObservableCollection<Tbl90Reference> Tbl90ReferencesAllList
        {
            get { return _tbl90ReferencesAllList; }
            set
            {
                if (_tbl90ReferencesAllList == value) return;
                _tbl90ReferencesAllList = value;
                RaisePropertyChanged("Tbl90ReferencesAllList");
            }
        }

        #endregion "Public Properties"   

       
        #region "Public Properties Tbl90RefExpert"

        public  ICollectionView View;
        public  Tbl90RefExpert CurrentTbl90RefExpert
        {
            get
            {
                if (View != null)
                    return View.CurrentItem as Tbl90RefExpert;
                return null;
            }
        }
        //--------------------------------------------                                               

        private string _searchRefExpertName;
        public  string SearchRefExpertName
        {
            get { return _searchRefExpertName; }
            set
            {
                if (value == _searchRefExpertName) return;
                _searchRefExpertName = value;
                RaisePropertyChanged("SearchRefExpertName");
            }
        }

        private ObservableCollection<Tbl90RefExpert> _tbl90RefExpertsList;
        public  ObservableCollection<Tbl90RefExpert> Tbl90RefExpertsList
        {
            get { return _tbl90RefExpertsList; }
            set
            {
                if (_tbl90RefExpertsList == value) return;
                _tbl90RefExpertsList = value;
                RaisePropertyChanged("Tbl90RefExpertsList");
            }
        }

        #endregion "Public Properties"
   
  
       
        #region "Public Properties Tbl90RefSource"

        public  ICollectionView View;
        public  Tbl90RefSource CurrentTbl90RefSource
        {
            get
            {
                if (View != null)
                    return View.CurrentItem as Tbl90RefSource;
                return null;
            }
        }
        //--------------------------------------------                                               

        private string _searchRefSourceName;
        public  string SearchRefSourceName
        {
            get { return _searchRefSourceName; }
            set
            {
                if (value == _searchRefSourceName) return;
                _searchRefSourceName = value;
                RaisePropertyChanged("SearchRefSourceName");
            }
        }

        private ObservableCollection<Tbl90RefSource> _tbl90RefSourcesList;
        public  ObservableCollection<Tbl90RefSource> Tbl90RefSourcesList
        {
            get { return _tbl90RefSourcesList; }
            set
            {
                if (_tbl90RefSourcesList == value) return;
                _tbl90RefSourcesList = value;
                RaisePropertyChanged("Tbl90RefSourcesList");
            }
        }

   
   

 //    Part 11    

      }
}   
