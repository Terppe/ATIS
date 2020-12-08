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

           
     public void GetTbl12SubphylumsById(int id)
    {
        Tbl12SubphylumsList = new ObservableCollection<Tbl12Subphylum>(_businessLayer.ListTbl12SubphylumsBySubphylumId(id));		   
        
        //direct children
        Tbl18SuperclassesList = new ObservableCollection<Tbl18Superclass>(_businessLayer.ListTbl18SuperclassesBySubphylumIdAndHash(id));

        //------------------------------------------------------------------------------

         var phylumSubphylumId = PhylumIdTbl12SubphylumsSelect(id);	

        Tbl06PhylumsList = new ObservableCollection<Tbl06Phylum>(_businessLayer.ListTbl06PhylumsByPhylumIdAndHash(phylumSubphylumId));		

        var regnumPhylumId = RegnumIdTbl06PhylumsSelect(phylumSubphylumId);

        Tbl03RegnumsList = new ObservableCollection<Tbl03Regnum>(_businessLayer.ListTbl03RegnumsByRegnumIdAndHash(regnumPhylumId));   
           
        //------------------------------------------------------------------------------
        Tbl90RefExpertsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90RefExpertsBySubphylumId(id));

        Tbl90RefSourcesList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90RefSourcesBySubphylumId(id));

        Tbl90RefAuthorsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90RefAuthorsBySubphylumId(id));

        Tbl90ExpertsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ExpertsBySubphylumId(id));

        Tbl90SourcesList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90SourcesBySubphylumId(id));

        Tbl90AuthorsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90AuthorsBySubphylumId(id));

        Tbl93CommentsList = new ObservableCollection<Tbl93Comment>(_businessLayer.ListTbl93CommentsBySubphylumId(id));
    } 
    private RelayCommand _pdfTbl12SubphylumsCommand;
    public ICommand PdfTbl12SubphylumsCommand
    {
        get { return _pdfTbl12SubphylumsCommand ?? (_pdfTbl12SubphylumsCommand = new RelayCommand(delegate { CreatePdfTbl12Subphylums(_mainId); })); }
    }

    private static void CreatePdfTbl12Subphylums(int id)
    {
        ReportTbl12SubphylumsPdf.CreateMainPdf(id);
    }
    //------------------------------------------------------------------------------
    //------------------------------------------------------------------------------  		   
           
        IList<Tbl12Subphylum> ListTbl12SubphylumsBySubphylumId(int subphylumId);

        IList<Tbl18Superclass> ListTbl18SuperclassesBySubphylumIdAndHash(int subphylumId);

        IList<Tbl90Reference> ListTbl90AuthorsBySubphylumId(int subphylumId);
        IList<Tbl90Reference> ListTbl90SourcesBySubphylumId(int subphylumId);
        IList<Tbl90Reference> ListTbl90ExpertsBySubphylumId(int subphylumId);

        IList<Tbl90Reference> ListTbl90RefAuthorsBySubphylumId(int subphylumId);
        IList<Tbl90Reference> ListTbl90RefSourcesBySubphylumId(int subphylumId);
        IList<Tbl90Reference> ListTbl90RefExpertsBySubphylumId(int subphylumId);

        IList<Tbl93Comment> ListTbl93CommentsBySubphylumId(int subphylumId);	   
           
		public IList<Tbl12Subphylum> ListTbl12SubphylumsBySubphylumId(int subphylumId)
		{
			return _tbl12SubphylumsRepository.ListWhereOrderByInclude(
				e => e.SubphylumID == subphylumId,
				_tbl12SubphylumsRepository.OrderBy(r => r.SubphylumName + r.Subregnum),
				p => p.Tbl18Superclasses, k => k.NULL);
		}

		public IList<Tbl18Superclass> ListTbl18SuperclassesBySubphylumIdAndHash(int subphylumId)
		{
			return _tbl18SuperclassesRepository.ListWhereOrderByInclude(
				e => e.SubphylumID == subphylumId &&
				e.SuperclassName.Contains("#") == false,
				_tbl18SuperclassesRepository.OrderBy(r => r.SuperclassName),
				p => p.NULL, k => k.Tbl12Subphylums);
		}

	               //------------------------------------------------

		public IList<Tbl90Reference> ListTbl90AuthorsBySubphylumId(int subphylumId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefAuthorID == e.Tbl90RefAuthors.RefAuthorID && e.SubphylumID == subphylumId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefAuthors.RefAuthorName + r.Tbl90RefAuthors.ArticelTitle + r.Tbl90RefAuthors.BookName + r.Tbl90RefAuthors.Page1 + r.Tbl90RefAuthors.Publisher),
				p => p.Tbl90RefAuthors);
		}

		public IList<Tbl90Reference> ListTbl90SourcesBySubphylumId(int subphylumId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefSourceID == e.Tbl90RefSources.RefSourceID && e.SubphylumID == subphylumId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefSources.RefSourceName + r.Tbl90RefSources.SourceYear),
				p => p.Tbl90RefSources);
		}

		public IList<Tbl90Reference> ListTbl90ExpertsBySubphylumId(int subphylumId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefExpertID == e.Tbl90RefExperts.RefExpertID && e.SubphylumID == subphylumId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefExperts.RefExpertName),
				p => p.Tbl90RefExperts);
		}

		public IList<Tbl90Reference> ListTbl90RefAuthorsBySubphylumId(int subphylumId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefAuthorID == e.Tbl90RefAuthors.RefAuthorID && e.SubphylumID == subphylumId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefAuthors.RefAuthorName + r.Tbl90RefAuthors.ArticelTitle + r.Tbl90RefAuthors.BookName + r.Tbl90RefAuthors.Page1 + r.Tbl90RefAuthors.Publisher),
				p => p.Tbl90RefAuthors);
		}

		public IList<Tbl90Reference> ListTbl90RefSourcesBySubphylumId(int subphylumId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefSourceID == e.Tbl90RefSources.RefSourceID && e.SubphylumID == subphylumId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefSources.RefSourceName + r.Tbl90RefSources.SourceYear),
				p => p.Tbl90RefSources);
		}

		public IList<Tbl90Reference> ListTbl90RefExpertsBySubphylumId(int subphylumId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefExpertID == e.Tbl90RefExperts.RefExpertID && e.SubphylumID == subphylumId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefExperts.RefExpertName),
				p => p.Tbl90RefExperts);
		}

	               //------------------------------------------------
 
		public IList<Tbl93Comment> ListTbl93CommentsBySubphylumId(int subphylumId)
		{
			return _tbl93CommentsRepository.ListWhereOrderByInclude(
				e => e.SubphylumID == subphylumId,
				_tbl93CommentsRepository.OrderBy(r => r.Info),
				p => p.Tbl18Superclasses);
		}      
  

         
}   

