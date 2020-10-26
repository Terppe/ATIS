using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Te.Atis.BusinessLayer;
using Te.Atis.DomainModel;
using Te.Atis.Ui.Desktop.Views.Report.PDF;    

// <!-- Interface Skriptdatum:  29.01.2019  10:32     -->  

namespace Te.Atis.Ui.Desktop.Views.Report   
{       

           
     public void GetTbl33OrdosById(int id)
    {
        Tbl33OrdosList = new ObservableCollection<Tbl33Ordo>(_businessLayer.ListTbl33OrdosByOrdoId(id));		   
        
        //direct children
        Tbl36SubordosList = new ObservableCollection<Tbl36Subordo>(_businessLayer.ListTbl36SubordosByOrdoIdAndHash(id));

        //------------------------------------------------------------------------------

         var legioOrdoId = LegioIdTbl33OrdosSelect(id);	
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
        Tbl90RefExpertsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90RefExpertsByOrdoId(id));

        Tbl90RefSourcesList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90RefSourcesByOrdoId(id));

        Tbl90RefAuthorsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90RefAuthorsByOrdoId(id));

        Tbl90ExpertsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ExpertsByOrdoId(id));

        Tbl90SourcesList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90SourcesByOrdoId(id));

        Tbl90AuthorsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90AuthorsByOrdoId(id));

        Tbl93CommentsList = new ObservableCollection<Tbl93Comment>(_businessLayer.ListTbl93CommentsByOrdoId(id));
    } 
    private RelayCommand _pdfTbl33OrdosCommand;
    public ICommand PdfTbl33OrdosCommand
    {
        get { return _pdfTbl33OrdosCommand ?? (_pdfTbl33OrdosCommand = new RelayCommand(delegate { CreatePdfTbl33Ordos(_mainId); })); }
    }

    private static void CreatePdfTbl33Ordos(int id)
    {
        ReportTbl33OrdosPdf.CreateMainPdf(id);
    }
    //------------------------------------------------------------------------------
    //------------------------------------------------------------------------------  		   
           
        IList<Tbl33Ordo> ListTbl33OrdosByOrdoId(int ordoId);

        IList<Tbl36Subordo> ListTbl36SubordosByOrdoIdAndHash(int ordoId);

        IList<Tbl90Reference> ListTbl90AuthorsByOrdoId(int ordoId);
        IList<Tbl90Reference> ListTbl90SourcesByOrdoId(int ordoId);
        IList<Tbl90Reference> ListTbl90ExpertsByOrdoId(int ordoId);

        IList<Tbl93Comment> ListTbl93CommentsByOrdoId(int ordoId);	   
           
		public IList<Tbl33Ordo> ListTbl33OrdosByOrdoId(int ordoId)
		{
			return _tbl33OrdosRepository.ListWhereOrderByInclude(
				e => e.OrdoID == ordoId,
				_tbl33OrdosRepository.OrderBy(r => r.OrdoName + r.Subregnum),
				p => p.Tbl36Subordos, k => k.Tbl39Infraordos);
		}

		public IList<Tbl36Subordo> ListTbl36SubordosByOrdoIdAndHash(int ordoId)
		{
			return _tbl36SubordosRepository.ListWhereOrderByInclude(
				e => e.OrdoID == ordoId &&
				e.SubordoName.Contains("#") == false,
				_tbl36SubordosRepository.OrderBy(r => r.SubordoName),
				p => p.NULL, k => k.Tbl33Ordos);
		}

	               //------------------------------------------------

		public IList<Tbl90Reference> ListTbl90AuthorsByOrdoId(int ordoId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefAuthorID == e.Tbl90RefAuthors.RefAuthorID && e.OrdoID == ordoId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefAuthors.RefAuthorName + r.Tbl90RefAuthors.ArticelTitle + r.Tbl90RefAuthors.BookName + r.Tbl90RefAuthors.Page1 + r.Tbl90RefAuthors.Publisher),
				p => p.Tbl90RefAuthors);
		}

		public IList<Tbl90Reference> ListTbl90SourcesByOrdoId(int ordoId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefSourceID == e.Tbl90RefSources.RefSourceID && e.OrdoID == ordoId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefSources.RefSourceName + r.Tbl90RefSources.SourceYear),
				p => p.Tbl90RefSources);
		}

		public IList<Tbl90Reference> ListTbl90ExpertsByOrdoId(int ordoId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefExpertID == e.Tbl90RefExperts.RefExpertID && e.OrdoID == ordoId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefExperts.RefExpertName),
				p => p.Tbl90RefExperts);
		}

		public IList<Tbl90Reference> ListTbl90RefAuthorsByOrdoId(int ordoId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefAuthorID == e.Tbl90RefAuthors.RefAuthorID && e.OrdoID == ordoId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefAuthors.RefAuthorName + r.Tbl90RefAuthors.ArticelTitle + r.Tbl90RefAuthors.BookName + r.Tbl90RefAuthors.Page1 + r.Tbl90RefAuthors.Publisher),
				p => p.Tbl90RefAuthors);
		}

		public IList<Tbl90Reference> ListTbl90RefSourcesByOrdoId(int ordoId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefSourceID == e.Tbl90RefSources.RefSourceID && e.OrdoID == ordoId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefSources.RefSourceName + r.Tbl90RefSources.SourceYear),
				p => p.Tbl90RefSources);
		}

		public IList<Tbl90Reference> ListTbl90RefExpertsByOrdoId(int ordoId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefExpertID == e.Tbl90RefExperts.RefExpertID && e.OrdoID == ordoId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefExperts.RefExpertName),
				p => p.Tbl90RefExperts);
		}

	               //------------------------------------------------
 
		public IList<Tbl93Comment> ListTbl93CommentsByOrdoId(int ordoId)
		{
			return _tbl93CommentsRepository.ListWhereOrderByInclude(
				e => e.OrdoID == ordoId,
				_tbl93CommentsRepository.OrderBy(r => r.Info),
				p => p.Tbl36Subordos);
		}      
  

         
}   

