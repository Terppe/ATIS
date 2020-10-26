   
       //------------------------------------------------------------------------------
       private RelayCommand _pdfTbl27InfraclassesCommand;
	public ICommand PdfTbl27InfraclassesCommand
	{
		get { return _pdfTbl27InfraclassesCommand ?? (_pdfTbl27InfraclassesCommand = new RelayCommand(delegate { CreatePdfTbl27Infraclasses(_mainId); })); }
	}

	private static void CreatePdfTbl27Infraclasses(int id)
	{
		ReportTbl27InfraclassesPdf.CreateMainPdf(id);
	}   
       //------------------------------------------------------------------------------
       //------------------------------------------------------------------------------  
   
        public ObservableCollection< Tbl27Infraclass> GetValueTbl27InfraclassesList(int currentId)
        {
             Tbl27InfraclassesList = new ObservableCollection<Tbl27Infraclass>
                  (from y in _tbl27InfraclassesRepository.GetAll()
                   where y.InfraclassID == currentId 
   
                   orderby y.InfraclassName   
                   select y);
            return Tbl27InfraclassesList;
        } 
   
        public ObservableCollection< Tbl27Infraclass> GetValueTbl27InfraclassesList()
        {
             Tbl27InfraclassesList = new ObservableCollection<Tbl27Infraclass>
                { new ObservableCollection<Tbl27Infraclass>
                    (from x in _tbl27InfraclassesRepository.GetAll() select x).LastOrDefault()
                };
            return Tbl27InfraclassesList;
        }

        #endregion  
   
        #region "Public Property  Tbl27Infraclass"

        private ObservableCollection< Tbl27Infraclass>  _tbl27InfraclassesList;
        public ObservableCollection< Tbl27Infraclass>  Tbl27InfraclassesList
        {
            get { return  _tbl27InfraclassesList; }
            set {  _tbl27InfraclassesList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection< Tbl27Infraclass>  _tbl27InfraclassesAllList;
        public  ObservableCollection< Tbl27Infraclass>  Tbl27InfraclassesAllList
        {
            get { return  _tbl27InfraclassesAllList; }
            set {  _tbl27InfraclassesAllList = value; RaisePropertyChanged(); }
        }

        #endregion "Public Properties" 
