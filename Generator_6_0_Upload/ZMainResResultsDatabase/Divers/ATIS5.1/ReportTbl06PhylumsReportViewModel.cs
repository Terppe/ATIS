using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Te.Atis.BusinessLayer;
using Te.Atis.DomainModel;
using Te.Atis.Ui.Desktop.Views.Report.PDF;    

// <!-- Interface Skriptdatum:  21.01.2019  12:32     -->  

namespace Te.Atis.Ui.Desktop.Views.Report   
{       

           
     public void GetTbl06PhylumsById(int id)
    {
        Tbl06PhylumsList = new ObservableCollection<Tbl06Phylum>(_businessLayer.ListTbl06PhylumsByPhylumId(id));		   
        
        //direct children
        Tbl12SubphylumsList = new ObservableCollection<Tbl12Subphylum>(_businessLayer.ListTbl12SubphylumsByPhylumIdAndHash(id));

        //------------------------------------------------------------------------------

         var regnumPhylumId = RegnumIdTbl06PhylumsSelect(id);	

        Tbl03RegnumsList = new ObservableCollection<Tbl03Regnum>(_businessLayer.ListTbl03RegnumsByRegnumIdAndHash(regnumPhylumId));		   
           
        //------------------------------------------------------------------------------
        Tbl90RefExpertsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90RefExpertsByPhylumId(id));

        Tbl90RefSourcesList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90RefSourcesByPhylumId(id));

        Tbl90RefAuthorsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90RefAuthorsByPhylumId(id));

        Tbl90ExpertsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ExpertsByPhylumId(id));

        Tbl90SourcesList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90SourcesByPhylumId(id));

        Tbl90AuthorsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90AuthorsByPhylumId(id));

        Tbl93CommentsList = new ObservableCollection<Tbl93Comment>(_businessLayer.ListTbl93CommentsByPhylumId(id));
    } 
    private RelayCommand _pdfTbl06PhylumsCommand;
    public ICommand PdfTbl06PhylumsCommand
    {
        get { return _pdfTbl06PhylumsCommand ?? (_pdfTbl06PhylumsCommand = new RelayCommand(delegate { CreatePdfTbl06Phylums(_mainId); })); }
    }

    private static void CreatePdfTbl06Phylums(int id)
    {
        ReportTbl06PhylumsPdf.CreateMainPdf(id);
    }
    //------------------------------------------------------------------------------
    //------------------------------------------------------------------------------  		   
           
        IList<Tbl06Phylum> ListTbl06PhylumsByPhylumId(int phylumId);

        IList<Tbl12Subphylum> ListTbl12SubphylumsByPhylumIdAndHash(int phylumId);

        IList<Tbl90Reference> ListTbl90AuthorsByPhylumId(int phylumId);
        IList<Tbl90Reference> ListTbl90SourcesByPhylumId(int phylumId);
        IList<Tbl90Reference> ListTbl90ExpertsByPhylumId(int phylumId);

        IList<Tbl93Comment> ListTbl93CommentsByPhylumId(int phylumId);	   
           
		public IList<Tbl06Phylum> ListTbl06PhylumsByPhylumId(int phylumId)
		{
			return _tbl06PhylumsRepository.ListWhereOrderByInclude(
				e => e.PhylumID == phylumId,
				_tbl06PhylumsRepository.OrderBy(r => r.PhylumName + r.Subregnum),
				p => p.Tbl12Subphylums, k => k.Tbl18Superclasses);
		}

		public IList<Tbl12Subphylum> ListTbl12SubphylumsByPhylumIdAndHash(int phylumId)
		{
			return _tbl12SubphylumsRepository.ListWhereOrderByInclude(
				e => e.PhylumID == phylumId &&
				e.SubphylumName.Contains("#") == false,
				_tbl12SubphylumsRepository.OrderBy(r => r.SubphylumName),
				p => p.NULL, k => k.Tbl06Phylums);
		}

	               //------------------------------------------------

		public IList<Tbl90Reference> ListTbl90AuthorsByPhylumId(int phylumId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefAuthorID == e.Tbl90RefAuthors.RefAuthorID && e.PhylumID == phylumId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefAuthors.RefAuthorName + r.Tbl90RefAuthors.ArticelTitle + r.Tbl90RefAuthors.BookName + r.Tbl90RefAuthors.Page1 + r.Tbl90RefAuthors.Publisher),
				p => p.Tbl90RefAuthors);
		}

		public IList<Tbl90Reference> ListTbl90SourcesByPhylumId(int phylumId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefSourceID == e.Tbl90RefSources.RefSourceID && e.PhylumID == phylumId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefSources.RefSourceName + r.Tbl90RefSources.SourceYear),
				p => p.Tbl90RefSources);
		}

		public IList<Tbl90Reference> ListTbl90ExpertsByPhylumId(int phylumId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefExpertID == e.Tbl90RefExperts.RefExpertID && e.PhylumID == phylumId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefExperts.RefExpertName),
				p => p.Tbl90RefExperts);
		}

		public IList<Tbl90Reference> ListTbl90RefAuthorsByPhylumId(int phylumId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefAuthorID == e.Tbl90RefAuthors.RefAuthorID && e.PhylumID == phylumId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefAuthors.RefAuthorName + r.Tbl90RefAuthors.ArticelTitle + r.Tbl90RefAuthors.BookName + r.Tbl90RefAuthors.Page1 + r.Tbl90RefAuthors.Publisher),
				p => p.Tbl90RefAuthors);
		}

		public IList<Tbl90Reference> ListTbl90RefSourcesByPhylumId(int phylumId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefSourceID == e.Tbl90RefSources.RefSourceID && e.PhylumID == phylumId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefSources.RefSourceName + r.Tbl90RefSources.SourceYear),
				p => p.Tbl90RefSources);
		}

		public IList<Tbl90Reference> ListTbl90RefExpertsByPhylumId(int phylumId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefExpertID == e.Tbl90RefExperts.RefExpertID && e.PhylumID == phylumId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefExperts.RefExpertName),
				p => p.Tbl90RefExperts);
		}

	               //------------------------------------------------
 
		public IList<Tbl93Comment> ListTbl93CommentsByPhylumId(int phylumId)
		{
			return _tbl93CommentsRepository.ListWhereOrderByInclude(
				e => e.PhylumID == phylumId,
				_tbl93CommentsRepository.OrderBy(r => r.Info),
				p => p.Tbl12Subphylums);
		}      
  

         
}   

