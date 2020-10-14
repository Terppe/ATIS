using ATIS.Dal.Models;
using ATIS.Ui.Helper;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ATIS.Ui.Views.Search
{
    public class SearchQuickViewModel : ViewModelBase
    {
        #region "Private Data Members"
        private readonly SearchBasicGet _extSearchGet = new SearchBasicGet();

        #endregion "Private Data Members"

        public SearchQuickViewModel(string filterText)
        {
            InitSearchQuick(filterText);
        }
        //------------------------------------------------------------------
        public SearchQuickViewModel()
        {

        }

        //------------------------------------------------------------------

        //------------------------------------------------------------------
        private RelayCommand _searchQuickByNameCommand;
        public ICommand RunQuickSearchCommand => _searchQuickByNameCommand ??= new RelayCommand(delegate { InitSearchQuick(FilterText); });

        private void InitSearchQuick(string filterText)
        {
            if (string.IsNullOrEmpty(filterText)) return;

            GenussesCollection ??= new ObservableCollection<Tbl66Genus>();
            GenussesCollection = _extSearchGet.SearchFilterTextReturnCollection<Tbl66Genus>(filterText, "genus");
            RaisePropertyChanged("GenussesCollection");

            FiSpeciessesCollection ??= new ObservableCollection<Tbl69FiSpecies>();
            FiSpeciessesCollection = _extSearchGet.SearchFilterTextReturnCollection<Tbl69FiSpecies>(filterText, "fispecies");
            RaisePropertyChanged("FiSpeciessesCollection");

            PlSpeciessesCollection ??= new ObservableCollection<Tbl72PlSpecies>();
            PlSpeciessesCollection = _extSearchGet.SearchFilterTextReturnCollection<Tbl72PlSpecies>(filterText, "plspecies");
            RaisePropertyChanged("PlSpeciessesCollection");

            RegnumsCollection ??= new ObservableCollection<Tbl03Regnum>();
            RegnumsCollection = _extSearchGet.SearchFilterTextReturnCollection<Tbl03Regnum>(filterText, "regnum");
            RaisePropertyChanged("RegnumsCollection");
        }

        //----------------------------------------------------------------------

        #region "Public Commands Select Tab"                                         
        public enum Page
        {
            Page0 = 0,
            Page1 = 1,
            Page2 = 2,
            Page3 = 3
        }

        private RelayCommand _getTabSelectionChangedFiSpeciesCommand;
        public ICommand TabSelectionChangedFiSpeciesCommand => _getTabSelectionChangedFiSpeciesCommand ??= new RelayCommand(delegate { TabSelectionChangedFiSpecies(FilterText); });
        private void TabSelectionChangedFiSpecies(string filterText)
        {
            FiSpeciessesCollection ??= new ObservableCollection<Tbl69FiSpecies>();
            FiSpeciessesCollection = _extSearchGet.SearchFilterTextReturnCollection<Tbl69FiSpecies>(filterText, "fispecies");
            RaisePropertyChanged("FiSpeciessesCollection");
        }

        private RelayCommand _getTabSelectionChangedPlSpeciesCommand;
        public ICommand TabSelectionChangedPlSpeciesCommand => _getTabSelectionChangedPlSpeciesCommand ??= new RelayCommand(delegate { TabSelectionChangedPlSpecies(FilterText); });
        private void TabSelectionChangedPlSpecies(string filterText)
        {
            PlSpeciessesCollection ??= new ObservableCollection<Tbl72PlSpecies>();
            PlSpeciessesCollection = _extSearchGet.SearchFilterTextReturnCollection<Tbl72PlSpecies>(filterText, "plspecies");
            RaisePropertyChanged("PlSpeciessesCollection");
        }

        public Page TabPageTribus { get; set; }
        private RelayCommand _getTabSelectionChangedTribusCommand;
        public ICommand TabSelectionChangedTribusCommand => _getTabSelectionChangedTribusCommand ??= new RelayCommand(delegate { TabSelectionChangedTribus(FilterText); });
        private void TabSelectionChangedTribus(string filterText)
        {
            switch (TabPageTribus)
            {
                case Page.Page0:
                    {
                        InfratribussesCollection ??= new ObservableCollection<Tbl63Infratribus>();
                        InfratribussesCollection = _extSearchGet.SearchFilterTextReturnCollection<Tbl63Infratribus>(filterText, "infratribus");
                        RaisePropertyChanged("InfratribussesCollection");
                        break;
                    }

                case Page.Page1:
                    {
                        SubtribussesCollection ??= new ObservableCollection<Tbl60Subtribus>();
                        SubtribussesCollection = _extSearchGet.SearchFilterTextReturnCollection<Tbl60Subtribus>(filterText, "subtribus");
                        RaisePropertyChanged("SubtribussesCollection");
                        break;
                    }

                case Page.Page2:
                    {
                        TribussesCollection ??= new ObservableCollection<Tbl57Tribus>();
                        TribussesCollection = _extSearchGet.SearchFilterTextReturnCollection<Tbl57Tribus>(filterText, "tribus");
                        RaisePropertyChanged("TribussesCollection");
                        break;
                    }

                case Page.Page3:
                    {
                        SupertribussesCollection ??= new ObservableCollection<Tbl54Supertribus>();
                        SupertribussesCollection = _extSearchGet.SearchFilterTextReturnCollection<Tbl54Supertribus>(filterText, "supertribus");
                        RaisePropertyChanged("SupertribussesCollection");
                        break;
                    }
            }
        }
        //-------------------------------------------------------------

        public Page TabPageFamily { get; set; }
        private RelayCommand _getTabSelectionChangedFamilyCommand;
        public ICommand TabSelectionChangedFamilyCommand => _getTabSelectionChangedFamilyCommand ??= new RelayCommand(delegate { TabSelectionChangedFamily(FilterText); });
        private void TabSelectionChangedFamily(string filterText)
        {
            switch (TabPageFamily)
            {
                case Page.Page0:
                    {
                        InfrafamiliesCollection ??= new ObservableCollection<Tbl51Infrafamily>();
                        InfrafamiliesCollection = _extSearchGet.SearchFilterTextReturnCollection<Tbl51Infrafamily>(filterText, "infrafamily");
                        RaisePropertyChanged("InfrafamiliesCollection");
                        break;
                    }

                case Page.Page1:
                    {
                        SubfamiliesCollection ??= new ObservableCollection<Tbl48Subfamily>();
                        SubfamiliesCollection = _extSearchGet.SearchFilterTextReturnCollection<Tbl48Subfamily>(filterText, "subfamily");
                        RaisePropertyChanged("SubfamiliesCollection");
                        break;
                    }

                case Page.Page2:
                    {
                        FamiliesCollection ??= new ObservableCollection<Tbl45Family>();
                        FamiliesCollection = _extSearchGet.SearchFilterTextReturnCollection<Tbl45Family>(filterText, "family");
                        RaisePropertyChanged("FamiliesCollection");
                        break;
                    }

                case Page.Page3:
                    {
                        SuperfamiliesCollection ??= new ObservableCollection<Tbl42Superfamily>();
                        SuperfamiliesCollection = _extSearchGet.SearchFilterTextReturnCollection<Tbl42Superfamily>(filterText, "superfamily");
                        RaisePropertyChanged("SuperfamiliesCollection");
                        break;
                    }
            }
        }
        //-------------------------------------------------------------
        public Page TabPageOrdo { get; set; }

        private RelayCommand _getTabSelectionChangedOrdoCommand;
        public ICommand TabSelectionChangedOrdoCommand => _getTabSelectionChangedOrdoCommand ??= new RelayCommand(delegate { TabSelectionChangedOrdo(FilterText); });
        private void TabSelectionChangedOrdo(string filterText)
        {

            switch (TabPageOrdo)
            {
                case Page.Page0:
                    {
                        InfraordosCollection ??= new ObservableCollection<Tbl39Infraordo>();
                        InfraordosCollection = _extSearchGet.SearchFilterTextReturnCollection<Tbl39Infraordo>(filterText, "infraordo");
                        RaisePropertyChanged("InfraordosCollection");
                        break;
                    }

                case Page.Page1:
                    {
                        SubordosCollection ??= new ObservableCollection<Tbl36Subordo>();
                        SubordosCollection = _extSearchGet.SearchFilterTextReturnCollection<Tbl36Subordo>(filterText, "subordo");
                        RaisePropertyChanged("SubordosCollection");
                        break;
                    }

                case Page.Page2:
                    {
                        OrdosCollection ??= new ObservableCollection<Tbl33Ordo>();
                        OrdosCollection = _extSearchGet.SearchFilterTextReturnCollection<Tbl33Ordo>(filterText, "ordo");
                        RaisePropertyChanged("OrdosCollection");
                        break;
                    }

                case Page.Page3:
                    {
                        LegiosCollection ??= new ObservableCollection<Tbl30Legio>();
                        LegiosCollection = _extSearchGet.SearchFilterTextReturnCollection<Tbl30Legio>(filterText, "legio");
                        RaisePropertyChanged("LegiosCollection");
                        break;
                    }
            }
        }
        //-------------------------------------------------------------
        public Page TabPageClass { get; set; }

        private RelayCommand _getTabSelectionChangedClassCommand;
        public ICommand TabSelectionChangedClassCommand => _getTabSelectionChangedClassCommand ??= new RelayCommand(delegate { TabSelectionChangedClass(FilterText); });
        private void TabSelectionChangedClass(string filterText)
        {
            switch (TabPageClass)
            {
                case Page.Page0:
                    {
                        InfraclassesCollection ??= new ObservableCollection<Tbl27Infraclass>();
                        InfraclassesCollection = _extSearchGet.SearchFilterTextReturnCollection<Tbl27Infraclass>(filterText, "infraclass");
                        RaisePropertyChanged("InfraclassesCollection");
                        break;
                    }

                case Page.Page1:
                    {
                        SubclassesCollection ??= new ObservableCollection<Tbl24Subclass>();
                        SubclassesCollection = _extSearchGet.SearchFilterTextReturnCollection<Tbl24Subclass>(filterText, "subclass");
                        RaisePropertyChanged("SubclassesCollection");
                        break;
                    }

                case Page.Page2:
                    {
                        ClassesCollection ??= new ObservableCollection<Tbl21Class>();
                        ClassesCollection = _extSearchGet.SearchFilterTextReturnCollection<Tbl21Class>(filterText, "class");
                        RaisePropertyChanged("ClassesCollection");
                        break;
                    }

                case Page.Page3:
                    {
                        SuperclassesCollection ??= new ObservableCollection<Tbl18Superclass>();
                        SuperclassesCollection = _extSearchGet.SearchFilterTextReturnCollection<Tbl18Superclass>(filterText, "superclass");
                        RaisePropertyChanged("SuperclassesCollection");
                        break;
                    }
            }
        }
        //-------------------------------------------------------------
        public Page TabPageDivision { get; set; }

        private RelayCommand _getTabSelectionChangedDivisionCommand;
        public ICommand TabSelectionChangedDivisionCommand => _getTabSelectionChangedDivisionCommand ??= new RelayCommand(delegate { TabSelectionChangedDivision(FilterText); });
        private void TabSelectionChangedDivision(string filterText)
        {
            switch (TabPageDivision)
            {
                case Page.Page0:
                    {
                        SubdivisionsCollection ??= new ObservableCollection<Tbl15Subdivision>();
                        SubdivisionsCollection = _extSearchGet.SearchFilterTextReturnCollection<Tbl15Subdivision>(filterText, "subdivision");
                        RaisePropertyChanged("SubdivisionsCollection");
                        break;
                    }

                case Page.Page1:
                    {
                        DivisionsCollection ??= new ObservableCollection<Tbl09Division>();
                        DivisionsCollection = _extSearchGet.SearchFilterTextReturnCollection<Tbl09Division>(filterText, "division");
                        RaisePropertyChanged("DivisionsCollection");
                        break;
                    }

            }
        }
        //-------------------------------------------------------------
        public Page TabPagePhylum { get; set; }

        private RelayCommand _getTabSelectionChangedPhylumCommand;
        public ICommand TabSelectionChangedPhylumCommand => _getTabSelectionChangedPhylumCommand ??= new RelayCommand(delegate { TabSelectionChangedPhylum(FilterText); });
        private void TabSelectionChangedPhylum(string filterText)
        {
            switch (TabPagePhylum)
            {
                case Page.Page0:
                    SubphylumsCollection ??= new ObservableCollection<Tbl12Subphylum>();
                    SubphylumsCollection = _extSearchGet.SearchFilterTextReturnCollection<Tbl12Subphylum>(filterText, "subphylum");
                    RaisePropertyChanged("SubphylumsCollection");
                    break;
                case Page.Page1:
                    PhylumsCollection ??= new ObservableCollection<Tbl06Phylum>();
                    PhylumsCollection = _extSearchGet.SearchFilterTextReturnCollection<Tbl06Phylum>(filterText, "phylum");
                    RaisePropertyChanged("PhylumsCollection");
                    break;
            }
        }

        //-------------------------------------------------------------

        private RelayCommand _getTabSelectionChangedRegnumCommand;
        public ICommand TabSelectionChangedRegnumCommand => _getTabSelectionChangedRegnumCommand ??= new RelayCommand(delegate { TabSelectionChangedRegnum(FilterText); });
        private void TabSelectionChangedRegnum(string filterText)
        {
            RegnumsCollection ??= new ObservableCollection<Tbl03Regnum>();
            RegnumsCollection = _extSearchGet.SearchFilterTextReturnCollection<Tbl03Regnum>(filterText, "regnum");
            RaisePropertyChanged("RegnumsCollection");
        }

        #endregion "Public Commands Select Tap"  

        #region "Print PDF"

        //------------------------------PDF Print all-------------------------------
        public void GetListAllByFilterText(string filterText)
        {
            RegnumsCollection = _extSearchGet.SearchFilterTextReturnCollection<Tbl03Regnum>(filterText, "regnum");
            PhylumsCollection = _extSearchGet.SearchFilterTextReturnCollection<Tbl06Phylum>(filterText, "phylum");
            SubphylumsCollection = _extSearchGet.SearchFilterTextReturnCollection<Tbl12Subphylum>(filterText, "subphylum");
            DivisionsCollection = _extSearchGet.SearchFilterTextReturnCollection<Tbl09Division>(filterText, "division");
            SubdivisionsCollection = _extSearchGet.SearchFilterTextReturnCollection<Tbl15Subdivision>(filterText, "subdivision");
            SuperclassesCollection = _extSearchGet.SearchFilterTextReturnCollection<Tbl18Superclass>(filterText, "superclass");
            ClassesCollection = _extSearchGet.SearchFilterTextReturnCollection<Tbl21Class>(filterText, "class");
            SubclassesCollection = _extSearchGet.SearchFilterTextReturnCollection<Tbl24Subclass>(filterText, "subclass");
            InfraclassesCollection = _extSearchGet.SearchFilterTextReturnCollection<Tbl27Infraclass>(filterText, "infraclass");
            LegiosCollection = _extSearchGet.SearchFilterTextReturnCollection<Tbl30Legio>(filterText, "legio");
            OrdosCollection = _extSearchGet.SearchFilterTextReturnCollection<Tbl33Ordo>(filterText, "ordo");
            SubordosCollection = _extSearchGet.SearchFilterTextReturnCollection<Tbl36Subordo>(filterText, "subordo");
            InfraordosCollection = _extSearchGet.SearchFilterTextReturnCollection<Tbl39Infraordo>(filterText, "infraordo");
            SuperfamiliesCollection = _extSearchGet.SearchFilterTextReturnCollection<Tbl42Superfamily>(filterText, "superfamily");
            FamiliesCollection = _extSearchGet.SearchFilterTextReturnCollection<Tbl45Family>(filterText, "family");
            SubfamiliesCollection = _extSearchGet.SearchFilterTextReturnCollection<Tbl48Subfamily>(filterText, "subfamily");
            InfrafamiliesCollection = _extSearchGet.SearchFilterTextReturnCollection<Tbl51Infrafamily>(filterText, "infrafamily");
            SupertribussesCollection = _extSearchGet.SearchFilterTextReturnCollection<Tbl54Supertribus>(filterText, "supertribus");
            TribussesCollection = _extSearchGet.SearchFilterTextReturnCollection<Tbl57Tribus>(filterText, "tribus");
            SubtribussesCollection = _extSearchGet.SearchFilterTextReturnCollection<Tbl60Subtribus>(filterText, "subtribus");
            InfratribussesCollection = _extSearchGet.SearchFilterTextReturnCollection<Tbl63Infratribus>(filterText, "infratribus");
            GenussesCollection = _extSearchGet.SearchFilterTextReturnCollection<Tbl66Genus>(filterText, "genus");
            FiSpeciessesCollection = _extSearchGet.SearchFilterTextReturnCollection<Tbl69FiSpecies>(filterText, "fispecies");
            PlSpeciessesCollection = _extSearchGet.SearchFilterTextReturnCollection<Tbl72PlSpecies>(filterText, "plspecies");
            NamesCollection = _extSearchGet.SearchFilterTextReturnCollection<Tbl78Name>(filterText, "name");
            SynonymsCollection = _extSearchGet.SearchFilterTextReturnCollection<Tbl84Synonym>(filterText, "synonym");

            //    RegnumsCollection = new ObservableCollection<Tbl03Regnum>(_businessLayer.ListTbl03RegnumsByFilterText(filterText));
            //    PhylumsCollection = new ObservableCollection<Tbl06Phylum>(_businessLayer.ListTbl06PhylumsByFilterText(filterText));
            //    DivisionsCollection = new ObservableCollection<Tbl09Division>(_businessLayer.ListTbl09DivisionsByFilterText(filterText));
            //    SubphylumsCollection = new ObservableCollection<Tbl12Subphylum>(_businessLayer.ListTbl12SubphylumsByFilterText(filterText));
            //    Tbl15SubdivisionsList = new ObservableCollection<Tbl15Subdivision>(_businessLayer.ListTbl15SubdivisionsByFilterText(filterText));
            //    Tbl18SuperclassesList = new ObservableCollection<Tbl18Superclass>(_businessLayer.ListTbl18SuperclassesByFilterText(filterText));
            //    Tbl21ClassesList = new ObservableCollection<Tbl21Class>(_businessLayer.ListTbl21ClassesByFilterText(filterText));
            //    Tbl24SubclassesList = new ObservableCollection<Tbl24Subclass>(_businessLayer.ListTbl24SubclassesByFilterText(filterText));
            //    Tbl27InfraclassesList = new ObservableCollection<Tbl27Infraclass>(_businessLayer.ListTbl27InfraclassesByFilterText(filterText));
            //    Tbl30LegiosList = new ObservableCollection<Tbl30Legio>(_businessLayer.ListTbl30LegiosByFilterText(filterText));
            //    Tbl33OrdosList = new ObservableCollection<Tbl33Ordo>(_businessLayer.ListTbl33OrdosByFilterText(filterText));
            //    Tbl36SubordosList = new ObservableCollection<Tbl36Subordo>(_businessLayer.ListTbl36SubordosByFilterText(filterText));
            //    Tbl39InfraordosList = new ObservableCollection<Tbl39Infraordo>(_businessLayer.ListTbl39InfraordosByFilterText(filterText));
            //    Tbl42SuperfamiliesList = new ObservableCollection<Tbl42Superfamily>(_businessLayer.ListTbl42SuperfamiliesByFilterText(filterText));
            //    Tbl45FamiliesList = new ObservableCollection<Tbl45Family>(_businessLayer.ListTbl45FamiliesByFilterText(filterText));
            //    Tbl48SubfamiliesList = new ObservableCollection<Tbl48Subfamily>(_businessLayer.ListTbl48SubfamiliesByFilterText(filterText));
            //    Tbl51InfrafamiliesList = new ObservableCollection<Tbl51Infrafamily>(_businessLayer.ListTbl51InfrafamiliesByFilterText(filterText));
            //    Tbl54SupertribussesList = new ObservableCollection<Tbl54Supertribus>(_businessLayer.ListTbl54SupertribussesByFilterText(filterText));
            //    Tbl57TribussesList = new ObservableCollection<Tbl57Tribus>(_businessLayer.ListTbl57TribussesByFilterText(filterText));
            //    Tbl60SubtribussesList = new ObservableCollection<Tbl60Subtribus>(_businessLayer.ListTbl60SubtribussesByFilterText(filterText));
            //    Tbl63InfratribussesList = new ObservableCollection<Tbl63Infratribus>(_businessLayer.ListTbl63InfratribussesByFilterText(filterText));
            //    Tbl66GenussesList = new ObservableCollection<Tbl66Genus>(_businessLayer.ListTbl66GenussesByFilterText(filterText));
            //    Tbl69FiSpeciessesList = new ObservableCollection<Tbl69FiSpecies>(_businessLayer.ListTbl69FiSpeciessesByFilterText(filterText));
            //    Tbl72PlSpeciessesList = new ObservableCollection<Tbl72PlSpecies>(_businessLayer.ListTbl72PlSpeciessesByFilterText(filterText));
            //    Tbl78NamesList = new ObservableCollection<Tbl78Name>(_businessLayer.ListTbl78NamesByFilterText(filterText));
            //    Tbl84SynonymsList = new ObservableCollection<Tbl84Synonym>(_businessLayer.ListTbl84SynonymsByFilterText(filterText));
        }
        private RelayCommand _pdfListAllCommand;
        public ICommand PdfListAllCommand
        {
            get { return _pdfListAllCommand ??= new RelayCommand(delegate { CreatePdfListAll(FilterText); }); }
        }

        private static void CreatePdfListAll(string filterText)
        {
            SearchQuickListPdf.CreateMainAllPdf(filterText);
        }

        //---------------------------------------------------------------
        #endregion "Print PDF"

        #region "Private Properties"

        public string FilterText { get; set; }
        public ObservableCollection<Tbl03Regnum> RegnumsCollection { get; set; }
        public ObservableCollection<Tbl06Phylum> PhylumsCollection { get; set; }
        public ObservableCollection<Tbl12Subphylum> SubphylumsCollection { get; set; }
        public ObservableCollection<Tbl09Division> DivisionsCollection { get; set; }
        public ObservableCollection<Tbl15Subdivision> SubdivisionsCollection { get; set; }
        public ObservableCollection<Tbl18Superclass> SuperclassesCollection { get; set; }
        public ObservableCollection<Tbl21Class> ClassesCollection { get; set; }
        public ObservableCollection<Tbl24Subclass> SubclassesCollection { get; set; }
        public ObservableCollection<Tbl27Infraclass> InfraclassesCollection { get; set; }
        public ObservableCollection<Tbl30Legio> LegiosCollection { get; set; }
        public ObservableCollection<Tbl33Ordo> OrdosCollection { get; set; }
        public ObservableCollection<Tbl36Subordo> SubordosCollection { get; set; }
        public ObservableCollection<Tbl39Infraordo> InfraordosCollection { get; set; }
        public ObservableCollection<Tbl42Superfamily> SuperfamiliesCollection { get; set; }
        public ObservableCollection<Tbl45Family> FamiliesCollection { get; set; }
        public ObservableCollection<Tbl48Subfamily> SubfamiliesCollection { get; set; }
        public ObservableCollection<Tbl51Infrafamily> InfrafamiliesCollection { get; set; }
        public ObservableCollection<Tbl54Supertribus> SupertribussesCollection { get; set; }
        public ObservableCollection<Tbl57Tribus> TribussesCollection { get; set; }
        public ObservableCollection<Tbl60Subtribus> SubtribussesCollection { get; set; }
        public ObservableCollection<Tbl63Infratribus> InfratribussesCollection { get; set; }
        public ObservableCollection<Tbl66Genus> GenussesCollection { get; set; }
        public ObservableCollection<Tbl69FiSpecies> FiSpeciessesCollection { get; set; }
        public ObservableCollection<Tbl72PlSpecies> PlSpeciessesCollection { get; set; }
        public ObservableCollection<Tbl78Name> NamesCollection { get; set; }
        public ObservableCollection<Tbl84Synonym> SynonymsCollection { get; set; }


        #endregion "Private Properties"


        //#region "Private Properties"

        //private string _filterText;
        //public string FilterText
        //{
        //    get => _filterText;
        //    set { _filterText = value; RaisePropertyChanged(); }
        //}

        //private string _tbAuthor;
        //public string TbAuthor
        //{
        //    get => _tbAuthor;
        //    set
        //    {
        //        _tbAuthor = value;
        //        if (_tbAuthor.Contains("("))
        //        {
        //            var length = _tbAuthor.Length;
        //            _tbAuthor = "- " + _tbAuthor.Insert(length - 1, ", " + _tbAuthorYear);
        //        }
        //        else
        //        {
        //            _tbAuthor = "- " + _tbAuthor + ", " + _tbAuthorYear;
        //        }

        //        RaisePropertyChanged();
        //    }
        //}



    }

}
