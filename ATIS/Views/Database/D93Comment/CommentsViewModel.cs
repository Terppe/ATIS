using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;


using System.Windows.Data;
using System.Windows.Input;
using ATIS.Ui.Models;
using ATIS.Ui.Core;
using ATIS.Ui.Helper;

//    CommentsViewModel Skriptdatum:  10.02.2021  10:32    

namespace ATIS.Ui.Views.Database.D93Comment
{

    public class CommentsViewModel : ViewModelBase
    {
        // Version with Generic Unit Of Work and AtisDbContext for general use   

        #region [Private Data Members]
        private readonly CrudFunctions _extCrud = new CrudFunctions();
        private readonly DeleteFunctions _extDelete = new DeleteFunctions();
        private readonly SaveFunctions _extSave = new SaveFunctions();
        private readonly AllMessageBoxes _allMessageBoxes = new AllMessageBoxes();
        private int _position;

        #endregion [Private Data Members]               

        #region [Constructor]

        public CommentsViewModel()
        {
            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.
            }
            else
            {

                // Code runs "for real" 
                Tbl93CommentsList = new ObservableCollection<Tbl93Comment>();
            }
        }
        public bool IsInDesignMode { get; set; }

        #endregion [Constructor]          


        //    Part 1    



        #region [Commands Comment]

        private RelayCommand _getCommentsByNameOrIdCommand;
        public ICommand GetCommentsByNameOrIdCommand => _getCommentsByNameOrIdCommand ??= new RelayCommand(delegate { ExecuteGetCommentsByNameOrId(SearchCommentInfo); });

        private RelayCommand _addCommentCommand;
        public ICommand AddCommentCommand => _addCommentCommand ??= new RelayCommand(delegate { ExecuteAddComment(null); });

        private RelayCommand _copyCommentCommand;
        public ICommand CopyCommentCommand => _copyCommentCommand ??= new RelayCommand(delegate { ExecuteCopyComment(null); });

        private RelayCommand _deleteCommentCommand;
        public ICommand DeleteCommentCommand => _deleteCommentCommand ??= new RelayCommand(delegate { ExecuteDeleteComment(SearchCommentInfo); });

        private RelayCommand _saveCommentCommand;
        public ICommand SaveCommentCommand => _saveCommentCommand ??= new RelayCommand(delegate { ExecuteSaveComment(SearchCommentInfo); });

        #endregion [Commands Comment]       


        #region [Methods Comment]

        private void ExecuteGetCommentsByNameOrId(string searchName)
        {
            ConnectedAllLists();

            if (Tbl93CommentsList == null)
                Tbl93CommentsList ??= new ObservableCollection<Tbl93Comment>();
            else
                Tbl93CommentsList.Clear();

            Tbl93CommentsList = _extCrud.GetCommentsCollectionFromSearchInfoOrIdOrderBy<Tbl93Comment>(searchName);

            if (_allMessageBoxes.NoDatasetFoundInfoMessageBox(Tbl93CommentsList.Count)) return;

            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            CommentsView.Refresh();
        }

        private void ExecuteAddComment(object o)
        {
            ConnectedAllLists();

            if (Tbl93CommentsList == null)
                Tbl93CommentsList ??= new ObservableCollection<Tbl93Comment>();
            else
                Tbl93CommentsList.Clear();

            Tbl93CommentsList.Insert(0, new Tbl93Comment { Info = CultRes.StringsRes.DatasetNew });

            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            CommentsView.MoveCurrentToFirst();
        }

