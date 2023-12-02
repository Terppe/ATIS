﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATIS.WinUi.Contracts.Services;
using ATIS.WinUi.Helpers;
using ATIS.WinUi.Models;
using ATIS.WinUi.Services;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ATIS.WinUi.ViewModels.Main;

public class Test2ViewModel : ObservableObject
{
    #region [Private Data Members]
    private readonly IDataService _dataService;
    private readonly AtisDbContext _context = new();
    public ObservableCollection<Tbl03Regnum?> RegnumItems { get; } = new();
    public ObservableCollection<Tbl06Phylum> PhylumItems { get; } = new();
    public ObservableCollection<Tbl09Division> DivisionItems { get; } = new();
    public ObservableCollection<Tbl90Reference> ReferenceExpertItems { get; } = new();
    public ObservableCollection<Tbl90Reference> ReferenceSourceItems { get; } = new();
    public ObservableCollection<Tbl90Reference> ReferenceAuthorItems { get; } = new();
    public ObservableCollection<Tbl93Comment> CommentItems { get; } = new();
    private readonly AllDialogs _allDialogs = new();

    #endregion [Private Data Members]


    #region [Constructor]
    public Test2ViewModel(IDataService dataService)
    {
        _dataService = dataService;
    }


    #endregion [Constructor]

