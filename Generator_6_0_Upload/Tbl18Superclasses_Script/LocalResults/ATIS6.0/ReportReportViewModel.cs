using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Te.Atis.BusinessLayer;
using Te.Atis.DomainModel;
using Te.Atis.Ui.Desktop.Views.Report.PDF;    

// <!-- Interface Skriptdatum:  04.11.2020  12:32     -->  

namespace Te.Atis.Ui.Desktop.Views.Report   
{       

           
     public void GetTbl18SuperclassesById(int id)
    {
        Tbl18SuperclassesList = new ObservableCollection<Tbl18Superclass>(_businessLayer.ListTbl18SuperclassesBySuperclassId(id));		   
        
        //direct children
        Tbl21ClassesList = new ObservableCollection<Tbl21Class>(_businessLayer.ListTbl21ClassesBySuperclassIdAndHash(id));

        //------------------------------------------------------------------------------

         var subdivisionSuperclassId = SubdivisionIdTbl18SuperclassesSelect(id);	
        if (subdivisionSuperclassId == 1)  //special situation because SubphylumID = 48 and SubdivisionID = 1
        {
             var subphylumSuperclassId = SubphylumIdTbl18SuperclassesSelect(id);	
             Tbl12SubphylumsList = new ObservableCollection<Tbl12Subphylum>(_businessLayer.ListTbl12SubphylumsBySubphylumIdAndHash(subphylumSuperclassId));		
         
            var phylumSubphylumId = PhylumIdTbl12SubphylumsSelect(subphylumSuperclassId);
            Tbl06PhylumsList = new ObservableCollection<Tbl06Phylum>(_businessLayer.ListTbl06PhylumsByPhylumIdAndHash(phylumSubphylumId));

             var regnumPhylumId = RegnumIdTbl06PhylumsSelect(phylumSubphylumId);
            Tbl03RegnumsList = new ObservableCollection<Tbl03Regnum>(_businessLayer.ListTbl03RegnumsByRegnumIdAndHash(regnumPhylumId));
      }
      else
      {
         Tbl15SubdivisionsList = new ObservableCollection<Tbl15Subdivision>(_businessLayer.ListTbl15SubdivisionsBySubdivisionIdAndHash(subdivisionSuperclassId));

         var divisionSubdivisionId = DivisionIdTbl15SubdivisionsSelect(subdivisionSuperclassId);
         Tbl09DivisionsList = new ObservableCollection<Tbl09Division>(_businessLayer.ListTbl09DivisionsByDivisionIdAndHash(divisionSubdivisionId));

          var regnumDivisionId = RegnumIdTbl09DivisionsSelect(divisionSubdivisionId);
         Tbl03RegnumsList = new ObservableCollection<Tbl03Regnum>(_businessLayer.ListTbl03RegnumsByRegnumIdAndHash(regnumDivisionId)); 
      }       
           
        //------------------------------------------------------------------------------
        Tbl90RefExpertsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90RefExpertsBySuperclassId(id));

