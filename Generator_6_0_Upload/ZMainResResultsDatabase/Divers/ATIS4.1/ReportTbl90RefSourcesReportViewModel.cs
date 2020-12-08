using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Te.Atis.BusinessLayer;
using Te.Atis.DomainModel;
using Te.Atis.Ui.Desktop.Views.Report.PDF;    

// <!-- Interface Skriptdatum:   24.07.2018  10:32     -->  

namespace Te.Atis.Ui.Desktop.Views.Report   
{       

           
     public void GetTbl90RefSourcesById(int id)
    {
        Tbl90RefSourcesList = new ObservableCollection<Tbl90RefSource>(_businessLayer.ListTbl90RefSourcesByRefSourceId(id));		   
           
        //------------------------------------------------------------------------------
        Tbl90RefExpertsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90RefExpertsByRefSourceId(id));

        Tbl90RefSourcesList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90RefSourcesByRefSourceId(id));

        Tbl90RefAuthorsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90RefAuthorsByRefSourceId(id));

        Tbl90ExpertsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ExpertsByRefSourceId(id));

        Tbl90SourcesList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90SourcesByRefSourceId(id));

        Tbl90AuthorsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90AuthorsByRefSourceId(id));

        Tbl93CommentsList = new ObservableCollection<Tbl93Comment>(_businessLayer.ListTbl93CommentsByRefSourceId(id));
    } 
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
           
        IList<Tbl90RefSource> ListTbl90RefSourcesByRefSourceId(int refSourceId);

        IList<NULL> ListTbl69FiSpeciessesByRefSourceIdAndHash(int refSourceId);

        IList<Tbl90Reference> ListTbl90AuthorsByRefSourceId(int refSourceId);
        IList<Tbl90Reference> ListTbl90SourcesByRefSourceId(int refSourceId);
        IList<Tbl90Reference> ListTbl90ExpertsByRefSourceId(int refSourceId);

        IList<Tbl93Comment> ListTbl93CommentsByRefSourceId(int refSourceId);	   
           
		public IList<Tbl90RefSource> ListTbl90RefSourcesByRefSourceId(int refSourceId)
		{
			return _tbl90RefSourcesRepository.ListWhereOrderByInclude(
				e => e.RefSourceID == refSourceId,
				_tbl90RefSourcesRepository.OrderBy(r => r.RefSourceName + r.Subregnum),
				p => p.Tbl69FiSpeciesses, k => k.Tbl72PlSpeciesses);
		}

		public IList<NULL> ListTbl69FiSpeciessesByRefSourceIdAndHash(int refSourceId)
		{
			return NULLRepository.ListWhereOrderByInclude(
				e => e.RefSourceID == refSourceId &&
				e.NULL.Contains("#") == false,
				NULLRepository.OrderBy(r => r.NULL),
				p => p.NULL, k => k.Tbl90RefSources);
		}

	               //------------------------------------------------

		public IList<Tbl90Reference> ListTbl90AuthorsByRefSourceId(int refSourceId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefAuthorID == e.Tbl90RefAuthors.RefAuthorID && e.RefSourceID == refSourceId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefAuthors.RefAuthorName + r.Tbl90RefAuthors.ArticelTitle + r.Tbl90RefAuthors.BookName + r.Tbl90RefAuthors.Page1 + r.Tbl90RefAuthors.Publisher),
				p => p.Tbl90RefAuthors);
		}

		public IList<Tbl90Reference> ListTbl90SourcesByRefSourceId(int refSourceId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefSourceID == e.Tbl90RefSources.RefSourceID && e.RefSourceID == refSourceId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefSources.RefSourceName + r.Tbl90RefSources.SourceYear),
				p => p.Tbl90RefSources);
		}

		public IList<Tbl90Reference> ListTbl90ExpertsByRefSourceId(int refSourceId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefExpertID == e.Tbl90RefExperts.RefExpertID && e.RefSourceID == refSourceId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefExperts.RefExpertName),
				p => p.Tbl90RefExperts);
		}

		public IList<Tbl90Reference> ListTbl90RefAuthorsByRefSourceId(int refSourceId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefAuthorID == e.Tbl90RefAuthors.RefAuthorID && e.RefSourceID == refSourceId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefAuthors.RefAuthorName + r.Tbl90RefAuthors.ArticelTitle + r.Tbl90RefAuthors.BookName + r.Tbl90RefAuthors.Page1 + r.Tbl90RefAuthors.Publisher),
				p => p.Tbl90RefAuthors);
		}

		public IList<Tbl90Reference> ListTbl90RefSourcesByRefSourceId(int refSourceId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefSourceID == e.Tbl90RefSources.RefSourceID && e.RefSourceID == refSourceId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefSources.RefSourceName + r.Tbl90RefSources.SourceYear),
				p => p.Tbl90RefSources);
		}

		public IList<Tbl90Reference> ListTbl90RefExpertsByRefSourceId(int refSourceId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefExpertID == e.Tbl90RefExperts.RefExpertID && e.RefSourceID == refSourceId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefExperts.RefExpertName),
				p => p.Tbl90RefExperts);
		}

	               //------------------------------------------------
 
		public IList<Tbl93Comment> ListTbl93CommentsByRefSourceId(int refSourceId)
		{
			return _tbl93CommentsRepository.ListWhereOrderByInclude(
				e => e.RefSourceID == refSourceId,
				_tbl93CommentsRepository.OrderBy(r => r.Info),
				p => p.Tbl69FiSpeciesses);
		}      
  

         
}   

