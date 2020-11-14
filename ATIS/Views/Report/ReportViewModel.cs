using ATIS.Dal.Models;
using ATIS.Ui.Core;
using ATIS.Ui.Helper;
using ATIS.Ui.Views.Report.PDF;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using ATIS.Ui.Views.Report.D03Regnum;
using ATIS.Ui.Views.Report.D06Phylum;

namespace ATIS.Ui.Views.Report
{
    public class ReportViewModel : ViewModelBase
    {
        #region "Private Data Members"
        //private static IBusinessLayer _businessLayer;
        //private static DbEntityException _entityException;
        //	private readonly UnitOfWork _uow = new UnitOfWork(new AtisDbContext());
        private readonly AtisDbContext _context = new AtisDbContext();
        private readonly ReportBasicGet _extReportBasicGet = new ReportBasicGet();

        private readonly int _mainId;
        private readonly int _fishId;
        private readonly int _plantId;

        #endregion "Private Data Members"

        public ReportViewModel(int id, string tab)
        {
            //  Search for SubdivisionID of name Plantae#Regnum# 
            //         var plantaeRegnum = _businessLayer.SingleListTbl15SubdivisionsBySubdivisionName("Plantae#Regnum#");
            var plantaeRegnum = _context.Tbl15Subdivisions.FirstOrDefault(e => e.SubdivisionName == "Plantae#Regnum#");
            if (plantaeRegnum != null) _plantId = plantaeRegnum.SubdivisionId;

            //Search for SubphylumID of name Anaimalia#Regnum# 
            //    var animaliaRegnum = _businessLayer.SingleListTbl12SubphylumsBySubphylumName("Animalia#Regnum#");
            var animaliaRegnum = _context.Tbl12Subphylums.FirstOrDefault(e => e.SubphylumName == "Animalia#Regnum#");

            if (animaliaRegnum != null) _fishId = animaliaRegnum.SubphylumId;

            _mainId = id;

            switch (tab)
            {
                case "Tbl03Regnums":
                    GetTbl03RegnumsById(id);
                    break;
                case "Tbl06Phylums":
                    GetTbl06PhylumsById(id);
                    break;
                    //    case "Tbl09Divisions":
                    //        GetTbl09DivisionsById(id);
                    //        break;
                    //    case "Tbl12Subphylums":
                    //        GetTbl12SubphylumsById(id);
                    //        break;
                    //    case "Tbl15Subdivisions":
                    //        GetTbl15SubdivisionsById(id);
                    //        break;
                    //    case "Tbl18Superclasses":
                    //        GetTbl18SuperclassesById(id, _fishId, _plantId);
                    //        break;
                    //    case "Tbl21Classes":
                    //        GetTbl21ClassesById(id, _fishId, _plantId);
                    //        break;
                    //    case "Tbl24Subclasses":
                    //        GetTbl24SubclassesById(id, _fishId, _plantId);
                    //        break;
                    //    case "Tbl27Infraclasses":
                    //        GetTbl27InfraclassesById(id, _fishId, _plantId);
                    //        break;
                    //    case "Tbl30legios":
                    //        GetTbl30LegiosById(id, _fishId, _plantId);
                    //        break;
                    //    case "Tbl33Ordos":
                    //        GetTbl33OrdosById(id, _fishId, _plantId);
                    //        break;
                    //    case "Tbl36Subordos":
                    //        GetTbl36SubordosById(id, _fishId, _plantId);
                    //        break;
                    //    case "Tbl39Infraordos":
                    //        GetTbl39InfraordosById(id, _fishId, _plantId);
                    //        break;
                    //    case "Tbl42Superfamilies":
                    //        GetTbl42SuperfamiliesById(id, _fishId, _plantId);
                    //        break;
                    //    case "Tbl45Families":
                    //        GetTbl45FamiliesById(id, _fishId, _plantId);
                    //        break;
                    //    case "Tbl48Subfamilies":
                    //        GetTbl48SubfamiliesById(id, _fishId, _plantId);
                    //        break;
                    //    case "Tbl51Infrafamilies":
                    //        GetTbl51InfrafamiliesById(id, _fishId, _plantId);
                    //        break;
                    //    case "Tbl54Supertribusses":
                    //        GetTbl54SupertribussesById(id, _fishId, _plantId);
                    //        break;
                    //    case "Tbl57Tribusses":
                    //        GetTbl57TribussesById(id, _fishId, _plantId);
                    //        break;
                    //    case "Tbl60Subtribusses":
                    //        GetTbl60SubtribussesById(id, _fishId, _plantId);
                    //        break;
                    //    case "Tbl63Infratribusses":
                    //        GetTbl63InfratribussesById(id, _fishId, _plantId);
                    //        break;
                    //    case "Tbl66Genusses":
                    //        GetTbl66GenussesById(id, _fishId, _plantId);
                    //        break;
                    //    case "Tbl68Speciesgroups":
                    //        GetTbl68SpeciesgroupsById(id);
                    //        break;
                    //    case "Tbl69FiSpeciesses":
                    //        GetTbl69FiSpeciessesById(id, _fishId, _plantId);
                    //        break;
                    //    case "Tbl72PlSpeciesses":
                    //        GetTbl72PlSpeciessesById(id, _fishId, _plantId);
                    //        break;
                    //    case "Tbl78Names":
                    //        GetTbl78NamesById(id);
                    //        break;
                    //    case "Tbl84Synonyms":
                    //        GetTbl84SynonymsById(id);
                    //        break;

            }
        }

