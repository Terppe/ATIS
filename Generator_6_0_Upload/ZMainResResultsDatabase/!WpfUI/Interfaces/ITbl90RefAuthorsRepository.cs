using System.Linq;
using Atis.WpfUi.Model;      
   
// <!-- Interface Skriptdatum:  14.11.2017  10:32     -->  

namespace Atis.WpfUi.Interfaces     {         
    public interface ITbl90RefAuthorsRepository    {
        IQueryable<Tbl90RefAuthor> Tbl90RefAuthors { get; }                   
        IQueryable<Tbl90RefAuthor> FindAll();
        IQueryable<Tbl90RefAuthor> FindAllSort();          
    
Tbl90RefAuthor Get(int id);   
        

        void Add(Tbl90RefAuthor tbl90RefAuthor);
        void Delete(Tbl90RefAuthor tbl90RefAuthor);
        void Save( );               
     }
}   

