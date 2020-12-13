using System.Linq;
using Atis.WpfUi.Model;      
   
// <!-- Interface Skriptdatum:  08.11.2018  10:32     -->  

namespace Atis.WpfUi.Interfaces     {         
    public interface ITbl54SupertribussesRepository    {
        IQueryable<Tbl54Supertribus> Tbl54Supertribusses { get; }                   
        IQueryable<Tbl54Supertribus> FindAll();
        IQueryable<Tbl54Supertribus> FindAllSort();          
    
Tbl54Supertribus Get(int id);   
        

        void Add(Tbl54Supertribus supertribus);
        void Delete(Tbl54Supertribus supertribus);
        void Save( );               
     }
}   

