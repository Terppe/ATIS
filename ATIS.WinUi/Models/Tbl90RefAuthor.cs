using System.ComponentModel.DataAnnotations;

namespace ATIS.WinUi.Models;

public class Tbl90RefAuthor
{

    [Key]
    public int RefAuthorId { get; set; }
    public int CountId { get; set; }
    public bool? Valid { get; set; }
    public string? ValidYear { get; set; }
    public string? RefAuthorName { get; set; }
    public string? PublicationYear { get; set; }
    public string? ArticelTitle { get; set; }
    public string? BookName { get; set; }
    public string? Page { get; set; }
    public string? Page1 { get; set; }

    public string FullName => $"{RefAuthorName} {BookName} {Page1}";

    public string? Publisher { get; set; }
    public string? PublicationPlace { get; set; }
    public string? ISBN { get; set; }
    public string? Notes { get; set; }
    public string? Info { get; set; }
    public string? Writer { get; set; }
    public System.DateTime WriterDate { get; set; }
    public string? Updater { get; set; }
    public System.DateTime UpdaterDate { get; set; }
    public string? Memo { get; set; }
    //    public byte[] RowVersion { get; set; }

    //   public virtual ICollection<Tbl90Reference> Tbl90References { get; set; }

    //    public virtual Tbl90Reference Tbl90Reference { get; set; }


}
