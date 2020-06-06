using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using ATIS.DAL.Models;
using ATIS.Ui.Core;
using ATIS.Ui.Helper;

namespace ATIS.Ui.Views.Database.D03Regnum
{
    public class RegnumsViewModel1 : ViewModelBase
    {

        #region "Private Data Members"
        private readonly AtisDbContext _context = new AtisDbContext();
        private int _position;

        #endregion "Private Data Members"               

        #region "Constructor"

        public override string Name => "Regnum1";

        public RegnumsViewModel1()
        {
        }
        #endregion "Constructor"         


        #region "Public Commands Basic Tbl03Regnum"
        //-------------------------------------------------------------------------
        //private RelayCommand _clearRegnumCommand;

        //public ICommand ClearRegnumCommand => _clearRegnumCommand ??= new RelayCommand(delegate { ClearRegnum(null); });

        private RelayCommand _getRegnumsByNameOrIdCommand;

        public ICommand GetRegnumsByNameOrIdCommand => _getRegnumsByNameOrIdCommand ??= new RelayCommand(delegate { GetRegnumsByNameOrId(null); });

        private RelayCommand _addRegnumCommand;

        public ICommand AddRegnumCommand => _addRegnumCommand ??= new RelayCommand(delegate { AddRegnum(null); });

        //-------------------------------------------------------------------------          

        private void ClearRegnum(object o)
        {
            SearchRegnumName = "";

            SelectedMainTabIndex = 0;  //change tab
            SelectedDetailTabIndex = 0;
            SelectedDetailSubTabIndex = 0;
            SelectedDetailSubRefTabIndex = 0;

            RegnumsList?.Clear();
            PhylumsList?.Clear();
            ReferenceExpertsList?.Clear();
            ReferenceSourcesList?.Clear();
            ReferenceAuthorsList?.Clear();
            CommentsList?.Clear();
        }
        //----------------------------------------------------------------------                  

