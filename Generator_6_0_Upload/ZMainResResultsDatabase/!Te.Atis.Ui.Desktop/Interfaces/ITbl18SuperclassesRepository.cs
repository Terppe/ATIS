using System.Linq;
using Atis.WpfUi.Model;      
   
// <!-- Interface Skriptdatum:  12.12.2018  12:32     -->  

namespace Atis.WpfUi.Interfaces     {         
    public interface ITbl18SuperclassesRepository    {
        IQueryable<Tbl18Superclass> Tbl18Superclasses { get; }                   
        IQueryable<Tbl18Superclass> FindAll();
        IQueryable<Tbl18Superclass> FindAllSort();          
    
Tbl18Superclass Get(int id);   
        

        void Add(Tbl18Superclasstbl18Superclass);
        void Delete(Tbl18Superclasstbl18Superclass);
        void Save( );               
     }
}   

