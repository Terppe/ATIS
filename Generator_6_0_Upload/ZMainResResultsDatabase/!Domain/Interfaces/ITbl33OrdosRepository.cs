using System.Linq;
using Atis.Domain.Models;      
   
// <!-- Interface Skriptdatum:  7.1.2012  10:32     -->  

namespace Atis.Domain.Interfaces     {         
    public interface ITbl33OrdosRepository    {
        IQueryable<Tbl33Ordo> Tbl33Ordos { get; }                   
        IQueryable<Tbl33Ordo> FindAll();
        IQueryable<Tbl33Ordo> FindAllSort();          
    
Tbl33Ordo Get(int id);   
        

        void Add(Tbl33Ordo tbl33Ordo);
        void Delete(Tbl33Ordo tbl33Ordo);
        void Save( );               
     }
}   

