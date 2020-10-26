using System.Linq;
using Atis.Domain.Models;      
   
// <!-- Interface Skriptdatum:  17.03.2014  12:32     -->  

namespace Atis.Domain.Interfaces     {         
    public interface ITbl15SubdivisionsRepository    {
        IQueryable<Tbl15Subdivision> Tbl15Subdivisions { get; }                   
        IQueryable<Tbl15Subdivision> FindAll();
        IQueryable<Tbl15Subdivision> FindAllSort();          
    
Tbl15Subdivision Get(int id);   
        

        void Add(Tbl15Subdivision tbl15Subdivision);
        void Delete(Tbl15Subdivision tbl15Subdivision);
        void Save( );               
     }
}   

