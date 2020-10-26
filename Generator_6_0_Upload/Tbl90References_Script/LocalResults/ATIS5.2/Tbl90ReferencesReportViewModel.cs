   
       //------------------------------------------------------------------------------
       private RelayCommand _pdfTbl90ReferencesCommand;
	public ICommand PdfTbl90ReferencesCommand
	{
		get { return _pdfTbl90ReferencesCommand ?? (_pdfTbl90ReferencesCommand = new RelayCommand(delegate { CreatePdfTbl90References(_mainId); })); }
	}

	private static void CreatePdfTbl90References(int id)
	{
		ReportTbl90ReferencesPdf.CreateMainPdf(id);
	}   
       //------------------------------------------------------------------------------
       //------------------------------------------------------------------------------  
   
        public ObservableCollection< Tbl90Reference> GetValueTbl90ReferencesList(int currentId)
        {
             Tbl90ReferencesList = new ObservableCollection<Tbl90Reference>
                  (from y in _tbl90ReferencesRepository.GetAll()
                   where y.ReferenceID == currentId 
   
                   orderby y.ReferenceName   
                   select y);
            return Tbl90ReferencesList;
        } 
   
        public ObservableCollection< Tbl90Reference> GetValueTbl90ReferencesList()
        {
             Tbl90ReferencesList = new ObservableCollection<Tbl90Reference>
                { new ObservableCollection<Tbl90Reference>
                    (from x in _tbl90ReferencesRepository.GetAll() select x).LastOrDefault()
                };
            return Tbl90ReferencesList;
        }

        #endregion  
   
        #region "Public Property  Tbl90Reference"

        private ObservableCollection< Tbl90Reference>  _tbl90ReferencesList;
        public ObservableCollection< Tbl90Reference>  Tbl90ReferencesList
        {
            get { return  _tbl90ReferencesList; }
            set {  _tbl90ReferencesList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection< Tbl90Reference>  _tbl90ReferencesAllList;
        public  ObservableCollection< Tbl90Reference>  Tbl90ReferencesAllList
        {
            get { return  _tbl90ReferencesAllList; }
            set {  _tbl90ReferencesAllList = value; RaisePropertyChanged(); }
        }

        #endregion "Public Properties" 
