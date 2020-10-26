using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Te.Atis.BusinessLayer;
using Te.Atis.DomainModel;
using Te.Atis.Ui.Desktop.Views.Report.PDF;    

// <!-- Interface Skriptdatum:  08.11.2018  10:32     -->  

namespace Te.Atis.Ui.Desktop.Views.Report   
{       

           
     public void GetTbl57TribussesById(int id)
    {
        Tbl57TribussesList = new ObservableCollection<Tbl57Tribus>(_businessLayer.ListTbl57TribussesByTribusId(id));		   
        
        //direct children
        Tbl60SubtribussesList = new ObservableCollection<Tbl60Subtribus>(_businessLayer.ListTbl60SubtribussesByTribusIdAndHash(id));

        //------------------------------------------------------------------------------

         var supertribusTribusId = SupertribusIdTbl57TribussesSelect(id);	
        Tbl54SupertribussesList = new ObservableCollection<Tbl54Supertribus>(_businessLayer.ListTbl54SupertribussesBySupertribusIdAndHash(supertribusTribusId));		

        var infrafamilySupertribusId = InfrafamilyIdTbl54SupertribussesSelect(supertribusTribusId);
        Tbl51InfrafamiliesList = new ObservableCollection<Tbl51Infrafamily>(_businessLayer.ListTbl51InfrafamiliesByInfrafamilyIdAndHash(infrafamilySupertribusId));

        var subfamilyInfrafamilyId = SubfamilyIdTbl51InfrafamiliesSelect(infrafamilySupertribusId);
        Tbl48SubfamiliesList = new ObservableCollection<Tbl48Subfamily>(_businessLayer.ListTbl48SubfamiliesBySubfamilyIdAndHash(subfamilyInfrafamilyId));

        var familySubfamilyId = FamilyIdTbl48SubfamiliesSelect(subfamilyInfrafamilyId);
        Tbl45FamiliesList = new ObservableCollection<Tbl45Family>(_businessLayer.ListTbl45FamiliesByFamilyIdAndHash(familySubfamilyId));

        var superfamilyFamilyId = SuperfamilyIdTbl45FamiliesSelect(familySubfamilyId);
        Tbl42SuperfamiliesList = new ObservableCollection<Tbl42Superfamily>(_businessLayer.ListTbl42SuperfamiliesBySuperfamilyIdAndHash(superfamilyFamilyId));

        var infraordoSuperfamilyId = InfraordoIdTbl42SuperfamiliesSelect(superfamilyFamilyId);
        Tbl39InfraordosList = new ObservableCollection<Tbl39Infraordo>(_businessLayer.ListTbl39InfraordosByInfraordoIdAndHash(infraordoSuperfamilyId));

        var subordoInfraordoId = SubordoIdTbl39InfraordosSelect(infraordoSuperfamilyId);
        Tbl36SubordosList = new ObservableCollection<Tbl36Subordo>(_businessLayer.ListTbl36SubordosBySubordoIdAndHash(subordoInfraordoId));

        var ordoSubordoId = OrdoIdTbl36SubordosSelect(subordoInfraordoId);
        Tbl33OrdosList = new ObservableCollection<Tbl33Ordo>(_businessLayer.ListTbl33OrdosByOrdoIdAndHash(ordoSubordoId));

        var legioOrdoId = LegioIdTbl33OrdosSelect(ordoSubordoId);
        Tbl30LegiosList = new ObservableCollection<Tbl30Legio>(_businessLayer.ListTbl30LegiosByLegioIdAndHash(legioOrdoId));

         var infraclassLegioId = InfraclassIdTbl30LegiosSelect(legioOrdoId);
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
        Tbl90RefExpertsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90RefExpertsByTribusId(id));

