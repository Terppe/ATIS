using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Te.Atis.BusinessLayer;
using Te.Atis.DomainModel;
using Te.Atis.Ui.Desktop.Views.Report.PDF;    

// <!-- Interface Skriptdatum:  19.06.2018  10:32     -->  

namespace Te.Atis.Ui.Desktop.Views.Report   
{       

           
     public void GetTbl39InfraordosById(int id)
    {
        Tbl39InfraordosList = new ObservableCollection<Tbl39Infraordo>(_businessLayer.ListTbl39InfraordosByInfraordoId(id));		   
        
        //direct children
        Tbl42SuperfamiliesList = new ObservableCollection<Tbl42Superfamily>(_businessLayer.ListTbl42SuperfamiliesByInfraordoIdAndHash(id));

        //------------------------------------------------------------------------------

         var subordoInfraordoId = SubordoIdTbl39InfraordosSelect(id);	
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
        Tbl90RefExpertsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90RefExpertsByInfraordoId(id));

        Tbl90RefSourcesList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90RefSourcesByInfraordoId(id));

        Tbl90RefAuthorsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90RefAuthorsByInfraordoId(id));

        Tbl90ExpertsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ExpertsByInfraordoId(id));

        Tbl90SourcesList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90SourcesByInfraordoId(id));

        Tbl90AuthorsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90AuthorsByInfraordoId(id));

        Tbl93CommentsList = new ObservableCollection<Tbl93Comment>(_businessLayer.ListTbl93CommentsByInfraordoId(id));
    } 
    private RelayCommand _pdfTbl39InfraordosCommand;
    public ICommand PdfTbl39InfraordosCommand
    {
        get { return _pdfTbl39InfraordosCommand ?? (_pdfTbl39InfraordosCommand = new RelayCommand(delegate { CreatePdfTbl39Infraordos(_mainId); })); }
    }

    private static void CreatePdfTbl39Infraordos(int id)
    {
        ReportTbl39InfraordosPdf.CreateMainPdf(id);
    }
    //------------------------------------------------------------------------------
    //------------------------------------------------------------------------------  		   
           
        IList<Tbl39Infraordo> ListTbl39InfraordosByInfraordoId(int infraordoId);

        IList<Tbl42Superfamily> ListTbl42SuperfamiliesByInfraordoIdAndHash(int infraordoId);

        IList<Tbl90Reference> ListTbl90AuthorsByInfraordoId(int infraordoId);
        IList<Tbl90Reference> ListTbl90SourcesByInfraordoId(int infraordoId);
        IList<Tbl90Reference> ListTbl90ExpertsByInfraordoId(int infraordoId);

        IList<Tbl93Comment> ListTbl93CommentsByInfraordoId(int infraordoId);	   
           
		public IList<Tbl39Infraordo> ListTbl39InfraordosByInfraordoId(int infraordoId)
		{
			return _tbl39InfraordosRepository.ListWhereOrderByInclude(
				e => e.InfraordoID == infraordoId,
				_tbl39InfraordosRepository.OrderBy(r => r.InfraordoName + r.Subregnum),
				p => p.Tbl42Superfamilies, k => k.NULL);
		}

		public IList<Tbl42Superfamily> ListTbl42SuperfamiliesByInfraordoIdAndHash(int infraordoId)
		{
			return _tbl42SuperfamiliesRepository.ListWhereOrderByInclude(
				e => e.InfraordoID == infraordoId &&
				e.SuperfamilyName.Contains("#") == false,
				_tbl42SuperfamiliesRepository.OrderBy(r => r.SuperfamilyName),
				p => p.NULL, k => k.Tbl39Infraordos);
		}

	               //------------------------------------------------

		public IList<Tbl90Reference> ListTbl90AuthorsByInfraordoId(int infraordoId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefAuthorID == e.Tbl90RefAuthors.RefAuthorID && e.InfraordoID == infraordoId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefAuthors.RefAuthorName + r.Tbl90RefAuthors.ArticelTitle + r.Tbl90RefAuthors.BookName + r.Tbl90RefAuthors.Page1 + r.Tbl90RefAuthors.Publisher),
				p => p.Tbl90RefAuthors);
		}

		public IList<Tbl90Reference> ListTbl90SourcesByInfraordoId(int infraordoId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefSourceID == e.Tbl90RefSources.RefSourceID && e.InfraordoID == infraordoId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefSources.RefSourceName + r.Tbl90RefSources.SourceYear),
				p => p.Tbl90RefSources);
		}

		public IList<Tbl90Reference> ListTbl90ExpertsByInfraordoId(int infraordoId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefExpertID == e.Tbl90RefExperts.RefExpertID && e.InfraordoID == infraordoId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefExperts.RefExpertName),
				p => p.Tbl90RefExperts);
		}

		public IList<Tbl90Reference> ListTbl90RefAuthorsByInfraordoId(int infraordoId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefAuthorID == e.Tbl90RefAuthors.RefAuthorID && e.InfraordoID == infraordoId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefAuthors.RefAuthorName + r.Tbl90RefAuthors.ArticelTitle + r.Tbl90RefAuthors.BookName + r.Tbl90RefAuthors.Page1 + r.Tbl90RefAuthors.Publisher),
				p => p.Tbl90RefAuthors);
		}

		public IList<Tbl90Reference> ListTbl90RefSourcesByInfraordoId(int infraordoId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefSourceID == e.Tbl90RefSources.RefSourceID && e.InfraordoID == infraordoId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefSources.RefSourceName + r.Tbl90RefSources.SourceYear),
				p => p.Tbl90RefSources);
		}

		public IList<Tbl90Reference> ListTbl90RefExpertsByInfraordoId(int infraordoId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefExpertID == e.Tbl90RefExperts.RefExpertID && e.InfraordoID == infraordoId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefExperts.RefExpertName),
				p => p.Tbl90RefExperts);
		}

	               //------------------------------------------------
 
		public IList<Tbl93Comment> ListTbl93CommentsByInfraordoId(int infraordoId)
		{
			return _tbl93CommentsRepository.ListWhereOrderByInclude(
				e => e.InfraordoID == infraordoId,
				_tbl93CommentsRepository.OrderBy(r => r.Info),
				p => p.Tbl42Superfamilies);
		}      
  

         
}   

