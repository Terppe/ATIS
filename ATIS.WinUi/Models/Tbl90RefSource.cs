using System.ComponentModel.DataAnnotations;

namespace ATIS.WinUi.Models;

public class Tbl90RefSource
{
    [Key]
    public int RefSourceId { get; set; }
    public int CountId { get; set; }
    public bool? Valid { get; set; }
    public string? ValidYear { get; set; }
    public string? RefSourceName { get; set; }
    public string? SourceYear { get; set; }

    public string FullName => $"{RefSourceName} {SourceYear}";

    public string? Author { get; set; }
    public string? Notes { get; set; }
    public string? Info { get; set; }
    public string? Writer { get; set; }
    public System.DateTime WriterDate { get; set; }
    public string? Updater { get; set; }
    public System.DateTime UpdaterDate { get; set; }
    public string? Memo { get; set; }
    //   public byte[] RowVersion { get; set; }

    //    public virtual Tbl90Reference Tbl90Reference { get; set; }

}
