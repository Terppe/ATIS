   
       //------------------------------------------------------------------------------
       private RelayCommand _pdfTbl36SubordosCommand;
	public ICommand PdfTbl36SubordosCommand
	{
		get { return _pdfTbl36SubordosCommand ?? (_pdfTbl36SubordosCommand = new RelayCommand(delegate { CreatePdfTbl36Subordos(_mainId); })); }
	}

	private static void CreatePdfTbl36Subordos(int id)
	{
		ReportTbl36SubordosPdf.CreateMainPdf(id);
	}   
       //------------------------------------------------------------------------------
       //------------------------------------------------------------------------------  
   
        public ObservableCollection< Tbl36Subordo> GetValueTbl36SubordosList(int currentId)
        {
             Tbl36SubordosList = new ObservableCollection<Tbl36Subordo>
                  (from y in _tbl36SubordosRepository.GetAll()
                   where y.SubordoID == currentId 
   
                   orderby y.SubordoName   
                   select y);
            return Tbl36SubordosList;
        } 
   
        public ObservableCollection< Tbl36Subordo> GetValueTbl36SubordosList()
        {
             Tbl36SubordosList = new ObservableCollection<Tbl36Subordo>
                { new ObservableCollection<Tbl36Subordo>
                    (from x in _tbl36SubordosRepository.GetAll() select x).LastOrDefault()
                };
            return Tbl36SubordosList;
        }

        #endregion  
   
        #region "Public Property  Tbl36Subordo"

        private ObservableCollection< Tbl36Subordo>  _tbl36SubordosList;
        public ObservableCollection< Tbl36Subordo>  Tbl36SubordosList
        {
            get { return  _tbl36SubordosList; }
            set {  _tbl36SubordosList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection< Tbl36Subordo>  _tbl36SubordosAllList;
        public  ObservableCollection< Tbl36Subordo>  Tbl36SubordosAllList
        {
            get { return  _tbl36SubordosAllList; }
            set {  _tbl36SubordosAllList = value; RaisePropertyChanged(); }
        }

        #endregion "Public Properties" 
