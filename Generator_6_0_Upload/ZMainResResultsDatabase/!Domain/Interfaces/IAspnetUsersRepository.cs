using System.Linq;
using Atis.Domain.Models;      
  
using System;      
   
// <!-- Interface Skriptdatum:  16.03.2012  10:32     -->  

namespace Atis.Domain.Interfaces     {         
    public interface IAspnetUsersRepository    {
        IQueryable<AspnetUser> AspnetUsers { get; }                   
        IQueryable<AspnetUser> FindAll();
        IQueryable<AspnetUser> FindAllSort();          
    
AspnetUser Get(Guid id);    
        

        void Add(AspnetUser aspnetUser);
        void Delete(AspnetUser aspnetUser);
        void Save( );               
     }
}   

