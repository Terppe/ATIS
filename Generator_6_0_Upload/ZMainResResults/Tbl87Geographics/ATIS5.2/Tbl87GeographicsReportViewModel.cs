   
       //------------------------------------------------------------------------------
       private RelayCommand _pdfTbl87GeographicsCommand;
	public ICommand PdfTbl87GeographicsCommand
	{
		get { return _pdfTbl87GeographicsCommand ?? (_pdfTbl87GeographicsCommand = new RelayCommand(delegate { CreatePdfTbl87Geographics(_mainId); })); }
	}

	private static void CreatePdfTbl87Geographics(int id)
	{
		ReportTbl87GeographicsPdf.CreateMainPdf(id);
	}   
       //------------------------------------------------------------------------------
       //------------------------------------------------------------------------------  
   
        public ObservableCollection< Tbl87Geographic> GetValueTbl87GeographicsList(int currentId)
        {
             Tbl87GeographicsList = new ObservableCollection<Tbl87Geographic>
                  (from y in _tbl87GeographicsRepository.GetAll()
                   where y.GeographicID == currentId 
      
                 orderby y.Info  
   
                   select y);
            return Tbl87GeographicsList;
        } 
   
        public ObservableCollection< Tbl87Geographic> GetValueTbl87GeographicsList()
        {
             Tbl87GeographicsList = new ObservableCollection<Tbl87Geographic>
                { new ObservableCollection<Tbl87Geographic>
                    (from x in _tbl87GeographicsRepository.GetAll() select x).LastOrDefault()
                };
            return Tbl87GeographicsList;
        }

        #endregion  
   
        #region "Public Property  Tbl87Geographic"

        private ObservableCollection< Tbl87Geographic>  _tbl87GeographicsList;
        public ObservableCollection< Tbl87Geographic>  Tbl87GeographicsList
        {
            get { return  _tbl87GeographicsList; }
            set {  _tbl87GeographicsList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection< Tbl87Geographic>  _tbl87GeographicsAllList;
        public  ObservableCollection< Tbl87Geographic>  Tbl87GeographicsAllList
        {
            get { return  _tbl87GeographicsAllList; }
            set {  _tbl87GeographicsAllList = value; RaisePropertyChanged(); }
        }

        #endregion "Public Properties" 
