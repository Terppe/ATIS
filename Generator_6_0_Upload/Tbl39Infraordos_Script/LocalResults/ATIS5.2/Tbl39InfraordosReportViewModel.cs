   
       //------------------------------------------------------------------------------
       private RelayCommand _pdfTbl39InfraordosCommand;
	public ICommand PdfTbl39InfraordosCommand
	{
		get { return _pdfTbl39InfraordosCommand ?? (_pdfTbl39InfraordosCommand = new RelayCommand(delegate { CreatePdfTbl39Infraordos(_mainId); })); }
	}

	private static void CreatePdfTbl39Infraordos(int id)
	{
		ReportTbl39InfraordosPdf.CreateMainPdf(id);
	}   
       //------------------------------------------------------------------------------
       //------------------------------------------------------------------------------  
   
        public ObservableCollection< Tbl39Infraordo> GetValueTbl39InfraordosList(int currentId)
        {
             Tbl39InfraordosList = new ObservableCollection<Tbl39Infraordo>
                  (from y in _tbl39InfraordosRepository.GetAll()
                   where y.InfraordoID == currentId 
   
                   orderby y.InfraordoName   
                   select y);
            return Tbl39InfraordosList;
        } 
   
        public ObservableCollection< Tbl39Infraordo> GetValueTbl39InfraordosList()
        {
             Tbl39InfraordosList = new ObservableCollection<Tbl39Infraordo>
                { new ObservableCollection<Tbl39Infraordo>
                    (from x in _tbl39InfraordosRepository.GetAll() select x).LastOrDefault()
                };
            return Tbl39InfraordosList;
        }

        #endregion  
   
        #region "Public Property  Tbl39Infraordo"

        private ObservableCollection< Tbl39Infraordo>  _tbl39InfraordosList;
        public ObservableCollection< Tbl39Infraordo>  Tbl39InfraordosList
        {
            get { return  _tbl39InfraordosList; }
            set {  _tbl39InfraordosList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection< Tbl39Infraordo>  _tbl39InfraordosAllList;
        public  ObservableCollection< Tbl39Infraordo>  Tbl39InfraordosAllList
        {
            get { return  _tbl39InfraordosAllList; }
            set {  _tbl39InfraordosAllList = value; RaisePropertyChanged(); }
        }

        #endregion "Public Properties" 
