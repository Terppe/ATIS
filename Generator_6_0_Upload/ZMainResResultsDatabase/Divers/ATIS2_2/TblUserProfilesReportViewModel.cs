   
       //------------------------------------------------------------------------------
       private RelayCommand _pdfTblUserProfilesCommand;
	public ICommand PdfTblUserProfilesCommand
	{
		get { return _pdfTblUserProfilesCommand ?? (_pdfTblUserProfilesCommand = new RelayCommand(delegate { CreatePdfTblUserProfiles(_mainId); })); }
	}

	private static void CreatePdfTblUserProfiles(int id)
	{
		ReportTblUserProfilesPdf.CreateMainPdf(id);
	}   
       //------------------------------------------------------------------------------
       //------------------------------------------------------------------------------  
   
        public ObservableCollection< TblUserProfile> GetValueTblUserProfilesList(int currentId)
        {
             TblUserProfilesList = new ObservableCollection<TblUserProfile>
                  (from y in _tblUserProfilesRepository.GetAll()
                   where y.UserProfileID == currentId 
   
                   orderby y.LastName   
                   select y);
            return TblUserProfilesList;
        } 
   
        public ObservableCollection< TblUserProfile> GetValueTblUserProfilesList()
        {
             TblUserProfilesList = new ObservableCollection<TblUserProfile>
                { new ObservableCollection<TblUserProfile>
                    (from x in _tblUserProfilesRepository.GetAll() select x).LastOrDefault()
                };
            return TblUserProfilesList;
        }

        #endregion  
   
        #region "Public Property  TblUserProfile"

        private ObservableCollection< TblUserProfile>  _tblUserProfilesList;
        public ObservableCollection< TblUserProfile>  TblUserProfilesList
        {
            get { return  _tblUserProfilesList; }
            set {  _tblUserProfilesList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection< TblUserProfile>  _tblUserProfilesAllList;
        public  ObservableCollection< TblUserProfile>  TblUserProfilesAllList
        {
            get { return  _tblUserProfilesAllList; }
            set {  _tblUserProfilesAllList = value; RaisePropertyChanged(); }
        }

        #endregion "Public Properties" 
