using System.Linq;
using Atis.WpfUi.Model;      
  
using System;      
   
// <!-- Interface Skriptdatum:  24.03.2012  10:32     -->  

namespace Atis.WpfUi.Interfaces     {         
    public interface IAspnetMembershipsRepository    {
        IQueryable<AspnetMembership> AspnetMemberships { get; }                   
        IQueryable<AspnetMembership> FindAll();
        IQueryable<AspnetMembership> FindAllSort();          
    
AspnetMembership Get(Guid id);    
        

        void Add(AspnetMembership aspnetMembership);
        void Delete(AspnetMembership aspnetMembership);
        void Save( );               
     }
}   