    #region "Public Commands to change TabItems"
    private int _selectedFishesTabIndex;
    public int SelectedFishesTabIndex
    {
        get => _selectedFishesTabIndex;
        set
        {
            if (value == _selectedFishesTabIndex)
            {
                return;
            }

            _selectedFishesTabIndex = value; OnPropertyChanged();

            if (_selectedFishesTabIndex == 0)
            {
                //GenussesCollection = _dataService.GetTbl66GenussesCollectionOrderByGenusNameFromFilterText(FilterText);
                //DataSetCount = GenussesCollection.Count;

                //OnPropertyChanged(nameof(GenussesCollection));
                //RefreshGenusItems();
            }

            if (_selectedFishesTabIndex == 1)
            {
                //FiSpeciessesCollection = _dataService.GetTbl69FiSpeciessesCollectionOrderByGenusNameAndFiSpeciesNameAndSubspeciesAndDiversFromFilterText(FilterText);
                //DataSetCount = FiSpeciessesCollection.Count;

                //OnPropertyChanged(nameof(FiSpeciessesCollection));
                //RefreshFiSpeciesItems();
            }

            if (_selectedFishesTabIndex == 2)
            {
                //PlSpeciessesCollection = _dataService.GetTbl72PlSpeciessesCollectionOrderByGenusNameAndPlSpeciesNameAndSubspeciesAndDiversFromFilterText(FilterText);
                //DataSetCount = PlSpeciessesCollection.Count;

                //OnPropertyChanged(nameof(PlSpeciessesCollection));
                //RefreshPlSpeciesItems();
            }

            if (_selectedFishesTabIndex == 3)  //Tribus
            {
                //TribussesCollection = _dataService.GetTbl57TribussesCollectionOrderByTribusNameFromFilterText(FilterText);
                //DataSetCount = TribussesCollection.Count;

                //OnPropertyChanged(nameof(TribussesCollection));
                //RefreshTribusItems();
                //SelectedTribusTabIndex = 2;
            }
            if (_selectedFishesTabIndex == 4)  //Family
            {
                //FamiliesCollection = _dataService.GetTbl45FamiliesCollectionOrderByFamilyNameFromFilterText(FilterText);
                //DataSetCount = FamiliesCollection.Count;

                //OnPropertyChanged(nameof(FamiliesCollection));
                //RefreshFamilyItems();
                //SelectedFamilyTabIndex = 2;
            }
            if (_selectedFishesTabIndex == 5)  //Ordo
            {
                //OrdosCollection = _dataService.GetTbl33OrdosCollectionOrderByOrdoNameFromFilterText(FilterText);
                //DataSetCount = OrdosCollection.Count;

                //OnPropertyChanged(nameof(OrdosCollection));
                //RefreshOrdoItems();
                //SelectedOrdoTabIndex = 2;
            }
            if (_selectedFishesTabIndex == 6)  //Class
            {
                //ClassesCollection = _dataService.GetTbl21ClassesCollectionOrderByClassNameFromFilterText(FilterText);
                //DataSetCount = ClassesCollection.Count;

                //OnPropertyChanged(nameof(ClassesCollection));
                //RefreshClassItems();
                //SelectedClassTabIndex = 2;
            }
            if (_selectedFishesTabIndex == 7)  //Division
            {
                //DivisionsCollection = _dataService.GetTbl09DivisionsCollectionOrderByDivisionNameFromFilterText(FilterText);
                //DataSetCount = DivisionsCollection.Count;

                //OnPropertyChanged(nameof(DivisionsCollection));
                //RefreshDivisionItems();

                //SelectedDivisionTabIndex = 1;
            }
            if (_selectedFishesTabIndex == 19)  //Subphylum
            {
                //PhylumsCollection = _dataService.GetTbl06PhylumsCollectionOrderByPhylumNameFromFilterText(FilterText);
                //DataSetCount = PhylumsCollection.Count;

                //OnPropertyChanged(nameof(PhylumsCollection));
                //RefreshPhylumItems();
                //SelectedPhylumTabIndex = 1;
            }
            if (_selectedFishesTabIndex == 20)  //Phylum
            {
                //        IsLoading = true;
                //        PhylumStartModify();
                //      OnPropertyChanged(nameof(Tbl03RegnumsAllList));
                //Tbl03RegnumsAllList ??= new ObservableCollection<Tbl03Regnum>();
                //Tbl03RegnumsAllList = _dataService.GetTbl03RegnumsCollectionOrderByRegnumNameAndSubregnum();
                //OnPropertyChanged(nameof(Tbl03RegnumsAllList));
                //          Tbl06PhylumsList = _dataService.GetTbl06PhylumsCollectionOrderByPhylumNameFromPhylumId(SubphylumSelected.PhylumId);

                //         PhylumDataSetCount = Tbl06PhylumsList.Count;
                //      RefreshPhylumItems();
                //        IsLoading = false;

                //PhylumsCollection = _dataService.GetTbl06PhylumsCollectionOrderByPhylumNameFromFilterText(FilterText);
                //DataSetCount = PhylumsCollection.Count;

                //OnPropertyChanged(nameof(PhylumsCollection));
                //RefreshPhylumItems();
                //SelectedPhylumTabIndex = 1;
            }
            if (_selectedFishesTabIndex == 21)  //Regnum
            {
                if (RegnumSelected != null)
                {
                    //                RegnumStartModify();
                    //         Tbl03RegnumsList = _dataService.GetTbl03RegnumsCollectionOrderByRegnumNameAndSubregnumFromRegnumId(PhylumSelected.RegnumId);

                    //       RegnumDataSetCount = Tbl03RegnumsList.Count;
                    //          RefreshRegnumItems();

                    //                  IsLoading = true;
                    ////                  PhylumStartModify();
                    //                  Tbl03RegnumsAllList ??= new ObservableCollection<Tbl03Regnum>();
                    //                  Tbl03RegnumsAllList = _dataService.GetTbl03RegnumsCollectionOrderByRegnumNameAndSubregnum();

                    //                  Tbl06PhylumsList ??= new ObservableCollection<Tbl06Phylum>();
                    //                  Tbl06PhylumsList = _dataService.GetTbl06PhylumsCollectionOrderByPhylumNameFromRegnumId(RegnumSelected.RegnumId);
                    //                  PhylumDataSetCount = Tbl06PhylumsList.Count;
                    //                  RefreshPhylumItems();
                    //                  IsLoading = false;
                }
                //RegnumsCollection = _dataService.GetTbl03RegnumsCollectionOrderByRegnumNameAndSubregnumFromFilterText(FilterText);
                //DataSetCount = RegnumsCollection.Count;

                //OnPropertyChanged(nameof(RegnumsCollection));
                //RefreshRegnumItems();

                //SelectedRegnumTabIndex = 0;
            }
        }
    }

    #endregion "Public Commands to change TabItems"

    #region All Properties

    #region Selected Properties

