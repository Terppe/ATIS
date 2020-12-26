using System;
using System.Data.Linq.Mapping;
using System.Web.Mvc;
using Atis.Domain.Helpers;      
using System.ComponentModel.DataAnnotations;    

// <!-- Entities Skriptdatum:  24.03.2012  10:32    -->  

namespace Atis.Domain.Models     {  
    [MetadataType(typeof(AspnetMembershipValidation))]
    public partial class AspnetMembership    {   
   

    }     

    public class AspnetMembershipValidation    {               
    
        [HiddenInput(DisplayValue = false)]     
        public Guid UserId { get; set; }     
  
        [StringLength(100, ErrorMessageResourceName = "StringLength_100", ErrorMessageResourceType = typeof(SharedRes.StringsRes))]
        [Column]
        public string Password { get; set; }   

        [Column]
        public int PasswordFormat { get; set; } 
  
        [StringLength(100, ErrorMessageResourceName = "StringLength_100", ErrorMessageResourceType = typeof(SharedRes.StringsRes))]
        [Column]
        public string PasswordSalt { get; set; }  
 
        [Column]
        public string MobilePIN { get; set; }   
    
        [StringLength(200, ErrorMessageResourceName = "StringLength_200", ErrorMessageResourceType = typeof(SharedRes.StringsRes))]
        [Column]
        public string Email { get; set; } 
 
        [StringLength(200, ErrorMessageResourceName = "StringLength_200", ErrorMessageResourceType = typeof(SharedRes.StringsRes))]
        [Column]
        public string LoweredEmail { get; set; }  
                                                                                                                                                                                                                                                                                   
        [StringLength(200, ErrorMessageResourceName = "StringLength_200", ErrorMessageResourceType = typeof(SharedRes.StringsRes))]
        [Column]
        public string PasswordQuestion { get; set; }  
                                                                                                                                                                                                                                                                                   
        [StringLength(100, ErrorMessageResourceName = "StringLength_100", ErrorMessageResourceType = typeof(SharedRes.StringsRes))]
        [Column]
        public string PasswordAnswer { get; set; }  

        [Column]
        public bool IsApproved { get; set; }  
                                                                                                                                                                                                                                                                                   
        [Column]
        public bool IsLockedOut { get; set; }  
               
        [Column]
        public DateTime CreateDate { get; set; }       
                                                                                                                                                                                                                                                                    
        [Column]
        public DateTime LastLoginDate { get; set; }       

        [Column]
        public DateTime LastPasswordChangedDate { get; set; }       
       
        [Column]
        public DateTime LastLockoutDate { get; set; }       
     
        [Column]
        public int FailedPasswordAttemptCount { get; set; }   
    
        [Column]
        public DateTime FailedPasswordAttemptWindowStart { get; set; }       
                                                                                                                                                                                                                                                                    
        [Column]
        public int FailedPasswordAnswerAttemptCount { get; set; }   
    
        [Column]
        public DateTime FailedPasswordAnswerAttemptWindowStart { get; set; }       
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               
        [Column]
        public string Comment { get; set; }                                                                                                                                                                                                                                                                                          
        
        [Column]
        public Guid ApplicationId { get; set; }          
        
        [Column]
        public Guid UserId { get; set; }          
     
    }
}   


