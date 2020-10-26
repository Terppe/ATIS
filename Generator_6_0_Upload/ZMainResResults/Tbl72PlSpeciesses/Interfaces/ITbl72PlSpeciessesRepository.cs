using System.Linq;
using Atis.WpfUi.Model;      
   
// <!-- Interface Skriptdatum:  13.12.2019  12:32     -->  

namespace Atis.WpfUi.Interfaces     {         
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

