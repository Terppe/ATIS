using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using ATIS.Dal.Models;
using ATIS.Ui.Core;
using ATIS.Ui.Helper;
using ATIS.Ui.Views.Report.PDF;

namespace ATIS.Ui.Views.Report
{
    public class ReportViewModel : ViewModelBase
    {
        #region "Private Data Members"
        //private static IBusinessLayer _businessLayer;
        //private static DbEntityException _entityException;
        private readonly UnitOfWork _uow = new UnitOfWork(new AtisDbContext());
        private readonly AtisDbContext _context = new AtisDbContext();

        private readonly int _mainId;
        private readonly int _fishId;
        private readonly int _plantId;

        #endregion "Private Data Members"

        public ReportViewModel(int id, string tab)
        {

            //_businessLayer = new BusinessLayer.BusinessLayer();
            //_entityException = new DbEntityException();

          //  Search for SubdivisionID of name Plantae#Regnum# 
     //         var plantaeRegnum = _businessLayer.SingleListTbl15SubdivisionsBySubdivisionName("Plantae#Regnum#");

            var plantaeRegnum =   _context.Tbl15Subdivisions.FirstOrDefault(e => e.SubdivisionName == "Plantae#Regnum#");

            if (plantaeRegnum != null) _plantId = plantaeRegnum.SubdivisionId;


            //Search for SubphylumID of name Anaimalia#Regnum# 
        //    var animaliaRegnum = _businessLayer.SingleListTbl12SubphylumsBySubphylumName("Animalia#Regnum#");

            var animaliaRegnum = _context.Tbl12Subphylums.FirstOrDefault(e => e.SubphylumName == "Animalia#Regnum#");

            if (animaliaRegnum != null) _fishId = animaliaRegnum.SubphylumId;

            _mainId = id;

            switch (tab)
            {
            //    case "Tbl03Regnums":
            //        GetTbl03RegnumsById(id);
            //        break;
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
          //  Tbl03RegnumsList = new ObservableCollection<Tbl03Regnum>(_businessLayer.ListTbl03RegnumsByRegnumId(id));
            Tbl03RegnumsList = new ObservableCollection<Tbl03Regnum>(_uow.Tbl03Regnums.Find(e => e.RegnumId == id));

            //direct children
            //      Tbl06PhylumsList = new ObservableCollection<Tbl06Phylum>(_businessLayer.ListTbl06PhylumsByRegnumIdAndHash(id));
            Tbl06PhylumsList = new ObservableCollection<Tbl06Phylum>(_uow.Tbl06Phylums
                .Find(e => e.RegnumId == id && e.PhylumName.Contains("#") == false)
                .OrderBy(a => a.PhylumName));

            //   Tbl09DivisionsList = new ObservableCollection<Tbl09Division>(_businessLayer.ListTbl09DivisionsByRegnumIdAndHash(id));
            Tbl09DivisionsList = new ObservableCollection<Tbl09Division>(_uow.Tbl09Divisions
                .Find(e => e.RegnumId == id && e.DivisionName.Contains("#") == false)
                .OrderBy(a => a.DivisionName));


            //------------------------------------------------------------------------------

            //     Tbl90ExpertsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ExpertsByRegnumId(id));
            Tbl90ExpertsList = new ObservableCollection<Tbl90Reference>(_uow.Tbl90References
                .Find(e => e.RegnumId == id &&
                           e.RefAuthorId.HasValue == false &&
                           e.RefSourceId.HasValue == false));

            //public IList<Tbl90Reference> ListTbl90ExpertsByRegnumId(int regnumId)
            //{
            //    return _tbl90ReferencesRepository.ListWhereOrderByInclude(
            //        e => e.RefExpertID == e.Tbl90RefExperts.RefExpertID &&
            //             e.RegnumID == regnumId &&
            //             e.RefAuthorID.HasValue == false &&
            //             e.RefSourceID.HasValue == false,
            //        _tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefExperts.RefExpertName),
            //        p => p.Tbl90RefExperts);
            //}


            //      Tbl90SourcesList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90SourcesByRegnumId(id));
            Tbl90SourcesList = new ObservableCollection<Tbl90Reference>(_uow.Tbl90References
                .Find(e => e.RegnumId == id &&
                           e.RefExpertId.HasValue == false &&
                           e.RefAuthorId.HasValue == false));


            //public IList<Tbl90Reference> ListTbl90SourcesByRegnumId(int regnumId)
            //{
            //    return _tbl90ReferencesRepository.ListWhereOrderByInclude(
            //        e => e.RefSourceID == e.Tbl90RefSources.RefSourceID &&
            //             e.RegnumID == regnumId &&
            //             e.RefExpertID.HasValue == false &&
            //             e.RefAuthorID.HasValue == false,
            //        _tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefSources.RefSourceName + r.Tbl90RefSources.SourceYear),
            //        p => p.Tbl90RefSources);
            //}


            //       Tbl90AuthorsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90AuthorsByRegnumId(id));
            Tbl90AuthorsList = new ObservableCollection<Tbl90Reference>(_uow.Tbl90References
                .Find(e => e.RegnumId == id &&
                           e.RefExpertId.HasValue == false &&
                           e.RefSourceId.HasValue == false));

            //public IList<Tbl90Reference> ListTbl90AuthorsByRegnumId(int regnumId)
            //{
            //    return _tbl90ReferencesRepository.ListWhereOrderByInclude(
            //        e => e.RefAuthorID == e.Tbl90RefAuthors.RefAuthorID &&
            //             e.RegnumID == regnumId &&
            //             e.RefExpertID.HasValue == false &&
            //             e.RefSourceID.HasValue == false,
            //        _tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefAuthors.RefAuthorName + r.Tbl90RefAuthors.ArticelTitle + r.Tbl90RefAuthors.BookName + r.Tbl90RefAuthors.Page1 + r.Tbl90RefAuthors.Publisher),
            //        p => p.Tbl90RefAuthors);
            //}


            //     Tbl93CommentsList = new ObservableCollection<Tbl93Comment>(_businessLayer.ListTbl93CommentsByRegnumId(id));
            Tbl93CommentsList = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments
                .Find(e => e.RegnumId == id )
                .OrderBy(r => r.Info));

            //public IList<Tbl93Comment> ListTbl93CommentsByRegnumId(int regnumId)
            //{
            //    return _tbl93CommentsRepository.ListWhereOrderByInclude(
            //        e => e.RegnumID == regnumId,
            //        _tbl93CommentsRepository.OrderBy(r => r.Info),
            //        p => p.Tbl06Phylums, k => k.Tbl09Divisions);
            //}

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
            //    Tbl06PhylumsList = new ObservableCollection<Tbl06Phylum>(_businessLayer.ListTbl06PhylumsByPhylumId(id));
            Tbl06PhylumsList = new ObservableCollection<Tbl06Phylum>(_uow.Tbl06Phylums
                .Find(e => e.PhylumId == id));

            //      PhylumsCollection = _extGet.SearchNameAndIdReturnCollection<Tbl06Phylum>(searchName, "phylum");

            //    //direct children
            //    Tbl12SubphylumsList = new ObservableCollection<Tbl12Subphylum>(_businessLayer.ListTbl12SubphylumsByPhylumIdAndHash(id));
            Tbl12SubphylumsList = new ObservableCollection<Tbl12Subphylum>(_uow.Tbl12Subphylums
                .Find(e => e.PhylumId == Id &&
                           e.SubphylumName.Contains("#") == false
                ));

            //    //------------------------------------------------------------------------------

            //    var regnumPhylumId = RegnumIdTbl06PhylumsSelect(id);

            //    Tbl03RegnumsList = new ObservableCollection<Tbl03Regnum>(_businessLayer.ListTbl03RegnumsByRegnumIdAndHash(regnumPhylumId));
            Tbl03RegnumsList = new ObservableCollection<Tbl03Regnum>(_uow.Tbl03Regnums
                .Find(e => e.RegnumId == Id &&
                           e.RegnumName.Contains("#") == false
                ));

            //    //------------------------------------------------------------------------------

            //    Tbl90ExpertsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ExpertsByPhylumId(id));

            //    Tbl90SourcesList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90SourcesByPhylumId(id));

            //    Tbl90AuthorsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90AuthorsByPhylumId(id));

            //    Tbl93CommentsList = new ObservableCollection<Tbl93Comment>(_businessLayer.ListTbl93CommentsByPhylumId(id));
        }
        //private RelayCommand _pdfTbl06PhylumsCommand;
        //public ICommand PdfTbl06PhylumsCommand
        //{
        //    get { return _pdfTbl06PhylumsCommand ??= new RelayCommand(delegate { CreatePdfTbl06Phylums(_mainId); }); }
        //}

