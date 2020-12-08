   
       //------------------------------------------------------------------------------
       private RelayCommand _pdfTbl90RefExpertsCommand;
	public ICommand PdfTbl90RefExpertsCommand
	{
		get { return _pdfTbl90RefExpertsCommand ?? (_pdfTbl90RefExpertsCommand = new RelayCommand(delegate { CreatePdfTbl90RefExperts(_mainId); })); }
	}

	private static void CreatePdfTbl90RefExperts(int id)
	{
		ReportTbl90RefExpertsPdf.CreateMainPdf(id);
	}   
       //------------------------------------------------------------------------------
       //------------------------------------------------------------------------------  
      
        public ObservableCollection<Tbl90References> GetValueTbl90RefExpertsList(int currentId)          
        {
            Tbl90RefExpertsList = new ObservableCollection<Tbl90References>
                (from y in _tbl90ReferencesRepository.GetAll()
                 where y.ReferenceID == currentId
                        && y.Tbl90RefAuthors == null
                        && y.Tbl90RefSources == null  
      
                 orderby y.Tbl90RefExperts.RefExpertName, y.Tbl90RefExperts.Info 
   
                   select y);
            return Tbl90RefExpertsList;
        } 
      
        public ObservableCollection<Tbl90References> GetValueTbl90RefExpertsList()
        {
            Tbl90RefExpertsList = new ObservableCollection<Tbl90References>
                { new ObservableCollection<Tbl90References>
                    (from x in _tbl90ReferencesRepository.GetAll()  select x).LastOrDefault()
                };
            return Tbl90RefExpertsList;
        }

        #endregion  
      
        #region Tbl90Experts  ---------------------------------------

        public ObservableCollection<Tbl90RefExperts> GetValueTbl90ExpertsList(string searchRefExpertName)
        {
            Tbl90ExpertsList = new ObservableCollection<Tbl90RefExperts>
               (from x in _tbl90RefExpertsRepository.GetAll()
                where x.RefExpertName.StartsWith(searchRefExpertName)
                orderby x.RefExpertName, x.Info
                select x);
            return Tbl90ExpertsList;
        }

        public ObservableCollection<Tbl90RefExperts> GetValueTbl90ExpertsAllList()
        {
            Tbl90ExpertsAllList = new ObservableCollection<Tbl90RefExperts>
               (from z in _tbl90RefExpertsRepository.GetAll()
                orderby z.RefExpertName, z.Info
                select z);
            return Tbl90ExpertsAllList;
        }

        public ObservableCollection<Tbl90RefExperts> GetValueTbl90ExpertsList(int currentId)
        {
            Tbl90ExpertsList = new ObservableCollection<Tbl90RefExperts>
                 (from y in _tbl90RefExpertsRepository.GetAll()
                  where y.RefExpertID == currentId
                  orderby y.RefExpertName, y.Info
                  select y);
            return Tbl90ExpertsList;
        }

        public ObservableCollection<Tbl90RefExperts> GetValueTbl90ExpertsList()
        {
            Tbl90ExpertsList = new ObservableCollection<Tbl90RefExperts>
                { new ObservableCollection<Tbl90RefExperts>
                    (from x in _tbl90RefExpertsRepository.GetAll() select x).LastOrDefault()
                };
            return Tbl90ExpertsList;
        }

        #endregion          
      
        #region "Public Property  Tbl90RefExpert"

        private ObservableCollection<Tbl90Reference> _tbl90RefExpertsList;
        public ObservableCollection<Tbl90Reference> Tbl90RefExpertsList
        {
            get { return _tbl90RefExpertsList; }
            set { _tbl90RefExpertsList = value; RaisePropertyChanged(); }
        }


        #endregion "Public Properties"    
      
        #region "Public Property  Tbl90Expert"

        private ObservableCollection<Tbl90RefExpert> _tbl90ExpertsList;
        public ObservableCollection<Tbl90RefExpert> Tbl90ExpertsList
        {
            get { return _tbl90ExpertsList; }
            set { _tbl90ExpertsList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl90RefExpert> _tbl90ExpertsAllList;
        public ObservableCollection<Tbl90RefExpert> Tbl90ExpertsAllList
        {
            get { return _tbl90ExpertsAllList; }
            set { _tbl90ExpertsAllList = value; RaisePropertyChanged(); }
        }

        #endregion "Public Properties"    
