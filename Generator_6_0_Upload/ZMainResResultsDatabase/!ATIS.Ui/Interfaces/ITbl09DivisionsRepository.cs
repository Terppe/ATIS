using System.Linq;
using Atis.WpfUi.Model;      
   
// <!-- Interface Skriptdatum:  04.11.2020  12:32     -->  

namespace Atis.WpfUi.Interfaces     {         
    public interface ITbl09DivisionsRepository    {
        IQueryable<Tbl09Division> Tbl09Divisions { get; }                   
        IQueryable<Tbl09Division> FindAll();
        IQueryable<Tbl09Division> FindAllSort();          
    
Tbl09Division Get(int id);   
        

        void Add(Tbl09Division tbl09Division);
        void Delete(Tbl09Division tbl09Division);
        void Save( );               
     }
}   

