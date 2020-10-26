using System.Linq;
using Atis.Domain.Models;      
   
// <!-- Interface Skriptdatum:  7.1.2012  10:32     -->  

namespace Atis.Domain.Interfaces     {         
    public interface ITbl51InfrafamiliesRepository    {
        IQueryable<Tbl51Infrafamily> Tbl51Infrafamilies { get; }                   
        IQueryable<Tbl51Infrafamily> FindAll();
        IQueryable<Tbl51Infrafamily> FindAllSort();          
    
Tbl51Infrafamily Get(int id);   
        

        void Add(Tbl51Infrafamily tbl51Infrafamily);
        void Delete(Tbl51Infrafamily tbl51Infrafamily);
        void Save( );               
     }
}   

