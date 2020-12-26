using System.Linq;
using Atis.WpfUi.Model;      
   
// <!-- Interface Skriptdatum:  10.12.2020  10:32     -->  

namespace Atis.WpfUi.Interfaces     {         
    public interface ITbl33OrdosRepository    {
        IQueryable<Tbl33Ordo> Tbl33Ordos { get; }                   
        IQueryable<Tbl33Ordo> FindAll();
        IQueryable<Tbl33Ordo> FindAllSort();          
    
Tbl33Ordo Get(int id);   
        

        void Add(Tbl33Ordo ordo);
        void Delete(Tbl33Ordo ordo);
        void Save( );               
     }
}   

