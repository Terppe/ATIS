using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ATIS.DAL.Models
{
    public class Tbl90Reference
    {

        [Key]
        public int ReferenceId { get; set; }
        public int? FiSpeciesId { get; set; }
        public int? PlSpeciesId { get; set; }
        public int? GenusId { get; set; }
        public int? InfratribusID { get; set; }
        public int? SubtribusID { get; set; }
        public Nullable<int> TribusID { get; set; }
        public Nullable<int> SupertribusID { get; set; }
        public Nullable<int> InfrafamilyID { get; set; }
        public Nullable<int> SubfamilyID { get; set; }
        public Nullable<int> FamilyID { get; set; }
        public Nullable<int> SuperfamilyID { get; set; }
        public Nullable<int> InfraordoID { get; set; }
        public Nullable<int> SubordoID { get; set; }
        public Nullable<int> OrdoID { get; set; }
        public Nullable<int> LegioID { get; set; }
        public Nullable<int> InfraclassID { get; set; }
        public int? SubclassId { get; set; }
        public int? ClassId { get; set; }
        public int? SuperclassId { get; set; }
        public int? SubdivisionId { get; set; }
        public int? SubphylumId { get; set; }
        public int? DivisionId { get; set; }
        public int? PhylumId { get; set; }
        public int? RegnumId { get; set; }
        public int? RefExpertId { get; set; }
        public int? RefSourceId { get; set; }
        public int? RefAuthorId { get; set; }
        public int CountId { get; set; }
        public bool? Valid { get; set; }
        public string ValidYear { get; set; }
        public string Info { get; set; }
        public string Writer { get; set; }
        public System.DateTime WriterDate { get; set; }
        public string Updater { get; set; }
        public System.DateTime UpdaterDate { get; set; }
        public string Memo { get; set; }
        //     public byte[] RowVersion { get; set; }

        //public virtual Tbl03Regnum Tbl03Regnums { get; set; }
        //public virtual Tbl06Phylum Tbl06Phylums { get; set; }
        //public virtual Tbl09Division Tbl09Divisions { get; set; }
        //public virtual Tbl12Subphylum Tbl12Subphylums { get; set; }
        //public virtual Tbl15Subdivision Tbl15Subdivisions { get; set; }
        //    public virtual Tbl90RefAuthor Tbl90RefAuthors { get; set; }
        //public virtual Tbl90RefExpert Tbl90RefExperts { get; set; }
        //public virtual Tbl90RefSource Tbl90RefSources { get; set; }


    }
}
