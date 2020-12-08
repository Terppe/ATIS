using System.Linq;
using Atis.WpfUi.Model;      
   
// <!-- Interface Skriptdatum:  19.03.2014  12:32     -->  

namespace Atis.WpfUi.Interfaces     {         
    public interface ITbl18SuperclassesRepository    {
        IQueryable<Tbl18Superclass> Tbl18Superclasses { get; }                   
        IQueryable<Tbl18Superclass> FindAll();
        IQueryable<Tbl18Superclass> FindAllSort();          
    
Tbl18Superclass Get(int id);   
        

        void Add(Tbl18Superclass tbl18Superclass);
        void Delete(Tbl18Superclass tbl18Superclass);
        void Save( );               
     }
}   

