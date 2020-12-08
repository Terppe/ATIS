   
       //------------------------------------------------------------------------------
       private RelayCommand _pdfTbl90RefSourcesCommand;
	public ICommand PdfTbl90RefSourcesCommand
	{
		get { return _pdfTbl90RefSourcesCommand ?? (_pdfTbl90RefSourcesCommand = new RelayCommand(delegate { CreatePdfTbl90RefSources(_mainId); })); }
	}

	private static void CreatePdfTbl90RefSources(int id)
	{
		ReportTbl90RefSourcesPdf.CreateMainPdf(id);
	}   
       //------------------------------------------------------------------------------
       //------------------------------------------------------------------------------  
      
        public ObservableCollection<Tbl90References> GetValueTbl90RefSourcesList(int currentId)
        {
            Tbl90RefSourcesList = new ObservableCollection<Tbl90References>
                (from y in _tbl90ReferencesRepository.GetAll()
                 where y.ReferenceID == currentId
                        && y.Tbl90RefExperts == null
                        && y.Tbl90RefAuthors == null  
      
                 orderby y.Tbl90RefSources.RefSourceName, y.Tbl90RefSources.Author, y.Tbl90RefSources.SourceYear  
   
                   select y);
            return Tbl90RefSourcesList;
        } 
      
        public ObservableCollection<Tbl90References> GetValueTbl90RefSourcesList()
        {
            Tbl90RefSourcesList = new ObservableCollection<Tbl90References>
                { new ObservableCollection<Tbl90References>
                    (from x in tbl90ReferencesRepository.GetAll()  select x).LastOrDefault()
                };
            return Tbl90RefSourcesList;
        }

        #endregion  
      
        #region Tbl90Sources  ---------------------------------------


        public ObservableCollection<Tbl90RefSources> GetValueTbl90SourcesList(string searchRefSourceName)
        {
            Tbl90SourcesList = new ObservableCollection<Tbl90RefSources>
               (from x in _tbl90RefSourcesRepository.GetAll()
                where x.RefSourceName.StartsWith(searchRefSourceName)
                orderby x.RefSourceName, x.Author, x.SourceYear
                select x);
            return Tbl90SourcesList;
        }

        public ObservableCollection<Tbl90RefSources> GetValueTbl90SourcesAllList()
        {
            Tbl90SourcesAllList = new ObservableCollection<Tbl90RefSources>
               (from z in _tbl90RefSourcesRepository.GetAll()
                orderby z.RefSourceName, z.Author, z.SourceYear
                select z);
            return Tbl90SourcesAllList;
        }

        public ObservableCollection<Tbl90RefSources> GetValueTbl90SourcesList(int currentId)
        {
            Tbl90SourcesList = new ObservableCollection<Tbl90RefSources>
                 (from y in _tbl90RefSourcesRepository.GetAll()
                  where y.RefSourceID == currentId
                  orderby y.RefSourceName, y.Author, y.SourceYear
                  select y);
            return Tbl90SourcesList;
        }

        public ObservableCollection<Tbl90RefSources> GetValueTbl90SourcesList()
        {
            Tbl90SourcesList = new ObservableCollection<Tbl90RefSources>
                { new ObservableCollection<Tbl90RefSources>
                    (from x in _tbl90RefSourcesV.GetAll() select x).LastOrDefault()
                };
            return Tbl90SourcesList;
        }

        #endregion    
      
        #region "Public Property  Tbl90RefSource"

        private ObservableCollection<Tbl90Reference> _tbl90RefSourcesList;
        public ObservableCollection<Tbl90Reference> Tbl90RefSourcesList
        {
            get { return _tbl90RefSourcesList; }
            set { _tbl90RefSourcesList = value; RaisePropertyChanged(); }
        }


        #endregion "Public Properties"     
      
        #region "Public Property  Tbl90RefSource"

        private ObservableCollection<Tbl90RefSource> _tbl90SourcesList;
        public ObservableCollection<Tbl90RefSource> Tbl90SourcesList
        {
            get { return _tbl90SourcesList; }
            set { _tbl90SourcesList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl90RefSource> _tbl90SourcesAllList;
        public ObservableCollection<Tbl90RefSource> Tbl90SourcesAllList
        {
            get { return _tbl90SourcesAllList; }
            set { _tbl90SourcesAllList = value; RaisePropertyChanged(); }
        }

        #endregion "Public Properties"    
