using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ATIS.WinUi.Models;

public class Tbl81Image
{
    [Key]

    public int ImageId { get; set; }
    public int FiSpeciesId { get; set; }
    public int PlSpeciesId { get; set; }
    public int CountId { get; set; }
    public bool? Valid { get; set; }
    public string? ValidYear { get; set; }
    public DateTime? ShotDate { get; set; }
    public string? Info { get; set; }
    public string? Writer { get; set; }
    public DateTime WriterDate { get; set; }
    public string? Updater { get; set; }
    public DateTime UpdaterDate { get; set; }
    public string? Memo { get; set; }
    public byte[]? ImageData { get; set; }
    public string? ImageMimeType { get; set; }
    public byte[]? Filestream { get; set; }
    public Guid FilestreamId { get; set; }
    //     public byte[] RowVersion { get; set; }

    [ForeignKey("FiSpeciesId")]
    public virtual Tbl69FiSpecies? Tbl69FiSpeciesses { get; set; }

    [ForeignKey("PlSpeciesId")]
    public virtual Tbl72PlSpecies? Tbl72PlSpeciesses { get; set; }


}