    private Tbl03Regnum _regnumSelected = null!;
    public Tbl03Regnum RegnumSelected
    {
        get => _regnumSelected;
        set => SetProperty(ref _regnumSelected, value);
    }

    private Tbl06Phylum _phylumSelected = null!;
    public Tbl06Phylum PhylumSelected
    {
        get => _phylumSelected;
        set => SetProperty(ref _phylumSelected, value);
    }

    private Tbl09Division _divisionSelected = null!;
    public Tbl09Division DivisionSelected
    {
        get => _divisionSelected;
        set => SetProperty(ref _divisionSelected, value);
    }

    private Tbl90Reference _referenceSelected = null!;
    public Tbl90Reference ReferenceSelected
    {
        get => _referenceSelected;
        set => SetProperty(ref _referenceSelected, value);
    }

    private Tbl90Reference _referenceExpertSelected = null!;
    public Tbl90Reference ReferenceExpertSelected
    {
        get => _referenceExpertSelected;
        set => SetProperty(ref _referenceExpertSelected, value);
    }

    private Tbl90Reference _referenceSourceSelected = null!;
    public Tbl90Reference ReferenceSourceSelected
    {
        get => _referenceSourceSelected;
        set => SetProperty(ref _referenceSourceSelected, value);
    }
    private Tbl90Reference _referenceAuthorSelected = null!;
    public Tbl90Reference ReferenceAuthorSelected
    {
        get => _referenceAuthorSelected;
        set => SetProperty(ref _referenceAuthorSelected, value);
    }

    private Tbl93Comment _commentSelected = null!;
    public Tbl93Comment CommentSelected
    {
        get => _commentSelected;
        set => SetProperty(ref _commentSelected, value);
    }

    #endregion

    #region Public Properties
    public string SearchRegnumName { get; set; } = null!;


    private ObservableCollection<Tbl03Regnum> _tbl03RegnumsList = null!;
    public ObservableCollection<Tbl03Regnum> Tbl03RegnumsList
    {
        get => _tbl03RegnumsList;
        set
        {
            _tbl03RegnumsList = value; OnPropertyChanged();
        }
    }
    private ObservableCollection<Tbl03Regnum>? _tbl03RegnumsAllList = null!;
    public ObservableCollection<Tbl03Regnum>? Tbl03RegnumsAllList
    {
        get => _tbl03RegnumsAllList;
        set
        {
            _tbl03RegnumsAllList = value; OnPropertyChanged();
        }
    }

    private ObservableCollection<Tbl06Phylum> _tbl06PhylumsList = null!;
    public ObservableCollection<Tbl06Phylum> Tbl06PhylumsList
    {
        get => _tbl06PhylumsList;
        set
        {
            _tbl06PhylumsList = value; OnPropertyChanged();
        }
    }

    private ObservableCollection<Tbl90RefAuthor> _tbl90AuthorsAllList = null!;
    public ObservableCollection<Tbl90RefAuthor> Tbl90AuthorsAllList
    {
        get => _tbl90AuthorsAllList;
        set
        {
            _tbl90AuthorsAllList = value; OnPropertyChanged();
        }
    }

    private ObservableCollection<Tbl09Division>? _tbl09DivisionsList;
    public ObservableCollection<Tbl09Division>? Tbl09DivisionsList
    {
        get => _tbl09DivisionsList;
        set
        {
            _tbl09DivisionsList = value; OnPropertyChanged(nameof(Tbl09DivisionsList));
        }
    }

    private ObservableCollection<Tbl90RefSource> _tbl90SourcesAllList = null!;
    public ObservableCollection<Tbl90RefSource> Tbl90SourcesAllList
    {
        get => _tbl90SourcesAllList;
        set
        {
            _tbl90SourcesAllList = value; OnPropertyChanged();
        }
    }

    private ObservableCollection<Tbl90RefExpert> _tbl90ExpertsAllList = null!;
    public ObservableCollection<Tbl90RefExpert> Tbl90ExpertsAllList
    {
        get => _tbl90ExpertsAllList;
        set
        {
            _tbl90ExpertsAllList = value; OnPropertyChanged();
        }
    }

