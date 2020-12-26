using System.Linq;
using Atis.WpfUi.Model;      
   
// <!-- Interface Skriptdatum:  7.1.2012  10:32     -->  

namespace Atis.WpfUi.Interfaces     {         
    public interface ITbl54SupertribussesRepository    {
        IQueryable<Tbl54Supertribus> Tbl54Supertribusses { get; }                   
        IQueryable<Tbl54Supertribus> FindAll();
        IQueryable<Tbl54Supertribus> FindAllSort();          
    
Tbl54Supertribus Get(int id);   
        

        void Add(Tbl54Supertribus tbl54Supertribus);
        void Delete(Tbl54Supertribus tbl54Supertribus);
        void Save( );               
     }
}   

