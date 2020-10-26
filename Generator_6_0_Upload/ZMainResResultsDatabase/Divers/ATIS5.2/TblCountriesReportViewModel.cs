   
       //------------------------------------------------------------------------------
       private RelayCommand _pdfTblCountriesCommand;
	public ICommand PdfTblCountriesCommand
	{
		get { return _pdfTblCountriesCommand ?? (_pdfTblCountriesCommand = new RelayCommand(delegate { CreatePdfTblCountries(_mainId); })); }
	}

	private static void CreatePdfTblCountries(int id)
	{
		ReportTblCountriesPdf.CreateMainPdf(id);
	}   
       //------------------------------------------------------------------------------
       //------------------------------------------------------------------------------  
      
        public ObservableCollection<TblCountry> GetValueTblCountriesList(int currentId)
        {
            TblCountriesList = new ObservableCollection<TblCountry>
                 (from y in tblCountryRepository.GetAll()
                  where y.CountryID == currentId  
      
                 orderby y.Name  
   
                   select y);
            return TblCountriesList;
        } 
      
        public ObservableCollection<TblCountry> GetValueTblCountriesList()
        {
            TblCountriesList = new ObservableCollection<TblCountry>
                { new ObservableCollection<TblCountry>
                    (from x in _tblCountryRepository.GetAll() select x).LastOrDefault()
                };
            return TblCountriesList;
        }

        #endregion   
      
        #region "Public Property  TblCountry"

        private ObservableCollection<TblCountry> _tblCountriesList;
        public ObservableCollection<TblCountry> TblCountriesList
        {
            get { return _tblCountriesList; }
            set { _tblCountriesList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<TblCountry> _tblCountriesAllList;
        public ObservableCollection<TblCountry> TblCountriesAllList
        {
            get { return _tblCountriesAllList; }
            set { _tblCountriesAllList = value; RaisePropertyChanged(); }
        }

        #endregion "Public Properties"  
