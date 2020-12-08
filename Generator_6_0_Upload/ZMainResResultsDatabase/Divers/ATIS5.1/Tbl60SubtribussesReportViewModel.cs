   
       //------------------------------------------------------------------------------
       private RelayCommand _pdfTbl60SubtribussesCommand;
	public ICommand PdfTbl60SubtribussesCommand
	{
		get { return _pdfTbl60SubtribussesCommand ?? (_pdfTbl60SubtribussesCommand = new RelayCommand(delegate { CreatePdfTbl60Subtribusses(_mainId); })); }
	}

	private static void CreatePdfTbl60Subtribusses(int id)
	{
		ReportTbl60SubtribussesPdf.CreateMainPdf(id);
	}   
       //------------------------------------------------------------------------------
       //------------------------------------------------------------------------------  
   
        public ObservableCollection< Tbl60Subtribus> GetValueTbl60SubtribussesList(int currentId)
        {
             Tbl60SubtribussesList = new ObservableCollection<Tbl60Subtribus>
                  (from y in _tbl60SubtribussesRepository.GetAll()
                   where y.SubtribusID == currentId 
   
                   orderby y.SubtribusName   
                   select y);
            return Tbl60SubtribussesList;
        } 
   
        public ObservableCollection< Tbl60Subtribus> GetValueTbl60SubtribussesList()
        {
             Tbl60SubtribussesList = new ObservableCollection<Tbl60Subtribus>
                { new ObservableCollection<Tbl60Subtribus>
                    (from x in _tbl60SubtribussesRepository.GetAll() select x).LastOrDefault()
                };
            return Tbl60SubtribussesList;
        }

        #endregion  
   
        #region "Public Property  Tbl60Subtribus"

        private ObservableCollection< Tbl60Subtribus>  _tbl60SubtribussesList;
        public ObservableCollection< Tbl60Subtribus>  Tbl60SubtribussesList
        {
            get { return  _tbl60SubtribussesList; }
            set {  _tbl60SubtribussesList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection< Tbl60Subtribus>  _tbl60SubtribussesAllList;
        public  ObservableCollection< Tbl60Subtribus>  Tbl60SubtribussesAllList
        {
            get { return  _tbl60SubtribussesAllList; }
            set {  _tbl60SubtribussesAllList = value; RaisePropertyChanged(); }
        }

        #endregion "Public Properties" 
