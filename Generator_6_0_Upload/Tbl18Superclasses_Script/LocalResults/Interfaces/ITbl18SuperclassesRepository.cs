using System.Linq;
using Atis.WpfUi.Model;      
   
// <!-- Interface Skriptdatum:  04.11.2020  12:32     -->  

namespace Atis.WpfUi.Interfaces     {         
    public interface ITbl18SuperclassesRepository    {
        IQueryable<Tbl18Superclass> Tbl18Superclasses { get; }                   
        IQueryable<Tbl18Superclass> FindAll();
        IQueryable<Tbl18Superclass> FindAllSort();          
    
Tbl18Superclass Get(int id);   
        

        void Add(Tbl18Superclass  superclass);
        void Delete(Tbl18Superclass  superclass);
        void Save( );               
     }
}   

