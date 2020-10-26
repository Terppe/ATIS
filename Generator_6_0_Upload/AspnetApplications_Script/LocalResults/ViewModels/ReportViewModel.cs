using System;
using System.Collections.Generic;
using Atis.Domain.Models;

// <!-- ReportViewModel Skriptdatum:  23.03.2012  10:32    -->  

namespace Atis.Domain.ViewModels.AspnetApplications     {    

    public class ReportViewModel                         {   

         // Data properties  
     
         public IEnumerable<AspnetApplication> AspnetApplications { get; set; }    
  
         public Guid? ApplicationId { get; set; }
         public string ApplicationName { get; set; }   
         public IList<AspnetApplication> AspnetApplicationsSearchResults { get; set; }      
 
    }
}   

