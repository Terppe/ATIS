using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Te.Atis.BusinessLayer;
using Te.Atis.DomainModel;
using Te.Atis.Ui.Desktop.Views.Report.PDF;    

// <!-- Interface Skriptdatum:  13.06.2018  12:32     -->  

namespace Te.Atis.Ui.Desktop.Views.Report   
{       

           
     public void GetTbl09DivisionsById(int id)
    {
        Tbl09DivisionsList = new ObservableCollection<Tbl09Division>(_businessLayer.ListTbl09DivisionsByDivisionId(id));		   
        
        //direct children
        Tbl15SubdivisionsList = new ObservableCollection<Tbl15Subdivision>(_businessLayer.ListTbl15SubdivisionsByDivisionIdAndHash(id));

        //------------------------------------------------------------------------------

         var regnumDivisionId = RegnumIdTbl09DivisionsSelect(id);	

        Tbl03RegnumsList = new ObservableCollection<Tbl03Regnum>(_businessLayer.ListTbl03RegnumsByRegnumIdAndHash(regnumDivisionId));		   
           
        //------------------------------------------------------------------------------
        Tbl90RefExpertsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90RefExpertsByDivisionId(id));

        Tbl90RefSourcesList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90RefSourcesByDivisionId(id));

        Tbl90RefAuthorsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90RefAuthorsByDivisionId(id));

        Tbl90ExpertsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ExpertsByDivisionId(id));

        Tbl90SourcesList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90SourcesByDivisionId(id));

        Tbl90AuthorsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90AuthorsByDivisionId(id));

        Tbl93CommentsList = new ObservableCollection<Tbl93Comment>(_businessLayer.ListTbl93CommentsByDivisionId(id));
    } 
    private RelayCommand _pdfTbl09DivisionsCommand;
    public ICommand PdfTbl09DivisionsCommand
    {
        get { return _pdfTbl09DivisionsCommand ?? (_pdfTbl09DivisionsCommand = new RelayCommand(delegate { CreatePdfTbl09Divisions(_mainId); })); }
    }

    private static void CreatePdfTbl09Divisions(int id)
    {
        ReportTbl09DivisionsPdf.CreateMainPdf(id);
    }
    //------------------------------------------------------------------------------
    //------------------------------------------------------------------------------  		   
           
        IList<Tbl09Division> ListTbl09DivisionsByDivisionId(int divisionId);

        IList<Tbl15Subdivision> ListTbl15SubdivisionsByDivisionIdAndHash(int divisionId);

        IList<Tbl90Reference> ListTbl90AuthorsByDivisionId(int divisionId);
        IList<Tbl90Reference> ListTbl90SourcesByDivisionId(int divisionId);
        IList<Tbl90Reference> ListTbl90ExpertsByDivisionId(int divisionId);

        IList<Tbl93Comment> ListTbl93CommentsByDivisionId(int divisionId);	   
           
		public IList<Tbl09Division> ListTbl09DivisionsByDivisionId(int divisionId)
		{
			return _tbl09DivisionsRepository.ListWhereOrderByInclude(
				e => e.DivisionID == divisionId,
				_tbl09DivisionsRepository.OrderBy(r => r.DivisionName + r.Subregnum),
				p => p.Tbl15Subdivisions, k => k.NULL);
		}

		public IList<Tbl15Subdivision> ListTbl15SubdivisionsByDivisionIdAndHash(int divisionId)
		{
			return _tbl15SubdivisionsRepository.ListWhereOrderByInclude(
				e => e.DivisionID == divisionId &&
				e.SubdivisionName.Contains("#") == false,
				_tbl15SubdivisionsRepository.OrderBy(r => r.SubdivisionName),
				p => p.NULL, k => k.Tbl09Divisions);
		}

	               //------------------------------------------------

		public IList<Tbl90Reference> ListTbl90AuthorsByDivisionId(int divisionId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefAuthorID == e.Tbl90RefAuthors.RefAuthorID && e.DivisionID == divisionId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefAuthors.RefAuthorName + r.Tbl90RefAuthors.ArticelTitle + r.Tbl90RefAuthors.BookName + r.Tbl90RefAuthors.Page1 + r.Tbl90RefAuthors.Publisher),
				p => p.Tbl90RefAuthors);
		}

		public IList<Tbl90Reference> ListTbl90SourcesByDivisionId(int divisionId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefSourceID == e.Tbl90RefSources.RefSourceID && e.DivisionID == divisionId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefSources.RefSourceName + r.Tbl90RefSources.SourceYear),
				p => p.Tbl90RefSources);
		}

		public IList<Tbl90Reference> ListTbl90ExpertsByDivisionId(int divisionId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefExpertID == e.Tbl90RefExperts.RefExpertID && e.DivisionID == divisionId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefExperts.RefExpertName),
				p => p.Tbl90RefExperts);
		}

		public IList<Tbl90Reference> ListTbl90RefAuthorsByDivisionId(int divisionId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefAuthorID == e.Tbl90RefAuthors.RefAuthorID && e.DivisionID == divisionId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefAuthors.RefAuthorName + r.Tbl90RefAuthors.ArticelTitle + r.Tbl90RefAuthors.BookName + r.Tbl90RefAuthors.Page1 + r.Tbl90RefAuthors.Publisher),
				p => p.Tbl90RefAuthors);
		}

		public IList<Tbl90Reference> ListTbl90RefSourcesByDivisionId(int divisionId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefSourceID == e.Tbl90RefSources.RefSourceID && e.DivisionID == divisionId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefSources.RefSourceName + r.Tbl90RefSources.SourceYear),
				p => p.Tbl90RefSources);
		}

		public IList<Tbl90Reference> ListTbl90RefExpertsByDivisionId(int divisionId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefExpertID == e.Tbl90RefExperts.RefExpertID && e.DivisionID == divisionId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefExperts.RefExpertName),
				p => p.Tbl90RefExperts);
		}

	               //------------------------------------------------
 
		public IList<Tbl93Comment> ListTbl93CommentsByDivisionId(int divisionId)
		{
			return _tbl93CommentsRepository.ListWhereOrderByInclude(
				e => e.DivisionID == divisionId,
				_tbl93CommentsRepository.OrderBy(r => r.Info),
				p => p.Tbl15Subdivisions);
		}      
  

         
}   

