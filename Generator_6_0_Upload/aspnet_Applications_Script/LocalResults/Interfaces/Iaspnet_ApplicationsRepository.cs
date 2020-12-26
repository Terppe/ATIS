using System.Linq;
using Atis.Domain.Models;      
  
using System;      
   
// <!-- Interface Skriptdatum:  16.03.2012  10:32     -->  

namespace Atis.Domain.Interfaces     {         
    public interface IAspnetApplicationsRepository    {
        IQueryable<AspnetApplication> AspnetApplications { get; }                   
        IQueryable<AspnetApplication> FindAll();
        IQueryable<AspnetApplication> FindAllSort();          
    
AspnetApplication Get(Guid id);    
        

        void Add(AspnetApplication aspnetApplication);
        void Delete(AspnetApplication aspnetApplication);
        void Save( );               
     }
}   