        private void GetRegnumsByNameOrId(object o)
        {
            if (SearchRegnumName != "")
            {
                RegnumsList?.Clear();
                if (SearchRegnumName == "*") // show whole table
                {
                    RegnumsList = new ObservableCollection<Tbl03Regnum>(_context.Tbl03Regnums.ToList());
                }
                else
                {
                    RegnumsList = int.TryParse(SearchRegnumName, out var id)
                        ? new ObservableCollection<Tbl03Regnum>(_context.Tbl03Regnums
                            .Where(e => e.RegnumId == id))
                        : new ObservableCollection<Tbl03Regnum>(_context.Tbl03Regnums
                            .Where(e => e.RegnumName.StartsWith(SearchRegnumName))
                            .OrderBy(a => a.RegnumName + a.Subregnum)
                        );
                }

                if (RegnumsList.Count == 0)
                {
                    MessageBox.Show("Tables", "DatasetNot",
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                }
                else
                {
                    PhylumsList?.Clear();
                    ReferenceExpertsList?.Clear();
                    ReferenceSourcesList?.Clear();
                    ReferenceAuthorsList?.Clear();
                    CommentsList?.Clear();
                }
            }
            else
            {
                RegnumsList = new ObservableCollection<Tbl03Regnum>();

                MessageBox.Show("SearchNameOrId", "InputRequested",
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
            }
            RegnumsView = CollectionViewSource.GetDefaultView(RegnumsList);
            RegnumsView.Refresh();
        }
        //------------------------------------------------------------------------------------                           

        private void AddRegnum(object o)
        {
            if (RegnumsList == null)
                RegnumsList = new ObservableCollection<Tbl03Regnum>();

            RegnumsList.Insert(0, new Tbl03Regnum { RegnumName = "DatasetNew" });

            RegnumsView = CollectionViewSource.GetDefaultView(RegnumsList);
            RegnumsView.MoveCurrentToFirst();
        }
        #endregion "Public Commands"                  


        #region "Public Commands Connect ==> Tbl06Phylum"                 
        //-------------------------------------------------------------------------
        private RelayCommand _addPhylumCommand;

        public ICommand AddPhylumCommand => _addPhylumCommand ??= new RelayCommand(delegate { AddPhylum(null); });


        //-------------------------------------------------------------------------          

        private void AddPhylum(object o)
        {
            if (PhylumsList == null)
                PhylumsList = new ObservableCollection<Tbl06Phylum>();

            PhylumsList.Insert(0, new Tbl06Phylum { PhylumName = "DatasetNew" });

            RegnumsAllList = new ObservableCollection<Tbl03Regnum>(_context.Tbl03Regnums.ToList());

            PhylumsView = CollectionViewSource.GetDefaultView(PhylumsList);
            PhylumsView.MoveCurrentToFirst();
        }
        #endregion "Public Commands"                  


        #region "Public Commands Connect ==> Tbl09Division"                 
        //-------------------------------------------------------------------------
        private RelayCommand _addDivisionCommand;

        public ICommand AddDivisionCommand => _addDivisionCommand ??= new RelayCommand(delegate { AddDivision(null); });


        //-------------------------------------------------------------------------          

        private void AddDivision(object o)
        {
            if (DivisionsList == null)
                DivisionsList = new ObservableCollection<Tbl09Division>();

            DivisionsList.Insert(0, new Tbl09Division { DivisionName = "DatasetNew" });

            RegnumsAllList = new ObservableCollection<Tbl03Regnum>(_context.Tbl03Regnums.ToList());

            DivisionsView = CollectionViewSource.GetDefaultView(DivisionsList);
            DivisionsView.MoveCurrentToFirst();
        }
        #endregion "Public Commands"                  


        #region "Public Commands Connect ==> Tbl90ReferenceAuthor"
        //-------------------------------------------------------------------------
        private RelayCommand _addReferenceAuthorCommand;

        public ICommand AddReferenceAuthorCommand => _addReferenceAuthorCommand ??= new RelayCommand(delegate { AddReferenceAuthor(null); });

        //-------------------------------------------------------------------------                    

        public void AddReferenceAuthor(object o)
        {
            if (ReferenceAuthorsList == null)
                ReferenceAuthorsList = new ObservableCollection<Tbl90Reference>();

            ReferenceAuthorsList.Insert(0, new Tbl90Reference { Info = "DatasetNew" });

            AuthorsAllList = new ObservableCollection<Tbl90RefAuthor>(_context.Tbl90RefAuthors.ToList());

            ReferenceAuthorsView = CollectionViewSource.GetDefaultView(ReferenceAuthorsList);
            ReferenceAuthorsView.MoveCurrentToFirst();
        }
        #endregion "Public Commands"                  

        #region "Public Commands Connect ==> Tbl90ReferenceSource" 
        //-------------------------------------------------------------------------
        private RelayCommand _addReferenceSourceCommand;

        public ICommand AddReferenceSourceCommand => _addReferenceSourceCommand ??= new RelayCommand(delegate { AddReferenceSource(null); });


        //-------------------------------------------------------------------------          

        public void AddReferenceSource(object o)
        {
            if (ReferenceSourcesList == null)
                ReferenceSourcesList = new ObservableCollection<Tbl90Reference>();

            ReferenceSourcesList.Insert(0, new Tbl90Reference { Info = "DatasetNew" });

            SourcesAllList = new ObservableCollection<Tbl90RefSource>(_context.Tbl90RefSources.ToList());

            ReferenceSourcesView = CollectionViewSource.GetDefaultView(ReferenceSourcesList);
            ReferenceSourcesView.MoveCurrentToFirst();
        }
        #endregion "Public Commands"                  

        #region "Public Commands Connect ==> Tbl90ReferenceExpert"
        //-------------------------------------------------------------------------

        private RelayCommand _addReferenceExpertCommand;

        public ICommand AddReferenceExpertCommand => _addReferenceExpertCommand ??= new RelayCommand(delegate { AddReferenceExpert(null); });

        //-------------------------------------------------------------------------          

        public void AddReferenceExpert(object o)
        {
            if (ReferenceExpertsList == null)
                ReferenceExpertsList = new ObservableCollection<Tbl90Reference>();

            ReferenceExpertsList.Insert(0, new Tbl90Reference { Info = "DatasetNew" });

            ExpertsAllList = new ObservableCollection<Tbl90RefExpert>(_context.Tbl90RefExperts.ToList());


            ReferenceExpertsView = CollectionViewSource.GetDefaultView(ReferenceExpertsList);
            ReferenceExpertsView.MoveCurrentToFirst();
        }
        #endregion "Public Commands"                  

        #region "Public Commands Connect ==> Tbl93Comment"

        //-------------------------------------------------------------------------
        private RelayCommand _addCommentCommand;

        public ICommand AddCommentCommand => _addCommentCommand ??= new RelayCommand(delegate { AddComment(null); });

        //-------------------------------------------------------------------------          

        public void AddComment(object o)
        {
            if (CommentsList == null)
                CommentsList = new ObservableCollection<Tbl93Comment>();

            CommentsList.Insert(0, new Tbl93Comment { Info = "DatasetNew" });

            CommentsView = CollectionViewSource.GetDefaultView(CommentsList);
            CommentsView.MoveCurrentToFirst();
        }
        #endregion "Public Commands"                  


        #region "Public Commands Connected Tables by DoubleClick"

        private RelayCommand _getConnectedTablesCommand;
        public ICommand GetConnectedTablesCommand => _getConnectedTablesCommand ??= new RelayCommand(delegate { GetConnectedTablesById(null); });

        private void GetConnectedTablesById(object o)
        {
            ReferenceExpertsList?.Clear();
            ReferenceSourcesList?.Clear();
            ReferenceAuthorsList?.Clear();
            CommentsList?.Clear();

            SelectedMainTabIndex = 0;  //change to Connect tab
            SelectedMainSubRefTabIndex = 0;
            SelectedDetailTabIndex = 1;
            SelectedDetailSubTabIndex = 0;
            SelectedDetailSubRefTabIndex = 0;


            PhylumsList = new ObservableCollection<Tbl06Phylum>(_context.Tbl06Phylums
                .Where(e => e.RegnumId == CurrentTbl03Regnum.RegnumId)
                .OrderBy(a => a.PhylumName));


            PhylumsView = CollectionViewSource.GetDefaultView(PhylumsList);
            PhylumsView.Refresh();
        }

        #endregion "Public Commands Connected Tables by DoubleClick"     

        #region "Public Commands to open Detail TabItems"

        private int _selectedMainTabIndex;
        private int _selectedMainSubRefTabIndex;
        private int _selectedDetailTabIndex;
        private int _selectedDetailSubTabIndex;
        private int _selectedDetailSubRefTabIndex;

        public int SelectedMainTabIndex
        {
            get => _selectedMainTabIndex;
            set
            {
                if (value == _selectedMainTabIndex) return;
                _selectedMainTabIndex = value;
                RaisePropertyChanged("SelectedMainTabIndex");
                if (_selectedMainTabIndex == 0)
                    SelectedDetailSubTabIndex = 0;
                if (_selectedMainTabIndex == 1)
                {
                    SelectedDetailTabIndex = 1;
                    SelectedDetailSubTabIndex = 1;
                }
                if (_selectedMainTabIndex == 2)
                {
                    SelectedDetailTabIndex = 1;
                    SelectedDetailSubTabIndex = 2;
                }
                if (_selectedMainTabIndex == 3)
                {
                    SelectedDetailTabIndex = 1;
                    SelectedDetailSubTabIndex = 3;
                }
            }
        }

        public int SelectedMainSubRefTabIndex
        {
            get => _selectedMainSubRefTabIndex;
            set
            {
                if (value == _selectedMainSubRefTabIndex) return;
                _selectedMainSubRefTabIndex = value;
                RaisePropertyChanged("SelectedMainSubRefTabIndex");
                if (_selectedMainSubRefTabIndex == 0)
                    SelectedDetailSubRefTabIndex = 0;
                if (_selectedMainSubRefTabIndex == 1)
                    SelectedDetailSubRefTabIndex = 1;
                if (_selectedMainSubRefTabIndex == 2)
                    SelectedDetailSubRefTabIndex = 2;
            }
        }

        public int SelectedDetailTabIndex
        {
            get => _selectedDetailTabIndex;
            set
            {
                if (value == _selectedDetailTabIndex) return;
                _selectedDetailTabIndex = value;
                RaisePropertyChanged("SelectedDetailTabIndex");
                if (_selectedDetailTabIndex == 0)
                {
                    SelectedDetailSubTabIndex = 0;
                    SelectedMainTabIndex = 0;
                }
                if (_selectedDetailTabIndex == 1)
                    SelectedDetailSubTabIndex = 1;
                if (_selectedDetailTabIndex == 2)
                    SelectedDetailSubTabIndex = 2;
                if (_selectedDetailTabIndex == 3)
                    SelectedDetailSubTabIndex = 3;
            }
        }

        public int SelectedDetailSubTabIndex
        {
            get => _selectedDetailSubTabIndex;
            set
            {
                if (value == _selectedDetailSubTabIndex) return;
                _selectedDetailSubTabIndex = value;
                RaisePropertyChanged("SelectedDetailSubTabIndex");
                if (_selectedDetailSubTabIndex == 0)
                {

                    PhylumsList = new ObservableCollection<Tbl06Phylum>(_context.Tbl06Phylums
                        .Where(e => e.RegnumId == CurrentTbl03Regnum.RegnumId)
                        .OrderBy(a => a.PhylumName));


                    PhylumsView = CollectionViewSource.GetDefaultView(PhylumsList);
                    PhylumsView.Refresh();

                    SelectedMainTabIndex = 0;
                }
                if (_selectedDetailSubTabIndex == 1)
                {

                    DivisionsList = new ObservableCollection<Tbl09Division>(_context.Tbl09Divisions
                        .Where(e => e.RegnumId == CurrentTbl03Regnum.RegnumId)
                        .OrderBy(a => a.DivisionName));


                    DivisionsView = CollectionViewSource.GetDefaultView(DivisionsList);
                    DivisionsView.Refresh();

                    SelectedMainTabIndex = 1;
                }
                if (_selectedDetailSubTabIndex == 2)
                {

                    ExpertsAllList = new ObservableCollection<Tbl90RefExpert>(_context.Tbl90RefExperts.ToList());

                    ReferenceExpertsList = new ObservableCollection<Tbl90Reference>(_context.Tbl90References
                        .Where(e => e.RegnumId == CurrentTbl03Regnum.RegnumId && e.RefSourceId == null && e.RefAuthorId == null));

                    ReferenceExpertsView = CollectionViewSource.GetDefaultView(ReferenceExpertsList);
                    ReferenceExpertsView.Refresh();

                    SelectedMainTabIndex = 2;
                }
                if (_selectedDetailSubTabIndex == 3)
                {

                    CommentsList = new ObservableCollection<Tbl93Comment>(_context.Tbl93Comments
                        .Where(e => e.RegnumId == CurrentTbl03Regnum.RegnumId)
                        .OrderBy(a => a.Info));


                    CommentsView = CollectionViewSource.GetDefaultView(CommentsList);
                    CommentsView.Refresh();

                    SelectedMainTabIndex = 3;
                }
            }
        }

        public int SelectedDetailSubRefTabIndex
        {
            get => _selectedDetailSubRefTabIndex;
            set
            {
                if (value == _selectedDetailSubRefTabIndex) return;
                _selectedDetailSubRefTabIndex = value;
                RaisePropertyChanged("SelectedDetailSubRefTabIndex");
                if (_selectedDetailSubRefTabIndex == 0)
                {

                    ExpertsAllList = new ObservableCollection<Tbl90RefExpert>(_context.Tbl90RefExperts.ToList());

                    ReferenceExpertsList = new ObservableCollection<Tbl90Reference>(_context.Tbl90References
                        .Where(e => e.RegnumId == CurrentTbl03Regnum.RegnumId && e.RefSourceId == null && e.RefAuthorId == null));


                    ReferenceExpertsView = CollectionViewSource.GetDefaultView(ReferenceExpertsList);
                    ReferenceExpertsView.Refresh();

                    SelectedMainSubRefTabIndex = 0;
                }
                if (_selectedDetailSubRefTabIndex == 1)
                {

                    SourcesAllList = new ObservableCollection<Tbl90RefSource>(_context.Tbl90RefSources.ToList());

                    ReferenceSourcesList = new ObservableCollection<Tbl90Reference>(_context.Tbl90References
                        .Where(e => e.RegnumId == CurrentTbl03Regnum.RegnumId && e.RefExpertId == null && e.RefAuthorId == null));


                    ReferenceSourcesView = CollectionViewSource.GetDefaultView(ReferenceSourcesList);
                    ReferenceSourcesView.Refresh();

                    SelectedMainSubRefTabIndex = 1;
                }
                if (_selectedDetailSubRefTabIndex == 2)
                {
                    AuthorsAllList = new ObservableCollection<Tbl90RefAuthor>(_context.Tbl90RefAuthors.ToList());

                    ReferenceAuthorsList = new ObservableCollection<Tbl90Reference>(_context.Tbl90References
                        .Where(e => e.RegnumId == CurrentTbl03Regnum.RegnumId && e.RefExpertId == null && e.RefSourceId == null));


                    if (ReferenceAuthorsList != null)
                        ReferenceAuthorsView = CollectionViewSource.GetDefaultView(ReferenceAuthorsList);
                    ReferenceAuthorsView.Refresh();

                    SelectedMainSubRefTabIndex = 2;
                }
            }
        }

        #endregion "Public Commands to open Detail TabItems"


        #region "Public Properties Tbl03Regnum"

        private string _searchRegnumName = "";
        public string SearchRegnumName
        {
            get => _searchRegnumName;
            set { _searchRegnumName = value; RaisePropertyChanged("SearchRegnumName"); }
        }

        public ICollectionView RegnumsView;
        private Tbl03Regnum CurrentTbl03Regnum => RegnumsView?.CurrentItem as Tbl03Regnum;

        private ObservableCollection<Tbl03Regnum> _tbl03RegnumsList;
        public ObservableCollection<Tbl03Regnum> RegnumsList
        {
            get => _tbl03RegnumsList;
            set { _tbl03RegnumsList = value; RaisePropertyChanged("RegnumsList"); }
        }

        private ObservableCollection<Tbl03Regnum> _tbl03RegnumsAllList;
        public ObservableCollection<Tbl03Regnum> RegnumsAllList
        {
            get => _tbl03RegnumsAllList;
            set { _tbl03RegnumsAllList = value; RaisePropertyChanged("RegnumsAllList"); }
        }

        #endregion "Public Properties"   

        #region "Public Properties Tbl06Phylum"

        public ICollectionView PhylumsView;
        private Tbl06Phylum CurrentTbl06Phylum => PhylumsView?.CurrentItem as Tbl06Phylum;

        private ObservableCollection<Tbl06Phylum> _tbl06PhylumsList;
        public ObservableCollection<Tbl06Phylum> PhylumsList
        {
            get => _tbl06PhylumsList;
            set { _tbl06PhylumsList = value; RaisePropertyChanged("PhylumsList"); }
        }
        #endregion "Public Properties"     

        #region "Public Properties Tbl09Division"

        public ICollectionView DivisionsView;
        private Tbl09Division CurrentTbl09Division => DivisionsView?.CurrentItem as Tbl09Division;

        private ObservableCollection<Tbl09Division> _tbl09DivisionsList;
        public ObservableCollection<Tbl09Division> DivisionsList
        {
            get => _tbl09DivisionsList;
            set { _tbl09DivisionsList = value; RaisePropertyChanged("DivisionsList"); }
        }
        #endregion "Public Properties"     

        #region "Public Properties Tbl12Subphylum"

        private ObservableCollection<Tbl12Subphylum> _tbl12SubphylumsList;
        public ObservableCollection<Tbl12Subphylum> SubphylumsList
        {
            get => _tbl12SubphylumsList;
            set { _tbl12SubphylumsList = value; RaisePropertyChanged("SubphylumsList"); }
        }

        #endregion "Public Properties"     

        #region "Public Properties Tbl15Subdivision"

        private ObservableCollection<Tbl15Subdivision> _tbl15SubdivisionsList;
        public ObservableCollection<Tbl15Subdivision> SubdivisionsList
        {
            get => _tbl15SubdivisionsList;
            set { _tbl15SubdivisionsList = value; RaisePropertyChanged("SubdivisionsList"); }
        }

        #endregion "Public Properties"     

        #region "Public Properties Tbl90Author"

        private ObservableCollection<Tbl90RefAuthor> _tbl90AuthorsAllList;
        public ObservableCollection<Tbl90RefAuthor> AuthorsAllList
        {
            get => _tbl90AuthorsAllList;
            set { _tbl90AuthorsAllList = value; RaisePropertyChanged("AuthorsAllList"); }
        }

        #endregion "Public Properties "

        #region "Public Properties Tbl90Source"

        private ObservableCollection<Tbl90RefSource> _tbl90SourcesAllList;
        public ObservableCollection<Tbl90RefSource> SourcesAllList
        {
            get => _tbl90SourcesAllList;
            set { _tbl90SourcesAllList = value; RaisePropertyChanged("SourcesAllList"); }
        }

        #endregion "Public Properties "

        #region "Public Properties Tbl90Expert"

        private ObservableCollection<Tbl90RefExpert> _tbl90ExpertsAllList;
        public ObservableCollection<Tbl90RefExpert> ExpertsAllList
        {
            get => _tbl90ExpertsAllList;
            set { _tbl90ExpertsAllList = value; RaisePropertyChanged("ExpertsAllList"); }
        }

        #endregion "Public Properties "

        #region "Public Properties Tbl90ReferenceAuthor"

        public ICollectionView ReferenceAuthorsView;
        private Tbl90Reference CurrentTbl90ReferenceAuthor => ReferenceAuthorsView?.CurrentItem as Tbl90Reference;

        private ObservableCollection<Tbl90Reference> _tbl90ReferenceAuthorsList;
        public ObservableCollection<Tbl90Reference> ReferenceAuthorsList
        {
            get => _tbl90ReferenceAuthorsList;
            set { _tbl90ReferenceAuthorsList = value; RaisePropertyChanged("ReferenceAuthorsList"); }
        }

        #endregion "Public Properties"

        #region "Public Properties Tbl90ReferenceSource"

        public ICollectionView ReferenceSourcesView;
        private Tbl90Reference CurrentTbl90ReferenceSource => ReferenceSourcesView?.CurrentItem as Tbl90Reference;

        private ObservableCollection<Tbl90Reference> _tbl90ReferenceSourcesList;
        public ObservableCollection<Tbl90Reference> ReferenceSourcesList
        {
            get => _tbl90ReferenceSourcesList;
            set { _tbl90ReferenceSourcesList = value; RaisePropertyChanged("ReferenceSourcesList"); }
        }

        #endregion "Public Properties"

        #region "Public Properties Tbl90ReferenceExpert"

        public ICollectionView ReferenceExpertsView;
        private Tbl90Reference CurrentTbl90ReferenceExpert => ReferenceExpertsView?.CurrentItem as Tbl90Reference;

        private ObservableCollection<Tbl90Reference> _tbl90ReferenceExpertsList;
        public ObservableCollection<Tbl90Reference> ReferenceExpertsList
        {
            get => _tbl90ReferenceExpertsList;
            set { _tbl90ReferenceExpertsList = value; RaisePropertyChanged("ReferenceExpertsList"); }
        }

        #endregion "Public Properties"   

        #region "Public Properties Tbl93Comment"

        public ICollectionView CommentsView;
        private Tbl93Comment CurrentTbl93Comment => CommentsView?.CurrentItem as Tbl93Comment;

        private ObservableCollection<Tbl93Comment> _tbl93CommentsList;
        public ObservableCollection<Tbl93Comment> CommentsList
        {
            get => _tbl93CommentsList;
            set { _tbl93CommentsList = value; RaisePropertyChanged("ReferenceExpertsList"); }
        }

        #endregion "Public Properties"


    }
}
