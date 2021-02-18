using System.ComponentModel.DataAnnotations;

namespace ATIS.Ui.Models
{
    public class Tbl03Regnum
    {
        [Key]
        public int RegnumId { get; set; }
        public string RegnumName { get; set; }
        public string Subregnum { get; set; }
        public int CountId { get; set; }
        public bool? Valid { get; set; }
        public string ValidYear { get; set; }
        public string Synonym { get; set; }
        public string Author { get; set; }
        public string AuthorYear { get; set; }
        public string Info { get; set; }
        public string EngName { get; set; }
        public string GerName { get; set; }
        public string FraName { get; set; }
        public string PorName { get; set; }
        public string Writer { get; set; }
        public System.DateTime WriterDate { get; set; }
        public string Updater { get; set; }
        public System.DateTime UpdaterDate { get; set; }
        public string Memo { get; set; }
        //   public byte[] RowVersion { get; set; }

        //      public  EntityState EntityState { get; set; }

    }
}
