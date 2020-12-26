   
       //------------------------------------------------------------------------------
       private RelayCommand _pdfTbl21ClassesCommand;
	public ICommand PdfTbl21ClassesCommand
	{
		get { return _pdfTbl21ClassesCommand ?? (_pdfTbl21ClassesCommand = new RelayCommand(delegate { CreatePdfTbl21Classes(_mainId); })); }
	}

	private static void CreatePdfTbl21Classes(int id)
	{
		ReportTbl21ClassesPdf.CreateMainPdf(id);
	}   
       //------------------------------------------------------------------------------
       //------------------------------------------------------------------------------  
   
        public ObservableCollection< Tbl21Class> GetValueTbl21ClassesList(int currentId)
        {
             Tbl21ClassesList = new ObservableCollection<Tbl21Class>
                  (from y in _tbl21ClassesRepository.GetAll()
                   where y.ClassID == currentId 
   
                   orderby y.ClassName   
                   select y);
            return Tbl21ClassesList;
        } 
   
        public ObservableCollection< Tbl21Class> GetValueTbl21ClassesList()
        {
             Tbl21ClassesList = new ObservableCollection<Tbl21Class>
                { new ObservableCollection<Tbl21Class>
                    (from x in _tbl21ClassesRepository.GetAll() select x).LastOrDefault()
                };
            return Tbl21ClassesList;
        }

        #endregion  
   
        #region "Public Property  Tbl21Class"

        private ObservableCollection< Tbl21Class>  _tbl21ClassesList;
        public ObservableCollection< Tbl21Class>  Tbl21ClassesList
        {
            get { return  _tbl21ClassesList; }
            set {  _tbl21ClassesList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection< Tbl21Class>  _tbl21ClassesAllList;
        public  ObservableCollection< Tbl21Class>  Tbl21ClassesAllList
        {
            get { return  _tbl21ClassesAllList; }
            set {  _tbl21ClassesAllList = value; RaisePropertyChanged(); }
        }

        #endregion "Public Properties" 
