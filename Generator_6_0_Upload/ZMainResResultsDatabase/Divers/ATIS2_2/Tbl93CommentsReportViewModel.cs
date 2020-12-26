   
       //------------------------------------------------------------------------------
       private RelayCommand _pdfTbl93CommentsCommand;
	public ICommand PdfTbl93CommentsCommand
	{
		get { return _pdfTbl93CommentsCommand ?? (_pdfTbl93CommentsCommand = new RelayCommand(delegate { CreatePdfTbl93Comments(_mainId); })); }
	}

	private static void CreatePdfTbl93Comments(int id)
	{
		ReportTbl93CommentsPdf.CreateMainPdf(id);
	}   
       //------------------------------------------------------------------------------
       //------------------------------------------------------------------------------  
   
        public ObservableCollection< Tbl93Comment> GetValueTbl93CommentsList(int currentId)
        {
             Tbl93CommentsList = new ObservableCollection<Tbl93Comment>
                  (from y in _tbl93CommentsRepository.GetAll()
                   where y.CommentID == currentId 
      
                 orderby y.Info  
   
                   select y);
            return Tbl93CommentsList;
        } 
   
        public ObservableCollection< Tbl93Comment> GetValueTbl93CommentsList()
        {
             Tbl93CommentsList = new ObservableCollection<Tbl93Comment>
                { new ObservableCollection<Tbl93Comment>
                    (from x in _tbl93CommentsRepository.GetAll() select x).LastOrDefault()
                };
            return Tbl93CommentsList;
        }

        #endregion  
   
        #region "Public Property  Tbl93Comment"

        private ObservableCollection< Tbl93Comment>  _tbl93CommentsList;
        public ObservableCollection< Tbl93Comment>  Tbl93CommentsList
        {
            get { return  _tbl93CommentsList; }
            set {  _tbl93CommentsList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection< Tbl93Comment>  _tbl93CommentsAllList;
        public  ObservableCollection< Tbl93Comment>  Tbl93CommentsAllList
        {
            get { return  _tbl93CommentsAllList; }
            set {  _tbl93CommentsAllList = value; RaisePropertyChanged(); }
        }

        #endregion "Public Properties" 