        Tbl90RefSourcesList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90RefSourcesByTribusId(id));

        Tbl90RefAuthorsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90RefAuthorsByTribusId(id));

        Tbl90ExpertsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ExpertsByTribusId(id));

        Tbl90SourcesList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90SourcesByTribusId(id));

        Tbl90AuthorsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90AuthorsByTribusId(id));

        Tbl93CommentsList = new ObservableCollection<Tbl93Comment>(_businessLayer.ListTbl93CommentsByTribusId(id));
    } 
    private RelayCommand _pdfTbl57TribussesCommand;
    public ICommand PdfTbl57TribussesCommand
    {
        get { return _pdfTbl57TribussesCommand ?? (_pdfTbl57TribussesCommand = new RelayCommand(delegate { CreatePdfTbl57Tribusses(_mainId); })); }
    }

    private static void CreatePdfTbl57Tribusses(int id)
    {
        ReportTbl57TribussesPdf.CreateMainPdf(id);
    }
    //------------------------------------------------------------------------------
    //------------------------------------------------------------------------------  		   
           
        IList<Tbl57Tribus> ListTbl57TribussesByTribusId(int tribusId);

        IList<Tbl60Subtribus> ListTbl60SubtribussesByTribusIdAndHash(int tribusId);

        IList<Tbl90Reference> ListTbl90AuthorsByTribusId(int tribusId);
        IList<Tbl90Reference> ListTbl90SourcesByTribusId(int tribusId);
        IList<Tbl90Reference> ListTbl90ExpertsByTribusId(int tribusId);

        IList<Tbl93Comment> ListTbl93CommentsByTribusId(int tribusId);	   
           
		public IList<Tbl57Tribus> ListTbl57TribussesByTribusId(int tribusId)
		{
			return _tbl57TribussesRepository.ListWhereOrderByInclude(
				e => e.TribusID == tribusId,
				_tbl57TribussesRepository.OrderBy(r => r.TribusName + r.Subregnum),
				p => p.Tbl60Subtribusses, k => k.Tbl63Infratribusses);
		}

		public IList<Tbl60Subtribus> ListTbl60SubtribussesByTribusIdAndHash(int tribusId)
		{
			return _tbl60SubtribussesRepository.ListWhereOrderByInclude(
				e => e.TribusID == tribusId &&
				e.SubtribusName.Contains("#") == false,
				_tbl60SubtribussesRepository.OrderBy(r => r.SubtribusName),
				p => p.NULL, k => k.Tbl57Tribusses);
		}

	               //------------------------------------------------

		public IList<Tbl90Reference> ListTbl90AuthorsByTribusId(int tribusId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefAuthorID == e.Tbl90RefAuthors.RefAuthorID && e.TribusID == tribusId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefAuthors.RefAuthorName + r.Tbl90RefAuthors.ArticelTitle + r.Tbl90RefAuthors.BookName + r.Tbl90RefAuthors.Page1 + r.Tbl90RefAuthors.Publisher),
				p => p.Tbl90RefAuthors);
		}

		public IList<Tbl90Reference> ListTbl90SourcesByTribusId(int tribusId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefSourceID == e.Tbl90RefSources.RefSourceID && e.TribusID == tribusId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefSources.RefSourceName + r.Tbl90RefSources.SourceYear),
				p => p.Tbl90RefSources);
		}

		public IList<Tbl90Reference> ListTbl90ExpertsByTribusId(int tribusId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefExpertID == e.Tbl90RefExperts.RefExpertID && e.TribusID == tribusId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefExperts.RefExpertName),
				p => p.Tbl90RefExperts);
		}

		public IList<Tbl90Reference> ListTbl90RefAuthorsByTribusId(int tribusId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefAuthorID == e.Tbl90RefAuthors.RefAuthorID && e.TribusID == tribusId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefAuthors.RefAuthorName + r.Tbl90RefAuthors.ArticelTitle + r.Tbl90RefAuthors.BookName + r.Tbl90RefAuthors.Page1 + r.Tbl90RefAuthors.Publisher),
				p => p.Tbl90RefAuthors);
		}

		public IList<Tbl90Reference> ListTbl90RefSourcesByTribusId(int tribusId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefSourceID == e.Tbl90RefSources.RefSourceID && e.TribusID == tribusId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefSources.RefSourceName + r.Tbl90RefSources.SourceYear),
				p => p.Tbl90RefSources);
		}

		public IList<Tbl90Reference> ListTbl90RefExpertsByTribusId(int tribusId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefExpertID == e.Tbl90RefExperts.RefExpertID && e.TribusID == tribusId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefExperts.RefExpertName),
				p => p.Tbl90RefExperts);
		}

	               //------------------------------------------------
 
		public IList<Tbl93Comment> ListTbl93CommentsByTribusId(int tribusId)
		{
			return _tbl93CommentsRepository.ListWhereOrderByInclude(
				e => e.TribusID == tribusId,
				_tbl93CommentsRepository.OrderBy(r => r.Info),
				p => p.Tbl60Subtribusses);
		}      
  

         
}   

