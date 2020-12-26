using System.Linq;
using Atis.WpfUi.Model;      
   
// <!-- Interface Skriptdatum:  08.11.2018  10:32     -->  

namespace Atis.WpfUi.Interfaces     {         
    public interface ITbl51InfrafamiliesRepository    {
        IQueryable<Tbl51Infrafamily> Tbl51Infrafamilies { get; }                   
        IQueryable<Tbl51Infrafamily> FindAll();
        IQueryable<Tbl51Infrafamily> FindAllSort();          
    
Tbl51Infrafamily Get(int id);   
        

        void Add(Tbl51Infrafamily infrafamily);
        void Delete(Tbl51Infrafamily infrafamily);
        void Save( );               
     }
}   

