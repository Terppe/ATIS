using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using ATIS.Dal.Models;
using ATIS.Ui.Core;
using ATIS.Ui.Helper;
using log4net;

//    RefAuthorsViewModel Skriptdatum:  07.02.2021  10:32    

namespace ATIS.Ui.Views.Database.D90RefAuthor
{

    public class RefAuthorsViewModel : ViewModelBase
    {
        // Version with Generic Unit Of Work and AtisDbContext for general use   

        #region [Private Data Members]
        private static readonly ILog Log = LogManager.GetLogger(typeof(RefAuthorsViewModel));
        private readonly CrudFunctions _extCrud = new CrudFunctions();
        private readonly DeleteFunctions _extDelete = new DeleteFunctions();
        private readonly SaveFunctions _extSave = new SaveFunctions();
        private readonly AllMessageBoxes _allMessageBoxes = new AllMessageBoxes();
        private int _position;

        #endregion [Private Data Members]               

        #region [Constructor]

        public RefAuthorsViewModel()
        {
            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.
            }
            else
            {

                // Code runs "for real" 
                Tbl90RefAuthorsList = new ObservableCollection<Tbl90RefAuthor>();
            }
        }
        public bool IsInDesignMode { get; set; }

        #endregion [Constructor]          


        //    Part 1    



        #region [Commands RefAuthor]

        private RelayCommand _getRefAuthorsByNameOrIdCommand;
        public ICommand GetRefAuthorsByNameOrIdCommand => _getRefAuthorsByNameOrIdCommand ??= new RelayCommand(delegate { ExecuteGetRefAuthorsByNameOrId(SearchRefAuthorName); });

        private RelayCommand _addRefAuthorCommand;
        public ICommand AddRefAuthorCommand => _addRefAuthorCommand ??= new RelayCommand(delegate { ExecuteAddRefAuthor(null); });

        private RelayCommand _copyRefAuthorCommand;
        public ICommand CopyRefAuthorCommand => _copyRefAuthorCommand ??= new RelayCommand(delegate { ExecuteCopyRefAuthor(null); });

        private RelayCommand _deleteRefAuthorCommand;
        public ICommand DeleteRefAuthorCommand => _deleteRefAuthorCommand ??= new RelayCommand(delegate { ExecuteDeleteRefAuthor(SearchRefAuthorName); });

        private RelayCommand _saveRefAuthorCommand;
        public ICommand SaveRefAuthorCommand => _saveRefAuthorCommand ??= new RelayCommand(delegate { ExecuteSaveRefAuthor(SearchRefAuthorName); });

        #endregion [Commands RefAuthor]       


        #region [Methods RefAuthor]

        private void ExecuteGetRefAuthorsByNameOrId(string searchName)
        {
            if (Tbl90RefAuthorsList == null)
                Tbl90RefAuthorsList ??= new ObservableCollection<Tbl90RefAuthor>();
            else
                Tbl90RefAuthorsList.Clear();

            Tbl90RefAuthorsList = _extCrud.GetRefAuthorsCollectionFromSearchNameOrIdOrderBy<Tbl90RefAuthor>(searchName);

            if (_allMessageBoxes.NoDatasetFoundInfoMessageBox(Tbl90RefAuthorsList.Count)) return;

            RefAuthorsView = CollectionViewSource.GetDefaultView(Tbl90RefAuthorsList);
            RefAuthorsView.Refresh();
        }

        private void ExecuteAddRefAuthor(object o)
        {
            if (Tbl90RefAuthorsList == null)
                Tbl90RefAuthorsList ??= new ObservableCollection<Tbl90RefAuthor>();
            else
                Tbl90RefAuthorsList.Clear();

            Tbl90RefAuthorsList.Insert(0, new Tbl90RefAuthor { RefAuthorName = CultRes.StringsRes.DatasetNew });

            RefAuthorsView = CollectionViewSource.GetDefaultView(Tbl90RefAuthorsList);
            RefAuthorsView.MoveCurrentToFirst();
        }

        private void ExecuteCopyRefAuthor(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90RefAuthor)) return;

            Tbl90RefAuthorsList = _extCrud.CopyRefAuthor(CurrentTbl90RefAuthor);

            RefAuthorsView = CollectionViewSource.GetDefaultView(Tbl90RefAuthorsList);
            RefAuthorsView.MoveCurrentToFirst();
        }

        private void ExecuteDeleteRefAuthor(string searchName)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90RefAuthor)) return;

            _extDelete.DeleteRefAuthor(CurrentTbl90RefAuthor);

            Tbl90RefAuthorsList = _extCrud.GetCollectionFromSearchNameOrIdOrderBy<Tbl90RefAuthor>(searchName, "RefAuthor");
            RefAuthorsView = CollectionViewSource.GetDefaultView(Tbl90RefAuthorsList);
            RefAuthorsView.MoveCurrentToLast();
        }

        private void ExecuteSaveRefAuthor(string searchName)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90RefAuthor)) return;

            _position = RefAuthorsView.CurrentPosition;

            var ret = _extSave.SaveRefAuthor(CurrentTbl90RefAuthor);

            if (ret != true)
            {
                RefAuthorsView = CollectionViewSource.GetDefaultView(Tbl90RefAuthorsList);
                RefAuthorsView.Refresh();
                return;
            }

            if (_position == 0) //new
            {
                Tbl90RefAuthorsList = _extCrud.GetLastRefAuthorsDatasetOrderById();
                RefAuthorsView = CollectionViewSource.GetDefaultView(Tbl90RefAuthorsList);
                RefAuthorsView.MoveCurrentToFirst();
            }
            else
            {
                Tbl90RefAuthorsList = _extCrud.GetRefAuthorsCollectionFromSearchNameOrIdOrderBy<Tbl90RefAuthor>(searchName);
                RefAuthorsView = CollectionViewSource.GetDefaultView(Tbl90RefAuthorsList);
                RefAuthorsView.MoveCurrentToPosition(_position);
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



        //    Part 11    


        #region "Public Properties Tbl90RefAuthor"

        private string _searchRefAuthorName = "";
        public string SearchRefAuthorName
        {
            get => _searchRefAuthorName;
            set { _searchRefAuthorName = value; RaisePropertyChanged(""); }
        }

        public ICollectionView RefAuthorsView;
        private Tbl90RefAuthor CurrentTbl90RefAuthor => RefAuthorsView?.CurrentItem as Tbl90RefAuthor;

        private ObservableCollection<Tbl90RefAuthor> _tbl90RefAuthorsList;
        public ObservableCollection<Tbl90RefAuthor> Tbl90RefAuthorsList
        {
            get => _tbl90RefAuthorsList;
            set { _tbl90RefAuthorsList = value; RaisePropertyChanged(""); }
        }

        private ObservableCollection<Tbl90RefAuthor> _tbl90RefAuthorsAllList;
        public ObservableCollection<Tbl90RefAuthor> Tbl90RefAuthorsAllList
        {
            get => _tbl90RefAuthorsAllList;
            set { _tbl90RefAuthorsAllList = value; RaisePropertyChanged(""); }
        }


        #endregion "Public Properties"   


    }
}
