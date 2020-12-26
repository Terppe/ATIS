   
       //------------------------------------------------------------------------------
       private RelayCommand _pdfTbl30LegiosCommand;
	public ICommand PdfTbl30LegiosCommand
	{
		get { return _pdfTbl30LegiosCommand ?? (_pdfTbl30LegiosCommand = new RelayCommand(delegate { CreatePdfTbl30Legios(_mainId); })); }
	}

	private static void CreatePdfTbl30Legios(int id)
	{
		ReportTbl30LegiosPdf.CreateMainPdf(id);
	}   
       //------------------------------------------------------------------------------
       //------------------------------------------------------------------------------  
   
        public ObservableCollection< Tbl30Legio> GetValueTbl30LegiosList(int currentId)
        {
             Tbl30LegiosList = new ObservableCollection<Tbl30Legio>
                  (from y in _tbl30LegiosRepository.GetAll()
                   where y.LegioID == currentId 
   
                   orderby y.LegioName   
                   select y);
            return Tbl30LegiosList;
        } 
   
        public ObservableCollection< Tbl30Legio> GetValueTbl30LegiosList()
        {
             Tbl30LegiosList = new ObservableCollection<Tbl30Legio>
                { new ObservableCollection<Tbl30Legio>
                    (from x in _tbl30LegiosRepository.GetAll() select x).LastOrDefault()
                };
            return Tbl30LegiosList;
        }

        #endregion  
   
        #region "Public Property  Tbl30Legio"

        private ObservableCollection< Tbl30Legio>  _tbl30LegiosList;
        public ObservableCollection< Tbl30Legio>  Tbl30LegiosList
        {
            get { return  _tbl30LegiosList; }
            set {  _tbl30LegiosList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection< Tbl30Legio>  _tbl30LegiosAllList;
        public  ObservableCollection< Tbl30Legio>  Tbl30LegiosAllList
        {
            get { return  _tbl30LegiosAllList; }
            set {  _tbl30LegiosAllList = value; RaisePropertyChanged(); }
        }

        #endregion "Public Properties" 
