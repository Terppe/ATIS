using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Te.Atis.BusinessLayer;
using Te.Atis.DomainModel;
using Te.Atis.Ui.Desktop.Views.Report.PDF;    

// <!-- Interface Skriptdatum:  14.11.2017  10:32     -->  

namespace Te.Atis.Ui.Desktop.Views.Report   
{       

           
     public void GetTbl90RefAuthorsById(int id)
    {
        Tbl90RefAuthorsList = new ObservableCollection<Tbl90RefAuthor>(_businessLayer.ListTbl90RefAuthorsByRefAuthorId(id));		   
           
        //------------------------------------------------------------------------------
        Tbl90RefExpertsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90RefExpertsByRefAuthorId(id));

        Tbl90RefSourcesList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90RefSourcesByRefAuthorId(id));

        Tbl90RefAuthorsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90RefAuthorsByRefAuthorId(id));

        Tbl90ExpertsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ExpertsByRefAuthorId(id));

        Tbl90SourcesList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90SourcesByRefAuthorId(id));

        Tbl90AuthorsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90AuthorsByRefAuthorId(id));

        Tbl93CommentsList = new ObservableCollection<Tbl93Comment>(_businessLayer.ListTbl93CommentsByRefAuthorId(id));
    } 
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
           
        IList<Tbl90RefAuthor> ListTbl90RefAuthorsByRefAuthorId(int refAuthorId);

        IList<NULL> ListTbl69FiSpeciessesByRefAuthorIdAndHash(int refAuthorId);

        IList<Tbl90Reference> ListTbl90AuthorsByRefAuthorId(int refAuthorId);
        IList<Tbl90Reference> ListTbl90SourcesByRefAuthorId(int refAuthorId);
        IList<Tbl90Reference> ListTbl90ExpertsByRefAuthorId(int refAuthorId);

        IList<Tbl93Comment> ListTbl93CommentsByRefAuthorId(int refAuthorId);	   
           
		public IList<Tbl90RefAuthor> ListTbl90RefAuthorsByRefAuthorId(int refAuthorId)
		{
			return _tbl90RefAuthorsRepository.ListWhereOrderByInclude(
				e => e.RefAuthorID == refAuthorId,
				_tbl90RefAuthorsRepository.OrderBy(r => r.RefAuthorName + r.Subregnum),
				p => p.Tbl69FiSpeciesses, k => k.Tbl72PlSpeciesses);
		}

		public IList<NULL> ListTbl69FiSpeciessesByRefAuthorIdAndHash(int refAuthorId)
		{
			return NULLRepository.ListWhereOrderByInclude(
				e => e.RefAuthorID == refAuthorId &&
				e.NULL.Contains("#") == false,
				NULLRepository.OrderBy(r => r.NULL),
				p => p.NULL, k => k.Tbl90RefAuthors);
		}

	               //------------------------------------------------

		public IList<Tbl90Reference> ListTbl90AuthorsByRefAuthorId(int refAuthorId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefAuthorID == e.Tbl90RefAuthors.RefAuthorID && e.RefAuthorID == refAuthorId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefAuthors.RefAuthorName + r.Tbl90RefAuthors.ArticelTitle + r.Tbl90RefAuthors.BookName + r.Tbl90RefAuthors.Page1 + r.Tbl90RefAuthors.Publisher),
				p => p.Tbl90RefAuthors);
		}

		public IList<Tbl90Reference> ListTbl90SourcesByRefAuthorId(int refAuthorId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefSourceID == e.Tbl90RefSources.RefSourceID && e.RefAuthorID == refAuthorId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefSources.RefSourceName + r.Tbl90RefSources.SourceYear),
				p => p.Tbl90RefSources);
		}

		public IList<Tbl90Reference> ListTbl90ExpertsByRefAuthorId(int refAuthorId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefExpertID == e.Tbl90RefExperts.RefExpertID && e.RefAuthorID == refAuthorId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefExperts.RefExpertName),
				p => p.Tbl90RefExperts);
		}

		public IList<Tbl90Reference> ListTbl90RefAuthorsByRefAuthorId(int refAuthorId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefAuthorID == e.Tbl90RefAuthors.RefAuthorID && e.RefAuthorID == refAuthorId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefAuthors.RefAuthorName + r.Tbl90RefAuthors.ArticelTitle + r.Tbl90RefAuthors.BookName + r.Tbl90RefAuthors.Page1 + r.Tbl90RefAuthors.Publisher),
				p => p.Tbl90RefAuthors);
		}

		public IList<Tbl90Reference> ListTbl90RefSourcesByRefAuthorId(int refAuthorId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefSourceID == e.Tbl90RefSources.RefSourceID && e.RefAuthorID == refAuthorId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefSources.RefSourceName + r.Tbl90RefSources.SourceYear),
				p => p.Tbl90RefSources);
		}

		public IList<Tbl90Reference> ListTbl90RefExpertsByRefAuthorId(int refAuthorId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefExpertID == e.Tbl90RefExperts.RefExpertID && e.RefAuthorID == refAuthorId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefExperts.RefExpertName),
				p => p.Tbl90RefExperts);
		}

	               //------------------------------------------------
 
		public IList<Tbl93Comment> ListTbl93CommentsByRefAuthorId(int refAuthorId)
		{
			return _tbl93CommentsRepository.ListWhereOrderByInclude(
				e => e.RefAuthorID == refAuthorId,
				_tbl93CommentsRepository.OrderBy(r => r.Info),
				p => p.Tbl69FiSpeciesses);
		}      
  

         
}   

