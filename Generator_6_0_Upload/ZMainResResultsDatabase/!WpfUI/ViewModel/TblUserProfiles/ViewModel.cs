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

//    TblUserProfileViewModel Skriptdatum:  16.03.2012  10:32    

namespace Atis.WpfUi.ViewModel
{   


    
    public class TblUserProfilesViewModel : AspnetUsersViewModel                     
    {     
        
          #endregion "Private Data Members"            
    
        #region "Constructor"

        public TblUserProfilesViewModel()
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
       
        #region "Public Commands Basic TblUserProfile"

        private RelayCommand _getUserProfileByNameCommand;
        public new ICommand GetUserProfileByNameCommand
        {
            get { return _getUserProfileByNameCommand ?? (_getUserProfileByNameCommand = new RelayCommand(GetUserProfileByName)); }
        }

        private void GetUserProfileByName()
        {   
TblUserProfilesList =
                 new ObservableCollection<TblUserProfile>((from x in TblUserProfilesRepository.TblUserProfiles
                                                        where x.LastName.StartsWith(SearchLastName)
                                                        orderby x.LastName
                                                        select x));

            TblUserProfilesAllList =
                 new ObservableCollection<TblUserProfile>((from y in TblUserProfilesRepository.TblUserProfiles
                                                        orderby y.LastName
                                                        select y));

            AspnetUsersAllList =
                 new ObservableCollection<aspnet_User>((from z in AspnetUsersRepository.AspnetUsers
                                                        orderby z.UserName
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

  
AspnetUsersList = null;                  
  View = CollectionViewSource.GetDefaultView(TblUserProfilesList);
            if (View != null)
                View.CurrentChanged += tbluserprofileView_CurrentChanged;                   
            RaisePropertyChanged("CurrentTblUserProfile");
        }
        //------------------------------------------------------------------------------
        private RelayCommand _addUserProfileCommand;
        public new ICommand AddUserProfileCommand
        {
            get { return _addUserProfileCommand ?? (_addUserProfileCommand = new RelayCommand(AddUserProfile)); }
        }

        private void AddUserProfile()
        {
            if (TblUserProfilesList == null)
                TblUserProfilesList = new ObservableCollection<TblUserProfile>();
            TblUserProfilesList.Add(new TblUserProfile{ LastName= "New " });
            View = CollectionViewSource.GetDefaultView(TblUserProfilesList);
            if (View != null)
                View.CurrentChanged += tbluserprofileView_CurrentChanged;
            RaisePropertyChanged("CurrentTblUserProfile");
        }
        //---------------------------------------------------------------------------------------
  
       
        private RelayCommand _deleteUserProfileCommand;
        public new ICommand DeleteUserProfileCommand
        {
            get { return _deleteUserProfileCommand ?? (_deleteUserProfileCommand = new RelayCommand(DeleteUserProfile)); }
        }

        private void DeleteUserProfile()
        {
            try
            {
                var userprofile= TblUserProfilesRepository.TblUserProfiles.FirstOrDefault(x => x.UserProfileID== CurrentTblUserProfile.UserProfileID);
                if (userprofile!= null)
                {
                    if (MessageBox.Show("Are you really shure to delete this "
                                        + CurrentTblUserProfile.LastName, "Question", MessageBoxButton.YesNo) !=
                        MessageBoxResult.Yes) return;
                    TblUserProfilesRepository.Delete(userprofile);
                    TblUserProfilesRepository.Save();
                    MessageBox.Show(CurrentTblUserProfile.LastName + " was deleted successfully");
                    GetUserProfileByName(); //Refresh
                }
                else
                {
                    MessageBox.Show("Only " + CurrentTblUserProfile.LastName+ " can be deleted");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //-------------------------------------------------------------------------------------------------
        private RelayCommand _saveUserProfileCommand;
        public new ICommand SaveUserProfileCommand
        {
            get { return _saveUserProfileCommand ?? (_saveUserProfileCommand = new RelayCommand(SaveUserProfile)); }
        }

        private void SaveUserProfile()
        {
            try
            {
                var userprofile= TblUserProfilesRepository.TblUserProfiles.FirstOrDefault(x => x.UserProfileID== CurrentTblUserProfile.UserProfileID);
                if (CurrentTblUserProfile == null)
                {
                    MessageBox.Show("userprofile was not found");
                }
                else
                {
                    if (CurrentTblUserProfile.UserProfileID!= 0)
                    {
                        if (userprofile!= null) //update
                        {
                            userprofile.Updater = Environment.UserName;
                            userprofile.UpdaterDate = DateTime.Now;
                        }
                    }
                    else
                    {
                        TblUserProfilesRepository.Add(new TblUserProfile
                        {
                            UserId= CurrentTblUserProfile.UserId,              
                            LastName= CurrentTblUserProfile.LastName,              
                            CountID = TblCountersRepository.Counter(),
                            Valid = CurrentTblUserProfile.Valid,
                            ValidYear = CurrentTblUserProfile.ValidYear,
                            Synonym = CurrentTblUserProfile.Synonym,
                            Author = CurrentTblUserProfile.Author,
                            AuthorYear = CurrentTblUserProfile.AuthorYear,
                            Info = CurrentTblUserProfile.Info,
                            EngName = CurrentTblUserProfile.EngName,
                            GerName = CurrentTblUserProfile.GerName,
                            FraName = CurrentTblUserProfile.FraName,
                            PorName = CurrentTblUserProfile.PorName,
                            Writer = Environment.UserName,
                            WriterDate = DateTime.Now,
                            Updater = Environment.UserName,
                            UpdaterDate = DateTime.Now,
                            Memo = CurrentTblUserProfile.Memo
                        });
                    }
                    {
                        TblUserProfilesRepository.Save();
                        MessageBox.Show(CurrentTblUserProfile.LastName+  " was successfully saved ");
                        GetUserProfileByName();  //Refresh
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion "Public Commands"
  

        
        #region "Public Commands Connect <== aspnet_User"                 

        private RelayCommand _getUserByNameCommand;
        public new ICommand GetUserByNameCommand
        {
            get { return _getUserByNameCommand ?? (_getUserByNameCommand = new RelayCommand(GetUserByName)); }
        }

        private void GetUserByName()
        {
            AspnetUsersList =
                new ObservableCollection<aspnet_User>((from user in AspnetUsersRepository.AspnetUsers
                                                       where user.UserName.StartsWith(SearchUserName)
                                                       orderby user.UserName
                                                       select user));

            View = CollectionViewSource.GetDefaultView(AspnetUsersList);
            if (View != null)
                View.CurrentChanged += aspnet_userView_CurrentChanged;                   
            RaisePropertyChanged("Currentaspnet_User");
        }
        //------------------------------------------------------------------------------
        private RelayCommand _addUserCommand;
        public new ICommand AddUserCommand
        {
            get { return _addUserCommand ?? (_addUserCommand = new RelayCommand(AddUser)); }
        }

        private void AddUser()
        {
            if (AspnetUsersList == null)
                AspnetUsersList = new ObservableCollection<aspnet_User>();
            AspnetUsersList.Add(new aspnet_User{ UserName= "New " });                   
            View = CollectionViewSource.GetDefaultView(AspnetUsersList);
            if (View != null)
                View.CurrentChanged += aspnet_userView_CurrentChanged;
            RaisePropertyChanged("Currentaspnet_User");
        }
        //----------------------------------------------------------------------------------------------------------
        private RelayCommand _deleteUserCommand;
        public ICommand UserPhylumCommand
        {
            get { return _deleteUserCommand ?? (_deleteUserCommand = new RelayCommand(DeleteUser)); }
        }

        private void DeleteUser()
        {
            try
            {
                var user= AspnetUsersRepository.AspnetUsers.FirstOrDefault(x => x.UserId== Currentaspnet_User.UserId);
                if (user!= null)
                {
                    if (MessageBox.Show("Are you really shure to delete this "
                                        + Currentaspnet_User.UserName, "Question", MessageBoxButton.YesNo) !=
                        MessageBoxResult.Yes) return;
                    AspnetUsersRepository.Delete(user);
                    AspnetUsersRepository.Save();
                    MessageBox.Show(Currentaspnet_User.UserName+ " was deleted successfully");
                    if (SearchUserName == null)
                    {
                        GetConnectedTablesById(); //refresh doubleClick                    
                    }
                    else
                    {
                        GetUserByName(); //search
                    }
                }
                else
                {
                    MessageBox.Show("Only " + Currentaspnet_User.UserName+ " can be deleted");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //-------------------------------------------------------------------------------------------------
        private RelayCommand _saveUserCommand;   
        public new ICommand SaveUserCommand
        {
            get { return _saveUserCommand ?? (_saveUserCommand = new RelayCommand(SaveUser)); }
        }

        private void SaveUser()
        {
            try
            {
                var user= AspnetUsersRepository.AspnetUsers.FirstOrDefault(x => x.UserId== Currentaspnet_User.UserId);
                if (Currentaspnet_User == null)
                {
                    MessageBox.Show("user was not found");
                }
                else
                {
                    if (Currentaspnet_User.UserId!= 0)
                    {
                        if (user!= null) //update
                        {
                            user.Updater = Environment.UserName;
                            user.UpdaterDate = DateTime.Now;
                        }
                    }
                    else
                    {
                        AspnetUsersRepository.Add(new aspnet_User()
                        {
                            UserName= Currentaspnet_User.UserName,              
                            CountID = TblCountersRepository.Counter(),
                            Valid = Currentaspnet_User.Valid,
                            ValidYear = Currentaspnet_User.ValidYear,
                            Synonym = Currentaspnet_User.Synonym,
                            Author = Currentaspnet_User.Author,
                            AuthorYear = Currentaspnet_User.AuthorYear,
                            Info = Currentaspnet_User.Info,
                            EngName = Currentaspnet_User.EngName,
                            GerName = Currentaspnet_User.GerName,
                            FraName = Currentaspnet_User.FraName,
                            PorName = Currentaspnet_User.PorName,
                            Writer = Environment.UserName,
                            WriterDate = DateTime.Now,
                            Updater = Environment.UserName,
                            UpdaterDate = DateTime.Now,
                            Memo = Currentaspnet_User.Memo
                        });
                    }
                    {
                        AspnetUsersRepository.Save();
                        MessageBox.Show(Currentaspnet_User.UserName+  " was successfully saved ");
                        GetUserByName();  //Refresh
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
                            UserProfileID= CurrentTbl90RefAuthor.UserProfileID,
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
                            UserProfileID= CurrentTbl90RefSource.UserProfileID,
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
                            UserProfileID= CurrentTbl90RefExpert.UserProfileID,
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
                            UserProfileID= CurrentTbl93Comment.UserProfileID,                
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
            SearchUserName = null;                       
            SearchNULLName = null;
            SearchCommentInfo = null;
            SearchRefExpertName = null;
            SearchRefSourceName = null;
            SearchRefAuthorName = null;

            AspnetUsersList =
                new ObservableCollection<aspnet_User>((from user in AspnetUsersRepository.AspnetUsers
                                                       where user.UserId== CurrentTblUserProfile.UserId
                                                       orderby user.UserName
                                                       select user));

            View = CollectionViewSource.GetDefaultView(AspnetUsersList);
            if (View != null)
                View.CurrentChanged += aspnet_userView_CurrentChanged;
            RaisePropertyChanged("CurrentAspnetUsers");
            //-----------------------------------------------------------------------------------
            NULLList =
                new ObservableCollection<NULL>((from NULL in NULLRepository.NULL
                                                       where NULL.UserProfileID== CurrentTblUserProfile.UserProfileID
                                                       orderby NULL.TblUserProfiles.LastName
                                                       select NULL));


            View = CollectionViewSource.GetDefaultView(NULLList);
            if (View != null)
                View.CurrentChanged += NULLView_CurrentChanged;
            RaisePropertyChanged("CurrentNULL");
            //-----------------------------------------------------------------------------------
            Tbl90RefAuthorsList =
                new ObservableCollection<Tbl90Reference>((from refAu in Tbl90ReferencesRepository.Tbl90References
                                                          where refAu.UserProfileID== CurrentTblUserProfile.UserProfileID
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
                                                          where refSo.UserProfileID== CurrentTblUserProfile.UserProfileID
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
                                                          where refEx.UserProfileID== CurrentTblUserProfile.UserProfileID
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
                                                          where comm.UserProfileID== CurrentTblUserProfile.UserProfileID
                                                        orderby comm.Info
                                                        select comm));

            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            if (CommentsView != null)
                CommentsView.CurrentChanged += tbl93CommentView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl93Comment");
            //--------------------------------------------------------------

        }

        #endregion "Public Commands Connected Tables by DoubleClick"
   

     
        #region "Public Properties TblUserProfile"

        public new ICollectionView View;
        public new TblUserProfile CurrentTblUserProfile
        {
            get
            {
                if (View != null)
                    return View.CurrentItem as TblUserProfile;
                return null;
            }
        }
        //--------------------------------------------
        private string _searchLastName;
        public new string SearchLastName
        {
            get { return _searchLastName; }
            set
            {
                if (value == _searchLastName) return;
                _searchLastName = value;
                RaisePropertyChanged("SearchLastName");
            }
        }

        private ObservableCollection<TblUserProfile> _tblUserProfilesList;
        public new ObservableCollection<TblUserProfile> TblUserProfilesList
        {
            get { return _tblUserProfilesList; }
            set
            {
                if (_tblUserProfilesList == value) return;
                _tblUserProfilesList = value;
                RaisePropertyChanged("TblUserProfilesList");

                //Clear Search-TextBox
                SearchUserName = null;                                
                SearchNULL = null;
                SearchCommentInfo = null;
                SearchRefExpertName = null;
                SearchRefSourceName = null;
                SearchRefAuthorName = null;
            }
        }

        private ObservableCollection<TblUserProfile> _tblUserProfilesAllList;
        public ObservableCollection<TblUserProfile> TblUserProfilesAllList
        {
            get { return _tblUserProfilesAllList; }
            set
            {
                if (_tblUserProfilesAllList == value) return;
                _tblUserProfilesAllList = value;
                RaisePropertyChanged("TblUserProfilesAllList");
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

        private ObservableCollection<aspnet_User> _aspnetUsersList;
        public new ObservableCollection<aspnet_User> AspnetUsersList
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
