using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ATIS.WinUi.Models;

public class Tbl93Comment
{
    [Key]
    public int CommentId { get; set; }
    public int? FiSpeciesId { get; set; }
    public int? PlSpeciesId { get; set; }
    public int? GenusId { get; set; }
    public int? InfratribusId { get; set; }
    public int? SubtribusId { get; set; }
    public int? TribusId { get; set; }
    public int? SupertribusId { get; set; }
    public int? InfrafamilyId { get; set; }
    public int? SubfamilyId { get; set; }
    public int? FamilyId { get; set; }
    public int? SuperfamilyId { get; set; }
    public int? InfraordoId { get; set; }
    public int? SubordoId { get; set; }
    public int? OrdoId { get; set; }
    public int? LegioId { get; set; }
    public int? InfraclassId { get; set; }
    public int? SubclassId { get; set; }
    public int? ClassId { get; set; }
    public int? SuperclassId { get; set; }
    public int? SubdivisionId { get; set; }
    public int? SubphylumId { get; set; }
    public int? DivisionId { get; set; }
    public int? PhylumId { get; set; }
    public int? RegnumId { get; set; }
    public int CountId { get; set; }
    public bool? Valid { get; set; }
    public string? ValidYear { get; set; }
    public string? Info { get; set; }
    public string? Writer { get; set; }
    public System.DateTime WriterDate { get; set; }
    public string? Updater { get; set; }
    public System.DateTime UpdaterDate { get; set; }
    public string? Memo { get; set; }
    //   public byte[] RowVersion { get; set; }

    [ForeignKey("FiSpeciesId")]
    public virtual Tbl69FiSpecies? Tbl69FiSpeciesses { get; set; }
    [ForeignKey("PlSpeciesId")]
    public virtual Tbl72PlSpecies? Tbl72PlSpeciesses { get; set; }

}
