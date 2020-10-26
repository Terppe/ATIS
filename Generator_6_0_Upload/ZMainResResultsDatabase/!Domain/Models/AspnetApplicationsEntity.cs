using System;
using System.Data.Linq.Mapping;
using System.Web.Mvc;
using Atis.Domain.Helpers;      
using System.ComponentModel.DataAnnotations;    

// <!-- Entities Skriptdatum:  23.03.2012  10:32    -->  

namespace Atis.Domain.Models     {  
    [MetadataType(typeof(AspnetApplicationValidation))]
    public partial class AspnetApplication    {   
   

    }     

    public class AspnetApplicationValidation    {               
     
        [HiddenInput(DisplayValue = false)]   
        [Column(IsPrimaryKey = true, IsDbGenerated = true, AutoSync = AutoSync.OnInsert)]    
        public Guid ApplicationId { get; set; }     
      
        [Required(ErrorMessageResourceName = "RequiredApplicationName", ErrorMessageResourceType = typeof(SharedRes.StringsRes))]
        [LocalizedDisplayName("NameAspnetApplication", NameResourceType = typeof(SharedRes.StringsRes))]
        [StringLength(200, ErrorMessageResourceName = "StringLength_200", ErrorMessageResourceType = typeof(SharedRes.StringsRes))]
        [Column]
        public string ApplicationName { get; set; }   
  
        [StringLength(200, ErrorMessageResourceName = "StringLength_200", ErrorMessageResourceType = typeof(SharedRes.StringsRes))]
        [Column]
        public string LoweredApplicationName { get; set; }   
                                                                                                                                                                                                                                                                                       
        [StringLength(200, ErrorMessageResourceName = "StringLength_200", ErrorMessageResourceType = typeof(SharedRes.StringsRes))]
        [Column]
        public string Description { get; set; }                                                                                                                                                                                                                                                                                          
 
    }
}   


