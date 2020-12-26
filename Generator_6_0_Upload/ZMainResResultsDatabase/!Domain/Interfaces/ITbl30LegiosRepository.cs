using System.Linq;
using Atis.Domain.Models;      
   
// <!-- Interface Skriptdatum:  7.1.2012  10:32     -->  

namespace Atis.Domain.Interfaces     {         
    public interface ITbl30LegiosRepository    {
        IQueryable<Tbl30Legio> Tbl30Legios { get; }                   
        IQueryable<Tbl30Legio> FindAll();
        IQueryable<Tbl30Legio> FindAllSort();          
    
Tbl30Legio Get(int id);   
        

        void Add(Tbl30Legio tbl30Legio);
        void Delete(Tbl30Legio tbl30Legio);
        void Save( );               
     }
}   

