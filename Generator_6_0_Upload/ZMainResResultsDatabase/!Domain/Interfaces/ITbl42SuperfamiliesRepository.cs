using System.Linq;
using Atis.Domain.Models;      
   
// <!-- Interface Skriptdatum:  15.03.2012  10:32     -->  

namespace Atis.Domain.Interfaces     {         
    public interface ITbl42SuperfamiliesRepository    {
        IQueryable<Tbl42Superfamily> Tbl42Superfamilies { get; }                   
        IQueryable<Tbl42Superfamily> FindAll();
        IQueryable<Tbl42Superfamily> FindAllSort();          
    
Tbl42Superfamily Get(int id);   
        

        void Add(Tbl42Superfamily tbl42Superfamily);
        void Delete(Tbl42Superfamily tbl42Superfamily);
        void Save( );               
     }
}   

