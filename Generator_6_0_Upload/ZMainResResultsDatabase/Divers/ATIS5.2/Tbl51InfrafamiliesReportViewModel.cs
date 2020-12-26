   
       //------------------------------------------------------------------------------
       private RelayCommand _pdfTbl51InfrafamiliesCommand;
	public ICommand PdfTbl51InfrafamiliesCommand
	{
		get { return _pdfTbl51InfrafamiliesCommand ?? (_pdfTbl51InfrafamiliesCommand = new RelayCommand(delegate { CreatePdfTbl51Infrafamilies(_mainId); })); }
	}

	private static void CreatePdfTbl51Infrafamilies(int id)
	{
		ReportTbl51InfrafamiliesPdf.CreateMainPdf(id);
	}   
       //------------------------------------------------------------------------------
       //------------------------------------------------------------------------------  
   
        public ObservableCollection< Tbl51Infrafamily> GetValueTbl51InfrafamiliesList(int currentId)
        {
             Tbl51InfrafamiliesList = new ObservableCollection<Tbl51Infrafamily>
                  (from y in _tbl51InfrafamiliesRepository.GetAll()
                   where y.InfrafamilyID == currentId 
   
                   orderby y.InfrafamilyName   
                   select y);
            return Tbl51InfrafamiliesList;
        } 
   
        public ObservableCollection< Tbl51Infrafamily> GetValueTbl51InfrafamiliesList()
        {
             Tbl51InfrafamiliesList = new ObservableCollection<Tbl51Infrafamily>
                { new ObservableCollection<Tbl51Infrafamily>
                    (from x in _tbl51InfrafamiliesRepository.GetAll() select x).LastOrDefault()
                };
            return Tbl51InfrafamiliesList;
        }

        #endregion  
   
        #region "Public Property  Tbl51Infrafamily"

        private ObservableCollection< Tbl51Infrafamily>  _tbl51InfrafamiliesList;
        public ObservableCollection< Tbl51Infrafamily>  Tbl51InfrafamiliesList
        {
            get { return  _tbl51InfrafamiliesList; }
            set {  _tbl51InfrafamiliesList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection< Tbl51Infrafamily>  _tbl51InfrafamiliesAllList;
        public  ObservableCollection< Tbl51Infrafamily>  Tbl51InfrafamiliesAllList
        {
            get { return  _tbl51InfrafamiliesAllList; }
            set {  _tbl51InfrafamiliesAllList = value; RaisePropertyChanged(); }
        }

        #endregion "Public Properties" 
