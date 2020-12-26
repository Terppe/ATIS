   
       //------------------------------------------------------------------------------
       private RelayCommand _pdfTbl03RegnumsCommand;
	public ICommand PdfTbl03RegnumsCommand
	{
		get { return _pdfTbl03RegnumsCommand ?? (_pdfTbl03RegnumsCommand = new RelayCommand(delegate { CreatePdfTbl03Regnums(_mainId); })); }
	}

	private static void CreatePdfTbl03Regnums(int id)
	{
		ReportTbl03RegnumsPdf.CreateMainPdf(id);
	}   
       //------------------------------------------------------------------------------
       //------------------------------------------------------------------------------  
   
        public ObservableCollection< Tbl03Regnum> GetValueTbl03RegnumsList(int currentId)
        {
             Tbl03RegnumsList = new ObservableCollection<Tbl03Regnum>
                  (from y in _tbl03RegnumsRepository.GetAll()
                   where y.RegnumID == currentId 
      
                 orderby y.RegnumName, y.Subregnum        
   
                   select y);
            return Tbl03RegnumsList;
        } 
   
        public ObservableCollection< Tbl03Regnum> GetValueTbl03RegnumsList()
        {
             Tbl03RegnumsList = new ObservableCollection<Tbl03Regnum>
                { new ObservableCollection<Tbl03Regnum>
                    (from x in _tbl03RegnumsRepository.GetAll() select x).LastOrDefault()
                };
            return Tbl03RegnumsList;
        }

        #endregion  
   
        #region "Public Property  Tbl03Regnum"

        private ObservableCollection< Tbl03Regnum>  _tbl03RegnumsList;
        public ObservableCollection< Tbl03Regnum>  Tbl03RegnumsList
        {
            get { return  _tbl03RegnumsList; }
            set {  _tbl03RegnumsList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection< Tbl03Regnum>  _tbl03RegnumsAllList;
        public  ObservableCollection< Tbl03Regnum>  Tbl03RegnumsAllList
        {
            get { return  _tbl03RegnumsAllList; }
            set {  _tbl03RegnumsAllList = value; RaisePropertyChanged(); }
        }

        #endregion "Public Properties" 
