using System.Linq;
using Atis.WpfUi.Model;      
   
// <!-- Interface Skriptdatum:  13.12.2020  10:32     -->  

namespace Atis.WpfUi.Interfaces     {         
    public interface ITbl63InfratribussesRepository    {
        IQueryable<Tbl63Infratribus> Tbl63Infratribusses { get; }                   
        IQueryable<Tbl63Infratribus> FindAll();
        IQueryable<Tbl63Infratribus> FindAllSort();          
    
Tbl63Infratribus Get(int id);   
        

        void Add(Tbl63Infratribus infratribus);
        void Delete(Tbl63Infratribus infratribus);
        void Save( );               
     }
}   

