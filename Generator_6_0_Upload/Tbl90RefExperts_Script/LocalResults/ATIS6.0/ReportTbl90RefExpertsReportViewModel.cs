using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Te.Atis.BusinessLayer;
using Te.Atis.DomainModel;
using Te.Atis.Ui.Desktop.Views.Report.PDF;    

// <!-- Interface Skriptdatum:  29.11.2018  10:32     -->  

namespace Te.Atis.Ui.Desktop.Views.Report   
{       

           
     public void GetTbl90RefExpertsById(int id)
    {
        Tbl90RefExpertsList = new ObservableCollection<Tbl90RefExpert>(_businessLayer.ListTbl90RefExpertsByRefExpertId(id));		   
           
        //------------------------------------------------------------------------------
        Tbl90RefExpertsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90RefExpertsByRefExpertId(id));

        Tbl90RefSourcesList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90RefSourcesByRefExpertId(id));

        Tbl90RefAuthorsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90RefAuthorsByRefExpertId(id));

        Tbl90ExpertsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ExpertsByRefExpertId(id));

        Tbl90SourcesList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90SourcesByRefExpertId(id));

        Tbl90AuthorsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90AuthorsByRefExpertId(id));

        Tbl93CommentsList = new ObservableCollection<Tbl93Comment>(_businessLayer.ListTbl93CommentsByRefExpertId(id));
    } 
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
           
        IList<Tbl90RefExpert> ListTbl90RefExpertsByRefExpertId(int refExpertId);

        IList<NULL> ListTbl69FiSpeciessesByRefExpertIdAndHash(int refExpertId);

        IList<Tbl90Reference> ListTbl90AuthorsByRefExpertId(int refExpertId);
        IList<Tbl90Reference> ListTbl90SourcesByRefExpertId(int refExpertId);
        IList<Tbl90Reference> ListTbl90ExpertsByRefExpertId(int refExpertId);

        IList<Tbl93Comment> ListTbl93CommentsByRefExpertId(int refExpertId);	   
           
		public IList<Tbl90RefExpert> ListTbl90RefExpertsByRefExpertId(int refExpertId)
		{
			return _tbl90RefExpertsRepository.ListWhereOrderByInclude(
				e => e.RefExpertID == refExpertId,
				_tbl90RefExpertsRepository.OrderBy(r => r.RefExpertName + r.Subregnum),
				p => p.Tbl69FiSpeciesses, k => k.Tbl72PlSpeciesses);
		}

		public IList<NULL> ListTbl69FiSpeciessesByRefExpertIdAndHash(int refExpertId)
		{
			return NULLRepository.ListWhereOrderByInclude(
				e => e.RefExpertID == refExpertId &&
				e.NULL.Contains("#") == false,
				NULLRepository.OrderBy(r => r.NULL),
				p => p.NULL, k => k.Tbl90RefExperts);
		}

	               //------------------------------------------------

		public IList<Tbl90Reference> ListTbl90AuthorsByRefExpertId(int refExpertId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefAuthorID == e.Tbl90RefAuthors.RefAuthorID && e.RefExpertID == refExpertId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefAuthors.RefAuthorName + r.Tbl90RefAuthors.ArticelTitle + r.Tbl90RefAuthors.BookName + r.Tbl90RefAuthors.Page1 + r.Tbl90RefAuthors.Publisher),
				p => p.Tbl90RefAuthors);
		}

		public IList<Tbl90Reference> ListTbl90SourcesByRefExpertId(int refExpertId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefSourceID == e.Tbl90RefSources.RefSourceID && e.RefExpertID == refExpertId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefSources.RefSourceName + r.Tbl90RefSources.SourceYear),
				p => p.Tbl90RefSources);
		}

		public IList<Tbl90Reference> ListTbl90ExpertsByRefExpertId(int refExpertId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefExpertID == e.Tbl90RefExperts.RefExpertID && e.RefExpertID == refExpertId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefExperts.RefExpertName),
				p => p.Tbl90RefExperts);
		}

		public IList<Tbl90Reference> ListTbl90RefAuthorsByRefExpertId(int refExpertId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefAuthorID == e.Tbl90RefAuthors.RefAuthorID && e.RefExpertID == refExpertId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefAuthors.RefAuthorName + r.Tbl90RefAuthors.ArticelTitle + r.Tbl90RefAuthors.BookName + r.Tbl90RefAuthors.Page1 + r.Tbl90RefAuthors.Publisher),
				p => p.Tbl90RefAuthors);
		}

		public IList<Tbl90Reference> ListTbl90RefSourcesByRefExpertId(int refExpertId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefSourceID == e.Tbl90RefSources.RefSourceID && e.RefExpertID == refExpertId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefSources.RefSourceName + r.Tbl90RefSources.SourceYear),
				p => p.Tbl90RefSources);
		}

		public IList<Tbl90Reference> ListTbl90RefExpertsByRefExpertId(int refExpertId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefExpertID == e.Tbl90RefExperts.RefExpertID && e.RefExpertID == refExpertId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefExperts.RefExpertName),
				p => p.Tbl90RefExperts);
		}

	               //------------------------------------------------
 
		public IList<Tbl93Comment> ListTbl93CommentsByRefExpertId(int refExpertId)
		{
			return _tbl93CommentsRepository.ListWhereOrderByInclude(
				e => e.RefExpertID == refExpertId,
				_tbl93CommentsRepository.OrderBy(r => r.Info),
				p => p.Tbl69FiSpeciesses);
		}      
  

         
}   

