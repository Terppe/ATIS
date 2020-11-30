   
        public void GetTbl03RegnumsById(int id)
        {
            RegnumsCollection = _extReportBasicGet.CollRegnumsByRegnumId(id);

            //direct children
            PhylumsCollection = _extReportBasicGet.CollPhylumsByRegnumIdAndHash(id);

            DivisionsCollection = _extReportBasicGet.CollDivisionsByRegnumIdAndHash(id);

            //------------------------------------------------------------------------------

            ExpertsCollection = _extReportBasicGet.CollExpertsByRegnumId(id);

            SourcesCollection = _extReportBasicGet.CollSourcesByRegnumId(id);

            AuthorsCollection = _extReportBasicGet.CollAuthorsByRegnumId(id);

            //------------------------------------------------------------------------------

            CommentsCollection = _extReportBasicGet.CollCommentsByRegnumId(id);
        }
        //------------------------------------------------------------------------------

        private RelayCommand _pdfRegnumPrintCommand;
        public ICommand PdfRegnumPrintCommand
        {
            get { return _pdfRegnumPrintCommand ??= new RelayCommand(delegate { CreatePdfRegnumPrint(_mainId); }); }
        }

        private static void CreatePdfRegnumPrint(int id)
        {
            const string use = "print";
            ReportRegnumPdf.CreateMainPdf(id, use);
        }
        //------------------------------------------------------------------------------
        private RelayCommand _pdfRegnumSaveCommand;
        public ICommand PdfRegnumSaveCommand
        {
            get { return _pdfRegnumSaveCommand ??= new RelayCommand(delegate { CreatePdfRegnumSave(_mainId); }); }
        }

        private static void CreatePdfRegnumSave(int id)
        {
            const string use = "save";
            ReportRegnumPdf.CreateMainPdf(id, use);
        }

        //------------------------------------------------------------------------------

        public void GetTbl06PhylumsById(int id)
        {
            PhylumsCollection = _extReportBasicGet.CollPhylumsByPhylumId(id);

            //direct children
            SubphylumsCollection = _extReportBasicGet.CollSubphylumsByPhylumIdAndHash(id);

            //------------------------------------------------------------------------------
            //Function
            var regnumId = _extReportBasicGet.RegnumIdFromPhylumsCollectionSelect(id);

            //-----------------------------------------------------------------------------
            //ForeignKeyTable
            RegnumsCollection = _extReportBasicGet.CollRegnumsByRegnumIdAndHash(regnumId);

            //------------------------------------------------------------------------------

            ExpertsCollection = _extReportBasicGet.CollExpertsByPhylumId(id);

            SourcesCollection = _extReportBasicGet.CollSourcesByPhylumId(id);

            AuthorsCollection = _extReportBasicGet.CollAuthorsByPhylumId(id);

            //------------------------------------------------------------------------------

            CommentsCollection = _extReportBasicGet.CollCommentsByPhylumId(id);
        }
        //------------------------------------------------------------------------------

        private RelayCommand _pdfPhylumPrintCommand;
        public ICommand PdfPhylumPrintCommand
        {
            get { return _pdfPhylumPrintCommand ??= new RelayCommand(delegate { CreatePdfPhylumPrint(_mainId); }); }
        }

        private static void CreatePdfPhylumPrint(int id)
        {
            const string use = "print";
            ReportPhylumPdf.CreateMainPdf(id, use);
        }
        //------------------------------------------------------------------------------
        private RelayCommand _pdfPhylumSaveCommand;
        public ICommand PdfPhylumSaveCommand
        {
            get { return _pdfPhylumSaveCommand ??= new RelayCommand(delegate { CreatePdfPhylumSave(_mainId); }); }
        }

        private static void CreatePdfPhylumSave(int id)
        {
            const string use = "save";
            ReportPhylumPdf.CreateMainPdf(id, use);
        }

        //------------------------------------------------------------------------------  
        public void GetTbl09DivisionsById(int id)
        {
            DivisionsCollection = _extReportBasicGet.CollDivisionsByDivisionId(id);

            //direct children
            SubdivisionsCollection = _extReportBasicGet.CollSubdivisionsByDivisionIdAndHash(id);

            //------------------------------------------------------------------------------
            //Function
            var regnumId = _extReportBasicGet.RegnumIdFromDivisionsCollectionSelect(id);

            //-----------------------------------------------------------------------------
            //ForeignKeyTable
            RegnumsCollection = _extReportBasicGet.CollRegnumsByRegnumIdAndHash(regnumId);

            //------------------------------------------------------------------------------

            ExpertsCollection = _extReportBasicGet.CollExpertsByDivisionId(id);

            SourcesCollection = _extReportBasicGet.CollSourcesByDivisionId(id);

            AuthorsCollection = _extReportBasicGet.CollAuthorsByDivisionId(id);

            //------------------------------------------------------------------------------

            CommentsCollection = _extReportBasicGet.CollCommentsByDivisionId(id);
        }
        //------------------------------------------------------------------------------

        private RelayCommand _pdfDivisionPrintCommand;
        public ICommand PdfDivisionPrintCommand
        {
            get { return _pdfDivisionPrintCommand ??= new RelayCommand(delegate { CreatePdfDivisionPrint(_mainId); }); }
        }

        private static void CreatePdfDivisionPrint(int id)
        {
            const string use = "print";
            ReportDivisionPdf.CreateMainPdf(id, use);
        }
        //------------------------------------------------------------------------------
        private RelayCommand _pdfDivisionSaveCommand;
        public ICommand PdfDivisionSaveCommand
        {
            get { return _pdfDivisionSaveCommand ??= new RelayCommand(delegate { CreatePdfDivisionSave(_mainId); }); }
        }

        private static void CreatePdfDivisionSave(int id)
        {
            const string use = "save";
            ReportDivisionPdf.CreateMainPdf(id, use);
        }

        //------------------------------------------------------------------------------  
   
        public ObservableCollection< Tbl06Phylum> GetValueTbl06PhylumsList(int currentId)
        {
             Tbl06PhylumsList = new ObservableCollection<Tbl06Phylum>
                  (from y in _tbl06PhylumsRepository.GetAll()
                   where y.PhylumId == currentId 
   
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
