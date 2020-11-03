using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Te.Atis.BusinessLayer;
using Te.Atis.DomainModel;
using Te.Atis.Ui.Desktop.Views.Report.PDF;    

// <!-- Interface Skriptdatum:  01.11.2020  12:32       -->  

namespace Te.Atis.Ui.Desktop.Views.Report   
{       

        
     public void GetTbl03RegnumsById(int id)
    {
        Tbl03RegnumsList = new ObservableCollection<Tbl03Regnum>(_businessLayer.ListTbl03RegnumsByRegnumId(id));	   
        
        //direct children
        Tbl06PhylumsList = new ObservableCollection<Tbl06Phylum>(_businessLayer.ListTbl06PhylumsByRegnumIdAndHash(id));

        Tbl09DivisionsList = new ObservableCollection<Tbl09Division>(_businessLayer.ListTbl09DivisionsByRegnumIdAndHash(id));	   
           
        //------------------------------------------------------------------------------
        Tbl90RefExpertsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90RefExpertsByRegnumId(id));

        Tbl90RefSourcesList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90RefSourcesByRegnumId(id));

        Tbl90RefAuthorsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90RefAuthorsByRegnumId(id));

        Tbl90ExpertsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ExpertsByRegnumId(id));

        Tbl90SourcesList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90SourcesByRegnumId(id));

        Tbl90AuthorsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90AuthorsByRegnumId(id));

        Tbl93CommentsList = new ObservableCollection<Tbl93Comment>(_businessLayer.ListTbl93CommentsByRegnumId(id));
    } 
    private RelayCommand _pdfTbl03RegnumsCommand;
    public ICommand PdfTbl03RegnumsCommand
    {
        get { return _pdfTbl03RegnumsCommand ?? (_pdfTbl03RegnumsCommand = new RelayCommand(delegate { CreatePdfTbl03Regnums(_mainId); })); }
    }

    private static void CreatePdfTbl03Regnums(int id)
    {
        ReportTbl03RegnumsPdf.CreateMainPdf(id);
    }
    //------------------------------------------------------------------------------
    //------------------------------------------------------------------------------  		   
        
        IList<Tbl03Regnum> ListTbl03RegnumsByRegnumId(int regnumId);
        IList<Tbl06Phylum> ListTbl06PhylumsByRegnumIdAndHash(int regnumId);
        IList<Tbl09Division> ListTbl09DivisionsByRegnumIdAndHash(int regnumId);

        IList<Tbl90Reference> ListTbl90AuthorsByRegnumId(int regnumId);
        IList<Tbl90Reference> ListTbl90SourcesByRegnumId(int regnumId);
        IList<Tbl90Reference> ListTbl90ExpertsByRegnumId(int regnumId);

        IList<Tbl93Comment> ListTbl93CommentsByRegnumId(int regnumId);   
        
		public IList<Tbl03Regnum> ListTbl03RegnumsByRegnumId(int regnumId)
		{
			return _tbl03RegnumsRepository.ListWhereOrderByInclude(
				e => e.RegnumID == regnumId,
				_tbl03RegnumsRepository.OrderBy(r => r.RegnumName + r.Subregnum),
				p => p.Tbl06Phylums, k => k.Tbl09Divisions);
		}

		public IList<Tbl06Phylum> ListTbl06PhylumsByRegnumIdAndHash(int regnumId)
		{
			return _tbl06PhylumsRepository.ListWhereOrderByInclude(
				e => e.RegnumID == regnumId &&
				e.PhylumName.Contains("#") == false,
				_tbl06PhylumsRepository.OrderBy(r => r.PhylumName),
				p => p.Tbl12Subphylums, k => k.Tbl03Regnums);
		}

		public IList<Tbl09Division> ListTbl09DivisionsByRegnumIdAndHash(int regnumId)
		{
			return _tbl09DivisionsRepository.ListWhereOrderByInclude(
				e => e.RegnumID == regnumId &&
				e.DivisionName.Contains("#") == false,
				_tbl09DivisionsRepository.OrderBy(r => r.DivisionName),
				p => p.Tbl15Subdivisions, k => k.Tbl03Regnums);
		}

	               //------------------------------------------------

		public IList<Tbl90Reference> ListTbl90AuthorsByRegnumId(int regnumId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefAuthorID == e.Tbl90RefAuthors.RefAuthorID && e.RegnumID == regnumId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefAuthors.RefAuthorName + r.Tbl90RefAuthors.ArticelTitle + r.Tbl90RefAuthors.BookName + r.Tbl90RefAuthors.Page1 + r.Tbl90RefAuthors.Publisher),
				p => p.Tbl90RefAuthors);
		}

		public IList<Tbl90Reference> ListTbl90SourcesByRegnumId(int regnumId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefSourceID == e.Tbl90RefSources.RefSourceID && e.RegnumID == regnumId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefSources.RefSourceName + r.Tbl90RefSources.SourceYear),
				p => p.Tbl90RefSources);
		}

		public IList<Tbl90Reference> ListTbl90ExpertsByRegnumId(int regnumId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefExpertID == e.Tbl90RefExperts.RefExpertID && e.RegnumID == regnumId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefExperts.RefExpertName),
				p => p.Tbl90RefExperts);
		}

		public IList<Tbl90Reference> ListTbl90RefAuthorsByRegnumId(int regnumId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefAuthorID == e.Tbl90RefAuthors.RefAuthorID && e.RegnumID == regnumId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefAuthors.RefAuthorName + r.Tbl90RefAuthors.ArticelTitle + r.Tbl90RefAuthors.BookName + r.Tbl90RefAuthors.Page1 + r.Tbl90RefAuthors.Publisher),
				p => p.Tbl90RefAuthors);
		}

		public IList<Tbl90Reference> ListTbl90RefSourcesByRegnumId(int regnumId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefSourceID == e.Tbl90RefSources.RefSourceID && e.RegnumID == regnumId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefSources.RefSourceName + r.Tbl90RefSources.SourceYear),
				p => p.Tbl90RefSources);
		}

		public IList<Tbl90Reference> ListTbl90RefExpertsByRegnumId(int regnumId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefExpertID == e.Tbl90RefExperts.RefExpertID && e.RegnumID == regnumId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefExperts.RefExpertName),
				p => p.Tbl90RefExperts);
		}

	               //------------------------------------------------

		public IList<Tbl93Comment> ListTbl93CommentsByRegnumId(int regnumId)
		{
			return _tbl93CommentsRepository.ListWhereOrderByInclude(
				e => e.RegnumID == regnumId,
				_tbl93CommentsRepository.OrderBy(r => r.Info),
				p => p.Tbl06Phylums, k => k.Tbl09Divisions);
		}      
  

         
}   

