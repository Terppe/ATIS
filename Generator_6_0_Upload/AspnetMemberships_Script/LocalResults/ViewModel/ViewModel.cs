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

//    AspnetMembershipViewModel Skriptdatum:  24.03.2012  10:32    

namespace Atis.WpfUi.ViewModel
{   


    
    public class AspnetMembershipsViewModel : AspnetApplicationsViewModel                     
    {     
        
          #endregion "Private Data Members"            
    
        #region "Constructor"

        public AspnetMembershipsViewModel()
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
       
        #region "Public Commands Basic AspnetMembership"

        private RelayCommand _getMembershipByNameCommand;
        public new ICommand GetMembershipByNameCommand
        {
            get { return _getMembershipByNameCommand ?? (_getMembershipByNameCommand = new RelayCommand(GetMembershipByName)); }
        }

        private void GetMembershipByName()
        {   
AspnetMembershipsList =
                 new ObservableCollection<AspnetMembership>((from x in AspnetMembershipsRepository.AspnetMemberships
                                                        where x.UserName.StartsWith(SearchUserName)
                                                        orderby x.UserName
                                                        select x));

            AspnetMembershipsAllList =
                 new ObservableCollection<AspnetMembership>((from y in AspnetMembershipsRepository.AspnetMemberships
                                                        orderby y.UserName
                                                        select y));

            AspnetApplicationsAllList =
                 new ObservableCollection<AspnetApplication>((from z in AspnetApplicationsRepository.AspnetApplications
                                                        orderby z.ApplicationName
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

  
AspnetApplicationsList = null;                  
  View = CollectionViewSource.GetDefaultView(AspnetMembershipsList);
            if (View != null)
                View.CurrentChanged += aspnetMembershipView_CurrentChanged;                   
            RaisePropertyChanged("CurrentAspnetMembership");
        }
        //------------------------------------------------------------------------------
        private RelayCommand _addMembershipCommand;
        public new ICommand AddMembershipCommand
        {
            get { return _addMembershipCommand ?? (_addMembershipCommand = new RelayCommand(AddMembership)); }
        }

        private void AddMembership()
        {
            if (AspnetMembershipsList == null)
                AspnetMembershipsList = new ObservableCollection<AspnetMembership>();
            AspnetMembershipsList.Add(new AspnetMembership{ UserName= "New " });
            View = CollectionViewSource.GetDefaultView(AspnetMembershipsList);
            if (View != null)
                View.CurrentChanged += aspnetMembershipView_CurrentChanged;
            RaisePropertyChanged("CurrentAspnetMembership");
        }
        //---------------------------------------------------------------------------------------
  
       
        private RelayCommand _deleteMembershipCommand;
        public new ICommand DeleteMembershipCommand
        {
            get { return _deleteMembershipCommand ?? (_deleteMembershipCommand = new RelayCommand(DeleteMembership)); }
        }

        private void DeleteMembership()
        {
            try
            {
                var membership= AspnetMembershipsRepository.AspnetMemberships.FirstOrDefault(x => x.UserId== CurrentAspnetMembership.UserId);
                if (membership!= null)
                {
                    if (MessageBox.Show("Are you really shure to delete this "
                                        + CurrentAspnetMembership.UserName, "Question", MessageBoxButton.YesNo) !=
                        MessageBoxResult.Yes) return;
                    AspnetMembershipsRepository.Delete(membership);
                    AspnetMembershipsRepository.Save();
                    MessageBox.Show(CurrentAspnetMembership.UserName + " was deleted successfully");
                    GetMembershipByName(); //Refresh
                }
                else
                {
                    MessageBox.Show("Only " + CurrentAspnetMembership.UserName+ " can be deleted");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //-------------------------------------------------------------------------------------------------
        private RelayCommand _saveMembershipCommand;
        public new ICommand SaveMembershipCommand
        {
            get { return _saveMembershipCommand ?? (_saveMembershipCommand = new RelayCommand(SaveMembership)); }
        }

        private void SaveMembership()
        {
            try
            {
                var membership= AspnetMembershipsRepository.AspnetMemberships.FirstOrDefault(x => x.UserId== CurrentAspnetMembership.UserId);
                if (CurrentAspnetMembership == null)
                {
                    MessageBox.Show("membership was not found");
                }
                else
                {
                    if (CurrentAspnetMembership.UserId!= 0)
                    {
                        if (membership!= null) //update
                        {
                            membership.Updater = Environment.UserName;
                            membership.UpdaterDate = DateTime.Now;
                        }
                    }
                    else
                    {
                        AspnetMembershipsRepository.Add(new AspnetMembership
                        {
                            ApplicationId= CurrentAspnetMembership.ApplicationId,              
                            UserName= CurrentAspnetMembership.UserName,              
                            CountID = TblCountersRepository.Counter(),
                            Valid = CurrentAspnetMembership.Valid,
                            ValidYear = CurrentAspnetMembership.ValidYear,
                            Synonym = CurrentAspnetMembership.Synonym,
                            Author = CurrentAspnetMembership.Author,
                            AuthorYear = CurrentAspnetMembership.AuthorYear,
                            Info = CurrentAspnetMembership.Info,
                            EngName = CurrentAspnetMembership.EngName,
                            GerName = CurrentAspnetMembership.GerName,
                            FraName = CurrentAspnetMembership.FraName,
                            PorName = CurrentAspnetMembership.PorName,
                            Writer = Environment.UserName,
                            WriterDate = DateTime.Now,
                            Updater = Environment.UserName,
                            UpdaterDate = DateTime.Now,
                            Memo = CurrentAspnetMembership.Memo
                        });
                    }
                    {
                        AspnetMembershipsRepository.Save();
                        MessageBox.Show(CurrentAspnetMembership.UserName+  " was successfully saved ");
                        GetMembershipByName();  //Refresh
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion "Public Commands"
  

        
        #region "Public Commands Connect <== AspnetApplication"                 

        private RelayCommand _getApplicationByNameCommand;
        public new ICommand GetApplicationByNameCommand
        {
            get { return _getApplicationByNameCommand ?? (_getApplicationByNameCommand = new RelayCommand(GetApplicationByName)); }
        }

        private void GetApplicationByName()
        {
            AspnetApplicationsList =
                new ObservableCollection<AspnetApplication>((from application in AspnetApplicationsRepository.AspnetApplications
                                                       where application.ApplicationName.StartsWith(SearchApplicationName)
                                                       orderby application.ApplicationName
                                                       select application));

            View = CollectionViewSource.GetDefaultView(AspnetApplicationsList);
            if (View != null)
                View.CurrentChanged += aspnetApplicationView_CurrentChanged;                   
            RaisePropertyChanged("CurrentAspnetApplication");
        }
        //------------------------------------------------------------------------------
        private RelayCommand _addApplicationCommand;
        public new ICommand AddApplicationCommand
        {
            get { return _addApplicationCommand ?? (_addApplicationCommand = new RelayCommand(AddApplication)); }
        }

        private void AddApplication()
        {
            if (AspnetApplicationsList == null)
                AspnetApplicationsList = new ObservableCollection<AspnetApplication>();
            AspnetApplicationsList.Add(new AspnetApplication{ ApplicationName= "New " });                   
            View = CollectionViewSource.GetDefaultView(AspnetApplicationsList);
            if (View != null)
                View.CurrentChanged += aspnetApplicationView_CurrentChanged;
            RaisePropertyChanged("CurrentAspnetApplication");
        }
        //----------------------------------------------------------------------------------------------------------
        private RelayCommand _deleteApplicationCommand;
        public ICommand ApplicationPhylumCommand
        {
            get { return _deleteApplicationCommand ?? (_deleteApplicationCommand = new RelayCommand(DeleteApplication)); }
        }

        private void DeleteApplication()
        {
            try
            {
                var application= AspnetApplicationsRepository.AspnetApplications.FirstOrDefault(x => x.ApplicationId== CurrentAspnetApplication.ApplicationId);
                if (application!= null)
                {
                    if (MessageBox.Show("Are you really shure to delete this "
                                        + CurrentAspnetApplication.ApplicationName, "Question", MessageBoxButton.YesNo) !=
                        MessageBoxResult.Yes) return;
                    AspnetApplicationsRepository.Delete(application);
                    AspnetApplicationsRepository.Save();
                    MessageBox.Show(CurrentAspnetApplication.ApplicationName+ " was deleted successfully");
                    if (SearchApplicationName == null)
                    {
                        GetConnectedTablesById(); //refresh doubleClick                    
                    }
                    else
                    {
                        GetApplicationByName(); //search
                    }
                }
                else
                {
                    MessageBox.Show("Only " + CurrentAspnetApplication.ApplicationName+ " can be deleted");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //-------------------------------------------------------------------------------------------------
        private RelayCommand _saveApplicationCommand;   
        public new ICommand SaveApplicationCommand
        {
            get { return _saveApplicationCommand ?? (_saveApplicationCommand = new RelayCommand(SaveApplication)); }
        }

        private void SaveApplication()
        {
            try
            {
                var application= AspnetApplicationsRepository.AspnetApplications.FirstOrDefault(x => x.ApplicationId== CurrentAspnetApplication.ApplicationId);
                if (CurrentAspnetApplication == null)
                {
                    MessageBox.Show("application was not found");
                }
                else
                {
                    if (CurrentAspnetApplication.ApplicationId!= 0)
                    {
                        if (application!= null) //update
                        {
                            application.Updater = Environment.UserName;
                            application.UpdaterDate = DateTime.Now;
                        }
                    }
                    else
                    {
                        AspnetApplicationsRepository.Add(new AspnetApplication()
                        {
                            ApplicationName= CurrentAspnetApplication.ApplicationName,              
                            CountID = TblCountersRepository.Counter(),
                            Valid = CurrentAspnetApplication.Valid,
                            ValidYear = CurrentAspnetApplication.ValidYear,
                            Synonym = CurrentAspnetApplication.Synonym,
                            Author = CurrentAspnetApplication.Author,
                            AuthorYear = CurrentAspnetApplication.AuthorYear,
                            Info = CurrentAspnetApplication.Info,
                            EngName = CurrentAspnetApplication.EngName,
                            GerName = CurrentAspnetApplication.GerName,
                            FraName = CurrentAspnetApplication.FraName,
                            PorName = CurrentAspnetApplication.PorName,
                            Writer = Environment.UserName,
                            WriterDate = DateTime.Now,
                            Updater = Environment.UserName,
                            UpdaterDate = DateTime.Now,
                            Memo = CurrentAspnetApplication.Memo
                        });
                    }
                    {
                        AspnetApplicationsRepository.Save();
                        MessageBox.Show(CurrentAspnetApplication.ApplicationName+  " was successfully saved ");
                        GetApplicationByName();  //Refresh
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
                            UserId= CurrentTbl90RefAuthor.UserId,
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
                            UserId= CurrentTbl90RefSource.UserId,
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
                            UserId= CurrentTbl90RefExpert.UserId,
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
                            UserId= CurrentTbl93Comment.UserId,                
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
            SearchApplicationName = null;                       
            SearchNULLName = null;
            SearchCommentInfo = null;
            SearchRefExpertName = null;
            SearchRefSourceName = null;
            SearchRefAuthorName = null;

            AspnetApplicationsList =
                new ObservableCollection<AspnetApplication>((from application in AspnetApplicationsRepository.AspnetApplications
                                                       where application.ApplicationId== CurrentAspnetMembership.ApplicationId
                                                       orderby application.ApplicationName
                                                       select application));

            View = CollectionViewSource.GetDefaultView(AspnetApplicationsList);
            if (View != null)
                View.CurrentChanged += aspnetApplicationView_CurrentChanged;
            RaisePropertyChanged("CurrentAspnetApplications");
            //-----------------------------------------------------------------------------------
            NULLList =
                new ObservableCollection<NULL>((from NULL in NULLRepository.NULL
                                                       where NULL.UserId== CurrentAspnetMembership.UserId
                                                       orderby NULL.AspnetMemberships.UserName
                                                       select NULL));


            View = CollectionViewSource.GetDefaultView(NULLList);
            if (View != null)
                View.CurrentChanged += NULLView_CurrentChanged;
            RaisePropertyChanged("CurrentNULL");
            //-----------------------------------------------------------------------------------
            Tbl90RefAuthorsList =
                new ObservableCollection<Tbl90Reference>((from refAu in Tbl90ReferencesRepository.Tbl90References
                                                          where refAu.UserId== CurrentAspnetMembership.UserId
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
                                                          where refSo.UserId== CurrentAspnetMembership.UserId
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
                                                          where refEx.UserId== CurrentAspnetMembership.UserId
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
                                                          where comm.UserId== CurrentAspnetMembership.UserId
                                                        orderby comm.Info
                                                        select comm));

            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            if (CommentsView != null)
                CommentsView.CurrentChanged += tbl93CommentView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl93Comment");
            //--------------------------------------------------------------

        }

        #endregion "Public Commands Connected Tables by DoubleClick"
   

     
        #region "Public Properties AspnetMembership"

        public new ICollectionView View;
        public new AspnetMembership CurrentAspnetMembership
        {
            get
            {
                if (View != null)
                    return View.CurrentItem as AspnetMembership;
                return null;
            }
        }
        //--------------------------------------------
        private string _searchUserName;
        public new string SearchUserName
        {
            get { return _searchUserName; }
            set
            {
                if (value == _searchUserName) return;
                _searchUserName = value;
                RaisePropertyChanged("SearchUserName");
            }
        }

        private ObservableCollection<AspnetMembership> _aspnetMembershipsList;
        public new ObservableCollection<AspnetMembership> AspnetMembershipsList
        {
            get { return _aspnetMembershipsList; }
            set
            {
                if (_aspnetMembershipsList == value) return;
                _aspnetMembershipsList = value;
                RaisePropertyChanged("AspnetMembershipsList");

                //Clear Search-TextBox
                SearchApplicationName = null;                                
                SearchNULL = null;
                SearchCommentInfo = null;
                SearchRefExpertName = null;
                SearchRefSourceName = null;
                SearchRefAuthorName = null;
            }
        }

        private ObservableCollection<AspnetMembership> _aspnetMembershipsAllList;
        public ObservableCollection<AspnetMembership> AspnetMembershipsAllList
        {
            get { return _aspnetMembershipsAllList; }
            set
            {
                if (_aspnetMembershipsAllList == value) return;
                _aspnetMembershipsAllList = value;
                RaisePropertyChanged("AspnetMembershipsAllList");
            }
        }

        #endregion "Public Properties"
   

       
        #region "Public Properties AspnetApplication"

        public new ICollectionView View;
        public new AspnetApplication CurrentAspnetApplication
        {
            get
            {
                if (View != null)
                    return View.CurrentItem as AspnetApplication;
                return null;
            }
        }
        //--------------------------------------------                                               

        private string _searchApplicationName;
        public new string SearchApplicationName
        {
            get { return _searchApplicationName; }
            set
            {
                if (value == _searchApplicationName) return;
                _searchApplicationName = value;
                RaisePropertyChanged("SearchApplicationName");
            }
        }

        private ObservableCollection<AspnetApplication> _aspnetApplicationsList;
        public new ObservableCollection<AspnetApplication> AspnetApplicationsList
        {
            get { return _aspnetApplicationsList; }
            set
            {
                if (_aspnetApplicationsList == value) return;
                _aspnetApplicationsList = value;
                RaisePropertyChanged("AspnetApplicationsList");
            }
        }

        #endregion "Public Properties"
   
  
       
        #region "Public Properties AspnetUser"

        public new ICollectionView View;
        public new AspnetUser CurrentAspnetUser
        {
            get
            {
                if (View != null)
                    return View.CurrentItem as AspnetUser;
                return null;
            }
        }
        //--------------------------------------------                                               

        private string _searchUserName;
        public new string SearchUserName
        {
            get { return _searchUserName; }
            set
            {
                if (value == _searchUserName) return;
                _searchUserName = value;
                RaisePropertyChanged("SearchUserName");
            }
        }

        private ObservableCollection<AspnetUser> _aspnetUsersList;
        public new ObservableCollection<AspnetUser> AspnetUsersList
        {
            get { return _aspnetUsersList; }
            set
            {
                if (_aspnetUsersList == value) return;
                _aspnetUsersList = value;
                RaisePropertyChanged("AspnetUsersList");
            }
        }

        #endregion "Public Properties"
   
   

   
    }
}   
