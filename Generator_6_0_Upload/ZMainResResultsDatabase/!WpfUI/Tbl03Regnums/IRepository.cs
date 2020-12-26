using System.Linq;
using Atis.WpfUi.Model;      
   
// <!-- Interface Skriptdatum:  14.03.2014  12:32       -->  

namespace Atis.WpfUi.Interfaces     {         
    public interface ITbl03RegnumsRepository    {
        IQueryable<Tbl03Regnum> Tbl03Regnums { get; }                   
        IQueryable<Tbl03Regnum> FindAll();
        IQueryable<Tbl03Regnum> FindAllSort();          
    
Tbl03Regnum Get(int id);   
        

        void Add(Tbl03Regnum tbl03Regnum);
        void Delete(Tbl03Regnum tbl03Regnum);
        void Save( );               
     }
}   

