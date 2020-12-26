using System.Linq;
using Atis.WpfUi.Model;      
   
// <!-- Interface Skriptdatum:  28.12.2011  10:32     -->  

namespace Atis.WpfUi.Interfaces     {         
    public interface ITbl78NamesRepository    {
        IQueryable<Tbl78Name> Tbl78Names { get; }                   
        IQueryable<Tbl78Name> FindAll();
        IQueryable<Tbl78Name> FindAllSort();          
    
Tbl78Name Get(int id);   
        

        void Add(Tbl78Name tbl78Name);
        void Delete(Tbl78Name tbl78Name);
        void Save( );               
     }
}   

