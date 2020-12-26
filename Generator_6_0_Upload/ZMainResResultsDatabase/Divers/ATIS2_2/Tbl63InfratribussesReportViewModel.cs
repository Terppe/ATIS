   
       //------------------------------------------------------------------------------
       private RelayCommand _pdfTbl63InfratribussesCommand;
	public ICommand PdfTbl63InfratribussesCommand
	{
		get { return _pdfTbl63InfratribussesCommand ?? (_pdfTbl63InfratribussesCommand = new RelayCommand(delegate { CreatePdfTbl63Infratribusses(_mainId); })); }
	}

	private static void CreatePdfTbl63Infratribusses(int id)
	{
		ReportTbl63InfratribussesPdf.CreateMainPdf(id);
	}   
       //------------------------------------------------------------------------------
       //------------------------------------------------------------------------------  
   
        public ObservableCollection< Tbl63Infratribus> GetValueTbl63InfratribussesList(int currentId)
        {
             Tbl63InfratribussesList = new ObservableCollection<Tbl63Infratribus>
                  (from y in _tbl63InfratribussesRepository.GetAll()
                   where y.InfratribusID == currentId 
   
                   orderby y.InfratribusName   
                   select y);
            return Tbl63InfratribussesList;
        } 
   
        public ObservableCollection< Tbl63Infratribus> GetValueTbl63InfratribussesList()
        {
             Tbl63InfratribussesList = new ObservableCollection<Tbl63Infratribus>
                { new ObservableCollection<Tbl63Infratribus>
                    (from x in _tbl63InfratribussesRepository.GetAll() select x).LastOrDefault()
                };
            return Tbl63InfratribussesList;
        }

        #endregion  
   
        #region "Public Property  Tbl63Infratribus"

        private ObservableCollection< Tbl63Infratribus>  _tbl63InfratribussesList;
        public ObservableCollection< Tbl63Infratribus>  Tbl63InfratribussesList
        {
            get { return  _tbl63InfratribussesList; }
            set {  _tbl63InfratribussesList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection< Tbl63Infratribus>  _tbl63InfratribussesAllList;
        public  ObservableCollection< Tbl63Infratribus>  Tbl63InfratribussesAllList
        {
            get { return  _tbl63InfratribussesAllList; }
            set {  _tbl63InfratribussesAllList = value; RaisePropertyChanged(); }
        }

        #endregion "Public Properties" 
