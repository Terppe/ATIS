using ATIS.Dal.Models;
using ATIS.Ui.Core;
using ATIS.Ui.Helper;
using System.Collections.ObjectModel;

namespace ATIS.Ui.Views.Database.CrudHelper
{
    public class BasicDelete : ViewModelBase
    {
        private readonly UnitOfWork _uow = new UnitOfWork(new AtisDbContext());

        //-------------------------Search with RegnumId---------------------------------
        public ObservableCollection<Tbl06Phylum> SearchForConnectedDatasetsWithRegnumIdInTablePhylum(Tbl03Regnum selected)
        {
            var collection = new ObservableCollection<Tbl06Phylum>(_uow.Tbl06Phylums.Find(x => x.RegnumId == selected.RegnumId));
            return collection;
        }
        //-------------------------Search with RegnumId---------------------------------
        public ObservableCollection<Tbl09Division> SearchForConnectedDatasetsWithRegnumIdInTableDivision(Tbl03Regnum selected)
        {
            var collection = new ObservableCollection<Tbl09Division>(_uow.Tbl09Divisions.Find(x => x.RegnumId == selected.RegnumId));
            return collection;
        }
        //-------------------------Search with PhylumId---------------------------------
        public ObservableCollection<Tbl12Subphylum> SearchForConnectedDatasetsWithPhylumIdInTableSubphylum(Tbl06Phylum selected)
        {
            var collection = new ObservableCollection<Tbl12Subphylum>(_uow.Tbl12Subphylums.Find(x => x.PhylumId == selected.PhylumId));
            return collection;
        }
        //------------------------------Search with SubphylumId----------------------------
        public ObservableCollection<Tbl18Superclass> SearchForConnectedDatasetsWithSubphylumIdInTableSuperclass(Tbl12Subphylum selected)
        {
            var collection = new ObservableCollection<Tbl18Superclass>(_uow.Tbl18Superclasses.Find(x => x.SubphylumId == selected.SubphylumId));
            return collection;
        }
        //-------------------------------------------------------------------------------
        //------------------------------Delete with RegnumId----------------------------

