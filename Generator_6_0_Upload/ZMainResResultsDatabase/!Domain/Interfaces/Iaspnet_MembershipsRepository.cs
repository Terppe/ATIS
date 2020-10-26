using System.Linq;
using Atis.Domain.Models;      
   
// <!-- Interface Skriptdatum:  06.02.2012  10:32     -->  

namespace Atis.Domain.Interfaces     {         
    public interface Iaspnet_MembershipsRepository    {
        IQueryable<aspnet_Membership> aspnet_Memberships { get; }                   
        IQueryable<aspnet_Membership> FindAll();
        IQueryable<aspnet_Membership> FindAllSort();          
    
aspnet_Membership Get(int id);   
        

        void Add(aspnet_Membership aspnet_membership);
        void Delete(aspnet_Membership aspnet_membership);
        void Save( );               
     }
}   

