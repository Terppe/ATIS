using System.Linq;
using Atis.WpfUi.Model;      
   
// <!-- Interface Skriptdatum:  06.12.2020   18:32     -->  

namespace Atis.WpfUi.Interfaces     {         
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