        public ObservableCollection<Tbl90Reference> DeleteDatasetsWithRegnumIdInTableReference(Tbl03Regnum selected)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_uow.Tbl90References.Find(x => x.RegnumId == selected.RegnumId));
            return collection;
        }

        public ObservableCollection<Tbl93Comment> DeleteDatasetsWithRegnumIdInTableComment(Tbl03Regnum selected)
        {
            var collection = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments.Find(x => x.RegnumId == selected.RegnumId));
            return collection;
        }
        //-----------------------------Delete with PhylumId-----------------------------
        public ObservableCollection<Tbl90Reference> DeleteDatasetsWithPhylumIdInTableReference(Tbl06Phylum selected)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_uow.Tbl90References.Find(x => x.PhylumId == selected.PhylumId));
            return collection;
        }
        public ObservableCollection<Tbl93Comment> DeleteDatasetsWithPhylumIdInTableComment(Tbl06Phylum selected)
        {
            var collection = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments.Find(x => x.PhylumId == selected.PhylumId));
            return collection;
        }

        //--------------------------------------Delete with SubphylumId--------------------
        public ObservableCollection<Tbl90Reference> DeleteDatasetsWithSubphylumIdInTableReference(Tbl12Subphylum selected)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_uow.Tbl90References.Find(x => x.SubphylumId == selected.SubphylumId));
            return collection;
        }
        public ObservableCollection<Tbl93Comment> DeleteDatasetsWithSubphylumIdInTableComment(Tbl12Subphylum selected)
        {
            var collection = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments.Find(x => x.SubphylumId == selected.SubphylumId));
            return collection;
        }

        //--------------------------------------------------------------------------------------------
        //--------------------------Area Delete and search with RegnumId------------------------------
        public ObservableCollection<Tbl90Reference> SearchForDatasetWithRegnumIdAndRefAuthorIdAndRefSourceIdInTableReference(Tbl03Regnum selected)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_uow.Tbl90References
                .Find(x => x.RegnumId == selected.RegnumId && x.RefAuthorId == null && x.RefSourceId == null));
            return collection;
        }
        public ObservableCollection<Tbl90Reference> SearchForDatasetWithRegnumIdAndRefAuthorIdAndRefExpertIdInTableReference(Tbl03Regnum selected)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_uow.Tbl90References
                .Find(x => x.RegnumId == selected.RegnumId && x.RefAuthorId == null && x.RefExpertId == null));
            return collection;
        }
        public ObservableCollection<Tbl90Reference> SearchForDatasetWithRegnumIdAndRefSourceIdAndRefExpertIdInTableReference(Tbl03Regnum selected)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_uow.Tbl90References
                .Find(x => x.RegnumId == selected.RegnumId && x.RefSourceId == null && x.RefExpertId == null));
            return collection;
        }
        //----------------------------------------------------------
        public ObservableCollection<Tbl93Comment> SearchForDatasetWithRegnumIdInTableComment(Tbl03Regnum selected)
        {
            var collection = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments
                .Find(x => x.RegnumId == selected.RegnumId));
            return collection;
        }
        //--------------------------------------------------------------------------------------------
        //--------------------------Area Delete and search with PhylumId------------------------------
        public ObservableCollection<Tbl90Reference> SearchForDatasetWithPhylumIdAndRefAuthorIdAndRefSourceIdInTableReference(Tbl06Phylum selected)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_uow.Tbl90References
                .Find(x => x.PhylumId == selected.PhylumId && x.RefAuthorId == null && x.RefSourceId == null));
            return collection;
        }
        public ObservableCollection<Tbl90Reference> SearchForDatasetWithPhylumIdAndRefAuthorIdAndRefExpertIdInTableReference(Tbl06Phylum selected)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_uow.Tbl90References
                .Find(x => x.PhylumId == selected.PhylumId && x.RefAuthorId == null && x.RefExpertId == null));
            return collection;
        }
        public ObservableCollection<Tbl90Reference> SearchForDatasetWithPhylumIdAndRefSourceIdAndRefExpertIdInTableReference(Tbl06Phylum selected)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_uow.Tbl90References
                .Find(x => x.PhylumId == selected.PhylumId && x.RefSourceId == null && x.RefExpertId == null));
            return collection;
        }
        //----------------------------------------------------------
        public ObservableCollection<Tbl93Comment> SearchForDatasetWithPhylumIdInTableComment(Tbl06Phylum selected)
        {
            var collection = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments
                .Find(x => x.PhylumId == selected.PhylumId));
            return collection;
        }
        //--------------------------------------------------------------------------------------------
        //--------------------------Area Delete and search with SubphylumId------------------------------
        public ObservableCollection<Tbl90Reference> SearchForDatasetWithSubphylumIdAndRefAuthorIdAndRefSourceIdInTableReference(Tbl12Subphylum selected)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_uow.Tbl90References
                .Find(x => x.SubphylumId == selected.SubphylumId && x.RefAuthorId == null && x.RefSourceId == null));
            return collection;
        }
        public ObservableCollection<Tbl90Reference> SearchForDatasetWithSubphylumIdAndRefAuthorIdAndRefExpertIdInTableReference(Tbl12Subphylum selected)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_uow.Tbl90References
                .Find(x => x.SubphylumId == selected.SubphylumId && x.RefAuthorId == null && x.RefExpertId == null));
            return collection;
        }
        public ObservableCollection<Tbl90Reference> SearchForDatasetWithSubphylumIdAndRefSourceIdAndRefExpertIdInTableReference(Tbl12Subphylum selected)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_uow.Tbl90References
                .Find(x => x.SubphylumId == selected.SubphylumId && x.RefSourceId == null && x.RefExpertId == null));
            return collection;
        }
        //----------------------------------------------------------
        public ObservableCollection<Tbl93Comment> SearchForDatasetWithSubphylumIdInTableComment(Tbl12Subphylum selected)
        {
            var collection = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments
                .Find(x => x.SubphylumId == selected.SubphylumId));
            return collection;
        }

        //----------------------------------------------------------
        //----------------------------------------------------------

        public void DeleteReferences(ObservableCollection<Tbl90Reference> referencesCollection)
        {
            foreach (var t in referencesCollection)
                _uow.Tbl90References.Remove(t);
            _uow.Complete();
        }
        public void DeleteComments(ObservableCollection<Tbl93Comment> commentsCollection)
        {
            foreach (var t in commentsCollection)
                _uow.Tbl93Comments.Remove(t);
            _uow.Complete();
        }
        //-----------------------------Delete------------------------------
        public void DeleteRegnum(Tbl03Regnum regnum)
        {
            _uow.Tbl03Regnums.Remove(regnum);
            _uow.Complete();
        }
        public void DeletePhylum(Tbl06Phylum phylum)
        {
            _uow.Tbl06Phylums.Remove(phylum);
            _uow.Complete();
        }
        public void DeleteSubphylum(Tbl12Subphylum subphylum)
        {
            _uow.Tbl12Subphylums.Remove(subphylum);
            _uow.Complete();
        }
        public void DeleteReference(Tbl90Reference reference)
        {
            _uow.Tbl90References.Remove(reference);
            _uow.Complete();
        }
        public void DeleteComment(Tbl93Comment comment)
        {
            _uow.Tbl93Comments.Remove(comment);
            _uow.Complete();
        }
    }
}
