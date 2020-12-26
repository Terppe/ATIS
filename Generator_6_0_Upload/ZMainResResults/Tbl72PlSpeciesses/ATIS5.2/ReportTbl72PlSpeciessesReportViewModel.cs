using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Te.Atis.BusinessLayer;
using Te.Atis.DomainModel;
using Te.Atis.Ui.Desktop.Views.Report.PDF;    

// <!-- Interface Skriptdatum:  13.12.2019  12:32     -->  

namespace Te.Atis.Ui.Desktop.Views.Report   
{       

           
     public void GetTbl72PlSpeciessesById(int id)
    {
        Tbl72PlSpeciessesList = new ObservableCollection<Tbl72PlSpecies>(_businessLayer.ListTbl72PlSpeciessesByPlSpeciesId(id));		   
        
        //direct children
        Tbl78NamesList = new ObservableCollection<Tbl78Name>(_businessLayer.ListTbl78NamesByPlSpeciesIdAndHash(id));

        Tbl84SynonymsList = new ObservableCollection<Tbl84Synonym>(_businessLayer.ListTbl84SynonymsByPlSpeciesIdAndHash(id));
        //------------------------------------------------------------------------------

         var genusPlSpeciesId = GenusIdTbl72PlSpeciessesSelect(id);	
        Tbl66GenussesList = new ObservableCollection<Tbl66Genus>(_businessLayer.ListTbl66GenussesByGenusIdAndHash(genusPlSpeciesId));		

        var infratribusGenusId = InfratribusIdTbl66GenussesSelect(genusPlSpeciesId);
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
        Tbl90RefExpertsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90RefExpertsByPlSpeciesId(id));

