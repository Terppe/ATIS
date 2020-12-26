   
       //------------------------------------------------------------------------------
       private RelayCommand _pdfTbl15SubdivisionsCommand;
	public ICommand PdfTbl15SubdivisionsCommand
	{
		get { return _pdfTbl15SubdivisionsCommand ?? (_pdfTbl15SubdivisionsCommand = new RelayCommand(delegate { CreatePdfTbl15Subdivisions(_mainId); })); }
	}

	private static void CreatePdfTbl15Subdivisions(int id)
	{
		ReportTbl15SubdivisionsPdf.CreateMainPdf(id);
	}   
       //------------------------------------------------------------------------------
       //------------------------------------------------------------------------------  
   
        public ObservableCollection< Tbl15Subdivision> GetValueTbl15SubdivisionsList(int currentId)
        {
             Tbl15SubdivisionsList = new ObservableCollection<Tbl15Subdivision>
                  (from y in _tbl15SubdivisionsRepository.GetAll()
                   where y.SubdivisionID == currentId 
   
                   orderby y.SubdivisionName   
                   select y);
            return Tbl15SubdivisionsList;
        } 
   
        public ObservableCollection< Tbl15Subdivision> GetValueTbl15SubdivisionsList()
        {
             Tbl15SubdivisionsList = new ObservableCollection<Tbl15Subdivision>
                { new ObservableCollection<Tbl15Subdivision>
                    (from x in _tbl15SubdivisionsRepository.GetAll() select x).LastOrDefault()
                };
            return Tbl15SubdivisionsList;
        }

        #endregion  
   
        #region "Public Property  Tbl15Subdivision"

        private ObservableCollection< Tbl15Subdivision>  _tbl15SubdivisionsList;
        public ObservableCollection< Tbl15Subdivision>  Tbl15SubdivisionsList
        {
            get { return  _tbl15SubdivisionsList; }
            set {  _tbl15SubdivisionsList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection< Tbl15Subdivision>  _tbl15SubdivisionsAllList;
        public  ObservableCollection< Tbl15Subdivision>  Tbl15SubdivisionsAllList
        {
            get { return  _tbl15SubdivisionsAllList; }
            set {  _tbl15SubdivisionsAllList = value; RaisePropertyChanged(); }
        }

        #endregion "Public Properties" 
