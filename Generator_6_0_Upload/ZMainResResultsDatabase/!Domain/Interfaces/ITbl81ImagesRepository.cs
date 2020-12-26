using System.Linq;
using Atis.Domain.Models;      
   
// <!-- Interface Skriptdatum:  28.12.2011  10:32     -->  

namespace Atis.Domain.Interfaces     {         
    public interface ITbl81ImagesRepository    {
        IQueryable<Tbl81Image> Tbl81Images { get; }                   
        IQueryable<Tbl81Image> FindAll();
        IQueryable<Tbl81Image> FindAllSort();          
    
Tbl81Image Get(int id);   
        

        void Add(Tbl81Image tbl81Image);
        void Delete(Tbl81Image tbl81Image);
        void Save( );               
     }
}   