        Tbl90RefSourcesList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90RefSourcesByPlSpeciesId(id));

        Tbl90RefAuthorsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90RefAuthorsByPlSpeciesId(id));

        Tbl90ExpertsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ExpertsByPlSpeciesId(id));

        Tbl90SourcesList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90SourcesByPlSpeciesId(id));

        Tbl90AuthorsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90AuthorsByPlSpeciesId(id));

        Tbl93CommentsList = new ObservableCollection<Tbl93Comment>(_businessLayer.ListTbl93CommentsByPlSpeciesId(id));
    } 
    private RelayCommand _pdfTbl72PlSpeciessesCommand;
    public ICommand PdfTbl72PlSpeciessesCommand
    {
        get { return _pdfTbl72PlSpeciessesCommand ?? (_pdfTbl72PlSpeciessesCommand = new RelayCommand(delegate { CreatePdfTbl72PlSpeciesses(_mainId); })); }
    }

    private static void CreatePdfTbl72PlSpeciesses(int id)
    {
        ReportTbl72PlSpeciessesPdf.CreateMainPdf(id);
    }
    //------------------------------------------------------------------------------
    //------------------------------------------------------------------------------  		   
        
        IList<Tbl72PlSpecies> ListTbl72PlSpeciessesByPlSpeciesId(int plspeciesId);
        IList<Tbl78Name> ListTbl78NamesByPlSpeciesIdAndHash(int plspeciesId);
        IList<Tbl84Synonym> ListTbl84SynonymsByPlSpeciesIdAndHash(int plspeciesId);

        IList<Tbl90Reference> ListTbl90AuthorsByPlSpeciesId(int plspeciesId);
        IList<Tbl90Reference> ListTbl90SourcesByPlSpeciesId(int plspeciesId);
        IList<Tbl90Reference> ListTbl90ExpertsByPlSpeciesId(int plspeciesId);

        IList<Tbl93Comment> ListTbl93CommentsByPlSpeciesId(int plspeciesId);   
        
		public IList<Tbl72PlSpecies> ListTbl72PlSpeciessesByPlSpeciesId(int plspeciesId)
		{
			return _tbl72PlSpeciessesRepository.ListWhereOrderByInclude(
				e => e.PlSpeciesID == plspeciesId,
				_tbl72PlSpeciessesRepository.OrderBy(r => r.PlSpeciesName + r.Subregnum),
				p => p.Tbl78Names, k => k.Tbl81Images);
		}

		public IList<Tbl78Name> ListTbl78NamesByPlSpeciesIdAndHash(int plspeciesId)
		{
			return _tbl78NamesRepository.ListWhereOrderByInclude(
				e => e.PlSpeciesID == plspeciesId &&
				e.NameName.Contains("#") == false,
				_tbl78NamesRepository.OrderBy(r => r.NameName),
				p => p.Tbl81Images, k => k.Tbl72PlSpeciesses);
		}

		public IList<Tbl84Synonym> ListTbl84SynonymsByPlSpeciesIdAndHash(int plspeciesId)
		{
			return _tbl84SynonymsRepository.ListWhereOrderByInclude(
				e => e.PlSpeciesID == plspeciesId &&
				e.SynonymName.Contains("#") == false,
				_tbl84SynonymsRepository.OrderBy(r => r.SynonymName),
				p => p.Tbl87Geographics, k => k.Tbl72PlSpeciesses);
		}

	               //------------------------------------------------

		public IList<Tbl90Reference> ListTbl90AuthorsByPlSpeciesId(int plspeciesId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefAuthorID == e.Tbl90RefAuthors.RefAuthorID && e.PlSpeciesID == plspeciesId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefAuthors.RefAuthorName + r.Tbl90RefAuthors.ArticelTitle + r.Tbl90RefAuthors.BookName + r.Tbl90RefAuthors.Page1 + r.Tbl90RefAuthors.Publisher),
				p => p.Tbl90RefAuthors);
		}

		public IList<Tbl90Reference> ListTbl90SourcesByPlSpeciesId(int plspeciesId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefSourceID == e.Tbl90RefSources.RefSourceID && e.PlSpeciesID == plspeciesId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefSources.RefSourceName + r.Tbl90RefSources.SourceYear),
				p => p.Tbl90RefSources);
		}

		public IList<Tbl90Reference> ListTbl90ExpertsByPlSpeciesId(int plspeciesId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefExpertID == e.Tbl90RefExperts.RefExpertID && e.PlSpeciesID == plspeciesId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefExperts.RefExpertName),
				p => p.Tbl90RefExperts);
		}

		public IList<Tbl90Reference> ListTbl90RefAuthorsByPlSpeciesId(int plspeciesId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefAuthorID == e.Tbl90RefAuthors.RefAuthorID && e.PlSpeciesID == plspeciesId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefAuthors.RefAuthorName + r.Tbl90RefAuthors.ArticelTitle + r.Tbl90RefAuthors.BookName + r.Tbl90RefAuthors.Page1 + r.Tbl90RefAuthors.Publisher),
				p => p.Tbl90RefAuthors);
		}

		public IList<Tbl90Reference> ListTbl90RefSourcesByPlSpeciesId(int plspeciesId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefSourceID == e.Tbl90RefSources.RefSourceID && e.PlSpeciesID == plspeciesId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefSources.RefSourceName + r.Tbl90RefSources.SourceYear),
				p => p.Tbl90RefSources);
		}

		public IList<Tbl90Reference> ListTbl90RefExpertsByPlSpeciesId(int plspeciesId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefExpertID == e.Tbl90RefExperts.RefExpertID && e.PlSpeciesID == plspeciesId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefExperts.RefExpertName),
				p => p.Tbl90RefExperts);
		}

	               //------------------------------------------------

		public IList<Tbl93Comment> ListTbl93CommentsByPlSpeciesId(int plspeciesId)
		{
			return _tbl93CommentsRepository.ListWhereOrderByInclude(
				e => e.PlSpeciesID == plspeciesId,
				_tbl93CommentsRepository.OrderBy(r => r.Info),
				p => p.Tbl78Names, k => k.Tbl81Images);
		}      
  

         
}   

