using System.Linq;
using Atis.Domain.Models;      
   
// <!-- Interface Skriptdatum:  30.12.2011  18:32     -->  

namespace Atis.Domain.Interfaces     {         
    public interface ITbl27InfraclassesRepository    {
        IQueryable<Tbl27Infraclass> Tbl27Infraclasses { get; }                   
        IQueryable<Tbl27Infraclass> FindAll();
        IQueryable<Tbl27Infraclass> FindAllSort();          
    
Tbl27Infraclass Get(int id);   
        

        void Add(Tbl27Infraclass tbl27Infraclass);
        void Delete(Tbl27Infraclass tbl27Infraclass);
        void Save( );               
     }
}   

