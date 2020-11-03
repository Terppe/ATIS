   
       //------------------------------------------------------------------------------
       private RelayCommand _pdfTbl12SubphylumsCommand;
	public ICommand PdfTbl12SubphylumsCommand
	{
		get { return _pdfTbl12SubphylumsCommand ?? (_pdfTbl12SubphylumsCommand = new RelayCommand(delegate { CreatePdfTbl12Subphylums(_mainId); })); }
	}

	private static void CreatePdfTbl12Subphylums(int id)
	{
		ReportTbl12SubphylumsPdf.CreateMainPdf(id);
	}   
       //------------------------------------------------------------------------------
       //------------------------------------------------------------------------------  
   
        public ObservableCollection< Tbl12Subphylum> GetValueTbl12SubphylumsList(int currentId)
        {
             Tbl12SubphylumsList = new ObservableCollection<Tbl12Subphylum>
                  (from y in _tbl12SubphylumsRepository.GetAll()
                   where y.SubphylumId == currentId 
   
                   orderby y.SubphylumName   
                   select y);
            return Tbl12SubphylumsList;
        } 
   
        public ObservableCollection< Tbl12Subphylum> GetValueTbl12SubphylumsList()
        {
             Tbl12SubphylumsList = new ObservableCollection<Tbl12Subphylum>
                { new ObservableCollection<Tbl12Subphylum>
                    (from x in _tbl12SubphylumsRepository.GetAll() select x).LastOrDefault()
                };
            return Tbl12SubphylumsList;
        }

        #endregion  
   
        #region "Public Property  Tbl12Subphylum"

        private ObservableCollection< Tbl12Subphylum>  _tbl12SubphylumsList;
        public ObservableCollection< Tbl12Subphylum>  Tbl12SubphylumsList
        {
            get { return  _tbl12SubphylumsList; }
            set {  _tbl12SubphylumsList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection< Tbl12Subphylum>  _tbl12SubphylumsAllList;
        public  ObservableCollection< Tbl12Subphylum>  Tbl12SubphylumsAllList
        {
            get { return  _tbl12SubphylumsAllList; }
            set {  _tbl12SubphylumsAllList = value; RaisePropertyChanged(); }
        }

        #endregion "Public Properties" 
