using System.Linq;
using Atis.Domain.Models;      
   
// <!-- Interface Skriptdatum:  7.1.2012  10:32     -->  

namespace Atis.Domain.Interfaces     {         
    public interface ITbl48SubfamiliesRepository    {
        IQueryable<Tbl48Subfamily> Tbl48Subfamilies { get; }                   
        IQueryable<Tbl48Subfamily> FindAll();
        IQueryable<Tbl48Subfamily> FindAllSort();          
    
Tbl48Subfamily Get(int id);   
        

        void Add(Tbl48Subfamily tbl48Subfamily);
        void Delete(Tbl48Subfamily tbl48Subfamily);
        void Save( );               
     }
}   

