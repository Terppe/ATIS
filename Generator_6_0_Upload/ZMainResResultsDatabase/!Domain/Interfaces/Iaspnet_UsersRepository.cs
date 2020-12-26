using System.Linq;
using Atis.Domain.Models;      
   
// <!-- Interface Skriptdatum:  01.02.2012  10:32     -->  

namespace Atis.Domain.Interfaces     {         
    public interface Iaspnet_UsersRepository    {
        IQueryable<aspnet_User> aspnet_Users { get; }                   
        IQueryable<aspnet_User> FindAll();
        IQueryable<aspnet_User> FindAllSort();          
    
aspnet_User Get(int id);   
        

        void Add(aspnet_User aspnet_user);
        void Delete(aspnet_User aspnet_user);
        void Save( );               
     }
}   

