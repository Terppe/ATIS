   
       //------------------------------------------------------------------------------
       private RelayCommand _pdfTbl24SubclassesCommand;
	public ICommand PdfTbl24SubclassesCommand
	{
		get { return _pdfTbl24SubclassesCommand ?? (_pdfTbl24SubclassesCommand = new RelayCommand(delegate { CreatePdfTbl24Subclasses(_mainId); })); }
	}

	private static void CreatePdfTbl24Subclasses(int id)
	{
		ReportTbl24SubclassesPdf.CreateMainPdf(id);
	}   
       //------------------------------------------------------------------------------
       //------------------------------------------------------------------------------  
   
        public ObservableCollection< Tbl24Subclass> GetValueTbl24SubclassesList(int currentId)
        {
             Tbl24SubclassesList = new ObservableCollection<Tbl24Subclass>
                  (from y in _tbl24SubclassesRepository.GetAll()
                   where y.SubclassID == currentId 
   
                   orderby y.SubclassName   
                   select y);
            return Tbl24SubclassesList;
        } 
   
        public ObservableCollection< Tbl24Subclass> GetValueTbl24SubclassesList()
        {
             Tbl24SubclassesList = new ObservableCollection<Tbl24Subclass>
                { new ObservableCollection<Tbl24Subclass>
                    (from x in _tbl24SubclassesRepository.GetAll() select x).LastOrDefault()
                };
            return Tbl24SubclassesList;
        }

        #endregion  
   
        #region "Public Property  Tbl24Subclass"

        private ObservableCollection< Tbl24Subclass>  _tbl24SubclassesList;
        public ObservableCollection< Tbl24Subclass>  Tbl24SubclassesList
        {
            get { return  _tbl24SubclassesList; }
            set {  _tbl24SubclassesList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection< Tbl24Subclass>  _tbl24SubclassesAllList;
        public  ObservableCollection< Tbl24Subclass>  Tbl24SubclassesAllList
        {
            get { return  _tbl24SubclassesAllList; }
            set {  _tbl24SubclassesAllList = value; RaisePropertyChanged(); }
        }

        #endregion "Public Properties" 
