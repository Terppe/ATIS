using System.Linq;
using Atis.WpfUi.Model;      
  
using System;      
   
// <!-- Interface Skriptdatum:  16.03.2012  10:32     -->  

namespace Atis.WpfUi.Interfaces     {         
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

