using System.Linq;
using Atis.WpfUi.Model;      
   
// <!-- Interface Skriptdatum:  12.12.2019  12:32     -->  

namespace Atis.WpfUi.Interfaces     {         
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

