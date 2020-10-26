   
       //------------------------------------------------------------------------------
       private RelayCommand _pdfTbl84SynonymsCommand;
	public ICommand PdfTbl84SynonymsCommand
	{
		get { return _pdfTbl84SynonymsCommand ?? (_pdfTbl84SynonymsCommand = new RelayCommand(delegate { CreatePdfTbl84Synonyms(_mainId); })); }
	}

	private static void CreatePdfTbl84Synonyms(int id)
	{
		ReportTbl84SynonymsPdf.CreateMainPdf(id);
	}   
       //------------------------------------------------------------------------------
       //------------------------------------------------------------------------------  
   
        public ObservableCollection< Tbl84Synonym> GetValueTbl84SynonymsList(int currentId)
        {
             Tbl84SynonymsList = new ObservableCollection<Tbl84Synonym>
                  (from y in _tbl84SynonymsRepository.GetAll()
                   where y.SynonymID == currentId 
   
                   orderby y.SynonymName   
                   select y);
            return Tbl84SynonymsList;
        } 
   
        public ObservableCollection< Tbl84Synonym> GetValueTbl84SynonymsList()
        {
             Tbl84SynonymsList = new ObservableCollection<Tbl84Synonym>
                { new ObservableCollection<Tbl84Synonym>
                    (from x in _tbl84SynonymsRepository.GetAll() select x).LastOrDefault()
                };
            return Tbl84SynonymsList;
        }

        #endregion  
   
        #region "Public Property  Tbl84Synonym"

        private ObservableCollection< Tbl84Synonym>  _tbl84SynonymsList;
        public ObservableCollection< Tbl84Synonym>  Tbl84SynonymsList
        {
            get { return  _tbl84SynonymsList; }
            set {  _tbl84SynonymsList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection< Tbl84Synonym>  _tbl84SynonymsAllList;
        public  ObservableCollection< Tbl84Synonym>  Tbl84SynonymsAllList
        {
            get { return  _tbl84SynonymsAllList; }
            set {  _tbl84SynonymsAllList = value; RaisePropertyChanged(); }
        }

        #endregion "Public Properties" 