        Tbl90RefSourcesList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90RefSourcesBySuperclassId(id));

        Tbl90RefAuthorsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90RefAuthorsBySuperclassId(id));

        Tbl90ExpertsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ExpertsBySuperclassId(id));

        Tbl90SourcesList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90SourcesBySuperclassId(id));

        Tbl90AuthorsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90AuthorsBySuperclassId(id));

        Tbl93CommentsList = new ObservableCollection<Tbl93Comment>(_businessLayer.ListTbl93CommentsBySuperclassId(id));
    } 
    private RelayCommand _pdfTbl18SuperclassesCommand;
    public ICommand PdfTbl18SuperclassesCommand
    {
        get { return _pdfTbl18SuperclassesCommand ?? (_pdfTbl18SuperclassesCommand = new RelayCommand(delegate { CreatePdfTbl18Superclasses(_mainId); })); }
    }

    private static void CreatePdfTbl18Superclasses(int id)
    {
        ReportTbl18SuperclassesPdf.CreateMainPdf(id);
    }
    //------------------------------------------------------------------------------
    //------------------------------------------------------------------------------  		   
        
        IList<Tbl18Superclass> ListTbl18SuperclassesBySuperclassId(int superclassId);
        IList<Tbl21Class> ListTbl21ClassesBySuperclassIdAndHash(int superclassId);
        IList<Tbl24Subclass> ListTbl24SubclassesBySuperclassIdAndHash(int superclassId);

        IList<Tbl90Reference> ListTbl90AuthorsBySuperclassId(int superclassId);
        IList<Tbl90Reference> ListTbl90SourcesBySuperclassId(int superclassId);
        IList<Tbl90Reference> ListTbl90ExpertsBySuperclassId(int superclassId);

        IList<Tbl93Comment> ListTbl93CommentsBySuperclassId(int superclassId);   
        
		public IList<Tbl18Superclass> ListTbl18SuperclassesBySuperclassId(int superclassId)
		{
			return _tbl18SuperclassesRepository.ListWhereOrderByInclude(
				e => e.SuperclassID == superclassId,
				_tbl18SuperclassesRepository.OrderBy(r => r.SuperclassName + r.Subregnum),
				p => p.Tbl21Classes, k => k.Tbl24Subclasses);
		}

		public IList<Tbl21Class> ListTbl21ClassesBySuperclassIdAndHash(int superclassId)
		{
			return _tbl21ClassesRepository.ListWhereOrderByInclude(
				e => e.SuperclassID == superclassId &&
				e.ClassName.Contains("#") == false,
				_tbl21ClassesRepository.OrderBy(r => r.ClassName),
				p => p.NULL, k => k.Tbl18Superclasses);
		}

		public IList<Tbl24Subclass> ListTbl24SubclassesBySuperclassIdAndHash(int superclassId)
		{
			return _tbl24SubclassesRepository.ListWhereOrderByInclude(
				e => e.SuperclassID == superclassId &&
				e.SubclassName.Contains("#") == false,
				_tbl24SubclassesRepository.OrderBy(r => r.SubclassName),
				p => p.NULL, k => k.Tbl18Superclasses);
		}

	               //------------------------------------------------

		public IList<Tbl90Reference> ListTbl90AuthorsBySuperclassId(int superclassId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefAuthorID == e.Tbl90RefAuthors.RefAuthorID && e.SuperclassID == superclassId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefAuthors.RefAuthorName + r.Tbl90RefAuthors.ArticelTitle + r.Tbl90RefAuthors.BookName + r.Tbl90RefAuthors.Page1 + r.Tbl90RefAuthors.Publisher),
				p => p.Tbl90RefAuthors);
		}

		public IList<Tbl90Reference> ListTbl90SourcesBySuperclassId(int superclassId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefSourceID == e.Tbl90RefSources.RefSourceID && e.SuperclassID == superclassId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefSources.RefSourceName + r.Tbl90RefSources.SourceYear),
				p => p.Tbl90RefSources);
		}

		public IList<Tbl90Reference> ListTbl90ExpertsBySuperclassId(int superclassId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefExpertID == e.Tbl90RefExperts.RefExpertID && e.SuperclassID == superclassId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefExperts.RefExpertName),
				p => p.Tbl90RefExperts);
		}

		public IList<Tbl90Reference> ListTbl90RefAuthorsBySuperclassId(int superclassId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefAuthorID == e.Tbl90RefAuthors.RefAuthorID && e.SuperclassID == superclassId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefAuthors.RefAuthorName + r.Tbl90RefAuthors.ArticelTitle + r.Tbl90RefAuthors.BookName + r.Tbl90RefAuthors.Page1 + r.Tbl90RefAuthors.Publisher),
				p => p.Tbl90RefAuthors);
		}

		public IList<Tbl90Reference> ListTbl90RefSourcesBySuperclassId(int superclassId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefSourceID == e.Tbl90RefSources.RefSourceID && e.SuperclassID == superclassId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefSources.RefSourceName + r.Tbl90RefSources.SourceYear),
				p => p.Tbl90RefSources);
		}

		public IList<Tbl90Reference> ListTbl90RefExpertsBySuperclassId(int superclassId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefExpertID == e.Tbl90RefExperts.RefExpertID && e.SuperclassID == superclassId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefExperts.RefExpertName),
				p => p.Tbl90RefExperts);
		}

	               //------------------------------------------------

		public IList<Tbl93Comment> ListTbl93CommentsBySuperclassId(int superclassId)
		{
			return _tbl93CommentsRepository.ListWhereOrderByInclude(
				e => e.SuperclassID == superclassId,
				_tbl93CommentsRepository.OrderBy(r => r.Info),
				p => p.Tbl21Classes, k => k.Tbl24Subclasses);
		}      
  

         
}   

