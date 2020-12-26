   
       //------------------------------------------------------------------------------
       private RelayCommand _pdfTbl18SuperclassesCommand;
	public ICommand PdfTbl18SuperclassesCommand
	{
		get { return _pdfTbl18SuperclassesCommand ?? (_pdfTbl18SuperclassesCommand = new RelayCommand(delegate { CreatePdfTbl18Superclasses(_mainId); })); }
	}

	private static void CreatePdfTbl18Superclasses(int id)
	{
		ReportTbl18SuperclassesPdf.CreateMainPdf(id);
	}   
       //------------------------------------------------------------------------------
       //------------------------------------------------------------------------------  
   
        public ObservableCollection< Tbl18Superclass> GetValueTbl18SuperclassesList(int currentId)
        {
             Tbl18SuperclassesList = new ObservableCollection<Tbl18Superclass>
                  (from y in _tbl18SuperclassesRepository.GetAll()
                   where y.SuperclassID == currentId 
   
                   orderby y.SuperclassName   
                   select y);
            return Tbl18SuperclassesList;
        } 
   
        public ObservableCollection< Tbl18Superclass> GetValueTbl18SuperclassesList()
        {
             Tbl18SuperclassesList = new ObservableCollection<Tbl18Superclass>
                { new ObservableCollection<Tbl18Superclass>
                    (from x in _tbl18SuperclassesRepository.GetAll() select x).LastOrDefault()
                };
            return Tbl18SuperclassesList;
        }

        #endregion  
   
        #region "Public Property  Tbl18Superclass"

        private ObservableCollection< Tbl18Superclass>  _tbl18SuperclassesList;
        public ObservableCollection< Tbl18Superclass>  Tbl18SuperclassesList
        {
            get { return  _tbl18SuperclassesList; }
            set {  _tbl18SuperclassesList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection< Tbl18Superclass>  _tbl18SuperclassesAllList;
        public  ObservableCollection< Tbl18Superclass>  Tbl18SuperclassesAllList
        {
            get { return  _tbl18SuperclassesAllList; }
            set {  _tbl18SuperclassesAllList = value; RaisePropertyChanged(); }
        }

        #endregion "Public Properties" 
