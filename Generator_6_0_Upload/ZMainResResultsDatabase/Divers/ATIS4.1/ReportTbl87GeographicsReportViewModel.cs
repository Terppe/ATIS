using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Te.Atis.BusinessLayer;
using Te.Atis.DomainModel;
using Te.Atis.Ui.Desktop.Views.Report.PDF;    

// <!-- Interface Skriptdatum:  17.07.2018  10:32     -->  

namespace Te.Atis.Ui.Desktop.Views.Report   
{       

           
     public void GetTbl87GeographicsById(int id)
    {
        Tbl87GeographicsList = new ObservableCollection<Tbl87Geographic>(_businessLayer.ListTbl87GeographicsByGeographicId(id));		   
           
        //------------------------------------------------------------------------------
        Tbl90RefExpertsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90RefExpertsByGeographicId(id));

        Tbl90RefSourcesList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90RefSourcesByGeographicId(id));

        Tbl90RefAuthorsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90RefAuthorsByGeographicId(id));

        Tbl90ExpertsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ExpertsByGeographicId(id));

        Tbl90SourcesList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90SourcesByGeographicId(id));

        Tbl90AuthorsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90AuthorsByGeographicId(id));

        Tbl93CommentsList = new ObservableCollection<Tbl93Comment>(_businessLayer.ListTbl93CommentsByGeographicId(id));
    } 
    private RelayCommand _pdfTbl87GeographicsCommand;
    public ICommand PdfTbl87GeographicsCommand
    {
        get { return _pdfTbl87GeographicsCommand ?? (_pdfTbl87GeographicsCommand = new RelayCommand(delegate { CreatePdfTbl87Geographics(_mainId); })); }
    }

    private static void CreatePdfTbl87Geographics(int id)
    {
        ReportTbl87GeographicsPdf.CreateMainPdf(id);
    }
    //------------------------------------------------------------------------------
    //------------------------------------------------------------------------------  		   
           
        IList<Tbl87Geographic> ListTbl87GeographicsByGeographicId(int geographicId);

        IList<Tbl68Speciesgroup> ListNULLByGeographicIdAndHash(int geographicId);

        IList<Tbl90Reference> ListTbl90AuthorsByGeographicId(int geographicId);
        IList<Tbl90Reference> ListTbl90SourcesByGeographicId(int geographicId);
        IList<Tbl90Reference> ListTbl90ExpertsByGeographicId(int geographicId);

        IList<Tbl93Comment> ListTbl93CommentsByGeographicId(int geographicId);	   
           
		public IList<Tbl87Geographic> ListTbl87GeographicsByGeographicId(int geographicId)
		{
			return _tbl87GeographicsRepository.ListWhereOrderByInclude(
				e => e.GeographicID == geographicId,
				_tbl87GeographicsRepository.OrderBy(r => r.GeographicName + r.Subregnum),
				p => p.NULL, k => k.NULL);
		}

		public IList<Tbl68Speciesgroup> ListNULLByGeographicIdAndHash(int geographicId)
		{
			return _tbl68SpeciesgroupsRepository.ListWhereOrderByInclude(
				e => e.GeographicID == geographicId &&
				e.NULL.Contains("#") == false,
				_tbl68SpeciesgroupsRepository.OrderBy(r => r.NULL),
				p => p.NULL, k => k.Tbl87Geographics);
		}

	               //------------------------------------------------

		public IList<Tbl90Reference> ListTbl90AuthorsByGeographicId(int geographicId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefAuthorID == e.Tbl90RefAuthors.RefAuthorID && e.GeographicID == geographicId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefAuthors.RefAuthorName + r.Tbl90RefAuthors.ArticelTitle + r.Tbl90RefAuthors.BookName + r.Tbl90RefAuthors.Page1 + r.Tbl90RefAuthors.Publisher),
				p => p.Tbl90RefAuthors);
		}

		public IList<Tbl90Reference> ListTbl90SourcesByGeographicId(int geographicId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefSourceID == e.Tbl90RefSources.RefSourceID && e.GeographicID == geographicId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefSources.RefSourceName + r.Tbl90RefSources.SourceYear),
				p => p.Tbl90RefSources);
		}

		public IList<Tbl90Reference> ListTbl90ExpertsByGeographicId(int geographicId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefExpertID == e.Tbl90RefExperts.RefExpertID && e.GeographicID == geographicId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefExperts.RefExpertName),
				p => p.Tbl90RefExperts);
		}

		public IList<Tbl90Reference> ListTbl90RefAuthorsByGeographicId(int geographicId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefAuthorID == e.Tbl90RefAuthors.RefAuthorID && e.GeographicID == geographicId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefAuthors.RefAuthorName + r.Tbl90RefAuthors.ArticelTitle + r.Tbl90RefAuthors.BookName + r.Tbl90RefAuthors.Page1 + r.Tbl90RefAuthors.Publisher),
				p => p.Tbl90RefAuthors);
		}

		public IList<Tbl90Reference> ListTbl90RefSourcesByGeographicId(int geographicId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefSourceID == e.Tbl90RefSources.RefSourceID && e.GeographicID == geographicId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefSources.RefSourceName + r.Tbl90RefSources.SourceYear),
				p => p.Tbl90RefSources);
		}

		public IList<Tbl90Reference> ListTbl90RefExpertsByGeographicId(int geographicId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefExpertID == e.Tbl90RefExperts.RefExpertID && e.GeographicID == geographicId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefExperts.RefExpertName),
				p => p.Tbl90RefExperts);
		}

	               //------------------------------------------------
 
		public IList<Tbl93Comment> ListTbl93CommentsByGeographicId(int geographicId)
		{
			return _tbl93CommentsRepository.ListWhereOrderByInclude(
				e => e.GeographicID == geographicId,
				_tbl93CommentsRepository.OrderBy(r => r.Info),
				p => p.NULL);
		}      
  

         
}   

