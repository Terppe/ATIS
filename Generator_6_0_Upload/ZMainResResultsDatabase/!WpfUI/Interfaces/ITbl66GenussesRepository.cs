using System.Linq;
using Atis.WpfUi.Model;      
   
// <!-- Interface Skriptdatum:  23.12.2017  10:32     -->  

namespace Atis.WpfUi.Interfaces     {         
    public interface ITbl66GenussesRepository    {
        IQueryable<Tbl66Genus> Tbl66Genusses { get; }                   
        IQueryable<Tbl66Genus> FindAll();
        IQueryable<Tbl66Genus> FindAllSort();          
    
Tbl66Genus Get(int id);   
        

        void Add(Tbl66Genus tbl66Genus);
        void Delete(Tbl66Genus tbl66Genus);
        void Save( );               
     }
}   

