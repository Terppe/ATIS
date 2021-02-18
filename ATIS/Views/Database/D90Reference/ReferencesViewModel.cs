using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;


using System.Windows.Data;
using System.Windows.Input;
using ATIS.Ui.Models;
using ATIS.Ui.Core;
using ATIS.Ui.Helper;

//    ReferencesViewModel Skriptdatum:  03.02.2021  10:32    

namespace ATIS.Ui.Views.Database.D90Reference
{

    public class ReferencesViewModel : ViewModelBase
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

        public ReferencesViewModel()
        {
            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.
            }
            else
            {

                // Code runs "for real" 
                Tbl90ReferencesList = new ObservableCollection<Tbl90Reference>();
            }
        }
        public bool IsInDesignMode { get; set; }

        #endregion [Constructor]          


        //    Part 1    



        #region [Commands Reference]

        private RelayCommand _getReferencesByNameOrIdCommand;
        public ICommand GetReferencesByNameOrIdCommand => _getReferencesByNameOrIdCommand ??= new RelayCommand(delegate { ExecuteGetReferencesByNameOrId(SearchReferenceInfo); });

        private RelayCommand _addReferenceCommand;
        public ICommand AddReferenceCommand => _addReferenceCommand ??= new RelayCommand(delegate { ExecuteAddReference(null); });

        private RelayCommand _copyReferenceCommand;
        public ICommand CopyReferenceCommand => _copyReferenceCommand ??= new RelayCommand(delegate { ExecuteCopyReference(null); });

        private RelayCommand _deleteReferenceCommand;
        public ICommand DeleteReferenceCommand => _deleteReferenceCommand ??= new RelayCommand(delegate { ExecuteDeleteReference(null); });

        private RelayCommand _saveReferenceCommand;
        public ICommand SaveReferenceCommand => _saveReferenceCommand ??= new RelayCommand(delegate { ExecuteSaveReference(SearchReferenceInfo); });

        #endregion [Commands Reference]       


        #region [Methods Reference]

        private void ExecuteGetReferencesByNameOrId(string searchInfo)
        {
            ConnectedAllLists();

            if (Tbl90ReferencesList == null)
                Tbl90ReferencesList ??= new ObservableCollection<Tbl90Reference>();
            else
                Tbl90ReferencesList.Clear();

            Tbl90ReferencesList = _extCrud.GetReferencesCollectionFromSearchInfoOrIdOrderBy<Tbl90Reference>(searchInfo);

            if (_allMessageBoxes.NoDatasetFoundInfoMessageBox(Tbl90ReferencesList.Count)) return;

            ReferencesView = CollectionViewSource.GetDefaultView(Tbl90ReferencesList);
            ReferencesView.Refresh();
        }
        private void ExecuteAddReference(object o)
        {
            ConnectedAllLists();

            if (Tbl90ReferencesList == null)
                Tbl90ReferencesList ??= new ObservableCollection<Tbl90Reference>();
            else
                Tbl90ReferencesList.Clear();

            Tbl90ReferencesList.Insert(0, new Tbl90Reference { Info = CultRes.StringsRes.DatasetNew });

            ReferencesView = CollectionViewSource.GetDefaultView(Tbl90ReferencesList);
            ReferencesView.MoveCurrentToFirst();
        }
        private void ExecuteCopyReference(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90Reference)) return;

            Tbl90ReferencesList = _extCrud.CopyReference(CurrentTbl90Reference);

            // evtl verbundene tabellen-Datensätze auch kopieren Expert, Source, Author und Comment

            ReferencesView = CollectionViewSource.GetDefaultView(Tbl90ReferencesList);
            ReferencesView.MoveCurrentToFirst();
        }
        private void ExecuteDeleteReference(string searchName)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90Reference)) return;

            _extDelete.DeleteReference(CurrentTbl90Reference);

            Tbl90ReferencesList = _extCrud.GetCollectionFromSearchNameOrIdOrderBy<Tbl90Reference>(searchName, "Reference");
            ReferencesView = CollectionViewSource.GetDefaultView(Tbl90ReferencesList);
            ReferencesView.MoveCurrentToLast();
        }
        private void ExecuteSaveReference(string searchName)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90Reference)) return;

            _position = ReferencesView.CurrentPosition;

           var ret = _extSave.SaveReference(CurrentTbl90Reference);

           if (ret != true)
           {
               ReferencesView = CollectionViewSource.GetDefaultView(Tbl90ReferencesList);
               ReferencesView.Refresh();
               return;
           }

            if (CurrentTbl90Reference.ReferenceId == 0) //new
            {
                Tbl90ReferencesList = _extCrud.GetLastReferencesDatasetOrderById();
                ReferencesView = CollectionViewSource.GetDefaultView(Tbl90ReferencesList);
                ReferencesView.MoveCurrentToFirst();
            }
            else
            {
                Tbl90ReferencesList = _extCrud.GetReferencesCollectionFromSearchInfoOrIdOrderBy<Tbl90Reference>(searchName);
                ReferencesView = CollectionViewSource.GetDefaultView(Tbl90ReferencesList);
                ReferencesView.MoveCurrentToPosition(_position);
            }
        }
        #endregion "Public Commands"                   



        //    Part 2    


        #region "Public Commands Connect <== Tbl90RefExpert"                 


        private RelayCommand _saveRefExpertCommand;

        public ICommand SaveRefExpertCommand => _saveRefExpertCommand ??= new RelayCommand(delegate { ExecuteSaveRefExpert(null); });

        private void ExecuteSaveRefExpert(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90RefExpert)) return;

            _extSave.SaveRefExpert(CurrentTbl90RefExpert);

            Tbl90RefExpertsList = _extCrud.GetRefExpertsCollectionFromRefExpertIdOrderBy<Tbl90RefExpert>(CurrentTbl90Reference.RefExpertId);
            RefExpertsView = CollectionViewSource.GetDefaultView(Tbl90RefExpertsList);
            RefExpertsView.Refresh();
        }

        #endregion "Public Commands"                  


        //    Part 3    


        #region "Public Commands Connect <== Tbl90RefSource"                 

        private RelayCommand _saveRefSourceCommand;

        public ICommand SaveRefSourceCommand => _saveRefSourceCommand ??= new RelayCommand(delegate { ExecuteSaveRefSource(null); });

        private void ExecuteSaveRefSource(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90RefSource)) return;

            _extSave.SaveRefSource(CurrentTbl90RefSource);

            Tbl90RefSourcesList = _extCrud.GetRefSourcesCollectionFromRefSourceIdOrderBy<Tbl90RefSource>(CurrentTbl90Reference.RefSourceId);
            RefSourcesView = CollectionViewSource.GetDefaultView(Tbl90RefSourcesList);
            RefSourcesView.Refresh();
        }


        #endregion "Public Commands"                  



        //    Part 4    

        #region "Public Commands Connect <== Tbl90RefAuthor"                 

        private RelayCommand _saveRefAuthorCommand;

        public ICommand SaveRefAuthorCommand => _saveRefAuthorCommand ??= new RelayCommand(delegate { ExecuteSaveRefAuthor(null); });

        private void ExecuteSaveRefAuthor(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90RefAuthor)) return;

            _extSave.SaveRefAuthor(CurrentTbl90RefAuthor);

            Tbl90RefAuthorsList = _extCrud.GetRefAuthorsCollectionFromRefAuthorIdOrderBy<Tbl90RefAuthor>(CurrentTbl90Reference.RefAuthorId);
            RefAuthorsView = CollectionViewSource.GetDefaultView(Tbl90RefAuthorsList);
            RefAuthorsView.Refresh();
        }


        #endregion "Public Commands"                  



        //    Part 5    



        //    Part 6    




        //    Part 7    



        //    Part 8    



        //    Part 9    



        #region "Public Commands Connected Tables by DoubleClick"

        private RelayCommand _getConnectedTablesCommand;
        public ICommand GetConnectedTablesCommand => _getConnectedTablesCommand ??= new RelayCommand(delegate { GetConnectedTablesById(null); });

        #endregion "Public Commands Connected Tables by DoubleClick"

        #region "Public Method Connected Tables by DoubleClick"

        private void GetConnectedTablesById(object o)
        {
            Tbl90RefExpertsList = _extCrud.GetRefExpertsCollectionFromRefExpertIdOrderBy<Tbl90RefExpert>(CurrentTbl90Reference.RefExpertId);

            RefExpertsView = CollectionViewSource.GetDefaultView(Tbl90RefExpertsList);
            RefExpertsView.Refresh();

        }

        #endregion "Public Method Connected Tables by DoubleClick"     



        //    Part 10    


        #region "Public Commands to open Detail TabItems"

        private int _selectedMainTabIndex;
        private int _selectedDetailTabIndex;


        public int SelectedMainTabIndex
        {
            get => _selectedMainTabIndex;
            set
            {
                if (value == _selectedMainTabIndex) return;
                _selectedMainTabIndex = value; RaisePropertyChanged("");

                if (_selectedMainTabIndex == 0)
                {
                    if (CurrentTbl90Reference != null)
                    {
                        Tbl90RefExpertsList = _extCrud.GetRefExpertsCollectionFromRefExpertIdOrderBy<Tbl90RefExpert>(CurrentTbl90Reference.RefExpertId);
                        
                        RefExpertsView = CollectionViewSource.GetDefaultView(Tbl90RefExpertsList);
                        RefExpertsView.Refresh();
                    }
                    SelectedDetailTabIndex = 1;
                }

                if (_selectedMainTabIndex == 1)
                {
                    if (CurrentTbl90Reference != null)
                    {
                        Tbl90RefSourcesList = _extCrud.GetRefSourcesCollectionFromRefSourceIdOrderBy<Tbl90RefSource>(CurrentTbl90Reference.RefSourceId);

                        RefSourcesView = CollectionViewSource.GetDefaultView(Tbl90RefSourcesList);
                        RefSourcesView.Refresh();
                    }
                    SelectedDetailTabIndex = 2;
                }

                if (_selectedMainTabIndex == 2)
                {
                    Tbl90RefAuthorsList = _extCrud.GetRefAuthorsCollectionFromRefAuthorIdOrderBy<Tbl90RefAuthor>(CurrentTbl90Reference.RefAuthorId);

                    RefAuthorsView = CollectionViewSource.GetDefaultView(Tbl90RefAuthorsList);
                    RefAuthorsView.Refresh();

                    SelectedDetailTabIndex = 3;
                }

 
            }
        }

        public int SelectedDetailTabIndex
        {
            get => _selectedDetailTabIndex;
            set
            {
                if (value == _selectedDetailTabIndex) return;
                _selectedDetailTabIndex = value; RaisePropertyChanged("");

                if (_selectedDetailTabIndex == 0)
                {
                    SelectedMainTabIndex = 0;
                }

                if (_selectedDetailTabIndex == 1)
                {
                    if (CurrentTbl90Reference != null)
                    {
                        Tbl90RefExpertsList = _extCrud.GetRefExpertsCollectionFromRefExpertIdOrderBy<Tbl90RefExpert>(CurrentTbl90Reference.RefExpertId);

                        RefExpertsView = CollectionViewSource.GetDefaultView(Tbl90RefExpertsList);
                        RefExpertsView.Refresh();
                    }

                    SelectedMainTabIndex = 0;
                }

                if (_selectedDetailTabIndex == 2)
                {
                    if (CurrentTbl90Reference != null)
                    {
                        Tbl90RefSourcesList = _extCrud.GetRefSourcesCollectionFromRefSourceIdOrderBy<Tbl90RefSource>(CurrentTbl90Reference.RefSourceId);

                        ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90RefSourcesList);
                        ReferenceSourcesView.Refresh();
                    }
                    SelectedMainTabIndex = 1;
                }
                if (_selectedDetailTabIndex == 3)
                {
                    if (CurrentTbl90Reference != null)
                    {
                        Tbl90RefAuthorsList = _extCrud.GetRefAuthorsCollectionFromRefAuthorIdOrderBy<Tbl90RefAuthor>(CurrentTbl90Reference.RefAuthorId);

                        RefAuthorsView = CollectionViewSource.GetDefaultView(Tbl90RefAuthorsList);
                        RefAuthorsView.Refresh();
                    }
                    SelectedMainTabIndex = 2;
                }
            }
        }

        #endregion "Public Commands to open Detail TabItems"          

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


            if (Tbl90RefExpertsAllList == null)
                Tbl90RefExpertsAllList ??= new ObservableCollection<Tbl90RefExpert>();
            else
                Tbl90RefExpertsAllList.Clear();
            Tbl90RefExpertsAllList = _extCrud.GetCollectionAllOrderBy<Tbl90RefExpert>("Expert");

            if (Tbl90RefSourcesAllList == null)
                Tbl90RefSourcesAllList ??= new ObservableCollection<Tbl90RefSource>();
            else
                Tbl90RefSourcesAllList.Clear();
            Tbl90RefSourcesAllList = _extCrud.GetCollectionAllOrderBy<Tbl90RefSource>("Source");

            if (Tbl90RefAuthorsAllList == null)
                Tbl90RefAuthorsAllList ??= new ObservableCollection<Tbl90RefAuthor>();
            else
                Tbl90RefAuthorsAllList.Clear();
            Tbl90RefAuthorsAllList = _extCrud.GetCollectionAllOrderBy<Tbl90RefAuthor>("Author");
        }

        #endregion
        //    Part 11    


        #region "Public Properties Tbl90Reference"

        private string _searchReferenceInfo = "";
        public string SearchReferenceInfo
        {
            get => _searchReferenceInfo;
            set { _searchReferenceInfo = value; RaisePropertyChanged(""); }
        }

        public ICollectionView ReferencesView;
        private Tbl90Reference CurrentTbl90Reference => ReferencesView?.CurrentItem as Tbl90Reference;

        private ObservableCollection<Tbl90Reference> _tbl90ReferencesList;
        public ObservableCollection<Tbl90Reference> Tbl90ReferencesList
        {
            get => _tbl90ReferencesList;
            set { _tbl90ReferencesList = value; RaisePropertyChanged(""); }
        }

        private ObservableCollection<Tbl90Reference> _tbl90ReferencesAllList;
        public ObservableCollection<Tbl90Reference> Tbl90ReferencesAllList
        {
            get => _tbl90ReferencesAllList;
            set { _tbl90ReferencesAllList = value; RaisePropertyChanged(""); }
        }

        private ObservableCollection<Tbl90RefExpert> _tbl90RefExpertsAllList;

        public ObservableCollection<Tbl90RefExpert> Tbl90RefExpertsAllList
        {
            get => _tbl90RefExpertsAllList;
            set { _tbl90RefExpertsAllList = value; RaisePropertyChanged(""); }
        }

        private ObservableCollection<Tbl90RefSource> _tbl90RefSourcesAllList;

        public ObservableCollection<Tbl90RefSource> Tbl90RefSourcesAllList
        {
            get => _tbl90RefSourcesAllList;
            set { _tbl90RefSourcesAllList = value; RaisePropertyChanged(""); }
        }

        private ObservableCollection<Tbl90RefAuthor> _tbl90RefAuthorsAllList;

        public ObservableCollection<Tbl90RefAuthor> Tbl90RefAuthorsAllList
        {
            get => _tbl90RefAuthorsAllList;
            set { _tbl90RefAuthorsAllList = value; RaisePropertyChanged(""); }
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

        #region "Public Properties Tbl90RefExpert"

        private string _searchRefExpertName = string.Empty;
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
        #endregion "Public Properties"   


        #region "Public Properties Tbl90RefSource"

        private string _searchRefSourceName = string.Empty;
        public string SearchRefSourceName
        {
            get => _searchRefSourceName;
            set { _searchRefSourceName = value; RaisePropertyChanged(""); }
        }

        public ICollectionView RefSourcesView;
        private Tbl90RefSource CurrentTbl90RefSource => RefSourcesView?.CurrentItem as Tbl90RefSource;

        private ObservableCollection<Tbl90RefSource> _tbl90RefSourcesList;
        public ObservableCollection<Tbl90RefSource> Tbl90RefSourcesList
        {
            get => _tbl90RefSourcesList;
            set { _tbl90RefSourcesList = value; RaisePropertyChanged(""); }
        }
        #endregion "Public Properties"   

        #region "Public Properties Tbl90RefAuthor"

        private string _searchRefAuthorName = string.Empty;
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
        #endregion "Public Properties"     

        #region "Public Properties Tbl90Author"

        private ObservableCollection<Tbl90RefAuthor> _tbl90AuthorsAllList;
        public ObservableCollection<Tbl90RefAuthor> Tbl90AuthorsAllList
        {
            get => _tbl90AuthorsAllList;
            set { _tbl90AuthorsAllList = value; RaisePropertyChanged(""); }
        }

        #endregion "Public Properties "

        #region "Public Properties Tbl90Source"

        private ObservableCollection<Tbl90RefSource> _tbl90SourcesAllList;
        public ObservableCollection<Tbl90RefSource> Tbl90SourcesAllList
        {
            get => _tbl90SourcesAllList;
            set { _tbl90SourcesAllList = value; RaisePropertyChanged(""); }
        }

        #endregion "Public Properties "

        #region "Public Properties Tbl90Expert"

        private ObservableCollection<Tbl90RefExpert> _tbl90ExpertsAllList;
        public ObservableCollection<Tbl90RefExpert> Tbl90ExpertsAllList
        {
            get => _tbl90ExpertsAllList;
            set { _tbl90ExpertsAllList = value; RaisePropertyChanged(""); }
        }

        #endregion "Public Properties "

        #region "Public Properties Tbl90ReferenceAuthor"

        public ICollectionView ReferenceAuthorsView;
        private Tbl90Reference CurrentTbl90ReferenceAuthor => ReferenceAuthorsView?.CurrentItem as Tbl90Reference;

        private ObservableCollection<Tbl90Reference> _tbl90ReferenceAuthorsList;
        public ObservableCollection<Tbl90Reference> Tbl90ReferenceAuthorsList
        {
            get => _tbl90ReferenceAuthorsList;
            set { _tbl90ReferenceAuthorsList = value; RaisePropertyChanged(""); }
        }

        #endregion "Public Properties"

        #region "Public Properties Tbl90ReferenceSource"

        public ICollectionView ReferenceSourcesView;
        private Tbl90Reference CurrentTbl90ReferenceSource => ReferenceSourcesView?.CurrentItem as Tbl90Reference;

        private ObservableCollection<Tbl90Reference> _tbl90ReferenceSourcesList;
        public ObservableCollection<Tbl90Reference> Tbl90ReferenceSourcesList
        {
            get => _tbl90ReferenceSourcesList;
            set { _tbl90ReferenceSourcesList = value; RaisePropertyChanged(""); }
        }

        #endregion "Public Properties"

        #region "Public Properties Tbl90ReferenceExpert"

        public ICollectionView ReferenceExpertsView;
        private Tbl90Reference CurrentTbl90ReferenceExpert => ReferenceExpertsView?.CurrentItem as Tbl90Reference;

        private ObservableCollection<Tbl90Reference> _tbl90ReferenceExpertsList;
        public ObservableCollection<Tbl90Reference> Tbl90ReferenceExpertsList
        {
            get => _tbl90ReferenceExpertsList;
            set { _tbl90ReferenceExpertsList = value; RaisePropertyChanged(""); }
        }

        #endregion "Public Properties"   


    }
}
