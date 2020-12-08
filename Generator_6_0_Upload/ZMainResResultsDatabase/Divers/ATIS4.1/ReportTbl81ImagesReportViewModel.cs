using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Te.Atis.BusinessLayer;
using Te.Atis.DomainModel;
using Te.Atis.Ui.Desktop.Views.Report.PDF;    

// <!-- Interface Skriptdatum:  12.07.2018  10:32     -->  

namespace Te.Atis.Ui.Desktop.Views.Report   
{       

           
     public void GetTbl81ImagesById(int id)
    {
        Tbl81ImagesList = new ObservableCollection<Tbl81Image>(_businessLayer.ListTbl81ImagesByImageId(id));		   
           
        //------------------------------------------------------------------------------
        Tbl90RefExpertsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90RefExpertsByImageId(id));

        Tbl90RefSourcesList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90RefSourcesByImageId(id));

        Tbl90RefAuthorsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90RefAuthorsByImageId(id));

        Tbl90ExpertsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ExpertsByImageId(id));

        Tbl90SourcesList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90SourcesByImageId(id));

        Tbl90AuthorsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90AuthorsByImageId(id));

        Tbl93CommentsList = new ObservableCollection<Tbl93Comment>(_businessLayer.ListTbl93CommentsByImageId(id));
    } 
    private RelayCommand _pdfTbl81ImagesCommand;
    public ICommand PdfTbl81ImagesCommand
    {
        get { return _pdfTbl81ImagesCommand ?? (_pdfTbl81ImagesCommand = new RelayCommand(delegate { CreatePdfTbl81Images(_mainId); })); }
    }

    private static void CreatePdfTbl81Images(int id)
    {
        ReportTbl81ImagesPdf.CreateMainPdf(id);
    }
    //------------------------------------------------------------------------------
    //------------------------------------------------------------------------------  		   
           
        IList<Tbl81Image> ListTbl81ImagesByImageId(int imageId);

        IList<Tbl68Speciesgroup> ListNULLByImageIdAndHash(int imageId);

        IList<Tbl90Reference> ListTbl90AuthorsByImageId(int imageId);
        IList<Tbl90Reference> ListTbl90SourcesByImageId(int imageId);
        IList<Tbl90Reference> ListTbl90ExpertsByImageId(int imageId);

        IList<Tbl93Comment> ListTbl93CommentsByImageId(int imageId);	   
           
		public IList<Tbl81Image> ListTbl81ImagesByImageId(int imageId)
		{
			return _tbl81ImagesRepository.ListWhereOrderByInclude(
				e => e.ImageID == imageId,
				_tbl81ImagesRepository.OrderBy(r => r.ImageName + r.Subregnum),
				p => p.NULL, k => k.NULL);
		}

		public IList<Tbl68Speciesgroup> ListNULLByImageIdAndHash(int imageId)
		{
			return _tbl68SpeciesgroupsRepository.ListWhereOrderByInclude(
				e => e.ImageID == imageId &&
				e.NULL.Contains("#") == false,
				_tbl68SpeciesgroupsRepository.OrderBy(r => r.NULL),
				p => p.NULL, k => k.Tbl81Images);
		}

	               //------------------------------------------------

		public IList<Tbl90Reference> ListTbl90AuthorsByImageId(int imageId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefAuthorID == e.Tbl90RefAuthors.RefAuthorID && e.ImageID == imageId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefAuthors.RefAuthorName + r.Tbl90RefAuthors.ArticelTitle + r.Tbl90RefAuthors.BookName + r.Tbl90RefAuthors.Page1 + r.Tbl90RefAuthors.Publisher),
				p => p.Tbl90RefAuthors);
		}

		public IList<Tbl90Reference> ListTbl90SourcesByImageId(int imageId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefSourceID == e.Tbl90RefSources.RefSourceID && e.ImageID == imageId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefSources.RefSourceName + r.Tbl90RefSources.SourceYear),
				p => p.Tbl90RefSources);
		}

		public IList<Tbl90Reference> ListTbl90ExpertsByImageId(int imageId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefExpertID == e.Tbl90RefExperts.RefExpertID && e.ImageID == imageId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefExperts.RefExpertName),
				p => p.Tbl90RefExperts);
		}

		public IList<Tbl90Reference> ListTbl90RefAuthorsByImageId(int imageId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefAuthorID == e.Tbl90RefAuthors.RefAuthorID && e.ImageID == imageId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefAuthors.RefAuthorName + r.Tbl90RefAuthors.ArticelTitle + r.Tbl90RefAuthors.BookName + r.Tbl90RefAuthors.Page1 + r.Tbl90RefAuthors.Publisher),
				p => p.Tbl90RefAuthors);
		}

		public IList<Tbl90Reference> ListTbl90RefSourcesByImageId(int imageId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefSourceID == e.Tbl90RefSources.RefSourceID && e.ImageID == imageId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefSources.RefSourceName + r.Tbl90RefSources.SourceYear),
				p => p.Tbl90RefSources);
		}

		public IList<Tbl90Reference> ListTbl90RefExpertsByImageId(int imageId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefExpertID == e.Tbl90RefExperts.RefExpertID && e.ImageID == imageId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefExperts.RefExpertName),
				p => p.Tbl90RefExperts);
		}

	               //------------------------------------------------
 
		public IList<Tbl93Comment> ListTbl93CommentsByImageId(int imageId)
		{
			return _tbl93CommentsRepository.ListWhereOrderByInclude(
				e => e.ImageID == imageId,
				_tbl93CommentsRepository.OrderBy(r => r.Info),
				p => p.NULL);
		}      
  

         
}   

