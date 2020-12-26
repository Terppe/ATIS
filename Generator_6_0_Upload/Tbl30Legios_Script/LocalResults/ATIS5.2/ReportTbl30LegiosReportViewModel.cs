using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Te.Atis.BusinessLayer;
using Te.Atis.DomainModel;
using Te.Atis.Ui.Desktop.Views.Report.PDF;    

// <!-- Interface Skriptdatum:  08.11.20201817  10:32     -->  

namespace Te.Atis.Ui.Desktop.Views.Report   
{       

           
     public void GetTbl30LegiosById(int id)
    {
        Tbl30LegiosList = new ObservableCollection<Tbl30Legio>(_businessLayer.ListTbl30LegiosByLegioId(id));		   
        
        //direct children
        Tbl33OrdosList = new ObservableCollection<Tbl33Ordo>(_businessLayer.ListTbl33OrdosByLegioIdAndHash(id));

        //------------------------------------------------------------------------------

         var infraclassLegioId = InfraclassIdTbl30LegiosSelect(id);	
        Tbl27InfraclassesList = new ObservableCollection<Tbl27Infraclass>(_businessLayer.ListTbl27InfraclassesByInfraclassIdAndHash(infraclassLegioId));		

         var subclassInfraclassId = SubclassIdTbl27InfraclassesSelect(infraclassLegioId);
         Tbl24SubclassesList = new ObservableCollection<Tbl24Subclass>(_businessLayer.ListTbl24SubclassesBySubclassIdAndHash(subclassInfraclassId));

         var classSubclassId = ClassIdTbl24SubclassesSelect(subclassInfraclassId);
         Tbl21ClassesList = new ObservableCollection<Tbl21Class>(_businessLayer.ListTbl21ClassesByClassIdAndHash(classSubclassId));

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
        Tbl90RefExpertsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90RefExpertsByLegioId(id));

        Tbl90RefSourcesList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90RefSourcesByLegioId(id));

        Tbl90RefAuthorsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90RefAuthorsByLegioId(id));

        Tbl90ExpertsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ExpertsByLegioId(id));

        Tbl90SourcesList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90SourcesByLegioId(id));

        Tbl90AuthorsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90AuthorsByLegioId(id));

        Tbl93CommentsList = new ObservableCollection<Tbl93Comment>(_businessLayer.ListTbl93CommentsByLegioId(id));
    } 
    private RelayCommand _pdfTbl30LegiosCommand;
    public ICommand PdfTbl30LegiosCommand
    {
        get { return _pdfTbl30LegiosCommand ?? (_pdfTbl30LegiosCommand = new RelayCommand(delegate { CreatePdfTbl30Legios(_mainId); })); }
    }

    private static void CreatePdfTbl30Legios(int id)
    {
        ReportTbl30LegiosPdf.CreateMainPdf(id);
    }
    //------------------------------------------------------------------------------
    //------------------------------------------------------------------------------  		   
           
        IList<Tbl30Legio> ListTbl30LegiosByLegioId(int legioId);

        IList<Tbl33Ordo> ListTbl33OrdosByLegioIdAndHash(int legioId);

        IList<Tbl90Reference> ListTbl90AuthorsByLegioId(int legioId);
        IList<Tbl90Reference> ListTbl90SourcesByLegioId(int legioId);
        IList<Tbl90Reference> ListTbl90ExpertsByLegioId(int legioId);

        IList<Tbl93Comment> ListTbl93CommentsByLegioId(int legioId);	   
           
		public IList<Tbl30Legio> ListTbl30LegiosByLegioId(int legioId)
		{
			return _tbl30LegiosRepository.ListWhereOrderByInclude(
				e => e.LegioID == legioId,
				_tbl30LegiosRepository.OrderBy(r => r.LegioName + r.Subregnum),
				p => p.Tbl33Ordos, k => k.Tbl36Subordos);
		}

		public IList<Tbl33Ordo> ListTbl33OrdosByLegioIdAndHash(int legioId)
		{
			return _tbl33OrdosRepository.ListWhereOrderByInclude(
				e => e.LegioID == legioId &&
				e.OrdoName.Contains("#") == false,
				_tbl33OrdosRepository.OrderBy(r => r.OrdoName),
				p => p.NULL, k => k.Tbl30Legios);
		}

	               //------------------------------------------------

		public IList<Tbl90Reference> ListTbl90AuthorsByLegioId(int legioId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefAuthorID == e.Tbl90RefAuthors.RefAuthorID && e.LegioID == legioId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefAuthors.RefAuthorName + r.Tbl90RefAuthors.ArticelTitle + r.Tbl90RefAuthors.BookName + r.Tbl90RefAuthors.Page1 + r.Tbl90RefAuthors.Publisher),
				p => p.Tbl90RefAuthors);
		}

		public IList<Tbl90Reference> ListTbl90SourcesByLegioId(int legioId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefSourceID == e.Tbl90RefSources.RefSourceID && e.LegioID == legioId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefSources.RefSourceName + r.Tbl90RefSources.SourceYear),
				p => p.Tbl90RefSources);
		}

		public IList<Tbl90Reference> ListTbl90ExpertsByLegioId(int legioId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefExpertID == e.Tbl90RefExperts.RefExpertID && e.LegioID == legioId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefExperts.RefExpertName),
				p => p.Tbl90RefExperts);
		}

		public IList<Tbl90Reference> ListTbl90RefAuthorsByLegioId(int legioId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefAuthorID == e.Tbl90RefAuthors.RefAuthorID && e.LegioID == legioId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefAuthors.RefAuthorName + r.Tbl90RefAuthors.ArticelTitle + r.Tbl90RefAuthors.BookName + r.Tbl90RefAuthors.Page1 + r.Tbl90RefAuthors.Publisher),
				p => p.Tbl90RefAuthors);
		}

		public IList<Tbl90Reference> ListTbl90RefSourcesByLegioId(int legioId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefSourceID == e.Tbl90RefSources.RefSourceID && e.LegioID == legioId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefSources.RefSourceName + r.Tbl90RefSources.SourceYear),
				p => p.Tbl90RefSources);
		}

		public IList<Tbl90Reference> ListTbl90RefExpertsByLegioId(int legioId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefExpertID == e.Tbl90RefExperts.RefExpertID && e.LegioID == legioId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefExperts.RefExpertName),
				p => p.Tbl90RefExperts);
		}

	               //------------------------------------------------
 
		public IList<Tbl93Comment> ListTbl93CommentsByLegioId(int legioId)
		{
			return _tbl93CommentsRepository.ListWhereOrderByInclude(
				e => e.LegioID == legioId,
				_tbl93CommentsRepository.OrderBy(r => r.Info),
				p => p.Tbl33Ordos);
		}      
  

         
}   

