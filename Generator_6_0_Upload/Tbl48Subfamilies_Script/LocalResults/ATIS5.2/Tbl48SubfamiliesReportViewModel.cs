   
       //------------------------------------------------------------------------------
       private RelayCommand _pdfTbl48SubfamiliesCommand;
	public ICommand PdfTbl48SubfamiliesCommand
	{
		get { return _pdfTbl48SubfamiliesCommand ?? (_pdfTbl48SubfamiliesCommand = new RelayCommand(delegate { CreatePdfTbl48Subfamilies(_mainId); })); }
	}

	private static void CreatePdfTbl48Subfamilies(int id)
	{
		ReportTbl48SubfamiliesPdf.CreateMainPdf(id);
	}   
       //------------------------------------------------------------------------------
       //------------------------------------------------------------------------------  
   
        public ObservableCollection< Tbl48Subfamily> GetValueTbl48SubfamiliesList(int currentId)
        {
             Tbl48SubfamiliesList = new ObservableCollection<Tbl48Subfamily>
                  (from y in _tbl48SubfamiliesRepository.GetAll()
                   where y.SubfamilyID == currentId 
   
                   orderby y.SubfamilyName   
                   select y);
            return Tbl48SubfamiliesList;
        } 
   
        public ObservableCollection< Tbl48Subfamily> GetValueTbl48SubfamiliesList()
        {
             Tbl48SubfamiliesList = new ObservableCollection<Tbl48Subfamily>
                { new ObservableCollection<Tbl48Subfamily>
                    (from x in _tbl48SubfamiliesRepository.GetAll() select x).LastOrDefault()
                };
            return Tbl48SubfamiliesList;
        }

        #endregion  
   
        #region "Public Property  Tbl48Subfamily"

        private ObservableCollection< Tbl48Subfamily>  _tbl48SubfamiliesList;
        public ObservableCollection< Tbl48Subfamily>  Tbl48SubfamiliesList
        {
            get { return  _tbl48SubfamiliesList; }
            set {  _tbl48SubfamiliesList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection< Tbl48Subfamily>  _tbl48SubfamiliesAllList;
        public  ObservableCollection< Tbl48Subfamily>  Tbl48SubfamiliesAllList
        {
            get { return  _tbl48SubfamiliesAllList; }
            set {  _tbl48SubfamiliesAllList = value; RaisePropertyChanged(); }
        }

        #endregion "Public Properties" 
