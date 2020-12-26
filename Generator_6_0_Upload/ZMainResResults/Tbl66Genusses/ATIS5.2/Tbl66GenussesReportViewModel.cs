   
       //------------------------------------------------------------------------------
       private RelayCommand _pdfTbl66GenussesCommand;
	public ICommand PdfTbl66GenussesCommand
	{
		get { return _pdfTbl66GenussesCommand ?? (_pdfTbl66GenussesCommand = new RelayCommand(delegate { CreatePdfTbl66Genusses(_mainId); })); }
	}

	private static void CreatePdfTbl66Genusses(int id)
	{
		ReportTbl66GenussesPdf.CreateMainPdf(id);
	}   
       //------------------------------------------------------------------------------
       //------------------------------------------------------------------------------  
   
        public ObservableCollection< Tbl66Genus> GetValueTbl66GenussesList(int currentId)
        {
             Tbl66GenussesList = new ObservableCollection<Tbl66Genus>
                  (from y in _tbl66GenussesRepository.GetAll()
                   where y.GenusID == currentId 
   
                   orderby y.GenusName   
                   select y);
            return Tbl66GenussesList;
        } 
   
        public ObservableCollection< Tbl66Genus> GetValueTbl66GenussesList()
        {
             Tbl66GenussesList = new ObservableCollection<Tbl66Genus>
                { new ObservableCollection<Tbl66Genus>
                    (from x in _tbl66GenussesRepository.GetAll() select x).LastOrDefault()
                };
            return Tbl66GenussesList;
        }

        #endregion  
   
        #region "Public Property  Tbl66Genus"

        private ObservableCollection< Tbl66Genus>  _tbl66GenussesList;
        public ObservableCollection< Tbl66Genus>  Tbl66GenussesList
        {
            get { return  _tbl66GenussesList; }
            set {  _tbl66GenussesList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection< Tbl66Genus>  _tbl66GenussesAllList;
        public  ObservableCollection< Tbl66Genus>  Tbl66GenussesAllList
        {
            get { return  _tbl66GenussesAllList; }
            set {  _tbl66GenussesAllList = value; RaisePropertyChanged(); }
        }

        #endregion "Public Properties" 
