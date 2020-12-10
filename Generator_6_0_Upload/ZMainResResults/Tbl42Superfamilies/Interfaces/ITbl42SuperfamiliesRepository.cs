using System.Linq;
using Atis.WpfUi.Model;      
   
// <!-- Interface Skriptdatum:  10.12.2020  10:32     -->  

namespace Atis.WpfUi.Interfaces     {         
    public interface ITbl42SuperfamiliesRepository    {
        IQueryable<Tbl42Superfamily> Tbl42Superfamilies { get; }                   
        IQueryable<Tbl42Superfamily> FindAll();
        IQueryable<Tbl42Superfamily> FindAllSort();          
    
Tbl42Superfamily Get(int id);   
        

        void Add(Tbl42Superfamily superfamily);
        void Delete(Tbl42Superfamily superfamily);
        void Save( );               
     }
}   

