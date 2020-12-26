using System;
using System.Data.Linq.Mapping;
using System.Web.Mvc;
using Atis.Domain.Helpers;      
using System.ComponentModel.DataAnnotations;    

// <!-- Entities Skriptdatum:  7.1.2012  10:32    -->  

namespace Atis.Domain.Models     {  
    [MetadataType(typeof(Tbl39InfraordoValidation))]
    public partial class Tbl39Infraordo    {   
   

    }     

    public class Tbl39InfraordoValidation    {               
  
        [HiddenInput(DisplayValue = false)]   
        [Column(IsPrimaryKey = true, IsDbGenerated = true, AutoSync = AutoSync.OnInsert)]   
        public int InfraordoID { get; set; }   
      
        [Required(ErrorMessageResourceName = "RequiredInfraordoName", ErrorMessageResourceType = typeof(SharedRes.StringsRes))]
        [LocalizedDisplayName("NameTbl39Infraordo", NameResourceType = typeof(SharedRes.StringsRes))]
        [StringLength(200, ErrorMessageResourceName = "StringLength_200", ErrorMessageResourceType = typeof(SharedRes.StringsRes))]
        [Column]
        public string InfraordoName { get; set; }   
  
        [HiddenInput(DisplayValue = false)]
        [Required(ErrorMessageResourceName = "RequiredCountID", ErrorMessageResourceType = typeof(SharedRes.StringsRes))]
        [Column]
        public int CountID { get; set; }   
      
        [Column]
        public bool? Valid { get; set; }    
      
        [StringLength(4, MinimumLength=4, ErrorMessageResourceName = "StringLengthYear_4", ErrorMessageResourceType = typeof(SharedRes.StringsRes))]
        [Column]
        public string ValidYear { get; set; }    
      
        [DataType(DataType.MultilineText)]
        [Column]
        public string Synonym { get; set; }     
      
        [StringLength(100, ErrorMessageResourceName = "StringLength_100", ErrorMessageResourceType = typeof(SharedRes.StringsRes))]
        [Column]
        public string Author { get; set; }     
      
        [StringLength(4, MinimumLength=4, ErrorMessageResourceName = "StringLengthYear_4", ErrorMessageResourceType = typeof(SharedRes.StringsRes))]
        [Column]
        public string AuthorYear { get; set; }     
      
        [StringLength(100, ErrorMessageResourceName = "StringLength_100", ErrorMessageResourceType = typeof(SharedRes.StringsRes))]
        [Column]
        public string Info { get; set; }     
      
        [StringLength(200, ErrorMessageResourceName = "StringLength_200", ErrorMessageResourceType = typeof(SharedRes.StringsRes))]
        [Column]
        public string EngName { get; set; }     
      
        [StringLength(200, ErrorMessageResourceName = "StringLength_200", ErrorMessageResourceType = typeof(SharedRes.StringsRes))]
        [Column]
        public string GerName { get; set; }     
      
        [StringLength(200, ErrorMessageResourceName = "StringLength_200", ErrorMessageResourceType = typeof(SharedRes.StringsRes))]
        [Column]
        public string FraName { get; set; }     
      
        [StringLength(200, ErrorMessageResourceName = "StringLength_200", ErrorMessageResourceType = typeof(SharedRes.StringsRes))]
        [Column]
        public string PorName { get; set; }     
      
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
        public int SubordoID { get; set; }          
     
    }
}   


