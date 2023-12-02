using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ATIS.WinUi.Models;

public class Tbl87Geographic
{
   [Key]

    public int GeographicId { get; set; }
    public string? Address { get; set; }
    public string? Country { get; set; }
    public int FiSpeciesId { get; set; }
    public int PlSpeciesId { get; set; }
    public int CountId { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public double Latitude1 { get; set; }
    public double Longitude1 { get; set; }
    public double Latitude2 { get; set; }
    public double Longitude2 { get; set; }
    public double Latitude3 { get; set; }
    public double Longitude3 { get; set; }
    public double ZoomLevel { get; set; }
    public bool? Valid { get; set; }
    public string? ValidYear { get; set; }
    public string? Author { get; set; }
    public string? AuthorYear { get; set; }
    public string? Info { get; set; }
    public string? Writer { get; set; }
    public DateTime WriterDate { get; set; }
    public string? Updater { get; set; }
    public DateTime UpdaterDate { get; set; }
    public string? Memo { get; set; }
    public string? Continent { get; set; }
    public string? Http { get; set; }
    //     public byte[] RowVersion { get; set; }

    [ForeignKey("FiSpeciesId")]
    public virtual Tbl69FiSpecies? Tbl69FiSpeciesses { get; set; }

    [ForeignKey("PlSpeciesId")]
    public virtual Tbl72PlSpecies? Tbl72PlSpeciesses { get; set; }


}
