using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Te.Atis.BusinessLayer;
using Te.Atis.DomainModel;
using Te.Atis.Ui.Desktop.Views.Report.PDF;    

// <!-- Interface Skriptdatum:  13.11.2018  10:32     -->  

namespace Te.Atis.Ui.Desktop.Views.Report   
{       

           
     public void GetTbl84SynonymsById(int id)
    {
        Tbl84SynonymsList = new ObservableCollection<Tbl84Synonym>(_businessLayer.ListTbl84SynonymsBySynonymId(id));		   
           
        //------------------------------------------------------------------------------
        Tbl90RefExpertsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90RefExpertsBySynonymId(id));

        Tbl90RefSourcesList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90RefSourcesBySynonymId(id));

        Tbl90RefAuthorsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90RefAuthorsBySynonymId(id));

        Tbl90ExpertsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ExpertsBySynonymId(id));

        Tbl90SourcesList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90SourcesBySynonymId(id));

        Tbl90AuthorsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90AuthorsBySynonymId(id));

        Tbl93CommentsList = new ObservableCollection<Tbl93Comment>(_businessLayer.ListTbl93CommentsBySynonymId(id));
    } 
    private RelayCommand _pdfTbl84SynonymsCommand;
    public ICommand PdfTbl84SynonymsCommand
    {
        get { return _pdfTbl84SynonymsCommand ?? (_pdfTbl84SynonymsCommand = new RelayCommand(delegate { CreatePdfTbl84Synonyms(_mainId); })); }
    }

    private static void CreatePdfTbl84Synonyms(int id)
    {
        ReportTbl84SynonymsPdf.CreateMainPdf(id);
    }
    //------------------------------------------------------------------------------
    //------------------------------------------------------------------------------  		   
           
        IList<Tbl84Synonym> ListTbl84SynonymsBySynonymId(int synonymId);

        IList<Tbl68Speciesgroup> ListNULLBySynonymIdAndHash(int synonymId);

        IList<Tbl90Reference> ListTbl90AuthorsBySynonymId(int synonymId);
        IList<Tbl90Reference> ListTbl90SourcesBySynonymId(int synonymId);
        IList<Tbl90Reference> ListTbl90ExpertsBySynonymId(int synonymId);

        IList<Tbl93Comment> ListTbl93CommentsBySynonymId(int synonymId);	   
           
		public IList<Tbl84Synonym> ListTbl84SynonymsBySynonymId(int synonymId)
		{
			return _tbl84SynonymsRepository.ListWhereOrderByInclude(
				e => e.SynonymID == synonymId,
				_tbl84SynonymsRepository.OrderBy(r => r.SynonymName + r.Subregnum),
				p => p.NULL, k => k.NULL);
		}

		public IList<Tbl68Speciesgroup> ListNULLBySynonymIdAndHash(int synonymId)
		{
			return _tbl68SpeciesgroupsRepository.ListWhereOrderByInclude(
				e => e.SynonymID == synonymId &&
				e.NULL.Contains("#") == false,
				_tbl68SpeciesgroupsRepository.OrderBy(r => r.NULL),
				p => p.NULL, k => k.Tbl84Synonyms);
		}

	               //------------------------------------------------

		public IList<Tbl90Reference> ListTbl90AuthorsBySynonymId(int synonymId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefAuthorID == e.Tbl90RefAuthors.RefAuthorID && e.SynonymID == synonymId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefAuthors.RefAuthorName + r.Tbl90RefAuthors.ArticelTitle + r.Tbl90RefAuthors.BookName + r.Tbl90RefAuthors.Page1 + r.Tbl90RefAuthors.Publisher),
				p => p.Tbl90RefAuthors);
		}

		public IList<Tbl90Reference> ListTbl90SourcesBySynonymId(int synonymId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefSourceID == e.Tbl90RefSources.RefSourceID && e.SynonymID == synonymId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefSources.RefSourceName + r.Tbl90RefSources.SourceYear),
				p => p.Tbl90RefSources);
		}

		public IList<Tbl90Reference> ListTbl90ExpertsBySynonymId(int synonymId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefExpertID == e.Tbl90RefExperts.RefExpertID && e.SynonymID == synonymId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefExperts.RefExpertName),
				p => p.Tbl90RefExperts);
		}

		public IList<Tbl90Reference> ListTbl90RefAuthorsBySynonymId(int synonymId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefAuthorID == e.Tbl90RefAuthors.RefAuthorID && e.SynonymID == synonymId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefAuthors.RefAuthorName + r.Tbl90RefAuthors.ArticelTitle + r.Tbl90RefAuthors.BookName + r.Tbl90RefAuthors.Page1 + r.Tbl90RefAuthors.Publisher),
				p => p.Tbl90RefAuthors);
		}

		public IList<Tbl90Reference> ListTbl90RefSourcesBySynonymId(int synonymId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefSourceID == e.Tbl90RefSources.RefSourceID && e.SynonymID == synonymId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefSources.RefSourceName + r.Tbl90RefSources.SourceYear),
				p => p.Tbl90RefSources);
		}

		public IList<Tbl90Reference> ListTbl90RefExpertsBySynonymId(int synonymId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefExpertID == e.Tbl90RefExperts.RefExpertID && e.SynonymID == synonymId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefExperts.RefExpertName),
				p => p.Tbl90RefExperts);
		}

	               //------------------------------------------------
 
		public IList<Tbl93Comment> ListTbl93CommentsBySynonymId(int synonymId)
		{
			return _tbl93CommentsRepository.ListWhereOrderByInclude(
				e => e.SynonymID == synonymId,
				_tbl93CommentsRepository.OrderBy(r => r.Info),
				p => p.NULL);
		}      
  

         
}   