        //private static void CreatePdfTbl06Phylums(int id)
        //{
        //    ReportTbl06PhylumsPdf.CreateMainPdf(id);
        //}
        //------------------------------------------------------------------------------
        //------------------------------------------------------------------------------  		   

        //------------Direct Children-------------------------------------

        #region "Private Properties"
        public string FilterText { get; set; }

         public int Id { get; set; }

 
        public ObservableCollection<Tbl03Regnum> Tbl03RegnumsList { get; set; }


        public ObservableCollection<Tbl06Phylum> Tbl06PhylumsList { get; set; }
        public ObservableCollection<Tbl09Division> Tbl09DivisionsList { get; set; }
        public ObservableCollection<Tbl12Subphylum> Tbl12SubphylumsList { get; set; }


        #endregion "Public Properties Tbl93Comment"

        #region "Public Properties Tbl90Author"
        public ObservableCollection<Tbl90Reference> Tbl90AuthorsList { get; set; }

          public ObservableCollection<Tbl90Reference> Tbl90RefAuthorsList { get; set; }

        #endregion "Public Properties Tbl90Author"

        #region "Public Properties Tbl90Source"
        public ObservableCollection<Tbl90Reference> Tbl90SourcesList { get; set; }

         public ObservableCollection<Tbl90Reference> Tbl90RefSourcesList { get; set; }

 
        #endregion "Public Properties Tbl90Source"

