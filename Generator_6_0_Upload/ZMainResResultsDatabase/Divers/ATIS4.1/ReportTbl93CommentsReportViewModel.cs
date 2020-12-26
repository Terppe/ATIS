using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Te.Atis.BusinessLayer;
using Te.Atis.DomainModel;
using Te.Atis.Ui.Desktop.Views.Report.PDF;    

// <!-- Interface Skriptdatum:  30.07.2018  10:32     -->  

namespace Te.Atis.Ui.Desktop.Views.Report   
{       

           
     public void GetTbl93CommentsById(int id)
    {
        Tbl93CommentsList = new ObservableCollection<Tbl93Comment>(_businessLayer.ListTbl93CommentsByCommentId(id));		   
           
        //------------------------------------------------------------------------------
        Tbl90RefExpertsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90RefExpertsByCommentId(id));

        Tbl90RefSourcesList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90RefSourcesByCommentId(id));

        Tbl90RefAuthorsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90RefAuthorsByCommentId(id));

        Tbl90ExpertsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ExpertsByCommentId(id));

        Tbl90SourcesList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90SourcesByCommentId(id));

        Tbl90AuthorsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90AuthorsByCommentId(id));

        Tbl93CommentsList = new ObservableCollection<Tbl93Comment>(_businessLayer.ListTbl93CommentsByCommentId(id));
    } 
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
           
        IList<Tbl93Comment> ListTbl93CommentsByCommentId(int commentId);

        IList<NULL> ListNULLByCommentIdAndHash(int commentId);

        IList<Tbl90Reference> ListTbl90AuthorsByCommentId(int commentId);
        IList<Tbl90Reference> ListTbl90SourcesByCommentId(int commentId);
        IList<Tbl90Reference> ListTbl90ExpertsByCommentId(int commentId);

        IList<Tbl93Comment> ListTbl93CommentsByCommentId(int commentId);	   
           
		public IList<Tbl93Comment> ListTbl93CommentsByCommentId(int commentId)
		{
			return _tbl93CommentsRepository.ListWhereOrderByInclude(
				e => e.CommentID == commentId,
				_tbl93CommentsRepository.OrderBy(r => r.CommentName + r.Subregnum),
				p => p.NULL, k => k.NULL);
		}

		public IList<NULL> ListNULLByCommentIdAndHash(int commentId)
		{
			return NULLRepository.ListWhereOrderByInclude(
				e => e.CommentID == commentId &&
				e.NULL.Contains("#") == false,
				NULLRepository.OrderBy(r => r.NULL),
				p => p.NULL, k => k.Tbl93Comments);
		}

	               //------------------------------------------------

		public IList<Tbl90Reference> ListTbl90AuthorsByCommentId(int commentId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefAuthorID == e.Tbl90RefAuthors.RefAuthorID && e.CommentID == commentId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefAuthors.RefAuthorName + r.Tbl90RefAuthors.ArticelTitle + r.Tbl90RefAuthors.BookName + r.Tbl90RefAuthors.Page1 + r.Tbl90RefAuthors.Publisher),
				p => p.Tbl90RefAuthors);
		}

		public IList<Tbl90Reference> ListTbl90SourcesByCommentId(int commentId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefSourceID == e.Tbl90RefSources.RefSourceID && e.CommentID == commentId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefSources.RefSourceName + r.Tbl90RefSources.SourceYear),
				p => p.Tbl90RefSources);
		}

		public IList<Tbl90Reference> ListTbl90ExpertsByCommentId(int commentId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefExpertID == e.Tbl90RefExperts.RefExpertID && e.CommentID == commentId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefExperts.RefExpertName),
				p => p.Tbl90RefExperts);
		}

		public IList<Tbl90Reference> ListTbl90RefAuthorsByCommentId(int commentId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefAuthorID == e.Tbl90RefAuthors.RefAuthorID && e.CommentID == commentId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefAuthors.RefAuthorName + r.Tbl90RefAuthors.ArticelTitle + r.Tbl90RefAuthors.BookName + r.Tbl90RefAuthors.Page1 + r.Tbl90RefAuthors.Publisher),
				p => p.Tbl90RefAuthors);
		}

		public IList<Tbl90Reference> ListTbl90RefSourcesByCommentId(int commentId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefSourceID == e.Tbl90RefSources.RefSourceID && e.CommentID == commentId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefSources.RefSourceName + r.Tbl90RefSources.SourceYear),
				p => p.Tbl90RefSources);
		}

		public IList<Tbl90Reference> ListTbl90RefExpertsByCommentId(int commentId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefExpertID == e.Tbl90RefExperts.RefExpertID && e.CommentID == commentId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefExperts.RefExpertName),
				p => p.Tbl90RefExperts);
		}

	               //------------------------------------------------
 
		public IList<Tbl93Comment> ListTbl93CommentsByCommentId(int commentId)
		{
			return _tbl93CommentsRepository.ListWhereOrderByInclude(
				e => e.CommentID == commentId,
				_tbl93CommentsRepository.OrderBy(r => r.Info),
				p => p.NULL);
		}      
  

         
}   

