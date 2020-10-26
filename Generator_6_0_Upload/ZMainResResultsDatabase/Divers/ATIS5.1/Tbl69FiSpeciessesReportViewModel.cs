   
       //------------------------------------------------------------------------------
       private RelayCommand _pdfTbl69FiSpeciessesCommand;
	public ICommand PdfTbl69FiSpeciessesCommand
	{
		get { return _pdfTbl69FiSpeciessesCommand ?? (_pdfTbl69FiSpeciessesCommand = new RelayCommand(delegate { CreatePdfTbl69FiSpeciesses(_mainId); })); }
	}

	private static void CreatePdfTbl69FiSpeciesses(int id)
	{
		ReportTbl69FiSpeciessesPdf.CreateMainPdf(id);
	}   
       //------------------------------------------------------------------------------
       //------------------------------------------------------------------------------  
   
        public ObservableCollection< Tbl69FiSpecies> GetValueTbl69FiSpeciessesList(int currentId)
        {
             Tbl69FiSpeciessesList = new ObservableCollection<Tbl69FiSpecies>
                  (from y in _tbl69FiSpeciessesRepository.GetAll()
                   where y.FiSpeciesID == currentId 
      
                 orderby y.Tbl66Genusses.GenusName, y.FiSpeciesName, y.Subspecies, y.Divers        
   
                   select y);
            return Tbl69FiSpeciessesList;
        } 
   
        public ObservableCollection< Tbl69FiSpecies> GetValueTbl69FiSpeciessesList()
        {
             Tbl69FiSpeciessesList = new ObservableCollection<Tbl69FiSpecies>
                { new ObservableCollection<Tbl69FiSpecies>
                    (from x in _tbl69FiSpeciessesRepository.GetAll() select x).LastOrDefault()
                };
            return Tbl69FiSpeciessesList;
        }

        #endregion  
   
        #region "Public Property  Tbl69FiSpecies"

        private ObservableCollection< Tbl69FiSpecies>  _tbl69FiSpeciessesList;
        public ObservableCollection< Tbl69FiSpecies>  Tbl69FiSpeciessesList
        {
            get { return  _tbl69FiSpeciessesList; }
            set {  _tbl69FiSpeciessesList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection< Tbl69FiSpecies>  _tbl69FiSpeciessesAllList;
        public  ObservableCollection< Tbl69FiSpecies>  Tbl69FiSpeciessesAllList
        {
            get { return  _tbl69FiSpeciessesAllList; }
            set {  _tbl69FiSpeciessesAllList = value; RaisePropertyChanged(); }
        }

        #endregion "Public Properties" 
