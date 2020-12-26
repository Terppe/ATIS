using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Te.Atis.BusinessLayer;
using Te.Atis.DomainModel;
using Te.Atis.Ui.Desktop.Views.Report.PDF;    

// <!-- Interface Skriptdatum:  12.12.2019  10:32     -->  

namespace Te.Atis.Ui.Desktop.Views.Report   
{       

           
     public void GetTbl66GenussesById(int id)
    {
        Tbl66GenussesList = new ObservableCollection<Tbl66Genus>(_businessLayer.ListTbl66GenussesByGenusId(id));		   
        
        //direct children
        Tbl69FiSpeciessesList = new ObservableCollection<Tbl69FiSpecies>(_businessLayer.ListTbl69FiSpeciessesByGenusIdAndHash(id));

        Tbl72PlSpeciessesList = new ObservableCollection<Tbl72PlSpecies>(_businessLayer.ListTbl72PlSpeciessesByGenusIdAndHash(id));
        //------------------------------------------------------------------------------

         var infratribusGenusId = InfratribusIdTbl66GenussesSelect(id);	
        Tbl63InfratribussesList = new ObservableCollection<Tbl63Infratribus>(_businessLayer.ListTbl63InfratribussesByInfratribusIdAndHash(infratribusGenusId));		

        var subtribusInfratribusId = SubtribusIdTbl63InfratribussesSelect(infratribusGenusId);
        Tbl60SubtribussesList = new ObservableCollection<Tbl60Subtribus>(_businessLayer.ListTbl60SubtribussesBySubtribusIdAndHash(subtribusInfratribusId));

        var tribusSubtribusId = TribusIdTbl60SubtribussesSelect(subtribusInfratribusId);
        Tbl57TribussesList = new ObservableCollection<Tbl57Tribus>(_businessLayer.ListTbl57TribussesByTribusIdAndHash(tribusSubtribusId));

        var supertribusTribusId = SupertribusIdTbl57TribussesSelect(tribusSubtribusId);
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
        Tbl90RefExpertsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90RefExpertsByGenusId(id));

