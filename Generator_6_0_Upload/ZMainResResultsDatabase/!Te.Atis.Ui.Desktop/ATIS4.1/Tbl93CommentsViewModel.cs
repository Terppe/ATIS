using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity.Validation;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using Te.Atis.BusinessLayer;
using Te.Atis.DomainModel;
using Te.Atis.Ui.Desktop.Domain;
using Te.Atis.Ui.Desktop.Domain.Helper;
using Te.Atis.Ui.Desktop.MessageBox;    

    
         //    Tbl93CommentsViewModel Skriptdatum:  30.07.2018  10:32    

namespace Te.Atis.Ui.Desktop.Views.Database
{     
    
    public class Tbl93CommentsViewModel : ViewModelBase                     
    {     
         
        #region "Private Data Members"

        private static IBusinessLayer _businessLayer;
        private static DbEntityException _entityException;   
         
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
                _businessLayer = new BusinessLayer.BusinessLayer();
                _entityException = new DbEntityException();
            }
        }     
        #endregion "Constructor"         
 

 //    Part 1    

             
        #region "Public Commands Basic Tbl93Comment"
        //-------------------------------------------------------------------------
        private RelayCommand _clearCommentCommand;

        public ICommand ClearCommentCommand => _clearCommentCommand ??
                                                  (_clearCommentCommand = new RelayCommand(delegate { ClearComment(null); }));         
             
        private RelayCommand _getCommentsByNameOrIdCommand;  

        public  ICommand GetCommentsByNameOrIdCommand => _getCommentsByNameOrIdCommand ??
                                                           (_getCommentsByNameOrIdCommand = new RelayCommand(delegate { GetCommentsByNameOrId(null); }));        
             
        private RelayCommand _addCommentCommand;

        public ICommand AddCommentCommand => _addCommentCommand ??
                                                (_addCommentCommand = new RelayCommand(delegate { AddComment(null); }));

        private RelayCommand _copyCommentCommand;

        public ICommand CopyCommentCommand => _copyCommentCommand ??
                                                 (_copyCommentCommand = new RelayCommand(delegate { CopyComment(null); }));      
             
        private RelayCommand _deleteCommentCommand;

        public ICommand DeleteCommentCommand => _deleteCommentCommand ??
                                                   (_deleteCommentCommand = new RelayCommand(delegate { DeleteComment(null); }));    
             
        private RelayCommand _saveCommentCommand;

        public ICommand SaveCommentCommand => _saveCommentCommand ??
                                                 (_saveCommentCommand = new RelayCommand(delegate { SaveComment(null); }));
        //-------------------------------------------------------------------------          
        
        private void ClearComment(object o)
        {
            SearchCommentInfo = string.Empty;

            Tbl93CommentsList?.Clear();
        }
        //----------------------------------------------------------------------                  
        
        private void GetCommentsByNameOrId(object o)
        {
            Tbl93CommentsList = int.TryParse(SearchCommentInfo, out var id) ?
                new ObservableCollection<Tbl93Comment>(_businessLayer.ListTbl93CommentsByCommentId(id)) :
                new ObservableCollection<Tbl93Comment>(_businessLayer.ListTbl93CommentsByInfo(SearchCommentInfo));

            Tbl03RegnumsAllList = new ObservableCollection<Tbl03Regnum>(_businessLayer.ListTbl03Regnums());
            Tbl06PhylumsAllList = new ObservableCollection<Tbl06Phylum>(_businessLayer.ListTbl06Phylums());
            Tbl09DivisionsAllList = new ObservableCollection<Tbl09Division>(_businessLayer.ListTbl09Divisions());
            Tbl12SubphylumsAllList = new ObservableCollection<Tbl12Subphylum>(_businessLayer.ListTbl12Subphylums());
            Tbl15SubdivisionsAllList = new ObservableCollection<Tbl15Subdivision>(_businessLayer.ListTbl15Subdivisions());
            Tbl18SuperclassesAllList = new ObservableCollection<Tbl18Superclass>(_businessLayer.ListTbl18Superclasses());
            Tbl21ClassesAllList = new ObservableCollection<Tbl21Class>(_businessLayer.ListTbl21Classes());
            Tbl24SubclassesAllList = new ObservableCollection<Tbl24Subclass>(_businessLayer.ListTbl24Subclasses());
            Tbl27InfraclassesAllList = new ObservableCollection<Tbl27Infraclass>(_businessLayer.ListTbl27Infraclasses());
            Tbl30LegiosAllList = new ObservableCollection<Tbl30Legio>(_businessLayer.ListTbl30Legios());
            Tbl33OrdosAllList = new ObservableCollection<Tbl33Ordo>(_businessLayer.ListTbl33Ordos());
            Tbl36SubordosAllList = new ObservableCollection<Tbl36Subordo>(_businessLayer.ListTbl36Subordos());
            Tbl39InfraordosAllList = new ObservableCollection<Tbl39Infraordo>(_businessLayer.ListTbl39Infraordos());
            Tbl42SuperfamiliesAllList = new ObservableCollection<Tbl42Superfamily>(_businessLayer.ListTbl42Superfamilies());
            Tbl45FamiliesAllList = new ObservableCollection<Tbl45Family>(_businessLayer.ListTbl45Families());
            Tbl48SubfamiliesAllList = new ObservableCollection<Tbl48Subfamily>(_businessLayer.ListTbl48Subfamilies());
            Tbl51InfrafamiliesAllList = new ObservableCollection<Tbl51Infrafamily>(_businessLayer.ListTbl51Infrafamilies());
            Tbl54SupertribussesAllList = new ObservableCollection<Tbl54Supertribus>(_businessLayer.ListTbl54Supertribusses());
            Tbl57TribussesAllList = new ObservableCollection<Tbl57Tribus>(_businessLayer.ListTbl57Tribusses());
            Tbl60SubtribussesAllList = new ObservableCollection<Tbl60Subtribus>(_businessLayer.ListTbl60Subtribusses());
            Tbl63InfratribussesAllList = new ObservableCollection<Tbl63Infratribus>(_businessLayer.ListTbl63Infratribusses());
            Tbl66GenussesAllList = new ObservableCollection<Tbl66Genus>(_businessLayer.ListTbl66Genusses());
            Tbl69FiSpeciessesAllList = new ObservableCollection<Tbl69FiSpecies>(_businessLayer.ListTbl69FiSpeciesses());
            Tbl72PlSpeciessesAllList = new ObservableCollection<Tbl72PlSpecies>(_businessLayer.ListTbl72PlSpeciesses());

            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            CommentsView.Refresh();
        }
        //------------------------------------------------------------------------------------                          
        
        private void AddComment(object o)
        {
            Tbl93CommentsList = new ObservableCollection<Tbl93Comment> {new Tbl93Comment {CommentID = 0}};

            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            CommentsView.MoveCurrentToFirst();
        }
        //------------------------------------------------------------------------------------                               
        
        private void CopyComment(object o)
        {
            Tbl93CommentsList = new ObservableCollection<Tbl93Comment>();

            var comment = _businessLayer.SingleListTbl93CommentsByCommentId(CurrentTbl93Comment.CommentID);

            Tbl93CommentsList.Add(new Tbl93Comment
            {
                Valid = comment.Valid,
                ValidYear = comment.ValidYear,
                Info = comment.Info,
                Memo = comment.Memo
            });

            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            CommentsView.MoveCurrentToFirst();
        }
        //---------------------------------------------------------------------------------------                            
        
        private void DeleteComment(object o)
        {
            try
            {
                var comment = _businessLayer.SingleListTbl93CommentsByCommentId(CurrentTbl93Comment.CommentID);
                if (comment != null)
                {
                    if (WpfMessageBox.Show(CultRes.StringsRes.DeleteQuestion1, CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl93Comment.Info,
                            MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes)
                        return;
                    comment.EntityState = EntityState.Deleted;
                    _businessLayer.RemoveComment(comment);

                    WpfMessageBox.Show(CultRes.StringsRes.DeleteSuccess, CurrentTbl93Comment.Info,
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                }
                else
                {
                    WpfMessageBox.Show(CultRes.StringsRes.Information, CultRes.StringsRes.DeleteCan + " " + CurrentTbl93Comment.Info + " " + CultRes.StringsRes.DeleteCan1,
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                }
            }
            catch (DbEntityValidationException ex)
            {
                _entityException.EntityException(ex);
            }

            Tbl93CommentsList = new ObservableCollection<Tbl93Comment>(_businessLayer.ListTbl93CommentsByInfo(SearchCommentInfo));

            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            CommentsView.Refresh();
        }
        //-------------------------------------------------------------------------------------------------                    
       
        private void SaveComment(object o)
        {
            try
            {
                var comment = _businessLayer.SingleListTbl93CommentsByCommentId(CurrentTbl93Comment.CommentID);
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
                        comment.EntityState = EntityState.Modified;
                    }
                }
                else
                {
                            comment = new Tbl93Comment   //add new
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
                            UpdaterDate = DateTime.Now,
                            EntityState = EntityState.Added
                    };
                }
                {
                    //check if dataset RegnumID to PlSpeciesID and Info already exist       
                    var dataset = _businessLayer.ListTbl93CommentsByCurrentItem(CurrentTbl93Comment);

                    if (dataset.Count != 0 && CurrentTbl93Comment.CommentID == 0)  //dataset exist
                    {
                        WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTbl93Comment.CommentID.ToString(),
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                    }
                    if (dataset.Count == 0 && CurrentTbl93Comment.CommentID == 0 ||
                        dataset.Count != 0 && CurrentTbl93Comment.CommentID != 0 ||
                        dataset.Count == 0 && CurrentTbl93Comment.CommentID != 0) //new dataset and update
                    {
                        if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTbl93Comment.CommentID.ToString(),
                                MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes)
                            return;
                        {
                            _businessLayer.UpdateComment(comment);

                            WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess, CurrentTbl93Comment.CommentID.ToString(),
                                MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        }
                    }
                }
            }
            catch (DbEntityValidationException ex)
            {
                _entityException.EntityException(ex);
            }

            if (CurrentTbl93Comment.CommentID == 0)  //new Dataset                        
                Tbl93CommentsList = new ObservableCollection<Tbl93Comment>(_businessLayer.ListTbl93CommentsByInfo(CurrentTbl93Comment.Info));
            if (CurrentTbl93Comment.CommentID != 0)   //update 
                Tbl93CommentsList = new ObservableCollection<Tbl93Comment>(_businessLayer.ListTbl93CommentsByCommentId(CurrentTbl93Comment.CommentID));

            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            CommentsView.Refresh();
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

 

 //    Part 11    

        
        #region "Public Properties Tbl93Comment"

        private string _searchCommentInfo = string.Empty;
        public string SearchCommentInfo
        {
            get => _searchCommentInfo;
            set { _searchCommentInfo = value; RaisePropertyChanged(); }
        }

        public ICollectionView CommentsView;
        public Tbl93Comment CurrentTbl93Comment => CommentsView?.CurrentItem as Tbl93Comment;

        private ObservableCollection<Tbl93Comment> _tbl93CommentsList;
        public ObservableCollection<Tbl93Comment> Tbl93CommentsList
        {
            get => _tbl93CommentsList;
            set { _tbl93CommentsList = value; RaisePropertyChanged(); }
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
 

 



   }
}   
