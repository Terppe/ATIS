   
       //------------------------------------------------------------------------------
       private RelayCommand _pdfTbl06PhylumsCommand;
	public ICommand PdfTbl06PhylumsCommand
	{
		get { return _pdfTbl06PhylumsCommand ?? (_pdfTbl06PhylumsCommand = new RelayCommand(delegate { CreatePdfTbl06Phylums(_mainId); })); }
	}

	private static void CreatePdfTbl06Phylums(int id)
	{
		ReportTbl06PhylumsPdf.CreateMainPdf(id);
	}   
       //------------------------------------------------------------------------------
       //------------------------------------------------------------------------------  
   
        public ObservableCollection< Tbl06Phylum> GetValueTbl06PhylumsList(int currentId)
        {
             Tbl06PhylumsList = new ObservableCollection<Tbl06Phylum>
                  (from y in _tbl06PhylumsRepository.GetAll()
                   where y.PhylumID == currentId 
   
                   orderby y.PhylumName   
                   select y);
            return Tbl06PhylumsList;
        } 
   
        public ObservableCollection< Tbl06Phylum> GetValueTbl06PhylumsList()
        {
             Tbl06PhylumsList = new ObservableCollection<Tbl06Phylum>
                { new ObservableCollection<Tbl06Phylum>
                    (from x in _tbl06PhylumsRepository.GetAll() select x).LastOrDefault()
                };
            return Tbl06PhylumsList;
        }

        #endregion  
   
        #region "Public Property  Tbl06Phylum"

        private ObservableCollection< Tbl06Phylum>  _tbl06PhylumsList;
        public ObservableCollection< Tbl06Phylum>  Tbl06PhylumsList
        {
            get { return  _tbl06PhylumsList; }
            set {  _tbl06PhylumsList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection< Tbl06Phylum>  _tbl06PhylumsAllList;
        public  ObservableCollection< Tbl06Phylum>  Tbl06PhylumsAllList
        {
            get { return  _tbl06PhylumsAllList; }
            set {  _tbl06PhylumsAllList = value; RaisePropertyChanged(); }
        }

        #endregion "Public Properties" 
