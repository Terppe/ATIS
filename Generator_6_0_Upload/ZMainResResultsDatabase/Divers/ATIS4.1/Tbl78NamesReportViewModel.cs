   
       //------------------------------------------------------------------------------
       private RelayCommand _pdfTbl78NamesCommand;
	public ICommand PdfTbl78NamesCommand
	{
		get { return _pdfTbl78NamesCommand ?? (_pdfTbl78NamesCommand = new RelayCommand(delegate { CreatePdfTbl78Names(_mainId); })); }
	}

	private static void CreatePdfTbl78Names(int id)
	{
		ReportTbl78NamesPdf.CreateMainPdf(id);
	}   
       //------------------------------------------------------------------------------
       //------------------------------------------------------------------------------  
   
        public ObservableCollection< Tbl78Name> GetValueTbl78NamesList(int currentId)
        {
             Tbl78NamesList = new ObservableCollection<Tbl78Name>
                  (from y in _tbl78NamesRepository.GetAll()
                   where y.NameID == currentId 
   
                   orderby y.NameName   
                   select y);
            return Tbl78NamesList;
        } 
   
        public ObservableCollection< Tbl78Name> GetValueTbl78NamesList()
        {
             Tbl78NamesList = new ObservableCollection<Tbl78Name>
                { new ObservableCollection<Tbl78Name>
                    (from x in _tbl78NamesRepository.GetAll() select x).LastOrDefault()
                };
            return Tbl78NamesList;
        }

        #endregion  
   
        #region "Public Property  Tbl78Name"

        private ObservableCollection< Tbl78Name>  _tbl78NamesList;
        public ObservableCollection< Tbl78Name>  Tbl78NamesList
        {
            get { return  _tbl78NamesList; }
            set {  _tbl78NamesList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection< Tbl78Name>  _tbl78NamesAllList;
        public  ObservableCollection< Tbl78Name>  Tbl78NamesAllList
        {
            get { return  _tbl78NamesAllList; }
            set {  _tbl78NamesAllList = value; RaisePropertyChanged(); }
        }

        #endregion "Public Properties" 
