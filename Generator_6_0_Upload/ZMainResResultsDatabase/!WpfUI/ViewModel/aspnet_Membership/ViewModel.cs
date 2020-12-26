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

//    aspnet_MembershipViewModel Skriptdatum:  06.02.2012  10:32    

namespace Atis.WpfUi.ViewModel
{   


    
    public class aspnet_MembershipsViewModel : aspnet_ApplicationsViewModel                     
    {     
        
          #endregion "Private Data Members"            
    
        #region "Constructor"

        public aspnet_MembershipsViewModel()
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
       
        #region "Public Commands Basic aspnet_Membership"

        private RelayCommand _getMembershipByNameCommand;
        public new ICommand GetMembershipByNameCommand
        {
            get { return _getMembershipByNameCommand ?? (_getMembershipByNameCommand = new RelayCommand(GetMembershipByName)); }
        }

        private void GetMembershipByName()
        {   
aspnet_MembershipsList =
                 new ObservableCollection<aspnet_Membership>((from x in aspnet_MembershipsRepository.aspnet_Memberships
                                                        where x.UserName.StartsWith(SearchUserName)
                                                        orderby x.UserName
                                                        select x));

            aspnet_MembershipsAllList =
                 new ObservableCollection<aspnet_Membership>((from y in aspnet_MembershipsRepository.aspnet_Memberships
                                                        orderby y.UserName
                                                        select y));

            aspnet_ApplicationsAllList =
                 new ObservableCollection<aspnet_Application>((from z in aspnet_ApplicationsRepository.aspnet_Applications
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

  
aspnet_ApplicationsList = null;                  
  View = CollectionViewSource.GetDefaultView(aspnet_MembershipsList);
            if (View != null)
                View.CurrentChanged += aspnet_membershipView_CurrentChanged;                   
            RaisePropertyChanged("Currentaspnet_Membership");
        }
        //------------------------------------------------------------------------------
        private RelayCommand _addMembershipCommand;
        public new ICommand AddMembershipCommand
        {
            get { return _addMembershipCommand ?? (_addMembershipCommand = new RelayCommand(AddMembership)); }
        }

        private void AddMembership()
        {
            if (aspnet_MembershipsList == null)
                aspnet_MembershipsList = new ObservableCollection<aspnet_Membership>();
            aspnet_MembershipsList.Add(new aspnet_Membership{ UserName= "New " });
            View = CollectionViewSource.GetDefaultView(aspnet_MembershipsList);
            if (View != null)
                View.CurrentChanged += aspnet_membershipView_CurrentChanged;
            RaisePropertyChanged("Currentaspnet_Membership");
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
                var membership= aspnet_MembershipsRepository.aspnet_Memberships.FirstOrDefault(x => x.UserId== Currentaspnet_Membership.UserId);
                if (membership!= null)
                {
                    if (MessageBox.Show("Are you really shure to delete this "
                                        + Currentaspnet_Membership.UserName, "Question", MessageBoxButton.YesNo) !=
                        MessageBoxResult.Yes) return;
                    aspnet_MembershipsRepository.Delete(membership);
                    aspnet_MembershipsRepository.Save();
                    MessageBox.Show(Currentaspnet_Membership.UserName + " was deleted successfully");
                    GetMembershipByName(); //Refresh
                }
                else
                {
                    MessageBox.Show("Only " + Currentaspnet_Membership.UserName+ " can be deleted");
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
                var membership= aspnet_MembershipsRepository.aspnet_Memberships.FirstOrDefault(x => x.UserId== Currentaspnet_Membership.UserId);
                if (Currentaspnet_Membership == null)
                {
                    MessageBox.Show("membership was not found");
                }
                else
                {
                    if (Currentaspnet_Membership.UserId!= 0)
                    {
                        if (membership!= null) //update
                        {
                            membership.Updater = Environment.UserName;
                            membership.UpdaterDate = DateTime.Now;
                        }
                    }
                    else
                    {
                        aspnet_MembershipsRepository.Add(new aspnet_Membership
                        {
                            ApplicationId= Currentaspnet_Membership.ApplicationId,              
                            UserName= Currentaspnet_Membership.UserName,              
                            CountID = TblCountersRepository.Counter(),
                            Valid = Currentaspnet_Membership.Valid,
                            ValidYear = Currentaspnet_Membership.ValidYear,
                            Synonym = Currentaspnet_Membership.Synonym,
                            Author = Currentaspnet_Membership.Author,
                            AuthorYear = Currentaspnet_Membership.AuthorYear,
                            Info = Currentaspnet_Membership.Info,
                            EngName = Currentaspnet_Membership.EngName,
                            GerName = Currentaspnet_Membership.GerName,
                            FraName = Currentaspnet_Membership.FraName,
                            PorName = Currentaspnet_Membership.PorName,
                            Writer = Environment.UserName,
                            WriterDate = DateTime.Now,
                            Updater = Environment.UserName,
                            UpdaterDate = DateTime.Now,
                            Memo = Currentaspnet_Membership.Memo
                        });
                    }
                    {
                        aspnet_MembershipsRepository.Save();
                        MessageBox.Show(Currentaspnet_Membership.UserName+  " was successfully saved ");
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
  

        
        #region "Public Commands Connect <== aspnet_Application"                 

        private RelayCommand _getApplicationByNameCommand;
        public new ICommand GetApplicationByNameCommand
        {
            get { return _getApplicationByNameCommand ?? (_getApplicationByNameCommand = new RelayCommand(GetApplicationByName)); }
        }

        private void GetApplicationByName()
        {
            aspnet_ApplicationsList =
                new ObservableCollection<aspnet_Application>((from application in aspnet_ApplicationsRepository.aspnet_Applications
                                                       where application.ApplicationName.StartsWith(SearchApplicationName)
                                                       orderby application.ApplicationName
                                                       select application));

            View = CollectionViewSource.GetDefaultView(aspnet_ApplicationsList);
            if (View != null)
                View.CurrentChanged += aspnet_applicationView_CurrentChanged;                   
            RaisePropertyChanged("Currentaspnet_Application");
        }
        //------------------------------------------------------------------------------
        private RelayCommand _addApplicationCommand;
        public new ICommand AddApplicationCommand
        {
            get { return _addApplicationCommand ?? (_addApplicationCommand = new RelayCommand(AddApplication)); }
        }

        private void AddApplication()
        {
            if (aspnet_ApplicationsList == null)
                aspnet_ApplicationsList = new ObservableCollection<aspnet_Application>();
            aspnet_ApplicationsList.Add(new aspnet_Application{ ApplicationName= "New " });                   
            View = CollectionViewSource.GetDefaultView(aspnet_ApplicationsList);
            if (View != null)
                View.CurrentChanged += aspnet_applicationView_CurrentChanged;
            RaisePropertyChanged("Currentaspnet_Application");
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
                var application= aspnet_ApplicationsRepository.aspnet_Applications.FirstOrDefault(x => x.ApplicationId== Currentaspnet_Application.ApplicationId);
                if (application!= null)
                {
                    if (MessageBox.Show("Are you really shure to delete this "
                                        + Currentaspnet_Application.ApplicationName, "Question", MessageBoxButton.YesNo) !=
                        MessageBoxResult.Yes) return;
                    aspnet_ApplicationsRepository.Delete(application);
                    aspnet_ApplicationsRepository.Save();
                    MessageBox.Show(Currentaspnet_Application.ApplicationName+ " was deleted successfully");
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
                    MessageBox.Show("Only " + Currentaspnet_Application.ApplicationName+ " can be deleted");
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
                var application= aspnet_ApplicationsRepository.aspnet_Applications.FirstOrDefault(x => x.ApplicationId== Currentaspnet_Application.ApplicationId);
                if (Currentaspnet_Application == null)
                {
                    MessageBox.Show("application was not found");
                }
                else
                {
                    if (Currentaspnet_Application.ApplicationId!= 0)
                    {
                        if (application!= null) //update
                        {
                            application.Updater = Environment.UserName;
                            application.UpdaterDate = DateTime.Now;
                        }
                    }
                    else
                    {
                        aspnet_ApplicationsRepository.Add(new aspnet_Application()
                        {
                            ApplicationName= Currentaspnet_Application.ApplicationName,              
                            CountID = TblCountersRepository.Counter(),
                            Valid = Currentaspnet_Application.Valid,
                            ValidYear = Currentaspnet_Application.ValidYear,
                            Synonym = Currentaspnet_Application.Synonym,
                            Author = Currentaspnet_Application.Author,
                            AuthorYear = Currentaspnet_Application.AuthorYear,
                            Info = Currentaspnet_Application.Info,
                            EngName = Currentaspnet_Application.EngName,
                            GerName = Currentaspnet_Application.GerName,
                            FraName = Currentaspnet_Application.FraName,
                            PorName = Currentaspnet_Application.PorName,
                            Writer = Environment.UserName,
                            WriterDate = DateTime.Now,
                            Updater = Environment.UserName,
                            UpdaterDate = DateTime.Now,
                            Memo = Currentaspnet_Application.Memo
                        });
                    }
                    {
                        aspnet_ApplicationsRepository.Save();
                        MessageBox.Show(Currentaspnet_Application.ApplicationName+  " was successfully saved ");
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

            aspnet_ApplicationsList =
                new ObservableCollection<aspnet_Application>((from application in aspnet_ApplicationsRepository.aspnet_Applications
                                                       where application.ApplicationId== Currentaspnet_Membership.ApplicationId
                                                       orderby application.ApplicationName
                                                       select application));

            View = CollectionViewSource.GetDefaultView(aspnet_ApplicationsList);
            if (View != null)
                View.CurrentChanged += aspnet_applicationView_CurrentChanged;
            RaisePropertyChanged("Currentaspnet_Applications");
            //-----------------------------------------------------------------------------------
            NULLList =
                new ObservableCollection<NULL>((from NULL in NULLRepository.NULL
                                                       where NULL.UserId== Currentaspnet_Membership.UserId
                                                       orderby NULL.aspnet_Memberships.UserName
                                                       select NULL));


            View = CollectionViewSource.GetDefaultView(NULLList);
            if (View != null)
                View.CurrentChanged += NULLView_CurrentChanged;
            RaisePropertyChanged("CurrentNULL");
            //-----------------------------------------------------------------------------------
            Tbl90RefAuthorsList =
                new ObservableCollection<Tbl90Reference>((from refAu in Tbl90ReferencesRepository.Tbl90References
                                                          where refAu.UserId== Currentaspnet_Membership.UserId
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
                                                          where refSo.UserId== Currentaspnet_Membership.UserId
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
                                                          where refEx.UserId== Currentaspnet_Membership.UserId
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
                                                          where comm.UserId== Currentaspnet_Membership.UserId
                                                        orderby comm.Info
                                                        select comm));

            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            if (CommentsView != null)
                CommentsView.CurrentChanged += tbl93CommentView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl93Comment");
            //--------------------------------------------------------------

        }

        #endregion "Public Commands Connected Tables by DoubleClick"
   

     
        #region "Public Properties aspnet_Membership"

        public new ICollectionView View;
        public new aspnet_Membership Currentaspnet_Membership
        {
            get
            {
                if (View != null)
                    return View.CurrentItem as aspnet_Membership;
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

        private ObservableCollection<aspnet_Membership> aspnet_MembershipsList;
        public new ObservableCollection<aspnet_Membership> aspnet_MembershipsList
        {
            get { return aspnet_MembershipsList; }
            set
            {
                if (aspnet_MembershipsList == value) return;
                aspnet_MembershipsList = value;
                RaisePropertyChanged("aspnet_MembershipsList");

                //Clear Search-TextBox
                SearchApplicationName = null;                                
                SearchNULL = null;
                SearchCommentInfo = null;
                SearchRefExpertName = null;
                SearchRefSourceName = null;
                SearchRefAuthorName = null;
            }
        }

        private ObservableCollection<aspnet_Membership> aspnet_MembershipsAllList;
        public ObservableCollection<aspnet_Membership> aspnet_MembershipsAllList
        {
            get { return aspnet_MembershipsAllList; }
            set
            {
                if (aspnet_MembershipsAllList == value) return;
                aspnet_MembershipsAllList = value;
                RaisePropertyChanged("aspnet_MembershipsAllList");
            }
        }

        #endregion "Public Properties"
   

       
        #region "Public Properties aspnet_Application"

        public new ICollectionView View;
        public new aspnet_Application Currentaspnet_Application
        {
            get
            {
                if (View != null)
                    return View.CurrentItem as aspnet_Application;
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

        private ObservableCollection<aspnet_Application> aspnet_ApplicationsList;
        public new ObservableCollection<aspnet_Application> aspnet_ApplicationsList
        {
            get { return aspnet_ApplicationsList; }
            set
            {
                if (aspnet_ApplicationsList == value) return;
                aspnet_ApplicationsList = value;
                RaisePropertyChanged("aspnet_ApplicationsList");
            }
        }

        #endregion "Public Properties"
   
  
       
        #region "Public Properties aspnet_User"

        public new ICollectionView View;
        public new aspnet_User Currentaspnet_User
        {
            get
            {
                if (View != null)
                    return View.CurrentItem as aspnet_User;
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

        private ObservableCollection<aspnet_User> aspnet_UsersList;
        public new ObservableCollection<aspnet_User> aspnet_UsersList
        {
            get { return aspnet_UsersList; }
            set
            {
                if (aspnet_UsersList == value) return;
                aspnet_UsersList = value;
                RaisePropertyChanged("aspnet_UsersList");
            }
        }

        #endregion "Public Properties"
   
   

   
    }
}   