    private ObservableCollection<Tbl90Reference> _tbl90ReferenceAuthorsList = null!;
    public ObservableCollection<Tbl90Reference> Tbl90ReferenceAuthorsList
    {
        get => _tbl90ReferenceAuthorsList;
        set
        {
            _tbl90ReferenceAuthorsList = value; OnPropertyChanged();
        }
    }

    private ObservableCollection<Tbl90Reference> _tbl90ReferenceSourcesList = null!;
    public ObservableCollection<Tbl90Reference> Tbl90ReferenceSourcesList
    {
        get => _tbl90ReferenceSourcesList;
        set
        {
            _tbl90ReferenceSourcesList = value; OnPropertyChanged();
        }
    }

    private ObservableCollection<Tbl90Reference> _tbl90ReferenceExpertsList = null!;
    public ObservableCollection<Tbl90Reference> Tbl90ReferenceExpertsList
    {
        get => _tbl90ReferenceExpertsList;
        set
        {
            _tbl90ReferenceExpertsList = value; OnPropertyChanged();
        }
    }

    private ObservableCollection<Tbl93Comment> _tbl93CommentsList = null!;
    public ObservableCollection<Tbl93Comment> Tbl93CommentsList
    {
        get => _tbl93CommentsList;
        set
        {
            _tbl93CommentsList = value; OnPropertyChanged();
        }
    }


    //---------------------------------------------------------------------

    private int _regnumDataSetCount;
    public int RegnumDataSetCount
    {
        get => _regnumDataSetCount;
        set
        {
            _regnumDataSetCount = value; OnPropertyChanged();
        }
    }

    private int _phylumDataSetCount;
    public int PhylumDataSetCount
    {
        get => _phylumDataSetCount;
        set
        {
            _phylumDataSetCount = value; OnPropertyChanged();
        }
    }

    private int _divisionDataSetCount;
    public int DivisionDataSetCount
    {
        get => _divisionDataSetCount;
        set
        {
            _divisionDataSetCount = value; OnPropertyChanged();
        }
    }

    private int _referenceExpertDataSetCount;
    public int ReferenceExpertDataSetCount
    {
        get => _referenceExpertDataSetCount;
        set
        {
            _referenceExpertDataSetCount = value; OnPropertyChanged();
        }
    }
    private int _referenceSourceDataSetCount;
    public int ReferenceSourceDataSetCount
    {
        get => _referenceSourceDataSetCount;
        set
        {
            _referenceSourceDataSetCount = value; OnPropertyChanged();
        }
    }
    private int _referenceAuthorDataSetCount;
    public int ReferenceAuthorDataSetCount
    {
        get => _referenceAuthorDataSetCount;
        set
        {
            _referenceAuthorDataSetCount = value; OnPropertyChanged();
        }
    }

    private int _commentDataSetCount;
    public int CommentDataSetCount
    {
        get => _commentDataSetCount;
        set
        {
            _commentDataSetCount = value; OnPropertyChanged();
        }
    }


    //---------------------------------------------------------------------
    public bool IsModified
    {
        get; set;
    }

    private bool _isLoading;
    public bool IsLoading
    {
        get => _isLoading;
        set => SetProperty(ref _isLoading, value);
    }

    private bool _isInEdit = false;
    public bool IsInEdit
    {
        get => _isInEdit;
        set => SetProperty(ref _isInEdit, value);
    }

    private bool _isNewRegnum;
    public bool IsNewRegnum
    {
        get => _isNewRegnum;
        set => SetProperty(ref _isNewRegnum, value);
    }
    private bool _isNewPhylum;
    public bool IsNewPhylum
    {
        get => _isNewPhylum;
        set => SetProperty(ref _isNewPhylum, value);
    }

    private bool _isNewDivision;
    public bool IsNewDivision
    {
        get => _isNewDivision;
        set => SetProperty(ref _isNewDivision, value);
    }

    private bool _isNewReferenceExpert;
    public bool IsNewReferenceExpert
    {
        get => _isNewReferenceExpert;
        set => SetProperty(ref _isNewReferenceExpert, value);
    }

    private bool _isNewReferenceSource;
    public bool IsNewReferenceSource
    {
        get => _isNewReferenceSource;
        set => SetProperty(ref _isNewReferenceSource, value);
    }

