using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Te.Atis.BusinessLayer;
using Te.Atis.DomainModel;
using Te.Atis.Ui.Desktop.Views.Report.PDF;    

// <!-- Interface Skriptdatum:  15.06.2018  18:32     -->  

namespace Te.Atis.Ui.Desktop.Views.Report   
{       

           
     public void GetTbl24SubclassesById(int id)
    {
        Tbl24SubclassesList = new ObservableCollection<Tbl24Subclass>(_businessLayer.ListTbl24SubclassesBySubclassId(id));		   
        
        //direct children
        Tbl27InfraclassesList = new ObservableCollection<Tbl27Infraclass>(_businessLayer.ListTbl27InfraclassesBySubclassIdAndHash(id));

        //------------------------------------------------------------------------------

         var classeSubclassId = ClassIdTbl24SubclassesSelect(id);	
        Tbl21ClassesList = new ObservableCollection<Tbl21Class>(_businessLayer.ListTbl21ClassesByClassIdAndHash(classeSubclassId));		

        var superclassClassId = SuperclassIdTbl21ClassesSelect(classSubclassId);
        Tbl18SuperclassesList = new ObservableCollection<Tbl18Superclass>(_businessLayer.ListTbl18SuperclassesBySuperclassIdAndHash(superclassClassId));

        var subdivisionSuperclassId = SubdivisionIdTbl18SuperclassesSelect(superclassClassId);
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
        Tbl90RefExpertsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90RefExpertsBySubclassId(id));

        Tbl90RefSourcesList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90RefSourcesBySubclassId(id));

        Tbl90RefAuthorsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90RefAuthorsBySubclassId(id));

        Tbl90ExpertsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ExpertsBySubclassId(id));

        Tbl90SourcesList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90SourcesBySubclassId(id));

        Tbl90AuthorsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90AuthorsBySubclassId(id));

        Tbl93CommentsList = new ObservableCollection<Tbl93Comment>(_businessLayer.ListTbl93CommentsBySubclassId(id));
    } 
    private RelayCommand _pdfTbl24SubclassesCommand;
    public ICommand PdfTbl24SubclassesCommand
    {
        get { return _pdfTbl24SubclassesCommand ?? (_pdfTbl24SubclassesCommand = new RelayCommand(delegate { CreatePdfTbl24Subclasses(_mainId); })); }
    }

    private static void CreatePdfTbl24Subclasses(int id)
    {
        ReportTbl24SubclassesPdf.CreateMainPdf(id);
    }
    //------------------------------------------------------------------------------
    //------------------------------------------------------------------------------  		   
           
        IList<Tbl24Subclass> ListTbl24SubclassesBySubclassId(int subclassId);

        IList<Tbl27Infraclass> ListTbl27InfraclassesBySubclassIdAndHash(int subclassId);

        IList<Tbl90Reference> ListTbl90AuthorsBySubclassId(int subclassId);
        IList<Tbl90Reference> ListTbl90SourcesBySubclassId(int subclassId);
        IList<Tbl90Reference> ListTbl90ExpertsBySubclassId(int subclassId);

        IList<Tbl93Comment> ListTbl93CommentsBySubclassId(int subclassId);	   
           
		public IList<Tbl24Subclass> ListTbl24SubclassesBySubclassId(int subclassId)
		{
			return _tbl24SubclassesRepository.ListWhereOrderByInclude(
				e => e.SubclassID == subclassId,
				_tbl24SubclassesRepository.OrderBy(r => r.SubclassName + r.Subregnum),
				p => p.Tbl27Infraclasses, k => k.NULL);
		}

		public IList<Tbl27Infraclass> ListTbl27InfraclassesBySubclassIdAndHash(int subclassId)
		{
			return _tbl27InfraclassesRepository.ListWhereOrderByInclude(
				e => e.SubclassID == subclassId &&
				e.InfraclassName.Contains("#") == false,
				_tbl27InfraclassesRepository.OrderBy(r => r.InfraclassName),
				p => p.NULL, k => k.Tbl24Subclasses);
		}

	               //------------------------------------------------

		public IList<Tbl90Reference> ListTbl90AuthorsBySubclassId(int subclassId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefAuthorID == e.Tbl90RefAuthors.RefAuthorID && e.SubclassID == subclassId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefAuthors.RefAuthorName + r.Tbl90RefAuthors.ArticelTitle + r.Tbl90RefAuthors.BookName + r.Tbl90RefAuthors.Page1 + r.Tbl90RefAuthors.Publisher),
				p => p.Tbl90RefAuthors);
		}

		public IList<Tbl90Reference> ListTbl90SourcesBySubclassId(int subclassId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefSourceID == e.Tbl90RefSources.RefSourceID && e.SubclassID == subclassId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefSources.RefSourceName + r.Tbl90RefSources.SourceYear),
				p => p.Tbl90RefSources);
		}

		public IList<Tbl90Reference> ListTbl90ExpertsBySubclassId(int subclassId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefExpertID == e.Tbl90RefExperts.RefExpertID && e.SubclassID == subclassId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefExperts.RefExpertName),
				p => p.Tbl90RefExperts);
		}

		public IList<Tbl90Reference> ListTbl90RefAuthorsBySubclassId(int subclassId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefAuthorID == e.Tbl90RefAuthors.RefAuthorID && e.SubclassID == subclassId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefAuthors.RefAuthorName + r.Tbl90RefAuthors.ArticelTitle + r.Tbl90RefAuthors.BookName + r.Tbl90RefAuthors.Page1 + r.Tbl90RefAuthors.Publisher),
				p => p.Tbl90RefAuthors);
		}

		public IList<Tbl90Reference> ListTbl90RefSourcesBySubclassId(int subclassId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefSourceID == e.Tbl90RefSources.RefSourceID && e.SubclassID == subclassId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefSources.RefSourceName + r.Tbl90RefSources.SourceYear),
				p => p.Tbl90RefSources);
		}

		public IList<Tbl90Reference> ListTbl90RefExpertsBySubclassId(int subclassId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefExpertID == e.Tbl90RefExperts.RefExpertID && e.SubclassID == subclassId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefExperts.RefExpertName),
				p => p.Tbl90RefExperts);
		}

	               //------------------------------------------------
 
		public IList<Tbl93Comment> ListTbl93CommentsBySubclassId(int subclassId)
		{
			return _tbl93CommentsRepository.ListWhereOrderByInclude(
				e => e.SubclassID == subclassId,
				_tbl93CommentsRepository.OrderBy(r => r.Info),
				p => p.Tbl27Infraclasses);
		}      
  

         
}   

