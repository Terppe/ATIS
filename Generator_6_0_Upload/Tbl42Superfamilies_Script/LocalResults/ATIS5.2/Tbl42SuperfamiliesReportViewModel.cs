   
       //------------------------------------------------------------------------------
       private RelayCommand _pdfTbl42SuperfamiliesCommand;
	public ICommand PdfTbl42SuperfamiliesCommand
	{
		get { return _pdfTbl42SuperfamiliesCommand ?? (_pdfTbl42SuperfamiliesCommand = new RelayCommand(delegate { CreatePdfTbl42Superfamilies(_mainId); })); }
	}

	private static void CreatePdfTbl42Superfamilies(int id)
	{
		ReportTbl42SuperfamiliesPdf.CreateMainPdf(id);
	}   
       //------------------------------------------------------------------------------
       //------------------------------------------------------------------------------  
   
        public ObservableCollection< Tbl42Superfamily> GetValueTbl42SuperfamiliesList(int currentId)
        {
             Tbl42SuperfamiliesList = new ObservableCollection<Tbl42Superfamily>
                  (from y in _tbl42SuperfamiliesRepository.GetAll()
                   where y.SuperfamilyID == currentId 
   
                   orderby y.SuperfamilyName   
                   select y);
            return Tbl42SuperfamiliesList;
        } 
   
        public ObservableCollection< Tbl42Superfamily> GetValueTbl42SuperfamiliesList()
        {
             Tbl42SuperfamiliesList = new ObservableCollection<Tbl42Superfamily>
                { new ObservableCollection<Tbl42Superfamily>
                    (from x in _tbl42SuperfamiliesRepository.GetAll() select x).LastOrDefault()
                };
            return Tbl42SuperfamiliesList;
        }

        #endregion  
   
        #region "Public Property  Tbl42Superfamily"

        private ObservableCollection< Tbl42Superfamily>  _tbl42SuperfamiliesList;
        public ObservableCollection< Tbl42Superfamily>  Tbl42SuperfamiliesList
        {
            get { return  _tbl42SuperfamiliesList; }
            set {  _tbl42SuperfamiliesList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection< Tbl42Superfamily>  _tbl42SuperfamiliesAllList;
        public  ObservableCollection< Tbl42Superfamily>  Tbl42SuperfamiliesAllList
        {
            get { return  _tbl42SuperfamiliesAllList; }
            set {  _tbl42SuperfamiliesAllList = value; RaisePropertyChanged(); }
        }

        #endregion "Public Properties" 