    private bool _isNewReferenceAuthor;
    public bool IsNewReferenceAuthor
    {
        get => _isNewReferenceAuthor;
        set => SetProperty(ref _isNewReferenceAuthor, value);
    }

    private bool _isNewComment;
    public bool IsNewComment
    {
        get => _isNewComment;
        set => SetProperty(ref _isNewComment, value);
    }
    #endregion "Public Properties"            

    #region Refresh Methods

    private void RefreshRegnumItems()
    {
        RegnumItems.Clear();
        if (Tbl03RegnumsList != null)
        {
            foreach (var item in Tbl03RegnumsList)
            {
                RegnumItems.Add(item);
            }

            if (Tbl03RegnumsList.Count == 0) return;
            if (RegnumSelected == null && Tbl03RegnumsList.Count != 0)
            {
                RegnumSelected = RegnumItems.FirstOrDefault()!;
            }
        }
    }

    private void RefreshPhylumItems()
    {
        PhylumItems.Clear();
        if (Tbl06PhylumsList != null)
        {
            foreach (var item in Tbl06PhylumsList)
            {
                PhylumItems.Add(item);
            }

            if (Tbl06PhylumsList.Count == 0) return;
            if (PhylumSelected == null && Tbl06PhylumsList.Count != 0)
            {
                PhylumSelected = PhylumItems.FirstOrDefault()!;
            }
        }
    }
    private void RefreshDivisionItems()
    {
        DivisionItems.Clear();
        if (Tbl09DivisionsList != null)
        {
            foreach (var item in Tbl09DivisionsList)
            {
                DivisionItems.Add(item);
            }

            if (Tbl09DivisionsList.Count == 0) return;
            if (DivisionSelected == null && Tbl09DivisionsList.Count != 0)
            {
                DivisionSelected = DivisionItems.FirstOrDefault()!;
            }
        }
    }

    private void RefreshReferenceExpertItems()
    {
        ReferenceExpertItems.Clear();
        if (Tbl90ReferenceExpertsList != null)
        {
            foreach (var item in Tbl90ReferenceExpertsList)
            {
                ReferenceExpertItems.Add(item);
            }

            if (Tbl90ReferenceExpertsList.Count == 0) return;
            if (ReferenceExpertSelected == null && Tbl90ReferenceExpertsList.Count != 0)
            {
                ReferenceExpertSelected = ReferenceExpertItems.FirstOrDefault()!;
            }
        }
    }
    private void RefreshReferenceSourceItems()
    {
        ReferenceSourceItems.Clear();
        if (Tbl90ReferenceSourcesList != null)
        {
            foreach (var item in Tbl90ReferenceSourcesList)
            {
                ReferenceSourceItems.Add(item);
            }

            if (Tbl90ReferenceSourcesList.Count == 0) return;
            if (ReferenceSourceSelected == null && Tbl90ReferenceSourcesList.Count != 0)
            {
                ReferenceSourceSelected = ReferenceSourceItems.FirstOrDefault()!;
            }
        }
    }
    private void RefreshReferenceAuthorItems()
    {
        ReferenceAuthorItems.Clear();
        if (Tbl90ReferenceAuthorsList != null)
        {
            foreach (var item in Tbl90ReferenceAuthorsList)
            {
                ReferenceAuthorItems.Add(item);
            }

            if (Tbl90ReferenceAuthorsList.Count == 0) return;
            if (ReferenceAuthorSelected == null && Tbl90ReferenceAuthorsList.Count != 0)
            {
                ReferenceAuthorSelected = ReferenceAuthorItems.FirstOrDefault()!;
            }
        }
    }
    private void RefreshCommentItems()
    {
        CommentItems.Clear();
        if (Tbl93CommentsList != null)
        {
            foreach (var item in Tbl93CommentsList)
            {
                CommentItems.Add(item);
            }

            if (Tbl93CommentsList.Count == 0) return;
            if (CommentSelected == null && Tbl93CommentsList.Count != 0)
            {
                CommentSelected = CommentItems.FirstOrDefault()!;
            }
        }
    }

    #endregion

    #endregion

}
