using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Te.Atis.BusinessLayer;
using Te.Atis.DomainModel;
using Te.Atis.Ui.Desktop.Views.Report.PDF;    

// <!-- Interface Skriptdatum:   31.07.2018 12:32       -->  

namespace Te.Atis.Ui.Desktop.Views.Report   
{       

           
     public void GetTblCountriesById(int id)
    {
        TblCountriesList = new ObservableCollection<TblCountry>(_businessLayer.ListTblCountriesByCountryId(id));		   
           
        //------------------------------------------------------------------------------
        Tbl90RefExpertsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90RefExpertsByCountryId(id));

        Tbl90RefSourcesList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90RefSourcesByCountryId(id));

        Tbl90RefAuthorsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90RefAuthorsByCountryId(id));

        Tbl90ExpertsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ExpertsByCountryId(id));

        Tbl90SourcesList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90SourcesByCountryId(id));

        Tbl90AuthorsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90AuthorsByCountryId(id));

        Tbl93CommentsList = new ObservableCollection<Tbl93Comment>(_businessLayer.ListTbl93CommentsByCountryId(id));
    } 
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
           
        IList<TblCountry> ListTblCountriesByCountryId(int countryId);

        IList<NULL> ListNULLByCountryIdAndHash(int countryId);

        IList<Tbl90Reference> ListTbl90AuthorsByCountryId(int countryId);
        IList<Tbl90Reference> ListTbl90SourcesByCountryId(int countryId);
        IList<Tbl90Reference> ListTbl90ExpertsByCountryId(int countryId);

        IList<Tbl93Comment> ListTbl93CommentsByCountryId(int countryId);	   
           
		public IList<TblCountry> ListTblCountriesByCountryId(int countryId)
		{
			return _tblCountriesRepository.ListWhereOrderByInclude(
				e => e.CountryID == countryId,
				_tblCountriesRepository.OrderBy(r => r.CountryName + r.Subregnum),
				p => p.NULL, k => k.);
		}

		public IList<NULL> ListNULLByCountryIdAndHash(int countryId)
		{
			return NULLRepository.ListWhereOrderByInclude(
				e => e.CountryID == countryId &&
				e.NULL.Contains("#") == false,
				NULLRepository.OrderBy(r => r.NULL),
				p => p., k => k.TblCountries);
		}

	               //------------------------------------------------

		public IList<Tbl90Reference> ListTbl90AuthorsByCountryId(int countryId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefAuthorID == e.Tbl90RefAuthors.RefAuthorID && e.CountryID == countryId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefAuthors.RefAuthorName + r.Tbl90RefAuthors.ArticelTitle + r.Tbl90RefAuthors.BookName + r.Tbl90RefAuthors.Page1 + r.Tbl90RefAuthors.Publisher),
				p => p.Tbl90RefAuthors);
		}

		public IList<Tbl90Reference> ListTbl90SourcesByCountryId(int countryId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefSourceID == e.Tbl90RefSources.RefSourceID && e.CountryID == countryId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefSources.RefSourceName + r.Tbl90RefSources.SourceYear),
				p => p.Tbl90RefSources);
		}

		public IList<Tbl90Reference> ListTbl90ExpertsByCountryId(int countryId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefExpertID == e.Tbl90RefExperts.RefExpertID && e.CountryID == countryId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefExperts.RefExpertName),
				p => p.Tbl90RefExperts);
		}

		public IList<Tbl90Reference> ListTbl90RefAuthorsByCountryId(int countryId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefAuthorID == e.Tbl90RefAuthors.RefAuthorID && e.CountryID == countryId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefAuthors.RefAuthorName + r.Tbl90RefAuthors.ArticelTitle + r.Tbl90RefAuthors.BookName + r.Tbl90RefAuthors.Page1 + r.Tbl90RefAuthors.Publisher),
				p => p.Tbl90RefAuthors);
		}

		public IList<Tbl90Reference> ListTbl90RefSourcesByCountryId(int countryId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefSourceID == e.Tbl90RefSources.RefSourceID && e.CountryID == countryId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefSources.RefSourceName + r.Tbl90RefSources.SourceYear),
				p => p.Tbl90RefSources);
		}

		public IList<Tbl90Reference> ListTbl90RefExpertsByCountryId(int countryId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefExpertID == e.Tbl90RefExperts.RefExpertID && e.CountryID == countryId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefExperts.RefExpertName),
				p => p.Tbl90RefExperts);
		}

	               //------------------------------------------------
 
		public IList<Tbl93Comment> ListTbl93CommentsByCountryId(int countryId)
		{
			return _tbl93CommentsRepository.ListWhereOrderByInclude(
				e => e.CountryID == countryId,
				_tbl93CommentsRepository.OrderBy(r => r.Info),
				p => p.NULL);
		}      
  

         
}   

