using System;
using System.Data.Linq.Mapping;
using System.Web.Mvc;
using Atis.Domain.Helpers;      
using System.ComponentModel.DataAnnotations;    

// <!-- Entities Skriptdatum:  01.02.2012  10:32    -->  

namespace Atis.Domain.Models     {  
    [MetadataType(typeof(aspnet_UserValidation))]
    public partial class aspnet_User    {   
   

    }     

    public class aspnet_UserValidation    {               
      
        [HiddenInput(DisplayValue = false)]   
        [Column(IsPrimaryKey = true, IsDbGenerated = true, AutoSync = AutoSync.OnInsert)]   
        public Guid UserId { get; set; }     
      
        [Required(ErrorMessageResourceName = "RequiredUserName", ErrorMessageResourceType = typeof(SharedRes.StringsRes))]
        [LocalizedDisplayName("Nameaspnet_User", NameResourceType = typeof(SharedRes.StringsRes))]
        [StringLength(200, ErrorMessageResourceName = "StringLength_200", ErrorMessageResourceType = typeof(SharedRes.StringsRes))]
        [Column]
        public string UserName { get; set; }   
  
        [StringLength(200, ErrorMessageResourceName = "StringLength_200", ErrorMessageResourceType = typeof(SharedRes.StringsRes))]
        [Column]
        public string LoweredUserName { get; set; }   
                                                                                                                                                                                                                                                                                       
        [Column]
        public string MobileAlias { get; set; }    

        [Column]
        public DateTime LastActivityDate { get; set; }     
          
        [Column]
        public bool IsAnonymous { get; set; }                                                                                                                                                                                                                                                                               
        
        [Column]
        public Guid ApplicationId { get; set; }          
     
    }
}   


