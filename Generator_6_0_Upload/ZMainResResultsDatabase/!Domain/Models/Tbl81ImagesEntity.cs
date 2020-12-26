using System;
using System.Data.Linq.Mapping;
using System.Web.Mvc;
using Atis.Domain.Helpers;      
using System.ComponentModel.DataAnnotations;    

// <!-- Entities Skriptdatum:  28.12.2011  10:32    -->  

namespace Atis.Domain.Models     {  
    [MetadataType(typeof(Tbl81ImageValidation))]
    public partial class Tbl81Image    {   
   

    }     

    public class Tbl81ImageValidation    {               
  
        [HiddenInput(DisplayValue = false)]   
        [Column(IsPrimaryKey = true, IsDbGenerated = true, AutoSync = AutoSync.OnInsert)]   
        public int ImageID { get; set; }   
  
        [Column]
        public byte[] ImageData { get; set; }                 

        [ScaffoldColumn(false)]
        [Column]
        public string ImageMimeType { get; set; }                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                
  
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
        public DateTime ShotDate { get; set; }                                                                                                                                                                                                                                                                                          
        [Column]
        public Guid FilestreamID { get; set; }                                                                                                                                                                                                                                                                                          
        [Column]
        public byte[] Filestream { get; set; }                                                                                                                                                                                                                                                                                           
 
    }
}   


