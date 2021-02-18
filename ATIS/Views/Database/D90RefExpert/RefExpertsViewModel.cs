using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using ATIS.Ui.Models;
using ATIS.Ui.Core;
using ATIS.Ui.Helper;

//    RefExpertsViewModel Skriptdatum:  09.02.2021  10:32    

namespace ATIS.Ui.Views.Database.D90RefExpert
{

    public class RefExpertsViewModel : ViewModelBase
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

        public RefExpertsViewModel()
        {
            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.
            }
            else
            {

                // Code runs "for real" 
                Tbl90RefExpertsList = new ObservableCollection<Tbl90RefExpert>();
            }
        }
        public bool IsInDesignMode { get; set; }

        #endregion [Constructor]          


        //    Part 1    



        #region [Commands RefExpert]

        private RelayCommand _getRefExpertsByNameOrIdCommand;
        public ICommand GetRefExpertsByNameOrIdCommand => _getRefExpertsByNameOrIdCommand ??= new RelayCommand(delegate { ExecuteGetRefExpertsByNameOrId(SearchRefExpertName); });

        private RelayCommand _addRefExpertCommand;
        public ICommand AddRefExpertCommand => _addRefExpertCommand ??= new RelayCommand(delegate { ExecuteAddRefExpert(null); });

        private RelayCommand _copyRefExpertCommand;
        public ICommand CopyRefExpertCommand => _copyRefExpertCommand ??= new RelayCommand(delegate { ExecuteCopyRefExpert(null); });

        private RelayCommand _deleteRefExpertCommand;
        public ICommand DeleteRefExpertCommand => _deleteRefExpertCommand ??= new RelayCommand(delegate { ExecuteDeleteRefExpert(SearchRefExpertName); });

        private RelayCommand _saveRefExpertCommand;
        public ICommand SaveRefExpertCommand => _saveRefExpertCommand ??= new RelayCommand(delegate { ExecuteSaveRefExpert(SearchRefExpertName); });

        #endregion [Commands RefExpert]       


        #region [Methods RefExpert]

        private void ExecuteGetRefExpertsByNameOrId(string searchName)
        {
            if (Tbl90RefExpertsList == null)
                Tbl90RefExpertsList ??= new ObservableCollection<Tbl90RefExpert>();
            else
                Tbl90RefExpertsList.Clear();

            Tbl90RefExpertsList = _extCrud.GetRefExpertsCollectionFromSearchNameOrIdOrderBy<Tbl90RefExpert>(searchName);

            if (_allMessageBoxes.NoDatasetFoundInfoMessageBox(Tbl90RefExpertsList.Count)) return;

            RefExpertsView = CollectionViewSource.GetDefaultView(Tbl90RefExpertsList);
            RefExpertsView.Refresh();
        }

        private void ExecuteAddRefExpert(object o)
        {
            if (Tbl90RefExpertsList == null)
                Tbl90RefExpertsList ??= new ObservableCollection<Tbl90RefExpert>();
            else
                Tbl90RefExpertsList.Clear();

            Tbl90RefExpertsList.Insert(0, new Tbl90RefExpert { RefExpertName = CultRes.StringsRes.DatasetNew });

            RefExpertsView = CollectionViewSource.GetDefaultView(Tbl90RefExpertsList);
            RefExpertsView.MoveCurrentToFirst();
        }

        private void ExecuteCopyRefExpert(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90RefExpert)) return;

            Tbl90RefExpertsList = _extCrud.CopyRefExpert(CurrentTbl90RefExpert);

            RefExpertsView = CollectionViewSource.GetDefaultView(Tbl90RefExpertsList);
            RefExpertsView.MoveCurrentToFirst();
        }

        private void ExecuteDeleteRefExpert(string searchName)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90RefExpert)) return;

            _extDelete.DeleteRefExpert(CurrentTbl90RefExpert);

            Tbl90RefExpertsList = _extCrud.GetCollectionFromSearchNameOrIdOrderBy<Tbl90RefExpert>(searchName, "RefExpert");
            RefExpertsView = CollectionViewSource.GetDefaultView(Tbl90RefExpertsList);
            RefExpertsView.MoveCurrentToLast();
        }

        private void ExecuteSaveRefExpert(string searchName)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90RefExpert)) return;

            _position = RefExpertsView.CurrentPosition;

            var ret = _extSave.SaveRefExpert(CurrentTbl90RefExpert);

            if (ret != true)
            {
                RefExpertsView = CollectionViewSource.GetDefaultView(Tbl90RefExpertsList);
                RefExpertsView.Refresh();
                return;
            }

            if (_position == 0) //new
            {
                Tbl90RefExpertsList = _extCrud.GetLastRefExpertsDatasetOrderById();
                RefExpertsView = CollectionViewSource.GetDefaultView(Tbl90RefExpertsList);
                RefExpertsView.MoveCurrentToFirst();
            }
            else
            {
                Tbl90RefExpertsList = _extCrud.GetRefExpertsCollectionFromSearchNameOrIdOrderBy<Tbl90RefExpert>(searchName);
                RefExpertsView = CollectionViewSource.GetDefaultView(Tbl90RefExpertsList);
                RefExpertsView.MoveCurrentToPosition(_position);
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



        //    Part 10    



        //    Part 11    


        #region "Public Properties Tbl90RefExpert"

        private string _searchRefExpertName = "";
        public string SearchRefExpertName
        {
            get => _searchRefExpertName;
            set { _searchRefExpertName = value; RaisePropertyChanged(""); }
        }

        public ICollectionView RefExpertsView;
        private Tbl90RefExpert CurrentTbl90RefExpert => RefExpertsView?.CurrentItem as Tbl90RefExpert;

        private ObservableCollection<Tbl90RefExpert> _tbl90RefExpertsList;
        public ObservableCollection<Tbl90RefExpert> Tbl90RefExpertsList
        {
            get => _tbl90RefExpertsList;
            set { _tbl90RefExpertsList = value; RaisePropertyChanged(""); }
        }

        private ObservableCollection<Tbl90RefExpert> _tbl90RefExpertsAllList;
        public ObservableCollection<Tbl90RefExpert> Tbl90RefExpertsAllList
        {
            get => _tbl90RefExpertsAllList;
            set { _tbl90RefExpertsAllList = value; RaisePropertyChanged(""); }
        }


        #endregion "Public Properties"   


    }
}
