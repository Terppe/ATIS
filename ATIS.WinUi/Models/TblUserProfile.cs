using System.ComponentModel.DataAnnotations;

namespace ATIS.WinUi.Models;

public class TblUserProfile
{
    [Key]
    public int UserProfileId { get; set; }
    public string? Email { get; set; }
    public string? LastName { get; set; }
    public string? FirstName { get; set; }
    public string? Title { get; set; }
    public string? Password { get; set; }
    public string? Role { get; set; }
    public bool? Flag { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? Notes { get; set; }
    public string? Colour { get; set; }
    public int CountId { get; set; }
    public DateTime? BirthDate { get; set; }
    public string? Gender { get; set; }
    public string? Country { get; set; }
    public string? Postcode { get; set; }
    public string? City { get; set; }
    public string? Street1 { get; set; }
    public string? Street2 { get; set; }
    public string? Tel { get; set; }
    public string? Mobil { get; set; }
    public string? Fax { get; set; }
    public string? HomePageUrl { get; set; }
    public string? Business { get; set; }
    public string? Company { get; set; }
    public string? Writer { get; set; }
    public DateTime? WriterDate { get; set; }
    public string? Updater { get; set; }
    public DateTime? UpdaterDate { get; set; }
    public string? Memo { get; set; }
    public byte[]? Filestream { get; set; }
    public string? ImageMimeType { get; set; }
    public Guid? FilestreamId { get; set; }
    public string? Signature { get; set; }
    public bool? MailNewsletter { get; set; }
    public bool? MaulHtml { get; set; }
    public string? Known { get; set; }
    //    public byte[] RowVersion { get; set; }
    //   public EntityState EntityState { get; set; }
}
