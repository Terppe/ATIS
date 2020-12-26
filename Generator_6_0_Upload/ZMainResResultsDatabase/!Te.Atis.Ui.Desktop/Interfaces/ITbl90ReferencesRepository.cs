using System.Linq;
using Atis.WpfUi.Model;      
   
// <!-- Interface Skriptdatum:  29.11.2018  10:32     -->  

namespace Atis.WpfUi.Interfaces     {         
    public interface ITbl90ReferencesRepository    {
        IQueryable<Tbl90Reference> Tbl90References { get; }                   
        IQueryable<Tbl90Reference> FindAll();
        IQueryable<Tbl90Reference> FindAllSort();          
    
Tbl90Reference Get(int id);   
        

        void Add(Tbl90Reference tbl90Reference);
        void Delete(Tbl90Reference tbl90Reference);
        void Save( );               
     }
}   

