﻿using System.ComponentModel.DataAnnotations;

namespace ATIS.WinUi.Models;

public class Tbl66Genus
{

    [Key]
    public int GenusId { get; set; }
    public string? GenusName { get; set; }
    public int InfratribusId { get; set; }
    public int CountId { get; set; }
    public bool? Valid { get; set; }
    public string? ValidYear { get; set; }
    public string? Synonym { get; set; }
    public string? Author { get; set; }
    public string? AuthorYear { get; set; }
    public string? Info { get; set; }
    public string? EngName { get; set; }
    public string? GerName { get; set; }
    public string? FraName { get; set; }
    public string? PorName { get; set; }
    public string? Writer { get; set; }
    public DateTime WriterDate { get; set; }
    public string? Updater { get; set; }
    public DateTime UpdaterDate { get; set; }
    public string? Memo { get; set; }
    //    public byte[] RowVersion { get; set; }



}
