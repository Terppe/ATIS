using System.ComponentModel.DataAnnotations;

namespace ATIS.WinUi.Models;

public class Tbl21Class
{
    [Key]
    public int ClassId { get; set; }
    [Required(ErrorMessage = "Name is Required")]
    [MinLength(2, ErrorMessage = "Name should be longer than one character")]
    [MaxLength(100, ErrorMessage = "Name should not be longer than 100 characters")]
    public string? ClassName { get; set; }
    [Required(ErrorMessage = "SuperclassId is Required")]
    public int SuperclassId { get; set; }
    public int CountId { get; set; }
    public bool? Valid { get; set; }
    [MaxLength(4, ErrorMessage = "Valid Year should not be longer than 4 characters")]
    public string? ValidYear { get; set; }
    [MaxLength(100, ErrorMessage = "Synonym should not be longer than 100 characters")]
    public string? Synonym { get; set; }
    [MaxLength(60, ErrorMessage = "Author should not be longer than 60 characters")]
    public string? Author { get; set; }
    [MaxLength(4, ErrorMessage = "Author Year should not be longer than 4 characters")]
    public string? AuthorYear { get; set; }
    [MaxLength(100, ErrorMessage = "Info should not be longer than 100 characters")]
    public string? Info { get; set; }
    [MaxLength(200, ErrorMessage = "English Name should not be longer than 200 characters")]
    public string? EngName { get; set; }
    [MaxLength(200, ErrorMessage = "German Name should not be longer than 200 characters")]
    public string? GerName { get; set; }
    [MaxLength(200, ErrorMessage = "French Name should not be longer than 200 characters")]
    public string? FraName { get; set; }
    [MaxLength(200, ErrorMessage = "Spanish Name should not be longer than 200 characters")]
    public string? PorName { get; set; }
    public string? Writer { get; set; }
    public DateTime WriterDate { get; set; }
    public string? Updater { get; set; }
    public DateTime UpdaterDate { get; set; }
    public string? Memo { get; set; }
    //  public byte[] RowVersion { get; set; }


}