        private void ExecuteCopyComment(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl93Comment)) return;

            Tbl93CommentsList = _extCrud.CopyComment(CurrentTbl93Comment);

            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            CommentsView.MoveCurrentToFirst();
        }

        private void ExecuteDeleteComment(string searchName)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl93Comment)) return;

            _extDelete.DeleteComment(CurrentTbl93Comment);

            Tbl93CommentsList = _extCrud.GetCollectionFromSearchNameOrIdOrderBy<Tbl93Comment>(searchName, "Comment");
            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            CommentsView.MoveCurrentToLast();
        }

        private void ExecuteSaveComment(string searchName)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl93Comment)) return;

            _position = CommentsView.CurrentPosition;

            var ret = _extSave.SaveComment(CurrentTbl93Comment);

            if (ret != true)
            {
                CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
                CommentsView.Refresh();
                return;
            }

            if (CurrentTbl93Comment.CommentId == 0) //new
            {
                Tbl93CommentsList = _extCrud.GetLastCommentsDatasetOrderById();
                CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
                CommentsView.MoveCurrentToFirst();
            }
            else
            {
                Tbl93CommentsList = _extCrud.GetCommentsCollectionFromSearchInfoOrIdOrderBy<Tbl93Comment>(searchName);
                CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
                CommentsView.MoveCurrentToPosition(_position);
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

        #region Methode AllLists

        private void ConnectedAllLists()
        {
            if (Tbl03RegnumsAllList == null)
                Tbl03RegnumsAllList ??= new ObservableCollection<Tbl03Regnum>();
            else
                Tbl03RegnumsAllList.Clear();
            Tbl03RegnumsAllList = _extCrud.GetCollectionAllOrderBy<Tbl03Regnum>("Regnum");

            if (Tbl06PhylumsAllList == null)
                Tbl06PhylumsAllList ??= new ObservableCollection<Tbl06Phylum>();
            else
                Tbl06PhylumsAllList.Clear();
            Tbl06PhylumsAllList = _extCrud.GetCollectionAllOrderBy<Tbl06Phylum>("Phylum");

            if (Tbl09DivisionsAllList == null)
                Tbl09DivisionsAllList ??= new ObservableCollection<Tbl09Division>();
            else
                Tbl09DivisionsAllList.Clear();
            Tbl09DivisionsAllList = _extCrud.GetCollectionAllOrderBy<Tbl09Division>("Division");

            if (Tbl12SubphylumsAllList == null)
                Tbl12SubphylumsAllList ??= new ObservableCollection<Tbl12Subphylum>();
            else
                Tbl12SubphylumsAllList.Clear();
            Tbl12SubphylumsAllList = _extCrud.GetCollectionAllOrderBy<Tbl12Subphylum>("Subphylum");

            if (Tbl15SubdivisionsAllList == null)
                Tbl15SubdivisionsAllList ??= new ObservableCollection<Tbl15Subdivision>();
            else
                Tbl15SubdivisionsAllList.Clear();
            Tbl15SubdivisionsAllList = _extCrud.GetCollectionAllOrderBy<Tbl15Subdivision>("Subdivision");

            if (Tbl18SuperclassesAllList == null)
                Tbl18SuperclassesAllList ??= new ObservableCollection<Tbl18Superclass>();
            else
                Tbl18SuperclassesAllList.Clear();
            Tbl18SuperclassesAllList = _extCrud.GetCollectionAllOrderBy<Tbl18Superclass>("Superclass");

            if (Tbl21ClassesAllList == null)
                Tbl21ClassesAllList ??= new ObservableCollection<Tbl21Class>();
            else
                Tbl21ClassesAllList.Clear();
            Tbl21ClassesAllList = _extCrud.GetCollectionAllOrderBy<Tbl21Class>("Class");

            if (Tbl24SubclassesAllList == null)
                Tbl24SubclassesAllList ??= new ObservableCollection<Tbl24Subclass>();
            else
                Tbl24SubclassesAllList.Clear();
            Tbl24SubclassesAllList = _extCrud.GetCollectionAllOrderBy<Tbl24Subclass>("Subclass");

            if (Tbl27InfraclassesAllList == null)
                Tbl27InfraclassesAllList ??= new ObservableCollection<Tbl27Infraclass>();
            else
                Tbl27InfraclassesAllList.Clear();
            Tbl27InfraclassesAllList = _extCrud.GetCollectionAllOrderBy<Tbl27Infraclass>("Infraclass");

            if (Tbl30LegiosAllList == null)
                Tbl30LegiosAllList ??= new ObservableCollection<Tbl30Legio>();
            else
                Tbl30LegiosAllList.Clear();
            Tbl30LegiosAllList = _extCrud.GetCollectionAllOrderBy<Tbl30Legio>("Legio");

            if (Tbl33OrdosAllList == null)
                Tbl33OrdosAllList ??= new ObservableCollection<Tbl33Ordo>();
            else
                Tbl33OrdosAllList.Clear();
            Tbl33OrdosAllList = _extCrud.GetCollectionAllOrderBy<Tbl33Ordo>("Ordo");

            if (Tbl36SubordosAllList == null)
                Tbl36SubordosAllList ??= new ObservableCollection<Tbl36Subordo>();
            else
                Tbl36SubordosAllList.Clear();
            Tbl36SubordosAllList = _extCrud.GetCollectionAllOrderBy<Tbl36Subordo>("Subordo");

            if (Tbl39InfraordosAllList == null)
                Tbl39InfraordosAllList ??= new ObservableCollection<Tbl39Infraordo>();
            else
                Tbl39InfraordosAllList.Clear();
            Tbl39InfraordosAllList = _extCrud.GetCollectionAllOrderBy<Tbl39Infraordo>("Infraordo");

            if (Tbl42SuperfamiliesAllList == null)
                Tbl42SuperfamiliesAllList ??= new ObservableCollection<Tbl42Superfamily>();
            else
                Tbl42SuperfamiliesAllList.Clear();
            Tbl42SuperfamiliesAllList = _extCrud.GetCollectionAllOrderBy<Tbl42Superfamily>("Superfamily");

            if (Tbl45FamiliesAllList == null)
                Tbl45FamiliesAllList ??= new ObservableCollection<Tbl45Family>();
            else
                Tbl45FamiliesAllList.Clear();
            Tbl45FamiliesAllList = _extCrud.GetCollectionAllOrderBy<Tbl45Family>("Family");

            if (Tbl48SubfamiliesAllList == null)
                Tbl48SubfamiliesAllList ??= new ObservableCollection<Tbl48Subfamily>();
            else
                Tbl48SubfamiliesAllList.Clear();
            Tbl48SubfamiliesAllList = _extCrud.GetCollectionAllOrderBy<Tbl48Subfamily>("Subfamily");

            if (Tbl51InfrafamiliesAllList == null)
                Tbl51InfrafamiliesAllList ??= new ObservableCollection<Tbl51Infrafamily>();
            else
                Tbl51InfrafamiliesAllList.Clear();
            Tbl51InfrafamiliesAllList = _extCrud.GetCollectionAllOrderBy<Tbl51Infrafamily>("Infrafamily");

            if (Tbl54SupertribussesAllList == null)
                Tbl54SupertribussesAllList ??= new ObservableCollection<Tbl54Supertribus>();
            else
                Tbl54SupertribussesAllList.Clear();
            Tbl54SupertribussesAllList = _extCrud.GetCollectionAllOrderBy<Tbl54Supertribus>("Supertribus");

            if (Tbl57TribussesAllList == null)
                Tbl57TribussesAllList ??= new ObservableCollection<Tbl57Tribus>();
            else
                Tbl57TribussesAllList.Clear();
            Tbl57TribussesAllList = _extCrud.GetCollectionAllOrderBy<Tbl57Tribus>("Tribus");

            if (Tbl60SubtribussesAllList == null)
                Tbl60SubtribussesAllList ??= new ObservableCollection<Tbl60Subtribus>();
            else
                Tbl60SubtribussesAllList.Clear();
            Tbl60SubtribussesAllList = _extCrud.GetCollectionAllOrderBy<Tbl60Subtribus>("Subtribus");

            if (Tbl63InfratribussesAllList == null)
                Tbl63InfratribussesAllList ??= new ObservableCollection<Tbl63Infratribus>();
            else
                Tbl63InfratribussesAllList.Clear();
            Tbl63InfratribussesAllList = _extCrud.GetCollectionAllOrderBy<Tbl63Infratribus>("Infratribus");

            if (Tbl66GenussesAllList == null)
                Tbl66GenussesAllList ??= new ObservableCollection<Tbl66Genus>();
            else
                Tbl66GenussesAllList.Clear();
            Tbl66GenussesAllList = _extCrud.GetCollectionAllOrderBy<Tbl66Genus>("Genus");

            if (Tbl69FiSpeciessesAllList == null)
                Tbl69FiSpeciessesAllList ??= new ObservableCollection<Tbl69FiSpecies>();
            else
                Tbl69FiSpeciessesAllList.Clear();
            Tbl69FiSpeciessesAllList = _extCrud.GetCollectionAllOrderBy<Tbl69FiSpecies>("FiSpecies");

            if (Tbl72PlSpeciessesAllList == null)
                Tbl72PlSpeciessesAllList ??= new ObservableCollection<Tbl72PlSpecies>();
            else
                Tbl72PlSpeciessesAllList.Clear();
            Tbl72PlSpeciessesAllList = _extCrud.GetCollectionAllOrderBy<Tbl72PlSpecies>("PlSpecies");


         }

        #endregion



        //    Part 10    



        //    Part 11    


        #region "Public Properties Tbl93Comment"

        private string _searchCommentInfo = "";
        public string SearchCommentInfo
        {
            get => _searchCommentInfo;
            set { _searchCommentInfo = value; RaisePropertyChanged(""); }
        }

        public ICollectionView CommentsView;
        public Tbl93Comment CurrentTbl93Comment => CommentsView?.CurrentItem as Tbl93Comment;

        private ObservableCollection<Tbl93Comment> _tbl93CommentsList;
        public ObservableCollection<Tbl93Comment> Tbl93CommentsList
        {
            get => _tbl93CommentsList;
            set { _tbl93CommentsList = value; RaisePropertyChanged(""); }
        }

        private ObservableCollection<Tbl93Comment> _tbl93CommentsAllList;
        public ObservableCollection<Tbl93Comment> Tbl93CommentsAllList
        {
            get => _tbl93CommentsAllList;
            set { _tbl93CommentsAllList = value; RaisePropertyChanged(""); }
        }

        private ObservableCollection<Tbl03Regnum> _tbl03RegnumsAllList;
        public ObservableCollection<Tbl03Regnum> Tbl03RegnumsAllList
        {
            get => _tbl03RegnumsAllList;
            set { _tbl03RegnumsAllList = value; RaisePropertyChanged(""); }
        }

        private ObservableCollection<Tbl06Phylum> _tbl06PhylumsAllList;
        public ObservableCollection<Tbl06Phylum> Tbl06PhylumsAllList
        {
            get => _tbl06PhylumsAllList;
            set { _tbl06PhylumsAllList = value; RaisePropertyChanged(""); }
        }

        private ObservableCollection<Tbl09Division> _tbl09DivisionsAllList;
        public ObservableCollection<Tbl09Division> Tbl09DivisionsAllList
        {
            get => _tbl09DivisionsAllList;
            set { _tbl09DivisionsAllList = value; RaisePropertyChanged(""); }
        }

        private ObservableCollection<Tbl12Subphylum> _tbl12SubphylumsAllList;
        public ObservableCollection<Tbl12Subphylum> Tbl12SubphylumsAllList
        {
            get => _tbl12SubphylumsAllList;
            set { _tbl12SubphylumsAllList = value; RaisePropertyChanged(""); }
        }

        private ObservableCollection<Tbl15Subdivision> _tbl15SubdivisionsAllList;
        public ObservableCollection<Tbl15Subdivision> Tbl15SubdivisionsAllList
        {
            get => _tbl15SubdivisionsAllList;
            set { _tbl15SubdivisionsAllList = value; RaisePropertyChanged(""); }
        }

        private ObservableCollection<Tbl18Superclass> _tbl18SuperclassesAllList;
        public ObservableCollection<Tbl18Superclass> Tbl18SuperclassesAllList
        {
            get => _tbl18SuperclassesAllList;
            set { _tbl18SuperclassesAllList = value; RaisePropertyChanged(""); }
        }

        private ObservableCollection<Tbl21Class> _tbl21ClassesAllList;
        public ObservableCollection<Tbl21Class> Tbl21ClassesAllList
        {
            get => _tbl21ClassesAllList;
            set { _tbl21ClassesAllList = value; RaisePropertyChanged(""); }
        }

        private ObservableCollection<Tbl24Subclass> _tbl24SubclassesAllList;
        public ObservableCollection<Tbl24Subclass> Tbl24SubclassesAllList
        {
            get => _tbl24SubclassesAllList;
            set { _tbl24SubclassesAllList = value; RaisePropertyChanged(""); }
        }

        private ObservableCollection<Tbl27Infraclass> _tbl27InfraclassesAllList;
        public ObservableCollection<Tbl27Infraclass> Tbl27InfraclassesAllList
        {
            get => _tbl27InfraclassesAllList;
            set { _tbl27InfraclassesAllList = value; RaisePropertyChanged(""); }
        }

        private ObservableCollection<Tbl30Legio> _tbl30LegiosAllList;
        public ObservableCollection<Tbl30Legio> Tbl30LegiosAllList
        {
            get => _tbl30LegiosAllList;
            set { _tbl30LegiosAllList = value; RaisePropertyChanged(""); }
        }

        private ObservableCollection<Tbl33Ordo> _tbl33OrdosAllList;
        public ObservableCollection<Tbl33Ordo> Tbl33OrdosAllList
        {
            get => _tbl33OrdosAllList;
            set { _tbl33OrdosAllList = value; RaisePropertyChanged(""); }
        }

        private ObservableCollection<Tbl36Subordo> _tbl36SubordosAllList;
        public ObservableCollection<Tbl36Subordo> Tbl36SubordosAllList
        {
            get => _tbl36SubordosAllList;
            set { _tbl36SubordosAllList = value; RaisePropertyChanged(""); }
        }

        private ObservableCollection<Tbl39Infraordo> _tbl39InfraordosAllList;
        public ObservableCollection<Tbl39Infraordo> Tbl39InfraordosAllList
        {
            get => _tbl39InfraordosAllList;
            set { _tbl39InfraordosAllList = value; RaisePropertyChanged(""); }
        }

        private ObservableCollection<Tbl42Superfamily> _tbl42SuperfamiliesAllList;
        public ObservableCollection<Tbl42Superfamily> Tbl42SuperfamiliesAllList
        {
            get => _tbl42SuperfamiliesAllList;
            set { _tbl42SuperfamiliesAllList = value; RaisePropertyChanged(""); }
        }

        private ObservableCollection<Tbl45Family> _tbl45FamiliesAllList;
        public ObservableCollection<Tbl45Family> Tbl45FamiliesAllList
        {
            get => _tbl45FamiliesAllList;
            set { _tbl45FamiliesAllList = value; RaisePropertyChanged(""); }
        }

        private ObservableCollection<Tbl48Subfamily> _tbl48SubfamiliesAllList;
        public ObservableCollection<Tbl48Subfamily> Tbl48SubfamiliesAllList
        {
            get => _tbl48SubfamiliesAllList;
            set { _tbl48SubfamiliesAllList = value; RaisePropertyChanged(""); }
        }

        private ObservableCollection<Tbl51Infrafamily> _tbl51InfrafamiliesAllList;
        public ObservableCollection<Tbl51Infrafamily> Tbl51InfrafamiliesAllList
        {
            get => _tbl51InfrafamiliesAllList;
            set { _tbl51InfrafamiliesAllList = value; RaisePropertyChanged(""); }
        }

        private ObservableCollection<Tbl54Supertribus> _tbl54SupertribussesAllList;
        public ObservableCollection<Tbl54Supertribus> Tbl54SupertribussesAllList
        {
            get => _tbl54SupertribussesAllList;
            set { _tbl54SupertribussesAllList = value; RaisePropertyChanged(""); }
        }

        private ObservableCollection<Tbl57Tribus> _tbl57TribussesAllList;
        public ObservableCollection<Tbl57Tribus> Tbl57TribussesAllList
        {
            get => _tbl57TribussesAllList;
            set { _tbl57TribussesAllList = value; RaisePropertyChanged(""); }
        }

        private ObservableCollection<Tbl60Subtribus> _tbl60SubtribussesAllList;
        public ObservableCollection<Tbl60Subtribus> Tbl60SubtribussesAllList
        {
            get => _tbl60SubtribussesAllList;
            set { _tbl60SubtribussesAllList = value; RaisePropertyChanged(""); }
        }

        private ObservableCollection<Tbl63Infratribus> _tbl63InfratribussesAllList;
        public ObservableCollection<Tbl63Infratribus> Tbl63InfratribussesAllList
        {
            get => _tbl63InfratribussesAllList;
            set { _tbl63InfratribussesAllList = value; RaisePropertyChanged(""); }
        }

        private ObservableCollection<Tbl66Genus> _tbl66GenussesAllList;
        public ObservableCollection<Tbl66Genus> Tbl66GenussesAllList
        {
            get => _tbl66GenussesAllList;
            set { _tbl66GenussesAllList = value; RaisePropertyChanged(""); }
        }

        private ObservableCollection<Tbl69FiSpecies> _tbl69FiSpeciessesAllList;
        public ObservableCollection<Tbl69FiSpecies> Tbl69FiSpeciessesAllList
        {
            get => _tbl69FiSpeciessesAllList;
            set { _tbl69FiSpeciessesAllList = value; RaisePropertyChanged(""); }
        }

        private ObservableCollection<Tbl72PlSpecies> _tbl72PlSpeciessesAllList;
        public ObservableCollection<Tbl72PlSpecies> Tbl72PlSpeciessesAllList
        {
            get => _tbl72PlSpeciessesAllList;
            set { _tbl72PlSpeciessesAllList = value; RaisePropertyChanged(""); }
        }

        #endregion "Public Properties"     


    }
}
