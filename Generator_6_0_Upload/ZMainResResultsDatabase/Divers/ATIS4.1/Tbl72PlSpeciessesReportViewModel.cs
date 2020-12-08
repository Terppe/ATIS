   
       //------------------------------------------------------------------------------
       private RelayCommand _pdfTbl72PlSpeciessesCommand;
	public ICommand PdfTbl72PlSpeciessesCommand
	{
		get { return _pdfTbl72PlSpeciessesCommand ?? (_pdfTbl72PlSpeciessesCommand = new RelayCommand(delegate { CreatePdfTbl72PlSpeciesses(_mainId); })); }
	}

	private static void CreatePdfTbl72PlSpeciesses(int id)
	{
		ReportTbl72PlSpeciessesPdf.CreateMainPdf(id);
	}   
       //------------------------------------------------------------------------------
       //------------------------------------------------------------------------------  
   
        public ObservableCollection< Tbl72PlSpecies> GetValueTbl72PlSpeciessesList(int currentId)
        {
             Tbl72PlSpeciessesList = new ObservableCollection<Tbl72PlSpecies>
                  (from y in _tbl72PlSpeciessesRepository.GetAll()
                   where y.PlSpeciesID == currentId 
      
                 orderby y.Tbl66Genusses.GenusName, y.PlSpeciesName, y.Subspecies, y.Divers        
   
                   select y);
            return Tbl72PlSpeciessesList;
        } 
   
        public ObservableCollection< Tbl72PlSpecies> GetValueTbl72PlSpeciessesList()
        {
             Tbl72PlSpeciessesList = new ObservableCollection<Tbl72PlSpecies>
                { new ObservableCollection<Tbl72PlSpecies>
                    (from x in _tbl72PlSpeciessesRepository.GetAll() select x).LastOrDefault()
                };
            return Tbl72PlSpeciessesList;
        }

        #endregion  
   
        #region "Public Property  Tbl72PlSpecies"

        private ObservableCollection< Tbl72PlSpecies>  _tbl72PlSpeciessesList;
        public ObservableCollection< Tbl72PlSpecies>  Tbl72PlSpeciessesList
        {
            get { return  _tbl72PlSpeciessesList; }
            set {  _tbl72PlSpeciessesList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection< Tbl72PlSpecies>  _tbl72PlSpeciessesAllList;
        public  ObservableCollection< Tbl72PlSpecies>  Tbl72PlSpeciessesAllList
        {
            get { return  _tbl72PlSpeciessesAllList; }
            set {  _tbl72PlSpeciessesAllList = value; RaisePropertyChanged(); }
        }

        #endregion "Public Properties" 
