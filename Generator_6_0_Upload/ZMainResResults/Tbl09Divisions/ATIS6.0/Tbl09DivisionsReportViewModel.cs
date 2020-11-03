   
       //------------------------------------------------------------------------------
       private RelayCommand _pdfTbl09DivisionsCommand;
	public ICommand PdfTbl09DivisionsCommand
	{
		get { return _pdfTbl09DivisionsCommand ?? (_pdfTbl09DivisionsCommand = new RelayCommand(delegate { CreatePdfTbl09Divisions(_mainId); })); }
	}

	private static void CreatePdfTbl09Divisions(int id)
	{
		ReportTbl09DivisionsPdf.CreateMainPdf(id);
	}   
       //------------------------------------------------------------------------------
       //------------------------------------------------------------------------------  
   
        public ObservableCollection< Tbl09Division> GetValueTbl09DivisionsList(int currentId)
        {
             Tbl09DivisionsList = new ObservableCollection<Tbl09Division>
                  (from y in _tbl09DivisionsRepository.GetAll()
                   where y.DivisionID == currentId 
   
                   orderby y.DivisionName   
                   select y);
            return Tbl09DivisionsList;
        } 
   
        public ObservableCollection< Tbl09Division> GetValueTbl09DivisionsList()
        {
             Tbl09DivisionsList = new ObservableCollection<Tbl09Division>
                { new ObservableCollection<Tbl09Division>
                    (from x in _tbl09DivisionsRepository.GetAll() select x).LastOrDefault()
                };
            return Tbl09DivisionsList;
        }

        #endregion  
   
        #region "Public Property  Tbl09Division"

        private ObservableCollection< Tbl09Division>  _tbl09DivisionsList;
        public ObservableCollection< Tbl09Division>  Tbl09DivisionsList
        {
            get { return  _tbl09DivisionsList; }
            set {  _tbl09DivisionsList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection< Tbl09Division>  _tbl09DivisionsAllList;
        public  ObservableCollection< Tbl09Division>  Tbl09DivisionsAllList
        {
            get { return  _tbl09DivisionsAllList; }
            set {  _tbl09DivisionsAllList = value; RaisePropertyChanged(); }
        }

        #endregion "Public Properties" 
