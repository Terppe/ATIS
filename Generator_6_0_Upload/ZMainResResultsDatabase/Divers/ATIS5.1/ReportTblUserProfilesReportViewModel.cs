using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Te.Atis.BusinessLayer;
using Te.Atis.DomainModel;
using Te.Atis.Ui.Desktop.Views.Report.PDF;    

// <!-- Interface Skriptdatum:   26.02.2019  10:32     -->  

namespace Te.Atis.Ui.Desktop.Views.Report   
{       

           
     public void GetTblUserProfilesById(int id)
    {
        TblUserProfilesList = new ObservableCollection<TblUserProfile>(_businessLayer.ListTblUserProfilesByUserProfileId(id));		   
           
        //------------------------------------------------------------------------------
        Tbl90RefExpertsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90RefExpertsByUserProfileId(id));

        Tbl90RefSourcesList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90RefSourcesByUserProfileId(id));

        Tbl90RefAuthorsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90RefAuthorsByUserProfileId(id));

        Tbl90ExpertsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ExpertsByUserProfileId(id));

        Tbl90SourcesList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90SourcesByUserProfileId(id));

        Tbl90AuthorsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90AuthorsByUserProfileId(id));

        Tbl93CommentsList = new ObservableCollection<Tbl93Comment>(_businessLayer.ListTbl93CommentsByUserProfileId(id));
    } 
    private RelayCommand _pdfTblUserProfilesCommand;
    public ICommand PdfTblUserProfilesCommand
    {
        get { return _pdfTblUserProfilesCommand ?? (_pdfTblUserProfilesCommand = new RelayCommand(delegate { CreatePdfTblUserProfiles(_mainId); })); }
    }

    private static void CreatePdfTblUserProfiles(int id)
    {
        ReportTblUserProfilesPdf.CreateMainPdf(id);
    }
    //------------------------------------------------------------------------------
    //------------------------------------------------------------------------------  		   
           
        IList<TblUserProfile> ListTblUserProfilesByUserProfileId(int userprofileId);

        IList<NULL> ListNULLByUserProfileIdAndHash(int userprofileId);

        IList<Tbl90Reference> ListTbl90AuthorsByUserProfileId(int userprofileId);
        IList<Tbl90Reference> ListTbl90SourcesByUserProfileId(int userprofileId);
        IList<Tbl90Reference> ListTbl90ExpertsByUserProfileId(int userprofileId);

        IList<Tbl93Comment> ListTbl93CommentsByUserProfileId(int userprofileId);	   
           
		public IList<TblUserProfile> ListTblUserProfilesByUserProfileId(int userprofileId)
		{
			return _tblUserProfilesRepository.ListWhereOrderByInclude(
				e => e.UserProfileID == userprofileId,
				_tblUserProfilesRepository.OrderBy(r => r.LastName + r.Subregnum),
				p => p.NULL, k => k.NULL);
		}

		public IList<NULL> ListNULLByUserProfileIdAndHash(int userprofileId)
		{
			return NULLRepository.ListWhereOrderByInclude(
				e => e.UserProfileID == userprofileId &&
				e.NULL.Contains("#") == false,
				NULLRepository.OrderBy(r => r.NULL),
				p => p.NULL, k => k.TblUserProfiles);
		}

	               //------------------------------------------------

		public IList<Tbl90Reference> ListTbl90AuthorsByUserProfileId(int userprofileId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefAuthorID == e.Tbl90RefAuthors.RefAuthorID && e.UserProfileID == userprofileId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefAuthors.RefAuthorName + r.Tbl90RefAuthors.ArticelTitle + r.Tbl90RefAuthors.BookName + r.Tbl90RefAuthors.Page1 + r.Tbl90RefAuthors.Publisher),
				p => p.Tbl90RefAuthors);
		}

		public IList<Tbl90Reference> ListTbl90SourcesByUserProfileId(int userprofileId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefSourceID == e.Tbl90RefSources.RefSourceID && e.UserProfileID == userprofileId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefSources.RefSourceName + r.Tbl90RefSources.SourceYear),
				p => p.Tbl90RefSources);
		}

		public IList<Tbl90Reference> ListTbl90ExpertsByUserProfileId(int userprofileId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefExpertID == e.Tbl90RefExperts.RefExpertID && e.UserProfileID == userprofileId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefExperts.RefExpertName),
				p => p.Tbl90RefExperts);
		}

		public IList<Tbl90Reference> ListTbl90RefAuthorsByUserProfileId(int userprofileId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefAuthorID == e.Tbl90RefAuthors.RefAuthorID && e.UserProfileID == userprofileId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefAuthors.RefAuthorName + r.Tbl90RefAuthors.ArticelTitle + r.Tbl90RefAuthors.BookName + r.Tbl90RefAuthors.Page1 + r.Tbl90RefAuthors.Publisher),
				p => p.Tbl90RefAuthors);
		}

		public IList<Tbl90Reference> ListTbl90RefSourcesByUserProfileId(int userprofileId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefSourceID == e.Tbl90RefSources.RefSourceID && e.UserProfileID == userprofileId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefSources.RefSourceName + r.Tbl90RefSources.SourceYear),
				p => p.Tbl90RefSources);
		}

		public IList<Tbl90Reference> ListTbl90RefExpertsByUserProfileId(int userprofileId)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefExpertID == e.Tbl90RefExperts.RefExpertID && e.UserProfileID == userprofileId,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefExperts.RefExpertName),
				p => p.Tbl90RefExperts);
		}

	               //------------------------------------------------
 
		public IList<Tbl93Comment> ListTbl93CommentsByUserProfileId(int userprofileId)
		{
			return _tbl93CommentsRepository.ListWhereOrderByInclude(
				e => e.UserProfileID == userprofileId,
				_tbl93CommentsRepository.OrderBy(r => r.Info),
				p => p.NULL);
		}      
  

         
}   

