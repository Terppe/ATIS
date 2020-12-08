using System.Linq;
using Atis.Domain.Models;      
   
// <!-- Interface Skriptdatum:  15.03.2012  10:32     -->  

namespace Atis.Domain.Interfaces     {         
    public interface ITbl72PlSpeciessesRepository    {
        IQueryable<Tbl72PlSpecies> Tbl72PlSpeciesses { get; }                   
        IQueryable<Tbl72PlSpecies> FindAll();
        IQueryable<Tbl72PlSpecies> FindAllSort();          
    
Tbl72PlSpecies Get(int id);   
        

        void Add(Tbl72PlSpecies tbl72PlSpecies);
        void Delete(Tbl72PlSpecies tbl72PlSpecies);
        void Save( );               
     }
}   

