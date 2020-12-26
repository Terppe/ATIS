   
       //------------------------------------------------------------------------------
       private RelayCommand _pdfTbl54SupertribussesCommand;
	public ICommand PdfTbl54SupertribussesCommand
	{
		get { return _pdfTbl54SupertribussesCommand ?? (_pdfTbl54SupertribussesCommand = new RelayCommand(delegate { CreatePdfTbl54Supertribusses(_mainId); })); }
	}

	private static void CreatePdfTbl54Supertribusses(int id)
	{
		ReportTbl54SupertribussesPdf.CreateMainPdf(id);
	}   
       //------------------------------------------------------------------------------
       //------------------------------------------------------------------------------  
   
        public ObservableCollection< Tbl54Supertribus> GetValueTbl54SupertribussesList(int currentId)
        {
             Tbl54SupertribussesList = new ObservableCollection<Tbl54Supertribus>
                  (from y in _tbl54SupertribussesRepository.GetAll()
                   where y.SupertribusID == currentId 
   
                   orderby y.SupertribusName   
                   select y);
            return Tbl54SupertribussesList;
        } 
   
        public ObservableCollection< Tbl54Supertribus> GetValueTbl54SupertribussesList()
        {
             Tbl54SupertribussesList = new ObservableCollection<Tbl54Supertribus>
                { new ObservableCollection<Tbl54Supertribus>
                    (from x in _tbl54SupertribussesRepository.GetAll() select x).LastOrDefault()
                };
            return Tbl54SupertribussesList;
        }

        #endregion  
   
        #region "Public Property  Tbl54Supertribus"

        private ObservableCollection< Tbl54Supertribus>  _tbl54SupertribussesList;
        public ObservableCollection< Tbl54Supertribus>  Tbl54SupertribussesList
        {
            get { return  _tbl54SupertribussesList; }
            set {  _tbl54SupertribussesList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection< Tbl54Supertribus>  _tbl54SupertribussesAllList;
        public  ObservableCollection< Tbl54Supertribus>  Tbl54SupertribussesAllList
        {
            get { return  _tbl54SupertribussesAllList; }
            set {  _tbl54SupertribussesAllList = value; RaisePropertyChanged(); }
        }

        #endregion "Public Properties" 
