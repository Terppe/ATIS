   
       //------------------------------------------------------------------------------
       private RelayCommand _pdfTbl68SpeciesgroupsCommand;
	public ICommand PdfTbl68SpeciesgroupsCommand
	{
		get { return _pdfTbl68SpeciesgroupsCommand ?? (_pdfTbl68SpeciesgroupsCommand = new RelayCommand(delegate { CreatePdfTbl68Speciesgroups(_mainId); })); }
	}

	private static void CreatePdfTbl68Speciesgroups(int id)
	{
		ReportTbl68SpeciesgroupsPdf.CreateMainPdf(id);
	}   
       //------------------------------------------------------------------------------
       //------------------------------------------------------------------------------  
   
        public ObservableCollection< Tbl68Speciesgroup> GetValueTbl68SpeciesgroupsList(int currentId)
        {
             Tbl68SpeciesgroupsList = new ObservableCollection<Tbl68Speciesgroup>
                  (from y in _tbl68SpeciesgroupsRepository.GetAll()
                   where y.SpeciesgroupID == currentId 
      
                 orderby y.SpeciesgroupName, y.Subspeciesgroup        
   
                   select y);
            return Tbl68SpeciesgroupsList;
        } 
   
        public ObservableCollection< Tbl68Speciesgroup> GetValueTbl68SpeciesgroupsList()
        {
             Tbl68SpeciesgroupsList = new ObservableCollection<Tbl68Speciesgroup>
                { new ObservableCollection<Tbl68Speciesgroup>
                    (from x in _tbl68SpeciesgroupsRepository.GetAll() select x).LastOrDefault()
                };
            return Tbl68SpeciesgroupsList;
        }

        #endregion  
   
        #region "Public Property  Tbl68Speciesgroup"

        private ObservableCollection< Tbl68Speciesgroup>  _tbl68SpeciesgroupsList;
        public ObservableCollection< Tbl68Speciesgroup>  Tbl68SpeciesgroupsList
        {
            get { return  _tbl68SpeciesgroupsList; }
            set {  _tbl68SpeciesgroupsList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection< Tbl68Speciesgroup>  _tbl68SpeciesgroupsAllList;
        public  ObservableCollection< Tbl68Speciesgroup>  Tbl68SpeciesgroupsAllList
        {
            get { return  _tbl68SpeciesgroupsAllList; }
            set {  _tbl68SpeciesgroupsAllList = value; RaisePropertyChanged(); }
        }

        #endregion "Public Properties" 
