   
       //------------------------------------------------------------------------------
       private RelayCommand _pdfTbl90RefAuthorsCommand;
	public ICommand PdfTbl90RefAuthorsCommand
	{
		get { return _pdfTbl90RefAuthorsCommand ?? (_pdfTbl90RefAuthorsCommand = new RelayCommand(delegate { CreatePdfTbl90RefAuthors(_mainId); })); }
	}

	private static void CreatePdfTbl90RefAuthors(int id)
	{
		ReportTbl90RefAuthorsPdf.CreateMainPdf(id);
	}   
       //------------------------------------------------------------------------------
       //------------------------------------------------------------------------------  
      
        public ObservableCollection<Tbl90References> GetValueTbl90RefAuthorsList(int currentId)
        {
            Tbl90RefAuthorsList = new ObservableCollection<Tbl90References>
                (from y in _tbl90ReferencesRepository.GetAll()
                 where y.ReferenceID == currentId
                        && y.Tbl90RefExperts == null
                        && y.Tbl90RefSources == null  
      
                 orderby y.Tbl90RefAuthors.RefAuthorName, y.Tbl90RefAuthors.BookName, y.Tbl90RefAuthors.Page1  
   
                   select y);
            return Tbl90RefAuthorsList;
        } 
      
        public ObservableCollection<Tbl90References> GetValueTbl90RefAuthorsList()
        {
            Tbl90RefAuthorsList = new ObservableCollection<Tbl90References>
                { new ObservableCollection<Tbl90References>
                    (from x in _tbl90ReferencesRepository.GetAll()  select x).LastOrDefault()
                };
            return Tbl90RefAuthorsList;
        }

        #endregion  
      
        #region Tbl90Authors  ---------------------------------------

        public ObservableCollection<Tbl90RefAuthors> GetValueTbl90AuthorsList(string searchRefAuthorName)
        {
            Tbl90AuthorsList = new ObservableCollection<Tbl90RefAuthors>
               (from x in _tbl90RefAuthorsRepository.GetAll()
                where x.RefAuthorName.StartsWith(searchRefAuthorName)
                orderby x.RefAuthorName, x.BookName, x.Page1
                select x);
            return Tbl90AuthorsList;
        }


        public ObservableCollection<Tbl90RefAuthors> GetValueTbl90AuthorsAllList()
        {
            Tbl90AuthorsAllList = new ObservableCollection<Tbl90RefAuthors>
                (from z in _tbl90RefAuthorsRepository.GetAll()
                 orderby z.RefAuthorName, z.BookName, z.Page1
                 select z);
            return Tbl90AuthorsAllList;
        }

        public ObservableCollection<Tbl90RefAuthors> GetValueTbl90AuthorsList(int currentId)
        {
            Tbl90AuthorsList = new ObservableCollection<Tbl90RefAuthors>
                 (from y in _tbl90RefAuthorsRepository.GetAll()
                  where y.RefAuthorID == currentId
                  orderby y.RefAuthorName, y.BookName, y.Page1
                  select y);
            return Tbl90AuthorsList;
        }

        public ObservableCollection<Tbl90RefAuthors> GetValueTbl90AuthorsList()
        {
            Tbl90AuthorsList = new ObservableCollection<Tbl90RefAuthors>
                { new ObservableCollection<Tbl90RefAuthors>
                    (from x in _tbl90RefAuthorsRepository.GetAll()  select x).LastOrDefault()
                };
            return Tbl90AuthorsList;
        }

        #endregion     
      
        #region "Public Property  Tbl90RefAuthor"

        private ObservableCollection< Tbl90Reference>  _tbl90RefAuthorsList;
        public ObservableCollection< Tbl90Reference>  Tbl90RefAuthorsList
        {
            get { return  _tbl90RefAuthorsList; }
            set {  _tbl90RefAuthorsList = value; RaisePropertyChanged(); }
        }

        #endregion "Public Properties"     
      
        #region "Public Properties Tbl90Author"

        private ObservableCollection<Tbl90RefAuthor> _tbl90AuthorsList;
        public ObservableCollection<Tbl90RefAuthor> Tbl90AuthorsList
        {
            get { return _tbl90AuthorsList; }
            set { _tbl90AuthorsList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl90RefAuthor> _tbl90AuthorsAllList;
        public ObservableCollection<Tbl90RefAuthor> Tbl90AuthorsAllList
        {
            get { return _tbl90AuthorsAllList; }
            set { _tbl90AuthorsAllList = value; RaisePropertyChanged(); }
        }

        #endregion     
