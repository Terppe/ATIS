using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ATIS.WinUi.Models;

public class Tbl69FiSpecies
{
    [Key]
    public int FiSpeciesId { get; set; }
    public string? FiSpeciesName { get; set; }
    public string? Subspecies { get; set; }
    public string? Divers { get; set; }
    public string FiSpeciesFullName => $"{Tbl66Genusses!.GenusName} {FiSpeciesName} {Subspecies} {Divers}";

    public int GenusId { get; set; }
    public int? SpeciesgroupId { get; set; }
    public int CountId { get; set; }
    public bool? Valid { get; set; }
    public string? ValidYear { get; set; }
    public string? MemoSpecies { get; set; }
    public string? TradeName { get; set; }
    public string? Author { get; set; }
    public string? AuthorYear { get; set; }
    public string? Importer { get; set; }
    public string? ImportingYear { get; set; }
    public bool? TypeSpecies { get; set; }
    public string? LNumber { get; set; }
    public string? LOrigin { get; set; }
    public string? LdaNumber { get; set; }
    public string? LdaOrigin { get; set; }
    public int? BasinLength { get; set; }
    public decimal? FishLength { get; set; }
    public bool? Karnivore { get; set; }
    public bool? Herbivore { get; set; }
    public bool? Limnivore { get; set; }
    public bool? Omnivore { get; set; }
    public string? MemoFoods { get; set; }
    public bool? Difficult1 { get; set; }
    public bool? Difficult2 { get; set; }
    public bool? Difficult3 { get; set; }
    public bool? Difficult4 { get; set; }
    public bool? RegionTop { get; set; }
    public bool? RegionMiddle { get; set; }
    public bool? RegionBottom { get; set; }
    public string? MemoRegion { get; set; }
    public string? MemoTech { get; set; }
    public decimal? Ph1 { get; set; }
    public decimal? Ph2 { get; set; }
    public int? Temp1 { get; set; }
    public int? Temp2 { get; set; }
    public int? Hardness1 { get; set; }
    public int? Hardness2 { get; set; }
    public int? CarboHardness1 { get; set; }
    public int? CarboHardness2 { get; set; }
    public string? Writer { get; set; }
    public DateTime WriterDate { get; set; }
    public string? Updater { get; set; }
    public DateTime UpdaterDate { get; set; }
    public string? MemoHusbandry { get; set; }
    public string? MemoBreeding { get; set; }
    public string? MemoBuilt { get; set; }
    public string? MemoColor { get; set; }
    public string? MemoSozial { get; set; }
    public string? MemoDomorphism { get; set; }
    public string? MemoSpecial { get; set; }
    //     public byte[] RowVersion { get; set; }

    [ForeignKey("GenusId")]
    public virtual Tbl66Genus? Tbl66Genusses { get; set; }
    [ForeignKey("SpeciesgroupId")]
    public virtual Tbl68Speciesgroup? Tbl68Speciesgroups { get; set; }

    //public virtual ICollection<Tbl78Name> Tbl78Names { get; set; }
    //public virtual ICollection<Tbl81Image> Tbl81Images { get; set; }
    //public virtual ICollection<Tbl84Synonym> Tbl84Synonyms { get; set; }
    //public virtual ICollection<Tbl87Geographic> Tbl87Geographics { get; set; }
    //public virtual ICollection<Tbl90Reference> Tbl90References { get; set; }
    //public virtual ICollection<Tbl93Comment> Tbl93Comments { get; set; }

    public void SetSpeciesgroupId(System.Object obj)
    {
        SpeciesgroupId = (int)obj;
    }


}