        public ReportViewModel()
        {

        }

        //		#region Methods

        public void GetTbl03RegnumsById(int id)
        {
            RegnumsCollection = _extReportBasicGet.CollRegnumsByRegnumId(id);

            //direct children
            PhylumsCollection = _extReportBasicGet.CollPhylumsByRegnumIdAndHash(id);

            DivisionsCollection = _extReportBasicGet.CollDivisionsByRegnumIdAndHash(id);

            //------------------------------------------------------------------------------

            ExpertsCollection = _extReportBasicGet.CollExpertsByRegnumId(id);

            SourcesCollection = _extReportBasicGet.CollSourcesByRegnumId(id);

            AuthorsCollection = _extReportBasicGet.CollAuthorsByRegnumId(id);

            //------------------------------------------------------------------------------

            CommentsCollection = _extReportBasicGet.CollCommentsByRegnumId(id);
        }
        private RelayCommand _pdfTbl03RegnumsCommand;
        public ICommand PdfTbl03RegnumsCommand
        {
            get { return _pdfTbl03RegnumsCommand ??= new RelayCommand(delegate { CreatePdfTbl03Regnums(_mainId); }); }
        }

        private static void CreatePdfTbl03Regnums(int id)
        {
            ReportRegnumPdf.CreateMainPdf(id);
        }
        //------------------------------------------------------------------------------
        //------------------------------------------------------------------------------  		   
        public void GetTbl06PhylumsById(int id)
        {
            PhylumsCollection = _extReportBasicGet.CollPhylumsByPhylumId(id);

            //direct children
            SubphylumsCollection = _extReportBasicGet.CollSubphylumsByPhylumIdAndHash(id);

            //------------------------------------------------------------------------------
            //Function
            var regnumId = _extReportBasicGet.RegnumIdFromPhylumsCollectionSelect(id);

            //-----------------------------------------------------------------------------
            //ForeignKeyTable
            RegnumsCollection = _extReportBasicGet.CollRegnumsByRegnumIdAndHash(regnumId);

            //------------------------------------------------------------------------------

            ExpertsCollection = _extReportBasicGet.CollExpertsByPhylumId(id);

            SourcesCollection = _extReportBasicGet.CollSourcesByPhylumId(id);

            AuthorsCollection = _extReportBasicGet.CollAuthorsByPhylumId(id);

            //------------------------------------------------------------------------------

            CommentsCollection = _extReportBasicGet.CollCommentsByPhylumId(id);
        }
        private RelayCommand _pdfTbl06PhylumsCommand;
        public ICommand PdfTbl06PhylumsCommand
        {
            get { return _pdfTbl06PhylumsCommand ??= new RelayCommand(delegate { CreatePdfTbl06Phylums(_mainId); }); }
        }

        private static void CreatePdfTbl06Phylums(int id)
        {
            ReportPhylumPdf.CreateMainPdf(id);
        }
        //------------------------------------------------------------------------------
        //------------------------------------------------------------------------------  		   

        //------------Direct Children-------------------------------------

        #region "Private Properties"
        public string FilterText { get; set; }

        public int Id { get; set; }


        public ObservableCollection<Tbl03Regnum> RegnumsCollection { get; set; }
        public ObservableCollection<Tbl06Phylum> PhylumsCollection { get; set; }
        public ObservableCollection<Tbl09Division> DivisionsCollection { get; set; }
        public ObservableCollection<Tbl12Subphylum> SubphylumsCollection { get; set; }


        #endregion "Public Properties Tbl93Comment"

        #region "Public Properties Tbl90Author"
        public ObservableCollection<Tbl90Reference> AuthorsCollection { get; set; }

        public ObservableCollection<Tbl90Reference> Tbl90RefAuthorsList { get; set; }

        #endregion "Public Properties Tbl90Author"

        #region "Public Properties Tbl90Source"
        public ObservableCollection<Tbl90Reference> SourcesCollection { get; set; }

        public ObservableCollection<Tbl90Reference> Tbl90RefSourcesList { get; set; }


        #endregion "Public Properties Tbl90Source"

        #region "Public Properties Tbl90Expert"
        public ObservableCollection<Tbl90Reference> ExpertsCollection { get; set; }


        public ObservableCollection<Tbl90Reference> Tbl90RefExpertsList { get; set; }


        #endregion "Public Properties Tbl90Expert"

        #region "Public Properties Tbl93Comment"
        public ObservableCollection<Tbl93Comment> CommentsCollection { get; set; }


        #endregion "Public Properties Tbl93Comment"

    }

    #region Item Properties

    #endregion
}
