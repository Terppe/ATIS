using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Te.Atis.BusinessLayer;
using Te.Atis.DomainModel;
using Te.Atis.Ui.Desktop.Views.Report.PDF;    

// <!-- Interface Skriptdatum:  21.06.2018  10:32     -->  

namespace Te.Atis.Ui.Desktop.Views.Report   
{       

           
     public void GetTbl68SpeciesgroupsById(int id)
    {
        Tbl68SpeciesgroupsList = new ObservableCollection<Tbl68Speciesgroup>(_businessLayer.ListTbl68SpeciesgroupsBySpeciesgroupId(id));		   
           
        //------------------------------------------------------------------------------
        Tbl90RefExpertsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90RefExpertsBySpeciesgroupId(id));

        Tbl90RefSourcesList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90RefSourcesBySpeciesgroupId(id));

        Tbl90RefAuthorsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90RefAuthorsBySpeciesgroupId(id));

        Tbl90ExpertsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ExpertsBySpeciesgroupId(id));

        Tbl90SourcesList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90SourcesBySpeciesgroupId(id));

        Tbl90AuthorsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90AuthorsBySpeciesgroupId(id));

        Tbl93CommentsList = new ObservableCollection<Tbl93Comment>(_businessLayer.ListTbl93CommentsBySpeciesgroupId(id));
    } 
    private RelayCommand _pdfTbl68SpeciesgroupsCommand;
    public ICommand PdfTbl68SpeciesgroupsCommand
    {
        get { return _pdfTbl68SpeciesgroupsCommand ?? (_pdfTbl68SpeciesgroupsCommand = new RelayCommand(delegate { CreatePdfTbl68Speciesgroups(_mainId); })); }
    }

    private static void CreatePdfTbl68Speciesgroups(int id)
    {
        ReportTbl68SpeciesgroupsPdf.CreateMainPdf(id);
    }
    //------------------------------------------------------------------------------
    //------------------------------------------------------------------------------  		   
           
        IList<Tbl68Speciesgroup> ListTbl68SpeciesgroupsBySpeciesgroupId(int speciesgroupId);

        IList<Tbl69FiSpecies> ListTbl69FiSpeciessesBySpeciesgroupIdAndHash(int speciesgroupId);

        IList<Tbl90Reference> ListTbl90AuthorsBySpeciesgroupId(int speciesgroupId);
        IList<Tbl90Reference> ListTbl90SourcesBySpeciesgroupId(int speciesgroupId);
        IList<Tbl90Reference> ListTbl90ExpertsBySpeciesgroupId(int speciesgroupId);

        IList<Tbl93Comment> ListTbl93CommentsBySpeciesgroupId(int speciesgroupId);	   
           
		public IList<Tbl68Speciesgroup> ListTbl68SpeciesgroupsBySpeciesgroupId(int speciesgroupId)
		{
			return _tbl68SpeciesgroupsRepository.ListWhereOrderByInclude(
				e => e.SpeciesgroupID == speciesgroupId,
				_tbl68SpeciesgroupsRepository.OrderBy(r => r.SpeciesgroupName + r.Subregnum),
				p => p.Tbl69FiSpeciesses, k => k.Tbl72PlSpeciesses);
		}

		public IList<Tbl69FiSpecies> ListTbl69FiSpeciessesBySpeciesgroupIdAndHash(int speciesgroupId)
		{
			return _tbl69FiSpeciessesRepository.ListWhereOrderByInclude(
				e => e.SpeciesgroupID == speciesgroupId &&
				e.FiSpeciesName.Contains("#") == false,
				_tbl69FiSpeciessesRepository.OrderBy(r => r.FiSpeciesName),
				p => p.NULL, k => k.Tbl68Speciesgroups);
		}

	               //------------------------------------------------

		public IList<Tbl90Reference> ListTbl90AuthorsBySpeciesgroupId(int speciesgroupId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefAuthorID == e.Tbl90RefAuthors.RefAuthorID && e.SpeciesgroupID == speciesgroupId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefAuthors.RefAuthorName + r.Tbl90RefAuthors.ArticelTitle + r.Tbl90RefAuthors.BookName + r.Tbl90RefAuthors.Page1 + r.Tbl90RefAuthors.Publisher),
				p => p.Tbl90RefAuthors);
		}

		public IList<Tbl90Reference> ListTbl90SourcesBySpeciesgroupId(int speciesgroupId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefSourceID == e.Tbl90RefSources.RefSourceID && e.SpeciesgroupID == speciesgroupId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefSources.RefSourceName + r.Tbl90RefSources.SourceYear),
				p => p.Tbl90RefSources);
		}

		public IList<Tbl90Reference> ListTbl90ExpertsBySpeciesgroupId(int speciesgroupId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefExpertID == e.Tbl90RefExperts.RefExpertID && e.SpeciesgroupID == speciesgroupId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefExperts.RefExpertName),
				p => p.Tbl90RefExperts);
		}

		public IList<Tbl90Reference> ListTbl90RefAuthorsBySpeciesgroupId(int speciesgroupId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefAuthorID == e.Tbl90RefAuthors.RefAuthorID && e.SpeciesgroupID == speciesgroupId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefAuthors.RefAuthorName + r.Tbl90RefAuthors.ArticelTitle + r.Tbl90RefAuthors.BookName + r.Tbl90RefAuthors.Page1 + r.Tbl90RefAuthors.Publisher),
				p => p.Tbl90RefAuthors);
		}

		public IList<Tbl90Reference> ListTbl90RefSourcesBySpeciesgroupId(int speciesgroupId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefSourceID == e.Tbl90RefSources.RefSourceID && e.SpeciesgroupID == speciesgroupId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefSources.RefSourceName + r.Tbl90RefSources.SourceYear),
				p => p.Tbl90RefSources);
		}

		public IList<Tbl90Reference> ListTbl90RefExpertsBySpeciesgroupId(int speciesgroupId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefExpertID == e.Tbl90RefExperts.RefExpertID && e.SpeciesgroupID == speciesgroupId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefExperts.RefExpertName),
				p => p.Tbl90RefExperts);
		}

	               //------------------------------------------------
 
		public IList<Tbl93Comment> ListTbl93CommentsBySpeciesgroupId(int speciesgroupId)
		{
			return _tbl93CommentsRepository.ListWhereOrderByInclude(
				e => e.SpeciesgroupID == speciesgroupId,
				_tbl93CommentsRepository.OrderBy(r => r.Info),
				p => p.Tbl69FiSpeciesses);
		}      
  

         
}   

