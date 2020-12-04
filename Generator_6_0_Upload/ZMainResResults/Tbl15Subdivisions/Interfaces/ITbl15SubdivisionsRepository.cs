using System.Linq;
using Atis.WpfUi.Model;      
   
// <!-- Interface Skriptdatum:  04.11.2020  12:32     -->  

namespace Atis.WpfUi.Interfaces     {         
    public interface ITbl15SubdivisionsRepository    {
        IQueryable<Tbl15Subdivision> Tbl15Subdivisions { get; }                   
        IQueryable<Tbl15Subdivision> FindAll();
        IQueryable<Tbl15Subdivision> FindAllSort();          
    
Tbl15Subdivision Get(int id);   
        

        void Add(Tbl15Subdivision subdivision);
        void Delete(Tbl15Subdivision subdivision);
        void Save( );               
     }
}   

