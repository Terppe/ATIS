   
       //------------------------------------------------------------------------------
       private RelayCommand _pdfTbl81ImagesCommand;
	public ICommand PdfTbl81ImagesCommand
	{
		get { return _pdfTbl81ImagesCommand ?? (_pdfTbl81ImagesCommand = new RelayCommand(delegate { CreatePdfTbl81Images(_mainId); })); }
	}

	private static void CreatePdfTbl81Images(int id)
	{
		ReportTbl81ImagesPdf.CreateMainPdf(id);
	}   
       //------------------------------------------------------------------------------
       //------------------------------------------------------------------------------  
   
        public ObservableCollection< Tbl81Image> GetValueTbl81ImagesList(int currentId)
        {
             Tbl81ImagesList = new ObservableCollection<Tbl81Image>
                  (from y in _tbl81ImagesRepository.GetAll()
                   where y.ImageID == currentId 
      
                 orderby y.Info  
   
                   select y);
            return Tbl81ImagesList;
        } 
   
        public ObservableCollection< Tbl81Image> GetValueTbl81ImagesList()
        {
             Tbl81ImagesList = new ObservableCollection<Tbl81Image>
                { new ObservableCollection<Tbl81Image>
                    (from x in _tbl81ImagesRepository.GetAll() select x).LastOrDefault()
                };
            return Tbl81ImagesList;
        }

        #endregion  
   
        #region "Public Property  Tbl81Image"

        private ObservableCollection< Tbl81Image>  _tbl81ImagesList;
        public ObservableCollection< Tbl81Image>  Tbl81ImagesList
        {
            get { return  _tbl81ImagesList; }
            set {  _tbl81ImagesList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection< Tbl81Image>  _tbl81ImagesAllList;
        public  ObservableCollection< Tbl81Image>  Tbl81ImagesAllList
        {
            get { return  _tbl81ImagesAllList; }
            set {  _tbl81ImagesAllList = value; RaisePropertyChanged(); }
        }

        #endregion "Public Properties" 
