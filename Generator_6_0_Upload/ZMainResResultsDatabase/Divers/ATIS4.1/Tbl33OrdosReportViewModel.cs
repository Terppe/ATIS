   
       //------------------------------------------------------------------------------
       private RelayCommand _pdfTbl33OrdosCommand;
	public ICommand PdfTbl33OrdosCommand
	{
		get { return _pdfTbl33OrdosCommand ?? (_pdfTbl33OrdosCommand = new RelayCommand(delegate { CreatePdfTbl33Ordos(_mainId); })); }
	}

	private static void CreatePdfTbl33Ordos(int id)
	{
		ReportTbl33OrdosPdf.CreateMainPdf(id);
	}   
       //------------------------------------------------------------------------------
       //------------------------------------------------------------------------------  
   
        public ObservableCollection< Tbl33Ordo> GetValueTbl33OrdosList(int currentId)
        {
             Tbl33OrdosList = new ObservableCollection<Tbl33Ordo>
                  (from y in _tbl33OrdosRepository.GetAll()
                   where y.OrdoID == currentId 
   
                   orderby y.OrdoName   
                   select y);
            return Tbl33OrdosList;
        } 
   
        public ObservableCollection< Tbl33Ordo> GetValueTbl33OrdosList()
        {
             Tbl33OrdosList = new ObservableCollection<Tbl33Ordo>
                { new ObservableCollection<Tbl33Ordo>
                    (from x in _tbl33OrdosRepository.GetAll() select x).LastOrDefault()
                };
            return Tbl33OrdosList;
        }

        #endregion  
   
        #region "Public Property  Tbl33Ordo"

        private ObservableCollection< Tbl33Ordo>  _tbl33OrdosList;
        public ObservableCollection< Tbl33Ordo>  Tbl33OrdosList
        {
            get { return  _tbl33OrdosList; }
            set {  _tbl33OrdosList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection< Tbl33Ordo>  _tbl33OrdosAllList;
        public  ObservableCollection< Tbl33Ordo>  Tbl33OrdosAllList
        {
            get { return  _tbl33OrdosAllList; }
            set {  _tbl33OrdosAllList = value; RaisePropertyChanged(); }
        }

        #endregion "Public Properties" 
