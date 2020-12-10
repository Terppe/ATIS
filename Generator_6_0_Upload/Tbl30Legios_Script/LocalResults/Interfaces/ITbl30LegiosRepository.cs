using System.Linq;
using Atis.WpfUi.Model;      
   
// <!-- Interface Skriptdatum:  10.12.2020  10:32     -->  

namespace Atis.WpfUi.Interfaces     {         
    public interface ITbl30LegiosRepository    {
        IQueryable<Tbl30Legio> Tbl30Legios { get; }                   
        IQueryable<Tbl30Legio> FindAll();
        IQueryable<Tbl30Legio> FindAllSort();          
    
Tbl30Legio Get(int id);   
        

        void Add(Tbl30Legio legio);
        void Delete(Tbl30Legio legio);
        void Save( );               
     }
}   

