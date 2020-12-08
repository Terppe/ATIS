   
       //------------------------------------------------------------------------------
       private RelayCommand _pdfTbl45FamiliesCommand;
	public ICommand PdfTbl45FamiliesCommand
	{
		get { return _pdfTbl45FamiliesCommand ?? (_pdfTbl45FamiliesCommand = new RelayCommand(delegate { CreatePdfTbl45Families(_mainId); })); }
	}

	private static void CreatePdfTbl45Families(int id)
	{
		ReportTbl45FamiliesPdf.CreateMainPdf(id);
	}   
       //------------------------------------------------------------------------------
       //------------------------------------------------------------------------------  
   
        public ObservableCollection< Tbl45Family> GetValueTbl45FamiliesList(int currentId)
        {
             Tbl45FamiliesList = new ObservableCollection<Tbl45Family>
                  (from y in _tbl45FamiliesRepository.GetAll()
                   where y.FamilyID == currentId 
   
                   orderby y.FamilyName   
                   select y);
            return Tbl45FamiliesList;
        } 
   
        public ObservableCollection< Tbl45Family> GetValueTbl45FamiliesList()
        {
             Tbl45FamiliesList = new ObservableCollection<Tbl45Family>
                { new ObservableCollection<Tbl45Family>
                    (from x in _tbl45FamiliesRepository.GetAll() select x).LastOrDefault()
                };
            return Tbl45FamiliesList;
        }

        #endregion  
   
        #region "Public Property  Tbl45Family"

        private ObservableCollection< Tbl45Family>  _tbl45FamiliesList;
        public ObservableCollection< Tbl45Family>  Tbl45FamiliesList
        {
            get { return  _tbl45FamiliesList; }
            set {  _tbl45FamiliesList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection< Tbl45Family>  _tbl45FamiliesAllList;
        public  ObservableCollection< Tbl45Family>  Tbl45FamiliesAllList
        {
            get { return  _tbl45FamiliesAllList; }
            set {  _tbl45FamiliesAllList = value; RaisePropertyChanged(); }
        }

        #endregion "Public Properties" 
