using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Te.Atis.BusinessLayer;
using Te.Atis.DomainModel;
using Te.Atis.Ui.Desktop.Views.Report.PDF;    

// <!-- Interface Skriptdatum:  21.07.2018  10:32     -->  

namespace Te.Atis.Ui.Desktop.Views.Report   
{       

           
     public void GetTbl90ReferencesById(int id)
    {
        Tbl90ReferencesList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ReferencesByReferenceId(id));		   
           
        //------------------------------------------------------------------------------
        Tbl90RefExpertsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90RefExpertsByReferenceId(id));

        Tbl90RefSourcesList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90RefSourcesByReferenceId(id));

        Tbl90RefAuthorsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90RefAuthorsByReferenceId(id));

        Tbl90ExpertsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ExpertsByReferenceId(id));

        Tbl90SourcesList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90SourcesByReferenceId(id));

        Tbl90AuthorsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90AuthorsByReferenceId(id));

        Tbl93CommentsList = new ObservableCollection<Tbl93Comment>(_businessLayer.ListTbl93CommentsByReferenceId(id));
    } 
    private RelayCommand _pdfTbl90ReferencesCommand;
    public ICommand PdfTbl90ReferencesCommand
    {
        get { return _pdfTbl90ReferencesCommand ?? (_pdfTbl90ReferencesCommand = new RelayCommand(delegate { CreatePdfTbl90References(_mainId); })); }
    }

    private static void CreatePdfTbl90References(int id)
    {
        ReportTbl90ReferencesPdf.CreateMainPdf(id);
    }
    //------------------------------------------------------------------------------
    //------------------------------------------------------------------------------  		   
           
        IList<Tbl90Reference> ListTbl90ReferencesByReferenceId(int referenceId);

        IList<Tbl90RefAuthor> ListTbl90RefAuthorsByReferenceIdAndHash(int referenceId);

        IList<Tbl90Reference> ListTbl90AuthorsByReferenceId(int referenceId);
        IList<Tbl90Reference> ListTbl90SourcesByReferenceId(int referenceId);
        IList<Tbl90Reference> ListTbl90ExpertsByReferenceId(int referenceId);

        IList<Tbl93Comment> ListTbl93CommentsByReferenceId(int referenceId);	   
           
		public IList<Tbl90Reference> ListTbl90ReferencesByReferenceId(int referenceId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.ReferenceID == referenceId,
				_tbl90ReferencesRepository.OrderBy(r => r.ReferenceName + r.Subregnum),
				p => p.Tbl90RefAuthors, k => k.NULL);
		}

		public IList<Tbl90RefAuthor> ListTbl90RefAuthorsByReferenceIdAndHash(int referenceId)
		{
			return _tbl90RefAuthorsRepository.ListWhereOrderByInclude(
				e => e.ReferenceID == referenceId &&
				e.RefAuthorName.Contains("#") == false,
				_tbl90RefAuthorsRepository.OrderBy(r => r.RefAuthorName),
				p => p.NULL, k => k.Tbl90References);
		}

	               //------------------------------------------------

		public IList<Tbl90Reference> ListTbl90AuthorsByReferenceId(int referenceId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefAuthorID == e.Tbl90RefAuthors.RefAuthorID && e.ReferenceID == referenceId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefAuthors.RefAuthorName + r.Tbl90RefAuthors.ArticelTitle + r.Tbl90RefAuthors.BookName + r.Tbl90RefAuthors.Page1 + r.Tbl90RefAuthors.Publisher),
				p => p.Tbl90RefAuthors);
		}

		public IList<Tbl90Reference> ListTbl90SourcesByReferenceId(int referenceId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefSourceID == e.Tbl90RefSources.RefSourceID && e.ReferenceID == referenceId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefSources.RefSourceName + r.Tbl90RefSources.SourceYear),
				p => p.Tbl90RefSources);
		}

		public IList<Tbl90Reference> ListTbl90ExpertsByReferenceId(int referenceId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefExpertID == e.Tbl90RefExperts.RefExpertID && e.ReferenceID == referenceId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefExperts.RefExpertName),
				p => p.Tbl90RefExperts);
		}

		public IList<Tbl90Reference> ListTbl90RefAuthorsByReferenceId(int referenceId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefAuthorID == e.Tbl90RefAuthors.RefAuthorID && e.ReferenceID == referenceId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefAuthors.RefAuthorName + r.Tbl90RefAuthors.ArticelTitle + r.Tbl90RefAuthors.BookName + r.Tbl90RefAuthors.Page1 + r.Tbl90RefAuthors.Publisher),
				p => p.Tbl90RefAuthors);
		}

		public IList<Tbl90Reference> ListTbl90RefSourcesByReferenceId(int referenceId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefSourceID == e.Tbl90RefSources.RefSourceID && e.ReferenceID == referenceId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefSources.RefSourceName + r.Tbl90RefSources.SourceYear),
				p => p.Tbl90RefSources);
		}

		public IList<Tbl90Reference> ListTbl90RefExpertsByReferenceId(int referenceId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefExpertID == e.Tbl90RefExperts.RefExpertID && e.ReferenceID == referenceId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefExperts.RefExpertName),
				p => p.Tbl90RefExperts);
		}

	               //------------------------------------------------
 
		public IList<Tbl93Comment> ListTbl93CommentsByReferenceId(int referenceId)
		{
			return _tbl93CommentsRepository.ListWhereOrderByInclude(
				e => e.ReferenceID == referenceId,
				_tbl93CommentsRepository.OrderBy(r => r.Info),
				p => p.Tbl90RefAuthors);
		}      
  

         
}   

