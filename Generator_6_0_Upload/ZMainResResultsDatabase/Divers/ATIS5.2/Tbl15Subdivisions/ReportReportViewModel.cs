using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Te.Atis.BusinessLayer;
using Te.Atis.DomainModel;
using Te.Atis.Ui.Desktop.Views.Report.PDF;    

// <!-- Interface Skriptdatum:  07.11.2018  12:32     -->  

namespace Te.Atis.Ui.Desktop.Views.Report   
{       

           
     public void GetTbl15SubdivisionsById(int id)
    {
        Tbl15SubdivisionsList = new ObservableCollection<Tbl15Subdivision>(_businessLayer.ListTbl15SubdivisionsBySubdivisionId(id));		   
        
        //direct children
        Tbl18SuperclassesList = new ObservableCollection<Tbl18Superclass>(_businessLayer.ListTbl18SuperclassesBySubdivisionIdAndHash(id));

        //------------------------------------------------------------------------------

         var divisionSubdivisionId = DivisionIdTbl15SubdivisionsSelect(id);	

        Tbl09DivisionsList = new ObservableCollection<Tbl09Division>(_businessLayer.ListTbl09DivisionsByDivisionIdAndHash(divisionSubdivisionId));		

        var regnumDivisionId = RegnumIdTbl09DivisionsSelect(divisionSubdivisionId);

        Tbl03RegnumsList = new ObservableCollection<Tbl03Regnum>(_businessLayer.ListTbl03RegnumsByRegnumIdAndHash(regnumDivisionId));     
           
        //------------------------------------------------------------------------------
        Tbl90RefExpertsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90RefExpertsBySubdivisionId(id));

        Tbl90RefSourcesList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90RefSourcesBySubdivisionId(id));

        Tbl90RefAuthorsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90RefAuthorsBySubdivisionId(id));

        Tbl90ExpertsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ExpertsBySubdivisionId(id));

        Tbl90SourcesList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90SourcesBySubdivisionId(id));

        Tbl90AuthorsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90AuthorsBySubdivisionId(id));

        Tbl93CommentsList = new ObservableCollection<Tbl93Comment>(_businessLayer.ListTbl93CommentsBySubdivisionId(id));
    } 
    private RelayCommand _pdfTbl15SubdivisionsCommand;
    public ICommand PdfTbl15SubdivisionsCommand
    {
        get { return _pdfTbl15SubdivisionsCommand ?? (_pdfTbl15SubdivisionsCommand = new RelayCommand(delegate { CreatePdfTbl15Subdivisions(_mainId); })); }
    }

    private static void CreatePdfTbl15Subdivisions(int id)
    {
        ReportTbl15SubdivisionsPdf.CreateMainPdf(id);
    }
    //------------------------------------------------------------------------------
    //------------------------------------------------------------------------------  		   
           
        IList<Tbl15Subdivision> ListTbl15SubdivisionsBySubdivisionId(int subdivisionId);

        IList<Tbl18Superclass> ListTbl18SuperclassesBySubdivisionIdAndHash(int subdivisionId);

        IList<Tbl90Reference> ListTbl90AuthorsBySubdivisionId(int subdivisionId);
        IList<Tbl90Reference> ListTbl90SourcesBySubdivisionId(int subdivisionId);
        IList<Tbl90Reference> ListTbl90ExpertsBySubdivisionId(int subdivisionId);

        IList<Tbl93Comment> ListTbl93CommentsBySubdivisionId(int subdivisionId);	   
           
		public IList<Tbl15Subdivision> ListTbl15SubdivisionsBySubdivisionId(int subdivisionId)
		{
			return _tbl15SubdivisionsRepository.ListWhereOrderByInclude(
				e => e.SubdivisionID == subdivisionId,
				_tbl15SubdivisionsRepository.OrderBy(r => r.SubdivisionName + r.Subregnum),
				p => p.Tbl18Superclasses, k => k.Tbl21Classes);
		}

		public IList<Tbl18Superclass> ListTbl18SuperclassesBySubdivisionIdAndHash(int subdivisionId)
		{
			return _tbl18SuperclassesRepository.ListWhereOrderByInclude(
				e => e.SubdivisionID == subdivisionId &&
				e.SuperclassName.Contains("#") == false,
				_tbl18SuperclassesRepository.OrderBy(r => r.SuperclassName),
				p => p.NULL, k => k.Tbl15Subdivisions);
		}

	               //------------------------------------------------

		public IList<Tbl90Reference> ListTbl90AuthorsBySubdivisionId(int subdivisionId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefAuthorID == e.Tbl90RefAuthors.RefAuthorID && e.SubdivisionID == subdivisionId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefAuthors.RefAuthorName + r.Tbl90RefAuthors.ArticelTitle + r.Tbl90RefAuthors.BookName + r.Tbl90RefAuthors.Page1 + r.Tbl90RefAuthors.Publisher),
				p => p.Tbl90RefAuthors);
		}

		public IList<Tbl90Reference> ListTbl90SourcesBySubdivisionId(int subdivisionId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefSourceID == e.Tbl90RefSources.RefSourceID && e.SubdivisionID == subdivisionId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefSources.RefSourceName + r.Tbl90RefSources.SourceYear),
				p => p.Tbl90RefSources);
		}

		public IList<Tbl90Reference> ListTbl90ExpertsBySubdivisionId(int subdivisionId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefExpertID == e.Tbl90RefExperts.RefExpertID && e.SubdivisionID == subdivisionId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefExperts.RefExpertName),
				p => p.Tbl90RefExperts);
		}

		public IList<Tbl90Reference> ListTbl90RefAuthorsBySubdivisionId(int subdivisionId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefAuthorID == e.Tbl90RefAuthors.RefAuthorID && e.SubdivisionID == subdivisionId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefAuthors.RefAuthorName + r.Tbl90RefAuthors.ArticelTitle + r.Tbl90RefAuthors.BookName + r.Tbl90RefAuthors.Page1 + r.Tbl90RefAuthors.Publisher),
				p => p.Tbl90RefAuthors);
		}

		public IList<Tbl90Reference> ListTbl90RefSourcesBySubdivisionId(int subdivisionId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefSourceID == e.Tbl90RefSources.RefSourceID && e.SubdivisionID == subdivisionId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefSources.RefSourceName + r.Tbl90RefSources.SourceYear),
				p => p.Tbl90RefSources);
		}

		public IList<Tbl90Reference> ListTbl90RefExpertsBySubdivisionId(int subdivisionId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefExpertID == e.Tbl90RefExperts.RefExpertID && e.SubdivisionID == subdivisionId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefExperts.RefExpertName),
				p => p.Tbl90RefExperts);
		}

	               //------------------------------------------------
 
		public IList<Tbl93Comment> ListTbl93CommentsBySubdivisionId(int subdivisionId)
		{
			return _tbl93CommentsRepository.ListWhereOrderByInclude(
				e => e.SubdivisionID == subdivisionId,
				_tbl93CommentsRepository.OrderBy(r => r.Info),
				p => p.Tbl18Superclasses);
		}      
  

         
}   