        Tbl90RefSourcesList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90RefSourcesByGenusId(id));

        Tbl90RefAuthorsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90RefAuthorsByGenusId(id));

        Tbl90ExpertsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ExpertsByGenusId(id));

        Tbl90SourcesList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90SourcesByGenusId(id));

        Tbl90AuthorsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90AuthorsByGenusId(id));

        Tbl93CommentsList = new ObservableCollection<Tbl93Comment>(_businessLayer.ListTbl93CommentsByGenusId(id));
    } 
    private RelayCommand _pdfTbl66GenussesCommand;
    public ICommand PdfTbl66GenussesCommand
    {
        get { return _pdfTbl66GenussesCommand ?? (_pdfTbl66GenussesCommand = new RelayCommand(delegate { CreatePdfTbl66Genusses(_mainId); })); }
    }

    private static void CreatePdfTbl66Genusses(int id)
    {
        ReportTbl66GenussesPdf.CreateMainPdf(id);
    }
    //------------------------------------------------------------------------------
    //------------------------------------------------------------------------------  		   
        
        IList<Tbl66Genus> ListTbl66GenussesByGenusId(int genusId);
        IList<Tbl69FiSpecies> ListTbl69FiSpeciessesByGenusIdAndHash(int genusId);
        IList<Tbl72PlSpecies> ListTbl72PlSpeciessesByGenusIdAndHash(int genusId);

        IList<Tbl90Reference> ListTbl90AuthorsByGenusId(int genusId);
        IList<Tbl90Reference> ListTbl90SourcesByGenusId(int genusId);
        IList<Tbl90Reference> ListTbl90ExpertsByGenusId(int genusId);

        IList<Tbl93Comment> ListTbl93CommentsByGenusId(int genusId);   
        
		public IList<Tbl66Genus> ListTbl66GenussesByGenusId(int genusId)
		{
			return _tbl66GenussesRepository.ListWhereOrderByInclude(
				e => e.GenusID == genusId,
				_tbl66GenussesRepository.OrderBy(r => r.GenusName + r.Subregnum),
				p => p.Tbl69FiSpeciesses, k => k.Tbl72PlSpeciesses);
		}

		public IList<Tbl69FiSpecies> ListTbl69FiSpeciessesByGenusIdAndHash(int genusId)
		{
			return _tbl69FiSpeciessesRepository.ListWhereOrderByInclude(
				e => e.GenusID == genusId &&
				e.FiSpeciesName.Contains("#") == false,
				_tbl69FiSpeciessesRepository.OrderBy(r => r.FiSpeciesName),
				p => p.NULL, k => k.Tbl66Genusses);
		}

		public IList<Tbl72PlSpecies> ListTbl72PlSpeciessesByGenusIdAndHash(int genusId)
		{
			return _tbl72PlSpeciessesRepository.ListWhereOrderByInclude(
				e => e.GenusID == genusId &&
				e.PlSpeciesName.Contains("#") == false,
				_tbl72PlSpeciessesRepository.OrderBy(r => r.PlSpeciesName),
				p => p.NULL, k => k.Tbl66Genusses);
		}

	               //------------------------------------------------

		public IList<Tbl90Reference> ListTbl90AuthorsByGenusId(int genusId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefAuthorID == e.Tbl90RefAuthors.RefAuthorID && e.GenusID == genusId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefAuthors.RefAuthorName + r.Tbl90RefAuthors.ArticelTitle + r.Tbl90RefAuthors.BookName + r.Tbl90RefAuthors.Page1 + r.Tbl90RefAuthors.Publisher),
				p => p.Tbl90RefAuthors);
		}

		public IList<Tbl90Reference> ListTbl90SourcesByGenusId(int genusId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefSourceID == e.Tbl90RefSources.RefSourceID && e.GenusID == genusId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefSources.RefSourceName + r.Tbl90RefSources.SourceYear),
				p => p.Tbl90RefSources);
		}

		public IList<Tbl90Reference> ListTbl90ExpertsByGenusId(int genusId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefExpertID == e.Tbl90RefExperts.RefExpertID && e.GenusID == genusId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefExperts.RefExpertName),
				p => p.Tbl90RefExperts);
		}

		public IList<Tbl90Reference> ListTbl90RefAuthorsByGenusId(int genusId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefAuthorID == e.Tbl90RefAuthors.RefAuthorID && e.GenusID == genusId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefAuthors.RefAuthorName + r.Tbl90RefAuthors.ArticelTitle + r.Tbl90RefAuthors.BookName + r.Tbl90RefAuthors.Page1 + r.Tbl90RefAuthors.Publisher),
				p => p.Tbl90RefAuthors);
		}

		public IList<Tbl90Reference> ListTbl90RefSourcesByGenusId(int genusId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefSourceID == e.Tbl90RefSources.RefSourceID && e.GenusID == genusId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefSources.RefSourceName + r.Tbl90RefSources.SourceYear),
				p => p.Tbl90RefSources);
		}

		public IList<Tbl90Reference> ListTbl90RefExpertsByGenusId(int genusId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefExpertID == e.Tbl90RefExperts.RefExpertID && e.GenusID == genusId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefExperts.RefExpertName),
				p => p.Tbl90RefExperts);
		}

	               //------------------------------------------------

		public IList<Tbl93Comment> ListTbl93CommentsByGenusId(int genusId)
		{
			return _tbl93CommentsRepository.ListWhereOrderByInclude(
				e => e.GenusID == genusId,
				_tbl93CommentsRepository.OrderBy(r => r.Info),
				p => p.Tbl69FiSpeciesses, k => k.Tbl72PlSpeciesses);
		}      
  

         
}   

