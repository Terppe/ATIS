using System.Linq;
using Atis.WpfUi.Model;      
   
// <!-- Interface Skriptdatum:  20.12.2011  18:32     -->  

namespace Atis.WpfUi.Interfaces     {         
    public interface ITbl24SubclassesRepository    {
        IQueryable<Tbl24Subclass> Tbl24Subclasses { get; }                   
        IQueryable<Tbl24Subclass> FindAll();
        IQueryable<Tbl24Subclass> FindAllSort();          
    
Tbl24Subclass Get(int id);   
        

        void Add(Tbl24Subclass tbl24Subclass);
        void Delete(Tbl24Subclass tbl24Subclass);
        void Save( );               
     }
}   

