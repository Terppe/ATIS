using System;
using System.Collections.Generic;
using Atis.Domain.Models;

// <!-- ReportViewModel Skriptdatum:  24.03.2012  10:32    -->  

namespace Atis.Domain.ViewModels.AspnetMemberships     {    

    public class ReportViewModel                         {   

         // Data properties  
    
         public IEnumerable<AspnetApplication> AspnetApplications { get; set; }                                   
    
         public IEnumerable<AspnetUser> AspnetUsers { get; set; }                                   
     
         public IEnumerable<AspnetMembership> AspnetMemberships { get; set; }    

        public Guid? ApplicationId { get; set; }
        public string ApplicationName { get; set; }   
         public IList<AspnetApplication> AspnetApplicationsSearchResults { get; set; }  
 
        public Guid? UserId { get; set; }
        public string UserName { get; set; }    
         public IList<AspnetUser> AspnetUsersSearchResults { get; set; }                             
   
         public Guid? UserId { get; set; }
         public string UserName { get; set; }   
         public IList<AspnetMembership> AspnetMembershipsSearchResults { get; set; }      
 
    }
}   

