using ATIS.Dal.Models;
using ATIS.Ui.Core;
using ATIS.Ui.Helper;
using System.Collections.ObjectModel;

namespace ATIS.Ui.Views.Database.CrudHelper
{
    public class BasicDelete : ViewModelBase
    {
        private readonly UnitOfWork _uow = new UnitOfWork(new AtisDbContext());

        //------------------------------ Regnum   --------------------------------------------------------------------------------------------
        public void DeleteRegnum(Tbl03Regnum selected)
        {
            _uow.Tbl03Regnums.Remove(selected);
            _uow.Complete();
        }
        public ObservableCollection<Tbl06Phylum> SearchForConnectedDatasetsWithRegnumIdInTablePhylum(Tbl03Regnum selected)
        {
            var collection = new ObservableCollection<Tbl06Phylum>(_uow.Tbl06Phylums.Find(x => x.RegnumId == selected.RegnumId));
            return collection;
        }
        public ObservableCollection<Tbl09Division> SearchForConnectedDatasetsWithRegnumIdInTableDivision(Tbl03Regnum selected)
        {
            var collection = new ObservableCollection<Tbl09Division>(_uow.Tbl09Divisions.Find(x => x.RegnumId == selected.RegnumId));
            return collection;
        }
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

        //------------------------------ Phylum   --------------------------------------------------------------------------------------------
        public void DeletePhylum(Tbl06Phylum selected)
        {
            _uow.Tbl06Phylums.Remove(selected);
            _uow.Complete();
        }
        public ObservableCollection<Tbl12Subphylum> SearchForConnectedDatasetsWithPhylumIdInTableSubphylum(Tbl06Phylum selected)
        {
            var collection = new ObservableCollection<Tbl12Subphylum>(_uow.Tbl12Subphylums.Find(x => x.PhylumId == selected.PhylumId));
            return collection;
        }
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
        //------------------------------ Division   --------------------------------------------------------------------------------------------
        public void DeleteDivision(Tbl09Division selected)
        {
            _uow.Tbl09Divisions.Remove(selected);
            _uow.Complete();
        }
        public ObservableCollection<Tbl15Subdivision> SearchForConnectedDatasetsWithDivisionIdInTableSubdivision(Tbl09Division selected)
        {
            var collection = new ObservableCollection<Tbl15Subdivision>(_uow.Tbl15Subdivisions.Find(x => x.DivisionId == selected.DivisionId));
            return collection;
        }
        public ObservableCollection<Tbl90Reference> DeleteDatasetsWithDivisionIdInTableReference(Tbl09Division selected)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_uow.Tbl90References.Find(x => x.DivisionId == selected.DivisionId));
            return collection;
        }
        public ObservableCollection<Tbl93Comment> DeleteDatasetsWithDivisionIdInTableComment(Tbl09Division selected)
        {
            var collection = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments.Find(x => x.DivisionId == selected.DivisionId));
            return collection;
        }



        //------------------------------ Subphylum   --------------------------------------------------------------------------------------------
        public void DeleteSubphylum(Tbl12Subphylum selected)
        {
            _uow.Tbl12Subphylums.Remove(selected);
            _uow.Complete();
        }
        public ObservableCollection<Tbl18Superclass> SearchForConnectedDatasetsWithSubphylumIdInTableSuperclass(Tbl12Subphylum selected)
        {
            var collection = new ObservableCollection<Tbl18Superclass>(_uow.Tbl18Superclasses.Find(x => x.SubphylumId == selected.SubphylumId));
            return collection;
        }
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

        //--------------------------------Subdivision------------------------------
        public void DeleteSubdivision(Tbl15Subdivision selected)
        {
            _uow.Tbl15Subdivisions.Remove(selected);
            _uow.Complete();
        }
        public ObservableCollection<Tbl18Superclass> SearchForConnectedDatasetsWithSubdivisionIdInTableSuperclass(Tbl15Subdivision selected)
        {
            var collection = new ObservableCollection<Tbl18Superclass>(_uow.Tbl18Superclasses.Find(x => x.SubdivisionId == selected.SubdivisionId));
            return collection;
        }
        public ObservableCollection<Tbl90Reference> DeleteDatasetsWithSubdivisionIdInTableReference(Tbl15Subdivision selected)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_uow.Tbl90References.Find(x => x.SubdivisionId == selected.SubdivisionId));
            return collection;
        }
        public ObservableCollection<Tbl93Comment> DeleteDatasetsWithSubdivisionIdInTableComment(Tbl15Subdivision selected)
        {
            var collection = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments.Find(x => x.SubdivisionId == selected.SubdivisionId));
            return collection;
        }

        //----------------------------- Superclass------------------------------  
        public void DeleteSuperclass(Tbl18Superclass selected)
        {
            _uow.Tbl18Superclasses.Remove(selected);
            _uow.Complete();
        } 
        public ObservableCollection<Tbl21Class> SearchForConnectedDatasetsWithSuperclassIdInTableClass(Tbl18Superclass selected)
        {
            var collection = new ObservableCollection<Tbl21Class>(_uow.Tbl21Classes.Find(x => x.SuperclassId == selected.SuperclassId));
            return collection;
        }
        public ObservableCollection<Tbl90Reference> DeleteDatasetsWithSuperclassIdInTableReference(Tbl18Superclass selected)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_uow.Tbl90References.Find(x => x.SuperclassId == selected.SuperclassId));
            return collection;
        }
        public ObservableCollection<Tbl93Comment> DeleteDatasetsWithSuperclassIdInTableComment(Tbl18Superclass selected)
        {
            var collection = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments.Find(x => x.SuperclassId == selected.SuperclassId));
            return collection;
        }


        //-----------------------------Class-----------------------------------------
        public void DeleteClass(Tbl21Class selected)
        {
            _uow.Tbl21Classes.Remove(selected);
            _uow.Complete();
        }
        public ObservableCollection<Tbl24Subclass> SearchForConnectedDatasetsWithClassIdInTableSubclass(Tbl21Class selected)
        {
            var collection = new ObservableCollection<Tbl24Subclass>(_uow.Tbl24Subclasses.Find(x => x.ClassId == selected.ClassId));
            return collection;
        }
        public ObservableCollection<Tbl90Reference> DeleteDatasetsWithClassIdInTableReference(Tbl21Class selected)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_uow.Tbl90References.Find(x => x.ClassId == selected.ClassId));
            return collection;
        }
        public ObservableCollection<Tbl93Comment> DeleteDatasetsWithClassIdInTableComment(Tbl21Class selected)
        {
            var collection = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments.Find(x => x.ClassId == selected.ClassId));
            return collection;
        }

        //------------------------------ Subclass -----------------------------------
        public void DeleteSubclass(Tbl24Subclass selected)
        {
            _uow.Tbl24Subclasses.Remove(selected);
            _uow.Complete();
        }

        public ObservableCollection<Tbl27Infraclass> SearchForConnectedDatasetsWithSubclassIdInTableInfraclass(Tbl24Subclass selected)
        {
            var collection = new ObservableCollection<Tbl27Infraclass>(_uow.Tbl27Infraclasses.Find(x => x.SubclassId == selected.SubclassId));
            return collection;
        }
        public ObservableCollection<Tbl90Reference> DeleteDatasetsWithSubclassIdInTableReference(Tbl24Subclass selected)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_uow.Tbl90References.Find(x => x.SubclassId == selected.SubclassId));
            return collection;
        }
        public ObservableCollection<Tbl93Comment> DeleteDatasetsWithSubclassIdInTableComment(Tbl24Subclass selected)
        {
            var collection = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments.Find(x => x.SubclassId == selected.SubclassId));
            return collection;
        }

        //------------------------------Delete Infraclass -----------------------------------
        public void DeleteInfraclass(Tbl27Infraclass selected)
        {
            _uow.Tbl27Infraclasses.Remove(selected);
            _uow.Complete();
        }
        public ObservableCollection<Tbl30Legio> SearchForConnectedDatasetsWithInfraclassIdInTableLegio(Tbl27Infraclass selected)
        {
            var collection = new ObservableCollection<Tbl30Legio>(_uow.Tbl30Legios.Find(x => x.InfraclassId == selected.InfraclassId));
            return collection;
        }

        public ObservableCollection<Tbl90Reference> DeleteDatasetsWithInfraclassIdInTableReference(Tbl27Infraclass selected)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_uow.Tbl90References.Find(x => x.InfraclassId == selected.InfraclassId));
            return collection;
        }
        public ObservableCollection<Tbl93Comment> DeleteDatasetsWithInfraclassIdInTableComment(Tbl27Infraclass selected)
        {
            var collection = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments.Find(x => x.InfraclassId == selected.InfraclassId));
            return collection;
        }
 
        //--------------------------------Delete Legio---------------------------------------
        public void DeleteLegio(Tbl30Legio selected)
        {
            _uow.Tbl30Legios.Remove(selected);
            _uow.Complete();
        }
        public ObservableCollection<Tbl33Ordo> SearchForConnectedDatasetsWithLegioIdInTableOrdo(Tbl30Legio selected)
        {
            var collection = new ObservableCollection<Tbl33Ordo>(_uow.Tbl33Ordos.Find(x => x.OrdoId == selected.LegioId));
            return collection;
        }
        public ObservableCollection<Tbl90Reference> DeleteDatasetsWithLegioIdInTableReference(Tbl30Legio selected)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_uow.Tbl90References.Find(x => x.LegioId == selected.LegioId));
            return collection;
        }
        public ObservableCollection<Tbl93Comment> DeleteDatasetsWithLegioIdInTableComment(Tbl30Legio selected)
        {
            var collection = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments.Find(x => x.LegioId == selected.LegioId));
            return collection;
        }

        //----------------------------------------------------------------------------------------
        //-----------------------DeleteReferences, DeleteComments, DeleteReference, DeleteComment-----------------------------------

        public void DeleteReferences(ObservableCollection<Tbl90Reference> coll)
        {
            foreach (var t in coll)
                _uow.Tbl90References.Remove(t);
            _uow.Complete();
        }
        public void DeleteComments(ObservableCollection<Tbl93Comment> coll)
        {
            foreach (var t in coll)
                _uow.Tbl93Comments.Remove(t);
            _uow.Complete();
        }
        public void DeleteReference(Tbl90Reference selected)
        {
            _uow.Tbl90References.Remove(selected);
            _uow.Complete();
        }
        public void DeleteComment(Tbl93Comment selected)
        {
            _uow.Tbl93Comments.Remove(selected);
            _uow.Complete();
        }
        //--------------------------------------------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------------------------------------------
    }
}
