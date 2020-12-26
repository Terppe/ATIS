using System;
using System.Data.Linq.Mapping;
using System.Web.Mvc;
using Atis.Domain.Helpers;      
using System.ComponentModel.DataAnnotations;    

// <!-- Entities Skriptdatum:  15.03.2012  10:32    -->  

namespace Atis.Domain.Models     {  
    [MetadataType(typeof(Tbl87GeographicValidation))]
    public partial class Tbl87Geographic    {   
    
        /* public bool IsHostedBy(string userName)   {
     return String.Equals(HostedById ?? HostedBy, userName, StringComparison.Ordinal);
 }

 public bool IsUserRegistered(string userName)   {
     return RSVPs.Any(r => r.AttendeeNameId == userName || (r.AttendeeNameId == null && r.AttendeeName == userName));
 }

 [UIHint("LocationDetail")]
 public LocationDetail Location   {
     get   {
         return new LocationDetail() { Latitude = this.Latitude, Longitude = this.Longitude, Title = this.Title, Address = this.Address };
     }
     set   {
         this.Latitude = value.Latitude;
         this.Longitude = value.Longitude;
         this.Title = value.Title;
         this.Address = value.Address;
     }
 }
 */
                                                                                                                                                                                                                                                                                         
   

    }     

    public class Tbl87GeographicValidation    {               
  
        [HiddenInput(DisplayValue = false)]   
        [Column(IsPrimaryKey = true, IsDbGenerated = true, AutoSync = AutoSync.OnInsert)]   
        public int GeographicID { get; set; }   
  
        [HiddenInput(DisplayValue = false)]
        [Required(ErrorMessageResourceName = "RequiredCountID", ErrorMessageResourceType = typeof(SharedRes.StringsRes))]
        [Column]
        public int CountID { get; set; }   
      
        [Column]
        public bool? Valid { get; set; }    
      
        [StringLength(4, MinimumLength=4, ErrorMessageResourceName = "StringLengthYear_4", ErrorMessageResourceType = typeof(SharedRes.StringsRes))]
        [Column]
        public string ValidYear { get; set; }    
      
        [StringLength(100, ErrorMessageResourceName = "StringLength_100", ErrorMessageResourceType = typeof(SharedRes.StringsRes))]
        [Column]
        public string Author { get; set; }     
      
        [StringLength(4, MinimumLength=4, ErrorMessageResourceName = "StringLengthYear_4", ErrorMessageResourceType = typeof(SharedRes.StringsRes))]
        [Column]
        public string AuthorYear { get; set; }     
      
        [StringLength(100, ErrorMessageResourceName = "StringLength_100", ErrorMessageResourceType = typeof(SharedRes.StringsRes))]
        [Column]
        public string Info { get; set; }     
      
        [HiddenInput(DisplayValue = false)]
        [Column]
        public string Writer { get; set; }     

        [HiddenInput(DisplayValue = false)]
        [Column]
        public DateTime WriterDate { get; set; }     

        [HiddenInput(DisplayValue = false)]
        [Column]
        public string Updater { get; set; }     

        [HiddenInput(DisplayValue = false)]
        [Column]
        public DateTime UpdaterDate { get; set; }       
      
        [DataType(DataType.MultilineText)]
        [Column]
        public string Memo { get; set; }                  
            
        [Column]
        public int FiSpeciesID { get; set; }          
        
        [Column]
        public int PlSpeciesID { get; set; }          
    
        [Column]
        public string Address { get; set; }   

        [Column]
        public string Country { get; set; }   

        [Column]
        public double Latitude { get; set; }   

        [Column]
        public double Longitude { get; set; }   
 
    }
}   


