using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Te.Atis.BusinessLayer;
using Te.Atis.DomainModel;
using Te.Atis.Ui.Desktop.Views.Report.PDF;    

// <!-- Interface Skriptdatum:  12.11.2018  10:32     -->  

namespace Te.Atis.Ui.Desktop.Views.Report   
{       

           
     public void GetTbl78NamesById(int id)
    {
        Tbl78NamesList = new ObservableCollection<Tbl78Name>(_businessLayer.ListTbl78NamesByNameId(id));		   
           
        //------------------------------------------------------------------------------
        Tbl90RefExpertsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90RefExpertsByNameId(id));

        Tbl90RefSourcesList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90RefSourcesByNameId(id));

        Tbl90RefAuthorsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90RefAuthorsByNameId(id));

        Tbl90ExpertsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ExpertsByNameId(id));

        Tbl90SourcesList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90SourcesByNameId(id));

        Tbl90AuthorsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90AuthorsByNameId(id));

        Tbl93CommentsList = new ObservableCollection<Tbl93Comment>(_businessLayer.ListTbl93CommentsByNameId(id));
    } 
    private RelayCommand _pdfTbl78NamesCommand;
    public ICommand PdfTbl78NamesCommand
    {
        get { return _pdfTbl78NamesCommand ?? (_pdfTbl78NamesCommand = new RelayCommand(delegate { CreatePdfTbl78Names(_mainId); })); }
    }

    private static void CreatePdfTbl78Names(int id)
    {
        ReportTbl78NamesPdf.CreateMainPdf(id);
    }
    //------------------------------------------------------------------------------
    //------------------------------------------------------------------------------  		   
           
        IList<Tbl78Name> ListTbl78NamesByNameId(int nameId);

        IList<Tbl68Speciesgroup> ListNULLByNameIdAndHash(int nameId);

        IList<Tbl90Reference> ListTbl90AuthorsByNameId(int nameId);
        IList<Tbl90Reference> ListTbl90SourcesByNameId(int nameId);
        IList<Tbl90Reference> ListTbl90ExpertsByNameId(int nameId);

        IList<Tbl93Comment> ListTbl93CommentsByNameId(int nameId);	   
           
		public IList<Tbl78Name> ListTbl78NamesByNameId(int nameId)
		{
			return _tbl78NamesRepository.ListWhereOrderByInclude(
				e => e.NameID == nameId,
				_tbl78NamesRepository.OrderBy(r => r.NameName + r.Subregnum),
				p => p.NULL, k => k.NULL);
		}

		public IList<Tbl68Speciesgroup> ListNULLByNameIdAndHash(int nameId)
		{
			return _tbl68SpeciesgroupsRepository.ListWhereOrderByInclude(
				e => e.NameID == nameId &&
				e.NULL.Contains("#") == false,
				_tbl68SpeciesgroupsRepository.OrderBy(r => r.NULL),
				p => p.NULL, k => k.Tbl78Names);
		}

	               //------------------------------------------------

		public IList<Tbl90Reference> ListTbl90AuthorsByNameId(int nameId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefAuthorID == e.Tbl90RefAuthors.RefAuthorID && e.NameID == nameId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefAuthors.RefAuthorName + r.Tbl90RefAuthors.ArticelTitle + r.Tbl90RefAuthors.BookName + r.Tbl90RefAuthors.Page1 + r.Tbl90RefAuthors.Publisher),
				p => p.Tbl90RefAuthors);
		}

		public IList<Tbl90Reference> ListTbl90SourcesByNameId(int nameId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefSourceID == e.Tbl90RefSources.RefSourceID && e.NameID == nameId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefSources.RefSourceName + r.Tbl90RefSources.SourceYear),
				p => p.Tbl90RefSources);
		}

		public IList<Tbl90Reference> ListTbl90ExpertsByNameId(int nameId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefExpertID == e.Tbl90RefExperts.RefExpertID && e.NameID == nameId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefExperts.RefExpertName),
				p => p.Tbl90RefExperts);
		}

		public IList<Tbl90Reference> ListTbl90RefAuthorsByNameId(int nameId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefAuthorID == e.Tbl90RefAuthors.RefAuthorID && e.NameID == nameId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefAuthors.RefAuthorName + r.Tbl90RefAuthors.ArticelTitle + r.Tbl90RefAuthors.BookName + r.Tbl90RefAuthors.Page1 + r.Tbl90RefAuthors.Publisher),
				p => p.Tbl90RefAuthors);
		}

		public IList<Tbl90Reference> ListTbl90RefSourcesByNameId(int nameId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefSourceID == e.Tbl90RefSources.RefSourceID && e.NameID == nameId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefSources.RefSourceName + r.Tbl90RefSources.SourceYear),
				p => p.Tbl90RefSources);
		}

		public IList<Tbl90Reference> ListTbl90RefExpertsByNameId(int nameId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefExpertID == e.Tbl90RefExperts.RefExpertID && e.NameID == nameId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefExperts.RefExpertName),
				p => p.Tbl90RefExperts);
		}

	               //------------------------------------------------
 
		public IList<Tbl93Comment> ListTbl93CommentsByNameId(int nameId)
		{
			return _tbl93CommentsRepository.ListWhereOrderByInclude(
				e => e.NameID == nameId,
				_tbl93CommentsRepository.OrderBy(r => r.Info),
				p => p.NULL);
		}      
  

         
}   

