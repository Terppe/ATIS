using System.Linq;
using Atis.WpfUi.Model;      
   
// <!-- Interface Skriptdatum:  7.1.2012  10:32     -->  

namespace Atis.WpfUi.Interfaces     {         
    public interface ITbl60SubtribussesRepository    {
        IQueryable<Tbl60Subtribus> Tbl60Subtribusses { get; }                   
        IQueryable<Tbl60Subtribus> FindAll();
        IQueryable<Tbl60Subtribus> FindAllSort();          
    
Tbl60Subtribus Get(int id);   
        

        void Add(Tbl60Subtribus tbl60Subtribus);
        void Delete(Tbl60Subtribus tbl60Subtribus);
        void Save( );               
     }
}   

