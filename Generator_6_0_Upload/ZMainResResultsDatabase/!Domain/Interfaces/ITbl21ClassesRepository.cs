using System.Linq;
using Atis.Domain.Models;      
   
// <!-- Interface Skriptdatum:  15.03.2012  18:32     -->  

namespace Atis.Domain.Interfaces     {         
    public interface ITbl21ClassesRepository    {
        IQueryable<Tbl21Class> Tbl21Classes { get; }                   
        IQueryable<Tbl21Class> FindAll();
        IQueryable<Tbl21Class> FindAllSort();          
    
Tbl21Class Get(int id);   
        

        void Add(Tbl21Class tbl21Class);
        void Delete(Tbl21Class tbl21Class);
        void Save( );               
     }
}   

