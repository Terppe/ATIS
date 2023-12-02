using System.ComponentModel.DataAnnotations;

namespace ATIS.WinUi.Models;


public class Tbl90RefExpert
{
    [Key]
    public int RefExpertId { get; set; }
    public int CountId { get; set; }
    public bool? Valid { get; set; }
    public string? ValidYear { get; set; }
    public string? RefExpertName { get; set; }
    public string? Notes { get; set; }
    public string? Info { get; set; }
    public string? Writer { get; set; }
    public DateTime WriterDate { get; set; }
    public string? Updater { get; set; }
    public DateTime UpdaterDate { get; set; }
    public string? Memo { get; set; }
    //    public byte[] RowVersion { get; set; }

    //    public virtual Tbl90Reference Tbl90Reference { get; set; }

}
