using System;
using System.Collections.Generic;
using Atis.Domain.Models;

// <!-- ReportViewModel Skriptdatum:  06.02.2012  10:32    -->  

namespace Atis.Domain.ViewModels.aspnet_Memberships     {    

    public class ReportViewModel                         {   
        // Constructor
        public ReportViewModel()     {
            // Define any default values here...
            Valid = false; 
        } 

         // Data properties  
    
         public IEnumerable<aspnet_Application> aspnet_Applications { get; set; }                                   
    
         public IEnumerable<aspnet_User> aspnet_Users { get; set; }                                   
     
         public IEnumerable<aspnet_Membership> aspnet_Memberships { get; set; }    
  
        // Properties        

        public string Author { get; set; }
        public DateTime AuthorYear { get; set; }
        public bool Valid { get; set; }
        public string EngName { get; set; }
        public string GerName { get; set; }
        public string FraName { get; set; }
        public string PorName { get; set; }

        public int? CountID { get; set; }
        public string Synonym { get; set; }  

          public int? ApplicationId { get; set; }
          public string ApplicationName { get; set; }   
                                    
         public IList<aspnet_Application> aspnet_ApplicationsSearchResults { get; set; }  
 
          public int? UserId { get; set; }
          public string UserName { get; set; }    
                                    
         public IList<aspnet_User> aspnet_UsersSearchResults { get; set; }                             
 
        
        public int? UserId { get; set; }
        public string UserName { get; set; }  
                                      
         public IList<aspnet_Membership> aspnet_MembershipsSearchResults { get; set; }  
 
    }
}   

