   
       //------------------------------------------------------------------------------
       private RelayCommand _pdfTbl57TribussesCommand;
	public ICommand PdfTbl57TribussesCommand
	{
		get { return _pdfTbl57TribussesCommand ?? (_pdfTbl57TribussesCommand = new RelayCommand(delegate { CreatePdfTbl57Tribusses(_mainId); })); }
	}

	private static void CreatePdfTbl57Tribusses(int id)
	{
		ReportTbl57TribussesPdf.CreateMainPdf(id);
	}   
       //------------------------------------------------------------------------------
       //------------------------------------------------------------------------------  
   
        public ObservableCollection< Tbl57Tribus> GetValueTbl57TribussesList(int currentId)
        {
             Tbl57TribussesList = new ObservableCollection<Tbl57Tribus>
                  (from y in _tbl57TribussesRepository.GetAll()
                   where y.TribusID == currentId 
   
                   orderby y.TribusName   
                   select y);
            return Tbl57TribussesList;
        } 
   
        public ObservableCollection< Tbl57Tribus> GetValueTbl57TribussesList()
        {
             Tbl57TribussesList = new ObservableCollection<Tbl57Tribus>
                { new ObservableCollection<Tbl57Tribus>
                    (from x in _tbl57TribussesRepository.GetAll() select x).LastOrDefault()
                };
            return Tbl57TribussesList;
        }

        #endregion  
   
        #region "Public Property  Tbl57Tribus"

        private ObservableCollection< Tbl57Tribus>  _tbl57TribussesList;
        public ObservableCollection< Tbl57Tribus>  Tbl57TribussesList
        {
            get { return  _tbl57TribussesList; }
            set {  _tbl57TribussesList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection< Tbl57Tribus>  _tbl57TribussesAllList;
        public  ObservableCollection< Tbl57Tribus>  Tbl57TribussesAllList
        {
            get { return  _tbl57TribussesAllList; }
            set {  _tbl57TribussesAllList = value; RaisePropertyChanged(); }
        }

        #endregion "Public Properties" 
