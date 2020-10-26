using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Te.Atis.BusinessLayer;
using Te.Atis.DomainModel;
using Te.Atis.Ui.Desktop.Views.Report.PDF;    

// <!-- Interface Skriptdatum:  23.06.2018  10:32     -->  

namespace Te.Atis.Ui.Desktop.Views.Report   
{       

           
     public void GetTbl69FiSpeciessesById(int id)
    {
        Tbl69FiSpeciessesList = new ObservableCollection<Tbl69FiSpecies>(_businessLayer.ListTbl69FiSpeciessesByFiSpeciesId(id));		   
        
        //direct children
        Tbl78NamesList = new ObservableCollection<Tbl78Name>(_businessLayer.ListTbl78NamesByFiSpeciesIdAndHash(id));

        Tbl84SynonymsList = new ObservableCollection<Tbl84Synonym>(_businessLayer.ListTbl84SynonymsByFiSpeciesIdAndHash(id));
        //------------------------------------------------------------------------------

         var genusFiSpeciesId = GenusIdTbl69FiSpeciessesSelect(id);	
        Tbl66GenussesList = new ObservableCollection<Tbl66Genus>(_businessLayer.ListTbl66GenussesByGenusIdAndHash(genusFiSpeciesId));		

        var infratribusGenusId = InfratribusIdTbl66GenussesSelect(genusFiSpeciesId);
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
        Tbl90RefExpertsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90RefExpertsByFiSpeciesId(id));

        Tbl90RefSourcesList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90RefSourcesByFiSpeciesId(id));

        Tbl90RefAuthorsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90RefAuthorsByFiSpeciesId(id));

        Tbl90ExpertsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ExpertsByFiSpeciesId(id));

        Tbl90SourcesList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90SourcesByFiSpeciesId(id));

        Tbl90AuthorsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90AuthorsByFiSpeciesId(id));

        Tbl93CommentsList = new ObservableCollection<Tbl93Comment>(_businessLayer.ListTbl93CommentsByFiSpeciesId(id));
    } 
    private RelayCommand _pdfTbl69FiSpeciessesCommand;
    public ICommand PdfTbl69FiSpeciessesCommand
    {
        get { return _pdfTbl69FiSpeciessesCommand ?? (_pdfTbl69FiSpeciessesCommand = new RelayCommand(delegate { CreatePdfTbl69FiSpeciesses(_mainId); })); }
    }

    private static void CreatePdfTbl69FiSpeciesses(int id)
    {
        ReportTbl69FiSpeciessesPdf.CreateMainPdf(id);
    }
    //------------------------------------------------------------------------------
    //------------------------------------------------------------------------------  		   
        
        IList<Tbl69FiSpecies> ListTbl69FiSpeciessesByFiSpeciesId(int fispeciesId);
        IList<Tbl78Name> ListTbl78NamesByFiSpeciesIdAndHash(int fispeciesId);
        IList<Tbl84Synonym> ListTbl84SynonymsByFiSpeciesIdAndHash(int fispeciesId);

        IList<Tbl90Reference> ListTbl90AuthorsByFiSpeciesId(int fispeciesId);
        IList<Tbl90Reference> ListTbl90SourcesByFiSpeciesId(int fispeciesId);
        IList<Tbl90Reference> ListTbl90ExpertsByFiSpeciesId(int fispeciesId);

        IList<Tbl93Comment> ListTbl93CommentsByFiSpeciesId(int fispeciesId);   
        
		public IList<Tbl69FiSpecies> ListTbl69FiSpeciessesByFiSpeciesId(int fispeciesId)
		{
			return _tbl69FiSpeciessesRepository.ListWhereOrderByInclude(
				e => e.FiSpeciesID == fispeciesId,
				_tbl69FiSpeciessesRepository.OrderBy(r => r.FiSpeciesName + r.Subregnum),
				p => p.Tbl78Names, k => k.Tbl81Images);
		}

		public IList<Tbl78Name> ListTbl78NamesByFiSpeciesIdAndHash(int fispeciesId)
		{
			return _tbl78NamesRepository.ListWhereOrderByInclude(
				e => e.FiSpeciesID == fispeciesId &&
				e.NameName.Contains("#") == false,
				_tbl78NamesRepository.OrderBy(r => r.NameName),
				p => p.Tbl81Images, k => k.Tbl69FiSpeciesses);
		}

		public IList<Tbl84Synonym> ListTbl84SynonymsByFiSpeciesIdAndHash(int fispeciesId)
		{
			return _tbl84SynonymsRepository.ListWhereOrderByInclude(
				e => e.FiSpeciesID == fispeciesId &&
				e.SynonymName.Contains("#") == false,
				_tbl84SynonymsRepository.OrderBy(r => r.SynonymName),
				p => p.Tbl87Geographics, k => k.Tbl69FiSpeciesses);
		}

	               //------------------------------------------------

		public IList<Tbl90Reference> ListTbl90AuthorsByFiSpeciesId(int fispeciesId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefAuthorID == e.Tbl90RefAuthors.RefAuthorID && e.FiSpeciesID == fispeciesId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefAuthors.RefAuthorName + r.Tbl90RefAuthors.ArticelTitle + r.Tbl90RefAuthors.BookName + r.Tbl90RefAuthors.Page1 + r.Tbl90RefAuthors.Publisher),
				p => p.Tbl90RefAuthors);
		}

		public IList<Tbl90Reference> ListTbl90SourcesByFiSpeciesId(int fispeciesId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefSourceID == e.Tbl90RefSources.RefSourceID && e.FiSpeciesID == fispeciesId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefSources.RefSourceName + r.Tbl90RefSources.SourceYear),
				p => p.Tbl90RefSources);
		}

		public IList<Tbl90Reference> ListTbl90ExpertsByFiSpeciesId(int fispeciesId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefExpertID == e.Tbl90RefExperts.RefExpertID && e.FiSpeciesID == fispeciesId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefExperts.RefExpertName),
				p => p.Tbl90RefExperts);
		}

		public IList<Tbl90Reference> ListTbl90RefAuthorsByFiSpeciesId(int fispeciesId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefAuthorID == e.Tbl90RefAuthors.RefAuthorID && e.FiSpeciesID == fispeciesId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefAuthors.RefAuthorName + r.Tbl90RefAuthors.ArticelTitle + r.Tbl90RefAuthors.BookName + r.Tbl90RefAuthors.Page1 + r.Tbl90RefAuthors.Publisher),
				p => p.Tbl90RefAuthors);
		}

		public IList<Tbl90Reference> ListTbl90RefSourcesByFiSpeciesId(int fispeciesId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefSourceID == e.Tbl90RefSources.RefSourceID && e.FiSpeciesID == fispeciesId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefSources.RefSourceName + r.Tbl90RefSources.SourceYear),
				p => p.Tbl90RefSources);
		}

		public IList<Tbl90Reference> ListTbl90RefExpertsByFiSpeciesId(int fispeciesId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefExpertID == e.Tbl90RefExperts.RefExpertID && e.FiSpeciesID == fispeciesId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefExperts.RefExpertName),
				p => p.Tbl90RefExperts);
		}

	               //------------------------------------------------

		public IList<Tbl93Comment> ListTbl93CommentsByFiSpeciesId(int fispeciesId)
		{
			return _tbl93CommentsRepository.ListWhereOrderByInclude(
				e => e.FiSpeciesID == fispeciesId,
				_tbl93CommentsRepository.OrderBy(r => r.Info),
				p => p.Tbl78Names, k => k.Tbl81Images);
		}      
  

         
}   