        #region "Public Properties Tbl90Expert"
        public ObservableCollection<Tbl90Reference> Tbl90ExpertsList { get; set; }

 
        public ObservableCollection<Tbl90Reference> Tbl90RefExpertsList { get; set; }

 
        #endregion "Public Properties Tbl90Expert"

        #region "Public Properties Tbl93Comment"
        public ObservableCollection<Tbl93Comment> Tbl93CommentsList { get; set; }


        #endregion "Public Properties Tbl93Comment"
    }

    #region Item Properties

    public class SourceItemRefSource
    {
        public string RefSourceName { get; set; }
        public string SourceYear { get; set; }
        public string Author { get; set; }
        public string Notes { get; set; }
        public string Info { get; set; }
        public string Memo { get; set; }
        public bool? Valid { get; set; }
        public string ValidYear { get; set; }
        public string RefMemo { get; set; }
        public string RefInfo { get; set; }
    }
    public class SourceItemRefExpert
    {
        public string RefExpertName { get; set; }
        public string Notes { get; set; }
        public string Info { get; set; }
        public string Memo { get; set; }
        public bool? Valid { get; set; }
        public string ValidYear { get; set; }
        public string RefMemo { get; set; }
        public string RefInfo { get; set; }
    }
    public class SourceItemRefAuthor
    {
        public string RefAuthorName { get; set; }
        public string PublicationYear { get; set; }
        public string ArticelTitle { get; set; }
        public string BookName { get; set; }
        public string Page1 { get; set; }
        public string Publisher { get; set; }
        public string PublicationPlace { get; set; }
        public string ISBN { get; set; }
        public string Notes { get; set; }
        public string Info { get; set; }
        public string Memo { get; set; }
        public bool? Valid { get; set; }
        public string ValidYear { get; set; }
        public string RefMemo { get; set; }
        public string RefInfo { get; set; }

    }
    #endregion
}
